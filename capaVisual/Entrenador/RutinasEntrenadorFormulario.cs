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
    /* Presenta el catalogo de rutinas y sus ejercicios mediante eventos de Windows Forms. */
    [DesignerCategory("Form")]
    public partial class RutinasEntrenadorFormulario : Form
    {
        private readonly UsuarioSistema usuario;
        private readonly RutinaLogica rutinas = new RutinaLogica();
        private readonly RutinaEjercicioLogica ejerciciosRutina = new RutinaEjercicioLogica();
        private readonly EjercicioLogica ejercicios = new EjercicioLogica();
        private int idRutina;
        private int idRutinaEjercicio;
        private bool cargandoDetalle;
        private readonly int idSocioObjetivo;
        private readonly bool esRutinaPersonalizada;
        private readonly bool modoAdministrador;
        private readonly bool editarRutinaPersonalizada;
        private int idEntrenadorRutina;
        private bool estadoRutinaSeleccionada = true;

        public RutinasEntrenadorFormulario() : this(new UsuarioSistema { Nombre = "Entrenador", Apellido = "de diseno" }) { }

        public RutinasEntrenadorFormulario(UsuarioSistema usuario)
        {
            if (usuario == null) throw new ArgumentNullException("usuario");
            this.usuario = usuario;
            InitializeComponent();
        }

        public RutinasEntrenadorFormulario(UsuarioSistema usuario, int idSocio) : this(usuario)
        {
            if (idSocio <= 0) throw new ArgumentException("El socio seleccionado no es valido.", "idSocio");
            idSocioObjetivo = idSocio;
            esRutinaPersonalizada = true;
            Text = "SysGym | Rutina personalizada";
            lblTitulo.Text = "Rutina personalizada";
            lblDescripcion.Text = "Crea una rutina exclusiva para el socio seleccionado";
        }

        public RutinasEntrenadorFormulario(UsuarioSistema usuario, int idSocio, bool modoAdministrador) : this(usuario, idSocio)
        {
            this.modoAdministrador = modoAdministrador;
        }

        public RutinasEntrenadorFormulario(UsuarioSistema usuario, int idSocio, bool modoAdministrador, bool editarRutinaPersonalizada)
            : this(usuario, idSocio, modoAdministrador)
        {
            this.editarRutinaPersonalizada = editarRutinaPersonalizada;
        }

        public RutinasEntrenadorFormulario(UsuarioSistema usuario, bool modoAdministrador) : this(usuario)
        {
            this.modoAdministrador = modoAdministrador;
            if (modoAdministrador)
            {
                Text = "SysGym | Gestionar rutinas";
                lblTitulo.Text = "Gestionar rutinas";
                lblDescripcion.Text = "Crea, edita y administra el catalogo de rutinas";
            }
        }

        private void CargarEjercicios()
        {
            ejercicio.DataSource = ejercicios.ListarActivos();
            ejercicio.DisplayMember = "Nombre";
            ejercicio.ValueMember = "IdEjercicio";
        }

        private void Cargar()
        {
            try
            {
                tabla.Rows.Clear();
                var lista = modoAdministrador ? rutinas.ListarParaGestion() : rutinas.ListarPorEntrenador(usuario.IdUsuarioSistema);
                foreach (var rutina in lista)
                {
                    var asignados = rutina.Membresias == null ? 0 : rutina.Membresias.Count(m => m.Estado);
                    tabla.Rows.Add(rutina.IdRutina, rutina.Nombre,
                        rutina.Entrenador == null ? "-" : rutina.Entrenador.Nombre + " " + rutina.Entrenador.Apellido,
                        asignados, rutina.FechaCreacion.ToString("dd/MM/yyyy"), rutina.Estado ? "Activa" : "Inactiva");
                }
                lblEstado.Text = tabla.Rows.Count + " rutina(s) en el catalogo";
            }
            catch (Exception ex) { AyudaFormularioVisual.MostrarError(lblEstado, ex); }
        }

        private void tabla_SelectionChanged(object origen, EventArgs e)
        {
            try
            {
                if (tabla.CurrentRow == null || tabla.CurrentRow.Cells[0].Value == null) return;
                idRutina = Convert.ToInt32(tabla.CurrentRow.Cells[0].Value);
                var rutina = rutinas.ObtenerPorId(idRutina);
                if (rutina == null) return;
                idEntrenadorRutina = rutina.IdEntrenador;
                estadoRutinaSeleccionada = rutina.Estado;
                nombre.Text = rutina.Nombre;
                descripcion.Text = rutina.Descripcion ?? string.Empty;
                darDeBaja.Visible = rutina.Estado;
                reactivar.Visible = !rutina.Estado;
                CargarDetalleDeRutina();
            }
            catch (Exception ex) { AyudaFormularioVisual.MostrarError(lblEstado, ex); }
        }

        private void CargarDetalleDeRutina()
        {
            cargandoDetalle = true;
            try
            {
                tablaEjercicios.Rows.Clear();
                if (idRutina == 0) return;
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
            finally { cargandoDetalle = false; }
        }

        private void CargarRutinaPersonalizadaExistente()
        {
            var rutina = rutinas.ObtenerRutinaActivaPorSocio(idSocioObjetivo);
            if (rutina == null)
            {
                nuevaRutina_Click(null, EventArgs.Empty);
                return;
            }

            idRutina = rutina.IdRutina;
            idEntrenadorRutina = rutina.IdEntrenador;
            estadoRutinaSeleccionada = rutina.Estado;
            nombre.Text = rutina.Nombre;
            descripcion.Text = rutina.Descripcion ?? string.Empty;
            darDeBaja.Visible = rutina.Estado;
            reactivar.Visible = !rutina.Estado;
            guardarRutina.Text = "Actualizar rutina";
            CargarDetalleDeRutina();
        }

        private void tablaEjercicios_SelectionChanged(object origen, EventArgs e)
        {
            if (cargandoDetalle || tablaEjercicios.CurrentRow == null || !tablaEjercicios.CurrentRow.Selected) return;
            var fila = tablaEjercicios.CurrentRow;
            if (fila.Cells[0].Value == null) return;
            idRutinaEjercicio = Convert.ToInt32(fila.Cells[0].Value);
            var nombreDia = Convert.ToString(fila.Cells[1].Value);
            var indiceDia = dia.Items.IndexOf(nombreDia);
            if (indiceDia >= 0) dia.SelectedIndex = indiceDia;
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
            series.Text = Convert.ToString(fila.Cells[4].Value) == "-" ? string.Empty : Convert.ToString(fila.Cells[4].Value);
            repeticiones.Text = Convert.ToString(fila.Cells[5].Value) == "-" ? string.Empty : Convert.ToString(fila.Cells[5].Value);
            peso.Text = Convert.ToString(fila.Cells[6].Value) == "-" ? string.Empty : Convert.ToString(fila.Cells[6].Value);
            descanso.Text = Convert.ToString(fila.Cells[7].Value).TrimEnd('s');
        }

        private void quitarEjercicio_Click(object origen, EventArgs e)
        {
            try
            {
                if (idRutinaEjercicio == 0) throw new InvalidOperationException("Selecciona un ejercicio de la rutina.");
                if (MessageBox.Show("¿Quitar el ejercicio seleccionado de la rutina?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;
                ejerciciosRutina.Quitar(idRutinaEjercicio);
                CargarDetalleDeRutina();
                AyudaFormularioVisual.MostrarExito(lblEstado, "Ejercicio quitado de la rutina.");
            }
            catch (Exception ex) { AyudaFormularioVisual.MostrarError(lblEstado, ex); }
        }

        private int? DiaSeleccionado()
        {
            return dia.SelectedIndex < 0 ? (int?)null : dia.SelectedIndex + ValidacionesGimnasio.PrimerDiaRutina;
        }

        private void nuevaRutina_Click(object origen, EventArgs e)
        {
            idRutina = 0;
            idRutinaEjercicio = 0;
            idEntrenadorRutina = usuario.IdUsuarioSistema;
            estadoRutinaSeleccionada = true;
            nombre.Clear();
            descripcion.Clear();
            tabla.ClearSelection();
            tablaEjercicios.Rows.Clear();
            guardarRutina.Text = "Guardar rutina";
            darDeBaja.Visible = false;
            reactivar.Visible = false;
        }

        private void guardarRutina_Click(object origen, EventArgs e)
        {
            try
            {
                var rutina = new Rutina
                {
                    IdRutina = idRutina,
                    Nombre = nombre.Text.Trim(),
                    Descripcion = descripcion.Text.Trim(),
                    IdEntrenador = idRutina == 0 || !modoAdministrador ? usuario.IdUsuarioSistema : idEntrenadorRutina,
                    FechaCreacion = DateTime.Now,
                    Estado = idRutina == 0 || estadoRutinaSeleccionada
                };
                if (idRutina == 0)
                {
                    if (esRutinaPersonalizada)
                    {
                        var membresia = rutinas.ObtenerMembresiaActivaParaSocio(idSocioObjetivo);
                        if (membresia == null) throw new InvalidOperationException("El socio no posee una membresia activa.");
                        rutinas.CrearPersonalizada(rutina, membresia.IdMembresia);
                    }
                    else
                    {
                        rutinas.Crear(rutina);
                    }
                    idRutina = rutina.IdRutina;
                    AyudaFormularioVisual.MostrarExito(lblEstado, esRutinaPersonalizada
                        ? "Rutina personalizada creada y asignada al socio."
                        : "Rutina creada. Ahora podes agregarle ejercicios.");
                }
                else
                {
                    rutinas.Modificar(rutina);
                    AyudaFormularioVisual.MostrarExito(lblEstado, "Rutina actualizada.");
                }
                Cargar();
            }
            catch (Exception ex) { AyudaFormularioVisual.MostrarError(lblEstado, ex); }
        }

        private void agregarEjercicio_Click(object origen, EventArgs e)
        {
            try
            {
                if (idRutina == 0)
                {
                    guardarRutina_Click(null, EventArgs.Empty);
                    if (idRutina == 0) return;
                }

                AyudaFormularioVisual.ValidarComboSeleccionado(ejercicio, "un ejercicio");
                ejerciciosRutina.AgregarEjercicio(new RutinaEjercicio
                {
                    IdRutina = idRutina,
                    IdEjercicio = Convert.ToInt32(ejercicio.SelectedValue),
                    Series = EnteroObligatorio(series, "series"),
                    Repeticiones = EnteroObligatorio(repeticiones, "repeticiones"),
                    Peso = DecimalOpcional(peso),
                    Descanso = EnteroOpcional(descanso) ?? 0,
                    Orden = EnteroObligatorio(orden, "orden"),
                    DiaSemana = DiaSeleccionado()
                });
                CargarDetalleDeRutina();
                AyudaFormularioVisual.MostrarExito(lblEstado, "Ejercicio agregado a la rutina.");
            }
            catch (Exception ex) { AyudaFormularioVisual.MostrarError(lblEstado, ex); }
        }

        private void actualizarEjercicio_Click(object origen, EventArgs e)
        {
            try
            {
                if (idRutina == 0)
                    throw new InvalidOperationException("Seleccione una rutina.");
                if (idRutinaEjercicio == 0)
                    throw new InvalidOperationException("Seleccione un ejercicio de la rutina.");
                AyudaFormularioVisual.ValidarComboSeleccionado(ejercicio, "un ejercicio");
                ejerciciosRutina.Modificar(new RutinaEjercicio
                {
                    IdRutinaEjercicio = idRutinaEjercicio,
                    IdRutina = idRutina,
                    IdEjercicio = Convert.ToInt32(ejercicio.SelectedValue),
                    Series = EnteroObligatorio(series, "series"),
                    Repeticiones = EnteroObligatorio(repeticiones, "repeticiones"),
                    Peso = DecimalOpcional(peso),
                    Descanso = EnteroOpcional(descanso) ?? 0,
                    Orden = EnteroObligatorio(orden, "orden"),
                    DiaSemana = DiaSeleccionado(),
                    Estado = true
                });
                CargarDetalleDeRutina();
                AyudaFormularioVisual.MostrarExito(lblEstado, "Ejercicio actualizado en la rutina.");
            }
            catch (Exception ex) { AyudaFormularioVisual.MostrarError(lblEstado, ex); }
        }

        private void darDeBaja_Click(object origen, EventArgs e)
        {
            try
            {
                if (idRutina == 0) throw new InvalidOperationException("Selecciona una rutina.");
                if (MessageBox.Show("¿Dar de baja la rutina seleccionada?", "Confirmar baja", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;
                rutinas.DarDeBaja(idRutina);
                Cargar();
                nuevaRutina_Click(null, EventArgs.Empty);
                AyudaFormularioVisual.MostrarExito(lblEstado, "Rutina dada de baja.");
            }
            catch (Exception ex) { AyudaFormularioVisual.MostrarError(lblEstado, ex); }
        }

        private void reactivar_Click(object origen, EventArgs e)
        {
            try
            {
                if (idRutina == 0) throw new InvalidOperationException("Selecciona una rutina.");
                rutinas.Reactivar(idRutina);
                Cargar();
                AyudaFormularioVisual.MostrarExito(lblEstado, "Rutina reactivada.");
            }
            catch (Exception ex) { AyudaFormularioVisual.MostrarError(lblEstado, ex); }
        }

        private static int EnteroObligatorio(TextBox campo, string nombre)
        {
            if (string.IsNullOrWhiteSpace(campo.Text))
                throw new InvalidOperationException("El campo " + nombre + " es obligatorio.");
            return AyudaFormularioVisual.Entero(campo, nombre);
        }

        private static int? EnteroOpcional(TextBox campo)
        {
            int valor;
            return string.IsNullOrWhiteSpace(campo.Text) ? (int?)null : (int.TryParse(campo.Text, out valor) ? valor : throw new InvalidOperationException("Revisa los valores numericos del ejercicio."));
        }

        private static decimal? DecimalOpcional(TextBox campo)
        {
            decimal valor;
            return string.IsNullOrWhiteSpace(campo.Text) ? (decimal?)null : (decimal.TryParse(campo.Text, out valor) ? valor : throw new InvalidOperationException("Revisa el peso del ejercicio."));
        }

        private void series_KeyPress(object origen, KeyPressEventArgs e) { AyudaFormularioVisual.ValidarEntradaEntero(e); }
        private void repeticiones_KeyPress(object origen, KeyPressEventArgs e) { AyudaFormularioVisual.ValidarEntradaEntero(e); }
        private void peso_KeyPress(object origen, KeyPressEventArgs e) { AyudaFormularioVisual.ValidarEntradaDecimal(peso, e); }
        private void descanso_KeyPress(object origen, KeyPressEventArgs e) { AyudaFormularioVisual.ValidarEntradaEntero(e); }
        private void orden_KeyPress(object origen, KeyPressEventArgs e) { AyudaFormularioVisual.ValidarEntradaEntero(e); }

        private void RutinasEntrenadorFormulario_Load(object origen, EventArgs e)
        {
            if (AyudaFormularioVisual.EnModoDisenio(this)) return;
            try
            {
                CargarEjercicios();
                Cargar();
                if (editarRutinaPersonalizada) CargarRutinaPersonalizadaExistente();
                else nuevaRutina_Click(null, EventArgs.Empty);
            }
            catch (Exception ex) { AyudaFormularioVisual.MostrarError(lblEstado, ex); }
        }

        private void btnVolver_Click(object origen, EventArgs e) { Close(); }

        private void actualizar_Click(object origen, EventArgs e)
        {
            Cargar();
            CargarDetalleDeRutina();
        }
    }
}
