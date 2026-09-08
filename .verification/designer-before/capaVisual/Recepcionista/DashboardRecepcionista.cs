using System;
using System.Drawing;
using System.Windows.Forms;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaVisual.Compartido;

namespace exxen2._0.capaVisual.Recepcionista
{
    [System.ComponentModel.DesignerCategory("Form")]
    public partial class DashboardRecepcionista : Form, IDashboardSesion
    {
        private readonly UsuarioSistema usuario;
        private readonly DashboardController navegacion;

        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public bool CambioCuentaSolicitado
        {
            get { return navegacion != null && navegacion.CambioCuentaSolicitado; }
        }

        public DashboardRecepcionista()
            : this(new UsuarioSistema { Nombre = "Recepcionista", Apellido = "de diseno" })
        {
        }

        public DashboardRecepcionista(UsuarioSistema usuario)
        {
            if (usuario == null) throw new ArgumentNullException("usuario");
            this.usuario = usuario;
            InitializeComponent();
            lblUsuarioRol.Text = "Usuario: " + usuario.Nombre + " " + usuario.Apellido + "    |    Rol: " + NombreRol(usuario, "Recepcionista");
            navegacion = new DashboardController(this, panelContenido);
            btnCambiarCuenta.Click += delegate { navegacion.CambiarCuenta(); };
            btnSalir.Click += delegate { navegacion.Salir(); };
            btnSocios.Click += delegate { navegacion.AbrirFormulario(new GestionSociosForm(Color.FromArgb(5, 150, 105))); };
            btnMembresias.Click += delegate { navegacion.AbrirFormulario(new GestionMembresiasForm(usuario)); };
            btnPagos.Click += delegate { navegacion.AbrirFormulario(new GestionPagosForm()); };
            btnAsignar.Click += delegate { navegacion.AbrirFormulario(new GestionAsignacionesForm()); };
            btnConsultar.Click += delegate { navegacion.AbrirFormulario(new GestionAsignacionesForm()); };
            btnAsistencias.Click += delegate { navegacion.AbrirFormulario(new GestionAsistenciasForm(Color.FromArgb(5, 150, 105))); };
        }

        private static string NombreRol(UsuarioSistema usuarioActual, string predeterminado)
        {
            return usuarioActual.Rol == null || string.IsNullOrWhiteSpace(usuarioActual.Rol.Descripcion)
                ? predeterminado
                : usuarioActual.Rol.Descripcion;
        }
    }
}
