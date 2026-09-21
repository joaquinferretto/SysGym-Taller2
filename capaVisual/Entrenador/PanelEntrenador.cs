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

        /* Obtiene la descripción del rol o utiliza el nombre predeterminado cuando no está disponible. */
        private static string NombreRol(UsuarioSistema usuarioActual, string predeterminado)
        {
            return usuarioActual.Rol == null || string.IsNullOrWhiteSpace(usuarioActual.Rol.Descripcion) ? predeterminado : usuarioActual.Rol.Descripcion;
        }

        /* Al hacer clic en btnCambiarCuenta, cierra la sesión para volver al acceso. */
        private void EstablecerModuloActual(string titulo)
        {
            var partes = (titulo ?? string.Empty).Split(new[] { '|' }, 2);
            lblModuloActual.Text = string.IsNullOrWhiteSpace(titulo) ? "Inicio" : partes[0].Trim();
            lblSubtituloModulo.Text = partes.Length > 1 ? partes[1].Trim() : string.Empty;
            btnVolver.Enabled = !string.IsNullOrWhiteSpace(titulo);
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

        /* Al hacer clic en btnSocios, abre el módulo correspondiente dentro del panel principal. */
        private void btnSocios_Click(object origen, EventArgs e)
        {
            navegacion.AbrirFormulario(new MisSociosFormulario(usuario), "Socios y rutinas | Consulta de rutinas de socios");
        }

        /* Al hacer clic en btnRutinas, abre el módulo correspondiente dentro del panel principal. */
        private void btnRutinas_Click(object origen, EventArgs e)
        {
            navegacion.AbrirFormulario(new RutinasEntrenadorFormulario(usuario), "Gestionar rutinas | Catálogo y composición de rutinas");
        }

        /* Al hacer clic en btnEjercicios, abre el módulo correspondiente dentro del panel principal. */
        private void btnEjercicios_Click(object origen, EventArgs e)
        {
            navegacion.AbrirFormulario(new GestionEjerciciosFormulario(), "Ejercicios | Catálogo de ejercicios");
        }

        /* Al cargar la pantalla en ejecución, prepara sus datos iniciales sin realizar consultas desde el diseñador. */
        private void PanelEntrenador_Load(object origen, EventArgs e)
        {
            if (AyudaFormularioVisual.EnModoDisenio(this))
                return;
            ConfigurarMenuDesplegable();
            lblUsuarioRol.Text = "Usuario: " + usuario.Nombre + " " + usuario.Apellido + "    |    Rol: " + NombreRol(usuario, "Entrenador");
            navegacion.EstablecerContenidoInicio(lblBienvenida, null);
        }
    }
}
