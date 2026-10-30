using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;
using exxen2._0.capaLogica;

namespace exxen2._0.capaVisual.Compartido.Utilidades
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
        /* Abre el manual instalado o el del repositorio cuando se ejecuta desde bin/Debug. */
        internal static void AbrirManual(IWin32Window propietario)
        {
            const string nombreManual = "Manual de usuario SysGym - 2026 monocromo.pdf";
            var carpetaEjecutable = AppDomain.CurrentDomain.BaseDirectory;
            var ruta = Path.Combine(carpetaEjecutable, nombreManual);
            if (!File.Exists(ruta))
                ruta = Path.GetFullPath(Path.Combine(carpetaEjecutable, "..", "..", "Manual", nombreManual));

            if (!File.Exists(ruta))
            {
                MessageBox.Show(propietario, "No se encontró el manual de usuario. Verifique que el archivo PDF esté disponible.",
                    "SysGym", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(ruta) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                RegistrarDetalleError(ex);
                MessageBox.Show(propietario, "No se pudo abrir el manual de usuario. Verifique que tenga un lector de PDF instalado.",
                    "SysGym", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

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
            var mensaje = ObtenerMensajeError(excepcion);
            if (estado != null)
            {
                estado.Text = mensaje;
                if (resaltar)
                {
                    estado.ForeColor = System.Drawing.Color.FromArgb(185, 28, 28);  // Rojo oscuro: Color.Red no se lee bien sobre fondo claro.
                    estado.Visible = true;
                }
            }
            MessageBox.Show(mensaje, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /* Devuelve un mensaje apto para mostrar y registra el detalle completo, incluso errores de EF y SQL. */
        internal static string ObtenerMensajeError(Exception excepcion, string mensajeAlternativo = null)
        {
            if (excepcion == null)
                return mensajeAlternativo ?? "No se pudo completar la operación.";

            RegistrarDetalleError(excepcion);
            if (ContieneSqlException(excepcion))
                return "No se pudo conectar con SQL Server. Verifique que esté instalado y en ejecución.";

            var error = DesenvolverAgregado(excepcion);
            if (error is InvalidOperationException || error is ArgumentException ||
                error is FileNotFoundException || error is DirectoryNotFoundException)
                return error.Message;

            return mensajeAlternativo ?? "No se pudo completar la operación.";
        }

        private static Exception DesenvolverAgregado(Exception excepcion)
        {
            var actual = excepcion;
            while (actual is AggregateException)
            {
                var agregado = ((AggregateException)actual).Flatten();
                if (agregado.InnerExceptions.Count == 0)
                    break;
                actual = agregado.InnerExceptions[0];
            }
            return actual;
        }

        private static bool ContieneSqlException(Exception excepcion)
        {
            if (excepcion == null)
                return false;
            if (excepcion is SqlException)
                return true;
            var agregado = excepcion as AggregateException;
            if (agregado != null && agregado.Flatten().InnerExceptions.Any(ContieneSqlException))
                return true;
            return ContieneSqlException(excepcion.InnerException);
        }

        private static void RegistrarDetalleError(Exception excepcion)
        {
            try
            {
                var raiz = AlmacenamientoImagenes.DirectorioRaiz;
                Directory.CreateDirectory(raiz);
                var ruta = Path.Combine(raiz, "SysGym-error.log");
                var detalle = new StringBuilder()
                    .AppendLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture))
                    .AppendLine(excepcion.ToString());

                foreach (var agregado in ObtenerAgregados(excepcion))
                    foreach (var interna in agregado.Flatten().InnerExceptions)
                        detalle.AppendLine("AggregateException.InnerException:").AppendLine(interna.ToString());

                foreach (var validacion in ObtenerErroresValidacion(excepcion))
                    foreach (var entidad in validacion.EntityValidationErrors)
                        foreach (var error in entidad.ValidationErrors)
                            detalle.Append("EntityValidation: ").Append(entidad.Entry.Entity.GetType().Name)
                                .Append('.').Append(error.PropertyName).Append(": ").AppendLine(error.ErrorMessage);

                foreach (var sql in ObtenerSqlExceptions(excepcion))
                    foreach (SqlError error in sql.Errors)
                        detalle.Append("SqlException ").Append(error.Number).Append(" (línea ").Append(error.LineNumber)
                            .Append("): ").AppendLine(error.Message);

                File.AppendAllText(ruta, detalle.AppendLine().ToString(), Encoding.UTF8);
            }
            catch
            {
                // Un fallo de logging no debe impedir que la interfaz presente el error original.
            }
        }

        private static System.Collections.Generic.IEnumerable<AggregateException> ObtenerAgregados(Exception excepcion)
        {
            var agregado = excepcion as AggregateException;
            if (agregado != null)
            {
                yield return agregado;
                foreach (var interna in agregado.InnerExceptions)
                    foreach (var anidado in ObtenerAgregados(interna))
                        yield return anidado;
            }
            else if (excepcion.InnerException != null)
            {
                foreach (var anidado in ObtenerAgregados(excepcion.InnerException))
                    yield return anidado;
            }
        }

        private static System.Collections.Generic.IEnumerable<SqlException> ObtenerSqlExceptions(Exception excepcion)
        {
            var sql = excepcion as SqlException;
            if (sql != null)
                yield return sql;
            var agregado = excepcion as AggregateException;
            if (agregado != null)
                foreach (var interna in agregado.InnerExceptions)
                    foreach (var anidado in ObtenerSqlExceptions(interna))
                        yield return anidado;
            else if (excepcion.InnerException != null)
                foreach (var anidado in ObtenerSqlExceptions(excepcion.InnerException))
                    yield return anidado;
        }

        private static System.Collections.Generic.IEnumerable<System.Data.Entity.Validation.DbEntityValidationException> ObtenerErroresValidacion(Exception excepcion)
        {
            var validacion = excepcion as System.Data.Entity.Validation.DbEntityValidationException;
            if (validacion != null)
                yield return validacion;
            var agregado = excepcion as AggregateException;
            if (agregado != null)
                foreach (var interna in agregado.InnerExceptions)
                    foreach (var anidado in ObtenerErroresValidacion(interna))
                        yield return anidado;
            else if (excepcion.InnerException != null)
                foreach (var anidado in ObtenerErroresValidacion(excepcion.InnerException))
                    yield return anidado;
        }

        /* Actualiza el mensaje de estado después de completar una operación. */
        internal static void MostrarExito(Label estado, string mensaje, bool resaltar = false)
        {
            if (estado != null)
            {
                estado.Text = mensaje;
                if (resaltar)
                {
                    estado.ForeColor = System.Drawing.Color.FromArgb(22, 101, 52);  // Verde oscuro legible sobre fondo claro.
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
            return ValidarConError(errores, campo, delegate { ValidacionesGimnasio.ValidarDni(campo.Text); });
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
