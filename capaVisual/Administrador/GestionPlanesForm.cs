using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaLogica;
using exxen2._0.capaVisual.Compartido;

namespace exxen2._0.capaVisual.Administrador
{
    [DesignerCategory("Form")]
    public partial class GestionPlanesForm : Form
    {
        private readonly PlanLogica logica = new PlanLogica();
        private readonly RutinaLogica rutinas = new RutinaLogica();
        private List<Plan> planesCargados = new List<Plan>();
        private int idSeleccionado;
        private bool cargandoTabla;
        private bool estadoSeleccionado = true;

        public GestionPlanesForm()
        {
            InitializeComponent();
            btnVolver.Click += delegate { Close(); };
            FormularioVisualHelper.ConfigurarEntradaDecimal(precio);
            tabla.SelectionChanged += Seleccionar;
            buscador.TextChanged += delegate { AplicarFiltro(); };
            filtroEstado.SelectedIndexChanged += delegate { AplicarFiltro(); };
            FormularioVisualHelper.AlCargarEnEjecucion(this, delegate { Inicializar(); });
        }

        private void Inicializar()
        {
            try { CargarRutinas(); Cargar(); NuevoPlan(null, EventArgs.Empty); }
            catch (Exception ex) { FormularioVisualHelper.MostrarError(lblEstado, ex); }
        }

        private void CargarRutinas()
        {
            rutina.DataSource = rutinas.ListarActivas(); rutina.DisplayMember = "Nombre"; rutina.ValueMember = "IdRutina";
        }

        private void Cargar()
        {
            try { planesCargados = logica.ListarParaGestion(); AplicarFiltro(); }
            catch (Exception ex) { FormularioVisualHelper.MostrarError(lblEstado, ex); }
        }

        private void AplicarFiltro()
        {
            var criterio = buscador.Text.Trim(); var estadoElegido = Convert.ToString(filtroEstado.SelectedItem);
            var filtrados = planesCargados.AsEnumerable();
            if (estadoElegido == "Activos") filtrados = filtrados.Where(p => p.Estado);
            else if (estadoElegido == "Inactivos") filtrados = filtrados.Where(p => !p.Estado);
            if (!string.IsNullOrWhiteSpace(criterio)) filtrados = filtrados.Where(p => Contiene(p.Nombre, criterio) || Contiene(p.Descripcion, criterio));
            cargandoTabla = true; tabla.Rows.Clear();
            foreach (var plan in filtrados) tabla.Rows.Add(plan.IdPlan, plan.Nombre, plan.Precio.ToString("C"), plan.Rutina == null ? "Sin rutina" : plan.Rutina.Nombre, DescribirBeneficios(plan), plan.Estado ? "Activo" : "Inactivo");
            tabla.ClearSelection(); cargandoTabla = false; lblEstado.Text = tabla.Rows.Count + " plan(es) encontrado(s)";
        }

        private static string DescribirBeneficios(Plan plan)
        {
            if (plan.IncluyeEntrenador && plan.IncluyeRutinaPersonal) return "Entrenador y rutina personalizada";
            if (plan.IncluyeEntrenador) return "Entrenador";
            if (plan.IncluyeRutinaPersonal) return "Rutina personalizada";
            return "Plan basico";
        }

        private static bool Contiene(string valor, string criterio)
        {
            return !string.IsNullOrEmpty(valor) && valor.IndexOf(criterio, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private void Seleccionar(object sender, EventArgs e)
        {
            if (cargandoTabla || tabla.CurrentRow == null || !tabla.CurrentRow.Selected) return;
            try
            {
                idSeleccionado = Convert.ToInt32(tabla.CurrentRow.Cells[0].Value); var plan = logica.ObtenerPorId(idSeleccionado); if (plan == null) return;
                estadoSeleccionado = plan.Estado; nombre.Text = plan.Nombre; descripcion.Text = plan.Descripcion; precio.Text = plan.Precio.ToString("0.00"); rutina.SelectedValue = plan.IdRutina; incluyeEntrenador.Checked = plan.IncluyeEntrenador; incluyeRutina.Checked = plan.IncluyeRutinaPersonal; EstablecerModo(false, plan.Estado);
            }
            catch (Exception ex) { FormularioVisualHelper.MostrarError(lblEstado, ex); }
        }

        private void NuevoPlan(object sender, EventArgs e)
        {
            idSeleccionado = 0; estadoSeleccionado = true; nombre.Clear(); descripcion.Clear(); precio.Clear(); incluyeEntrenador.Checked = false; incluyeRutina.Checked = false; if (rutina.Items.Count > 0) rutina.SelectedIndex = 0; tabla.ClearSelection(); EstablecerModo(true, true); nombre.Focus();
        }

        private void EstablecerModo(bool nuevoRegistro, bool activo)
        {
            lblFormulario.Text = nuevoRegistro ? "Nuevo plan - Estado inicial: Activo" : "Editar plan"; guardar.Enabled = nuevoRegistro; actualizar.Enabled = !nuevoRegistro; darDeBaja.Enabled = !nuevoRegistro && activo; reactivar.Enabled = !nuevoRegistro && !activo;
        }

        private Plan LeerPlan()
        {
            if (rutina.SelectedValue == null) throw new InvalidOperationException("Selecciona una rutina base.");
            return new Plan { IdPlan = idSeleccionado, Nombre = nombre.Text.Trim(), Descripcion = descripcion.Text.Trim(), Precio = FormularioVisualHelper.DecimalPositivo(precio, "precio"), IdRutina = Convert.ToInt32(rutina.SelectedValue), IncluyeEntrenador = incluyeEntrenador.Checked, IncluyeRutinaPersonal = incluyeRutina.Checked, Estado = estadoSeleccionado };
        }

        private void GuardarNuevo(object sender, EventArgs e)
        {
            try { if (idSeleccionado != 0) return; logica.Crear(LeerPlan()); Cargar(); NuevoPlan(null, EventArgs.Empty); FormularioVisualHelper.MostrarExito(lblEstado, "Plan creado correctamente."); }
            catch (Exception ex) { FormularioVisualHelper.MostrarError(lblEstado, ex); }
        }

        private void Actualizar(object sender, EventArgs e)
        {
            try { if (idSeleccionado == 0) throw new InvalidOperationException("Selecciona un plan."); logica.Modificar(LeerPlan()); Cargar(); NuevoPlan(null, EventArgs.Empty); FormularioVisualHelper.MostrarExito(lblEstado, "Plan actualizado correctamente."); }
            catch (Exception ex) { FormularioVisualHelper.MostrarError(lblEstado, ex); }
        }

        private void DarDeBaja(object sender, EventArgs e)
        {
            try { if (idSeleccionado == 0) throw new InvalidOperationException("Selecciona un plan."); if (MessageBox.Show("Dar de baja al plan seleccionado?", "Confirmar baja", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return; logica.DarDeBaja(idSeleccionado); Cargar(); NuevoPlan(null, EventArgs.Empty); FormularioVisualHelper.MostrarExito(lblEstado, "Plan dado de baja."); }
            catch (Exception ex) { FormularioVisualHelper.MostrarError(lblEstado, ex); }
        }

        private void Reactivar(object sender, EventArgs e)
        {
            try { if (idSeleccionado == 0) throw new InvalidOperationException("Selecciona un plan."); logica.Reactivar(idSeleccionado); Cargar(); NuevoPlan(null, EventArgs.Empty); FormularioVisualHelper.MostrarExito(lblEstado, "Plan reactivado correctamente."); }
            catch (Exception ex) { FormularioVisualHelper.MostrarError(lblEstado, ex); }
        }
    }
}
