using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Recepcionista
{
    partial class GestionPagosForm
    {
        private IContainer components;
        private Panel panelEncabezado; private Label lblTitulo; private Label lblDescripcion; private Button btnVolver; private Label lblEstado; private Panel panelContenido; private TableLayoutPanel layoutContenido; private Panel panelListado; private Label lblListado; private Label lblAyuda; private Panel panelFiltro; private Label lblFiltro; private Panel panelDetalle; private TableLayoutPanel layoutDetalle; private Label lblFormulario; private TableLayoutPanel layoutCampos; private Panel panelAcciones; private Label lblMembresia; private Label lblCuota; private Label lblImporte; private Label lblMetodo; private Label lblEstadoPago; private FlowLayoutPanel barraAcciones; private DataGridView tabla; private DataGridViewTextBoxColumn colIdCuota; private DataGridViewTextBoxColumn colIdPago; private DataGridViewTextBoxColumn colSocio; private DataGridViewTextBoxColumn colDni; private DataGridViewTextBoxColumn colPlan; private DataGridViewTextBoxColumn colPeriodo; private DataGridViewTextBoxColumn colImporte; private DataGridViewTextBoxColumn colEstadoTabla;
        private TextBox buscador; private ComboBox filtroEstado; private ComboBox membresia; private TextBox cuota; private TextBox importe; private ComboBox metodo; private ComboBox estado; private Button nuevo; private Button registrar; private Button anular; private Button reembolsar;

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
            this.colIdCuota = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIdPago = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSocio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDni = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPlan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPeriodo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colImporte = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstadoTabla = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelFiltro = new System.Windows.Forms.Panel();
            this.filtroEstado = new System.Windows.Forms.ComboBox();
            this.lblFiltro = new System.Windows.Forms.Label();
            this.buscador = new System.Windows.Forms.TextBox();
            this.lblAyuda = new System.Windows.Forms.Label();
            this.lblListado = new System.Windows.Forms.Label();
            this.panelDetalle = new System.Windows.Forms.Panel();
            this.layoutDetalle = new System.Windows.Forms.TableLayoutPanel();
            this.lblFormulario = new System.Windows.Forms.Label();
            this.layoutCampos = new System.Windows.Forms.TableLayoutPanel();
            this.lblMembresia = new System.Windows.Forms.Label();
            this.membresia = new System.Windows.Forms.ComboBox();
            this.lblCuota = new System.Windows.Forms.Label();
            this.cuota = new System.Windows.Forms.TextBox();
            this.lblImporte = new System.Windows.Forms.Label();
            this.importe = new System.Windows.Forms.TextBox();
            this.lblMetodo = new System.Windows.Forms.Label();
            this.metodo = new System.Windows.Forms.ComboBox();
            this.lblEstadoPago = new System.Windows.Forms.Label();
            this.estado = new System.Windows.Forms.ComboBox();
            this.panelAcciones = new System.Windows.Forms.Panel();
            this.nuevo = new System.Windows.Forms.Button();
            this.registrar = new System.Windows.Forms.Button();
            this.anular = new System.Windows.Forms.Button();
            this.reembolsar = new System.Windows.Forms.Button();
            this.panelEncabezado.SuspendLayout();
            this.panelContenido.SuspendLayout();
            this.layoutContenido.SuspendLayout();
            this.panelListado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).BeginInit();
            this.panelFiltro.SuspendLayout();
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
            this.panelEncabezado.Size = new System.Drawing.Size(1100, 80);
            this.panelEncabezado.TabIndex = 0;
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.lblDescripcion.Location = new System.Drawing.Point(24, 47);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(240, 17);
            this.lblDescripcion.TabIndex = 1;
            this.lblDescripcion.Text = "Registro de pagos y consulta de cuotas";
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(22, 10);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(188, 32);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Cuotas y pagos";
            // 
            // btnVolver
            // 
            this.btnVolver.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVolver.BackColor = System.Drawing.Color.White;
            this.btnVolver.FlatAppearance.BorderSize = 0;
            this.btnVolver.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVolver.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(150)))), ((int)(((byte)(105)))));
            this.btnVolver.Location = new System.Drawing.Point(930, 22);
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
            this.barraAcciones.Size = new System.Drawing.Size(1100, 52);
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
            this.lblEstado.Size = new System.Drawing.Size(1100, 32);
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
            this.panelContenido.Size = new System.Drawing.Size(1100, 516);
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
            this.layoutContenido.Size = new System.Drawing.Size(1076, 492);
            this.layoutContenido.TabIndex = 0;
            // 
            // panelListado
            // 
            this.panelListado.BackColor = System.Drawing.Color.White;
            this.panelListado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelListado.Controls.Add(this.tabla);
            this.panelListado.Controls.Add(this.panelFiltro);
            this.panelListado.Controls.Add(this.lblAyuda);
            this.panelListado.Controls.Add(this.lblListado);
            this.panelListado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelListado.Location = new System.Drawing.Point(3, 3);
            this.panelListado.Name = "panelListado";
            this.panelListado.Padding = new System.Windows.Forms.Padding(16);
            this.panelListado.Size = new System.Drawing.Size(607, 486);
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
            this.colIdCuota,
            this.colIdPago,
            this.colSocio,
            this.colDni,
            this.colPlan,
            this.colPeriodo,
            this.colImporte,
            this.colEstadoTabla});
            this.tabla.Location = new System.Drawing.Point(16, 128);
            this.tabla.MultiSelect = false;
            this.tabla.Name = "tabla";
            this.tabla.ReadOnly = true;
            this.tabla.RowHeadersVisible = false;
            this.tabla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tabla.Size = new System.Drawing.Size(570, 340);
            this.tabla.TabIndex = 3;
            // 
            // colIdCuota
            // 
            this.colIdCuota.HeaderText = "IdCuota";
            this.colIdCuota.Name = "colIdCuota";
            this.colIdCuota.ReadOnly = true;
            this.colIdCuota.Visible = false;
            // 
            // colIdPago
            // 
            this.colIdPago.HeaderText = "IdPago";
            this.colIdPago.Name = "colIdPago";
            this.colIdPago.ReadOnly = true;
            this.colIdPago.Visible = false;
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
            // colPeriodo
            // 
            this.colPeriodo.HeaderText = "Periodo";
            this.colPeriodo.Name = "colPeriodo";
            this.colPeriodo.ReadOnly = true;
            // 
            // colImporte
            // 
            this.colImporte.HeaderText = "Importe";
            this.colImporte.Name = "colImporte";
            this.colImporte.ReadOnly = true;
            // 
            // colEstadoTabla
            // 
            this.colEstadoTabla.HeaderText = "Estado";
            this.colEstadoTabla.Name = "colEstadoTabla";
            this.colEstadoTabla.ReadOnly = true;
            // 
            // panelFiltro
            // 
            this.panelFiltro.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelFiltro.Controls.Add(this.filtroEstado);
            this.panelFiltro.Controls.Add(this.lblFiltro);
            this.panelFiltro.Controls.Add(this.buscador);
            this.panelFiltro.Location = new System.Drawing.Point(16, 68);
            this.panelFiltro.Name = "panelFiltro";
            this.panelFiltro.Size = new System.Drawing.Size(570, 54);
            this.panelFiltro.TabIndex = 2;
            // 
            // filtroEstado
            // 
            this.filtroEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filtroEstado.Items.AddRange(new object[] {
            "Todas",
            "Pendientes",
            "Pagadas"});
            this.filtroEstado.Location = new System.Drawing.Point(310, 22);
            this.filtroEstado.Name = "filtroEstado";
            this.filtroEstado.Size = new System.Drawing.Size(190, 25);
            this.filtroEstado.TabIndex = 2;
            // 
            // lblFiltro
            // 
            this.lblFiltro.AutoSize = true;
            this.lblFiltro.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblFiltro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblFiltro.Location = new System.Drawing.Point(310, 7);
            this.lblFiltro.Name = "lblFiltro";
            this.lblFiltro.Size = new System.Drawing.Size(48, 15);
            this.lblFiltro.TabIndex = 1;
            this.lblFiltro.Text = "Mostrar";
            // 
            // buscador
            // 
            this.buscador.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buscador.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.buscador.Location = new System.Drawing.Point(3, 22);
            this.buscador.Name = "buscador";
            this.buscador.Size = new System.Drawing.Size(281, 24);
            this.buscador.TabIndex = 0;
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
            this.lblListado.Size = new System.Drawing.Size(55, 20);
            this.lblListado.TabIndex = 0;
            this.lblListado.Text = "Cuotas";
            // 
            // panelDetalle
            // 
            this.panelDetalle.BackColor = System.Drawing.Color.White;
            this.panelDetalle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDetalle.Controls.Add(this.layoutDetalle);
            this.panelDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDetalle.Location = new System.Drawing.Point(616, 3);
            this.panelDetalle.Name = "panelDetalle";
            this.panelDetalle.Padding = new System.Windows.Forms.Padding(16);
            this.panelDetalle.Size = new System.Drawing.Size(457, 486);
            this.panelDetalle.TabIndex = 1;
            // 
            // layoutDetalle
            // 
            this.layoutDetalle.ColumnCount = 1;
            this.layoutDetalle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 423F));
            this.layoutDetalle.Controls.Add(this.lblFormulario, 0, 0);
            this.layoutDetalle.Controls.Add(this.layoutCampos, 0, 1);
            this.layoutDetalle.Controls.Add(this.panelAcciones, 0, 2);
            this.layoutDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutDetalle.Location = new System.Drawing.Point(16, 16);
            this.layoutDetalle.Name = "layoutDetalle";
            this.layoutDetalle.RowCount = 3;
            this.layoutDetalle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.layoutDetalle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutDetalle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 124F));
            this.layoutDetalle.Size = new System.Drawing.Size(423, 452);
            this.layoutDetalle.TabIndex = 0;
            // 
            // lblFormulario
            // 
            this.lblFormulario.AutoSize = true;
            this.lblFormulario.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblFormulario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblFormulario.Location = new System.Drawing.Point(3, 0);
            this.lblFormulario.Name = "lblFormulario";
            this.lblFormulario.Size = new System.Drawing.Size(100, 21);
            this.lblFormulario.TabIndex = 0;
            this.lblFormulario.Text = "Nuevo pago";
            // 
            // layoutCampos
            // 
            this.layoutCampos.ColumnCount = 2;
            this.layoutCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42F));
            this.layoutCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58F));
            this.layoutCampos.Controls.Add(this.lblMembresia, 0, 0);
            this.layoutCampos.Controls.Add(this.membresia, 1, 0);
            this.layoutCampos.Controls.Add(this.lblCuota, 0, 1);
            this.layoutCampos.Controls.Add(this.cuota, 1, 1);
            this.layoutCampos.Controls.Add(this.lblImporte, 0, 2);
            this.layoutCampos.Controls.Add(this.importe, 1, 2);
            this.layoutCampos.Controls.Add(this.lblMetodo, 0, 3);
            this.layoutCampos.Controls.Add(this.metodo, 1, 3);
            this.layoutCampos.Controls.Add(this.lblEstadoPago, 0, 4);
            this.layoutCampos.Controls.Add(this.estado, 1, 4);
            this.layoutCampos.Dock = System.Windows.Forms.DockStyle.Top;
            this.layoutCampos.Location = new System.Drawing.Point(3, 37);
            this.layoutCampos.Name = "layoutCampos";
            this.layoutCampos.RowCount = 5;
            this.layoutCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.layoutCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.layoutCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.layoutCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.layoutCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.layoutCampos.Size = new System.Drawing.Size(417, 210);
            this.layoutCampos.TabIndex = 1;
            // 
            // lblMembresia
            // 
            this.lblMembresia.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblMembresia.AutoSize = true;
            this.lblMembresia.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblMembresia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblMembresia.Location = new System.Drawing.Point(3, 13);
            this.lblMembresia.Name = "lblMembresia";
            this.lblMembresia.Size = new System.Drawing.Size(69, 15);
            this.lblMembresia.TabIndex = 0;
            this.lblMembresia.Text = "Membresia:";
            // 
            // membresia
            // 
            this.membresia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.membresia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.membresia.Location = new System.Drawing.Point(175, 4);
            this.membresia.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.membresia.Name = "membresia";
            this.membresia.Size = new System.Drawing.Size(242, 25);
            this.membresia.TabIndex = 1;
            // 
            // lblCuota
            // 
            this.lblCuota.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCuota.AutoSize = true;
            this.lblCuota.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblCuota.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblCuota.Location = new System.Drawing.Point(3, 55);
            this.lblCuota.Name = "lblCuota";
            this.lblCuota.Size = new System.Drawing.Size(41, 15);
            this.lblCuota.TabIndex = 2;
            this.lblCuota.Text = "Cuota:";
            // 
            // cuota
            // 
            this.cuota.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.cuota.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cuota.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cuota.Location = new System.Drawing.Point(175, 46);
            this.cuota.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.cuota.Name = "cuota";
            this.cuota.ReadOnly = true;
            this.cuota.Size = new System.Drawing.Size(242, 24);
            this.cuota.TabIndex = 3;
            // 
            // lblImporte
            // 
            this.lblImporte.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblImporte.AutoSize = true;
            this.lblImporte.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblImporte.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblImporte.Location = new System.Drawing.Point(3, 97);
            this.lblImporte.Name = "lblImporte";
            this.lblImporte.Size = new System.Drawing.Size(53, 15);
            this.lblImporte.TabIndex = 4;
            this.lblImporte.Text = "Importe:";
            // 
            // importe
            // 
            this.importe.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.importe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.importe.Location = new System.Drawing.Point(175, 88);
            this.importe.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.importe.Name = "importe";
            this.importe.Size = new System.Drawing.Size(242, 24);
            this.importe.TabIndex = 5;
            // 
            // lblMetodo
            // 
            this.lblMetodo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblMetodo.AutoSize = true;
            this.lblMetodo.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblMetodo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblMetodo.Location = new System.Drawing.Point(3, 139);
            this.lblMetodo.Name = "lblMetodo";
            this.lblMetodo.Size = new System.Drawing.Size(52, 15);
            this.lblMetodo.TabIndex = 6;
            this.lblMetodo.Text = "Metodo:";
            // 
            // metodo
            // 
            this.metodo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metodo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.metodo.Location = new System.Drawing.Point(175, 130);
            this.metodo.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.metodo.Name = "metodo";
            this.metodo.Size = new System.Drawing.Size(242, 25);
            this.metodo.TabIndex = 7;
            // 
            // lblEstadoPago
            // 
            this.lblEstadoPago.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblEstadoPago.AutoSize = true;
            this.lblEstadoPago.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblEstadoPago.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblEstadoPago.Location = new System.Drawing.Point(3, 181);
            this.lblEstadoPago.Name = "lblEstadoPago";
            this.lblEstadoPago.Size = new System.Drawing.Size(45, 15);
            this.lblEstadoPago.TabIndex = 8;
            this.lblEstadoPago.Text = "Estado:";
            // 
            // estado
            // 
            this.estado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.estado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.estado.Items.AddRange(new object[] {
            "Pendiente",
            "Aprobado",
            "Rechazado"});
            this.estado.Location = new System.Drawing.Point(175, 172);
            this.estado.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.estado.Name = "estado";
            this.estado.Size = new System.Drawing.Size(242, 25);
            this.estado.TabIndex = 9;
            // 
            // panelAcciones
            // 
            this.panelAcciones.Controls.Add(this.nuevo);
            this.panelAcciones.Controls.Add(this.registrar);
            this.panelAcciones.Controls.Add(this.anular);
            this.panelAcciones.Controls.Add(this.reembolsar);
            this.panelAcciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAcciones.Location = new System.Drawing.Point(3, 331);
            this.panelAcciones.Name = "panelAcciones";
            this.panelAcciones.Size = new System.Drawing.Size(417, 118);
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
            this.nuevo.Text = "+ Nuevo pago";
            this.nuevo.UseVisualStyleBackColor = false;
            // 
            // registrar
            // 
            this.registrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(150)))), ((int)(((byte)(105)))));
            this.registrar.FlatAppearance.BorderSize = 0;
            this.registrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.registrar.ForeColor = System.Drawing.Color.White;
            this.registrar.Location = new System.Drawing.Point(0, 44);
            this.registrar.Name = "registrar";
            this.registrar.Size = new System.Drawing.Size(100, 32);
            this.registrar.TabIndex = 1;
            this.registrar.Text = "Registrar";
            this.registrar.UseVisualStyleBackColor = false;
            // 
            // anular
            // 
            this.anular.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.anular.FlatAppearance.BorderSize = 0;
            this.anular.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.anular.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.anular.Location = new System.Drawing.Point(104, 44);
            this.anular.Name = "anular";
            this.anular.Size = new System.Drawing.Size(100, 32);
            this.anular.TabIndex = 2;
            this.anular.Text = "Anular";
            this.anular.UseVisualStyleBackColor = false;
            // 
            // reembolsar
            // 
            this.reembolsar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.reembolsar.FlatAppearance.BorderSize = 0;
            this.reembolsar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.reembolsar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.reembolsar.Location = new System.Drawing.Point(0, 84);
            this.reembolsar.Name = "reembolsar";
            this.reembolsar.Size = new System.Drawing.Size(100, 32);
            this.reembolsar.TabIndex = 3;
            this.reembolsar.Text = "Reembolsar";
            this.reembolsar.UseVisualStyleBackColor = false;
            // 
            // GestionPagosForm
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
            this.MinimumSize = new System.Drawing.Size(760, 540);
            this.Name = "GestionPagosForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SysGym | Cuotas y pagos";
            this.panelEncabezado.ResumeLayout(false);
            this.panelEncabezado.PerformLayout();
            this.panelContenido.ResumeLayout(false);
            this.layoutContenido.ResumeLayout(false);
            this.panelListado.ResumeLayout(false);
            this.panelListado.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).EndInit();
            this.panelFiltro.ResumeLayout(false);
            this.panelFiltro.PerformLayout();
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
