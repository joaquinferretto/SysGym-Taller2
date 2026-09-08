using System;
using System.Drawing;
using System.Windows.Forms;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaVisual.Compartido;
using exxen2._0.capaVisual.Recepcionista;

namespace exxen2._0.capaVisual.Administrador
{
    /* Presenta administrador y atiende sus acciones mediante eventos de Windows Forms. */
    [System.ComponentModel.DesignerCategory("Form")]
    public partial class DashboardAdministrador : Form, IDashboardSesion
    {
        private readonly UsuarioSistema usuario;
        private readonly DashboardController navegacion;
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public bool CambioCuentaSolicitado
        {
            /* Permite al formulario de acceso saber si debe iniciar otra sesión al cerrar el dashboard. */
            get
            {
                return navegacion != null && navegacion.CambioCuentaSolicitado;
            }
        }

        /* Inicializa los componentes existentes y las dependencias de la pantalla sin consultar la base de datos. */
        public DashboardAdministrador() : this(new UsuarioSistema { Nombre = "Administrador", Apellido = "de diseno" })
        {
        }

        /* Inicializa los componentes existentes y las dependencias de la pantalla sin consultar la base de datos. */
        public DashboardAdministrador(UsuarioSistema usuario)
        {
            if (usuario == null)
                throw new ArgumentNullException("usuario");
            this.usuario = usuario;
            InitializeComponent();
            navegacion = new DashboardController(this, panelContenido);
        }

        /* Obtiene la descripción del rol o utiliza el nombre predeterminado cuando no está disponible. */
        private static string NombreRol(UsuarioSistema usuarioActual, string predeterminado)
        {
            return usuarioActual.Rol == null || string.IsNullOrWhiteSpace(usuarioActual.Rol.Descripcion) ? predeterminado : usuarioActual.Rol.Descripcion;
        }

        /* Al hacer clic en btnCambiarCuenta, cierra la sesión para volver al acceso. */
        private void btnCambiarCuenta_Click(object sender, EventArgs e)
        {
            navegacion.CambiarCuenta();
        }

        /* Al hacer clic en btnSalir, inicia el cierre de la pantalla. */
        private void btnSalir_Click(object sender, EventArgs e)
        {
            navegacion.Salir();
        }

        /* Al hacer clic en btnUsuarios, abre el módulo correspondiente dentro del dashboard. */
        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            navegacion.AbrirFormulario(new GestionUsuariosForm());
        }

        /* Al hacer clic en btnSocios, abre el módulo correspondiente dentro del dashboard. */
        private void btnSocios_Click(object sender, EventArgs e)
        {
            navegacion.AbrirFormulario(new GestionSociosForm(Color.FromArgb(79, 70, 229)));
        }

        /* Al hacer clic en btnPlanes, abre el módulo correspondiente dentro del dashboard. */
        private void btnPlanes_Click(object sender, EventArgs e)
        {
            navegacion.AbrirFormulario(new GestionPlanesForm());
        }

        /* Al hacer clic en btnMembresias, abre el módulo correspondiente dentro del dashboard. */
        private void btnMembresias_Click(object sender, EventArgs e)
        {
            navegacion.AbrirFormulario(new GestionMembresiasForm(usuario, Color.FromArgb(79, 70, 229)));
        }

        /* Al hacer clic en btnPagos, abre el módulo correspondiente dentro del dashboard. */
        private void btnPagos_Click(object sender, EventArgs e)
        {
            navegacion.AbrirFormulario(new GestionPagosForm());
        }

        /* Al hacer clic en btnEjercicios, abre el módulo correspondiente dentro del dashboard. */
        private void btnEjercicios_Click(object sender, EventArgs e)
        {
            navegacion.AbrirFormulario(new GestionEjerciciosForm(Color.FromArgb(79, 70, 229)));
        }

        /* Al hacer clic en btnRutinas, abre el módulo correspondiente dentro del dashboard. */
        private void btnRutinas_Click(object sender, EventArgs e)
        {
            navegacion.AbrirFormulario(new ConsultaRutinasAdministradorForm());
        }

        /* Al hacer clic en btnReportes, abre el módulo correspondiente dentro del dashboard. */
        private void btnReportes_Click(object sender, EventArgs e)
        {
            navegacion.AbrirFormulario(new ReportesForm());
        }

        /* Al cargar la pantalla en ejecución, prepara sus datos iniciales sin realizar consultas desde el diseñador. */
        private void DashboardAdministrador_Load(object sender, EventArgs e)
        {
            if (FormularioVisualHelper.EnModoDisenio(this))
                return;
            lblUsuarioRol.Text = "Usuario: " + usuario.Nombre + " " + usuario.Apellido + "    |    Rol: " + NombreRol(usuario, "Administrador");
            navegacion.EstablecerContenidoInicio(dashboardInicio, dashboardInicio.Actualizar);
        }
    }
}
