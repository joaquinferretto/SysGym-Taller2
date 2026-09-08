using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Compartido
{
    /* Agrupa validaciones de entrada y mensajes compartidos por los formularios estándar. */
    internal static class FormularioVisualHelper
    {
        /* Detecta si el control se está editando en Visual Studio para evitar cargas de ejecución. */
        internal static bool EnModoDisenio(Control control)
        {
            return LicenseManager.UsageMode == LicenseUsageMode.Designtime || (control != null && control.Site != null && control.Site.DesignMode);
        }

        /* Informa el error en la pantalla y en un mensaje para que el usuario pueda corregir la operación. */
        internal static void MostrarError(Label estado, Exception excepcion)
        {
            if (estado != null)
                estado.Text = excepcion.Message;
            MessageBox.Show(excepcion.Message, "SysGym", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /* Actualiza el mensaje de estado después de completar una operación. */
        internal static void MostrarExito(Label estado, string mensaje)
        {
            if (estado != null)
                estado.Text = mensaje;
        }

        /* Comprueba que el campo contenga un entero positivo antes de enviarlo a la lógica. */
        internal static int Entero(TextBox campo, string nombre)
        {
            int resultado;
            if (!int.TryParse(campo.Text.Trim(), out resultado) || resultado <= 0)
                throw new InvalidOperationException("El campo " + nombre + " debe ser un entero positivo.");
            return resultado;
        }

        /* Interpreta coma o punto decimal y valida el rango permitido del campo. */
        internal static decimal DecimalPositivo(TextBox campo, string nombre, bool permitirCero = false)
        {
            decimal resultado;
            var texto = campo.Text.Trim();
            var esValido = decimal.TryParse(texto.Replace(',', '.'), NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out resultado);
            if (!esValido || (permitirCero ? resultado < 0 : resultado <= 0))
                throw new InvalidOperationException("El campo " + nombre + " debe ser numerico.");
            return resultado;
        }

        /* Permite números y un único separador decimal durante la escritura del campo. */
        internal static void ValidarEntradaDecimal(TextBox campo, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar))
                return;
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
        }
    }
}
