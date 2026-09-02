using System.ComponentModel;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaVisual.Compartido;

namespace exxen2._0.capaVisual.Entrenador
{
    [DesignerCategory("Form")]
    public class MisSociosForm : MisSociosFormBase
    {
        public MisSociosForm()
            : base()
        {
        }

        public MisSociosForm(UsuarioSistema usuario)
            : base(usuario)
        {
        }
    }
}
