using System;
using System.IO;
using exxen2._0.capaDatos.Servicios;
using exxen2._0.capaDatos.Repositorios;
using exxen2._0.capaLogica.Auditoria;

namespace exxen2._0.capaLogica
{
    public sealed class RestauracionCompletadaException : Exception
    {
        public RestauracionCompletadaException(Exception innerException)
            : base("La base se restauró, pero no se pudo registrar la auditoría de la operación.", innerException) { }
    }

    /* Restringe las copias y restauraciones a un Administrador y registra las operaciones completadas. */
    public sealed class CopiaSeguridadLogica
    {
        private readonly AuditoriaLogica auditoria;

        public CopiaSeguridadLogica(int idUsuarioAutenticado)
        {
            auditoria = new AuditoriaLogica(idUsuarioAutenticado);
        }

        public void ValidarAcceso()
        {
            auditoria.ValidarAcceso();
        }

        public string ObtenerCarpetaRespaldoSql()
        {
            ValidarAcceso();
            return AdministracionBaseDatos.ObtenerCarpetaRespaldoSql();
        }

        public DateTime? ObtenerFechaUltimaCopia()
        {
            ValidarAcceso();
            return AdministracionBaseDatos.ObtenerFechaUltimaCopia();
        }

        public void CrearCopia(string destino)
        {
            ValidarAcceso();
            if (string.IsNullOrWhiteSpace(destino))
                throw new ArgumentException("Seleccioná dónde guardar la copia.", "destino");

            string temporal = null;
            try
            {
                temporal = AdministracionBaseDatos.CrearCopiaTemporal(destino);
                try
                {
                    AdministracionBaseDatos.CopiarCopia(temporal, destino);
                }
                catch (UnauthorizedAccessException ex)
                {
                    throw new InvalidOperationException("No hay permisos para guardar la copia en la carpeta seleccionada.", ex);
                }
                catch (IOException ex)
                {
                    throw new InvalidOperationException("No se pudo guardar la copia. Verificá los permisos y el espacio disponible en la carpeta elegida.", ex);
                }

                RegistrarOperacion(AuditoriaLogica.CrearBackup, "Copia guardada en " + destino);
            }
            finally
            {
                AdministracionBaseDatos.EliminarCopiaTemporal(temporal);
            }
        }

        public void RestaurarCopia(string ruta)
        {
            ValidarAcceso();
            AdministracionBaseDatos.RestaurarCopia(ruta);
            try
            {
                RegistrarOperacion(AuditoriaLogica.RestaurarBackup, "Base restaurada desde " + ruta);
            }
            catch (Exception ex)
            {
                throw new RestauracionCompletadaException(ex);
            }
        }

        private void RegistrarOperacion(string operacion, string detalle)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            using (var transaccion = datos.IniciarTransaccion())
            {
                auditoria.RegistrarOperacion(datos, operacion, "BaseDatos", 1, detalle);
                datos.GuardarCambios();
                transaccion.Confirmar();
            }
        }

    }
}
