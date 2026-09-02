using System.ComponentModel;
using System.Drawing;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaVisual.Compartido;
using exxen2._0.capaVisual.Recepcionista;

namespace exxen2._0.capaVisual.Administrador
{
    [DesignerCategory("Form")]
    public class DashboardAdministrador : DashboardAdministradorBase
    {
        public DashboardAdministrador()
            : base()
        {
        }

        public DashboardAdministrador(UsuarioSistema usuario)
            : base(usuario)
        {
        }
    }

    public class DashboardAdministradorBase : DashboardBase
    {
        public DashboardAdministradorBase()
            : this(new UsuarioSistema { Nombre = "Administrador", Apellido = "de diseño" })
        {
        }

        public DashboardAdministradorBase(UsuarioSistema usuario)
            : base(usuario, "SysGym - Administrador", Color.FromArgb(79, 70, 229))
        {
            var administracion = AgregarSeccion("Administración");
            AgregarOpcion(administracion, "Usuarios y roles", delegate { AbrirFormulario(new GestionUsuariosForm()); });
            AgregarOpcion(administracion, "Socios", delegate { AbrirFormulario(new GestionSociosForm(Color.FromArgb(79, 70, 229))); });

            var operacion = AgregarSeccion("Operación");
            AgregarOpcion(operacion, "Planes", delegate { AbrirFormulario(new GestionPlanesForm()); });
            AgregarOpcion(operacion, "Membresías", delegate
            {
                AbrirFormulario(new GestionMembresiasForm(usuario, Color.FromArgb(79, 70, 229)));
            });
            AgregarOpcion(operacion, "Cuotas y pagos", delegate { AbrirFormulario(new GestionPagosForm()); });

            var rutinas = AgregarSeccion("Rutinas");
            AgregarOpcion(rutinas, "Ejercicios", delegate { AbrirFormulario(new GestionEjerciciosForm(Color.FromArgb(79, 70, 229))); });
            AgregarOpcion(rutinas, "Catálogo de rutinas", delegate { AbrirFormulario(new ConsultaRutinasAdministradorForm()); });

            var consultas = AgregarSeccion("Consultas");
            AgregarOpcion(consultas, "Reportes", delegate { AbrirFormulario(new ReportesForm()); });
        }
    }
}
