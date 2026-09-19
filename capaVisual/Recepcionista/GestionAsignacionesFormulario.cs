using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using exxen2._0.capaLogica;
using exxen2._0.capaVisual.Compartido;

namespace exxen2._0.capaVisual.Recepcionista
{
    /* Presenta membresías y su ficha de vinculación con entrenador mediante un flujo master/detail. */
    [DesignerCategory("Form")]
    public partial class GestionAsignacionesFormulario : Form
    {
        private readonly MembresiaEntrenadorLogica logica = new MembresiaEntrenadorLogica();
        private readonly UsuarioSistemaLogica usuarios = new UsuarioSistemaLogica();
        private List<MembresiaAsignacionItem> membresiasCargadas = new List<MembresiaAsignacionItem>();
        private int idSeleccionado;
        private int idAsignacionSeleccionada;
        private bool cargandoTabla;

        /* Inicializa los componentes existentes y las dependencias de la pantalla sin consultar la base de datos. */
        public GestionAsignacionesFormulario()
        {
            InitializeComponent();
        }

        /* Carga entrenadores activos mostrando nombre completo y DNI como identidad visible. */
        private void CargarEntrenadores()
        {
            entrenador.DataSource = usuarios.ListarPorRol("Entrenador")
                .Select(u => new OpcionEntrenador { IdEntrenador = u.IdUsuarioSistema, Texto = u.Apellido + ", " + u.Nombre + " - DNI " + u.DNI }).ToList();
            entrenador.DisplayMember = "Texto";
            entrenador.ValueMember = "IdEntrenador";
            entrenador.SelectedIndex = -1;
        }

        /* Consulta y filtra el listado de membresías que se pueden seleccionar para vinculación. */
        private void CargarListado()
        {
            try
            {
                membresiasCargadas = logica.ListarParaGestion();
                AplicarFiltro();
            }
            catch (Exception ex) { AyudaFormularioVisual.MostrarError(lblEstado, ex); }
        }

        /* Aplica búsqueda simple por socio/DNI y filtro de asignación sin modificar reglas de negocio. */
        private void AplicarFiltro()
        {
            var criterio = buscador.Text.Trim();
            var estado = Convert.ToString(filtroEstado.SelectedItem);
            var listado = membresiasCargadas.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(criterio))
                listado = listado.Where(m => Contiene(m.NombreSocio, criterio) || Contiene(m.DNI, criterio));
            if (estado == "Asignados") listado = listado.Where(m => m.Asignado);
            else if (estado == "Sin asignar") listado = listado.Where(m => !m.Asignado);

            cargandoTabla = true;
            tabla.Rows.Clear();
            foreach (var m in listado)
                tabla.Rows.Add(m.IdMembresia, m.NombreSocio, m.NombrePlan, m.NombreEntrenador, m.EstadoMembresia ? "Vigente" : "Inactiva");
            tabla.ClearSelection();
            cargandoTabla = false;
            PrepararFichaVacia();
            lblEstado.Text = tabla.Rows.Count + " membresía(s) encontrada(s)";
        }

