using System;
using System.Linq;
using exxen2._0.capaDatos.Entidades;

namespace exxen2._0.capaLogica
{
    /* Centraliza las comprobaciones de roles activos para los casos de uso del gimnasio. */
    // Validaciones reutilizables (nombre, DNI, edad, días de rutina) y controles de rol.
    // static class: no se crea con new, se usa directo: ValidacionesGimnasio.ValidarDni(...).
    public static class ValidacionesGimnasio
    {
        public const int LongitudMaximaNombrePersona = 100;
        public const int LongitudMaximaDni = 20;
        public const int LongitudMaximaNombreUsuario = 50;
        public const int TamanoMaximoFoto = ProcesadorImagenes.TamanoMaximoBytes;

        /* Valida un nombre humano sin aceptar numeros ni simbolos ajenos al nombre. */
        public static void ValidarNombre(string valor, string campo)
        {
            if (string.IsNullOrWhiteSpace(valor))
                throw new InvalidOperationException("El " + campo + " es obligatorio.");  // Regla incumplida: corta la operación y el formulario muestra este mensaje.
            if (valor.Trim().Length > LongitudMaximaNombrePersona)
                throw new InvalidOperationException("El " + campo + " no puede superar " + LongitudMaximaNombrePersona + " caracteres.");
            if (!valor.Any(char.IsLetter) || valor.Any(caracter => !char.IsLetter(caracter) && caracter != ' ' && caracter != '\'' && caracter != '-'))  // ¿Existe al menos uno? (no trae filas).
                throw new InvalidOperationException("El " + campo + " contiene caracteres no válidos.");
        }

        /* Valida el DNI en el formato numerico que conserva el modelo actual. */
        public static void ValidarDni(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni))
                throw new InvalidOperationException("El DNI es obligatorio.");
            if (dni.Trim().Length > LongitudMaximaDni)
                throw new InvalidOperationException("El DNI no puede superar " + LongitudMaximaDni + " dígitos.");
            if (dni.Any(caracter => caracter < '0' || caracter > '9'))
                throw new InvalidOperationException("El DNI solo puede contener números.");
        }

        /* Valida el identificador de acceso sin aplicarle las reglas de nombres personales. */
        public static void ValidarNombreUsuario(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                throw new InvalidOperationException("El nombre de usuario es obligatorio.");
            var normalizado = valor.Trim();
            if (normalizado.Length > LongitudMaximaNombreUsuario)
                throw new InvalidOperationException("El nombre de usuario no puede superar " + LongitudMaximaNombreUsuario + " caracteres.");
            if (normalizado.Any(caracter => !char.IsLetterOrDigit(caracter) && caracter != '.' && caracter != '_' && caracter != '-'))
                throw new InvalidOperationException("El nombre de usuario contiene caracteres no válidos.");
        }

        /* Calcula la edad completa considerando si el cumpleaños ya ocurrió. */
        public static int CalcularEdad(DateTime fechaNacimiento, DateTime fechaReferencia)
        {
            var edad = fechaReferencia.Year - fechaNacimiento.Year;
            if (fechaNacimiento.Date > fechaReferencia.Date.AddYears(-edad))
                edad--;
            return edad;
        }

        /* Valida que la fecha exista, no sea futura y cumpla la edad minima solicitada. */
        public static void ValidarEdadMinima(DateTime? fechaNacimiento, int edadMinima, string mensajeEdad)
        {
            if (!fechaNacimiento.HasValue)  // HasValue: ¿el valor opcional (int?, DateTime?) tiene dato?
                throw new InvalidOperationException("La fecha de nacimiento es obligatoria.");
            if (fechaNacimiento.Value.Date > DateTime.Today)
                throw new InvalidOperationException("La fecha de nacimiento no puede ser futura.");
            if (CalcularEdad(fechaNacimiento.Value.Date, DateTime.Today) < edadMinima)
                throw new InvalidOperationException(mensajeEdad);
        }

        /* Valida el sexo opcional y delega la imagen a la política técnica central. */
        public static void ValidarFotoYSexo(byte[] foto, string sexo)
        {
            if (sexo != null && sexo != "M" && sexo != "F")
                throw new InvalidOperationException("El sexo debe ser Masculino, Femenino o quedar sin seleccionar.");
            if (foto == null)
                return;
            ValidarImagen(foto);
        }

        /* Comprueba firma, tamaño, dimensiones y decodificación mediante el procesador central. */
        public static void ValidarImagen(byte[] imagen)
        {
            ProcesadorImagenes.Validar(imagen);
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

        /* Comprueba que el usuario activo pueda crear y gestionar rutinas. */
        public static bool PuedeGestionarRutinas(UsuarioSistema usuario)
        {
            return EsEntrenadorActivo(usuario) || EsAdministradorActivo(usuario);
        }

        /* Comprueba que el usuario activo tenga rol de administrador. */
        public static bool EsAdministradorActivo(UsuarioSistema usuario)
        {
            return TieneRolActivo(usuario, "Administrador");
        }

        /* Valida el estado del usuario y del rol, comparando su descripción sin distinguir mayúsculas. */
        private static bool TieneRolActivo(UsuarioSistema usuario, string descripcionRol)
        {
            return usuario != null && usuario.Estado && usuario.Rol != null && string.Equals(usuario.Rol.Descripcion, descripcionRol, StringComparison.OrdinalIgnoreCase) && usuario.Rol.Estado;
        }
    }
}
