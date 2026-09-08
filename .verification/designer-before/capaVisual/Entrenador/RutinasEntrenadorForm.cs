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
    [DesignerCategory("Form")]
    public partial class RutinasEntrenadorForm : Form
    {
        private readonly UsuarioSistema usuario;
        private readonly RutinaLogica rutinas = new RutinaLogica();
        private readonly RutinaAsignacionLogica asignaciones = new RutinaAsignacionLogica();
        private readonly RutinaEjercicioLogica ejerciciosRutina = new RutinaEjercicioLogica();
        private readonly EjercicioLogica ejercicios = new EjercicioLogica();
        private readonly MembresiaLogica membresias = new MembresiaLogica();
        private int idRutina;

        public RutinasEntrenadorForm()
            : this(new UsuarioSistema { Nombre = "Entrenador", Apellido = "de diseno" })
        {
        }

        public RutinasEntrenadorForm(UsuarioSistema usuario)
        {
            if (usuario == null) throw new ArgumentNullException("usuario");
            this.usuario = usuario;
            InitializeComponent();
            btnVolver.Click += delegate { Close(); };
            membresia.Format += FormatearMembresia;
            tabla.SelectionChanged += Seleccionar;
            FormularioVisualHelper.AlCargarEnEjecucion(this, delegate { CargarEjercicios(); CargarMembresias(); Cargar(); NuevaRutina(null, EventArgs.Empty); });
        }

        private void CargarEjercicios() { ejercicio.DataSource = ejercicios.ListarActivos(); ejercicio.DisplayMember = "Nombre"; ejercicio.ValueMember = "IdEjercicio"; }
        private void CargarMembresias() { membresia.DataSource = membresias.ListarHabilitadas(); membresia.DisplayMember = "IdMembresia"; membresia.ValueMember = "IdMembresia"; }
        private static void FormatearMembresia(object sender, ListControlConvertEventArgs e) { var m = e.ListItem as Membresia; if (m != null && m.Socio != null) e.Value = m.Socio.Apellido + ", " + m.Socio.Nombre + " - " + (m.Plan == null ? "Membresia" : m.Plan.Nombre); }

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
            catch (Exception ex) { FormularioVisualHelper.MostrarError(lblEstado, ex); }
        }

        private void Seleccionar(object sender, EventArgs e)
        {
            if (tabla.CurrentRow == null || tabla.CurrentRow.Cells[0].Value == null) return;
            idRutina = Convert.ToInt32(tabla.CurrentRow.Cells[0].Value); var rutina = rutinas.ObtenerPorId(idRutina); if (rutina == null) return; nombre.Text = rutina.Nombre; descripcion.Text = rutina.Descripcion ?? string.Empty;
        }

        private void NuevaRutina(object sender, EventArgs e) { idRutina = 0; nombre.Clear(); descripcion.Clear(); tabla.ClearSelection(); }
        private void CrearOActualizar(object sender, EventArgs e)
        {
            try
            {
                var rutina = new Rutina { IdRutina = idRutina, Nombre = nombre.Text.Trim(), Descripcion = descripcion.Text.Trim(), IdEntrenador = usuario.IdUsuarioSistema, FechaCreacion = DateTime.Now, Estado = true };
                if (idRutina == 0) { rutinas.Crear(rutina); idRutina = rutina.IdRutina; FormularioVisualHelper.MostrarExito(lblEstado, "Plantilla creada. Ahora podes agregarle ejercicios y asignarla a socios."); } else { rutinas.Modificar(rutina); FormularioVisualHelper.MostrarExito(lblEstado, "Plantilla actualizada."); }
                Cargar();
            }
            catch (Exception ex) { FormularioVisualHelper.MostrarError(lblEstado, ex); }
        }

        private void AgregarEjercicio(object sender, EventArgs e)
        {
            try
            {
                if (idRutina == 0) { CrearOActualizar(null, EventArgs.Empty); if (idRutina == 0) return; }
                ejerciciosRutina.AgregarEjercicio(new RutinaEjercicio { IdRutina = idRutina, IdEjercicio = Convert.ToInt32(ejercicio.SelectedValue), Series = EnteroOpcional(series), Repeticiones = EnteroOpcional(repeticiones), Peso = DecimalOpcional(peso), Descanso = EnteroOpcional(descanso) ?? 0, Orden = EnteroOpcional(orden) ?? 1 }); FormularioVisualHelper.MostrarExito(lblEstado, "Ejercicio agregado a la plantilla.");
            }
            catch (Exception ex) { FormularioVisualHelper.MostrarError(lblEstado, ex); }
        }

        private void Asignar(object sender, EventArgs e)
        {
            try { if (idRutina == 0) throw new InvalidOperationException("Selecciona o crea una rutina primero."); if (membresia.SelectedValue == null) throw new InvalidOperationException("Selecciona una membresia."); asignaciones.Asignar(idRutina, Convert.ToInt32(membresia.SelectedValue)); Cargar(); FormularioVisualHelper.MostrarExito(lblEstado, "Rutina asignada al socio. La misma plantilla puede asignarse a otros socios."); }
            catch (Exception ex) { FormularioVisualHelper.MostrarError(lblEstado, ex); }
        }

        private void DarDeBaja(object sender, EventArgs e)
        {
            try { if (idRutina == 0) throw new InvalidOperationException("Selecciona una rutina."); rutinas.DarDeBaja(idRutina); Cargar(); NuevaRutina(null, EventArgs.Empty); FormularioVisualHelper.MostrarExito(lblEstado, "Plantilla dada de baja y asignaciones finalizadas."); }
            catch (Exception ex) { FormularioVisualHelper.MostrarError(lblEstado, ex); }
        }

        private static int? EnteroOpcional(TextBox campo) { int valor; return string.IsNullOrWhiteSpace(campo.Text) ? (int?)null : (int.TryParse(campo.Text, out valor) ? valor : throw new InvalidOperationException("Revisa los valores numericos del ejercicio.")); }
        private static decimal? DecimalOpcional(TextBox campo) { decimal valor; return string.IsNullOrWhiteSpace(campo.Text) ? (decimal?)null : (decimal.TryParse(campo.Text, out valor) ? valor : throw new InvalidOperationException("Revisa el peso del ejercicio.")); }
    }
}
