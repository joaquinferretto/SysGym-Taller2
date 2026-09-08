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
    public partial class PanelAdministrador : Form, ISesionPanel
    {
        private readonly UsuarioSistema usuario;
        private readonly ControladorNavegacion navegacion;
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public bool CambioCuentaSolicitado
        {
            /* Permite al formulario de acceso saber si debe iniciar otra sesión al cerrar el panel principal. */
            get
            {
                return navegacion != null && navegacion.CambioCuentaSolicitado;
            }
        }

        /* Inicializa los componentes existentes y las dependencias de la pantalla sin consultar la base de datos. */
        public PanelAdministrador() : this(new UsuarioSistema { Nombre = "Administrador", Apellido = "de diseno" })
        {
        }

        /* Inicializa los componentes existentes y las dependencias de la pantalla sin consultar la base de datos. */
        public PanelAdministrador(UsuarioSistema usuario)
        {
            if (usuario == null)
                throw new ArgumentNullException("usuario");
            this.usuario = usuario;
            InitializeComponent();
            navegacion = new ControladorNavegacion(this, panelContenido);
        }

        /* Obtiene la descripción del rol o utiliza el nombre predeterminado cuando no está disponible. */
        private static string NombreRol(UsuarioSistema usuarioActual, string predeterminado)
        {
            return usuarioActual.Rol == null || string.IsNullOrWhiteSpace(usuarioActual.Rol.Descripcion) ? predeterminado : usuarioActual.Rol.Descripcion;
        }

        /* Al hacer clic en btnCambiarCuenta, cierra la sesión para volver al acceso. */
        private void btnCambiarCuenta_Click(object origen, EventArgs e)
        {
            navegacion.CambiarCuenta();
        }

        /* Al hacer clic en btnSalir, inicia el cierre de la pantalla. */
        private void btnSalir_Click(object origen, EventArgs e)
        {
            navegacion.Salir();
        }

        /* Al hacer clic en btnUsuarios, abre el módulo correspondiente dentro del panel principal. */
        private void btnUsuarios_Click(object origen, EventArgs e)
        {
            navegacion.AbrirFormulario(new GestionUsuariosFormulario());
        }

        /* Al hacer clic en btnSocios, abre el módulo correspondiente dentro del panel principal. */
        private void btnSocios_Click(object origen, EventArgs e)
        {
            navegacion.AbrirFormulario(new GestionSociosFormulario(Color.FromArgb(79, 70, 229)));
        }

        /* Al hacer clic en btnPlanes, abre el módulo correspondiente dentro del panel principal. */
        private void btnPlanes_Click(object origen, EventArgs e)
        {
            navegacion.AbrirFormulario(new GestionPlanesFormulario());
        }

        /* Al hacer clic en btnMembresias, abre el módulo correspondiente dentro del panel principal. */
        private void btnMembresias_Click(object origen, EventArgs e)
        {
            navegacion.AbrirFormulario(new GestionMembresiasFormulario(usuario, Color.FromArgb(79, 70, 229)));
        }

        /* Al hacer clic en btnPagos, abre el módulo correspondiente dentro del panel principal. */
        private void btnPagos_Click(object origen, EventArgs e)
        {
            navegacion.AbrirFormulario(new GestionPagosFormulario());
        }

        /* Al hacer clic en btnEjercicios, abre el módulo correspondiente dentro del panel principal. */
        private void btnEjercicios_Click(object origen, EventArgs e)
        {
            navegacion.AbrirFormulario(new GestionEjerciciosFormulario(Color.FromArgb(79, 70, 229)));
        }

        /* Al hacer clic en btnRutinas, abre el módulo correspondiente dentro del panel principal. */
        private void btnRutinas_Click(object origen, EventArgs e)
        {
            navegacion.AbrirFormulario(new ConsultaRutinasAdministradorFormulario());
        }

        /* Al hacer clic en btnReportes, abre el módulo correspondiente dentro del panel principal. */
        private void btnReportes_Click(object origen, EventArgs e)
        {
            navegacion.AbrirFormulario(new ReportesFormulario());
        }

        /* Al cargar la pantalla en ejecución, prepara sus datos iniciales sin realizar consultas desde el diseñador. */
        private void PanelAdministrador_Load(object origen, EventArgs e)
        {
            if (AyudaFormularioVisual.EnModoDisenio(this))
                return;
            lblUsuarioRol.Text = "Usuario: " + usuario.Nombre + " " + usuario.Apellido + "    |    Rol: " + NombreRol(usuario, "Administrador");
            navegacion.EstablecerContenidoInicio(inicioPanel, inicioPanel.Actualizar);
        }
    }
}
