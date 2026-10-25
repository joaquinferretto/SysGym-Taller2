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
        private System.Windows.Forms.ContextMenuStrip menuAlumnos;
        private System.Windows.Forms.ToolStripMenuItem actualizarListadoMenu;
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
        private System.Windows.Forms.Label lblSugerencia;
        private System.Windows.Forms.Label lblObjetivo;
        private System.Windows.Forms.ComboBox objetivo;
        private System.Windows.Forms.Label lblNivel;
        private System.Windows.Forms.ComboBox nivel;
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.indicadorErrores = new System.Windows.Forms.ErrorProvider(this.components);
            this.menuAlumnos = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.actualizarListadoMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.lblBuscar = new System.Windows.Forms.Label();
            this.buscador = new System.Windows.Forms.TextBox();
            this.lblFiltroRutina = new System.Windows.Forms.Label();
            this.filtroRutina = new System.Windows.Forms.ComboBox();
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
            this.lblSugerencia = new System.Windows.Forms.Label();
            this.lblObjetivo = new System.Windows.Forms.Label();
            this.objetivo = new System.Windows.Forms.ComboBox();
            this.lblNivel = new System.Windows.Forms.Label();
            this.nivel = new System.Windows.Forms.ComboBox();
            this.rutinaDisponible = new System.Windows.Forms.ComboBox();
            this.asignarRutina = new System.Windows.Forms.Button();
            this.verRutina = new System.Windows.Forms.Button();
            this.crearPersonalizada = new System.Windows.Forms.Button();
            this.exportarPdf = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.indicadorErrores)).BeginInit();
            this.menuAlumnos.SuspendLayout();
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
            // menuAlumnos
            //
            this.menuAlumnos.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuAlumnos.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.actualizarListadoMenu});
            this.menuAlumnos.Name = "menuAlumnos";
            this.menuAlumnos.Size = new System.Drawing.Size(194, 28);
            //
            // actualizarListadoMenu
            //
            this.actualizarListadoMenu.Name = "actualizarListadoMenu";
            this.actualizarListadoMenu.Size = new System.Drawing.Size(193, 24);
            this.actualizarListadoMenu.Text = "Actualizar listado";
            this.actualizarListadoMenu.Click += new System.EventHandler(this.actualizar_Click);
            //
            // lblBuscar
            //
            this.lblBuscar.Location = new System.Drawing.Point(5, 45);
            this.lblBuscar.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(75, 32);
            this.lblBuscar.TabIndex = 1;
            this.lblBuscar.Text = "Buscar:";
            this.lblBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // buscador
            //
            this.buscador.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buscador.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.buscador.Location = new System.Drawing.Point(84, 45);
            this.buscador.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buscador.Name = "buscador";
            this.buscador.Size = new System.Drawing.Size(413, 29);
            this.buscador.TabIndex = 1;
            this.buscador.TextChanged += new System.EventHandler(this.buscador_TextChanged);
            //
            // lblFiltroRutina
            //
            this.lblFiltroRutina.Location = new System.Drawing.Point(5, 85);
            this.lblFiltroRutina.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFiltroRutina.Name = "lblFiltroRutina";
            this.lblFiltroRutina.Size = new System.Drawing.Size(75, 32);
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
            this.filtroRutina.Location = new System.Drawing.Point(84, 85);
            this.filtroRutina.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.filtroRutina.Name = "filtroRutina";
            this.filtroRutina.Size = new System.Drawing.Size(413, 29);
            this.filtroRutina.TabIndex = 2;
            this.filtroRutina.SelectedIndexChanged += new System.EventHandler(this.filtroRutina_SelectedIndexChanged);
            //
            // lblEstado
            //
            this.lblEstado.BackColor = System.Drawing.Color.Transparent;
            this.lblEstado.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblEstado.Location = new System.Drawing.Point(20, 795);
            this.lblEstado.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Padding = new System.Windows.Forms.Padding(20, 0, 10, 0);
            this.lblEstado.Size = new System.Drawing.Size(1335, 35);
            this.lblEstado.TabIndex = 1;
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // splitContenido
            //
            this.splitContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContenido.IsSplitterFixed = true;
            this.splitContenido.Location = new System.Drawing.Point(20, 20);
            this.splitContenido.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitContenido.MinimumSize = new System.Drawing.Size(1078, 525);
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
            this.splitContenido.Panel1.Controls.Add(this.tabla);
            this.splitContenido.Panel1.Padding = new System.Windows.Forms.Padding(20, 20, 20, 20);
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
            this.splitContenido.Panel2.Controls.Add(this.lblSugerencia);
            this.splitContenido.Panel2.Controls.Add(this.lblObjetivo);
            this.splitContenido.Panel2.Controls.Add(this.objetivo);
            this.splitContenido.Panel2.Controls.Add(this.lblNivel);
            this.splitContenido.Panel2.Controls.Add(this.nivel);
            this.splitContenido.Panel2.Controls.Add(this.rutinaDisponible);
            this.splitContenido.Panel2.Controls.Add(this.asignarRutina);
            this.splitContenido.Panel2.Controls.Add(this.verRutina);
            this.splitContenido.Panel2.Controls.Add(this.crearPersonalizada);
            this.splitContenido.Panel2.Controls.Add(this.exportarPdf);
            this.splitContenido.Panel2.Padding = new System.Windows.Forms.Padding(0, 20, 20, 20);
            this.splitContenido.Panel2MinSize = 550;
            this.splitContenido.Size = new System.Drawing.Size(1335, 775);
            this.splitContenido.SplitterDistance = 505;
            this.splitContenido.SplitterWidth = 8;
            this.splitContenido.TabIndex = 0;
            this.splitContenido.TabStop = false;
            //
            // lblListadoTitulo
            //
            this.lblListadoTitulo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblListadoTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblListadoTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblListadoTitulo.Location = new System.Drawing.Point(4, 0);
            this.lblListadoTitulo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblListadoTitulo.Name = "lblListadoTitulo";
            this.lblListadoTitulo.Size = new System.Drawing.Size(268, 38);
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
            this.tabla.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
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
            this.tabla.ContextMenuStrip = this.menuAlumnos;
            this.tabla.Location = new System.Drawing.Point(4, 132);
            this.tabla.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabla.MultiSelect = false;
            this.tabla.Name = "tabla";
            this.tabla.ReadOnly = true;
            this.tabla.RowHeadersVisible = false;
            this.tabla.RowHeadersWidth = 51;
            this.tabla.RowTemplate.Height = 28;
            this.tabla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tabla.Size = new System.Drawing.Size(478, 570);
            this.tabla.TabIndex = 4;
            this.tabla.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.tabla_CellMouseDown);
            this.tabla.SelectionChanged += new System.EventHandler(this.tabla_SelectionChanged);
            //
            // colIdSocio
            //
            this.colIdSocio.MinimumWidth = 6;
            this.colIdSocio.Name = "colIdSocio";
            this.colIdSocio.ReadOnly = true;
            this.colIdSocio.Visible = false;
            //
            // colIdMembresia
            //
            this.colIdMembresia.MinimumWidth = 6;
            this.colIdMembresia.Name = "colIdMembresia";
            this.colIdMembresia.ReadOnly = true;
            this.colIdMembresia.Visible = false;
            //
            // colSocio
            //
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.colSocio.DefaultCellStyle = dataGridViewCellStyle1;
            this.colSocio.FillWeight = 35F;
            this.colSocio.HeaderText = "Socio";
            this.colSocio.MinimumWidth = 105;
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
            this.colDni.Visible = false;
            //
            // colPlan
            //
            this.colPlan.FillWeight = 20F;
            this.colPlan.HeaderText = "Plan";
            this.colPlan.MinimumWidth = 50;
            this.colPlan.Name = "colPlan";
            this.colPlan.ReadOnly = true;
            this.colPlan.Visible = false;
            //
            // colRutina
            //
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.colRutina.DefaultCellStyle = dataGridViewCellStyle2;
            this.colRutina.FillWeight = 52F;
            this.colRutina.HeaderText = "Rutina";
            this.colRutina.MinimumWidth = 145;
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
            this.colVencimiento.Visible = false;
            //
            // colEstadoRutina
            //
            this.colEstadoRutina.FillWeight = 25F;
            this.colEstadoRutina.HeaderText = "Estado";
            this.colEstadoRutina.MinimumWidth = 75;
            this.colEstadoRutina.Name = "colEstadoRutina";
            this.colEstadoRutina.ReadOnly = true;
            //
            // lblDetalleTitulo
            //
            this.lblDetalleTitulo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDetalleTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblDetalleTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblDetalleTitulo.Location = new System.Drawing.Point(12, 10);
            this.lblDetalleTitulo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDetalleTitulo.Name = "lblDetalleTitulo";
            this.lblDetalleTitulo.Size = new System.Drawing.Size(784, 35);
            this.lblDetalleTitulo.TabIndex = 0;
            this.lblDetalleTitulo.Text = "Socio seleccionado";
            //
            // lblSocio
            //
            this.lblSocio.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblSocio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblSocio.Location = new System.Drawing.Point(12, 52);
            this.lblSocio.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSocio.Name = "lblSocio";
            this.lblSocio.Size = new System.Drawing.Size(125, 30);
            this.lblSocio.TabIndex = 1;
            this.lblSocio.Text = "Socio:";
            this.lblSocio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // txtSocio
            //
            this.txtSocio.BackColor = System.Drawing.SystemColors.Window;
            this.txtSocio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSocio.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtSocio.Location = new System.Drawing.Point(145, 52);
            this.txtSocio.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSocio.Name = "txtSocio";
            this.txtSocio.ReadOnly = true;
            this.txtSocio.Size = new System.Drawing.Size(224, 29);
            this.txtSocio.TabIndex = 2;
            this.txtSocio.TabStop = false;
            this.txtSocio.Text = "Selecciona un socio";
            //
            // lblDni
            //
            this.lblDni.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblDni.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblDni.Location = new System.Drawing.Point(382, 52);
            this.lblDni.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDni.Name = "lblDni";
            this.lblDni.Size = new System.Drawing.Size(125, 30);
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
            this.txtDni.Location = new System.Drawing.Point(515, 52);
            this.txtDni.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtDni.Name = "txtDni";
            this.txtDni.ReadOnly = true;
            this.txtDni.Size = new System.Drawing.Size(262, 29);
            this.txtDni.TabIndex = 4;
            this.txtDni.TabStop = false;
            this.txtDni.Text = "-";
            //
            // lblPlan
            //
            this.lblPlan.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblPlan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblPlan.Location = new System.Drawing.Point(12, 92);
            this.lblPlan.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPlan.Name = "lblPlan";
            this.lblPlan.Size = new System.Drawing.Size(125, 30);
            this.lblPlan.TabIndex = 5;
            this.lblPlan.Text = "Plan:";
            this.lblPlan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // txtPlan
            //
            this.txtPlan.BackColor = System.Drawing.SystemColors.Window;
            this.txtPlan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPlan.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtPlan.Location = new System.Drawing.Point(145, 92);
            this.txtPlan.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPlan.Name = "txtPlan";
            this.txtPlan.ReadOnly = true;
            this.txtPlan.Size = new System.Drawing.Size(224, 29);
            this.txtPlan.TabIndex = 6;
            this.txtPlan.TabStop = false;
            this.txtPlan.Text = "-";
            //
            // lblEntrenador
            //
            this.lblEntrenador.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblEntrenador.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblEntrenador.Location = new System.Drawing.Point(382, 92);
            this.lblEntrenador.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEntrenador.Name = "lblEntrenador";
            this.lblEntrenador.Size = new System.Drawing.Size(125, 30);
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
            this.txtEntrenador.Location = new System.Drawing.Point(515, 92);
            this.txtEntrenador.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtEntrenador.Name = "txtEntrenador";
            this.txtEntrenador.ReadOnly = true;
            this.txtEntrenador.Size = new System.Drawing.Size(262, 29);
            this.txtEntrenador.TabIndex = 8;
            this.txtEntrenador.TabStop = false;
            this.txtEntrenador.Text = "-";
            //
            // lblVencimiento
            //
            this.lblVencimiento.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblVencimiento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblVencimiento.Location = new System.Drawing.Point(12, 132);
            this.lblVencimiento.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVencimiento.Name = "lblVencimiento";
            this.lblVencimiento.Size = new System.Drawing.Size(125, 30);
            this.lblVencimiento.TabIndex = 9;
            this.lblVencimiento.Text = "Cuota hasta:";
            this.lblVencimiento.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // txtVencimiento
            //
            this.txtVencimiento.BackColor = System.Drawing.SystemColors.Window;
            this.txtVencimiento.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVencimiento.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtVencimiento.Location = new System.Drawing.Point(145, 132);
            this.txtVencimiento.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtVencimiento.Name = "txtVencimiento";
            this.txtVencimiento.ReadOnly = true;
            this.txtVencimiento.Size = new System.Drawing.Size(224, 29);
            this.txtVencimiento.TabIndex = 10;
            this.txtVencimiento.TabStop = false;
            this.txtVencimiento.Text = "-";
            //
            // lblRutina
            //
            this.lblRutina.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblRutina.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblRutina.Location = new System.Drawing.Point(382, 132);
            this.lblRutina.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRutina.Name = "lblRutina";
            this.lblRutina.Size = new System.Drawing.Size(125, 30);
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
            this.txtRutina.Location = new System.Drawing.Point(515, 132);
            this.txtRutina.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtRutina.Name = "txtRutina";
            this.txtRutina.ReadOnly = true;
            this.txtRutina.Size = new System.Drawing.Size(262, 29);
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
            this.lblRutinaTitulo.Location = new System.Drawing.Point(12, 230);
            this.lblRutinaTitulo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRutinaTitulo.Name = "lblRutinaTitulo";
            this.lblRutinaTitulo.Size = new System.Drawing.Size(765, 35);
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
            this.tablaRutina.Location = new System.Drawing.Point(12, 272);
            this.tablaRutina.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tablaRutina.MultiSelect = false;
            this.tablaRutina.Name = "tablaRutina";
            this.tablaRutina.ReadOnly = true;
            this.tablaRutina.RowHeadersVisible = false;
            this.tablaRutina.RowHeadersWidth = 51;
            this.tablaRutina.RowTemplate.Height = 28;
            this.tablaRutina.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tablaRutina.Size = new System.Drawing.Size(765, 288);
            this.tablaRutina.TabIndex = 5;
            //
            // colDia
            //
            this.colDia.FillWeight = 16F;
            this.colDia.HeaderText = "Día";
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
            this.lblAccionInfo.Location = new System.Drawing.Point(9, 525);
            this.lblAccionInfo.Margin = new System.Windows.Forms.Padding(0, 5, 10, 0);
            this.lblAccionInfo.Name = "lblAccionInfo";
            this.lblAccionInfo.Size = new System.Drawing.Size(765, 40);
            this.lblAccionInfo.TabIndex = 14;
            this.lblAccionInfo.Text = "Selecciona un socio para ver su rutina y acciones.";
            this.lblAccionInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblSugerencia
            //
            this.lblSugerencia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.lblSugerencia.Location = new System.Drawing.Point(12, 174);
            this.lblSugerencia.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSugerencia.Name = "lblSugerencia";
            this.lblSugerencia.Size = new System.Drawing.Size(768, 52);
            this.lblSugerencia.TabIndex = 15;
            this.lblSugerencia.Text = "IMC no disponible. Sin sugerencia.";
            //
            // lblObjetivo
            //
            this.lblObjetivo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblObjetivo.Location = new System.Drawing.Point(12, 570);
            this.lblObjetivo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblObjetivo.Name = "lblObjetivo";
            this.lblObjetivo.Size = new System.Drawing.Size(152, 25);
            this.lblObjetivo.TabIndex = 16;
            this.lblObjetivo.Text = "Objetivo";
            //
            // objetivo
            //
            this.objetivo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.objetivo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.objetivo.Items.AddRange(new object[] {
            "Libre",
            "Fuerza",
            "Hipertrofia",
            "Acondicionamiento"});
            this.objetivo.Location = new System.Drawing.Point(12, 596);
            this.objetivo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.objetivo.Name = "objetivo";
            this.objetivo.Size = new System.Drawing.Size(212, 29);
            this.objetivo.TabIndex = 17;
            this.objetivo.SelectedIndexChanged += new System.EventHandler(this.filtroModelo_SelectedIndexChanged);
            //
            // lblNivel
            //
            this.lblNivel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNivel.Location = new System.Drawing.Point(238, 570);
            this.lblNivel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNivel.Name = "lblNivel";
            this.lblNivel.Size = new System.Drawing.Size(150, 25);
            this.lblNivel.TabIndex = 18;
            this.lblNivel.Text = "Nivel";
            //
            // nivel
            //
            this.nivel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nivel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.nivel.Items.AddRange(new object[] {
            "Principiante",
            "Intermedio",
            "Avanzado"});
            this.nivel.Location = new System.Drawing.Point(238, 596);
            this.nivel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.nivel.Name = "nivel";
            this.nivel.Size = new System.Drawing.Size(186, 29);
            this.nivel.TabIndex = 19;
            this.nivel.SelectedIndexChanged += new System.EventHandler(this.filtroModelo_SelectedIndexChanged);
            //
            // rutinaDisponible
            //
            this.rutinaDisponible.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rutinaDisponible.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.rutinaDisponible.Enabled = false;
            this.rutinaDisponible.Location = new System.Drawing.Point(12, 638);
            this.rutinaDisponible.Margin = new System.Windows.Forms.Padding(0, 5, 10, 0);
            this.rutinaDisponible.Name = "rutinaDisponible";
            this.rutinaDisponible.Size = new System.Drawing.Size(584, 29);
            this.rutinaDisponible.TabIndex = 6;
            //
            // asignarRutina
            //
            this.asignarRutina.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.asignarRutina.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(75)))), ((int)(((byte)(138)))));
            this.asignarRutina.Enabled = false;
            this.asignarRutina.FlatAppearance.BorderSize = 0;
            this.asignarRutina.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(57)))), ((int)(((byte)(105)))));
            this.asignarRutina.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(65)))), ((int)(((byte)(119)))));
            this.asignarRutina.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.asignarRutina.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.asignarRutina.ForeColor = System.Drawing.Color.White;
            this.asignarRutina.Location = new System.Drawing.Point(608, 638);
            this.asignarRutina.Margin = new System.Windows.Forms.Padding(0, 2, 10, 0);
            this.asignarRutina.Name = "asignarRutina";
            this.asignarRutina.Size = new System.Drawing.Size(170, 40);
            this.asignarRutina.TabIndex = 7;
            this.asignarRutina.Text = "Asignar rutina";
            this.asignarRutina.UseVisualStyleBackColor = false;
            this.asignarRutina.Click += new System.EventHandler(this.asignarRutina_Click);
            //
            // verRutina
            //
            this.verRutina.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.verRutina.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(75)))), ((int)(((byte)(138)))));
            this.verRutina.Enabled = false;
            this.verRutina.FlatAppearance.BorderSize = 0;
            this.verRutina.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(48)))), ((int)(((byte)(89)))));
            this.verRutina.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(61)))), ((int)(((byte)(112)))));
            this.verRutina.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.verRutina.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.verRutina.ForeColor = System.Drawing.Color.White;
            this.verRutina.Location = new System.Drawing.Point(394, 688);
            this.verRutina.Margin = new System.Windows.Forms.Padding(0, 2, 10, 0);
            this.verRutina.Name = "verRutina";
            this.verRutina.Size = new System.Drawing.Size(204, 40);
            this.verRutina.TabIndex = 9;
            this.verRutina.Text = "Ver / editar";
            this.verRutina.UseVisualStyleBackColor = false;
            this.verRutina.Click += new System.EventHandler(this.verRutina_Click);
            //
            // crearPersonalizada
            //
            this.crearPersonalizada.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.crearPersonalizada.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.crearPersonalizada.Enabled = false;
            this.crearPersonalizada.FlatAppearance.BorderSize = 0;
            this.crearPersonalizada.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(75)))), ((int)(((byte)(31)))));
            this.crearPersonalizada.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(101)))), ((int)(((byte)(41)))));
            this.crearPersonalizada.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.crearPersonalizada.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.crearPersonalizada.ForeColor = System.Drawing.Color.White;
            this.crearPersonalizada.Location = new System.Drawing.Point(144, 688);
            this.crearPersonalizada.Margin = new System.Windows.Forms.Padding(0, 2, 10, 0);
            this.crearPersonalizada.Name = "crearPersonalizada";
            this.crearPersonalizada.Size = new System.Drawing.Size(240, 40);
            this.crearPersonalizada.TabIndex = 8;
            this.crearPersonalizada.Text = "Crear personalizada";
            this.crearPersonalizada.UseVisualStyleBackColor = false;
            this.crearPersonalizada.Click += new System.EventHandler(this.crearPersonalizada_Click);
            //
            // exportarPdf
            //
            this.exportarPdf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.exportarPdf.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(75)))), ((int)(((byte)(138)))));
            this.exportarPdf.Enabled = false;
            this.exportarPdf.FlatAppearance.BorderSize = 0;
            this.exportarPdf.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(48)))), ((int)(((byte)(89)))));
            this.exportarPdf.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(61)))), ((int)(((byte)(112)))));
            this.exportarPdf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exportarPdf.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.exportarPdf.ForeColor = System.Drawing.Color.White;
            this.exportarPdf.Location = new System.Drawing.Point(608, 688);
            this.exportarPdf.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.exportarPdf.Name = "exportarPdf";
            this.exportarPdf.Size = new System.Drawing.Size(170, 40);
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
            this.ClientSize = new System.Drawing.Size(1375, 850);
            this.Controls.Add(this.splitContenido);
            this.Controls.Add(this.lblEstado);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(1140, 688);
            this.Name = "MisSociosFormulario";
            this.Padding = new System.Windows.Forms.Padding(20, 20, 20, 20);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SysGym";
            this.Load += new System.EventHandler(this.MisSociosFormulario_Load);
            ((System.ComponentModel.ISupportInitialize)(this.indicadorErrores)).EndInit();
            this.menuAlumnos.ResumeLayout(false);
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
