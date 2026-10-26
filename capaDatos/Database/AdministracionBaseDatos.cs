using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;

namespace exxen2._0.capaDatos.Servicios
{
    /* Ejecuta las tareas de instalación y copias que SQL Server debe realizar sobre la base. */
    public static class AdministracionBaseDatos
    {
        private static readonly string[] NombresConexion = { "GymContext", "GymContextRespaldo" };

        public static string NombreBaseConfigurada
        {
            get
            {
                var conexion = ConfigurationManager.ConnectionStrings[ NombresConexion[0] ];
                if (conexion == null)
                    throw new InvalidOperationException("No se encontró la conexión de SysGym en App.config.");
                var constructor = new SqlConnectionStringBuilder(conexion.ConnectionString);
                if (string.IsNullOrWhiteSpace(constructor.InitialCatalog))
                    throw new InvalidOperationException("La conexión de SysGym no tiene una base configurada.");
                return constructor.InitialCatalog;
            }
        }

        public static void ValidarConexionServidor()
        {
            using (AbrirConexionMaster()) { }
        }

        public static bool ExisteBaseConfigurada()
        {
            var nombreBase = NombreBaseConfigurada;
            using (var conexion = AbrirConexionMaster())
            using (var comando = new SqlCommand("SELECT CASE WHEN DB_ID(@nombre) IS NULL THEN 0 ELSE 1 END;", conexion))
            {
                comando.Parameters.Add("@nombre", SqlDbType.NVarChar, 128).Value = nombreBase;
                return Convert.ToInt32(comando.ExecuteScalar()) == 1;
            }
        }

        public static void CrearBaseDesdeScript(string rutaScript)
        {
            if (string.IsNullOrWhiteSpace(rutaScript) || !File.Exists(rutaScript))
                throw new FileNotFoundException("No se encontró el esquema de SysGym para instalar la base.", rutaScript);

            var nombreBase = NombreBaseConfigurada;
            var nombreSeguro = new SqlCommandBuilder().QuoteIdentifier(nombreBase);
            var script = File.ReadAllText(rutaScript, Encoding.UTF8);
            const string crearOriginal = "CREATE DATABASE SysGymDB;";
            const string usarOriginal = "USE SysGymDB;";
            if (!script.Contains(crearOriginal) || !script.Contains(usarOriginal))
                throw new InvalidOperationException("El archivo de esquema no tiene las instrucciones de instalación esperadas.");
            var instruccionCrear = EsInstalacionLocalDb()
                ? CrearBaseConArchivos(nombreSeguro)
                : "CREATE DATABASE " + nombreSeguro + ";";
            script = script.Replace(crearOriginal, instruccionCrear)
                .Replace(usarOriginal, "USE " + nombreSeguro + ";");

            var lotes = Regex.Split(script, @"(?im)^\s*GO\s*(?:--.*)?$");
            var baseCreada = false;
            using (var conexion = AbrirConexionMaster())
            {
                try
                {
                    foreach (var lote in lotes)
                    {
                        if (string.IsNullOrWhiteSpace(lote))
                            continue;
                        using (var comando = new SqlCommand(lote, conexion))
                        {
                            comando.CommandTimeout = 0;
                            comando.ExecuteNonQuery();
                        }
                        if (Regex.IsMatch(lote, @"(?is)^\s*CREATE\s+DATABASE\b"))
                            baseCreada = true;
                    }
                }
                catch
                {
                    if (baseCreada)
                        EliminarBaseIncompleta(conexion, nombreSeguro, nombreBase);
                    throw;
                }
            }
        }

        public static void PrepararDatosBaseInstalada(string rutaScript)
        {
            if (!EsInstalacionLocalDb())
                return;
            if (string.IsNullOrWhiteSpace(rutaScript) || !File.Exists(rutaScript))
                throw new FileNotFoundException("No se encontró la configuración inicial de SysGym.", rutaScript);

            var configuracion = ConfigurationManager.ConnectionStrings[NombresConexion[0]];
            var script = File.ReadAllText(rutaScript, Encoding.UTF8);
            using (var conexion = new SqlConnection(configuracion.ConnectionString))
            {
                conexion.Open();
                foreach (var lote in Regex.Split(script, @"(?im)^\s*GO\s*(?:--.*)?$"))
                {
                    if (string.IsNullOrWhiteSpace(lote))
                        continue;
                    using (var comando = new SqlCommand(lote, conexion))
                    {
                        comando.CommandTimeout = 0;
                        comando.ExecuteNonQuery();
                    }
                }
            }
        }

