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
        /* Valida y registra membresías mediante la unidad de trabajo, conservando sus reglas de alta. */
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
                if (datos.Membresias.Any(m => m.IdSocio == membresia.IdSocio && m.Estado))
                {
                    throw new InvalidOperationException("El socio ya posee una membresía habilitada.");
                }

                membresia.Estado = true;
                if (membresia.FechaInicio == default(DateTime))
                {
                    membresia.FechaInicio = DateTime.Today;
                }

                if (membresia.FechaVencimiento == default(DateTime))
                {
                    membresia.FechaVencimiento = CuotaMembresiaLogica.CalcularPeriodoHasta(membresia.FechaInicio);
                }

                if (membresia.FechaVencimiento < membresia.FechaInicio)
                {
                    throw new InvalidOperationException("La fecha de vencimiento no puede ser anterior a la fecha de inicio.");
                }

                datos.Membresias.Agregar(membresia);
                datos.GuardarCambios();
                CuotaMembresiaLogica.CrearPrimeraCuotaEnContexto(datos, membresia, plan);
                datos.GuardarCambios();
                transaccion.Confirmar();
                return membresia;
            }
        }

        /* Valida y guarda los cambios de membresías sobre el registro existente. */
        public Membresia Modificar(Membresia membresia)
        {
            if (membresia == null)
            {
                throw new ArgumentNullException("membresia");
            }

            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                var existente = datos.Membresias.Buscar(membresia.IdMembresia);
                if (existente == null)
                {
                    throw new InvalidOperationException("La membresía no existe.");
                }

                if (existente.IdPlan != membresia.IdPlan)
                {
                    throw new InvalidOperationException("El plan debe cambiarse mediante CambiarPlan.");
                }

                if (existente.IdSocio != membresia.IdSocio || existente.IdUsuarioSistema != membresia.IdUsuarioSistema)
                {
                    throw new InvalidOperationException("No se pueden cambiar las referencias históricas de la membresía.");
                }

                if (membresia.FechaInicio == default(DateTime))
                {
                    throw new InvalidOperationException("La fecha de inicio es obligatoria.");
                }

                if (membresia.FechaVencimiento == default(DateTime))
                {
                    membresia.FechaVencimiento = CuotaMembresiaLogica.CalcularPeriodoHasta(membresia.FechaInicio);
                }

                if (membresia.FechaVencimiento < membresia.FechaInicio)
                {
                    throw new InvalidOperationException("La fecha de vencimiento no puede ser anterior a la fecha de inicio.");
                }

                if (membresia.Estado && TieneCuotaVencidaPendienteEnContexto(datos, membresia.IdMembresia))
                {
                    throw new InvalidOperationException("La membresía posee cuotas vencidas pendientes.");
                }

                existente.FechaInicio = membresia.FechaInicio;
                existente.FechaVencimiento = membresia.FechaVencimiento;
                existente.Estado = membresia.Estado;
                datos.GuardarCambios();
                return existente;
            }
        }

        /* Busca el registro de membresías por identificador y devuelve los datos disponibles. */
        public Membresia ObtenerPorId(int idMembresia)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                return datos.Membresias.ConsultarSoloLectura("Plan", "Socio", "UsuarioSistema", "Cuotas.Pago", "Entrenadores.Entrenador").SingleOrDefault(m => m.IdMembresia == idMembresia);
            }
        }

        /* Busca el registro de membresías por socio y devuelve los datos disponibles. */
        public List<Membresia> ObtenerPorSocio(int idSocio)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                return datos.Membresias.ConsultarSoloLectura("Plan").Where(m => m.IdSocio == idSocio).OrderByDescending(m => m.FechaInicio).ToList();
            }
        }

        /* Consulta membresías habilitadas para devolver los datos a la capa visual. */
        public List<Membresia> ListarHabilitadas()
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                return datos.Membresias.ConsultarSoloLectura("Plan", "Socio").Where(m => m.Estado).OrderBy(m => m.FechaInicio).ToList();
            }
        }

        /* Consulta membresías activos e inactivos para su gestión para devolver los datos a la capa visual. */
        public List<Membresia> ListarParaGestion()
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                return datos.Membresias.ConsultarSoloLectura("Plan", "Socio").OrderByDescending(m => m.Estado).ThenBy(m => m.Socio.Apellido).ThenBy(m => m.Socio.Nombre).ToList();
            }
        }

        /* Cambia el plan y finaliza las asignaciones que el flujo existente desactiva, sin borrar historia. */
        public void CambiarPlan(int idMembresia, int idPlan)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            using (var transaccion = datos.IniciarTransaccion())
            {
                var membresia = datos.Membresias.Buscar(idMembresia);
                var plan = datos.Planes.Consultar("RutinasDisponibles").SingleOrDefault(p => p.IdPlan == idPlan);
                if (membresia == null)
                {
                    throw new InvalidOperationException("La membresía no existe.");
                }

                if (plan == null || !plan.Estado)
                {
                    throw new InvalidOperationException("El plan seleccionado se encuentra inactivo o no existe.");
                }

                membresia.IdPlan = plan.IdPlan;
                if (!plan.IncluyeEntrenador)
                {
                    var asignaciones = datos.MembresiasEntrenadores.Where(me => me.IdMembresia == idMembresia && me.Estado).ToList();
                    foreach (var asignacion in asignaciones)
                    {
                        asignacion.Estado = false;
                    }

                }
                var permitidas = plan.RutinasDisponibles.Where(r => r.Estado).Select(r => r.IdRutina).ToList();
                var rutinas = datos.RutinaAsignaciones.Where(ra => ra.IdMembresia == idMembresia &&
                    ra.Estado && !permitidas.Contains(ra.IdRutina)).ToList();
                foreach (var asignacion in rutinas)
                {
                    asignacion.Estado = false;
                    asignacion.FechaFin = DateTime.Now;
                }

                datos.GuardarCambios();
                transaccion.Confirmar();
            }
        }

        /* Habilita la membresía si no posee cuotas vencidas pendientes. */
        public void Habilitar(int idMembresia)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                var membresia = datos.Membresias.Buscar(idMembresia);
                if (membresia == null)
                {
                    throw new InvalidOperationException("La membresía no existe.");
                }

                if (TieneCuotaVencidaPendienteEnContexto(datos, idMembresia))
                {
                    throw new InvalidOperationException("La membresía posee cuotas vencidas pendientes.");
                }

                membresia.Estado = true;
                datos.GuardarCambios();
            }
        }

        /* Solicita la baja lógica de la membresía manteniendo su historial. */
        public void Deshabilitar(int idMembresia)
        {
            CambiarEstado(idMembresia, false);
        }

        /* Desactiva el registro de membresías sin eliminar su historial. */
        public void DarDeBaja(int idMembresia)
        {
            Deshabilitar(idMembresia);
        }

        /* Consulta si la membresía posee cuotas impagas anteriores al día actual. */
        public bool TieneCuotaVencidaPendiente(int idMembresia)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                return TieneCuotaVencidaPendienteEnContexto(datos, idMembresia);
            }
        }

        /* Indica si la deuda vencida exige deshabilitar la membresía. */
        public bool DebeDeshabilitarMembresia(int idMembresia)
        {
            return TieneCuotaVencidaPendiente(idMembresia);
        }

        /* Guarda la habilitación de la membresía según su deuda vencida. */
        public void ActualizarEstadoPorDeuda(int idMembresia)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                ActualizarEstadoPorDeudaEnContexto(datos, idMembresia);
                datos.GuardarCambios();
            }
        }

        /* Comprueba la deuda vencida utilizando la unidad de trabajo de la operación. */
        internal static bool TieneCuotaVencidaPendienteEnContexto(IUnidadDeTrabajo datos, int idMembresia)
        {
            var hoy = DateTime.Today;
            return datos.CuotasMembresia.Any(c => c.IdMembresia == idMembresia && c.EstadoPago == EstadosCuota.Pendiente && c.FechaHasta < hoy);
        }

        /* Ajusta la habilitación según las cuotas persistidas dentro de la operación actual. */
        internal static void ActualizarEstadoPorDeudaEnContexto(IUnidadDeTrabajo datos, int idMembresia)
        {
            var membresia = datos.Membresias.Buscar(idMembresia);
            if (membresia == null)
            {
                throw new InvalidOperationException("La membresía no existe.");
            }

            membresia.Estado = !TieneCuotaVencidaPendienteEnContexto(datos, idMembresia);
        }

        /* Modifica la habilitación de la membresía y finaliza sus rutinas al deshabilitarla. */
        private static void CambiarEstado(int idMembresia, bool estado)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                var membresia = datos.Membresias.Buscar(idMembresia);
                if (membresia == null)
                {
                    throw new InvalidOperationException("La membresía no existe.");
                }

                membresia.Estado = estado;
                if (!estado)
                {
                    var rutinas = datos.RutinaAsignaciones.Where(ra => ra.IdMembresia == idMembresia && ra.Estado).ToList();
                    foreach (var asignacion in rutinas)
                    {
                        asignacion.Estado = false;
                        asignacion.FechaFin = DateTime.Now;
                    }
                }

                datos.GuardarCambios();
            }
        }

        /* Exige los identificadores de socio, plan y usuario que registra la membresía. */
        private static void ValidarMembresia(Membresia membresia)
        {
            if (membresia == null)
            {
                throw new ArgumentNullException("membresia");
            }

            if (membresia.IdSocio <= 0 || membresia.IdPlan <= 0 || membresia.IdUsuarioSistema <= 0)
            {
                throw new InvalidOperationException("Socio, plan actual y usuario de alta son obligatorios.");
            }
        }

        /* Comprueba socio y plan activos y que el registrador tenga un rol permitido. */
        private static void ValidarReferenciasActivas(Socio socio, Plan plan, UsuarioSistema usuario)
        {
            if (socio == null || !socio.Estado)
            {
                throw new InvalidOperationException("El socio seleccionado no existe o está inactivo.");
            }

            if (plan == null || !plan.Estado)
            {
                throw new InvalidOperationException("El plan seleccionado no existe o está inactivo.");
            }

            if (!ValidacionesGimnasio.PuedeRegistrarMembresia(usuario))
            {
                throw new InvalidOperationException("El usuario de alta debe ser Administrador o Recepcionista activo.");
            }
        }
    }
}
