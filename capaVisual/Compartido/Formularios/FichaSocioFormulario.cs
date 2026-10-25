using exxen2._0.capaLogica.Navegacion;
using exxen2._0.capaVisual.Compartido.Utilidades;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaLogica;
using exxen2._0.capaLogica.Reportes;
using exxen2._0.capaVisual.Compartido.Controles;

namespace exxen2._0.capaVisual.Compartido
{
    [DesignerCategory("Form")]
    public partial class FichaSocioFormulario : Form
    {
        private readonly int idSocio;
        private readonly FichaSocioLogica logica;
        private readonly ReportesPagosServicio reportes;
        private FichaSocioResultado ficha;
        public event EventHandler<AccionEstadoSocioEventArgs> AccionSolicitada;

        public FichaSocioFormulario() : this(0, null) { }
        public FichaSocioFormulario(int idSocio, UsuarioSistema usuario)
        {
            this.idSocio = idSocio;
            var idUsuario = usuario == null ? 0 : usuario.IdUsuarioSistema;
            logica = new FichaSocioLogica(idUsuario);
            reportes = new ReportesPagosServicio(idUsuario);
            InitializeComponent();
        }

        private void FichaSocioFormulario_Load(object sender, EventArgs e)
        {
            if (AyudaFormularioVisual.EnModoDisenio(this)) return;
            try
            {
                ficha = logica.Obtener(idSocio);
                var s = ficha.DatosSocio;
                lblNombre.Text = s.Nombre + " " + s.Apellido;
                lblDatos.Text = "DNI: " + s.DNI + "   ·   " + (ficha.Estado.Activo ? "Activo" : "Inactivo") +
                    "\r\nSexo: " + (s.Sexo == "M" ? "Masculino" : s.Sexo == "F" ? "Femenino" : "Sin información") +
                    "   ·   Nacimiento: " + Fecha(s.FechaNacimiento) + "\r\nAlta: " + Fecha(s.FechaAlta) + "   ·   Antigüedad: " + ficha.Antiguedad;
                AyudaFormularioVisual.MostrarFotoRuta(fotoSocio, s.FotoRuta, s.Sexo);
                lblMembresia.Text = "MEMBRESÍA\r\n" + ficha.Estado.Plan + " · " + ficha.Estado.EstadoMembresia +
                    "\r\nInicio: " + Fecha(ficha.InicioMembresia) + " · Cuotas hasta: " + Fecha(ficha.CubiertaHasta) +
                    "\r\n" + ficha.Estado.EstadoDeuda + " · Límite: " + ficha.LimiteDeuda + " vencidas";
                lblEntrenador.Text = "ENTRENADOR\r\n" + ficha.Entrenador + "\r\n" + ficha.EstadoAsignacion;
                lblRutina.Text = "RUTINA\r\n" + ficha.Rutina + "\r\nEjercicios: " + ficha.CantidadEjercicios;
                lblFinanzas.Text = "RESUMEN FINANCIERO\r\nTotal abonado histórico: " + ficha.TotalAbonado.ToString("C") +
                    "   ·   Pagos: " + ficha.CantidadPagos + "   ·   Cuotas pagadas históricas: " + ficha.CuotasPagadas +
                    "\r\nMembresía actual — Deuda: " + ficha.Estado.DeudaTotal.ToString("C") + "   ·   Pendientes: " + ficha.Estado.CuotasPendientes +
                    "   ·   Vencidas: " + ficha.Estado.CuotasVencidas + "\r\nÚltimo pago: " + Fecha(ficha.UltimoPago) +
                    "   ·   Próximo vencimiento: " + Fecha(ficha.Estado.ProximoVencimiento);
                MostrarPagos(false);
                ActualizarAcciones();
            }
            catch (Exception ex) { lblEstado.Text = "No se pudo cargar la ficha: " + ex.Message; }
        }