        public static string ObtenerCarpetaRespaldoSql()
        {
            using (var conexion = AbrirConexionMaster())
            using (var comando = new SqlCommand(
                "DECLARE @carpeta nvarchar(4000) = NULL; " +
                "SET @carpeta = NULLIF(CONVERT(nvarchar(4000), SERVERPROPERTY('InstanceDefaultBackupPath')), N''); " +
                "IF @carpeta IS NULL " +
                "    SET @carpeta = NULLIF(CONVERT(nvarchar(4000), SERVERPROPERTY('InstanceDefaultDataPath')), N''); " +
                "IF @carpeta IS NULL " +
                "    EXEC master.dbo.xp_instance_regread N'HKEY_LOCAL_MACHINE', " +
                "        N'Software\\Microsoft\\MSSQLServer\\MSSQLServer', N'BackupDirectory', @carpeta OUTPUT; " +
                "SELECT @carpeta;", conexion))
            {
                var ruta = Convert.ToString(comando.ExecuteScalar());
                if (string.IsNullOrWhiteSpace(ruta))
                    throw new InvalidOperationException("SQL Server no informó una carpeta predeterminada para copias de seguridad.");
                return ruta.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            }
        }

        public static DateTime? ObtenerFechaUltimaCopia()
        {
            using (var conexion = AbrirConexionMaster())
            using (var comando = new SqlCommand(
                "SELECT MAX(backup_finish_date) FROM msdb.dbo.backupset " +
                "WHERE database_name = @nombre AND type = 'D';", conexion))
            {
                comando.Parameters.Add("@nombre", SqlDbType.NVarChar, 128).Value = NombreBaseConfigurada;
                var resultado = comando.ExecuteScalar();
                return resultado == null || resultado == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(resultado);
            }
        }

        public static string CrearCopiaTemporal(string destinoFinal)
        {
            var nombreBase = NombreBaseConfigurada;
            var nombreSeguro = new SqlCommandBuilder().QuoteIdentifier(nombreBase);
            if (string.IsNullOrWhiteSpace(destinoFinal))
                throw new ArgumentException("Seleccion\u00e1 d\u00f3nde guardar la copia.", "destinoFinal");

            var carpetaDestino = Path.GetDirectoryName(Path.GetFullPath(destinoFinal));
            if (string.IsNullOrWhiteSpace(carpetaDestino) || !Directory.Exists(carpetaDestino))
                throw new DirectoryNotFoundException("La carpeta elegida para guardar la copia no existe.");

            var carpeta = Path.Combine(carpetaDestino, ".SysGymBackup-" + Guid.NewGuid().ToString("N"));
            try
            {
                Directory.CreateDirectory(carpeta);
                if (!EsConexionLocalDb())
                    AsignarPermisoTemporalServicio(carpeta, ObtenerCuentaServicioSql());
            }
            catch (Exception ex) when (ex is UnauthorizedAccessException || ex is IdentityNotMappedException || ex is System.Security.SecurityException)
            {
                EliminarCarpetaTemporal(carpeta);
                throw new InvalidOperationException("No se pudieron habilitar temporalmente los permisos de SQL Server en la carpeta elegida.", ex);
            }
            catch
            {
                EliminarCarpetaTemporal(carpeta);
                throw;
            }

            var fecha = DateTime.Now;
            var nombreArchivo = "SysGym-" + fecha.ToString("yyyy-MM-dd-HHmm") + "-sqltmp.bak";
            var destino = Path.Combine(carpeta, nombreArchivo);

            try
            {
                using (var conexion = AbrirConexionMaster())
                using (var comando = new SqlCommand(
                    "BACKUP DATABASE " + nombreSeguro + " TO DISK = @destino WITH INIT, COPY_ONLY, CHECKSUM;", conexion))
                {
                    comando.CommandTimeout = 0;
                    comando.Parameters.Add("@destino", SqlDbType.NVarChar, 4000).Value = destino;
                    try
                    {
                        comando.ExecuteNonQuery();
                    }
                    catch (SqlException ex) when (EsErrorPermisoArchivo(ex))
                    {
                        throw new InvalidOperationException("SQL Server no tiene permisos para escribir la copia temporal en la carpeta elegida.", ex);
                    }
                }
            }
            catch
            {
                EliminarCopiaTemporal(destino);
                throw;
            }
            return destino;
        }

        public static void CopiarCopia(string origen, string destino)
        {
            File.Copy(origen, destino, true);
        }

