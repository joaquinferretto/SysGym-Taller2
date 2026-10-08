using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using exxen2._0.capaLogica.Analisis;
using exxen2._0.capaLogica.Reportes;
using exxen2._0.capaLogica.Utilidades;
using exxen2._0.capaVisual.Compartido;

namespace exxen2._0.capaVisual.Administrador
{
    public partial class AnalisisFormulario
    {
        private readonly ReporteAnalisisServicio reporte;
        private readonly int idUsuarioGenerador;
        private ResultadoAnalisis resultadoActual;
        private DateTime desdeAplicado, hastaAplicado;
        private ComparacionAnalisisPdf comparacionActual;

        private void periodoReporte_Cambiado(object sender, EventArgs e)
        {
            exportarReporte.Enabled = resultadoActual != null && idUsuarioGenerador > 0 &&
                desde.Value.Date == desdeAplicado && hasta.Value.Date == hastaAplicado;
        }

        private void periodoComparacion_Cambiado(object sender, EventArgs e) { comparacionActual = null; }

        private void exportarReporte_Click(object sender, EventArgs e)
        {
            try
            {
                ValidarAnalisisExportable();
                reporte.ValidarAcceso();
                using (var dialogo = new SaveFileDialog {
                    Title = "Exportar reporte de análisis", Filter = "PDF (*.pdf)|*.pdf",
                    DefaultExt = "pdf", AddExtension = true, OverwritePrompt = true,
                    FileName = ReporteAnalisisServicio.NombreSugerido(desdeAplicado, hastaAplicado) })
                {
                    if (dialogo.ShowDialog(this) != DialogResult.OK) return;
                    Cursor = Cursors.WaitCursor;
                    ExportarActual(dialogo.FileName);
                    lblEstado.Text = "Reporte PDF guardado en " + dialogo.FileName;
                }
            }
            catch (Exception ex) { AyudaFormularioVisual.MostrarError(lblEstado, ex); }
            finally { Cursor = Cursors.Default; }
        }

        private void ValidarAnalisisExportable()
        {
            if (resultadoActual == null || desde.Value.Date != desdeAplicado || hasta.Value.Date != hastaAplicado)
                throw new InvalidOperationException("Aplicá el período seleccionado antes de exportar.");
        }

        private void ExportarActual(string destino)
        {
            ValidarAnalisisExportable();
            var imagenes = new Dictionary<string, byte[]>();
            AgregarGrafico(imagenes, "Ingresos", chartIngresos);
            AgregarGrafico(imagenes, "Activos", chartActivos);
            AgregarGrafico(imagenes, "Deuda", chartDeuda);
            AgregarGrafico(imagenes, "Altas", chartAltas);
            AgregarGrafico(imagenes, "Planes", chartPlanes);
            AgregarGrafico(imagenes, "IngresosPlan", chartIngresosPlan);
            AgregarGrafico(imagenes, "Entrenadores", chartEntrenadores);
            AgregarGrafico(imagenes, "Metodos", chartMetodos);
            AgregarGrafico(imagenes, "Rutinas", chartRutinas);
            AgregarGrafico(imagenes, "Ejercicios", chartEjercicios);
            reporte.Generar(resultadoActual, desdeAplicado, hastaAplicado, imagenes, comparacionActual, destino);
        }

        private static void AgregarGrafico(Dictionary<string, byte[]> imagenes, string nombre, Chart chart)
        {
            var png = ExportadorGraficosAnalisis.Renderizar(chart);
            if (png != null) imagenes.Add(nombre, png);
        }
    }
}
