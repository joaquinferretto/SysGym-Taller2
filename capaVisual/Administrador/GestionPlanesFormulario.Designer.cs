using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Administrador
{
    partial class GestionPlanesFormulario
    {
        private IContainer components;
        private Panel panelEncabezado;
        private Label lblTitulo;
        private Label lblDescripcion;
        private Button btnVolver;
        private Panel barraAcciones;
        private Label lblBuscar;
        private TextBox buscador;
        private Label lblFiltroEstado;
        private ComboBox filtroEstado;
        private Label lblEstado;
        private SplitContainer splitContenido;
        private Panel panelListado;
        private Label lblListado;
        private DataGridView tabla;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colNombre;
        private DataGridViewTextBoxColumn colPrecio;
        private DataGridViewTextBoxColumn colEstado;
        private Panel panelDetalle;
        private Panel contenedorDetalle;
        private Label lblFormulario;
        private TableLayoutPanel contenedorCampos;
        private Label lblNombre;
        private Label lblDescripcionPlan;
        private Label lblPrecio;
        private TextBox nombre;
        private TextBox descripcion;
        private TextBox precio;
        private Panel panelAcciones;
        private Button nuevo;
        private Button guardar;
        private Button actualizar;
        private Button darDeBaja;
        private Button reactivar;

        protected override void Dispose(bool liberarRecursos)
        {
            if (liberarRecursos && components != null) components.Dispose();
            base.Dispose(liberarRecursos);
        }

        private void InitializeComponent()
        {
            components = new Container();
            panelEncabezado = new Panel(); lblTitulo = new Label(); lblDescripcion = new Label(); btnVolver = new Button();
            barraAcciones = new Panel(); lblBuscar = new Label(); buscador = new TextBox(); lblFiltroEstado = new Label(); filtroEstado = new ComboBox(); lblEstado = new Label();
            splitContenido = new SplitContainer(); panelListado = new Panel(); lblListado = new Label(); tabla = new DataGridView(); colId = new DataGridViewTextBoxColumn(); colNombre = new DataGridViewTextBoxColumn(); colPrecio = new DataGridViewTextBoxColumn(); colEstado = new DataGridViewTextBoxColumn();
            panelDetalle = new Panel(); contenedorDetalle = new Panel(); lblFormulario = new Label(); contenedorCampos = new TableLayoutPanel(); lblNombre = new Label(); lblDescripcionPlan = new Label(); lblPrecio = new Label(); nombre = new TextBox(); descripcion = new TextBox(); precio = new TextBox(); panelAcciones = new Panel(); nuevo = new Button(); guardar = new Button(); actualizar = new Button(); darDeBaja = new Button(); reactivar = new Button();
            ((ISupportInitialize)(tabla)).BeginInit(); ((ISupportInitialize)(splitContenido)).BeginInit(); splitContenido.Panel1.SuspendLayout(); splitContenido.Panel2.SuspendLayout(); splitContenido.SuspendLayout(); panelEncabezado.SuspendLayout(); barraAcciones.SuspendLayout(); panelListado.SuspendLayout(); panelDetalle.SuspendLayout(); contenedorDetalle.SuspendLayout(); contenedorCampos.SuspendLayout(); panelAcciones.SuspendLayout(); SuspendLayout();

            panelEncabezado.BackColor = Color.FromArgb(79, 70, 229); panelEncabezado.Dock = DockStyle.Top; panelEncabezado.Height = 76; panelEncabezado.Controls.Add(lblTitulo); panelEncabezado.Controls.Add(lblDescripcion); panelEncabezado.Controls.Add(btnVolver);
            lblTitulo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right; lblTitulo.AutoSize = false; lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold); lblTitulo.ForeColor = Color.White; lblTitulo.Location = new Point(22, 8); lblTitulo.Size = new Size(880, 36); lblTitulo.Text = "Planes"; lblTitulo.TextAlign = ContentAlignment.BottomLeft;
            lblDescripcion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right; lblDescripcion.AutoSize = false; lblDescripcion.ForeColor = Color.FromArgb(226, 232, 240); lblDescripcion.Location = new Point(24, 46); lblDescripcion.Size = new Size(880, 24); lblDescripcion.Text = "Administra los planes comerciales del gimnasio";
            btnVolver.Anchor = AnchorStyles.Top | AnchorStyles.Right; btnVolver.BackColor = Color.White; btnVolver.FlatAppearance.BorderSize = 0; btnVolver.FlatStyle = FlatStyle.Flat; btnVolver.ForeColor = Color.FromArgb(79, 70, 229); btnVolver.Location = new Point(974, 20); btnVolver.Size = new Size(104, 38); btnVolver.Text = "Volver"; btnVolver.UseVisualStyleBackColor = false; btnVolver.Click += new System.EventHandler(btnVolver_Click);

            barraAcciones.BackColor = Color.White; barraAcciones.Dock = DockStyle.Top; barraAcciones.Height = 54; barraAcciones.Controls.Add(lblBuscar); barraAcciones.Controls.Add(buscador); barraAcciones.Controls.Add(lblFiltroEstado); barraAcciones.Controls.Add(filtroEstado);
            lblBuscar.AutoSize = false; lblBuscar.Location = new Point(16, 12); lblBuscar.Size = new Size(52, 28); lblBuscar.Text = "Buscar:"; lblBuscar.TextAlign = ContentAlignment.MiddleLeft;
            buscador.Location = new Point(72, 15); buscador.Size = new Size(250, 24); buscador.TabIndex = 0; buscador.TextChanged += new System.EventHandler(buscador_TextChanged);
            lblFiltroEstado.AutoSize = false; lblFiltroEstado.Location = new Point(350, 12); lblFiltroEstado.Size = new Size(46, 28); lblFiltroEstado.Text = "Estado:"; lblFiltroEstado.TextAlign = ContentAlignment.MiddleLeft;
            filtroEstado.DropDownStyle = ComboBoxStyle.DropDownList; filtroEstado.Items.AddRange(new object[] { "Todos", "Activos", "Inactivos" }); filtroEstado.Location = new Point(404, 15); filtroEstado.Size = new Size(150, 25); filtroEstado.TabIndex = 1; filtroEstado.SelectedIndex = 0; filtroEstado.SelectedIndexChanged += new System.EventHandler(filtroEstado_SelectedIndexChanged);
            lblEstado.BackColor = Color.FromArgb(226, 232, 240); lblEstado.Dock = DockStyle.Bottom; lblEstado.Height = 28; lblEstado.Padding = new Padding(16, 0, 8, 0); lblEstado.Text = "Listo"; lblEstado.TextAlign = ContentAlignment.MiddleLeft;

            splitContenido.Dock = DockStyle.Fill; splitContenido.FixedPanel = FixedPanel.None; splitContenido.IsSplitterFixed = false; splitContenido.MinimumSize = new Size(900, 420); splitContenido.Panel1MinSize = 400; splitContenido.Panel2MinSize = 400; splitContenido.SplitterWidth = 6; splitContenido.Panel1.BackColor = Color.FromArgb(248, 250, 252); splitContenido.Panel1.Padding = new Padding(16); splitContenido.Panel2.BackColor = Color.FromArgb(248, 250, 252); splitContenido.Panel2.Padding = new Padding(0, 16, 16, 16);
            panelListado.Dock = DockStyle.Fill; panelListado.Controls.Add(tabla); panelListado.Controls.Add(lblListado); lblListado.Dock = DockStyle.Top; lblListado.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold); lblListado.ForeColor = Color.FromArgb(30, 41, 59); lblListado.Height = 34; lblListado.Text = "Planes"; lblListado.TextAlign = ContentAlignment.MiddleLeft;
            tabla.Dock = DockStyle.Fill; tabla.AllowUserToAddRows = false; tabla.AllowUserToDeleteRows = false; tabla.AllowUserToResizeRows = false; tabla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; tabla.BackgroundColor = Color.White; tabla.BorderStyle = BorderStyle.FixedSingle; tabla.ColumnHeadersHeight = 34; tabla.MultiSelect = false; tabla.ReadOnly = true; tabla.RowHeadersVisible = false; tabla.RowTemplate.Height = 30; tabla.SelectionMode = DataGridViewSelectionMode.FullRowSelect; tabla.SelectionChanged += new System.EventHandler(tabla_SelectionChanged); tabla.Columns.AddRange(new DataGridViewColumn[] { colId, colNombre, colPrecio, colEstado });
            colId.Name = "colId"; colId.Visible = false; colNombre.HeaderText = "Nombre"; colNombre.Name = "colNombre"; colNombre.FillWeight = 48; colNombre.MinimumWidth = 130; colPrecio.HeaderText = "Precio"; colPrecio.Name = "colPrecio"; colPrecio.FillWeight = 25; colPrecio.MinimumWidth = 90; colEstado.HeaderText = "Estado"; colEstado.Name = "colEstado"; colEstado.FillWeight = 27; colEstado.MinimumWidth = 90;

            panelDetalle.Dock = DockStyle.Fill; panelDetalle.Controls.Add(contenedorDetalle); contenedorDetalle.Dock = DockStyle.Fill; contenedorDetalle.Padding = new Padding(18); contenedorDetalle.Controls.Add(panelAcciones); contenedorDetalle.Controls.Add(contenedorCampos); contenedorDetalle.Controls.Add(lblFormulario);
            lblFormulario.Dock = DockStyle.Top; lblFormulario.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold); lblFormulario.ForeColor = Color.FromArgb(30, 41, 59); lblFormulario.Height = 38; lblFormulario.Text = "Nuevo plan"; lblFormulario.TextAlign = ContentAlignment.MiddleLeft;
            contenedorCampos.Dock = DockStyle.Top; contenedorCampos.Height = 150; contenedorCampos.ColumnCount = 2; contenedorCampos.RowCount = 3; contenedorCampos.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 116F)); contenedorCampos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F)); contenedorCampos.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F)); contenedorCampos.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F)); contenedorCampos.RowStyles.Add(new RowStyle(SizeType.Percent, 33.34F));
            AgregarCampo(contenedorCampos, lblNombre, "Nombre:", nombre, 0); AgregarCampo(contenedorCampos, lblDescripcionPlan, "Descripcion:", descripcion, 1); AgregarCampo(contenedorCampos, lblPrecio, "Precio:", precio, 2); precio.KeyPress += new KeyPressEventHandler(precio_KeyPress);
            panelAcciones.Dock = DockStyle.Top; panelAcciones.Height = 100; panelAcciones.Controls.Add(nuevo); panelAcciones.Controls.Add(guardar); panelAcciones.Controls.Add(actualizar); panelAcciones.Controls.Add(darDeBaja); panelAcciones.Controls.Add(reactivar);
            ConfigurarBoton(nuevo, "+ Nuevo", new Point(0, 8), new Size(112, 38), Color.FromArgb(79, 70, 229), Color.White, 2, nuevo_Click); ConfigurarBoton(guardar, "Guardar", new Point(120, 8), new Size(112, 38), Color.FromArgb(79, 70, 229), Color.White, 3, guardar_Click); ConfigurarBoton(actualizar, "Modificar", new Point(240, 8), new Size(112, 38), Color.FromArgb(226, 232, 240), Color.FromArgb(30, 41, 59), 4, actualizar_Click); ConfigurarBoton(darDeBaja, "Dar de baja", new Point(0, 54), new Size(112, 38), Color.FromArgb(254, 242, 242), Color.FromArgb(185, 28, 28), 5, darDeBaja_Click); ConfigurarBoton(reactivar, "Reactivar", new Point(120, 54), new Size(112, 38), Color.FromArgb(226, 232, 240), Color.FromArgb(30, 41, 59), 6, reactivar_Click);

            splitContenido.Panel1.Controls.Add(panelListado); splitContenido.Panel2.Controls.Add(panelDetalle); Controls.Add(splitContenido); Controls.Add(lblEstado); Controls.Add(barraAcciones); Controls.Add(panelEncabezado); AutoScaleMode = AutoScaleMode.Font; BackColor = Color.FromArgb(241, 245, 249); ClientSize = new Size(1100, 680); Font = new Font("Segoe UI", 9.5F); MinimumSize = new Size(900, 560); Name = "GestionPlanesFormulario"; StartPosition = FormStartPosition.CenterParent; Text = "SysGym | Planes"; Load += new System.EventHandler(GestionPlanesFormulario_Load);
            panelAcciones.ResumeLayout(false); contenedorCampos.ResumeLayout(false); contenedorDetalle.ResumeLayout(false); panelDetalle.ResumeLayout(false); panelListado.ResumeLayout(false); splitContenido.Panel2.ResumeLayout(false); splitContenido.Panel1.ResumeLayout(false); ((ISupportInitialize)(splitContenido)).EndInit(); splitContenido.ResumeLayout(false); barraAcciones.ResumeLayout(false); panelEncabezado.ResumeLayout(false); ((ISupportInitialize)(tabla)).EndInit(); ResumeLayout(false); PerformLayout(); splitContenido.SplitterDistance = 500;
        }

        private static void AgregarCampo(TableLayoutPanel tablaCampos, Label etiqueta, string texto, Control control, int fila)
        {
            etiqueta.Dock = DockStyle.Fill; etiqueta.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold); etiqueta.ForeColor = Color.FromArgb(51, 65, 85); etiqueta.Text = texto; etiqueta.TextAlign = ContentAlignment.MiddleLeft; etiqueta.Margin = new Padding(0, 2, 8, 2);
            control.Dock = DockStyle.Fill; control.Margin = new Padding(0, 3, 12, 3); control.TabIndex = fila + 1;
            tablaCampos.Controls.Add(etiqueta, 0, fila); tablaCampos.Controls.Add(control, 1, fila);
        }

        private static void ConfigurarBoton(Button boton, string texto, Point ubicacion, Size tamano, Color fondo, Color textoColor, int tabIndex, System.EventHandler evento)
        {
            boton.AutoSize = false; boton.BackColor = fondo; boton.FlatAppearance.BorderSize = 0; boton.FlatStyle = FlatStyle.Flat; boton.ForeColor = textoColor; boton.Location = ubicacion; boton.Size = tamano; boton.TabIndex = tabIndex; boton.Text = texto; boton.UseVisualStyleBackColor = false; boton.Click += evento;
        }
    }
}
