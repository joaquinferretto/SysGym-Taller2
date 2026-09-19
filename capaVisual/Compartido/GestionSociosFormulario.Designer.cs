using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Compartido
{
    partial class GestionSociosFormulario
    {
        private System.Windows.Forms.PictureBox fotoSocio;
        private System.Windows.Forms.Button btnSeleccionarFoto;
        private System.Windows.Forms.Button btnQuitarFoto;
        private System.Windows.Forms.ComboBox sexo;
        private System.Windows.Forms.Label lblSexo;

        private IContainer components;
        private Panel panelEncabezado;
        private Label lblTitulo;
        private Label lblDescripcion;
        private Button btnVolver;
        private Panel barraAcciones;
        private Label lblEstado;
        private TextBox nombre;
        private TextBox apellido;
        private TextBox dni;
        private DateTimePicker fechaNacimiento;
        private TextBox peso;
        private TextBox altura;
        private Button nuevo;
        private Button guardar;
        private Button actualizar;
        private Button calcularImc;
        private Button verRutina;
        private ComboBox filtroEstado;
        private TextBox buscador;
        private Panel panelContenido;
        private SplitContainer contenedorContenido;
        private TableLayoutPanel panelListado;
        private Label lblListado;
        private Label lblAyuda;
        private Label lblFiltro;
        private Panel panelDetalle;
        private TableLayoutPanel contenedorCampos;
        private FlowLayoutPanel panelAcciones;
        private Label lblFormulario;
        private Label lblNombre;
        private Label lblApellido;
        private Label lblDni;
        private Label lblFechaNacimiento;
        private Label lblPeso;
        private Label lblAltura;
        private DataGridView tabla;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colNombre;
        private DataGridViewTextBoxColumn colDni;
        private DataGridViewTextBoxColumn colNacimiento;
        private DataGridViewTextBoxColumn colEstado;

        protected override void Dispose(bool liberarRecursos)
        {
            if (liberarRecursos && components != null) components.Dispose();
            base.Dispose(liberarRecursos);
        }

        private void InitializeComponent()
        {
            this.fotoSocio = new System.Windows.Forms.PictureBox();
            this.btnSeleccionarFoto = new System.Windows.Forms.Button();
            this.btnQuitarFoto = new System.Windows.Forms.Button();
            this.sexo = new System.Windows.Forms.ComboBox();
            this.lblSexo = new System.Windows.Forms.Label();
            this.panelEncabezado = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.btnVolver = new System.Windows.Forms.Button();
            this.barraAcciones = new System.Windows.Forms.Panel();
            this.lblEstado = new System.Windows.Forms.Label();
            this.panelContenido = new System.Windows.Forms.Panel();
            this.contenedorContenido = new System.Windows.Forms.SplitContainer();
            this.panelListado = new System.Windows.Forms.TableLayoutPanel();
            this.lblListado = new System.Windows.Forms.Label();
            this.lblAyuda = new System.Windows.Forms.Label();
            this.buscador = new System.Windows.Forms.TextBox();
            this.lblFiltro = new System.Windows.Forms.Label();
            this.filtroEstado = new System.Windows.Forms.ComboBox();
            this.tabla = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDni = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNacimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelDetalle = new System.Windows.Forms.Panel();
            this.contenedorCampos = new System.Windows.Forms.TableLayoutPanel();
            this.verRutina = new System.Windows.Forms.Button();
            this.lblFormulario = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.nombre = new System.Windows.Forms.TextBox();
            this.lblApellido = new System.Windows.Forms.Label();
            this.apellido = new System.Windows.Forms.TextBox();
            this.lblDni = new System.Windows.Forms.Label();
            this.dni = new System.Windows.Forms.TextBox();
            this.lblFechaNacimiento = new System.Windows.Forms.Label();
            this.fechaNacimiento = new System.Windows.Forms.DateTimePicker();
            this.lblPeso = new System.Windows.Forms.Label();
            this.peso = new System.Windows.Forms.TextBox();
            this.lblAltura = new System.Windows.Forms.Label();
            this.altura = new System.Windows.Forms.TextBox();
            this.panelAcciones = new System.Windows.Forms.FlowLayoutPanel();
            this.nuevo = new System.Windows.Forms.Button();
            this.guardar = new System.Windows.Forms.Button();
            this.actualizar = new System.Windows.Forms.Button();
            this.calcularImc = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.fotoSocio)).BeginInit();
            this.panelEncabezado.SuspendLayout();
            this.panelContenido.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.contenedorContenido)).BeginInit();
            this.contenedorContenido.Panel1.SuspendLayout();
            this.contenedorContenido.Panel2.SuspendLayout();
            this.contenedorContenido.SuspendLayout();
            this.panelListado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).BeginInit();
            this.panelDetalle.SuspendLayout();
            this.contenedorCampos.SuspendLayout();
            this.panelAcciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // fotoSocio
            // 
            this.fotoSocio.BackColor = System.Drawing.Color.LightGray;
            this.fotoSocio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fotoSocio.Location = new System.Drawing.Point(3, 371);
            this.fotoSocio.Name = "fotoSocio";
            this.contenedorCampos.SetRowSpan(this.fotoSocio, 2);
            this.fotoSocio.Size = new System.Drawing.Size(120, 120);
            this.fotoSocio.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.fotoSocio.TabIndex = 1;
            this.fotoSocio.TabStop = false;
            this.fotoSocio.Disposed += new System.EventHandler(this.fotoSocio_Disposed);
            // 
            // btnSeleccionarFoto
            // 
            this.btnSeleccionarFoto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSeleccionarFoto.Location = new System.Drawing.Point(129, 371);
            this.btnSeleccionarFoto.Name = "btnSeleccionarFoto";
            this.btnSeleccionarFoto.Size = new System.Drawing.Size(236, 36);
            this.btnSeleccionarFoto.TabIndex = 2;
            this.btnSeleccionarFoto.Text = "Seleccionar foto";
            this.btnSeleccionarFoto.UseVisualStyleBackColor = true;
            this.btnSeleccionarFoto.Click += new System.EventHandler(this.btnSeleccionarFoto_Click);
            // 
            // btnQuitarFoto
            // 
            this.btnQuitarFoto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuitarFoto.Location = new System.Drawing.Point(129, 435);
            this.btnQuitarFoto.Name = "btnQuitarFoto";
            this.btnQuitarFoto.Size = new System.Drawing.Size(236, 36);
            this.btnQuitarFoto.TabIndex = 3;
            this.btnQuitarFoto.Text = "Quitar foto";
            this.btnQuitarFoto.UseVisualStyleBackColor = true;
            this.btnQuitarFoto.Click += new System.EventHandler(this.btnQuitarFoto_Click);
            // 
            // sexo
            // 
            this.sexo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sexo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sexo.Items.AddRange(new object[] {
            "Masculino",
            "Femenino"});
            this.sexo.Location = new System.Drawing.Point(126, 245);
            this.sexo.Margin = new System.Windows.Forms.Padding(0, 3, 16, 8);
            this.sexo.Name = "sexo";
            this.sexo.Size = new System.Drawing.Size(226, 25);
            this.sexo.TabIndex = 6;
            this.sexo.SelectedIndexChanged += new System.EventHandler(this.sexo_SelectedIndexChanged);
            // 
            // lblSexo
            // 
            this.lblSexo.AutoSize = true;
            this.lblSexo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSexo.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblSexo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblSexo.Location = new System.Drawing.Point(0, 242);
            this.lblSexo.Margin = new System.Windows.Forms.Padding(0, 0, 8, 8);
            this.lblSexo.Name = "lblSexo";
            this.lblSexo.Size = new System.Drawing.Size(118, 24);
            this.lblSexo.TabIndex = 12;
            this.lblSexo.Text = "Sexo:";
            this.lblSexo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelEncabezado
            // 
            this.panelEncabezado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.panelEncabezado.Controls.Add(this.lblTitulo);
            this.panelEncabezado.Controls.Add(this.lblDescripcion);
            this.panelEncabezado.Controls.Add(this.btnVolver);
            this.panelEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEncabezado.Location = new System.Drawing.Point(0, 0);
            this.panelEncabezado.Name = "panelEncabezado";
            this.panelEncabezado.Padding = new System.Windows.Forms.Padding(22, 8, 22, 8);
            this.panelEncabezado.Size = new System.Drawing.Size(1100, 56);
            this.panelEncabezado.TabIndex = 0;
            this.panelEncabezado.Visible = false;
            // 
            // lblTitulo
            // 
            this.lblTitulo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(22, 8);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(890, 40);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Socios | Gestión de socios e información personal";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDescripcion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.lblDescripcion.Location = new System.Drawing.Point(24, 48);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(890, 26);
            this.lblDescripcion.TabIndex = 1;
            this.lblDescripcion.Text = "Alta, actualizacion, baja logica y consulta de IMC";
            this.lblDescripcion.Visible = false;
            // 
            // btnVolver
            // 
            this.btnVolver.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVolver.BackColor = System.Drawing.Color.White;
            this.btnVolver.FlatAppearance.BorderSize = 0;
            this.btnVolver.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVolver.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.btnVolver.Location = new System.Drawing.Point(974, 10);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(104, 38);
            this.btnVolver.TabIndex = 2;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = false;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // barraAcciones
            // 
            this.barraAcciones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.barraAcciones.Dock = System.Windows.Forms.DockStyle.Top;
            this.barraAcciones.Location = new System.Drawing.Point(0, 56);
            this.barraAcciones.Name = "barraAcciones";
            this.barraAcciones.Size = new System.Drawing.Size(1100, 1);
            this.barraAcciones.TabIndex = 1;
            // 
            // lblEstado
            // 
            this.lblEstado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.lblEstado.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblEstado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblEstado.Location = new System.Drawing.Point(0, 650);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Padding = new System.Windows.Forms.Padding(18, 0, 12, 0);
            this.lblEstado.Size = new System.Drawing.Size(1100, 30);
            this.lblEstado.TabIndex = 3;
            this.lblEstado.Text = "Listo";
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelContenido
            // 
            this.panelContenido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelContenido.Controls.Add(this.contenedorContenido);
            this.panelContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenido.Location = new System.Drawing.Point(0, 57);
            this.panelContenido.Name = "panelContenido";
            this.panelContenido.Padding = new System.Windows.Forms.Padding(16);
            this.panelContenido.Size = new System.Drawing.Size(1100, 593);
            this.panelContenido.TabIndex = 2;
            // 
            // contenedorContenido
            // 
            this.contenedorContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contenedorContenido.Location = new System.Drawing.Point(16, 16);
            this.contenedorContenido.Name = "contenedorContenido";
            // 
            // contenedorContenido.Panel1
            // 
            this.contenedorContenido.Panel1.Controls.Add(this.panelListado);
            this.contenedorContenido.Panel1MinSize = 240;
            // 
            // contenedorContenido.Panel2
            // 
            this.contenedorContenido.Panel2.Controls.Add(this.panelDetalle);
            this.contenedorContenido.Panel2MinSize = 360;
            this.contenedorContenido.Size = new System.Drawing.Size(1068, 561);
            this.contenedorContenido.SplitterDistance = 652;
            this.contenedorContenido.SplitterWidth = 16;
            this.contenedorContenido.TabIndex = 0;
            // 
            // panelListado
            // 
            this.panelListado.BackColor = System.Drawing.Color.White;
            this.panelListado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelListado.ColumnCount = 3;
            this.panelListado.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelListado.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panelListado.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 148F));
            this.panelListado.Controls.Add(this.lblListado, 0, 0);
            this.panelListado.Controls.Add(this.lblAyuda, 0, 1);
            this.panelListado.Controls.Add(this.buscador, 0, 2);
            this.panelListado.Controls.Add(this.lblFiltro, 1, 2);
            this.panelListado.Controls.Add(this.filtroEstado, 2, 2);
            this.panelListado.Controls.Add(this.tabla, 0, 3);
            this.panelListado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelListado.Location = new System.Drawing.Point(0, 0);
            this.panelListado.Name = "panelListado";
            this.panelListado.Padding = new System.Windows.Forms.Padding(16);
            this.panelListado.RowCount = 4;
            this.panelListado.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.panelListado.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.panelListado.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.panelListado.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelListado.Size = new System.Drawing.Size(652, 561);
            this.panelListado.TabIndex = 0;
            // 
            // lblListado
            // 
            this.lblListado.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelListado.SetColumnSpan(this.lblListado, 3);
            this.lblListado.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblListado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblListado.Location = new System.Drawing.Point(19, 16);
            this.lblListado.Name = "lblListado";
            this.lblListado.Size = new System.Drawing.Size(612, 28);
            this.lblListado.TabIndex = 0;
            this.lblListado.Text = "Listado de socios";
            this.lblListado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAyuda
            // 
            this.lblAyuda.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelListado.SetColumnSpan(this.lblAyuda, 3);
            this.lblAyuda.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblAyuda.Location = new System.Drawing.Point(19, 44);
            this.lblAyuda.Name = "lblAyuda";
            this.lblAyuda.Size = new System.Drawing.Size(612, 24);
            this.lblAyuda.TabIndex = 1;
            this.lblAyuda.Text = "Busca por nombre o DNI";
            this.lblAyuda.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buscador
            // 
            this.buscador.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buscador.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.buscador.Location = new System.Drawing.Point(19, 75);
            this.buscador.Name = "buscador";
            this.buscador.Size = new System.Drawing.Size(412, 24);
            this.buscador.TabIndex = 0;
            this.buscador.TextChanged += new System.EventHandler(this.buscador_TextChanged);
            // 
            // lblFiltro
            // 
            this.lblFiltro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFiltro.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblFiltro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblFiltro.Location = new System.Drawing.Point(437, 72);
            this.lblFiltro.Name = "lblFiltro";
            this.lblFiltro.Size = new System.Drawing.Size(46, 24);
            this.lblFiltro.TabIndex = 1;
            this.lblFiltro.Text = "Estado";
            this.lblFiltro.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // filtroEstado
            // 
            this.filtroEstado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.filtroEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filtroEstado.Items.AddRange(new object[] {
            "Todos",
            "Activos",
            "Inactivos"});
            this.filtroEstado.Location = new System.Drawing.Point(489, 75);
            this.filtroEstado.Name = "filtroEstado";
            this.filtroEstado.Size = new System.Drawing.Size(142, 25);
            this.filtroEstado.TabIndex = 2;
            this.filtroEstado.SelectedIndexChanged += new System.EventHandler(this.filtroEstado_SelectedIndexChanged);
            // 
            // tabla
            // 
            this.tabla.AllowUserToAddRows = false;
            this.tabla.AllowUserToDeleteRows = false;
            this.tabla.AllowUserToResizeRows = false;
            this.tabla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabla.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tabla.BackgroundColor = System.Drawing.Color.White;
            this.tabla.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tabla.ColumnHeadersHeight = 46;
            this.tabla.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colNombre,
            this.colDni,
            this.colNacimiento,
            this.colEstado});
            this.panelListado.SetColumnSpan(this.tabla, 3);
            this.tabla.Location = new System.Drawing.Point(19, 121);
            this.tabla.MultiSelect = false;
            this.tabla.Name = "tabla";
            this.tabla.ReadOnly = true;
            this.tabla.RowHeadersVisible = false;
            this.tabla.RowHeadersWidth = 51;
            this.tabla.RowTemplate.Height = 30;
            this.tabla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tabla.Size = new System.Drawing.Size(612, 419);
            this.tabla.TabIndex = 3;
            this.tabla.SelectionChanged += new System.EventHandler(this.tabla_SelectionChanged);
            // 
            // colId
            // 
            this.colId.HeaderText = "Id";
            this.colId.MinimumWidth = 6;
            this.colId.Name = "colId";
            this.colId.ReadOnly = true;
            this.colId.Visible = false;
            // 
            // colNombre
            // 
            this.colNombre.FillWeight = 150F;
            this.colNombre.HeaderText = "Nombre";
            this.colNombre.MinimumWidth = 180;
            this.colNombre.Name = "colNombre";
            this.colNombre.ReadOnly = true;
            // 
            // colDni
            // 
            this.colDni.FillWeight = 95F;
            this.colDni.HeaderText = "DNI";
            this.colDni.MinimumWidth = 110;
            this.colDni.Name = "colDni";
            this.colDni.ReadOnly = true;
            // 
            // colNacimiento
            // 
            this.colNacimiento.FillWeight = 95F;
            this.colNacimiento.HeaderText = "Nacimiento";
            this.colNacimiento.MinimumWidth = 120;
            this.colNacimiento.Name = "colNacimiento";
            this.colNacimiento.ReadOnly = true;
            // 
            // colEstado
            // 
            this.colEstado.FillWeight = 70F;
            this.colEstado.HeaderText = "Estado";
            this.colEstado.MinimumWidth = 90;
            this.colEstado.Name = "colEstado";
            this.colEstado.ReadOnly = true;
            // 
            // panelDetalle
            // 
            this.panelDetalle.AutoScroll = true;
            this.panelDetalle.Controls.Add(this.contenedorCampos);
            this.panelDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDetalle.Location = new System.Drawing.Point(0, 0);
            this.panelDetalle.Name = "panelDetalle";
            this.panelDetalle.Padding = new System.Windows.Forms.Padding(16);
            this.panelDetalle.Size = new System.Drawing.Size(400, 561);
            this.panelDetalle.TabIndex = 1;
            // 
            // contenedorCampos
            // 
            this.contenedorCampos.AutoSize = true;
            this.contenedorCampos.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.contenedorCampos.ColumnCount = 2;
            this.contenedorCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.contenedorCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.contenedorCampos.Controls.Add(this.lblFormulario, 0, 0);
            this.contenedorCampos.Controls.Add(this.lblNombre, 0, 1);
            this.contenedorCampos.Controls.Add(this.nombre, 1, 1);
            this.contenedorCampos.Controls.Add(this.lblApellido, 0, 2);
            this.contenedorCampos.Controls.Add(this.apellido, 1, 2);
            this.contenedorCampos.Controls.Add(this.lblDni, 0, 3);
            this.contenedorCampos.Controls.Add(this.dni, 1, 3);
            this.contenedorCampos.Controls.Add(this.lblFechaNacimiento, 0, 4);
            this.contenedorCampos.Controls.Add(this.fechaNacimiento, 1, 4);
            this.contenedorCampos.Controls.Add(this.lblPeso, 0, 5);
            this.contenedorCampos.Controls.Add(this.peso, 1, 5);
            this.contenedorCampos.Controls.Add(this.lblAltura, 0, 6);
            this.contenedorCampos.Controls.Add(this.altura, 1, 6);
            this.contenedorCampos.Controls.Add(this.lblSexo, 0, 7);
            this.contenedorCampos.Controls.Add(this.sexo, 1, 7);
            this.contenedorCampos.Controls.Add(this.panelAcciones, 0, 8);
            this.contenedorCampos.Controls.Add(this.fotoSocio, 0, 9);
            this.contenedorCampos.Controls.Add(this.btnSeleccionarFoto, 1, 9);
            this.contenedorCampos.Controls.Add(this.btnQuitarFoto, 1, 10);
            this.contenedorCampos.Controls.Add(this.verRutina, 0, 11);
            this.contenedorCampos.Dock = System.Windows.Forms.DockStyle.Top;
            this.contenedorCampos.Location = new System.Drawing.Point(16, 16);
            this.contenedorCampos.Name = "contenedorCampos";
            this.contenedorCampos.RowCount = 12;
            this.contenedorCampos.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.contenedorCampos.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.contenedorCampos.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.contenedorCampos.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.contenedorCampos.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.contenedorCampos.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.contenedorCampos.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.contenedorCampos.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.contenedorCampos.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.contenedorCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.contenedorCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.contenedorCampos.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.contenedorCampos.Size = new System.Drawing.Size(368, 540);
            this.contenedorCampos.TabIndex = 0;
            // 
            // verRutina
            // 
            this.verRutina.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.contenedorCampos.SetColumnSpan(this.verRutina, 2);
            this.verRutina.FlatAppearance.BorderSize = 0;
            this.verRutina.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.verRutina.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.verRutina.Location = new System.Drawing.Point(3, 499);
            this.verRutina.Name = "verRutina";
            this.verRutina.Size = new System.Drawing.Size(180, 38);
            this.verRutina.TabIndex = 4;
            this.verRutina.Text = "Ver rutina semanal";
            this.verRutina.UseVisualStyleBackColor = false;
            this.verRutina.Click += new System.EventHandler(this.verRutina_Click);
            // 
            // lblFormulario
            // 
            this.contenedorCampos.SetColumnSpan(this.lblFormulario, 2);
            this.lblFormulario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFormulario.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblFormulario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblFormulario.Location = new System.Drawing.Point(3, 0);
            this.lblFormulario.MinimumSize = new System.Drawing.Size(0, 32);
            this.lblFormulario.Name = "lblFormulario";
            this.lblFormulario.Size = new System.Drawing.Size(362, 32);
            this.lblFormulario.TabIndex = 0;
            this.lblFormulario.Text = "Nuevo socio";
            this.lblFormulario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNombre.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblNombre.Location = new System.Drawing.Point(0, 32);
            this.lblNombre.Margin = new System.Windows.Forms.Padding(0, 0, 8, 8);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(118, 27);
            this.lblNombre.TabIndex = 0;
            this.lblNombre.Text = "Nombre:";
            this.lblNombre.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nombre
            // 
            this.nombre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nombre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nombre.Location = new System.Drawing.Point(126, 35);
            this.nombre.Margin = new System.Windows.Forms.Padding(0, 3, 16, 8);
            this.nombre.MaxLength = 100;
            this.nombre.Name = "nombre";
            this.nombre.Size = new System.Drawing.Size(226, 24);
            this.nombre.TabIndex = 1;
            this.nombre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.nombre_KeyPress);
            // 
            // lblApellido
            // 
            this.lblApellido.AutoSize = true;
            this.lblApellido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblApellido.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblApellido.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblApellido.Location = new System.Drawing.Point(0, 67);
            this.lblApellido.Margin = new System.Windows.Forms.Padding(0, 0, 8, 8);
            this.lblApellido.Name = "lblApellido";
            this.lblApellido.Size = new System.Drawing.Size(118, 27);
            this.lblApellido.TabIndex = 2;
            this.lblApellido.Text = "Apellido:";
            this.lblApellido.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // apellido
            // 
            this.apellido.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.apellido.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.apellido.Location = new System.Drawing.Point(126, 70);
            this.apellido.Margin = new System.Windows.Forms.Padding(0, 3, 16, 8);
            this.apellido.MaxLength = 100;
            this.apellido.Name = "apellido";
            this.apellido.Size = new System.Drawing.Size(226, 24);
            this.apellido.TabIndex = 3;
            this.apellido.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.apellido_KeyPress);
            // 
            // lblDni
            // 
            this.lblDni.AutoSize = true;
            this.lblDni.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDni.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblDni.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblDni.Location = new System.Drawing.Point(0, 102);
            this.lblDni.Margin = new System.Windows.Forms.Padding(0, 0, 8, 8);
            this.lblDni.Name = "lblDni";
            this.lblDni.Size = new System.Drawing.Size(118, 27);
            this.lblDni.TabIndex = 4;
            this.lblDni.Text = "DNI:";
            this.lblDni.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dni
            // 
            this.dni.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dni.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dni.Location = new System.Drawing.Point(126, 105);
            this.dni.Margin = new System.Windows.Forms.Padding(0, 3, 16, 8);
            this.dni.MaxLength = 20;
            this.dni.Name = "dni";
            this.dni.Size = new System.Drawing.Size(226, 24);
            this.dni.TabIndex = 5;
            this.dni.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dni_KeyPress);
            // 
            // lblFechaNacimiento
            // 
            this.lblFechaNacimiento.AutoSize = true;
            this.lblFechaNacimiento.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFechaNacimiento.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblFechaNacimiento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblFechaNacimiento.Location = new System.Drawing.Point(0, 137);
            this.lblFechaNacimiento.Margin = new System.Windows.Forms.Padding(0, 0, 8, 8);
            this.lblFechaNacimiento.Name = "lblFechaNacimiento";
            this.lblFechaNacimiento.Size = new System.Drawing.Size(118, 27);
            this.lblFechaNacimiento.TabIndex = 6;
            this.lblFechaNacimiento.Text = "Nacimiento:";
            this.lblFechaNacimiento.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // fechaNacimiento
            // 
            this.fechaNacimiento.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fechaNacimiento.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.fechaNacimiento.Location = new System.Drawing.Point(126, 140);
            this.fechaNacimiento.Margin = new System.Windows.Forms.Padding(0, 3, 16, 8);
            this.fechaNacimiento.Name = "fechaNacimiento";
            this.fechaNacimiento.ShowCheckBox = true;
            this.fechaNacimiento.Size = new System.Drawing.Size(226, 24);
            this.fechaNacimiento.TabIndex = 7;
            // 
            // lblPeso
            // 
            this.lblPeso.AutoSize = true;
            this.lblPeso.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPeso.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblPeso.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblPeso.Location = new System.Drawing.Point(0, 172);
            this.lblPeso.Margin = new System.Windows.Forms.Padding(0, 0, 8, 8);
            this.lblPeso.Name = "lblPeso";
            this.lblPeso.Size = new System.Drawing.Size(118, 27);
            this.lblPeso.TabIndex = 8;
            this.lblPeso.Text = "Peso (kg):";
            this.lblPeso.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // peso
            // 
            this.peso.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.peso.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.peso.Location = new System.Drawing.Point(126, 175);
            this.peso.Margin = new System.Windows.Forms.Padding(0, 3, 16, 8);
            this.peso.Name = "peso";
            this.peso.Size = new System.Drawing.Size(226, 24);
            this.peso.TabIndex = 9;
            this.peso.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.peso_KeyPress);
            // 
            // lblAltura
            // 
            this.lblAltura.AutoSize = true;
            this.lblAltura.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAltura.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblAltura.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblAltura.Location = new System.Drawing.Point(0, 207);
            this.lblAltura.Margin = new System.Windows.Forms.Padding(0, 0, 8, 8);
            this.lblAltura.Name = "lblAltura";
            this.lblAltura.Size = new System.Drawing.Size(118, 27);
            this.lblAltura.TabIndex = 10;
            this.lblAltura.Text = "Altura (m):";
            this.lblAltura.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // altura
            // 
            this.altura.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.altura.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.altura.Location = new System.Drawing.Point(126, 210);
            this.altura.Margin = new System.Windows.Forms.Padding(0, 3, 16, 8);
            this.altura.Name = "altura";
            this.altura.Size = new System.Drawing.Size(226, 24);
            this.altura.TabIndex = 11;
            this.altura.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.altura_KeyPress);
            // 
            // panelAcciones
            // 
            this.panelAcciones.AutoSize = true;
            this.panelAcciones.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.contenedorCampos.SetColumnSpan(this.panelAcciones, 2);
            this.panelAcciones.Controls.Add(this.nuevo);
            this.panelAcciones.Controls.Add(this.guardar);
            this.panelAcciones.Controls.Add(this.actualizar);
            this.panelAcciones.Controls.Add(this.calcularImc);
            this.panelAcciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAcciones.Location = new System.Drawing.Point(3, 277);
            this.panelAcciones.Name = "panelAcciones";
            this.panelAcciones.Size = new System.Drawing.Size(362, 88);
            this.panelAcciones.TabIndex = 2;
            // 
            // nuevo
            // 
            this.nuevo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.nuevo.FlatAppearance.BorderSize = 0;
            this.nuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nuevo.ForeColor = System.Drawing.Color.White;
            this.nuevo.Location = new System.Drawing.Point(0, 0);
            this.nuevo.Margin = new System.Windows.Forms.Padding(0, 0, 4, 6);
            this.nuevo.Name = "nuevo";
            this.nuevo.Size = new System.Drawing.Size(108, 38);
            this.nuevo.TabIndex = 0;
            this.nuevo.Text = "+ Nuevo socio";
            this.nuevo.UseVisualStyleBackColor = false;
            this.nuevo.Click += new System.EventHandler(this.nuevo_Click);
            // 
            // guardar
            // 
            this.guardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.guardar.FlatAppearance.BorderSize = 0;
            this.guardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.guardar.ForeColor = System.Drawing.Color.White;
            this.guardar.Location = new System.Drawing.Point(112, 0);
            this.guardar.Margin = new System.Windows.Forms.Padding(0, 0, 4, 6);
            this.guardar.Name = "guardar";
            this.guardar.Size = new System.Drawing.Size(108, 38);
            this.guardar.TabIndex = 1;
            this.guardar.Text = "Guardar";
            this.guardar.UseVisualStyleBackColor = false;
            this.guardar.Click += new System.EventHandler(this.guardar_Click);
            // 
            // actualizar
            // 
            this.actualizar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.actualizar.FlatAppearance.BorderSize = 0;
            this.actualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.panelAcciones.SetFlowBreak(this.actualizar, true);
            this.actualizar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.actualizar.Location = new System.Drawing.Point(224, 0);
            this.actualizar.Margin = new System.Windows.Forms.Padding(0, 0, 4, 6);
            this.actualizar.Name = "actualizar";
            this.actualizar.Size = new System.Drawing.Size(108, 38);
            this.actualizar.TabIndex = 2;
            this.actualizar.Text = "Actualizar";
            this.actualizar.UseVisualStyleBackColor = false;
            this.actualizar.Click += new System.EventHandler(this.actualizar_Click);
            // 
            // calcularImc
            // 
            this.calcularImc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.calcularImc.FlatAppearance.BorderSize = 0;
            this.calcularImc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.calcularImc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.calcularImc.Location = new System.Drawing.Point(0, 44);
            this.calcularImc.Margin = new System.Windows.Forms.Padding(0, 0, 4, 6);
            this.calcularImc.Name = "calcularImc";
            this.calcularImc.Size = new System.Drawing.Size(108, 38);
            this.calcularImc.TabIndex = 3;
            this.calcularImc.Text = "Calcular IMC";
            this.calcularImc.UseVisualStyleBackColor = false;
            this.calcularImc.Click += new System.EventHandler(this.calcularImc_Click);
            // 
            // GestionSociosFormulario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1100, 680);
            this.Controls.Add(this.panelContenido);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.barraAcciones);
            this.Controls.Add(this.panelEncabezado);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.MinimumSize = new System.Drawing.Size(900, 560);
            this.Name = "GestionSociosFormulario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SysGym | Socios";
            this.Load += new System.EventHandler(this.GestionSociosFormulario_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fotoSocio)).EndInit();
            this.panelEncabezado.ResumeLayout(false);
            this.panelContenido.ResumeLayout(false);
            this.contenedorContenido.Panel1.ResumeLayout(false);
            this.contenedorContenido.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.contenedorContenido)).EndInit();
            this.contenedorContenido.ResumeLayout(false);
            this.panelListado.ResumeLayout(false);
            this.panelListado.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).EndInit();
            this.panelDetalle.ResumeLayout(false);
            this.panelDetalle.PerformLayout();
            this.contenedorCampos.ResumeLayout(false);
            this.contenedorCampos.PerformLayout();
            this.panelAcciones.ResumeLayout(false);
            this.ResumeLayout(false);

        }

    }
}
