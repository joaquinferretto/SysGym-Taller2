using System;
using System.Drawing;
using System.Windows.Forms;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaVisual.Compartido;

namespace exxen2._0.capaVisual.Recepcionista
{
    /* Presenta recepcionista y atiende sus acciones mediante eventos de Windows Forms. */
    [System.ComponentModel.DesignerCategory("Form")]
    public partial class PanelRecepcionista : Form, ISesionPanel
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
        public PanelRecepcionista() : this(new UsuarioSistema { Nombre = "Recepcionista", Apellido = "de diseno" })
        {
        }

        /* Inicializa los componentes existentes y las dependencias de la pantalla sin consultar la base de datos. */
        public PanelRecepcionista(UsuarioSistema usuario)
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

        /* Al hacer clic en btnSocios, abre el módulo correspondiente dentro del panel principal. */
        private void btnSocios_Click(object origen, EventArgs e)
        {
            navegacion.AbrirFormulario(new GestionSociosFormulario(Color.FromArgb(5, 150, 105)));
        }

        /* Al hacer clic en btnMembresias, abre el módulo correspondiente dentro del panel principal. */
        private void btnMembresias_Click(object origen, EventArgs e)
        {
            navegacion.AbrirFormulario(new GestionMembresiasFormulario(usuario));
        }

        /* Al hacer clic en btnPagos, abre el módulo correspondiente dentro del panel principal. */
        private void btnPagos_Click(object origen, EventArgs e)
        {
            navegacion.AbrirFormulario(new GestionPagosFormulario());
        }

        /* Al hacer clic en btnAsignar, abre el módulo correspondiente dentro del panel principal. */
        private void btnAsignar_Click(object origen, EventArgs e)
        {
            navegacion.AbrirFormulario(new GestionAsignacionesFormulario());
        }

        /* Al hacer clic en btnConsultar, abre el módulo correspondiente dentro del panel principal. */
        private void btnConsultar_Click(object origen, EventArgs e)
        {
            navegacion.AbrirFormulario(new GestionAsignacionesFormulario());
        }

        /* Al hacer clic en btnAsistencias, abre el módulo correspondiente dentro del panel principal. */
        private void btnAsistencias_Click(object origen, EventArgs e)
        {
            navegacion.AbrirFormulario(new GestionAsistenciasFormulario(Color.FromArgb(5, 150, 105)));
        }

        /* Al cargar la pantalla en ejecución, prepara sus datos iniciales sin realizar consultas desde el diseñador. */
        private void PanelRecepcionista_Load(object origen, EventArgs e)
        {
            if (AyudaFormularioVisual.EnModoDisenio(this))
                return;
            lblUsuarioRol.Text = "Usuario: " + usuario.Nombre + " " + usuario.Apellido + "    |    Rol: " + NombreRol(usuario, "Recepcionista");
        }
    }
}
