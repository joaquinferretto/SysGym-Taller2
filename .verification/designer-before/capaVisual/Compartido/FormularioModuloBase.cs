using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Compartido
{
    /// <summary>
    /// Funciones de comportamiento compartido. No crea ni configura controles visuales.
    /// </summary>
    internal static class FormularioVisualHelper
    {
        internal static bool EnModoDisenio(Control control)
        {
            return LicenseManager.UsageMode == LicenseUsageMode.Designtime
                || (control != null && control.Site != null && control.Site.DesignMode);
        }

        internal static void AlCargarEnEjecucion(Form formulario, EventHandler accion)
        {
            if (formulario == null || accion == null) return;
            formulario.Load += delegate(object sender, EventArgs e)
            {
                if (!EnModoDisenio(formulario)) accion(sender, e);
            };
        }

        internal static void MostrarError(Label estado, Exception excepcion)
        {
            if (estado != null) estado.Text = excepcion.Message;
            MessageBox.Show(excepcion.Message, "SysGym", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        internal static void MostrarExito(Label estado, string mensaje)
        {
            if (estado != null) estado.Text = mensaje;
        }

        internal static int Entero(TextBox campo, string nombre)
        {
            int resultado;
            if (!int.TryParse(campo.Text.Trim(), out resultado) || resultado <= 0)
                throw new InvalidOperationException("El campo " + nombre + " debe ser un entero positivo.");
            return resultado;
        }

        internal static decimal DecimalPositivo(TextBox campo, string nombre, bool permitirCero = false)
        {
            decimal resultado;
            var texto = campo.Text.Trim();
            var esValido = decimal.TryParse(texto.Replace(',', '.'),
                NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign,
                CultureInfo.InvariantCulture, out resultado);
            if (!esValido || (permitirCero ? resultado < 0 : resultado <= 0))
                throw new InvalidOperationException("El campo " + nombre + " debe ser numerico.");
            return resultado;
        }

        internal static void ConfigurarEntradaDecimal(TextBox campo)
        {
            campo.KeyPress += delegate(object sender, KeyPressEventArgs e)
            {
                if (char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar)) return;
                if (e.KeyChar == ',' || e.KeyChar == '.')
                {
                    if (campo.Text.Contains(",") || campo.Text.Contains("."))
                    {
                        e.Handled = true;
                        return;
                    }

                    e.KeyChar = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];
                    return;
                }

                e.Handled = true;
            };
        }
    }
}
