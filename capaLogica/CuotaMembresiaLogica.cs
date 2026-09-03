using System;
using System.Collections.Generic;
using System.Linq;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaDatos.Repositorios;

namespace exxen2._0.capaLogica
{
    public sealed class EstadoCuentaMembresia
    {
        public int IdMembresia { get; set; }
        public string Socio { get; set; }
        public string DNI { get; set; }
        public string Plan { get; set; }
        public DateTime? UltimaCuotaDesde { get; set; }
        public DateTime? UltimaCuotaHasta { get; set; }
        public string EstadoUltimaCuota { get; set; }
        public decimal SaldoPendiente { get; set; }
        public string Situacion { get; set; }
        public bool AlDia { get; set; }
        public bool TieneDeuda { get; set; }
    }

    public class CuotaMembresiaLogica
    {
        public CuotaMembresia CrearPrimeraCuota(int idMembresia)
        {
            using (var context = new GymUnidadDeTrabajo())
            using (var transaction = context.IniciarTransaccion())
            {
                var membresia = context.Membresias.Consultar("Plan")
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
                context.GuardarCambios();
                transaction.Confirmar();
                return cuota;
            }
        }

        public CuotaMembresia GenerarSiguienteCuota(int idMembresia)
        {
            using (var context = new GymUnidadDeTrabajo())
            using (var transaction = context.IniciarTransaccion())
            {
                var membresia = context.Membresias.Consultar("Plan")
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

                context.GuardarCambios();
                transaction.Confirmar();
                return cuota;
            }
        }

        public CuotaMembresia ObtenerPorId(int idCuotaMembresia)
        {
            using (var context = new GymUnidadDeTrabajo())
            {
                return context.CuotasMembresia.Consultar("Membresia", "Pago")
                    .SingleOrDefault(c => c.IdCuotaMembresia == idCuotaMembresia);
            }
        }

        public CuotaMembresia ObtenerCuotaActual(int idMembresia)
        {
            var hoy = DateTime.Today;
            using (var context = new GymUnidadDeTrabajo())
            {
                return context.CuotasMembresia.Consultar("Pago")
                    .Where(c => c.IdMembresia == idMembresia
                        && c.EstadoPago != EstadosCuota.Anulada
                        && c.FechaDesde <= hoy && c.FechaHasta >= hoy)
                    .SingleOrDefault();
            }
        }

