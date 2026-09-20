using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using PdfSharp.Drawing;

namespace exxen2._0.capaLogica
{
    /* Genera un documento a partir de datos preparados; no conoce controles ni persistencia. */
    public sealed class ExportadorRutinaPdf
    {
        private static readonly string[] Dias = { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo" };

        public string NombreArchivoSugerido(RutinaParaExportar rutina)
        {
            if (rutina == null) throw new ArgumentNullException(nameof(rutina));
            var nombre = "Rutina_" + rutina.NombreSocio + "_" + rutina.ApellidoSocio;
            var invalidos = Path.GetInvalidFileNameChars();
            return new string(nombre.Select(c => invalidos.Contains(c) || char.IsWhiteSpace(c) ? '_' : c).ToArray()) + ".pdf";
        }

        // Las imágenes ausentes/ilegibles se informan sin impedir exportar los datos textuales.
        public IReadOnlyList<string> Generar(RutinaParaExportar rutina, string destino)
        {
            if (rutina == null) throw new ArgumentNullException(nameof(rutina));
            if (rutina.Ejercicios == null || rutina.Ejercicios.Count == 0)
                throw new InvalidOperationException("La rutina no contiene ejercicios para exportar.");
            if (string.IsNullOrWhiteSpace(destino)) throw new ArgumentException("Seleccione el archivo de destino.", nameof(destino));
            var avisos = new List<string>();
            var documento = new Document();
            documento.Info.Title = "Rutina - " + rutina.NombreSocio + " " + rutina.ApellidoSocio;
            documento.Info.Author = "SYSGYM";
            var normal = documento.Styles[StyleNames.Normal];
            normal.Font.Name = "Arial";
            normal.Font.Size = 10;
            normal.ParagraphFormat.SpaceAfter = Unit.FromPoint(6);
            var seccion = documento.AddSection();
            seccion.PageSetup.PageFormat = PageFormat.A4;
            seccion.PageSetup.TopMargin = Unit.FromCentimeter(1.8);
            seccion.PageSetup.BottomMargin = Unit.FromCentimeter(1.8);
            seccion.PageSetup.LeftMargin = Unit.FromCentimeter(2);
            seccion.PageSetup.RightMargin = Unit.FromCentimeter(2);
            var marca = seccion.AddParagraph("SYSGYM");
            marca.Format.Alignment = ParagraphAlignment.Center;
            marca.Format.Font.Size = 22;
            marca.Format.Font.Bold = true;
            marca.Format.Font.Color = Color.FromRgb(79, 70, 229);
            marca.Format.KeepWithNext = true;
            var socio = seccion.AddParagraph((rutina.NombreSocio + " " + rutina.ApellidoSocio).Trim());
            socio.Format.Alignment = ParagraphAlignment.Center;
            socio.Format.Font.Size = 16;
            socio.Format.KeepWithNext = true;
            if (!string.IsNullOrWhiteSpace(rutina.NombreRutina))
            {
                var nombre = seccion.AddParagraph(rutina.NombreRutina);
                nombre.Format.Alignment = ParagraphAlignment.Center;
                nombre.Format.KeepWithNext = true;
            }
            var pie = seccion.Footers.Primary.AddParagraph();
            pie.Format.Alignment = ParagraphAlignment.Right;
            pie.Format.Font.Size = 8;
            pie.AddText("SYSGYM · Página ");
            pie.AddPageField();

            foreach (var dia in rutina.Ejercicios.GroupBy(e => e.DiaSemana).OrderBy(g => OrdenDia(g.Key)))
            {
                var tituloDia = seccion.AddParagraph(dia.Key.HasValue && dia.Key >= 1 && dia.Key <= 7
                    ? Dias[dia.Key.Value - 1].ToUpperInvariant() : "SIN DÍA ASIGNADO");
                tituloDia.Format.SpaceBefore = Unit.FromPoint(14);
                tituloDia.Format.Font.Size = 13;
                tituloDia.Format.Font.Bold = true;
                tituloDia.Format.Font.Color = Color.FromRgb(79, 70, 229);
                tituloDia.Format.KeepWithNext = true;
                foreach (var ejercicio in dia.OrderBy(e => e.Orden))
                {
                    var titulo = seccion.AddParagraph(ejercicio.Orden + ". " + ejercicio.Nombre);
                    titulo.Format.Font.Size = 12;
                    titulo.Format.Font.Bold = true;
                    titulo.Format.SpaceBefore = Unit.FromPoint(8);
                    titulo.Format.KeepWithNext = true;
                    var parametros = seccion.AddParagraph("Series: " + Valor(ejercicio.Series) +
                        "    Repeticiones: " + Valor(ejercicio.Repeticiones) + "    Peso: " +
                        (ejercicio.Peso.HasValue ? ejercicio.Peso.Value.ToString("0.##", CultureInfo.CurrentCulture) + " kg" : "-") +
                        "    Descanso: " + ejercicio.Descanso + " s");
                    bool tieneDescripcion = !string.IsNullOrWhiteSpace(ejercicio.Descripcion);
                    var imagenes = LeerImagenes(ejercicio, avisos);
                    parametros.Format.KeepWithNext = tieneDescripcion || imagenes.Count > 0;
                    if (tieneDescripcion)
                    {
                        var descripcion = seccion.AddParagraph(ejercicio.Descripcion);
                        descripcion.Format.KeepTogether = false;
                        descripcion.Format.WidowControl = true;
                        // Una descripción extensa puede paginar; las breves acompañan la primera imagen.
                        descripcion.Format.KeepWithNext = imagenes.Count > 0 && ejercicio.Descripcion.Length < 500;
                    }
                    for (int i = 0; i < imagenes.Count; i += 2)
                    {
                        var fila = seccion.AddParagraph();
                        fila.Format.Alignment = ParagraphAlignment.Center;
                        fila.Format.KeepTogether = true;
                        AgregarImagen(fila, imagenes[i]);
                        if (i + 1 < imagenes.Count)
                        {
                            fila.AddText("   ");
                            AgregarImagen(fila, imagenes[i + 1]);
                        }
                    }
                }
            }

            // Renderiza antes de reemplazar el destino; un error no trunca un PDF existente.
            var ruta = Path.GetFullPath(destino);
            var temporal = Path.Combine(Path.GetDirectoryName(ruta), ".sysgym-" + Guid.NewGuid().ToString("N") + ".tmp");
            try
            {
                var renderer = new PdfDocumentRenderer { Document = documento };
                renderer.RenderDocument();
                using (var pdf = renderer.PdfDocument) pdf.Save(temporal);
                if (File.Exists(ruta)) File.Replace(temporal, ruta, null);
                else File.Move(temporal, ruta);
            }
            finally
            {
                if (File.Exists(temporal)) File.Delete(temporal);
            }
            return avisos.AsReadOnly();
        }

        private static int OrdenDia(int? dia) { return dia >= 1 && dia <= 7 ? dia.Value : 8; }
        private static string Valor(int? valor) { return valor.HasValue ? valor.Value.ToString() : "-"; }

        private sealed class ImagenPreparada
        {
            public string Contenido;
            public double Ancho;
        }

        private static List<ImagenPreparada> LeerImagenes(EjercicioParaExportar ejercicio, List<string> avisos)
        {
            var resultado = new List<ImagenPreparada>();
            foreach (var relativa in ejercicio.Imagenes ?? new List<string>())
            {
                try
                {
                    var ruta = AlmacenamientoImagenes.RutaAbsoluta(relativa);
                    if (string.IsNullOrWhiteSpace(ruta) || !File.Exists(ruta))
                        throw new FileNotFoundException();
                    var contenido = File.ReadAllBytes(ruta);
                    using (var stream = new MemoryStream(contenido))
                    using (var imagen = XImage.FromStream(stream))
                    {
                        double escala = Math.Min(200.0 / imagen.PixelWidth, 130.0 / imagen.PixelHeight);
                        resultado.Add(new ImagenPreparada
                        {
                            Contenido = "base64:" + Convert.ToBase64String(contenido),
                            Ancho = imagen.PixelWidth * escala
                        });
                    }
                }
                catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException ||
                    ex is ArgumentException || ex is InvalidOperationException || ex is System.Runtime.InteropServices.ExternalException)
                {
                    avisos.Add(ejercicio.Nombre + ": no se pudo incluir la imagen " + relativa);
                }
            }
            return resultado;
        }

        private static void AgregarImagen(Paragraph fila, ImagenPreparada preparada)
        {
            var imagen = fila.AddImage(preparada.Contenido);
            imagen.LockAspectRatio = true;
            imagen.Width = Unit.FromPoint(preparada.Ancho);
        }
    }
}
