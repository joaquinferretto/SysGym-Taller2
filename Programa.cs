
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using exxen2._0.capaVisual.Autenticacion;

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
            Application.Run(new InicioSesion());
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
