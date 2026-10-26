using exxen2._0.capaLogica.Navegacion;
using exxen2._0.capaVisual.Compartido.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using exxen2._0.capaLogica;

namespace exxen2._0.capaVisual.Compartido.Controles
{
    [DesignerCategory("UserControl")]
    public partial class EstadoSociosControl : UserControl
    {
        private readonly EstadoSociosLogica logica = new EstadoSociosLogica();
        private List<EstadoSocioDashboard> socios = new List<EstadoSocioDashboard>();
        private bool cargando;

        public event EventHandler<AccionEstadoSocioEventArgs> AccionSolicitada;

        public EstadoSociosControl()
        {
            InitializeComponent();
            filtroEstado.SelectedIndex = 0;
        }

        public void Actualizar()
        {
            if (AyudaFormularioVisual.EnModoDisenio(this)) return;
            try
            {
                var resumen = logica.ObtenerResumen();
                socios = resumen.Socios;
                valorActivos.Text = resumen.SociosActivos.ToString();
                valorInactivos.Text = resumen.SociosInactivos.ToString();
                valorAlDia.Text = resumen.AlDia.ToString();
                valorConDeuda.Text = resumen.ConDeuda.ToString();
                valorLimiteAlcanzado.Text = resumen.LimiteAlcanzado.ToString();
                valorPendientes.Text = resumen.CuotasPendientes.ToString();
                var avisos = new List<string>();
                if (resumen.CuotasVencidas > 0)
                    avisos.Add(resumen.CuotasVencidas + " cuotas vencidas");
                if (resumen.VencenEnPlazoAviso > 0)
                    avisos.Add(resumen.VencenEnPlazoAviso + " cuotas por vencer en " + resumen.DiasAvisoVencimiento + " días (incluye " + resumen.VencenHoy + " hoy)");
                alertas.Text = avisos.Count == 0 ? "Sin avisos pendientes" : string.Join("  ·  ", avisos);
                AplicarFiltro();
            }
            catch (Exception ex)
            {
                socios.Clear();
                AplicarFiltro();
                foreach (var valor in new[] { valorActivos, valorInactivos, valorAlDia, valorConDeuda, valorLimiteAlcanzado, valorPendientes })
                    valor.Text = "—";
                alertas.Text = "No se pudo cargar el estado de socios: " + AyudaFormularioVisual.ObtenerMensajeError(ex);
            }
        }

        private void AplicarFiltro()
        {
            if (socios == null) return;
            var texto = buscador.Text.Trim();
            var filtro = Convert.ToString(filtroEstado.SelectedItem);
            IEnumerable<EstadoSocioDashboard> resultado = socios;
            if (filtro == "Al día") resultado = resultado.Where(s => s.Activo && s.CuotasVencidas == 0);
            else if (filtro == "Con deuda") resultado = resultado.Where(s => s.Activo && s.CuotasVencidas > 0 && !s.LimiteAlcanzado);
            else if (filtro == "1 cuota vencida") resultado = resultado.Where(s => s.CuotasVencidas == 1);
            else if (filtro == "Límite alcanzado") resultado = resultado.Where(s => s.LimiteAlcanzado);
            else if (filtro == "Inactivos") resultado = resultado.Where(s => !s.Activo && !s.LimiteAlcanzado);
            else if (filtro == "Activos") resultado = resultado.Where(s => s.Activo);
            else if (filtro == "Cuotas pendientes") resultado = resultado.Where(s => s.CuotasPendientes > 0);
            else if (filtro == "Cuotas vencidas") resultado = resultado.Where(s => s.CuotasVencidas > 0);
            else if (filtro == "Cuotas por vencer") resultado = resultado.Where(s => s.CuotasPorVencer > 0);
            if (!string.IsNullOrWhiteSpace(texto))
                resultado = resultado.Where(s => Contiene(s.Socio, texto) || Contiene(s.DNI, texto));
            cargando = true;
            filas.Rows.Clear();
            foreach (var socio in resultado)
                filas.Rows.Add(socio.IdSocio, socio.Socio, socio.DNI, socio.Plan, socio.EstadoMembresia,
                    socio.EstadoPago, socio.CuotasVencidas, socio.DeudaTotal,
                    socio.UltimoPago, socio.ProximoVencimiento);
            filas.ClearSelection();
            filas.CurrentCell = null;
            cargando = false;
            ActualizarAcciones();
            cantidad.Text = filas.Rows.Count == 1 ? "1 socio" : filas.Rows.Count + " socios";
        }

        private void ActualizarAcciones()
        {
            var seleccionado = filas.SelectedRows.Count == 1;
            btnPago.Enabled = seleccionado;
            btnFicha.Enabled = seleccionado;
            verCuotasMenu.Enabled = seleccionado;
            verMembresiaMenu.Enabled = seleccionado;
        }

        private void SolicitarAccion(AccionEstadoSocio accion)
        {
            if (filas.SelectedRows.Count != 1) return;
            var evento = AccionSolicitada;
            if (evento != null) evento(this, new AccionEstadoSocioEventArgs(Convert.ToInt32(filas.SelectedRows[0].Cells["colIdSocio"].Value), accion));
        }

        private static bool Contiene(string valor, string texto) { return !string.IsNullOrEmpty(valor) && valor.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0; }
        private void filtroEstado_SelectedIndexChanged(object sender, EventArgs e) { AplicarFiltro(); }
        private void buscador_TextChanged(object sender, EventArgs e) { AplicarFiltro(); }
        private void filas_SelectionChanged(object sender, EventArgs e) { if (!cargando) ActualizarAcciones(); }
        private void btnPago_Click(object sender, EventArgs e) { SolicitarAccion(AccionEstadoSocio.RegistrarPago); }

        private void btnFicha_Click(object sender, EventArgs e) { SolicitarAccion(AccionEstadoSocio.VerFicha); }
        private void verCuotasMenu_Click(object sender, EventArgs e) { SolicitarAccion(AccionEstadoSocio.VerCuotas); }
        private void verMembresiaMenu_Click(object sender, EventArgs e) { SolicitarAccion(AccionEstadoSocio.VerMembresia); }
        private void filas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            filas.ClearSelection();
            filas.Rows[e.RowIndex].Selected = true;
            SolicitarAccion(AccionEstadoSocio.VerFicha);
        }
        private void filas_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right || e.RowIndex < 0) return;
            filas.ClearSelection();
            filas.Rows[e.RowIndex].Selected = true;
        }

        private void tarjeta_Click(object sender, EventArgs e)
        {
            var control = sender as Control;
            var filtro = control == null ? null : Convert.ToString(control.Tag);
            if (string.IsNullOrEmpty(filtro)) return;
            var indice = filtroEstado.Items.IndexOf(filtro);
            if (indice >= 0) filtroEstado.SelectedIndex = indice;
        }
    }
}
