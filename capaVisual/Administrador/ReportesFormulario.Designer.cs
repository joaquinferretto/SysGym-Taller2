namespace exxen2._0.capaVisual.Administrador
{
    partial class ReportesFormulario
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.disposicion = new System.Windows.Forms.TableLayoutPanel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.indicadores = new System.Windows.Forms.FlowLayoutPanel();
            this.lblSociosActivosValor = new System.Windows.Forms.Label();
            this.lblUsuariosActivosValor = new System.Windows.Forms.Label();
            this.lblMembresiasValor = new System.Windows.Forms.Label();
            this.lblRutinasActivasValor = new System.Windows.Forms.Label();
            this.lblEjerciciosValor = new System.Windows.Forms.Label();
            this.filtros = new System.Windows.Forms.FlowLayoutPanel();
            this.lblTipo = new System.Windows.Forms.Label();
            this.tipoReporte = new System.Windows.Forms.ComboBox();
            this.lblBuscar = new System.Windows.Forms.Label();
            this.buscador = new System.Windows.Forms.TextBox();
            this.generar = new System.Windows.Forms.Button();
            this.panelFechas = new System.Windows.Forms.FlowLayoutPanel();
            this.lblDesde = new System.Windows.Forms.Label();
            this.desde = new System.Windows.Forms.DateTimePicker();
            this.lblHasta = new System.Windows.Forms.Label();
            this.hasta = new System.Windows.Forms.DateTimePicker();
            this.lblCriterio = new System.Windows.Forms.Label();
            this.tabla = new System.Windows.Forms.DataGridView();
            this.col1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblResumen = new System.Windows.Forms.Label();
            this.acciones = new System.Windows.Forms.Panel();
            this.btnExportar = new System.Windows.Forms.Button();
            this.btnFicha = new System.Windows.Forms.Button();
            this.lblEstado = new System.Windows.Forms.Label();
            this.dialogoPdf = new System.Windows.Forms.SaveFileDialog();
            this.disposicion.SuspendLayout();
            this.indicadores.SuspendLayout();
            this.filtros.SuspendLayout();
            this.panelFechas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).BeginInit();
            this.acciones.SuspendLayout();
            this.SuspendLayout();
            //
            // disposicion
            //
            this.disposicion.ColumnCount = 1;
            this.disposicion.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.disposicion.Controls.Add(this.lblTitulo, 0, 0);
            this.disposicion.Controls.Add(this.indicadores, 0, 1);
            this.disposicion.Controls.Add(this.filtros, 0, 2);
            this.disposicion.Controls.Add(this.panelFechas, 0, 3);
            this.disposicion.Controls.Add(this.tabla, 0, 4);
            this.disposicion.Controls.Add(this.lblResumen, 0, 5);
            this.disposicion.Controls.Add(this.acciones, 0, 6);
            this.disposicion.Controls.Add(this.lblEstado, 0, 7);
            this.disposicion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.disposicion.Location = new System.Drawing.Point(0, 0);
            this.disposicion.Name = "disposicion";
            this.disposicion.Padding = new System.Windows.Forms.Padding(12);
            this.disposicion.RowCount = 8;
            this.disposicion.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.disposicion.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.disposicion.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.disposicion.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.disposicion.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.disposicion.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.disposicion.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.disposicion.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.disposicion.Size = new System.Drawing.Size(1100, 680);
            this.disposicion.TabIndex = 0;
            //
            // lblTitulo
            //
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(15, 12);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(1070, 36);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "REPORTES OPERATIVOS";
            //
            // indicadores
            //
            this.indicadores.Controls.Add(this.lblSociosActivosValor);
            this.indicadores.Controls.Add(this.lblUsuariosActivosValor);
            this.indicadores.Controls.Add(this.lblMembresiasValor);
            this.indicadores.Controls.Add(this.lblRutinasActivasValor);
            this.indicadores.Controls.Add(this.lblEjerciciosValor);
            this.indicadores.Dock = System.Windows.Forms.DockStyle.Fill;
            this.indicadores.Location = new System.Drawing.Point(15, 51);
            this.indicadores.Name = "indicadores";
            this.indicadores.Size = new System.Drawing.Size(1070, 28);
            this.indicadores.TabIndex = 1;
            this.indicadores.WrapContents = false;
            //
            // lblSociosActivosValor
            //
            this.lblSociosActivosValor.AutoSize = true;
            this.lblSociosActivosValor.Location = new System.Drawing.Point(0, 6);
            this.lblSociosActivosValor.Margin = new System.Windows.Forms.Padding(0, 6, 18, 0);
            this.lblSociosActivosValor.Name = "lblSociosActivosValor";
            this.lblSociosActivosValor.Size = new System.Drawing.Size(120, 21);
            this.lblSociosActivosValor.TabIndex = 0;
            this.lblSociosActivosValor.Text = "Socios activos: -";
            //
            // lblUsuariosActivosValor
            //
            this.lblUsuariosActivosValor.AutoSize = true;
            this.lblUsuariosActivosValor.Location = new System.Drawing.Point(138, 6);
            this.lblUsuariosActivosValor.Margin = new System.Windows.Forms.Padding(0, 6, 18, 0);
            this.lblUsuariosActivosValor.Name = "lblUsuariosActivosValor";
            this.lblUsuariosActivosValor.Size = new System.Drawing.Size(136, 21);
            this.lblUsuariosActivosValor.TabIndex = 1;
            this.lblUsuariosActivosValor.Text = "Usuarios activos: -";
            //
            // lblMembresiasValor
            //
            this.lblMembresiasValor.AutoSize = true;
            this.lblMembresiasValor.Location = new System.Drawing.Point(292, 6);
            this.lblMembresiasValor.Margin = new System.Windows.Forms.Padding(0, 6, 18, 0);
            this.lblMembresiasValor.Name = "lblMembresiasValor";
            this.lblMembresiasValor.Size = new System.Drawing.Size(187, 21);
            this.lblMembresiasValor.TabIndex = 2;
            this.lblMembresiasValor.Text = "Membresías habilitadas: -";
            //
            // lblRutinasActivasValor
            //
            this.lblRutinasActivasValor.AutoSize = true;
            this.lblRutinasActivasValor.Location = new System.Drawing.Point(497, 6);
            this.lblRutinasActivasValor.Margin = new System.Windows.Forms.Padding(0, 6, 18, 0);
            this.lblRutinasActivasValor.Name = "lblRutinasActivasValor";
            this.lblRutinasActivasValor.Size = new System.Drawing.Size(126, 21);
            this.lblRutinasActivasValor.TabIndex = 3;
            this.lblRutinasActivasValor.Text = "Rutinas activas: -";
            //
            // lblEjerciciosValor
            //
            this.lblEjerciciosValor.AutoSize = true;
            this.lblEjerciciosValor.Location = new System.Drawing.Point(641, 6);
            this.lblEjerciciosValor.Margin = new System.Windows.Forms.Padding(0, 6, 18, 0);
            this.lblEjerciciosValor.Name = "lblEjerciciosValor";
            this.lblEjerciciosValor.Size = new System.Drawing.Size(170, 21);
            this.lblEjerciciosValor.TabIndex = 4;
            this.lblEjerciciosValor.Text = "Ejercicios disponibles: -";
            //
            // filtros
            //
            this.filtros.Margin = new System.Windows.Forms.Padding(0);
            this.filtros.Controls.Add(this.lblTipo);
            this.filtros.Controls.Add(this.tipoReporte);
            this.filtros.Controls.Add(this.lblBuscar);
            this.filtros.Controls.Add(this.buscador);
            this.filtros.Controls.Add(this.generar);
            this.filtros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filtros.Location = new System.Drawing.Point(15, 85);
            this.filtros.Name = "filtros";
            this.filtros.Size = new System.Drawing.Size(1070, 36);
            this.filtros.TabIndex = 2;
            this.filtros.WrapContents = false;
            //
            // lblTipo
            //
            this.lblTipo.Location = new System.Drawing.Point(3, 0);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.Size = new System.Drawing.Size(110, 28);
            this.lblTipo.TabIndex = 0;
            this.lblTipo.Text = "Tipo de reporte:";
            this.lblTipo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // tipoReporte
            //
            this.tipoReporte.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tipoReporte.Items.AddRange(new object[] {
            "Deuda pendiente",
            "Socios en límite de deuda",
            "Socios sin entrenador",
            "Socios sin rutina",
            "Próximos vencimientos",
            "Membresías inactivas"});
            this.tipoReporte.Location = new System.Drawing.Point(119, 3);
            this.tipoReporte.Name = "tipoReporte";
            this.tipoReporte.Size = new System.Drawing.Size(250, 29);
            this.tipoReporte.TabIndex = 1;
            this.tipoReporte.SelectedIndexChanged += new System.EventHandler(this.tipoReporte_SelectedIndexChanged);
            //
            // lblBuscar
            //
            this.lblBuscar.Location = new System.Drawing.Point(375, 0);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(76, 28);
            this.lblBuscar.TabIndex = 2;
            this.lblBuscar.Text = "Socio/DNI:";
            this.lblBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // buscador
            //
            this.buscador.Location = new System.Drawing.Point(457, 3);
            this.buscador.MaxLength = 100;
            this.buscador.Name = "buscador";
            this.buscador.Size = new System.Drawing.Size(210, 29);
            this.buscador.TabIndex = 3;
            this.buscador.TextChanged += new System.EventHandler(this.filtros_Cambiados);
            //
            // generar
            //
            this.generar.Location = new System.Drawing.Point(673, 3);
            this.generar.Name = "generar";
            this.generar.BackColor = System.Drawing.Color.FromArgb(91, 75, 138);
            this.generar.FlatAppearance.BorderSize = 0;
            this.generar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(58, 48, 89);
            this.generar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(74, 61, 112);
            this.generar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.generar.ForeColor = System.Drawing.Color.White;
            this.generar.UseVisualStyleBackColor = false;







            this.generar.Size = new System.Drawing.Size(130, 32);
            this.generar.TabIndex = 4;
            this.generar.Text = "Consultar";
            this.generar.Click += new System.EventHandler(this.generar_Click);
            //
            // panelFechas
            //
            this.panelFechas.Margin = new System.Windows.Forms.Padding(0);
            this.panelFechas.Controls.Add(this.lblDesde);
            this.panelFechas.Controls.Add(this.desde);
            this.panelFechas.Controls.Add(this.lblHasta);
            this.panelFechas.Controls.Add(this.hasta);
            this.panelFechas.Controls.Add(this.lblCriterio);
            this.panelFechas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFechas.Location = new System.Drawing.Point(15, 127);
            this.panelFechas.Name = "panelFechas";
            this.panelFechas.Size = new System.Drawing.Size(1070, 38);
            this.panelFechas.TabIndex = 3;
            this.panelFechas.WrapContents = false;
            //
            // lblDesde
            //
            this.lblDesde.Location = new System.Drawing.Point(3, 0);
            this.lblDesde.Name = "lblDesde";
            this.lblDesde.Size = new System.Drawing.Size(48, 28);
            this.lblDesde.TabIndex = 0;
            this.lblDesde.Text = "Desde:";
            this.lblDesde.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // desde
            //
            this.desde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.desde.Location = new System.Drawing.Point(57, 3);
            this.desde.Name = "desde";
            this.desde.ShowCheckBox = true;
            this.desde.Size = new System.Drawing.Size(140, 29);
            this.desde.TabIndex = 1;
            this.desde.ValueChanged += new System.EventHandler(this.filtros_Cambiados);
            //
            // lblHasta
            //
            this.lblHasta.Location = new System.Drawing.Point(203, 0);
            this.lblHasta.Name = "lblHasta";
            this.lblHasta.Size = new System.Drawing.Size(48, 28);
            this.lblHasta.TabIndex = 2;
            this.lblHasta.Text = "Hasta:";
            this.lblHasta.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // hasta
            //
            this.hasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.hasta.Location = new System.Drawing.Point(257, 3);
            this.hasta.Name = "hasta";
            this.hasta.ShowCheckBox = true;
            this.hasta.Size = new System.Drawing.Size(140, 29);
            this.hasta.TabIndex = 3;
            this.hasta.ValueChanged += new System.EventHandler(this.filtros_Cambiados);
            //
            // lblCriterio
            //
            this.lblCriterio.Location = new System.Drawing.Point(403, 0);
            this.lblCriterio.Name = "lblCriterio";
            this.lblCriterio.Size = new System.Drawing.Size(500, 34);
            this.lblCriterio.TabIndex = 4;
            this.lblCriterio.Text = "Las fechas acotan el plazo de aviso configurado.";
            this.lblCriterio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // tabla
            //
            this.tabla.AllowUserToAddRows = false;
            this.tabla.AllowUserToDeleteRows = false;
            this.tabla.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tabla.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.tabla.BackgroundColor = System.Drawing.Color.White;
            this.tabla.ColumnHeadersHeight = 29;
            this.tabla.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col1,
            this.col2,
            this.col3,
            this.col4,
            this.col5,
            this.col6,
            this.col7});
            this.tabla.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabla.Location = new System.Drawing.Point(15, 171);
            this.tabla.MultiSelect = false;
            this.tabla.Name = "tabla";
            this.tabla.ReadOnly = true;
            this.tabla.RowHeadersVisible = false;
            this.tabla.RowHeadersWidth = 51;
            this.tabla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tabla.Size = new System.Drawing.Size(1070, 392);
            this.tabla.TabIndex = 4;
            this.tabla.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tabla_CellDoubleClick);
            this.tabla.SelectionChanged += new System.EventHandler(this.tabla_SelectionChanged);
            //
            // col1
            //
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.col1.DefaultCellStyle = dataGridViewCellStyle1;
            this.col1.FillWeight = 145F;
            this.col1.HeaderText = "Socio";
            this.col1.MinimumWidth = 6;
            this.col1.Name = "col1";
            this.col1.ReadOnly = true;
            this.col1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // col2
            //
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.col2.DefaultCellStyle = dataGridViewCellStyle2;
            this.col2.HeaderText = "DNI";
            this.col2.MinimumWidth = 6;
            this.col2.Name = "col2";
            this.col2.ReadOnly = true;
            this.col2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // col3
            //
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.col3.DefaultCellStyle = dataGridViewCellStyle3;
            this.col3.HeaderText = "Plan";
            this.col3.MinimumWidth = 6;
            this.col3.Name = "col3";
            this.col3.ReadOnly = true;
            this.col3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // col4
            //
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.col4.DefaultCellStyle = dataGridViewCellStyle4;
            this.col4.HeaderText = "Cuotas vencidas";
            this.col4.MinimumWidth = 6;
            this.col4.Name = "col4";
            this.col4.ReadOnly = true;
            this.col4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // col5
            //
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.col5.DefaultCellStyle = dataGridViewCellStyle5;
            this.col5.HeaderText = "Deuda total";
            this.col5.MinimumWidth = 6;
            this.col5.Name = "col5";
            this.col5.ReadOnly = true;
            this.col5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // col6
            //
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.col6.DefaultCellStyle = dataGridViewCellStyle6;
            this.col6.HeaderText = "Estado";
            this.col6.MinimumWidth = 6;
            this.col6.Name = "col6";
            this.col6.ReadOnly = true;
            this.col6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // col7
            //
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.col7.DefaultCellStyle = dataGridViewCellStyle7;
            this.col7.HeaderText = "";
            this.col7.MinimumWidth = 6;
            this.col7.Name = "col7";
            this.col7.ReadOnly = true;
            this.col7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.col7.Visible = false;
            //
            // lblResumen
            //
            this.lblResumen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblResumen.Location = new System.Drawing.Point(15, 566);
            this.lblResumen.Name = "lblResumen";
            this.lblResumen.Size = new System.Drawing.Size(1070, 30);
            this.lblResumen.TabIndex = 5;
            this.lblResumen.Text = "Cantidad: -";
            this.lblResumen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // acciones
            //
            this.acciones.Controls.Add(this.btnExportar);
            this.acciones.Controls.Add(this.btnFicha);
            this.acciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.acciones.Location = new System.Drawing.Point(15, 599);
            this.acciones.Name = "acciones";
            this.acciones.Size = new System.Drawing.Size(1070, 36);
            this.acciones.TabIndex = 6;
            //
            // btnExportar
            //
            this.btnExportar.Enabled = false;
            this.btnExportar.Location = new System.Drawing.Point(3, 3);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.BackColor = System.Drawing.Color.FromArgb(91, 75, 138);
            this.btnExportar.FlatAppearance.BorderSize = 0;
            this.btnExportar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(58, 48, 89);
            this.btnExportar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(74, 61, 112);
            this.btnExportar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportar.ForeColor = System.Drawing.Color.White;
            this.btnExportar.UseVisualStyleBackColor = false;







            this.btnExportar.Size = new System.Drawing.Size(130, 32);
            this.btnExportar.TabIndex = 0;
            this.btnExportar.Text = "Exportar PDF";
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            //
            // btnFicha
            //
            this.btnFicha.Enabled = false;
            this.btnFicha.Location = new System.Drawing.Point(139, 3);
            this.btnFicha.Name = "btnFicha";
            this.btnFicha.BackColor = System.Drawing.Color.FromArgb(91, 75, 138);
            this.btnFicha.FlatAppearance.BorderSize = 0;
            this.btnFicha.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(58, 48, 89);
            this.btnFicha.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(74, 61, 112);
            this.btnFicha.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFicha.ForeColor = System.Drawing.Color.White;
            this.btnFicha.UseVisualStyleBackColor = false;







            this.btnFicha.Size = new System.Drawing.Size(130, 32);
            this.btnFicha.TabIndex = 1;
            this.btnFicha.Text = "Ver ficha";
            this.btnFicha.Click += new System.EventHandler(this.btnFicha_Click);
            //
            // lblEstado
            //
            this.lblEstado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEstado.Location = new System.Drawing.Point(15, 638);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(1070, 30);
            this.lblEstado.TabIndex = 7;
            this.lblEstado.Text = "Seleccioná un reporte para consultar.";
            //
            // dialogoPdf
            //
            this.dialogoPdf.DefaultExt = "pdf";
            this.dialogoPdf.Filter = "Documento PDF (*.pdf)|*.pdf";
            this.dialogoPdf.Title = "Exportar reporte operativo";
            //
            // ReportesFormulario
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 680);
            this.Controls.Add(this.disposicion);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.MinimumSize = new System.Drawing.Size(900, 560);
            this.Name = "ReportesFormulario";
            this.Text = "SysGym";
            this.Load += new System.EventHandler(this.ReportesFormulario_Load);
            this.disposicion.ResumeLayout(false);
            this.indicadores.ResumeLayout(false);
            this.indicadores.PerformLayout();
            this.filtros.ResumeLayout(false);
            this.filtros.PerformLayout();
            this.panelFechas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).EndInit();
            this.acciones.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        private System.Windows.Forms.TableLayoutPanel disposicion;
        private System.Windows.Forms.FlowLayoutPanel indicadores;
        private System.Windows.Forms.FlowLayoutPanel filtros;
        private System.Windows.Forms.FlowLayoutPanel panelFechas;
        private System.Windows.Forms.Panel acciones;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSociosActivosValor;
        private System.Windows.Forms.Label lblUsuariosActivosValor;
        private System.Windows.Forms.Label lblMembresiasValor;
        private System.Windows.Forms.Label lblRutinasActivasValor;
        private System.Windows.Forms.Label lblEjerciciosValor;
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.ComboBox tipoReporte;
        private System.Windows.Forms.Label lblBuscar;
        private System.Windows.Forms.TextBox buscador;
        private System.Windows.Forms.Button generar;
        private System.Windows.Forms.Label lblDesde;
        private System.Windows.Forms.DateTimePicker desde;
        private System.Windows.Forms.Label lblHasta;
        private System.Windows.Forms.DateTimePicker hasta;
        private System.Windows.Forms.Label lblCriterio;
        private System.Windows.Forms.DataGridView tabla;
        private System.Windows.Forms.Label lblResumen;
        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.Button btnFicha;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.SaveFileDialog dialogoPdf;
        private System.Windows.Forms.DataGridViewTextBoxColumn col1;
        private System.Windows.Forms.DataGridViewTextBoxColumn col2;
        private System.Windows.Forms.DataGridViewTextBoxColumn col3;
        private System.Windows.Forms.DataGridViewTextBoxColumn col4;
        private System.Windows.Forms.DataGridViewTextBoxColumn col5;
        private System.Windows.Forms.DataGridViewTextBoxColumn col6;
        private System.Windows.Forms.DataGridViewTextBoxColumn col7;
    }
}
