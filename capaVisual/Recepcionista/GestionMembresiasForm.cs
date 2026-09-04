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
    public partial class GestionMembresiasForm : Form
    {
        private readonly UsuarioSistema usuario;
        private readonly MembresiaLogica logica = new MembresiaLogica();
        private readonly SocioLogica socios = new SocioLogica();
        private readonly PlanLogica planes = new PlanLogica();
        private readonly CuotaMembresiaLogica cuotas = new CuotaMembresiaLogica();
        private List<Membresia> membresiasCargadas = new List<Membresia>();
        private Membresia membresiaSeleccionada;
        private int idSeleccionado;
        private bool cargandoTabla;

        public GestionMembresiasForm()
            : this(new UsuarioSistema { Nombre = "Recepcionista", Apellido = "de diseno" })
        {
        }

        public GestionMembresiasForm(UsuarioSistema usuario)
            : this(usuario, Color.FromArgb(5, 150, 105))
        {
        }

        public GestionMembresiasForm(UsuarioSistema usuario, Color colorPrimario)
        {
            if (usuario == null) throw new ArgumentNullException("usuario");
            this.usuario = usuario;
            InitializeComponent();
            btnVolver.Click += delegate { Close(); };
            tabla.SelectionChanged += Seleccionar;
            buscador.TextChanged += delegate { AplicarFiltro(); };
            inicio.ValueChanged += delegate { if (idSeleccionado == 0) vencimiento.Value = inicio.Value.Date.AddMonths(1).AddDays(-1); };
            FormularioVisualHelper.AlCargarEnEjecucion(this, delegate { Inicializar(); });
        }

        private void Inicializar()
        {
            try { CargarCombos(); Cargar(); NuevaMembresia(null, EventArgs.Empty); }
            catch (Exception ex) { FormularioVisualHelper.MostrarError(lblEstado, ex); }
        }

        private void CargarCombos()
        {
            socio.DataSource = socios.ListarActivos().Select(s => new SocioMembresiaItem { IdSocio = s.IdSocio, Texto = s.Apellido + ", " + s.Nombre + " - DNI " + s.DNI }).ToList();
            socio.DisplayMember = "Texto"; socio.ValueMember = "IdSocio";
            plan.DataSource = planes.ListarActivos(); plan.DisplayMember = "Nombre"; plan.ValueMember = "IdPlan";
        }

        private void Cargar()
        {
            try { membresiasCargadas = logica.ListarParaGestion(); AplicarFiltro(); }
            catch (Exception ex) { FormularioVisualHelper.MostrarError(lblEstado, ex); }
        }

        private void AplicarFiltro()
        {
            var criterio = buscador.Text.Trim(); var filtradas = membresiasCargadas.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(criterio)) filtradas = filtradas.Where(m => Contiene(NombreSocio(m), criterio) || Contiene(m.Socio == null ? string.Empty : m.Socio.DNI, criterio) || Contiene(NombrePlan(m), criterio));
            cargandoTabla = true; tabla.Rows.Clear();
            foreach (var membresiaActual in filtradas) tabla.Rows.Add(membresiaActual.IdMembresia, NombreSocio(membresiaActual), membresiaActual.Socio == null ? "-" : membresiaActual.Socio.DNI, NombrePlan(membresiaActual), membresiaActual.FechaInicio.ToString("dd/MM/yyyy"), membresiaActual.FechaVencimiento.ToString("dd/MM/yyyy"), membresiaActual.Estado ? "Habilitada" : "Deshabilitada");
            tabla.ClearSelection(); cargandoTabla = false; lblEstado.Text = tabla.Rows.Count + " membresia(s) encontrada(s)";
        }

        private void Seleccionar(object sender, EventArgs e)
        {
            if (cargandoTabla || tabla.CurrentRow == null || !tabla.CurrentRow.Selected) return;
            idSeleccionado = Convert.ToInt32(tabla.CurrentRow.Cells[0].Value); membresiaSeleccionada = membresiasCargadas.FirstOrDefault(m => m.IdMembresia == idSeleccionado); if (membresiaSeleccionada == null) return;
            socio.SelectedValue = membresiaSeleccionada.IdSocio; plan.SelectedValue = membresiaSeleccionada.IdPlan; inicio.Value = membresiaSeleccionada.FechaInicio; vencimiento.Value = membresiaSeleccionada.FechaVencimiento; socio.Enabled = false; plan.Enabled = false; EstablecerModo(false); lblFormulario.Text = "Membresia de " + NombreSocio(membresiaSeleccionada) + " - " + (membresiaSeleccionada.Estado ? "Habilitada" : "Deshabilitada");
        }

        private void NuevaMembresia(object sender, EventArgs e)
        {
            idSeleccionado = 0; membresiaSeleccionada = null; tabla.ClearSelection(); socio.Enabled = true; plan.Enabled = true; if (socio.Items.Count > 0) socio.SelectedIndex = 0; if (plan.Items.Count > 0) plan.SelectedIndex = 0; inicio.Value = DateTime.Today; vencimiento.Value = DateTime.Today.AddMonths(1).AddDays(-1); EstablecerModo(true);
        }

        private void EstablecerModo(bool nueva)
        {
            var puedeCrear = socio.Items.Count > 0 && plan.Items.Count > 0;
            if (nueva) lblFormulario.Text = plan.Items.Count == 0 ? "Primero crea un plan" : (socio.Items.Count == 0 ? "Primero registra un socio" : "Nueva membresia - Estado inicial: Habilitada");
            crear.Enabled = nueva && puedeCrear; actualizar.Enabled = !nueva; habilitar.Enabled = !nueva && membresiaSeleccionada != null && !membresiaSeleccionada.Estado; deshabilitar.Enabled = !nueva && membresiaSeleccionada != null && membresiaSeleccionada.Estado; generarCuota.Enabled = !nueva && membresiaSeleccionada != null && membresiaSeleccionada.Estado;
        }

        private void Crear(object sender, EventArgs e)
        {
            try { if (socio.SelectedValue == null || plan.SelectedValue == null) throw new InvalidOperationException("Selecciona un socio y un plan."); logica.Crear(new Membresia { IdSocio = Convert.ToInt32(socio.SelectedValue), IdPlan = Convert.ToInt32(plan.SelectedValue), IdUsuarioSistema = usuario.IdUsuarioSistema, FechaInicio = inicio.Value.Date, FechaVencimiento = vencimiento.Value.Date }); Cargar(); NuevaMembresia(null, EventArgs.Empty); FormularioVisualHelper.MostrarExito(lblEstado, "Membresia creada y primera cuota generada."); }
            catch (Exception ex) { FormularioVisualHelper.MostrarError(lblEstado, ex); }
        }

        private void Actualizar(object sender, EventArgs e)
        {
            try { if (membresiaSeleccionada == null) throw new InvalidOperationException("Selecciona una membresia."); logica.Modificar(new Membresia { IdMembresia = membresiaSeleccionada.IdMembresia, IdSocio = membresiaSeleccionada.IdSocio, IdPlan = membresiaSeleccionada.IdPlan, IdUsuarioSistema = membresiaSeleccionada.IdUsuarioSistema, FechaInicio = inicio.Value.Date, FechaVencimiento = vencimiento.Value.Date, Estado = membresiaSeleccionada.Estado }); Cargar(); NuevaMembresia(null, EventArgs.Empty); FormularioVisualHelper.MostrarExito(lblEstado, "Membresia actualizada."); }
            catch (Exception ex) { FormularioVisualHelper.MostrarError(lblEstado, ex); }
        }

        private void Habilitar(object sender, EventArgs e)
        {
            try { if (idSeleccionado == 0) throw new InvalidOperationException("Selecciona una membresia."); logica.Habilitar(idSeleccionado); Cargar(); NuevaMembresia(null, EventArgs.Empty); FormularioVisualHelper.MostrarExito(lblEstado, "Membresia habilitada."); }
            catch (Exception ex) { FormularioVisualHelper.MostrarError(lblEstado, ex); }
        }

        private void Deshabilitar(object sender, EventArgs e)
        {
            try { if (idSeleccionado == 0) throw new InvalidOperationException("Selecciona una membresia."); if (MessageBox.Show("Deshabilitar la membresia seleccionada?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return; logica.Deshabilitar(idSeleccionado); Cargar(); NuevaMembresia(null, EventArgs.Empty); FormularioVisualHelper.MostrarExito(lblEstado, "Membresia deshabilitada."); }
            catch (Exception ex) { FormularioVisualHelper.MostrarError(lblEstado, ex); }
        }

        private void GenerarCuota(object sender, EventArgs e)
        {
            try { if (idSeleccionado == 0) throw new InvalidOperationException("Selecciona una membresia."); cuotas.GenerarSiguienteCuota(idSeleccionado); FormularioVisualHelper.MostrarExito(lblEstado, "Nueva cuota generada."); }
            catch (Exception ex) { FormularioVisualHelper.MostrarError(lblEstado, ex); }
        }

        private static string NombreSocio(Membresia membresiaActual) { return membresiaActual.Socio == null ? "Socio no disponible" : membresiaActual.Socio.Apellido + ", " + membresiaActual.Socio.Nombre; }
        private static string NombrePlan(Membresia membresiaActual) { return membresiaActual.Plan == null ? "Plan no disponible" : membresiaActual.Plan.Nombre; }
        private static bool Contiene(string valor, string criterio) { return !string.IsNullOrEmpty(valor) && valor.IndexOf(criterio, StringComparison.OrdinalIgnoreCase) >= 0; }

        private sealed class SocioMembresiaItem
        {
            public int IdSocio { get; set; }
            public string Texto { get; set; }
        }
    }
}
