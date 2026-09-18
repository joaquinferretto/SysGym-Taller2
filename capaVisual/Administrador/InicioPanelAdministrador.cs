using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;
using exxen2._0.capaLogica;

namespace exxen2._0.capaVisual.Administrador
{
    /* Transporta el socio elegido desde el estado de cuenta hasta el panel administrador. */
    public sealed class SocioEstadoCuentaEventArgs : EventArgs
    {
        public SocioEstadoCuentaEventArgs(int idSocio)
        {
            IdSocio = idSocio;
        }

        public int IdSocio { get; private set; }
    }

    /* Presenta el pronostico y el estado de cuotas en el panel de inicio. */
    [DesignerCategory("Component")]
    public sealed partial class InicioPanelAdministrador : UserControl
    {
        public event EventHandler<SocioEstadoCuentaEventArgs> SocioDobleClic;
        private ClimaLogica clima;
        private CuotaMembresiaLogica cuotas;
        private bool cargando;
        private bool EnModoDisenio { get { return DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime; } }

        public InicioPanelAdministrador()
        {
            InitializeComponent();
        }

        public async void Actualizar()
        {
            if (cargando || EnModoDisenio || clima == null || cuotas == null) return;
            cargando = true;
            try
            {
                CargarEstadoCuotas();
                await CargarClimaAsincrono();
            }
            finally { cargando = false; }
        }

        private async Task CargarClimaAsincrono()
        {
            estadoClima.Text = "Actualizando...";
            ayudaClima.SetToolTip(estadoClima, string.Empty);
            try
            {
                var pronostico = await clima.ObtenerPronosticoSemanalAsincrono();
                if (IsDisposed) return;
                LimpiarClima();
                foreach (var dia in pronostico) AgregarDiaClima(dia);
                estadoClima.Text = clima.UltimaConsultaUsoCache ? "Respaldo local (sin internet)" : "Datos: Open-Meteo";
            }
            catch (Exception ex)
            {
                if (IsDisposed) return;
                MostrarClimaSinConexion();
                var detalle = ObtenerDetalleError(ex);
                estadoClima.Text = detalle.Length > 48 ? "No disponible: " + detalle.Substring(0, 45) + "..." : "No disponible: " + detalle;
                ayudaClima.SetToolTip(estadoClima, detalle);
            }
        }

        private static string ObtenerDetalleError(Exception excepcion)
        {
            var detalle = excepcion;
            while (detalle.InnerException != null) detalle = detalle.InnerException;
            return detalle is TaskCanceledException ? "El servicio demoro demasiado en responder." : detalle.Message;
        }

        private void MostrarClimaSinConexion()
        {
            LimpiarClima();
            for (var indice = 0; indice < ClimaLogica.DiasPronostico; indice++)
                AgregarDiaClima(new PronosticoDia { Fecha = DateTime.Today.AddDays(indice), Descripcion = "Sin datos", Icono = "-" }, false);
        }

        private void LimpiarClima()
        {
            while (listaClima.Controls.Count > 0)
            {
                var tarjeta = listaClima.Controls[0];
                listaClima.Controls.Remove(tarjeta);
                tarjeta.Dispose();
            }
        }

        private void AgregarDiaClima(PronosticoDia dia, bool tieneDatos = true)
        {
            var columna = listaClima.Controls.Count;
            if (columna >= listaClima.ColumnCount) return;
            listaClima.Controls.Add(CrearDiaClima(dia, tieneDatos), columna, 0);
        }

