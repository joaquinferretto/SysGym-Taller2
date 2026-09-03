using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaLogica;

namespace exxen2._0.capaVisual.Compartido
{
    [System.ComponentModel.DesignerCategory("Code")]
    [System.ComponentModel.DesignTimeVisible(false)]
    public abstract class GestionSociosFormBase : FormularioModuloBase
    {
        private readonly SocioLogica logica = new SocioLogica();
        private readonly bool permitirEdicion;
        private readonly VistaListadoDetalle vista;
        private readonly TextBox nombre;
        private readonly TextBox apellido;
        private readonly TextBox dni;
        private readonly DateTimePicker fechaNacimiento;
        private readonly TextBox peso;
        private readonly TextBox altura;
        private readonly Button guardar;
        private readonly Button actualizar;
        private readonly Button darDeBaja;
        private readonly Button reactivar;
        private readonly ComboBox filtroEstado;
        private List<Socio> sociosCargados = new List<Socio>();
        private int idSeleccionado;
        private bool cargandoTabla;
        private bool estadoSeleccionado = true;

        public GestionSociosFormBase()
            : this(Color.FromArgb(79, 70, 229))
        {
        }

        public GestionSociosFormBase(Color colorPrimario, bool permitirEdicion = true)
            : base("Socios", "Alta, actualización, baja lógica y consulta de IMC", colorPrimario)
        {
            this.permitirEdicion = permitirEdicion;
            vista = CrearVistaListadoDetalle("Socios", "Buscar por nombre o DNI",
                "+ Nuevo socio", NuevoSocio);
            nombre = vista.AgregarCampo("Nombre", CrearTexto());
            apellido = vista.AgregarCampo("Apellido", CrearTexto());
            dni = vista.AgregarCampo("DNI", CrearTexto());
            fechaNacimiento = vista.AgregarCampo("Fecha de nacimiento", new DateTimePicker
            {
                Format = DateTimePickerFormat.Short,
                ShowCheckBox = true
            });
            peso = vista.AgregarCampo("Peso (kg)", CrearTexto());
            altura = vista.AgregarCampo("Altura (m) · Ejemplo: 1,80", CrearTexto());
            ConfigurarEntradaDecimal(peso);
            ConfigurarEntradaDecimal(altura);
            guardar = vista.AgregarAccion("Guardar", GuardarNuevo, true);
            actualizar = vista.AgregarAccion("Actualizar", Actualizar);
            darDeBaja = vista.AgregarAccion("Dar de baja", DarDeBaja, false, true);
            reactivar = vista.AgregarAccion("Reactivar", Reactivar);
            vista.AgregarAccion("Calcular IMC", CalcularImc);

            filtroEstado = vista.AgregarFiltroListado("Estado", new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList
            });
            filtroEstado.Items.AddRange(new object[] { "Todos", "Activos", "Inactivos" });
            filtroEstado.SelectedIndex = 0;

            Tabla.Columns.Add("Id", "Id"); Tabla.Columns[0].Visible = false;
            Tabla.Columns.Add("Nombre", "Nombre");
            Tabla.Columns.Add("DNI", "DNI");
            Tabla.Columns.Add("Nacimiento", "Nacimiento");
            Tabla.Columns.Add("Estado", "Estado");
            Tabla.Columns[1].FillWeight = 150;
            Tabla.Columns[2].FillWeight = 95;
            Tabla.Columns[3].FillWeight = 95;
            Tabla.Columns[4].FillWeight = 70;
            Tabla.SelectionChanged += Seleccionar;
            vista.Buscador.TextChanged += delegate { AplicarFiltro(); };
            filtroEstado.SelectedIndexChanged += delegate { AplicarFiltro(); };
            AlCargarEnEjecucion(delegate { Cargar(); NuevoSocio(null, EventArgs.Empty); });

            if (!permitirEdicion)
            {
                vista.BotonNuevo.Visible = false;
                guardar.Visible = false;
                actualizar.Visible = false;
                darDeBaja.Visible = false;
                nombre.ReadOnly = true; apellido.ReadOnly = true; dni.ReadOnly = true;
                peso.ReadOnly = true; altura.ReadOnly = true; fechaNacimiento.Enabled = false;
            }
        }

        private static TextBox CrearTexto()
        {
            return new TextBox { BorderStyle = BorderStyle.FixedSingle };
        }

        private void Cargar()
        {
            try
            {
                sociosCargados = logica.ListarParaGestion();
                AplicarFiltro();
            }
            catch (Exception ex) { MostrarError(ex); }
        }

