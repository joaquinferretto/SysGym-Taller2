using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaLogica;
using exxen2._0.capaVisual.Compartido;

using exxen2._0.capaLogica.Utilidades;

namespace exxen2._0.capaVisual.Entrenador
{
    /* Presenta socios y su rutina vigente en una unica pantalla master/detail. */
    [System.ComponentModel.DesignerCategory("Form")]
    public partial class MisSociosFormulario : Form
    {
        private readonly RutinaLogica rutinas = new RutinaLogica();
        private readonly RutinaEjercicioLogica ejerciciosRutina = new RutinaEjercicioLogica();
        private readonly UsuarioSistema usuario;
        private readonly bool modoAdministrador;
        private List<SocioRutinaItem> sociosCargados = new List<SocioRutinaItem>();
        private SocioRutinaItem socioSeleccionado;
        private bool cargandoTabla;
        private readonly RutinaExportacionLogica exportacion = new RutinaExportacionLogica();
        private readonly ExportadorRutinaPdf exportadorPdf = new ExportadorRutinaPdf();

        public MisSociosFormulario() : this(new UsuarioSistema { Nombre = "Entrenador", Apellido = "de diseno" }) { }

        public MisSociosFormulario(UsuarioSistema usuario)
        {
            if (usuario == null) throw new ArgumentNullException("usuario");
            this.usuario = usuario;
            InitializeComponent();
        }

        public MisSociosFormulario(UsuarioSistema usuario, bool modoAdministrador) : this(usuario)
        {
            this.modoAdministrador = modoAdministrador;
            if (modoAdministrador)
            {
                Text = "SysGym | Socios y rutinas";
            }
        }

        private void Cargar(int? idSocioMantener = null)
        {
            try
            {
                sociosCargados = modoAdministrador
                    ? rutinas.ListarSociosParaAdministracion()
                    : rutinas.ListarSociosPorEntrenador(usuario.IdUsuarioSistema);
                CargarRutinasDisponibles();
                AplicarFiltro(idSocioMantener);
            }
            catch (Exception ex) { AyudaFormularioVisual.MostrarError(lblEstado, ex); }
        }

        private void CargarRutinasDisponibles()
        {
            rutinaDisponible.DataSource = null;
            rutinaDisponible.DisplayMember = "Nombre";
            rutinaDisponible.ValueMember = "IdRutina";
            rutinaDisponible.DataSource = rutinas.ListarActivas();
        }

        private void AplicarFiltro(int? idSocioMantener = null)
        {
            var criterio = buscador.Text.Trim();
            var filtro = Convert.ToString(filtroRutina.SelectedItem);
            var listado = sociosCargados.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(criterio))
                listado = listado.Where(s => Contiene(s.NombreSocio, criterio) || Contiene(s.DNI, criterio));
            if (filtro == "Con rutina") listado = listado.Where(s => s.TieneRutina);
            else if (filtro == "Sin rutina") listado = listado.Where(s => !s.TieneRutina);

            cargandoTabla = true;
            tabla.Rows.Clear();
            foreach (var socio in listado)
            {
                tabla.Rows.Add(
                    socio.IdSocio,
                    socio.IdMembresia,
                    socio.NombreSocio,
                    socio.DNI,
                    socio.NombrePlan,
                    string.IsNullOrWhiteSpace(socio.NombreRutina) ? "Sin rutina" : socio.NombreRutina,
                    socio.FechaVencimiento.ToString("dd/MM/yyyy"),
                    socio.TieneRutina ? "Con rutina" : "Sin rutina");
            }
            tabla.ClearSelection();
            cargandoTabla = false;
            MostrarFichaVacia();
            if (idSocioMantener.HasValue) SeleccionarFila(idSocioMantener.Value);
            lblEstado.Text = tabla.Rows.Count + " socio(s) encontrado(s)";
        }

        private void SeleccionarFila(int idSocio)
        {
            foreach (DataGridViewRow fila in tabla.Rows)
            {
                if (fila.Cells[0].Value != null && Convert.ToInt32(fila.Cells[0].Value) == idSocio)
                {
                    fila.Selected = true;
                    tabla.CurrentCell = fila.Cells[2];
                    break;
                }
            }
        }

