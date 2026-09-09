using System;
using System.Drawing;
using System.Windows.Forms;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaVisual.Compartido;

namespace exxen2._0.capaVisual.Entrenador
{
    /* Presenta entrenador y atiende sus acciones mediante eventos de Windows Forms. */
    [System.ComponentModel.DesignerCategory("Form")]
    public partial class PanelEntrenador : Form, ISesionPanel
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
        public PanelEntrenador() : this(new UsuarioSistema { Nombre = "Entrenador", Apellido = "de diseno" })
        {
        }

        /* Inicializa los componentes existentes y las dependencias de la pantalla sin consultar la base de datos. */
        public PanelEntrenador(UsuarioSistema usuario)
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
            navegacion.AbrirFormulario(new MisSociosFormulario(usuario));
        }

        /* Al hacer clic en btnRutinas, abre el módulo correspondiente dentro del panel principal. */
        private void btnRutinas_Click(object origen, EventArgs e)
        {
            navegacion.AbrirFormulario(new RutinasEntrenadorFormulario(usuario));
        }

        /* Al hacer clic en btnEjercicios, abre el módulo correspondiente dentro del panel principal. */
        private void btnEjercicios_Click(object origen, EventArgs e)
        {
            navegacion.AbrirFormulario(new GestionEjerciciosFormulario(Color.FromArgb(14, 116, 144)));
        }

        /* Al hacer clic en btnAsistencias, abre el módulo correspondiente dentro del panel principal. */
        private void btnAsistencias_Click(object origen, EventArgs e)
        {
            navegacion.AbrirFormulario(new GestionAsistenciasFormulario(Color.FromArgb(14, 116, 144)));
        }

        /* Al cargar la pantalla en ejecución, prepara sus datos iniciales sin realizar consultas desde el diseñador. */
        private void PanelEntrenador_Load(object origen, EventArgs e)
        {
            if (AyudaFormularioVisual.EnModoDisenio(this))
                return;
            lblUsuarioRol.Text = "Usuario: " + usuario.Nombre + " " + usuario.Apellido + "    |    Rol: " + NombreRol(usuario, "Entrenador");
            navegacion.EstablecerContenidoInicio(lblBienvenida, null);
        }
    }
}
