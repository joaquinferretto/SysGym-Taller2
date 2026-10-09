using System;
using exxen2._0.capaVisual.Compartido.Utilidades;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using exxen2._0.capaLogica.Analisis;

namespace exxen2._0.capaVisual.Administrador
{
    [DesignerCategory("Form")]
    public partial class AnalisisFormulario : Form
    {
        private readonly AnalisisLogica logica = new AnalisisLogica();
        private readonly AnalisisConsultaLogica consultas = new AnalisisConsultaLogica();
        private bool cargandoSocios;
        private static readonly Font FuenteTituloGrafico = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
        private static readonly Font FuenteEjesGrafico = new Font("Segoe UI", 8.5F, FontStyle.Regular);
        private static readonly Font FuenteDatosGrafico = new Font("Segoe UI", 8F, FontStyle.Regular);

        public AnalisisFormulario() : this(null) { }

        public AnalisisFormulario(exxen2._0.capaDatos.Entidades.UsuarioSistema usuarioActual)
        {
            reporte = new exxen2._0.capaLogica.Reportes.ReporteAnalisisServicio(usuarioActual == null ? 0 : usuarioActual.IdUsuarioSistema);
            idUsuarioGenerador = usuarioActual == null ? 0 : usuarioActual.IdUsuarioSistema;
            InitializeComponent();
            EstilizarGraficos();
        }

        private void EstilizarGraficos()
        {
            var graficos = new[]
            {
                chartIngresos, chartMetodos, chartAltas, chartActivos, chartDeuda,
                chartPlanes, chartEntrenadores, chartIngresosPlan, chartRutinas,
                chartEjercicios, chartCompararIngresos, chartCompararCantidades, chartSocio
            };
            foreach (var grafico in graficos)
                EstilizarGrafico(grafico);
        }

        private static void EstilizarGrafico(Chart grafico)
        {
            grafico.AntiAliasing = AntiAliasingStyles.All;
            grafico.TextAntiAliasingQuality = TextAntiAliasingQuality.High;
            foreach (Title tituloGrafico in grafico.Titles)
            {
                tituloGrafico.Font = FuenteTituloGrafico;
                tituloGrafico.ForeColor = Color.FromArgb(30, 41, 59);
            }
            foreach (Legend leyenda in grafico.Legends)
            {
                leyenda.Font = FuenteEjesGrafico;
                leyenda.Docking = Docking.Bottom;
                leyenda.Alignment = StringAlignment.Center;
                leyenda.LegendStyle = LegendStyle.Row;
            }

            var esBarraHorizontal = grafico.Series.Any(serie => serie.ChartType == SeriesChartType.Bar);
            var esCircular = grafico.Series.Any(serie => serie.ChartType == SeriesChartType.Pie || serie.ChartType == SeriesChartType.Doughnut);
            foreach (ChartArea area in grafico.ChartAreas)
            {
                area.BackColor = Color.White;
                area.AxisX.LabelStyle.Font = FuenteEjesGrafico;
                area.AxisY.LabelStyle.Font = FuenteEjesGrafico;
                area.AxisX.LineColor = Color.FromArgb(203, 213, 225);
                area.AxisY.LineColor = Color.FromArgb(203, 213, 225);
                area.AxisX.MajorGrid.Enabled = !esCircular && esBarraHorizontal;
                area.AxisY.MajorGrid.Enabled = !esCircular && !esBarraHorizontal;
                area.AxisX.MajorGrid.LineColor = Color.FromArgb(226, 232, 240);
                area.AxisY.MajorGrid.LineColor = Color.FromArgb(226, 232, 240);
                area.AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
                area.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
            }
            foreach (Series serie in grafico.Series)
            {
                serie.Font = FuenteDatosGrafico;
                serie.LabelForeColor = Color.FromArgb(51, 65, 85);
            }
        }

        private void AnalisisFormulario_Load(object sender, EventArgs e)
        {
            if (AyudaFormularioVisual.EnModoDisenio(this))
                return;
            var hoy = DateTime.Today;
            desde.Value = new DateTime(hoy.Year, hoy.Month, 1);
            hasta.Value = hoy;
            desdeA.Value = new DateTime(hoy.Year, hoy.Month, 1).AddMonths(-1);
            hastaA.Value = new DateTime(hoy.Year, hoy.Month, 1).AddDays(-1);
            desdeB.Value = new DateTime(hoy.Year, hoy.Month, 1);
            hastaB.Value = hoy;
            Aplicar();
            BuscarSocios();
        }

