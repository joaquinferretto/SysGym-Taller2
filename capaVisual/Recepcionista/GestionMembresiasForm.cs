using System.ComponentModel;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaVisual.Compartido;

namespace exxen2._0.capaVisual.Recepcionista
{
    [DesignerCategory("Form")]
    public class GestionMembresiasForm : GestionMembresiasFormBase
    {
        public GestionMembresiasForm()
            : base()
        {
        }

        public GestionMembresiasForm(UsuarioSistema usuario)
            : base(usuario)
        {
        }

        public GestionMembresiasForm(UsuarioSistema usuario, System.Drawing.Color colorPrimario)
            : base(usuario, colorPrimario)
        {
        }
    }
}
