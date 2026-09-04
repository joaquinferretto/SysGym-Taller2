using System;
using System.Drawing;
using System.Windows.Forms;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaVisual.Compartido;

namespace exxen2._0.capaVisual.Entrenador
{
    [System.ComponentModel.DesignerCategory("Form")]
    public partial class DashboardEntrenador : Form, IDashboardSesion
    {
        private readonly UsuarioSistema usuario;
        private readonly DashboardController navegacion;

        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public bool CambioCuentaSolicitado
        {
            get { return navegacion != null && navegacion.CambioCuentaSolicitado; }
        }

        public DashboardEntrenador()
            : this(new UsuarioSistema { Nombre = "Entrenador", Apellido = "de diseno" })
        {
        }

        public DashboardEntrenador(UsuarioSistema usuario)
        {
            if (usuario == null) throw new ArgumentNullException("usuario");
            this.usuario = usuario;
            InitializeComponent();
            lblUsuarioRol.Text = "Usuario: " + usuario.Nombre + " " + usuario.Apellido + "    |    Rol: " + NombreRol(usuario, "Entrenador");
            navegacion = new DashboardController(this, panelContenido);
            btnCambiarCuenta.Click += delegate { navegacion.CambiarCuenta(); };
            btnSalir.Click += delegate { navegacion.Salir(); };
            btnSocios.Click += delegate { navegacion.AbrirFormulario(new MisSociosForm(usuario)); };
            btnRutinas.Click += delegate { navegacion.AbrirFormulario(new RutinasEntrenadorForm(usuario)); };
            btnEjercicios.Click += delegate { navegacion.AbrirFormulario(new GestionEjerciciosForm(Color.FromArgb(14, 116, 144))); };
            btnAsistencias.Click += delegate { navegacion.AbrirFormulario(new GestionAsistenciasForm(Color.FromArgb(14, 116, 144))); };
        }

        private static string NombreRol(UsuarioSistema usuarioActual, string predeterminado)
        {
            return usuarioActual.Rol == null || string.IsNullOrWhiteSpace(usuarioActual.Rol.Descripcion)
                ? predeterminado
                : usuarioActual.Rol.Descripcion;
        }
    }
}
