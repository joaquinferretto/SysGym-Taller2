using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaDatos.Repositorios;
using Konscious.Security.Cryptography;

namespace exxen2._0.capaLogica
{
    /* Coordina las operaciones y validaciones de negocio de usuarios del sistema. */
    public class UsuarioSistemaLogica
    {
        private const string FormatoClave = "ARGON2ID";
        private const int VersionArgon2 = 19;
        private const int MemoriaArgon2 = 65536;
        private const int IteracionesArgon2 = 3;
        private const int ParalelismoArgon2 = 2;
        private const int TamanoSal = 16;
        private const int TamanoResumen = 32;
        /* Valida y registra usuarios del sistema mediante la unidad de trabajo, conservando sus reglas de alta. */
        public UsuarioSistema Crear(UsuarioSistema usuario, string clave)
        {
            ValidarDatos(usuario);
            if (string.IsNullOrWhiteSpace(clave))
            {
                throw new InvalidOperationException("La contraseña es obligatoria.");
            }

            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                var rol = ObtenerRolActivo(datos, usuario.IdRol);
                ValidarUnicidad(datos, usuario.DNI, usuario.NombreUsuario, 0);
                usuario.IdRol = rol.IdRol;
                usuario.Rol = rol;
                usuario.Clave = GenerarClave(clave);
                usuario.Estado = true;
                datos.UsuariosSistema.Agregar(usuario);
                datos.GuardarCambios();
                return usuario;
            }
        }

