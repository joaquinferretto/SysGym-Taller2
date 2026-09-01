using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using exxen2._0.capaDatos.Contexto;
using exxen2._0.capaDatos.Entidades;

namespace exxen2._0.capaLogica
{
    public class CuotaMembresiaLogica
    {
        public CuotaMembresia CrearPrimeraCuota(int idMembresia)
        {
            using (var context = new GymContext())
            using (var transaction = context.Database.BeginTransaction())
            {
                var membresia = context.Membresias.Include(m => m.Plan)
                    .SingleOrDefault(m => m.IdMembresia == idMembresia);
                if (membresia == null)
                {
                    throw new InvalidOperationException("La membresía no existe.");
                }

                if (context.CuotasMembresia.Any(c => c.IdMembresia == idMembresia))
                {
                    throw new InvalidOperationException("La membresía ya posee una cuota.");
                }

                var cuota = CrearPrimeraCuotaEnContexto(context, membresia, membresia.Plan);
                context.SaveChanges();
                transaction.Commit();
                return cuota;
            }
        }

        public CuotaMembresia GenerarSiguienteCuota(int idMembresia)
        {
            using (var context = new GymContext())
            using (var transaction = context.Database.BeginTransaction())
            {
                var membresia = context.Membresias.Include(m => m.Plan)
                    .SingleOrDefault(m => m.IdMembresia == idMembresia);
                if (membresia == null)
                {
                    throw new InvalidOperationException("La membresía no existe.");
                }

                var ultima = context.CuotasMembresia
                    .Where(c => c.IdMembresia == idMembresia && c.EstadoPago != EstadosCuota.Anulada)
                    .OrderByDescending(c => c.FechaHasta).FirstOrDefault();

                CuotaMembresia cuota;
                if (ultima == null)
                {
                    cuota = CrearPrimeraCuotaEnContexto(context, membresia, membresia.Plan);
                }
                else
                {
                    var plan = context.Planes.Find(membresia.IdPlan);
                    ValidarPlanActivo(plan);
                    var desde = ultima.FechaHasta.AddDays(1);
                    cuota = CrearCuotaEnContexto(context, membresia, plan, desde);
                }

                context.SaveChanges();
                transaction.Commit();
                return cuota;
            }
        }

        public CuotaMembresia ObtenerPorId(int idCuotaMembresia)
        {
            using (var context = new GymContext())
            {
                return context.CuotasMembresia.Include(c => c.Membresia)
                    .Include(c => c.Pago).SingleOrDefault(c => c.IdCuotaMembresia == idCuotaMembresia);
            }
        }

        public CuotaMembresia ObtenerCuotaActual(int idMembresia)
        {
            var hoy = DateTime.Today;
            using (var context = new GymContext())
            {
                return context.CuotasMembresia.Include(c => c.Pago)
                    .Where(c => c.IdMembresia == idMembresia
                        && c.EstadoPago != EstadosCuota.Anulada
                        && c.FechaDesde <= hoy && c.FechaHasta >= hoy)
                    .SingleOrDefault();
            }
        }

        public List<CuotaMembresia> ListarPorMembresia(int idMembresia)
        {
            using (var context = new GymContext())
            {
                return context.CuotasMembresia
                    .Where(c => c.IdMembresia == idMembresia)
                    .OrderBy(c => c.FechaDesde).ToList();
            }
        }

        public List<CuotaMembresia> ListarPendientes()
        {
            return ListarPorEstado(EstadosCuota.Pendiente);
        }

        public List<CuotaMembresia> ListarPagadas()
        {
            return ListarPorEstado(EstadosCuota.Pagada);
        }

        public List<CuotaMembresia> ListarAnuladas()
        {
            return ListarPorEstado(EstadosCuota.Anulada);
        }

        public void AnularCuota(int idCuotaMembresia)
        {
            using (var context = new GymContext())
            using (var transaction = context.Database.BeginTransaction())
            {
                var cuota = context.CuotasMembresia.Include(c => c.Pago)
                    .SingleOrDefault(c => c.IdCuotaMembresia == idCuotaMembresia);
                if (cuota == null)
                {
                    throw new InvalidOperationException("La cuota no existe.");
                }

                cuota.EstadoPago = EstadosCuota.Anulada;
                MembresiaLogica.ActualizarEstadoPorDeudaEnContexto(context, cuota.IdMembresia);
                context.SaveChanges();
                transaction.Commit();
            }
        }

        public bool EstaVencida(CuotaMembresia cuota)
        {
            return cuota != null && cuota.EstadoPago == EstadosCuota.Pendiente
                && cuota.FechaHasta.Date < DateTime.Today;
        }

