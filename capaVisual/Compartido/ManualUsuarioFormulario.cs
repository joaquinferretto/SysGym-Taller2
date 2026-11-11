using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using exxen2._0.capaLogica.Manuales;
using exxen2._0.capaVisual.Compartido.Utilidades;

namespace exxen2._0.capaVisual.Compartido
{
    [DesignerCategory("Form")]
    public sealed class ManualUsuarioFormulario : Form
    {
        private readonly ManualUsuarioServicio manual;
        private readonly TextBox buscar = new TextBox { Name = "buscar", Dock = DockStyle.Fill };
        private readonly ListBox indice = new ListBox { Name = "indice", Dock = DockStyle.Fill, IntegralHeight = false, HorizontalScrollbar = true };
        private readonly RichTextBox lectura = new RichTextBox { Name = "lectura", Dock = DockStyle.Fill, ReadOnly = true,
            BorderStyle = BorderStyle.None, BackColor = Color.White, DetectUrls = false, WordWrap = true, ScrollBars = RichTextBoxScrollBars.Vertical };
        private readonly Label estado = new Label { Name = "estado", Dock = DockStyle.Fill, AutoEllipsis = true, TextAlign = ContentAlignment.MiddleLeft };
        private readonly Button anterior = Boton("anterior", "Anterior");
        private readonly Button siguiente = Boton("siguiente", "Siguiente");
        private readonly Button abrirPdf = Boton("abrirPdf", "Abrir PDF");
        private readonly Button exportarPdf = Boton("exportarPdf", "Exportar PDF");

        public ManualUsuarioFormulario() : this("Administrador") { }