        /* Valida y guarda los cambios de usuarios del sistema sobre el registro existente. */
        public UsuarioSistema Modificar(UsuarioSistema usuario, string nuevaClave = null)
        {
            ValidarDatos(usuario);
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                var existente = datos.UsuariosSistema.SingleOrDefault(u => u.IdUsuarioSistema == usuario.IdUsuarioSistema);
                if (existente == null)
                {
                    throw new InvalidOperationException("El usuario no existe.");
                }

                var rol = ObtenerRolActivo(datos, usuario.IdRol);
                ValidarUnicidad(datos, usuario.DNI, usuario.NombreUsuario, usuario.IdUsuarioSistema);
                existente.Nombre = usuario.Nombre;
                existente.Apellido = usuario.Apellido;
                existente.DNI = usuario.DNI;
                existente.Telefono = usuario.Telefono;
                existente.FechaNacimiento = usuario.FechaNacimiento;
                existente.Salario = usuario.Salario;
                existente.NombreUsuario = usuario.NombreUsuario;
                existente.IdRol = rol.IdRol;
                existente.Rol = rol;
                existente.Estado = usuario.Estado;
                existente.Foto = usuario.Foto;
                existente.Sexo = usuario.Sexo;
                if (!string.IsNullOrWhiteSpace(nuevaClave))
                {
                    existente.Clave = GenerarClave(nuevaClave);
                }

                datos.GuardarCambios();
                return existente;
            }
        }

        /* Busca el registro de usuarios del sistema por identificador y devuelve los datos disponibles. */
        public UsuarioSistema ObtenerPorId(int idUsuarioSistema)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                return datos.UsuariosSistema.ConsultarSoloLectura("Rol").SingleOrDefault(u => u.IdUsuarioSistema == idUsuarioSistema);
            }
        }

        /* Busca el registro de usuarios del sistema por DNI y devuelve los datos disponibles. */
        public UsuarioSistema ObtenerPorDni(string dni)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                return datos.UsuariosSistema.ConsultarSoloLectura("Rol").SingleOrDefault(u => u.DNI == dni);
            }
        }

        /* Busca el registro de usuarios del sistema por nombre de usuario y devuelve los datos disponibles. */
        public UsuarioSistema ObtenerPorNombreUsuario(string nombreUsuario)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                return datos.UsuariosSistema.ConsultarSoloLectura("Rol").SingleOrDefault(u => u.NombreUsuario == nombreUsuario);
            }
        }

        /* Consulta usuarios del sistema activos para devolver los datos a la capa visual. */
        public List<UsuarioSistema> ListarActivos()
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                return ListarSinFotos(datos.UsuariosSistema.ConsultarSoloLectura().Where(u => u.Estado && u.Rol.Estado).OrderBy(u => u.Apellido).ThenBy(u => u.Nombre));
            }
        }

        /* Consulta usuarios del sistema activos e inactivos para su gestión para devolver los datos a la capa visual. */
        public List<UsuarioSistema> ListarParaGestion()
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                return ListarSinFotos(datos.UsuariosSistema.ConsultarSoloLectura().OrderByDescending(u => u.Estado).ThenBy(u => u.Apellido).ThenBy(u => u.Nombre));
            }
        }

        /* Consulta usuarios del sistema con el rol activo solicitado para devolver los datos a la capa visual. */
        public List<UsuarioSistema> ListarPorRol(string descripcionRol)
        {
            if (string.IsNullOrWhiteSpace(descripcionRol))
            {
                return new List<UsuarioSistema>();
            }

            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                return ListarSinFotos(datos.UsuariosSistema.ConsultarSoloLectura().Where(u => u.Estado && u.Rol.Estado && u.Rol.Descripcion == descripcionRol).OrderBy(u => u.Apellido).ThenBy(u => u.Nombre));
            }
        }

        /* Proyecta personal y rol sin descargar fotos ni contraseñas para los listados. */
        private static List<UsuarioSistema> ListarSinFotos(IQueryable<UsuarioSistema> consulta)
        {
            return consulta.Select(u => new
            {
                u.IdUsuarioSistema, u.Nombre, u.Apellido, u.DNI, u.Telefono,
                u.FechaNacimiento, u.Salario, u.NombreUsuario, u.Estado, u.IdRol, u.Rol, u.Sexo
            }).ToList().Select(u => new UsuarioSistema
            {
                IdUsuarioSistema = u.IdUsuarioSistema, Nombre = u.Nombre, Apellido = u.Apellido,
                DNI = u.DNI, Telefono = u.Telefono, FechaNacimiento = u.FechaNacimiento,
                Salario = u.Salario, NombreUsuario = u.NombreUsuario, Estado = u.Estado,
                IdRol = u.IdRol, Rol = u.Rol, Sexo = u.Sexo
            }).ToList();
        }

        /* Desactiva el registro de usuarios del sistema sin eliminar su historial. */
        public void DarDeBaja(int idUsuarioSistema)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                var usuario = datos.UsuariosSistema.Buscar(idUsuarioSistema);
                if (usuario == null)
                {
                    throw new InvalidOperationException("El usuario no existe.");
                }

                usuario.Estado = false;
                datos.GuardarCambios();
            }
        }

        /* Recupera el estado activo del registro de usuarios del sistema según las validaciones de la operación. */
        public void Reactivar(int idUsuarioSistema)
        {
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                var usuario = datos.UsuariosSistema.Consultar("Rol").SingleOrDefault(u => u.IdUsuarioSistema == idUsuarioSistema);
                if (usuario == null)
                {
                    throw new InvalidOperationException("El usuario no existe.");
                }

                if (usuario.Rol == null || !usuario.Rol.Estado)
                {
                    throw new InvalidOperationException("No se puede reactivar el usuario porque su rol está inactivo.");
                }

                usuario.Estado = true;
                datos.GuardarCambios();
            }
        }

        /* Valida las credenciales y el rol activo; devuelve el usuario autenticado o null. */
        public UsuarioSistema Autenticar(string nombreUsuario, string clave)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario) || string.IsNullOrWhiteSpace(clave))
            {
                return null;
            }

            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                var usuario = datos.UsuariosSistema.ConsultarSoloLectura("Rol").SingleOrDefault(u => u.NombreUsuario == nombreUsuario && u.Estado);
                if (usuario == null || usuario.Rol == null || !usuario.Rol.Estado)
                {
                    return null;
                }

                return VerificarClave(clave, usuario.Clave) ? usuario : null;
            }
        }

        /* Genera una sal aleatoria y almacena la contraseña derivada con sus parámetros Argon2id. */
        public static string GenerarClave(string clave)
        {
            if (string.IsNullOrWhiteSpace(clave))
            {
                throw new ArgumentException("La contraseña es obligatoria.", nameof(clave));
            }

            var sal = new byte[TamanoSal];
            using (var generadorAleatorio = RandomNumberGenerator.Create())
            {
                generadorAleatorio.GetBytes(sal);
            }

            var resumen = DerivarClave(clave, sal, MemoriaArgon2, IteracionesArgon2, ParalelismoArgon2);
            return string.Format("{0}:{1}:{2}:{3}:{4}:{5}:{6}", FormatoClave, VersionArgon2, MemoriaArgon2, IteracionesArgon2, ParalelismoArgon2, Convert.ToBase64String(sal), Convert.ToBase64String(resumen));
        }

        /* Comprueba la contraseña contra el formato Argon2id almacenado y rechaza datos inválidos. */
        public static bool VerificarClave(string clave, string claveAlmacenada)
        {
            if (string.IsNullOrWhiteSpace(clave) || string.IsNullOrWhiteSpace(claveAlmacenada))
            {
                return false;
            }

            var partes = claveAlmacenada.Split(':');
            if (partes.Length != 7 || partes[0] != FormatoClave)
            {
                return false;
            }

            int version;
            int memoria;
            int iteraciones;
            int paralelismo;
            byte[] sal;
            byte[] resumenEsperado;
            try
            {
                version = int.Parse(partes[1]);
                memoria = int.Parse(partes[2]);
                iteraciones = int.Parse(partes[3]);
                paralelismo = int.Parse(partes[4]);
                sal = Convert.FromBase64String(partes[5]);
                resumenEsperado = Convert.FromBase64String(partes[6]);
            }
            catch (FormatException)
            {
                return false;
            }
            catch (OverflowException)
            {
                return false;
            }

            if (version != VersionArgon2 || memoria < 8 * paralelismo || iteraciones <= 0 || paralelismo <= 0 || sal.Length == 0 || resumenEsperado.Length == 0)
            {
                return false;
            }

            byte[] resumenCalculado;
            try
            {
                resumenCalculado = DerivarClave(clave, sal, memoria, iteraciones, paralelismo, resumenEsperado.Length);
            }
            catch (ArgumentException)
            {
                return false;
            }

            return CompararBytes(resumenCalculado, resumenEsperado);
        }

        /* Calcula el hash Argon2id utilizando la sal y los parámetros indicados. */
        private static byte[] DerivarClave(string clave, byte[] sal, int memoria, int iteraciones, int paralelismo, int tamanoResumen = TamanoResumen)
        {
            using (var derivador = new Argon2id(Encoding.UTF8.GetBytes(clave)))
            {
                derivador.Salt = sal;
                derivador.MemorySize = memoria;
                derivador.Iterations = iteraciones;
                derivador.DegreeOfParallelism = paralelismo;
                return derivador.GetBytes(tamanoResumen);
            }
        }

        /* Comprueba los campos y rangos obligatorios de usuarios del sistema antes de persistirlos. */
        private static void ValidarDatos(UsuarioSistema usuario)
        {
            if (usuario == null)
            {
                throw new ArgumentNullException("usuario");
            }

            if (string.IsNullOrWhiteSpace(usuario.Nombre) || string.IsNullOrWhiteSpace(usuario.Apellido))
            {
                throw new InvalidOperationException("Nombre y apellido son obligatorios.");
            }

            if (string.IsNullOrWhiteSpace(usuario.DNI) || string.IsNullOrWhiteSpace(usuario.NombreUsuario))
            {
                throw new InvalidOperationException("DNI y nombre de usuario son obligatorios.");
            }

            if (usuario.Salario <= 0)
            {
                throw new InvalidOperationException("El salario debe ser mayor que cero.");
            }
            ValidacionesGimnasio.ValidarFotoYSexo(usuario.Foto, usuario.Sexo);
        }

        /* Obtiene el rol requerido y rechaza roles inexistentes o inactivos. */
        private static Rol ObtenerRolActivo(IUnidadDeTrabajo datos, int idRol)
        {
            var rol = datos.Roles.SingleOrDefault(r => r.IdRol == idRol);
            if (rol == null || !rol.Estado)
            {
                throw new InvalidOperationException("El rol seleccionado no existe o está inactivo.");
            }

            return rol;
        }

        /* Impide duplicar DNI o nombre de usuario, excluyendo el registro que se modifica. */
        private static void ValidarUnicidad(IUnidadDeTrabajo datos, string dni, string nombreUsuario, int idActual)
        {
            if (datos.UsuariosSistema.Any(u => u.DNI == dni && u.IdUsuarioSistema != idActual))
            {
                throw new InvalidOperationException("El DNI ya está registrado.");
            }

            if (datos.UsuariosSistema.Any(u => u.NombreUsuario == nombreUsuario && u.IdUsuarioSistema != idActual))
            {
                throw new InvalidOperationException("El nombre de usuario ya está registrado.");
            }
        }

        /* Compara el contenido de los hashes sin terminar al encontrar la primera diferencia. */
        private static bool CompararBytes(byte[] izquierdo, byte[] derecho)
        {
            if (izquierdo == null || derecho == null || izquierdo.Length != derecho.Length)
            {
                return false;
            }

            var diferencia = 0;
            for (var i = 0; i < izquierdo.Length; i++)
            {
                diferencia |= izquierdo[i] ^ derecho[i];
            }

            return diferencia == 0;
        }
    }
}
