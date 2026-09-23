using System.Collections.Generic;
using System.Linq;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaDatos.Repositorios;

namespace exxen2._0.capaLogica
{
    /* Coordina las operaciones y validaciones de negocio de roles. */
    // Consulta de roles (Administrador, Entrenador, Recepcionista). La usa GestionUsuariosFormulario.
    public class RolLogica
    {
        /* Busca el registro de roles por identificador y devuelve los datos disponibles. */
        public Rol ObtenerPorId(int idRol)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())  // Abre la conexión; al salir del bloque se cierra sola, aunque haya error (try-with-resources).
            {
                return datos.Roles.ConsultarSoloLectura().SingleOrDefault(r => r.IdRol == idRol);  // Solo lectura: EF no vigila cambios (más liviano para listar).
            }
        }

        /* Busca el registro de roles por descripción y devuelve los datos disponibles. */
        public Rol ObtenerPorDescripcion(string descripcion)
        {
            if (string.IsNullOrWhiteSpace(descripcion))
            {
                return null;
            }

            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                return datos.Roles.ConsultarSoloLectura().SingleOrDefault(r => r.Descripcion == descripcion);  // Devuelve el único que cumple o null.
            }
        }

        /* Consulta roles activos para devolver los datos a la capa visual. */
        public List<Rol> ListarActivos()
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                return datos.Roles.ConsultarSoloLectura().Where(r => r.Estado).OrderBy(r => r.Descripcion).ToList();  // Acá se ejecuta la consulta en SQL y se trae la lista.
            }
        }
    }
}
