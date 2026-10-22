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
                leyenda.LegendStyle = grafico.Series.Any(s => s.ChartType == SeriesChartType.Pie)
                    ? LegendStyle.Column : LegendStyle.Row;
            }

            var esCircular = grafico.Series.Any(serie => serie.ChartType == SeriesChartType.Pie || serie.ChartType == SeriesChartType.Doughnut);
            foreach (ChartArea area in grafico.ChartAreas)
            {
                area.BackColor = Color.White;
                area.AxisX.LabelStyle.Font = FuenteEjesGrafico;
                area.AxisY.LabelStyle.Font = FuenteEjesGrafico;
                area.AxisX.LineColor = Color.FromArgb(203, 213, 225);
                area.AxisY.LineColor = Color.FromArgb(203, 213, 225);
                var tieneDatos = grafico.Series.Any(s => s.Points.Count > 0);
                area.AxisX.Enabled = tieneDatos && !esCircular ? AxisEnabled.Auto : AxisEnabled.False;
                area.AxisY.Enabled = tieneDatos && !esCircular ? AxisEnabled.Auto : AxisEnabled.False;
                area.AxisX.MajorGrid.Enabled = false;
                area.AxisY.MajorGrid.Enabled = !esCircular;
                area.AxisX.MajorTickMark.Enabled = !esCircular;
                area.AxisY.MajorTickMark.Enabled = !esCircular;
                area.AxisX.IsLabelAutoFit = false;
                area.AxisY.IsLabelAutoFit = false;
                area.AxisX.LabelAutoFitStyle = LabelAutoFitStyles.None;
                area.AxisY.LabelAutoFitStyle = LabelAutoFitStyles.None;
                area.AxisX.LabelStyle.IsStaggered = false;
                area.AxisY.LabelStyle.IsStaggered = false;
                area.AxisX.LabelAutoFitMinFontSize = 8;
                area.AxisX.LabelAutoFitMaxFontSize = 10;
                area.AxisX.Minimum = double.NaN;
                area.AxisY.Minimum = double.NaN;
                if (area.AxisY.LabelStyle.Format == "C0")
                {
                    var maximo = grafico.Series.SelectMany(s => s.Points).Select(p => p.YValues[0]).DefaultIfEmpty().Max();
                    if (maximo > 0)
                    {
                        var escala = Math.Pow(10, Math.Floor(Math.Log10(maximo / 3)));
                        var paso = maximo / 3 / escala;
                        area.AxisY.Interval = (paso <= 1 ? 1 : paso <= 2 ? 2 : paso <= 5 ? 5 : 10) * escala;
                    }
                }
                area.AxisX.MajorGrid.LineColor = Color.FromArgb(226, 232, 240);
                area.AxisY.MajorGrid.LineColor = Color.FromArgb(226, 232, 240);
                area.AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
                area.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
            }
            foreach (Series serie in grafico.Series)
            {
                serie.Font = FuenteDatosGrafico;
                serie.LabelForeColor = Color.FromArgb(51, 65, 85);
                if (serie.ChartType == SeriesChartType.Bar || serie.ChartType == SeriesChartType.Column)
                {
                    serie["PointWidth"] = "0.8";
                    serie["MaxPixelPointWidth"] = "54";
                    serie.IsVisibleInLegend = grafico.Series.Count > 1;
                }
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
                resultadoActual = datos;
                PresentarResultado(datos);
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

        private void PresentarResultado(ResultadoAnalisis resultado)
        {
            lblIngresos.Text = resultado.TotalIngresos.ToString("C2");
            lblPagos.Text = resultado.CantidadPagos.ToString("N0");
            lblActivos.Text = resultado.SociosActivos.ToString("N0");
            lblDeuda.Text = resultado.SociosConDeuda.ToString("N0");
            lblComparacion.Text = "Período anterior: " + resultado.IngresosPeriodoAnterior.ToString("C2") +
                "   |   Variación: " + (resultado.VariacionIngresosPorcentual.HasValue
                    ? resultado.VariacionIngresosPorcentual.Value.ToString("+0.00;-0.00;0.00", CultureInfo.CurrentCulture) + " %"
                    : "— (anterior sin ingresos)");
            lblPorcentajeActivos.Text = "Total socios: " + resultado.TotalSocios +
                "   |   Activos: " + resultado.SociosActivos +
                "   |   Inactivos: " + (resultado.TotalSocios - resultado.SociosActivos) +
                "   |   Activos: " + (resultado.TotalSocios == 0 ? "0" :
                    (100m * resultado.SociosActivos / resultado.TotalSocios).ToString("0.##")) + "%";

            CargarMensual(chartIngresos, resultado.IngresosPorMes);
            if (resultado.IngresosPorMes.Count >= 2)
                chartIngresos.Titles.Add("Último mes vs anterior: " + FormatearVariacion(resultado.VariacionUltimoMesPorcentual));
            CargarTorta(chartMetodos, resultado.MetodosPago, "Métodos de pago del período");
            CargarTorta(chartActivos, resultado.EstadoSocios, "Socios: estado actual");
            CargarTorta(chartDeuda, resultado.EstadoDeuda, "Deuda: estado actual");
            CargarBarras(chartPlanes, resultado.Planes, SeriesChartType.Column, "Planes más contratados (actual)");
            CargarBarras(chartEntrenadores, resultado.Entrenadores, SeriesChartType.Column, "Socios por entrenador (actual)");
            CargarMensual(chartAltas, resultado.AltasPorMes, "Altas de socios por mes", false);
            lblPorcentajeActivos.Text += Environment.NewLine + "Altas del período: " + resultado.TotalAltas +
                "   |   Sin fecha de alta: " + resultado.SociosSinFechaAlta + " (excluidos de altas)";
            if (resultado.AltasPorMes.Count >= 2)
                chartAltas.Titles.Add("Último mes vs anterior: " + FormatearVariacion(resultado.VariacionAltasUltimoMes));
            CargarImportes(chartIngresosPlan, resultado.IngresosPorPlan);
            CargarBarras(chartRutinas, resultado.Rutinas.OrderByDescending(d => d.Cantidad).ThenBy(d => d.Nombre).Take(10), SeriesChartType.Column, "Top 10 rutinas más utilizadas (socios actuales)");
            lblSinRutina.Text = "Socios activos sin rutina activa: " + resultado.SinRutina;
            CargarBarras(chartEjercicios, resultado.EjerciciosTop, SeriesChartType.Column, "Top 10 ejercicios: rutinas distintas");
            gridEjerciciosSinUso.DataSource = resultado.EjerciciosSinUso;
            if (gridEjerciciosSinUso.Columns.Contains("Descripcion"))
                gridEjerciciosSinUso.Columns["Descripcion"].HeaderText = "Descripción";
            lblEjerciciosSinUso.Text = "Ejercicios activos sin uso: " + resultado.EjerciciosSinUso.Count;
            EstilizarGraficos();
            lblEstado.Text = "Actualizado. Ingresos y altas usan el período; socios, planes, entrenadores, rutinas y ejercicios muestran el estado actual.";
            lblEstado.ForeColor = Color.FromArgb(51, 65, 85);
        }

        private static void CargarMensual(Chart chart, IEnumerable<DatoMensual> datos, string titulo = "Ingresos por mes", bool dinero = true)
        {
            chart.Series.Clear();
            chart.Annotations.Clear();
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
                LabelFormat = dinero ? "C0" : "N0",
                SmartLabelStyle = { Enabled = true, IsMarkerOverlappingAllowed = false, MovingDirection = LabelAlignmentStyles.Top, MaxMovingDistance = 100 }
            };
            serie.ToolTip = dinero ? "#VALX: #VALY{C2}" : "#VALX: #VALY{N0}";
            foreach (var dato in datos)
                serie.Points.AddXY(dato.Mes.ToString("MMM yy", CultureInfo.CurrentCulture), dato.Valor);
            chart.Series.Add(serie);
            chart.ChartAreas[0].AxisX.Interval = 1;
            chart.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            chart.ChartAreas[0].AxisY.Minimum = 0;
            var maximo = serie.Points.Select(p => p.YValues[0]).DefaultIfEmpty().Max();
            chart.ChartAreas[0].AxisY.Maximum = maximo > 0 ? maximo * 1.2 : double.NaN;
            chart.ChartAreas[0].AxisY.LabelStyle.Format = dinero ? "C0" : "N0";
            if (!datos.Any(d => d.Valor != 0))
                MostrarSinDatos(chart);
        }

        private static void CargarTorta(Chart chart, IEnumerable<DatoCategoria> datos, string titulo)
        {
            chart.Series.Clear();
            chart.Annotations.Clear();
            chart.Titles.Clear();
            chart.Titles.Add(titulo);
            var valores = datos.Where(d => d.Cantidad > 0).ToList();
            if (valores.Count == 0)
            {
                MostrarSinDatos(chart);
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
            chart.Annotations.Clear();
            chart.Titles.Clear();
            chart.Titles.Add(titulo);
            var valores = datos.Where(d => d.Cantidad > 0).ToList();
            if (valores.Count == 0)
            {
                MostrarSinDatos(chart);
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
                chart.ChartAreas[0].AxisY.LabelStyle.Interval = 1;
                chart.ChartAreas[0].AxisY.LabelStyle.IsStaggered = false;
                chart.ChartAreas[0].AxisY.IsLabelAutoFit = false;
                chart.ChartAreas[0].AxisY.LabelAutoFitStyle = LabelAutoFitStyles.None;
                chart.ChartAreas[0].AxisY.Minimum = double.NaN;
                chart.ChartAreas[0].AxisY.LabelStyle.Format = string.Empty;
                chart.ChartAreas[0].AxisX.Minimum = 0;
                var intervaloValores = CalcularIntervaloEje(valores.Max(d => d.Cantidad));
                chart.ChartAreas[0].AxisX.Interval = intervaloValores;
                chart.ChartAreas[0].AxisX.LabelStyle.Interval = intervaloValores;
                chart.ChartAreas[0].AxisX.MajorTickMark.Interval = intervaloValores;
                chart.ChartAreas[0].AxisX.LabelStyle.Format = "N0";
            }
            else
            {
                chart.ChartAreas[0].AxisX.Interval = 1;
                chart.ChartAreas[0].AxisX.LabelStyle.Interval = 1;
                chart.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
                chart.ChartAreas[0].AxisX.LabelStyle.IsStaggered = false;
                chart.ChartAreas[0].AxisY.Minimum = 0;
                chart.ChartAreas[0].AxisY.LabelStyle.Format = "N0";
            }
        }

        private static double CalcularIntervaloEje(double maximo)
        {
            var intervaloBase = maximo / 5d;
            var magnitud = Math.Pow(10, Math.Floor(Math.Log10(intervaloBase)));
            var normalizado = intervaloBase / magnitud;
            return (normalizado <= 1 ? 1 : normalizado <= 2 ? 2 : normalizado <= 5 ? 5 : 10) * magnitud;
        }

        private static void MostrarSinDatos(Chart chart, string texto = "Sin datos para esta consulta")
        {
            chart.Annotations.Clear();
            chart.Annotations.Add(new TextAnnotation
            {
                Text = texto, X = 10, Y = 42, Width = 80, Height = 16,
                Font = FuenteEjesGrafico, ForeColor = Color.FromArgb(100, 116, 139),
                Alignment = ContentAlignment.MiddleCenter
            });
        }
    }
}