        public decimal CalcularSaldo(int idCuotaMembresia)
        {
            using (var context = new GymContext())
            {
                var cuota = context.CuotasMembresia.Include(c => c.Pago)
                    .SingleOrDefault(c => c.IdCuotaMembresia == idCuotaMembresia);
                if (cuota == null)
                {
                    throw new InvalidOperationException("La cuota no existe.");
                }

                return CalcularSaldoEnContexto(context, cuota);
            }
        }

        public decimal CalcularSaldo(CuotaMembresia cuota)
        {
            if (cuota == null)
            {
                throw new ArgumentNullException("cuota");
            }

            using (var context = new GymContext())
            {
                var cuotaActual = context.CuotasMembresia.Include(c => c.Pago)
                    .SingleOrDefault(c => c.IdCuotaMembresia == cuota.IdCuotaMembresia);
                if (cuotaActual == null)
                {
                    throw new InvalidOperationException("La cuota no existe.");
                }

                return CalcularSaldoEnContexto(context, cuotaActual);
            }
        }

        public void RecalcularEstadoPago(int idCuotaMembresia)
        {
            using (var context = new GymContext())
            using (var transaction = context.Database.BeginTransaction())
            {
                var cuota = context.CuotasMembresia.Include(c => c.Pago)
                    .SingleOrDefault(c => c.IdCuotaMembresia == idCuotaMembresia);
                if (cuota == null)
                {
                    throw new InvalidOperationException("La cuota no existe.");
                }

                RecalcularEstadoPagoEnContexto(context, cuota);
                MembresiaLogica.ActualizarEstadoPorDeudaEnContexto(context, cuota.IdMembresia);
                context.SaveChanges();
                transaction.Commit();
            }
        }

        internal static CuotaMembresia CrearPrimeraCuotaEnContexto(GymContext context, Membresia membresia, Plan plan)
        {
            if (membresia == null || plan == null)
            {
                throw new InvalidOperationException("La membresía y el plan son obligatorios para crear la cuota.");
            }

            ValidarPlanActivo(plan);
            return CrearCuotaEnContexto(context, membresia, plan, membresia.FechaInicio);
        }

        internal static void RecalcularEstadoPagoEnContexto(GymContext context, CuotaMembresia cuota)
        {
            if (cuota.EstadoPago == EstadosCuota.Anulada)
            {
                return;
            }

            var totalAprobado = cuota.IdRegistroPago.HasValue
                && cuota.Pago != null
                && cuota.Pago.Estado == EstadosTransaccionPago.Aprobado
                ? cuota.Pago.Importe : 0m;
            cuota.EstadoPago = totalAprobado >= cuota.Importe
                ? EstadosCuota.Pagada : EstadosCuota.Pendiente;
        }

        internal static decimal CalcularSaldoEnContexto(GymContext context, CuotaMembresia cuota)
        {
            if (cuota.EstadoPago == EstadosCuota.Anulada)
            {
                return 0m;
            }

            var totalAprobado = cuota.IdRegistroPago.HasValue
                && cuota.Pago != null
                && cuota.Pago.Estado == EstadosTransaccionPago.Aprobado
                ? cuota.Pago.Importe : 0m;
            return Math.Max(0m, cuota.Importe - totalAprobado);
        }

        private static CuotaMembresia CrearCuotaEnContexto(GymContext context, Membresia membresia, Plan plan, DateTime periodoDesde)
        {
            var cuota = new CuotaMembresia
            {
                IdMembresia = membresia.IdMembresia,
                FechaDesde = periodoDesde,
                FechaHasta = CalcularPeriodoHasta(periodoDesde),
                Importe = plan.Precio,
                EstadoPago = EstadosCuota.Pendiente
            };
            context.CuotasMembresia.Add(cuota);
            return cuota;
        }

        public static DateTime CalcularPeriodoHasta(DateTime periodoDesde)
        {
            return periodoDesde.AddMonths(1).AddDays(-1);
        }

        private List<CuotaMembresia> ListarPorEstado(string estado)
        {
            using (var context = new GymContext())
            {
                return context.CuotasMembresia.Include(c => c.Pago)
                    .Where(c => c.EstadoPago == estado)
                    .OrderBy(c => c.FechaDesde).ToList();
            }
        }

        private static void ValidarPlanActivo(Plan plan)
        {
            if (plan == null || !plan.Estado)
            {
                throw new InvalidOperationException("El plan actual no existe o está inactivo.");
            }
        }
    }
}
