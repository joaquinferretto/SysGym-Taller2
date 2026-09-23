using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Recepcionista
{
    partial class GestionPagosFormulario
    {
        private IContainer components;
        private ErrorProvider indicadorErrores;
        private Label lblEstado; private TableLayoutPanel panelContenido; private Panel panelListado; private Label lblListado; private Label lblAyuda; private Panel panelFiltro; private Label lblFiltro; private Panel panelDetalle; private Label lblFormulario; private Label lblMembresia; private Label lblCuota; private Label lblImporte; private Label lblMetodo; private Label lblEstadoPago; private DataGridView tabla; private DataGridViewTextBoxColumn colIdCuota; private DataGridViewTextBoxColumn colIdPago; private DataGridViewTextBoxColumn colSocio; private DataGridViewTextBoxColumn colDni; private DataGridViewTextBoxColumn colPlan; private DataGridViewTextBoxColumn colPeriodo; private DataGridViewTextBoxColumn colImporte; private DataGridViewTextBoxColumn colEstadoTabla;
        private TextBox buscador; private ComboBox filtroEstado; private ComboBox membresia; private TextBox cuota; private TextBox importe; private ComboBox metodo; private ComboBox estado; private Button nuevo; private Button registrar; private Button anular; private Button reembolsar;

        protected override void Dispose(bool liberarRecursos) { if (liberarRecursos && components != null) components.Dispose(); base.Dispose(liberarRecursos); }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
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
            this.nuevo = new System.Windows.Forms.Button();
            this.registrar = new System.Windows.Forms.Button();
            this.anular = new System.Windows.Forms.Button();
            this.reembolsar = new System.Windows.Forms.Button();
            this.indicadorErrores = new System.Windows.Forms.ErrorProvider(this.components);
            this.panelContenido.SuspendLayout();
            this.panelListado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).BeginInit();
            this.panelFiltro.SuspendLayout();
            this.panelDetalle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.indicadorErrores)).BeginInit();
            this.SuspendLayout();
            //
            // lblEstado
            //
            this.lblEstado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.lblEstado.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblEstado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblEstado.Location = new System.Drawing.Point(0, 568);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Padding = new System.Windows.Forms.Padding(18, 0, 12, 0);
            this.lblEstado.Size = new System.Drawing.Size(1076, 30);
            this.lblEstado.TabIndex = 3;
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // panelContenido
            //
            this.panelContenido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelContenido.ColumnCount = 2;
            this.panelContenido.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelContenido.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 396F));
            this.panelContenido.Controls.Add(this.panelListado, 0, 0);
            this.panelContenido.Controls.Add(this.panelDetalle, 1, 0);
            this.panelContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenido.Location = new System.Drawing.Point(0, 0);
            this.panelContenido.Name = "panelContenido";
            this.panelContenido.Padding = new System.Windows.Forms.Padding(16);
            this.panelContenido.RowCount = 1;
            this.panelContenido.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelContenido.Size = new System.Drawing.Size(1076, 568);
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
            this.panelListado.Location = new System.Drawing.Point(16, 16);
            this.panelListado.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.panelListado.Name = "panelListado";
            this.panelListado.Padding = new System.Windows.Forms.Padding(16);
            this.panelListado.Size = new System.Drawing.Size(632, 536);
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
            this.tabla.Location = new System.Drawing.Point(16, 124);
            this.tabla.Margin = new System.Windows.Forms.Padding(0);
            this.tabla.MultiSelect = false;
            this.tabla.Name = "tabla";
            this.tabla.ReadOnly = true;
            this.tabla.RowHeadersVisible = false;
            this.tabla.RowTemplate.Height = 30;
            this.tabla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tabla.Size = new System.Drawing.Size(598, 394);
            this.tabla.TabIndex = 3;
            this.tabla.SelectionChanged += new System.EventHandler(this.tabla_SelectionChanged);
            //
            // colIdCuota
            //
            this.colIdCuota.FillWeight = 50F;
            this.colIdCuota.HeaderText = "IdCuota";
            this.colIdCuota.Name = "colIdCuota";
            this.colIdCuota.ReadOnly = true;
            this.colIdCuota.Visible = false;
            //
            // colIdPago
            //
            this.colIdPago.FillWeight = 50F;
            this.colIdPago.HeaderText = "IdPago";
            this.colIdPago.Name = "colIdPago";
            this.colIdPago.ReadOnly = true;
            this.colIdPago.Visible = false;
            //
            // colSocio
            //
            this.colSocio.FillWeight = 125F;
            this.colSocio.HeaderText = "Socio";
            this.colSocio.MinimumWidth = 90;
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
            //
            // colPlan
            //
            this.colPlan.FillWeight = 90F;
            this.colPlan.HeaderText = "Plan";
            this.colPlan.MinimumWidth = 90;
            this.colPlan.Name = "colPlan";
            this.colPlan.ReadOnly = true;
            //
            // colPeriodo
            //
            this.colPeriodo.FillWeight = 125F;
            this.colPeriodo.HeaderText = "Periodo";
            this.colPeriodo.MinimumWidth = 90;
            this.colPeriodo.Name = "colPeriodo";
            this.colPeriodo.ReadOnly = true;
            //
            // colImporte
            //
            this.colImporte.FillWeight = 90F;
            this.colImporte.HeaderText = "Importe";
            this.colImporte.MinimumWidth = 90;
            this.colImporte.Name = "colImporte";
            this.colImporte.ReadOnly = true;
            //
            // colEstadoTabla
            //
            this.colEstadoTabla.FillWeight = 85F;
            this.colEstadoTabla.HeaderText = "Estado";
            this.colEstadoTabla.MinimumWidth = 90;
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
            this.panelFiltro.Location = new System.Drawing.Point(16, 72);
            this.panelFiltro.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.panelFiltro.Name = "panelFiltro";
            this.panelFiltro.Size = new System.Drawing.Size(598, 42);
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
            this.filtroEstado.Location = new System.Drawing.Point(450, 8);
            this.filtroEstado.Margin = new System.Windows.Forms.Padding(0, 8, 0, 8);
            this.filtroEstado.Name = "filtroEstado";
            this.filtroEstado.Size = new System.Drawing.Size(148, 25);
            this.filtroEstado.TabIndex = 2;
            this.filtroEstado.SelectedIndexChanged += new System.EventHandler(this.filtroEstado_SelectedIndexChanged);
            //
            // lblFiltro
            //
            this.lblFiltro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFiltro.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblFiltro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblFiltro.Location = new System.Drawing.Point(388, 10);
            this.lblFiltro.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.lblFiltro.Name = "lblFiltro";
            this.lblFiltro.Size = new System.Drawing.Size(58, 24);
            this.lblFiltro.TabIndex = 1;
            this.lblFiltro.Text = "Mostrar";
            this.lblFiltro.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // buscador
            //
            this.buscador.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buscador.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.buscador.Location = new System.Drawing.Point(0, 9);
            this.buscador.Margin = new System.Windows.Forms.Padding(0, 8, 12, 8);
            this.buscador.Name = "buscador";
            this.buscador.Size = new System.Drawing.Size(376, 26);
            this.buscador.TabIndex = 0;
            this.buscador.TextChanged += new System.EventHandler(this.buscador_TextChanged);
            //
            // lblAyuda
            //
            this.lblAyuda.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAyuda.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblAyuda.Location = new System.Drawing.Point(16, 44);
            this.lblAyuda.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.lblAyuda.Name = "lblAyuda";
            this.lblAyuda.Size = new System.Drawing.Size(598, 24);
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
            this.lblListado.Location = new System.Drawing.Point(16, 16);
            this.lblListado.Margin = new System.Windows.Forms.Padding(0);
            this.lblListado.Name = "lblListado";
            this.lblListado.Size = new System.Drawing.Size(598, 28);
            this.lblListado.TabIndex = 0;
            this.lblListado.Text = "Cuotas";
            this.lblListado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // panelDetalle
            //
            this.panelDetalle.AutoScroll = true;
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
            this.panelDetalle.Controls.Add(this.nuevo);
            this.panelDetalle.Controls.Add(this.registrar);
            this.panelDetalle.Controls.Add(this.anular);
            this.panelDetalle.Controls.Add(this.reembolsar);
            this.panelDetalle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDetalle.Location = new System.Drawing.Point(664, 16);
            this.panelDetalle.Margin = new System.Windows.Forms.Padding(0);
            this.panelDetalle.Name = "panelDetalle";
            this.panelDetalle.Padding = new System.Windows.Forms.Padding(16);
            this.panelDetalle.Size = new System.Drawing.Size(396, 370);
            this.panelDetalle.TabIndex = 1;
            //
            // lblFormulario
            //
            this.lblFormulario.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblFormulario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblFormulario.Location = new System.Drawing.Point(16, 16);
            this.lblFormulario.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.lblFormulario.Name = "lblFormulario";
            this.lblFormulario.Size = new System.Drawing.Size(362, 32);
            this.lblFormulario.TabIndex = 0;
            this.lblFormulario.Text = "Nuevo pago";
            this.lblFormulario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblMembresia
            //
            this.lblMembresia.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblMembresia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblMembresia.Location = new System.Drawing.Point(16, 58);
            this.lblMembresia.Margin = new System.Windows.Forms.Padding(0, 0, 8, 8);
            this.lblMembresia.Name = "lblMembresia";
            this.lblMembresia.Size = new System.Drawing.Size(116, 30);
            this.lblMembresia.TabIndex = 1;
            this.lblMembresia.Text = "Membresia:";
            this.lblMembresia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // membresia
            //
            this.membresia.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.membresia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.membresia.Location = new System.Drawing.Point(140, 64);
            this.membresia.Margin = new System.Windows.Forms.Padding(0, 3, 16, 8);
            this.membresia.Name = "membresia";
            this.membresia.Size = new System.Drawing.Size(222, 25);
            this.membresia.TabIndex = 2;
            this.membresia.SelectedIndexChanged += new System.EventHandler(this.membresia_SelectedIndexChanged);
            //
            // lblCuota
            //
            this.lblCuota.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblCuota.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblCuota.Location = new System.Drawing.Point(16, 96);
            this.lblCuota.Margin = new System.Windows.Forms.Padding(0, 0, 8, 8);
            this.lblCuota.Name = "lblCuota";
            this.lblCuota.Size = new System.Drawing.Size(116, 30);
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
            this.cuota.Location = new System.Drawing.Point(140, 100);
            this.cuota.Margin = new System.Windows.Forms.Padding(0, 3, 16, 8);
            this.cuota.Name = "cuota";
            this.cuota.ReadOnly = true;
            this.cuota.Size = new System.Drawing.Size(222, 24);
            this.cuota.TabIndex = 4;
            //
            // lblImporte
            //
            this.lblImporte.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblImporte.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblImporte.Location = new System.Drawing.Point(16, 134);
            this.lblImporte.Margin = new System.Windows.Forms.Padding(0, 0, 8, 8);
            this.lblImporte.Name = "lblImporte";
            this.lblImporte.Size = new System.Drawing.Size(116, 30);
            this.lblImporte.TabIndex = 5;
            this.lblImporte.Text = "Importe:";
            this.lblImporte.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // importe
            //
            this.importe.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.importe.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.importe.Location = new System.Drawing.Point(140, 138);
            this.importe.Margin = new System.Windows.Forms.Padding(0, 3, 16, 8);
            this.importe.Name = "importe";
            this.importe.Size = new System.Drawing.Size(222, 24);
            this.importe.TabIndex = 6;
            this.importe.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.importe_KeyPress);
            //
            // lblMetodo
            //
            this.lblMetodo.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblMetodo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblMetodo.Location = new System.Drawing.Point(16, 172);
            this.lblMetodo.Margin = new System.Windows.Forms.Padding(0, 0, 8, 8);
            this.lblMetodo.Name = "lblMetodo";
            this.lblMetodo.Size = new System.Drawing.Size(116, 30);
            this.lblMetodo.TabIndex = 7;
            this.lblMetodo.Text = "Metodo:";
            this.lblMetodo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // metodo
            //
            this.metodo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metodo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.metodo.Location = new System.Drawing.Point(140, 178);
            this.metodo.Margin = new System.Windows.Forms.Padding(0, 3, 16, 8);
            this.metodo.Name = "metodo";
            this.metodo.Size = new System.Drawing.Size(222, 25);
            this.metodo.TabIndex = 8;
            //
            // lblEstadoPago
            //
            this.lblEstadoPago.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblEstadoPago.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblEstadoPago.Location = new System.Drawing.Point(16, 210);
            this.lblEstadoPago.Margin = new System.Windows.Forms.Padding(0, 0, 8, 8);
            this.lblEstadoPago.Name = "lblEstadoPago";
            this.lblEstadoPago.Size = new System.Drawing.Size(116, 30);
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
            this.estado.Location = new System.Drawing.Point(140, 216);
            this.estado.Margin = new System.Windows.Forms.Padding(0, 3, 16, 8);
            this.estado.Name = "estado";
            this.estado.Size = new System.Drawing.Size(222, 25);
            this.estado.TabIndex = 10;
            //
            // nuevo
            //
            this.nuevo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(149)))), ((int)(((byte)(111)))));
            this.nuevo.FlatAppearance.BorderSize = 0;
            this.nuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nuevo.ForeColor = System.Drawing.Color.White;
            this.nuevo.Location = new System.Drawing.Point(16, 264);
            this.nuevo.Margin = new System.Windows.Forms.Padding(0, 0, 8, 8);
            this.nuevo.Name = "nuevo";
            this.nuevo.Size = new System.Drawing.Size(112, 38);
            this.nuevo.TabIndex = 11;
            this.nuevo.Text = "+ Nuevo pago";
            this.nuevo.UseVisualStyleBackColor = false;
            this.nuevo.Click += new System.EventHandler(this.nuevo_Click);
            //
            // registrar
            //
            this.registrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(66)))), ((int)(((byte)(217)))));
            this.registrar.FlatAppearance.BorderSize = 0;
            this.registrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.registrar.ForeColor = System.Drawing.Color.White;
            this.registrar.Location = new System.Drawing.Point(136, 264);
            this.registrar.Margin = new System.Windows.Forms.Padding(0, 0, 8, 8);
            this.registrar.Name = "registrar";
            this.registrar.Size = new System.Drawing.Size(112, 38);
            this.registrar.TabIndex = 12;
            this.registrar.Text = "Registrar";
            this.registrar.UseVisualStyleBackColor = false;
            this.registrar.Click += new System.EventHandler(this.registrar_Click);
            //
            // anular
            //
            this.anular.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.anular.FlatAppearance.BorderSize = 0;
            this.anular.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.anular.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.anular.Location = new System.Drawing.Point(256, 264);
            this.anular.Margin = new System.Windows.Forms.Padding(0, 0, 8, 8);
            this.anular.Name = "anular";
            this.anular.Size = new System.Drawing.Size(112, 38);
            this.anular.TabIndex = 13;
            this.anular.Text = "Anular";
            this.anular.UseVisualStyleBackColor = false;
            this.anular.Click += new System.EventHandler(this.anular_Click);
            //
            // reembolsar
            //
            this.reembolsar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.reembolsar.FlatAppearance.BorderSize = 0;
            this.reembolsar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.reembolsar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.reembolsar.Location = new System.Drawing.Point(16, 310);
            this.reembolsar.Margin = new System.Windows.Forms.Padding(0, 0, 8, 8);
            this.reembolsar.Name = "reembolsar";
            this.reembolsar.Size = new System.Drawing.Size(112, 38);
            this.reembolsar.TabIndex = 14;
            this.reembolsar.Text = "Reembolsar";
            this.reembolsar.UseVisualStyleBackColor = false;
            this.reembolsar.Click += new System.EventHandler(this.reembolsar_Click);
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
            this.ClientSize = new System.Drawing.Size(1076, 598);
            this.Controls.Add(this.panelContenido);
            this.Controls.Add(this.lblEstado);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.MinimumSize = new System.Drawing.Size(900, 560);
            this.Name = "GestionPagosFormulario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SysGym";
            this.Load += new System.EventHandler(this.GestionPagosFormulario_Load);
            this.panelContenido.ResumeLayout(false);
            this.panelListado.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).EndInit();
            this.panelFiltro.ResumeLayout(false);
            this.panelDetalle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.indicadorErrores)).EndInit();
            this.ResumeLayout(false);

        }

    }
}
