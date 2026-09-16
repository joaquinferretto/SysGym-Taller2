using System;
using System.Collections.Generic;
using System.Linq;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaDatos.Repositorios;

namespace exxen2._0.capaLogica
{
    /* Resume una membresía y su entrenador vigente para la pantalla de asignaciones. */
    public sealed class MembresiaAsignacionItem
    {
        public int IdMembresia { get; set; }
        public int IdMembresiaEntrenador { get; set; }
        public string NombreSocio { get; set; }
        public string DNI { get; set; }
        public string NombrePlan { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string NombreEntrenador { get; set; }
        public int IdEntrenador { get; set; }
        public bool EstadoMembresia { get; set; }
        public bool Asignado { get; set; }
    }

    /* Coordina las operaciones y validaciones de negocio de asignaciones de entrenador. */
    public class MembresiaEntrenadorLogica
    {
        /* Valida los beneficios de la membresía y crea su asignación de entrenador activo. */
        public MembresiaEntrenador AsignarEntrenador(int idMembresia, int idEntrenador)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            using (var transaccion = datos.IniciarTransaccion())
            {
                var membresia = ObtenerMembresiaConPlan(datos, idMembresia);
                ValidarAsignacion(membresia, datos, idEntrenador);
                if (datos.MembresiasEntrenadores.Any(me => me.IdMembresia == idMembresia && me.Estado))
                {
                    throw new InvalidOperationException("La membresía ya posee un entrenador activo.");
                }

                var asignacion = new MembresiaEntrenador
                {
                    IdMembresia = idMembresia,
                    IdEntrenador = idEntrenador,
                    Estado = true
                };
                datos.MembresiasEntrenadores.Agregar(asignacion);
                datos.GuardarCambios();
                transaccion.Confirmar();
                return asignacion;
            }
        }

        /* Finaliza las asignaciones activas y registra el nuevo entrenador en una transacción. */
        public MembresiaEntrenador CambiarEntrenador(int idMembresia, int idEntrenador)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            using (var transaccion = datos.IniciarTransaccion())
            {
                var membresia = ObtenerMembresiaConPlan(datos, idMembresia);
                ValidarAsignacion(membresia, datos, idEntrenador);
                var activas = datos.MembresiasEntrenadores.Where(me => me.IdMembresia == idMembresia && me.Estado).ToList();
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
                datos.MembresiasEntrenadores.Agregar(nueva);
                datos.GuardarCambios();
                transaccion.Confirmar();
                return nueva;
            }
        }

        /* Obtiene el entrenador de la asignación activa de la membresía, si existe. */
        public UsuarioSistema ObtenerEntrenadorActivo(int idMembresia)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                return datos.MembresiasEntrenadores.ConsultarSoloLectura("Entrenador").Where(me => me.IdMembresia == idMembresia && me.Estado).Select(me => me.Entrenador).SingleOrDefault();
            }
        }

        /* Consulta asignaciones de entrenador de la membresía indicada para devolver los datos a la capa visual. */
        public List<MembresiaEntrenador> ListarPorMembresia(int idMembresia)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                return datos.MembresiasEntrenadores.ConsultarSoloLectura("Entrenador").Where(me => me.IdMembresia == idMembresia).OrderByDescending(me => me.IdMembresiaEntrenador).ToList();
            }
        }

        /* Consulta membresías activas e históricas con el contexto mínimo para asignar entrenador. */
        public List<MembresiaAsignacionItem> ListarParaGestion()
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                return datos.Membresias.ConsultarSoloLectura("Socio", "Plan", "Entrenadores.Entrenador")
                    .OrderByDescending(m => m.Estado)
                    .ThenBy(m => m.Socio.Apellido)
                    .ThenBy(m => m.Socio.Nombre)
                    .ToList()
                    .Select(m =>
                    {
                        var activo = m.Entrenadores == null ? null : m.Entrenadores.FirstOrDefault(e => e.Estado);
                        return new MembresiaAsignacionItem
                        {
                            IdMembresia = m.IdMembresia,
                            IdMembresiaEntrenador = activo == null ? 0 : activo.IdMembresiaEntrenador,
                            NombreSocio = m.Socio == null ? "Socio no disponible" : m.Socio.Apellido + ", " + m.Socio.Nombre,
                            DNI = m.Socio == null ? "-" : m.Socio.DNI,
                            NombrePlan = m.Plan == null ? "-" : m.Plan.Nombre,
                            FechaVencimiento = m.FechaVencimiento,
                            NombreEntrenador = activo == null || activo.Entrenador == null ? "Sin asignar" : activo.Entrenador.Apellido + ", " + activo.Entrenador.Nombre + " - DNI " + activo.Entrenador.DNI,
                            IdEntrenador = activo == null ? 0 : activo.IdEntrenador,
                            EstadoMembresia = m.Estado,
                            Asignado = activo != null
                        };
                    })
                    .ToList();
            }
        }

        /* Desactiva la asignación del entrenador conservando el registro histórico. */
        public void DarDeBajaAsignacion(int idMembresiaEntrenador)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                var asignacion = datos.MembresiasEntrenadores.Buscar(idMembresiaEntrenador);
                if (asignacion == null)
                {
                    throw new InvalidOperationException("La asignación no existe.");
                }

                asignacion.Estado = false;
                datos.GuardarCambios();
            }
        }

        /* Valida el plan y el entrenador antes de recuperar una asignación sin duplicar la activa. */
        public void ReactivarAsignacion(int idMembresiaEntrenador)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                var asignacion = datos.MembresiasEntrenadores.Consultar("Membresia.Plan", "Entrenador.Rol").SingleOrDefault(me => me.IdMembresiaEntrenador == idMembresiaEntrenador);
                if (asignacion == null)
                {
                    throw new InvalidOperationException("La asignación no existe.");
                }

                ValidarAsignacion(asignacion.Membresia, datos, asignacion.IdEntrenador);
                if (datos.MembresiasEntrenadores.Any(me => me.IdMembresia == asignacion.IdMembresia && me.Estado && me.IdMembresiaEntrenador != idMembresiaEntrenador))
                {
                    throw new InvalidOperationException("La membresía ya posee un entrenador activo.");
                }

                asignacion.Estado = true;
                datos.GuardarCambios();
            }
        }

        /* Carga la membresía junto con su plan y rechaza identificadores inexistentes. */
        private static Membresia ObtenerMembresiaConPlan(IUnidadDeTrabajo datos, int idMembresia)
        {
            var membresia = datos.Membresias.Consultar("Plan").SingleOrDefault(m => m.IdMembresia == idMembresia);
            if (membresia == null)
            {
                throw new InvalidOperationException("La membresía no existe.");
            }

            return membresia;
        }

        /* Comprueba membresía habilitada, beneficio del plan y rol activo del entrenador. */
        private static void ValidarAsignacion(Membresia membresia, IUnidadDeTrabajo datos, int idEntrenador)
        {
            if (!membresia.Estado)
            {
                throw new InvalidOperationException("La membresía no está habilitada.");
            }

            if (membresia.Plan == null || !membresia.Plan.Estado)
            {
                throw new InvalidOperationException("El plan actual no esta habilitado.");
            }

            var entrenador = datos.UsuariosSistema.Consultar("Rol").SingleOrDefault(u => u.IdUsuarioSistema == idEntrenador);
            if (!ValidacionesGimnasio.EsEntrenadorActivo(entrenador))
            {
                throw new InvalidOperationException("El usuario no posee rol de Entrenador activo.");
            }
        }
    }
}
