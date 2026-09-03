using System;
using System.Collections.Generic;
using System.Linq;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaDatos.Repositorios;

namespace exxen2._0.capaLogica
{
    public class MembresiaEntrenadorLogica
    {
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
                context.MembresiasEntrenadores.Add(asignacion);
                context.GuardarCambios();
                transaction.Confirmar();
                return asignacion;
            }
        }

        public MembresiaEntrenador CambiarEntrenador(int idMembresia, int idEntrenador)
        {
            using (var context = new GymUnidadDeTrabajo())
            using (var transaction = context.IniciarTransaccion())
            {
                var membresia = ObtenerMembresiaConPlan(context, idMembresia);
                ValidarAsignacion(membresia, context, idEntrenador);
                var activas = context.MembresiasEntrenadores
                    .Where(me => me.IdMembresia == idMembresia && me.Estado).ToList();
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
                context.MembresiasEntrenadores.Add(nueva);
                context.GuardarCambios();
                transaction.Confirmar();
                return nueva;
            }
        }

        public UsuarioSistema ObtenerEntrenadorActivo(int idMembresia)
        {
            using (var context = new GymUnidadDeTrabajo())
            {
                return context.MembresiasEntrenadores.Consultar("Entrenador")
                    .Where(me => me.IdMembresia == idMembresia && me.Estado)
                    .Select(me => me.Entrenador).SingleOrDefault();
            }
        }

        public List<MembresiaEntrenador> ListarPorMembresia(int idMembresia)
        {
            using (var context = new GymUnidadDeTrabajo())
            {
                return context.MembresiasEntrenadores.Consultar("Entrenador")
                    .Where(me => me.IdMembresia == idMembresia)
                    .OrderByDescending(me => me.IdMembresiaEntrenador).ToList();
            }
        }

        public void DarDeBajaAsignacion(int idMembresiaEntrenador)
        {
            using (var context = new GymUnidadDeTrabajo())
            {
                var asignacion = context.MembresiasEntrenadores.Find(idMembresiaEntrenador);
                if (asignacion == null)
                {
                    throw new InvalidOperationException("La asignación no existe.");
                }

                asignacion.Estado = false;
                context.GuardarCambios();
            }
        }

        public void ReactivarAsignacion(int idMembresiaEntrenador)
        {
            using (var context = new GymUnidadDeTrabajo())
            {
                var asignacion = context.MembresiasEntrenadores
                    .Consultar("Membresia.Plan", "Entrenador.Rol")
                    .SingleOrDefault(me => me.IdMembresiaEntrenador == idMembresiaEntrenador);
                if (asignacion == null)
                {
                    throw new InvalidOperationException("La asignación no existe.");
                }

                ValidarAsignacion(asignacion.Membresia, context, asignacion.IdEntrenador);
                if (context.MembresiasEntrenadores.Any(me => me.IdMembresia == asignacion.IdMembresia
                    && me.Estado && me.IdMembresiaEntrenador != idMembresiaEntrenador))
                {
                    throw new InvalidOperationException("La membresía ya posee un entrenador activo.");
                }

                asignacion.Estado = true;
                context.GuardarCambios();
            }
        }

        private static Membresia ObtenerMembresiaConPlan(IUnidadDeTrabajo context, int idMembresia)
        {
            var membresia = context.Membresias.Consultar("Plan")
                .SingleOrDefault(m => m.IdMembresia == idMembresia);
            if (membresia == null)
            {
                throw new InvalidOperationException("La membresía no existe.");
            }

            return membresia;
        }

        private static void ValidarAsignacion(Membresia membresia, IUnidadDeTrabajo context, int idEntrenador)
        {
            if (!membresia.Estado)
            {
                throw new InvalidOperationException("La membresía no está habilitada.");
            }

            if (membresia.Plan == null || !membresia.Plan.Estado
                || !membresia.Plan.IncluyeEntrenador)
            {
                throw new InvalidOperationException("El plan actual no incluye entrenador.");
            }

            var entrenador = context.UsuariosSistema.Consultar("Rol")
                .SingleOrDefault(u => u.IdUsuarioSistema == idEntrenador);
            if (!ValidacionesGym.EsEntrenadorActivo(entrenador))
            {
                throw new InvalidOperationException("El usuario no posee rol de Entrenador activo.");
            }
        }
    }
}
