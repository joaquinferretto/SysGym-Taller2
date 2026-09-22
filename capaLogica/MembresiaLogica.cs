using System;
using System.Collections.Generic;
using System.Linq;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaDatos.Repositorios;

namespace exxen2._0.capaLogica
{
    /* Coordina las operaciones y validaciones de negocio de membresías. */
    public class MembresiaLogica
    {
        private const int CuotasVencidasParaDarDeBaja = 2;
        private const string MensajeReactivacionBloqueadaPorDeuda = "No se puede reactivar la membresía mientras existan dos o más cuotas vencidas pendientes.";

        /* Crea una membresía y su primera cuota; la asignación de entrenador es opcional y posterior. */
        public Membresia Crear(Membresia membresia)
        {
            ValidarMembresia(membresia);
            using (var datos = new UnidadDeTrabajoGimnasio())
            using (var transaccion = datos.IniciarTransaccion())
            {
                var socio = datos.Socios.Buscar(membresia.IdSocio);
                var plan = datos.Planes.Buscar(membresia.IdPlan);
                var usuario = datos.UsuariosSistema.Consultar("Rol").SingleOrDefault(u => u.IdUsuarioSistema == membresia.IdUsuarioSistema);
                ValidarReferenciasActivas(socio, plan, usuario);
                if (datos.Membresias.Any(m => m.IdSocio == membresia.IdSocio))
                    throw new InvalidOperationException("El socio ya tiene una membresía histórica. Reactivá esa misma membresía desde la gestión de membresías.");

                membresia.Estado = true;
                membresia.IdRutina = null;
                if (membresia.FechaInicio == default(DateTime))
                    membresia.FechaInicio = DateTime.Today;
                if (membresia.FechaVencimiento == default(DateTime))
                    membresia.FechaVencimiento = CuotaMembresiaLogica.CalcularPeriodoHasta(membresia.FechaInicio);
                if (membresia.FechaVencimiento < membresia.FechaInicio)
                    throw new InvalidOperationException("La fecha de vencimiento no puede ser anterior a la fecha de inicio.");

                datos.Membresias.Agregar(membresia);
                datos.GuardarCambios();
                socio.Estado = true;
                CuotaMembresiaLogica.CrearPrimeraCuotaEnContexto(datos, membresia, plan);
                datos.GuardarCambios();
                transaccion.Confirmar();
                return membresia;
            }
        }

        /* Lista socios activos que todavía no tienen una membresía histórica. */
        public List<Socio> ListarSociosDisponiblesParaAlta()
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                var idsSociosConMembresia = datos.Membresias
                    .Select(m => m.IdSocio)
                    .Distinct()
                    .ToList();

