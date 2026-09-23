using System;
using System.Collections.Generic;
using System.Linq;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaDatos.Repositorios;

namespace exxen2._0.capaLogica
{
    /* Transporta el saldo, el período y la situación calculada de una membresía. */
    public sealed class EstadoCuentaMembresia
    {
        public int IdMembresia { get; set; }
        public int IdSocio { get; set; }
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

    /* Informa si puede generarse la siguiente cuota y el período que correspondería crear. */
    public sealed class DisponibilidadGeneracionCuota
    {
        public bool PuedeGenerar { get; set; }
        public int CantidadCuotasVencidasImpagas { get; set; }
        public DateTime? UltimaFechaDesde { get; set; }
        public DateTime? UltimaFechaHasta { get; set; }
        public DateTime? NuevaFechaDesde { get; set; }
        public DateTime? NuevaFechaHasta { get; set; }
        public string Motivo { get; set; }
    }

    /* Coordina las operaciones y validaciones de negocio de cuotas de membresía. */
    // Cuotas mensuales: cada cuota cubre un mes (FechaDesde a FechaHasta) y guarda el precio de ese momento.
    // Genera la siguiente cuota, calcula saldos y decide cuándo una cuota está vencida.
    // La usan GestionMembresiasFormulario, GestionPagosFormulario e InicioPanelAdministrador.
    public class CuotaMembresiaLogica
    {
        /* Genera la cuota inicial si la membresía todavía no posee cuotas. */
        public CuotaMembresia CrearPrimeraCuota(int idMembresia)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())  // Abre la conexión; al salir del bloque se cierra sola, aunque haya error (try-with-resources).
            using (var transaccion = datos.IniciarTransaccion())  // Transacción: si algo falla antes de Confirmar(), se deshace todo.
            {
                var membresia = datos.Membresias.Consultar("Plan").SingleOrDefault(m => m.IdMembresia == idMembresia);  // Consulta con seguimiento: si se modifica el objeto, GuardarCambios hace el UPDATE.
                if (membresia == null)
                {
                    throw new InvalidOperationException("La membresía no existe.");  // Regla incumplida: corta la operación y el formulario muestra este mensaje.
                }

                if (datos.CuotasMembresia.Any(c => c.IdMembresia == idMembresia))  // ¿Existe al menos uno? (no trae filas).
                {
                    throw new InvalidOperationException("La membresía ya posee una cuota.");
                }

                var cuota = CrearPrimeraCuotaEnContexto(datos, membresia, membresia.Plan);
                datos.GuardarCambios();  // EF envía a SQL los INSERT/UPDATE pendientes.
                EvaluarEstadoMembresiaPorDeudaEnContexto(datos, idMembresia);
                transaccion.Confirmar();  // Recién acá quedan grabados todos los cambios de la transacción.
                return cuota;
            }
        }

        /* Crea exactamente el período posterior a la última cuota si la deuda y la membresía lo permiten. */
        public CuotaMembresia GenerarSiguienteCuota(int idMembresia)
        {
            CuotaMembresia cuota = null;
            string motivoBloqueoPorDeuda = null;
            using (var datos = new UnidadDeTrabajoGimnasio())
            using (var transaccion = datos.IniciarTransaccion())
            {
                var membresia = datos.Membresias.Consultar("Plan").SingleOrDefault(m => m.IdMembresia == idMembresia);  // Devuelve el único que cumple o null.
                if (membresia == null)
                    throw new InvalidOperationException("La membresía no existe.");

                var disponibilidad = EvaluarDisponibilidadGeneracionEnContexto(datos, membresia);
                if (MembresiaLogica.DebeDarseDeBajaPorDeuda(disponibilidad.CantidadCuotasVencidasImpagas))
                {
                    MembresiaLogica.ActualizarEstadoPorDeudaEnContexto(datos, idMembresia);
                    datos.GuardarCambios();
                    transaccion.Confirmar();
                    motivoBloqueoPorDeuda = disponibilidad.Motivo;
                }
                else
                {
                    cuota = GenerarSiguienteCuotaEnContexto(datos, membresia, disponibilidad);
                    datos.GuardarCambios();
                    transaccion.Confirmar();
                }
            }

            if (!string.IsNullOrWhiteSpace(motivoBloqueoPorDeuda))
                throw new InvalidOperationException(motivoBloqueoPorDeuda);
            return cuota;
        }

