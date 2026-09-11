using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaLogica;
using exxen2._0.capaVisual.Compartido;

namespace exxen2._0.capaVisual.Entrenador
{
    /* Presenta rutinas entrenador y atiende sus acciones mediante eventos de Windows Forms. */
    [DesignerCategory("Form")]
    public partial class RutinasEntrenadorFormulario : Form
    {
        private readonly UsuarioSistema usuario;
        private readonly RutinaLogica rutinas = new RutinaLogica();
        private readonly RutinaAsignacionLogica asignaciones = new RutinaAsignacionLogica();
        private readonly RutinaEjercicioLogica ejerciciosRutina = new RutinaEjercicioLogica();
        private readonly EjercicioLogica ejercicios = new EjercicioLogica();
        private int idRutina;
        private int idRutinaEjercicio;
        private bool cargandoDetalle;
        /* Inicializa los componentes existentes y las dependencias de la pantalla sin consultar la base de datos. */
        public RutinasEntrenadorFormulario() : this(new UsuarioSistema { Nombre = "Entrenador", Apellido = "de diseno" })
        {
        }

        /* Inicializa los componentes existentes y las dependencias de la pantalla sin consultar la base de datos. */
        public RutinasEntrenadorFormulario(UsuarioSistema usuario)
        {
            if (usuario == null)
                throw new ArgumentNullException("usuario");
            this.usuario = usuario;
            InitializeComponent();
        }

        /* Carga el catálogo de ejercicios activos para incorporarlos a una rutina. */
        private void CargarEjercicios()
        {
            ejercicio.DataSource = ejercicios.ListarActivos();
            ejercicio.DisplayMember = "Nombre";
            ejercicio.ValueMember = "IdEjercicio";
        }

        /* Carga las membresías disponibles y sus datos de presentación para seleccionarlas. */
        private void CargarMembresias()
        {
            try
            {
                membresia.DataSource = null;
                if (idRutina == 0)
                    return;
                membresia.DataSource = asignaciones.ListarMembresiasDisponibles(idRutina);
                membresia.DisplayMember = "IdMembresia";
                membresia.ValueMember = "IdMembresia";
            }
            catch (Exception ex)
            {
                AyudaFormularioVisual.MostrarError(lblEstado, ex);
            }
        }

        /* Al mostrar una opción de membresía, presenta el nombre del socio y su plan. */
        private void membresia_Format(object origen, ListControlConvertEventArgs e)
        {
            var m = e.ListItem as Membresia;
            if (m != null && m.Socio != null)
                e.Value = m.Socio.Apellido + ", " + m.Socio.Nombre + " - " + (m.Plan == null ? "Membresia" : m.Plan.Nombre);
        }

        /* Consulta los registros del módulo y actualiza la grilla, informando los errores de carga. */
        private void Cargar()
        {
            try
            {
                tabla.Rows.Clear();
                foreach (var rutina in rutinas.ListarPorEntrenador(usuario.IdUsuarioSistema))
                {
                    var asignados = rutina.Asignaciones == null ? 0 : rutina.Asignaciones.Count(a => a.Estado);
                    tabla.Rows.Add(rutina.IdRutina, rutina.Nombre, rutina.Entrenador == null ? "-" : rutina.Entrenador.Nombre + " " + rutina.Entrenador.Apellido, asignados, rutina.FechaCreacion.ToString("dd/MM/yyyy"));
                }

                lblEstado.Text = tabla.Rows.Count + " plantilla(s) de rutina";
            }
            catch (Exception ex)
            {
                AyudaFormularioVisual.MostrarError(lblEstado, ex);
            }
        }

        /* Al cambiar la fila seleccionada, toma su identificador y actualiza los datos o acciones del registro. */
        private void tabla_SelectionChanged(object origen, EventArgs e)
        {
            try
            {
                if (tabla.CurrentRow == null || tabla.CurrentRow.Cells[0].Value == null)
                    return;
                idRutina = Convert.ToInt32(tabla.CurrentRow.Cells[0].Value);
                var rutina = rutinas.ObtenerPorId(idRutina);
                if (rutina == null)
                    return;
                nombre.Text = rutina.Nombre;
                descripcion.Text = rutina.Descripcion ?? string.Empty;
                CargarMembresias();
                CargarDetalleDeRutina();
            }
            catch (Exception ex)
            {
                AyudaFormularioVisual.MostrarError(lblEstado, ex);
            }
        }

