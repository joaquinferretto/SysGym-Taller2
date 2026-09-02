using System.ComponentModel;
using System.Drawing;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaVisual.Compartido;

namespace exxen2._0.capaVisual.Recepcionista
{
    [DesignerCategory("Form")]
    public class DashboardRecepcionista : DashboardRecepcionistaBase
    {
        public DashboardRecepcionista()
            : base()
        {
        }

        public DashboardRecepcionista(UsuarioSistema usuario)
            : base(usuario)
        {
        }
    }

    public class DashboardRecepcionistaBase : DashboardBase
    {
        public DashboardRecepcionistaBase()
            : this(new UsuarioSistema { Nombre = "Recepcionista", Apellido = "de diseño" })
        {
        }

        public DashboardRecepcionistaBase(UsuarioSistema usuario)
            : base(usuario, "SysGym - Recepcionista", Color.FromArgb(5, 150, 105))
        {
            var clientes = AgregarSeccion("Clientes");
            AgregarOpcion(clientes, "Socios", delegate { AbrirFormulario(new GestionSociosForm(Color.FromArgb(5, 150, 105))); });
            AgregarOpcion(clientes, "Membresías", delegate { AbrirFormulario(new GestionMembresiasForm(usuario)); });

            var caja = AgregarSeccion("Caja");
            AgregarOpcion(caja, "Cuotas y pagos", delegate { AbrirFormulario(new GestionPagosForm()); });

            var entrenadores = AgregarSeccion("Entrenadores");
            AgregarOpcion(entrenadores, "Asignar entrenador", delegate { AbrirFormulario(new GestionAsignacionesForm()); });
            AgregarOpcion(entrenadores, "Consultar entrenador", delegate { AbrirFormulario(new GestionAsignacionesForm()); });

            var control = AgregarSeccion("Control de acceso");
            AgregarOpcion(control, "Asistencias", delegate { AbrirFormulario(new GestionAsistenciasForm(Color.FromArgb(5, 150, 105))); });
        }
    }
}
