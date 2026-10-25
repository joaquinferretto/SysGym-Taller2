using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Recepcionista
{
    partial class GestionPagosFormulario
    {
        private IContainer components;
        private ErrorProvider indicadorErrores;
        private Label lblRegistradoPor;
        private Label lblAvisoCuota;
        private Label lblEstado; private TableLayoutPanel panelContenido; private Panel panelListado; private Label lblListado; private Label lblAyuda; private Panel panelFiltro; private Label lblFiltro; private Panel panelDetalle; private Label lblFormulario; private Label lblMembresia; private Label lblCuota; private Label lblImporte; private Label lblMetodo; private Label lblEstadoPago; private DataGridView tabla; private DataGridViewTextBoxColumn colIdCuota; private DataGridViewTextBoxColumn colIdPago; private DataGridViewTextBoxColumn colSocio; private DataGridViewTextBoxColumn colDni; private DataGridViewTextBoxColumn colPlan; private DataGridViewTextBoxColumn colPeriodo; private DataGridViewTextBoxColumn colImporte; private DataGridViewTextBoxColumn colEstadoTabla;
        private TextBox buscador; private ComboBox filtroEstado; private ComboBox membresia; private TextBox cuota; private TextBox importe; private ComboBox metodo; private ComboBox estado; private Button nuevo; private Button registrar; private Button anular; private Button reembolsar; private Button exportarComprobante; private SaveFileDialog dialogoPdf;
        private ContextMenuStrip menuCuotas;
        private ToolStripMenuItem exportarHistorialMenu;
        private ToolStripMenuItem verFichaMenu;

        protected override void Dispose(bool liberarRecursos) { if (liberarRecursos && components != null) components.Dispose(); base.Dispose(liberarRecursos); }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dialogoPdf = new System.Windows.Forms.SaveFileDialog();
            this.lblEstado = new System.Windows.Forms.Label();
            this.panelContenido = new System.Windows.Forms.TableLayoutPanel();
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
            this.menuCuotas = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.verFichaMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.exportarHistorialMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.panelFiltro = new System.Windows.Forms.Panel();
            this.filtroEstado = new System.Windows.Forms.ComboBox();
            this.lblFiltro = new System.Windows.Forms.Label();
            this.buscador = new System.Windows.Forms.TextBox();
            this.lblAyuda = new System.Windows.Forms.Label();
            this.lblListado = new System.Windows.Forms.Label();
            this.panelDetalle = new System.Windows.Forms.Panel();
            this.lblFormulario = new System.Windows.Forms.Label();
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
            this.lblRegistradoPor = new System.Windows.Forms.Label();
            this.lblAvisoCuota = new System.Windows.Forms.Label();
            this.nuevo = new System.Windows.Forms.Button();
            this.registrar = new System.Windows.Forms.Button();
            this.anular = new System.Windows.Forms.Button();
            this.reembolsar = new System.Windows.Forms.Button();
            this.exportarComprobante = new System.Windows.Forms.Button();
            this.indicadorErrores = new System.Windows.Forms.ErrorProvider(this.components);
            this.panelContenido.SuspendLayout();
            this.panelListado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).BeginInit();
            this.menuCuotas.SuspendLayout();
            this.panelFiltro.SuspendLayout();
            this.panelDetalle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.indicadorErrores)).BeginInit();
            this.SuspendLayout();
            //
            // dialogoPdf
            //
            this.dialogoPdf.DefaultExt = "pdf";
            this.dialogoPdf.Filter = "Documento PDF (*.pdf)|*.pdf";
            //
            // lblEstado
            //
            this.lblEstado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.lblEstado.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblEstado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblEstado.Location = new System.Drawing.Point(0, 562);
            this.lblEstado.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Padding = new System.Windows.Forms.Padding(22, 0, 15, 0);
            this.lblEstado.Size = new System.Drawing.Size(1250, 38);
            this.lblEstado.TabIndex = 3;
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // panelContenido
            //
            this.panelContenido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelContenido.ColumnCount = 2;
            this.panelContenido.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelContenido.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 525F));
            this.panelContenido.Controls.Add(this.panelListado, 0, 0);
            this.panelContenido.Controls.Add(this.panelDetalle, 1, 0);
            this.panelContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenido.Location = new System.Drawing.Point(0, 0);
            this.panelContenido.Margin = new System.Windows.Forms.Padding(4);
            this.panelContenido.Name = "panelContenido";
            this.panelContenido.Padding = new System.Windows.Forms.Padding(20);
            this.panelContenido.RowCount = 1;
            this.panelContenido.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelContenido.Size = new System.Drawing.Size(1250, 562);
            this.panelContenido.TabIndex = 2;
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
            this.panelListado.Location = new System.Drawing.Point(20, 20);
            this.panelListado.Margin = new System.Windows.Forms.Padding(0, 0, 20, 0);
            this.panelListado.Name = "panelListado";
            this.panelListado.Padding = new System.Windows.Forms.Padding(20);
            this.panelListado.Size = new System.Drawing.Size(665, 522);
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
            this.tabla.ColumnHeadersHeight = 46;
            this.tabla.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colIdCuota,
            this.colIdPago,
            this.colSocio,
            this.colDni,
            this.colPlan,
            this.colPeriodo,
            this.colImporte,
            this.colEstadoTabla});
            this.tabla.ContextMenuStrip = this.menuCuotas;
            this.tabla.Location = new System.Drawing.Point(20, 155);
            this.tabla.Margin = new System.Windows.Forms.Padding(0);
            this.tabla.MultiSelect = false;
            this.tabla.Name = "tabla";
            this.tabla.ReadOnly = true;
            this.tabla.RowHeadersVisible = false;
            this.tabla.RowHeadersWidth = 51;
            this.tabla.RowTemplate.Height = 30;
            this.tabla.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tabla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tabla.Size = new System.Drawing.Size(623, 348);
            this.tabla.TabIndex = 3;
            this.tabla.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tabla_CellDoubleClick);
            this.tabla.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.tabla_CellMouseDown);
            this.tabla.SelectionChanged += new System.EventHandler(this.tabla_SelectionChanged);
            //
            // colIdCuota
            //
            this.colIdCuota.FillWeight = 50F;
            this.colIdCuota.HeaderText = "IdCuota";
            this.colIdCuota.MinimumWidth = 6;
            this.colIdCuota.Name = "colIdCuota";
            this.colIdCuota.ReadOnly = true;
            this.colIdCuota.Visible = false;
            //
            // colIdPago
            //
            this.colIdPago.FillWeight = 50F;
            this.colIdPago.HeaderText = "IdPago";
            this.colIdPago.MinimumWidth = 6;
            this.colIdPago.Name = "colIdPago";
            this.colIdPago.ReadOnly = true;
            this.colIdPago.Visible = false;
            //
            // colSocio
            //
            this.colSocio.FillWeight = 125F;
            this.colSocio.HeaderText = "Socio";
            this.colSocio.MinimumWidth = 115;
            this.colSocio.Name = "colSocio";
            this.colSocio.ReadOnly = true;
            //
            // colDni
            //
            this.colDni.FillWeight = 80F;
            this.colDni.HeaderText = "DNI";
            this.colDni.MinimumWidth = 90;
            this.colDni.Name = "colDni";
            this.colDni.ReadOnly = true;
            this.colDni.Visible = false;
            //
            // colPlan
            //
            this.colPlan.FillWeight = 90F;
            this.colPlan.HeaderText = "Plan";
            this.colPlan.MinimumWidth = 90;
            this.colPlan.Name = "colPlan";
            this.colPlan.ReadOnly = true;
            this.colPlan.Visible = false;
            //
            // colPeriodo
            //
            this.colPeriodo.FillWeight = 190F;
            this.colPeriodo.HeaderText = "Período";
            this.colPeriodo.MinimumWidth = 145;
            this.colPeriodo.Name = "colPeriodo";
            this.colPeriodo.ReadOnly = true;
            //
            // colImporte
            //
            this.colImporte.HeaderText = "Importe";
            this.colImporte.MinimumWidth = 95;
            this.colImporte.Name = "colImporte";
            this.colImporte.ReadOnly = true;
            this.colImporte.Visible = false;
            //
            // colEstadoTabla
            //
            this.colEstadoTabla.FillWeight = 95F;
            this.colEstadoTabla.HeaderText = "Estado";
            this.colEstadoTabla.MinimumWidth = 80;
            this.colEstadoTabla.Name = "colEstadoTabla";
            this.colEstadoTabla.ReadOnly = true;
            //
            // menuCuotas
            //
            this.menuCuotas.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuCuotas.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.verFichaMenu,
            this.exportarHistorialMenu});
            this.menuCuotas.Name = "menuCuotas";
            this.menuCuotas.Size = new System.Drawing.Size(203, 52);
            //
            // verFichaMenu
            //
            this.verFichaMenu.Enabled = false;
            this.verFichaMenu.Name = "verFichaMenu";
            this.verFichaMenu.Size = new System.Drawing.Size(202, 24);
            this.verFichaMenu.Text = "Ver ficha completa";
            this.verFichaMenu.Click += new System.EventHandler(this.btnFicha_Click);
            //
            // exportarHistorialMenu
            //
            this.exportarHistorialMenu.Enabled = false;
            this.exportarHistorialMenu.Name = "exportarHistorialMenu";
            this.exportarHistorialMenu.Size = new System.Drawing.Size(202, 24);
            this.exportarHistorialMenu.Text = "Exportar historial";
            this.exportarHistorialMenu.Click += new System.EventHandler(this.exportarHistorial_Click);
            //
            // panelFiltro
            //
            this.panelFiltro.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelFiltro.Controls.Add(this.filtroEstado);
            this.panelFiltro.Controls.Add(this.lblFiltro);
            this.panelFiltro.Controls.Add(this.buscador);
            this.panelFiltro.Location = new System.Drawing.Point(20, 90);
            this.panelFiltro.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.panelFiltro.Name = "panelFiltro";
            this.panelFiltro.Size = new System.Drawing.Size(623, 52);
            this.panelFiltro.TabIndex = 2;
            //
            // filtroEstado
            //
            this.filtroEstado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.filtroEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filtroEstado.Items.AddRange(new object[] {
            "Todas",
            "Pendientes",
            "Pagadas"});
            this.filtroEstado.Location = new System.Drawing.Point(483, 10);
            this.filtroEstado.Margin = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.filtroEstado.Name = "filtroEstado";
            this.filtroEstado.Size = new System.Drawing.Size(139, 29);
            this.filtroEstado.TabIndex = 2;
            this.filtroEstado.SelectedIndexChanged += new System.EventHandler(this.filtroEstado_SelectedIndexChanged);
            //
            // lblFiltro
            //
            this.lblFiltro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFiltro.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblFiltro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblFiltro.Location = new System.Drawing.Point(403, 12);
            this.lblFiltro.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.lblFiltro.Name = "lblFiltro";
            this.lblFiltro.Size = new System.Drawing.Size(70, 30);
            this.lblFiltro.TabIndex = 1;
            this.lblFiltro.Text = "Mostrar";
            this.lblFiltro.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // buscador
            //
            this.buscador.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buscador.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.buscador.Location = new System.Drawing.Point(0, 11);
            this.buscador.Margin = new System.Windows.Forms.Padding(0, 10, 15, 10);
            this.buscador.Name = "buscador";
            this.buscador.Size = new System.Drawing.Size(388, 29);
            this.buscador.TabIndex = 0;
            this.buscador.TextChanged += new System.EventHandler(this.buscador_TextChanged);
            //
            // lblAyuda
            //
            this.lblAyuda.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAyuda.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblAyuda.Location = new System.Drawing.Point(20, 55);
            this.lblAyuda.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.lblAyuda.Name = "lblAyuda";
            this.lblAyuda.Size = new System.Drawing.Size(623, 30);
            this.lblAyuda.TabIndex = 1;
            this.lblAyuda.Text = "Busca por socio, DNI o plan";
            this.lblAyuda.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblListado
            //
            this.lblListado.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblListado.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblListado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblListado.Location = new System.Drawing.Point(20, 20);
            this.lblListado.Margin = new System.Windows.Forms.Padding(0);
            this.lblListado.Name = "lblListado";
            this.lblListado.Size = new System.Drawing.Size(623, 35);
            this.lblListado.TabIndex = 0;
            this.lblListado.Text = "Cuotas";
            this.lblListado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // panelDetalle
            //
            this.panelDetalle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDetalle.BackColor = System.Drawing.Color.White;
            this.panelDetalle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDetalle.Controls.Add(this.lblFormulario);
            this.panelDetalle.Controls.Add(this.lblMembresia);
            this.panelDetalle.Controls.Add(this.membresia);
            this.panelDetalle.Controls.Add(this.lblCuota);
            this.panelDetalle.Controls.Add(this.cuota);
            this.panelDetalle.Controls.Add(this.lblImporte);
            this.panelDetalle.Controls.Add(this.importe);
            this.panelDetalle.Controls.Add(this.lblMetodo);
            this.panelDetalle.Controls.Add(this.metodo);
            this.panelDetalle.Controls.Add(this.lblEstadoPago);
            this.panelDetalle.Controls.Add(this.estado);
            this.panelDetalle.Controls.Add(this.lblRegistradoPor);
            this.panelDetalle.Controls.Add(this.lblAvisoCuota);
            this.panelDetalle.Controls.Add(this.nuevo);
            this.panelDetalle.Controls.Add(this.registrar);
            this.panelDetalle.Controls.Add(this.anular);
            this.panelDetalle.Controls.Add(this.reembolsar);
            this.panelDetalle.Controls.Add(this.exportarComprobante);
            this.panelDetalle.Location = new System.Drawing.Point(705, 20);
            this.panelDetalle.Margin = new System.Windows.Forms.Padding(0);
            this.panelDetalle.Name = "panelDetalle";
            this.panelDetalle.Padding = new System.Windows.Forms.Padding(20);
            this.panelDetalle.Size = new System.Drawing.Size(525, 522);
            this.panelDetalle.TabIndex = 1;
            //
            // lblFormulario
            //
            this.lblFormulario.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblFormulario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblFormulario.Location = new System.Drawing.Point(20, 20);
            this.lblFormulario.Margin = new System.Windows.Forms.Padding(0, 0, 0, 15);
            this.lblFormulario.Name = "lblFormulario";
            this.lblFormulario.Size = new System.Drawing.Size(485, 40);
            this.lblFormulario.TabIndex = 0;
            this.lblFormulario.Text = "Nuevo pago";
            this.lblFormulario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblMembresia
            //
            this.lblMembresia.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblMembresia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblMembresia.Location = new System.Drawing.Point(20, 72);
            this.lblMembresia.Margin = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.lblMembresia.Name = "lblMembresia";
            this.lblMembresia.Size = new System.Drawing.Size(110, 38);
            this.lblMembresia.TabIndex = 1;
            this.lblMembresia.Text = "Membresía:";
            this.lblMembresia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // membresia
            //
            this.membresia.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.membresia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.membresia.Location = new System.Drawing.Point(140, 80);
            this.membresia.Margin = new System.Windows.Forms.Padding(0, 4, 20, 10);
            this.membresia.Name = "membresia";
            this.membresia.Size = new System.Drawing.Size(345, 29);
            this.membresia.TabIndex = 2;
            this.membresia.SelectedIndexChanged += new System.EventHandler(this.membresia_SelectedIndexChanged);
            //
            // lblCuota
            //
            this.lblCuota.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblCuota.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblCuota.Location = new System.Drawing.Point(20, 120);
            this.lblCuota.Margin = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.lblCuota.Name = "lblCuota";
            this.lblCuota.Size = new System.Drawing.Size(110, 38);
            this.lblCuota.TabIndex = 3;
            this.lblCuota.Text = "Cuota:";
            this.lblCuota.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // cuota
            //
            this.cuota.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cuota.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.cuota.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cuota.Location = new System.Drawing.Point(140, 125);
            this.cuota.Margin = new System.Windows.Forms.Padding(0, 4, 20, 10);
            this.cuota.Name = "cuota";
            this.cuota.ReadOnly = true;
            this.cuota.Size = new System.Drawing.Size(345, 29);
            this.cuota.TabIndex = 4;
            //
            // lblImporte
            //
            this.lblImporte.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblImporte.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblImporte.Location = new System.Drawing.Point(20, 168);
            this.lblImporte.Margin = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.lblImporte.Name = "lblImporte";
            this.lblImporte.Size = new System.Drawing.Size(110, 38);
            this.lblImporte.TabIndex = 5;
            this.lblImporte.Text = "Importe:";
            this.lblImporte.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // importe
            //
            this.importe.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.importe.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.importe.Location = new System.Drawing.Point(140, 172);
            this.importe.Margin = new System.Windows.Forms.Padding(0, 4, 20, 10);
            this.importe.Name = "importe";
            this.importe.Size = new System.Drawing.Size(345, 29);
            this.importe.TabIndex = 6;
            this.importe.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.importe_KeyPress);
            //
            // lblMetodo
            //
            this.lblMetodo.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblMetodo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblMetodo.Location = new System.Drawing.Point(20, 215);
            this.lblMetodo.Margin = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.lblMetodo.Name = "lblMetodo";
            this.lblMetodo.Size = new System.Drawing.Size(110, 38);
            this.lblMetodo.TabIndex = 7;
            this.lblMetodo.Text = "Método:";
            this.lblMetodo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // metodo
            //
            this.metodo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metodo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.metodo.Location = new System.Drawing.Point(140, 222);
            this.metodo.Margin = new System.Windows.Forms.Padding(0, 4, 20, 10);
            this.metodo.Name = "metodo";
            this.metodo.Size = new System.Drawing.Size(345, 29);
            this.metodo.TabIndex = 8;
            //
            // lblEstadoPago
            //
            this.lblEstadoPago.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblEstadoPago.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblEstadoPago.Location = new System.Drawing.Point(20, 262);
            this.lblEstadoPago.Margin = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.lblEstadoPago.Name = "lblEstadoPago";
            this.lblEstadoPago.Size = new System.Drawing.Size(110, 38);
            this.lblEstadoPago.TabIndex = 9;
            this.lblEstadoPago.Text = "Estado:";
            this.lblEstadoPago.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // estado
            //
            this.estado.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.estado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.estado.Items.AddRange(new object[] {
            "Pendiente",
            "Aprobado",
            "Rechazado",
            "Anulado",
            "Reembolsado"});
            this.estado.Location = new System.Drawing.Point(140, 270);
            this.estado.Margin = new System.Windows.Forms.Padding(0, 4, 20, 10);
            this.estado.Name = "estado";
            this.estado.Size = new System.Drawing.Size(345, 29);
            this.estado.TabIndex = 10;
            //
            // lblRegistradoPor
            //
            this.lblRegistradoPor.Location = new System.Drawing.Point(20, 300);
            this.lblRegistradoPor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRegistradoPor.Name = "lblRegistradoPor";
            this.lblRegistradoPor.Size = new System.Drawing.Size(485, 25);
            this.lblRegistradoPor.TabIndex = 17;
            this.lblRegistradoPor.Text = "Se registrará como: Usuario actual";
            this.lblRegistradoPor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblAvisoCuota
            //
            this.lblAvisoCuota.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAvisoCuota.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.lblAvisoCuota.Location = new System.Drawing.Point(20, 330);
            this.lblAvisoCuota.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAvisoCuota.Name = "lblAvisoCuota";
            this.lblAvisoCuota.Size = new System.Drawing.Size(485, 48);
            this.lblAvisoCuota.TabIndex = 18;
            this.lblAvisoCuota.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblAvisoCuota.Visible = false;
            //
            // nuevo
            //
            this.nuevo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.nuevo.FlatAppearance.BorderSize = 0;
            this.nuevo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.nuevo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(98)))), ((int)(((byte)(112)))));
            this.nuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nuevo.ForeColor = System.Drawing.Color.White;
            this.nuevo.Location = new System.Drawing.Point(20, 330);
            this.nuevo.Margin = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.nuevo.Name = "nuevo";
            this.nuevo.Size = new System.Drawing.Size(237, 48);
            this.nuevo.TabIndex = 11;
            this.nuevo.Text = "Nuevo";
            this.nuevo.UseVisualStyleBackColor = false;
            this.nuevo.Click += new System.EventHandler(this.nuevo_Click);
            //
            // registrar
            //
            this.registrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.registrar.FlatAppearance.BorderSize = 0;
            this.registrar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(75)))), ((int)(((byte)(31)))));
            this.registrar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(101)))), ((int)(((byte)(41)))));
            this.registrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.registrar.ForeColor = System.Drawing.Color.White;
            this.registrar.Location = new System.Drawing.Point(248, 330);
            this.registrar.Margin = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.registrar.Name = "registrar";
            this.registrar.Size = new System.Drawing.Size(237, 48);
            this.registrar.TabIndex = 12;
            this.registrar.Text = "Registrar pago";
            this.registrar.UseVisualStyleBackColor = false;
            this.registrar.Click += new System.EventHandler(this.registrar_Click);
            //
            // anular
            //
            this.anular.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.anular.FlatAppearance.BorderSize = 0;
            this.anular.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.anular.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.anular.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.anular.ForeColor = System.Drawing.Color.White;
            this.anular.Location = new System.Drawing.Point(20, 388);
            this.anular.Margin = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.anular.Name = "anular";
            this.anular.Size = new System.Drawing.Size(237, 48);
            this.anular.TabIndex = 13;
            this.anular.Text = "Anular";
            this.anular.UseVisualStyleBackColor = false;
            this.anular.Click += new System.EventHandler(this.anular_Click);
            //
            // reembolsar
            //
            this.reembolsar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(121)))), ((int)(((byte)(31)))));
            this.reembolsar.FlatAppearance.BorderSize = 0;
            this.reembolsar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(78)))), ((int)(((byte)(19)))));
            this.reembolsar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(100)))), ((int)(((byte)(25)))));
            this.reembolsar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.reembolsar.ForeColor = System.Drawing.Color.White;
            this.reembolsar.Location = new System.Drawing.Point(248, 388);
            this.reembolsar.Margin = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.reembolsar.Name = "reembolsar";
            this.reembolsar.Size = new System.Drawing.Size(237, 48);
            this.reembolsar.TabIndex = 14;
            this.reembolsar.Text = "Reembolsar";
            this.reembolsar.UseVisualStyleBackColor = false;
            this.reembolsar.Click += new System.EventHandler(this.reembolsar_Click);
            //
            // exportarComprobante
            //
            this.exportarComprobante.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(75)))), ((int)(((byte)(138)))));
            this.exportarComprobante.Enabled = false;
            this.exportarComprobante.FlatAppearance.BorderSize = 0;
            this.exportarComprobante.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(48)))), ((int)(((byte)(89)))));
            this.exportarComprobante.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(61)))), ((int)(((byte)(112)))));
            this.exportarComprobante.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exportarComprobante.ForeColor = System.Drawing.Color.White;
            this.exportarComprobante.Location = new System.Drawing.Point(20, 445);
            this.exportarComprobante.Margin = new System.Windows.Forms.Padding(4);
            this.exportarComprobante.Name = "exportarComprobante";
            this.exportarComprobante.Size = new System.Drawing.Size(485, 48);
            this.exportarComprobante.TabIndex = 15;
            this.exportarComprobante.Text = "Exportar comprobante";
            this.exportarComprobante.UseVisualStyleBackColor = false;
            this.exportarComprobante.Click += new System.EventHandler(this.exportarComprobante_Click);
            //
            // indicadorErrores
            //
            this.indicadorErrores.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.indicadorErrores.ContainerControl = this;
            //
            // GestionPagosFormulario
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1250, 600);
            this.Controls.Add(this.panelContenido);
            this.Controls.Add(this.lblEstado);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1146, 563);
            this.Name = "GestionPagosFormulario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SysGym";
            this.Load += new System.EventHandler(this.GestionPagosFormulario_Load);
            this.panelContenido.ResumeLayout(false);
            this.panelListado.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).EndInit();
            this.menuCuotas.ResumeLayout(false);
            this.panelFiltro.ResumeLayout(false);
            this.panelFiltro.PerformLayout();
            this.panelDetalle.ResumeLayout(false);
            this.panelDetalle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.indicadorErrores)).EndInit();
            this.ResumeLayout(false);

        }

    }
}
