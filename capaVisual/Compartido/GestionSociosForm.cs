using System.ComponentModel;
using System.Drawing;

namespace exxen2._0.capaVisual.Compartido
{
    [DesignerCategory("Form")]
    public class GestionSociosForm : GestionSociosFormBase
    {
        public GestionSociosForm()
            : base()
        {
        }

        public GestionSociosForm(Color colorPrimario, bool permitirEdicion = true)
            : base(colorPrimario, permitirEdicion)
        {
        }
    }
}
