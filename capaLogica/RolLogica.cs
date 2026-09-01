using System.Collections.Generic;
using System.Linq;
using exxen2._0.capaDatos.Contexto;
using exxen2._0.capaDatos.Entidades;

namespace exxen2._0.capaLogica
{
    public class RolLogica
    {
        public Rol ObtenerPorId(int idRol)
        {
            using (var context = new GymContext())
            {
                return context.Roles.SingleOrDefault(r => r.IdRol == idRol);
            }
        }

        public Rol ObtenerPorDescripcion(string descripcion)
        {
            if (string.IsNullOrWhiteSpace(descripcion))
            {
                return null;
            }

            using (var context = new GymContext())
            {
                return context.Roles.SingleOrDefault(r => r.Descripcion == descripcion);
            }
        }

        public List<Rol> ListarActivos()
        {
            using (var context = new GymContext())
            {
                return context.Roles.Where(r => r.Estado).OrderBy(r => r.Descripcion).ToList();
            }
        }
    }
}
