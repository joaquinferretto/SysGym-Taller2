using System;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Compartido
{
    /* Expone la intención de cambiar de cuenta al cerrar un panel principal. */
    public interface ISesionPanel
    {
        bool CambioCuentaSolicitado { get; }
    }

    /* Coordina la navegación entre los formularios existentes sin aportar controles ni herencia visual. */
    internal sealed class ControladorNavegacion
    {
        private readonly Form propietario;
        private readonly Panel panelContenido;
        private readonly Action<string> establecerModulo;
        private Form formularioActual;
        private Control contenidoInicio;
        private Action actualizarContenidoInicio;
        /* Inicializa los componentes existentes y las dependencias de la pantalla sin consultar la base de datos. */
        internal ControladorNavegacion(Form propietario, Panel panelContenido)
            : this(propietario, panelContenido, null)
        {
        }

        /* Inicializa la navegación y el actualizador del título del encabezado global. */
        internal ControladorNavegacion(Form propietario, Panel panelContenido, Action<string> establecerModulo)
        {
            if (propietario == null)
                throw new ArgumentNullException("propietario");
            if (panelContenido == null)
                throw new ArgumentNullException("panelContenido");
            this.propietario = propietario;
            this.panelContenido = panelContenido;
            this.establecerModulo = establecerModulo;
        }

        internal bool CambioCuentaSolicitado { get; private set; }

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

        /* Muestra el módulo solicitado dentro del panel principal y libera el módulo anterior. */
        internal void AbrirFormulario(Form formulario)
        {
            AbrirFormulario(formulario, null);
        }

        /* Muestra el módulo solicitado y actualiza el título central del encabezado global. */
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
                    formularioActual.BringToFront();
                    return;
                }

                var anterior = formularioActual;
                formularioActual = null;
                anterior.Close();
                anterior.Dispose();
            }

            formularioActual = formulario;
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            formulario.MinimumSize = Size.Empty;
            formulario.FormClosed += new FormClosedEventHandler(formulario_FormClosed);
            ActualizarModulo(tituloModulo);
            if (contenidoInicio != null)
                contenidoInicio.Visible = false;
            panelContenido.Controls.Add(formulario);
            formulario.Show();
            formulario.BringToFront();
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
            if (contenidoInicio.Parent != panelContenido)
                panelContenido.Controls.Add(contenidoInicio);
            contenidoInicio.Visible = true;
            contenidoInicio.BringToFront();
            ActualizarModulo(string.Empty);
            if (actualizar && actualizarContenidoInicio != null)
                actualizarContenidoInicio();
        }
    }
}
