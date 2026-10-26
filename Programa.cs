
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using exxen2._0.capaVisual.Autenticacion;
using exxen2._0.capaLogica;

namespace exxen2._0
{
    /* Contiene el punto de entrada de la aplicación de escritorio. */
    internal static class Programa
    {
        /* Inicia Windows Forms con estilos visuales y abre el formulario de acceso. */
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (sender, args) => RegistrarErrorGlobal(args.Exception);
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
                RegistrarErrorGlobal(args.ExceptionObject as Exception ?? new Exception("Excepción no controlada."));
            Application.SetCompatibleTextRenderingDefault(false);
            if (!PrepararPrimerInicio())
                return;
            Application.Run(new InicioSesion());
        }

        private static bool PrepararPrimerInicio()
        {
            var inicializacion = new InicializacionSistemaLogica();
            try
            {
                inicializacion.ValidarConexionServidor();
            }
            catch
            {
                MessageBox.Show("No se pudo conectar con SQL Server. Verifique que esté instalado y en ejecución.",
                    "SysGym", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            try
            {
                if (!inicializacion.ExisteBaseConfigurada())
                {
                    var respuesta = MessageBox.Show(
                        "La base de datos " + inicializacion.NombreBaseConfigurada + " no existe. ¿Desea crearla ahora?",
                        "Primer inicio", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (respuesta != DialogResult.Yes)
                        return false;

                    var rutaScript = Path.Combine(Application.StartupPath, "capaDatos", "Database", "SysGymDB.sql");
                    inicializacion.CrearBaseDesdeScript(rutaScript);
                }

                if (!inicializacion.HayAdministradorActivo())
                {
                    using (var registro = new RegistrarAdministradorFormulario())
                    {
                        if (registro.ShowDialog() != DialogResult.OK)
                            return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                if (EsErrorSql(ex))
                {
                    MessageBox.Show("No se pudo conectar con SQL Server. Verifique que esté instalado y en ejecución.",
                        "SysGym", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                MessageBox.Show(
                    "No se pudo preparar la base de datos de SysGym. Verifique los permisos de SQL Server y vuelva a iniciar la aplicación.",
                    "SysGym", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private static bool EsErrorSql(Exception excepcion)
        {
            if (excepcion == null)
                return false;
            if (excepcion is SqlException)
                return true;

            var agregado = excepcion as AggregateException;
            return (agregado != null && agregado.InnerExceptions.Any(EsErrorSql)) || EsErrorSql(excepcion.InnerException);
        }

        private static void RegistrarErrorGlobal(Exception excepcion)
        {
            try
            {
                var rutaLog = Path.Combine(Application.StartupPath, "SysGym-error.log");
                File.AppendAllText(rutaLog, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + Environment.NewLine +
                    excepcion + Environment.NewLine + Environment.NewLine);
            }
            catch (Exception error)
            {
                Trace.WriteLine("No se pudo escribir el log global de SysGym: " + error);
            }

            try
            {
                MessageBox.Show("Ocurrió un error inesperado. Si el problema continúa, avisá al administrador.",
                    "Error de SysGym", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception error)
            {
                Trace.WriteLine("No se pudo mostrar el mensaje global de SysGym: " + error);
            }
        }
    }
}
