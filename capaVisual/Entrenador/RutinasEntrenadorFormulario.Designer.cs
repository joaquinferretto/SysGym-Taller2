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
        private TableLayoutPanel layoutContenido;
        private Panel panelFormulario;
        private TableLayoutPanel contenedorFormulario;
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
        private Button darDeBaja;
        private Button quitarEjercicio;
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
            barraAcciones = new Panel(); nuevaRutina = new Button(); guardarRutina = new Button(); actualizar = new Button(); agregarEjercicio = new Button(); darDeBaja = new Button(); quitarEjercicio = new Button(); lblEstado = new Label();
            panelContenido = new Panel(); layoutContenido = new TableLayoutPanel(); panelFormulario = new Panel(); contenedorFormulario = new TableLayoutPanel();
            lblNombre = new Label(); lblDescripcionRutina = new Label(); lblEjercicio = new Label(); lblSeries = new Label(); lblRepeticiones = new Label(); lblPeso = new Label(); lblDescanso = new Label(); lblOrden = new Label(); lblDia = new Label();
            tabla = new DataGridView(); colId = new DataGridViewTextBoxColumn(); colRutina = new DataGridViewTextBoxColumn(); colCreador = new DataGridViewTextBoxColumn(); colAsignados = new DataGridViewTextBoxColumn(); colCreacion = new DataGridViewTextBoxColumn();
            dia = new ComboBox(); ejercicio = new ComboBox(); nombre = new TextBox(); descripcion = new TextBox(); series = new TextBox(); repeticiones = new TextBox(); peso = new TextBox(); descanso = new TextBox(); orden = new TextBox();
            tablaEjercicios = new DataGridView(); colDetalleId = new DataGridViewTextBoxColumn(); colDetalleDia = new DataGridViewTextBoxColumn(); colDetalleOrden = new DataGridViewTextBoxColumn(); colDetalleEjercicio = new DataGridViewTextBoxColumn(); colDetalleSeries = new DataGridViewTextBoxColumn(); colDetalleRepeticiones = new DataGridViewTextBoxColumn(); colDetallePeso = new DataGridViewTextBoxColumn(); colDetalleDescanso = new DataGridViewTextBoxColumn();
            ((ISupportInitialize)(tabla)).BeginInit(); ((ISupportInitialize)(tablaEjercicios)).BeginInit(); panelEncabezado.SuspendLayout(); barraAcciones.SuspendLayout(); panelContenido.SuspendLayout(); layoutContenido.SuspendLayout(); panelFormulario.SuspendLayout(); contenedorFormulario.SuspendLayout(); SuspendLayout();

            panelEncabezado.BackColor = Color.FromArgb(14, 116, 144); panelEncabezado.Dock = DockStyle.Top; panelEncabezado.Height = 84; panelEncabezado.Padding = new Padding(22, 8, 22, 8); panelEncabezado.Controls.Add(lblTitulo); panelEncabezado.Controls.Add(lblDescripcion); panelEncabezado.Controls.Add(btnVolver);
            lblTitulo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right; lblTitulo.AutoSize = false; lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold); lblTitulo.ForeColor = Color.White; lblTitulo.Location = new Point(22, 8); lblTitulo.Size = new Size(890, 36); lblTitulo.Text = "Catalogo de rutinas"; lblTitulo.TextAlign = ContentAlignment.BottomLeft;
            lblDescripcion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right; lblDescripcion.AutoSize = false; lblDescripcion.ForeColor = Color.FromArgb(226, 232, 240); lblDescripcion.Location = new Point(24, 48); lblDescripcion.Size = new Size(890, 26); lblDescripcion.Text = "Crea plantillas y administra sus ejercicios"; lblDescripcion.TextAlign = ContentAlignment.TopLeft;
            btnVolver.Anchor = AnchorStyles.Top | AnchorStyles.Right; btnVolver.BackColor = Color.White; btnVolver.FlatAppearance.BorderSize = 0; btnVolver.FlatStyle = FlatStyle.Flat; btnVolver.ForeColor = Color.FromArgb(14, 116, 144); btnVolver.Location = new Point(974, 24); btnVolver.Size = new Size(104, 38); btnVolver.Text = "Volver"; btnVolver.UseVisualStyleBackColor = false; btnVolver.Click += new System.EventHandler(btnVolver_Click);

            barraAcciones.BackColor = Color.White; barraAcciones.Dock = DockStyle.Top; barraAcciones.Height = 52; barraAcciones.Padding = new Padding(16, 8, 16, 8); barraAcciones.Controls.Add(nuevaRutina); barraAcciones.Controls.Add(guardarRutina); barraAcciones.Controls.Add(actualizar); barraAcciones.Controls.Add(agregarEjercicio); barraAcciones.Controls.Add(darDeBaja); barraAcciones.Controls.Add(quitarEjercicio);
            ConfigurarBoton(nuevaRutina, "+ Nueva rutina", new Point(16, 8), new Size(112, 34), Color.FromArgb(226, 232, 240), Color.FromArgb(30, 41, 59), 0, nuevaRutina_Click);
            ConfigurarBoton(guardarRutina, "Guardar", new Point(136, 8), new Size(112, 34), Color.FromArgb(14, 116, 144), Color.White, 1, guardarRutina_Click);
            ConfigurarBoton(actualizar, "Actualizar", new Point(256, 8), new Size(112, 34), Color.FromArgb(226, 232, 240), Color.FromArgb(30, 41, 59), 2, actualizar_Click);
            ConfigurarBoton(agregarEjercicio, "Agregar ejercicio", new Point(376, 8), new Size(132, 34), Color.FromArgb(14, 116, 144), Color.White, 3, agregarEjercicio_Click);
            ConfigurarBoton(darDeBaja, "Dar de baja", new Point(516, 8), new Size(112, 34), Color.FromArgb(254, 242, 242), Color.FromArgb(185, 28, 28), 4, darDeBaja_Click);
            ConfigurarBoton(quitarEjercicio, "Quitar ejercicio", new Point(636, 8), new Size(132, 34), Color.FromArgb(254, 242, 242), Color.FromArgb(185, 28, 28), 5, quitarEjercicio_Click);

            lblEstado.BackColor = Color.FromArgb(226, 232, 240); lblEstado.Dock = DockStyle.Bottom; lblEstado.Height = 30; lblEstado.ForeColor = Color.FromArgb(51, 65, 85); lblEstado.Padding = new Padding(18, 0, 12, 0); lblEstado.Text = "Listo"; lblEstado.TextAlign = ContentAlignment.MiddleLeft;
            panelContenido.BackColor = Color.FromArgb(248, 250, 252); panelContenido.Dock = DockStyle.Fill; panelContenido.Padding = new Padding(16); panelContenido.Controls.Add(layoutContenido);
            layoutContenido.Dock = DockStyle.Fill; layoutContenido.ColumnCount = 1; layoutContenido.RowCount = 3; layoutContenido.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F)); layoutContenido.RowStyles.Add(new RowStyle(SizeType.Percent, 28F)); layoutContenido.RowStyles.Add(new RowStyle(SizeType.Percent, 36F)); layoutContenido.RowStyles.Add(new RowStyle(SizeType.Percent, 36F)); layoutContenido.Controls.Add(tabla, 0, 0); layoutContenido.Controls.Add(tablaEjercicios, 0, 1); layoutContenido.Controls.Add(panelFormulario, 0, 2);

            ConfigurarTabla(tabla); tabla.ColumnHeadersHeight = 34; tabla.TabIndex = 1; tabla.SelectionChanged += new System.EventHandler(tabla_SelectionChanged); tabla.Columns.AddRange(new DataGridViewColumn[] { colId, colRutina, colCreador, colAsignados, colCreacion });
            colId.Name = "colId"; colId.Visible = false; colRutina.HeaderText = "Rutina"; colRutina.Name = "colRutina"; colRutina.FillWeight = 34; colRutina.MinimumWidth = 120; colCreador.HeaderText = "Entrenador"; colCreador.Name = "colCreador"; colCreador.FillWeight = 28; colCreador.MinimumWidth = 110; colAsignados.HeaderText = "Socios asignados"; colAsignados.Name = "colAsignados"; colAsignados.FillWeight = 20; colAsignados.MinimumWidth = 100; colCreacion.HeaderText = "Creacion"; colCreacion.Name = "colCreacion"; colCreacion.FillWeight = 18; colCreacion.MinimumWidth = 90;

            ConfigurarTabla(tablaEjercicios); tablaEjercicios.ColumnHeadersHeight = 32; tablaEjercicios.TabIndex = 2; tablaEjercicios.Columns.AddRange(new DataGridViewColumn[] { colDetalleId, colDetalleDia, colDetalleOrden, colDetalleEjercicio, colDetalleSeries, colDetalleRepeticiones, colDetallePeso, colDetalleDescanso }); tablaEjercicios.SelectionChanged += new System.EventHandler(tablaEjercicios_SelectionChanged);
            colDetalleId.Name = "colDetalleId"; colDetalleId.Visible = false; colDetalleDia.HeaderText = "Dia"; colDetalleDia.Name = "colDetalleDia"; colDetalleDia.FillWeight = 12; colDetalleDia.MinimumWidth = 70; colDetalleOrden.HeaderText = "Orden"; colDetalleOrden.Name = "colDetalleOrden"; colDetalleOrden.FillWeight = 10; colDetalleOrden.MinimumWidth = 62; colDetalleEjercicio.HeaderText = "Ejercicio"; colDetalleEjercicio.Name = "colDetalleEjercicio"; colDetalleEjercicio.FillWeight = 27; colDetalleEjercicio.MinimumWidth = 110; colDetalleSeries.HeaderText = "Series"; colDetalleSeries.Name = "colDetalleSeries"; colDetalleSeries.FillWeight = 10; colDetalleSeries.MinimumWidth = 62; colDetalleRepeticiones.HeaderText = "Repeticiones"; colDetalleRepeticiones.Name = "colDetalleRepeticiones"; colDetalleRepeticiones.FillWeight = 16; colDetalleRepeticiones.MinimumWidth = 90; colDetallePeso.HeaderText = "Peso"; colDetallePeso.Name = "colDetallePeso"; colDetallePeso.FillWeight = 10; colDetallePeso.MinimumWidth = 62; colDetalleDescanso.HeaderText = "Descanso"; colDetalleDescanso.Name = "colDetalleDescanso"; colDetalleDescanso.FillWeight = 15; colDetalleDescanso.MinimumWidth = 80;

            panelFormulario.BackColor = Color.White; panelFormulario.BorderStyle = BorderStyle.FixedSingle; panelFormulario.Padding = new Padding(12); panelFormulario.Controls.Add(contenedorFormulario);
            contenedorFormulario.Dock = DockStyle.Fill; contenedorFormulario.ColumnCount = 4; contenedorFormulario.RowCount = 5; contenedorFormulario.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 88F)); contenedorFormulario.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F)); contenedorFormulario.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 96F)); contenedorFormulario.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F)); for (var fila = 0; fila < 5; fila++) contenedorFormulario.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            AgregarCampo(contenedorFormulario, lblNombre, "Nombre:", nombre, 0, 0); AgregarCampo(contenedorFormulario, lblDescripcionRutina, "Descripcion:", descripcion, 2, 0); AgregarCampo(contenedorFormulario, lblEjercicio, "Ejercicio:", ejercicio, 0, 1); AgregarCampo(contenedorFormulario, lblSeries, "Series:", series, 2, 1); AgregarCampo(contenedorFormulario, lblRepeticiones, "Repeticiones:", repeticiones, 0, 2); AgregarCampo(contenedorFormulario, lblPeso, "Peso:", peso, 2, 2); AgregarCampo(contenedorFormulario, lblDescanso, "Descanso:", descanso, 0, 3); AgregarCampo(contenedorFormulario, lblOrden, "Orden:", orden, 2, 3); AgregarCampo(contenedorFormulario, lblDia, "Dia:", dia, 0, 4);
            ejercicio.DropDownStyle = ComboBoxStyle.DropDownList; dia.DropDownStyle = ComboBoxStyle.DropDownList; dia.Items.AddRange(new object[] { "Lunes", "Martes", "Miercoles", "Jueves", "Viernes" }); dia.SelectedIndex = 0; nombre.BorderStyle = BorderStyle.FixedSingle; descripcion.BorderStyle = BorderStyle.FixedSingle; series.BorderStyle = BorderStyle.FixedSingle; repeticiones.BorderStyle = BorderStyle.FixedSingle; peso.BorderStyle = BorderStyle.FixedSingle; descanso.BorderStyle = BorderStyle.FixedSingle; orden.BorderStyle = BorderStyle.FixedSingle;

            Controls.Add(panelContenido); Controls.Add(lblEstado); Controls.Add(barraAcciones); Controls.Add(panelEncabezado); AutoScaleMode = AutoScaleMode.Font; BackColor = Color.FromArgb(241, 245, 249); ClientSize = new Size(1100, 680); Font = new Font("Segoe UI", 9.5F); MinimumSize = new Size(900, 560); Name = "RutinasEntrenadorFormulario"; StartPosition = FormStartPosition.CenterParent; Text = "SysGym | Catalogo de rutinas"; Load += new System.EventHandler(RutinasEntrenadorFormulario_Load);
            contenedorFormulario.ResumeLayout(false); panelFormulario.ResumeLayout(false); layoutContenido.ResumeLayout(false); panelContenido.ResumeLayout(false); barraAcciones.ResumeLayout(false); panelEncabezado.ResumeLayout(false); ((ISupportInitialize)(tablaEjercicios)).EndInit(); ((ISupportInitialize)(tabla)).EndInit(); ResumeLayout(false);
        }

        private static void ConfigurarTabla(DataGridView grilla)
        {
            grilla.Dock = DockStyle.Fill; grilla.BackgroundColor = Color.White; grilla.BorderStyle = BorderStyle.None; grilla.AllowUserToAddRows = false; grilla.AllowUserToDeleteRows = false; grilla.AllowUserToResizeRows = false; grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; grilla.MultiSelect = false; grilla.ReadOnly = true; grilla.RowHeadersVisible = false; grilla.RowTemplate.Height = 28; grilla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private static void ConfigurarBoton(Button boton, string texto, Point ubicacion, Size tamano, Color fondo, Color textoColor, int tabIndex, System.EventHandler evento)
        {
            boton.AutoSize = false; boton.BackColor = fondo; boton.FlatAppearance.BorderSize = 0; boton.FlatStyle = FlatStyle.Flat; boton.ForeColor = textoColor; boton.Location = ubicacion; boton.Size = tamano; boton.TabIndex = tabIndex; boton.Text = texto; boton.UseVisualStyleBackColor = false; boton.Click += evento;
        }

        private static void AgregarCampo(TableLayoutPanel tablaCampos, Label etiqueta, string texto, Control control, int columna, int fila)
        {
            etiqueta.Dock = DockStyle.Fill; etiqueta.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold); etiqueta.ForeColor = Color.FromArgb(30, 41, 59); etiqueta.Text = texto; etiqueta.TextAlign = ContentAlignment.MiddleLeft; etiqueta.Margin = new Padding(0, 2, 8, 2);
            control.Dock = DockStyle.Fill; control.Margin = new Padding(0, 3, 12, 3); control.TabIndex = fila * 2 + columna + 1;
            tablaCampos.Controls.Add(etiqueta, columna, fila); tablaCampos.Controls.Add(control, columna + 1, fila);
        }
    }
}
