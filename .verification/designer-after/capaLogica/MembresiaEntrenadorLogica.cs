using System;
using System.Collections.Generic;
using System.Linq;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaDatos.Repositorios;

namespace exxen2._0.capaLogica
{
    /* Coordina las operaciones y validaciones de negocio de asignaciones de entrenador. */
    public class MembresiaEntrenadorLogica
    {
        /* Valida los beneficios de la membresía y crea su asignación de entrenador activo. */
        public MembresiaEntrenador AsignarEntrenador(int idMembresia, int idEntrenador)
        {
            using (var context = new GymUnidadDeTrabajo())
            using (var transaction = context.IniciarTransaccion())
            {
                var membresia = ObtenerMembresiaConPlan(context, idMembresia);
                ValidarAsignacion(membresia, context, idEntrenador);
                if (context.MembresiasEntrenadores.Any(me => me.IdMembresia == idMembresia && me.Estado))
                {
                    throw new InvalidOperationException("La membresía ya posee un entrenador activo.");
                }

                var asignacion = new MembresiaEntrenador
                {
                    IdMembresia = idMembresia,
                    IdEntrenador = idEntrenador,
                    Estado = true
                };
                context.MembresiasEntrenadores.Agregar(asignacion);
                context.GuardarCambios();
                transaction.Confirmar();
                return asignacion;
            }
        }

        /* Finaliza las asignaciones activas y registra el nuevo entrenador en una transacción. */
        public MembresiaEntrenador CambiarEntrenador(int idMembresia, int idEntrenador)
        {
            using (var context = new GymUnidadDeTrabajo())
            using (var transaction = context.IniciarTransaccion())
            {
                var membresia = ObtenerMembresiaConPlan(context, idMembresia);
                ValidarAsignacion(membresia, context, idEntrenador);
                var activas = context.MembresiasEntrenadores.Where(me => me.IdMembresia == idMembresia && me.Estado).ToList();
                foreach (var activa in activas)
                {
                    activa.Estado = false;
                }

                var nueva = new MembresiaEntrenador
                {
                    IdMembresia = idMembresia,
                    IdEntrenador = idEntrenador,
                    Estado = true
                };
                context.MembresiasEntrenadores.Agregar(nueva);
                context.GuardarCambios();
                transaction.Confirmar();
                return nueva;
            }
        }

        /* Obtiene el entrenador de la asignación activa de la membresía, si existe. */
        public UsuarioSistema ObtenerEntrenadorActivo(int idMembresia)
        {
            using (var context = new GymUnidadDeTrabajo())
            {
                return context.MembresiasEntrenadores.ConsultarSoloLectura("Entrenador").Where(me => me.IdMembresia == idMembresia && me.Estado).Select(me => me.Entrenador).SingleOrDefault();
            }
        }

        /* Consulta asignaciones de entrenador de la membresía indicada para devolver los datos a la capa visual. */
        public List<MembresiaEntrenador> ListarPorMembresia(int idMembresia)
        {
            using (var context = new GymUnidadDeTrabajo())
            {
                return context.MembresiasEntrenadores.ConsultarSoloLectura("Entrenador").Where(me => me.IdMembresia == idMembresia).OrderByDescending(me => me.IdMembresiaEntrenador).ToList();
            }
        }

        /* Desactiva la asignación del entrenador conservando el registro histórico. */
        public void DarDeBajaAsignacion(int idMembresiaEntrenador)
        {
            using (var context = new GymUnidadDeTrabajo())
            {
                var asignacion = context.MembresiasEntrenadores.Buscar(idMembresiaEntrenador);
                if (asignacion == null)
                {
                    throw new InvalidOperationException("La asignación no existe.");
                }

                asignacion.Estado = false;
                context.GuardarCambios();
            }
        }

        /* Valida el plan y el entrenador antes de recuperar una asignación sin duplicar la activa. */
        public void ReactivarAsignacion(int idMembresiaEntrenador)
        {
            using (var context = new GymUnidadDeTrabajo())
            {
                var asignacion = context.MembresiasEntrenadores.Consultar("Membresia.Plan", "Entrenador.Rol").SingleOrDefault(me => me.IdMembresiaEntrenador == idMembresiaEntrenador);
                if (asignacion == null)
                {
                    throw new InvalidOperationException("La asignación no existe.");
                }

                ValidarAsignacion(asignacion.Membresia, context, asignacion.IdEntrenador);
                if (context.MembresiasEntrenadores.Any(me => me.IdMembresia == asignacion.IdMembresia && me.Estado && me.IdMembresiaEntrenador != idMembresiaEntrenador))
                {
                    throw new InvalidOperationException("La membresía ya posee un entrenador activo.");
                }

                asignacion.Estado = true;
                context.GuardarCambios();
            }
        }

        /* Carga la membresía junto con su plan y rechaza identificadores inexistentes. */
        private static Membresia ObtenerMembresiaConPlan(IUnidadDeTrabajo context, int idMembresia)
        {
            var membresia = context.Membresias.Consultar("Plan").SingleOrDefault(m => m.IdMembresia == idMembresia);
            if (membresia == null)
            {
                throw new InvalidOperationException("La membresía no existe.");
            }

            return membresia;
        }

        /* Comprueba membresía habilitada, beneficio del plan y rol activo del entrenador. */
        private static void ValidarAsignacion(Membresia membresia, IUnidadDeTrabajo context, int idEntrenador)
        {
            if (!membresia.Estado)
            {
                throw new InvalidOperationException("La membresía no está habilitada.");
            }

            if (membresia.Plan == null || !membresia.Plan.Estado || !membresia.Plan.IncluyeEntrenador)
            {
                throw new InvalidOperationException("El plan actual no incluye entrenador.");
            }

            var entrenador = context.UsuariosSistema.Consultar("Rol").SingleOrDefault(u => u.IdUsuarioSistema == idEntrenador);
            if (!ValidacionesGym.EsEntrenadorActivo(entrenador))
            {
                throw new InvalidOperationException("El usuario no posee rol de Entrenador activo.");
            }
        }
    }
}
