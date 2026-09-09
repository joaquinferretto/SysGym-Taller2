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
        /* Valida y registra planes mediante la unidad de trabajo, conservando sus reglas de alta. */
        public Plan Crear(Plan plan)
        {
            ValidarDatos(plan);
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                plan.RutinasDisponibles = ValidarRutinas(datos, plan);
                plan.Estado = true;
                datos.Planes.Agregar(plan);
                datos.GuardarCambios();
                return plan;
            }
        }

        /* Valida y guarda los cambios de planes sobre el registro existente. */
        public Plan Modificar(Plan plan)
        {
            ValidarDatos(plan);
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                var existente = datos.Planes.Consultar("RutinasDisponibles").SingleOrDefault(p => p.IdPlan == plan.IdPlan);
                if (existente == null)
                {
                    throw new InvalidOperationException("El plan no existe.");
                }

                var disponibles = ValidarRutinas(datos, plan);
                var permitidas = disponibles.Select(r => r.IdRutina).ToList();
                var retiradas = datos.RutinaAsignaciones.Where(a => a.Estado &&
                    a.Membresia.IdPlan == plan.IdPlan && !permitidas.Contains(a.IdRutina)).ToList();
                foreach (var asignacion in retiradas)
                {
                    asignacion.Estado = false;
                    asignacion.FechaFin = DateTime.Now;
                }
                existente.RutinasDisponibles.Clear();
                foreach (var rutina in disponibles)
                    existente.RutinasDisponibles.Add(rutina);
                existente.Nombre = plan.Nombre;
                existente.Descripcion = plan.Descripcion;
                existente.Precio = plan.Precio;
                existente.IncluyeEntrenador = plan.IncluyeEntrenador;
                existente.IncluyeRutinaPersonal = plan.IncluyeRutinaPersonal;
                existente.Estado = plan.Estado;
                existente.IdRutina = plan.IdRutina;
                datos.GuardarCambios();
                return existente;
            }
        }

        /* Busca el registro de planes por identificador y devuelve los datos disponibles. */
        public Plan ObtenerPorId(int idPlan)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                return datos.Planes.ConsultarSoloLectura("Rutina", "RutinasDisponibles").SingleOrDefault(p => p.IdPlan == idPlan);
            }
        }

        /* Consulta planes activos para devolver los datos a la capa visual. */
        public List<Plan> ListarActivos()
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                return datos.Planes.ConsultarSoloLectura("Rutina", "RutinasDisponibles").Where(p => p.Estado).OrderBy(p => p.Nombre).ToList();
            }
        }

        /* Consulta planes activos e inactivos para su gestión para devolver los datos a la capa visual. */
        public List<Plan> ListarParaGestion()
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                return datos.Planes.ConsultarSoloLectura("Rutina", "RutinasDisponibles").OrderByDescending(p => p.Estado).ThenBy(p => p.Nombre).ToList();
            }
        }

        /* Desactiva el registro de planes sin eliminar su historial. */
        public void DarDeBaja(int idPlan)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                var plan = datos.Planes.Buscar(idPlan);
                if (plan == null)
                {
                    throw new InvalidOperationException("El plan no existe.");
                }

                plan.Estado = false;
                datos.GuardarCambios();
            }
        }

        /* Recupera el estado activo del registro de planes según las validaciones de la operación. */
        public void Reactivar(int idPlan)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                var plan = datos.Planes.Buscar(idPlan);
                if (plan == null)
                {
                    throw new InvalidOperationException("El plan no existe.");
                }

                var rutina = datos.Rutinas.Buscar(plan.IdRutina);
                if (rutina == null || !rutina.Estado)
                {
                    throw new InvalidOperationException("No se puede reactivar el plan porque su rutina está inactiva.");
                }

                plan.Estado = true;
                datos.GuardarCambios();
            }
        }

        /* Comprueba el catálogo elegido y obtiene entidades con seguimiento para guardar los vínculos. */
        private static List<Rutina> ValidarRutinas(IUnidadDeTrabajo datos, Plan plan)
        {
            var identificadores = (plan.RutinasDisponibles ?? new List<Rutina>())
                .Select(r => r.IdRutina).Distinct().ToList();
            if (identificadores.Count == 0 || !identificadores.Contains(plan.IdRutina))
                throw new InvalidOperationException("Selecciona las rutinas disponibles e incluye la rutina base.");
            var seleccionadas = datos.Rutinas.Consultar("Entrenador.Rol")
                .Where(r => identificadores.Contains(r.IdRutina)).ToList();
            if (seleccionadas.Count != identificadores.Count ||
                seleccionadas.Any(r => !r.Estado || !ValidacionesGimnasio.EsEntrenadorActivo(r.Entrenador)))
                throw new InvalidOperationException("Las rutinas seleccionadas deben estar activas y tener un entrenador activo.");
            return seleccionadas;
        }

        /* Comprueba los campos y rangos obligatorios de planes antes de persistirlos. */
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
