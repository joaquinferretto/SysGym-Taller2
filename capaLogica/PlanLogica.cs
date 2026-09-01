using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using exxen2._0.capaDatos.Contexto;
using exxen2._0.capaDatos.Entidades;

namespace exxen2._0.capaLogica
{
    public class PlanLogica
    {
        public Plan Crear(Plan plan)
        {
            ValidarDatos(plan);
            using (var context = new GymContext())
            {
                plan.Estado = true;
                context.Planes.Add(plan);
                context.SaveChanges();
                return plan;
            }
        }

        public Plan Modificar(Plan plan)
        {
            ValidarDatos(plan);
            using (var context = new GymContext())
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
                context.SaveChanges();
                return existente;
            }
        }

        public Plan ObtenerPorId(int idPlan)
        {
            using (var context = new GymContext())
            {
                return context.Planes.Include(p => p.Rutina)
                    .SingleOrDefault(p => p.IdPlan == idPlan);
            }
        }

        public List<Plan> ListarActivos()
        {
            using (var context = new GymContext())
            {
                return context.Planes.Where(p => p.Estado).OrderBy(p => p.Nombre).ToList();
            }
        }

        public void DarDeBaja(int idPlan)
        {
            using (var context = new GymContext())
            {
                var plan = context.Planes.Find(idPlan);
                if (plan == null)
                {
                    throw new InvalidOperationException("El plan no existe.");
                }

                plan.Estado = false;
                context.SaveChanges();
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
