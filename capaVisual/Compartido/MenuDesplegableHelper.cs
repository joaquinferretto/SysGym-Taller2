using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Compartido
{
    /* Reacomoda una sección del menú lateral y muestra u oculta sus opciones. */
    public static class MenuDesplegableHelper
    {
        /* Coloca el encabezado y sus botones según el estado expandido de la sección. */
        public static int ColocarSeccion(Label encabezado, bool expandida, int posicionY, params Control[] opciones)
        {
            var titulo = encabezado.Tag as string;
            if (string.IsNullOrEmpty(titulo))
            {
                titulo = encabezado.Text;
                encabezado.Tag = titulo;
            }

            encabezado.Text = (expandida ? "▼ " : "▶ ") + titulo;
            encabezado.Cursor = Cursors.Hand;
            encabezado.Location = new Point(14, posicionY);
            posicionY += encabezado.Height;

            if (expandida)
                posicionY += 2;

            foreach (var opcion in opciones)
            {
                opcion.Visible = expandida;
                if (!expandida)
                    continue;
                opcion.Location = new Point(14, posicionY);
                posicionY += opcion.Height + 5;
            }

            return posicionY + 8;
        }
    }
}
