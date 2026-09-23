namespace exxen2._0.capaVisual.Entrenador
{
    partial class MisSociosFormulario
    {
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.ErrorProvider indicadorErrores;
        private System.Windows.Forms.Label lblBuscar;
        private System.Windows.Forms.TextBox buscador;
        private System.Windows.Forms.Label lblFiltroRutina;
        private System.Windows.Forms.ComboBox filtroRutina;
        private System.Windows.Forms.Button actualizar;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.SplitContainer splitContenido;
        private System.Windows.Forms.Label lblListadoTitulo;
        private System.Windows.Forms.DataGridView tabla;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIdSocio;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIdMembresia;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSocio;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDni;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPlan;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRutina;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVencimiento;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEstadoRutina;
        private System.Windows.Forms.Label lblDetalleTitulo;
        private System.Windows.Forms.Label lblRutinaTitulo;
        private System.Windows.Forms.Label lblSocio;
        private System.Windows.Forms.TextBox txtSocio;
        private System.Windows.Forms.Label lblDni;
        private System.Windows.Forms.TextBox txtDni;
        private System.Windows.Forms.Label lblPlan;
        private System.Windows.Forms.TextBox txtPlan;
        private System.Windows.Forms.Label lblEntrenador;
        private System.Windows.Forms.TextBox txtEntrenador;
        private System.Windows.Forms.Label lblVencimiento;
        private System.Windows.Forms.TextBox txtVencimiento;
        private System.Windows.Forms.Label lblRutina;
        private System.Windows.Forms.TextBox txtRutina;
        private System.Windows.Forms.DataGridView tablaRutina;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDia;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEjercicio;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSeries;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRepeticiones;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPeso;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescanso;
        private System.Windows.Forms.Label lblAccionInfo;
        private System.Windows.Forms.ComboBox rutinaDisponible;
        private System.Windows.Forms.Button asignarRutina;
        private System.Windows.Forms.Button verRutina;
        private System.Windows.Forms.Button crearPersonalizada;
        private System.Windows.Forms.Button exportarPdf;

        protected override void Dispose(bool liberarRecursos)
        {
            if (liberarRecursos && components != null) components.Dispose();
            base.Dispose(liberarRecursos);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.indicadorErrores = new System.Windows.Forms.ErrorProvider(this.components);
            this.lblBuscar = new System.Windows.Forms.Label();
            this.buscador = new System.Windows.Forms.TextBox();
            this.lblFiltroRutina = new System.Windows.Forms.Label();
            this.filtroRutina = new System.Windows.Forms.ComboBox();
            this.actualizar = new System.Windows.Forms.Button();
            this.lblEstado = new System.Windows.Forms.Label();
            this.splitContenido = new System.Windows.Forms.SplitContainer();
            this.lblListadoTitulo = new System.Windows.Forms.Label();
            this.tabla = new System.Windows.Forms.DataGridView();
            this.colIdSocio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIdMembresia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSocio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDni = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPlan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRutina = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVencimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstadoRutina = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblDetalleTitulo = new System.Windows.Forms.Label();
            this.lblSocio = new System.Windows.Forms.Label();
            this.txtSocio = new System.Windows.Forms.TextBox();
            this.lblDni = new System.Windows.Forms.Label();
            this.txtDni = new System.Windows.Forms.TextBox();
            this.lblPlan = new System.Windows.Forms.Label();
            this.txtPlan = new System.Windows.Forms.TextBox();
            this.lblEntrenador = new System.Windows.Forms.Label();
            this.txtEntrenador = new System.Windows.Forms.TextBox();
            this.lblVencimiento = new System.Windows.Forms.Label();
            this.txtVencimiento = new System.Windows.Forms.TextBox();
            this.lblRutina = new System.Windows.Forms.Label();
            this.txtRutina = new System.Windows.Forms.TextBox();
            this.lblRutinaTitulo = new System.Windows.Forms.Label();
            this.tablaRutina = new System.Windows.Forms.DataGridView();
            this.colDia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEjercicio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSeries = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRepeticiones = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPeso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescanso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblAccionInfo = new System.Windows.Forms.Label();
            this.rutinaDisponible = new System.Windows.Forms.ComboBox();
            this.asignarRutina = new System.Windows.Forms.Button();
            this.verRutina = new System.Windows.Forms.Button();
            this.crearPersonalizada = new System.Windows.Forms.Button();
            this.exportarPdf = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.indicadorErrores)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContenido)).BeginInit();
            this.splitContenido.Panel1.SuspendLayout();
            this.splitContenido.Panel2.SuspendLayout();
            this.splitContenido.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablaRutina)).BeginInit();
            this.SuspendLayout();
            //
            // indicadorErrores
            //
            this.indicadorErrores.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.indicadorErrores.ContainerControl = this;
            //
            // lblBuscar
            //
            this.lblBuscar.Location = new System.Drawing.Point(4, 36);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(60, 26);
            this.lblBuscar.TabIndex = 1;
            this.lblBuscar.Text = "Buscar:";
            this.lblBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // buscador
            //
            this.buscador.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buscador.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.buscador.Location = new System.Drawing.Point(67, 36);
            this.buscador.Name = "buscador";
            this.buscador.Size = new System.Drawing.Size(331, 24);
            this.buscador.TabIndex = 1;
            this.buscador.TextChanged += new System.EventHandler(this.buscador_TextChanged);
            //
            // lblFiltroRutina
            //
            this.lblFiltroRutina.Location = new System.Drawing.Point(4, 68);
            this.lblFiltroRutina.Name = "lblFiltroRutina";
            this.lblFiltroRutina.Size = new System.Drawing.Size(60, 26);
            this.lblFiltroRutina.TabIndex = 2;
            this.lblFiltroRutina.Text = "Rutina:";
            this.lblFiltroRutina.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // filtroRutina
            //
            this.filtroRutina.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filtroRutina.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filtroRutina.Items.AddRange(new object[] {
            "Todas",
            "Con rutina",
            "Sin rutina"});
            this.filtroRutina.Location = new System.Drawing.Point(67, 68);
            this.filtroRutina.Name = "filtroRutina";
            this.filtroRutina.Size = new System.Drawing.Size(331, 25);
            this.filtroRutina.TabIndex = 2;
            this.filtroRutina.SelectedIndexChanged += new System.EventHandler(this.filtroRutina_SelectedIndexChanged);
            //
            // actualizar
            //
            this.actualizar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(237)))), ((int)(((byte)(247)))));
            this.actualizar.FlatAppearance.BorderSize = 0;
            this.actualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.actualizar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(68)))), ((int)(((byte)(95)))));
            this.actualizar.Location = new System.Drawing.Point(124, 106);
            this.actualizar.Name = "actualizar";
            this.actualizar.Size = new System.Drawing.Size(150, 32);
            this.actualizar.TabIndex = 3;
            this.actualizar.Text = "Actualizar listado";
            this.actualizar.UseVisualStyleBackColor = false;
            this.actualizar.Click += new System.EventHandler(this.actualizar_Click);
            //
            // lblEstado
            //
            this.lblEstado.BackColor = System.Drawing.Color.Transparent;
            this.lblEstado.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblEstado.Location = new System.Drawing.Point(16, 636);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Padding = new System.Windows.Forms.Padding(16, 0, 8, 0);
            this.lblEstado.Size = new System.Drawing.Size(1068, 28);
            this.lblEstado.TabIndex = 1;
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // splitContenido
            //
            this.splitContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContenido.IsSplitterFixed = true;
            this.splitContenido.Location = new System.Drawing.Point(16, 16);
            this.splitContenido.MinimumSize = new System.Drawing.Size(862, 420);
            this.splitContenido.Name = "splitContenido";
            //
            // splitContenido.Panel1
            //
            this.splitContenido.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.splitContenido.Panel1.Controls.Add(this.lblListadoTitulo);
            this.splitContenido.Panel1.Controls.Add(this.lblBuscar);
            this.splitContenido.Panel1.Controls.Add(this.buscador);
            this.splitContenido.Panel1.Controls.Add(this.lblFiltroRutina);
            this.splitContenido.Panel1.Controls.Add(this.filtroRutina);
            this.splitContenido.Panel1.Controls.Add(this.actualizar);
            this.splitContenido.Panel1.Controls.Add(this.tabla);
            this.splitContenido.Panel1.Padding = new System.Windows.Forms.Padding(16);
            this.splitContenido.Panel1MinSize = 300;
            //
            // splitContenido.Panel2
            //
            this.splitContenido.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.splitContenido.Panel2.Controls.Add(this.lblDetalleTitulo);
            this.splitContenido.Panel2.Controls.Add(this.lblSocio);
            this.splitContenido.Panel2.Controls.Add(this.txtSocio);
            this.splitContenido.Panel2.Controls.Add(this.lblDni);
            this.splitContenido.Panel2.Controls.Add(this.txtDni);
            this.splitContenido.Panel2.Controls.Add(this.lblPlan);
            this.splitContenido.Panel2.Controls.Add(this.txtPlan);
            this.splitContenido.Panel2.Controls.Add(this.lblEntrenador);
            this.splitContenido.Panel2.Controls.Add(this.txtEntrenador);
            this.splitContenido.Panel2.Controls.Add(this.lblVencimiento);
            this.splitContenido.Panel2.Controls.Add(this.txtVencimiento);
            this.splitContenido.Panel2.Controls.Add(this.lblRutina);
            this.splitContenido.Panel2.Controls.Add(this.txtRutina);
            this.splitContenido.Panel2.Controls.Add(this.lblRutinaTitulo);
            this.splitContenido.Panel2.Controls.Add(this.tablaRutina);
            this.splitContenido.Panel2.Controls.Add(this.lblAccionInfo);
            this.splitContenido.Panel2.Controls.Add(this.rutinaDisponible);
            this.splitContenido.Panel2.Controls.Add(this.asignarRutina);
            this.splitContenido.Panel2.Controls.Add(this.verRutina);
            this.splitContenido.Panel2.Controls.Add(this.crearPersonalizada);
            this.splitContenido.Panel2.Controls.Add(this.exportarPdf);
            this.splitContenido.Panel2.Padding = new System.Windows.Forms.Padding(0, 16, 16, 16);
            this.splitContenido.Panel2MinSize = 550;
            this.splitContenido.Size = new System.Drawing.Size(1068, 620);
            this.splitContenido.SplitterDistance = 404;
            this.splitContenido.SplitterWidth = 6;
            this.splitContenido.TabIndex = 0;
            this.splitContenido.TabStop = false;
            //
            // lblListadoTitulo
            //
            this.lblListadoTitulo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblListadoTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblListadoTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblListadoTitulo.Location = new System.Drawing.Point(3, 0);
            this.lblListadoTitulo.Name = "lblListadoTitulo";
            this.lblListadoTitulo.Size = new System.Drawing.Size(214, 30);
            this.lblListadoTitulo.TabIndex = 0;
            this.lblListadoTitulo.Text = "Socios con membresía activa";
            this.lblListadoTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.tabla.ColumnHeadersHeight = 34;
            this.tabla.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colIdSocio,
            this.colIdMembresia,
            this.colSocio,
            this.colDni,
            this.colPlan,
            this.colRutina,
            this.colVencimiento,
            this.colEstadoRutina});
            this.tabla.Location = new System.Drawing.Point(3, 148);
            this.tabla.MultiSelect = false;
            this.tabla.Name = "tabla";
            this.tabla.ReadOnly = true;
            this.tabla.RowHeadersVisible = false;
            this.tabla.RowTemplate.Height = 28;
            this.tabla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tabla.Size = new System.Drawing.Size(382, 456);
            this.tabla.TabIndex = 4;
            this.tabla.SelectionChanged += new System.EventHandler(this.tabla_SelectionChanged);
            //
            // colIdSocio
            //
            this.colIdSocio.Name = "colIdSocio";
            this.colIdSocio.ReadOnly = true;
            this.colIdSocio.Visible = false;
            //
            // colIdMembresia
            //
            this.colIdMembresia.Name = "colIdMembresia";
            this.colIdMembresia.ReadOnly = true;
            this.colIdMembresia.Visible = false;
            //
            // colSocio
            //
            this.colSocio.FillWeight = 28F;
            this.colSocio.HeaderText = "Socio";
            this.colSocio.MinimumWidth = 60;
            this.colSocio.Name = "colSocio";
            this.colSocio.ReadOnly = true;
            //
            // colDni
            //
            this.colDni.FillWeight = 16F;
            this.colDni.HeaderText = "DNI";
            this.colDni.MinimumWidth = 50;
            this.colDni.Name = "colDni";
            this.colDni.ReadOnly = true;
            //
            // colPlan
            //
            this.colPlan.FillWeight = 20F;
            this.colPlan.HeaderText = "Plan";
            this.colPlan.MinimumWidth = 50;
            this.colPlan.Name = "colPlan";
            this.colPlan.ReadOnly = true;
            //
            // colRutina
            //
            this.colRutina.FillWeight = 22F;
            this.colRutina.HeaderText = "Rutina";
            this.colRutina.MinimumWidth = 62;
            this.colRutina.Name = "colRutina";
            this.colRutina.ReadOnly = true;
            //
            // colVencimiento
            //
            this.colVencimiento.FillWeight = 16F;
            this.colVencimiento.HeaderText = "Cuota hasta";
            this.colVencimiento.MinimumWidth = 62;
            this.colVencimiento.Name = "colVencimiento";
            this.colVencimiento.ReadOnly = true;
            //
            // colEstadoRutina
            //
            this.colEstadoRutina.FillWeight = 16F;
            this.colEstadoRutina.HeaderText = "Estado";
            this.colEstadoRutina.MinimumWidth = 66;
            this.colEstadoRutina.Name = "colEstadoRutina";
            this.colEstadoRutina.ReadOnly = true;
            //
            // lblDetalleTitulo
            //
            this.lblDetalleTitulo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDetalleTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblDetalleTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblDetalleTitulo.Location = new System.Drawing.Point(10, 8);
            this.lblDetalleTitulo.Name = "lblDetalleTitulo";
            this.lblDetalleTitulo.Size = new System.Drawing.Size(629, 28);
            this.lblDetalleTitulo.TabIndex = 0;
            this.lblDetalleTitulo.Text = "Socio seleccionado";
            //
            // lblSocio
            //
            this.lblSocio.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblSocio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblSocio.Location = new System.Drawing.Point(10, 42);
            this.lblSocio.Name = "lblSocio";
            this.lblSocio.Size = new System.Drawing.Size(100, 24);
            this.lblSocio.TabIndex = 1;
            this.lblSocio.Text = "Socio:";
            this.lblSocio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // txtSocio
            //
            this.txtSocio.BackColor = System.Drawing.SystemColors.Window;
            this.txtSocio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSocio.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtSocio.Location = new System.Drawing.Point(116, 42);
            this.txtSocio.Name = "txtSocio";
            this.txtSocio.ReadOnly = true;
            this.txtSocio.Size = new System.Drawing.Size(180, 24);
            this.txtSocio.TabIndex = 2;
            this.txtSocio.TabStop = false;
            this.txtSocio.Text = "Selecciona un socio";
            //
            // lblDni
            //
            this.lblDni.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblDni.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblDni.Location = new System.Drawing.Point(306, 42);
            this.lblDni.Name = "lblDni";
            this.lblDni.Size = new System.Drawing.Size(100, 24);
            this.lblDni.TabIndex = 3;
            this.lblDni.Text = "DNI:";
            this.lblDni.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // txtDni
            //
            this.txtDni.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDni.BackColor = System.Drawing.SystemColors.Window;
            this.txtDni.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDni.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtDni.Location = new System.Drawing.Point(412, 42);
            this.txtDni.Name = "txtDni";
            this.txtDni.ReadOnly = true;
            this.txtDni.Size = new System.Drawing.Size(212, 24);
            this.txtDni.TabIndex = 4;
            this.txtDni.TabStop = false;
            this.txtDni.Text = "-";
            //
            // lblPlan
            //
            this.lblPlan.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblPlan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblPlan.Location = new System.Drawing.Point(10, 74);
            this.lblPlan.Name = "lblPlan";
            this.lblPlan.Size = new System.Drawing.Size(100, 24);
            this.lblPlan.TabIndex = 5;
            this.lblPlan.Text = "Plan:";
            this.lblPlan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // txtPlan
            //
            this.txtPlan.BackColor = System.Drawing.SystemColors.Window;
            this.txtPlan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPlan.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtPlan.Location = new System.Drawing.Point(116, 74);
            this.txtPlan.Name = "txtPlan";
            this.txtPlan.ReadOnly = true;
            this.txtPlan.Size = new System.Drawing.Size(180, 24);
            this.txtPlan.TabIndex = 6;
            this.txtPlan.TabStop = false;
            this.txtPlan.Text = "-";
            //
            // lblEntrenador
            //
            this.lblEntrenador.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblEntrenador.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblEntrenador.Location = new System.Drawing.Point(306, 74);
            this.lblEntrenador.Name = "lblEntrenador";
            this.lblEntrenador.Size = new System.Drawing.Size(100, 24);
            this.lblEntrenador.TabIndex = 7;
            this.lblEntrenador.Text = "Entrenador:";
            this.lblEntrenador.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // txtEntrenador
            //
            this.txtEntrenador.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEntrenador.BackColor = System.Drawing.SystemColors.Window;
            this.txtEntrenador.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEntrenador.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtEntrenador.Location = new System.Drawing.Point(412, 74);
            this.txtEntrenador.Name = "txtEntrenador";
            this.txtEntrenador.ReadOnly = true;
            this.txtEntrenador.Size = new System.Drawing.Size(212, 24);
            this.txtEntrenador.TabIndex = 8;
            this.txtEntrenador.TabStop = false;
            this.txtEntrenador.Text = "-";
            //
            // lblVencimiento
            //
            this.lblVencimiento.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblVencimiento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblVencimiento.Location = new System.Drawing.Point(10, 106);
            this.lblVencimiento.Name = "lblVencimiento";
            this.lblVencimiento.Size = new System.Drawing.Size(100, 24);
            this.lblVencimiento.TabIndex = 9;
            this.lblVencimiento.Text = "Cuota hasta:";
            this.lblVencimiento.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // txtVencimiento
            //
            this.txtVencimiento.BackColor = System.Drawing.SystemColors.Window;
            this.txtVencimiento.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVencimiento.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtVencimiento.Location = new System.Drawing.Point(116, 106);
            this.txtVencimiento.Name = "txtVencimiento";
            this.txtVencimiento.ReadOnly = true;
            this.txtVencimiento.Size = new System.Drawing.Size(180, 24);
            this.txtVencimiento.TabIndex = 10;
            this.txtVencimiento.TabStop = false;
            this.txtVencimiento.Text = "-";
            //
            // lblRutina
            //
            this.lblRutina.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblRutina.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblRutina.Location = new System.Drawing.Point(306, 106);
            this.lblRutina.Name = "lblRutina";
            this.lblRutina.Size = new System.Drawing.Size(100, 24);
            this.lblRutina.TabIndex = 11;
            this.lblRutina.Text = "Rutina:";
            this.lblRutina.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // txtRutina
            //
            this.txtRutina.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRutina.BackColor = System.Drawing.SystemColors.Window;
            this.txtRutina.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRutina.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtRutina.Location = new System.Drawing.Point(412, 106);
            this.txtRutina.Name = "txtRutina";
            this.txtRutina.ReadOnly = true;
            this.txtRutina.Size = new System.Drawing.Size(212, 24);
            this.txtRutina.TabIndex = 12;
            this.txtRutina.TabStop = false;
            this.txtRutina.Text = "-";
            //
            // lblRutinaTitulo
            //
            this.lblRutinaTitulo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRutinaTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblRutinaTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblRutinaTitulo.Location = new System.Drawing.Point(10, 148);
            this.lblRutinaTitulo.Name = "lblRutinaTitulo";
            this.lblRutinaTitulo.Size = new System.Drawing.Size(614, 28);
            this.lblRutinaTitulo.TabIndex = 13;
            this.lblRutinaTitulo.Text = "Rutina semanal";
            //
            // tablaRutina
            //
            this.tablaRutina.AllowUserToAddRows = false;
            this.tablaRutina.AllowUserToDeleteRows = false;
            this.tablaRutina.AllowUserToResizeRows = false;
            this.tablaRutina.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tablaRutina.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tablaRutina.BackgroundColor = System.Drawing.Color.White;
            this.tablaRutina.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tablaRutina.ColumnHeadersHeight = 34;
            this.tablaRutina.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDia,
            this.colEjercicio,
            this.colSeries,
            this.colRepeticiones,
            this.colPeso,
            this.colDescanso});
            this.tablaRutina.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tablaRutina.Location = new System.Drawing.Point(10, 184);
            this.tablaRutina.MultiSelect = false;
            this.tablaRutina.Name = "tablaRutina";
            this.tablaRutina.ReadOnly = true;
            this.tablaRutina.RowHeadersVisible = false;
            this.tablaRutina.RowTemplate.Height = 28;
            this.tablaRutina.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tablaRutina.Size = new System.Drawing.Size(614, 256);
            this.tablaRutina.TabIndex = 5;
            //
            // colDia
            //
            this.colDia.FillWeight = 16F;
            this.colDia.HeaderText = "Dia";
            this.colDia.MinimumWidth = 54;
            this.colDia.Name = "colDia";
            this.colDia.ReadOnly = true;
            //
            // colEjercicio
            //
            this.colEjercicio.FillWeight = 30F;
            this.colEjercicio.HeaderText = "Ejercicio";
            this.colEjercicio.MinimumWidth = 100;
            this.colEjercicio.Name = "colEjercicio";
            this.colEjercicio.ReadOnly = true;
            //
            // colSeries
            //
            this.colSeries.FillWeight = 12F;
            this.colSeries.HeaderText = "Series";
            this.colSeries.MinimumWidth = 48;
            this.colSeries.Name = "colSeries";
            this.colSeries.ReadOnly = true;
            //
            // colRepeticiones
            //
            this.colRepeticiones.FillWeight = 17F;
            this.colRepeticiones.HeaderText = "Repeticiones";
            this.colRepeticiones.MinimumWidth = 94;
            this.colRepeticiones.Name = "colRepeticiones";
            this.colRepeticiones.ReadOnly = true;
            //
            // colPeso
            //
            this.colPeso.FillWeight = 12F;
            this.colPeso.HeaderText = "Peso";
            this.colPeso.MinimumWidth = 48;
            this.colPeso.Name = "colPeso";
            this.colPeso.ReadOnly = true;
            //
            // colDescanso
            //
            this.colDescanso.FillWeight = 15F;
            this.colDescanso.HeaderText = "Descanso";
            this.colDescanso.MinimumWidth = 78;
            this.colDescanso.Name = "colDescanso";
            this.colDescanso.ReadOnly = true;
            //
            // lblAccionInfo
            //
            this.lblAccionInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAccionInfo.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblAccionInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblAccionInfo.Location = new System.Drawing.Point(7, 462);
            this.lblAccionInfo.Margin = new System.Windows.Forms.Padding(0, 4, 8, 0);
            this.lblAccionInfo.Name = "lblAccionInfo";
            this.lblAccionInfo.Size = new System.Drawing.Size(315, 44);
            this.lblAccionInfo.TabIndex = 14;
            this.lblAccionInfo.Text = "Selecciona un socio para ver su rutina y acciones.";
            this.lblAccionInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // rutinaDisponible
            //
            this.rutinaDisponible.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rutinaDisponible.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.rutinaDisponible.Location = new System.Drawing.Point(10, 510);
            this.rutinaDisponible.Margin = new System.Windows.Forms.Padding(0, 4, 8, 0);
            this.rutinaDisponible.Name = "rutinaDisponible";
            this.rutinaDisponible.Size = new System.Drawing.Size(470, 25);
            this.rutinaDisponible.TabIndex = 6;
            this.rutinaDisponible.Enabled = false;
            //
            // asignarRutina
            //
            this.asignarRutina.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.asignarRutina.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(66)))), ((int)(((byte)(217)))));
            this.asignarRutina.FlatAppearance.BorderSize = 0;
            this.asignarRutina.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.asignarRutina.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.asignarRutina.ForeColor = System.Drawing.Color.White;
            this.asignarRutina.Location = new System.Drawing.Point(488, 510);
            this.asignarRutina.Margin = new System.Windows.Forms.Padding(0, 2, 8, 0);
            this.asignarRutina.Name = "asignarRutina";
            this.asignarRutina.Size = new System.Drawing.Size(136, 32);
            this.asignarRutina.TabIndex = 7;
            this.asignarRutina.Text = "Asignar rutina";
            this.asignarRutina.UseVisualStyleBackColor = false;
            this.asignarRutina.Enabled = false;
            this.asignarRutina.Click += new System.EventHandler(this.asignarRutina_Click);
            //
            // verRutina
            //
            this.verRutina.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.verRutina.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(237)))), ((int)(((byte)(247)))));
            this.verRutina.FlatAppearance.BorderSize = 0;
            this.verRutina.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.verRutina.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.verRutina.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(68)))), ((int)(((byte)(95)))));
            this.verRutina.Location = new System.Drawing.Point(317, 550);
            this.verRutina.Margin = new System.Windows.Forms.Padding(0, 2, 8, 0);
            this.verRutina.Name = "verRutina";
            this.verRutina.Size = new System.Drawing.Size(163, 32);
            this.verRutina.TabIndex = 9;
            this.verRutina.Text = "Ver / editar";
            this.verRutina.UseVisualStyleBackColor = false;
            this.verRutina.Enabled = false;
            this.verRutina.Click += new System.EventHandler(this.verRutina_Click);
            //
            // crearPersonalizada
            //
            this.crearPersonalizada.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.crearPersonalizada.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(149)))), ((int)(((byte)(111)))));
            this.crearPersonalizada.FlatAppearance.BorderSize = 0;
            this.crearPersonalizada.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.crearPersonalizada.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.crearPersonalizada.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.crearPersonalizada.Location = new System.Drawing.Point(117, 550);
            this.crearPersonalizada.Margin = new System.Windows.Forms.Padding(0, 2, 8, 0);
            this.crearPersonalizada.Name = "crearPersonalizada";
            this.crearPersonalizada.Size = new System.Drawing.Size(192, 32);
            this.crearPersonalizada.TabIndex = 8;
            this.crearPersonalizada.Text = "Crear personalizada";
            this.crearPersonalizada.UseVisualStyleBackColor = false;
            this.crearPersonalizada.Enabled = false;
            this.crearPersonalizada.Click += new System.EventHandler(this.crearPersonalizada_Click);
            //
            // exportarPdf
            //
            this.exportarPdf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.exportarPdf.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(237)))), ((int)(((byte)(247)))));
            this.exportarPdf.Enabled = false;
            this.exportarPdf.FlatAppearance.BorderSize = 0;
            this.exportarPdf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exportarPdf.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.exportarPdf.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(68)))), ((int)(((byte)(95)))));
            this.exportarPdf.Location = new System.Drawing.Point(488, 550);
            this.exportarPdf.Name = "exportarPdf";
            this.exportarPdf.Size = new System.Drawing.Size(136, 32);
            this.exportarPdf.TabIndex = 10;
            this.exportarPdf.Text = "Exportar PDF";
            this.exportarPdf.UseVisualStyleBackColor = false;
            this.exportarPdf.Click += new System.EventHandler(this.exportarPdf_Click);
            //
            // MisSociosFormulario
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1100, 680);
            this.Controls.Add(this.splitContenido);
            this.Controls.Add(this.lblEstado);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.MinimumSize = new System.Drawing.Size(916, 560);
            this.Name = "MisSociosFormulario";
            this.Padding = new System.Windows.Forms.Padding(16);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SysGym";
            this.Load += new System.EventHandler(this.MisSociosFormulario_Load);
            ((System.ComponentModel.ISupportInitialize)(this.indicadorErrores)).EndInit();
            this.splitContenido.Panel1.ResumeLayout(false);
            this.splitContenido.Panel1.PerformLayout();
            this.splitContenido.Panel2.ResumeLayout(false);
            this.splitContenido.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContenido)).EndInit();
            this.splitContenido.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablaRutina)).EndInit();
            this.ResumeLayout(false);

        }

    }
}
