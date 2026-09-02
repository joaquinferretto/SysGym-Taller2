using System;
using System.Collections.Generic;
using System.Linq;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaDatos.Repositorios;

namespace exxen2._0.capaLogica
{
    public class PagoLogica
    {
        public Pago RegistrarPago(Pago pago, int idCuotaMembresia)
        {
            ValidarDatos(pago);
            if (idCuotaMembresia <= 0)
            {
                throw new InvalidOperationException("La cuota es obligatoria.");
            }

            using (var context = new GymUnidadDeTrabajo())
            using (var transaction = context.IniciarTransaccion())
            {
                var cuota = context.CuotasMembresia.Consultar("Pago")
                    .SingleOrDefault(c => c.IdCuotaMembresia == idCuotaMembresia);
                if (cuota == null)
                {
                    throw new InvalidOperationException("La cuota no existe.");
                }

                if (cuota.EstadoPago == EstadosCuota.Anulada)
                {
                    throw new InvalidOperationException("La cuota se encuentra anulada.");
                }

                if (cuota.IdRegistroPago.HasValue)
                {
                    throw new InvalidOperationException("La cuota ya está asociada a un pago.");
                }

                ValidarMetodoPago(context, pago.IdMetodoPago);
                if (pago.Estado == EstadosTransaccionPago.Aprobado)
                {
                    ValidarImporteAprobado(cuota, pago.Importe);
                }

                context.Pagos.Add(pago);
                context.GuardarCambios();

                cuota.IdRegistroPago = pago.IdRegistroPago;
                cuota.Pago = pago;
                CuotaMembresiaLogica.RecalcularEstadoPagoEnContexto(context, cuota);
                MembresiaLogica.ActualizarEstadoPorDeudaEnContexto(context, cuota.IdMembresia);
                context.GuardarCambios();
                transaction.Confirmar();
                return pago;
            }
        }

        public Pago ObtenerPorId(int idRegistroPago)
        {
            using (var context = new GymUnidadDeTrabajo())
            {
                return context.Pagos.Consultar("MetodoPago", "Cuotas")
                    .SingleOrDefault(p => p.IdRegistroPago == idRegistroPago);
            }
        }

        public List<Pago> ListarPorCuota(int idCuotaMembresia)
        {
            using (var context = new GymUnidadDeTrabajo())
            {
                return context.CuotasMembresia.Consultar("Pago")
                    .Where(c => c.IdCuotaMembresia == idCuotaMembresia
                        && c.IdRegistroPago.HasValue && c.Pago != null)
                    .Select(c => c.Pago).ToList();
            }
        }

        public List<Pago> ListarPorMembresia(int idMembresia)
        {
            using (var context = new GymUnidadDeTrabajo())
            {
                return context.CuotasMembresia.Consultar("Pago")
                    .Where(c => c.IdMembresia == idMembresia
                        && c.IdRegistroPago.HasValue && c.Pago != null)
                    .Select(c => c.Pago).Distinct()
                    .OrderBy(p => p.Fecha).ThenBy(p => p.IdRegistroPago).ToList();
            }
        }

        public List<MetodoPago> ListarMetodosPagoActivos()
        {
            using (var context = new GymUnidadDeTrabajo())
            {
                return context.MetodosPago.Where(m => m.Estado)
                    .OrderBy(m => m.Observaciones).ToList();
            }
        }

        public decimal CalcularTotalAprobado(int idCuotaMembresia)
        {
            using (var context = new GymUnidadDeTrabajo())
            {
                return context.CuotasMembresia
                    .Where(c => c.IdCuotaMembresia == idCuotaMembresia
                        && c.IdRegistroPago.HasValue
                        && c.Pago.Estado == EstadosTransaccionPago.Aprobado)
                    .Select(c => (decimal?)c.Pago.Importe).SingleOrDefault() ?? 0m;
            }
        }

