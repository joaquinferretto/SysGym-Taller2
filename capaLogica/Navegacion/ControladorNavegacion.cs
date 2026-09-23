using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace exxen2._0.capaLogica.Navegacion
{
    /* Expone la intención de cambiar de cuenta al cerrar un panel principal. */
    public interface ISesionPanel
    {
        bool CambioCuentaSolicitado { get; }
    }

    /* Coordina la navegación MDI: los módulos se abren como formularios hijos del panel principal. */
    // El panel principal tiene IsMdiContainer = true, por eso WinForms le agrega un control MdiClient (el "área MDI")
    // que ocupa el espacio libre entre el menú lateral y el encabezado. Los módulos se abren ahí con MdiParent.
    // panelInicio también ocupa ese espacio (Dock = Fill) y queda por encima del área MDI: se oculta mientras hay un
    // módulo abierto y vuelve a mostrarse al cerrarlo.
    internal sealed class ControladorNavegacion
    {
        private readonly Form propietario;
        private readonly Panel panelInicio;
        private readonly Action<string> establecerModulo;
        private readonly MdiClient areaMdi;
        private Form formularioActual;
        private Control contenidoInicio;
        private Action actualizarContenidoInicio;
        /* Inicializa los componentes existentes y las dependencias de la pantalla sin consultar la base de datos. */
        internal ControladorNavegacion(Form propietario, Panel panelInicio)
            : this(propietario, panelInicio, null)
        {
        }

        /* Inicializa la navegación MDI y el actualizador del título del encabezado global. */
        internal ControladorNavegacion(Form propietario, Panel panelInicio, Action<string> establecerModulo)
        {
            if (propietario == null)
                throw new ArgumentNullException("propietario");
            if (panelInicio == null)
                throw new ArgumentNullException("panelInicio");
            if (!propietario.IsMdiContainer)
                throw new InvalidOperationException("El panel principal debe tener IsMdiContainer = true.");
            this.propietario = propietario;
            this.panelInicio = panelInicio;
            this.establecerModulo = establecerModulo;
            areaMdi = propietario.Controls.OfType<MdiClient>().First();  // WinForms lo crea al activar IsMdiContainer.
            PrepararAreaMdi();
            Utilidades.ContrasteVisual.Aplicar(propietario);  // Cubre el panel, el inicio y cada módulo que se abra.
        }

        internal bool CambioCuentaSolicitado { get; private set; }

        /* Cierra el módulo actual; su evento de cierre restaura el inicio existente. */
        internal void VolverAlInicio()
        {
            if (formularioActual != null && !formularioActual.IsDisposed)
                formularioActual.Close();
        }

        /* Marca el cambio de sesión y cierra el panel principal para volver al acceso. */
        internal void CambiarCuenta()
        {
            CambioCuentaSolicitado = true;
            propietario.Close();
        }

        /* Solicita confirmación antes de cerrar la sesión y salir de la aplicación. */
        internal void Salir()
        {
            if (MessageBox.Show("Deseas salir de SysGym?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            CambioCuentaSolicitado = false;
            propietario.Close();
        }

        /* Muestra el módulo solicitado como hijo MDI y cierra el módulo anterior. */
        internal void AbrirFormulario(Form formulario)
        {
            AbrirFormulario(formulario, null);
        }

        /* Muestra el módulo solicitado como hijo MDI y actualiza el título central del encabezado global. */
        internal void AbrirFormulario(Form formulario, string tituloModulo)
        {
            if (formulario == null)
                return;
            if (formularioActual != null && !formularioActual.IsDisposed)
            {
                if (formularioActual.GetType() == formulario.GetType())
                {
                    formulario.Dispose();
                    ActualizarModulo(tituloModulo);
                    formularioActual.Activate();
                    return;
                }

                var anterior = formularioActual;
                formularioActual = null;  // Antes de cerrar: así su FormClosed no vuelve a mostrar el inicio.
                anterior.Close();
                anterior.Dispose();
            }

            formularioActual = formulario;
            formulario.MdiParent = propietario;  // Ensamble MDI: el módulo pasa a vivir dentro del área MDI.
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.ControlBox = false;  // Sin botones de ventana ni menú de sistema.
            formulario.MinimizeBox = false;
            formulario.MaximizeBox = false;
            formulario.ShowIcon = false;
            formulario.MinimumSize = Size.Empty;
            // Dock en lugar de maximizar: un hijo maximizado agrega su título al del panel ("SysGym - [..]") y una barra
            // con sus botones de ventana, y Windows lo ubica como si todavía tuviera borde.
            formulario.Dock = DockStyle.Fill;
            formulario.FormClosed += new FormClosedEventHandler(formulario_FormClosed);
            ActualizarModulo(tituloModulo);
            panelInicio.Visible = false;  // Deja ver el área MDI que estaba debajo.
            formulario.Show();
            formulario.Activate();
        }

        /* Actualiza el texto del módulo sin crear controles desde runtime. */
        private void ActualizarModulo(string tituloModulo)
        {
            if (establecerModulo != null)
                establecerModulo(tituloModulo ?? string.Empty);
        }

        /* Registra el contenido inicial existente y la acción que lo actualiza al volver. */
        internal void EstablecerContenidoInicio(Control control, Action actualizar)
        {
            if (control == null)
                throw new ArgumentNullException("control");
            contenidoInicio = control;
            actualizarContenidoInicio = actualizar;
            MostrarContenidoInicio(false);
        }

        /* Al cerrar el módulo activo, restaura el contenido inicial del panel principal. */
        private void formulario_FormClosed(object origen, FormClosedEventArgs e)
        {
            if (e.CloseReason == CloseReason.MdiFormClosing)
                return;  // Se está cerrando el panel principal: no hay inicio que restaurar.
            if (!ReferenceEquals(formularioActual, origen))
                return;
            formularioActual = null;
            ActualizarModulo(string.Empty);
            MostrarContenidoInicio(true);
        }

        /* Restaura el contenido inicial del panel principal y lo actualiza cuando corresponde. */
        private void MostrarContenidoInicio(bool actualizar)
        {
            if (contenidoInicio == null || contenidoInicio.IsDisposed)
                return;
            if (contenidoInicio.Parent != panelInicio)
                panelInicio.Controls.Add(contenidoInicio);
            contenidoInicio.Visible = true;
            contenidoInicio.BringToFront();
            panelInicio.Visible = true;
            ActualizarModulo(string.Empty);
            if (actualizar && actualizarContenidoInicio != null)
                actualizarContenidoInicio();
        }

        /* Ordena el área MDI debajo del inicio, le da el fondo del panel y le quita el borde 3D de Windows. */
        private void PrepararAreaMdi()
        {
            // Dock se resuelve desde el último control de la colección hacia el primero: el área MDI y el inicio
            // quedan en los índices 1 y 0 para ocupar lo que dejan libre el menú (Left) y el encabezado (Top).
            propietario.Controls.SetChildIndex(areaMdi, 0);
            panelInicio.BringToFront();
            areaMdi.BackColor = panelInicio.BackColor;  // Por defecto es gris (AppWorkspace).
            if (areaMdi.IsHandleCreated)
                QuitarBorde3D(areaMdi);
            else
                areaMdi.HandleCreated += (origen, e) => QuitarBorde3D(areaMdi);
        }

        /* El MdiClient no tiene propiedad para su borde hundido: se quita el estilo WS_EX_CLIENTEDGE con la API de Windows. */
        private static void QuitarBorde3D(MdiClient area)
        {
            var estilo = MetodosNativos.GetWindowLong(area.Handle, MetodosNativos.GWL_EXSTYLE);
            if ((estilo & MetodosNativos.WS_EX_CLIENTEDGE) == 0)
                return;
            MetodosNativos.SetWindowLong(area.Handle, MetodosNativos.GWL_EXSTYLE, estilo & ~MetodosNativos.WS_EX_CLIENTEDGE);
            MetodosNativos.SetWindowPos(area.Handle, IntPtr.Zero, 0, 0, 0, 0, MetodosNativos.SWP_ACTUALIZAR_MARCO);  // Redibuja el marco.
        }

        /* Funciones de user32.dll necesarias para modificar el estilo de la ventana del área MDI. */
        private static class MetodosNativos
        {
            internal const int GWL_EXSTYLE = -20;
            internal const int WS_EX_CLIENTEDGE = 0x200;
            // SWP_NOSIZE | SWP_NOMOVE | SWP_NOZORDER | SWP_NOACTIVATE | SWP_FRAMECHANGED | SWP_NOOWNERZORDER
            internal const uint SWP_ACTUALIZAR_MARCO = 0x0001 | 0x0002 | 0x0004 | 0x0010 | 0x0020 | 0x0200;

            [DllImport("user32.dll")]
            internal static extern int GetWindowLong(IntPtr ventana, int indice);

            [DllImport("user32.dll")]
            internal static extern int SetWindowLong(IntPtr ventana, int indice, int valor);

            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool SetWindowPos(IntPtr ventana, IntPtr despuesDe, int x, int y, int ancho, int alto, uint opciones);
        }
    }
}
