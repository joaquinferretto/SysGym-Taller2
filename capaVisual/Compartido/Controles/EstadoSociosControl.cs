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
                if (resumen.LimiteAlcanzado > 0)
                    avisos.Add(resumen.LimiteAlcanzado + " socios alcanzaron el límite de deuda");
                if (resumen.VencenHoy > 0)
                    avisos.Add(resumen.VencenHoy + " cuotas vencen hoy");
                if (resumen.VencenEnPlazoAviso > 0)
                    avisos.Add(resumen.VencenEnPlazoAviso + " cuotas vencen entre hoy y los próximos " + resumen.DiasAvisoVencimiento + " días");
                if (resumen.ConDeuda > 0)
                    avisos.Add(resumen.ConDeuda + " socios tienen deuda debajo del límite");
                alertas.Text = avisos.Count == 0 ? "Sin avisos pendientes" : string.Join("  ·  ", avisos);
                AplicarFiltro();
            }
            catch (Exception ex)
            {
                filas.Rows.Clear();
                alertas.Text = "No se pudo cargar el estado de socios: " + ex.Message;
            }
        }

        private void AplicarFiltro()
        {
            if (socios == null) return;
            var texto = buscador.Text.Trim();
            var filtro = Convert.ToString(filtroEstado.SelectedItem);
            IEnumerable<EstadoSocioDashboard> resultado = socios;
            if (filtro == "Al día") resultado = resultado.Where(s => s.EstadoDeuda == "Al día");
            else if (filtro == "Con deuda") resultado = resultado.Where(s => s.CuotasVencidas > 0 && !s.LimiteAlcanzado);
            else if (filtro == "1 cuota vencida") resultado = resultado.Where(s => s.CuotasVencidas == 1);
            else if (filtro == "Límite alcanzado") resultado = resultado.Where(s => s.LimiteAlcanzado);
            else if (filtro == "Inactivos") resultado = resultado.Where(s => !s.Activo);
            if (!string.IsNullOrWhiteSpace(texto))
                resultado = resultado.Where(s => Contiene(s.Socio, texto) || Contiene(s.DNI, texto));
            cargando = true;
            filas.Rows.Clear();
            foreach (var socio in resultado)
                filas.Rows.Add(socio.IdSocio, socio.Socio, socio.DNI, socio.Plan, socio.EstadoMembresia,
                    socio.EstadoPago, socio.CuotasVencidas, socio.DeudaTotal.ToString("C"),
                    Fecha(socio.UltimoPago), Fecha(socio.ProximoVencimiento));
            filas.ClearSelection();
            cargando = false;
            ActualizarAcciones();
            cantidad.Text = filas.Rows.Count == 1 ? "1 socio" : filas.Rows.Count + " socios";
        }

        private void ActualizarAcciones()
        {
            var seleccionado = filas.CurrentRow != null && filas.CurrentRow.Selected;
            btnCuotas.Enabled = seleccionado;
            btnPago.Enabled = seleccionado;
            btnMembresia.Enabled = seleccionado;
            btnFicha.Enabled = seleccionado;
        }

        private void SolicitarAccion(AccionEstadoSocio accion)
        {
            if (filas.CurrentRow == null) return;
            var evento = AccionSolicitada;
            if (evento != null) evento(this, new AccionEstadoSocioEventArgs(Convert.ToInt32(filas.CurrentRow.Cells["colIdSocio"].Value), accion));
        }

        private static string Fecha(DateTime? fecha) { return fecha.HasValue ? fecha.Value.ToString("dd/MM/yyyy") : "-"; }
        private static bool Contiene(string valor, string texto) { return !string.IsNullOrEmpty(valor) && valor.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0; }
        private void filtroEstado_SelectedIndexChanged(object sender, EventArgs e) { AplicarFiltro(); }
        private void buscador_TextChanged(object sender, EventArgs e) { AplicarFiltro(); }
        private void filas_SelectionChanged(object sender, EventArgs e) { if (!cargando) ActualizarAcciones(); }
        private void btnCuotas_Click(object sender, EventArgs e) { SolicitarAccion(AccionEstadoSocio.VerCuotas); }
        private void btnPago_Click(object sender, EventArgs e) { SolicitarAccion(AccionEstadoSocio.RegistrarPago); }
        private void btnMembresia_Click(object sender, EventArgs e) { SolicitarAccion(AccionEstadoSocio.VerMembresia); }

        private void btnFicha_Click(object sender, EventArgs e) { SolicitarAccion(AccionEstadoSocio.VerFicha); }

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
