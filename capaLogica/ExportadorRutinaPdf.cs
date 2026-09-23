using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Drawing;

namespace exxen2._0.capaLogica
{
    /* Genera un documento a partir de datos preparados; no conoce controles ni persistencia. */
    // Dibuja el PDF de la rutina con la librería MigraDoc/PdfSharp: una tabla por día con 3 columnas
    // (ejercicio y observación | series, repeticiones, peso y descanso | imagen). La usa MisSociosFormulario.
    public sealed class ExportadorRutinaPdf
    {
        private static readonly string[] Dias = { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo" };

        public string NombreArchivoSugerido(RutinaParaExportar rutina)
        {
            if (rutina == null) throw new ArgumentNullException(nameof(rutina));  // Se recibió null donde no corresponde.
            var nombre = "Rutina_" + rutina.NombreSocio + "_" + rutina.ApellidoSocio;
            var invalidos = Path.GetInvalidFileNameChars();
            return new string(nombre.Select(c => invalidos.Contains(c) || char.IsWhiteSpace(c) ? '_' : c).ToArray()) + ".pdf";  // Select = transforma cada elemento (como map de Streams).
        }

        // Las imágenes ausentes/ilegibles se informan sin impedir exportar los datos textuales.
        public IReadOnlyList<string> Generar(RutinaParaExportar rutina, string destino)
        {
            if (rutina == null) throw new ArgumentNullException(nameof(rutina));
            if (rutina.Ejercicios == null || rutina.Ejercicios.Count == 0)
                throw new InvalidOperationException("La rutina no contiene ejercicios para exportar.");  // Regla incumplida: corta la operación y el formulario muestra este mensaje.
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
            seccion.PageSetup.Orientation = Orientation.Landscape;
            seccion.PageSetup.TopMargin = Unit.FromCentimeter(1.5);
            seccion.PageSetup.BottomMargin = Unit.FromCentimeter(1.5);
            seccion.PageSetup.LeftMargin = Unit.FromCentimeter(1.5);
            seccion.PageSetup.RightMargin = Unit.FromCentimeter(1.5);
            AgregarEncabezado(seccion, rutina);
            var pie = seccion.Footers.Primary.AddParagraph();
            pie.Format.Alignment = ParagraphAlignment.Right;
            pie.Format.Font.Size = 8;
            pie.AddText("SYSGYM · Página ");
            pie.AddPageField();

            foreach (var dia in rutina.Ejercicios.GroupBy(e => e.DiaSemana).OrderBy(g => OrdenDia(g.Key)))  // Ordena (ORDER BY).
            {
                var tituloDia = seccion.AddParagraph(dia.Key.HasValue && dia.Key >= 1 && dia.Key <= 7  // HasValue: ¿el valor opcional (int?, DateTime?) tiene dato?
                    ? Dias[dia.Key.Value - 1].ToUpperInvariant() : "SIN DÍA ASIGNADO");
                tituloDia.Format.SpaceBefore = Unit.FromPoint(14);
                tituloDia.Format.Font.Size = 13;
                tituloDia.Format.Font.Bold = true;
                tituloDia.Format.Font.Color = Color.FromRgb(79, 70, 229);
                tituloDia.Format.KeepWithNext = true;
                var tabla = seccion.AddTable();
                tabla.Borders.Color = Color.FromRgb(210, 214, 224);
                tabla.Borders.Width = Unit.FromPoint(0.5);
                tabla.Format.Font.Size = 9;
                tabla.Format.SpaceAfter = Unit.FromPoint(0);
                tabla.LeftPadding = Unit.FromPoint(6);
                tabla.RightPadding = Unit.FromPoint(6);
                // Matriz de 3 columnas: ejercicio y observación | carga | imagen.
                tabla.AddColumn(Unit.FromCentimeter(11));
                tabla.AddColumn(Unit.FromCentimeter(6));
                tabla.AddColumn(Unit.FromCentimeter(9.7));
                var encabezado = tabla.AddRow();
                encabezado.HeadingFormat = true;
                encabezado.Shading.Color = Color.FromRgb(239, 240, 255);
                encabezado.Format.Font.Bold = true;
                encabezado.TopPadding = Unit.FromPoint(5);
                encabezado.BottomPadding = Unit.FromPoint(5);
                string[] columnas = { "Ejercicio y observación", "Series / Repeticiones / Peso / Descanso", "Imagen" };
                for (int columna = 0; columna < columnas.Length; columna++)
                    encabezado.Cells[columna].AddParagraph(columnas[columna]);
                foreach (var ejercicio in dia.OrderBy(e => e.Orden))  // Recorre cada elemento (como el for-each de Java).
                {
                    var fila = tabla.AddRow();
                    fila.VerticalAlignment = VerticalAlignment.Center;
                    fila.TopPadding = Unit.FromPoint(6);
                    fila.BottomPadding = Unit.FromPoint(6);
                    var nombre = fila.Cells[0].AddParagraph(ejercicio.Orden + ". " + ejercicio.Nombre);
                    nombre.Format.Font.Bold = true;
                    nombre.Format.Font.Size = 11;
                    var observacion = fila.Cells[0].AddParagraph(string.IsNullOrWhiteSpace(ejercicio.Descripcion)
                        ? "Sin observación" : ejercicio.Descripcion);
                    observacion.Format.Font.Size = 9;
                    observacion.Format.Font.Italic = true;
                    observacion.Format.SpaceBefore = Unit.FromPoint(4);
                    AgregarDato(fila.Cells[1], "Series", Valor(ejercicio.Series));
                    AgregarDato(fila.Cells[1], "Repeticiones", Valor(ejercicio.Repeticiones));
                    AgregarDato(fila.Cells[1], "Peso", ejercicio.Peso.HasValue
                        ? ejercicio.Peso.Value.ToString("0.##", CultureInfo.CurrentCulture) + " kg" : "-");
                    AgregarDato(fila.Cells[1], "Descanso", ejercicio.Descanso + " s");
                    var imagen = LeerPrimeraImagen(ejercicio, avisos);
                    var celdaImagen = fila.Cells[2].AddParagraph();
                    celdaImagen.Format.Alignment = ParagraphAlignment.Center;
                    if (imagen == null) celdaImagen.AddText("Sin imagen");
                    else AgregarImagen(celdaImagen, imagen);
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

        // Encabezado: logo + "SYSGYM"; debajo Socio y Entrenador (solo si tiene) en la misma línea, y Rutina.
        private static void AgregarEncabezado(Section seccion, RutinaParaExportar rutina)
        {
            var titulo = seccion.AddTable();
            titulo.Borders.Visible = false;
            titulo.AddColumn(Unit.FromCentimeter(5.2));
            titulo.AddColumn(Unit.FromCentimeter(21.5));
            var filaTitulo = titulo.AddRow();
            filaTitulo.VerticalAlignment = VerticalAlignment.Center;
            var logo = filaTitulo.Cells[0].AddParagraph().AddImage("base64:" + Convert.ToBase64String(LogoPng()));
            logo.LockAspectRatio = true;
            logo.Width = Unit.FromCentimeter(4.8);
            var marca = filaTitulo.Cells[1].AddParagraph("SYSGYM");
            marca.Format.Font.Size = 28;
            marca.Format.Font.Bold = true;
            marca.Format.Font.Color = Color.FromRgb(79, 70, 229);
            marca.Format.SpaceAfter = Unit.FromPoint(0);

            var datos = seccion.AddTable();
            datos.Borders.Visible = false;
            datos.Format.Font.Size = 12;
            datos.TopPadding = Unit.FromPoint(3);
            datos.BottomPadding = Unit.FromPoint(3);
            datos.AddColumn(Unit.FromCentimeter(13.35));
            datos.AddColumn(Unit.FromCentimeter(13.35));
            var filaSocio = datos.AddRow();
            AgregarDato(filaSocio.Cells[0], "Socio", (rutina.NombreSocio + " " + rutina.ApellidoSocio).Trim());
            if (!string.IsNullOrWhiteSpace(rutina.NombreEntrenador))
            {
                AgregarDato(filaSocio.Cells[1], "Entrenador", rutina.NombreEntrenador);
                filaSocio.Cells[1].Format.Alignment = ParagraphAlignment.Right;
            }
            var filaRutina = datos.AddRow();
            AgregarDato(filaRutina.Cells[0], "Rutina", string.IsNullOrWhiteSpace(rutina.NombreRutina) ? "-" : rutina.NombreRutina);
            // Línea que separa el encabezado de las tablas de ejercicios.
            filaRutina.Borders.Bottom.Visible = true;
            filaRutina.Borders.Bottom.Color = Color.FromRgb(210, 214, 224);
            filaRutina.Borders.Bottom.Width = Unit.FromPoint(1);
        }

        // Logo de la aplicación desde los recursos del proyecto, convertido a PNG para incrustarlo en el PDF.
        private static byte[] LogoPng()
        {
            using (var logo = exxen2._0.Properties.Resources.SysGymLogo)
            using (var salida = new MemoryStream())
            {
                logo.Save(salida, System.Drawing.Imaging.ImageFormat.Png);
                return salida.ToArray();
            }
        }

        private static void AgregarDato(Cell celda, string etiqueta, string valor)
        {
            var parrafo = celda.AddParagraph();
            parrafo.Format.SpaceAfter = Unit.FromPoint(3);
            parrafo.AddFormattedText(etiqueta + ": ", TextFormat.Bold);
            parrafo.AddText(valor);
        }

        private sealed class ImagenPreparada
        {
            public string Contenido;
            public double Ancho;
        }

        private static ImagenPreparada LeerPrimeraImagen(EjercicioParaExportar ejercicio, List<string> avisos)
        {
            var relativa = ejercicio.Imagenes == null ? null : ejercicio.Imagenes.FirstOrDefault();
            if (string.IsNullOrWhiteSpace(relativa)) return null;
            try
            {
                var ruta = AlmacenamientoImagenes.RutaAbsoluta(relativa);
                if (string.IsNullOrWhiteSpace(ruta) || !File.Exists(ruta))
                    throw new FileNotFoundException();
                var contenido = RecortarMargenBlanco(File.ReadAllBytes(ruta));
                using (var stream = new MemoryStream(contenido))
                using (var imagen = XImage.FromStream(stream))
                {
                    double escala = Math.Min(250.0 / imagen.PixelWidth, 150.0 / imagen.PixelHeight);
                    return new ImagenPreparada
                    {
                        Contenido = "base64:" + Convert.ToBase64String(contenido),
                        Ancho = imagen.PixelWidth * escala
                    };
                }
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException ||
                ex is ArgumentException || ex is InvalidOperationException || ex is System.Runtime.InteropServices.ExternalException)
            {
                avisos.Add(ejercicio.Nombre + ": no se pudo incluir la imagen " + relativa);
                return null;
            }
        }

        // Las imágenes se guardan normalizadas en un lienzo cuadrado con relleno blanco; en la celda del PDF
        // ese relleno achica el dibujo. Se recorta solo la copia que va al PDF, sin modificar el archivo.
        private static byte[] RecortarMargenBlanco(byte[] contenido)
        {
            using (var entrada = new MemoryStream(contenido))
            using (var leido = new System.Drawing.Bitmap(entrada))
            using (var original = leido.Clone(new System.Drawing.Rectangle(0, 0, leido.Width, leido.Height),
                System.Drawing.Imaging.PixelFormat.Format32bppArgb))
            {
                bool esJpeg = leido.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg);
                int izquierda = original.Width, arriba = original.Height, derecha = -1, abajo = -1;
                var datos = original.LockBits(new System.Drawing.Rectangle(0, 0, original.Width, original.Height),
                    System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                var pixeles = new byte[datos.Stride * datos.Height];
                System.Runtime.InteropServices.Marshal.Copy(datos.Scan0, pixeles, 0, pixeles.Length);
                original.UnlockBits(datos);
                for (int y = 0; y < original.Height; y++)
                {
                    for (int x = 0; x < original.Width; x++)
                    {
                        int i = y * datos.Stride + x * 4; // orden BGRA
                        if (pixeles[i + 3] < 16 || (pixeles[i] > 240 && pixeles[i + 1] > 240 && pixeles[i + 2] > 240)) continue;
                        if (x < izquierda) izquierda = x;
                        if (x > derecha) derecha = x;
                        if (y < arriba) arriba = y;
                        if (y > abajo) abajo = y;
                    }
                }
                if (derecha < 0) return contenido;
                const int margen = 8;
                var area = System.Drawing.Rectangle.FromLTRB(Math.Max(0, izquierda - margen), Math.Max(0, arriba - margen),
                    Math.Min(original.Width, derecha + margen + 1), Math.Min(original.Height, abajo + margen + 1));
                if (area.Width == original.Width && area.Height == original.Height) return contenido;
                using (var recorte = original.Clone(area, original.PixelFormat))
                using (var salida = new MemoryStream())
                {
                    // Las fotos JPEG siguen en JPEG para no agrandar el PDF; el resto usa PNG para conservar transparencia.
                    recorte.Save(salida, esJpeg ? System.Drawing.Imaging.ImageFormat.Jpeg : System.Drawing.Imaging.ImageFormat.Png);
                    return salida.ToArray();
                }
            }
        }

        private static void AgregarImagen(Paragraph fila, ImagenPreparada preparada)
        {
            var imagen = fila.AddImage(preparada.Contenido);
            imagen.LockAspectRatio = true;
            imagen.Width = Unit.FromPoint(preparada.Ancho);
        }
    }
}
