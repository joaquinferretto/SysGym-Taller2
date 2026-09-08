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
        private const string FormatoPassword = "ARGON2ID";
        private const int VersionArgon2 = 19;
        private const int MemoriaArgon2 = 65536;
        private const int IteracionesArgon2 = 3;
        private const int ParalelismoArgon2 = 2;
        private const int TamanoSalt = 16;
        private const int TamanoHash = 32;
        /* Valida y registra usuarios del sistema mediante la unidad de trabajo, conservando sus reglas de alta. */
        public UsuarioSistema Crear(UsuarioSistema usuario, string password)
        {
            ValidarDatos(usuario);
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new InvalidOperationException("La contraseña es obligatoria.");
            }

            using (var context = new GymUnidadDeTrabajo())
            {
                var rol = ObtenerRolActivo(context, usuario.IdRol);
                ValidarUnicidad(context, usuario.DNI, usuario.Username, 0);
                usuario.IdRol = rol.IdRol;
                usuario.Rol = rol;
                usuario.Password = GenerarPassword(password);
                usuario.Estado = true;
                context.UsuariosSistema.Agregar(usuario);
                context.GuardarCambios();
                return usuario;
            }
        }

        /* Valida y guarda los cambios de usuarios del sistema sobre el registro existente. */
        public UsuarioSistema Modificar(UsuarioSistema usuario, string nuevaPassword = null)
        {
            ValidarDatos(usuario);
            using (var context = new GymUnidadDeTrabajo())
            {
                var existente = context.UsuariosSistema.SingleOrDefault(u => u.IdUsuarioSistema == usuario.IdUsuarioSistema);
                if (existente == null)
                {
                    throw new InvalidOperationException("El usuario no existe.");
                }

                var rol = ObtenerRolActivo(context, usuario.IdRol);
                ValidarUnicidad(context, usuario.DNI, usuario.Username, usuario.IdUsuarioSistema);
                existente.Nombre = usuario.Nombre;
                existente.Apellido = usuario.Apellido;
                existente.DNI = usuario.DNI;
                existente.Telefono = usuario.Telefono;
                existente.FechaNacimiento = usuario.FechaNacimiento;
                existente.Salario = usuario.Salario;
                existente.Username = usuario.Username;
                existente.IdRol = rol.IdRol;
                existente.Rol = rol;
                existente.Estado = usuario.Estado;
                if (!string.IsNullOrWhiteSpace(nuevaPassword))
                {
                    existente.Password = GenerarPassword(nuevaPassword);
                }

                context.GuardarCambios();
                return existente;
            }
        }

        /* Busca el registro de usuarios del sistema por identificador y devuelve los datos disponibles. */
        public UsuarioSistema ObtenerPorId(int idUsuarioSistema)
        {
            using (var context = new GymUnidadDeTrabajo())
            {
                return context.UsuariosSistema.ConsultarSoloLectura("Rol").SingleOrDefault(u => u.IdUsuarioSistema == idUsuarioSistema);
            }
        }

        /* Busca el registro de usuarios del sistema por DNI y devuelve los datos disponibles. */
        public UsuarioSistema ObtenerPorDni(string dni)
        {
            using (var context = new GymUnidadDeTrabajo())
            {
                return context.UsuariosSistema.ConsultarSoloLectura("Rol").SingleOrDefault(u => u.DNI == dni);
            }
        }

        /* Busca el registro de usuarios del sistema por nombre de usuario y devuelve los datos disponibles. */
        public UsuarioSistema ObtenerPorUsername(string username)
        {
            using (var context = new GymUnidadDeTrabajo())
            {
                return context.UsuariosSistema.ConsultarSoloLectura("Rol").SingleOrDefault(u => u.Username == username);
            }
        }

        /* Consulta usuarios del sistema activos para devolver los datos a la capa visual. */
        public List<UsuarioSistema> ListarActivos()
        {
            using (var context = new GymUnidadDeTrabajo())
            {
                return context.UsuariosSistema.ConsultarSoloLectura("Rol").Where(u => u.Estado && u.Rol.Estado).OrderBy(u => u.Apellido).ThenBy(u => u.Nombre).ToList();
            }
        }

        /* Consulta usuarios del sistema activos e inactivos para su gestión para devolver los datos a la capa visual. */
        public List<UsuarioSistema> ListarParaGestion()
        {
            using (var context = new GymUnidadDeTrabajo())
            {
                return context.UsuariosSistema.ConsultarSoloLectura("Rol").OrderByDescending(u => u.Estado).ThenBy(u => u.Apellido).ThenBy(u => u.Nombre).ToList();
            }
        }

        /* Consulta usuarios del sistema con el rol activo solicitado para devolver los datos a la capa visual. */
        public List<UsuarioSistema> ListarPorRol(string descripcionRol)
        {
            if (string.IsNullOrWhiteSpace(descripcionRol))
            {
                return new List<UsuarioSistema>();
            }

            using (var context = new GymUnidadDeTrabajo())
            {
                return context.UsuariosSistema.ConsultarSoloLectura("Rol").Where(u => u.Estado && u.Rol.Estado && u.Rol.Descripcion == descripcionRol).OrderBy(u => u.Apellido).ThenBy(u => u.Nombre).ToList();
            }
        }

        /* Desactiva el registro de usuarios del sistema sin eliminar su historial. */
        public void DarDeBaja(int idUsuarioSistema)
        {
            using (var context = new GymUnidadDeTrabajo())
            {
                var usuario = context.UsuariosSistema.Buscar(idUsuarioSistema);
                if (usuario == null)
                {
                    throw new InvalidOperationException("El usuario no existe.");
                }

                usuario.Estado = false;
                context.GuardarCambios();
            }
        }

        /* Recupera el estado activo del registro de usuarios del sistema según las validaciones de la operación. */
        public void Reactivar(int idUsuarioSistema)
        {
            using (var context = new GymUnidadDeTrabajo())
            {
                var usuario = context.UsuariosSistema.Consultar("Rol").SingleOrDefault(u => u.IdUsuarioSistema == idUsuarioSistema);
                if (usuario == null)
                {
                    throw new InvalidOperationException("El usuario no existe.");
                }

                if (usuario.Rol == null || !usuario.Rol.Estado)
                {
                    throw new InvalidOperationException("No se puede reactivar el usuario porque su rol está inactivo.");
                }

                usuario.Estado = true;
                context.GuardarCambios();
            }
        }

        /* Valida las credenciales y el rol activo; devuelve el usuario autenticado o null. */
        public UsuarioSistema Autenticar(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return null;
            }

            using (var context = new GymUnidadDeTrabajo())
            {
                var usuario = context.UsuariosSistema.ConsultarSoloLectura("Rol").SingleOrDefault(u => u.Username == username && u.Estado);
                if (usuario == null || usuario.Rol == null || !usuario.Rol.Estado)
                {
                    return null;
                }

                return VerificarPassword(password, usuario.Password) ? usuario : null;
            }
        }

        /* Genera una sal aleatoria y almacena la contraseña derivada con sus parámetros Argon2id. */
        public static string GenerarPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("La contraseña es obligatoria.", "password");
            }

            var salt = new byte[TamanoSalt];
            using (var random = RandomNumberGenerator.Create())
            {
                random.GetBytes(salt);
            }

            var hash = DerivarPassword(password, salt, MemoriaArgon2, IteracionesArgon2, ParalelismoArgon2);
            return string.Format("{0}:{1}:{2}:{3}:{4}:{5}:{6}", FormatoPassword, VersionArgon2, MemoriaArgon2, IteracionesArgon2, ParalelismoArgon2, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
        }

        /* Comprueba la contraseña contra el formato Argon2id almacenado y rechaza datos inválidos. */
        public static bool VerificarPassword(string password, string passwordAlmacenada)
        {
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(passwordAlmacenada))
            {
                return false;
            }

            var partes = passwordAlmacenada.Split(':');
            if (partes.Length != 7 || partes[0] != FormatoPassword)
            {
                return false;
            }

            int version;
            int memoria;
            int iteraciones;
            int paralelismo;
            byte[] salt;
            byte[] hashEsperado;
            try
            {
                version = int.Parse(partes[1]);
                memoria = int.Parse(partes[2]);
                iteraciones = int.Parse(partes[3]);
                paralelismo = int.Parse(partes[4]);
                salt = Convert.FromBase64String(partes[5]);
                hashEsperado = Convert.FromBase64String(partes[6]);
            }
            catch (FormatException)
            {
                return false;
            }
            catch (OverflowException)
            {
                return false;
            }

            if (version != VersionArgon2 || memoria < 8 * paralelismo || iteraciones <= 0 || paralelismo <= 0 || salt.Length == 0 || hashEsperado.Length == 0)
            {
                return false;
            }

            byte[] hashCalculado;
            try
            {
                hashCalculado = DerivarPassword(password, salt, memoria, iteraciones, paralelismo, hashEsperado.Length);
            }
            catch (ArgumentException)
            {
                return false;
            }

            return CompararBytes(hashCalculado, hashEsperado);
        }

        /* Calcula el hash Argon2id utilizando la sal y los parámetros indicados. */
        private static byte[] DerivarPassword(string password, byte[] salt, int memoria, int iteraciones, int paralelismo, int tamanoHash = TamanoHash)
        {
            using (var derivador = new Argon2id(Encoding.UTF8.GetBytes(password)))
            {
                derivador.Salt = salt;
                derivador.MemorySize = memoria;
                derivador.Iterations = iteraciones;
                derivador.DegreeOfParallelism = paralelismo;
                return derivador.GetBytes(tamanoHash);
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

            if (string.IsNullOrWhiteSpace(usuario.DNI) || string.IsNullOrWhiteSpace(usuario.Username))
            {
                throw new InvalidOperationException("DNI y username son obligatorios.");
            }

            if (usuario.Salario <= 0)
            {
                throw new InvalidOperationException("El salario debe ser mayor que cero.");
            }
        }

        /* Obtiene el rol requerido y rechaza roles inexistentes o inactivos. */
        private static Rol ObtenerRolActivo(IUnidadDeTrabajo context, int idRol)
        {
            var rol = context.Roles.SingleOrDefault(r => r.IdRol == idRol);
            if (rol == null || !rol.Estado)
            {
                throw new InvalidOperationException("El rol seleccionado no existe o está inactivo.");
            }

            return rol;
        }

        /* Impide duplicar DNI o nombre de usuario, excluyendo el registro que se modifica. */
        private static void ValidarUnicidad(IUnidadDeTrabajo context, string dni, string username, int idActual)
        {
            if (context.UsuariosSistema.Any(u => u.DNI == dni && u.IdUsuarioSistema != idActual))
            {
                throw new InvalidOperationException("El DNI ya está registrado.");
            }

            if (context.UsuariosSistema.Any(u => u.Username == username && u.IdUsuarioSistema != idActual))
            {
                throw new InvalidOperationException("El username ya está registrado.");
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
