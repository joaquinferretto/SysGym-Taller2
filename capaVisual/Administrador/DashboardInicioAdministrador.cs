using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;
using exxen2._0.capaLogica;

namespace exxen2._0.capaVisual.Administrador
{
    [DesignerCategory("Code")]
    [DesignTimeVisible(false)]
    public sealed class DashboardInicioAdministrador : UserControl
    {
        private readonly ClimaLogica clima = new ClimaLogica();
        private readonly CuotaMembresiaLogica cuotas = new CuotaMembresiaLogica();
        private readonly FlowLayoutPanel listaClima;
        private readonly Panel cabeceraClima;
        private readonly Panel cabeceraCuotas;
        private readonly Label estadoClima;
        private readonly Label resumenCuotas;
        private readonly DataGridView tablaCuotas;
        private readonly ToolTip ayudaClima;
        private bool cargando;

        public DashboardInicioAdministrador()
        {
            BackColor = Color.FromArgb(241, 245, 249);
            Dock = DockStyle.Fill;
            Font = new Font("Segoe UI", 9.5F);
            ayudaClima = new ToolTip
            {
                AutoPopDelay = 10000,
                InitialDelay = 300,
                ReshowDelay = 100
            };

            var principal = new TableLayoutPanel
            {
                BackColor = Color.Transparent,
                ColumnCount = 1,
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                RowCount = 4
            };
            principal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            principal.RowStyles.Add(new RowStyle(SizeType.Absolute, 62F));
            principal.RowStyles.Add(new RowStyle(SizeType.Absolute, 188F));
            principal.RowStyles.Add(new RowStyle(SizeType.Absolute, 92F));
            principal.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            principal.Controls.Add(CrearCabecera(), 0, 0);

            var tarjetaClima = CrearTarjeta();
            tarjetaClima.Margin = new Padding(0, 0, 0, 12);
            cabeceraClima = new Panel { BackColor = Color.White, Dock = DockStyle.Top, Height = 42 };
            cabeceraClima.Controls.Add(new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 41, 59),
                Location = new Point(16, 12),
                Text = "Pronóstico semanal · " + clima.Ciudad
            });
            estadoClima = new Label
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                AutoSize = true,
                ForeColor = Color.FromArgb(100, 116, 139),
                Text = "Actualizando...",
                Top = 14
            };
            cabeceraClima.Controls.Add(estadoClima);
            cabeceraClima.Resize += delegate
            {
                AlinearEtiquetaDerecha(cabeceraClima, estadoClima);
            };
            listaClima = new FlowLayoutPanel
            {
                AutoScroll = true,
                BackColor = Color.White,
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(10, 4, 10, 8),
                WrapContents = false
            };
            tarjetaClima.Controls.Add(listaClima);
            tarjetaClima.Controls.Add(cabeceraClima);
            principal.Controls.Add(tarjetaClima, 0, 1);

            var suscripcion = CrearTarjeta();
            suscripcion.Margin = new Padding(0, 0, 0, 12);
            suscripcion.Controls.Add(new Label
            {
                AutoSize = false,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(51, 65, 85),
                Padding = new Padding(18, 12, 18, 8),
                Text = "CUOTAS MENSUALES\r\nUna membresía funciona como una suscripción: cada período mensual debe tener una cuota. "
                    + "El socio está al día cuando no posee cuotas pendientes y el período vigente ya fue generado."
            });
            principal.Controls.Add(suscripcion, 0, 2);

            var tarjetaCuotas = CrearTarjeta();
            cabeceraCuotas = new Panel { BackColor = Color.White, Dock = DockStyle.Top, Height = 44 };
            cabeceraCuotas.Controls.Add(new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 41, 59),
                Location = new Point(16, 13),
                Text = "Estado de cuenta de socios"
            });
            resumenCuotas = new Label
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                AutoSize = true,
                ForeColor = Color.FromArgb(71, 85, 105),
                Text = "Cargando...",
                Top = 15
            };
            cabeceraCuotas.Controls.Add(resumenCuotas);
            cabeceraCuotas.Resize += delegate
            {
                AlinearEtiquetaDerecha(cabeceraCuotas, resumenCuotas);
            };

            tablaCuotas = CrearTablaCuotas();
            tarjetaCuotas.Controls.Add(tablaCuotas);
            tarjetaCuotas.Controls.Add(cabeceraCuotas);
            principal.Controls.Add(tarjetaCuotas, 0, 3);

            Controls.Add(principal);
            Load += delegate { Actualizar(); };
        }

        public async void Actualizar()
        {
            if (cargando || EnModoDiseno()) return;

            cargando = true;
            try
            {
                CargarEstadoCuotas();
                await CargarClimaAsync();
            }
            finally
            {
                cargando = false;
            }
        }

        private Control CrearCabecera()
        {
            var panel = new Panel { BackColor = Color.Transparent, Dock = DockStyle.Fill };
            panel.Controls.Add(new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 41, 59),
                Location = new Point(0, 0),
                Text = "Resumen general"
            });
            panel.Controls.Add(new Label
            {
                AutoSize = true,
                ForeColor = Color.FromArgb(100, 116, 139),
                Location = new Point(2, 38),
                Text = DateTime.Today.ToString("dddd, dd 'de' MMMM 'de' yyyy", new CultureInfo("es-AR"))
            });
            return panel;
        }

        private async Task CargarClimaAsync()
        {
            estadoClima.ForeColor = Color.FromArgb(100, 116, 139);
            estadoClima.Text = "Actualizando...";
            AlinearEtiquetaDerecha(cabeceraClima, estadoClima);
            ayudaClima.SetToolTip(estadoClima, string.Empty);
            try
            {
                var pronostico = await clima.ObtenerPronosticoSemanalAsync();
                if (IsDisposed) return;

                listaClima.Controls.Clear();
                foreach (var dia in pronostico)
                {
                    listaClima.Controls.Add(CrearDiaClima(dia));
                }
                estadoClima.Text = "Datos: Open-Meteo";
                AlinearEtiquetaDerecha(cabeceraClima, estadoClima);
            }
            catch (Exception ex)
            {
                if (IsDisposed) return;
                MostrarClimaSinConexion();
                var detalle = ObtenerDetalleError(ex);
                estadoClima.ForeColor = Color.FromArgb(185, 28, 28);
                estadoClima.Text = detalle.Length > 48
                    ? "No disponible: " + detalle.Substring(0, 45) + "..."
                    : "No disponible: " + detalle;
                AlinearEtiquetaDerecha(cabeceraClima, estadoClima);
                ayudaClima.SetToolTip(estadoClima, detalle);
            }
        }

        private static void AlinearEtiquetaDerecha(Panel cabecera, Label etiqueta)
        {
            etiqueta.Left = Math.Max(16, cabecera.ClientSize.Width - etiqueta.Width - 16);
        }

        private static string ObtenerDetalleError(Exception excepcion)
        {
            var detalle = excepcion;
            while (detalle.InnerException != null)
            {
                detalle = detalle.InnerException;
            }

            return detalle is TaskCanceledException
                ? "El servicio demoró demasiado en responder."
                : detalle.Message;
        }

        private void MostrarClimaSinConexion()
        {
            listaClima.Controls.Clear();
            for (var indice = 0; indice < 7; indice++)
            {
                listaClima.Controls.Add(CrearDiaClima(new PronosticoDia
                {
                    Fecha = DateTime.Today.AddDays(indice),
                    Descripcion = "Sin datos",
                    Icono = "—"
                }, false));
            }
        }

        private static Control CrearDiaClima(PronosticoDia dia, bool tieneDatos = true)
        {
            var tarjeta = new Panel
            {
                BackColor = Color.FromArgb(248, 250, 252),
                Height = 108,
                Margin = new Padding(6, 2, 6, 2),
                Width = 148
            };
            tarjeta.Controls.Add(new Label
            {
                AutoSize = false,
                Dock = DockStyle.Top,
                Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 65, 85),
                Height = 23,
                Text = dia.Fecha.Date == DateTime.Today ? "HOY" : dia.Fecha.ToString("ddd dd", new CultureInfo("es-AR")).ToUpperInvariant(),
                TextAlign = ContentAlignment.MiddleCenter
            });
            tarjeta.Controls.Add(new Label
            {
                AutoSize = false,
                Font = new Font("Segoe UI Symbol", 20F),
                ForeColor = Color.FromArgb(79, 70, 229),
                Location = new Point(8, 22),
                Size = new Size(132, 32),
                Text = dia.Icono,
                TextAlign = ContentAlignment.MiddleCenter
            });
            tarjeta.Controls.Add(new Label
            {
                AutoSize = false,
                ForeColor = Color.FromArgb(71, 85, 105),
                Location = new Point(5, 54),
                Size = new Size(138, 18),
                Text = dia.Descripcion,
                TextAlign = ContentAlignment.MiddleCenter
            });
            tarjeta.Controls.Add(new Label
            {
                AutoSize = false,
                Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 41, 59),
                Location = new Point(5, 72),
                Size = new Size(138, 17),
                Text = tieneDatos
                    ? Math.Round(dia.TemperaturaMinima) + "° / " + Math.Round(dia.TemperaturaMaxima) + "°"
                    : "—",
                TextAlign = ContentAlignment.MiddleCenter
            });
            tarjeta.Controls.Add(new Label
            {
                AutoSize = false,
                Font = new Font("Segoe UI", 8F),
                ForeColor = Color.FromArgb(100, 116, 139),
                Location = new Point(5, 90),
                Size = new Size(138, 14),
                Text = tieneDatos ? "Lluvia: " + dia.ProbabilidadLluvia + "%" : "Pronóstico no disponible",
                TextAlign = ContentAlignment.MiddleCenter
            });
            return tarjeta;
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
                        ? estado.UltimaCuotaDesde.Value.ToString("dd/MM/yyyy") + " - "
                            + estado.UltimaCuotaHasta.Value.ToString("dd/MM/yyyy")
                        : "Sin cuota";
                    var indice = tablaCuotas.Rows.Add(estado.IdMembresia, estado.Socio, estado.DNI,
                        estado.Plan, periodo, estado.EstadoUltimaCuota,
                        estado.SaldoPendiente.ToString("C"), estado.Situacion);
                    AplicarColorFila(tablaCuotas.Rows[indice], estado);
                }

                var alDia = estados.FindAll(e => e.AlDia).Count;
                var conDeuda = estados.FindAll(e => e.TieneDeuda).Count;
                resumenCuotas.ForeColor = Color.FromArgb(71, 85, 105);
                resumenCuotas.Text = estados.Count + " membresía(s) · " + alDia + " al día · "
                    + conDeuda + " con deuda o período pendiente";
                AlinearEtiquetaDerecha(cabeceraCuotas, resumenCuotas);
            }
            catch (Exception ex)
            {
                tablaCuotas.Rows.Clear();
                resumenCuotas.ForeColor = Color.FromArgb(185, 28, 28);
                resumenCuotas.Text = "No se pudo cargar: " + ex.Message;
                AlinearEtiquetaDerecha(cabeceraCuotas, resumenCuotas);
            }
        }

        private static void AplicarColorFila(DataGridViewRow fila, EstadoCuentaMembresia estado)
        {
            if (estado.AlDia)
            {
                fila.DefaultCellStyle.BackColor = Color.FromArgb(240, 253, 244);
                fila.DefaultCellStyle.SelectionBackColor = Color.FromArgb(187, 247, 208);
            }
            else if (estado.TieneDeuda)
            {
                fila.DefaultCellStyle.BackColor = Color.FromArgb(254, 242, 242);
                fila.DefaultCellStyle.SelectionBackColor = Color.FromArgb(254, 202, 202);
            }
            else
            {
                fila.DefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
                fila.DefaultCellStyle.SelectionBackColor = Color.FromArgb(226, 232, 240);
            }
            fila.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 41, 59);
        }

        private static Panel CrearTarjeta()
        {
            return new Panel
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Dock = DockStyle.Fill,
                Margin = new Padding(0)
            };
        }

        private static DataGridView CrearTablaCuotas()
        {
            var tabla = new DataGridView
            {
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                ColumnHeadersHeight = 36,
                Dock = DockStyle.Fill,
                GridColor = Color.FromArgb(226, 232, 240),
                MultiSelect = false,
                ReadOnly = true,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            tabla.EnableHeadersVisualStyles = false;
            tabla.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(241, 245, 249);
            tabla.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            tabla.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(30, 41, 59);
            tabla.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            tabla.DefaultCellStyle.ForeColor = Color.FromArgb(30, 41, 59);
            tabla.RowTemplate.Height = 32;

            tabla.Columns.Add("IdMembresia", "N.º");
            tabla.Columns.Add("Socio", "Socio");
            tabla.Columns.Add("DNI", "DNI");
            tabla.Columns.Add("Plan", "Plan");
            tabla.Columns.Add("Periodo", "Último período");
            tabla.Columns.Add("EstadoCuota", "Última cuota");
            tabla.Columns.Add("Saldo", "Saldo pendiente");
            tabla.Columns.Add("Situacion", "Situación");
            tabla.Columns[0].FillWeight = 45;
            tabla.Columns[1].FillWeight = 130;
            tabla.Columns[2].FillWeight = 75;
            tabla.Columns[3].FillWeight = 80;
            tabla.Columns[4].FillWeight = 115;
            tabla.Columns[5].FillWeight = 80;
            tabla.Columns[6].FillWeight = 90;
            tabla.Columns[7].FillWeight = 120;
            return tabla;
        }

        private bool EnModoDiseno()
        {
            return LicenseManager.UsageMode == LicenseUsageMode.Designtime
                || DesignMode || (Site != null && Site.DesignMode);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ayudaClima.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