        private void MostrarPagos(bool todos)
        {
            tabla.Rows.Clear();
            foreach (var pago in todos ? ficha.Pagos : ficha.Pagos.Take(20))
            {
                var fechaPago = pago.Fecha.TimeOfDay == TimeSpan.Zero ? pago.Fecha.ToString("dd/MM/yyyy") : pago.Fecha.ToString("dd/MM/yyyy HH:mm");
                var indice = tabla.Rows.Add(fechaPago, pago.Desde.ToString("dd/MM/yyyy") + " - " + pago.Hasta.ToString("dd/MM/yyyy"),
                    pago.Metodo + (pago.Estado == EstadosTransaccionPago.Aprobado ? "" : " (" + pago.Estado + ")"),
                    pago.Importe.ToString("C"), pago.RegistradoPor);
                tabla.Rows[indice].Tag = pago;
            }
            tabla.ClearSelection();
            lblEstado.Text = ficha.Pagos.Count == 0 ? "Sin pagos registrados" : "Mostrando " + tabla.Rows.Count + " de " + ficha.Pagos.Count + " pagos (" + ficha.CantidadPagos + " aprobados)";
            ActualizarAcciones();
        }

        private ReportesPagosServicio.PagoExportable PagoSeleccionado()
        {
            return tabla.CurrentRow != null && tabla.CurrentRow.Selected ? tabla.CurrentRow.Tag as ReportesPagosServicio.PagoExportable : null;
        }
        private void tabla_SelectionChanged(object sender, EventArgs e)
        {
            ActualizarAcciones();
        }
        private void tabla_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right || e.RowIndex < 0) return;
            var celda = e.ColumnIndex >= 0
                ? tabla.Rows[e.RowIndex].Cells[e.ColumnIndex]
                : tabla.Rows[e.RowIndex].Cells.Cast<DataGridViewCell>().First(c => c.Visible);
            tabla.CurrentCell = celda;
            tabla.ClearSelection();
            tabla.Rows[e.RowIndex].Selected = true;
            ActualizarAcciones();
        }
        private void ActualizarAcciones()
        {
            var cargada = ficha != null;
            var tieneMembresia = cargada && ficha.Estado.IdMembresia > 0;
            btnHistorial.Enabled = cargada;
            btnPago.Enabled = tieneMembresia;
            verMembresiaMenu.Enabled = tieneMembresia;
            verRutinaMenu.Enabled = cargada && ficha.TieneRutina;
            btnExportarHistorial.Enabled = cargada;
            var pago = PagoSeleccionado();
            exportarPagoMenu.Enabled = pago != null && pago.Estado == EstadosTransaccionPago.Aprobado;
        }
        private void btnHistorial_Click(object sender, EventArgs e) { if (ficha != null) MostrarPagos(true); }
        private void btnPago_Click(object sender, EventArgs e) { Solicitar(AccionEstadoSocio.RegistrarPago); }
        private void btnMembresia_Click(object sender, EventArgs e) { Solicitar(AccionEstadoSocio.VerMembresia); }
        private void Solicitar(AccionEstadoSocio accion)
        {
            var evento = AccionSolicitada;
            if (ficha != null && evento != null) evento(this, new AccionEstadoSocioEventArgs(idSocio, accion));
        }
        private void btnRutina_Click(object sender, EventArgs e)
        {
            if (ficha == null || !ficha.TieneRutina) return;
            using (var rutina = new RutinaSemanalFormulario(idSocio, lblNombre.Text, Color.FromArgb(79, 70, 229), ficha.IdRutina)) rutina.ShowDialog(this);
        }
        private void btnExportarPago_Click(object sender, EventArgs e) { Exportar(false); }
        private void btnExportarTodos_Click(object sender, EventArgs e) { Exportar(true); }
        private void Exportar(bool todos)
        {
            if (ficha == null) return;
            var pago = PagoSeleccionado();
            if (!todos && (pago == null || pago.Estado != EstadosTransaccionPago.Aprobado)) return;
            try
            {
                dialogoPdf.FileName = todos ? reportes.NombreHistorialSugerido(idSocio) : reportes.NombreComprobanteSugerido(pago.Id);
                if (dialogoPdf.ShowDialog(this) != DialogResult.OK) return;
                if (todos) reportes.GenerarHistorial(idSocio, dialogoPdf.FileName);
                else reportes.GenerarComprobante(pago.Id, dialogoPdf.FileName);
                lblEstado.Text = "PDF guardado correctamente.";
            }
            catch (Exception ex) { lblEstado.Text = "No se pudo exportar: " + ex.Message; }
        }
        private static string Fecha(DateTime? fecha) { return fecha.HasValue ? fecha.Value.ToString("dd/MM/yyyy") : "Sin información"; }
    }
}
