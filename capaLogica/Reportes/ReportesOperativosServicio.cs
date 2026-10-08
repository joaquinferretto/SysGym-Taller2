using System;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using exxen2._0.capaDatos.Repositorios;
using exxen2._0.capaLogica.Auditoria;

namespace exxen2._0.capaLogica.Reportes
{
    public sealed class ReportesOperativosServicio
    {
        private readonly AuditoriaLogica auditoria;
        public ReportesOperativosServicio(int idUsuarioAutenticado) { auditoria = new AuditoriaLogica(idUsuarioAutenticado); }
        public string NombreSugerido(ResultadoReporteOperativo reporte)
        {
            if (reporte == null) throw new ArgumentNullException("reporte");
            var nombre = "Reporte_" + reporte.Titulo + "_" + DateTime.Today.ToString("yyyy-MM-dd") + ".pdf";
            return new string(nombre.Select(c => Path.GetInvalidFileNameChars().Contains(c) || char.IsWhiteSpace(c) ? '_' : c).ToArray());
        }
        public void Generar(ResultadoReporteOperativo reporte, string destino)
        {
            if (reporte == null || string.IsNullOrWhiteSpace(reporte.Titulo) || reporte.Filas == null || reporte.Filas.Count == 0 || reporte.Columnas == null || reporte.Columnas.Length < 1 || reporte.Columnas.Length > 7 ||
                reporte.Filas.Any(f => f.Valores == null || f.Valores.Length != reporte.Columnas.Length) || !Enum.IsDefined(typeof(TipoReporteOperativo), reporte.Tipo))
                throw new ArgumentException("Consultá un reporte con resultados antes de exportar.");
            if (string.IsNullOrWhiteSpace(destino)) throw new ArgumentException("Seleccioná el archivo de destino.");
            var ruta = Path.GetFullPath(destino);
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                var usuario = auditoria.ObtenerUsuario(datos, true);
                var documento = new Document();
                documento.Info.Title = reporte.Titulo;
                documento.Info.Author = usuario.Nombre + " " + usuario.Apellido;
                documento.Styles[StyleNames.Normal].Font.Name = "Arial";
                documento.Styles[StyleNames.Normal].Font.Size = 9;
                documento.Styles[StyleNames.Normal].ParagraphFormat.SpaceAfter = Unit.FromPoint(5);
                var seccion = documento.AddSection();
                seccion.PageSetup.PageFormat = PageFormat.A4;
                seccion.PageSetup.Orientation = Orientation.Landscape;
                seccion.PageSetup.TopMargin = Unit.FromCentimeter(1.5);
                seccion.PageSetup.BottomMargin = Unit.FromCentimeter(1.5);
                seccion.PageSetup.LeftMargin = Unit.FromCentimeter(1.5);
                seccion.PageSetup.RightMargin = Unit.FromCentimeter(1.5);
                using (var memoria = new MemoryStream())
                {
                    Properties.Resources.SysGymLogo.Save(memoria, ImageFormat.Png);
                    var logo = seccion.AddParagraph().AddImage("base64:" + Convert.ToBase64String(memoria.ToArray()));
                    logo.Width = Unit.FromCentimeter(3.2); logo.LockAspectRatio = true;
                }
                var marca = seccion.AddParagraph("SysGym"); marca.Format.Font.Size = 18; marca.Format.Font.Bold = true;
                var titulo = seccion.AddParagraph("REPORTE: " + reporte.Titulo.ToUpperInvariant()); titulo.Format.Font.Size = 14; titulo.Format.Font.Bold = true;
                seccion.AddParagraph("Generado: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm", CultureInfo.GetCultureInfo("es-AR")));
                seccion.AddParagraph("Generado por: " + usuario.Nombre + " " + usuario.Apellido + " - " + usuario.Rol.Descripcion);
                seccion.AddParagraph("Consulta: " + reporte.FechaConsulta.ToString("dd/MM/yyyy HH:mm") + " · Filtros: " + reporte.Filtros);
                var tabla = seccion.AddTable();
                tabla.Borders.Width = Unit.FromPoint(0.5); tabla.Borders.Color = Color.FromRgb(210, 214, 220);
                tabla.TopPadding = Unit.FromPoint(4); tabla.BottomPadding = Unit.FromPoint(4);
                var resto = 26d - 5 - 2.7;
                for (var i = 0; i < reporte.Columnas.Length; i++) tabla.AddColumn(Unit.FromCentimeter(i == 0 ? 5 : i == 1 ? 2.7 : resto / (reporte.Columnas.Length - 2)));
                var encabezado = tabla.AddRow(); encabezado.HeadingFormat = true; encabezado.Format.Font.Bold = true; encabezado.Shading.Color = Color.FromRgb(239, 242, 246);
                for (var i = 0; i < reporte.Columnas.Length; i++) encabezado.Cells[i].AddParagraph(reporte.Columnas[i]);
                foreach (var item in reporte.Filas)
                {
                    var fila = tabla.AddRow();
                    for (var i = 0; i < item.Valores.Length; i++) fila.Cells[i].AddParagraph(item.Valores[i] ?? "Sin información");
                }
                var resumen = seccion.AddParagraph(reporte.Resumen); resumen.Format.SpaceBefore = Unit.FromPoint(12); resumen.Format.Font.Bold = true;
                var pie = seccion.Footers.Primary.AddParagraph("Reporte generado por SysGym · Página "); pie.AddPageField(); pie.Format.Alignment = ParagraphAlignment.Center; pie.Format.Font.Size = 8;
                var renderer = new PdfDocumentRenderer { Document = documento }; renderer.RenderDocument();
                byte[] contenido;
                using (var pdf = renderer.PdfDocument)
                using (var memoria = new MemoryStream()) { pdf.Save(memoria, false); contenido = memoria.ToArray(); }
                using (var transaccion = datos.IniciarTransaccion())
                {
                    auditoria.RegistrarOperacion(datos, AuditoriaLogica.ExportarReporteOperativo, "ReporteOperativo", (int)reporte.Tipo + 1,
                        "Reporte " + reporte.Titulo + " exportado con " + reporte.Filas.Count + " registros. " + reporte.Filtros);
                    datos.GuardarCambios();
                    File.WriteAllBytes(ruta, contenido);
                    transaccion.Confirmar();
                }
            }
        }
    }
}
