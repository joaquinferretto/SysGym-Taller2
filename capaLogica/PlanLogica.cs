using System;
using System.Collections.Generic;
using System.Linq;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaDatos.Repositorios;

namespace exxen2._0.capaLogica
{
    /* Coordina las operaciones y validaciones de negocio de planes. */
    // Reglas de los planes (nombre obligatorio, precio > 0) y altas/bajas.
    // La usan GestionPlanesFormulario y GestionMembresiasFormulario (combo de planes).
    public class PlanLogica
    {
        /* Valida y registra un plan activo mediante la unidad de trabajo. */
        public Plan Crear(Plan plan)
        {
            ValidarDatos(plan);
            using (var datos = new UnidadDeTrabajoGimnasio())  // Abre la conexión; al salir del bloque se cierra sola, aunque haya error (try-with-resources).
            {
                plan.Estado = true;
                datos.Planes.Agregar(plan);  // Deja el objeto listo para INSERT (se ejecuta en GuardarCambios).
                datos.GuardarCambios();  // EF envía a SQL los INSERT/UPDATE pendientes.
                return plan;
            }
        }

        /* Valida y guarda los datos propios del plan sin modificar membresías históricas. */
        public Plan Modificar(Plan plan)
        {
            ValidarDatos(plan);
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                var existente = datos.Planes.Buscar(plan.IdPlan);  // Busca por clave primaria; si no existe devuelve null.
                if (existente == null)
                    throw new InvalidOperationException("El plan no existe.");  // Regla incumplida: corta la operación y el formulario muestra este mensaje.

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
                return datos.Planes.ConsultarSoloLectura().SingleOrDefault(p => p.IdPlan == idPlan);  // Solo lectura: EF no vigila cambios (más liviano para listar).
        }

        /* Consulta planes activos para los combos de membresías. */
        public List<Plan> ListarActivos()
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
                return datos.Planes.ConsultarSoloLectura().Where(p => p.Estado).OrderBy(p => p.Nombre).ToList();  // Acá se ejecuta la consulta en SQL y se trae la lista.
        }

        /* Consulta planes activos e inactivos para su gestión. */
        public List<Plan> ListarParaGestion()
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
                return datos.Planes.ConsultarSoloLectura().OrderByDescending(p => p.Estado).ThenBy(p => p.Nombre).ToList();  // Ordena (ORDER BY).
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
                throw new ArgumentNullException("plan");  // Se recibió null donde no corresponde.
            if (string.IsNullOrWhiteSpace(plan.Nombre))
                throw new InvalidOperationException("El nombre del plan es obligatorio.");
            if (plan.Precio <= 0)
                throw new InvalidOperationException("El precio debe ser mayor que cero.");
        }
    }
}
