using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;
using exxen2._0.capaLogica;

namespace exxen2._0.capaVisual.Administrador
{
    /* Presenta el pronóstico y el estado de cuotas en el UserControl existente. */
    [DesignerCategory("Component")]
    public sealed partial class InicioPanelAdministrador : UserControl
    {
        private ClimaLogica clima;
        private CuotaMembresiaLogica cuotas;
        private bool cargando;
        private bool EnModoDisenio => DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime;

        /* Inicializa los componentes existentes y las dependencias de la pantalla sin consultar la base de datos. */
        public InicioPanelAdministrador()
        {
            InitializeComponent();
            components.Add(tarjetaClimaEjemplo);
        }

        /* Actualiza el estado de cuotas y el pronóstico, evitando cargas simultáneas. */
        public async void Actualizar()
        {
            if (cargando || EnModoDisenio || clima == null || cuotas == null)
                return;
            cargando = true;
            try
            {
                CargarEstadoCuotas();
                await CargarClimaAsincrono();
            }
            finally
            {
                cargando = false;
            }
        }

        /* Consulta el pronóstico sin bloquear la espera de red y muestra una alternativa si falla. */
        private async Task CargarClimaAsincrono()
        {
            estadoClima.Text = "Actualizando...";
            ayudaClima.SetToolTip(estadoClima, string.Empty);
            try
            {
                var pronostico = await clima.ObtenerPronosticoSemanalAsincrono();
                if (IsDisposed)
                    return;
                LimpiarClima();
                foreach (var dia in pronostico)
                    AgregarDiaClima(dia);
                estadoClima.Text = "Datos: Open-Meteo";
            }
            catch (Exception ex)
            {
                if (IsDisposed)
                    return;
                MostrarClimaSinConexion();
                var detalle = ObtenerDetalleError(ex);
                estadoClima.Text = detalle.Length > 48 ? "No disponible: " + detalle.Substring(0, 45) + "..." : "No disponible: " + detalle;
                ayudaClima.SetToolTip(estadoClima, detalle);
            }
        }

        /* Recupera la causa del error y traduce el tiempo de espera a un mensaje legible. */
        private static string ObtenerDetalleError(Exception excepcion)
        {
            var detalle = excepcion;
            while (detalle.InnerException != null)
                detalle = detalle.InnerException;
            return detalle is TaskCanceledException ? "El servicio demoro demasiado en responder." : detalle.Message;
        }

        /* Presenta los siete días sin datos cuando el servicio de pronóstico no responde. */
        private void MostrarClimaSinConexion()
        {
            LimpiarClima();
            for (var indice = 0; indice < 7; indice++)
                AgregarDiaClima(new PronosticoDia { Fecha = DateTime.Today.AddDays(indice), Descripcion = "Sin datos", Icono = "-" }, false);
        }

        /* Retira las tarjetas anteriores y libera sus recursos, conservando la plantilla del diseñador. */
        private void LimpiarClima()
        {
            while (listaClima.Controls.Count > 0)
            {
                var tarjeta = listaClima.Controls[0];
                listaClima.Controls.Remove(tarjeta);
                if (tarjeta != tarjetaClimaEjemplo)
                    tarjeta.Dispose();
            }
        }

        /* Ubica cada día junto al anterior sin imponer una distribución automática al panel editable. */
        private void AgregarDiaClima(PronosticoDia dia, bool tieneDatos = true)
        {
            var tarjeta = CrearDiaClima(dia, tieneDatos);
            tarjeta.Location = new Point(
                tarjetaClimaEjemplo.Left + listaClima.Controls.Count * (tarjeta.Width + tarjetaClimaEjemplo.Margin.Horizontal),
                tarjetaClimaEjemplo.Top);
            listaClima.Controls.Add(tarjeta);
        }

        /* Compone cada día copiando las posiciones y estilos de la tarjeta editable en el diseñador. */
        private Control CrearDiaClima(PronosticoDia dia, bool tieneDatos = true)
        {
            var tarjeta = new Panel
            {
                BackColor = tarjetaClimaEjemplo.BackColor,
                Size = tarjetaClimaEjemplo.Size,
                Margin = tarjetaClimaEjemplo.Margin,
                Font = tarjetaClimaEjemplo.Font
            };
            tarjeta.Controls.Add(CrearEtiquetaClima(lblDiaEjemplo, dia.Fecha.Date == DateTime.Today ? "HOY" : dia.Fecha.ToString("ddd dd", new CultureInfo("es-AR")).ToUpperInvariant()));
            tarjeta.Controls.Add(CrearEtiquetaClima(lblIconoEjemplo, dia.Icono));
            tarjeta.Controls.Add(CrearEtiquetaClima(lblDescripcionClimaEjemplo, dia.Descripcion));
            tarjeta.Controls.Add(CrearEtiquetaClima(lblTemperaturaEjemplo, tieneDatos ? Math.Round(dia.TemperaturaMinima) + "° / " + Math.Round(dia.TemperaturaMaxima) + "°" : "-"));
            tarjeta.Controls.Add(CrearEtiquetaClima(lblLluviaEjemplo, tieneDatos ? "Lluvia: " + dia.ProbabilidadLluvia + "%" : "Pronostico no disponible"));
            return tarjeta;
        }

        /* Copia la presentación de una etiqueta de ejemplo y coloca el dato del día correspondiente. */
        private static Label CrearEtiquetaClima(Label modelo, string texto)
        {
            return new Label
            {
                AutoSize = false,
                Location = modelo.Location,
                Size = modelo.Size,
                Font = modelo.Font,
                ForeColor = modelo.ForeColor,
                BackColor = modelo.BackColor,
                TextAlign = modelo.TextAlign,
                Padding = modelo.Padding,
                Text = texto
            };
        }

        /* Presenta el saldo y la situación de cada membresía y resume cuántas tienen deuda. */
        private void CargarEstadoCuotas()
        {
            try
            {
                var estados = cuotas.ListarEstadoCuentas();
                tablaCuotas.Rows.Clear();
                foreach (var estado in estados)
                {
                    var periodo = estado.UltimaCuotaDesde.HasValue && estado.UltimaCuotaHasta.HasValue ? estado.UltimaCuotaDesde.Value.ToString("dd/MM/yyyy") + " - " + estado.UltimaCuotaHasta.Value.ToString("dd/MM/yyyy") : "Sin cuota";
                    tablaCuotas.Rows.Add(estado.IdMembresia, estado.Socio, estado.DNI, estado.Plan, periodo, estado.EstadoUltimaCuota, estado.SaldoPendiente.ToString("C"), estado.Situacion);
                }

                var alDia = estados.FindAll(e => e.AlDia).Count;
                var conDeuda = estados.FindAll(e => e.TieneDeuda).Count;
                resumenCuotas.Text = estados.Count + " membresia(s) - " + alDia + " al dia - " + conDeuda + " con deuda o periodo pendiente";
            }
            catch (Exception ex)
            {
                tablaCuotas.Rows.Clear();
                resumenCuotas.Text = "No se pudo cargar: " + ex.Message;
            }
        }

        /* Al cargar la pantalla en ejecución, prepara sus datos iniciales sin realizar consultas desde el diseñador. */
        private void InicioPanelAdministrador_Load(object origen, EventArgs e)
        {
            if (EnModoDisenio)
                return;
            clima = new ClimaLogica();
            cuotas = new CuotaMembresiaLogica();
            tituloClima.Text = "Pronostico semanal - " + clima.Ciudad;
            Actualizar();
        }
    }
}