        /* Presenta los ejercicios de la rutina seleccionada repartidos por día de la semana. */
        private void CargarDetalleDeRutina()
        {
            cargandoDetalle = true;
            try
            {
                tablaEjercicios.Rows.Clear();
                if (idRutina == 0)
                    return;
                foreach (var detalle in ejerciciosRutina.ListarPorRutina(idRutina))
                {
                    tablaEjercicios.Rows.Add(
                        detalle.IdRutinaEjercicio,
                        ValidacionesGimnasio.NombreDia(detalle.DiaSemana),
                        detalle.Orden,
                        detalle.Ejercicio == null ? "-" : detalle.Ejercicio.Nombre,
                        detalle.Series.HasValue ? detalle.Series.Value.ToString() : "-",
                        detalle.Repeticiones.HasValue ? detalle.Repeticiones.Value.ToString() : "-",
                        detalle.Peso.HasValue ? detalle.Peso.Value.ToString("0.##") : "-",
                        detalle.Descanso + "s");
                }

                tablaEjercicios.ClearSelection();
                idRutinaEjercicio = 0;
            }
            finally
            {
                cargandoDetalle = false;
            }
        }

        /* Al elegir un ejercicio de la rutina, carga sus valores para revisarlos o quitarlo. */
        private void tablaEjercicios_SelectionChanged(object origen, EventArgs e)
        {
            if (cargandoDetalle || tablaEjercicios.CurrentRow == null || !tablaEjercicios.CurrentRow.Selected)
                return;
            var fila = tablaEjercicios.CurrentRow;
            if (fila.Cells[0].Value == null)
                return;
            idRutinaEjercicio = Convert.ToInt32(fila.Cells[0].Value);
            var nombreDia = Convert.ToString(fila.Cells[1].Value);
            var indiceDia = dia.Items.IndexOf(nombreDia);
            if (indiceDia >= 0)
                dia.SelectedIndex = indiceDia;
            orden.Text = Convert.ToString(fila.Cells[2].Value);
            var nombreEjercicio = Convert.ToString(fila.Cells[3].Value);
            for (var indice = 0; indice < ejercicio.Items.Count; indice++)
            {
                var opcion = ejercicio.Items[indice] as Ejercicio;
                if (opcion != null && opcion.Nombre == nombreEjercicio)
                {
                    ejercicio.SelectedIndex = indice;
                    break;
                }
            }
        }

        /* Al hacer clic en quitarEjercicio, da de baja el ejercicio elegido de la plantilla. */
        private void quitarEjercicio_Click(object origen, EventArgs e)
        {
            try
            {
                if (idRutinaEjercicio == 0)
                    throw new InvalidOperationException("Selecciona un ejercicio de la rutina.");
                ejerciciosRutina.Quitar(idRutinaEjercicio);
                CargarDetalleDeRutina();
                AyudaFormularioVisual.MostrarExito(lblEstado, "Ejercicio quitado de la plantilla.");
            }
            catch (Exception ex)
            {
                AyudaFormularioVisual.MostrarError(lblEstado, ex);
            }
        }

        /* Traduce el día elegido en el combo al número que guarda la rutina. */
        private int? DiaSeleccionado()
        {
            return dia.SelectedIndex < 0 ? (int? )null : dia.SelectedIndex + ValidacionesGimnasio.PrimerDiaRutina;
        }

        /* Al hacer clic en nuevaRutina, limpia la selección para crear una plantilla de rutina. */
        private void nuevaRutina_Click(object origen, EventArgs e)
        {
            idRutina = 0;
            idRutinaEjercicio = 0;
            nombre.Clear();
            descripcion.Clear();
            tabla.ClearSelection();
            CargarMembresias();
            CargarDetalleDeRutina();
        }

        /* Al hacer clic en guardarRutina, crea o actualiza la plantilla mediante RutinaLogica. */
        private void guardarRutina_Click(object origen, EventArgs e)
        {
            try
            {
                var rutina = new Rutina
                {
                    IdRutina = idRutina,
                    Nombre = nombre.Text.Trim(),
                    Descripcion = descripcion.Text.Trim(),
                    IdEntrenador = usuario.IdUsuarioSistema,
                    FechaCreacion = DateTime.Now,
                    Estado = true
                };
                if (idRutina == 0)
                {
                    rutinas.Crear(rutina);
                    idRutina = rutina.IdRutina;
                    AyudaFormularioVisual.MostrarExito(lblEstado, "Plantilla creada. Ahora podes agregarle ejercicios y asignarla a socios.");
                }
                else
                {
                    rutinas.Modificar(rutina);
                    AyudaFormularioVisual.MostrarExito(lblEstado, "Plantilla actualizada.");
                }

                Cargar();
            }
            catch (Exception ex)
            {
                AyudaFormularioVisual.MostrarError(lblEstado, ex);
            }
        }

