using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Recepcionista
{
    partial class GestionPagosFormulario
    {
        private IContainer components;
        private ErrorProvider indicadorErrores;
        private Label lblEstado; private TableLayoutPanel panelContenido; private Panel panelListado; private Label lblListado; private Label lblAyuda; private Panel panelFiltro; private Label lblFiltro; private Panel panelDetalle; private Panel contenedorDetalle; private Label lblFormulario; private Panel contenedorCampos; private Panel panelAcciones; private Label lblMembresia; private Label lblCuota; private Label lblImporte; private Label lblMetodo; private Label lblEstadoPago; private DataGridView tabla; private DataGridViewTextBoxColumn colIdCuota; private DataGridViewTextBoxColumn colIdPago; private DataGridViewTextBoxColumn colSocio; private DataGridViewTextBoxColumn colDni; private DataGridViewTextBoxColumn colPlan; private DataGridViewTextBoxColumn colPeriodo; private DataGridViewTextBoxColumn colImporte; private DataGridViewTextBoxColumn colEstadoTabla;
        private TextBox buscador; private ComboBox filtroEstado; private ComboBox membresia; private TextBox cuota; private TextBox importe; private ComboBox metodo; private ComboBox estado; private Button nuevo; private Button registrar; private Button anular; private Button reembolsar;

        protected override void Dispose(bool liberarRecursos) { if (liberarRecursos && components != null) components.Dispose(); base.Dispose(liberarRecursos); }

        private void InitializeComponent()
        {
            components = new Container(); lblEstado = new Label(); panelContenido = new TableLayoutPanel(); panelListado = new Panel(); lblListado = new Label(); lblAyuda = new Label(); buscador = new TextBox(); panelFiltro = new Panel(); lblFiltro = new Label(); filtroEstado = new ComboBox(); panelDetalle = new Panel(); contenedorDetalle = new Panel(); lblFormulario = new Label(); contenedorCampos = new Panel(); panelAcciones = new Panel(); lblMembresia = new Label(); lblCuota = new Label(); lblImporte = new Label(); lblMetodo = new Label(); lblEstadoPago = new Label(); membresia = new ComboBox(); cuota = new TextBox(); importe = new TextBox(); metodo = new ComboBox(); estado = new ComboBox(); nuevo = new Button(); registrar = new Button(); anular = new Button(); reembolsar = new Button(); tabla = new DataGridView(); colIdCuota = new DataGridViewTextBoxColumn(); colIdPago = new DataGridViewTextBoxColumn(); colSocio = new DataGridViewTextBoxColumn(); colDni = new DataGridViewTextBoxColumn(); colPlan = new DataGridViewTextBoxColumn(); colPeriodo = new DataGridViewTextBoxColumn(); colImporte = new DataGridViewTextBoxColumn(); colEstadoTabla = new DataGridViewTextBoxColumn(); indicadorErrores = new ErrorProvider(components); panelContenido.SuspendLayout(); panelListado.SuspendLayout(); panelFiltro.SuspendLayout(); ((ISupportInitialize)(tabla)).BeginInit(); panelDetalle.SuspendLayout(); contenedorDetalle.SuspendLayout(); contenedorCampos.SuspendLayout(); panelAcciones.SuspendLayout(); ((ISupportInitialize)(indicadorErrores)).BeginInit(); SuspendLayout();
             lblEstado.BackColor = Color.FromArgb(226, 232, 240);  lblEstado.ForeColor = Color.FromArgb(51, 65, 85);  lblEstado.Padding = new Padding(18, 8, 8, 0); lblEstado.Text = string.Empty; panelContenido.BackColor = Color.FromArgb(248, 250, 252);  panelContenido.Padding = new Padding(12); panelContenido.ColumnCount = 2; panelContenido.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F)); panelContenido.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 396F)); panelContenido.Controls.Add(panelListado, 0, 0); panelContenido.Controls.Add(panelDetalle, 1, 0); panelContenido.RowCount = 1; panelContenido.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            panelListado.BackColor = Color.White; panelListado.BorderStyle = BorderStyle.FixedSingle;  panelListado.Padding = new Padding(16); panelListado.Controls.Add(tabla); panelListado.Controls.Add(panelFiltro); panelListado.Controls.Add(lblAyuda); panelListado.Controls.Add(lblListado);  lblListado.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold); lblListado.ForeColor = Color.FromArgb(30, 41, 59);  lblListado.Text = "Cuotas";  lblAyuda.ForeColor = Color.FromArgb(100, 116, 139);  lblAyuda.Text = "Busca por socio, DNI o plan";    panelFiltro.Controls.Add(filtroEstado); panelFiltro.Controls.Add(lblFiltro); panelFiltro.Controls.Add(buscador);  lblFiltro.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);  lblFiltro.Text = "Mostrar";  buscador.BorderStyle = BorderStyle.FixedSingle;   filtroEstado.DropDownStyle = ComboBoxStyle.DropDownList;   filtroEstado.Items.AddRange(new object[] { "Todas", "Pendientes", "Pagadas" }); filtroEstado.SelectedIndex = 0;
                tabla.BackgroundColor = Color.White; tabla.BorderStyle = BorderStyle.None;       tabla.Columns.AddRange(new DataGridViewColumn[] { colIdCuota, colIdPago, colSocio, colDni, colPlan, colPeriodo, colImporte, colEstadoTabla }); colIdCuota.HeaderText = "IdCuota"; colIdCuota.Name = "colIdCuota"; colIdCuota.Visible = false; colIdPago.HeaderText = "IdPago"; colIdPago.Name = "colIdPago"; colIdPago.Visible = false; colSocio.HeaderText = "Socio"; colSocio.Name = "colSocio"; colDni.HeaderText = "DNI"; colDni.Name = "colDni"; colPlan.HeaderText = "Plan"; colPlan.Name = "colPlan"; colPeriodo.HeaderText = "Periodo"; colPeriodo.Name = "colPeriodo"; colImporte.HeaderText = "Importe"; colImporte.Name = "colImporte"; colEstadoTabla.HeaderText = "Estado"; colEstadoTabla.Name = "colEstadoTabla";
            panelDetalle.BackColor = Color.White; panelDetalle.BorderStyle = BorderStyle.FixedSingle;  panelDetalle.Padding = new Padding(16); panelDetalle.Controls.Add(contenedorDetalle);       contenedorDetalle.Controls.Add(lblFormulario); contenedorDetalle.Controls.Add(contenedorCampos); contenedorDetalle.Controls.Add(panelAcciones);  lblFormulario.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold); lblFormulario.ForeColor = Color.FromArgb(30, 41, 59); lblFormulario.Text = "Nuevo pago";
                      contenedorCampos.Controls.Add(lblMembresia); contenedorCampos.Controls.Add(membresia); contenedorCampos.Controls.Add(lblCuota); contenedorCampos.Controls.Add(cuota); contenedorCampos.Controls.Add(lblImporte); contenedorCampos.Controls.Add(importe); contenedorCampos.Controls.Add(lblMetodo); contenedorCampos.Controls.Add(metodo); contenedorCampos.Controls.Add(lblEstadoPago); contenedorCampos.Controls.Add(estado);  membresia.DropDownStyle = ComboBoxStyle.DropDownList;  cuota.ReadOnly = true; cuota.BackColor = Color.FromArgb(241, 245, 249);  importe.BorderStyle = BorderStyle.FixedSingle;  metodo.DropDownStyle = ComboBoxStyle.DropDownList;  estado.DropDownStyle = ComboBoxStyle.DropDownList; estado.Items.AddRange(new object[] { "Pendiente", "Aprobado", "Rechazado" }); estado.SelectedIndex = 1;
             panelAcciones.Controls.Add(nuevo); panelAcciones.Controls.Add(registrar); panelAcciones.Controls.Add(anular); panelAcciones.Controls.Add(reembolsar);
             lblEstado.Name = "lblEstado";  lblEstado.TabIndex = 3;  panelContenido.Name = "panelContenido";  panelContenido.TabIndex = 2;
            panelListado.Name = "panelListado"; panelListado.TabIndex = 0; lblListado.Name = "lblListado";  lblListado.TabIndex = 0; lblAyuda.Name = "lblAyuda";  lblAyuda.TabIndex = 1; panelFiltro.Name = "panelFiltro"; panelFiltro.TabIndex = 2; lblFiltro.ForeColor = Color.FromArgb(71, 85, 105); lblFiltro.Name = "lblFiltro"; lblFiltro.TabIndex = 1; buscador.Name = "buscador"; buscador.TabIndex = 0; filtroEstado.Name = "filtroEstado"; filtroEstado.TabIndex = 2;
               tabla.Name = "tabla";  tabla.TabIndex = 3; colIdCuota.FillWeight = 50; colIdPago.FillWeight = 50; colSocio.FillWeight = 125; colSocio.MinimumWidth = 90; colDni.FillWeight = 80; colDni.MinimumWidth = 90; colPlan.FillWeight = 90; colPlan.MinimumWidth = 90; colPeriodo.FillWeight = 125; colPeriodo.MinimumWidth = 90; colImporte.FillWeight = 90; colImporte.MinimumWidth = 90; colEstadoTabla.FillWeight = 85; colEstadoTabla.MinimumWidth = 90;
            panelDetalle.Name = "panelDetalle"; panelDetalle.TabIndex = 1; contenedorDetalle.Name = "contenedorDetalle"; contenedorDetalle.TabIndex = 0; lblFormulario.Name = "lblFormulario";  lblFormulario.TabIndex = 0; contenedorCampos.Name = "contenedorCampos";  contenedorCampos.TabIndex = 1; panelAcciones.Name = "panelAcciones"; panelAcciones.TabIndex = 2;
              lblMembresia.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold); lblMembresia.ForeColor = Color.FromArgb(51, 65, 85); lblMembresia.Name = "lblMembresia"; lblMembresia.Text = "Membresia:";   lblCuota.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold); lblCuota.ForeColor = Color.FromArgb(51, 65, 85); lblCuota.Name = "lblCuota"; lblCuota.Text = "Cuota:";   lblImporte.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold); lblImporte.ForeColor = Color.FromArgb(51, 65, 85); lblImporte.Name = "lblImporte"; lblImporte.Text = "Importe:";   lblMetodo.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold); lblMetodo.ForeColor = Color.FromArgb(51, 65, 85); lblMetodo.Name = "lblMetodo"; lblMetodo.Text = "Metodo:";   lblEstadoPago.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold); lblEstadoPago.ForeColor = Color.FromArgb(51, 65, 85); lblEstadoPago.Name = "lblEstadoPago"; lblEstadoPago.Text = "Estado:";
            membresia.Margin = new Padding(0, 4, 0, 4); membresia.Name = "membresia"; cuota.BorderStyle = BorderStyle.FixedSingle; cuota.Margin = new Padding(0, 4, 0, 4); cuota.Name = "cuota"; importe.Margin = new Padding(0, 4, 0, 4); importe.Name = "importe"; metodo.Margin = new Padding(0, 4, 0, 4); metodo.Name = "metodo"; estado.Margin = new Padding(0, 4, 0, 4); estado.Name = "estado";
            nuevo.BackColor = System.Drawing.Color.FromArgb(9, 149, 111); nuevo.FlatAppearance.BorderSize = 0; nuevo.FlatStyle = FlatStyle.Flat; nuevo.ForeColor = Color.White;  nuevo.Name = "nuevo";  nuevo.Text = "+ Nuevo pago"; nuevo.UseVisualStyleBackColor = false; registrar.BackColor = System.Drawing.Color.FromArgb(72, 66, 217); registrar.FlatAppearance.BorderSize = 0; registrar.FlatStyle = FlatStyle.Flat; registrar.ForeColor = Color.White;  registrar.Name = "registrar";  registrar.Text = "Registrar"; registrar.UseVisualStyleBackColor = false; anular.BackColor = System.Drawing.Color.FromArgb(255, 240, 240); anular.FlatAppearance.BorderSize = 0; anular.FlatStyle = FlatStyle.Flat; anular.ForeColor = System.Drawing.Color.FromArgb(173, 36, 36);  anular.Name = "anular";  anular.Text = "Anular"; anular.UseVisualStyleBackColor = false; reembolsar.BackColor = System.Drawing.Color.FromArgb(255, 240, 240); reembolsar.FlatAppearance.BorderSize = 0; reembolsar.FlatStyle = FlatStyle.Flat; reembolsar.ForeColor = System.Drawing.Color.FromArgb(173, 36, 36);  reembolsar.Name = "reembolsar";  reembolsar.Text = "Reembolsar"; reembolsar.UseVisualStyleBackColor = false;
            Controls.Add(panelContenido); Controls.Add(lblEstado); indicadorErrores.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            indicadorErrores.ContainerControl = this;
            AutoScaleMode = AutoScaleMode.Font;
             BackColor = Color.FromArgb(241, 245, 249); ClientSize = new Size(1100, 680); Font = new Font("Segoe UI", 9.5F); MinimumSize = new Size(900, 560); this.Name = "GestionPagosFormulario"; StartPosition = FormStartPosition.CenterParent; Text = "SysGym";

            // Barra de estado inferior.

            this.lblEstado.Padding = new Padding(18, 0, 12, 0);
            this.lblEstado.TextAlign = ContentAlignment.MiddleLeft;
            // Area de trabajo: listado que crece y panel de alta o edicion de ancho fijo.

            this.panelContenido.Padding = new Padding(16);

            this.panelListado.Margin = new Padding(0, 0, 16, 0);
            this.panelListado.Padding = new Padding(16);

            this.lblListado.Margin = new Padding(0);
            this.lblListado.TextAlign = ContentAlignment.MiddleLeft;

            this.lblAyuda.Margin = new Padding(0, 0, 0, 6);
            this.lblAyuda.TextAlign = ContentAlignment.MiddleLeft;

            this.panelFiltro.Margin = new Padding(0, 0, 0, 10);

            this.buscador.Margin = new Padding(0, 8, 12, 8);

            this.lblFiltro.Margin = new Padding(0, 0, 8, 0);
            this.lblFiltro.TextAlign = ContentAlignment.MiddleRight;

            this.filtroEstado.Margin = new Padding(0, 8, 0, 8);

            this.tabla.Margin = new Padding(0);
            this.tabla.RowTemplate.Height = 30;
            // Detalle: se desplaza solo cuando la altura disponible no alcanza.

            this.panelDetalle.Margin = new Padding(0);
            this.panelDetalle.Padding = new Padding(16);

            this.lblFormulario.Margin = new Padding(0, 0, 0, 12);
            this.lblFormulario.TextAlign = ContentAlignment.MiddleLeft;

            this.lblMembresia.Margin = new Padding(0, 0, 8, 8);
            this.lblMembresia.TextAlign = ContentAlignment.MiddleLeft;

            this.membresia.Margin = new Padding(0, 3, 16, 8);

            this.lblCuota.Margin = new Padding(0, 0, 8, 8);
            this.lblCuota.TextAlign = ContentAlignment.MiddleLeft;

            this.cuota.Margin = new Padding(0, 3, 16, 8);

            this.lblImporte.Margin = new Padding(0, 0, 8, 8);
            this.lblImporte.TextAlign = ContentAlignment.MiddleLeft;

            this.importe.Margin = new Padding(0, 3, 16, 8);

            this.lblMetodo.Margin = new Padding(0, 0, 8, 8);
            this.lblMetodo.TextAlign = ContentAlignment.MiddleLeft;

            this.metodo.Margin = new Padding(0, 3, 16, 8);

            this.lblEstadoPago.Margin = new Padding(0, 0, 8, 8);
            this.lblEstadoPago.TextAlign = ContentAlignment.MiddleLeft;

            this.estado.Margin = new Padding(0, 3, 16, 8);
            this.contenedorCampos.Margin = new Padding(0, 0, 0, 16);

            this.panelAcciones.Margin = new Padding(0);

            this.nuevo.Margin = new Padding(0, 0, 8, 8);

            this.registrar.Margin = new Padding(0, 0, 8, 8);

            this.anular.Margin = new Padding(0, 0, 8, 8);

            this.reembolsar.Margin = new Padding(0, 0, 8, 8);

            this.AutoScroll = false;
            this.lblEstado.AutoSize = false;
            this.lblEstado.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblEstado.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblEstado.Location = new System.Drawing.Point(0, 650);
            this.lblEstado.Size = new System.Drawing.Size(1100, 30);
            this.panelContenido.ColumnCount = 2;
            this.panelContenido.RowCount = 1;
            this.panelContenido.AutoSize = false;
            this.panelContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenido.Location = new System.Drawing.Point(0, 0);
            this.panelContenido.Size = new System.Drawing.Size(1100, 650);
            this.panelContenido.AutoScroll = false;
            this.panelListado.AutoSize = false;
            this.panelListado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelListado.Size = new System.Drawing.Size(656, 618);
            this.panelListado.AutoScroll = false;
            this.lblListado.AutoSize = false;
            this.lblListado.Dock = System.Windows.Forms.DockStyle.None;
            this.lblListado.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.lblListado.Location = new System.Drawing.Point(16, 16);
            this.lblListado.Size = new System.Drawing.Size(622, 28);
            this.lblAyuda.AutoSize = false;
            this.lblAyuda.Dock = System.Windows.Forms.DockStyle.None;
            this.lblAyuda.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.lblAyuda.Location = new System.Drawing.Point(16, 44);
            this.lblAyuda.Size = new System.Drawing.Size(622, 24);
            this.panelFiltro.AutoSize = false;
            this.panelFiltro.Dock = System.Windows.Forms.DockStyle.None;
            this.panelFiltro.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.panelFiltro.Location = new System.Drawing.Point(16, 72);
            this.panelFiltro.Size = new System.Drawing.Size(622, 42);
            this.panelFiltro.AutoScroll = false;
            this.lblFiltro.AutoSize = false;
            this.lblFiltro.Dock = System.Windows.Forms.DockStyle.None;
            this.lblFiltro.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.lblFiltro.Location = new System.Drawing.Point(424, 10);
            this.lblFiltro.Size = new System.Drawing.Size(46, 24);
            this.panelDetalle.AutoSize = false;
            this.panelDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDetalle.AutoScroll = true;
            this.contenedorDetalle.AutoSize = false;
            this.contenedorDetalle.Dock = System.Windows.Forms.DockStyle.None;
            this.contenedorDetalle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.contenedorDetalle.Location = new System.Drawing.Point(16, 16);
            this.contenedorDetalle.Size = new System.Drawing.Size(362, 348);
            this.contenedorDetalle.AutoScroll = false;
            this.lblFormulario.AutoSize = false;
            this.lblFormulario.Dock = System.Windows.Forms.DockStyle.None;
            this.lblFormulario.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblFormulario.Location = new System.Drawing.Point(0, 0);
            this.lblFormulario.Size = new System.Drawing.Size(362, 32);
            this.contenedorCampos.AutoSize = false;
            this.contenedorCampos.Dock = System.Windows.Forms.DockStyle.None;
            this.contenedorCampos.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.contenedorCampos.Location = new System.Drawing.Point(0, 42);
            this.contenedorCampos.Size = new System.Drawing.Size(362, 190);
            this.contenedorCampos.AutoScroll = false;
            this.panelAcciones.AutoSize = false;
            this.panelAcciones.Dock = System.Windows.Forms.DockStyle.None;
            this.panelAcciones.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.panelAcciones.Location = new System.Drawing.Point(0, 248);
            this.panelAcciones.Size = new System.Drawing.Size(362, 100);
            this.panelAcciones.AutoScroll = false;
            this.lblMembresia.AutoSize = false;
            this.lblMembresia.Dock = System.Windows.Forms.DockStyle.None;
            this.lblMembresia.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblMembresia.Location = new System.Drawing.Point(0, 0);
            this.lblMembresia.Size = new System.Drawing.Size(116, 30);
            this.lblCuota.AutoSize = false;
            this.lblCuota.Dock = System.Windows.Forms.DockStyle.None;
            this.lblCuota.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblCuota.Location = new System.Drawing.Point(0, 38);
            this.lblCuota.Size = new System.Drawing.Size(116, 30);
            this.lblImporte.AutoSize = false;
            this.lblImporte.Dock = System.Windows.Forms.DockStyle.None;
            this.lblImporte.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblImporte.Location = new System.Drawing.Point(0, 76);
            this.lblImporte.Size = new System.Drawing.Size(116, 30);
            this.lblMetodo.AutoSize = false;
            this.lblMetodo.Dock = System.Windows.Forms.DockStyle.None;
            this.lblMetodo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblMetodo.Location = new System.Drawing.Point(0, 114);
            this.lblMetodo.Size = new System.Drawing.Size(116, 30);
            this.lblEstadoPago.AutoSize = false;
            this.lblEstadoPago.Dock = System.Windows.Forms.DockStyle.None;
            this.lblEstadoPago.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblEstadoPago.Location = new System.Drawing.Point(0, 152);
            this.lblEstadoPago.Size = new System.Drawing.Size(116, 30);
            this.tabla.AutoSize = false;
            this.tabla.Dock = System.Windows.Forms.DockStyle.None;
            this.tabla.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.tabla.Location = new System.Drawing.Point(16, 124);
            this.tabla.Size = new System.Drawing.Size(622, 476);
            this.tabla.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tabla.ReadOnly = true;
            this.tabla.AllowUserToAddRows = false;
            this.tabla.AllowUserToDeleteRows = false;
            this.tabla.AllowUserToResizeRows = false;
            this.tabla.RowHeadersVisible = false;
            this.tabla.MultiSelect = false;
            this.tabla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tabla.ColumnHeadersHeight = 46;
            this.buscador.AutoSize = false;
            this.buscador.Dock = System.Windows.Forms.DockStyle.None;
            this.buscador.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.buscador.Location = new System.Drawing.Point(0, 9);
            this.buscador.Size = new System.Drawing.Size(412, 26);
            this.filtroEstado.AutoSize = false;
            this.filtroEstado.Dock = System.Windows.Forms.DockStyle.None;
            this.filtroEstado.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.filtroEstado.Location = new System.Drawing.Point(474, 8);
            this.filtroEstado.Size = new System.Drawing.Size(148, 26);
            this.membresia.AutoSize = false;
            this.membresia.Dock = System.Windows.Forms.DockStyle.None;
            this.membresia.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.membresia.Location = new System.Drawing.Point(124, 6);
            this.membresia.Size = new System.Drawing.Size(222, 25);
            this.cuota.AutoSize = false;
            this.cuota.Dock = System.Windows.Forms.DockStyle.None;
            this.cuota.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.cuota.Location = new System.Drawing.Point(124, 42);
            this.cuota.Size = new System.Drawing.Size(222, 24);
            this.importe.AutoSize = false;
            this.importe.Dock = System.Windows.Forms.DockStyle.None;
            this.importe.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.importe.Location = new System.Drawing.Point(124, 80);
            this.importe.Size = new System.Drawing.Size(222, 24);
            this.metodo.AutoSize = false;
            this.metodo.Dock = System.Windows.Forms.DockStyle.None;
            this.metodo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.metodo.Location = new System.Drawing.Point(124, 120);
            this.metodo.Size = new System.Drawing.Size(222, 25);
            this.estado.AutoSize = false;
            this.estado.Dock = System.Windows.Forms.DockStyle.None;
            this.estado.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.estado.Location = new System.Drawing.Point(124, 158);
            this.estado.Size = new System.Drawing.Size(222, 25);
            this.nuevo.AutoSize = false;
            this.nuevo.Dock = System.Windows.Forms.DockStyle.None;
            this.nuevo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.nuevo.Location = new System.Drawing.Point(0, 0);
            this.nuevo.Size = new System.Drawing.Size(112, 38);
            this.registrar.AutoSize = false;
            this.registrar.Dock = System.Windows.Forms.DockStyle.None;
            this.registrar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.registrar.Location = new System.Drawing.Point(120, 0);
            this.registrar.Size = new System.Drawing.Size(112, 38);
            this.anular.AutoSize = false;
            this.anular.Dock = System.Windows.Forms.DockStyle.None;
            this.anular.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.anular.Location = new System.Drawing.Point(240, 0);
            this.anular.Size = new System.Drawing.Size(112, 38);
            this.reembolsar.AutoSize = false;
            this.reembolsar.Dock = System.Windows.Forms.DockStyle.None;
            this.reembolsar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.reembolsar.Location = new System.Drawing.Point(0, 46);
            this.reembolsar.Size = new System.Drawing.Size(112, 38);
            panelContenido.ResumeLayout(false); panelListado.ResumeLayout(false); panelListado.PerformLayout(); panelFiltro.ResumeLayout(false); panelFiltro.PerformLayout(); ((ISupportInitialize)(tabla)).EndInit(); panelDetalle.ResumeLayout(false); contenedorDetalle.ResumeLayout(false); contenedorDetalle.PerformLayout(); contenedorCampos.ResumeLayout(false); contenedorCampos.PerformLayout(); panelAcciones.ResumeLayout(false); ((ISupportInitialize)(indicadorErrores)).EndInit(); ResumeLayout(false);

            this.Load += new System.EventHandler(this.GestionPagosFormulario_Load);
            this.membresia.SelectedIndexChanged += new System.EventHandler(this.membresia_SelectedIndexChanged);
            this.tabla.SelectionChanged += new System.EventHandler(this.tabla_SelectionChanged);
            this.buscador.TextChanged += new System.EventHandler(this.buscador_TextChanged);
            this.filtroEstado.SelectedIndexChanged += new System.EventHandler(this.filtroEstado_SelectedIndexChanged);
            this.nuevo.Click += new System.EventHandler(this.nuevo_Click);
            this.registrar.Click += new System.EventHandler(this.registrar_Click);
            this.anular.Click += new System.EventHandler(this.anular_Click);
            this.reembolsar.Click += new System.EventHandler(this.reembolsar_Click);
                    this.importe.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.importe_KeyPress);
        }

    }
}
