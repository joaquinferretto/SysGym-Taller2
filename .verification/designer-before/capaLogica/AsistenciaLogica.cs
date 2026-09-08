using System;
using System.Collections.Generic;
using System.Linq;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaDatos.Repositorios;

namespace exxen2._0.capaLogica
{
    public class AsistenciaLogica
    {
        public Asistencia Registrar(Asistencia asistencia)
        {
            if (asistencia == null)
            {
                throw new ArgumentNullException("asistencia");
            }

            using (var context = new GymUnidadDeTrabajo())
            using (var transaction = context.IniciarTransaccion())
            {
                var socio = context.Socios.Find(asistencia.IdSocio);
                if (socio == null || !socio.Estado)
                {
                    throw new InvalidOperationException("El socio no existe o está inactivo.");
                }

                var membresia = context.Membresias.Where(m => m.IdSocio == asistencia.IdSocio)
                    .OrderByDescending(m => m.FechaInicio).FirstOrDefault();
                if (membresia == null)
                {
                    throw new InvalidOperationException("El socio no posee una membresía.");
                }

                MembresiaLogica.ActualizarEstadoPorDeudaEnContexto(context, membresia.IdMembresia);
                if (!membresia.Estado)
                {
                    throw new InvalidOperationException("La membresía no está habilitada.");
                }

                var fecha = asistencia.Fecha == default(DateTime) ? DateTime.Now : asistencia.Fecha;
                var cuota = context.CuotasMembresia
                    .Where(c => c.IdMembresia == membresia.IdMembresia
                        && c.FechaDesde <= fecha && c.FechaHasta >= fecha
                        && c.EstadoPago != EstadosCuota.Anulada)
                    .SingleOrDefault();
                if (cuota == null || cuota.EstadoPago != EstadosCuota.Pagada)
                {
                    throw new InvalidOperationException("No existe una cuota pagada correspondiente a la fecha.");
                }

                asistencia.Fecha = fecha;
                asistencia.Estado = true;
                context.Asistencias.Add(asistencia);
                context.GuardarCambios();
                transaction.Confirmar();
                return asistencia;
            }
        }

        public List<Asistencia> ListarPorSocio(int idSocio)
        {
            using (var context = new GymUnidadDeTrabajo())
            {
                return context.Asistencias.Where(a => a.IdSocio == idSocio && a.Estado)
                    .OrderByDescending(a => a.Fecha).ToList();
            }
        }

        public List<Asistencia> ListarPorFecha(DateTime fecha)
        {
            var inicio = fecha.Date;
            var fin = inicio.AddDays(1);
            using (var context = new GymUnidadDeTrabajo())
            {
                return context.Asistencias.Consultar("Socio")
                    .Where(a => a.Estado && a.Fecha >= inicio && a.Fecha < fin)
                    .OrderBy(a => a.Fecha).ToList();
            }
        }

        public List<Asistencia> ListarPorFechaParaGestion(DateTime fecha)
        {
            var inicio = fecha.Date;
            var fin = inicio.AddDays(1);
            using (var context = new GymUnidadDeTrabajo())
            {
                return context.Asistencias.Consultar("Socio")
                    .Where(a => a.Fecha >= inicio && a.Fecha < fin)
                    .OrderByDescending(a => a.Estado).ThenBy(a => a.Fecha).ToList();
            }
        }

        public void DarDeBaja(int idAsistencia)
        {
            using (var context = new GymUnidadDeTrabajo())
            {
                var asistencia = context.Asistencias.Find(idAsistencia);
                if (asistencia == null)
                {
                    throw new InvalidOperationException("La asistencia no existe.");
                }

                asistencia.Estado = false;
                context.GuardarCambios();
            }
        }

        public void Reactivar(int idAsistencia)
        {
            using (var context = new GymUnidadDeTrabajo())
            {
                var asistencia = context.Asistencias.Find(idAsistencia);
                if (asistencia == null)
                {
                    throw new InvalidOperationException("La asistencia no existe.");
                }

                asistencia.Estado = true;
                context.GuardarCambios();
            }
        }
    }
}
