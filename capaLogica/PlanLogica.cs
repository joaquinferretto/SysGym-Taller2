using System;
using System.Collections.Generic;
using System.Linq;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaDatos.Repositorios;

namespace exxen2._0.capaLogica
{
    /* Coordina las operaciones y validaciones de negocio de planes. */
    public class PlanLogica
    {
        /* Valida y registra un plan activo mediante la unidad de trabajo. */
        public Plan Crear(Plan plan)
        {
            ValidarDatos(plan);
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                plan.Estado = true;
                datos.Planes.Agregar(plan);
                datos.GuardarCambios();
                return plan;
            }
        }

        /* Valida y guarda los datos propios del plan sin modificar membresías históricas. */
        public Plan Modificar(Plan plan)
        {
            ValidarDatos(plan);
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                var existente = datos.Planes.Buscar(plan.IdPlan);
                if (existente == null)
                    throw new InvalidOperationException("El plan no existe.");

                existente.Nombre = plan.Nombre;
                existente.Descripcion = plan.Descripcion;
                existente.Precio = plan.Precio;
                existente.Estado = plan.Estado;
                datos.GuardarCambios();
                return existente;
            }
        }

        /* Busca un plan por identificador para mostrarlo en la interfaz. */
        public Plan ObtenerPorId(int idPlan)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
                return datos.Planes.ConsultarSoloLectura().SingleOrDefault(p => p.IdPlan == idPlan);
        }

        /* Consulta planes activos para los combos de membresías. */
        public List<Plan> ListarActivos()
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
                return datos.Planes.ConsultarSoloLectura().Where(p => p.Estado).OrderBy(p => p.Nombre).ToList();
        }

        /* Consulta planes activos e inactivos para su gestión. */
        public List<Plan> ListarParaGestion()
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
                return datos.Planes.ConsultarSoloLectura().OrderByDescending(p => p.Estado).ThenBy(p => p.Nombre).ToList();
        }

        /* Desactiva un plan sin borrar membresías ni cuotas históricas. */
        public void DarDeBaja(int idPlan)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                var plan = datos.Planes.Buscar(idPlan);
                if (plan == null)
                    throw new InvalidOperationException("El plan no existe.");

                plan.Estado = false;
                datos.GuardarCambios();
            }
        }

        /* Reactiva un plan existente sin imponer una rutina ni otro beneficio opcional. */
        public void Reactivar(int idPlan)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                var plan = datos.Planes.Buscar(idPlan);
                if (plan == null)
                    throw new InvalidOperationException("El plan no existe.");

                plan.Estado = true;
                datos.GuardarCambios();
            }
        }

        /* Comprueba los campos obligatorios y el importe positivo del plan. */
        private static void ValidarDatos(Plan plan)
        {
            if (plan == null)
                throw new ArgumentNullException("plan");
            if (string.IsNullOrWhiteSpace(plan.Nombre))
                throw new InvalidOperationException("El nombre del plan es obligatorio.");
            if (plan.Precio <= 0)
                throw new InvalidOperationException("El precio debe ser mayor que cero.");
        }
    }
}
