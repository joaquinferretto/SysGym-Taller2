using System;
using System.Collections.Generic;
using System.Linq;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaDatos.Repositorios;

namespace exxen2._0.capaLogica
{
    /* Coordina las operaciones y validaciones de negocio de pagos. */
    public class PagoLogica
    {
        /* Valida el cobro y lo vincula a su cuota, actualizando la deuda en una misma transacción. */
        public Pago RegistrarPago(Pago pago, int idCuotaMembresia)
        {
            ValidarDatos(pago);
            if (idCuotaMembresia <= 0)
            {
                throw new InvalidOperationException("La cuota es obligatoria.");
            }

            using (var datos = new UnidadDeTrabajoGimnasio())
            using (var transaccion = datos.IniciarTransaccion())
            {
                var cuota = datos.CuotasMembresia.Consultar("Pago").SingleOrDefault(c => c.IdCuotaMembresia == idCuotaMembresia);
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

                ValidarMetodoPago(datos, pago.IdMetodoPago);
                if (pago.Estado == EstadosTransaccionPago.Aprobado)
                {
                    ValidarImporteAprobado(cuota, pago.Importe);
                }

                datos.Pagos.Agregar(pago);
                datos.GuardarCambios();
                cuota.IdRegistroPago = pago.IdRegistroPago;
                cuota.Pago = pago;
                CuotaMembresiaLogica.RecalcularEstadoPagoEnContexto(datos, cuota);
                datos.GuardarCambios();
                MembresiaLogica.ActualizarEstadoPorDeudaEnContexto(datos, cuota.IdMembresia);
                datos.GuardarCambios();
                transaccion.Confirmar();
                return pago;
            }
        }

