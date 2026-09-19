using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaLogica;
using exxen2._0.capaVisual.Compartido;

namespace exxen2._0.capaVisual.Administrador
{
    /* Presenta planes y atiende sus acciones mediante eventos de Windows Forms. */
    [DesignerCategory("Form")]
    public partial class GestionPlanesFormulario : Form
    {
        private readonly PlanLogica logica = new PlanLogica();
        private List<Plan> planesCargados = new List<Plan>();
        private int idSeleccionado;
        private bool cargandoTabla;
        private bool estadoSeleccionado = true;

        public GestionPlanesFormulario()
        {
            InitializeComponent();
        }

        private void Inicializar()
        {
            try
            {
                Cargar();
                nuevo_Click(null, EventArgs.Empty);
            }
            catch (Exception ex) { AyudaFormularioVisual.MostrarError(lblEstado, ex); }
        }

        private void Cargar()
        {
            try
            {
                planesCargados = logica.ListarParaGestion();
                AplicarFiltro();
            }
            catch (Exception ex) { AyudaFormularioVisual.MostrarError(lblEstado, ex); }
        }

        private void AplicarFiltro()
        {
            var criterio = buscador.Text.Trim();
            var estadoElegido = Convert.ToString(filtroEstado.SelectedItem);
            var filtrados = planesCargados.AsEnumerable();
            if (estadoElegido == "Activos") filtrados = filtrados.Where(p => p.Estado);
            else if (estadoElegido == "Inactivos") filtrados = filtrados.Where(p => !p.Estado);
            if (!string.IsNullOrWhiteSpace(criterio)) filtrados = filtrados.Where(p => Contiene(p.Nombre, criterio) || Contiene(p.Descripcion, criterio));

            cargandoTabla = true;
            tabla.Rows.Clear();
            foreach (var plan in filtrados)
                tabla.Rows.Add(plan.IdPlan, plan.Nombre, plan.Precio.ToString("C"), plan.Estado ? "Activo" : "Inactivo");
            tabla.ClearSelection();
            cargandoTabla = false;
            lblEstado.Text = tabla.Rows.Count + " plan(es) encontrado(s)";
        }

        private static bool Contiene(string valor, string criterio)
        {
            return !string.IsNullOrEmpty(valor) && valor.IndexOf(criterio, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private void tabla_SelectionChanged(object origen, EventArgs e)
        {
            if (cargandoTabla || tabla.CurrentRow == null || !tabla.CurrentRow.Selected) return;
            try
            {
                idSeleccionado = Convert.ToInt32(tabla.CurrentRow.Cells[0].Value);
                var plan = logica.ObtenerPorId(idSeleccionado);
                if (plan == null) return;
                estadoSeleccionado = plan.Estado;
                nombre.Text = plan.Nombre;
                descripcion.Text = plan.Descripcion ?? string.Empty;
                precio.Text = plan.Precio.ToString("0.00");
                EstablecerModo(false, plan.Estado);
            }
            catch (Exception ex) { AyudaFormularioVisual.MostrarError(lblEstado, ex); }
        }

        private void nuevo_Click(object origen, EventArgs e)
        {
            idSeleccionado = 0;
            estadoSeleccionado = true;
            nombre.Clear();
            descripcion.Clear();
            precio.Clear();
            tabla.ClearSelection();
            EstablecerModo(true, true);
            nombre.Focus();
        }

        private void EstablecerModo(bool nuevoRegistro, bool activo)
        {
            lblFormulario.Text = nuevoRegistro ? "Nuevo plan" : "Editar plan";
            guardar.Enabled = nuevoRegistro;
            actualizar.Enabled = !nuevoRegistro;
            darDeBaja.Enabled = !nuevoRegistro && activo;
            reactivar.Enabled = !nuevoRegistro && !activo;
        }

        private Plan LeerPlan()
        {
            return new Plan
            {
                IdPlan = idSeleccionado,
                Nombre = nombre.Text.Trim(),
                Descripcion = descripcion.Text.Trim(),
                Precio = AyudaFormularioVisual.DecimalPositivo(precio, "precio"),
                Estado = estadoSeleccionado
            };
        }

        private void guardar_Click(object origen, EventArgs e)
        {
            try
            {
                if (idSeleccionado != 0) return;
                logica.Crear(LeerPlan());
                Cargar();
                nuevo_Click(null, EventArgs.Empty);
                AyudaFormularioVisual.MostrarExito(lblEstado, "Plan creado correctamente.", true);
            }
            catch (Exception ex) { AyudaFormularioVisual.MostrarError(lblEstado, ex, true); }
        }

        private void actualizar_Click(object origen, EventArgs e)
        {
            try
            {
                if (idSeleccionado == 0) throw new InvalidOperationException("Selecciona un plan.");
                logica.Modificar(LeerPlan());
                Cargar();
                nuevo_Click(null, EventArgs.Empty);
                AyudaFormularioVisual.MostrarExito(lblEstado, "Plan actualizado correctamente.");
            }
            catch (Exception ex) { AyudaFormularioVisual.MostrarError(lblEstado, ex); }
        }

        private void darDeBaja_Click(object origen, EventArgs e)
        {
            try
            {
                if (idSeleccionado == 0) throw new InvalidOperationException("Selecciona un plan.");
                if (MessageBox.Show("Dar de baja al plan seleccionado?", "Confirmar baja", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
                logica.DarDeBaja(idSeleccionado);
                Cargar();
                nuevo_Click(null, EventArgs.Empty);
                AyudaFormularioVisual.MostrarExito(lblEstado, "Plan dado de baja.");
            }
            catch (Exception ex) { AyudaFormularioVisual.MostrarError(lblEstado, ex); }
        }

        private void reactivar_Click(object origen, EventArgs e)
        {
            try
            {
                if (idSeleccionado == 0) throw new InvalidOperationException("Selecciona un plan.");
                logica.Reactivar(idSeleccionado);
                Cargar();
                nuevo_Click(null, EventArgs.Empty);
                AyudaFormularioVisual.MostrarExito(lblEstado, "Plan reactivado correctamente.");
            }
            catch (Exception ex) { AyudaFormularioVisual.MostrarError(lblEstado, ex); }
        }

        private void GestionPlanesFormulario_Load(object origen, EventArgs e)
        {
            if (!AyudaFormularioVisual.EnModoDisenio(this)) Inicializar();
        }

        private void buscador_TextChanged(object origen, EventArgs e) { AplicarFiltro(); }
        private void filtroEstado_SelectedIndexChanged(object origen, EventArgs e)
        {
            if (!AyudaFormularioVisual.EnModoDisenio(this)) AplicarFiltro();
        }
        private void precio_KeyPress(object origen, KeyPressEventArgs e) { AyudaFormularioVisual.ValidarEntradaDecimal(precio, e); }
    }
}
