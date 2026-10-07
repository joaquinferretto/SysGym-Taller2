using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaDatos.Repositorios;

namespace exxen2._0.capaLogica.Reportes
{
    public sealed class ReportesPagosServicio
    {
        private sealed class PagoExportable
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public string Dni { get; set; }
            public string Plan { get; set; }
            public DateTime Desde { get; set; }
            public DateTime Hasta { get; set; }
            public DateTime Fecha { get; set; }
            public decimal Importe { get; set; }
            public string Metodo { get; set; }
            public string Estado { get; set; }
            public string RegistradoPor { get; set; }
        }

        private sealed class HistorialExportable
        {
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public string Dni { get; set; }
            public string Plan { get; set; }
            public List<PagoExportable> Pagos { get; set; }
        }

        public string NombreComprobanteSugerido(int idPago)
        {
            var pago = ObtenerPago(idPago);
            return NombreSeguro("Comprobante_Pago_" + pago.Id + "_" + pago.Nombre + "_" + pago.Apellido) + ".pdf";
        }

        public string NombreHistorialSugerido(int idSocio)
        {
            var historial = ObtenerHistorial(idSocio);
            return NombreSeguro("Historial_Pagos_" + historial.Nombre + "_" + historial.Apellido) + ".pdf";
        }

        public void GenerarComprobante(int idPago, string destino)
        {
            ValidarDestino(destino);
            var pago = ObtenerPago(idPago);
            var documento = CrearDocumento("Comprobante de pago");
            var seccion = documento.AddSection();
            seccion.PageSetup.PageFormat = PageFormat.A4;
            seccion.PageSetup.TopMargin = Unit.FromCentimeter(2);
            AgregarMarca(seccion);
            AgregarTitulo(seccion, "Comprobante de pago");
            AgregarLinea(seccion, "Pago", "N.º " + pago.Id);
            AgregarLinea(seccion, "Socio", pago.Nombre + " " + pago.Apellido);
            AgregarLinea(seccion, "DNI", pago.Dni);
            AgregarLinea(seccion, "Plan", pago.Plan);
            AgregarLinea(seccion, "Período", Periodo(pago));
            AgregarLinea(seccion, "Fecha del pago", pago.Fecha.ToString("dd/MM/yyyy HH:mm", CultureInfo.CurrentCulture));
            AgregarLinea(seccion, "Importe", pago.Importe.ToString("C", CultureInfo.CurrentCulture));
            AgregarLinea(seccion, "Método de pago", pago.Metodo);
            AgregarLinea(seccion, "Estado", pago.Estado);
            AgregarLinea(seccion, "Registrado por", pago.RegistradoPor);
            AgregarPie(seccion);
            Guardar(documento, destino);
        }

        public void GenerarHistorial(int idSocio, string destino)
        {
            ValidarDestino(destino);
            var pagos = ObtenerHistorial(idSocio);
            if (pagos.Pagos.Count == 0)
                throw new InvalidOperationException("El socio no tiene pagos aprobados para exportar.");
            var documento = CrearDocumento("Historial de pagos");
            var seccion = documento.AddSection();
            seccion.PageSetup.PageFormat = PageFormat.A4;
            seccion.PageSetup.Orientation = Orientation.Landscape;
            seccion.PageSetup.TopMargin = Unit.FromCentimeter(1.6);
            AgregarMarca(seccion);
            AgregarTitulo(seccion, "Historial de pagos");
            AgregarLinea(seccion, "Socio", pagos.Nombre + " " + pagos.Apellido);
            AgregarLinea(seccion, "DNI", pagos.Dni);
            AgregarLinea(seccion, "Plan", pagos.Plan);

            var tabla = seccion.AddTable();
            tabla.Borders.Color = Color.FromRgb(210, 214, 224);
            tabla.Borders.Width = Unit.FromPoint(0.5);
            tabla.LeftPadding = Unit.FromPoint(6);
            tabla.RightPadding = Unit.FromPoint(6);
            tabla.AddColumn(Unit.FromCentimeter(3.5));
            tabla.AddColumn(Unit.FromCentimeter(6));
            tabla.AddColumn(Unit.FromCentimeter(6));
            tabla.AddColumn(Unit.FromCentimeter(5));
            tabla.AddColumn(Unit.FromCentimeter(4));
            var encabezado = tabla.AddRow();
            encabezado.HeadingFormat = true;
            encabezado.Shading.Color = Color.FromRgb(239, 240, 255);
            encabezado.Format.Font.Bold = true;
            string[] titulos = { "Fecha", "Período", "Método", "Importe", "Estado" };
            for (var i = 0; i < titulos.Length; i++) encabezado.Cells[i].AddParagraph(titulos[i]);
            foreach (var pago in pagos.Pagos)
            {
                var fila = tabla.AddRow();
                fila.Cells[0].AddParagraph(pago.Fecha.ToString("dd/MM/yyyy", CultureInfo.CurrentCulture));
                fila.Cells[1].AddParagraph(Periodo(pago));
                fila.Cells[2].AddParagraph(pago.Metodo);
                fila.Cells[3].AddParagraph(pago.Importe.ToString("C", CultureInfo.CurrentCulture));
                fila.Cells[4].AddParagraph(pago.Estado);
            }
            var total = seccion.AddParagraph(pagos.Pagos.Count + " pago(s)  ·  Total abonado: " +
                pagos.Pagos.Sum(p => p.Importe).ToString("C", CultureInfo.CurrentCulture));
            total.Format.SpaceBefore = Unit.FromPoint(12);
            total.Format.Font.Bold = true;
            AgregarPie(seccion);
            Guardar(documento, destino);
        }

