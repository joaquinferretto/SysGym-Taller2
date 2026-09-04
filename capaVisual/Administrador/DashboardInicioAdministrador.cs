using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;
using exxen2._0.capaLogica;

namespace exxen2._0.capaVisual.Administrador
{
    [DesignerCategory("Component")]
    public sealed partial class DashboardInicioAdministrador : UserControl
    {
        private ClimaLogica clima;
        private CuotaMembresiaLogica cuotas;
        private bool cargando;

        private bool EnModoDisenio => DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime;

        public DashboardInicioAdministrador()
        {
            InitializeComponent();
            if (!EnModoDisenio)
            {
                clima = new ClimaLogica();
                cuotas = new CuotaMembresiaLogica();
                tituloClima.Text = "Pronostico semanal - " + clima.Ciudad;
            }

            Load += delegate { if (!EnModoDisenio) Actualizar(); };
        }

        public async void Actualizar()
        {
            if (cargando || EnModoDisenio) return;
            cargando = true;
            try { CargarEstadoCuotas(); await CargarClimaAsync(); }
            finally { cargando = false; }
        }

        private async Task CargarClimaAsync()
        {
            estadoClima.Text = "Actualizando..."; ayudaClima.SetToolTip(estadoClima, string.Empty);
            try
            {
                var pronostico = await clima.ObtenerPronosticoSemanalAsync(); if (IsDisposed) return;
                listaClima.Controls.Clear(); foreach (var dia in pronostico) listaClima.Controls.Add(CrearDiaClima(dia)); estadoClima.Text = "Datos: Open-Meteo";
            }
            catch (Exception ex)
            {
                if (IsDisposed) return; MostrarClimaSinConexion(); var detalle = ObtenerDetalleError(ex); estadoClima.Text = detalle.Length > 48 ? "No disponible: " + detalle.Substring(0, 45) + "..." : "No disponible: " + detalle; ayudaClima.SetToolTip(estadoClima, detalle);
            }
        }

        private static string ObtenerDetalleError(Exception excepcion) { var detalle = excepcion; while (detalle.InnerException != null) detalle = detalle.InnerException; return detalle is TaskCanceledException ? "El servicio demoro demasiado en responder." : detalle.Message; }
        private void MostrarClimaSinConexion() { listaClima.Controls.Clear(); for (var indice = 0; indice < 7; indice++) listaClima.Controls.Add(CrearDiaClima(new PronosticoDia { Fecha = DateTime.Today.AddDays(indice), Descripcion = "Sin datos", Icono = "-" }, false)); }

        private static Control CrearDiaClima(PronosticoDia dia, bool tieneDatos = true)
        {
            var tarjeta = new Panel { BackColor = Color.FromArgb(248, 250, 252), Height = 108, Margin = new Padding(6, 2, 6, 2), Width = 148 };
            tarjeta.Controls.Add(new Label { AutoSize = false, Dock = DockStyle.Top, Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold), ForeColor = Color.FromArgb(51, 65, 85), Height = 23, Text = dia.Fecha.Date == DateTime.Today ? "HOY" : dia.Fecha.ToString("ddd dd", new CultureInfo("es-AR")).ToUpperInvariant(), TextAlign = ContentAlignment.MiddleCenter });
            tarjeta.Controls.Add(new Label { AutoSize = false, Font = new Font("Segoe UI Symbol", 20F), ForeColor = Color.FromArgb(79, 70, 229), Location = new Point(8, 22), Size = new Size(132, 32), Text = dia.Icono, TextAlign = ContentAlignment.MiddleCenter });
            tarjeta.Controls.Add(new Label { AutoSize = false, ForeColor = Color.FromArgb(71, 85, 105), Location = new Point(5, 54), Size = new Size(138, 18), Text = dia.Descripcion, TextAlign = ContentAlignment.MiddleCenter });
            tarjeta.Controls.Add(new Label { AutoSize = false, Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold), ForeColor = Color.FromArgb(30, 41, 59), Location = new Point(5, 72), Size = new Size(138, 17), Text = tieneDatos ? Math.Round(dia.TemperaturaMinima) + "° / " + Math.Round(dia.TemperaturaMaxima) + "°" : "-", TextAlign = ContentAlignment.MiddleCenter });
            tarjeta.Controls.Add(new Label { AutoSize = false, Font = new Font("Segoe UI", 8F), ForeColor = Color.FromArgb(100, 116, 139), Location = new Point(5, 90), Size = new Size(138, 14), Text = tieneDatos ? "Lluvia: " + dia.ProbabilidadLluvia + "%" : "Pronostico no disponible", TextAlign = ContentAlignment.MiddleCenter });
            return tarjeta;
        }

        private void CargarEstadoCuotas()
        {
            try
            {
                var estados = cuotas.ListarEstadoCuentas(); tablaCuotas.Rows.Clear();
                foreach (var estado in estados)
                {
                    var periodo = estado.UltimaCuotaDesde.HasValue && estado.UltimaCuotaHasta.HasValue ? estado.UltimaCuotaDesde.Value.ToString("dd/MM/yyyy") + " - " + estado.UltimaCuotaHasta.Value.ToString("dd/MM/yyyy") : "Sin cuota";
                    tablaCuotas.Rows.Add(estado.IdMembresia, estado.Socio, estado.DNI, estado.Plan, periodo, estado.EstadoUltimaCuota, estado.SaldoPendiente.ToString("C"), estado.Situacion);
                }
                var alDia = estados.FindAll(e => e.AlDia).Count; var conDeuda = estados.FindAll(e => e.TieneDeuda).Count; resumenCuotas.Text = estados.Count + " membresia(s) - " + alDia + " al dia - " + conDeuda + " con deuda o periodo pendiente";
            }
            catch (Exception ex) { tablaCuotas.Rows.Clear(); resumenCuotas.Text = "No se pudo cargar: " + ex.Message; }
        }

    }
}