        private void AplicarFiltro()
        {
            var criterio = vista.Buscador.Text.Trim();
            var estadoElegido = Convert.ToString(filtroEstado.SelectedItem);
            var filtrados = sociosCargados.AsEnumerable();
            if (estadoElegido == "Activos") filtrados = filtrados.Where(s => s.Estado);
            else if (estadoElegido == "Inactivos") filtrados = filtrados.Where(s => !s.Estado);
            if (!string.IsNullOrWhiteSpace(criterio))
            {
                filtrados = filtrados.Where(s =>
                    Contiene(s.Nombre + " " + s.Apellido, criterio) || Contiene(s.DNI, criterio));
            }

            cargandoTabla = true;
            Tabla.Rows.Clear();
            foreach (var socio in filtrados)
                Tabla.Rows.Add(socio.IdSocio, socio.Apellido + ", " + socio.Nombre, socio.DNI,
                    socio.FechaNacimiento.HasValue ? socio.FechaNacimiento.Value.ToString("dd/MM/yyyy") : "-",
                    socio.Estado ? "Activo" : "Inactivo");
            Tabla.ClearSelection();
            cargandoTabla = false;
            Estado.Text = Tabla.Rows.Count + " socio(s) encontrado(s)";
        }

        private static bool Contiene(string valor, string criterio)
        {
            return !string.IsNullOrEmpty(valor)
                && valor.IndexOf(criterio, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private void Seleccionar(object sender, EventArgs e)
        {
            if (cargandoTabla || Tabla.CurrentRow == null || !Tabla.CurrentRow.Selected) return;
            try
            {
                idSeleccionado = Convert.ToInt32(Tabla.CurrentRow.Cells[0].Value);
                var socio = logica.ObtenerPorId(idSeleccionado);
                if (socio == null) return;
                estadoSeleccionado = socio.Estado;
                nombre.Text = socio.Nombre;
                apellido.Text = socio.Apellido;
                dni.Text = socio.DNI;
                fechaNacimiento.Checked = socio.FechaNacimiento.HasValue;
                if (socio.FechaNacimiento.HasValue) fechaNacimiento.Value = socio.FechaNacimiento.Value;
                peso.Text = socio.Peso.HasValue ? socio.Peso.Value.ToString("0.##") : string.Empty;
                altura.Text = socio.Altura.HasValue ? socio.Altura.Value.ToString("0.00") : string.Empty;
                EstablecerModo(false, socio.Estado);
                vista.TituloDetalle.Text = "Editar socio · Estado: " + (socio.Estado ? "Activo" : "Inactivo");
            }
            catch (Exception ex) { MostrarError(ex); }
        }

        private void NuevoSocio(object sender, EventArgs e)
        {
            idSeleccionado = 0;
            estadoSeleccionado = true;
            nombre.Clear(); apellido.Clear(); dni.Clear(); peso.Clear(); altura.Clear();
            fechaNacimiento.Value = DateTime.Today.AddYears(-18);
            fechaNacimiento.Checked = false;
            Tabla.ClearSelection();
            EstablecerModo(true, true);
            if (permitirEdicion) nombre.Focus();
        }

        private void EstablecerModo(bool nuevo, bool activo)
        {
            vista.TituloDetalle.Text = nuevo ? "Nuevo socio · Estado inicial: Activo" : "Editar socio";
            guardar.Enabled = permitirEdicion && nuevo;
            actualizar.Enabled = permitirEdicion && !nuevo;
            darDeBaja.Enabled = permitirEdicion && !nuevo && activo;
            reactivar.Enabled = permitirEdicion && !nuevo && !activo;
        }

        private Socio LeerSocio()
        {
            return new Socio
            {
                IdSocio = idSeleccionado,
                Nombre = nombre.Text.Trim(),
                Apellido = apellido.Text.Trim(),
                DNI = dni.Text.Trim(),
                FechaNacimiento = fechaNacimiento.Checked ? (DateTime?)fechaNacimiento.Value.Date : null,
                Peso = string.IsNullOrWhiteSpace(peso.Text) ? (decimal?)null : DecimalPositivo(peso, "peso"),
                Altura = string.IsNullOrWhiteSpace(altura.Text) ? (decimal?)null : DecimalPositivo(altura, "altura"),
                Estado = estadoSeleccionado
            };
        }

        private void GuardarNuevo(object sender, EventArgs e)
        {
            try
            {
                if (!permitirEdicion || idSeleccionado != 0) return;
                logica.Crear(LeerSocio());
                Cargar(); NuevoSocio(null, EventArgs.Empty);
                MostrarExito("Socio creado correctamente.");
            }
            catch (Exception ex) { MostrarError(ex); }
        }

        private void Actualizar(object sender, EventArgs e)
        {
            try
            {
                if (!permitirEdicion || idSeleccionado == 0) throw new InvalidOperationException("Selecciona un socio.");
                logica.Modificar(LeerSocio());
                Cargar(); NuevoSocio(null, EventArgs.Empty);
                MostrarExito("Socio actualizado correctamente.");
            }
            catch (Exception ex) { MostrarError(ex); }
        }

        private void DarDeBaja(object sender, EventArgs e)
        {
            try
            {
                if (!permitirEdicion || idSeleccionado == 0) throw new InvalidOperationException("Selecciona un socio.");
                if (MessageBox.Show("¿Dar de baja al socio seleccionado?", "Confirmar baja",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
                logica.DarDeBaja(idSeleccionado);
                Cargar(); NuevoSocio(null, EventArgs.Empty);
                MostrarExito("Socio dado de baja.");
            }
            catch (Exception ex) { MostrarError(ex); }
        }

        private void Reactivar(object sender, EventArgs e)
        {
            try
            {
                if (!permitirEdicion || idSeleccionado == 0)
                    throw new InvalidOperationException("Selecciona un socio.");
                logica.Reactivar(idSeleccionado);
                Cargar(); NuevoSocio(null, EventArgs.Empty);
                MostrarExito("Socio reactivado correctamente.");
            }
            catch (Exception ex) { MostrarError(ex); }
        }

        private void CalcularImc(object sender, EventArgs e)
        {
            try
            {
                var socio = new Socio
                {
                    Peso = DecimalPositivo(peso, "peso"),
                    Altura = DecimalPositivo(altura, "altura")
                };
                MessageBox.Show("IMC: " + logica.CalcularIMC(socio).ToString("0.00"),
                    "Índice de masa corporal", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MostrarError(ex); }
        }
    }

    [System.ComponentModel.DesignerCategory("Code")]
    [System.ComponentModel.DesignTimeVisible(false)]
    public abstract class GestionEjerciciosFormBase : FormularioModuloBase
    {
        private readonly EjercicioLogica logica = new EjercicioLogica();
        private readonly TextBox nombre;
        private readonly TextBox descripcion;
        private readonly ComboBox filtroEstado;
        private readonly Button darDeBaja;
        private readonly Button reactivar;
        private int idSeleccionado;
        private bool estadoSeleccionado = true;

        public GestionEjerciciosFormBase()
            : this(Color.FromArgb(79, 70, 229))
        {
        }

        public GestionEjerciciosFormBase(Color colorPrimario)
            : base("Ejercicios", "Catálogo de ejercicios disponibles para las rutinas", colorPrimario)
        {
            var grupo = CrearGrupo("Datos del ejercicio", 1060, 86, new Point(20, 160));
            var campos = new TableLayoutPanel
            {
                BackColor = Color.Transparent,
                ColumnCount = 4,
                Dock = DockStyle.Fill,
                Padding = new Padding(4, 8, 4, 4),
                RowCount = 1
            };
            campos.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 86F));
            campos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            campos.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            campos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            campos.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            nombre = CrearCampoEjercicio();
            descripcion = CrearCampoEjercicio();
            campos.Controls.Add(CrearEtiquetaEjercicio("Nombre"), 0, 0);
            campos.Controls.Add(nombre, 1, 0);
            campos.Controls.Add(CrearEtiquetaEjercicio("Descripción"), 2, 0);
            campos.Controls.Add(descripcion, 3, 0);
            grupo.Controls.Add(campos);
            AgregarPanelFormulario(grupo);
            grupo.SendToBack();
            AgregarBoton("Guardar", Guardar, true); darDeBaja = AgregarBoton("Dar de baja", DarDeBaja); reactivar = AgregarBoton("Reactivar", Reactivar); AgregarBoton("Actualizar", delegate { Cargar(); });
            filtroEstado = AgregarFiltroListado("Estado", new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList });
            filtroEstado.Items.AddRange(new object[] { "Todos", "Activos", "Inactivos" }); filtroEstado.SelectedIndex = 0;
            filtroEstado.SelectedIndexChanged += delegate { Cargar(); };
            Tabla.Columns.Add("Id", "Id"); Tabla.Columns[0].Visible = false;
            Tabla.Columns.Add("Nombre", "Nombre"); Tabla.Columns.Add("Descripción", "Descripción"); Tabla.Columns.Add("Estado", "Estado");
            Tabla.Columns[1].FillWeight = 32;
            Tabla.Columns[2].FillWeight = 53;
            Tabla.Columns[3].FillWeight = 15;
            Tabla.SelectionChanged += Seleccionar;
            AlCargarEnEjecucion(delegate { Cargar(); });
        }

        private static Label CrearEtiquetaEjercicio(string texto)
        {
            return new Label
            {
                Anchor = AnchorStyles.Left,
                AutoSize = true,
                Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 41, 59),
                Margin = new Padding(6, 0, 8, 0),
                Text = texto
            };
        }

        private static TextBox CrearCampoEjercicio()
        {
            return new TextBox
            {
                Anchor = AnchorStyles.Left | AnchorStyles.Right,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 9.5F),
                Margin = new Padding(0, 0, 18, 0)
            };
        }

