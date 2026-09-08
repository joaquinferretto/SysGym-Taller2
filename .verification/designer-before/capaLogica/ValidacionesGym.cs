using System;
using exxen2._0.capaDatos.Entidades;

namespace exxen2._0.capaLogica
{
    public static class ValidacionesGym
    {
        public static bool PuedeRegistrarMembresia(UsuarioSistema usuario)
        {
            return TieneRolActivo(usuario, "Administrador") || TieneRolActivo(usuario, "Recepcionista");
        }

        public static bool EsEntrenadorActivo(UsuarioSistema usuario)
        {
            return TieneRolActivo(usuario, "Entrenador");
        }

        private static bool TieneRolActivo(UsuarioSistema usuario, string descripcionRol)
        {
            return usuario != null
                && usuario.Estado
                && usuario.Rol != null
                && string.Equals(usuario.Rol.Descripcion, descripcionRol, StringComparison.OrdinalIgnoreCase)
                && usuario.Rol.Estado;
        }
    }
}