        public List<CuotaMembresia> ListarPorMembresia(int idMembresia)
        {
            using (var context = new GymUnidadDeTrabajo())
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

        public List<CuotaMembresia> ListarParaGestion()
        {
            using (var context = new GymUnidadDeTrabajo())
            {
                return context.CuotasMembresia.Consultar(
                        "Pago", "Membresia.Socio", "Membresia.Plan")
                    .Where(c => c.EstadoPago == EstadosCuota.Pendiente
                        || c.EstadoPago == EstadosCuota.Pagada
                        || c.EstadoPago == EstadosCuota.Anulada)
                    .OrderByDescending(c => c.FechaDesde)
                    .ThenBy(c => c.Membresia.Socio.Apellido)
                    .ThenBy(c => c.Membresia.Socio.Nombre)
                    .ToList();
            }
        }

        public List<EstadoCuentaMembresia> ListarEstadoCuentas()
        {
            using (var context = new GymUnidadDeTrabajo())
            {
                var membresias = context.Membresias.Consultar(
                        "Socio", "Plan", "Cuotas.Pago")
                    .OrderBy(m => m.Socio.Apellido)
                    .ThenBy(m => m.Socio.Nombre)
                    .ToList();

                return membresias.Select(CrearEstadoCuenta).ToList();
            }
        }

        private static EstadoCuentaMembresia CrearEstadoCuenta(Membresia membresia)
        {
            var hoy = DateTime.Today;
            var cuotasValidas = membresia.Cuotas
                .Where(c => c.EstadoPago != EstadosCuota.Anulada)
                .OrderBy(c => c.FechaDesde).ToList();
            var ultima = cuotasValidas.LastOrDefault();
            var pendientes = cuotasValidas
                .Where(c => c.EstadoPago == EstadosCuota.Pendiente).ToList();
            var tieneVencida = pendientes.Any(c => c.FechaHasta.Date < hoy);
            var faltaPeriodoActual = ultima != null && ultima.FechaHasta.Date < hoy;
            var saldo = pendientes.Sum(CalcularSaldoSinContexto);

            string situacion;
            bool alDia;
            bool tieneDeuda;
            if (ultima == null)
            {
                situacion = "Sin cuota generada";
                alDia = false;
                tieneDeuda = true;
            }
            else if (tieneVencida)
            {
                situacion = "Con deuda vencida";
                alDia = false;
                tieneDeuda = true;
            }
            else if (pendientes.Count > 0)
            {
                situacion = "Pago pendiente";
                alDia = false;
                tieneDeuda = true;
            }
            else if (faltaPeriodoActual)
            {
                situacion = "Falta generar nueva cuota";
                alDia = false;
                tieneDeuda = true;
            }
            else if (!membresia.Estado)
            {
                situacion = "Membresía inhabilitada";
                alDia = false;
                tieneDeuda = false;
            }
            else
            {
                situacion = "Al día";
                alDia = true;
                tieneDeuda = false;
            }

            return new EstadoCuentaMembresia
            {
                IdMembresia = membresia.IdMembresia,
                Socio = membresia.Socio == null
                    ? "Socio no disponible"
                    : membresia.Socio.Apellido + ", " + membresia.Socio.Nombre,
                DNI = membresia.Socio == null ? "-" : membresia.Socio.DNI,
                Plan = membresia.Plan == null ? "-" : membresia.Plan.Nombre,
                UltimaCuotaDesde = ultima == null ? (DateTime?)null : ultima.FechaDesde,
                UltimaCuotaHasta = ultima == null ? (DateTime?)null : ultima.FechaHasta,
                EstadoUltimaCuota = ultima == null ? "Sin cuota" : ultima.EstadoPago,
                SaldoPendiente = saldo,
                Situacion = situacion,
                AlDia = alDia,
                TieneDeuda = tieneDeuda
            };
        }

        private static decimal CalcularSaldoSinContexto(CuotaMembresia cuota)
        {
            var abonado = cuota.IdRegistroPago.HasValue
                && cuota.Pago != null
                && cuota.Pago.Estado == EstadosTransaccionPago.Aprobado
                ? cuota.Pago.Importe : 0m;
            return Math.Max(0m, cuota.Importe - abonado);
        }

        public void ReactivarCuota(int idCuotaMembresia)
        {
            using (var context = new GymUnidadDeTrabajo())
            using (var transaction = context.IniciarTransaccion())
            {
                var cuota = context.CuotasMembresia.Consultar("Pago")
                    .SingleOrDefault(c => c.IdCuotaMembresia == idCuotaMembresia);
                if (cuota == null)
                {
                    throw new InvalidOperationException("La cuota no existe.");
                }

                if (cuota.EstadoPago != EstadosCuota.Anulada)
                {
                    throw new InvalidOperationException("La cuota no está anulada.");
                }

                cuota.EstadoPago = EstadosCuota.Pendiente;
                RecalcularEstadoPagoEnContexto(context, cuota);
                MembresiaLogica.ActualizarEstadoPorDeudaEnContexto(context, cuota.IdMembresia);
                context.GuardarCambios();
                transaction.Confirmar();
            }
        }

        public void AnularCuota(int idCuotaMembresia)
        {
            using (var context = new GymUnidadDeTrabajo())
            using (var transaction = context.IniciarTransaccion())
            {
                var cuota = context.CuotasMembresia.Consultar("Pago")
                    .SingleOrDefault(c => c.IdCuotaMembresia == idCuotaMembresia);
                if (cuota == null)
                {
                    throw new InvalidOperationException("La cuota no existe.");
                }

                cuota.EstadoPago = EstadosCuota.Anulada;
                MembresiaLogica.ActualizarEstadoPorDeudaEnContexto(context, cuota.IdMembresia);
                context.GuardarCambios();
                transaction.Confirmar();
            }
        }

        public bool EstaVencida(CuotaMembresia cuota)
        {
            return cuota != null && cuota.EstadoPago == EstadosCuota.Pendiente
                && cuota.FechaHasta.Date < DateTime.Today;
        }

        public decimal CalcularSaldo(int idCuotaMembresia)
        {
            using (var context = new GymUnidadDeTrabajo())
            {
                var cuota = context.CuotasMembresia.Consultar("Pago")
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

            using (var context = new GymUnidadDeTrabajo())
            {
                var cuotaActual = context.CuotasMembresia.Consultar("Pago")
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
            using (var context = new GymUnidadDeTrabajo())
            using (var transaction = context.IniciarTransaccion())
            {
                var cuota = context.CuotasMembresia.Consultar("Pago")
                    .SingleOrDefault(c => c.IdCuotaMembresia == idCuotaMembresia);
                if (cuota == null)
                {
                    throw new InvalidOperationException("La cuota no existe.");
                }

                RecalcularEstadoPagoEnContexto(context, cuota);
                MembresiaLogica.ActualizarEstadoPorDeudaEnContexto(context, cuota.IdMembresia);
                context.GuardarCambios();
                transaction.Confirmar();
            }
        }

        internal static CuotaMembresia CrearPrimeraCuotaEnContexto(IUnidadDeTrabajo context, Membresia membresia, Plan plan)
        {
            if (membresia == null || plan == null)
            {
                throw new InvalidOperationException("La membresía y el plan son obligatorios para crear la cuota.");
            }

            ValidarPlanActivo(plan);
            return CrearCuotaEnContexto(context, membresia, plan, membresia.FechaInicio);
        }

        internal static void RecalcularEstadoPagoEnContexto(IUnidadDeTrabajo context, CuotaMembresia cuota)
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

        internal static decimal CalcularSaldoEnContexto(IUnidadDeTrabajo context, CuotaMembresia cuota)
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

        private static CuotaMembresia CrearCuotaEnContexto(IUnidadDeTrabajo context, Membresia membresia, Plan plan, DateTime periodoDesde)
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
            using (var context = new GymUnidadDeTrabajo())
            {
                return context.CuotasMembresia.Consultar(
                        "Pago", "Membresia.Socio", "Membresia.Plan")
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
