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
    /* Presenta pagos y atiende sus acciones mediante eventos de Windows Forms. */
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
        /* Inicializa los componentes existentes y las dependencias de la pantalla sin consultar la base de datos. */
        public GestionPagosForm()
        {
            InitializeComponent();
        }

        /* Carga las opciones y los registros necesarios y prepara el formulario para una nueva operación. */
        private void Inicializar()
        {
            try
            {
                CargarMembresias();
                CargarMetodosPago();
                Cargar();
                nuevo_Click(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                FormularioVisualHelper.MostrarError(lblEstado, ex);
            }
        }

        /* Carga las membresías disponibles y sus datos de presentación para seleccionarlas. */
        private void CargarMembresias()
        {
            membresiasCargadas = membresias.ListarParaGestion().Select(m => new MembresiaPagoItem { IdMembresia = m.IdMembresia, Habilitada = m.Estado, Texto = NombreSocio(m) + " - " + NombrePlan(m) + (m.Estado ? string.Empty : " (inactiva)") }).ToList();
            actualizandoFormulario = true;
            membresia.DataSource = membresiasCargadas;
            membresia.DisplayMember = "Texto";
            membresia.ValueMember = "IdMembresia";
            actualizandoFormulario = false;
        }

        /* Carga los métodos de pago activos que pueden utilizarse para registrar un cobro. */
        private void CargarMetodosPago()
        {
            metodo.DataSource = logica.ListarMetodosPagoActivos();
            metodo.DisplayMember = "Observaciones";
            metodo.ValueMember = "IdMetodoPago";
        }

        /* Consulta los registros del módulo y actualiza la grilla, informando los errores de carga. */
        private void Cargar()
        {
            try
            {
                cuotasCargadas = cuotas.ListarParaGestion();
                AplicarFiltro();
            }
            catch (Exception ex)
            {
                FormularioVisualHelper.MostrarError(lblEstado, ex);
            }
        }

        /* Filtra los registros cargados por el criterio ingresado y actualiza la grilla y su contador. */
        private void AplicarFiltro()
        {
            var criterio = buscador.Text.Trim();
            var elegido = Convert.ToString(filtroEstado.SelectedItem);
            var filtradas = cuotasCargadas.AsEnumerable();
            if (elegido == "Pendientes")
                filtradas = filtradas.Where(c => c.EstadoPago == EstadosCuota.Pendiente);
            else if (elegido == "Pagadas")
                filtradas = filtradas.Where(c => c.EstadoPago == EstadosCuota.Pagada);
            if (!string.IsNullOrWhiteSpace(criterio))
                filtradas = filtradas.Where(c => Contiene(NombreSocio(c.Membresia), criterio) || Contiene(c.Membresia == null || c.Membresia.Socio == null ? string.Empty : c.Membresia.Socio.DNI, criterio) || Contiene(NombrePlan(c.Membresia), criterio));
            cargandoTabla = true;
            tabla.Rows.Clear();
            foreach (var c in filtradas)
                tabla.Rows.Add(c.IdCuotaMembresia, c.IdRegistroPago.HasValue ? (object)c.IdRegistroPago.Value : null, NombreSocio(c.Membresia), c.Membresia == null || c.Membresia.Socio == null ? "-" : c.Membresia.Socio.DNI, NombrePlan(c.Membresia), Periodo(c), c.Importe.ToString("C"), c.EstadoPago);
            tabla.ClearSelection();
            cargandoTabla = false;
            lblEstado.Text = tabla.Rows.Count + " cuota(s) encontrada(s) - " + cuotasCargadas.Count(c => c.EstadoPago == EstadosCuota.Pagada) + " pagada(s)";
        }

        /* Al cambiar la fila seleccionada, toma su identificador y actualiza los datos o acciones del registro. */
        private void tabla_SelectionChanged(object sender, EventArgs e)
        {
            if (cargandoTabla || tabla.CurrentRow == null || !tabla.CurrentRow.Selected)
                return;
            var idCuota = Convert.ToInt32(tabla.CurrentRow.Cells[0].Value);
            var seleccionada = cuotasCargadas.FirstOrDefault(c => c.IdCuotaMembresia == idCuota);
            if (seleccionada != null)
                MostrarCuota(seleccionada);
        }

        /* Al hacer clic en nuevo, limpia la selección y prepara el registro de nuevos datos. */
        private void nuevo_Click(object sender, EventArgs e)
        {
            tabla.ClearSelection();
            idCuotaSeleccionada = 0;
            idPagoSeleccionado = 0;
            var primeraActiva = membresiasCargadas.FirstOrDefault(m => m.Habilitada);
            actualizandoFormulario = true;
            if (primeraActiva != null)
                membresia.SelectedValue = primeraActiva.IdMembresia;
            actualizandoFormulario = false;
            SeleccionarPrimeraPendiente();
        }

        /* Al elegir otra membresía, prepara su primera cuota pendiente si no se están cargando los controles. */
        private void membresia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!actualizandoFormulario)
                SeleccionarPrimeraPendiente();
        }

        /* Busca la primera cuota sin pago de la membresía elegida y prepara su registro. */
        private void SeleccionarPrimeraPendiente()
        {
            var item = membresia.SelectedItem as MembresiaPagoItem;
            if (item == null || !item.Habilitada)
            {
                MostrarSinCuota("Selecciona una membresia activa.");
                return;
            }

            var pendiente = cuotasCargadas.Where(c => c.IdMembresia == item.IdMembresia && c.EstadoPago == EstadosCuota.Pendiente && !c.IdRegistroPago.HasValue).OrderBy(c => c.FechaDesde).FirstOrDefault();
            if (pendiente == null)
            {
                MostrarSinCuota("La membresia no tiene cuotas pendientes disponibles.");
                return;
            }

            MostrarCuota(pendiente, true);
        }

        /* Carga el período y los datos de la cuota seleccionada y habilita las acciones correspondientes. */
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
                if (metodo.Items.Count > 0)
                    metodo.SelectedIndex = 0;
                estado.SelectedItem = EstadosTransaccionPago.Aprobado;
            }

            actualizandoFormulario = false;
            EstablecerModo(seleccionada, modoNuevo);
        }

        /* Limpia la selección y deshabilita las operaciones cuando no hay una cuota disponible. */
        private void MostrarSinCuota(string mensaje)
        {
            idCuotaSeleccionada = 0;
            idPagoSeleccionado = 0;
            cuota.Clear();
            importe.Clear();
            lblFormulario.Text = mensaje;
            registrar.Enabled = false;
            anular.Enabled = false;
            reembolsar.Enabled = false;
            importe.ReadOnly = true;
            metodo.Enabled = false;
            estado.Enabled = false;
            membresia.Enabled = true;
        }

        /* Habilita las acciones disponibles según la selección y el estado del registro. */
        private void EstablecerModo(CuotaMembresia seleccionada, bool modoNuevo)
        {
            var tienePago = seleccionada.Pago != null && seleccionada.IdRegistroPago.HasValue;
            lblFormulario.Text = tienePago ? "Detalle del pago - " + seleccionada.Pago.Estado : (modoNuevo ? "Nuevo pago - Cuota pendiente" : "Registrar cuota pendiente");
            membresia.Enabled = !tienePago;
            importe.ReadOnly = tienePago;
            metodo.Enabled = !tienePago;
            estado.Enabled = !tienePago;
            registrar.Enabled = !tienePago && seleccionada.EstadoPago == EstadosCuota.Pendiente;
            anular.Enabled = tienePago && seleccionada.Pago.Estado != EstadosTransaccionPago.Anulado && seleccionada.Pago.Estado != EstadosTransaccionPago.Reembolsado;
            reembolsar.Enabled = tienePago && seleccionada.Pago.Estado == EstadosTransaccionPago.Aprobado;
        }

        /* Al hacer clic en registrar, valida los campos y registra la operación mediante la capa lógica. */
        private void registrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (idCuotaSeleccionada <= 0)
                    throw new InvalidOperationException("Selecciona una cuota pendiente.");
                if (metodo.SelectedValue == null)
                    throw new InvalidOperationException("Selecciona un metodo de pago.");
                logica.RegistrarPago(new Pago { Importe = FormularioVisualHelper.DecimalPositivo(importe, "importe"), IdMetodoPago = Convert.ToInt32(metodo.SelectedValue), Estado = Convert.ToString(estado.SelectedItem), Fecha = DateTime.Now, Descripcion = "Pago registrado en recepcion" }, idCuotaSeleccionada);
                Cargar();
                nuevo_Click(null, EventArgs.Empty);
                FormularioVisualHelper.MostrarExito(lblEstado, "Pago registrado correctamente.");
            }
            catch (Exception ex)
            {
                FormularioVisualHelper.MostrarError(lblEstado, ex);
            }
        }

        /* Al hacer clic en anular, solicita confirmación y anula el pago seleccionado. */
        private void anular_Click(object sender, EventArgs e)
        {
            try
            {
                if (idPagoSeleccionado == 0)
                    throw new InvalidOperationException("Selecciona un pago.");
                if (MessageBox.Show("Anular el pago seleccionado?", "Confirmar anulacion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;
                logica.AnularPago(idPagoSeleccionado);
                Cargar();
                nuevo_Click(null, EventArgs.Empty);
                FormularioVisualHelper.MostrarExito(lblEstado, "Pago anulado.");
            }
            catch (Exception ex)
            {
                FormularioVisualHelper.MostrarError(lblEstado, ex);
            }
        }

        /* Al hacer clic en reembolsar, solicita confirmación y reembolsa el pago aprobado. */
        private void reembolsar_Click(object sender, EventArgs e)
        {
            try
            {
                if (idPagoSeleccionado == 0)
                    throw new InvalidOperationException("Selecciona un pago.");
                if (MessageBox.Show("Reembolsar el pago seleccionado?", "Confirmar reembolso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;
                logica.ReembolsarPago(idPagoSeleccionado);
                Cargar();
                nuevo_Click(null, EventArgs.Empty);
                FormularioVisualHelper.MostrarExito(lblEstado, "Pago reembolsado.");
            }
            catch (Exception ex)
            {
                FormularioVisualHelper.MostrarError(lblEstado, ex);
            }
        }

        /* Compone el nombre que se muestra en pantalla y contempla socios no disponibles. */
        private static string NombreSocio(Membresia m)
        {
            return m == null || m.Socio == null ? "Socio no disponible" : m.Socio.Apellido + ", " + m.Socio.Nombre;
        }

        /* Obtiene el nombre del plan para mostrarlo, contemplando relaciones no disponibles. */
        private static string NombrePlan(Membresia m)
        {
            return m == null || m.Plan == null ? "Plan no disponible" : m.Plan.Nombre;
        }

        /* Presenta el inicio y el fin de la cuota con el formato de fecha usado en la pantalla. */
        private static string Periodo(CuotaMembresia c)
        {
            return c.FechaDesde.ToString("dd/MM/yyyy") + " al " + c.FechaHasta.ToString("dd/MM/yyyy");
        }

        /* Compara el texto de búsqueda sin distinguir mayúsculas y admite valores vacíos. */
        private static bool Contiene(string valor, string criterio)
        {
            return !string.IsNullOrEmpty(valor) && valor.IndexOf(criterio, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        /* Asocia una membresía y su habilitación con el texto mostrado al registrar pagos. */
        private sealed class MembresiaPagoItem
        {
            public int IdMembresia { get; set; }
            public string Texto { get; set; }
            public bool Habilitada { get; set; }
        }

        /* Al cargar la pantalla en ejecución, prepara sus datos iniciales sin realizar consultas desde el diseñador. */
        private void GestionPagosForm_Load(object sender, EventArgs e)
        {
            if (FormularioVisualHelper.EnModoDisenio(this))
                return;
            try
            {
                Inicializar();
            }
            catch (Exception ex)
            {
                FormularioVisualHelper.MostrarError(lblEstado, ex);
            }
        }

        /* Al hacer clic en btnVolver, cierra el módulo y devuelve el control al dashboard. */
        private void btnVolver_Click(object sender, EventArgs e)
        {
            Close();
        }

        /* Al escribir un criterio de búsqueda, filtra los registros que se muestran en la grilla. */
        private void buscador_TextChanged(object sender, EventArgs e)
        {
            AplicarFiltro();
        }

        /* Al cambiar el filtro de estado, actualiza los registros visibles en la grilla. */
        private void filtroEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltro();
        }

        /* Al escribir en el campo, permite números y un único separador decimal mediante la validación visual compartida. */
        private void importe_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormularioVisualHelper.ValidarEntradaDecimal(importe, e);
        }
    }
}