        private static PagoExportable ObtenerPago(int idPago)
        {
            if (idPago <= 0) throw new InvalidOperationException("Seleccioná un pago válido.");
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                var pago = datos.Pagos.ConsultarSoloLectura("MetodoPago.MercadoPago", "MetodoPago.PagoEfectivo", "UsuarioRegistro", "Cuotas.Membresia.Socio", "Cuotas.Membresia.Plan")
                    .SingleOrDefault(p => p.IdRegistroPago == idPago);
                var cuota = pago == null ? null : pago.Cuotas.FirstOrDefault(c => c.EstadoPago == EstadosCuota.Pagada);
                if (pago == null || pago.Estado != EstadosTransaccionPago.Aprobado || cuota == null)
                    throw new InvalidOperationException("El pago seleccionado no está aprobado o no corresponde a una cuota pagada.");
                return Proyectar(pago, cuota);
            }
        }

        private static HistorialExportable ObtenerHistorial(int idSocio)
        {
            if (idSocio <= 0) throw new InvalidOperationException("Seleccioná un socio válido.");
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                var socio = datos.Socios.ConsultarSoloLectura("Membresias.Plan").SingleOrDefault(s => s.IdSocio == idSocio);
                if (socio == null) throw new InvalidOperationException("El socio no existe.");
                var cuotasPagadas = datos.CuotasMembresia.ConsultarSoloLectura("Pago.MetodoPago.MercadoPago", "Pago.MetodoPago.PagoEfectivo", "Membresia.Socio", "Membresia.Plan")
                    .Where(c => c.Membresia.IdSocio == idSocio && c.EstadoPago == EstadosCuota.Pagada &&
                        c.IdRegistroPago.HasValue && c.Pago.Estado == EstadosTransaccionPago.Aprobado)
                    .OrderByDescending(c => c.Pago.Fecha).ThenByDescending(c => c.Pago.IdRegistroPago).ToList();
                var pagos = cuotasPagadas.GroupBy(c => c.IdRegistroPago.Value)
                    .Select(grupo => grupo.First()).Select(c => Proyectar(c.Pago, c)).ToList();
                var plan = socio.Membresias.OrderByDescending(m => m.Estado).ThenByDescending(m => m.FechaInicio).FirstOrDefault();
                return new HistorialExportable
                {
                    Nombre = socio.Nombre,
                    Apellido = socio.Apellido,
                    Dni = socio.DNI,
                    Plan = plan == null || plan.Plan == null ? "Sin plan" : plan.Plan.Nombre,
                    Pagos = pagos
                };
            }
        }

        private static PagoExportable Proyectar(Pago pago, CuotaMembresia cuota)
        {
            var socio = cuota.Membresia == null ? null : cuota.Membresia.Socio;
            var metodo = pago.MetodoPago;
            return new PagoExportable
            {
                Id = pago.IdRegistroPago,
                Nombre = socio == null ? "Socio" : socio.Nombre,
                Apellido = socio == null ? "" : socio.Apellido,
                Dni = socio == null ? "-" : socio.DNI,
                Plan = cuota.Membresia == null || cuota.Membresia.Plan == null ? "-" : cuota.Membresia.Plan.Nombre,
                Desde = cuota.FechaDesde,
                Hasta = cuota.FechaHasta,
                Fecha = pago.Fecha,
                Importe = pago.Importe,
                Metodo = metodo == null ? "-" : metodo.IdNroPagoMP.HasValue ? "Mercado Pago" : metodo.IdPagoEfectivo.HasValue ? "Efectivo" : "Otro",
                Estado = pago.Estado,
                RegistradoPor = pago.UsuarioRegistro == null ? "Sin información" : pago.UsuarioRegistro.Nombre + " " + pago.UsuarioRegistro.Apellido
            };
        }

        private static Document CrearDocumento(string titulo)
        {
            var documento = new Document();
            documento.Info.Title = titulo;
            documento.Info.Author = "SysGym";
            documento.Styles[StyleNames.Normal].Font.Name = "Arial";
            documento.Styles[StyleNames.Normal].Font.Size = 10;
            documento.Styles[StyleNames.Normal].ParagraphFormat.SpaceAfter = Unit.FromPoint(7);
            return documento;
        }

        private static void AgregarMarca(Section seccion)
        {
            var logo = Properties.Resources.SysGymLogo;
            if (logo != null)
            {
                using (var memoria = new MemoryStream())
                {
                    logo.Save(memoria, ImageFormat.Png);
                    var imagen = seccion.AddParagraph().AddImage("base64:" + Convert.ToBase64String(memoria.ToArray()));
                    imagen.LockAspectRatio = true;
                    imagen.Width = Unit.FromCentimeter(4.2);
                }
            }
            var marca = seccion.AddParagraph("SYSGYM");
            marca.Format.Font.Size = 22;
            marca.Format.Font.Bold = true;
            marca.Format.Font.Color = Color.FromRgb(79, 70, 229);
        }

        private static void AgregarTitulo(Section seccion, string texto)
        {
            var titulo = seccion.AddParagraph(texto);
            titulo.Format.Font.Size = 17;
            titulo.Format.Font.Bold = true;
            titulo.Format.SpaceAfter = Unit.FromPoint(16);
        }

        private static void AgregarLinea(Section seccion, string nombre, string valor)
        {
            var parrafo = seccion.AddParagraph();
            parrafo.AddFormattedText(nombre + ": ", TextFormat.Bold);
            parrafo.AddText(valor ?? "-");
        }

        private static void AgregarPie(Section seccion)
        {
            var pie = seccion.Footers.Primary.AddParagraph("Comprobante generado por SysGym · " + DateTime.Now.ToString("dd/MM/yyyy HH:mm", CultureInfo.CurrentCulture));
            pie.Format.Font.Size = 8;
            pie.Format.Alignment = ParagraphAlignment.Center;
        }

        private static string Periodo(PagoExportable pago)
        {
            return pago.Desde.ToString("dd/MM/yyyy", CultureInfo.CurrentCulture) + " al " + pago.Hasta.ToString("dd/MM/yyyy", CultureInfo.CurrentCulture);
        }

        private static void ValidarDestino(string destino)
        {
            if (string.IsNullOrWhiteSpace(destino)) throw new ArgumentException("Seleccione el archivo de destino.", "destino");
        }

        private static string NombreSeguro(string nombre)
        {
            var invalidos = Path.GetInvalidFileNameChars();
            return new string(nombre.Select(c => invalidos.Contains(c) || char.IsWhiteSpace(c) ? '_' : c).ToArray());
        }

        private static void Guardar(Document documento, string destino)
        {
            var ruta = Path.GetFullPath(destino);
            var renderer = new PdfDocumentRenderer { Document = documento };
            renderer.RenderDocument();
            using (var pdf = renderer.PdfDocument) pdf.Save(ruta);
        }
    }
}
