using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Entrenador
{
    partial class RutinasEntrenadorFormulario
    {
        private IContainer components;
        private Panel panelEncabezado;
        private Label lblTitulo;
        private Label lblDescripcion;
        private Button btnVolver;
        private Panel barraAcciones;
        private Label lblEstado;
        private Panel panelContenido;
        private SplitContainer splitContenido;
        private TableLayoutPanel layoutDetalle;
        private Panel panelListado;
        private Label lblListadoTitulo;
        private Panel panelRutina;
        private Label lblDetalleTitulo;
        private TableLayoutPanel contenedorRutina;
        private FlowLayoutPanel accionesRutina;
        private Panel panelEjercicios;
        private Label lblEjerciciosTitulo;
        private FlowLayoutPanel accionesEjercicios;
        private Panel panelFormulario;
        private Label lblDetalleEjercicio;
        private TableLayoutPanel contenedorFormulario;
        private FlowLayoutPanel accionesFormulario;
        private Label lblNombre;
        private Label lblDescripcionRutina;
        private Label lblEjercicio;
        private Label lblSeries;
        private Label lblRepeticiones;
        private Label lblPeso;
        private Label lblDescanso;
        private Label lblOrden;
        private Label lblDia;
        private DataGridView tabla;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colRutina;
        private DataGridViewTextBoxColumn colCreador;
        private DataGridViewTextBoxColumn colAsignados;
        private DataGridViewTextBoxColumn colCreacion;
        private DataGridViewTextBoxColumn colEstado;
        private ComboBox dia;
        private ComboBox ejercicio;
        private TextBox nombre;
        private TextBox descripcion;
        private TextBox series;
        private TextBox repeticiones;
        private TextBox peso;
        private TextBox descanso;
        private TextBox orden;
        private Button nuevaRutina;
        private Button guardarRutina;
        private Button actualizar;
        private Button agregarEjercicio;
        private Button actualizarEjercicio;
        private Button darDeBaja;
        private Button reactivar;
        private Button quitarEjercicio;
        private Button guardarEjercicio;
        private Button cancelarEjercicio;
        private DataGridView tablaEjercicios;
        private DataGridViewTextBoxColumn colDetalleId;
        private DataGridViewTextBoxColumn colDetalleDia;
        private DataGridViewTextBoxColumn colDetalleOrden;
        private DataGridViewTextBoxColumn colDetalleEjercicio;
        private DataGridViewTextBoxColumn colDetalleSeries;
        private DataGridViewTextBoxColumn colDetalleRepeticiones;
        private DataGridViewTextBoxColumn colDetallePeso;
        private DataGridViewTextBoxColumn colDetalleDescanso;

        protected override void Dispose(bool liberarRecursos)
        {
            if (liberarRecursos && components != null) components.Dispose();
            base.Dispose(liberarRecursos);
        }

        private void InitializeComponent()
        {
            components = new Container();
            panelEncabezado = new Panel(); lblTitulo = new Label(); lblDescripcion = new Label(); btnVolver = new Button();
            barraAcciones = new Panel(); lblEstado = new Label(); panelContenido = new Panel(); splitContenido = new SplitContainer(); layoutDetalle = new TableLayoutPanel(); panelListado = new Panel(); lblListadoTitulo = new Label();
            panelRutina = new Panel(); lblDetalleTitulo = new Label(); contenedorRutina = new TableLayoutPanel(); accionesRutina = new FlowLayoutPanel();
            panelEjercicios = new Panel(); lblEjerciciosTitulo = new Label(); accionesEjercicios = new FlowLayoutPanel(); panelFormulario = new Panel(); lblDetalleEjercicio = new Label(); contenedorFormulario = new TableLayoutPanel(); accionesFormulario = new FlowLayoutPanel();
            lblNombre = new Label(); lblDescripcionRutina = new Label(); lblEjercicio = new Label(); lblSeries = new Label(); lblRepeticiones = new Label(); lblPeso = new Label(); lblDescanso = new Label(); lblOrden = new Label(); lblDia = new Label();
            tabla = new DataGridView(); colId = new DataGridViewTextBoxColumn(); colRutina = new DataGridViewTextBoxColumn(); colCreador = new DataGridViewTextBoxColumn(); colAsignados = new DataGridViewTextBoxColumn(); colCreacion = new DataGridViewTextBoxColumn(); colEstado = new DataGridViewTextBoxColumn();
            dia = new ComboBox(); ejercicio = new ComboBox(); nombre = new TextBox(); descripcion = new TextBox(); series = new TextBox(); repeticiones = new TextBox(); peso = new TextBox(); descanso = new TextBox(); orden = new TextBox();
            nuevaRutina = new Button(); guardarRutina = new Button(); actualizar = new Button(); agregarEjercicio = new Button(); actualizarEjercicio = new Button(); darDeBaja = new Button(); reactivar = new Button(); quitarEjercicio = new Button(); guardarEjercicio = new Button(); cancelarEjercicio = new Button();
            tablaEjercicios = new DataGridView(); colDetalleId = new DataGridViewTextBoxColumn(); colDetalleDia = new DataGridViewTextBoxColumn(); colDetalleOrden = new DataGridViewTextBoxColumn(); colDetalleEjercicio = new DataGridViewTextBoxColumn(); colDetalleSeries = new DataGridViewTextBoxColumn(); colDetalleRepeticiones = new DataGridViewTextBoxColumn(); colDetallePeso = new DataGridViewTextBoxColumn(); colDetalleDescanso = new DataGridViewTextBoxColumn();
            ((ISupportInitialize)(tabla)).BeginInit(); ((ISupportInitialize)(tablaEjercicios)).BeginInit();
            panelEncabezado.SuspendLayout(); barraAcciones.SuspendLayout(); panelContenido.SuspendLayout(); ((ISupportInitialize)(splitContenido)).BeginInit(); splitContenido.Panel1.SuspendLayout(); splitContenido.Panel2.SuspendLayout(); splitContenido.SuspendLayout(); layoutDetalle.SuspendLayout(); panelListado.SuspendLayout(); panelRutina.SuspendLayout(); contenedorRutina.SuspendLayout(); accionesRutina.SuspendLayout(); panelEjercicios.SuspendLayout(); accionesEjercicios.SuspendLayout(); panelFormulario.SuspendLayout(); contenedorFormulario.SuspendLayout(); accionesFormulario.SuspendLayout(); SuspendLayout();

            panelEncabezado.BackColor = Color.FromArgb(14, 116, 144); panelEncabezado.Dock = DockStyle.Top; panelEncabezado.Height = 56; panelEncabezado.Padding = new Padding(20, 8, 20, 8); panelEncabezado.Controls.Add(lblTitulo); panelEncabezado.Controls.Add(lblDescripcion); panelEncabezado.Controls.Add(btnVolver);
            lblTitulo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right; lblTitulo.AutoSize = false; lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold); lblTitulo.ForeColor = Color.White; lblTitulo.Location = new Point(20, 8); lblTitulo.Size = new Size(890, 40); lblTitulo.Text = "Gestionar rutinas | Catálogo y composición de rutinas"; lblTitulo.TextAlign = ContentAlignment.MiddleLeft;
            lblDescripcion.Visible = false;
            panelEncabezado.Visible = false;
            btnVolver.Anchor = AnchorStyles.Top | AnchorStyles.Right; btnVolver.BackColor = Color.White; btnVolver.FlatAppearance.BorderSize = 0; btnVolver.FlatStyle = FlatStyle.Flat; btnVolver.ForeColor = Color.FromArgb(14, 116, 144); btnVolver.Location = new Point(974, 10); btnVolver.Size = new Size(104, 36); btnVolver.Text = "Volver"; btnVolver.UseVisualStyleBackColor = false; btnVolver.Click += new System.EventHandler(btnVolver_Click);

            barraAcciones.BackColor = Color.FromArgb(203, 213, 225); barraAcciones.Dock = DockStyle.Top; barraAcciones.Height = 1;
            lblEstado.BackColor = Color.FromArgb(226, 232, 240); lblEstado.Dock = DockStyle.Bottom; lblEstado.Height = 30; lblEstado.ForeColor = Color.FromArgb(51, 65, 85); lblEstado.Padding = new Padding(18, 0, 12, 0); lblEstado.Text = "Listo"; lblEstado.TextAlign = ContentAlignment.MiddleLeft;
            panelContenido.BackColor = Color.FromArgb(248, 250, 252); panelContenido.Dock = DockStyle.Fill; panelContenido.Padding = new Padding(16); panelContenido.Controls.Add(splitContenido);

            splitContenido.Dock = DockStyle.Fill; splitContenido.Size = new Size(1068, 480); splitContenido.Orientation = Orientation.Vertical; splitContenido.FixedPanel = FixedPanel.Panel1; splitContenido.IsSplitterFixed = false; splitContenido.SplitterWidth = 6; splitContenido.SplitterDistance = 330; splitContenido.Panel1MinSize = 240; splitContenido.Panel2MinSize = 500; splitContenido.TabIndex = 0; splitContenido.Resize += new System.EventHandler(splitContenido_Resize);
            splitContenido.Panel1.Padding = new Padding(8); splitContenido.Panel1.Controls.Add(panelListado);
            splitContenido.Panel2.Padding = new Padding(8); splitContenido.Panel2.Controls.Add(layoutDetalle);
            layoutDetalle.Dock = DockStyle.Fill; layoutDetalle.ColumnCount = 1; layoutDetalle.RowCount = 3; layoutDetalle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F)); layoutDetalle.RowStyles.Add(new RowStyle(SizeType.Absolute, 136F)); layoutDetalle.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); layoutDetalle.RowStyles.Add(new RowStyle(SizeType.Absolute, 172F)); layoutDetalle.Controls.Add(panelRutina, 0, 0); layoutDetalle.Controls.Add(panelEjercicios, 0, 1); layoutDetalle.Controls.Add(panelFormulario, 0, 2);

            panelListado.BackColor = Color.White; panelListado.BorderStyle = BorderStyle.FixedSingle; panelListado.Dock = DockStyle.Fill; panelListado.Padding = new Padding(10); panelListado.Controls.Add(tabla); panelListado.Controls.Add(nuevaRutina); panelListado.Controls.Add(lblListadoTitulo);
            lblListadoTitulo.Dock = DockStyle.Top; lblListadoTitulo.Height = 30; lblListadoTitulo.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold); lblListadoTitulo.ForeColor = Color.FromArgb(30, 41, 59); lblListadoTitulo.Text = "Rutinas"; lblListadoTitulo.TextAlign = ContentAlignment.MiddleLeft;
            ConfigurarTabla(tabla); tabla.Dock = DockStyle.Fill; tabla.TabIndex = 1; tabla.SelectionChanged += new System.EventHandler(tabla_SelectionChanged); tabla.Columns.AddRange(new DataGridViewColumn[] { colId, colRutina, colCreador, colAsignados, colCreacion, colEstado });
            colId.Name = "colId"; colId.Visible = false; colRutina.HeaderText = "Rutina"; colRutina.Name = "colRutina"; colRutina.FillWeight = 34; colRutina.MinimumWidth = 115; colCreador.HeaderText = "Entrenador"; colCreador.Name = "colCreador"; colCreador.FillWeight = 26; colCreador.MinimumWidth = 100; colAsignados.HeaderText = "Socios"; colAsignados.Name = "colAsignados"; colAsignados.FillWeight = 15; colAsignados.MinimumWidth = 60; colCreacion.HeaderText = "Creación"; colCreacion.Name = "colCreacion"; colCreacion.FillWeight = 15; colCreacion.MinimumWidth = 82; colEstado.HeaderText = "Estado"; colEstado.Name = "colEstado"; colEstado.FillWeight = 10; colEstado.MinimumWidth = 70;
            ConfigurarBoton(nuevaRutina, "+ Nueva rutina", Point.Empty, new Size(130, 36), Color.FromArgb(226, 232, 240), Color.FromArgb(30, 41, 59), 0, nuevaRutina_Click); nuevaRutina.Dock = DockStyle.Bottom; nuevaRutina.Margin = new Padding(0, 8, 0, 0);

            panelRutina.BackColor = Color.White; panelRutina.BorderStyle = BorderStyle.FixedSingle; panelRutina.Dock = DockStyle.Fill; panelRutina.Height = 136; panelRutina.Padding = new Padding(10); panelRutina.Controls.Add(contenedorRutina); panelRutina.Controls.Add(accionesRutina); panelRutina.Controls.Add(lblDetalleTitulo);
            lblDetalleTitulo.Dock = DockStyle.Top; lblDetalleTitulo.Height = 28; lblDetalleTitulo.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold); lblDetalleTitulo.ForeColor = Color.FromArgb(30, 41, 59); lblDetalleTitulo.Text = "Rutina seleccionada"; lblDetalleTitulo.TextAlign = ContentAlignment.MiddleLeft;
            contenedorRutina.Dock = DockStyle.Fill; contenedorRutina.ColumnCount = 2; contenedorRutina.RowCount = 2; contenedorRutina.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 86F)); contenedorRutina.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F)); contenedorRutina.RowStyles.Add(new RowStyle(SizeType.Percent, 50F)); contenedorRutina.RowStyles.Add(new RowStyle(SizeType.Percent, 50F)); contenedorRutina.Padding = new Padding(0, 2, 0, 2);
            AgregarCampo(contenedorRutina, lblNombre, "Nombre:", nombre, 0, 0); AgregarCampo(contenedorRutina, lblDescripcionRutina, "Descripción:", descripcion, 0, 1);
            accionesRutina.Dock = DockStyle.Bottom; accionesRutina.Height = 38; accionesRutina.FlowDirection = FlowDirection.LeftToRight; accionesRutina.WrapContents = false; accionesRutina.Padding = new Padding(0, 2, 0, 0);
            ConfigurarBoton(guardarRutina, "Guardar", Point.Empty, new Size(108, 32), Color.FromArgb(14, 116, 144), Color.White, 1, guardarRutina_Click); ConfigurarBoton(actualizar, "Actualizar", Point.Empty, new Size(108, 32), Color.FromArgb(226, 232, 240), Color.FromArgb(30, 41, 59), 2, actualizar_Click); ConfigurarBoton(darDeBaja, "Dar de baja", Point.Empty, new Size(108, 32), Color.FromArgb(254, 242, 242), Color.FromArgb(185, 28, 28), 5, darDeBaja_Click); ConfigurarBoton(reactivar, "Reactivar", Point.Empty, new Size(108, 32), Color.FromArgb(220, 252, 231), Color.FromArgb(22, 101, 52), 6, reactivar_Click); accionesRutina.Controls.Add(guardarRutina); accionesRutina.Controls.Add(actualizar); accionesRutina.Controls.Add(darDeBaja); accionesRutina.Controls.Add(reactivar); darDeBaja.Visible = false; reactivar.Visible = false;

            panelEjercicios.BackColor = Color.White; panelEjercicios.BorderStyle = BorderStyle.FixedSingle; panelEjercicios.Dock = DockStyle.Fill; panelEjercicios.Padding = new Padding(10); panelEjercicios.Controls.Add(tablaEjercicios); panelEjercicios.Controls.Add(accionesEjercicios); panelEjercicios.Controls.Add(lblEjerciciosTitulo);
            lblEjerciciosTitulo.Dock = DockStyle.Top; lblEjerciciosTitulo.Height = 28; lblEjerciciosTitulo.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold); lblEjerciciosTitulo.ForeColor = Color.FromArgb(30, 41, 59); lblEjerciciosTitulo.Text = "Ejercicios de la rutina"; lblEjerciciosTitulo.TextAlign = ContentAlignment.MiddleLeft;
            accionesEjercicios.Dock = DockStyle.Top; accionesEjercicios.Height = 38; accionesEjercicios.FlowDirection = FlowDirection.LeftToRight; accionesEjercicios.WrapContents = false; accionesEjercicios.Padding = new Padding(0, 2, 0, 0);
            ConfigurarBoton(agregarEjercicio, "+ Agregar ejercicio", Point.Empty, new Size(132, 32), Color.FromArgb(14, 116, 144), Color.White, 3, agregarEjercicio_Click); ConfigurarBoton(actualizarEjercicio, "Editar", Point.Empty, new Size(92, 32), Color.FromArgb(226, 232, 240), Color.FromArgb(30, 41, 59), 4, actualizarEjercicio_Click); ConfigurarBoton(quitarEjercicio, "Quitar", Point.Empty, new Size(92, 32), Color.FromArgb(254, 242, 242), Color.FromArgb(185, 28, 28), 7, quitarEjercicio_Click); accionesEjercicios.Controls.Add(agregarEjercicio); accionesEjercicios.Controls.Add(actualizarEjercicio); accionesEjercicios.Controls.Add(quitarEjercicio);
            ConfigurarTabla(tablaEjercicios); tablaEjercicios.Dock = DockStyle.Fill; tablaEjercicios.TabIndex = 2; tablaEjercicios.Columns.AddRange(new DataGridViewColumn[] { colDetalleId, colDetalleDia, colDetalleOrden, colDetalleEjercicio, colDetalleSeries, colDetalleRepeticiones, colDetallePeso, colDetalleDescanso }); tablaEjercicios.SelectionChanged += new System.EventHandler(tablaEjercicios_SelectionChanged);
            colDetalleId.Name = "colDetalleId"; colDetalleId.Visible = false; colDetalleDia.HeaderText = "Día"; colDetalleDia.Name = "colDetalleDia"; colDetalleDia.FillWeight = 12; colDetalleDia.MinimumWidth = 65; colDetalleOrden.HeaderText = "Orden"; colDetalleOrden.Name = "colDetalleOrden"; colDetalleOrden.FillWeight = 10; colDetalleOrden.MinimumWidth = 58; colDetalleEjercicio.HeaderText = "Ejercicio"; colDetalleEjercicio.Name = "colDetalleEjercicio"; colDetalleEjercicio.FillWeight = 28; colDetalleEjercicio.MinimumWidth = 110; colDetalleSeries.HeaderText = "Series"; colDetalleSeries.Name = "colDetalleSeries"; colDetalleSeries.FillWeight = 10; colDetalleSeries.MinimumWidth = 58; colDetalleRepeticiones.HeaderText = "Repeticiones"; colDetalleRepeticiones.Name = "colDetalleRepeticiones"; colDetalleRepeticiones.FillWeight = 16; colDetalleRepeticiones.MinimumWidth = 86; colDetallePeso.HeaderText = "Peso"; colDetallePeso.Name = "colDetallePeso"; colDetallePeso.FillWeight = 10; colDetallePeso.MinimumWidth = 58; colDetalleDescanso.HeaderText = "Descanso"; colDetalleDescanso.Name = "colDetalleDescanso"; colDetalleDescanso.FillWeight = 14; colDetalleDescanso.MinimumWidth = 76;

            panelFormulario.BackColor = Color.White; panelFormulario.BorderStyle = BorderStyle.FixedSingle; panelFormulario.Dock = DockStyle.Fill; panelFormulario.Height = 190; panelFormulario.Padding = new Padding(10); panelFormulario.Controls.Add(contenedorFormulario); panelFormulario.Controls.Add(accionesFormulario); panelFormulario.Controls.Add(lblDetalleEjercicio);
            lblDetalleEjercicio.Dock = DockStyle.Top; lblDetalleEjercicio.Height = 26; lblDetalleEjercicio.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold); lblDetalleEjercicio.ForeColor = Color.FromArgb(30, 41, 59); lblDetalleEjercicio.Text = "Detalle del ejercicio"; lblDetalleEjercicio.TextAlign = ContentAlignment.MiddleLeft;
            accionesFormulario.Dock = DockStyle.Bottom; accionesFormulario.FlowDirection = FlowDirection.LeftToRight; accionesFormulario.Height = 38; accionesFormulario.Padding = new Padding(0, 2, 0, 0); accionesFormulario.WrapContents = false;
            ConfigurarBoton(guardarEjercicio, "Agregar", Point.Empty, new Size(120, 32), Color.FromArgb(14, 116, 144), Color.White, 8, guardarEjercicio_Click); ConfigurarBoton(cancelarEjercicio, "Cancelar", Point.Empty, new Size(100, 32), Color.FromArgb(226, 232, 240), Color.FromArgb(30, 41, 59), 9, cancelarEjercicio_Click); guardarEjercicio.Visible = false; cancelarEjercicio.Visible = false; accionesFormulario.Controls.Add(guardarEjercicio); accionesFormulario.Controls.Add(cancelarEjercicio);
            contenedorFormulario.Dock = DockStyle.Fill; contenedorFormulario.ColumnCount = 4; contenedorFormulario.RowCount = 4; contenedorFormulario.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 78F)); contenedorFormulario.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F)); contenedorFormulario.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 92F)); contenedorFormulario.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F)); for (var fila = 0; fila < 4; fila++) contenedorFormulario.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            AgregarCampo(contenedorFormulario, lblEjercicio, "Ejercicio:", ejercicio, 0, 0); AgregarCampo(contenedorFormulario, lblDia, "Día:", dia, 2, 0); AgregarCampo(contenedorFormulario, lblSeries, "Series:", series, 0, 1); AgregarCampo(contenedorFormulario, lblRepeticiones, "Repeticiones:", repeticiones, 2, 1); AgregarCampo(contenedorFormulario, lblPeso, "Peso:", peso, 0, 2); AgregarCampo(contenedorFormulario, lblDescanso, "Descanso:", descanso, 2, 2); AgregarCampo(contenedorFormulario, lblOrden, "Orden:", orden, 0, 3);
            ejercicio.DropDownStyle = ComboBoxStyle.DropDownList; dia.DropDownStyle = ComboBoxStyle.DropDownList; dia.Items.AddRange(new object[] { "Lunes", "Martes", "Miercoles", "Jueves", "Viernes" }); dia.SelectedIndex = 0; nombre.BorderStyle = BorderStyle.FixedSingle; descripcion.BorderStyle = BorderStyle.FixedSingle; series.BorderStyle = BorderStyle.FixedSingle; repeticiones.BorderStyle = BorderStyle.FixedSingle; peso.BorderStyle = BorderStyle.FixedSingle; descanso.BorderStyle = BorderStyle.FixedSingle; orden.BorderStyle = BorderStyle.FixedSingle; series.KeyPress += new KeyPressEventHandler(series_KeyPress); repeticiones.KeyPress += new KeyPressEventHandler(repeticiones_KeyPress); peso.KeyPress += new KeyPressEventHandler(peso_KeyPress); descanso.KeyPress += new KeyPressEventHandler(descanso_KeyPress); orden.KeyPress += new KeyPressEventHandler(orden_KeyPress);

            Controls.Add(panelContenido); Controls.Add(lblEstado); Controls.Add(barraAcciones); Controls.Add(panelEncabezado); AutoScaleMode = AutoScaleMode.Font; BackColor = Color.FromArgb(241, 245, 249); ClientSize = new Size(1100, 680); Font = new Font("Segoe UI", 9.5F); MinimumSize = new Size(900, 560); Name = "RutinasEntrenadorFormulario"; StartPosition = FormStartPosition.CenterParent; Text = "SysGym | Gestionar rutinas"; Load += new System.EventHandler(RutinasEntrenadorFormulario_Load);
            contenedorFormulario.ResumeLayout(false); accionesFormulario.ResumeLayout(false); panelFormulario.ResumeLayout(false); accionesEjercicios.ResumeLayout(false); panelEjercicios.ResumeLayout(false); accionesRutina.ResumeLayout(false); contenedorRutina.ResumeLayout(false); panelRutina.ResumeLayout(false); panelListado.ResumeLayout(false); layoutDetalle.ResumeLayout(false); splitContenido.Panel2.ResumeLayout(false); splitContenido.Panel1.ResumeLayout(false); ((ISupportInitialize)(splitContenido)).EndInit(); splitContenido.ResumeLayout(false); panelContenido.ResumeLayout(false); barraAcciones.ResumeLayout(false); panelEncabezado.ResumeLayout(false); ((ISupportInitialize)(tablaEjercicios)).EndInit(); ((ISupportInitialize)(tabla)).EndInit(); ResumeLayout(false);
        }

        private static void ConfigurarTabla(DataGridView grilla)
        {
            grilla.BackgroundColor = Color.White; grilla.BorderStyle = BorderStyle.None; grilla.AllowUserToAddRows = false; grilla.AllowUserToDeleteRows = false; grilla.AllowUserToResizeRows = false; grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; grilla.MultiSelect = false; grilla.ReadOnly = true; grilla.RowHeadersVisible = false; grilla.RowTemplate.Height = 28; grilla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private static void ConfigurarBoton(Button boton, string texto, Point ubicacion, Size tamano, Color fondo, Color textoColor, int tabIndex, System.EventHandler evento)
        {
            boton.AutoSize = false; boton.BackColor = fondo; boton.FlatAppearance.BorderSize = 0; boton.FlatStyle = FlatStyle.Flat; boton.ForeColor = textoColor; boton.Location = ubicacion; boton.Margin = new Padding(0, 0, 6, 0); boton.Size = tamano; boton.TabIndex = tabIndex; boton.Text = texto; boton.UseVisualStyleBackColor = false; boton.Click += evento;
        }

        private static void AgregarCampo(TableLayoutPanel tablaCampos, Label etiqueta, string texto, Control control, int columna, int fila)
        {
            etiqueta.Dock = DockStyle.Fill; etiqueta.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold); etiqueta.ForeColor = Color.FromArgb(30, 41, 59); etiqueta.Text = texto; etiqueta.TextAlign = ContentAlignment.MiddleLeft; etiqueta.Margin = new Padding(0, 2, 8, 2);
            control.Dock = DockStyle.Fill; control.Margin = new Padding(0, 2, 8, 2); control.TabIndex = fila * 2 + columna + 1;
            tablaCampos.Controls.Add(etiqueta, columna, fila); tablaCampos.Controls.Add(control, columna + 1, fila);
        }
    }
}
