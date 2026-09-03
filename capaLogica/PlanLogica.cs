using System;
using System.Collections.Generic;
using System.Linq;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaDatos.Repositorios;

namespace exxen2._0.capaLogica
{
    public class PlanLogica
    {
        public Plan Crear(Plan plan)
        {
            ValidarDatos(plan);
            using (var context = new GymUnidadDeTrabajo())
            {
                plan.Estado = true;
                context.Planes.Add(plan);
                context.GuardarCambios();
                return plan;
            }
        }

        public Plan Modificar(Plan plan)
        {
            ValidarDatos(plan);
            using (var context = new GymUnidadDeTrabajo())
            {
                var existente = context.Planes.Find(plan.IdPlan);
                if (existente == null)
                {
                    throw new InvalidOperationException("El plan no existe.");
                }

                existente.Nombre = plan.Nombre;
                existente.Descripcion = plan.Descripcion;
                existente.Precio = plan.Precio;
                existente.IncluyeEntrenador = plan.IncluyeEntrenador;
                existente.IncluyeRutinaPersonal = plan.IncluyeRutinaPersonal;
                existente.Estado = plan.Estado;
                existente.IdRutina = plan.IdRutina;
                context.GuardarCambios();
                return existente;
            }
        }

        public Plan ObtenerPorId(int idPlan)
        {
            using (var context = new GymUnidadDeTrabajo())
            {
                return context.Planes.Consultar("Rutina")
                    .SingleOrDefault(p => p.IdPlan == idPlan);
            }
        }

        public List<Plan> ListarActivos()
        {
            using (var context = new GymUnidadDeTrabajo())
            {
                return context.Planes.Consultar("Rutina")
                    .Where(p => p.Estado).OrderBy(p => p.Nombre).ToList();
            }
        }

        public List<Plan> ListarParaGestion()
        {
            using (var context = new GymUnidadDeTrabajo())
            {
                return context.Planes.Consultar("Rutina")
                    .OrderByDescending(p => p.Estado).ThenBy(p => p.Nombre).ToList();
            }
        }

        public void DarDeBaja(int idPlan)
        {
            using (var context = new GymUnidadDeTrabajo())
            {
                var plan = context.Planes.Find(idPlan);
                if (plan == null)
                {
                    throw new InvalidOperationException("El plan no existe.");
                }

                plan.Estado = false;
                context.GuardarCambios();
            }
        }

        public void Reactivar(int idPlan)
        {
            using (var context = new GymUnidadDeTrabajo())
            {
                var plan = context.Planes.Find(idPlan);
                if (plan == null)
                {
                    throw new InvalidOperationException("El plan no existe.");
                }

                var rutina = context.Rutinas.Find(plan.IdRutina);
                if (rutina == null || !rutina.Estado)
                {
                    throw new InvalidOperationException("No se puede reactivar el plan porque su rutina está inactiva.");
                }

                plan.Estado = true;
                context.GuardarCambios();
            }
        }

        private static void ValidarDatos(Plan plan)
        {
            if (plan == null)
            {
                throw new ArgumentNullException("plan");
            }

            if (string.IsNullOrWhiteSpace(plan.Nombre))
            {
                throw new InvalidOperationException("El nombre del plan es obligatorio.");
            }

            if (plan.Precio <= 0)
            {
                throw new InvalidOperationException("El precio debe ser mayor que cero.");
            }

            if (plan.IdRutina <= 0)
            {
                throw new InvalidOperationException("La rutina del plan es obligatoria.");
            }
        }
    }
}
