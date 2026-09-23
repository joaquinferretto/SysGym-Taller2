using System;
using System.Drawing;
using System.Windows.Forms;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaLogica;
using exxen2._0.capaVisual.Compartido;
using exxen2._0.capaVisual.Entrenador;
using exxen2._0.capaVisual.Recepcionista;

using exxen2._0.capaLogica.Navegacion;

using exxen2._0.capaLogica.Utilidades;

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
            Icon = Properties.Resources.SysGym;
            navegacion = new ControladorNavegacion(this, panelContenido, EstablecerModuloActual);
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
            posicionY = MenuDesplegableHelper.ColocarSeccion(lblOperacion, operacionExpandida, posicionY, btnPlanes, btnMembresias, btnPagos, btnAsignaciones);
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

        /* Recibe el «Título | Subtítulo» de ControladorNavegacion; al volver a Inicio también quita el resaltado del menú. */
        private void EstablecerModuloActual(string titulo)
        {
            EncabezadoPanelHelper.EstablecerModulo(titulo, "Resumen general", lblModuloActual, lblSubtituloModulo, btnVolver);
            if (string.IsNullOrWhiteSpace(titulo))
                EncabezadoPanelHelper.MarcarOpcionActiva(panelOpciones, null);
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

        /* Al hacer clic en btnUsuarios, abre el módulo correspondiente dentro del panel principal. */
        private void btnUsuarios_Click(object origen, EventArgs e)
        {
            var formulario = new GestionUsuariosFormulario();
            formulario.UsuarioActualizado += usuarios_UsuarioActualizado;
            Abrir(btnUsuarios, formulario, "Usuarios y roles | Administración del personal y sus permisos");
        }

        /* Si el usuario editado es el de la sesión, recarga sus datos y refresca foto, nombre y rol del encabezado sin volver a iniciar sesión. */
        private void usuarios_UsuarioActualizado(object origen, UsuarioActualizadoEventArgs e)
        {
            if (e.IdUsuarioSistema != usuario.IdUsuarioSistema)
                return;
            var actualizado = new UsuarioSistemaLogica().ObtenerPorId(e.IdUsuarioSistema);
            if (actualizado == null)
                return;
            // Se copian los datos al mismo objeto porque los módulos abiertos comparten esta referencia de la sesión.
            usuario.Nombre = actualizado.Nombre;
            usuario.Apellido = actualizado.Apellido;
            usuario.DNI = actualizado.DNI;
            usuario.Telefono = actualizado.Telefono;
            usuario.FechaNacimiento = actualizado.FechaNacimiento;
            usuario.Sexo = actualizado.Sexo;
            usuario.FotoRuta = actualizado.FotoRuta;
            usuario.NombreUsuario = actualizado.NombreUsuario;
            usuario.Salario = actualizado.Salario;
            usuario.Estado = actualizado.Estado;
            usuario.IdRol = actualizado.IdRol;
            usuario.Rol = actualizado.Rol;
            EncabezadoPanelHelper.MostrarUsuario(usuario, "Administrador", picUsuario, lblUsuario, lblRol, lblDni, lblSexo);
        }

        /* Al hacer clic en btnSocios, abre el módulo correspondiente dentro del panel principal. */
        private void btnSocios_Click(object origen, EventArgs e)
        {
            Abrir(btnSocios, new GestionSociosFormulario(Color.FromArgb(79, 70, 229)), "Socios | Gestión de socios e información personal");
        }

        /* Al hacer clic en btnPlanes, abre el módulo correspondiente dentro del panel principal. */
        private void btnPlanes_Click(object origen, EventArgs e)
        {
            Abrir(btnPlanes, new GestionPlanesFormulario(), "Planes | Gestión de planes y precios");
        }

        /* Al hacer clic en btnMembresias, abre el módulo correspondiente dentro del panel principal. */
        private void btnMembresias_Click(object origen, EventArgs e)
        {
            Abrir(btnMembresias, new GestionMembresiasFormulario(usuario, Color.FromArgb(79, 70, 229)), "Membresías | Gestión de membresías de socios");
        }

        /* Al hacer clic en btnPagos, abre el módulo correspondiente dentro del panel principal. */
        private void btnPagos_Click(object origen, EventArgs e)
        {
            Abrir(btnPagos, new GestionPagosFormulario(), "Cuotas y pagos | Gestión de cuotas y pagos");
        }

        /* Al hacer clic en btnEjercicios, abre el módulo correspondiente dentro del panel principal. */
        private void btnEjercicios_Click(object origen, EventArgs e)
        {
            Abrir(btnEjercicios, new GestionEjerciciosFormulario(), "Ejercicios | Catálogo de ejercicios");
        }

        /* Al hacer clic en btnRutinas, abre el módulo correspondiente dentro del panel principal. */
        private void btnRutinas_Click(object origen, EventArgs e)
        {
            Abrir(btnRutinas, new RutinasEntrenadorFormulario(usuario, true), "Gestionar rutinas | Catálogo y composición de rutinas");
        }

        /* Al hacer clic en btnAsignaciones, abre la gestión de entrenadores de las membresías. */
        private void btnAsignaciones_Click(object origen, EventArgs e)
        {
            Abrir(btnAsignaciones, new GestionAsignacionesFormulario(), "Asignar entrenador | Vinculación de entrenadores y membresías");
        }

        /* Al hacer clic en btnMisSocios, abre la gestión global de socios y rutinas. */
        private void btnMisSocios_Click(object origen, EventArgs e)
        {
            Abrir(btnMisSocios, new MisSociosFormulario(usuario, true), "Socios y rutinas | Consulta de rutinas de socios");
        }

        /* Al hacer clic en btnReportes, abre el módulo correspondiente dentro del panel principal. */
        private void btnReportes_Click(object origen, EventArgs e)
        {
            Abrir(btnReportes, new ReportesFormulario(), "Reportes | Consultas e indicadores");
        }

        /* Al recibir un doble clic del estado de cuenta, abre socios con el registro ya seleccionado. */
        private void inicioPanel_SocioDobleClic(object origen, SocioEstadoCuentaEventArgs e)
        {
            Abrir(btnSocios, new GestionSociosFormulario(Color.FromArgb(79, 70, 229), e.IdSocio, true), "Socios | Gestión de socios e información personal");
        }

        /* Al cargar la pantalla en ejecución, prepara sus datos iniciales sin realizar consultas desde el diseñador. */
        private void PanelAdministrador_Load(object origen, EventArgs e)
        {
            if (AyudaFormularioVisual.EnModoDisenio(this))
                return;
            ConfigurarMenuDesplegable();
            EncabezadoPanelHelper.MostrarUsuario(usuario, "Administrador", picUsuario, lblUsuario, lblRol, lblDni, lblSexo);
            navegacion.EstablecerContenidoInicio(inicioPanel, inicioPanel.Actualizar);
        }

    }
}
