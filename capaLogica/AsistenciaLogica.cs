using System;
using System.Collections.Generic;
using System.Linq;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaDatos.Repositorios;

namespace exxen2._0.capaLogica
{
    /* Coordina las operaciones y validaciones de negocio de asistencias. */
    public class AsistenciaLogica
    {
        /* Valida socio, membresía y cuota pagada para registrar el ingreso, incluyendo todo el día de vencimiento. */
        public Asistencia Registrar(Asistencia asistencia)
        {
            if (asistencia == null)
            {
                throw new ArgumentNullException("asistencia");
            }

            using (var datos = new UnidadDeTrabajoGimnasio())
            using (var transaccion = datos.IniciarTransaccion())
            {
                var socio = datos.Socios.Buscar(asistencia.IdSocio);
                if (socio == null || !socio.Estado)
                {
                    throw new InvalidOperationException("El socio no existe o está inactivo.");
                }

                var membresia = datos.Membresias.Where(m => m.IdSocio == asistencia.IdSocio).OrderByDescending(m => m.FechaInicio).FirstOrDefault();
                if (membresia == null)
                {
                    throw new InvalidOperationException("El socio no posee una membresía.");
                }

                MembresiaLogica.ActualizarEstadoPorDeudaEnContexto(datos, membresia.IdMembresia);
                if (!membresia.Estado)
                {
                    throw new InvalidOperationException("La membresía no está habilitada.");
                }

                var fecha = asistencia.Fecha == default(DateTime) ? DateTime.Now : asistencia.Fecha;
                var inicioDia = fecha.Date;
                var siguienteDia = inicioDia.AddDays(1);
                var cuota = datos.CuotasMembresia.Where(c => c.IdMembresia == membresia.IdMembresia && c.FechaDesde < siguienteDia && c.FechaHasta >= inicioDia && c.EstadoPago != EstadosCuota.Anulada).SingleOrDefault();
                if (cuota == null || cuota.EstadoPago != EstadosCuota.Pagada)
                {
                    throw new InvalidOperationException("No existe una cuota pagada correspondiente a la fecha.");
                }

                asistencia.Fecha = fecha;
                asistencia.Estado = true;
                datos.Asistencias.Agregar(asistencia);
                datos.GuardarCambios();
                transaccion.Confirmar();
                return asistencia;
            }
        }

        /* Consulta asistencias del socio indicado para devolver los datos a la capa visual. */
        public List<Asistencia> ListarPorSocio(int idSocio)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                return datos.Asistencias.ConsultarSoloLectura().Where(a => a.IdSocio == idSocio && a.Estado).OrderByDescending(a => a.Fecha).ToList();
            }
        }

        /* Consulta asistencias del día indicado para devolver los datos a la capa visual. */
        public List<Asistencia> ListarPorFecha(DateTime fecha)
        {
            var inicio = fecha.Date;
            var fin = inicio.AddDays(1);
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                return datos.Asistencias.ConsultarSoloLectura("Socio").Where(a => a.Estado && a.Fecha >= inicio && a.Fecha < fin).OrderBy(a => a.Fecha).ToList();
            }
        }

        /* Consulta asistencias del día indicado, incluyendo bajas para devolver los datos a la capa visual. */
        public List<Asistencia> ListarPorFechaParaGestion(DateTime fecha)
        {
            var inicio = fecha.Date;
            var fin = inicio.AddDays(1);
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                return datos.Asistencias.ConsultarSoloLectura("Socio").Where(a => a.Fecha >= inicio && a.Fecha < fin).OrderByDescending(a => a.Estado).ThenBy(a => a.Fecha).ToList();
            }
        }

        /* Desactiva el registro de asistencias sin eliminar su historial. */
        public void DarDeBaja(int idAsistencia)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                var asistencia = datos.Asistencias.Buscar(idAsistencia);
                if (asistencia == null)
                {
                    throw new InvalidOperationException("La asistencia no existe.");
                }

                asistencia.Estado = false;
                datos.GuardarCambios();
            }
        }

        /* Recupera el estado activo del registro de asistencias según las validaciones de la operación. */
        public void Reactivar(int idAsistencia)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                var asistencia = datos.Asistencias.Buscar(idAsistencia);
                if (asistencia == null)
                {
                    throw new InvalidOperationException("La asistencia no existe.");
                }

                asistencia.Estado = true;
                datos.GuardarCambios();
            }
        }
    }
}
