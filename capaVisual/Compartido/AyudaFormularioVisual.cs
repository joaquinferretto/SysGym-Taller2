using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Compartido
{
    /* Agrupa validaciones de entrada y mensajes compartidos por los formularios estándar. */
    internal static class AyudaFormularioVisual
    {
        /* Muestra una copia independiente de la foto o del avatar disponible, sin retener el flujo. */
        internal static void MostrarFoto(PictureBox destino, byte[] contenido, string sexo)
        {
            System.Drawing.Image nueva = null;
            if (contenido != null && contenido.Length > 0)
            {
                using (var memoria = new System.IO.MemoryStream(contenido))
                using (var original = System.Drawing.Image.FromStream(memoria, true, true))
                    nueva = new System.Drawing.Bitmap(original);
            }
            else
            {
                string nombreRecurso = sexo == "M" ? "avatarHombre" : sexo == "F" ? "avatarMujer" : "avatarGenerico";
                var recurso = Properties.Resources.ResourceManager.GetObject(nombreRecurso) as System.Drawing.Image;
                if (recurso == null && nombreRecurso != "avatarGenerico")
                    recurso = Properties.Resources.ResourceManager.GetObject("avatarGenerico") as System.Drawing.Image;
                if (recurso != null)
                    nueva = new System.Drawing.Bitmap(recurso);
            }
            var anterior = destino.Image;
            destino.Image = nueva;
            if (anterior != null)
                anterior.Dispose();
        }

        /* Permite elegir una foto y limita la lectura antes de validar sus bytes mediante lógica. */
        internal static byte[] SeleccionarFoto(IWin32Window propietario, string sexo)
        {
            using (var dialogo = new OpenFileDialog())
            {
                dialogo.Filter = "Imagenes|*.png;*.jpg;*.jpeg;*.bmp";
                if (dialogo.ShowDialog(propietario) != DialogResult.OK)
                    return null;
                using (var archivo = System.IO.File.OpenRead(dialogo.FileName))
                {
                    if (archivo.Length > capaLogica.ValidacionesGimnasio.TamanoMaximoFoto)
                        throw new InvalidOperationException("La foto no puede superar los 2 MB.");
                    using (var lector = new System.IO.BinaryReader(archivo))
                    {
                        var contenido = lector.ReadBytes(capaLogica.ValidacionesGimnasio.TamanoMaximoFoto + 1);
                        capaLogica.ValidacionesGimnasio.ValidarFotoYSexo(contenido, sexo);
                        return contenido;
                    }
                }
            }
        }

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
