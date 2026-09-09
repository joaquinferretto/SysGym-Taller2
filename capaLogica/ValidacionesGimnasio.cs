using System;
using exxen2._0.capaDatos.Entidades;

namespace exxen2._0.capaLogica
{
    /* Centraliza las comprobaciones de roles activos para los casos de uso del gimnasio. */
    public static class ValidacionesGimnasio
    {
        public const int TamanoMaximoFoto = 2 * 1024 * 1024;

        /* Valida el sexo opcional, el límite de dos MiB y la firma del formato de la foto. */
        public static void ValidarFotoYSexo(byte[] foto, string sexo)
        {
            if (sexo != null && sexo != "M" && sexo != "F")
                throw new InvalidOperationException("El sexo debe ser Masculino, Femenino o quedar sin seleccionar.");
            if (foto == null)
                return;
            if (foto.Length > TamanoMaximoFoto)
                throw new InvalidOperationException("La foto no puede superar los 2 MB.");
            bool png = foto.Length >= 24 && foto[0] == 137 && foto[1] == 80 && foto[2] == 78 && foto[3] == 71 && foto[4] == 13 && foto[5] == 10 && foto[6] == 26 && foto[7] == 10;
            bool jpeg = foto.Length >= 4 && foto[0] == 255 && foto[1] == 216 && foto[2] == 255 && foto[foto.Length - 2] == 255 && foto[foto.Length - 1] == 217;
            bool bmp = foto.Length >= 54 && foto[0] == 66 && foto[1] == 77;
            if (!png && !jpeg && !bmp)
                throw new InvalidOperationException("La foto debe tener formato PNG, JPG o BMP válido.");
        }

        public const int PrimerDiaRutina = 1;
        public const int UltimoDiaRutina = 5;

        private static readonly string[] DiasRutina = { "Lunes", "Martes", "Miercoles", "Jueves", "Viernes" };

        /* Valida que el día de la rutina quede sin asignar o dentro de la semana de lunes a viernes. */
        public static void ValidarDiaRutina(int? dia)
        {
            if (dia.HasValue && (dia.Value < PrimerDiaRutina || dia.Value > UltimoDiaRutina))
                throw new InvalidOperationException("El dia de la rutina debe estar entre lunes y viernes.");
        }

        /* Traduce el número de día de la rutina al nombre que se muestra en pantalla. */
        public static string NombreDia(int? dia)
        {
            if (!dia.HasValue || dia.Value < PrimerDiaRutina || dia.Value > UltimoDiaRutina)
                return "Sin dia";
            return DiasRutina[dia.Value - PrimerDiaRutina];
        }

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
