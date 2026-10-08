using System;
using System.ComponentModel;
using System.Windows.Forms;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaLogica.Reportes;
using exxen2._0.capaLogica.Utilidades;
using exxen2._0.capaVisual.Compartido.Controles;

namespace exxen2._0.capaVisual.Administrador
{
    [DesignerCategory("Form")]
    public partial class ReportesFormulario : Form
    {
        private readonly ReportesOperativosLogica logica;
        private readonly ReportesOperativosServicio servicio;
        private ResultadoReporteOperativo resultadoActual;
        private bool cargando;
        private bool autorizado;
        public event EventHandler<AccionEstadoSocioEventArgs> FichaSolicitada;
        public ReportesFormulario() : this(null) { }
        public ReportesFormulario(UsuarioSistema usuario)
        {
            var id = usuario == null ? 0 : usuario.IdUsuarioSistema;
            logica = new ReportesOperativosLogica(id);
            servicio = new ReportesOperativosServicio(id);
            InitializeComponent();
        }
        private void ReportesFormulario_Load(object sender, EventArgs e)
        {
            if (AyudaFormularioVisual.EnModoDisenio(this)) return;
            try
            {
                logica.ValidarAcceso(); autorizado = true;
                cargando = true;
                desde.Value = DateTime.Today; hasta.Value = DateTime.Today;
                desde.Checked = false; hasta.Checked = false;
                tipoReporte.SelectedIndex = 0;
                cargando = false;
                Consultar();
            }
            catch (Exception ex) { cargando = false; lblEstado.Text = "No se pudo cargar Reportes: " + ex.Message; }
        }
        private void tipoReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cargando || !autorizado) return;
            Consultar();
        }
        private void generar_Click(object sender, EventArgs e) { Consultar(); }
        private void Consultar()
        {
            if (!autorizado || tipoReporte.SelectedIndex < 0) return;
            Invalidar();
            var vencimientos = tipoReporte.SelectedIndex == (int)TipoReporteOperativo.Vencimientos;
            panelFechas.Visible = vencimientos;
            disposicion.RowStyles[3].Height = vencimientos ? 44F : 0F;
            try
            {
                resultadoActual = logica.Consultar((TipoReporteOperativo)tipoReporte.SelectedIndex, buscador.Text,
                    vencimientos && desde.Checked ? (DateTime?)desde.Value.Date : null,
                    vencimientos && hasta.Checked ? (DateTime?)hasta.Value.Date : null);
                for (var i = 0; i < tabla.Columns.Count; i++)
                {
                    tabla.Columns[i].Visible = i < resultadoActual.Columnas.Length;
                    if (i < resultadoActual.Columnas.Length) tabla.Columns[i].HeaderText = resultadoActual.Columnas[i];
                }
                foreach (var fila in resultadoActual.Filas)
                {
                    var valores = new object[tabla.Columns.Count];
                    Array.Copy(fila.Valores, valores, fila.Valores.Length);
                    var indice = tabla.Rows.Add(valores);
                    tabla.Rows[indice].Tag = fila.IdSocio;
                }
                tabla.ClearSelection(); btnFicha.Enabled = false;
                lblResumen.Text = resultadoActual.Resumen;
                lblEstado.Text = resultadoActual.Filas.Count == 0 ? "Sin resultados para los filtros seleccionados." : "Consulta: " + resultadoActual.FechaConsulta.ToString("dd/MM/yyyy HH:mm");
                btnExportar.Enabled = resultadoActual.Filas.Count > 0;
                lblCriterio.Text = resultadoActual.Filtros;
                if (vencimientos)
                {
                    cargando = true;
                    if (!desde.Checked) desde.Value = resultadoActual.Desde.Value;
                    if (!hasta.Checked) hasta.Value = resultadoActual.Hasta.Value;
                    cargando = false;
                }
                var indicadores = logica.ObtenerIndicadores();
                lblSociosActivosValor.Text = "Socios activos: " + indicadores.Socios;
                lblUsuariosActivosValor.Text = "Usuarios activos: " + indicadores.Usuarios;
                lblMembresiasValor.Text = "Membresías habilitadas: " + indicadores.Membresias;
                lblRutinasActivasValor.Text = "Rutinas activas: " + indicadores.Rutinas;
                lblEjerciciosValor.Text = "Ejercicios disponibles: " + indicadores.Ejercicios;
            }
            catch (Exception ex) { cargando = false; Invalidar(); lblEstado.Text = "No se pudo consultar: " + ex.Message; }
        }
        private void filtros_Cambiados(object sender, EventArgs e)
        {
            if (cargando || !autorizado) return;
            Invalidar(); lblEstado.Text = "Filtros pendientes. Presioná Consultar.";
        }
        private void Invalidar()
        {
            resultadoActual = null; btnExportar.Enabled = false; btnFicha.Enabled = false;
            tabla.Rows.Clear(); lblResumen.Text = string.Empty;
        }
        private void tabla_SelectionChanged(object sender, EventArgs e)
        {
            btnFicha.Enabled = resultadoActual != null && tabla.CurrentRow != null && tabla.CurrentRow.Selected && FichaSolicitada != null;
        }
        private void btnFicha_Click(object sender, EventArgs e)
        {
            if (tabla.CurrentRow == null || !tabla.CurrentRow.Selected || resultadoActual == null) return;
            var evento = FichaSolicitada;
            if (evento != null) evento(this, new AccionEstadoSocioEventArgs(Convert.ToInt32(tabla.CurrentRow.Tag), AccionEstadoSocio.VerFicha));
        }
        private void tabla_CellDoubleClick(object sender, DataGridViewCellEventArgs e) { if (e.RowIndex >= 0) btnFicha_Click(sender, EventArgs.Empty); }
        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (resultadoActual == null || resultadoActual.Filas.Count == 0) return;
            try
            {
                dialogoPdf.FileName = servicio.NombreSugerido(resultadoActual);
                if (dialogoPdf.ShowDialog(this) != DialogResult.OK) return;
                ExportarActual(dialogoPdf.FileName);
                lblEstado.Text = "Reporte PDF guardado correctamente.";
            }
            catch (Exception ex) { lblEstado.Text = "No se pudo exportar: " + ex.Message; }
        }
        private void ExportarActual(string destino)
        {
            if (resultadoActual == null || resultadoActual.Filas.Count == 0) throw new InvalidOperationException("Consultá un reporte con resultados antes de exportar.");
            servicio.Generar(resultadoActual, destino);
        }
    }
}
