using System;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Compartido
{
    /* Expone la intención de cambiar de cuenta al cerrar un dashboard. */
    public interface IDashboardSesion
    {
        bool CambioCuentaSolicitado { get; }
    }

    /* Coordina la navegación entre los formularios existentes sin aportar controles ni herencia visual. */
    internal sealed class DashboardController
    {
        private readonly Form propietario;
        private readonly Panel panelContenido;
        private Form formularioActual;
        private Control contenidoInicio;
        private Action actualizarContenidoInicio;
        /* Inicializa los componentes existentes y las dependencias de la pantalla sin consultar la base de datos. */
        internal DashboardController(Form propietario, Panel panelContenido)
        {
            if (propietario == null)
                throw new ArgumentNullException("propietario");
            if (panelContenido == null)
                throw new ArgumentNullException("panelContenido");
            this.propietario = propietario;
            this.panelContenido = panelContenido;
        }

        internal bool CambioCuentaSolicitado { get; private set; }

        /* Marca el cambio de sesión y cierra el dashboard para volver al acceso. */
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

        /* Muestra el módulo solicitado dentro del dashboard y libera el módulo anterior. */
        internal void AbrirFormulario(Form formulario)
        {
            if (formulario == null)
                return;
            if (formularioActual != null && !formularioActual.IsDisposed)
            {
                if (formularioActual.GetType() == formulario.GetType())
                {
                    formulario.Dispose();
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
            if (contenidoInicio != null)
                contenidoInicio.Visible = false;
            panelContenido.Controls.Add(formulario);
            formulario.Show();
            formulario.BringToFront();
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

        /* Al cerrar el módulo activo, restaura el contenido inicial del dashboard. */
        private void formulario_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!ReferenceEquals(formularioActual, sender))
                return;
            formularioActual = null;
            MostrarContenidoInicio(true);
        }

        /* Restaura el contenido inicial del dashboard y lo actualiza cuando corresponde. */
        private void MostrarContenidoInicio(bool actualizar)
        {
            if (contenidoInicio == null || contenidoInicio.IsDisposed)
                return;
            if (contenidoInicio.Parent != panelContenido)
                panelContenido.Controls.Add(contenidoInicio);
            contenidoInicio.Visible = true;
            contenidoInicio.BringToFront();
            if (actualizar && actualizarContenidoInicio != null)
                actualizarContenidoInicio();
        }
    }
}