        /* Compara texto visible sin distinguir mayúsculas y admite valores vacíos. */
        private static bool Contiene(string valor, string criterio)
        {
            return !string.IsNullOrEmpty(valor) && valor.IndexOf(criterio, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        /* Al seleccionar una membresía, carga su contexto y las acciones posibles sobre ella. */
        private void tabla_SelectionChanged(object origen, EventArgs e)
        {
            if (cargandoTabla || tabla.CurrentRow == null || !tabla.CurrentRow.Selected || tabla.CurrentRow.Cells[0].Value == null)
                return;
            idSeleccionado = Convert.ToInt32(tabla.CurrentRow.Cells[0].Value);
            var item = membresiasCargadas.FirstOrDefault(m => m.IdMembresia == idSeleccionado);
            if (item == null) return;
            idAsignacionSeleccionada = item.IdMembresiaEntrenador;
            txtSocio.Text = item.NombreSocio;
            txtDni.Text = item.DNI;
            txtPlan.Text = item.NombrePlan;
            txtVencimiento.Text = item.FechaVencimiento.ToString("dd/MM/yyyy");
            txtEstadoMembresia.Text = item.EstadoMembresia ? "Vigente" : "Inactiva";
            txtEntrenadorActual.Text = item.NombreEntrenador;
            if (item.Asignado && entrenador.Items.Count > 0) entrenador.SelectedValue = item.IdEntrenador; else entrenador.SelectedIndex = -1;
            asignar.Visible = !item.Asignado;
            cambiar.Visible = item.Asignado;
            darDeBaja.Visible = item.Asignado;
            asignar.Enabled = item.EstadoMembresia;
            cambiar.Enabled = item.EstadoMembresia;
            entrenador.Enabled = item.EstadoMembresia;
            darDeBaja.Enabled = item.EstadoMembresia;
            lblDetalleTitulo.Text = "Vinculación de la membresía";
        }

        /* Deja la ficha sin acciones hasta que el usuario elija una membresía inequívoca. */
        private void PrepararFichaVacia()
        {
            idSeleccionado = 0; idAsignacionSeleccionada = 0;
            txtSocio.Text = "Seleccioná una membresía"; txtDni.Text = "-"; txtPlan.Text = "-"; txtVencimiento.Text = "-"; txtEstadoMembresia.Text = "-"; txtEntrenadorActual.Text = "Sin asignar";
            entrenador.SelectedIndex = -1; entrenador.Enabled = false;
            asignar.Visible = false; cambiar.Visible = false; darDeBaja.Visible = false;
        }

        /* Al hacer clic en asignar, vincula el entrenador elegido a la membresía seleccionada. */
        private void asignar_Click(object origen, EventArgs e)
        {
            EjecutarAsignacion(false);
        }

        /* Al hacer clic en cambiar, finaliza la asignación activa y registra la nueva. */
        private void cambiar_Click(object origen, EventArgs e)
        {
            EjecutarAsignacion(true);
        }

        /* Ejecuta la operación contextual sin permitir una membresía distinta a la ficha visible. */
        private void EjecutarAsignacion(bool cambiarEntrenador)
        {
            try
            {
                if (idSeleccionado == 0) throw new InvalidOperationException("Seleccioná una membresía.");
                AyudaFormularioVisual.ValidarComboSeleccionado(entrenador, "un entrenador");
                if (cambiarEntrenador && MessageBox.Show("¿Reemplazar el entrenador actual de la membresía?", "Confirmar cambio", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;
                if (cambiarEntrenador) logica.CambiarEntrenador(idSeleccionado, Convert.ToInt32(entrenador.SelectedValue));
                else logica.AsignarEntrenador(idSeleccionado, Convert.ToInt32(entrenador.SelectedValue));
                CargarListado();
                AyudaFormularioVisual.MostrarExito(lblEstado, cambiarEntrenador ? "Entrenador cambiado." : "Entrenador asignado.", true);
            }
            catch (Exception ex) { AyudaFormularioVisual.MostrarError(lblEstado, ex, true); }
        }

        /* Al hacer clic en dar de baja, finaliza la vinculación activa y conserva su historial. */
        private void darDeBaja_Click(object origen, EventArgs e)
        {
            try
            {
                if (idAsignacionSeleccionada == 0) throw new InvalidOperationException("La membresía no tiene una asignación activa.");
                if (MessageBox.Show("¿Dar de baja la asignación seleccionada?", "Confirmar baja", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;
                logica.DarDeBajaAsignacion(idAsignacionSeleccionada); CargarListado();
                AyudaFormularioVisual.MostrarExito(lblEstado, "Asignación dada de baja.");
            }
            catch (Exception ex) { AyudaFormularioVisual.MostrarError(lblEstado, ex); }
        }

        /* Al cargar la pantalla en ejecución, prepara combos y datos fuera del diseñador. */
        private void GestionAsignacionesFormulario_Load(object origen, EventArgs e)
        {
            if (AyudaFormularioVisual.EnModoDisenio(this)) return;
            try { CargarEntrenadores(); CargarListado(); } catch (Exception ex) { AyudaFormularioVisual.MostrarError(lblEstado, ex); }
        }

        /* Al hacer clic en volver, cierra el módulo y devuelve el control al panel principal. */
        private void btnVolver_Click(object origen, EventArgs e) { Close(); }

        /* Al cambiar el texto de búsqueda, aplica el filtro sobre el listado cargado. */
        private void buscador_TextChanged(object origen, EventArgs e) { AplicarFiltro(); }

        /* Al cambiar el filtro de asignación, actualiza la lista visible. */
        private void filtroEstado_SelectedIndexChanged(object origen, EventArgs e)
        {
            if (!AyudaFormularioVisual.EnModoDisenio(this)) AplicarFiltro();
        }

        /* Al hacer clic en actualizar, vuelve a consultar membresías y entrenadores. */
        private void actualizar_Click(object origen, EventArgs e) { CargarEntrenadores(); CargarListado(); }

        /* Proyecta un entrenador a un texto visible sin usar apellido como identificador. */
        private sealed class OpcionEntrenador
        {
            public int IdEntrenador { get; set; }
            public string Texto { get; set; }
        }
    }
}