        private void aplicar_Click(object sender, EventArgs e)
        {
            Aplicar();
        }

        private void Aplicar()
        {
            resultadoActual = null;
            exportarReporte.Enabled = false;
            if (desde.Value.Date > hasta.Value.Date)
            {
                lblEstado.Text = "Desde debe ser anterior o igual a Hasta.";
                lblEstado.ForeColor = Color.Firebrick;
                return;
            }
            try
            {
                Cursor = Cursors.WaitCursor;
                var datos = logica.Obtener(desde.Value.Date, hasta.Value.Date);
                lblIngresos.Text = datos.TotalIngresos.ToString("C2");
                lblPagos.Text = datos.CantidadPagos.ToString("N0");
                lblActivos.Text = datos.SociosActivos.ToString("N0");
                lblDeuda.Text = datos.SociosConDeuda.ToString("N0");
                lblComparacion.Text = "Período anterior: " + datos.IngresosPeriodoAnterior.ToString("C2") +
                    "   |   Variación: " + (datos.VariacionIngresosPorcentual.HasValue
                        ? datos.VariacionIngresosPorcentual.Value.ToString("+0.##;-0.##;0", CultureInfo.CurrentCulture) + "%"
                        : "— (anterior sin ingresos)");
                lblPorcentajeActivos.Text = "Total socios: " + datos.TotalSocios +
                    "   |   Activos: " + datos.SociosActivos +
                    "   |   Inactivos: " + (datos.TotalSocios - datos.SociosActivos) +
                    "   |   Activos: " + (datos.TotalSocios == 0 ? "0" :
                        (100m * datos.SociosActivos / datos.TotalSocios).ToString("0.##")) + "%";

                CargarMensual(chartIngresos, datos.IngresosPorMes);
                if (datos.IngresosPorMes.Count >= 2)
                    chartIngresos.Titles.Add("Último mes vs anterior: " + FormatearVariacion(datos.VariacionUltimoMesPorcentual));
                CargarTorta(chartMetodos, datos.MetodosPago, "Métodos de pago del período");
                CargarTorta(chartActivos, datos.EstadoSocios, "Socios activos e inactivos (actual)");
                CargarTorta(chartDeuda, datos.EstadoDeuda, "Estado de deuda (actual)");
                CargarBarras(chartPlanes, datos.Planes, SeriesChartType.Column, "Planes más contratados (actual)");
                CargarBarras(chartEntrenadores, datos.Entrenadores, SeriesChartType.Bar, "Socios por entrenador (actual)");
                CargarMensual(chartAltas, datos.AltasPorMes, "Altas de socios por mes", false);
                lblPorcentajeActivos.Text += Environment.NewLine + "Altas del período: " + datos.TotalAltas +
                    "   |   Sin fecha de alta: " + datos.SociosSinFechaAlta + " (excluidos de altas)";
                if (datos.AltasPorMes.Count >= 2)
                    chartAltas.Titles.Add("Último mes vs anterior: " + FormatearVariacion(datos.VariacionAltasUltimoMes));
                CargarImportes(chartIngresosPlan, datos.IngresosPorPlan);
                CargarBarras(chartRutinas, datos.Rutinas, SeriesChartType.Bar, "Rutinas más utilizadas: socios actuales");
                lblSinRutina.Text = "Socios activos sin rutina activa: " + datos.SinRutina;
                CargarBarras(chartEjercicios, datos.EjerciciosTop, SeriesChartType.Bar, "Top 10 ejercicios: rutinas distintas");
                gridEjerciciosSinUso.DataSource = datos.EjerciciosSinUso;
                lblEjerciciosSinUso.Text = "Ejercicios activos sin uso: " + datos.EjerciciosSinUso.Count;
                EstilizarGraficos();
                lblEstado.Text = "Actualizado. Ingresos y altas usan el período; socios, planes, entrenadores, rutinas y ejercicios muestran el estado actual.";
                lblEstado.ForeColor = Color.FromArgb(51, 65, 85);
                resultadoActual = datos;
                desdeAplicado = desde.Value.Date;
                hastaAplicado = hasta.Value.Date;
                exportarReporte.Enabled = idUsuarioGenerador > 0;
            }
            catch (Exception ex)
            {
                AyudaFormularioVisual.MostrarError(lblEstado, ex);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private static void CargarMensual(Chart chart, IEnumerable<DatoMensual> datos, string titulo = "Ingresos por mes", bool dinero = true)
        {
            chart.Series.Clear();
            chart.Titles.Clear();
            chart.Titles.Add(titulo);
            var serie = new Series(dinero ? "Ingresos" : "Altas")
            {
                ChartType = SeriesChartType.Line,
                BorderWidth = 3,
                Color = Color.FromArgb(79, 70, 229),
                MarkerStyle = MarkerStyle.Circle,
                MarkerSize = 7,
                IsValueShownAsLabel = true,
                LabelFormat = dinero ? "C0" : "N0"
            };
            serie.ToolTip = dinero ? "#VALX: #VALY{C2}" : "#VALX: #VALY{N0}";
            foreach (var dato in datos)
                serie.Points.AddXY(dato.Mes.ToString("MMM yy", CultureInfo.CurrentCulture), dato.Valor);
            chart.Series.Add(serie);
            chart.ChartAreas[0].AxisX.Interval = 1;
            chart.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            chart.ChartAreas[0].AxisY.Minimum = 0;
            chart.ChartAreas[0].AxisY.LabelStyle.Format = dinero ? "C0" : "N0";
            if (!datos.Any(d => d.Valor != 0))
                chart.Titles.Add("Sin datos para el período seleccionado");
        }

        private static void CargarTorta(Chart chart, IEnumerable<DatoCategoria> datos, string titulo)
        {
            chart.Series.Clear();
            chart.Titles.Clear();
            chart.Titles.Add(titulo);
            var valores = datos.Where(d => d.Cantidad > 0).ToList();
            if (valores.Count == 0)
            {
                chart.Titles.Add("Sin datos para el período seleccionado");
                return;
            }
            var serie = new Series("Distribución")
            {
                ChartType = SeriesChartType.Pie,
                IsValueShownAsLabel = true,
                Label = "#PERCENT{P0}",
                LegendText = "#VALX"
            };
            serie.ToolTip = "#VALX: #VALY (#PERCENT{P1})";
            foreach (var dato in valores)
                serie.Points.AddXY(dato.Nombre, dato.Cantidad);
            chart.Series.Add(serie);
        }

        private static void CargarBarras(Chart chart, IEnumerable<DatoCategoria> datos, SeriesChartType tipo, string titulo)
        {
            chart.Series.Clear();
            chart.Titles.Clear();
            chart.Titles.Add(titulo);
            var valores = datos.Where(d => d.Cantidad > 0).ToList();
            if (valores.Count == 0)
            {
                chart.Titles.Add("Sin datos para el período seleccionado");
                return;
            }
            var serie = new Series("Socios")
            {
                ChartType = tipo,
                Color = Color.FromArgb(79, 70, 229),
                IsValueShownAsLabel = true
            };
            serie.ToolTip = "#VALX: #VALY";
            // En barras horizontales se invierte la inserción para conservar el mayor arriba.
            foreach (var dato in tipo == SeriesChartType.Bar ? valores.AsEnumerable().Reverse() : valores)
                serie.Points.AddXY(dato.Nombre, dato.Cantidad);
            chart.Series.Add(serie);
            if (tipo == SeriesChartType.Bar)
            {
                chart.ChartAreas[0].AxisY.Interval = 1;
                chart.ChartAreas[0].AxisY.LabelStyle.Angle = 0;
                chart.ChartAreas[0].AxisX.Minimum = 0;
                chart.ChartAreas[0].AxisX.LabelStyle.Format = "N0";
            }
            else
            {
                chart.ChartAreas[0].AxisX.Interval = 1;
                chart.ChartAreas[0].AxisX.LabelStyle.Angle = -35;
                chart.ChartAreas[0].AxisY.Minimum = 0;
                chart.ChartAreas[0].AxisY.LabelStyle.Format = "N0";
            }
        }
    }
}
