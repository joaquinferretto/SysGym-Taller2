using System;
using System.Collections.Generic;
using System.Linq;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaDatos.Repositorios;

namespace exxen2._0.capaLogica
{
    public class RutinaAsignacionLogica
    {
        public RutinaAsignacion Asignar(int idRutina, int idMembresia)
        {
            if (idRutina <= 0 || idMembresia <= 0)
            {
                throw new InvalidOperationException("La rutina y la membresía son obligatorias.");
            }

            using (var datos = new GymUnidadDeTrabajo())
            {
                var rutina = datos.Rutinas.Buscar(idRutina);
                var membresia = datos.Membresias.Consultar("Plan", "Socio")
                    .SingleOrDefault(m => m.IdMembresia == idMembresia);
                if (rutina == null || !rutina.Estado)
                {
                    throw new InvalidOperationException("La rutina no existe o está inactiva.");
                }

                if (membresia == null || !membresia.Estado
                    || membresia.Socio == null || !membresia.Socio.Estado)
                {
                    throw new InvalidOperationException("La membresía seleccionada no está habilitada.");
                }

                if (membresia.Plan == null || !membresia.Plan.Estado)
                {
                    throw new InvalidOperationException("El plan de la membresía no está activo.");
                }

                if (!membresia.Plan.IncluyeRutinaPersonal)
                {
                    throw new InvalidOperationException("El plan de la membresía no incluye rutinas.");
                }

                if (datos.RutinaAsignaciones.Any(a => a.IdRutina == idRutina
                    && a.IdMembresia == idMembresia && a.Estado))
                {
                    throw new InvalidOperationException("La rutina ya está asignada a esta membresía.");
                }

                var asignacion = datos.RutinaAsignaciones
                    .FirstOrDefault(a => a.IdRutina == idRutina && a.IdMembresia == idMembresia);
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

        public List<RutinaAsignacion> ListarActivas()
        {
            using (var datos = new GymUnidadDeTrabajo())
            {
                return datos.RutinaAsignaciones.Consultar("Rutina.Entrenador", "Membresia.Socio", "Membresia.Plan")
                    .Where(a => a.Estado && a.Rutina.Estado && a.Membresia.Estado)
                    .OrderByDescending(a => a.FechaAsignacion).ToList();
            }
        }

        public List<RutinaAsignacion> ListarPorEntrenador(int idEntrenador)
        {
            using (var datos = new GymUnidadDeTrabajo())
            {
                return datos.RutinaAsignaciones.Consultar("Rutina.Entrenador", "Membresia.Socio")
                    .Where(a => a.Estado && a.Rutina.Estado && a.Rutina.IdEntrenador == idEntrenador
                        && a.Membresia.Estado)
                    .OrderByDescending(a => a.FechaAsignacion).ToList();
            }
        }

        public void Desasignar(int idRutinaAsignacion)
        {
            using (var datos = new GymUnidadDeTrabajo())
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
