using System;
using System.Drawing;
using System.Windows.Forms;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaVisual.Compartido;

using exxen2._0.capaLogica.Navegacion;

using exxen2._0.capaLogica.Utilidades;

namespace exxen2._0.capaVisual.Entrenador
{
    /* Presenta entrenador y atiende sus acciones mediante eventos de Windows Forms. */
    [System.ComponentModel.DesignerCategory("Form")]
    public partial class PanelEntrenador : Form, ISesionPanel
    {
        private readonly UsuarioSistema usuario;
        private readonly ControladorNavegacion navegacion;
        private bool trabajoExpandida = true;
        private bool catalogoExpandida;
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
            Icon = Properties.Resources.SysGym;
            navegacion = new ControladorNavegacion(this, panelContenido, EstablecerModuloActual);
        }

        /* Configura las secciones del menú lateral para que puedan expandirse y contraerse. */
        private void ConfigurarMenuDesplegable()
        {
            lblTrabajo.Click += trabajo_Click;
            lblCatalogo.Click += catalogo_Click;
            AplicarMenuDesplegable();
        }

        /* Actualiza la visibilidad y posición de todas las secciones del menú entrenador. */
        private void AplicarMenuDesplegable()
        {
            var posicionY = 18;
            posicionY = MenuDesplegableHelper.ColocarSeccion(lblTrabajo, trabajoExpandida, posicionY, btnSocios, btnRutinas);
            posicionY = MenuDesplegableHelper.ColocarSeccion(lblCatalogo, catalogoExpandida, posicionY, btnEjercicios);
            panelOpciones.AutoScrollMinSize = new Size(0, posicionY);
        }

        /* Al hacer clic en mi trabajo, muestra u oculta sus opciones. */
        private void trabajo_Click(object origen, EventArgs e)
        {
            trabajoExpandida = !trabajoExpandida;
            AplicarMenuDesplegable();
        }

        /* Al hacer clic en catálogo, muestra u oculta sus opciones. */
        private void catalogo_Click(object origen, EventArgs e)
        {
            catalogoExpandida = !catalogoExpandida;
            AplicarMenuDesplegable();
        }

        /* Recibe el «Título | Subtítulo» de ControladorNavegacion; el Inicio del entrenador es Mis alumnos. */
        private void EstablecerModuloActual(string titulo)
        {
            EncabezadoPanelHelper.EstablecerModulo(titulo, "Alumnos asignados y sus rutinas", lblModuloActual, lblSubtituloModulo, btnVolver, "Mis alumnos");
            if (string.IsNullOrWhiteSpace(titulo))
                EncabezadoPanelHelper.MarcarOpcionActiva(panelOpciones, btnSocios);
        }

        /* Resalta la opción elegida y abre su módulo mediante la navegación existente. */
        private void Abrir(Button opcion, Form formulario, string titulo)
        {
            EncabezadoPanelHelper.MarcarOpcionActiva(panelOpciones, opcion);
            navegacion.AbrirFormulario(formulario, titulo);
        }

        private void btnVolver_Click(object origen, EventArgs e)
        {
            navegacion.VolverAlInicio();
        }

        private void btnCambiarCuenta_Click(object origen, EventArgs e)
        {
            navegacion.CambiarCuenta();
        }

        /* Al hacer clic en btnSalir, inicia el cierre de la pantalla. */
        private void btnSalir_Click(object origen, EventArgs e)
        {
            navegacion.Salir();
        }

        /* Al hacer clic en Mis alumnos, regresa al inicio del entrenador, que es esa misma pantalla. */
        private void btnSocios_Click(object origen, EventArgs e)
        {
            navegacion.VolverAlInicio();
        }

        /* Al hacer clic en btnRutinas, abre el módulo correspondiente dentro del panel principal. */
        private void btnRutinas_Click(object origen, EventArgs e)
        {
            Abrir(btnRutinas, new RutinasEntrenadorFormulario(usuario), "Gestionar rutinas | Catálogo y composición de rutinas");
        }

        /* Al hacer clic en btnEjercicios, abre el módulo correspondiente dentro del panel principal. */
        private void btnEjercicios_Click(object origen, EventArgs e)
        {
            Abrir(btnEjercicios, new GestionEjerciciosFormulario(), "Ejercicios | Catálogo de ejercicios");
        }

        /* Al cargar la pantalla en ejecución, prepara sus datos iniciales sin realizar consultas desde el diseñador. */
        private void PanelEntrenador_Load(object origen, EventArgs e)
        {
            if (AyudaFormularioVisual.EnModoDisenio(this))
                return;
            ConfigurarMenuDesplegable();
            EncabezadoPanelHelper.MostrarUsuario(usuario, "Entrenador", picUsuario, lblUsuario, lblRol, lblDni, lblSexo);
            // Mis alumnos es el inicio: se embebe igual que los módulos y se recarga al volver.
            var misAlumnos = new MisSociosFormulario(usuario)
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill,
                MinimumSize = Size.Empty
            };
            navegacion.EstablecerContenidoInicio(misAlumnos, misAlumnos.Recargar);
        }
    }
}
