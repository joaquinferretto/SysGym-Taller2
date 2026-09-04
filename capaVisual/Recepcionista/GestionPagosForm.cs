using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaLogica;
using exxen2._0.capaVisual.Compartido;

namespace exxen2._0.capaVisual.Recepcionista
{
    [DesignerCategory("Form")]
    public partial class GestionPagosForm : Form
    {
        private readonly PagoLogica logica = new PagoLogica();
        private readonly CuotaMembresiaLogica cuotas = new CuotaMembresiaLogica();
        private readonly MembresiaLogica membresias = new MembresiaLogica();
        private List<CuotaMembresia> cuotasCargadas = new List<CuotaMembresia>();
        private List<MembresiaPagoItem> membresiasCargadas = new List<MembresiaPagoItem>();
        private int idCuotaSeleccionada;
        private int idPagoSeleccionado;
        private bool cargandoTabla;
        private bool actualizandoFormulario;

        public GestionPagosForm()
        {
            InitializeComponent();
            btnVolver.Click += delegate { Close(); };
            FormularioVisualHelper.ConfigurarEntradaDecimal(importe);
            membresia.SelectedIndexChanged += CambioMembresia;
            tabla.SelectionChanged += Seleccionar;
            buscador.TextChanged += delegate { AplicarFiltro(); };
            filtroEstado.SelectedIndexChanged += delegate { AplicarFiltro(); };
            FormularioVisualHelper.AlCargarEnEjecucion(this, delegate { Inicializar(); });
        }

        private void Inicializar()
        {
            try { CargarMembresias(); CargarMetodosPago(); Cargar(); NuevoPago(null, EventArgs.Empty); }
            catch (Exception ex) { FormularioVisualHelper.MostrarError(lblEstado, ex); }
        }

        private void CargarMembresias()
        {
            membresiasCargadas = membresias.ListarParaGestion().Select(m => new MembresiaPagoItem { IdMembresia = m.IdMembresia, Habilitada = m.Estado, Texto = NombreSocio(m) + " - " + NombrePlan(m) + (m.Estado ? string.Empty : " (inactiva)") }).ToList(); actualizandoFormulario = true; membresia.DataSource = membresiasCargadas; membresia.DisplayMember = "Texto"; membresia.ValueMember = "IdMembresia"; actualizandoFormulario = false;
        }

        private void CargarMetodosPago() { metodo.DataSource = logica.ListarMetodosPagoActivos(); metodo.DisplayMember = "Observaciones"; metodo.ValueMember = "IdMetodoPago"; }
        private void Cargar() { try { cuotasCargadas = cuotas.ListarParaGestion(); AplicarFiltro(); } catch (Exception ex) { FormularioVisualHelper.MostrarError(lblEstado, ex); } }

        private void AplicarFiltro()
        {
            var criterio = buscador.Text.Trim(); var elegido = Convert.ToString(filtroEstado.SelectedItem); var filtradas = cuotasCargadas.AsEnumerable(); if (elegido == "Pendientes") filtradas = filtradas.Where(c => c.EstadoPago == EstadosCuota.Pendiente); else if (elegido == "Pagadas") filtradas = filtradas.Where(c => c.EstadoPago == EstadosCuota.Pagada); if (!string.IsNullOrWhiteSpace(criterio)) filtradas = filtradas.Where(c => Contiene(NombreSocio(c.Membresia), criterio) || Contiene(c.Membresia == null || c.Membresia.Socio == null ? string.Empty : c.Membresia.Socio.DNI, criterio) || Contiene(NombrePlan(c.Membresia), criterio)); cargandoTabla = true; tabla.Rows.Clear(); foreach (var c in filtradas) tabla.Rows.Add(c.IdCuotaMembresia, c.IdRegistroPago.HasValue ? (object)c.IdRegistroPago.Value : null, NombreSocio(c.Membresia), c.Membresia == null || c.Membresia.Socio == null ? "-" : c.Membresia.Socio.DNI, NombrePlan(c.Membresia), Periodo(c), c.Importe.ToString("C"), c.EstadoPago); tabla.ClearSelection(); cargandoTabla = false; lblEstado.Text = tabla.Rows.Count + " cuota(s) encontrada(s) - " + cuotasCargadas.Count(c => c.EstadoPago == EstadosCuota.Pagada) + " pagada(s)";
        }

        private void Seleccionar(object sender, EventArgs e) { if (cargandoTabla || tabla.CurrentRow == null || !tabla.CurrentRow.Selected) return; var idCuota = Convert.ToInt32(tabla.CurrentRow.Cells[0].Value); var seleccionada = cuotasCargadas.FirstOrDefault(c => c.IdCuotaMembresia == idCuota); if (seleccionada != null) MostrarCuota(seleccionada); }
        private void NuevoPago(object sender, EventArgs e) { tabla.ClearSelection(); idCuotaSeleccionada = 0; idPagoSeleccionado = 0; var primeraActiva = membresiasCargadas.FirstOrDefault(m => m.Habilitada); actualizandoFormulario = true; if (primeraActiva != null) membresia.SelectedValue = primeraActiva.IdMembresia; actualizandoFormulario = false; SeleccionarPrimeraPendiente(); }
        private void CambioMembresia(object sender, EventArgs e) { if (!actualizandoFormulario) SeleccionarPrimeraPendiente(); }

