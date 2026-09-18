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
        private ModoEditorEjercicio modoEditorEjercicio = ModoEditorEjercicio.Visualizacion;

        private enum ModoEditorEjercicio
        {
            Visualizacion,
            Nuevo,
            Edicion
        }

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
            lblTitulo.Text = "Rutina personalizada | Rutina para el socio seleccionado";
            lblDescripcion.Visible = false;
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
                lblTitulo.Text = "Gestionar rutinas | Catálogo y composición de rutinas";
                lblDescripcion.Visible = false;
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
                AplicarEstadoControles();
            }
            catch (Exception ex) { AyudaFormularioVisual.MostrarError(lblEstado, ex); }
        }

        private void tabla_SelectionChanged(object origen, EventArgs e)
        {
            try
            {
                if (tabla.CurrentRow == null || tabla.CurrentRow.Cells[0].Value == null)
                {
                    AplicarEstadoControles();
                    return;
                }
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
            finally { AplicarEstadoControles(); }
        }

        private void CargarDetalleDeRutina()
        {
            cargandoDetalle = true;
            try
            {
                tablaEjercicios.Rows.Clear();
                LimpiarDetalleEjercicio();
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
                EstablecerModoEditorEjercicio(ModoEditorEjercicio.Visualizacion);
            }
            finally
            {
                cargandoDetalle = false;
                AplicarEstadoControles();
            }
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

        private void LimpiarDetalleEjercicio()
        {
            ejercicio.SelectedIndex = -1;
            dia.SelectedIndex = -1;
            series.Clear();
            repeticiones.Clear();
            peso.Clear();
            descanso.Clear();
            orden.Clear();
        }

        private void tablaEjercicios_SelectionChanged(object origen, EventArgs e)
        {
            if (cargandoDetalle || tablaEjercicios.CurrentRow == null || !tablaEjercicios.CurrentRow.Selected)
            {
                AplicarEstadoControles();
                return;
            }
            var fila = tablaEjercicios.CurrentRow;
            if (fila.Cells[0].Value == null) return;
            var idSeleccionado = Convert.ToInt32(fila.Cells[0].Value);
            var detalle = ejerciciosRutina.ListarPorRutina(idRutina)
                .FirstOrDefault(item => item.IdRutinaEjercicio == idSeleccionado);
            if (detalle == null) return;

            idRutinaEjercicio = detalle.IdRutinaEjercicio;
            dia.SelectedIndex = detalle.DiaSemana.HasValue
                ? detalle.DiaSemana.Value - ValidacionesGimnasio.PrimerDiaRutina
                : -1;
            ejercicio.SelectedValue = detalle.IdEjercicio;
            orden.Text = detalle.Orden.ToString();
            series.Text = detalle.Series.HasValue ? detalle.Series.Value.ToString() : string.Empty;
            repeticiones.Text = detalle.Repeticiones.HasValue ? detalle.Repeticiones.Value.ToString() : string.Empty;
            peso.Text = detalle.Peso.HasValue ? detalle.Peso.Value.ToString("0.##") : string.Empty;
            descanso.Text = detalle.Descanso.ToString();
            EstablecerModoEditorEjercicio(ModoEditorEjercicio.Visualizacion);
            AplicarEstadoControles();
        }

        private void quitarEjercicio_Click(object origen, EventArgs e)
        {
            try
            {
                if (idRutinaEjercicio == 0) throw new InvalidOperationException("Selecciona un ejercicio de la rutina.");
                if (MessageBox.Show("¿Desea quitar este ejercicio de la rutina?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
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
            EstablecerModoEditorEjercicio(ModoEditorEjercicio.Visualizacion);
            idEntrenadorRutina = usuario.IdUsuarioSistema;
            estadoRutinaSeleccionada = true;
            nombre.Clear();
            descripcion.Clear();
            tabla.ClearSelection();
            tablaEjercicios.Rows.Clear();
            LimpiarDetalleEjercicio();
            guardarRutina.Text = "Guardar rutina";
            darDeBaja.Visible = false;
            reactivar.Visible = false;
            AplicarEstadoControles();
        }

        private void guardarRutina_Click(object origen, EventArgs e)
        {
            var eraNueva = idRutina == 0;
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
                }
                else
                {
                    rutinas.Modificar(rutina);
                    AyudaFormularioVisual.MostrarExito(lblEstado, "Rutina actualizada.");
                }
                Cargar();
                if (eraNueva)
                    AyudaFormularioVisual.MostrarExito(lblEstado, esRutinaPersonalizada
                        ? "Rutina personalizada creada y asignada al socio."
                        : "Rutina creada correctamente.", true);
            }
            catch (Exception ex) { AyudaFormularioVisual.MostrarError(lblEstado, ex, eraNueva); }
        }

        private void agregarEjercicio_Click(object origen, EventArgs e)
        {
            try
            {
                if (idRutina == 0)
                {
                    MessageBox.Show("Primero guarde la rutina.", "Rutinas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                idRutinaEjercicio = 0;
                ejercicio.SelectedIndex = -1;
                dia.SelectedIndex = 0;
                series.Clear();
                repeticiones.Clear();
                peso.Clear();
                descanso.Clear();
                orden.Clear();
                EstablecerModoEditorEjercicio(ModoEditorEjercicio.Nuevo);
                ejercicio.Focus();
            }
            catch (Exception ex) { AyudaFormularioVisual.MostrarError(lblEstado, ex); }
        }

        private void AplicarEstadoControles()
        {
            var hayRutina = idRutina > 0;
            var rutinaActiva = hayRutina && estadoRutinaSeleccionada;
            var hayEjercicio = idRutinaEjercicio > 0;
            var editandoEjercicio = modoEditorEjercicio != ModoEditorEjercicio.Visualizacion;
            guardarRutina.Enabled = true;
            actualizar.Enabled = hayRutina;
            darDeBaja.Enabled = rutinaActiva;
            reactivar.Enabled = hayRutina && !estadoRutinaSeleccionada;
            agregarEjercicio.Enabled = rutinaActiva && !editandoEjercicio;
            actualizarEjercicio.Enabled = rutinaActiva && hayEjercicio && !editandoEjercicio;
            quitarEjercicio.Enabled = rutinaActiva && hayEjercicio && !editandoEjercicio;
            tablaEjercicios.Enabled = hayRutina;
            contenedorFormulario.Enabled = rutinaActiva && editandoEjercicio;
            ejercicio.Enabled = rutinaActiva && editandoEjercicio;
            dia.Enabled = rutinaActiva && editandoEjercicio;
            series.ReadOnly = !editandoEjercicio;
            repeticiones.ReadOnly = !editandoEjercicio;
            peso.ReadOnly = !editandoEjercicio;
            descanso.ReadOnly = !editandoEjercicio;
            orden.ReadOnly = !editandoEjercicio;
            guardarEjercicio.Visible = rutinaActiva && editandoEjercicio;
            guardarEjercicio.Text = modoEditorEjercicio == ModoEditorEjercicio.Edicion ? "Guardar cambios" : "Agregar";
            cancelarEjercicio.Visible = rutinaActiva && editandoEjercicio;
            lblDetalleEjercicio.Text = modoEditorEjercicio == ModoEditorEjercicio.Nuevo
                ? "Detalle del ejercicio · Nuevo"
                : modoEditorEjercicio == ModoEditorEjercicio.Edicion
                    ? "Detalle del ejercicio · Editar"
                    : "Detalle del ejercicio";
        }

        private void EstablecerModoEditorEjercicio(ModoEditorEjercicio modo)
        {
            modoEditorEjercicio = modo;
            AplicarEstadoControles();
        }

        private void guardarEjercicio_Click(object origen, EventArgs e)
        {
            var eraNuevo = modoEditorEjercicio != ModoEditorEjercicio.Edicion;
            try
            {
                if (idRutina == 0)
                    throw new InvalidOperationException("Seleccione una rutina.");
                AyudaFormularioVisual.ValidarComboSeleccionado(ejercicio, "un ejercicio");
                var detalle = new RutinaEjercicio
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
                };
                if (modoEditorEjercicio == ModoEditorEjercicio.Edicion)
                {
                    ejerciciosRutina.Modificar(detalle);
                    AyudaFormularioVisual.MostrarExito(lblEstado, "Ejercicio actualizado en la rutina.");
                }
                else
                {
                    ejerciciosRutina.AgregarEjercicio(detalle);
                }
                CargarDetalleDeRutina();
                if (eraNuevo)
                    AyudaFormularioVisual.MostrarExito(lblEstado, "Ejercicio agregado a la rutina.", true);
            }
            catch (Exception ex) { AyudaFormularioVisual.MostrarError(lblEstado, ex, eraNuevo); }
        }

        private void cancelarEjercicio_Click(object origen, EventArgs e)
        {
            if (modoEditorEjercicio == ModoEditorEjercicio.Nuevo)
            {
                var fila = tablaEjercicios.CurrentRow;
                if (fila != null && fila.Cells[0].Value != null)
                {
                    idRutinaEjercicio = Convert.ToInt32(fila.Cells[0].Value);
                    tablaEjercicios_SelectionChanged(tablaEjercicios, EventArgs.Empty);
                }
                else
                {
                    idRutinaEjercicio = 0;
                    ejercicio.SelectedIndex = -1;
                    series.Clear();
                    repeticiones.Clear();
                    peso.Clear();
                    descanso.Clear();
                    orden.Clear();
                }
            }
            else if (idRutinaEjercicio > 0)
            {
                var fila = tablaEjercicios.CurrentRow;
                if (fila != null && fila.Cells[0].Value != null)
                {
                    tablaEjercicios_SelectionChanged(tablaEjercicios, EventArgs.Empty);
                }
            }
            EstablecerModoEditorEjercicio(ModoEditorEjercicio.Visualizacion);
        }

        private void actualizarEjercicio_Click(object origen, EventArgs e)
        {
            try
            {
                if (idRutina == 0)
                    throw new InvalidOperationException("Seleccione una rutina.");
                if (idRutinaEjercicio == 0)
                    throw new InvalidOperationException("Seleccione un ejercicio de la rutina.");
                EstablecerModoEditorEjercicio(ModoEditorEjercicio.Edicion);
                ejercicio.Focus();
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

        private void splitContenido_Resize(object origen, EventArgs e)
        {
            var distanciaMinima = splitContenido.Panel1MinSize;
            var distanciaMaxima = splitContenido.Width - splitContenido.Panel2MinSize;
            if (distanciaMaxima < distanciaMinima)
                return;
            var distanciaDeseada = (int)(splitContenido.Width * 0.32F);
            splitContenido.SplitterDistance = Math.Max(distanciaMinima, Math.Min(distanciaDeseada, distanciaMaxima));
        }

        private void actualizar_Click(object origen, EventArgs e)
        {
            Cargar();
            CargarDetalleDeRutina();
        }

        private void accionesRutina_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
