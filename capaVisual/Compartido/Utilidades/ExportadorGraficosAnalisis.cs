using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;

namespace exxen2._0.capaVisual.Compartido.Utilidades
{
    internal static class ExportadorGraficosAnalisis
    {
        // Serializa datos/estilo existentes en una copia sin Parent. Nunca redimensiona el Chart visible.
        internal static byte[] Renderizar(Chart original)
        {
            var valores = original.Series.SelectMany(s => s.Points)
                .SelectMany(p => p.YValues)
                .ToList();
            if (valores.Any(y => !EsNumeroFinito(y)))
                throw new InvalidOperationException("El gráfico contiene un valor no finito y no se puede exportar.");
            if (!valores.Any(y => y != 0)) return null;
            using (var configuracion = new MemoryStream())
            using (var imagen = new MemoryStream())
            using (var fuente = new Font("Arial", 18F))
            using (var titulo = new Font("Arial", 20F, FontStyle.Bold))
            using (var copia = new Chart())
            {
                copia.Size = new Size(1200, 675);
                original.Serializer.Save(configuracion);
                configuracion.Position = 0;
                copia.Serializer.Load(configuracion);
                copia.BackColor = Color.White;
                copia.Font = fuente;
                copia.AntiAliasing = AntiAliasingStyles.All;
                copia.TextAntiAliasingQuality = TextAntiAliasingQuality.High;
                foreach (var area in copia.ChartAreas)
                {
                    area.AxisX.IsLabelAutoFit = false;
                    area.AxisY.IsLabelAutoFit = false;
                    area.AxisX.LabelStyle.Font = copia.Font;
                    area.AxisY.LabelStyle.Font = copia.Font;
                }
                foreach (var texto in copia.Titles) texto.Font = titulo;
                foreach (var leyenda in copia.Legends) { leyenda.Font = fuente; leyenda.IsTextAutoFit = false; }
                foreach (var serie in copia.Series)
                {
                    serie.Font = fuente;
                    serie.MarkerSize = Math.Max(serie.MarkerSize, 12);
                    if (serie.ChartType == SeriesChartType.Line && serie.IsValueShownAsLabel)
                    {
                        // Reaplica la posición tras aumentar el tamaño del marcador para el PNG.
                        serie.SmartLabelStyle.Enabled = true;
                        serie.SmartLabelStyle.IsMarkerOverlappingAllowed = false;
                        serie.SmartLabelStyle.MovingDirection = LabelAlignmentStyles.Top;
                        serie.SmartLabelStyle.MaxMovingDistance = 100;
                    }
                }
                foreach (var area in copia.ChartAreas)
                {
                    var maximos = copia.Series.Where(s => s.ChartType == SeriesChartType.Line)
                        .SelectMany(s => s.Points).Where(p => p.YValues.Length > 0)
                        .Select(p => p.YValues[0]).Where(EsNumeroFinito).ToList();
                    var maximo = maximos.Count == 0 ? 0 : maximos.Max();
                    if (maximo > 0 && EsNumeroFinito(maximo * 1.2))
                    {
                        area.AxisY.Minimum = 0;
                        area.AxisY.Maximum = maximo * 1.2;
                    }
                    else
                    {
                        area.AxisY.Minimum = double.NaN;
                        area.AxisY.Maximum = double.NaN;
                    }
                }
                copia.CreateControl();
                copia.SaveImage(imagen, ChartImageFormat.Png);
                return imagen.ToArray();
            }
        }

        private static bool EsNumeroFinito(double valor)
        {
            return !double.IsNaN(valor) && !double.IsInfinity(valor);
        }
    }
}