        /* Busca el registro de pagos por identificador y devuelve los datos disponibles. */
        public Pago ObtenerPorId(int idRegistroPago)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                return datos.Pagos.ConsultarSoloLectura("MetodoPago", "Cuotas").SingleOrDefault(p => p.IdRegistroPago == idRegistroPago);
            }
        }

        /* Consulta pagos asociados a la cuota indicada para devolver los datos a la capa visual. */
        public List<Pago> ListarPorCuota(int idCuotaMembresia)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                return datos.CuotasMembresia.ConsultarSoloLectura("Pago").Where(c => c.IdCuotaMembresia == idCuotaMembresia && c.IdRegistroPago.HasValue && c.Pago != null).Select(c => c.Pago).ToList();
            }
        }

        /* Consulta pagos de la membresía indicada para devolver los datos a la capa visual. */
        public List<Pago> ListarPorMembresia(int idMembresia)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                return datos.CuotasMembresia.ConsultarSoloLectura("Pago").Where(c => c.IdMembresia == idMembresia && c.IdRegistroPago.HasValue && c.Pago != null).Select(c => c.Pago).Distinct().OrderBy(p => p.Fecha).ThenBy(p => p.IdRegistroPago).ToList();
            }
        }

        /* Consulta pagos disponibles para registrar un cobro para devolver los datos a la capa visual. */
        public List<MetodoPago> ListarMetodosPagoActivos()
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                return datos.MetodosPago.ConsultarSoloLectura().Where(m => m.Estado).OrderBy(m => m.Observaciones).ToList();
            }
        }

        /* Obtiene el importe contabilizado de la cuota; los pagos no aprobados aportan cero. */
        public decimal CalcularTotalAprobado(int idCuotaMembresia)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                return datos.CuotasMembresia.ConsultarSoloLectura().Where(c => c.IdCuotaMembresia == idCuotaMembresia && c.IdRegistroPago.HasValue && c.Pago.Estado == EstadosTransaccionPago.Aprobado).Select(c => (decimal? )c.Pago.Importe).SingleOrDefault() ?? 0m;
            }
        }

        /* Obtiene la cuota y calcula cuánto falta abonar descontando únicamente pagos aprobados. */
        public decimal CalcularSaldoPendiente(int idCuotaMembresia)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                var cuota = datos.CuotasMembresia.ConsultarSoloLectura("Pago").SingleOrDefault(c => c.IdCuotaMembresia == idCuotaMembresia);
                if (cuota == null)
                {
                    throw new InvalidOperationException("La cuota no existe.");
                }

                return CuotaMembresiaLogica.CalcularSaldoEnContexto(datos, cuota);
            }
        }

        /* Valida la transición y el importe aprobado antes de actualizar pago, cuota y membresía. */
        public void CambiarEstadoPago(int idRegistroPago, string nuevoEstado)
        {
            if (!EstadosTransaccionPago.EsValido(nuevoEstado))
            {
                throw new InvalidOperationException("El estado de pago no es válido.");
            }

            using (var datos = new UnidadDeTrabajoGimnasio())
            using (var transaccion = datos.IniciarTransaccion())
            {
                var pago = datos.Pagos.Buscar(idRegistroPago);
                if (pago == null)
                {
                    throw new InvalidOperationException("El pago no existe.");
                }

                var cuotas = datos.CuotasMembresia.Where(c => c.IdRegistroPago == idRegistroPago).ToList();
                if (cuotas.Count == 0)
                {
                    throw new InvalidOperationException("El pago no está asociado a ninguna cuota.");
                }

                if (pago.Estado == EstadosTransaccionPago.Reembolsado && nuevoEstado != EstadosTransaccionPago.Reembolsado)
                {
                    throw new InvalidOperationException("Un pago reembolsado no puede volver a contabilizarse.");
                }

                if (nuevoEstado == EstadosTransaccionPago.Reembolsado && pago.Estado != EstadosTransaccionPago.Aprobado)
                {
                    throw new InvalidOperationException("Solo se puede reembolsar un pago aprobado.");
                }

                if (nuevoEstado == EstadosTransaccionPago.Aprobado)
                {
                    foreach (var cuota in cuotas)
                    {
                        ValidarImporteAprobado(cuota, pago.Importe);
                    }
                }

                pago.Estado = nuevoEstado;
                foreach (var cuota in cuotas)
                {
                    cuota.Pago = pago;
                    CuotaMembresiaLogica.RecalcularEstadoPagoEnContexto(datos, cuota);
                    datos.GuardarCambios();
                    MembresiaLogica.ActualizarEstadoPorDeudaEnContexto(datos, cuota.IdMembresia);
                }

                datos.GuardarCambios();
                transaccion.Confirmar();
            }
        }

        /* Valida los datos modificados y recalcula cuota y deuda sin cambiar su asociación histórica. */
        public void ActualizarPago(Pago pagoActualizado, int idCuotaMembresia)
        {
            ValidarDatos(pagoActualizado);
            if (idCuotaMembresia <= 0)
            {
                throw new InvalidOperationException("La cuota es obligatoria.");
            }

            using (var datos = new UnidadDeTrabajoGimnasio())
            using (var transaccion = datos.IniciarTransaccion())
            {
                var pago = datos.Pagos.Buscar(pagoActualizado.IdRegistroPago);
                if (pago == null)
                {
                    throw new InvalidOperationException("El pago no existe.");
                }

                var cuota = datos.CuotasMembresia.Consultar("Pago").SingleOrDefault(c => c.IdCuotaMembresia == idCuotaMembresia && c.IdRegistroPago == pagoActualizado.IdRegistroPago);
                if (cuota == null)
                {
                    throw new InvalidOperationException("El pago no está asociado a la cuota seleccionada.");
                }

                if (cuota.EstadoPago == EstadosCuota.Anulada)
                {
                    throw new InvalidOperationException("La cuota se encuentra anulada. Reactivala antes de editar el pago.");
                }

                if (pago.Estado == EstadosTransaccionPago.Reembolsado && pagoActualizado.Estado != EstadosTransaccionPago.Reembolsado)
                {
                    throw new InvalidOperationException("Un pago reembolsado no puede volver a contabilizarse.");
                }

                if (pagoActualizado.Estado == EstadosTransaccionPago.Reembolsado && pago.Estado != EstadosTransaccionPago.Aprobado)
                {
                    throw new InvalidOperationException("Solo se puede reembolsar un pago aprobado.");
                }

                if (pagoActualizado.Estado == EstadosTransaccionPago.Aprobado)
                {
                    ValidarImporteAprobado(cuota, pagoActualizado.Importe);
                }

                ValidarMetodoPago(datos, pagoActualizado.IdMetodoPago);
                pago.Importe = pagoActualizado.Importe;
                pago.IdMetodoPago = pagoActualizado.IdMetodoPago;
                pago.Estado = pagoActualizado.Estado;
                pago.Descripcion = pagoActualizado.Descripcion;
                pago.Fecha = pagoActualizado.Fecha;
                cuota.Pago = pago;
                CuotaMembresiaLogica.RecalcularEstadoPagoEnContexto(datos, cuota);
                datos.GuardarCambios();
                MembresiaLogica.ActualizarEstadoPorDeudaEnContexto(datos, cuota.IdMembresia);
                datos.GuardarCambios();
                transaccion.Confirmar();
            }
        }

        /* Anula el pago seleccionado y recalcula el saldo mediante el cambio de estado validado. */
        public void AnularPago(int idRegistroPago)
        {
            CambiarEstadoPago(idRegistroPago, EstadosTransaccionPago.Anulado);
        }

        /* Marca el reembolso de un pago aprobado y recalcula su efecto en la cuota. */
        public void ReembolsarPago(int idRegistroPago)
        {
            CambiarEstadoPago(idRegistroPago, EstadosTransaccionPago.Reembolsado);
        }

        /* Comprueba los campos y rangos obligatorios de pagos antes de persistirlos. */
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

        /* Exige un método activo con exactamente un detalle de efectivo o Mercado Pago. */
        private static void ValidarMetodoPago(IUnidadDeTrabajo datos, int idMetodoPago)
        {
            var metodo = datos.MetodosPago.Buscar(idMetodoPago);
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

        /* Rechaza importes no positivos o superiores a la cuota antes de contabilizar un pago. */
        private static void ValidarImporteAprobado(CuotaMembresia cuota, decimal importe)
        {
            if (importe <= 0)
            {
                throw new InvalidOperationException("El importe debe ser mayor que cero.");
            }

            if (importe > cuota.Importe)
            {
                throw new InvalidOperationException("El pago supera el importe de la cuota.");
            }
        }
    }
}