        private static bool Contiene(string valor, string criterio)
        {
            return !string.IsNullOrEmpty(valor) && valor.IndexOf(criterio, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private void tabla_SelectionChanged(object origen, EventArgs e)
        {
            if (cargandoTabla || tabla.CurrentRow == null || !tabla.CurrentRow.Selected || tabla.CurrentRow.Cells[0].Value == null) return;
            var idSocio = Convert.ToInt32(tabla.CurrentRow.Cells[0].Value);
            socioSeleccionado = sociosCargados.FirstOrDefault(s => s.IdSocio == idSocio);
            if (socioSeleccionado == null) return;

            exportarPdf.Enabled = false;

            txtSocio.Text = socioSeleccionado.NombreSocio;
            txtDni.Text = socioSeleccionado.DNI;
            txtPlan.Text = socioSeleccionado.NombrePlan;
            txtEntrenador.Text = socioSeleccionado.NombreEntrenador;
            txtVencimiento.Text = socioSeleccionado.FechaVencimiento.ToString("dd/MM/yyyy");
            txtRutina.Text = socioSeleccionado.TieneRutina ? socioSeleccionado.NombreRutina : "Sin rutina asignada";
            CargarRutinaSemanal(socioSeleccionado.IdSocio);

            rutinaDisponible.Visible = true;
            asignarRutina.Visible = true;
            asignarRutina.Text = socioSeleccionado.TieneRutina ? "Cambiar rutina" : "Asignar rutina";
            verRutina.Visible = socioSeleccionado.TieneRutina;
            crearPersonalizada.Visible = !socioSeleccionado.TieneRutina;
            lblAccionInfo.Text = socioSeleccionado.TieneRutina
                ? "La rutina se muestra abajo. Podes cambiarla o editar sus ejercicios."
                : "Selecciona una rutina del catalogo o crea una personalizada.";
        }

        private void MostrarFichaVacia()
        {
            socioSeleccionado = null;
            txtSocio.Text = "Selecciona un socio";
            txtDni.Text = "-";
            txtPlan.Text = "-";
            txtEntrenador.Text = "-";
            txtVencimiento.Text = "-";
            txtRutina.Text = "-";
            lblAccionInfo.Text = "Selecciona un socio para ver su rutina y acciones.";
            tablaRutina.Rows.Clear();
            rutinaDisponible.Visible = false;
            asignarRutina.Visible = false;
            verRutina.Visible = false;
            crearPersonalizada.Visible = false;
            exportarPdf.Enabled = false;
        }

        private void CargarRutinaSemanal(int idSocio)
        {
            tablaRutina.Rows.Clear();
            exportarPdf.Enabled = false;
            var semana = ejerciciosRutina.ListarSemanaPorSocio(idSocio);
            foreach (var ejercicio in semana)
            {
                tablaRutina.Rows.Add(
                    ValidacionesGimnasio.NombreDia(ejercicio.DiaSemana),
                    ejercicio.Ejercicio == null ? "-" : ejercicio.Ejercicio.Nombre,
                    ejercicio.Series.HasValue ? ejercicio.Series.Value.ToString() : "-",
                    ejercicio.Repeticiones.HasValue ? ejercicio.Repeticiones.Value.ToString() : "-",
                    ejercicio.Peso.HasValue ? ejercicio.Peso.Value.ToString("0.##") : "-",
                    ejercicio.Descanso + " s");
            }
            exportarPdf.Enabled = socioSeleccionado != null && socioSeleccionado.TieneRutina && semana.Any();
        }

        private void exportarPdf_Click(object origen, EventArgs e)
        {
            try
            {
                if (socioSeleccionado == null) throw new InvalidOperationException("Selecciona un socio.");
                var documento = exportacion.Obtener(socioSeleccionado.IdSocio,
                    socioSeleccionado.IdMembresia, usuario.IdUsuarioSistema);
                using (var dialogo = new SaveFileDialog
                {
                    Title = "Exportar rutina a PDF", Filter = "PDF (*.pdf)|*.pdf",
                    DefaultExt = "pdf", AddExtension = true, OverwritePrompt = true,
                    FileName = exportadorPdf.NombreArchivoSugerido(documento)
                })
                {
                    if (dialogo.ShowDialog(this) != DialogResult.OK) return;
                    var avisos = exportadorPdf.Generar(documento, dialogo.FileName);
                    AyudaFormularioVisual.MostrarExito(lblEstado, "PDF guardado en " + dialogo.FileName, true);
                    MessageBox.Show(this, avisos.Count == 0 ? "La rutina se exportó correctamente." :
                        "La rutina se exportó, pero algunas imágenes no estaban disponibles:\n\n" + string.Join("\n", avisos),
                        "Exportar PDF", MessageBoxButtons.OK,
                        avisos.Count == 0 ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex) { AyudaFormularioVisual.MostrarError(lblEstado, ex, true); }
        }

        private void asignarRutina_Click(object origen, EventArgs e)
        {
            try
            {
                if (socioSeleccionado == null) throw new InvalidOperationException("Selecciona un socio.");
                var rutina = rutinaDisponible.SelectedItem as Rutina;
                if (rutina == null) throw new InvalidOperationException("Selecciona una rutina activa.");
                var idSocio = socioSeleccionado.IdSocio;
                rutinas.AsignarRutina(socioSeleccionado.IdMembresia, rutina.IdRutina);
                Cargar(idSocio);
                AyudaFormularioVisual.MostrarExito(lblEstado, "La rutina fue asignada a la membresia.", true);
            }
            catch (Exception ex) { AyudaFormularioVisual.MostrarError(lblEstado, ex, true); }
        }

        private void verRutina_Click(object origen, EventArgs e)
        {
            try
            {
                if (socioSeleccionado == null) throw new InvalidOperationException("Selecciona un socio.");
                var idSocio = socioSeleccionado.IdSocio;
                using (var formulario = new RutinasEntrenadorFormulario(usuario, idSocio, modoAdministrador, true)) formulario.ShowDialog(this);
                Cargar(idSocio);
            }
            catch (Exception ex) { AyudaFormularioVisual.MostrarError(lblEstado, ex); }
        }

        private void crearPersonalizada_Click(object origen, EventArgs e)
        {
            try
            {
                if (socioSeleccionado == null) throw new InvalidOperationException("Selecciona un socio.");
                var idSocio = socioSeleccionado.IdSocio;
                using (var formulario = new RutinasEntrenadorFormulario(usuario, idSocio, modoAdministrador)) formulario.ShowDialog(this);
                Cargar(idSocio);
            }
            catch (Exception ex) { AyudaFormularioVisual.MostrarError(lblEstado, ex); }
        }

        private void MisSociosFormulario_Load(object origen, EventArgs e)
        {
            if (!AyudaFormularioVisual.EnModoDisenio(this)) Cargar();
        }

        private void buscador_TextChanged(object origen, EventArgs e) { AplicarFiltro(); }
        private void filtroRutina_SelectedIndexChanged(object origen, EventArgs e)
        {
            if (!AyudaFormularioVisual.EnModoDisenio(this)) AplicarFiltro();
        }
        private void actualizar_Click(object origen, EventArgs e) { Cargar(socioSeleccionado == null ? (int?)null : socioSeleccionado.IdSocio); }
    }
}
