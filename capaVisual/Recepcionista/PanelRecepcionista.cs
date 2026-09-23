using System;
using System.Drawing;
using System.Windows.Forms;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaVisual.Compartido;

using exxen2._0.capaLogica.Navegacion;

using exxen2._0.capaLogica.Utilidades;

namespace exxen2._0.capaVisual.Recepcionista
{
    /* Presenta recepcionista y atiende sus acciones mediante eventos de Windows Forms. */
    [System.ComponentModel.DesignerCategory("Form")]
    public partial class PanelRecepcionista : Form, ISesionPanel
    {
        private readonly UsuarioSistema usuario;
        private readonly ControladorNavegacion navegacion;
        private bool clientesExpandida = true;
        private bool cajaExpandida;
        private bool entrenadoresExpandida;
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
            Icon = Properties.Resources.SysGym;
            navegacion = new ControladorNavegacion(this, panelContenido, EstablecerModuloActual);
        }

        /* Configura las secciones del menú lateral para que puedan expandirse y contraerse. */
        private void ConfigurarMenuDesplegable()
        {
            lblClientes.Click += clientes_Click;
            lblCaja.Click += caja_Click;
            lblEntrenadores.Click += entrenadores_Click;
            AplicarMenuDesplegable();
        }

        /* Actualiza la visibilidad y posición de todas las secciones del menú recepcionista. */
        private void AplicarMenuDesplegable()
        {
            var posicionY = 18;
            posicionY = MenuDesplegableHelper.ColocarSeccion(lblClientes, clientesExpandida, posicionY, btnSocios, btnMembresias);
            posicionY = MenuDesplegableHelper.ColocarSeccion(lblCaja, cajaExpandida, posicionY, btnPagos);
            posicionY = MenuDesplegableHelper.ColocarSeccion(lblEntrenadores, entrenadoresExpandida, posicionY, btnAsignar, btnConsultar);
            panelOpciones.AutoScrollMinSize = new Size(0, posicionY);
        }

        /* Al hacer clic en clientes, muestra u oculta sus opciones. */
        private void clientes_Click(object origen, EventArgs e)
        {
            clientesExpandida = !clientesExpandida;
            AplicarMenuDesplegable();
        }

        /* Al hacer clic en caja, muestra u oculta sus opciones. */
        private void caja_Click(object origen, EventArgs e)
        {
            cajaExpandida = !cajaExpandida;
            AplicarMenuDesplegable();
        }

        /* Al hacer clic en entrenadores, muestra u oculta sus opciones. */
        private void entrenadores_Click(object origen, EventArgs e)
        {
            entrenadoresExpandida = !entrenadoresExpandida;
            AplicarMenuDesplegable();
        }

        /* Recibe el «Título | Subtítulo» de ControladorNavegacion; al volver a Inicio también quita el resaltado del menú. */
        private void EstablecerModuloActual(string titulo)
        {
            EncabezadoPanelHelper.EstablecerModulo(titulo, "Panel del recepcionista", lblModuloActual, lblSubtituloModulo, btnVolver);
            if (string.IsNullOrWhiteSpace(titulo))
                EncabezadoPanelHelper.MarcarOpcionActiva(panelOpciones, null);
        }

        /* Resalta la opción elegida y abre su módulo mediante la navegación existente. */
        private void Abrir(Button opcion, Form formulario, string titulo)
        {
            EncabezadoPanelHelper.MarcarOpcionActiva(panelOpciones, opcion);
            navegacion.AbrirFormulario(formulario, titulo);
        }

        /* Cierra el módulo actual y restaura el inicio de recepción sin cerrar la sesión. */
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
            Abrir(btnSocios, new GestionSociosFormulario(Color.FromArgb(5, 150, 105)), "Socios | Gestión de socios e información personal");
        }

        /* Al hacer clic en btnMembresias, abre el módulo correspondiente dentro del panel principal. */
        private void btnMembresias_Click(object origen, EventArgs e)
        {
            Abrir(btnMembresias, new GestionMembresiasFormulario(usuario), "Membresías | Gestión de membresías de socios");
        }

        /* Al hacer clic en btnPagos, abre el módulo correspondiente dentro del panel principal. */
        private void btnPagos_Click(object origen, EventArgs e)
        {
            Abrir(btnPagos, new GestionPagosFormulario(), "Cuotas y pagos | Gestión de cuotas y pagos");
        }

        /* Al hacer clic en btnAsignar, abre el módulo correspondiente dentro del panel principal. */
        private void btnAsignar_Click(object origen, EventArgs e)
        {
            Abrir(btnAsignar, new GestionAsignacionesFormulario(), "Asignar entrenador | Vinculación de entrenadores y membresías");
        }

        /* Al hacer clic en btnConsultar, abre el módulo correspondiente dentro del panel principal. */
        private void btnConsultar_Click(object origen, EventArgs e)
        {
            Abrir(btnConsultar, new ConsultaEntrenadoresFormulario(), "Consultar entrenador | Entrenadores y socios asignados");
        }

        /* Al cargar la pantalla en ejecución, prepara sus datos iniciales sin realizar consultas desde el diseñador. */
        private void PanelRecepcionista_Load(object origen, EventArgs e)
        {
            if (AyudaFormularioVisual.EnModoDisenio(this))
                return;
            ConfigurarMenuDesplegable();
            EncabezadoPanelHelper.MostrarUsuario(usuario, "Recepcionista", picUsuario, lblUsuario, lblRol, lblDni, lblSexo);
            navegacion.EstablecerContenidoInicio(panelInicio, null);
        }
    }
}
