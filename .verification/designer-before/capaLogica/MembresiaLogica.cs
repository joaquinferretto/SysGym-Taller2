using System;
using System.Collections.Generic;
using System.Linq;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaDatos.Repositorios;

namespace exxen2._0.capaLogica
{
    public class MembresiaLogica
    {
        public Membresia Crear(Membresia membresia)
        {
            ValidarMembresia(membresia);

            using (var context = new GymUnidadDeTrabajo())
            using (var transaction = context.IniciarTransaccion())
            {
                var socio = context.Socios.Find(membresia.IdSocio);
                var plan = context.Planes.Find(membresia.IdPlan);
                var usuario = context.UsuariosSistema.Consultar("Rol")
                    .SingleOrDefault(u => u.IdUsuarioSistema == membresia.IdUsuarioSistema);

                ValidarReferenciasActivas(socio, plan, usuario);
                if (context.Membresias.Any(m => m.IdSocio == membresia.IdSocio && m.Estado))
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

                context.Membresias.Add(membresia);
                context.GuardarCambios();

                CuotaMembresiaLogica.CrearPrimeraCuotaEnContexto(context, membresia, plan);
                context.GuardarCambios();
                transaction.Confirmar();
                return membresia;
            }
        }

        public Membresia Modificar(Membresia membresia)
        {
            if (membresia == null)
            {
                throw new ArgumentNullException("membresia");
            }

            using (var context = new GymUnidadDeTrabajo())
            {
                var existente = context.Membresias.Find(membresia.IdMembresia);
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

                if (membresia.Estado && TieneCuotaVencidaPendienteEnContexto(context, membresia.IdMembresia))
                {
                    throw new InvalidOperationException("La membresía posee cuotas vencidas pendientes.");
                }

                existente.FechaInicio = membresia.FechaInicio;
                existente.FechaVencimiento = membresia.FechaVencimiento;
                existente.Estado = membresia.Estado;
                context.GuardarCambios();
                return existente;
            }
        }

        public Membresia ObtenerPorId(int idMembresia)
        {
            using (var context = new GymUnidadDeTrabajo())
            {
                return context.Membresias.Consultar("Plan", "Socio", "UsuarioSistema", "Cuotas.Pago", "Entrenadores.Entrenador")
                    .SingleOrDefault(m => m.IdMembresia == idMembresia);
            }
        }

        public List<Membresia> ObtenerPorSocio(int idSocio)
        {
            using (var context = new GymUnidadDeTrabajo())
            {
                return context.Membresias.Consultar("Plan")
                    .Where(m => m.IdSocio == idSocio)
                    .OrderByDescending(m => m.FechaInicio).ToList();
            }
        }

        public List<Membresia> ListarHabilitadas()
        {
            using (var context = new GymUnidadDeTrabajo())
            {
                return context.Membresias.Consultar("Plan", "Socio")
                    .Where(m => m.Estado).OrderBy(m => m.FechaInicio).ToList();
            }
        }

        public List<Membresia> ListarParaGestion()
        {
            using (var context = new GymUnidadDeTrabajo())
            {
                return context.Membresias.Consultar("Plan", "Socio")
                    .OrderByDescending(m => m.Estado)
                    .ThenBy(m => m.Socio.Apellido)
                    .ThenBy(m => m.Socio.Nombre)
                    .ToList();
            }
        }

        public void CambiarPlan(int idMembresia, int idPlan)
        {
            using (var context = new GymUnidadDeTrabajo())
            using (var transaction = context.IniciarTransaccion())
            {
                var membresia = context.Membresias.Find(idMembresia);
                var plan = context.Planes.Find(idPlan);
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
                    var asignaciones = context.MembresiasEntrenadores
                        .Where(me => me.IdMembresia == idMembresia && me.Estado).ToList();
                    foreach (var asignacion in asignaciones)
                    {
                        asignacion.Estado = false;
                    }

                    var rutinas = context.RutinaAsignaciones
                        .Where(ra => ra.IdMembresia == idMembresia && ra.Estado).ToList();
                    foreach (var asignacion in rutinas)
                    {
                        asignacion.Estado = false;
                        asignacion.FechaFin = DateTime.Now;
                    }
                }

                context.GuardarCambios();
                transaction.Confirmar();
            }
        }

        public void Habilitar(int idMembresia)
        {
            using (var context = new GymUnidadDeTrabajo())
            {
                var membresia = context.Membresias.Find(idMembresia);
                if (membresia == null)
                {
                    throw new InvalidOperationException("La membresía no existe.");
                }

                if (TieneCuotaVencidaPendienteEnContexto(context, idMembresia))
                {
                    throw new InvalidOperationException("La membresía posee cuotas vencidas pendientes.");
                }

                membresia.Estado = true;
                context.GuardarCambios();
            }
        }

        public void Deshabilitar(int idMembresia)
        {
            CambiarEstado(idMembresia, false);
        }

        public void DarDeBaja(int idMembresia)
        {
            Deshabilitar(idMembresia);
        }

        public bool TieneCuotaVencidaPendiente(int idMembresia)
        {
            using (var context = new GymUnidadDeTrabajo())
            {
                return TieneCuotaVencidaPendienteEnContexto(context, idMembresia);
            }
        }

        public bool DebeDeshabilitarMembresia(int idMembresia)
        {
            return TieneCuotaVencidaPendiente(idMembresia);
        }

        public void ActualizarEstadoPorDeuda(int idMembresia)
        {
            using (var context = new GymUnidadDeTrabajo())
            {
                ActualizarEstadoPorDeudaEnContexto(context, idMembresia);
                context.GuardarCambios();
            }
        }

        internal static bool TieneCuotaVencidaPendienteEnContexto(IUnidadDeTrabajo context, int idMembresia)
        {
            var hoy = DateTime.Today;
            return context.CuotasMembresia.Any(c => c.IdMembresia == idMembresia
                && c.EstadoPago == EstadosCuota.Pendiente
                && c.FechaHasta < hoy);
        }

        internal static void ActualizarEstadoPorDeudaEnContexto(IUnidadDeTrabajo context, int idMembresia)
        {
            var membresia = context.Membresias.Find(idMembresia);
            if (membresia == null)
            {
                throw new InvalidOperationException("La membresía no existe.");
            }

            membresia.Estado = !TieneCuotaVencidaPendienteEnContexto(context, idMembresia);
        }

        private static void CambiarEstado(int idMembresia, bool estado)
        {
            using (var context = new GymUnidadDeTrabajo())
            {
                var membresia = context.Membresias.Find(idMembresia);
                if (membresia == null)
                {
                    throw new InvalidOperationException("La membresía no existe.");
                }

                membresia.Estado = estado;
                if (!estado)
                {
                    var rutinas = context.RutinaAsignaciones
                        .Where(ra => ra.IdMembresia == idMembresia && ra.Estado).ToList();
                    foreach (var asignacion in rutinas)
                    {
                        asignacion.Estado = false;
                        asignacion.FechaFin = DateTime.Now;
                    }
                }
                context.GuardarCambios();
            }
        }

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

            if (!ValidacionesGym.PuedeRegistrarMembresia(usuario))
            {
                throw new InvalidOperationException("El usuario de alta debe ser Administrador o Recepcionista activo.");
            }
        }
    }
}
