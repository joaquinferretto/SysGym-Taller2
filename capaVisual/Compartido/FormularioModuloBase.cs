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

        internal static string TextoObligatorio(TextBox campo, string nombre)
        {
            if (campo == null || string.IsNullOrWhiteSpace(campo.Text))
                throw new InvalidOperationException("El campo " + nombre + " es obligatorio.");
            return campo.Text.Trim();
        }

        internal static string TextoSoloLetrasObligatorio(TextBox campo, string nombre)
        {
            var valor = TextoObligatorio(campo, nombre);
            foreach (var caracter in valor)
            {
                if (!char.IsLetter(caracter) && !char.IsWhiteSpace(caracter))
                    throw new InvalidOperationException("El campo " + nombre + " solo debe contener letras.");
            }

            return valor;
        }

        internal static string DniObligatorio(TextBox campo)
        {
            var valor = TextoObligatorio(campo, "DNI");
            foreach (var caracter in valor)
            {
                if (!char.IsDigit(caracter))
                    throw new InvalidOperationException("El campo DNI solo debe contener numeros.");
            }

            return valor;
        }

        internal static string UsernameObligatorio(TextBox campo)
        {
            var valor = TextoObligatorio(campo, "usuario");
            foreach (var caracter in valor)
            {
                if (!char.IsLetterOrDigit(caracter) && caracter != '_' && caracter != '.' && caracter != '-')
                    throw new InvalidOperationException(
                        "El campo usuario solo debe contener letras, numeros, punto, guion o guion bajo.");
            }

            return valor;
        }

        internal static void ValidarComboSeleccionado(ComboBox combo, string nombre)
        {
            if (combo == null || combo.SelectedIndex < 0)
                throw new InvalidOperationException("Selecciona " + nombre + ".");

            var requiereValor = combo.DataSource != null || !string.IsNullOrWhiteSpace(combo.ValueMember);
            if (requiereValor && combo.SelectedValue == null)
                throw new InvalidOperationException("Selecciona " + nombre + ".");
        }

        internal static void ValidarFechaNoFutura(DateTimePicker campo, string nombre)
        {
            if (campo != null && campo.Checked && campo.Value.Date > DateTime.Today)
                throw new InvalidOperationException("El campo " + nombre + " no puede ser una fecha futura.");
        }

        internal static void ValidarRangoFechas(DateTimePicker desde, DateTimePicker hasta,
            string nombreDesde, string nombreHasta)
        {
            if (desde == null || hasta == null) return;
            if (hasta.Value.Date < desde.Value.Date)
                throw new InvalidOperationException(
                    "El campo " + nombreHasta + " no puede ser anterior a " + nombreDesde + ".");
        }

        internal static int Entero(TextBox campo, string nombre)
        {
            int resultado;
            if (!int.TryParse(campo.Text.Trim(), out resultado) || resultado <= 0)
                throw new InvalidOperationException("El campo " + nombre + " debe ser un entero positivo.");
            return resultado;
        }

        internal static int EnteroOpcional(TextBox campo, string nombre, int valorPredeterminado)
        {
            if (campo == null || string.IsNullOrWhiteSpace(campo.Text)) return valorPredeterminado;
            return Entero(campo, nombre);
        }

        internal static int? EnteroOpcional(TextBox campo, string nombre)
        {
            if (campo == null || string.IsNullOrWhiteSpace(campo.Text)) return null;
            return Entero(campo, nombre);
        }

        internal static int EnteroNoNegativoOpcional(TextBox campo, string nombre, int valorPredeterminado)
        {
            if (campo == null || string.IsNullOrWhiteSpace(campo.Text)) return valorPredeterminado;
            int resultado;
            if (!int.TryParse(campo.Text.Trim(), out resultado) || resultado < 0)
                throw new InvalidOperationException("El campo " + nombre + " debe ser un entero no negativo.");
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

        internal static decimal? DecimalOpcional(TextBox campo, string nombre, bool permitirCero = true)
        {
            if (campo == null || string.IsNullOrWhiteSpace(campo.Text)) return null;
            return DecimalPositivo(campo, nombre, permitirCero);
        }

        internal static void ConfigurarEntradaSoloLetras(TextBox campo)
        {
            if (campo == null) return;
            campo.KeyPress += delegate(object sender, KeyPressEventArgs e)
            {
                if (char.IsControl(e.KeyChar) || char.IsLetter(e.KeyChar) || char.IsWhiteSpace(e.KeyChar))
                    return;

                e.Handled = true;
            };
        }

        internal static void ConfigurarEntradaSoloDigitos(TextBox campo)
        {
            if (campo == null) return;
            campo.KeyPress += delegate(object sender, KeyPressEventArgs e)
            {
                if (char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar)) return;
                e.Handled = true;
            };
        }

        internal static void ConfigurarEntradaEntero(TextBox campo)
        {
            ConfigurarEntradaSoloDigitos(campo);
        }

        internal static void ConfigurarEntradaUsername(TextBox campo)
        {
            if (campo == null) return;
            campo.KeyPress += delegate(object sender, KeyPressEventArgs e)
            {
                if (char.IsControl(e.KeyChar) || char.IsLetterOrDigit(e.KeyChar)
                    || e.KeyChar == '_' || e.KeyChar == '.' || e.KeyChar == '-')
                    return;

                e.Handled = true;
            };
        }

        internal static void ConfigurarEntradaDecimal(TextBox campo)
        {
            if (campo == null) return;
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
