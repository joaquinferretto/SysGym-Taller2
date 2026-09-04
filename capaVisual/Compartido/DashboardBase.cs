using System;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Compartido
{
    public interface IDashboardSesion
    {
        bool CambioCuentaSolicitado { get; }
    }

    /// <summary>
    /// Coordina la navegacion de un dashboard sin aportar controles ni herencia visual.
    /// </summary>
    internal sealed class DashboardController
    {
        private readonly Form propietario;
        private readonly Panel panelContenido;
        private Form formularioActual;
        private Control contenidoInicio;
        private Action actualizarContenidoInicio;

        internal DashboardController(Form propietario, Panel panelContenido)
        {
            if (propietario == null) throw new ArgumentNullException("propietario");
            if (panelContenido == null) throw new ArgumentNullException("panelContenido");
            this.propietario = propietario;
            this.panelContenido = panelContenido;
        }

        internal bool CambioCuentaSolicitado { get; private set; }

        internal void CambiarCuenta()
        {
            CambioCuentaSolicitado = true;
            propietario.Close();
        }

        internal void Salir()
        {
            if (MessageBox.Show("Deseas salir de SysGym?", "Salir", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) != DialogResult.Yes) return;
            CambioCuentaSolicitado = false;
            propietario.Close();
        }

        internal void AbrirFormulario(Form formulario)
        {
            if (formulario == null) return;
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
            formulario.FormClosed += delegate
            {
                if (!ReferenceEquals(formularioActual, formulario)) return;
                formularioActual = null;
                MostrarContenidoInicio(true);
            };
            if (contenidoInicio != null) contenidoInicio.Visible = false;
            panelContenido.Controls.Add(formulario);
            formulario.Show();
            formulario.BringToFront();
        }

        internal void EstablecerContenidoInicio(Control control, Action actualizar)
        {
            if (control == null) throw new ArgumentNullException("control");
            contenidoInicio = control;
            actualizarContenidoInicio = actualizar;
            MostrarContenidoInicio(false);
        }

        private void MostrarContenidoInicio(bool actualizar)
        {
            if (contenidoInicio == null || contenidoInicio.IsDisposed) return;
            if (contenidoInicio.Parent != panelContenido) panelContenido.Controls.Add(contenidoInicio);
            contenidoInicio.Visible = true;
            contenidoInicio.BringToFront();
            if (actualizar && actualizarContenidoInicio != null) actualizarContenidoInicio();
        }
    }
}
