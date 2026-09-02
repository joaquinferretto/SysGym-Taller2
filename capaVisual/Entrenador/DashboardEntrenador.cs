using System.ComponentModel;
using System.Drawing;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaVisual.Compartido;

namespace exxen2._0.capaVisual.Entrenador
{
    [DesignerCategory("Form")]
    public class DashboardEntrenador : DashboardEntrenadorBase
    {
        public DashboardEntrenador()
            : base()
        {
        }

        public DashboardEntrenador(UsuarioSistema usuario)
            : base(usuario)
        {
        }
    }

    public class DashboardEntrenadorBase : DashboardBase
    {
        public DashboardEntrenadorBase()
            : this(new UsuarioSistema { Nombre = "Entrenador", Apellido = "de diseño" })
        {
        }

        public DashboardEntrenadorBase(UsuarioSistema usuario)
            : base(usuario, "SysGym - Entrenador", Color.FromArgb(14, 116, 144))
        {
            var trabajo = AgregarSeccion("Mi trabajo");
            AgregarOpcion(trabajo, "Mis socios", delegate { AbrirFormulario(new MisSociosForm(usuario)); });
            AgregarOpcion(trabajo, "Rutinas", delegate { AbrirFormulario(new RutinasEntrenadorForm(usuario)); });

            var catalogo = AgregarSeccion("Catálogo");
            AgregarOpcion(catalogo, "Ejercicios", delegate { AbrirFormulario(new GestionEjerciciosForm(Color.FromArgb(14, 116, 144))); });

            var control = AgregarSeccion("Control de acceso");
            AgregarOpcion(control, "Asistencias", delegate { AbrirFormulario(new GestionAsistenciasForm(Color.FromArgb(14, 116, 144))); });
        }
    }
}