        public ManualUsuarioFormulario(string rol)
        {
            manual = new ManualUsuarioServicio(rol);
            Name = "ManualUsuarioFormulario";
            Text = "Manual de usuario - " + manual.Rol;
            Font = new Font("Segoe UI", 10F);
            BackColor = Color.FromArgb(247, 249, 252);
            ClientSize = new Size(1040, 720);
            AutoScaleMode = AutoScaleMode.Font;
            var layout = new TableLayoutPanel { Name = "estructura", Dock = DockStyle.Fill, Padding = new Padding(16), ColumnCount = 1, RowCount = 5 };
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 54));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 44));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34));
            layout.Controls.Add(new Label { Name = "titulo", Text = "Manual de usuario · " + manual.Rol,
                Font = new Font("Segoe UI", 16F, FontStyle.Bold), Dock = DockStyle.Fill, AutoEllipsis = true }, 0, 0);
            var filtro = new TableLayoutPanel { Name = "filtro", Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1 };
            filtro.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110));
            filtro.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            filtro.Controls.Add(new Label { Text = "Buscar tema:", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft }, 0, 0);
            filtro.Controls.Add(buscar, 1, 0);
            buscar.AccessibleName = "Buscar un tema del manual";
            layout.Controls.Add(filtro, 0, 1);
            var contenido = new TableLayoutPanel { Name = "contenido", Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1 };
            contenido.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34));
            contenido.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 66));
            contenido.Controls.Add(indice, 0, 0);
            var pagina = new Panel { Name = "pagina", Dock = DockStyle.Fill, BackColor = Color.White, Padding = new Padding(18) };
            pagina.Controls.Add(lectura);
            contenido.Controls.Add(pagina, 1, 0);
            layout.Controls.Add(contenido, 0, 2);
            var acciones = new FlowLayoutPanel { Name = "acciones", Dock = DockStyle.Fill, AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink, WrapContents = true, Padding = new Padding(0, 8, 0, 0) };
            acciones.Controls.AddRange(new Control[] { anterior, siguiente, abrirPdf, exportarPdf });
            layout.Controls.Add(acciones, 0, 3);
            layout.Controls.Add(estado, 0, 4);
            Controls.Add(layout);
            indice.AccessibleName = "Índice de temas del manual";
            lectura.AccessibleName = "Instrucciones del tema seleccionado";
            indice.SelectedIndexChanged += delegate { MostrarTema(); };
            buscar.TextChanged += delegate { Filtrar(); };
            anterior.Click += delegate { if (indice.SelectedIndex > 0) indice.SelectedIndex--; };
            siguiente.Click += delegate { if (indice.SelectedIndex < indice.Items.Count - 1) indice.SelectedIndex++; };
            abrirPdf.Click += async delegate { await GenerarPdf(true); };
            exportarPdf.Click += async delegate { await GenerarPdf(false); };
            Filtrar();
        }

        private static Button Boton(string nombre, string texto)
        {
            return new Button { Name = nombre, Text = texto, AutoSize = true, MinimumSize = new Size(125, 40),
                Margin = new Padding(0, 0, 8, 0), BackColor = Color.FromArgb(87, 70, 139), ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat, UseVisualStyleBackColor = false };
        }

        private void Filtrar()
        {
            var previo = indice.SelectedItem as TemaManual;
            var texto = buscar.Text.Trim();
            indice.BeginUpdate();
            indice.Items.Clear();
            indice.Items.AddRange(manual.Temas.Where(t => CultureInfo.GetCultureInfo("es-AR").CompareInfo.IndexOf(
                t.Grupo + " " + t.Titulo + " " + t.Texto, texto, CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace) >= 0).Cast<object>().ToArray());
            if (previo != null && indice.Items.Contains(previo)) indice.SelectedItem = previo;
            else if (indice.Items.Count > 0) indice.SelectedIndex = 0;
            indice.EndUpdate();
            MostrarTema();
        }

        private void MostrarTema()
        {
            var tema = indice.SelectedItem as TemaManual;
            lectura.Text = tema == null ? "No hay temas con esa búsqueda. Borrá el texto para volver al índice completo."
                : tema.Grupo.ToUpperInvariant() + Environment.NewLine + Environment.NewLine + tema.Titulo + Environment.NewLine + Environment.NewLine + tema.Texto;
            lectura.Select(0, 0);
            lectura.ScrollToCaret();
            anterior.Enabled = indice.SelectedIndex > 0;
            siguiente.Enabled = indice.SelectedIndex >= 0 && indice.SelectedIndex < indice.Items.Count - 1;
            estado.Text = indice.Items.Count + " tema(s) · Manual de " + manual.Rol + " · Edición " + ManualUsuarioServicio.Version;
            estado.ForeColor = Color.FromArgb(51, 65, 85);
        }

        private async Task GenerarPdf(bool abrir)
        {
            string ruta;
            if (abrir)
            {
                var carpeta = Path.Combine(Path.GetTempPath(), "SysGym", "Manuales", Guid.NewGuid().ToString("N"));
                try { Directory.CreateDirectory(carpeta); }
                catch (Exception ex) { AyudaFormularioVisual.MostrarError(estado, ex); return; }
                ruta = Path.Combine(carpeta, manual.NombreArchivo);
            }
            else
            {
                using (var dialogo = new SaveFileDialog { Title = "Exportar manual de " + manual.Rol, Filter = "PDF (*.pdf)|*.pdf",
                    FileName = manual.NombreArchivo, DefaultExt = "pdf", AddExtension = true, OverwritePrompt = true })
                {
                    if (dialogo.ShowDialog(this) != DialogResult.OK) return;
                    ruta = dialogo.FileName;
                }
            }
            abrirPdf.Enabled = exportarPdf.Enabled = false;
            estado.Text = "Preparando el manual PDF…";
            try
            {
                await Task.Run(() => manual.ExportarPdf(ruta));
                if (IsDisposed) return;
                estado.Text = "Manual guardado en " + ruta;
                if (abrir)
                {
                    try { Process.Start(new ProcessStartInfo(ruta) { UseShellExecute = true }); }
                    catch (Exception ex)
                    {
                        Trace.TraceError(ex.ToString());
                        estado.Text = "El PDF se guardó en " + ruta + ". Abrilo con un lector PDF o usá Exportar PDF.";
                    }
                }
            }
            catch (Exception ex) { if (!IsDisposed) AyudaFormularioVisual.MostrarError(estado, ex); }
            finally { if (!IsDisposed) abrirPdf.Enabled = exportarPdf.Enabled = true; }
        }
    }
}
