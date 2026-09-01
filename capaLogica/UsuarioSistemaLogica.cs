using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Data.Entity;
using exxen2._0.capaDatos.Contexto;
using exxen2._0.capaDatos.Entidades;
using Konscious.Security.Cryptography;

namespace exxen2._0.capaLogica
{
    public class UsuarioSistemaLogica
    {
        private const string FormatoPassword = "ARGON2ID";
        private const int VersionArgon2 = 19;
        private const int MemoriaArgon2 = 65536;
        private const int IteracionesArgon2 = 3;
        private const int ParalelismoArgon2 = 2;
        private const int TamanoSalt = 16;
        private const int TamanoHash = 32;

        public UsuarioSistema Crear(UsuarioSistema usuario, string password)
        {
            ValidarDatos(usuario);
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new InvalidOperationException("La contraseña es obligatoria.");
            }

            using (var context = new GymContext())
            {
                var rol = ObtenerRolActivo(context, usuario.IdRol);
                ValidarUnicidad(context, usuario.DNI, usuario.Username, 0);

                usuario.IdRol = rol.IdRol;
                usuario.Rol = rol;
                usuario.Password = GenerarPassword(password);
                usuario.Estado = true;

                context.UsuariosSistema.Add(usuario);
                context.SaveChanges();
                return usuario;
            }
        }

        public UsuarioSistema Modificar(UsuarioSistema usuario, string nuevaPassword = null)
        {
            ValidarDatos(usuario);

            using (var context = new GymContext())
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
                existente.Username = usuario.Username;
                existente.IdRol = rol.IdRol;
                existente.Rol = rol;
                existente.Estado = usuario.Estado;

                if (!string.IsNullOrWhiteSpace(nuevaPassword))
                {
                    existente.Password = GenerarPassword(nuevaPassword);
                }

                context.SaveChanges();
                return existente;
            }
        }

        public UsuarioSistema ObtenerPorId(int idUsuarioSistema)
        {
            using (var context = new GymContext())
            {
                return context.UsuariosSistema.Include(u => u.Rol)
                    .SingleOrDefault(u => u.IdUsuarioSistema == idUsuarioSistema);
            }
        }

        public UsuarioSistema ObtenerPorDni(string dni)
        {
            using (var context = new GymContext())
            {
                return context.UsuariosSistema.Include(u => u.Rol)
                    .SingleOrDefault(u => u.DNI == dni);
            }
        }

        public UsuarioSistema ObtenerPorUsername(string username)
        {
            using (var context = new GymContext())
            {
                return context.UsuariosSistema.Include(u => u.Rol)
                    .SingleOrDefault(u => u.Username == username);
            }
        }

        public List<UsuarioSistema> ListarActivos()
        {
            using (var context = new GymContext())
            {
                return context.UsuariosSistema.Include(u => u.Rol)
                    .Where(u => u.Estado && u.Rol.Estado)
                    .OrderBy(u => u.Apellido).ThenBy(u => u.Nombre).ToList();
            }
        }

        public List<UsuarioSistema> ListarPorRol(string descripcionRol)
        {
            if (string.IsNullOrWhiteSpace(descripcionRol))
            {
                return new List<UsuarioSistema>();
            }

            using (var context = new GymContext())
            {
                return context.UsuariosSistema.Include(u => u.Rol)
                    .Where(u => u.Estado && u.Rol.Estado && u.Rol.Descripcion == descripcionRol)
                    .OrderBy(u => u.Apellido).ThenBy(u => u.Nombre).ToList();
            }
        }

        public void DarDeBaja(int idUsuarioSistema)
        {
            using (var context = new GymContext())
            {
                var usuario = context.UsuariosSistema.Find(idUsuarioSistema);
                if (usuario == null)
                {
                    throw new InvalidOperationException("El usuario no existe.");
                }

                usuario.Estado = false;
                context.SaveChanges();
            }
        }

        public UsuarioSistema Autenticar(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return null;
            }

            using (var context = new GymContext())
            {
                var usuario = context.UsuariosSistema
                    .Include(u => u.Rol)
                    .SingleOrDefault(u => u.Username == username && u.Estado);

                if (usuario == null || usuario.Rol == null || !usuario.Rol.Estado)
                {
                    return null;
                }

                return VerificarPassword(password, usuario.Password) ? usuario : null;
            }
        }

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
            return string.Format("{0}:{1}:{2}:{3}:{4}:{5}:{6}", FormatoPassword, VersionArgon2,
                MemoriaArgon2, IteracionesArgon2, ParalelismoArgon2,
                Convert.ToBase64String(salt), Convert.ToBase64String(hash));
        }

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

            if (version != VersionArgon2 || memoria < 8 * paralelismo || iteraciones <= 0 ||
                paralelismo <= 0 || salt.Length == 0 || hashEsperado.Length == 0)
            {
                return false;
            }

            byte[] hashCalculado;
            try
            {
                hashCalculado = DerivarPassword(password, salt, memoria, iteraciones, paralelismo,
                    hashEsperado.Length);
            }
            catch (ArgumentException)
            {
                return false;
            }

            return CompararBytes(hashCalculado, hashEsperado);
        }

        private static byte[] DerivarPassword(string password, byte[] salt, int memoria, int iteraciones,
            int paralelismo, int tamanoHash = TamanoHash)
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
        }

        private static Rol ObtenerRolActivo(GymContext context, int idRol)
        {
            var rol = context.Roles.SingleOrDefault(r => r.IdRol == idRol);
            if (rol == null || !rol.Estado)
            {
                throw new InvalidOperationException("El rol seleccionado no existe o está inactivo.");
            }

            return rol;
        }

        private static void ValidarUnicidad(GymContext context, string dni, string username, int idActual)
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
