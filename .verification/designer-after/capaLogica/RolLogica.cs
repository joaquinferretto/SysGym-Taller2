using System.Collections.Generic;
using System.Linq;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaDatos.Repositorios;

namespace exxen2._0.capaLogica
{
    /* Coordina las operaciones y validaciones de negocio de roles. */
    public class RolLogica
    {
        /* Busca el registro de roles por identificador y devuelve los datos disponibles. */
        public Rol ObtenerPorId(int idRol)
        {
            using (var context = new GymUnidadDeTrabajo())
            {
                return context.Roles.ConsultarSoloLectura().SingleOrDefault(r => r.IdRol == idRol);
            }
        }

        /* Busca el registro de roles por descripción y devuelve los datos disponibles. */
        public Rol ObtenerPorDescripcion(string descripcion)
        {
            if (string.IsNullOrWhiteSpace(descripcion))
            {
                return null;
            }

            using (var context = new GymUnidadDeTrabajo())
            {
                return context.Roles.ConsultarSoloLectura().SingleOrDefault(r => r.Descripcion == descripcion);
            }
        }

        /* Consulta roles activos para devolver los datos a la capa visual. */
        public List<Rol> ListarActivos()
        {
            using (var context = new GymUnidadDeTrabajo())
            {
                return context.Roles.ConsultarSoloLectura().Where(r => r.Estado).OrderBy(r => r.Descripcion).ToList();
            }
        }
    }
}