                return datos.Socios.ConsultarSoloLectura()
                    .Where(s => s.Estado && !idsSociosConMembresia.Contains(s.IdSocio))
                    .OrderBy(s => s.Apellido)
                    .ThenBy(s => s.Nombre)
                    .ToList();
            }
        }

        /* Actualiza fechas y datos propios sin permitir cambiar referencias históricas desde este método. */
        public Membresia Modificar(Membresia membresia)
        {
            if (membresia == null)
                throw new ArgumentNullException("membresia");

            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                var existente = datos.Membresias.Buscar(membresia.IdMembresia);
                if (existente == null)
                    throw new InvalidOperationException("La membresía no existe.");
                if (existente.IdPlan != membresia.IdPlan)
                    throw new InvalidOperationException("El plan debe cambiarse mediante CambiarPlan.");
                if (existente.IdSocio != membresia.IdSocio || existente.IdUsuarioSistema != membresia.IdUsuarioSistema)
                    throw new InvalidOperationException("No se pueden cambiar las referencias históricas de la membresía.");
                if (existente.Estado != membresia.Estado)
                    throw new InvalidOperationException("El estado de la membresía debe cambiarse mediante las acciones Dar de baja o Reactivar.");
                ValidarFechas(membresia);

                existente.FechaInicio = membresia.FechaInicio;
                existente.FechaVencimiento = membresia.FechaVencimiento;
                ActualizarEstadoPorDeudaEnContexto(datos, existente.IdMembresia);
                datos.GuardarCambios();
                return existente;
            }
        }

        /* Busca una membresía con sus relaciones comerciales y de entrenador. */
        public Membresia ObtenerPorId(int idMembresia)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                if (datos.Membresias.Buscar(idMembresia) == null)
                    return null;
                ActualizarEstadoPorDeudaEnContexto(datos, idMembresia);
                datos.GuardarCambios();
                return datos.Membresias.ConsultarSoloLectura("Plan", "Socio", "UsuarioSistema", "Rutina", "Cuotas.Pago", "Entrenadores.Entrenador").SingleOrDefault(m => m.IdMembresia == idMembresia);
            }
        }

        /* Busca el historial de membresías de un socio ordenado desde la más reciente. */
        public List<Membresia> ObtenerPorSocio(int idSocio)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                ActualizarEstadosPorDeudaEnContexto(datos);
                datos.GuardarCambios();
                return datos.Membresias.ConsultarSoloLectura("Plan", "Rutina").Where(m => m.IdSocio == idSocio).OrderByDescending(m => m.FechaInicio).ToList();
            }
        }

        /* Consulta membresías activas para operaciones que requieren una relación vigente. */
        public List<Membresia> ListarHabilitadas()
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                ActualizarEstadosPorDeudaEnContexto(datos);
                datos.GuardarCambios();
                return datos.Membresias.ConsultarSoloLectura("Plan", "Socio", "Rutina").Where(m => m.Estado).OrderBy(m => m.FechaInicio).ToList();
            }
        }

        /* Consulta membresías activas e históricas para la gestión administrativa. */
        public List<Membresia> ListarParaGestion()
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                ActualizarEstadosPorDeudaEnContexto(datos);
                datos.GuardarCambios();
                return datos.Membresias.ConsultarSoloLectura("Plan", "Socio", "Rutina").OrderByDescending(m => m.Estado).ThenBy(m => m.Socio.Apellido).ThenBy(m => m.Socio.Nombre).ToList();
            }
        }

        /* Cambia solamente el plan y conserva las cuotas y asociaciones históricas de la membresía. */
        public void CambiarPlan(int idMembresia, int idPlan)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                var membresia = datos.Membresias.Buscar(idMembresia);
                var plan = datos.Planes.Buscar(idPlan);
                if (membresia == null)
                    throw new InvalidOperationException("La membresía no existe.");
                if (plan == null || !plan.Estado)
                    throw new InvalidOperationException("El plan seleccionado se encuentra inactivo o no existe.");

                membresia.IdPlan = plan.IdPlan;
                datos.GuardarCambios();
            }
        }

        /* Habilita una membresía y sincroniza el estado del socio. */
        public void Habilitar(int idMembresia)
        {
            var reactivacionBloqueada = false;
            using (var datos = new UnidadDeTrabajoGimnasio())
            using (var transaccion = datos.IniciarTransaccion())
            {
                var membresia = ObtenerMembresia(datos, idMembresia);
                if (DebeDarseDeBajaPorDeuda(ContarCuotasVencidasImpagasEnContexto(datos, idMembresia)))
                {
                    CambiarEstadoEnContexto(datos, membresia, false);
                    reactivacionBloqueada = true;
                }
                else
                {
                    CambiarEstadoEnContexto(datos, membresia, true);
                }
                datos.GuardarCambios();
                transaccion.Confirmar();
            }

            if (reactivacionBloqueada)
                throw new InvalidOperationException(MensajeReactivacionBloqueadaPorDeuda);
        }

        /* Da de baja lógicamente una membresía sin borrar cuotas, pagos, rutina ni entrenador. */
        public void Deshabilitar(int idMembresia)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                var membresia = ObtenerMembresia(datos, idMembresia);
                CambiarEstadoEnContexto(datos, membresia, false);
                datos.GuardarCambios();
            }
        }

        /* Conserva el nombre histórico de la operación para los consumidores existentes. */
        public void DarDeBaja(int idMembresia)
        {
            Deshabilitar(idMembresia);
        }

        /* Expone la evaluación central a operaciones de otros módulos que requieren una membresía vigente. */
        public void ActualizarEstadoPorDeuda(int idMembresia)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            using (var transaccion = datos.IniciarTransaccion())
            {
                ActualizarEstadoPorDeudaEnContexto(datos, idMembresia);
                datos.GuardarCambios();
                transaccion.Confirmar();
            }
        }

        /* Cuenta cuotas pendientes cuyo período terminó antes de hoy; las anuladas no generan deuda. */
        internal static int ContarCuotasVencidasImpagasEnContexto(IUnidadDeTrabajo datos, int idMembresia)
        {
            return ConsultarCuotasVencidasImpagasEnContexto(datos).Count(c => c.IdMembresia == idMembresia);
        }

        /* Da de baja por deuda y sincroniza al socio sin reactivar automáticamente al regularizar. */
        internal static void ActualizarEstadoPorDeudaEnContexto(IUnidadDeTrabajo datos, int idMembresia)
        {
            var membresia = ObtenerMembresia(datos, idMembresia);
            if (DebeDarseDeBajaPorDeuda(ContarCuotasVencidasImpagasEnContexto(datos, idMembresia)))
                CambiarEstadoEnContexto(datos, membresia, false);
        }

        /* Evalúa todas las membresías para los listados de gestión y consultas globales. */
        internal static void ActualizarEstadosPorDeudaEnContexto(IUnidadDeTrabajo datos)
        {
            var idsMembresias = ConsultarCuotasVencidasImpagasEnContexto(datos)
                .GroupBy(c => c.IdMembresia)
                .Select(cuotas => new { IdMembresia = cuotas.Key, Cantidad = cuotas.Count() })
                .ToList();
            var idsParaDarDeBaja = idsMembresias
                .Where(m => DebeDarseDeBajaPorDeuda(m.Cantidad))
                .Select(m => m.IdMembresia)
                .ToList();
            if (idsParaDarDeBaja.Count == 0)
                return;

            var membresias = datos.Membresias.Where(m => idsParaDarDeBaja.Contains(m.IdMembresia)).ToList();
            foreach (var membresia in membresias)
                membresia.Estado = false;

            var idsSocios = membresias.Select(m => m.IdSocio).Distinct().ToList();
            foreach (var idSocio in idsSocios)
            {
                var socio = datos.Socios.Buscar(idSocio);
                if (socio == null)
                    throw new InvalidOperationException("El socio de la membresía no existe.");
                socio.Estado = datos.Membresias.Any(m => m.IdSocio == idSocio && m.Estado && !idsParaDarDeBaja.Contains(m.IdMembresia));
            }
        }

        /* Proyecta una sola definición de cuotas vencidas e impagas para todas las evaluaciones. */
        private static IQueryable<CuotaMembresia> ConsultarCuotasVencidasImpagasEnContexto(IUnidadDeTrabajo datos)
        {
            var hoy = DateTime.Today;
            return datos.CuotasMembresia.Where(c => c.EstadoPago == EstadosCuota.Pendiente && c.FechaHasta < hoy);
        }

        /* Aplica el umbral común de deuda para bajas automáticas y validación de reactivaciones. */
        internal static bool DebeDarseDeBajaPorDeuda(int cantidadCuotasVencidasImpagas)
        {
            return cantidadCuotasVencidasImpagas >= CuotasVencidasParaDarDeBaja;
        }

        /* Devuelve la membresía seguida por la unidad de trabajo o informa una clave inválida. */
        private static Membresia ObtenerMembresia(IUnidadDeTrabajo datos, int idMembresia)
        {
            var membresia = datos.Membresias.Buscar(idMembresia);
            if (membresia == null)
                throw new InvalidOperationException("La membresía no existe.");
            return membresia;
        }

        /* Cambia el estado de la membresía y refleja si el socio conserva otra membresía activa. */
        private static void CambiarEstadoEnContexto(IUnidadDeTrabajo datos, Membresia membresia, bool estado)
        {
            membresia.Estado = estado;
            var socio = datos.Socios.Buscar(membresia.IdSocio);
            if (socio == null)
                throw new InvalidOperationException("El socio de la membresía no existe.");

            socio.Estado = estado || datos.Membresias.Any(m => m.IdSocio == membresia.IdSocio && m.IdMembresia != membresia.IdMembresia && m.Estado);
        }

        /* Exige los identificadores de socio, plan y usuario que registra la membresía. */
        private static void ValidarMembresia(Membresia membresia)
        {
            if (membresia == null)
                throw new ArgumentNullException("membresia");
            if (membresia.IdSocio <= 0 || membresia.IdPlan <= 0 || membresia.IdUsuarioSistema <= 0)
                throw new InvalidOperationException("Socio, plan actual y usuario de alta son obligatorios.");
        }

        /* Comprueba que las fechas de la membresía sean válidas. */
        private static void ValidarFechas(Membresia membresia)
        {
            if (membresia.FechaInicio == default(DateTime))
                throw new InvalidOperationException("La fecha de inicio es obligatoria.");
            if (membresia.FechaVencimiento == default(DateTime))
                membresia.FechaVencimiento = CuotaMembresiaLogica.CalcularPeriodoHasta(membresia.FechaInicio);
            if (membresia.FechaVencimiento < membresia.FechaInicio)
                throw new InvalidOperationException("La fecha de vencimiento no puede ser anterior a la fecha de inicio.");
        }

        /* Comprueba que socio, plan y usuario de alta estén disponibles para una nueva membresía. */
        private static void ValidarReferenciasActivas(Socio socio, Plan plan, UsuarioSistema usuario)
        {
            if (socio == null)
                throw new InvalidOperationException("El socio seleccionado no existe.");
            if (plan == null || !plan.Estado)
                throw new InvalidOperationException("El plan seleccionado no existe o está inactivo.");
            if (!ValidacionesGimnasio.PuedeRegistrarMembresia(usuario))
                throw new InvalidOperationException("El usuario de alta debe ser Administrador o Recepcionista activo.");
        }
    }
}
