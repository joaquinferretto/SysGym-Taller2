using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaLogica;
using exxen2._0.capaVisual.Compartido;

namespace exxen2._0.capaVisual.Entrenador
{
    public class RutinasEntrenadorFormBase : FormularioModuloBase
    {
        private readonly UsuarioSistema usuario;
        private readonly RutinaLogica rutinas = new RutinaLogica();
        private readonly RutinaAsignacionLogica asignaciones = new RutinaAsignacionLogica();
        private readonly RutinaEjercicioLogica ejerciciosRutina = new RutinaEjercicioLogica();
        private readonly EjercicioLogica ejercicios = new EjercicioLogica();
        private readonly MembresiaLogica membresias = new MembresiaLogica();
        private readonly ComboBox membresia;
        private readonly ComboBox ejercicio;
        private readonly TextBox nombre;
        private readonly TextBox descripcion;
        private readonly TextBox series;
        private readonly TextBox repeticiones;
        private readonly TextBox peso;
        private readonly TextBox descanso;
        private readonly TextBox orden;
        private int idRutina;

        public RutinasEntrenadorFormBase()
            : this(new UsuarioSistema
            {
                Nombre = "Entrenador",
                Apellido = "de diseño"
            })
        {
        }

        public RutinasEntrenadorFormBase(UsuarioSistema usuario)
            : base("Catálogo de rutinas", "Creá plantillas generales y asignalas a las membresías de tus socios", Color.FromArgb(14, 116, 144))
        {
            this.usuario = usuario;
            var grupo = CrearGrupo("Plantilla de rutina", 1060, 184, new Point(20, 160));
            nombre = Campo(grupo, "Nombre", 15, 25, 230);
            descripcion = Campo(grupo, "Descripción", 300, 25, 300);
            ejercicio = Selector(grupo, "Ejercicio", 15, 58, 285);
            series = Campo(grupo, "Series", 330, 58, 70);
            repeticiones = Campo(grupo, "Repeticiones", 430, 58, 90);
            peso = Campo(grupo, "Peso", 550, 58, 70);
            descanso = Campo(grupo, "Descanso (seg.)", 650, 58, 95);
            orden = Campo(grupo, "Orden", 775, 58, 65);
            membresia = Selector(grupo, "Asignar a membresía", 15, 99, 505);
            membresia.Format += FormatearMembresia;
            AgregarPanelFormulario(grupo);

            AgregarBoton("Nueva rutina", NuevaRutina);
            AgregarBoton("Guardar rutina", CrearOActualizar, true);
            AgregarBoton("Actualizar", delegate { Cargar(); });
            AgregarBoton("Agregar ejercicio", AgregarEjercicio);
            AgregarBoton("Asignar a socio", Asignar);
            AgregarBoton("Dar de baja", DarDeBaja);
            Tabla.Columns.Add("Id", "Id"); Tabla.Columns[0].Visible = false;
            Tabla.Columns.Add("Rutina", "Rutina"); Tabla.Columns.Add("Creador", "Entrenador");
            Tabla.Columns.Add("Asignados", "Socios asignados"); Tabla.Columns.Add("Creacion", "Creación");
            Tabla.SelectionChanged += Seleccionar;
            AlCargarEnEjecucion(delegate { CargarEjercicios(); CargarMembresias(); Cargar(); NuevaRutina(null, EventArgs.Empty); });
        }

        private void CargarEjercicios()
        {
            ejercicio.DataSource = ejercicios.ListarActivos();
            ejercicio.DisplayMember = "Nombre";
            ejercicio.ValueMember = "IdEjercicio";
        }

        private void CargarMembresias()
        {
            membresia.DataSource = membresias.ListarHabilitadas();
            membresia.DisplayMember = "IdMembresia";
            membresia.ValueMember = "IdMembresia";
        }

        private static void FormatearMembresia(object sender, ListControlConvertEventArgs e)
        {
            var m = e.ListItem as Membresia;
            if (m != null && m.Socio != null)
            {
                e.Value = m.Socio.Apellido + ", " + m.Socio.Nombre + " · "
                    + (m.Plan == null ? "Membresía" : m.Plan.Nombre);
            }
        }

        private void Cargar()
        {
            try
            {
                Tabla.Rows.Clear();
                foreach (var rutina in rutinas.ListarPorEntrenador(usuario.IdUsuarioSistema))
                {
                    var asignados = rutina.Asignaciones == null ? 0 : rutina.Asignaciones.Count(a => a.Estado);
                    Tabla.Rows.Add(rutina.IdRutina, rutina.Nombre,
                        rutina.Entrenador == null ? "-" : rutina.Entrenador.Nombre + " " + rutina.Entrenador.Apellido,
                        asignados, rutina.FechaCreacion.ToString("dd/MM/yyyy"));
                }
                Estado.Text = Tabla.Rows.Count + " plantilla(s) de rutina";
            }
            catch (Exception ex) { MostrarError(ex); }
        }

