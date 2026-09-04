using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Recepcionista
{
    partial class GestionMembresiasForm
    {
        private IContainer components;
        private Panel panelEncabezado; private Label lblTitulo; private Label lblDescripcion; private Button btnVolver; private FlowLayoutPanel barraAcciones; private Label lblEstado; private Panel panelContenido; private TableLayoutPanel layoutContenido; private Panel panelListado; private Label lblListado; private Label lblAyuda; private Panel panelDetalle; private TableLayoutPanel layoutDetalle; private Label lblFormulario; private TableLayoutPanel layoutCampos; private Panel panelAcciones; private Label lblSocio; private Label lblPlan; private Label lblInicio; private Label lblVencimiento; private DataGridView tabla; private DataGridViewTextBoxColumn colId; private DataGridViewTextBoxColumn colSocio; private DataGridViewTextBoxColumn colDni; private DataGridViewTextBoxColumn colPlan; private DataGridViewTextBoxColumn colInicio; private DataGridViewTextBoxColumn colVencimiento; private DataGridViewTextBoxColumn colEstado;
        private TextBox buscador; private ComboBox socio; private ComboBox plan; private DateTimePicker inicio; private DateTimePicker vencimiento; private Button nuevo; private Button crear; private Button actualizar; private Button habilitar; private Button deshabilitar; private Button generarCuota;

        protected override void Dispose(bool disposing) { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            this.panelEncabezado = new System.Windows.Forms.Panel();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.btnVolver = new System.Windows.Forms.Button();
            this.barraAcciones = new System.Windows.Forms.FlowLayoutPanel();
            this.lblEstado = new System.Windows.Forms.Label();
            this.panelContenido = new System.Windows.Forms.Panel();
            this.layoutContenido = new System.Windows.Forms.TableLayoutPanel();
            this.panelListado = new System.Windows.Forms.Panel();
            this.tabla = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSocio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDni = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPlan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colInicio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVencimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buscador = new System.Windows.Forms.TextBox();
            this.lblAyuda = new System.Windows.Forms.Label();
            this.lblListado = new System.Windows.Forms.Label();
            this.panelDetalle = new System.Windows.Forms.Panel();
            this.layoutDetalle = new System.Windows.Forms.TableLayoutPanel();
            this.lblFormulario = new System.Windows.Forms.Label();
            this.layoutCampos = new System.Windows.Forms.TableLayoutPanel();
            this.lblSocio = new System.Windows.Forms.Label();
            this.socio = new System.Windows.Forms.ComboBox();
            this.lblPlan = new System.Windows.Forms.Label();
            this.plan = new System.Windows.Forms.ComboBox();
            this.lblInicio = new System.Windows.Forms.Label();
            this.inicio = new System.Windows.Forms.DateTimePicker();
            this.lblVencimiento = new System.Windows.Forms.Label();
            this.vencimiento = new System.Windows.Forms.DateTimePicker();
            this.panelAcciones = new System.Windows.Forms.Panel();
            this.nuevo = new System.Windows.Forms.Button();
            this.crear = new System.Windows.Forms.Button();
            this.actualizar = new System.Windows.Forms.Button();
            this.habilitar = new System.Windows.Forms.Button();
            this.deshabilitar = new System.Windows.Forms.Button();
            this.generarCuota = new System.Windows.Forms.Button();
            this.panelEncabezado.SuspendLayout();
            this.panelContenido.SuspendLayout();
            this.layoutContenido.SuspendLayout();
            this.panelListado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).BeginInit();
            this.panelDetalle.SuspendLayout();
            this.layoutDetalle.SuspendLayout();
            this.layoutCampos.SuspendLayout();
            this.panelAcciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEncabezado
            // 
            this.panelEncabezado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(150)))), ((int)(((byte)(105)))));
            this.panelEncabezado.Controls.Add(this.lblDescripcion);
            this.panelEncabezado.Controls.Add(this.lblTitulo);
            this.panelEncabezado.Controls.Add(this.btnVolver);
            this.panelEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEncabezado.Location = new System.Drawing.Point(0, 0);
            this.panelEncabezado.Name = "panelEncabezado";
            this.panelEncabezado.Size = new System.Drawing.Size(940, 80);
            this.panelEncabezado.TabIndex = 0;
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.lblDescripcion.Location = new System.Drawing.Point(24, 47);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(290, 17);
            this.lblDescripcion.TabIndex = 1;
            this.lblDescripcion.Text = "Asignacion de planes a socios, vigencia y cuotas";
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(22, 10);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(152, 32);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Membresias";
            // 
            // btnVolver
            // 
            this.btnVolver.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVolver.BackColor = System.Drawing.Color.White;
            this.btnVolver.FlatAppearance.BorderSize = 0;
            this.btnVolver.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVolver.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(150)))), ((int)(((byte)(105)))));
            this.btnVolver.Location = new System.Drawing.Point(770, 22);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(92, 34);
            this.btnVolver.TabIndex = 2;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = false;
            // 
            // barraAcciones
            // 
            this.barraAcciones.BackColor = System.Drawing.Color.White;
            this.barraAcciones.Dock = System.Windows.Forms.DockStyle.Top;
            this.barraAcciones.Location = new System.Drawing.Point(0, 80);
            this.barraAcciones.Name = "barraAcciones";
            this.barraAcciones.Padding = new System.Windows.Forms.Padding(16, 8, 16, 8);
            this.barraAcciones.Size = new System.Drawing.Size(940, 52);
            this.barraAcciones.TabIndex = 1;
            this.barraAcciones.WrapContents = false;
            // 
            // lblEstado
            // 
            this.lblEstado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.lblEstado.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblEstado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblEstado.Location = new System.Drawing.Point(0, 648);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Padding = new System.Windows.Forms.Padding(18, 8, 8, 0);
            this.lblEstado.Size = new System.Drawing.Size(940, 32);
            this.lblEstado.TabIndex = 3;
            this.lblEstado.Text = "Listo";
            // 
            // panelContenido
            // 
            this.panelContenido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelContenido.Controls.Add(this.layoutContenido);
            this.panelContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenido.Location = new System.Drawing.Point(0, 132);
            this.panelContenido.Name = "panelContenido";
            this.panelContenido.Padding = new System.Windows.Forms.Padding(12);
            this.panelContenido.Size = new System.Drawing.Size(940, 516);
            this.panelContenido.TabIndex = 2;
            // 
            // layoutContenido
            // 
            this.layoutContenido.ColumnCount = 2;
            this.layoutContenido.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 57F));
            this.layoutContenido.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43F));
            this.layoutContenido.Controls.Add(this.panelListado, 0, 0);
            this.layoutContenido.Controls.Add(this.panelDetalle, 1, 0);
            this.layoutContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutContenido.Location = new System.Drawing.Point(12, 12);
            this.layoutContenido.Name = "layoutContenido";
            this.layoutContenido.RowCount = 1;
            this.layoutContenido.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutContenido.Size = new System.Drawing.Size(916, 492);
            this.layoutContenido.TabIndex = 0;
            // 
            // panelListado
            // 
            this.panelListado.BackColor = System.Drawing.Color.White;
            this.panelListado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelListado.Controls.Add(this.tabla);
            this.panelListado.Controls.Add(this.buscador);
            this.panelListado.Controls.Add(this.lblAyuda);
            this.panelListado.Controls.Add(this.lblListado);
            this.panelListado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelListado.Location = new System.Drawing.Point(3, 3);
            this.panelListado.Name = "panelListado";
            this.panelListado.Padding = new System.Windows.Forms.Padding(16);
            this.panelListado.Size = new System.Drawing.Size(516, 486);
            this.panelListado.TabIndex = 0;
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
            this.tabla.ColumnHeadersHeight = 38;
            this.tabla.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colSocio,
            this.colDni,
            this.colPlan,
            this.colInicio,
            this.colVencimiento,
            this.colEstado});
            this.tabla.Location = new System.Drawing.Point(16, 101);
            this.tabla.MultiSelect = false;
            this.tabla.Name = "tabla";
            this.tabla.ReadOnly = true;
            this.tabla.RowHeadersVisible = false;
            this.tabla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tabla.Size = new System.Drawing.Size(479, 384);
            this.tabla.TabIndex = 3;
            this.tabla.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tabla_CellContentClick);
            // 
            // colId
            // 
            this.colId.HeaderText = "Id";
            this.colId.Name = "colId";
            this.colId.ReadOnly = true;
            this.colId.Visible = false;
            // 
            // colSocio
            // 
            this.colSocio.HeaderText = "Socio";
            this.colSocio.Name = "colSocio";
            this.colSocio.ReadOnly = true;
            // 
            // colDni
            // 
            this.colDni.HeaderText = "DNI";
            this.colDni.Name = "colDni";
            this.colDni.ReadOnly = true;
            // 
            // colPlan
            // 
            this.colPlan.HeaderText = "Plan";
            this.colPlan.Name = "colPlan";
            this.colPlan.ReadOnly = true;
            // 
            // colInicio
            // 
            this.colInicio.HeaderText = "Inicio";
            this.colInicio.Name = "colInicio";
            this.colInicio.ReadOnly = true;
            // 
            // colVencimiento
            // 
            this.colVencimiento.HeaderText = "Vencimiento";
            this.colVencimiento.Name = "colVencimiento";
            this.colVencimiento.ReadOnly = true;
            // 
            // colEstado
            // 
            this.colEstado.HeaderText = "Estado";
            this.colEstado.Name = "colEstado";
            this.colEstado.ReadOnly = true;
            // 
            // buscador
            // 
            this.buscador.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buscador.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.buscador.Location = new System.Drawing.Point(16, 70);
            this.buscador.Name = "buscador";
            this.buscador.Size = new System.Drawing.Size(479, 24);
            this.buscador.TabIndex = 2;
            // 
            // lblAyuda
            // 
            this.lblAyuda.AutoSize = true;
            this.lblAyuda.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblAyuda.Location = new System.Drawing.Point(16, 42);
            this.lblAyuda.Name = "lblAyuda";
            this.lblAyuda.Size = new System.Drawing.Size(171, 17);
            this.lblAyuda.TabIndex = 1;
            this.lblAyuda.Text = "Busca por socio, DNI o plan";
            // 
            // lblListado
            // 
            this.lblListado.AutoSize = true;
            this.lblListado.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblListado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblListado.Location = new System.Drawing.Point(16, 14);
            this.lblListado.Name = "lblListado";
            this.lblListado.Size = new System.Drawing.Size(91, 20);
            this.lblListado.TabIndex = 0;
            this.lblListado.Text = "Membresias";
            // 
            // panelDetalle
            // 
            this.panelDetalle.BackColor = System.Drawing.Color.White;
            this.panelDetalle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDetalle.Controls.Add(this.layoutDetalle);
            this.panelDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDetalle.Location = new System.Drawing.Point(525, 3);
            this.panelDetalle.Name = "panelDetalle";
            this.panelDetalle.Padding = new System.Windows.Forms.Padding(16);
            this.panelDetalle.Size = new System.Drawing.Size(388, 486);
            this.panelDetalle.TabIndex = 1;
            // 
            // layoutDetalle
            // 
            this.layoutDetalle.ColumnCount = 1;
            this.layoutDetalle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layoutDetalle.Controls.Add(this.lblFormulario, 0, 0);
            this.layoutDetalle.Controls.Add(this.layoutCampos, 0, 1);
            this.layoutDetalle.Controls.Add(this.panelAcciones, 0, 2);
            this.layoutDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutDetalle.Location = new System.Drawing.Point(16, 16);
            this.layoutDetalle.Name = "layoutDetalle";
            this.layoutDetalle.RowCount = 3;
            this.layoutDetalle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.layoutDetalle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutDetalle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.layoutDetalle.Size = new System.Drawing.Size(354, 452);
            this.layoutDetalle.TabIndex = 0;
            // 
            // lblFormulario
            // 
            this.lblFormulario.AutoSize = true;
            this.lblFormulario.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblFormulario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblFormulario.Location = new System.Drawing.Point(3, 0);
            this.lblFormulario.Name = "lblFormulario";
            this.lblFormulario.Size = new System.Drawing.Size(141, 21);
            this.lblFormulario.TabIndex = 0;
            this.lblFormulario.Text = "Nueva membresia";
            // 
            // layoutCampos
            // 
            this.layoutCampos.ColumnCount = 2;
            this.layoutCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42F));
            this.layoutCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58F));
            this.layoutCampos.Controls.Add(this.lblSocio, 0, 0);
            this.layoutCampos.Controls.Add(this.socio, 1, 0);
            this.layoutCampos.Controls.Add(this.lblPlan, 0, 1);
            this.layoutCampos.Controls.Add(this.plan, 1, 1);
            this.layoutCampos.Controls.Add(this.lblInicio, 0, 2);
            this.layoutCampos.Controls.Add(this.inicio, 1, 2);
            this.layoutCampos.Controls.Add(this.lblVencimiento, 0, 3);
            this.layoutCampos.Controls.Add(this.vencimiento, 1, 3);
            this.layoutCampos.Dock = System.Windows.Forms.DockStyle.Top;
            this.layoutCampos.Location = new System.Drawing.Point(3, 37);
            this.layoutCampos.Name = "layoutCampos";
            this.layoutCampos.RowCount = 4;
            this.layoutCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.layoutCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.layoutCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.layoutCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.layoutCampos.Size = new System.Drawing.Size(348, 176);
            this.layoutCampos.TabIndex = 1;
            // 
            // lblSocio
            // 
            this.lblSocio.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSocio.AutoSize = true;
            this.lblSocio.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblSocio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblSocio.Location = new System.Drawing.Point(3, 14);
            this.lblSocio.Name = "lblSocio";
            this.lblSocio.Size = new System.Drawing.Size(40, 15);
            this.lblSocio.TabIndex = 0;
            this.lblSocio.Text = "Socio:";
            // 
            // socio
            // 
            this.socio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.socio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.socio.Location = new System.Drawing.Point(146, 4);
            this.socio.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.socio.Name = "socio";
            this.socio.Size = new System.Drawing.Size(202, 25);
            this.socio.TabIndex = 1;
            // 
            // lblPlan
            // 
            this.lblPlan.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPlan.AutoSize = true;
            this.lblPlan.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblPlan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblPlan.Location = new System.Drawing.Point(3, 58);
            this.lblPlan.Name = "lblPlan";
            this.lblPlan.Size = new System.Drawing.Size(33, 15);
            this.lblPlan.TabIndex = 2;
            this.lblPlan.Text = "Plan:";
            // 
            // plan
            // 
            this.plan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.plan.Location = new System.Drawing.Point(146, 48);
            this.plan.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.plan.Name = "plan";
            this.plan.Size = new System.Drawing.Size(202, 25);
            this.plan.TabIndex = 3;
            // 
            // lblInicio
            // 
            this.lblInicio.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblInicio.AutoSize = true;
            this.lblInicio.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblInicio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblInicio.Location = new System.Drawing.Point(3, 102);
            this.lblInicio.Name = "lblInicio";
            this.lblInicio.Size = new System.Drawing.Size(40, 15);
            this.lblInicio.TabIndex = 4;
            this.lblInicio.Text = "Inicio:";
            // 
            // inicio
            // 
            this.inicio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.inicio.Location = new System.Drawing.Point(146, 92);
            this.inicio.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.inicio.Name = "inicio";
            this.inicio.Size = new System.Drawing.Size(202, 24);
            this.inicio.TabIndex = 5;
            // 
            // lblVencimiento
            // 
            this.lblVencimiento.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblVencimiento.AutoSize = true;
            this.lblVencimiento.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblVencimiento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblVencimiento.Location = new System.Drawing.Point(3, 146);
            this.lblVencimiento.Name = "lblVencimiento";
            this.lblVencimiento.Size = new System.Drawing.Size(77, 15);
            this.lblVencimiento.TabIndex = 6;
            this.lblVencimiento.Text = "Vencimiento:";
            // 
            // vencimiento
            // 
            this.vencimiento.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vencimiento.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.vencimiento.Location = new System.Drawing.Point(146, 136);
            this.vencimiento.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.vencimiento.Name = "vencimiento";
            this.vencimiento.Size = new System.Drawing.Size(202, 24);
            this.vencimiento.TabIndex = 7;
            // 
            // panelAcciones
            // 
            this.panelAcciones.Controls.Add(this.nuevo);
            this.panelAcciones.Controls.Add(this.crear);
            this.panelAcciones.Controls.Add(this.actualizar);
            this.panelAcciones.Controls.Add(this.habilitar);
            this.panelAcciones.Controls.Add(this.deshabilitar);
            this.panelAcciones.Controls.Add(this.generarCuota);
            this.panelAcciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAcciones.Location = new System.Drawing.Point(3, 295);
            this.panelAcciones.Name = "panelAcciones";
            this.panelAcciones.Size = new System.Drawing.Size(348, 154);
            this.panelAcciones.TabIndex = 2;
            // 
            // nuevo
            // 
            this.nuevo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(150)))), ((int)(((byte)(105)))));
            this.nuevo.FlatAppearance.BorderSize = 0;
            this.nuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nuevo.ForeColor = System.Drawing.Color.White;
            this.nuevo.Location = new System.Drawing.Point(0, 2);
            this.nuevo.Name = "nuevo";
            this.nuevo.Size = new System.Drawing.Size(100, 32);
            this.nuevo.TabIndex = 0;
            this.nuevo.Text = "+ Nueva";
            this.nuevo.UseVisualStyleBackColor = false;
            // 
            // crear
            // 
            this.crear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(150)))), ((int)(((byte)(105)))));
            this.crear.FlatAppearance.BorderSize = 0;
            this.crear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.crear.ForeColor = System.Drawing.Color.White;
            this.crear.Location = new System.Drawing.Point(0, 42);
            this.crear.Name = "crear";
            this.crear.Size = new System.Drawing.Size(100, 32);
            this.crear.TabIndex = 1;
            this.crear.Text = "Crear";
            this.crear.UseVisualStyleBackColor = false;
            // 
            // actualizar
            // 
            this.actualizar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.actualizar.FlatAppearance.BorderSize = 0;
            this.actualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.actualizar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.actualizar.Location = new System.Drawing.Point(104, 42);
            this.actualizar.Name = "actualizar";
            this.actualizar.Size = new System.Drawing.Size(100, 32);
            this.actualizar.TabIndex = 2;
            this.actualizar.Text = "Actualizar";
            this.actualizar.UseVisualStyleBackColor = false;
            // 
            // habilitar
            // 
            this.habilitar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.habilitar.FlatAppearance.BorderSize = 0;
            this.habilitar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.habilitar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.habilitar.Location = new System.Drawing.Point(0, 82);
            this.habilitar.Name = "habilitar";
            this.habilitar.Size = new System.Drawing.Size(100, 32);
            this.habilitar.TabIndex = 3;
            this.habilitar.Text = "Habilitar";
            this.habilitar.UseVisualStyleBackColor = false;
            // 
            // deshabilitar
            // 
            this.deshabilitar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.deshabilitar.FlatAppearance.BorderSize = 0;
            this.deshabilitar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deshabilitar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.deshabilitar.Location = new System.Drawing.Point(104, 82);
            this.deshabilitar.Name = "deshabilitar";
            this.deshabilitar.Size = new System.Drawing.Size(100, 32);
            this.deshabilitar.TabIndex = 4;
            this.deshabilitar.Text = "Deshabilitar";
            this.deshabilitar.UseVisualStyleBackColor = false;
            // 
            // generarCuota
            // 
            this.generarCuota.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.generarCuota.FlatAppearance.BorderSize = 0;
            this.generarCuota.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.generarCuota.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.generarCuota.Location = new System.Drawing.Point(0, 122);
            this.generarCuota.Name = "generarCuota";
            this.generarCuota.Size = new System.Drawing.Size(100, 32);
            this.generarCuota.TabIndex = 5;
            this.generarCuota.Text = "Generar cuota";
            this.generarCuota.UseVisualStyleBackColor = false;
            // 
            // GestionMembresiasForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(940, 680);
            this.Controls.Add(this.panelContenido);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.barraAcciones);
            this.Controls.Add(this.panelEncabezado);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.MinimumSize = new System.Drawing.Size(760, 540);
            this.Name = "GestionMembresiasForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SysGym | Membresias";
            this.panelEncabezado.ResumeLayout(false);
            this.panelEncabezado.PerformLayout();
            this.panelContenido.ResumeLayout(false);
            this.layoutContenido.ResumeLayout(false);
            this.panelListado.ResumeLayout(false);
            this.panelListado.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).EndInit();
            this.panelDetalle.ResumeLayout(false);
            this.layoutDetalle.ResumeLayout(false);
            this.layoutDetalle.PerformLayout();
            this.layoutCampos.ResumeLayout(false);
            this.layoutCampos.PerformLayout();
            this.panelAcciones.ResumeLayout(false);
            this.ResumeLayout(false);

        }

    }
}
