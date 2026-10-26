using System;
using System.Configuration;
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

        /* La instalación usa ProgramData; Debug conserva la carpeta Datos junto al ejecutable. */
        public static string DirectorioRaiz
        {
            get
            {
                return EsInstalacion()
                    ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "SysGym")
                    : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Datos");
            }
        }

        /* Ubicación de los archivos MDF/LDF de la instalación. */
        public static string DirectorioBaseDatos
        {
            get { return EsInstalacion() ? Path.Combine(DirectorioRaiz, "Datos") : DirectorioRaiz; }
        }

        /* Prepara las carpetas comunes y configura DataDirectory para las capas de persistencia. */
        public static void PrepararDirectorios()
        {
            Directory.CreateDirectory(DirectorioRaiz);
            Directory.CreateDirectory(DirectorioBaseDatos);
            Directory.CreateDirectory(Path.Combine(DirectorioRaiz, CarpetaImagenes, CarpetaEjercicios));
            Directory.CreateDirectory(Path.Combine(DirectorioRaiz, CarpetaImagenes, CarpetaSocios));
            AppDomain.CurrentDomain.SetData("DataDirectory", DirectorioBaseDatos);
        }

        /* Guarda una foto de socio en la carpeta compartida de imágenes y devuelve la ruta relativa. */
        public static string GuardarSocio(byte[] contenido)
        {
            return Guardar(contenido, CarpetaSocios);
        }

        /* Guarda una foto de usuario dentro de Datos/Imagenes/Usuarios y devuelve la ruta relativa. */
        public static string GuardarUsuario(byte[] contenido)
        {
            return Guardar(contenido, CarpetaUsuarios);
        }

        /* Guarda una imagen de ejercicio dentro de Imagenes/Ejercicios/{id}. */
        public static string GuardarEjercicio(byte[] contenido, int idEjercicio)
        {
            if (idEjercicio <= 0)
                throw new InvalidOperationException("Primero guarde el ejercicio antes de agregar imágenes.");  // Regla incumplida: corta la operación y el formulario muestra este mensaje.

            return Guardar(contenido, Path.Combine(CarpetaEjercicios, idEjercicio.ToString()));
        }

        /* Resuelve una ruta relativa dentro de la raíz de datos activa. */
        public static string RutaAbsoluta(string rutaRelativa)
        {
            if (string.IsNullOrWhiteSpace(rutaRelativa))
                return null;

            ValidarRutaRelativa(rutaRelativa);
            var raiz = Path.GetFullPath(DirectorioRaiz);
            var ruta = Path.GetFullPath(Path.Combine(raiz, rutaRelativa.Replace('/', Path.DirectorySeparatorChar).Replace('\\', Path.DirectorySeparatorChar)));
            if (!ruta.StartsWith(raiz + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("La ruta de imagen no es válida.");
            return ruta;
        }

        /* Comprueba que la base no reciba rutas absolutas ni recorridos fuera de la carpeta raíz. */
        public static void ValidarRutaRelativa(string rutaRelativa)
        {
            if (string.IsNullOrWhiteSpace(rutaRelativa))
                return;
            if (Path.IsPathRooted(rutaRelativa) || rutaRelativa.Contains(":") || rutaRelativa.Contains(".."))
                throw new InvalidOperationException("La ruta de imagen debe ser relativa a Datos.");
        }

        /* Elimina un archivo administrado solo cuando el llamador confirmó que no tiene referencias. */
        public static void Eliminar(string rutaRelativa)
        {
            var ruta = RutaAbsoluta(rutaRelativa);
            if (!string.IsNullOrWhiteSpace(ruta) && File.Exists(ruta))
                File.Delete(ruta);
        }

        private static string Guardar(byte[] contenido, string carpeta)
        {
            var normalizada = ProcesadorImagenes.Procesar(contenido);
            var carpetaAbsoluta = Path.Combine(DirectorioRaiz, CarpetaImagenes, carpeta);
            Directory.CreateDirectory(carpetaAbsoluta);
            var nombre = Guid.NewGuid().ToString("N") + normalizada.Extension;
            var rutaAbsoluta = Path.Combine(carpetaAbsoluta, nombre);
            var temporal = rutaAbsoluta + ".tmp";
            try
            {
                using (var archivo = new FileStream(temporal, FileMode.CreateNew, FileAccess.Write, FileShare.None))
                {
                    archivo.Write(normalizada.Contenido, 0, normalizada.Contenido.Length);
                    archivo.Flush(true);
                }
                File.Move(temporal, rutaAbsoluta);
                return Path.Combine(CarpetaImagenes, carpeta, nombre).Replace(Path.DirectorySeparatorChar, '\\');
            }
            catch
            {
                if (File.Exists(temporal))
                    File.Delete(temporal);
                if (File.Exists(rutaAbsoluta))
                    File.Delete(rutaAbsoluta);
                throw;
            }
        }

        private static bool EsInstalacion()
        {
            return string.Equals(ConfigurationManager.AppSettings["SysGymModoInstalado"], "true", StringComparison.OrdinalIgnoreCase);
        }
    }
}
