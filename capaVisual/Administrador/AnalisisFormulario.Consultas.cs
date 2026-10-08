using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using exxen2._0.capaLogica.Analisis;
using exxen2._0.capaLogica.Utilidades;

namespace exxen2._0.capaVisual.Administrador
{
    public partial class AnalisisFormulario
    {
        private static string FormatearVariacion(decimal? valor)
        {
            return valor.HasValue ? valor.Value.ToString("+0.##;-0.##;0") + "%" : "— (base sin movimiento)";
        }

        private static void CargarImportes(Chart chart, IEnumerable<DatoImporte> datos)
        {
            chart.Series.Clear();
            chart.Titles.Clear();
            chart.Titles.Add("Ingresos reales por plan del período (plan actualmente relacionado)");
            var valores = datos.Where(x => x.Importe != 0m).ToList();
            if (valores.Count == 0) { chart.Titles.Add("Sin datos para el período seleccionado"); return; }
            var serie = new Series("Ingresos") { ChartType = SeriesChartType.Bar, IsValueShownAsLabel = true,
                LabelFormat = "C0", Color = Color.FromArgb(79, 70, 229), ToolTip = "#VALX: #VALY{C2}" };
            foreach (var dato in valores.AsEnumerable().Reverse()) serie.Points.AddXY(dato.Nombre, dato.Importe);
            chart.Series.Add(serie);
            chart.ChartAreas[0].AxisX.Interval = 1;
            chart.ChartAreas[0].AxisY.Minimum = 0;
            chart.ChartAreas[0].AxisY.LabelStyle.Format = "C0";
        }

        private void comparar_Click(object sender, EventArgs e)
        {
            comparacionActual = null;
            if (desdeA.Value.Date > hastaA.Value.Date || desdeB.Value.Date > hastaB.Value.Date)
            {
                lblCompararEstado.Text = "Cada período requiere Desde anterior o igual a Hasta.";
                return;
            }
            try
            {
                Cursor = Cursors.WaitCursor;
                var datos = consultas.Comparar(desdeA.Value, hastaA.Value, desdeB.Value, hastaB.Value);
                gridComparacion.Rows.Clear();
                foreach (var dato in datos)
                    gridComparacion.Rows.Add(dato.Nombre, dato.ValorA.ToString(dato.EsMoneda ? "C2" : "N0"),
                        dato.ValorB.ToString(dato.EsMoneda ? "C2" : "N0"), FormatearVariacion(dato.VariacionPorcentual));
                CargarComparacion(chartCompararIngresos, datos.Where(x => x.EsMoneda), "Ingresos", true);
                CargarComparacion(chartCompararCantidades, datos.Where(x => !x.EsMoneda), "Pagos y altas", false);
                lblCompararEstado.Text = "Variación de B respecto de A. Se comparan totales de los rangos elegidos. La deuda histórica no está disponible.";
                comparacionActual = new exxen2._0.capaLogica.Reportes.ComparacionAnalisisPdf {
                    DesdeA = desdeA.Value.Date, HastaA = hastaA.Value.Date,
                    DesdeB = desdeB.Value.Date, HastaB = hastaB.Value.Date, Indicadores = datos };
            }
            catch (Exception ex) { AyudaFormularioVisual.MostrarError(lblCompararEstado, ex); }
            finally { Cursor = Cursors.Default; }
        }

        private static void CargarComparacion(Chart chart, IEnumerable<IndicadorComparacion> datos, string titulo, bool moneda)
        {
            chart.Series.Clear(); chart.Titles.Clear(); chart.Titles.Add(titulo);
            var valores = datos.ToList();
            if (valores.All(x => x.ValorA == 0 && x.ValorB == 0))
            {
                chart.Titles.Add("Sin datos para el período seleccionado"); return;
            }
            var a = new Series("Período A") { ChartType = SeriesChartType.Column, IsValueShownAsLabel = true,
                LabelFormat = moneda ? "C0" : "N0", ToolTip = moneda ? "A - #VALX: #VALY{C2}" : "A - #VALX: #VALY{N0}" };
            var b = new Series("Período B") { ChartType = SeriesChartType.Column, IsValueShownAsLabel = true,
                LabelFormat = moneda ? "C0" : "N0", ToolTip = moneda ? "B - #VALX: #VALY{C2}" : "B - #VALX: #VALY{N0}" };
            foreach (var valor in valores) { a.Points.AddXY(valor.Nombre, valor.ValorA); b.Points.AddXY(valor.Nombre, valor.ValorB); }
            chart.Series.Add(a); chart.Series.Add(b);
            chart.ChartAreas[0].AxisX.Interval = 1;
            chart.ChartAreas[0].AxisY.Minimum = 0;
            chart.ChartAreas[0].AxisY.LabelStyle.Format = moneda ? "C0" : "N0";
        }

