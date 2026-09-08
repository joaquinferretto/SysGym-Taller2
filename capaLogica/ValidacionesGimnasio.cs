using System;
using exxen2._0.capaDatos.Entidades;

namespace exxen2._0.capaLogica
{
    /* Centraliza las comprobaciones de roles activos para los casos de uso del gimnasio. */
    public static class ValidacionesGimnasio
    {
        /* Comprueba si el usuario activo tiene rol de administrador o recepcionista. */
        public static bool PuedeRegistrarMembresia(UsuarioSistema usuario)
        {
            return TieneRolActivo(usuario, "Administrador") || TieneRolActivo(usuario, "Recepcionista");
        }

        /* Comprueba que tanto el usuario como su rol de entrenador estén activos. */
        public static bool EsEntrenadorActivo(UsuarioSistema usuario)
        {
            return TieneRolActivo(usuario, "Entrenador");
        }

        /* Valida el estado del usuario y del rol, comparando su descripción sin distinguir mayúsculas. */
        private static bool TieneRolActivo(UsuarioSistema usuario, string descripcionRol)
        {
            return usuario != null && usuario.Estado && usuario.Rol != null && string.Equals(usuario.Rol.Descripcion, descripcionRol, StringComparison.OrdinalIgnoreCase) && usuario.Rol.Estado;
        }
    }
}
