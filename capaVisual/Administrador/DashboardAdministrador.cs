using System;
using System.Drawing;
using System.Windows.Forms;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaVisual.Compartido;
using exxen2._0.capaVisual.Recepcionista;

namespace exxen2._0.capaVisual.Administrador
{
    [System.ComponentModel.DesignerCategory("Form")]
    public partial class DashboardAdministrador : Form, IDashboardSesion
    {
        private readonly UsuarioSistema usuario;
        private readonly DashboardController navegacion;

        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public bool CambioCuentaSolicitado
        {
            get { return navegacion != null && navegacion.CambioCuentaSolicitado; }
        }

        public DashboardAdministrador()
            : this(new UsuarioSistema { Nombre = "Administrador", Apellido = "de diseno" })
        {
        }

        public DashboardAdministrador(UsuarioSistema usuario)
        {
            if (usuario == null) throw new ArgumentNullException("usuario");
            this.usuario = usuario;
            InitializeComponent();
            lblUsuarioRol.Text = "Usuario: " + usuario.Nombre + " " + usuario.Apellido + "    |    Rol: " + NombreRol(usuario, "Administrador");
            navegacion = new DashboardController(this, panelContenido);
            btnCambiarCuenta.Click += delegate { navegacion.CambiarCuenta(); };
            btnSalir.Click += delegate { navegacion.Salir(); };
            btnUsuarios.Click += delegate { navegacion.AbrirFormulario(new GestionUsuariosForm()); };
            btnSocios.Click += delegate { navegacion.AbrirFormulario(new GestionSociosForm(Color.FromArgb(79, 70, 229))); };
            btnPlanes.Click += delegate { navegacion.AbrirFormulario(new GestionPlanesForm()); };
            btnMembresias.Click += delegate { navegacion.AbrirFormulario(new GestionMembresiasForm(usuario, Color.FromArgb(79, 70, 229))); };
            btnPagos.Click += delegate { navegacion.AbrirFormulario(new GestionPagosForm()); };
            btnEjercicios.Click += delegate { navegacion.AbrirFormulario(new GestionEjerciciosForm(Color.FromArgb(79, 70, 229))); };
            btnRutinas.Click += delegate { navegacion.AbrirFormulario(new ConsultaRutinasAdministradorForm()); };
            btnReportes.Click += delegate { navegacion.AbrirFormulario(new ReportesForm()); };
            navegacion.EstablecerContenidoInicio(dashboardInicio, dashboardInicio.Actualizar);
        }

        private static string NombreRol(UsuarioSistema usuarioActual, string predeterminado)
        {
            return usuarioActual.Rol == null || string.IsNullOrWhiteSpace(usuarioActual.Rol.Descripcion)
                ? predeterminado
                : usuarioActual.Rol.Descripcion;
        }

        private void dashboardInicio_Load(object sender, EventArgs e)
        {

        }
    }
}