        private void buscarSocio_Click(object sender, EventArgs e) { BuscarSocios(); }

        private void BuscarSocios()
        {
            try
            {
                cargandoSocios = true;
                var opciones = consultas.BuscarSocios(buscarSocio.Text);
                selectorSocio.DataSource = opciones;
                selectorSocio.DisplayMember = "NombreCompleto";
                selectorSocio.ValueMember = "IdSocio";
                selectorSocio.SelectedIndex = -1;
                gridSocio.Rows.Clear(); chartSocio.Series.Clear(); chartSocio.Titles.Clear();
                chartSocio.Titles.Add("Seleccione un socio para consultar sus pagos");
                lblBusquedaEstado.Text = opciones.Count == 0 ? "Sin socios para la búsqueda." :
                    "Seleccione un socio. Se muestran hasta 100 resultados; use nombre o DNI para acotar.";
            }
            catch (Exception ex) { AyudaFormularioVisual.MostrarError(lblBusquedaEstado, ex); }
            finally { cargandoSocios = false; }
        }

        private void selectorSocio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cargandoSocios || selectorSocio.SelectedIndex < 0) return;
            try
            {
                Cursor = Cursors.WaitCursor;
                var datos = consultas.ObtenerSocio(((OpcionSocioAnalisis)selectorSocio.SelectedItem).IdSocio);
                gridSocio.Rows.Clear(); chartSocio.Series.Clear(); chartSocio.Titles.Clear();
                if (datos == null) { lblBusquedaEstado.Text = "El socio ya no está disponible."; return; }
                var estado = datos.Estado;
                gridSocio.Rows.Add("Nombre completo", estado.Socio);
                gridSocio.Rows.Add("DNI", estado.DNI);
                gridSocio.Rows.Add("Estado actual", estado.Activo ? "Activo" : "Inactivo");
                gridSocio.Rows.Add("Plan relacionado", estado.Plan);
                gridSocio.Rows.Add("Entrenador actual", datos.Entrenador);
                gridSocio.Rows.Add("Rutina actual", datos.Rutina);
                gridSocio.Rows.Add("Total histórico abonado", datos.TotalAbonado.ToString("C2"));
                gridSocio.Rows.Add("Pagos aprobados", datos.CantidadPagos.ToString("N0"));
                gridSocio.Rows.Add("Estado de deuda", datos.EstadoDeuda);
                gridSocio.Rows.Add("Deuda actual", estado.DeudaTotal.ToString("C2"));
                gridSocio.Rows.Add("Cuotas pendientes", estado.CuotasPendientes);
                gridSocio.Rows.Add("Cuotas vencidas", estado.CuotasVencidas);
                gridSocio.Rows.Add("Último pago", datos.UltimoPago.HasValue ? datos.UltimoPago.Value.ToString("dd/MM/yyyy") : "Sin pagos aprobados");
                gridSocio.Rows.Add("Próximo vencimiento", estado.ProximoVencimiento.HasValue ? estado.ProximoVencimiento.Value.ToString("dd/MM/yyyy") : "Sin vencimiento próximo");
                gridSocio.Rows.Add("Fecha de alta", estado.FechaAlta.HasValue ? estado.FechaAlta.Value.ToString("dd/MM/yyyy HH:mm") : "No registrada (histórico)");
                gridSocio.Rows.Add("Antigüedad aproximada", datos.AntiguedadMeses.HasValue ?
                    (datos.AntiguedadMeses.Value / 12) + " años, " + (datos.AntiguedadMeses.Value % 12) + " meses" : "No disponible");
                if (datos.TieneHistoriaParaGraficar) CargarMensual(chartSocio, datos.PagosPorMes, "Pagos históricos del socio", true);
                else chartSocio.Titles.Add("Sin movimientos suficientes para graficar.");
                lblBusquedaEstado.Text = "Consulta individual: pagos históricos y deuda actual. El filtro principal no limita esta vista.";
            }
            catch (Exception ex) { AyudaFormularioVisual.MostrarError(lblBusquedaEstado, ex); }
            finally { Cursor = Cursors.Default; }
        }
    }
}