        private void Cargar()
        {
            try
            {
                var ejercicios = logica.ListarParaGestion();
                var estadoElegido = Convert.ToString(filtroEstado.SelectedItem);
                if (estadoElegido == "Activos") ejercicios = ejercicios.Where(e => e.Estado).ToList();
                else if (estadoElegido == "Inactivos") ejercicios = ejercicios.Where(e => !e.Estado).ToList();
                Tabla.Rows.Clear();
                foreach (var ejercicio in ejercicios) Tabla.Rows.Add(ejercicio.IdEjercicio, ejercicio.Nombre, ejercicio.Descripcion, ejercicio.Estado ? "Activo" : "Inactivo");
                Estado.Text = Tabla.Rows.Count + " ejercicio(s) encontrado(s)";
            }
            catch (Exception ex) { MostrarError(ex); }
        }
        private void Seleccionar(object sender, EventArgs e)
        {
            if (Tabla.CurrentRow == null) return;
            idSeleccionado = Convert.ToInt32(Tabla.CurrentRow.Cells[0].Value);
            nombre.Text = Convert.ToString(Tabla.CurrentRow.Cells[1].Value);
            descripcion.Text = Convert.ToString(Tabla.CurrentRow.Cells[2].Value);
            estadoSeleccionado = Convert.ToString(Tabla.CurrentRow.Cells[3].Value) == "Activo";
            darDeBaja.Enabled = estadoSeleccionado;
            reactivar.Enabled = !estadoSeleccionado;
        }
        private void Guardar(object sender, EventArgs e)
        {
            try
            {
                var ejercicio = new Ejercicio { IdEjercicio = idSeleccionado, Nombre = nombre.Text.Trim(), Descripcion = descripcion.Text.Trim(), Estado = estadoSeleccionado };
                if (idSeleccionado == 0) logica.Crear(ejercicio); else logica.Modificar(ejercicio);
                idSeleccionado = 0; nombre.Clear(); descripcion.Clear(); Cargar(); MostrarExito("Ejercicio guardado correctamente.");
            }
            catch (Exception ex) { MostrarError(ex); }
        }
        private void DarDeBaja(object sender, EventArgs e)
        {
            try { if (idSeleccionado == 0) throw new InvalidOperationException("Selecciona un ejercicio."); logica.DarDeBaja(idSeleccionado); idSeleccionado = 0; Cargar(); MostrarExito("Ejercicio dado de baja."); }
            catch (Exception ex) { MostrarError(ex); }
        }

