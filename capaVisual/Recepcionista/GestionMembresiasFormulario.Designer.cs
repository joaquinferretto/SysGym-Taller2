using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Recepcionista
{
    partial class GestionMembresiasFormulario
    {
        private IContainer components;
        private ErrorProvider indicadorErrores;
        private Label lblEstado; private TableLayoutPanel panelContenido; private Panel panelListado; private Label lblListado; private Label lblAyuda; private Panel panelDetalle; private Panel contenedorDetalle; private Label lblFormulario; private Panel contenedorCampos; private Panel panelAcciones; private Label lblSocio; private Label lblPlan; private Label lblInicio; private Label lblVencimiento; private DataGridView tabla; private DataGridViewTextBoxColumn colId; private DataGridViewTextBoxColumn colSocio; private DataGridViewTextBoxColumn colDni; private DataGridViewTextBoxColumn colPlan; private DataGridViewTextBoxColumn colInicio; private DataGridViewTextBoxColumn colVencimiento; private DataGridViewTextBoxColumn colEstado;
        private TextBox buscador; private ComboBox socio; private ComboBox plan; private DateTimePicker inicio; private DateTimePicker vencimiento; private Button nuevo; private Button crear; private Button actualizar; private Button habilitar; private Button deshabilitar; private Button generarCuota;

        protected override void Dispose(bool liberarRecursos) { if (liberarRecursos && components != null) components.Dispose(); base.Dispose(liberarRecursos); }

        private void InitializeComponent()
        {
            components = new Container(); lblEstado = new Label(); panelContenido = new TableLayoutPanel(); panelListado = new Panel(); lblListado = new Label(); lblAyuda = new Label(); buscador = new TextBox(); panelDetalle = new Panel(); contenedorDetalle = new Panel(); lblFormulario = new Label(); contenedorCampos = new Panel(); panelAcciones = new Panel(); lblSocio = new Label(); lblPlan = new Label(); lblInicio = new Label(); lblVencimiento = new Label(); socio = new ComboBox(); plan = new ComboBox(); inicio = new DateTimePicker(); vencimiento = new DateTimePicker(); nuevo = new Button(); crear = new Button(); actualizar = new Button(); habilitar = new Button(); deshabilitar = new Button(); generarCuota = new Button(); tabla = new DataGridView(); colId = new DataGridViewTextBoxColumn(); colSocio = new DataGridViewTextBoxColumn(); colDni = new DataGridViewTextBoxColumn(); colPlan = new DataGridViewTextBoxColumn(); colInicio = new DataGridViewTextBoxColumn(); colVencimiento = new DataGridViewTextBoxColumn(); colEstado = new DataGridViewTextBoxColumn(); indicadorErrores = new ErrorProvider(components); panelContenido.SuspendLayout(); panelListado.SuspendLayout(); ((ISupportInitialize)(tabla)).BeginInit(); panelDetalle.SuspendLayout(); contenedorDetalle.SuspendLayout(); contenedorCampos.SuspendLayout(); panelAcciones.SuspendLayout(); ((ISupportInitialize)(indicadorErrores)).BeginInit(); SuspendLayout();
             lblEstado.BackColor = Color.FromArgb(226, 232, 240);  lblEstado.ForeColor = Color.FromArgb(51, 65, 85);  lblEstado.Padding = new Padding(18, 8, 8, 0); lblEstado.Text = string.Empty; panelContenido.BackColor = Color.FromArgb(248, 250, 252);  panelContenido.Padding = new Padding(12); panelContenido.ColumnCount = 2; panelContenido.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F)); panelContenido.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 396F)); panelContenido.Controls.Add(panelListado, 0, 0); panelContenido.Controls.Add(panelDetalle, 1, 0); panelContenido.RowCount = 1; panelContenido.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            panelListado.BackColor = Color.White; panelListado.BorderStyle = BorderStyle.FixedSingle;  panelListado.Padding = new Padding(16); panelListado.Controls.Add(tabla); panelListado.Controls.Add(buscador); panelListado.Controls.Add(lblAyuda); panelListado.Controls.Add(lblListado);  lblListado.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold); lblListado.ForeColor = Color.FromArgb(30, 41, 59);  lblListado.Text = "Membresias";  lblAyuda.ForeColor = Color.FromArgb(100, 116, 139);  lblAyuda.Text = "Busca por socio, DNI o plan";  buscador.BorderStyle = BorderStyle.FixedSingle;
                tabla.BackgroundColor = Color.White; tabla.BorderStyle = BorderStyle.None;       tabla.Columns.AddRange(new DataGridViewColumn[] { colId, colSocio, colDni, colPlan, colInicio, colVencimiento, colEstado }); colId.HeaderText = "Id"; colId.Name = "colId"; colId.Visible = false; colSocio.HeaderText = "Socio"; colSocio.Name = "colSocio"; colDni.HeaderText = "DNI"; colDni.Name = "colDni"; colPlan.HeaderText = "Plan"; colPlan.Name = "colPlan"; colInicio.HeaderText = "Inicio"; colInicio.Name = "colInicio"; colVencimiento.HeaderText = "Vencimiento"; colVencimiento.Name = "colVencimiento"; colEstado.HeaderText = "Estado"; colEstado.Name = "colEstado";
            panelDetalle.BackColor = Color.White; panelDetalle.BorderStyle = BorderStyle.FixedSingle;  panelDetalle.Padding = new Padding(16); panelDetalle.Controls.Add(contenedorDetalle);       contenedorDetalle.Controls.Add(lblFormulario); contenedorDetalle.Controls.Add(contenedorCampos); contenedorDetalle.Controls.Add(panelAcciones);  lblFormulario.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold); lblFormulario.ForeColor = Color.FromArgb(30, 41, 59); lblFormulario.Text = "Nueva membresia";
                     contenedorCampos.Controls.Add(lblSocio); contenedorCampos.Controls.Add(socio); contenedorCampos.Controls.Add(lblPlan); contenedorCampos.Controls.Add(plan); contenedorCampos.Controls.Add(lblInicio); contenedorCampos.Controls.Add(inicio); contenedorCampos.Controls.Add(lblVencimiento); contenedorCampos.Controls.Add(vencimiento); socio.DropDownStyle = ComboBoxStyle.DropDownList;  plan.DropDownStyle = ComboBoxStyle.DropDownList;  inicio.Format = DateTimePickerFormat.Short;  vencimiento.Format = DateTimePickerFormat.Short;
             panelAcciones.Controls.Add(nuevo); panelAcciones.Controls.Add(crear); panelAcciones.Controls.Add(actualizar); panelAcciones.Controls.Add(habilitar); panelAcciones.Controls.Add(deshabilitar); panelAcciones.Controls.Add(generarCuota);
             lblEstado.Name = "lblEstado";  lblEstado.TabIndex = 3;  panelContenido.Name = "panelContenido";  panelContenido.TabIndex = 2;
            panelListado.Name = "panelListado"; panelListado.TabIndex = 0; lblListado.Name = "lblListado";  lblListado.TabIndex = 0; lblAyuda.Name = "lblAyuda";  lblAyuda.TabIndex = 1; buscador.Name = "buscador"; buscador.TabIndex = 2;
               tabla.Name = "tabla";  tabla.TabIndex = 3; colId.FillWeight = 50; colSocio.FillWeight = 135; colSocio.MinimumWidth = 90; colDni.FillWeight = 85; colDni.MinimumWidth = 90; colPlan.FillWeight = 90; colPlan.MinimumWidth = 90; colInicio.FillWeight = 85; colInicio.MinimumWidth = 90; colVencimiento.FillWeight = 95; colVencimiento.MinimumWidth = 90; colEstado.FillWeight = 100; colEstado.MinimumWidth = 90;
            panelDetalle.Name = "panelDetalle"; panelDetalle.TabIndex = 1; contenedorDetalle.Name = "contenedorDetalle"; contenedorDetalle.TabIndex = 0; lblFormulario.Name = "lblFormulario";  lblFormulario.TabIndex = 0; contenedorCampos.Name = "contenedorCampos";  contenedorCampos.TabIndex = 1; panelAcciones.Name = "panelAcciones"; panelAcciones.TabIndex = 2;
              lblSocio.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold); lblSocio.ForeColor = Color.FromArgb(51, 65, 85); lblSocio.Name = "lblSocio"; lblSocio.Text = "Socio:";   lblPlan.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold); lblPlan.ForeColor = Color.FromArgb(51, 65, 85); lblPlan.Name = "lblPlan"; lblPlan.Text = "Plan:";   lblInicio.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold); lblInicio.ForeColor = Color.FromArgb(51, 65, 85); lblInicio.Name = "lblInicio"; lblInicio.Text = "Inicio:";   lblVencimiento.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold); lblVencimiento.ForeColor = Color.FromArgb(51, 65, 85); lblVencimiento.Name = "lblVencimiento"; lblVencimiento.Text = "Vencimiento:";
            socio.Margin = new Padding(0, 4, 0, 4); socio.Name = "socio"; plan.Margin = new Padding(0, 4, 0, 4); plan.Name = "plan"; inicio.Margin = new Padding(0, 4, 0, 4); inicio.Name = "inicio"; vencimiento.Margin = new Padding(0, 4, 0, 4); vencimiento.Name = "vencimiento";
            nuevo.BackColor = System.Drawing.Color.FromArgb(9, 149, 111); nuevo.FlatAppearance.BorderSize = 0; nuevo.FlatStyle = FlatStyle.Flat; nuevo.ForeColor = Color.White;  nuevo.Name = "nuevo";  nuevo.Text = "+ Nueva"; nuevo.UseVisualStyleBackColor = false; crear.BackColor = System.Drawing.Color.FromArgb(9, 149, 111); crear.FlatAppearance.BorderSize = 0; crear.FlatStyle = FlatStyle.Flat; crear.ForeColor = Color.White;  crear.Name = "crear";  crear.Text = "Crear"; crear.UseVisualStyleBackColor = false; actualizar.BackColor = System.Drawing.Color.FromArgb(72, 66, 217); actualizar.FlatAppearance.BorderSize = 0; actualizar.FlatStyle = FlatStyle.Flat; actualizar.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);  actualizar.Name = "actualizar";  actualizar.Text = "Actualizar"; actualizar.UseVisualStyleBackColor = false; habilitar.BackColor = System.Drawing.Color.FromArgb(220, 252, 231); habilitar.FlatAppearance.BorderSize = 0; habilitar.FlatStyle = FlatStyle.Flat; habilitar.ForeColor = System.Drawing.Color.FromArgb(22, 101, 52);  habilitar.Name = "habilitar";  habilitar.Text = "Reactivar"; habilitar.UseVisualStyleBackColor = false; deshabilitar.BackColor = System.Drawing.Color.FromArgb(255, 240, 240); deshabilitar.FlatAppearance.BorderSize = 0; deshabilitar.FlatStyle = FlatStyle.Flat; deshabilitar.ForeColor = System.Drawing.Color.FromArgb(173, 36, 36);  deshabilitar.Name = "deshabilitar";  deshabilitar.Text = "Dar de baja"; deshabilitar.UseVisualStyleBackColor = false; generarCuota.BackColor = System.Drawing.Color.FromArgb(9, 149, 111); generarCuota.FlatAppearance.BorderSize = 0; generarCuota.FlatStyle = FlatStyle.Flat; generarCuota.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);  generarCuota.Name = "generarCuota";  generarCuota.Text = "Generar cuota"; generarCuota.UseVisualStyleBackColor = false;
            Controls.Add(panelContenido); Controls.Add(lblEstado); indicadorErrores.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            indicadorErrores.ContainerControl = this;
            AutoScaleMode = AutoScaleMode.Font;
             BackColor = Color.FromArgb(241, 245, 249); ClientSize = new Size(1100, 680); Font = new Font("Segoe UI", 9.5F); MinimumSize = new Size(900, 560); this.Name = "GestionMembresiasFormulario"; StartPosition = FormStartPosition.CenterParent; Text = "SysGym";

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

            this.buscador.Margin = new Padding(0, 0, 0, 10);

            this.tabla.Margin = new Padding(0);
            this.tabla.RowTemplate.Height = 30;
            // Detalle: se desplaza solo cuando la altura disponible no alcanza.

            this.panelDetalle.Margin = new Padding(0);
            this.panelDetalle.Padding = new Padding(16);

            this.lblFormulario.Margin = new Padding(0, 0, 0, 12);
            this.lblFormulario.TextAlign = ContentAlignment.MiddleLeft;

            this.lblSocio.Margin = new Padding(0, 0, 8, 8);
            this.lblSocio.TextAlign = ContentAlignment.MiddleLeft;

            this.socio.Margin = new Padding(0, 3, 16, 8);

            this.lblPlan.Margin = new Padding(0, 0, 8, 8);
            this.lblPlan.TextAlign = ContentAlignment.MiddleLeft;

            this.plan.Margin = new Padding(0, 3, 16, 8);

            this.lblInicio.Margin = new Padding(0, 0, 8, 8);
            this.lblInicio.TextAlign = ContentAlignment.MiddleLeft;

            this.inicio.Margin = new Padding(0, 3, 16, 8);

            this.lblVencimiento.Margin = new Padding(0, 0, 8, 8);
            this.lblVencimiento.TextAlign = ContentAlignment.MiddleLeft;

            this.vencimiento.Margin = new Padding(0, 3, 16, 8);
            this.contenedorCampos.Margin = new Padding(0, 0, 0, 16);

            this.panelAcciones.Margin = new Padding(0);

            this.nuevo.Margin = new Padding(0, 0, 8, 8);

            this.crear.Margin = new Padding(0, 0, 8, 8);

            this.actualizar.Margin = new Padding(0, 0, 8, 8);

            this.habilitar.Margin = new Padding(0, 0, 8, 8);

            this.deshabilitar.Margin = new Padding(0, 0, 8, 8);

            this.generarCuota.Margin = new Padding(0, 0, 8, 8);

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
            this.panelDetalle.AutoSize = false;
            this.panelDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDetalle.AutoScroll = true;
            this.contenedorDetalle.AutoSize = false;
            this.contenedorDetalle.Dock = System.Windows.Forms.DockStyle.None;
            this.contenedorDetalle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.contenedorDetalle.Location = new System.Drawing.Point(16, 16);
            this.contenedorDetalle.Size = new System.Drawing.Size(362, 310);
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
            this.contenedorCampos.Size = new System.Drawing.Size(362, 152);
            this.contenedorCampos.AutoScroll = false;
            this.panelAcciones.AutoSize = false;
            this.panelAcciones.Dock = System.Windows.Forms.DockStyle.None;
            this.panelAcciones.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.panelAcciones.Location = new System.Drawing.Point(0, 210);
            this.panelAcciones.Size = new System.Drawing.Size(362, 100);
            this.panelAcciones.AutoScroll = false;
            this.lblSocio.AutoSize = false;
            this.lblSocio.Dock = System.Windows.Forms.DockStyle.None;
            this.lblSocio.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblSocio.Location = new System.Drawing.Point(0, 0);
            this.lblSocio.Size = new System.Drawing.Size(116, 30);
            this.lblPlan.AutoSize = false;
            this.lblPlan.Dock = System.Windows.Forms.DockStyle.None;
            this.lblPlan.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblPlan.Location = new System.Drawing.Point(0, 38);
            this.lblPlan.Size = new System.Drawing.Size(116, 30);
            this.lblInicio.AutoSize = false;
            this.lblInicio.Dock = System.Windows.Forms.DockStyle.None;
            this.lblInicio.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblInicio.Location = new System.Drawing.Point(0, 76);
            this.lblInicio.Size = new System.Drawing.Size(116, 30);
            this.lblVencimiento.AutoSize = false;
            this.lblVencimiento.Dock = System.Windows.Forms.DockStyle.None;
            this.lblVencimiento.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblVencimiento.Location = new System.Drawing.Point(0, 114);
            this.lblVencimiento.Size = new System.Drawing.Size(116, 30);
            this.tabla.AutoSize = false;
            this.tabla.Dock = System.Windows.Forms.DockStyle.None;
            this.tabla.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.tabla.Location = new System.Drawing.Point(16, 106);
            this.tabla.Size = new System.Drawing.Size(622, 494);
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
            this.buscador.Location = new System.Drawing.Point(16, 70);
            this.buscador.Size = new System.Drawing.Size(622, 26);
            this.socio.AutoSize = false;
            this.socio.Dock = System.Windows.Forms.DockStyle.None;
            this.socio.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.socio.Location = new System.Drawing.Point(124, 6);
            this.socio.Size = new System.Drawing.Size(222, 25);
            this.plan.AutoSize = false;
            this.plan.Dock = System.Windows.Forms.DockStyle.None;
            this.plan.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.plan.Location = new System.Drawing.Point(124, 44);
            this.plan.Size = new System.Drawing.Size(222, 25);
            this.inicio.AutoSize = false;
            this.inicio.Dock = System.Windows.Forms.DockStyle.None;
            this.inicio.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.inicio.Location = new System.Drawing.Point(124, 80);
            this.inicio.Size = new System.Drawing.Size(222, 24);
            this.vencimiento.AutoSize = false;
            this.vencimiento.Dock = System.Windows.Forms.DockStyle.None;
            this.vencimiento.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.vencimiento.Location = new System.Drawing.Point(124, 118);
            this.vencimiento.Size = new System.Drawing.Size(222, 24);
            this.nuevo.AutoSize = false;
            this.nuevo.Dock = System.Windows.Forms.DockStyle.None;
            this.nuevo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.nuevo.Location = new System.Drawing.Point(0, 0);
            this.nuevo.Size = new System.Drawing.Size(112, 38);
            this.crear.AutoSize = false;
            this.crear.Dock = System.Windows.Forms.DockStyle.None;
            this.crear.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.crear.Location = new System.Drawing.Point(120, 0);
            this.crear.Size = new System.Drawing.Size(112, 38);
            this.actualizar.AutoSize = false;
            this.actualizar.Dock = System.Windows.Forms.DockStyle.None;
            this.actualizar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.actualizar.Location = new System.Drawing.Point(240, 0);
            this.actualizar.Size = new System.Drawing.Size(112, 38);
            this.habilitar.AutoSize = false;
            this.habilitar.Dock = System.Windows.Forms.DockStyle.None;
            this.habilitar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.habilitar.Location = new System.Drawing.Point(0, 46);
            this.habilitar.Size = new System.Drawing.Size(112, 38);
            this.deshabilitar.AutoSize = false;
            this.deshabilitar.Dock = System.Windows.Forms.DockStyle.None;
            this.deshabilitar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.deshabilitar.Location = new System.Drawing.Point(120, 46);
            this.deshabilitar.Size = new System.Drawing.Size(112, 38);
            this.generarCuota.AutoSize = false;
            this.generarCuota.Dock = System.Windows.Forms.DockStyle.None;
            this.generarCuota.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.generarCuota.Location = new System.Drawing.Point(240, 46);
            this.generarCuota.Size = new System.Drawing.Size(112, 38);
            panelContenido.ResumeLayout(false); panelListado.ResumeLayout(false); panelListado.PerformLayout(); ((ISupportInitialize)(tabla)).EndInit(); panelDetalle.ResumeLayout(false); contenedorDetalle.ResumeLayout(false); contenedorDetalle.PerformLayout(); contenedorCampos.ResumeLayout(false); contenedorCampos.PerformLayout(); panelAcciones.ResumeLayout(false); ((ISupportInitialize)(indicadorErrores)).EndInit(); ResumeLayout(false);

            this.Load += new System.EventHandler(this.GestionMembresiasFormulario_Load);
            this.tabla.SelectionChanged += new System.EventHandler(this.tabla_SelectionChanged);
            this.buscador.TextChanged += new System.EventHandler(this.buscador_TextChanged);
            this.inicio.ValueChanged += new System.EventHandler(this.inicio_ValueChanged);
            this.nuevo.Click += new System.EventHandler(this.nuevo_Click);
            this.crear.Click += new System.EventHandler(this.crear_Click);
            this.actualizar.Click += new System.EventHandler(this.actualizar_Click);
            this.habilitar.Click += new System.EventHandler(this.habilitar_Click);
            this.deshabilitar.Click += new System.EventHandler(this.deshabilitar_Click);
            this.generarCuota.Click += new System.EventHandler(this.generarCuota_Click);
                }

    }
}