        /* Consulta sin insertar si la membresía seleccionada admite una nueva cuota. */
        public DisponibilidadGeneracionCuota ConsultarDisponibilidadGeneracion(int idMembresia)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                var membresia = datos.Membresias.ConsultarSoloLectura("Plan").SingleOrDefault(m => m.IdMembresia == idMembresia);  // Solo lectura: EF no vigila cambios (más liviano para listar).
                if (membresia == null)
                    throw new InvalidOperationException("La membresía no existe.");
                return EvaluarDisponibilidadGeneracionEnContexto(datos, membresia);
            }
        }

        /* Busca el registro de cuotas de membresía por identificador y devuelve los datos disponibles. */
        public CuotaMembresia ObtenerPorId(int idCuotaMembresia)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                var cuota = datos.CuotasMembresia.Consultar("Membresia", "Pago").SingleOrDefault(c => c.IdCuotaMembresia == idCuotaMembresia);
                if (cuota != null)
                    EvaluarEstadoMembresiaPorDeudaEnContexto(datos, cuota.IdMembresia);
                return cuota;
            }
        }

        /* Busca la cuota no anulada que cubre el día actual de la membresía. */
        public CuotaMembresia ObtenerCuotaActual(int idMembresia)
        {
            var hoy = DateTime.Today;
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                EvaluarEstadoMembresiaPorDeudaEnContexto(datos, idMembresia);
                return datos.CuotasMembresia.ConsultarSoloLectura("Pago").Where(c => c.IdMembresia == idMembresia && c.EstadoPago != EstadosCuota.Anulada && c.FechaDesde <= hoy && c.FechaHasta >= hoy).SingleOrDefault();  // Where = filtro (como filter de Streams / WHERE de SQL). "x => ..." es una lambda.
            }
        }

        /* Consulta cuotas de membresía de la membresía indicada para devolver los datos a la capa visual. */
        public List<CuotaMembresia> ListarPorMembresia(int idMembresia)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                EvaluarEstadoMembresiaPorDeudaEnContexto(datos, idMembresia);
                return datos.CuotasMembresia.ConsultarSoloLectura().Where(c => c.IdMembresia == idMembresia).OrderBy(c => c.FechaDesde).ToList();  // Acá se ejecuta la consulta en SQL y se trae la lista.
            }
        }

        /* Consulta cuotas de membresía pendientes de pago para devolver los datos a la capa visual. */
        public List<CuotaMembresia> ListarPendientes()
        {
            return ListarPorEstado(EstadosCuota.Pendiente);
        }

        /* Consulta cuotas de membresía pagadas para devolver los datos a la capa visual. */
        public List<CuotaMembresia> ListarPagadas()
        {
            return ListarPorEstado(EstadosCuota.Pagada);
        }

        /* Consulta cuotas de membresía anuladas para devolver los datos a la capa visual. */
        public List<CuotaMembresia> ListarAnuladas()
        {
            return ListarPorEstado(EstadosCuota.Anulada);
        }

        /* Consulta cuotas de membresía activos e inactivos para su gestión para devolver los datos a la capa visual. */
        public List<CuotaMembresia> ListarParaGestion()
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                EvaluarTodasLasMembresiasPorDeudaEnContexto(datos);
                return datos.CuotasMembresia.ConsultarSoloLectura("Pago", "Membresia.Socio", "Membresia.Plan").Where(c => c.EstadoPago == EstadosCuota.Pendiente || c.EstadoPago == EstadosCuota.Pagada || c.EstadoPago == EstadosCuota.Anulada).OrderByDescending(c => c.FechaDesde).ThenBy(c => c.Membresia.Socio.Apellido).ThenBy(c => c.Membresia.Socio.Nombre).ToList();  // Ordena (ORDER BY).
            }
        }

        /* Consulta cuotas de membresía con su situación de deuda para devolver los datos a la capa visual. */
        public List<EstadoCuentaMembresia> ListarEstadoCuentas()
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                EvaluarTodasLasMembresiasPorDeudaEnContexto(datos);
                var membresias = datos.Membresias.ConsultarSoloLectura("Socio", "Plan", "Cuotas.Pago").OrderBy(m => m.Socio.Apellido).ThenBy(m => m.Socio.Nombre).ToList();
                return membresias.Select(CrearEstadoCuenta).ToList();  // Select = transforma cada elemento (como map de Streams).
            }
        }

        /* Resume cuotas, deuda y período cubierto para informar la situación de una membresía. */
        private static EstadoCuentaMembresia CrearEstadoCuenta(Membresia membresia)
        {
            var hoy = DateTime.Today;
            var cuotasValidas = membresia.Cuotas.Where(c => c.EstadoPago != EstadosCuota.Anulada).OrderBy(c => c.FechaDesde).ToList();
            var ultima = cuotasValidas.LastOrDefault();
            var pendientes = cuotasValidas.Where(c => c.EstadoPago == EstadosCuota.Pendiente).ToList();
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
                IdSocio = membresia.IdSocio,
                Socio = membresia.Socio == null ? "Socio no disponible" : membresia.Socio.Apellido + ", " + membresia.Socio.Nombre,
                DNI = membresia.Socio == null ? "-" : membresia.Socio.DNI,
                Plan = membresia.Plan == null ? "-" : membresia.Plan.Nombre,
                UltimaCuotaDesde = ultima == null ? (DateTime? )null : ultima.FechaDesde,
                UltimaCuotaHasta = ultima == null ? (DateTime? )null : ultima.FechaHasta,
                EstadoUltimaCuota = ultima == null ? "Sin cuota" : ultima.EstadoPago,
                SaldoPendiente = saldo,
                Situacion = situacion,
                AlDia = alDia,
                TieneDeuda = tieneDeuda
            };
        }

        /* Calcula el saldo con los datos ya cargados sin abrir otra consulta. */
        private static decimal CalcularSaldoSinContexto(CuotaMembresia cuota)
        {
            var abonado = cuota.IdRegistroPago.HasValue && cuota.Pago != null && cuota.Pago.Estado == EstadosTransaccionPago.Aprobado ? cuota.Pago.Importe : 0m;  // HasValue: ¿el valor opcional (int?, DateTime?) tiene dato?
            return Math.Max(0m, cuota.Importe - abonado);
        }

        /* Recupera una cuota anulada y recalcula su estado y la deuda dentro de una transacción. */
        public void ReactivarCuota(int idCuotaMembresia)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            using (var transaccion = datos.IniciarTransaccion())
            {
                var cuota = datos.CuotasMembresia.Consultar("Pago").SingleOrDefault(c => c.IdCuotaMembresia == idCuotaMembresia);
                if (cuota == null)
                {
                    throw new InvalidOperationException("La cuota no existe.");
                }

                if (cuota.EstadoPago != EstadosCuota.Anulada)
                {
                    throw new InvalidOperationException("La cuota no está anulada.");
                }

                cuota.EstadoPago = EstadosCuota.Pendiente;
                RecalcularEstadoPagoEnContexto(datos, cuota);
                datos.GuardarCambios();
                EvaluarEstadoMembresiaPorDeudaEnContexto(datos, cuota.IdMembresia);
                transaccion.Confirmar();
            }
        }

        /* Anula la cuota sin borrar su historia y actualiza la deuda de la membresía. */
        public void AnularCuota(int idCuotaMembresia)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            using (var transaccion = datos.IniciarTransaccion())
            {
                var cuota = datos.CuotasMembresia.Consultar("Pago").SingleOrDefault(c => c.IdCuotaMembresia == idCuotaMembresia);
                if (cuota == null)
                {
                    throw new InvalidOperationException("La cuota no existe.");
                }

                cuota.EstadoPago = EstadosCuota.Anulada;
                datos.GuardarCambios();
                EvaluarEstadoMembresiaPorDeudaEnContexto(datos, cuota.IdMembresia);
                transaccion.Confirmar();
            }
        }

        /* Indica si una cuota pendiente terminó antes del día actual. */
        public bool EstaVencida(CuotaMembresia cuota)
        {
            return cuota != null && cuota.EstadoPago == EstadosCuota.Pendiente && cuota.FechaHasta.Date < DateTime.Today;
        }

        /* Calcula el importe pendiente de la cuota utilizando su pago aprobado, si existe. */
        public decimal CalcularSaldo(int idCuotaMembresia)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                var cuota = datos.CuotasMembresia.Consultar("Pago").SingleOrDefault(c => c.IdCuotaMembresia == idCuotaMembresia);
                if (cuota == null)
                {
                    throw new InvalidOperationException("La cuota no existe.");
                }

                EvaluarEstadoMembresiaPorDeudaEnContexto(datos, cuota.IdMembresia);
                return CalcularSaldoEnContexto(datos, cuota);
            }
        }

        /* Calcula el importe pendiente de la cuota utilizando su pago aprobado, si existe. */
        public decimal CalcularSaldo(CuotaMembresia cuota)
        {
            if (cuota == null)
            {
                throw new ArgumentNullException("cuota");  // Se recibió null donde no corresponde.
            }

            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                var cuotaActual = datos.CuotasMembresia.Consultar("Pago").SingleOrDefault(c => c.IdCuotaMembresia == cuota.IdCuotaMembresia);
                if (cuotaActual == null)
                {
                    throw new InvalidOperationException("La cuota no existe.");
                }

                EvaluarEstadoMembresiaPorDeudaEnContexto(datos, cuotaActual.IdMembresia);
                return CalcularSaldoEnContexto(datos, cuotaActual);
            }
        }

        /* Actualiza el estado de la cuota y la deuda de su membresía en una transacción. */
        public void RecalcularEstadoPago(int idCuotaMembresia)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            using (var transaccion = datos.IniciarTransaccion())
            {
                var cuota = datos.CuotasMembresia.Consultar("Pago").SingleOrDefault(c => c.IdCuotaMembresia == idCuotaMembresia);
                if (cuota == null)
                {
                    throw new InvalidOperationException("La cuota no existe.");
                }

                RecalcularEstadoPagoEnContexto(datos, cuota);
                datos.GuardarCambios();
                EvaluarEstadoMembresiaPorDeudaEnContexto(datos, cuota.IdMembresia);
                transaccion.Confirmar();
            }
        }

        /* Agrega la primera cuota utilizando la misma unidad de trabajo del alta de membresía. */
        internal static CuotaMembresia CrearPrimeraCuotaEnContexto(IUnidadDeTrabajo datos, Membresia membresia, Plan plan)
        {
            if (membresia == null || plan == null)
            {
                throw new InvalidOperationException("La membresía y el plan son obligatorios para crear la cuota.");
            }

            ValidarPlanActivo(plan);
            return CrearCuotaEnContexto(datos, membresia, plan, membresia.FechaInicio);
        }

        /* Prepara una sola cuota posterior a la última existente sin confirmar la unidad de trabajo. */
        internal static CuotaMembresia GenerarSiguienteCuotaEnContexto(IUnidadDeTrabajo datos, Membresia membresia)
        {
            var disponibilidad = EvaluarDisponibilidadGeneracionEnContexto(datos, membresia);
            return GenerarSiguienteCuotaEnContexto(datos, membresia, disponibilidad);
        }

        /* Valida disponibilidad y agrega exactamente el período calculado a la unidad de trabajo. */
        private static CuotaMembresia GenerarSiguienteCuotaEnContexto(IUnidadDeTrabajo datos, Membresia membresia, DisponibilidadGeneracionCuota disponibilidad)
        {
            if (!disponibilidad.PuedeGenerar)
                throw new InvalidOperationException(disponibilidad.Motivo);

            var plan = membresia.Plan ?? datos.Planes.Buscar(membresia.IdPlan);  // Busca por clave primaria; si no existe devuelve null.
            ValidarPlanActivo(plan);
            var desde = disponibilidad.NuevaFechaDesde.Value;
            var hasta = disponibilidad.NuevaFechaHasta.Value;
            ValidarPeriodoNoDuplicadoEnContexto(datos, membresia.IdMembresia, desde, hasta);
            return CrearCuotaEnContexto(datos, membresia, plan, desde);
        }

        /* Evalúa la última cuota real, la deuda y las restricciones de estado sin insertar registros. */
        internal static DisponibilidadGeneracionCuota EvaluarDisponibilidadGeneracionEnContexto(IUnidadDeTrabajo datos, Membresia membresia)
        {
            if (datos == null)
                throw new ArgumentNullException("datos");
            if (membresia == null)
                throw new ArgumentNullException("membresia");

            var cuotas = datos.CuotasMembresia.ConsultarSoloLectura()
                .Where(c => c.IdMembresia == membresia.IdMembresia)
                .OrderByDescending(c => c.FechaHasta)
                .ThenByDescending(c => c.FechaDesde)
                .ThenByDescending(c => c.IdCuotaMembresia)
                .ToList();
            var ultima = cuotas.FirstOrDefault();
            var vencidas = MembresiaLogica.ContarCuotasVencidasImpagasEnContexto(datos, membresia.IdMembresia);
            var resultado = new DisponibilidadGeneracionCuota
            {
                CantidadCuotasVencidasImpagas = vencidas,
                UltimaFechaDesde = ultima == null ? (DateTime?)null : ultima.FechaDesde.Date,
                UltimaFechaHasta = ultima == null ? (DateTime?)null : ultima.FechaHasta.Date
            };

            if (MembresiaLogica.DebeDarseDeBajaPorDeuda(vencidas))
            {
                resultado.Motivo = "No se puede generar una nueva cuota porque la membresía tiene 2 o más cuotas vencidas sin pagar.";
                return resultado;
            }
            if (!membresia.Estado)
            {
                resultado.Motivo = "La membresía está inactiva. Reactivala antes de generar una cuota.";
                return resultado;
            }
            if (ultima == null)
            {
                resultado.Motivo = "La membresía no posee una cuota inicial.";
                return resultado;
            }

            var plan = membresia.Plan ?? datos.Planes.Buscar(membresia.IdPlan);  // ??: si lo de la izquierda es null, usa lo de la derecha.
            if (plan == null || !plan.Estado)
            {
                resultado.Motivo = "El plan actual no existe o está inactivo.";
                return resultado;
            }

            resultado.NuevaFechaDesde = ultima.FechaHasta.Date.AddDays(1);  // La nueva cuota empieza el día siguiente al fin de la anterior: no quedan huecos ni meses repetidos.
            resultado.NuevaFechaHasta = CalcularPeriodoHasta(resultado.NuevaFechaDesde.Value);
            if (cuotas.Any(c => c.FechaDesde.Date == resultado.NuevaFechaDesde.Value && c.FechaHasta.Date == resultado.NuevaFechaHasta.Value))
            {
                resultado.Motivo = "Ya existe una cuota para ese período.";
                return resultado;
            }

            resultado.PuedeGenerar = true;
            resultado.Motivo = string.Empty;
            return resultado;
        }

        /* Impide insertar un período ya registrado para la misma membresía. */
        internal static void ValidarPeriodoNoDuplicadoEnContexto(IUnidadDeTrabajo datos, int idMembresia, DateTime fechaDesde, DateTime fechaHasta)
        {
            var desde = fechaDesde.Date;
            var hasta = fechaHasta.Date;
            if (datos.CuotasMembresia.Any(c => c.IdMembresia == idMembresia && c.FechaDesde == desde && c.FechaHasta == hasta))
                throw new InvalidOperationException("Ya existe una cuota para ese período.");
        }

        /* Determina si la cuota está pagada según el importe aprobado y respeta las anulaciones. */
        internal static void RecalcularEstadoPagoEnContexto(IUnidadDeTrabajo datos, CuotaMembresia cuota)
        {
            if (cuota.EstadoPago == EstadosCuota.Anulada)
            {
                return;
            }

            var totalAprobado = cuota.IdRegistroPago.HasValue && cuota.Pago != null && cuota.Pago.Estado == EstadosTransaccionPago.Aprobado ? cuota.Pago.Importe : 0m;
            cuota.EstadoPago = totalAprobado >= cuota.Importe ? EstadosCuota.Pagada : EstadosCuota.Pendiente;
        }

        /* Calcula el saldo de la cuota cargada, considerando cero para cuotas anuladas. */
        internal static decimal CalcularSaldoEnContexto(IUnidadDeTrabajo datos, CuotaMembresia cuota)
        {
            if (cuota.EstadoPago == EstadosCuota.Anulada)
            {
                return 0m;
            }

            var totalAprobado = cuota.IdRegistroPago.HasValue && cuota.Pago != null && cuota.Pago.Estado == EstadosTransaccionPago.Aprobado ? cuota.Pago.Importe : 0m;
            return Math.Max(0m, cuota.Importe - totalAprobado);
        }

        /* Agrega una cuota mensual pendiente conservando el precio histórico del plan. */
        private static CuotaMembresia CrearCuotaEnContexto(IUnidadDeTrabajo datos, Membresia membresia, Plan plan, DateTime periodoDesde)
        {
            var cuota = new CuotaMembresia
            {
                IdMembresia = membresia.IdMembresia,
                FechaDesde = periodoDesde,
                FechaHasta = CalcularPeriodoHasta(periodoDesde),
                Importe = plan.Precio,  // Se copia el precio actual: si el plan sube después, esta cuota no cambia.
                EstadoPago = EstadosCuota.Pendiente
            };
            datos.CuotasMembresia.Agregar(cuota);  // Deja el objeto listo para INSERT (se ejecuta en GuardarCambios).
            return cuota;
        }

        /* Devuelve la última cuota no anulada: su período es la cobertura real de la membresía, que no vence por sí misma. */
        public static CuotaMembresia UltimaCuotaVigente(IEnumerable<CuotaMembresia> cuotas)
        {
            return cuotas == null
                ? null
                : cuotas.Where(c => c.EstadoPago != EstadosCuota.Anulada)
                    .OrderByDescending(c => c.FechaHasta)
                    .ThenByDescending(c => c.IdCuotaMembresia)
                    .FirstOrDefault();
        }

        /* Fecha hasta la que la membresía tiene cuotas generadas, o null si todavía no tiene. */
        public static DateTime? CubiertaHasta(IEnumerable<CuotaMembresia> cuotas)
        {
            var ultima = UltimaCuotaVigente(cuotas);
            return ultima == null ? (DateTime?)null : ultima.FechaHasta.Date;
        }

        /* Calcula el último día del período mensual a partir de su fecha de inicio. */
        public static DateTime CalcularPeriodoHasta(DateTime periodoDesde)
        {
            return periodoDesde.AddMonths(1).AddDays(-1);  // Ej.: del 15/09 al 14/10. AddMonths ajusta solo los meses cortos.
        }

        /* Consulta cuotas de membresía con el estado solicitado para devolver los datos a la capa visual. */
        private List<CuotaMembresia> ListarPorEstado(string estado)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                EvaluarTodasLasMembresiasPorDeudaEnContexto(datos);
                return datos.CuotasMembresia.ConsultarSoloLectura("Pago", "Membresia.Socio", "Membresia.Plan").Where(c => c.EstadoPago == estado).OrderBy(c => c.FechaDesde).ToList();
            }
        }

        /* Impide generar cuotas con un plan inexistente o dado de baja. */
        private static void ValidarPlanActivo(Plan plan)
        {
            if (plan == null || !plan.Estado)
            {
                throw new InvalidOperationException("El plan actual no existe o está inactivo.");
            }
        }

        /* Delega la decisión a MembresiaLogica y persiste su sincronización con el socio. */
        private static void EvaluarEstadoMembresiaPorDeudaEnContexto(IUnidadDeTrabajo datos, int idMembresia)
        {
            MembresiaLogica.ActualizarEstadoPorDeudaEnContexto(datos, idMembresia);
            datos.GuardarCambios();
        }

        /* Evalúa el estado de membresía antes de presentar listados globales de cuotas. */
        private static void EvaluarTodasLasMembresiasPorDeudaEnContexto(IUnidadDeTrabajo datos)
        {
            MembresiaLogica.ActualizarEstadosPorDeudaEnContexto(datos);
            datos.GuardarCambios();
        }
    }
}