        private static Control CrearDiaClima(PronosticoDia dia, bool tieneDatos = true)
        {
            var tarjeta = new Panel
            {
                BackColor = Color.FromArgb(248, 250, 252),
                Dock = DockStyle.Fill,
                Margin = new Padding(3, 2, 3, 2)
            };
            tarjeta.Controls.Add(CrearEtiquetaClima(dia.Fecha.Date == DateTime.Today ? "HOY" : dia.Fecha.ToString("ddd dd", new CultureInfo("es-AR")).ToUpperInvariant(), new Point(0, 4), new Size(148, 22), new Font("Segoe UI Semibold", 9F, FontStyle.Bold), Color.FromArgb(51, 65, 85)));
            tarjeta.Controls.Add(CrearEtiquetaClima(dia.Icono, new Point(8, 24), new Size(132, 34), new Font("Segoe UI Symbol", 20F), Color.FromArgb(79, 70, 229)));
            tarjeta.Controls.Add(CrearEtiquetaClima(dia.Descripcion, new Point(5, 58), new Size(138, 18), new Font("Segoe UI", 9F), Color.FromArgb(71, 85, 105)));
            tarjeta.Controls.Add(CrearEtiquetaClima(tieneDatos ? Math.Round(dia.TemperaturaMinima) + "° / " + Math.Round(dia.TemperaturaMaxima) + "°" : "-", new Point(5, 76), new Size(138, 18), new Font("Segoe UI Semibold", 9F, FontStyle.Bold), Color.FromArgb(30, 41, 59)));
            tarjeta.Controls.Add(CrearEtiquetaClima(tieneDatos ? "Lluvia: " + dia.ProbabilidadLluvia + "%" : "Pronostico no disponible", new Point(5, 92), new Size(138, 14), new Font("Segoe UI", 8F), Color.FromArgb(100, 116, 139)));
            return tarjeta;
        }

        private static Label CrearEtiquetaClima(string texto, Point ubicacion, Size tamano, Font fuente, Color color)
        {
            return new Label
            {
                AutoSize = false,
                Location = ubicacion,
                Size = tamano,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                AutoEllipsis = true,
                Font = fuente,
                ForeColor = color,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleCenter,
                Text = texto
            };
        }

        private void CargarEstadoCuotas()
        {
            try
            {
                var estados = cuotas.ListarEstadoCuentas();
                tablaCuotas.Rows.Clear();
                foreach (var estado in estados)
                {
                    var periodo = estado.UltimaCuotaDesde.HasValue && estado.UltimaCuotaHasta.HasValue
                        ? estado.UltimaCuotaDesde.Value.ToString("dd/MM/yyyy") + " - " + estado.UltimaCuotaHasta.Value.ToString("dd/MM/yyyy")
                        : "Sin cuota";
                    tablaCuotas.Rows.Add(estado.IdMembresia, estado.IdSocio, estado.Socio, estado.DNI, estado.Plan, periodo, estado.EstadoUltimaCuota, estado.SaldoPendiente.ToString("C"), estado.Situacion);
                }

                var alDia = estados.FindAll(e => e.AlDia).Count;
                var conDeuda = estados.FindAll(e => e.TieneDeuda).Count;
                resumenCuotas.Text = estados.Count + " membresia(s) - " + alDia + " al dia - " + conDeuda + " con deuda o periodo pendiente";
            }
            catch (Exception ex)
            {
                Trace.TraceError("No se pudo cargar el estado de cuenta del dashboard: " + ex);
                tablaCuotas.Rows.Clear();
                resumenCuotas.Text = "No se pudo cargar el estado de cuenta.";
            }
        }

        private void tablaCuotas_CellDoubleClick(object origen, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            int idSocio;
            if (!int.TryParse(Convert.ToString(tablaCuotas.Rows[e.RowIndex].Cells["colIdSocio"].Value), out idSocio) || idSocio <= 0) return;
            var evento = SocioDobleClic;
            if (evento != null) evento(this, new SocioEstadoCuentaEventArgs(idSocio));
        }

        private void InicioPanelAdministrador_Load(object origen, EventArgs e)
        {
            if (EnModoDisenio) return;
            clima = new ClimaLogica();
            cuotas = new CuotaMembresiaLogica();
            tituloClima.Text = "Pronóstico semanal - " + clima.Ciudad;
            Actualizar();
        }
    }
}
