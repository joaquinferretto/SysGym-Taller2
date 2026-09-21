using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace exxen2._0.capaLogica.Utilidades
{
    /* Conserva el archivo externo para guardar y una copia normalizada solo para vista previa. */
    internal sealed class ImagenSeleccionada
    {
        public byte[] Contenido { get; set; }
        public byte[] VistaPrevia { get; set; }
    }

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
                nueva = ObtenerAvatar(sexo);
            }
            ReemplazarImagen(destino, nueva);
        }

        /* Muestra una ruta administrada o el avatar correspondiente cuando el archivo no existe. */
        internal static void MostrarFotoRuta(PictureBox destino, string rutaRelativa, string sexo)
        {
            var nueva = CargarImagenDesdeRuta(rutaRelativa) ?? ObtenerAvatar(sexo);
            ReemplazarImagen(destino, nueva);
        }

        /* Carga una copia independiente para que el archivo no quede bloqueado por el PictureBox. */
        internal static System.Drawing.Image CargarImagenDesdeRuta(string rutaRelativa)
        {
            try
            {
                var ruta = capaLogica.AlmacenamientoImagenes.RutaAbsoluta(rutaRelativa);
                if (string.IsNullOrWhiteSpace(ruta) || !File.Exists(ruta))
                    return null;
                using (var archivo = File.OpenRead(ruta))
                using (var original = System.Drawing.Image.FromStream(archivo, true, true))
                    return new System.Drawing.Bitmap(original);
            }
            catch
            {
                return null;
            }
        }

        /* Selecciona bytes externos; la firma real y la vista segura dependen del procesador central. */
        internal static ImagenSeleccionada SeleccionarImagen(IWin32Window propietario, string sexo = null)
        {
            using (var dialogo = new OpenFileDialog())
            {
                dialogo.Filter = "Imágenes|*.jpg;*.jpeg;*.png;*.bmp;*.gif;*.tif;*.tiff;*.webp|Todos los archivos|*.*";
                if (dialogo.ShowDialog(propietario) != DialogResult.OK)
                    return null;
                using (var archivo = System.IO.File.OpenRead(dialogo.FileName))
                {
                    if (archivo.Length > capaLogica.ProcesadorImagenes.TamanoMaximoBytes)
                        throw new InvalidOperationException("La imagen supera el tamaño máximo permitido de 10 MB.");
                    using (var lector = new System.IO.BinaryReader(archivo))
                    {
                        var contenido = lector.ReadBytes(capaLogica.ProcesadorImagenes.TamanoMaximoBytes + 1);
                        var normalizada = capaLogica.ProcesadorImagenes.Procesar(contenido);
                        return new ImagenSeleccionada
                        {
                            Contenido = contenido,
                            VistaPrevia = normalizada.Contenido
                        };
                    }
                }
            }
        }

        private static System.Drawing.Image ObtenerAvatar(string sexo)
        {
            var nombreRecurso = sexo == "M" ? "socio_hombre_default" : sexo == "F" ? "socio_mujer_default" : "socio_hombre_default";
            var recurso = Properties.Resources.ResourceManager.GetObject(nombreRecurso) as System.Drawing.Image;
            if (recurso == null)
                recurso = Properties.Resources.ResourceManager.GetObject("socio_hombre_default") as System.Drawing.Image;
            return recurso == null ? null : new System.Drawing.Bitmap(recurso);
        }

        private static void ReemplazarImagen(PictureBox destino, System.Drawing.Image nueva)
        {
            var anterior = destino.Image;
            destino.Image = nueva;
            if (anterior != null)
                anterior.Dispose();
        }

        /* Detecta si el control se está editando en Visual Studio para evitar cargas de ejecución. */
        internal static bool EnModoDisenio(Control control)
        {
            return LicenseManager.UsageMode == LicenseUsageMode.Designtime || (control != null && control.Site != null && control.Site.DesignMode);
        }

        /* Informa el error en la pantalla y en un mensaje para que el usuario pueda corregir la operación. */
        internal static void MostrarError(Label estado, Exception excepcion, bool resaltar = false)
        {
            var mensaje = excepcion is InvalidOperationException || excepcion is ArgumentException
                ? excepcion.Message
                : "No se pudo completar la operación.";
            if (estado != null)
            {
                estado.Text = mensaje;
                if (resaltar)
                {
                    estado.ForeColor = System.Drawing.Color.Red;
                    estado.Visible = true;
                }
            }
            MessageBox.Show(mensaje, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /* Actualiza el mensaje de estado después de completar una operación. */
        internal static void MostrarExito(Label estado, string mensaje, bool resaltar = false)
        {
            if (estado != null)
            {
                estado.Text = mensaje;
                if (resaltar)
                {
                    estado.ForeColor = System.Drawing.Color.Green;
                    estado.Visible = true;
                }
            }
        }

        /* Comprueba que el campo contenga un entero positivo antes de enviarlo a la lógica. */
        internal static int Entero(TextBox campo, string nombre)
        {
            int resultado;
            if (!int.TryParse(campo.Text.Trim(), out resultado) || resultado <= 0)
                throw new InvalidOperationException("El campo " + nombre + " debe ser un entero positivo.");
            return resultado;
        }

        /* Comprueba que un combo enlazado tenga una opción válida antes de usar su clave. */
        internal static void ValidarComboSeleccionado(ComboBox combo, string nombre)
        {
            if (combo == null || combo.SelectedIndex < 0)
                throw new InvalidOperationException("Selecciona " + nombre + ".");

            var requiereValor = combo.DataSource != null || !string.IsNullOrWhiteSpace(combo.ValueMember);
            if (requiereValor && combo.SelectedValue == null)
                throw new InvalidOperationException("Selecciona " + nombre + ".");
        }

        /* Comprueba que la fecha final no sea anterior a la fecha inicial. */
        internal static void ValidarRangoFechas(DateTimePicker desde, DateTimePicker hasta, string nombreDesde, string nombreHasta)
        {
            if (desde != null && hasta != null && hasta.Value.Date < desde.Value.Date)
                throw new InvalidOperationException("El campo " + nombreHasta + " no puede ser anterior a " + nombreDesde + ".");
        }

        /* Interpreta coma o punto decimal y valida el rango permitido del campo. */
        internal static decimal DecimalPositivo(TextBox campo, string nombre, bool permitirCero = false)
        {
            decimal resultado;
            var texto = campo.Text.Trim();
            var esValido = decimal.TryParse(texto.Replace(',', '.'), NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out resultado);
            if (!esValido)
                throw new InvalidOperationException("El campo " + nombre + " debe ser numérico.");
            if (permitirCero && resultado < 0)
                throw new InvalidOperationException("El campo " + nombre + " no puede ser negativo.");
            if (!permitirCero && resultado <= 0)
                throw new InvalidOperationException("El campo " + nombre + " debe ser mayor que cero.");
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

        /* Permite escribir letras y los separadores habituales de nombres sin reemplazar la validación final. */
        internal static void ValidarEntradaNombre(KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || char.IsLetter(e.KeyChar) || e.KeyChar == ' ' || e.KeyChar == '\'' || e.KeyChar == '-')
                return;
            e.Handled = true;
        }

        /* Permite escribir únicamente dígitos en campos de DNI y conserva las teclas de control. */
        internal static void ValidarEntradaDni(KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || (e.KeyChar >= '0' && e.KeyChar <= '9'))
                return;
            e.Handled = true;
        }

        /* Permite escribir únicamente dígitos en cantidades enteras. */
        internal static void ValidarEntradaEntero(KeyPressEventArgs e)
        {
            ValidarEntradaDni(e);
        }

        /* Permite los caracteres admitidos por la regla compartida de nombres de usuario. */
        internal static void ValidarEntradaNombreUsuario(KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || char.IsLetterOrDigit(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == '_' || e.KeyChar == '-')
                return;
            e.Handled = true;
        }

        /* Ejecuta una regla y asocia su mensaje al control sin impedir cambiar el foco. */
        internal static bool ValidarConError(ErrorProvider errores, Control control, Action validacion)
        {
            if (errores == null || control == null)
                return true;
            var cuadroTexto = control as TextBox;
            if (!control.Enabled || !control.Visible || (cuadroTexto != null && cuadroTexto.ReadOnly))
            {
                errores.SetError(control, string.Empty);
                return true;
            }
            try
            {
                validacion();
                errores.SetError(control, string.Empty);
                return true;
            }
            catch (InvalidOperationException ex)
            {
                errores.SetError(control, ex.Message);
                return false;
            }
            catch (ArgumentException ex)
            {
                errores.SetError(control, ex.Message);
                return false;
            }
        }

        internal static bool ValidarRequerido(ErrorProvider errores, TextBox campo, string mensaje)
        {
            return ValidarConError(errores, campo, delegate
            {
                if (string.IsNullOrWhiteSpace(campo.Text))
                    throw new InvalidOperationException(mensaje);
            });
        }

        internal static bool ValidarNombre(ErrorProvider errores, TextBox campo, string nombreCampo)
        {
            return ValidarConError(errores, campo, delegate { ValidacionesGimnasio.ValidarNombre(campo.Text.Trim(), nombreCampo); });
        }

        internal static bool ValidarDni(ErrorProvider errores, TextBox campo)
        {
            return ValidarConError(errores, campo, delegate { ValidacionesGimnasio.ValidarDni(campo.Text.Trim()); });
        }

        internal static bool ValidarNombreUsuario(ErrorProvider errores, TextBox campo)
        {
            return ValidarConError(errores, campo, delegate { ValidacionesGimnasio.ValidarNombreUsuario(campo.Text.Trim()); });
        }

        internal static bool ValidarDecimal(ErrorProvider errores, TextBox campo, string nombreCampo, bool obligatorio, bool permitirCero)
        {
            return ValidarConError(errores, campo, delegate
            {
                if (string.IsNullOrWhiteSpace(campo.Text))
                {
                    if (obligatorio)
                        throw new InvalidOperationException("El campo " + nombreCampo + " es obligatorio.");
                    return;
                }
                DecimalPositivo(campo, nombreCampo, permitirCero);
            });
        }

        internal static bool ValidarEntero(ErrorProvider errores, TextBox campo, string nombreCampo, bool obligatorio, bool permitirCero)
        {
            return ValidarConError(errores, campo, delegate
            {
                if (string.IsNullOrWhiteSpace(campo.Text))
                {
                    if (obligatorio)
                        throw new InvalidOperationException("El campo " + nombreCampo + " es obligatorio.");
                    return;
                }
                int valor;
                if (!int.TryParse(campo.Text.Trim(), out valor))
                    throw new InvalidOperationException("El campo " + nombreCampo + " debe ser un número entero.");
                if (permitirCero ? valor < 0 : valor <= 0)
                    throw new InvalidOperationException(permitirCero
                        ? "El campo " + nombreCampo + " no puede ser negativo."
                        : "El campo " + nombreCampo + " debe ser mayor que cero.");
            });
        }

        internal static bool ValidarCombo(ErrorProvider errores, ComboBox combo, string mensaje)
        {
            return ValidarConError(errores, combo, delegate
            {
                var requiereValor = combo.DataSource != null || !string.IsNullOrWhiteSpace(combo.ValueMember);
                if (combo.SelectedIndex < 0 || (requiereValor && combo.SelectedValue == null))
                    throw new InvalidOperationException(mensaje);
            });
        }

        internal static void EnfocarPrimerError(ErrorProvider errores, params Control[] controles)
        {
            foreach (var control in controles)
            {
                if (control != null && !string.IsNullOrEmpty(errores.GetError(control)) && control.CanFocus)
                {
                    control.Focus();
                    return;
                }
            }
        }
    }
}