        private void Seleccionar(object sender, EventArgs e)
        {
            if (Tabla.CurrentRow == null || Tabla.CurrentRow.Cells[0].Value == null) return;
            idRutina = Convert.ToInt32(Tabla.CurrentRow.Cells[0].Value);
            var rutina = rutinas.ObtenerPorId(idRutina);
            if (rutina == null) return;
            nombre.Text = rutina.Nombre;
            descripcion.Text = rutina.Descripcion ?? string.Empty;
        }

        private void NuevaRutina(object sender, EventArgs e)
        {
            idRutina = 0;
            nombre.Clear(); descripcion.Clear();
            Tabla.ClearSelection();
        }

        private void CrearOActualizar(object sender, EventArgs e)
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
                    MostrarExito("Plantilla creada. Ahora podés agregarle ejercicios y asignarla a socios.");
                }
                else
                {
                    rutinas.Modificar(rutina);
                    MostrarExito("Plantilla actualizada.");
                }
                Cargar();
            }
            catch (Exception ex) { MostrarError(ex); }
        }

        private void AgregarEjercicio(object sender, EventArgs e)
        {
            try
            {
                if (idRutina == 0) { CrearOActualizar(null, EventArgs.Empty); if (idRutina == 0) return; }
                ejerciciosRutina.AgregarEjercicio(new RutinaEjercicio
                {
                    IdRutina = idRutina,
                    IdEjercicio = Convert.ToInt32(ejercicio.SelectedValue),
                    Series = EnteroOpcional(series),
                    Repeticiones = EnteroOpcional(repeticiones),
                    Peso = DecimalOpcional(peso),
                    Descanso = EnteroOpcional(descanso) ?? 0,
                    Orden = EnteroOpcional(orden) ?? 1
                });
                MostrarExito("Ejercicio agregado a la plantilla.");
            }
            catch (Exception ex) { MostrarError(ex); }
        }

        private void Asignar(object sender, EventArgs e)
        {
            try
            {
                if (idRutina == 0) throw new InvalidOperationException("Seleccioná o creá una rutina primero.");
                if (membresia.SelectedValue == null) throw new InvalidOperationException("Seleccioná una membresía.");
                asignaciones.Asignar(idRutina, Convert.ToInt32(membresia.SelectedValue));
                Cargar();
                MostrarExito("Rutina asignada al socio. La misma plantilla puede asignarse a otros socios.");
            }
            catch (Exception ex) { MostrarError(ex); }
        }

        private void DarDeBaja(object sender, EventArgs e)
        {
            try
            {
                if (idRutina == 0) throw new InvalidOperationException("Seleccioná una rutina.");
                rutinas.DarDeBaja(idRutina); Cargar(); NuevaRutina(null, EventArgs.Empty);
                MostrarExito("Plantilla dada de baja y asignaciones finalizadas.");
            }
            catch (Exception ex) { MostrarError(ex); }
        }

        private static int? EnteroOpcional(TextBox campo)
        {
            int valor;
            return string.IsNullOrWhiteSpace(campo.Text) ? (int?)null
                : (int.TryParse(campo.Text, out valor) ? valor : throw new InvalidOperationException("Revisá los valores numéricos del ejercicio."));
        }

        private static decimal? DecimalOpcional(TextBox campo)
        {
            decimal valor;
            return string.IsNullOrWhiteSpace(campo.Text) ? (decimal?)null
                : (decimal.TryParse(campo.Text, out valor) ? valor : throw new InvalidOperationException("Revisá el peso del ejercicio."));
        }
    }

    public class MisSociosFormBase : FormularioModuloBase
    {
        private readonly RutinaAsignacionLogica asignaciones = new RutinaAsignacionLogica();
        private readonly UsuarioSistema usuario;

        public MisSociosFormBase()
            : this(new UsuarioSistema
            {
                Nombre = "Entrenador",
                Apellido = "de diseño"
            })
        {
        }

        public MisSociosFormBase(UsuarioSistema usuario)
            : base("Mis socios", "Socios que tienen plantillas asignadas a este entrenador", Color.FromArgb(14, 116, 144))
        {
            this.usuario = usuario;
            AgregarBoton("Actualizar", delegate { Cargar(); }, true);
            Tabla.Columns.Add("Id", "Id"); Tabla.Columns[0].Visible = false;
            Tabla.Columns.Add("Socio", "Socio"); Tabla.Columns.Add("Rutinas", "Rutinas asignadas");
            AlCargarEnEjecucion(delegate { Cargar(); });
        }

        private void Cargar()
        {
            try
            {
                Tabla.Rows.Clear();
                foreach (var grupo in asignaciones.ListarPorEntrenador(usuario.IdUsuarioSistema)
                    .GroupBy(a => a.Membresia.IdSocio))
                {
                    var primera = grupo.First();
                    Tabla.Rows.Add(grupo.Key, primera.Membresia.Socio.Apellido + ", " + primera.Membresia.Socio.Nombre, grupo.Count());
                }
                Estado.Text = Tabla.Rows.Count + " socio(s) con rutinas asignadas";
            }
            catch (Exception ex) { MostrarError(ex); }
        }
    }
}
