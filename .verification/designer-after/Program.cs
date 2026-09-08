using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using exxen2._0.capaVisual.Autenticacion;

namespace exxen2._0
{
    /* Contiene el punto de entrada de la aplicación de escritorio. */
    internal static class Program
    {
        /* Inicia Windows Forms con estilos visuales y abre el formulario de acceso. */
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());
        }
    }
}
