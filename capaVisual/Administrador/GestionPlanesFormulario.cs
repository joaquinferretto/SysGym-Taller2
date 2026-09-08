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
    /* Presenta planes y atiende sus acciones mediante eventos de Windows Forms. */
    [DesignerCategory("Form")]
    public partial class GestionPlanesFormulario : Form
    {
        private readonly PlanLogica logica = new PlanLogica();
        private readonly RutinaLogica rutinas = new RutinaLogica();
        private List<Plan> planesCargados = new List<Plan>();
        private int idSeleccionado;
        private bool cargandoTabla;
        private bool estadoSeleccionado = true;
        /* Inicializa los componentes existentes y las dependencias de la pantalla sin consultar la base de datos. */
        public GestionPlanesFormulario()
        {
            InitializeComponent();
        }

        /* Carga las opciones y los registros necesarios y prepara el formulario para una nueva operación. */
        private void Inicializar()
        {
            try
            {
                CargarRutinas();
                Cargar();
                nuevo_Click(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                AyudaFormularioVisual.MostrarError(lblEstado, ex);
            }
        }

        /* Carga las plantillas activas que pueden elegirse como rutina base del plan. */
        private void CargarRutinas()
        {
            rutina.DataSource = rutinas.ListarActivas();
            rutina.DisplayMember = "Nombre";
            rutina.ValueMember = "IdRutina";
        }

        /* Consulta los registros del módulo y actualiza la grilla, informando los errores de carga. */
        private void Cargar()
        {
            try
            {
                planesCargados = logica.ListarParaGestion();
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
            var estadoElegido = Convert.ToString(filtroEstado.SelectedItem);
            var filtrados = planesCargados.AsEnumerable();
            if (estadoElegido == "Activos")
                filtrados = filtrados.Where(p => p.Estado);
            else if (estadoElegido == "Inactivos")
                filtrados = filtrados.Where(p => !p.Estado);
            if (!string.IsNullOrWhiteSpace(criterio))
                filtrados = filtrados.Where(p => Contiene(p.Nombre, criterio) || Contiene(p.Descripcion, criterio));
            cargandoTabla = true;
            tabla.Rows.Clear();
            foreach (var plan in filtrados)
                tabla.Rows.Add(plan.IdPlan, plan.Nombre, plan.Precio.ToString("C"), plan.Rutina == null ? "Sin rutina" : plan.Rutina.Nombre, DescribirBeneficios(plan), plan.Estado ? "Activo" : "Inactivo");
            tabla.ClearSelection();
            cargandoTabla = false;
            lblEstado.Text = tabla.Rows.Count + " plan(es) encontrado(s)";
        }

        /* Presenta los beneficios de entrenador y rutina del plan en una descripción legible. */
        private static string DescribirBeneficios(Plan plan)
        {
            if (plan.IncluyeEntrenador && plan.IncluyeRutinaPersonal)
                return "Entrenador y rutina personalizada";
            if (plan.IncluyeEntrenador)
                return "Entrenador";
            if (plan.IncluyeRutinaPersonal)
                return "Rutina personalizada";
            return "Plan basico";
        }

        /* Compara el texto de búsqueda sin distinguir mayúsculas y admite valores vacíos. */
        private static bool Contiene(string valor, string criterio)
        {
            return !string.IsNullOrEmpty(valor) && valor.IndexOf(criterio, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        /* Al cambiar la fila seleccionada, toma su identificador y actualiza los datos o acciones del registro. */
        private void tabla_SelectionChanged(object origen, EventArgs e)
        {
            if (cargandoTabla || tabla.CurrentRow == null || !tabla.CurrentRow.Selected)
                return;
            try
            {
                idSeleccionado = Convert.ToInt32(tabla.CurrentRow.Cells[0].Value);
                var plan = logica.ObtenerPorId(idSeleccionado);
                if (plan == null)
                    return;
                estadoSeleccionado = plan.Estado;
                nombre.Text = plan.Nombre;
                descripcion.Text = plan.Descripcion;
                precio.Text = plan.Precio.ToString("0.00");
                rutina.SelectedValue = plan.IdRutina;
                incluyeEntrenador.Checked = plan.IncluyeEntrenador;
                incluyeRutina.Checked = plan.IncluyeRutinaPersonal;
                EstablecerModo(false, plan.Estado);
            }
            catch (Exception ex)
            {
                AyudaFormularioVisual.MostrarError(lblEstado, ex);
            }
        }

        /* Al hacer clic en nuevo, limpia la selección y prepara el registro de nuevos datos. */
        private void nuevo_Click(object origen, EventArgs e)
        {
            idSeleccionado = 0;
            estadoSeleccionado = true;
            nombre.Clear();
            descripcion.Clear();
            precio.Clear();
            incluyeEntrenador.Checked = false;
            incluyeRutina.Checked = false;
            if (rutina.Items.Count > 0)
                rutina.SelectedIndex = 0;
            tabla.ClearSelection();
            EstablecerModo(true, true);
            nombre.Focus();
        }

        /* Habilita las acciones disponibles según la selección y el estado del registro. */
        private void EstablecerModo(bool nuevoRegistro, bool activo)
        {
            lblFormulario.Text = nuevoRegistro ? "Nuevo plan - Estado inicial: Activo" : "Editar plan";
            guardar.Enabled = nuevoRegistro;
            actualizar.Enabled = !nuevoRegistro;
            darDeBaja.Enabled = !nuevoRegistro && activo;
            reactivar.Enabled = !nuevoRegistro && !activo;
        }

        /* Recoge el precio, los beneficios y la rutina seleccionada para enviar el plan a la lógica. */
        private Plan LeerPlan()
        {
            if (rutina.SelectedValue == null)
                throw new InvalidOperationException("Selecciona una rutina base.");
            return new Plan
            {
                IdPlan = idSeleccionado,
                Nombre = nombre.Text.Trim(),
                Descripcion = descripcion.Text.Trim(),
                Precio = AyudaFormularioVisual.DecimalPositivo(precio, "precio"),
                IdRutina = Convert.ToInt32(rutina.SelectedValue),
                IncluyeEntrenador = incluyeEntrenador.Checked,
                IncluyeRutinaPersonal = incluyeRutina.Checked,
                Estado = estadoSeleccionado
            };
        }

        /* Al hacer clic en guardar, valida los campos y envía el registro a la capa lógica. */
        private void guardar_Click(object origen, EventArgs e)
        {
            try
            {
                if (idSeleccionado != 0)
                    return;
                logica.Crear(LeerPlan());
                Cargar();
                nuevo_Click(null, EventArgs.Empty);
                AyudaFormularioVisual.MostrarExito(lblEstado, "Plan creado correctamente.");
            }
            catch (Exception ex)
            {
                AyudaFormularioVisual.MostrarError(lblEstado, ex);
            }
        }

        /* Al hacer clic en actualizar, valida los campos y guarda las modificaciones mediante la capa lógica. */
        private void actualizar_Click(object origen, EventArgs e)
        {
            try
            {
                if (idSeleccionado == 0)
                    throw new InvalidOperationException("Selecciona un plan.");
                logica.Modificar(LeerPlan());
                Cargar();
                nuevo_Click(null, EventArgs.Empty);
                AyudaFormularioVisual.MostrarExito(lblEstado, "Plan actualizado correctamente.");
            }
            catch (Exception ex)
            {
                AyudaFormularioVisual.MostrarError(lblEstado, ex);
            }
        }

        /* Al hacer clic en darDeBaja, solicita la baja lógica del registro seleccionado y actualiza el listado. */
        private void darDeBaja_Click(object origen, EventArgs e)
        {
            try
            {
                if (idSeleccionado == 0)
                    throw new InvalidOperationException("Selecciona un plan.");
                if (MessageBox.Show("Dar de baja al plan seleccionado?", "Confirmar baja", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;
                logica.DarDeBaja(idSeleccionado);
                Cargar();
                nuevo_Click(null, EventArgs.Empty);
                AyudaFormularioVisual.MostrarExito(lblEstado, "Plan dado de baja.");
            }
            catch (Exception ex)
            {
                AyudaFormularioVisual.MostrarError(lblEstado, ex);
            }
        }

        /* Al hacer clic en reactivar, solicita la reactivación del registro seleccionado y actualiza el listado. */
        private void reactivar_Click(object origen, EventArgs e)
        {
            try
            {
                if (idSeleccionado == 0)
                    throw new InvalidOperationException("Selecciona un plan.");
                logica.Reactivar(idSeleccionado);
                Cargar();
                nuevo_Click(null, EventArgs.Empty);
                AyudaFormularioVisual.MostrarExito(lblEstado, "Plan reactivado correctamente.");
            }
            catch (Exception ex)
            {
                AyudaFormularioVisual.MostrarError(lblEstado, ex);
            }
        }

        /* Al cargar la pantalla en ejecución, prepara sus datos iniciales sin realizar consultas desde el diseñador. */
        private void GestionPlanesFormulario_Load(object origen, EventArgs e)
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

        /* Al cambiar el filtro de estado, actualiza los registros visibles en la grilla. */
        private void filtroEstado_SelectedIndexChanged(object origen, EventArgs e)
        {
            AplicarFiltro();
        }

        /* Al escribir en el campo, permite números y un único separador decimal mediante la validación visual compartida. */
        private void precio_KeyPress(object origen, KeyPressEventArgs e)
        {
            AyudaFormularioVisual.ValidarEntradaDecimal(precio, e);
        }
    }
}
