using System;
using System.Collections.Generic;
using System.Linq;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaDatos.Repositorios;

namespace exxen2._0.capaLogica
{
    /* Resume un socio con membresía y cantidad de rutinas para la pantalla del entrenador. */
    public sealed class SocioRutinaItem
    {
        public int IdSocio { get; set; }
        public int IdMembresia { get; set; }
        public string NombreSocio { get; set; }
        public string NombrePlan { get; set; }
        public int RutinasAsignadas { get; set; }
        public bool IncluyeRutinaPersonal { get; set; }
    }

    /* Coordina las operaciones y validaciones de negocio de asignaciones de rutina. */
    public class RutinaAsignacionLogica
    {
        /* Valida rutina, entrenador y beneficios de la membresía antes de vincular la plantilla. */
        public RutinaAsignacion Asignar(int idRutina, int idMembresia)
        {
            if (idRutina <= 0 || idMembresia <= 0)
            {
                throw new InvalidOperationException("La rutina y la membresía son obligatorias.");
            }

            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                var rutina = datos.Rutinas.Buscar(idRutina);
                var membresia = datos.Membresias.Consultar("Plan", "Socio").SingleOrDefault(m => m.IdMembresia == idMembresia);
                if (rutina == null || !rutina.Estado)
                {
                    throw new InvalidOperationException("La rutina no existe o está inactiva.");
                }

                var entrenador = datos.UsuariosSistema.ConsultarSoloLectura("Rol").SingleOrDefault(u => u.IdUsuarioSistema == rutina.IdEntrenador);
                if (!ValidacionesGimnasio.PuedeGestionarRutinas(entrenador))
                {
                    throw new InvalidOperationException("La rutina requiere un entrenador o administrador activo.");
                }

                if (membresia == null || !membresia.Estado || membresia.Socio == null || !membresia.Socio.Estado)
                {
                    throw new InvalidOperationException("La membresía seleccionada no está habilitada.");
                }

                if (membresia.Plan == null || !membresia.Plan.Estado)
                {
                    throw new InvalidOperationException("El plan de la membresía no está activo.");
                }

                if (!datos.Planes.Any(p => p.IdPlan == membresia.IdPlan && p.RutinasDisponibles.Any(r => r.IdRutina == idRutina)))
                {
                    throw new InvalidOperationException("La rutina seleccionada no está disponible para el plan de esta membresía.");
                }

                if (datos.RutinaAsignaciones.Any(a => a.IdRutina == idRutina && a.IdMembresia == idMembresia && a.Estado))
                {
                    throw new InvalidOperationException("La rutina ya está asignada a esta membresía.");
                }

                var asignacion = datos.RutinaAsignaciones.FirstOrDefault(a => a.IdRutina == idRutina && a.IdMembresia == idMembresia);
                if (asignacion == null)
                {
                    asignacion = new RutinaAsignacion
                    {
                        IdRutina = idRutina,
                        IdMembresia = idMembresia
                    };
                    datos.RutinaAsignaciones.Agregar(asignacion);
                }

                asignacion.FechaAsignacion = DateTime.Now;
                asignacion.FechaFin = null;
                asignacion.Estado = true;
                datos.GuardarCambios();
                return asignacion;
            }
        }

        /* Devuelve las membresías cuyo plan permite la rutina seleccionada para evitar opciones inválidas. */
        public List<Membresia> ListarMembresiasDisponibles(int idRutina)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                return datos.Membresias.ConsultarSoloLectura("Plan", "Socio")
                    .Where(m => m.Estado && m.Socio.Estado && m.Plan.Estado &&
                        m.Plan.RutinasDisponibles.Any(r => r.IdRutina == idRutina && r.Estado &&
                            r.Entrenador.Estado && r.Entrenador.Rol.Estado &&
                            (r.Entrenador.Rol.Descripcion == "Entrenador" || r.Entrenador.Rol.Descripcion == "Administrador")))
                    .OrderBy(m => m.Socio.Apellido).ThenBy(m => m.Socio.Nombre).ToList();
            }
        }

        /* Consulta los socios del entrenador con un inner join para mostrarlos en la grilla. */
        public List<SocioRutinaItem> ListarSociosPorEntrenador(int idEntrenador)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                var consulta =
                    from asignacionEntrenador in datos.MembresiasEntrenadores
                    join membresia in datos.Membresias on asignacionEntrenador.IdMembresia equals membresia.IdMembresia
                    join socio in datos.Socios on membresia.IdSocio equals socio.IdSocio
                    join plan in datos.Planes on membresia.IdPlan equals plan.IdPlan
                    join asignacionRutina in datos.RutinaAsignaciones
                        on membresia.IdMembresia equals asignacionRutina.IdMembresia into rutinasSocio
                    where (idEntrenador <= 0 || asignacionEntrenador.IdEntrenador == idEntrenador)
                        && asignacionEntrenador.Estado
                        && membresia.Estado
                        && socio.Estado
                        && plan.Estado
                    select new SocioRutinaItem
                    {
                        IdSocio = socio.IdSocio,
                        IdMembresia = membresia.IdMembresia,
                        NombreSocio = socio.Apellido + ", " + socio.Nombre,
                        NombrePlan = plan.Nombre,
                        RutinasAsignadas = rutinasSocio.Count(r => r.Estado),
                        IncluyeRutinaPersonal = plan.IncluyeRutinaPersonal
                    };

                return consulta.OrderBy(s => s.NombreSocio).ToList();
            }
        }

        /* Consulta todos los socios con entrenador activo para la gestión del administrador. */
        public List<SocioRutinaItem> ListarSociosParaAdministracion()
        {
            return ListarSociosPorEntrenador(0);
        }

        /* Consulta asignaciones de rutina activas para devolver los datos a la capa visual. */
        public List<RutinaAsignacion> ListarActivas()
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                return datos.RutinaAsignaciones.ConsultarSoloLectura("Rutina.Entrenador", "Membresia.Socio", "Membresia.Plan").Where(a => a.Estado && a.Rutina.Estado && a.Membresia.Estado).OrderByDescending(a => a.FechaAsignacion).ToList();
            }
        }

        /* Consulta asignaciones de rutina del entrenador indicado para devolver los datos a la capa visual. */
        public List<RutinaAsignacion> ListarPorEntrenador(int idEntrenador)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                return datos.RutinaAsignaciones.ConsultarSoloLectura("Rutina.Entrenador", "Membresia.Socio").Where(a => a.Estado && a.Rutina.Estado && a.Rutina.IdEntrenador == idEntrenador && a.Membresia.Estado).OrderByDescending(a => a.FechaAsignacion).ToList();
            }
        }

        /* Finaliza la asignación de rutina y conserva su fecha de cierre. */
        public void Desasignar(int idRutinaAsignacion)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                var asignacion = datos.RutinaAsignaciones.Buscar(idRutinaAsignacion);
                if (asignacion == null)
                {
                    throw new InvalidOperationException("La asignación no existe.");
                }

                asignacion.Estado = false;
                asignacion.FechaFin = DateTime.Now;
                datos.GuardarCambios();
            }
        }
    }
}