        public decimal CalcularSaldoPendiente(int idCuotaMembresia)
        {
            using (var context = new GymUnidadDeTrabajo())
            {
                var cuota = context.CuotasMembresia.Consultar("Pago")
                    .SingleOrDefault(c => c.IdCuotaMembresia == idCuotaMembresia);
                if (cuota == null)
                {
                    throw new InvalidOperationException("La cuota no existe.");
                }

                return CuotaMembresiaLogica.CalcularSaldoEnContexto(context, cuota);
            }
        }

        public void CambiarEstadoPago(int idRegistroPago, string nuevoEstado)
        {
            if (!EstadosTransaccionPago.EsValido(nuevoEstado))
            {
                throw new InvalidOperationException("El estado de pago no es válido.");
            }

            using (var context = new GymUnidadDeTrabajo())
            using (var transaction = context.IniciarTransaccion())
            {
                var pago = context.Pagos.Find(idRegistroPago);
                if (pago == null)
                {
                    throw new InvalidOperationException("El pago no existe.");
                }

                var cuotas = context.CuotasMembresia
                    .Where(c => c.IdRegistroPago == idRegistroPago).ToList();
                if (cuotas.Count == 0)
                {
                    throw new InvalidOperationException("El pago no está asociado a ninguna cuota.");
                }

                if (pago.Estado == EstadosTransaccionPago.Reembolsado
                    && nuevoEstado != EstadosTransaccionPago.Reembolsado)
                {
                    throw new InvalidOperationException("Un pago reembolsado no puede volver a contabilizarse.");
                }

                if (nuevoEstado == EstadosTransaccionPago.Reembolsado
                    && pago.Estado != EstadosTransaccionPago.Aprobado)
                {
                    throw new InvalidOperationException("Solo se puede reembolsar un pago aprobado.");
                }

                pago.Estado = nuevoEstado;
                foreach (var cuota in cuotas)
                {
                    cuota.Pago = pago;
                    CuotaMembresiaLogica.RecalcularEstadoPagoEnContexto(context, cuota);
                    MembresiaLogica.ActualizarEstadoPorDeudaEnContexto(context, cuota.IdMembresia);
                }

                context.GuardarCambios();
                transaction.Confirmar();
            }
        }

        public void AnularPago(int idRegistroPago)
        {
            CambiarEstadoPago(idRegistroPago, EstadosTransaccionPago.Anulado);
        }

        public void ReembolsarPago(int idRegistroPago)
        {
            CambiarEstadoPago(idRegistroPago, EstadosTransaccionPago.Reembolsado);
        }

        private static void ValidarDatos(Pago pago)
        {
            if (pago == null)
            {
                throw new ArgumentNullException("pago");
            }

            if (pago.Importe <= 0)
            {
                throw new InvalidOperationException("El importe debe ser mayor que cero.");
            }

            if (pago.IdMetodoPago <= 0)
            {
                throw new InvalidOperationException("El método de pago es obligatorio.");
            }

            if (string.IsNullOrWhiteSpace(pago.Estado))
            {
                pago.Estado = EstadosTransaccionPago.Pendiente;
            }

            if (!EstadosTransaccionPago.EsValido(pago.Estado))
            {
                throw new InvalidOperationException("El estado de pago no es válido.");
            }
        }

        private static void ValidarMetodoPago(IUnidadDeTrabajo context, int idMetodoPago)
        {
            var metodo = context.MetodosPago.Find(idMetodoPago);
            if (metodo == null || !metodo.Estado)
            {
                throw new InvalidOperationException("El método de pago no existe o está inactivo.");
            }

            var tieneMercadoPago = metodo.IdNroPagoMP.HasValue;
            var tieneEfectivo = metodo.IdPagoEfectivo.HasValue;
            if (tieneMercadoPago == tieneEfectivo)
            {
                throw new InvalidOperationException("El método de pago debe tener un único detalle asociado.");
            }
        }

        private static void ValidarImporteAprobado(CuotaMembresia cuota, decimal importe)
        {
            if (cuota.Pago != null && cuota.Pago.Estado == EstadosTransaccionPago.Aprobado)
            {
                throw new InvalidOperationException("La cuota ya tiene un pago aprobado.");
            }

            if (importe > cuota.Importe)
            {
                throw new InvalidOperationException("El pago supera el importe de la cuota.");
            }
        }
    }
}
