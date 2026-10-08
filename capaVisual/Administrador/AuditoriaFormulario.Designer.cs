namespace exxen2._0.capaVisual.Administrador
{
    partial class AuditoriaFormulario
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TableLayoutPanel disposicion;
        private System.Windows.Forms.Panel filtros;
        private System.Windows.Forms.Label titulo;
        private System.Windows.Forms.Label lblDesde;
        private System.Windows.Forms.Label lblHasta;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Label lblOperacion;
        private System.Windows.Forms.Label lblBuscar;
        private System.Windows.Forms.DateTimePicker desde;
        private System.Windows.Forms.DateTimePicker hasta;
        private System.Windows.Forms.ComboBox usuario;
        private System.Windows.Forms.ComboBox operacion;
        private System.Windows.Forms.TextBox buscar;
        private System.Windows.Forms.Button aplicar;
        private System.Windows.Forms.DataGridView tabla;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFechaHora;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUsuario;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRol;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOperacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEntidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetalle;
        private System.Windows.Forms.Label lblEstado;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle estiloFecha = new System.Windows.Forms.DataGridViewCellStyle();
            this.disposicion = new System.Windows.Forms.TableLayoutPanel();
            this.filtros = new System.Windows.Forms.Panel();
            this.titulo = new System.Windows.Forms.Label();
            this.lblDesde = new System.Windows.Forms.Label();
            this.lblHasta = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.lblOperacion = new System.Windows.Forms.Label();
            this.lblBuscar = new System.Windows.Forms.Label();
            this.desde = new System.Windows.Forms.DateTimePicker();
            this.hasta = new System.Windows.Forms.DateTimePicker();
            this.usuario = new System.Windows.Forms.ComboBox();
            this.operacion = new System.Windows.Forms.ComboBox();
            this.buscar = new System.Windows.Forms.TextBox();
            this.aplicar = new System.Windows.Forms.Button();
            this.tabla = new System.Windows.Forms.DataGridView();
            this.colFechaHora = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUsuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOperacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEntidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetalle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblEstado = new System.Windows.Forms.Label();
            this.disposicion.SuspendLayout();
            this.filtros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).BeginInit();
            this.SuspendLayout();
            // disposicion
            this.disposicion.ColumnCount = 1;
            this.disposicion.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.disposicion.Controls.Add(this.filtros, 0, 0);
            this.disposicion.Controls.Add(this.tabla, 0, 1);
            this.disposicion.Controls.Add(this.lblEstado, 0, 2);
            this.disposicion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.disposicion.Location = new System.Drawing.Point(0, 0);
            this.disposicion.Name = "disposicion";
            this.disposicion.RowCount = 3;
            this.disposicion.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 172F));
            this.disposicion.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.disposicion.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.disposicion.Size = new System.Drawing.Size(1100, 700);
            this.disposicion.TabIndex = 0;
            // filtros
            this.filtros.Controls.Add(this.titulo);
            this.filtros.Controls.Add(this.lblDesde);
            this.filtros.Controls.Add(this.lblHasta);
            this.filtros.Controls.Add(this.lblUsuario);
            this.filtros.Controls.Add(this.lblOperacion);
            this.filtros.Controls.Add(this.lblBuscar);
            this.filtros.Controls.Add(this.desde);
            this.filtros.Controls.Add(this.hasta);
            this.filtros.Controls.Add(this.usuario);
            this.filtros.Controls.Add(this.operacion);
            this.filtros.Controls.Add(this.buscar);
            this.filtros.Controls.Add(this.aplicar);
            this.filtros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filtros.Location = new System.Drawing.Point(3, 3);
            this.filtros.Name = "filtros";
            this.filtros.Size = new System.Drawing.Size(1094, 166);
            this.filtros.TabIndex = 0;
            // titulo
            this.titulo.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);
            this.titulo.Location = new System.Drawing.Point(14, 6);
            this.titulo.Name = "titulo";
            this.titulo.Size = new System.Drawing.Size(480, 40);
            this.titulo.Text = "Auditoría de operaciones";
            // labels
            this.lblDesde.Location = new System.Drawing.Point(16, 54);
            this.lblDesde.Name = "lblDesde";
            this.lblDesde.Size = new System.Drawing.Size(54, 24);
            this.lblDesde.Text = "Desde:";
            this.lblHasta.Location = new System.Drawing.Point(252, 54);
            this.lblHasta.Name = "lblHasta";
            this.lblHasta.Size = new System.Drawing.Size(54, 24);
            this.lblHasta.Text = "Hasta:";
            this.lblUsuario.Location = new System.Drawing.Point(16, 92);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(64, 24);
            this.lblUsuario.Text = "Usuario:";
            this.lblOperacion.Location = new System.Drawing.Point(392, 92);
            this.lblOperacion.Name = "lblOperacion";
            this.lblOperacion.Size = new System.Drawing.Size(82, 24);
            this.lblOperacion.Text = "Operación:";
            this.lblBuscar.Location = new System.Drawing.Point(16, 130);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(64, 24);
            this.lblBuscar.Text = "Buscar:";
            // desde
            this.desde.Checked = false;
            this.desde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.desde.Location = new System.Drawing.Point(86, 50);
            this.desde.Name = "desde";
            this.desde.ShowCheckBox = true;
            this.desde.Size = new System.Drawing.Size(150, 25);
            this.desde.TabIndex = 0;
            // hasta
            this.hasta.Checked = false;
            this.hasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.hasta.Location = new System.Drawing.Point(316, 50);
            this.hasta.Name = "hasta";
            this.hasta.ShowCheckBox = true;
            this.hasta.Size = new System.Drawing.Size(150, 25);
            this.hasta.TabIndex = 1;
            // usuario
            this.usuario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.usuario.Location = new System.Drawing.Point(86, 88);
            this.usuario.Name = "usuario";
            this.usuario.Size = new System.Drawing.Size(290, 25);
            this.usuario.TabIndex = 2;
            // operacion
            this.operacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.operacion.Location = new System.Drawing.Point(484, 88);
            this.operacion.Name = "operacion";
            this.operacion.Size = new System.Drawing.Size(230, 25);
            this.operacion.TabIndex = 3;
            // buscar
            this.buscar.Location = new System.Drawing.Point(86, 126);
            this.buscar.Name = "buscar";
            this.buscar.Size = new System.Drawing.Size(458, 25);
            this.buscar.TabIndex = 4;
            // aplicar
            this.aplicar.Location = new System.Drawing.Point(562, 123);
            this.aplicar.Name = "aplicar";
            this.aplicar.Size = new System.Drawing.Size(152, 32);
            this.aplicar.TabIndex = 5;
            this.aplicar.Text = "Aplicar filtros";
            this.aplicar.UseVisualStyleBackColor = true;
            this.aplicar.Click += new System.EventHandler(this.aplicar_Click);
            // tabla
            this.tabla.AllowUserToAddRows = false;
            this.tabla.AllowUserToDeleteRows = false;
            this.tabla.AutoGenerateColumns = false;
            this.tabla.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tabla.BackgroundColor = System.Drawing.Color.White;
            this.tabla.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colFechaHora, this.colUsuario, this.colRol, this.colOperacion, this.colEntidad, this.colDetalle });
            this.tabla.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabla.Location = new System.Drawing.Point(3, 175);
            this.tabla.Name = "tabla";
            this.tabla.ReadOnly = true;
            this.tabla.RowHeadersVisible = false;
            this.tabla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tabla.Size = new System.Drawing.Size(1094, 474);
            this.tabla.TabIndex = 1;
            // columns
            this.colFechaHora.DataPropertyName = "FechaHora";
            estiloFecha.Format = "dd/MM/yyyy HH:mm:ss";
            this.colFechaHora.DefaultCellStyle = estiloFecha;
            this.colFechaHora.FillWeight = 120F;
            this.colFechaHora.HeaderText = "Fecha/Hora";
            this.colFechaHora.Name = "colFechaHora";
            this.colFechaHora.ReadOnly = true;
            this.colFechaHora.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colUsuario.DataPropertyName = "Usuario";
            this.colUsuario.FillWeight = 115F;
            this.colUsuario.HeaderText = "Usuario";
            this.colUsuario.Name = "colUsuario";
            this.colUsuario.ReadOnly = true;
            this.colUsuario.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colRol.DataPropertyName = "Rol";
            this.colRol.FillWeight = 85F;
            this.colRol.HeaderText = "Rol";
            this.colRol.Name = "colRol";
            this.colRol.ReadOnly = true;
            this.colRol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colOperacion.DataPropertyName = "Operacion";
            this.colOperacion.FillWeight = 110F;
            this.colOperacion.HeaderText = "Operación";
            this.colOperacion.Name = "colOperacion";
            this.colOperacion.ReadOnly = true;
            this.colOperacion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colEntidad.DataPropertyName = "Entidad";
            this.colEntidad.FillWeight = 90F;
            this.colEntidad.HeaderText = "Entidad";
            this.colEntidad.Name = "colEntidad";
            this.colEntidad.ReadOnly = true;
            this.colEntidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colDetalle.DataPropertyName = "Detalle";
            this.colDetalle.FillWeight = 260F;
            this.colDetalle.HeaderText = "Detalle";
            this.colDetalle.Name = "colDetalle";
            this.colDetalle.ReadOnly = true;
            this.colDetalle.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // lblEstado
            this.lblEstado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEstado.Location = new System.Drawing.Point(3, 652);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Padding = new System.Windows.Forms.Padding(12, 4, 0, 0);
            this.lblEstado.Size = new System.Drawing.Size(1094, 48);
            this.lblEstado.TabIndex = 2;
            this.lblEstado.Text = "Historial de solo lectura. Hasta 500 registros, más recientes primero.";
            // AuditoriaFormulario
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(780, 500);
            this.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.ClientSize = new System.Drawing.Size(1100, 700);
            this.Controls.Add(this.disposicion);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.Name = "AuditoriaFormulario";
            this.Text = "Auditoría";
            this.Load += new System.EventHandler(this.AuditoriaFormulario_Load);
            this.filtros.ResumeLayout(false);
            this.filtros.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).EndInit();
            this.disposicion.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}
