using exxen2._0.capaLogica.Navegacion;
using exxen2._0.capaVisual.Compartido.Utilidades;
using System;
using exxen2._0.capaVisual.Compartido.Controles;
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
    /* Presenta membresias y atiende sus acciones mediante eventos de Windows Forms. */
    [DesignerCategory("Form")]
    public partial class GestionMembresiasFormulario : Form
    {
        public event EventHandler<AccionEstadoSocioEventArgs> FichaSolicitada;
        private void btnFicha_Click(object sender, EventArgs e)
        {
            var id = membresiaSeleccionada == null ? 0 : membresiaSeleccionada.IdSocio;
            var evento = FichaSolicitada;
            if (id > 0 && evento != null) evento(this, new AccionEstadoSocioEventArgs(id, AccionEstadoSocio.VerFicha));
        }
        private readonly int idSocioInicial;
        public GestionMembresiasFormulario(UsuarioSistema usuario, Color colorPrimario, int idSocioInicial) : this(usuario, colorPrimario) { this.idSocioInicial = idSocioInicial; }
        private readonly UsuarioSistema usuario;
        private readonly MembresiaLogica logica;
        private readonly RutinaLogica rutinas;
        private readonly PlanLogica planes = new PlanLogica();
        private readonly CuotaMembresiaLogica cuotas = new CuotaMembresiaLogica();
        private List<Membresia> membresiasCargadas = new List<Membresia>();
        private Membresia membresiaSeleccionada;
        private int idSeleccionado;
        private bool cargandoTabla;
        /* Inicializa los componentes existentes y las dependencias de la pantalla sin consultar la base de datos. */
        public GestionMembresiasFormulario() : this(new UsuarioSistema { Nombre = "Recepcionista", Apellido = "de diseno" })
        {
        }

        /* Inicializa los componentes existentes y las dependencias de la pantalla sin consultar la base de datos. */
        public GestionMembresiasFormulario(UsuarioSistema usuario) : this(usuario, Color.FromArgb(5, 150, 105))
        {
        }

        /* Inicializa los componentes existentes y las dependencias de la pantalla sin consultar la base de datos. */
        public GestionMembresiasFormulario(UsuarioSistema usuario, Color colorPrimario)
        {
            if (usuario == null)
                throw new ArgumentNullException("usuario");
            this.usuario = usuario;
            logica = new MembresiaLogica(usuario.IdUsuarioSistema);
            rutinas = new RutinaLogica(usuario.IdUsuarioSistema);
            InitializeComponent();
            ConfigurarValidaciones();
        }

        /* Conecta los avisos visuales de selección y rango de fechas. */
        private void ConfigurarValidaciones()
        {
            socio.Validating += socio_Validating;
            plan.Validating += plan_Validating;
            socio.SelectedIndexChanged += campoValidado_Cambiado;
            plan.SelectedIndexChanged += campoValidado_Cambiado;
            inicio.ValueChanged += campoValidado_Cambiado;
            socio.SelectedIndexChanged += socio_SelectedIndexChanged;
        }

        /* Carga las opciones y los registros necesarios y prepara el formulario para una nueva operación. */
        private void Inicializar()
        {
            try
            {
                CargarCombos();
                Cargar();
                nuevo_Click(null, EventArgs.Empty);
                if (idSocioInicial > 0)
                {
                    var inicial = membresiasCargadas.Where(m => m.IdSocio == idSocioInicial).OrderByDescending(m => m.Estado).ThenByDescending(m => m.FechaInicio).FirstOrDefault();
                    if (inicial != null)
                    {
                        foreach (DataGridViewRow fila in tabla.Rows)
                            if (Convert.ToInt32(fila.Cells[0].Value) == inicial.IdMembresia) { tabla.CurrentCell = fila.Cells[1]; fila.Selected = true; break; }
                    }
                    else if (socio.Items.Count > 0)
                        socio.SelectedValue = idSocioInicial;
                }
            }
            catch (Exception ex)
            {
                AyudaFormularioVisual.MostrarError(lblEstado, ex);
            }
        }

        /* Carga los planes activos para el alta de una membresía. */
        private void CargarCombos()
        {
            plan.DataSource = planes.ListarActivos();
            plan.DisplayMember = "Nombre";
            plan.ValueMember = "IdPlan";
        }

        /* Solo ofrece para nuevas altas socios sin ninguna membresía histórica. */
        private void CargarSociosDisponiblesParaAlta()
        {
            socio.DataSource = logica.ListarSociosDisponiblesParaAlta()
                .Select(s => new OpcionSocioMembresia { IdSocio = s.IdSocio, Texto = s.Apellido + ", " + s.Nombre })
                .ToList();
            socio.DisplayMember = "Texto";
            socio.ValueMember = "IdSocio";
        }

        /* Mantiene seleccionable en modo consulta al socio de la membresía histórica. */
        private void CargarSocioDeMembresia(Membresia membresia)
        {
            var opciones = logica.ListarSociosDisponiblesParaAlta()
                .Select(s => new OpcionSocioMembresia { IdSocio = s.IdSocio, Texto = s.Apellido + ", " + s.Nombre })
                .ToList();
            if (membresia.Socio != null && !opciones.Any(o => o.IdSocio == membresia.IdSocio))
                opciones.Add(new OpcionSocioMembresia { IdSocio = membresia.IdSocio, Texto = membresia.Socio.Apellido + ", " + membresia.Socio.Nombre + " - DNI " + membresia.Socio.DNI });
            socio.DataSource = opciones;
            socio.DisplayMember = "Texto";
            socio.ValueMember = "IdSocio";
        }

        /* Consulta los registros del módulo y actualiza la grilla, informando los errores de carga. */
        private void Cargar()
        {
            try
            {
                membresiasCargadas = logica.ListarParaGestion();
                AplicarFiltro();
            }
            catch (Exception ex)
            {
                AyudaFormularioVisual.MostrarError(lblEstado, ex);
            }
        }

        /* Filtra los registros cargados por el criterio ingresado y actualiza la grilla y su contador. */
        private void AplicarFiltro()
        {
            var criterio = buscador.Text.Trim();
            var filtradas = membresiasCargadas.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(criterio))
                filtradas = filtradas.Where(m => Contiene(NombreSocio(m), criterio) || Contiene(m.Socio == null ? string.Empty : m.Socio.DNI, criterio) || Contiene(NombrePlan(m), criterio));
            cargandoTabla = true;
            tabla.Rows.Clear();
            foreach (var membresiaActual in filtradas)
                tabla.Rows.Add(membresiaActual.IdMembresia, NombreSocio(membresiaActual), membresiaActual.Socio == null ? "-" : membresiaActual.Socio.DNI, NombrePlan(membresiaActual), membresiaActual.FechaInicio.ToString("dd/MM/yyyy"), membresiaActual.Estado ? "Activa" : "Inactiva");
            tabla.ClearSelection();
            cargandoTabla = false;
            lblEstado.Text = tabla.Rows.Count + (tabla.Rows.Count == 1 ? " membresía encontrada" : " membresías encontradas");
            if (idSeleccionado != 0) nuevo_Click(null, EventArgs.Empty);
        }

        /* Al cambiar la fila seleccionada, toma su identificador y actualiza los datos o acciones del registro. */
        private void tabla_SelectionChanged(object origen, EventArgs e)
        {
            if (cargandoTabla)
                return;
            if (tabla.SelectedRows.Count != 1)
            {
                nuevo_Click(null, EventArgs.Empty);
                return;
            }
            idSeleccionado = Convert.ToInt32(tabla.SelectedRows[0].Cells[0].Value);
            membresiaSeleccionada = membresiasCargadas.FirstOrDefault(m => m.IdMembresia == idSeleccionado);
            if (membresiaSeleccionada == null)
                return;
            CargarSocioDeMembresia(membresiaSeleccionada);
            socio.SelectedValue = membresiaSeleccionada.IdSocio;
            plan.SelectedValue = membresiaSeleccionada.IdPlan;
            inicio.Value = membresiaSeleccionada.FechaInicio;
            estadoMembresia.Text = membresiaSeleccionada.Estado ? "Activa" : "Inactiva";
            objetivo.SelectedItem = membresiaSeleccionada.Rutina == null || string.IsNullOrEmpty(membresiaSeleccionada.Rutina.Objetivo)
                ? "Libre" : membresiaSeleccionada.Rutina.Objetivo;
            if (membresiaSeleccionada.Rutina != null && !string.IsNullOrEmpty(membresiaSeleccionada.Rutina.Nivel))
                nivel.SelectedItem = membresiaSeleccionada.Rutina.Nivel;
            ActualizarSugerencia();
            inicio.Enabled = false;
            socio.Enabled = false;
            plan.Enabled = membresiaSeleccionada.Estado;
            LimpiarErroresDelDetalle();
            EstablecerModo(false);
            lblFormulario.Text = "Membresía de " + NombreSocio(membresiaSeleccionada);
        }

        /* Al hacer clic en nuevo, limpia la selección y prepara el registro de nuevos datos. */
        private void nuevo_Click(object origen, EventArgs e)
        {
            idSeleccionado = 0;
            membresiaSeleccionada = null;
            tabla.ClearSelection();
            CargarSociosDisponiblesParaAlta();
            socio.Enabled = true;
            plan.Enabled = true;
            if (socio.Items.Count > 0)
                socio.SelectedIndex = 0;
            if (plan.Items.Count > 0)
                plan.SelectedIndex = 0;
            inicio.Value = DateTime.Today;
            inicio.Enabled = true;
            estadoMembresia.Text = "Activa";
            objetivo.SelectedIndex = 0;
            nivel.SelectedIndex = Convert.ToString(objetivo.SelectedItem) == "Libre" ? -1 : 0;
            ActualizarSugerencia();
            LimpiarErroresDelDetalle();
            EstablecerModo(true);
        }

        /* Habilita las acciones disponibles según la selección y el estado del registro. */
        private void EstablecerModo(bool nueva)
        {
            btnFicha.Enabled = !nueva && membresiaSeleccionada != null && FichaSolicitada != null;
            var puedeCrear = socio.Items.Count > 0 && plan.Items.Count > 0;
            var puedeCambiarPlan = !nueva && membresiaSeleccionada != null && membresiaSeleccionada.Estado;
            if (nueva)
                lblFormulario.Text = plan.Items.Count == 0 ? "Primero crea un plan" : (socio.Items.Count == 0 ? "No hay socios disponibles para una nueva membresía" : "Nueva membresía");
            crear.Text = nueva ? "Guardar" : "Cambiar plan";
            crear.BackColor = nueva ? Color.FromArgb(46, 125, 50) : Color.FromArgb(91, 75, 138);
            crear.FlatAppearance.MouseDownBackColor = nueva ? Color.FromArgb(27, 75, 31) : Color.FromArgb(58, 48, 89);
            crear.FlatAppearance.MouseOverBackColor = nueva ? Color.FromArgb(37, 101, 41) : Color.FromArgb(74, 61, 112);
            crear.Visible = nueva || puedeCambiarPlan;
            crear.Enabled = nueva ? puedeCrear : puedeCambiarPlan;
            nuevo.Visible = nueva;
            nuevaExistente.Visible = !nueva;
            plan.Enabled = nueva || puedeCambiarPlan;
            actualizar.Enabled = puedeCambiarPlan;
            objetivo.Enabled = nueva || puedeCambiarPlan;
            nivel.Enabled = (nueva || puedeCambiarPlan) && Convert.ToString(objetivo.SelectedItem) != "Libre";
            habilitar.Enabled = !nueva && membresiaSeleccionada != null && !membresiaSeleccionada.Estado;
            deshabilitar.Enabled = !nueva && membresiaSeleccionada != null && membresiaSeleccionada.Estado;
            generarCuota.Enabled = false;
            if (!nueva && membresiaSeleccionada != null)
            {
                try
                {
                    var disponibilidad = cuotas.ConsultarDisponibilidadGeneracion(membresiaSeleccionada.IdMembresia);
                    generarCuota.Enabled = disponibilidad.PuedeGenerar;
                    if (!disponibilidad.PuedeGenerar) lblEstado.Text = disponibilidad.Motivo;
                }
                catch (Exception ex)
                {
                    AyudaFormularioVisual.MostrarError(lblEstado, ex);
                }
            }
            ActualizarAcciones();
        }

        private void ActualizarAcciones()
        {
            actualizar.Visible = membresiaSeleccionada != null && membresiaSeleccionada.Estado;
            actualizar.Text = membresiaSeleccionada != null && membresiaSeleccionada.IdRutina.HasValue
                ? "Cambiar rutina" : "Asignar rutina";
            habilitar.Visible = membresiaSeleccionada != null && idSeleccionado > 0 && !membresiaSeleccionada.Estado;
            deshabilitar.Visible = membresiaSeleccionada != null && idSeleccionado > 0 && membresiaSeleccionada.Estado;
            btnFicha.Visible = false;
            generarCuota.Visible = membresiaSeleccionada != null && membresiaSeleccionada.Estado;
            fichaMenu.Enabled = membresiaSeleccionada != null && FichaSolicitada != null;
            habilitarMenu.Visible = membresiaSeleccionada != null && !membresiaSeleccionada.Estado;
            deshabilitarMenu.Visible = membresiaSeleccionada != null && membresiaSeleccionada.Estado;
            habilitarMenu.Enabled = membresiaSeleccionada != null && !membresiaSeleccionada.Estado;
            deshabilitarMenu.Enabled = membresiaSeleccionada != null && membresiaSeleccionada.Estado;
        }

        private void tabla_CellMouseDown(object origen, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right || e.RowIndex < 0) return;
            tabla.ClearSelection();
            tabla.CurrentCell = tabla.Rows[e.RowIndex].Cells[1];
            tabla.Rows[e.RowIndex].Selected = true;
            tabla_SelectionChanged(tabla, EventArgs.Empty);
        }

        private void tabla_CellDoubleClick(object origen, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            tabla.ClearSelection();
            tabla.CurrentCell = tabla.Rows[e.RowIndex].Cells[1];
            tabla.Rows[e.RowIndex].Selected = true;
            tabla_SelectionChanged(tabla, EventArgs.Empty);
            btnFicha_Click(origen, EventArgs.Empty);
        }

        /* Guardar crea la membresía o modifica la seleccionada según el estado del formulario. */
        private void guardar_Click(object origen, EventArgs e)
        {
            if (membresiaSeleccionada != null)
            {
                CambiarPlanSeleccionada();
                return;
            }

            CrearMembresia();
        }

        private void CrearMembresia()
        {
            try
            {
                if (!ValidarFormulario(true))
                    return;
                var idRutina = RutinaModeloSeleccionada();
                logica.Crear(new Membresia { IdSocio = Convert.ToInt32(socio.SelectedValue), IdPlan = Convert.ToInt32(plan.SelectedValue), IdUsuarioSistema = usuario.IdUsuarioSistema, FechaInicio = inicio.Value.Date, IdRutina = idRutina });
                Cargar();
                nuevo_Click(null, EventArgs.Empty);
                AyudaFormularioVisual.MostrarExito(lblEstado, idRutina.HasValue
                    ? "Membresía creada con rutina modelo y primera cuota."
                    : "Membresía creada y primera cuota generada. El entrenador asignará una rutina personalizada.", true);
            }
            catch (Exception ex)
            {
                AyudaFormularioVisual.MostrarError(lblEstado, ex, true);
            }

        }

        private void CambiarPlanSeleccionada()
        {
            try
            {
                if (membresiaSeleccionada == null)
                    throw new InvalidOperationException("Selecciona una membresía.");
                if (!membresiaSeleccionada.Estado)
                    throw new InvalidOperationException("No se puede cambiar el plan de una membresía inactiva.");
                if (!AyudaFormularioVisual.ValidarCombo(indicadorErrores, plan, "Seleccioná un plan."))
                    return;
                var idPlan = Convert.ToInt32(plan.SelectedValue);
                logica.CambiarPlan(membresiaSeleccionada.IdMembresia, idPlan);
                Cargar();
                nuevo_Click(null, EventArgs.Empty);
                AyudaFormularioVisual.MostrarExito(lblEstado, "Plan de la membresía cambiado.");
            }
            catch (Exception ex)
            {
                AyudaFormularioVisual.MostrarError(lblEstado, ex);
            }

        }

        private int? RutinaModeloSeleccionada()
        {
            var elegido = Convert.ToString(objetivo.SelectedItem);
            if (elegido == "Libre") return null;
            var nivelElegido = Convert.ToString(nivel.SelectedItem);
            if (string.IsNullOrEmpty(elegido) || string.IsNullOrEmpty(nivelElegido))
                throw new InvalidOperationException("Seleccioná objetivo y nivel.");
            var modelo = rutinas.ListarModelos(elegido, nivelElegido).SingleOrDefault();
            if (modelo == null)
                throw new InvalidOperationException("No existe una rutina modelo activa para esa combinación.");
            return modelo.IdRutina;
        }

        private void asignarRutina_Click(object origen, EventArgs e)
        {
            try
            {
                if (membresiaSeleccionada == null || !membresiaSeleccionada.Estado)
                    throw new InvalidOperationException("Seleccioná una membresía activa.");
                var idRutina = RutinaModeloSeleccionada();
                rutinas.AsignarRutina(membresiaSeleccionada.IdMembresia, idRutina);
                Cargar();
                nuevo_Click(null, EventArgs.Empty);
                AyudaFormularioVisual.MostrarExito(lblEstado, idRutina.HasValue
                    ? "Rutina modelo asignada a la membresía."
                    : "Membresía sin rutina. El entrenador asignará una rutina personalizada.");
            }
            catch (Exception ex) { AyudaFormularioVisual.MostrarError(lblEstado, ex); }
        }

        private void objetivo_SelectedIndexChanged(object origen, EventArgs e)
        {
            var libre = Convert.ToString(objetivo.SelectedItem) == "Libre";
            if (libre)
                nivel.SelectedIndex = -1;
            else if (nivel.SelectedIndex < 0 && nivel.Items.Count > 0)
                nivel.SelectedIndex = 0;
            nivel.Enabled = objetivo.Enabled && !libre;
        }

        private void socio_SelectedIndexChanged(object origen, EventArgs e) { ActualizarSugerencia(); }

        private void ActualizarSugerencia()
        {
            var id = socio.SelectedValue is int ? (int)socio.SelectedValue : 0;
            if (id <= 0) { lblSugerencia.Text = "IMC no disponible. Sin sugerencia."; return; }
            var seleccionado = membresiaSeleccionada != null && membresiaSeleccionada.IdSocio == id
                ? membresiaSeleccionada.Socio : new SocioLogica().ObtenerPorId(id);
            var imc = seleccionado == null ? null : RutinaLogica.CalcularImc(seleccionado.Peso, seleccionado.Altura);
            var sugerencia = seleccionado == null ? null : RutinaLogica.SugerirObjetivo(seleccionado.Peso, seleccionado.Altura);
            lblSugerencia.Text = imc.HasValue
                ? "IMC: " + imc.Value.ToString("0.0") + ". Sugerencia según IMC: " + sugerencia + ". Es orientativa."
                : "IMC no disponible. Sin sugerencia.";
        }

        /* Al hacer clic en reactivar, habilita la misma membresía mediante la capa lógica. */
        private void habilitar_Click(object origen, EventArgs e)
        {
            try
            {
                if (idSeleccionado == 0)
                    throw new InvalidOperationException("Selecciona una membresía.");
                logica.Habilitar(idSeleccionado);
                Cargar();
                nuevo_Click(null, EventArgs.Empty);
                AyudaFormularioVisual.MostrarExito(lblEstado, "Membresía reactivada.");
            }
            catch (Exception ex)
            {
                AyudaFormularioVisual.MostrarError(lblEstado, ex);
            }
        }

        /* Al hacer clic en dar de baja, solicita confirmación y conserva la membresía seleccionada. */
        private void deshabilitar_Click(object origen, EventArgs e)
        {
            try
            {
                if (idSeleccionado == 0)
                    throw new InvalidOperationException("Selecciona una membresía.");
                if (MessageBox.Show("¿Dar de baja la membresía seleccionada?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;
                logica.Deshabilitar(idSeleccionado);
                Cargar();
                nuevo_Click(null, EventArgs.Empty);
                AyudaFormularioVisual.MostrarExito(lblEstado, "Membresía dada de baja.");
            }
            catch (Exception ex)
            {
                AyudaFormularioVisual.MostrarError(lblEstado, ex);
            }
        }

        /* Al hacer clic en generarCuota, genera el siguiente período de cuota para la membresía seleccionada. */
        private void generarCuota_Click(object origen, EventArgs e)
        {
            try
            {
                if (idSeleccionado == 0)
                    throw new InvalidOperationException("Selecciona una membresía.");
                var cuotaGenerada = cuotas.GenerarSiguienteCuota(idSeleccionado);
                EstablecerModo(false);
                AyudaFormularioVisual.MostrarExito(lblEstado, "Cuota generada: " + cuotaGenerada.FechaDesde.ToString("dd/MM/yyyy") + " al " + cuotaGenerada.FechaHasta.ToString("dd/MM/yyyy") + ".", true);
            }
            catch (Exception ex)
            {
                AyudaFormularioVisual.MostrarError(lblEstado, ex, true);
            }
        }

        /* Compone el nombre que se muestra en pantalla y contempla socios no disponibles. */
        private static string NombreSocio(Membresia membresiaActual)
        {
            return membresiaActual.Socio == null ? "Socio no disponible" : membresiaActual.Socio.Apellido + ", " + membresiaActual.Socio.Nombre;
        }

        /* Obtiene el nombre del plan para mostrarlo, contemplando relaciones no disponibles. */
        private static string NombrePlan(Membresia membresiaActual)
        {
            return membresiaActual.Plan == null ? "Plan no disponible" : membresiaActual.Plan.Nombre;
        }

        /* Compara el texto de búsqueda sin distinguir mayúsculas y admite valores vacíos. */
        private static bool Contiene(string valor, string criterio)
        {
            return !string.IsNullOrEmpty(valor) && valor.IndexOf(criterio, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        /* Asocia el identificador del socio con el texto mostrado al elegir una membresía. */
        private sealed class OpcionSocioMembresia
        {
            public int IdSocio { get; set; }
            public string Texto { get; set; }
        }

        /* Al cargar la pantalla en ejecución, prepara sus datos iniciales sin realizar consultas desde el diseñador. */
        private void GestionMembresiasFormulario_Load(object origen, EventArgs e)
        {
            if (AyudaFormularioVisual.EnModoDisenio(this))
                return;
            try
            {
                Inicializar();
            }
            catch (Exception ex)
            {
                AyudaFormularioVisual.MostrarError(lblEstado, ex);
            }
        }

        /* Al escribir un criterio de búsqueda, filtra los registros que se muestran en la grilla. */
        private void buscador_TextChanged(object origen, EventArgs e)
        {
            AplicarFiltro();
        }

        /* Limpia el aviso visual al elegir la fecha de inicio de la membresía. */
        private void inicio_ValueChanged(object origen, EventArgs e)
        {
            indicadorErrores.SetError(inicio, string.Empty);
        }

        /* Al salir del socio de un alta, exige una opción disponible. */
        private void socio_Validating(object origen, CancelEventArgs e)
        {
            if (idSeleccionado == 0)
                AyudaFormularioVisual.ValidarCombo(indicadorErrores, socio, "Seleccioná un socio.");
            else
                indicadorErrores.SetError(socio, string.Empty);
        }

        /* Al salir del plan, exige una opción activa. */
        private void plan_Validating(object origen, CancelEventArgs e)
        {
            AyudaFormularioVisual.ValidarCombo(indicadorErrores, plan, "Seleccioná un plan.");
        }

        /* Retira el aviso anterior cuando cambia una selección o fecha. */
        private void LimpiarErroresDelDetalle()
        {
            indicadorErrores.Clear();
        }

        private void campoValidado_Cambiado(object origen, EventArgs e)
        {
            var control = origen as Control;
            if (control != null)
                indicadorErrores.SetError(control, string.Empty);
        }

        /* Valida las selecciones antes de crear o modificar una membresía. */
        private bool ValidarFormulario(bool nueva)
        {
            LimpiarErroresDelDetalle();
            var valido = true;
            if (nueva)
                valido = AyudaFormularioVisual.ValidarCombo(indicadorErrores, socio, "Seleccioná un socio.");
            valido = AyudaFormularioVisual.ValidarCombo(indicadorErrores, plan, "Seleccioná un plan.") & valido;
            if (!valido)
                AyudaFormularioVisual.EnfocarPrimerError(indicadorErrores, socio, plan, inicio);
            return valido;
        }

    }
}
