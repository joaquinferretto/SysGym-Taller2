using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaLogica;
using exxen2._0.capaVisual.Compartido;

namespace exxen2._0.capaVisual.Recepcionista
{
    [System.ComponentModel.DesignerCategory("Code")]
    [System.ComponentModel.DesignTimeVisible(false)]
    public abstract class GestionMembresiasFormBase : FormularioModuloBase
    {
        private readonly UsuarioSistema usuario;
        private readonly MembresiaLogica logica = new MembresiaLogica();
        private readonly SocioLogica socios = new SocioLogica();
        private readonly PlanLogica planes = new PlanLogica();
        private readonly CuotaMembresiaLogica cuotas = new CuotaMembresiaLogica();
        private readonly VistaListadoDetalle vista;
        private readonly ComboBox socio;
        private readonly ComboBox plan;
        private readonly DateTimePicker inicio;
        private readonly DateTimePicker vencimiento;
        private readonly Button crear;
        private readonly Button actualizar;
        private readonly Button habilitar;
        private readonly Button deshabilitar;
        private readonly Button generarCuota;
        private List<Membresia> membresiasCargadas = new List<Membresia>();
        private Membresia membresiaSeleccionada;
        private int idSeleccionado;
        private bool cargandoTabla;

        public GestionMembresiasFormBase()
            : this(new UsuarioSistema
            {
                Nombre = "Recepcionista",
                Apellido = "de diseño"
            })
        {
        }

        public GestionMembresiasFormBase(UsuarioSistema usuario)
            : this(usuario, Color.FromArgb(5, 150, 105))
        {
        }

        public GestionMembresiasFormBase(UsuarioSistema usuario, Color colorPrimario)
            : base("Membresías", "Asignación de planes a socios, vigencia y cuotas", colorPrimario)
        {
            if (usuario == null) throw new ArgumentNullException("usuario");
            this.usuario = usuario;

            vista = CrearVistaListadoDetalle("Membresías", "Buscar por socio, DNI o plan",
                "+ Nueva membresía", NuevaMembresia);
            socio = vista.AgregarCampo("Socio", new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList
            });
            plan = vista.AgregarCampo("Plan contratado", new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList
            });
            inicio = vista.AgregarCampo("Fecha de inicio", new DateTimePicker
            {
                Format = DateTimePickerFormat.Short
            });
            vencimiento = vista.AgregarCampo("Fecha de vencimiento", new DateTimePicker
            {
                Format = DateTimePickerFormat.Short
            });

            crear = vista.AgregarAccion("Crear membresía", Crear, true);
            actualizar = vista.AgregarAccion("Actualizar", Actualizar);
            habilitar = vista.AgregarAccion("Habilitar", Habilitar);
            deshabilitar = vista.AgregarAccion("Deshabilitar", Deshabilitar, false, true);
            generarCuota = vista.AgregarAccion("Generar cuota", GenerarCuota);

            Tabla.Columns.Add("Id", "Id"); Tabla.Columns[0].Visible = false;
            Tabla.Columns.Add("Socio", "Socio");
            Tabla.Columns.Add("DNI", "DNI");
            Tabla.Columns.Add("Plan", "Plan");
            Tabla.Columns.Add("Inicio", "Inicio");
            Tabla.Columns.Add("Vencimiento", "Vencimiento");
            Tabla.Columns.Add("Estado", "Estado");
            Tabla.Columns[1].FillWeight = 130;
            Tabla.Columns[2].FillWeight = 80;
            Tabla.Columns[3].FillWeight = 90;
            Tabla.Columns[4].FillWeight = 80;
            Tabla.Columns[5].FillWeight = 80;
            Tabla.Columns[6].FillWeight = 75;

