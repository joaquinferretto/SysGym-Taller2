using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaLogica;
using exxen2._0.capaVisual.Compartido;

using exxen2._0.capaLogica.Utilidades;

namespace exxen2._0.capaVisual.Recepcionista
{
    /* Presenta membresias y atiende sus acciones mediante eventos de Windows Forms. */
    [DesignerCategory("Form")]
    public partial class GestionMembresiasFormulario : Form
    {
        private readonly UsuarioSistema usuario;
        private readonly MembresiaLogica logica = new MembresiaLogica();
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
            InitializeComponent();
        }

        /* Carga las opciones y los registros necesarios y prepara el formulario para una nueva operación. */
        private void Inicializar()
        {
            try
            {
                CargarCombos();
                Cargar();
                nuevo_Click(null, EventArgs.Empty);
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
                .Select(s => new OpcionSocioMembresia { IdSocio = s.IdSocio, Texto = s.Apellido + ", " + s.Nombre + " - DNI " + s.DNI })
                .ToList();
            socio.DisplayMember = "Texto";
            socio.ValueMember = "IdSocio";
        }

        /* Mantiene seleccionable en modo consulta al socio de la membresía histórica. */
        private void CargarSocioDeMembresia(Membresia membresia)
        {
            var opciones = logica.ListarSociosDisponiblesParaAlta()
                .Select(s => new OpcionSocioMembresia { IdSocio = s.IdSocio, Texto = s.Apellido + ", " + s.Nombre + " - DNI " + s.DNI })
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
                tabla.Rows.Add(membresiaActual.IdMembresia, NombreSocio(membresiaActual), membresiaActual.Socio == null ? "-" : membresiaActual.Socio.DNI, NombrePlan(membresiaActual), membresiaActual.FechaInicio.ToString("dd/MM/yyyy"), membresiaActual.FechaVencimiento.ToString("dd/MM/yyyy"), membresiaActual.Estado ? "Activa" : "Inactiva");
            tabla.ClearSelection();
            cargandoTabla = false;
            lblEstado.Text = tabla.Rows.Count + " membresia(s) encontrada(s)";
        }

        /* Al cambiar la fila seleccionada, toma su identificador y actualiza los datos o acciones del registro. */
        private void tabla_SelectionChanged(object origen, EventArgs e)
        {
            if (cargandoTabla || tabla.CurrentRow == null || !tabla.CurrentRow.Selected)
                return;
            idSeleccionado = Convert.ToInt32(tabla.CurrentRow.Cells[0].Value);
            membresiaSeleccionada = membresiasCargadas.FirstOrDefault(m => m.IdMembresia == idSeleccionado);
            if (membresiaSeleccionada == null)
                return;
            CargarSocioDeMembresia(membresiaSeleccionada);
            socio.SelectedValue = membresiaSeleccionada.IdSocio;
            plan.SelectedValue = membresiaSeleccionada.IdPlan;
            inicio.Value = membresiaSeleccionada.FechaInicio;
            vencimiento.Value = membresiaSeleccionada.FechaVencimiento;
            socio.Enabled = false;
            plan.Enabled = true;
            EstablecerModo(false);
            lblFormulario.Text = "Membresía de " + NombreSocio(membresiaSeleccionada) + " - " + (membresiaSeleccionada.Estado ? "Activa" : "Inactiva");
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
            vencimiento.Value = DateTime.Today.AddMonths(1).AddDays(-1);
            EstablecerModo(true);
        }

        /* Habilita las acciones disponibles según la selección y el estado del registro. */
        private void EstablecerModo(bool nueva)
        {
            var puedeCrear = socio.Items.Count > 0 && plan.Items.Count > 0;
            if (nueva)
                lblFormulario.Text = plan.Items.Count == 0 ? "Primero crea un plan" : (socio.Items.Count == 0 ? "No hay socios disponibles para una nueva membresía" : "Nueva membresía - Estado inicial: Activa");
            crear.Enabled = nueva && puedeCrear;
            actualizar.Enabled = !nueva;
            habilitar.Enabled = !nueva && membresiaSeleccionada != null && !membresiaSeleccionada.Estado;
            deshabilitar.Enabled = !nueva && membresiaSeleccionada != null && membresiaSeleccionada.Estado;
            generarCuota.Enabled = !nueva && membresiaSeleccionada != null && membresiaSeleccionada.Estado;
        }

        /* Al hacer clic en crear, registra la membresía y su primera cuota mediante MembresiaLogica. */
        private void crear_Click(object origen, EventArgs e)
        {
            try
            {
                AyudaFormularioVisual.ValidarComboSeleccionado(socio, "un socio");
                AyudaFormularioVisual.ValidarComboSeleccionado(plan, "un plan");
                logica.Crear(new Membresia { IdSocio = Convert.ToInt32(socio.SelectedValue), IdPlan = Convert.ToInt32(plan.SelectedValue), IdUsuarioSistema = usuario.IdUsuarioSistema, FechaInicio = inicio.Value.Date, FechaVencimiento = vencimiento.Value.Date });
                Cargar();
                nuevo_Click(null, EventArgs.Empty);
                AyudaFormularioVisual.MostrarExito(lblEstado, "Membresia creada y primera cuota generada.", true);
            }
            catch (Exception ex)
            {
                AyudaFormularioVisual.MostrarError(lblEstado, ex, true);
            }
        }

        /* Al hacer clic en actualizar, valida los campos y guarda las modificaciones mediante la capa lógica. */
        private void actualizar_Click(object origen, EventArgs e)
        {
            try
            {
                if (membresiaSeleccionada == null)
                    throw new InvalidOperationException("Selecciona una membresia.");
                AyudaFormularioVisual.ValidarComboSeleccionado(plan, "un plan");
                AyudaFormularioVisual.ValidarRangoFechas(inicio, vencimiento, "fecha de inicio", "fecha de vencimiento");
                var idPlan = Convert.ToInt32(plan.SelectedValue);
                if (idPlan != membresiaSeleccionada.IdPlan)
                    logica.CambiarPlan(membresiaSeleccionada.IdMembresia, idPlan);
                logica.Modificar(new Membresia { IdMembresia = membresiaSeleccionada.IdMembresia, IdSocio = membresiaSeleccionada.IdSocio, IdPlan = idPlan, IdUsuarioSistema = membresiaSeleccionada.IdUsuarioSistema, FechaInicio = inicio.Value.Date, FechaVencimiento = vencimiento.Value.Date, Estado = membresiaSeleccionada.Estado });
                Cargar();
                nuevo_Click(null, EventArgs.Empty);
                AyudaFormularioVisual.MostrarExito(lblEstado, "Membresia actualizada.");
            }
            catch (Exception ex)
            {
                AyudaFormularioVisual.MostrarError(lblEstado, ex);
            }
        }

        /* Al hacer clic en reactivar, habilita la misma membresía mediante la capa lógica. */
        private void habilitar_Click(object origen, EventArgs e)
        {
            try
            {
                if (idSeleccionado == 0)
                    throw new InvalidOperationException("Selecciona una membresia.");
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
                    throw new InvalidOperationException("Selecciona una membresia.");
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
                    throw new InvalidOperationException("Selecciona una membresia.");
                cuotas.GenerarSiguienteCuota(idSeleccionado);
                AyudaFormularioVisual.MostrarExito(lblEstado, "Nueva cuota generada.", true);
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

        /* Al hacer clic en btnVolver, cierra el módulo y devuelve el control al panel principal. */
        private void btnVolver_Click(object origen, EventArgs e)
        {
            Close();
        }

        /* Al escribir un criterio de búsqueda, filtra los registros que se muestran en la grilla. */
        private void buscador_TextChanged(object origen, EventArgs e)
        {
            AplicarFiltro();
        }

        /* Al cambiar la fecha de inicio de un alta, ajusta el vencimiento del período mensual. */
        private void inicio_ValueChanged(object origen, EventArgs e)
        {
            if (idSeleccionado == 0)
                vencimiento.Value = inicio.Value.Date.AddMonths(1).AddDays(-1);
        }
    }
}
