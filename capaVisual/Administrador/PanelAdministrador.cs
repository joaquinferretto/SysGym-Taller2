using System;
using System.Drawing;
using System.Windows.Forms;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaVisual.Compartido;
using exxen2._0.capaVisual.Entrenador;
using exxen2._0.capaVisual.Recepcionista;

namespace exxen2._0.capaVisual.Administrador
{
    /* Presenta administrador y atiende sus acciones mediante eventos de Windows Forms. */
    [System.ComponentModel.DesignerCategory("Form")]
    public partial class PanelAdministrador : Form, ISesionPanel
    {
        private readonly UsuarioSistema usuario;
        private readonly ControladorNavegacion navegacion;
        private bool administracionExpandida = true;
        private bool operacionExpandida;
        private bool rutinasExpandida;
        private bool consultasExpandida;
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
            inicioPanel.SocioDobleClic += inicioPanel_SocioDobleClic;
        }

        /* Configura las secciones del menú lateral para que puedan expandirse y contraerse. */
        private void ConfigurarMenuDesplegable()
        {
            lblAdministracion.Click += administracion_Click;
            lblOperacion.Click += operacion_Click;
            lblRutinas.Click += rutinas_Click;
            lblConsultas.Click += consultas_Click;
            AplicarMenuDesplegable();
        }

        /* Actualiza la visibilidad y posición de todas las secciones del menú administrador. */
        private void AplicarMenuDesplegable()
        {
            var posicionY = 18;
            posicionY = MenuDesplegableHelper.ColocarSeccion(lblAdministracion, administracionExpandida, posicionY, btnUsuarios, btnSocios);
            posicionY = MenuDesplegableHelper.ColocarSeccion(lblOperacion, operacionExpandida, posicionY, btnPlanes, btnMembresias, btnPagos, btnAsignaciones, btnAsistencias);
            posicionY = MenuDesplegableHelper.ColocarSeccion(lblRutinas, rutinasExpandida, posicionY, btnEjercicios, btnRutinas, btnMisSocios);
            posicionY = MenuDesplegableHelper.ColocarSeccion(lblConsultas, consultasExpandida, posicionY, btnReportes);
            panelOpciones.AutoScrollMinSize = new Size(0, posicionY);
        }

        /* Al hacer clic en administración, muestra u oculta sus opciones. */
        private void administracion_Click(object origen, EventArgs e)
        {
            administracionExpandida = !administracionExpandida;
            AplicarMenuDesplegable();
        }

        /* Al hacer clic en operación, muestra u oculta sus opciones. */
        private void operacion_Click(object origen, EventArgs e)
        {
            operacionExpandida = !operacionExpandida;
            AplicarMenuDesplegable();
        }

        /* Al hacer clic en rutinas, muestra u oculta sus opciones. */
        private void rutinas_Click(object origen, EventArgs e)
        {
            rutinasExpandida = !rutinasExpandida;
            AplicarMenuDesplegable();
        }

        /* Al hacer clic en consultas, muestra u oculta sus opciones. */
        private void consultas_Click(object origen, EventArgs e)
        {
            consultasExpandida = !consultasExpandida;
            AplicarMenuDesplegable();
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
            navegacion.AbrirFormulario(new RutinasEntrenadorFormulario(usuario, true));
        }

        /* Al hacer clic en btnAsignaciones, abre la gestión de entrenadores de las membresías. */
        private void btnAsignaciones_Click(object origen, EventArgs e)
        {
            navegacion.AbrirFormulario(new GestionAsignacionesFormulario());
        }

        /* Al hacer clic en btnAsistencias, abre la gestión de accesos de los socios. */
        private void btnAsistencias_Click(object origen, EventArgs e)
        {
            navegacion.AbrirFormulario(new GestionAsistenciasFormulario(Color.FromArgb(79, 70, 229)));
        }

        /* Al hacer clic en btnMisSocios, abre la gestión global de socios y rutinas. */
        private void btnMisSocios_Click(object origen, EventArgs e)
        {
            navegacion.AbrirFormulario(new MisSociosFormulario(usuario, true));
        }

        /* Al hacer clic en btnReportes, abre el módulo correspondiente dentro del panel principal. */
        private void btnReportes_Click(object origen, EventArgs e)
        {
            navegacion.AbrirFormulario(new ReportesFormulario());
        }

        /* Al recibir un doble clic del estado de cuenta, abre socios con el registro ya seleccionado. */
        private void inicioPanel_SocioDobleClic(object origen, SocioEstadoCuentaEventArgs e)
        {
            navegacion.AbrirFormulario(new GestionSociosFormulario(Color.FromArgb(79, 70, 229), e.IdSocio, true));
        }

        /* Al cargar la pantalla en ejecución, prepara sus datos iniciales sin realizar consultas desde el diseñador. */
        private void PanelAdministrador_Load(object origen, EventArgs e)
        {
            if (AyudaFormularioVisual.EnModoDisenio(this))
                return;
            ConfigurarMenuDesplegable();
            lblUsuarioRol.Text = "Usuario: " + usuario.Nombre + " " + usuario.Apellido + "    |    Rol: " + NombreRol(usuario, "Administrador");
            navegacion.EstablecerContenidoInicio(inicioPanel, inicioPanel.Actualizar);
        }
    }
}
