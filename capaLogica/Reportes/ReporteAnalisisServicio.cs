using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using exxen2._0.capaDatos.Repositorios;
using exxen2._0.capaLogica.Analisis;
using exxen2._0.capaLogica.Auditoria;

namespace exxen2._0.capaLogica.Reportes
{
    public sealed class ComparacionAnalisisPdf
    {
        public DateTime DesdeA { get; set; }
        public DateTime HastaA { get; set; }
        public DateTime DesdeB { get; set; }
        public DateTime HastaB { get; set; }
        public List<IndicadorComparacion> Indicadores { get; set; }
    }

    // Recibe el DTO y PNG del análisis ya cargado. No consulta movimientos ni crea controles WinForms.
    public sealed class ReporteAnalisisServicio
    {
        private static readonly CultureInfo Argentina = CultureInfo.GetCultureInfo("es-AR");
        private const string SinDatos = "Sin datos para el período seleccionado.";
        private readonly AuditoriaLogica auditoria;
        public ReporteAnalisisServicio(int idUsuarioAutenticado) { auditoria = new AuditoriaLogica(idUsuarioAutenticado); }
        public void ValidarAcceso() { auditoria.ValidarAcceso(); }
        public static string NombreSugerido(DateTime desde, DateTime hasta)
        {
            return "Analisis_SysGym_" + desde.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) + "_" +
                hasta.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) + ".pdf";
        }

        public void Generar(ResultadoAnalisis resultado, DateTime desde, DateTime hasta,
            IDictionary<string, byte[]> graficos, ComparacionAnalisisPdf comparacion, string destino)
        {
            if (resultado == null || graficos == null) throw new ArgumentException("Debe existir un análisis cargado.");
            if (desde.Date > hasta.Date) throw new ArgumentException("El período no es válido.");
            if (string.IsNullOrWhiteSpace(destino) || !string.Equals(Path.GetExtension(destino), ".pdf", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("Seleccioná un destino PDF válido.");
            var ruta = Path.GetFullPath(destino);
            if (comparacion != null && (comparacion.Indicadores == null || comparacion.DesdeA.Date > comparacion.HastaA.Date || comparacion.DesdeB.Date > comparacion.HastaB.Date))
                throw new ArgumentException("La comparación cargada no es válida.");
            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                var usuario = auditoria.ObtenerUsuario(datos, true);
                var documento = Construir(resultado, desde.Date, hasta.Date, graficos, comparacion,
                    usuario.Nombre + " " + usuario.Apellido + " - " + usuario.Rol.Descripcion, DateTime.Now);
                var renderer = new PdfDocumentRenderer { Document = documento };
                renderer.RenderDocument();
                byte[] contenido;
                using (var pdf = renderer.PdfDocument)
                using (var memoria = new MemoryStream())
                {
                    pdf.Save(memoria, false);
                    contenido = memoria.ToArray();
                }
                using (var transaccion = datos.IniciarTransaccion())
                {
                    // IdEntidad 1 identifica el módulo Análisis, no un pago ni un socio.
                    auditoria.RegistrarOperacion(datos, AuditoriaLogica.GenerarReporteAnalisis, "Analisis", 1,
                        "Reporte de análisis generado para el período " + Periodo(desde, hasta));
                    datos.GuardarCambios();
                    File.WriteAllBytes(ruta, contenido);
                    transaccion.Confirmar();
                }
            }
        }

        private static Document Construir(ResultadoAnalisis r, DateTime desde, DateTime hasta,
            IDictionary<string, byte[]> graficos, ComparacionAnalisisPdf comparacion, string usuario, DateTime generado)
        {
            var doc = new Document();
            doc.Info.Title = "SysGym - Reporte de análisis";
            doc.Info.Author = usuario;
            var normal = doc.Styles[StyleNames.Normal];
            normal.Font.Name = "Arial";
            normal.Font.Size = 10;
            normal.ParagraphFormat.SpaceAfter = Unit.FromPoint(7);
            doc.Styles[StyleNames.Heading1].Font.Size = 18;
            doc.Styles[StyleNames.Heading1].Font.Color = Color.FromRgb(31, 41, 55);
            doc.Styles[StyleNames.Heading1].ParagraphFormat.SpaceAfter = Unit.FromPoint(14);
            var portada = Seccion(doc, "REPORTE DE ANÁLISIS");
            using (var memoria = new MemoryStream())
            {
                Properties.Resources.SysGymLogo.Save(memoria, ImageFormat.Png);
                var logo = portada.AddParagraph().AddImage("base64:" + Convert.ToBase64String(memoria.ToArray()));
                logo.LockAspectRatio = true;
                logo.Width = Unit.FromCentimeter(4);
            }
            portada.AddParagraph("SysGym").Format.Font.Size = 16;
            portada.AddParagraph("Período: " + Periodo(desde, hasta));
            portada.AddParagraph("Generado: " + generado.ToString("dd/MM/yyyy HH:mm", Argentina));
            portada.AddParagraph("Generado por: " + usuario);
            portada.AddParagraph("Resumen ejecutivo", StyleNames.Heading2);
            Tabla(portada, new[] { "Indicador", "Valor" }, new[] {
                Fila("Ingresos del período", Moneda(r.TotalIngresos)), Fila("Cantidad de pagos", Numero(r.CantidadPagos)),
                Fila("Socios activos", Numero(r.SociosActivos)), Fila("Socios inactivos", Numero(r.TotalSocios - r.SociosActivos)),
                Fila("Socios al día", Numero(Cantidad(r.EstadoDeuda, "Al día"))),
                Fila("Socios con deuda", Numero(r.SociosConDeuda)),
                Fila("Deuda debajo del límite", Numero(Cantidad(r.EstadoDeuda, "Con deuda"))),
                Fila("Socios en límite de deuda", Numero(Cantidad(r.EstadoDeuda, "Límite alcanzado"))),
                Fila("Altas del período", Numero(r.TotalAltas)) });
            portada.AddParagraph("Ingresos, pagos y altas corresponden al período. Socios, deuda, planes, entrenadores, rutinas y ejercicios reflejan el estado actual cargado en Análisis. Las categorías de deuda usan la configuración vigente al aplicar el análisis.");

            var finanzas = Seccion(doc, "Finanzas · Ingresos por mes");
            finanzas.AddParagraph("Total: " + Moneda(r.TotalIngresos) + " · Pagos: " + Numero(r.CantidadPagos));
            finanzas.AddParagraph("Ingresos del período anterior: " + Moneda(r.IngresosPeriodoAnterior) + " · Variación: " + Porcentaje(r.VariacionIngresosPorcentual));
            Grafico(finanzas, "Ingresos", r.IngresosPorMes.Any(x => x.Valor != 0), graficos);
            TablaBreve(finanzas, new[] { "Mes", "Ingresos" }, r.IngresosPorMes.Select(x => Fila(x.Mes.ToString("MMMM yyyy", Argentina), Moneda(x.Valor))));
            if (r.VariacionUltimoMesPorcentual.HasValue) finanzas.AddParagraph("Último mes respecto del anterior: " + Porcentaje(r.VariacionUltimoMesPorcentual));

            Categorias(doc, "Socios · Activos e inactivos", "Activos", r.EstadoSocios, graficos, "Distribución actual de socios.");
            Categorias(doc, "Socios · Estado de deuda", "Deuda", r.EstadoDeuda, graficos, "Al día: 0 vencidas. Con deuda: debajo del límite. Límite alcanzado: umbral configurable alcanzado. Solo cuentan cuotas Pendiente vencidas.");
            var altas = Seccion(doc, "Socios · Altas por mes");
            altas.AddParagraph("Altas del período: " + Numero(r.TotalAltas));
            altas.AddParagraph("Los socios históricos sin fecha de alta no se incluyen en esta serie. Sin fecha de alta: " + Numero(r.SociosSinFechaAlta) + ".");
            Grafico(altas, "Altas", r.AltasPorMes.Any(x => x.Valor != 0), graficos);
            TablaBreve(altas, new[] { "Mes", "Altas" }, r.AltasPorMes.Select(x => Fila(x.Mes.ToString("MMMM yyyy", Argentina), Numero(x.Valor))));

            Categorias(doc, "Planes más contratados", "Planes", r.Planes, graficos, "Cantidad de socios actuales por plan; no representa dinero cobrado.");
            var ingresosPlan = Seccion(doc, "Ingresos por plan");
            ingresosPlan.AddParagraph("Dinero efectivamente cobrado en el período, según el plan actualmente relacionado; conserva la definición de Análisis.");
            Grafico(ingresosPlan, "IngresosPlan", r.IngresosPorPlan.Any(x => x.Importe != 0), graficos);
            TablaBreve(ingresosPlan, new[] { "Plan", "Ingresos" }, r.IngresosPorPlan.Select(x => Fila(x.Nombre, Moneda(x.Importe))));
            Categorias(doc, "Socios por entrenador", "Entrenadores", r.Entrenadores, graficos, "Asignaciones actuales; incluye Sin asignar cuando corresponde.");
            Categorias(doc, "Distribución por método de pago", "Metodos", r.MetodosPago, graficos, "Cantidad de pagos del período por método real.");
            Categorias(doc, "Rutinas más utilizadas", "Rutinas", r.Rutinas, graficos, "Socios actuales por rutina. Socios activos sin rutina activa: " + Numero(r.SinRutina));
            Categorias(doc, "Ejercicios más utilizados", "Ejercicios", r.EjerciciosTop, graficos, "Top 10 según cantidad de rutinas distintas; no cuenta repeticiones del entrenamiento.");
            var sinUso = Seccion(doc, "Ejercicios sin utilizar");
            sinUso.AddParagraph("Ejercicios activos sin uso: " + Numero(r.EjerciciosSinUso.Count));
            TablaBreve(sinUso, new[] { "Ejercicio", "Descripción" }, r.EjerciciosSinUso.Select(x => Fila(x.Nombre, x.Descripcion ?? "—")));
            if (comparacion != null)
            {
                var c = Seccion(doc, "Comparación de períodos");
                c.AddParagraph("Período A: " + Periodo(comparacion.DesdeA, comparacion.HastaA));
                c.AddParagraph("Período B: " + Periodo(comparacion.DesdeB, comparacion.HastaB));
                Tabla(c, new[] { "Indicador", "A", "B", "Variación B / A" }, comparacion.Indicadores.Select(x => new[] {
                    x.Nombre, x.EsMoneda ? Moneda(x.ValorA) : Numero(x.ValorA),
                    x.EsMoneda ? Moneda(x.ValorB) : Numero(x.ValorB), Porcentaje(x.VariacionPorcentual) }));
                c.AddParagraph("Se compara la información ya cargada en la pestaña Comparar. No se reconstruyen estados históricos de deuda.");
            }
            return doc;
        }

        private static Section Seccion(Document doc, string titulo)
        {
            var s = doc.AddSection();
            s.PageSetup.PageFormat = PageFormat.A4;
            s.PageSetup.TopMargin = Unit.FromCentimeter(1.8);
            s.PageSetup.BottomMargin = Unit.FromCentimeter(1.8);
            s.PageSetup.LeftMargin = Unit.FromCentimeter(2);
            s.PageSetup.RightMargin = Unit.FromCentimeter(2);
            s.AddParagraph(titulo, StyleNames.Heading1);
            var pie = s.Footers.Primary.AddParagraph("Reporte generado por SysGym · Página ");
            pie.AddPageField();
            pie.Format.Font.Size = 8;
            pie.Format.Alignment = ParagraphAlignment.Center;
            return s;
        }

        private static void Categorias(Document doc, string titulo, string clave, List<DatoCategoria> valores, IDictionary<string, byte[]> graficos, string nota)
        {
            var s = Seccion(doc, titulo);
            s.AddParagraph(nota);
            Grafico(s, clave, valores.Any(x => x.Cantidad > 0), graficos);
            TablaBreve(s, new[] { "Categoría", "Cantidad" }, valores.Select(x => Fila(x.Nombre, Numero(x.Cantidad))));
        }

        private static void Grafico(Section s, string clave, bool tieneDatos, IDictionary<string, byte[]> graficos)
        {
            if (!tieneDatos) { s.AddParagraph(SinDatos); return; }
            byte[] png;
            if (!graficos.TryGetValue(clave, out png) || png == null || png.Length == 0)
                throw new ArgumentException("Falta el gráfico cargado: " + clave);
            var p = s.AddParagraph();
            p.Format.KeepTogether = true;
            var imagen = p.AddImage("base64:" + Convert.ToBase64String(png));
            imagen.LockAspectRatio = true;
            imagen.Width = Unit.FromCentimeter(17);
        }

        private static void TablaBreve(Section s, string[] columnas, IEnumerable<string[]> filas)
        {
            var valores = filas.ToList();
            if (valores.Count == 0) { s.AddParagraph(SinDatos); return; }
            Tabla(s, columnas, valores.Take(10));
            if (valores.Count > 10) s.AddParagraph("Se muestran 10 de " + valores.Count + " filas. El gráfico conserva los datos de la pantalla.");
        }

        private static void Tabla(Section s, string[] columnas, IEnumerable<string[]> filas)
        {
            var t = s.AddTable();
            t.Borders.Width = Unit.FromPoint(0.5);
            t.Borders.Color = Color.FromRgb(210, 214, 220);
            t.TopPadding = Unit.FromPoint(5);
            t.BottomPadding = Unit.FromPoint(5);
            for (var i = 0; i < columnas.Length; i++) t.AddColumn(Unit.FromCentimeter(columnas.Length == 2 ? (i == 0 ? 11 : 6) : 17d / columnas.Length));
            var encabezado = t.AddRow();
            encabezado.HeadingFormat = true;
            encabezado.Shading.Color = Color.FromRgb(239, 242, 246);
            encabezado.Format.Font.Bold = true;
            for (var i = 0; i < columnas.Length; i++) encabezado.Cells[i].AddParagraph(columnas[i]);
            foreach (var valores in filas)
            {
                var fila = t.AddRow();
                for (var i = 0; i < columnas.Length; i++) fila.Cells[i].AddParagraph(valores[i] ?? "—");
            }
        }

        private static string[] Fila(string nombre, string valor) { return new[] { nombre, valor }; }
        private static int Cantidad(IEnumerable<DatoCategoria> categorias, string nombre) { return categorias.Where(x => x.Nombre == nombre).Sum(x => x.Cantidad); }
        private static string Numero(decimal valor) { return valor.ToString("N0", Argentina); }
        private static string Moneda(decimal valor) { return valor.ToString("C2", Argentina); }
        private static string Porcentaje(decimal? valor) { return valor.HasValue ? valor.Value.ToString("0.##", Argentina) + " %" : "— (base sin movimiento)"; }
        private static string Periodo(DateTime desde, DateTime hasta) { return desde.ToString("dd/MM/yyyy", Argentina) + " - " + hasta.ToString("dd/MM/yyyy", Argentina); }
    }
}