        private void Reactivar(object sender, EventArgs e)
        {
            try { if (idSeleccionado == 0) throw new InvalidOperationException("Selecciona un ejercicio."); logica.Reactivar(idSeleccionado); idSeleccionado = 0; Cargar(); MostrarExito("Ejercicio reactivado."); }
            catch (Exception ex) { MostrarError(ex); }
        }
    }

    [System.ComponentModel.DesignerCategory("Code")]
    [System.ComponentModel.DesignTimeVisible(false)]
    public abstract class GestionAsistenciasFormBase : FormularioModuloBase
    {
        private readonly AsistenciaLogica logica = new AsistenciaLogica();
        private readonly SocioLogica socios = new SocioLogica();
        private readonly ComboBox socio;
        private readonly DateTimePicker fecha;
        private readonly ComboBox filtroEstado;
        private readonly Button darDeBaja;
        private readonly Button reactivar;
        private int idSeleccionado;
        private bool estadoSeleccionado = true;

        public GestionAsistenciasFormBase()
            : this(Color.FromArgb(79, 70, 229))
        {
        }

        public GestionAsistenciasFormBase(Color colorPrimario)
            : base("Asistencias", "Registro y consulta de ingresos al gimnasio", colorPrimario)
        {
            var grupo = CrearGrupo("Registrar asistencia", 1060, 86, new Point(20, 160));
            socio = Selector(grupo, "Socio", 15, 25, 300);
            fecha = new DateTimePicker { Format = DateTimePickerFormat.Custom, CustomFormat = "dd/MM/yyyy HH:mm", Location = new Point(440, 25), Width = 180, Value = DateTime.Now };
            grupo.Controls.Add(new Label { AutoSize = true, Location = new Point(350, 29), Text = "Fecha" }); grupo.Controls.Add(fecha);
            AgregarPanelFormulario(grupo);
            AgregarBoton("Registrar", Registrar, true); darDeBaja = AgregarBoton("Dar de baja", DarDeBaja); reactivar = AgregarBoton("Reactivar", Reactivar); AgregarBoton("Actualizar", delegate { Cargar(); });
            filtroEstado = AgregarFiltroListado("Estado", new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList });
            filtroEstado.Items.AddRange(new object[] { "Todos", "Activos", "Inactivos" }); filtroEstado.SelectedIndex = 0;
            filtroEstado.SelectedIndexChanged += delegate { Cargar(); };
            Tabla.Columns.Add("Id", "Id"); Tabla.Columns[0].Visible = false;
            Tabla.Columns.Add("Fecha", "Fecha"); Tabla.Columns.Add("Socio", "Socio"); Tabla.Columns.Add("Descripción", "Descripción");
            Tabla.Columns.Add("Estado", "Estado");
            Tabla.SelectionChanged += delegate
            {
                if (Tabla.CurrentRow == null) return;
                idSeleccionado = Convert.ToInt32(Tabla.CurrentRow.Cells[0].Value);
                estadoSeleccionado = Convert.ToString(Tabla.CurrentRow.Cells[4].Value) == "Activo";
                darDeBaja.Enabled = estadoSeleccionado;
                reactivar.Enabled = !estadoSeleccionado;
            };
            AlCargarEnEjecucion(delegate { CargarSocios(); Cargar(); });
        }

        private void CargarSocios() { socio.DataSource = socios.ListarActivos(); socio.DisplayMember = "Apellido"; socio.ValueMember = "IdSocio"; }
        private void Cargar()
        {
            try
            {
                var asistencias = logica.ListarPorFechaParaGestion(fecha.Value);
                var estadoElegido = Convert.ToString(filtroEstado.SelectedItem);
                if (estadoElegido == "Activos") asistencias = asistencias.Where(a => a.Estado).ToList();
                else if (estadoElegido == "Inactivos") asistencias = asistencias.Where(a => !a.Estado).ToList();
                Tabla.Rows.Clear();
                foreach (var asistencia in asistencias)
                    Tabla.Rows.Add(asistencia.IdAsistencia, asistencia.Fecha.ToString("dd/MM/yyyy HH:mm"),
                        asistencia.Socio == null ? asistencia.IdSocio.ToString() : asistencia.Socio.Apellido + ", " + asistencia.Socio.Nombre,
                        asistencia.Descripcion, asistencia.Estado ? "Activo" : "Inactivo");
                Estado.Text = Tabla.Rows.Count + " asistencia(s) para la fecha seleccionada";
            }
            catch (Exception ex) { MostrarError(ex); }
        }
        private void Registrar(object sender, EventArgs e)
        {
            try
            {
                logica.Registrar(new Asistencia { IdSocio = Convert.ToInt32(socio.SelectedValue), Fecha = fecha.Value, Descripcion = "Ingreso registrado" });
                Cargar(); MostrarExito("Asistencia registrada.");
            }
            catch (Exception ex) { MostrarError(ex); }
        }
        private void DarDeBaja(object sender, EventArgs e)
        {
            try { if (idSeleccionado == 0) throw new InvalidOperationException("Selecciona una asistencia."); logica.DarDeBaja(idSeleccionado); Cargar(); MostrarExito("Asistencia anulada."); }
            catch (Exception ex) { MostrarError(ex); }
        }

        private void Reactivar(object sender, EventArgs e)
        {
            try { if (idSeleccionado == 0) throw new InvalidOperationException("Selecciona una asistencia."); logica.Reactivar(idSeleccionado); Cargar(); MostrarExito("Asistencia reactivada."); }
            catch (Exception ex) { MostrarError(ex); }
        }
    }
}
