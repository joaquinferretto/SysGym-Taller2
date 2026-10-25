using System;
using exxen2._0.capaVisual.Compartido.Utilidades;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using exxen2._0.capaLogica;
using exxen2._0.capaVisual.Compartido;

using exxen2._0.capaDatos.Entidades;

namespace exxen2._0.capaVisual.Recepcionista
{
    /* Presenta membresías y su ficha de vinculación con entrenador mediante un flujo master/detail. */
    [DesignerCategory("Form")]
    public partial class GestionAsignacionesFormulario : Form
    {
        private readonly MembresiaEntrenadorLogica logica;
        private readonly UsuarioSistemaLogica usuarios = new UsuarioSistemaLogica();
        private List<MembresiaAsignacionItem> membresiasCargadas = new List<MembresiaAsignacionItem>();
        private int idSeleccionado;
        private int idAsignacionSeleccionada;
        private bool cargandoTabla;

        /* Inicializa los componentes existentes y las dependencias de la pantalla sin consultar la base de datos. */
        public GestionAsignacionesFormulario() : this(null) { }

        public GestionAsignacionesFormulario(UsuarioSistema usuarioActual)
        {
            logica = new MembresiaEntrenadorLogica(usuarioActual == null ? 0 : usuarioActual.IdUsuarioSistema);
            InitializeComponent();
            tabla.Validating += tabla_Validating;
            entrenador.Validating += entrenador_Validating;
            entrenador.SelectedIndexChanged += entrenador_SelectedIndexChanged;
            filtroEstado.SelectedIndex = 0;
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
                tabla.Rows.Add(m.IdMembresia, m.NombreSocio, m.NombrePlan, NombreEntrenadorVisible(m.NombreEntrenador), m.EstadoMembresia ? "Vigente" : "Inactiva");
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

        private static string NombreEntrenadorVisible(string nombreEntrenador)
        {
            if (string.IsNullOrWhiteSpace(nombreEntrenador)) return nombreEntrenador;
            var finNombre = nombreEntrenador.IndexOf(" - DNI ", StringComparison.OrdinalIgnoreCase);
            return finNombre < 0 ? nombreEntrenador : nombreEntrenador.Substring(0, finNombre);
        }

        /* Al seleccionar una membresía, carga su contexto y las acciones posibles sobre ella. */
        private void tabla_SelectionChanged(object origen, EventArgs e)
        {
            if (cargandoTabla) return;
            if (tabla.SelectedRows.Count != 1 || tabla.CurrentRow == null || tabla.CurrentRow.Cells[0].Value == null)
            {
                PrepararFichaVacia();
                return;
            }
            idSeleccionado = Convert.ToInt32(tabla.CurrentRow.Cells[0].Value);
            var item = membresiasCargadas.FirstOrDefault(m => m.IdMembresia == idSeleccionado);
            if (item == null) return;
            idAsignacionSeleccionada = item.IdMembresiaEntrenador;
            indicadorErrores.Clear();
            txtSocio.Text = item.NombreSocio;
            txtDni.Text = item.DNI;
            txtPlan.Text = item.NombrePlan;
            txtVencimiento.Text = item.CuotaHasta.HasValue ? item.CuotaHasta.Value.ToString("dd/MM/yyyy") : "Sin cuotas";
            txtEstadoMembresia.Text = item.EstadoMembresia ? "Vigente" : "Inactiva";
            txtEntrenadorActual.Text = item.NombreEntrenador;
            entrenador.SelectedIndex = -1;
            ActualizarAcciones();
            lblDetalleTitulo.Text = string.Empty;
        }

        /* Deja la ficha sin acciones hasta que el usuario elija una membresía inequívoca. */
        private void PrepararFichaVacia()
        {
            idSeleccionado = 0; idAsignacionSeleccionada = 0;
            indicadorErrores.Clear();
            txtSocio.Text = "Seleccioná una membresía"; txtDni.Text = "-"; txtPlan.Text = "-"; txtVencimiento.Text = "-"; txtEstadoMembresia.Text = "-"; txtEntrenadorActual.Text = "Sin asignar";
            entrenador.SelectedIndex = -1;
            ActualizarAcciones();
        }

        /* Mantiene las acciones visibles y habilitadas según la asignación seleccionada. */
        private void ActualizarAcciones()
        {
            var item = idSeleccionado == 0
                ? null
                : membresiasCargadas.FirstOrDefault(m => m.IdMembresia == idSeleccionado);
            var haySeleccion = item != null;
            var asignado = haySeleccion && item.Asignado;
            var membresiaActiva = haySeleccion && item.EstadoMembresia;

            asignar.Visible = haySeleccion && !asignado;
            cambiar.Visible = haySeleccion && asignado;
            darDeBaja.Visible = haySeleccion && asignado;
            asignar.Enabled = membresiaActiva;
            cambiar.Enabled = membresiaActiva;
            darDeBaja.Enabled = membresiaActiva;
            entrenador.Enabled = membresiaActiva;
            actualizarMenu.Enabled = true;
        }

        /* El clic derecho deja seleccionada la fila que originó el menú contextual. */
        private void tabla_CellMouseDown(object origen, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right || e.RowIndex < 0)
                return;

            var celda = e.ColumnIndex >= 0
                ? tabla.Rows[e.RowIndex].Cells[e.ColumnIndex]
                : tabla.Rows[e.RowIndex].Cells.Cast<DataGridViewCell>().First(c => c.Visible);
            tabla.CurrentCell = celda;
            tabla.ClearSelection();
            tabla.Rows[e.RowIndex].Selected = true;
            ActualizarAcciones();
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
                if (!ValidarFormularioAsignacion()) return;
                var idEntrenadorElegido = Convert.ToInt32(entrenador.SelectedValue);
                var membresiaActual = membresiasCargadas.FirstOrDefault(m => m.IdMembresia == idSeleccionado);
                if (cambiarEntrenador && membresiaActual != null && membresiaActual.IdEntrenador == idEntrenadorElegido)
                {
                    AyudaFormularioVisual.MostrarError(lblEstado, new InvalidOperationException("El socio ya tiene asignado ese entrenador"));
                    return;
                }
                if (cambiarEntrenador && MessageBox.Show("¿Reemplazar el entrenador actual de la membresía?", "Confirmar cambio", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;
                if (cambiarEntrenador) logica.CambiarEntrenador(idSeleccionado, idEntrenadorElegido);
                else logica.AsignarEntrenador(idSeleccionado, idEntrenadorElegido);
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

        /* Al salir del listado, informa si todavía no se eligió una membresía. */
        private void tabla_Validating(object origen, CancelEventArgs e)
        {
            ValidarMembresiaSeleccionada();
        }

        /* Al salir del combo, exige entrenador solo cuando la acción está habilitada. */
        private void entrenador_Validating(object origen, CancelEventArgs e)
        {
            AyudaFormularioVisual.ValidarCombo(indicadorErrores, entrenador, "Seleccioná un entrenador.");
        }

        /* Al corregir la selección, retira el aviso anterior. */
        private void entrenador_SelectedIndexChanged(object origen, EventArgs e)
        {
            indicadorErrores.SetError(entrenador, string.Empty);
        }

        /* Valida las dos selecciones antes de asignar o cambiar entrenador. */
        private bool ValidarFormularioAsignacion()
        {
            indicadorErrores.Clear();
            var valido = ValidarMembresiaSeleccionada();
            valido = AyudaFormularioVisual.ValidarCombo(indicadorErrores, entrenador, "Seleccioná un entrenador.") & valido;
            if (!valido)
                AyudaFormularioVisual.EnfocarPrimerError(indicadorErrores, tabla, entrenador);
            return valido;
        }

        /* Asocia al listado el requisito de seleccionar una membresía editable. */
        private bool ValidarMembresiaSeleccionada()
        {
            return AyudaFormularioVisual.ValidarConError(indicadorErrores, tabla, delegate
            {
                if (idSeleccionado == 0)
                    throw new InvalidOperationException("Seleccioná una membresía.");
            });
        }

        /* Proyecta un entrenador a un texto visible sin usar apellido como identificador. */
        private sealed class OpcionEntrenador
        {
            public int IdEntrenador { get; set; }
            public string Texto { get; set; }
        }
    }
}
