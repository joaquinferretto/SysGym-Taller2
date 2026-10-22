namespace exxen2._0.capaVisual.Compartido
{
    partial class FichaSocioFormulario
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (fotoSocio != null && fotoSocio.Image != null) fotoSocio.Image.Dispose();
                if (components != null) components.Dispose();
            }
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.disposicion = new System.Windows.Forms.TableLayoutPanel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.encabezado = new System.Windows.Forms.Panel();
            this.fotoSocio = new System.Windows.Forms.PictureBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.lblDatos = new System.Windows.Forms.Label();
            this.secciones = new System.Windows.Forms.TableLayoutPanel();
            this.lblMembresia = new System.Windows.Forms.Label();
            this.lblEntrenador = new System.Windows.Forms.Label();
            this.lblRutina = new System.Windows.Forms.Label();
            this.lblFinanzas = new System.Windows.Forms.Label();
            this.tabla = new System.Windows.Forms.DataGridView();
            this.colFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPeriodo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMetodo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colImporte = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRegistrador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuPagos = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.verMembresiaMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.verRutinaMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.exportarPagoMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.acciones = new System.Windows.Forms.Panel();
            this.btnHistorial = new System.Windows.Forms.Button();
            this.btnPago = new System.Windows.Forms.Button();
            this.btnExportarHistorial = new System.Windows.Forms.Button();
            this.lblEstado = new System.Windows.Forms.Label();
            this.dialogoPdf = new System.Windows.Forms.SaveFileDialog();
            this.disposicion.SuspendLayout();
            this.encabezado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fotoSocio)).BeginInit();
            this.secciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).BeginInit();
            this.menuPagos.SuspendLayout();
            this.acciones.SuspendLayout();
            this.SuspendLayout();
            //
            // disposicion
            //
            this.disposicion.ColumnCount = 1;
            this.disposicion.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.disposicion.Controls.Add(this.lblTitulo, 0, 0);
            this.disposicion.Controls.Add(this.encabezado, 0, 1);
            this.disposicion.Controls.Add(this.secciones, 0, 2);
            this.disposicion.Controls.Add(this.lblFinanzas, 0, 3);
            this.disposicion.Controls.Add(this.tabla, 0, 4);
            this.disposicion.Controls.Add(this.acciones, 0, 5);
            this.disposicion.Controls.Add(this.lblEstado, 0, 6);
            this.disposicion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.disposicion.Location = new System.Drawing.Point(0, 0);
            this.disposicion.MinimumSize = new System.Drawing.Size(850, 620);
            this.disposicion.Name = "disposicion";
            this.disposicion.Padding = new System.Windows.Forms.Padding(12);
            this.disposicion.RowCount = 7;
            this.disposicion.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.disposicion.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.disposicion.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.disposicion.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.disposicion.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.disposicion.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.disposicion.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.disposicion.Size = new System.Drawing.Size(1000, 700);
            this.disposicion.TabIndex = 0;
            //
            // lblTitulo
            //
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(15, 12);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(970, 32);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "FICHA DEL SOCIO";
            //
            // encabezado
            //
            this.encabezado.Controls.Add(this.fotoSocio);
            this.encabezado.Controls.Add(this.lblNombre);
            this.encabezado.Controls.Add(this.lblDatos);
            this.encabezado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.encabezado.Location = new System.Drawing.Point(15, 47);
            this.encabezado.Name = "encabezado";
            this.encabezado.Size = new System.Drawing.Size(970, 114);
            this.encabezado.TabIndex = 1;
            //
            // fotoSocio
            //
            this.fotoSocio.Location = new System.Drawing.Point(0, 4);
            this.fotoSocio.Name = "fotoSocio";
            this.fotoSocio.Size = new System.Drawing.Size(104, 104);
            this.fotoSocio.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.fotoSocio.TabIndex = 0;
            this.fotoSocio.TabStop = false;
            //
            // lblNombre
            //
            this.lblNombre.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblNombre.Location = new System.Drawing.Point(120, 2);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(820, 32);
            this.lblNombre.TabIndex = 1;
            this.lblNombre.Text = "Nombre y apellido";
            //
            // lblDatos
            //
            this.lblDatos.Location = new System.Drawing.Point(120, 36);
            this.lblDatos.Name = "lblDatos";
            this.lblDatos.Size = new System.Drawing.Size(820, 76);
            this.lblDatos.TabIndex = 2;
            this.lblDatos.Text = "DNI · Estado\r\nNacimiento · Alta · Antigüedad";
            //
            // secciones
            //
            this.secciones.ColumnCount = 3;
            this.secciones.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 44F));
            this.secciones.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28F));
            this.secciones.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28F));
            this.secciones.Controls.Add(this.lblMembresia, 0, 0);
            this.secciones.Controls.Add(this.lblEntrenador, 1, 0);
            this.secciones.Controls.Add(this.lblRutina, 2, 0);
            this.secciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.secciones.Location = new System.Drawing.Point(15, 167);
            this.secciones.Name = "secciones";
            this.secciones.RowCount = 1;
            this.secciones.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.secciones.Size = new System.Drawing.Size(970, 124);
            this.secciones.TabIndex = 2;
            //
            // lblMembresia
            //
            this.lblMembresia.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.lblMembresia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMembresia.Location = new System.Drawing.Point(3, 0);
            this.lblMembresia.Name = "lblMembresia";
            this.lblMembresia.Padding = new System.Windows.Forms.Padding(8);
            this.lblMembresia.Size = new System.Drawing.Size(420, 124);
            this.lblMembresia.TabIndex = 0;
            this.lblMembresia.Text = "MEMBRESÍA\r\nPlan · Estado\r\nInicio · Cuotas hasta\r\nDeuda · Límite";
            //
            // lblEntrenador
            //
            this.lblEntrenador.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.lblEntrenador.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEntrenador.Location = new System.Drawing.Point(429, 0);
            this.lblEntrenador.Name = "lblEntrenador";
            this.lblEntrenador.Padding = new System.Windows.Forms.Padding(8);
            this.lblEntrenador.Size = new System.Drawing.Size(265, 124);
            this.lblEntrenador.TabIndex = 1;
            this.lblEntrenador.Text = "ENTRENADOR\r\nSin asignar";
            //
            // lblRutina
            //
            this.lblRutina.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.lblRutina.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRutina.Location = new System.Drawing.Point(700, 0);
            this.lblRutina.Name = "lblRutina";
            this.lblRutina.Padding = new System.Windows.Forms.Padding(8);
            this.lblRutina.Size = new System.Drawing.Size(267, 124);
            this.lblRutina.TabIndex = 2;
            this.lblRutina.Text = "RUTINA\r\nSin rutina";
            //
            // lblFinanzas
            //
            this.lblFinanzas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFinanzas.Location = new System.Drawing.Point(15, 294);
            this.lblFinanzas.Name = "lblFinanzas";
            this.lblFinanzas.Padding = new System.Windows.Forms.Padding(4, 8, 4, 0);
            this.lblFinanzas.Size = new System.Drawing.Size(970, 100);
            this.lblFinanzas.TabIndex = 3;
            this.lblFinanzas.Text = "RESUMEN FINANCIERO\r\nTotal abonado · Pagos · Cuotas pagadas\r\nDeuda · Pendientes · " +
    "Vencidas\r\nÚltimo pago · Próximo vencimiento";
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
            this.colFecha,
            this.colPeriodo,
            this.colMetodo,
            this.colImporte,
            this.colRegistrador});
            this.tabla.ContextMenuStrip = this.menuPagos;
            this.tabla.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabla.Location = new System.Drawing.Point(15, 397);
            this.tabla.MultiSelect = false;
            this.tabla.Name = "tabla";
            this.tabla.ReadOnly = true;
            this.tabla.RowHeadersVisible = false;
            this.tabla.RowHeadersWidth = 51;
            this.tabla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tabla.Size = new System.Drawing.Size(970, 216);
            this.tabla.TabIndex = 4;
            this.tabla.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.tabla_CellMouseDown);
            this.tabla.SelectionChanged += new System.EventHandler(this.tabla_SelectionChanged);
            //
            // colFecha
            //
            this.colFecha.HeaderText = "Fecha";
            this.colFecha.MinimumWidth = 6;
            this.colFecha.Name = "colFecha";
            this.colFecha.ReadOnly = true;
            this.colFecha.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colPeriodo
            //
            this.colPeriodo.FillWeight = 145F;
            this.colPeriodo.HeaderText = "Período";
            this.colPeriodo.MinimumWidth = 6;
            this.colPeriodo.Name = "colPeriodo";
            this.colPeriodo.ReadOnly = true;
            this.colPeriodo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colMetodo
            //
            this.colMetodo.HeaderText = "Método";
            this.colMetodo.MinimumWidth = 6;
            this.colMetodo.Name = "colMetodo";
            this.colMetodo.ReadOnly = true;
            this.colMetodo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colImporte
            //
            this.colImporte.HeaderText = "Importe";
            this.colImporte.MinimumWidth = 6;
            this.colImporte.Name = "colImporte";
            this.colImporte.ReadOnly = true;
            this.colImporte.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // colRegistrador
            //
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.colRegistrador.DefaultCellStyle = dataGridViewCellStyle1;
            this.colRegistrador.FillWeight = 150F;
            this.colRegistrador.HeaderText = "Registrado por";
            this.colRegistrador.MinimumWidth = 6;
            this.colRegistrador.Name = "colRegistrador";
            this.colRegistrador.ReadOnly = true;
            this.colRegistrador.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            //
            // menuPagos
            //
            this.menuPagos.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuPagos.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.verMembresiaMenu,
            this.verRutinaMenu,
            this.exportarPagoMenu});
            this.menuPagos.Name = "menuPagos";
            this.menuPagos.Size = new System.Drawing.Size(265, 76);
            //
            // verMembresiaMenu
            //
            this.verMembresiaMenu.Name = "verMembresiaMenu";
            this.verMembresiaMenu.Size = new System.Drawing.Size(264, 24);
            this.verMembresiaMenu.Text = "Ver membresía";
            this.verMembresiaMenu.Click += new System.EventHandler(this.btnMembresia_Click);
            //
            // verRutinaMenu
            //
            this.verRutinaMenu.Name = "verRutinaMenu";
            this.verRutinaMenu.Size = new System.Drawing.Size(264, 24);
            this.verRutinaMenu.Text = "Ver rutina";
            this.verRutinaMenu.Click += new System.EventHandler(this.btnRutina_Click);
            //
            // exportarPagoMenu
            //
            this.exportarPagoMenu.Name = "exportarPagoMenu";
            this.exportarPagoMenu.Size = new System.Drawing.Size(264, 24);
            this.exportarPagoMenu.Text = "Exportar pago seleccionado";
            this.exportarPagoMenu.Click += new System.EventHandler(this.btnExportarPago_Click);
            //
            // acciones
            //
            this.acciones.Controls.Add(this.btnHistorial);
            this.acciones.Controls.Add(this.btnPago);
            this.acciones.Controls.Add(this.btnExportarHistorial);
            this.acciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.acciones.Location = new System.Drawing.Point(15, 619);
            this.acciones.Name = "acciones";
            this.acciones.Size = new System.Drawing.Size(970, 42);
            this.acciones.TabIndex = 5;
            //
            // btnHistorial
            //
            this.btnHistorial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(75)))), ((int)(((byte)(138)))));
            this.btnHistorial.Enabled = false;
            this.btnHistorial.FlatAppearance.BorderSize = 0;
            this.btnHistorial.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(48)))), ((int)(((byte)(89)))));
            this.btnHistorial.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(61)))), ((int)(((byte)(112)))));
            this.btnHistorial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHistorial.ForeColor = System.Drawing.Color.White;
            this.btnHistorial.Location = new System.Drawing.Point(0, 3);
            this.btnHistorial.Name = "btnHistorial";
            this.btnHistorial.Size = new System.Drawing.Size(180, 36);
            this.btnHistorial.TabIndex = 0;
            this.btnHistorial.Text = "Ver historial completo";
            this.btnHistorial.UseVisualStyleBackColor = false;
            this.btnHistorial.Click += new System.EventHandler(this.btnHistorial_Click);
            //
            // btnPago
            //
            this.btnPago.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.btnPago.Enabled = false;
            this.btnPago.FlatAppearance.BorderSize = 0;
            this.btnPago.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(75)))), ((int)(((byte)(31)))));
            this.btnPago.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(101)))), ((int)(((byte)(41)))));
            this.btnPago.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPago.ForeColor = System.Drawing.Color.White;
            this.btnPago.Location = new System.Drawing.Point(190, 3);
            this.btnPago.Name = "btnPago";
            this.btnPago.Size = new System.Drawing.Size(180, 36);
            this.btnPago.TabIndex = 1;
            this.btnPago.Text = "Registrar pago";
            this.btnPago.UseVisualStyleBackColor = false;
            this.btnPago.Click += new System.EventHandler(this.btnPago_Click);
            //
            // btnExportarHistorial
            //
            this.btnExportarHistorial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(75)))), ((int)(((byte)(138)))));
            this.btnExportarHistorial.FlatAppearance.BorderSize = 0;
            this.btnExportarHistorial.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(48)))), ((int)(((byte)(89)))));
            this.btnExportarHistorial.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(61)))), ((int)(((byte)(112)))));
            this.btnExportarHistorial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportarHistorial.ForeColor = System.Drawing.Color.White;
            this.btnExportarHistorial.Location = new System.Drawing.Point(380, 3);
            this.btnExportarHistorial.Name = "btnExportarHistorial";
            this.btnExportarHistorial.Size = new System.Drawing.Size(180, 36);
            this.btnExportarHistorial.TabIndex = 2;
            this.btnExportarHistorial.Text = "Exportar historial PDF";
            this.btnExportarHistorial.UseVisualStyleBackColor = false;
            this.btnExportarHistorial.Click += new System.EventHandler(this.btnExportarTodos_Click);
            //
            // lblEstado
            //
            this.lblEstado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEstado.Location = new System.Drawing.Point(15, 664);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(970, 24);
            this.lblEstado.TabIndex = 6;
            this.lblEstado.Text = "Seleccioná un socio para consultar su ficha.";
            //
            // dialogoPdf
            //
            this.dialogoPdf.DefaultExt = "pdf";
            this.dialogoPdf.Filter = "Documento PDF (*.pdf)|*.pdf";
            this.dialogoPdf.Title = "Guardar reporte de pagos";
            //
            // FichaSocioFormulario
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.disposicion);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.MinimumSize = new System.Drawing.Size(900, 660);
            this.Name = "FichaSocioFormulario";
            this.Text = "SysGym";
            this.Load += new System.EventHandler(this.FichaSocioFormulario_Load);
            this.disposicion.ResumeLayout(false);
            this.encabezado.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fotoSocio)).EndInit();
            this.secciones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).EndInit();
            this.menuPagos.ResumeLayout(false);
            this.acciones.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        private System.Windows.Forms.TableLayoutPanel disposicion;
        private System.Windows.Forms.Panel encabezado;
        private System.Windows.Forms.TableLayoutPanel secciones;
        private System.Windows.Forms.Panel acciones;
        private System.Windows.Forms.PictureBox fotoSocio;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label lblDatos;
        private System.Windows.Forms.Label lblMembresia;
        private System.Windows.Forms.Label lblEntrenador;
        private System.Windows.Forms.Label lblRutina;
        private System.Windows.Forms.Label lblFinanzas;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.DataGridView tabla;
        private System.Windows.Forms.Button btnHistorial;
        private System.Windows.Forms.Button btnPago;
        private System.Windows.Forms.Button btnExportarHistorial;
        private System.Windows.Forms.ContextMenuStrip menuPagos;
        private System.Windows.Forms.ToolStripMenuItem verMembresiaMenu;
        private System.Windows.Forms.ToolStripMenuItem verRutinaMenu;
        private System.Windows.Forms.ToolStripMenuItem exportarPagoMenu;
        private System.Windows.Forms.SaveFileDialog dialogoPdf;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPeriodo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMetodo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colImporte;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRegistrador;
    }
}
