using System.ComponentModel;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaVisual.Compartido;

namespace exxen2._0.capaVisual.Entrenador
{
    [DesignerCategory("Form")]
    public class RutinasEntrenadorForm : RutinasEntrenadorFormBase
    {
        public RutinasEntrenadorForm()
            : base()
        {
        }

        public RutinasEntrenadorForm(UsuarioSistema usuario)
            : base(usuario)
        {
        }
    }
}