            Tabla.SelectionChanged += Seleccionar;
            vista.Buscador.TextChanged += delegate { AplicarFiltro(); };
            inicio.ValueChanged += delegate
            {
                if (idSeleccionado == 0)
                    vencimiento.Value = inicio.Value.Date.AddMonths(1).AddDays(-1);
            };
            AlCargarEnEjecucion(delegate { Inicializar(); });
        }

        private void Inicializar()
        {
            try
            {
                CargarCombos();
                Cargar();
                NuevaMembresia(null, EventArgs.Empty);
            }
            catch (Exception ex) { MostrarError(ex); }
        }

        private void CargarCombos()
        {
            socio.DataSource = socios.ListarActivos().Select(s => new SocioMembresiaItem
            {
                IdSocio = s.IdSocio,
                Texto = s.Apellido + ", " + s.Nombre + " · DNI " + s.DNI
            }).ToList();
            socio.DisplayMember = "Texto";
            socio.ValueMember = "IdSocio";

            plan.DataSource = planes.ListarActivos();
            plan.DisplayMember = "Nombre";
            plan.ValueMember = "IdPlan";
        }

        private void Cargar()
        {
            try
            {
                membresiasCargadas = logica.ListarParaGestion();
                AplicarFiltro();
            }
            catch (Exception ex) { MostrarError(ex); }
        }

        private void AplicarFiltro()
        {
            var criterio = vista.Buscador.Text.Trim();
            var filtradas = membresiasCargadas.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(criterio))
            {
                filtradas = filtradas.Where(m => Contiene(NombreSocio(m), criterio)
                    || Contiene(m.Socio == null ? string.Empty : m.Socio.DNI, criterio)
                    || Contiene(NombrePlan(m), criterio));
            }

            cargandoTabla = true;
            Tabla.Rows.Clear();
            foreach (var membresiaActual in filtradas)
            {
                Tabla.Rows.Add(membresiaActual.IdMembresia,
                    NombreSocio(membresiaActual),
                    membresiaActual.Socio == null ? "-" : membresiaActual.Socio.DNI,
                    NombrePlan(membresiaActual),
                    membresiaActual.FechaInicio.ToString("dd/MM/yyyy"),
                    membresiaActual.FechaVencimiento.ToString("dd/MM/yyyy"),
                    membresiaActual.Estado ? "Habilitada" : "Deshabilitada");
            }
            Tabla.ClearSelection();
            cargandoTabla = false;
            Estado.Text = Tabla.Rows.Count + " membresía(s) encontrada(s)";
        }

        private void Seleccionar(object sender, EventArgs e)
        {
            if (cargandoTabla || Tabla.CurrentRow == null || !Tabla.CurrentRow.Selected) return;
            idSeleccionado = Convert.ToInt32(Tabla.CurrentRow.Cells[0].Value);
            membresiaSeleccionada = membresiasCargadas
                .FirstOrDefault(m => m.IdMembresia == idSeleccionado);
            if (membresiaSeleccionada == null) return;

            socio.SelectedValue = membresiaSeleccionada.IdSocio;
            plan.SelectedValue = membresiaSeleccionada.IdPlan;
            inicio.Value = membresiaSeleccionada.FechaInicio;
            vencimiento.Value = membresiaSeleccionada.FechaVencimiento;
            socio.Enabled = false;
            plan.Enabled = false;
            EstablecerModo(false);
            vista.TituloDetalle.Text = "Membresía de " + NombreSocio(membresiaSeleccionada)
                + " · " + (membresiaSeleccionada.Estado ? "Habilitada" : "Deshabilitada");
        }

        private void NuevaMembresia(object sender, EventArgs e)
        {
            idSeleccionado = 0;
            membresiaSeleccionada = null;
            Tabla.ClearSelection();
            socio.Enabled = true;
            plan.Enabled = true;
            if (socio.Items.Count > 0) socio.SelectedIndex = 0;
            if (plan.Items.Count > 0) plan.SelectedIndex = 0;
            inicio.Value = DateTime.Today;
            vencimiento.Value = DateTime.Today.AddMonths(1).AddDays(-1);
            EstablecerModo(true);
        }

        private void EstablecerModo(bool nueva)
        {
            var puedeCrear = socio.Items.Count > 0 && plan.Items.Count > 0;
            if (nueva)
            {
                if (plan.Items.Count == 0)
                    vista.TituloDetalle.Text = "Primero crea los planes Básico y Premium";
                else if (socio.Items.Count == 0)
                    vista.TituloDetalle.Text = "Primero registra un socio";
                else
                    vista.TituloDetalle.Text = "Nueva membresía · Estado inicial: Habilitada";
            }

            crear.Enabled = nueva && puedeCrear;
            actualizar.Enabled = !nueva;
            habilitar.Enabled = !nueva && membresiaSeleccionada != null
                && !membresiaSeleccionada.Estado;
            deshabilitar.Enabled = !nueva && membresiaSeleccionada != null
                && membresiaSeleccionada.Estado;
            generarCuota.Enabled = !nueva && membresiaSeleccionada != null
                && membresiaSeleccionada.Estado;
        }

        private void Crear(object sender, EventArgs e)
        {
            try
            {
                if (socio.SelectedValue == null || plan.SelectedValue == null)
                    throw new InvalidOperationException("Selecciona un socio y un plan.");
                logica.Crear(new Membresia
                {
                    IdSocio = Convert.ToInt32(socio.SelectedValue),
                    IdPlan = Convert.ToInt32(plan.SelectedValue),
                    IdUsuarioSistema = usuario.IdUsuarioSistema,
                    FechaInicio = inicio.Value.Date,
                    FechaVencimiento = vencimiento.Value.Date
                });
                Cargar(); NuevaMembresia(null, EventArgs.Empty);
                MostrarExito("Membresía creada y primera cuota generada.");
            }
            catch (Exception ex) { MostrarError(ex); }
        }

        private void Actualizar(object sender, EventArgs e)
        {
            try
            {
                if (membresiaSeleccionada == null)
                    throw new InvalidOperationException("Selecciona una membresía.");
                logica.Modificar(new Membresia
                {
                    IdMembresia = membresiaSeleccionada.IdMembresia,
                    IdSocio = membresiaSeleccionada.IdSocio,
                    IdPlan = membresiaSeleccionada.IdPlan,
                    IdUsuarioSistema = membresiaSeleccionada.IdUsuarioSistema,
                    FechaInicio = inicio.Value.Date,
                    FechaVencimiento = vencimiento.Value.Date,
                    Estado = membresiaSeleccionada.Estado
                });
                Cargar(); NuevaMembresia(null, EventArgs.Empty);
                MostrarExito("Membresía actualizada.");
            }
            catch (Exception ex) { MostrarError(ex); }
        }

        private void Habilitar(object sender, EventArgs e)
        {
            try
            {
                if (idSeleccionado == 0) throw new InvalidOperationException("Selecciona una membresía.");
                logica.Habilitar(idSeleccionado);
                Cargar(); NuevaMembresia(null, EventArgs.Empty);
                MostrarExito("Membresía habilitada.");
            }
            catch (Exception ex) { MostrarError(ex); }
        }

        private void Deshabilitar(object sender, EventArgs e)
        {
            try
            {
                if (idSeleccionado == 0) throw new InvalidOperationException("Selecciona una membresía.");
                if (MessageBox.Show("¿Deshabilitar la membresía seleccionada?", "Confirmar",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
                logica.Deshabilitar(idSeleccionado);
                Cargar(); NuevaMembresia(null, EventArgs.Empty);
                MostrarExito("Membresía deshabilitada.");
            }
            catch (Exception ex) { MostrarError(ex); }
        }

        private void GenerarCuota(object sender, EventArgs e)
        {
            try
            {
                if (idSeleccionado == 0) throw new InvalidOperationException("Selecciona una membresía.");
                cuotas.GenerarSiguienteCuota(idSeleccionado);
                MostrarExito("Nueva cuota generada.");
            }
            catch (Exception ex) { MostrarError(ex); }
        }

        private static string NombreSocio(Membresia membresiaActual)
        {
            return membresiaActual.Socio == null
                ? "Socio no disponible"
                : membresiaActual.Socio.Apellido + ", " + membresiaActual.Socio.Nombre;
        }

        private static string NombrePlan(Membresia membresiaActual)
        {
            return membresiaActual.Plan == null ? "Plan no disponible" : membresiaActual.Plan.Nombre;
        }

        private static bool Contiene(string valor, string criterio)
        {
            return !string.IsNullOrEmpty(valor)
                && valor.IndexOf(criterio, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private sealed class SocioMembresiaItem
        {
            public int IdSocio { get; set; }
            public string Texto { get; set; }
        }
    }

    [System.ComponentModel.DesignerCategory("Code")]
    [System.ComponentModel.DesignTimeVisible(false)]
    public abstract class GestionPagosFormBase : FormularioModuloBase
    {
        private readonly PagoLogica logica = new PagoLogica();
        private readonly CuotaMembresiaLogica cuotas = new CuotaMembresiaLogica();
        private readonly MembresiaLogica membresias = new MembresiaLogica();
        private readonly VistaListadoDetalle vista;
        private readonly ComboBox filtroEstado;
        private readonly ComboBox membresia;
        private readonly TextBox cuota;
        private readonly TextBox importe;
        private readonly ComboBox metodo;
        private readonly ComboBox estado;
        private readonly Button registrar;
        private readonly Button anular;
        private readonly Button reembolsar;
        private List<CuotaMembresia> cuotasCargadas = new List<CuotaMembresia>();
        private List<MembresiaPagoItem> membresiasCargadas = new List<MembresiaPagoItem>();
        private int idCuotaSeleccionada;
        private int idPagoSeleccionado;
        private bool cargandoTabla;
        private bool actualizandoFormulario;

        public GestionPagosFormBase()
            : base("Cuotas y pagos", "Registro de pagos y consulta de cuotas", Color.FromArgb(5, 150, 105))
        {
            vista = CrearVistaListadoDetalle("Cuotas", "Buscar por socio, DNI o plan",
                "+ Nuevo pago", NuevoPago);

            membresia = vista.AgregarCampo("Membresía del socio", new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList
            });
            cuota = vista.AgregarCampo("Período de la cuota", new TextBox
            {
                BackColor = Color.FromArgb(241, 245, 249),
                BorderStyle = BorderStyle.FixedSingle,
                ReadOnly = true
            });
            importe = vista.AgregarCampo("Importe", new TextBox
            {
                BorderStyle = BorderStyle.FixedSingle
            });
            ConfigurarEntradaDecimal(importe);
            metodo = vista.AgregarCampo("Método de pago", new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList
            });
            estado = vista.AgregarCampo("Estado del pago", new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList
            });
            estado.Items.AddRange(new object[]
            {
                EstadosTransaccionPago.Pendiente,
                EstadosTransaccionPago.Aprobado,
                EstadosTransaccionPago.Rechazado
            });

            registrar = vista.AgregarAccion("Registrar pago", Registrar, true);
            anular = vista.AgregarAccion("Anular", Anular, false, true);
            reembolsar = vista.AgregarAccion("Reembolsar", Reembolsar);

            filtroEstado = vista.AgregarFiltroListado("Mostrar", new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList
            });
            filtroEstado.Items.AddRange(new object[] { "Todas", "Pendientes", "Pagadas" });
            filtroEstado.SelectedIndex = 0;

            Tabla.Columns.Add("IdCuota", "IdCuota"); Tabla.Columns[0].Visible = false;
            Tabla.Columns.Add("IdPago", "IdPago"); Tabla.Columns[1].Visible = false;
            Tabla.Columns.Add("Socio", "Socio");
            Tabla.Columns.Add("DNI", "DNI");
            Tabla.Columns.Add("Plan", "Plan");
            Tabla.Columns.Add("Periodo", "Período");
            Tabla.Columns.Add("Importe", "Importe");
            Tabla.Columns.Add("Estado", "Estado");
            Tabla.Columns[2].FillWeight = 130;
            Tabla.Columns[3].FillWeight = 80;
            Tabla.Columns[4].FillWeight = 100;
            Tabla.Columns[5].FillWeight = 125;
            Tabla.Columns[6].FillWeight = 75;
            Tabla.Columns[7].FillWeight = 70;

            membresia.SelectedIndexChanged += CambioMembresia;
            Tabla.SelectionChanged += Seleccionar;
            vista.Buscador.TextChanged += delegate { AplicarFiltro(); };
            filtroEstado.SelectedIndexChanged += delegate { AplicarFiltro(); };
            AlCargarEnEjecucion(delegate { Inicializar(); });
        }

        private void Inicializar()
        {
            try
            {
                CargarMembresias();
                CargarMetodosPago();
                Cargar();
                NuevoPago(null, EventArgs.Empty);
            }
            catch (Exception ex) { MostrarError(ex); }
        }

        private void CargarMembresias()
        {
            membresiasCargadas = membresias.ListarParaGestion().Select(m => new MembresiaPagoItem
            {
                IdMembresia = m.IdMembresia,
                Habilitada = m.Estado,
                Texto = NombreSocio(m) + " · " + NombrePlan(m) + (m.Estado ? string.Empty : " (inactiva)")
            }).ToList();
            actualizandoFormulario = true;
            membresia.DataSource = membresiasCargadas;
            membresia.DisplayMember = "Texto";
            membresia.ValueMember = "IdMembresia";
            actualizandoFormulario = false;
        }

        private void CargarMetodosPago()
        {
            metodo.DataSource = logica.ListarMetodosPagoActivos();
            metodo.DisplayMember = "Observaciones";
            metodo.ValueMember = "IdMetodoPago";
        }

        private void Cargar()
        {
            try
            {
                cuotasCargadas = cuotas.ListarParaGestion();
                AplicarFiltro();
            }
            catch (Exception ex) { MostrarError(ex); }
        }

        private void AplicarFiltro()
        {
            var criterio = vista.Buscador.Text.Trim();
            var estadoElegido = Convert.ToString(filtroEstado.SelectedItem);
            var filtradas = cuotasCargadas.AsEnumerable();

            if (estadoElegido == "Pendientes")
                filtradas = filtradas.Where(c => c.EstadoPago == EstadosCuota.Pendiente);
            else if (estadoElegido == "Pagadas")
                filtradas = filtradas.Where(c => c.EstadoPago == EstadosCuota.Pagada);

            if (!string.IsNullOrWhiteSpace(criterio))
            {
                filtradas = filtradas.Where(c => Contiene(NombreSocio(c.Membresia), criterio)
                    || Contiene(c.Membresia == null || c.Membresia.Socio == null
                        ? string.Empty : c.Membresia.Socio.DNI, criterio)
                    || Contiene(NombrePlan(c.Membresia), criterio));
            }

            cargandoTabla = true;
            Tabla.Rows.Clear();
            foreach (var c in filtradas)
            {
                Tabla.Rows.Add(c.IdCuotaMembresia,
                    c.IdRegistroPago.HasValue ? (object)c.IdRegistroPago.Value : null,
                    NombreSocio(c.Membresia),
                    c.Membresia == null || c.Membresia.Socio == null ? "-" : c.Membresia.Socio.DNI,
                    NombrePlan(c.Membresia),
                    Periodo(c), c.Importe.ToString("C"), c.EstadoPago);
            }
            Tabla.ClearSelection();
            cargandoTabla = false;
            Estado.Text = Tabla.Rows.Count + " cuota(s) encontrada(s) · "
                + cuotasCargadas.Count(c => c.EstadoPago == EstadosCuota.Pagada) + " pagada(s)";
        }

        private void Seleccionar(object sender, EventArgs e)
        {
            if (cargandoTabla || Tabla.CurrentRow == null || !Tabla.CurrentRow.Selected) return;
            var idCuota = Convert.ToInt32(Tabla.CurrentRow.Cells[0].Value);
            var seleccionada = cuotasCargadas.FirstOrDefault(c => c.IdCuotaMembresia == idCuota);
            if (seleccionada != null) MostrarCuota(seleccionada);
        }

        private void NuevoPago(object sender, EventArgs e)
        {
            Tabla.ClearSelection();
            idCuotaSeleccionada = 0;
            idPagoSeleccionado = 0;
            var primeraActiva = membresiasCargadas.FirstOrDefault(m => m.Habilitada);
            actualizandoFormulario = true;
            if (primeraActiva != null) membresia.SelectedValue = primeraActiva.IdMembresia;
            actualizandoFormulario = false;
            SeleccionarPrimeraPendiente();
        }

        private void CambioMembresia(object sender, EventArgs e)
        {
            if (!actualizandoFormulario) SeleccionarPrimeraPendiente();
        }

        private void SeleccionarPrimeraPendiente()
        {
            var item = membresia.SelectedItem as MembresiaPagoItem;
            if (item == null || !item.Habilitada)
            {
                MostrarSinCuota("Selecciona una membresía activa.");
                return;
            }

            var pendiente = cuotasCargadas
                .Where(c => c.IdMembresia == item.IdMembresia
                    && c.EstadoPago == EstadosCuota.Pendiente
                    && !c.IdRegistroPago.HasValue)
                .OrderBy(c => c.FechaDesde).FirstOrDefault();
            if (pendiente == null)
            {
                MostrarSinCuota("La membresía no tiene cuotas pendientes disponibles.");
                return;
            }

            MostrarCuota(pendiente, true);
        }

        private void MostrarCuota(CuotaMembresia seleccionada, bool modoNuevo = false)
        {
            actualizandoFormulario = true;
            idCuotaSeleccionada = seleccionada.IdCuotaMembresia;
            idPagoSeleccionado = seleccionada.IdRegistroPago ?? 0;
            membresia.SelectedValue = seleccionada.IdMembresia;
            cuota.Text = Periodo(seleccionada);
            importe.Text = seleccionada.Importe.ToString("0.00");
            if (seleccionada.Pago != null)
            {
                metodo.SelectedValue = seleccionada.Pago.IdMetodoPago;
                estado.SelectedItem = seleccionada.Pago.Estado;
            }
            else
            {
                if (metodo.Items.Count > 0) metodo.SelectedIndex = 0;
                estado.SelectedItem = EstadosTransaccionPago.Aprobado;
            }
            actualizandoFormulario = false;
            EstablecerModo(seleccionada, modoNuevo);
        }

        private void MostrarSinCuota(string mensaje)
        {
            idCuotaSeleccionada = 0;
            idPagoSeleccionado = 0;
            cuota.Clear();
            importe.Clear();
            vista.TituloDetalle.Text = mensaje;
            registrar.Enabled = false;
            anular.Enabled = false;
            reembolsar.Enabled = false;
            importe.ReadOnly = true;
            metodo.Enabled = false;
            estado.Enabled = false;
            membresia.Enabled = true;
        }

        private void EstablecerModo(CuotaMembresia seleccionada, bool modoNuevo)
        {
            var tienePago = seleccionada.Pago != null && seleccionada.IdRegistroPago.HasValue;
            vista.TituloDetalle.Text = tienePago
                ? "Detalle del pago · " + seleccionada.Pago.Estado
                : (modoNuevo ? "Nuevo pago · Cuota pendiente" : "Registrar cuota pendiente");
            membresia.Enabled = !tienePago;
            importe.ReadOnly = tienePago;
            metodo.Enabled = !tienePago;
            estado.Enabled = !tienePago;
            registrar.Enabled = !tienePago && seleccionada.EstadoPago == EstadosCuota.Pendiente;
            anular.Enabled = tienePago
                && seleccionada.Pago.Estado != EstadosTransaccionPago.Anulado
                && seleccionada.Pago.Estado != EstadosTransaccionPago.Reembolsado;
            reembolsar.Enabled = tienePago
                && seleccionada.Pago.Estado == EstadosTransaccionPago.Aprobado;
        }

        private void Registrar(object sender, EventArgs e)
        {
            try
            {
                if (idCuotaSeleccionada <= 0)
                    throw new InvalidOperationException("Selecciona una cuota pendiente.");
                if (metodo.SelectedValue == null)
                    throw new InvalidOperationException("Selecciona un método de pago.");

                logica.RegistrarPago(new Pago
                {
                    Importe = DecimalPositivo(importe, "importe"),
                    IdMetodoPago = Convert.ToInt32(metodo.SelectedValue),
                    Estado = Convert.ToString(estado.SelectedItem),
                    Fecha = DateTime.Now,
                    Descripcion = "Pago registrado en recepción"
                }, idCuotaSeleccionada);
                Cargar(); NuevoPago(null, EventArgs.Empty);
                MostrarExito("Pago registrado correctamente.");
            }
            catch (Exception ex) { MostrarError(ex); }
        }

        private void Anular(object sender, EventArgs e)
        {
            try
            {
                if (idPagoSeleccionado == 0) throw new InvalidOperationException("Selecciona un pago.");
                if (MessageBox.Show("¿Anular el pago seleccionado?", "Confirmar anulación",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
                logica.AnularPago(idPagoSeleccionado);
                Cargar(); NuevoPago(null, EventArgs.Empty);
                MostrarExito("Pago anulado.");
            }
            catch (Exception ex) { MostrarError(ex); }
        }

        private void Reembolsar(object sender, EventArgs e)
        {
            try
            {
                if (idPagoSeleccionado == 0) throw new InvalidOperationException("Selecciona un pago.");
                if (MessageBox.Show("¿Reembolsar el pago seleccionado?", "Confirmar reembolso",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
                logica.ReembolsarPago(idPagoSeleccionado);
                Cargar(); NuevoPago(null, EventArgs.Empty);
                MostrarExito("Pago reembolsado.");
            }
            catch (Exception ex) { MostrarError(ex); }
        }

        private static string NombreSocio(Membresia membresiaActual)
        {
            return membresiaActual == null || membresiaActual.Socio == null
                ? "Socio no disponible"
                : membresiaActual.Socio.Apellido + ", " + membresiaActual.Socio.Nombre;
        }

        private static string NombrePlan(Membresia membresiaActual)
        {
            return membresiaActual == null || membresiaActual.Plan == null
                ? "Plan no disponible" : membresiaActual.Plan.Nombre;
        }

        private static string Periodo(CuotaMembresia cuotaActual)
        {
            return cuotaActual.FechaDesde.ToString("dd/MM/yyyy") + " al "
                + cuotaActual.FechaHasta.ToString("dd/MM/yyyy");
        }

        private static bool Contiene(string valor, string criterio)
        {
            return !string.IsNullOrEmpty(valor)
                && valor.IndexOf(criterio, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private sealed class MembresiaPagoItem
        {
            public int IdMembresia { get; set; }
            public string Texto { get; set; }
            public bool Habilitada { get; set; }
        }
    }

    [System.ComponentModel.DesignerCategory("Code")]
    [System.ComponentModel.DesignTimeVisible(false)]
    public abstract class GestionAsignacionesFormBase : FormularioModuloBase
    {
        private readonly MembresiaEntrenadorLogica logica = new MembresiaEntrenadorLogica();
        private readonly UsuarioSistemaLogica usuarios = new UsuarioSistemaLogica();
        private readonly TextBox membresia;
        private readonly ComboBox entrenador;
        private int idSeleccionado;

        public GestionAsignacionesFormBase()
            : base("Asignar entrenador", "Vinculacion de entrenadores con membresias", Color.FromArgb(5, 150, 105))
        {
            var grupo = CrearGrupo("Asignacion", 1060, 86, new Point(20, 160));
            membresia = Campo(grupo, "Id membresia", 15, 25, 130); entrenador = Selector(grupo, "Entrenador", 340, 25, 300); AgregarPanelFormulario(grupo);
            AgregarBoton("Asignar", Asignar, true); AgregarBoton("Cambiar", Cambiar); AgregarBoton("Consultar", Consultar); AgregarBoton("Dar de baja", DarDeBaja);
            AlCargarEnEjecucion(delegate { CargarEntrenadores(); });
            Tabla.Columns.Add("Id", "Id"); Tabla.Columns[0].Visible = false; Tabla.Columns.Add("Membresia", "Membresia"); Tabla.Columns.Add("Entrenador", "Entrenador"); Tabla.Columns.Add("Estado", "Estado");
        }

        private void CargarEntrenadores() { entrenador.DataSource = usuarios.ListarPorRol("Entrenador"); entrenador.DisplayMember = "Apellido"; entrenador.ValueMember = "IdUsuarioSistema"; }
        private void Asignar(object sender, EventArgs e) { try { var id = Entero(membresia, "membresia"); var a = logica.AsignarEntrenador(id, Convert.ToInt32(entrenador.SelectedValue)); idSeleccionado = a.IdMembresiaEntrenador; CargarLista(id); MostrarExito("Entrenador asignado."); } catch (Exception ex) { MostrarError(ex); } }
        private void Cambiar(object sender, EventArgs e) { try { var id = Entero(membresia, "membresia"); var a = logica.CambiarEntrenador(id, Convert.ToInt32(entrenador.SelectedValue)); idSeleccionado = a.IdMembresiaEntrenador; CargarLista(id); MostrarExito("Entrenador cambiado."); } catch (Exception ex) { MostrarError(ex); } }
        private void Consultar(object sender, EventArgs e) { try { var id = Entero(membresia, "membresia"); var activo = logica.ObtenerEntrenadorActivo(id); MessageBox.Show(activo == null ? "No hay entrenador activo." : activo.Nombre + " " + activo.Apellido, "Entrenador actual", MessageBoxButtons.OK, MessageBoxIcon.Information); CargarLista(id); } catch (Exception ex) { MostrarError(ex); } }
        private void CargarLista(int id) { Tabla.Rows.Clear(); foreach (var a in logica.ListarPorMembresia(id)) Tabla.Rows.Add(a.IdMembresiaEntrenador, a.IdMembresia, a.Entrenador == null ? a.IdEntrenador.ToString() : a.Entrenador.Nombre + " " + a.Entrenador.Apellido, a.Estado ? "Activo" : "Historico"); }
        private void DarDeBaja(object sender, EventArgs e) { try { if (idSeleccionado == 0) throw new InvalidOperationException("Selecciona una asignacion."); logica.DarDeBajaAsignacion(idSeleccionado); CargarLista(Entero(membresia, "membresia")); MostrarExito("Asignacion dada de baja."); } catch (Exception ex) { MostrarError(ex); } }
    }
}
