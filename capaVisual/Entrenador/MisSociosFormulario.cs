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
            rutinaDisponible.Validating += rutinaDisponible_Validating;
            rutinaDisponible.SelectedIndexChanged += rutinaDisponible_SelectedIndexChanged;
        }

        public MisSociosFormulario(UsuarioSistema usuario, bool modoAdministrador) : this(usuario)
        {
            this.modoAdministrador = modoAdministrador;
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
                    socio.CuotaHasta.HasValue ? socio.CuotaHasta.Value.ToString("dd/MM/yyyy") : "-",
                    socio.TieneRutina ? "Con rutina" : "Sin rutina");
            }
            tabla.ClearSelection();
            cargandoTabla = false;
            MostrarFichaVacia();
            if (idSocioMantener.HasValue) SeleccionarFila(idSocioMantener.Value);
            lblEstado.Text = tabla.Rows.Count + (modoAdministrador ? " socio(s) encontrado(s)" : " alumno(s) encontrado(s)");
        }

        private void SeleccionarFila(int idSocio)
        {
            foreach (DataGridViewRow fila in tabla.Rows)
            {
                if (fila.Cells[0].Value != null && Convert.ToInt32(fila.Cells[0].Value) == idSocio)
                {
                    // Al seleccionar por código, SelectionChanged llega antes de actualizar CurrentRow: se carga la ficha explícitamente.
                    tabla.CurrentCell = fila.Cells[2];
                    fila.Selected = true;
                    tabla_SelectionChanged(tabla, EventArgs.Empty);
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

            indicadorErrores.Clear();
            exportarPdf.Enabled = false;

            txtSocio.Text = socioSeleccionado.NombreSocio;
            txtDni.Text = socioSeleccionado.DNI;
            txtPlan.Text = socioSeleccionado.NombrePlan;
            txtEntrenador.Text = socioSeleccionado.NombreEntrenador;
            txtVencimiento.Text = socioSeleccionado.CuotaHasta.HasValue ? socioSeleccionado.CuotaHasta.Value.ToString("dd/MM/yyyy") : "Sin cuotas";
            txtRutina.Text = socioSeleccionado.TieneRutina ? socioSeleccionado.NombreRutina : "Sin rutina asignada";
            CargarRutinaSemanal(socioSeleccionado.IdSocio);

            asignarRutina.Text = socioSeleccionado.TieneRutina ? "Cambiar rutina" : "Asignar rutina";
            ActualizarAcciones();
            lblAccionInfo.Text = socioSeleccionado.TieneRutina
                ? "La rutina se muestra abajo. Podes cambiarla o editar sus ejercicios."
                : "Selecciona una rutina del catalogo o crea una personalizada.";
        }

        private void MostrarFichaVacia()
        {
            socioSeleccionado = null;
            indicadorErrores.Clear();
            txtSocio.Text = "Selecciona un socio";
            txtDni.Text = "-";
            txtPlan.Text = "-";
            txtEntrenador.Text = "-";
            txtVencimiento.Text = "-";
            txtRutina.Text = "-";
            lblAccionInfo.Text = "Selecciona un socio para ver su rutina y acciones.";
            tablaRutina.Rows.Clear();
            asignarRutina.Text = "Asignar rutina";
            exportarPdf.Enabled = false;
            ActualizarAcciones();
        }

        /* Habilita cada acción según el socio seleccionado: asignar usa el catálogo, crear solo sin rutina y ver solo con rutina. */
        private void ActualizarAcciones()
        {
            var haySocio = socioSeleccionado != null;
            rutinaDisponible.Enabled = haySocio;
            asignarRutina.Enabled = haySocio && rutinaDisponible.SelectedItem is Rutina;
            crearPersonalizada.Enabled = haySocio && !socioSeleccionado.TieneRutina;
            verRutina.Enabled = haySocio && socioSeleccionado.TieneRutina;
        }

        /* Exige un socio seleccionado e informa mediante el ErrorProvider de la grilla. */
        private bool ValidarSocioSeleccionado()
        {
            indicadorErrores.Clear();
            var valido = AyudaFormularioVisual.ValidarConError(indicadorErrores, tabla, delegate
            {
                if (socioSeleccionado == null)
                    throw new InvalidOperationException("Seleccioná un socio.");
            });
            if (!valido)
                AyudaFormularioVisual.EnfocarPrimerError(indicadorErrores, tabla);
            return valido;
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
                if (!ValidarAsignacionRutina()) return;
                var rutina = rutinaDisponible.SelectedItem as Rutina;
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
                if (!ValidarSocioSeleccionado()) return;
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
                if (!ValidarSocioSeleccionado()) return;
                var idSocio = socioSeleccionado.IdSocio;
                using (var formulario = new RutinasEntrenadorFormulario(usuario, idSocio, modoAdministrador)) formulario.ShowDialog(this);
                Cargar(idSocio);
            }
            catch (Exception ex) { AyudaFormularioVisual.MostrarError(lblEstado, ex); }
        }

        private void MisSociosFormulario_Load(object origen, EventArgs e)
        {
            if (AyudaFormularioVisual.EnModoDisenio(this)) return;
            if (!modoAdministrador)
            {
                // Para el entrenador, los socios asignados son sus alumnos.
                lblListadoTitulo.Text = "Mis alumnos";
                lblDetalleTitulo.Text = "Alumno seleccionado";
                lblSocio.Text = "Alumno:";
            }
            Cargar();
        }

        /* Vuelve a consultar el listado conservando el alumno seleccionado; se usa al regresar al inicio del entrenador. */
        internal void Recargar()
        {
            Cargar(socioSeleccionado == null ? (int?)null : socioSeleccionado.IdSocio);
        }

        private void buscador_TextChanged(object origen, EventArgs e) { AplicarFiltro(); }
        private void filtroRutina_SelectedIndexChanged(object origen, EventArgs e)
        {
            if (!AyudaFormularioVisual.EnModoDisenio(this)) AplicarFiltro();
        }
        private void actualizar_Click(object origen, EventArgs e) { Cargar(socioSeleccionado == null ? (int?)null : socioSeleccionado.IdSocio); }

        /* Al salir del catálogo, exige una rutina cuando la acción de asignar está disponible. */
        private void rutinaDisponible_Validating(object origen, System.ComponentModel.CancelEventArgs e)
        {
            AyudaFormularioVisual.ValidarCombo(indicadorErrores, rutinaDisponible, "Seleccioná una rutina activa.");
        }

        /* Al corregir la selección de rutina, retira el aviso anterior. */
        private void rutinaDisponible_SelectedIndexChanged(object origen, EventArgs e)
        {
            indicadorErrores.SetError(rutinaDisponible, string.Empty);
            ActualizarAcciones();
        }

        /* Valida el socio visible y la rutina elegida antes de asignar. */
        private bool ValidarAsignacionRutina()
        {
            indicadorErrores.Clear();
            var valido = AyudaFormularioVisual.ValidarConError(indicadorErrores, tabla, delegate
            {
                if (socioSeleccionado == null)
                    throw new InvalidOperationException("Seleccioná un socio.");
            });
            valido = AyudaFormularioVisual.ValidarCombo(indicadorErrores, rutinaDisponible, "Seleccioná una rutina activa.") & valido;
            if (!valido)
                AyudaFormularioVisual.EnfocarPrimerError(indicadorErrores, tabla, rutinaDisponible);
            return valido;
        }
    }
}
