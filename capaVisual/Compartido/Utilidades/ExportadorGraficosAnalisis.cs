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
            if (!original.Series.SelectMany(s => s.Points).Any(p => p.YValues.Any(y => y != 0))) return null;
            using (var configuracion = new MemoryStream())
            using (var imagen = new MemoryStream())
            using (var fuente = new Font("Arial", 18F))
            using (var titulo = new Font("Arial", 20F, FontStyle.Bold))
            using (var copia = new Chart())
            {
                original.Serializer.Save(configuracion);
                configuracion.Position = 0;
                copia.Serializer.Load(configuracion);
                copia.Size = new Size(1200, 675);
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
                foreach (var serie in copia.Series) { serie.Font = fuente; serie.MarkerSize = Math.Max(serie.MarkerSize, 12); }
                copia.SaveImage(imagen, ChartImageFormat.Png);
                return imagen.ToArray();
            }
        }
    }
}
