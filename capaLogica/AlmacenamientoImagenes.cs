using System;
using System.IO;

namespace exxen2._0.capaLogica
{
    /* Centraliza las carpetas administradas por SysGym y las rutas relativas de sus imágenes. */
    public static class AlmacenamientoImagenes
    {
        private const string CarpetaImagenes = "Imagenes";
        private const string CarpetaSocios = "Socios";
        private const string CarpetaUsuarios = "Usuarios";
        private const string CarpetaEjercicios = "Ejercicios";

        /* Guarda una foto de socio dentro de Datos/Imagenes/Socios y devuelve la ruta relativa. */
        public static string GuardarSocio(byte[] contenido, string extension)
        {
            return Guardar(contenido, extension, CarpetaSocios);
        }

        /* Guarda una foto de usuario dentro de Datos/Imagenes/Usuarios y devuelve la ruta relativa. */
        public static string GuardarUsuario(byte[] contenido, string extension)
        {
            return Guardar(contenido, extension, CarpetaUsuarios);
        }

        /* Guarda una imagen de ejercicio dentro de Datos/Imagenes/Ejercicios/{id}. */
        public static string GuardarEjercicio(byte[] contenido, string extension, int idEjercicio)
        {
            if (idEjercicio <= 0)
                throw new InvalidOperationException("Primero guarde el ejercicio antes de agregar imágenes.");

            return Guardar(contenido, extension, Path.Combine(CarpetaEjercicios, idEjercicio.ToString()));
        }

        /* Resuelve una ruta relativa de imagen dentro de la carpeta portable Datos. */
        public static string RutaAbsoluta(string rutaRelativa)
        {
            if (string.IsNullOrWhiteSpace(rutaRelativa))
                return null;

            ValidarRutaRelativa(rutaRelativa);
            var baseDatos = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Datos"));
            var ruta = Path.GetFullPath(Path.Combine(baseDatos, rutaRelativa.Replace('/', Path.DirectorySeparatorChar).Replace('\\', Path.DirectorySeparatorChar)));
            if (!ruta.StartsWith(baseDatos + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("La ruta de imagen no es válida.");
            return ruta;
        }

        /* Comprueba que la base no reciba rutas absolutas ni recorridos fuera de Datos. */
        public static void ValidarRutaRelativa(string rutaRelativa)
        {
            if (string.IsNullOrWhiteSpace(rutaRelativa))
                return;
            if (Path.IsPathRooted(rutaRelativa) || rutaRelativa.Contains(":") || rutaRelativa.Contains(".."))
                throw new InvalidOperationException("La ruta de imagen debe ser relativa a Datos.");
        }

        /* Valida y normaliza las únicas extensiones aceptadas por la aplicación. */
        public static string NormalizarExtension(string extension)
        {
            var normalizada = (extension ?? string.Empty).Trim().ToLowerInvariant();
            if (!normalizada.StartsWith("."))
                normalizada = "." + normalizada;
            if (normalizada != ".jpg" && normalizada != ".jpeg" && normalizada != ".png")
                throw new InvalidOperationException("Seleccione un archivo de imagen válido.");
            return normalizada;
        }

        /* Elimina un archivo administrado solo cuando el llamador confirmó que no tiene referencias. */
        public static void Eliminar(string rutaRelativa)
        {
            var ruta = RutaAbsoluta(rutaRelativa);
            if (!string.IsNullOrWhiteSpace(ruta) && File.Exists(ruta))
                File.Delete(ruta);
        }

        private static string Guardar(byte[] contenido, string extension, string carpeta)
        {
            ValidacionesGimnasio.ValidarImagen(contenido);
            var extensionNormalizada = NormalizarExtension(extension);
            var carpetaAbsoluta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Datos", CarpetaImagenes, carpeta);
            Directory.CreateDirectory(carpetaAbsoluta);
            var nombre = Guid.NewGuid().ToString("N") + extensionNormalizada;
            var rutaAbsoluta = Path.Combine(carpetaAbsoluta, nombre);
            File.WriteAllBytes(rutaAbsoluta, contenido);
            return Path.Combine(CarpetaImagenes, carpeta, nombre).Replace(Path.DirectorySeparatorChar, '\\');
        }
    }
}