        private void SeleccionarPrimeraPendiente()
        {
            var item = membresia.SelectedItem as MembresiaPagoItem; if (item == null || !item.Habilitada) { MostrarSinCuota("Selecciona una membresia activa."); return; } var pendiente = cuotasCargadas.Where(c => c.IdMembresia == item.IdMembresia && c.EstadoPago == EstadosCuota.Pendiente && !c.IdRegistroPago.HasValue).OrderBy(c => c.FechaDesde).FirstOrDefault(); if (pendiente == null) { MostrarSinCuota("La membresia no tiene cuotas pendientes disponibles."); return; } MostrarCuota(pendiente, true);
        }

        private void MostrarCuota(CuotaMembresia seleccionada, bool modoNuevo = false)
        {
            actualizandoFormulario = true; idCuotaSeleccionada = seleccionada.IdCuotaMembresia; idPagoSeleccionado = seleccionada.IdRegistroPago ?? 0; membresia.SelectedValue = seleccionada.IdMembresia; cuota.Text = Periodo(seleccionada); importe.Text = seleccionada.Importe.ToString("0.00"); if (seleccionada.Pago != null) { metodo.SelectedValue = seleccionada.Pago.IdMetodoPago; estado.SelectedItem = seleccionada.Pago.Estado; } else { if (metodo.Items.Count > 0) metodo.SelectedIndex = 0; estado.SelectedItem = EstadosTransaccionPago.Aprobado; } actualizandoFormulario = false; EstablecerModo(seleccionada, modoNuevo);
        }

        private void MostrarSinCuota(string mensaje) { idCuotaSeleccionada = 0; idPagoSeleccionado = 0; cuota.Clear(); importe.Clear(); lblFormulario.Text = mensaje; registrar.Enabled = false; anular.Enabled = false; reembolsar.Enabled = false; importe.ReadOnly = true; metodo.Enabled = false; estado.Enabled = false; membresia.Enabled = true; }
        private void EstablecerModo(CuotaMembresia seleccionada, bool modoNuevo) { var tienePago = seleccionada.Pago != null && seleccionada.IdRegistroPago.HasValue; lblFormulario.Text = tienePago ? "Detalle del pago - " + seleccionada.Pago.Estado : (modoNuevo ? "Nuevo pago - Cuota pendiente" : "Registrar cuota pendiente"); membresia.Enabled = !tienePago; importe.ReadOnly = tienePago; metodo.Enabled = !tienePago; estado.Enabled = !tienePago; registrar.Enabled = !tienePago && seleccionada.EstadoPago == EstadosCuota.Pendiente; anular.Enabled = tienePago && seleccionada.Pago.Estado != EstadosTransaccionPago.Anulado && seleccionada.Pago.Estado != EstadosTransaccionPago.Reembolsado; reembolsar.Enabled = tienePago && seleccionada.Pago.Estado == EstadosTransaccionPago.Aprobado; }

        private void Registrar(object sender, EventArgs e)
        {
            try { if (idCuotaSeleccionada <= 0) throw new InvalidOperationException("Selecciona una cuota pendiente."); if (metodo.SelectedValue == null) throw new InvalidOperationException("Selecciona un metodo de pago."); logica.RegistrarPago(new Pago { Importe = FormularioVisualHelper.DecimalPositivo(importe, "importe"), IdMetodoPago = Convert.ToInt32(metodo.SelectedValue), Estado = Convert.ToString(estado.SelectedItem), Fecha = DateTime.Now, Descripcion = "Pago registrado en recepcion" }, idCuotaSeleccionada); Cargar(); NuevoPago(null, EventArgs.Empty); FormularioVisualHelper.MostrarExito(lblEstado, "Pago registrado correctamente."); }
            catch (Exception ex) { FormularioVisualHelper.MostrarError(lblEstado, ex); }
        }

        private void Anular(object sender, EventArgs e) { try { if (idPagoSeleccionado == 0) throw new InvalidOperationException("Selecciona un pago."); if (MessageBox.Show("Anular el pago seleccionado?", "Confirmar anulacion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return; logica.AnularPago(idPagoSeleccionado); Cargar(); NuevoPago(null, EventArgs.Empty); FormularioVisualHelper.MostrarExito(lblEstado, "Pago anulado."); } catch (Exception ex) { FormularioVisualHelper.MostrarError(lblEstado, ex); } }
        private void Reembolsar(object sender, EventArgs e) { try { if (idPagoSeleccionado == 0) throw new InvalidOperationException("Selecciona un pago."); if (MessageBox.Show("Reembolsar el pago seleccionado?", "Confirmar reembolso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return; logica.ReembolsarPago(idPagoSeleccionado); Cargar(); NuevoPago(null, EventArgs.Empty); FormularioVisualHelper.MostrarExito(lblEstado, "Pago reembolsado."); } catch (Exception ex) { FormularioVisualHelper.MostrarError(lblEstado, ex); } }
        private static string NombreSocio(Membresia m) { return m == null || m.Socio == null ? "Socio no disponible" : m.Socio.Apellido + ", " + m.Socio.Nombre; }
        private static string NombrePlan(Membresia m) { return m == null || m.Plan == null ? "Plan no disponible" : m.Plan.Nombre; }
        private static string Periodo(CuotaMembresia c) { return c.FechaDesde.ToString("dd/MM/yyyy") + " al " + c.FechaHasta.ToString("dd/MM/yyyy"); }
        private static bool Contiene(string valor, string criterio) { return !string.IsNullOrEmpty(valor) && valor.IndexOf(criterio, StringComparison.OrdinalIgnoreCase) >= 0; }

        private sealed class MembresiaPagoItem { public int IdMembresia { get; set; } public string Texto { get; set; } public bool Habilitada { get; set; } }
    }
}