        /* Al hacer clic en agregarEjercicio, valida los parámetros y agrega el ejercicio a la rutina. */
        private void agregarEjercicio_Click(object origen, EventArgs e)
        {
            try
            {
                if (idRutina == 0)
                {
                    guardarRutina_Click(null, EventArgs.Empty);
                    if (idRutina == 0)
                        return;
                }

                ejerciciosRutina.AgregarEjercicio(new RutinaEjercicio { IdRutina = idRutina, IdEjercicio = Convert.ToInt32(ejercicio.SelectedValue), Series = EnteroOpcional(series), Repeticiones = EnteroOpcional(repeticiones), Peso = DecimalOpcional(peso), Descanso = EnteroOpcional(descanso) ?? 0, Orden = EnteroOpcional(orden) ?? 1, DiaSemana = DiaSeleccionado() });
                CargarDetalleDeRutina();
                AyudaFormularioVisual.MostrarExito(lblEstado, "Ejercicio agregado al " + ValidacionesGimnasio.NombreDia(DiaSeleccionado()) + " de la plantilla.");
            }
            catch (Exception ex)
            {
                AyudaFormularioVisual.MostrarError(lblEstado, ex);
            }
        }

        /* Al hacer clic en asignar, valida la selección y registra la asignación mediante la capa lógica. */
        private void asignar_Click(object origen, EventArgs e)
        {
            try
            {
                if (idRutina == 0)
                    throw new InvalidOperationException("Selecciona o crea una rutina primero.");
                if (membresia.SelectedValue == null)
                    throw new InvalidOperationException("Selecciona una membresia.");
                asignaciones.Asignar(idRutina, Convert.ToInt32(membresia.SelectedValue));
                Cargar();
                AyudaFormularioVisual.MostrarExito(lblEstado, "Rutina asignada al socio. La misma plantilla puede asignarse a otros socios.");
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
                if (idRutina == 0)
                    throw new InvalidOperationException("Selecciona una rutina.");
                rutinas.DarDeBaja(idRutina);
                Cargar();
                nuevaRutina_Click(null, EventArgs.Empty);
                AyudaFormularioVisual.MostrarExito(lblEstado, "Plantilla dada de baja y asignaciones finalizadas.");
            }
            catch (Exception ex)
            {
                AyudaFormularioVisual.MostrarError(lblEstado, ex);
            }
        }

        /* Convierte un campo opcional a entero o informa un formato inválido. */
        private static int? EnteroOpcional(TextBox campo)
        {
            int valor;
            return string.IsNullOrWhiteSpace(campo.Text) ? (int? )null : (int.TryParse(campo.Text, out valor) ? valor : throw new InvalidOperationException("Revisa los valores numericos del ejercicio."));
        }

        /* Convierte un campo opcional a decimal o informa un formato inválido. */
        private static decimal? DecimalOpcional(TextBox campo)
        {
            decimal valor;
            return string.IsNullOrWhiteSpace(campo.Text) ? (decimal? )null : (decimal.TryParse(campo.Text, out valor) ? valor : throw new InvalidOperationException("Revisa el peso del ejercicio."));
        }

        /* Al cargar la pantalla en ejecución, prepara sus datos iniciales sin realizar consultas desde el diseñador. */
        private void RutinasEntrenadorFormulario_Load(object origen, EventArgs e)
        {
            if (AyudaFormularioVisual.EnModoDisenio(this))
                return;
            try
            {
                CargarEjercicios();
                CargarMembresias();
                Cargar();
                nuevaRutina_Click(null, EventArgs.Empty);
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

        /* Al hacer clic en actualizar, vuelve a consultar y mostrar los registros del módulo. */
        private void actualizar_Click(object origen, EventArgs e)
        {
            Cargar();
            CargarMembresias();
            CargarDetalleDeRutina();
        }
    }
}