        public static void EliminarCopiaTemporal(string ruta)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(ruta) && File.Exists(ruta))
                    File.Delete(ruta);
                if (!string.IsNullOrWhiteSpace(ruta))
                    EliminarCarpetaTemporal(Path.GetDirectoryName(ruta));
            }
            catch
            {
                // El respaldo final ya está copiado; un temporal no se conserva como dato de la aplicación.
            }
        }

        private static string ObtenerCuentaServicioSql()
        {
            using (var conexion = AbrirConexionMaster())
            using (var comando = new SqlCommand(
                "SELECT TOP (1) service_account FROM sys.dm_server_services " +
                "WHERE process_id = CONVERT(int, SERVERPROPERTY('ProcessID'));", conexion))
            {
                var cuenta = Convert.ToString(comando.ExecuteScalar());
                if (string.IsNullOrWhiteSpace(cuenta))
                    throw new InvalidOperationException("No se pudo determinar la cuenta del servicio de SQL Server.");
                return cuenta.Trim();
            }
        }

        private static void AsignarPermisoTemporalServicio(string carpeta, string cuentaServicio)
        {
            var seguridad = Directory.GetAccessControl(carpeta);
            var cuenta = new NTAccount(cuentaServicio);
            seguridad.AddAccessRule(new FileSystemAccessRule(cuenta, FileSystemRights.Modify,
                InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
                PropagationFlags.None, AccessControlType.Allow));
            Directory.SetAccessControl(carpeta, seguridad);
        }

        private static void EliminarCarpetaTemporal(string carpeta)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(carpeta) &&
                    Path.GetFileName(carpeta).StartsWith(".SysGymBackup-", StringComparison.OrdinalIgnoreCase) &&
                    Directory.Exists(carpeta))
                    Directory.Delete(carpeta, true);
            }
            catch
            {
                // Se conserva el resultado de la copia; los temporales se eliminan al terminar la operación.
            }
        }

        public static void RestaurarCopia(string ruta)
        {
            if (string.IsNullOrWhiteSpace(ruta) || !File.Exists(ruta))
                throw new FileNotFoundException("No se encontró el archivo de copia seleccionado.", ruta);

            var nombreBase = NombreBaseConfigurada;
            var nombreSeguro = new SqlCommandBuilder().QuoteIdentifier(nombreBase);
            try
            {
                using (var conexion = AbrirConexionMaster())
                {
                    var posicion = ObtenerPosicionCopiaSysGym(conexion, ruta, nombreBase);
                    var baseEnUnSoloUsuario = false;
                    Exception errorRestauracion = null;
                    try
                    {
                        Ejecutar(conexion, "ALTER DATABASE " + nombreSeguro + " SET SINGLE_USER WITH ROLLBACK IMMEDIATE;");
                        baseEnUnSoloUsuario = true;
                        using (var comando = new SqlCommand(
                            "RESTORE DATABASE " + nombreSeguro + " FROM DISK = @ruta WITH FILE = " + posicion + ", REPLACE, CHECKSUM, RECOVERY;", conexion))
                        {
                            comando.CommandTimeout = 0;
                            comando.Parameters.Add("@ruta", SqlDbType.NVarChar, 4000).Value = ruta;
                            comando.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        errorRestauracion = ex;
                    }

                    Exception errorModoCompartido = null;
                    if (baseEnUnSoloUsuario)
                    {
                        try
                        {
                            Ejecutar(conexion, "ALTER DATABASE " + nombreSeguro + " SET MULTI_USER;");
                        }
                        catch (Exception ex)
                        {
                            errorModoCompartido = ex;
                        }
                    }

                    if (errorRestauracion != null)
                    {
                        var errorSql = errorRestauracion as SqlException;
                        if (errorSql != null && EsErrorPermisoArchivo(errorSql))
                            throw new InvalidOperationException("SQL Server no tiene permisos para leer el archivo de copia seleccionado.", errorRestauracion);
                        throw new InvalidOperationException("No se pudo restaurar la copia de seguridad.", errorRestauracion);
                    }
                    if (errorModoCompartido != null)
                        throw new InvalidOperationException("La copia se restauró, pero no se pudo devolver la base al modo multiusuario.", errorModoCompartido);
                }
            }
            catch (SqlException ex) when (EsErrorPermisoArchivo(ex))
            {
                throw new InvalidOperationException("SQL Server no tiene permisos para leer el archivo de copia seleccionado.", ex);
            }
        }

        private static int ObtenerPosicionCopiaSysGym(SqlConnection conexion, string ruta, string nombreBase)
        {
            using (var comando = new SqlCommand("RESTORE HEADERONLY FROM DISK = @ruta;", conexion))
            {
                comando.CommandTimeout = 0;
                comando.Parameters.Add("@ruta", SqlDbType.NVarChar, 4000).Value = ruta;
                using (var lector = comando.ExecuteReader())
                {
                    var indiceBase = lector.GetOrdinal("DatabaseName");
                    var indiceTipo = lector.GetOrdinal("BackupType");
                    var indicePosicion = lector.GetOrdinal("Position");
                    while (lector.Read())
                    {
                        if (!lector.IsDBNull(indiceBase) &&
                            string.Equals(lector.GetString(indiceBase), nombreBase, StringComparison.OrdinalIgnoreCase) &&
                            !lector.IsDBNull(indiceTipo) && Convert.ToInt32(lector.GetValue(indiceTipo)) == 1)
                            return Convert.ToInt32(lector.GetValue(indicePosicion));
                    }
                }
            }
            throw new InvalidOperationException("El archivo seleccionado no contiene una copia de seguridad de " + nombreBase + ".");
        }

        private static SqlConnection AbrirConexionMaster()
        {
            var errores = new List<Exception>();
            foreach (var nombreConexion in NombresConexion)
            {
                var configuracion = ConfigurationManager.ConnectionStrings[nombreConexion];
                if (configuracion == null)
                    continue;
                SqlConnection conexion = null;
                try
                {
                    var constructor = new SqlConnectionStringBuilder(configuracion.ConnectionString)
                    {
                        InitialCatalog = "master"
                    };
                    conexion = new SqlConnection(constructor.ConnectionString);
                    conexion.Open();
                    return conexion;
                }
                catch (Exception ex)
                {
                    if (conexion != null)
                        conexion.Dispose();
                    errores.Add(ex);
                }
            }
            throw new InvalidOperationException("No se pudo abrir una conexión con SQL Server.", new AggregateException(errores));
        }

        private static bool EsConexionLocalDb()
        {
            var configuracion = ConfigurationManager.ConnectionStrings[NombresConexion[0]];
            if (configuracion == null)
                return false;
            var constructor = new SqlConnectionStringBuilder(configuracion.ConnectionString);
            return constructor.DataSource.IndexOf("(localdb)", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private static bool EsInstalacionLocalDb()
        {
            return EsConexionLocalDb() && string.Equals(
                ConfigurationManager.AppSettings["SysGymModoInstalado"], "true", StringComparison.OrdinalIgnoreCase);
        }

        private static string CrearBaseConArchivos(string nombreSeguro)
        {
            var directorio = AppDomain.CurrentDomain.GetData("DataDirectory") as string;
            if (string.IsNullOrWhiteSpace(directorio))
                throw new InvalidOperationException("No se configuró la carpeta de datos de SysGym.");
            Directory.CreateDirectory(directorio);
            var archivoDatos = Path.Combine(directorio, "SysGymDB.mdf").Replace("'", "''");
            var archivoRegistro = Path.Combine(directorio, "SysGymDB_log.ldf").Replace("'", "''");
            return "CREATE DATABASE " + nombreSeguro +
                " ON PRIMARY (NAME = N'SysGymDB', FILENAME = N'" + archivoDatos + "')" +
                " LOG ON (NAME = N'SysGymDB_log', FILENAME = N'" + archivoRegistro + "');";
        }

        private static void Ejecutar(SqlConnection conexion, string texto)
        {
            using (var comando = new SqlCommand(texto, conexion))
            {
                comando.CommandTimeout = 0;
                comando.ExecuteNonQuery();
            }
        }

        private static bool EsErrorPermisoArchivo(SqlException excepcion)
        {
            foreach (SqlError detalle in excepcion.Errors)
            {
                if (detalle.Number == 5 || detalle.Number == 3201 || detalle.Number == 3202 || detalle.Number == 5120)
                    return true;
                if (detalle.Message.IndexOf("access is denied", StringComparison.OrdinalIgnoreCase) >= 0 ||
                    detalle.Message.IndexOf("acceso denegado", StringComparison.OrdinalIgnoreCase) >= 0)
                    return true;
            }
            return false;
        }

        private static void EliminarBaseIncompleta(SqlConnection conexion, string nombreSeguro, string nombreBase)
        {
            try
            {
                Ejecutar(conexion, "USE [master]; IF DB_ID(N'" + nombreBase.Replace("'", "''") +
                    "') IS NOT NULL BEGIN ALTER DATABASE " + nombreSeguro +
                    " SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE " + nombreSeguro + "; END;");
            }
            catch
            {
                // Se conserva el error del esquema para mostrar un mensaje de instalación comprensible.
            }
        }
    }
}
