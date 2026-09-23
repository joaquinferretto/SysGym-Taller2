namespace exxen2._0.capaVisual.Recepcionista
{
    partial class ConsultaEntrenadoresFormulario
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.SplitContainer splitConsulta;
        private System.Windows.Forms.Label lblEntrenadores;
        private System.Windows.Forms.Label lblBuscar;
        private System.Windows.Forms.TextBox buscar;
        private System.Windows.Forms.Button actualizar;
        private System.Windows.Forms.DataGridView tablaEntrenadores;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIdEntrenador;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEntrenador;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDniEntrenador;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCantidadSocios;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEstadoEntrenador;
        private System.Windows.Forms.Label lblSocios;
        private System.Windows.Forms.Label lblSeleccion;
        private System.Windows.Forms.Label lblResumen;
        private System.Windows.Forms.DataGridView tablaSocios;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSocio;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDniSocio;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPlan;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVencimiento;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRutina;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEstadoMembresia;
        private System.Windows.Forms.Label lblEstado;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.splitConsulta = new System.Windows.Forms.SplitContainer();
            this.lblEntrenadores = new System.Windows.Forms.Label();
            this.lblBuscar = new System.Windows.Forms.Label();
            this.buscar = new System.Windows.Forms.TextBox();
            this.actualizar = new System.Windows.Forms.Button();
            this.tablaEntrenadores = new System.Windows.Forms.DataGridView();
            this.colIdEntrenador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEntrenador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDniEntrenador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCantidadSocios = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstadoEntrenador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblSocios = new System.Windows.Forms.Label();
            this.lblSeleccion = new System.Windows.Forms.Label();
            this.lblResumen = new System.Windows.Forms.Label();
            this.tablaSocios = new System.Windows.Forms.DataGridView();
            this.colSocio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDniSocio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPlan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVencimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRutina = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstadoMembresia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblEstado = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitConsulta)).BeginInit();
            this.splitConsulta.Panel1.SuspendLayout();
            this.splitConsulta.Panel2.SuspendLayout();
            this.splitConsulta.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablaEntrenadores)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablaSocios)).BeginInit();
            this.SuspendLayout();
            //
            // splitConsulta
            //
            this.splitConsulta.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitConsulta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitConsulta.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitConsulta.Location = new System.Drawing.Point(16, 16);
            this.splitConsulta.Name = "splitConsulta";
            //
            // splitConsulta.Panel1
            //
            this.splitConsulta.Panel1.BackColor = System.Drawing.Color.White;
            this.splitConsulta.Panel1.Controls.Add(this.lblEntrenadores);
            this.splitConsulta.Panel1.Controls.Add(this.lblBuscar);
            this.splitConsulta.Panel1.Controls.Add(this.buscar);
            this.splitConsulta.Panel1.Controls.Add(this.actualizar);
            this.splitConsulta.Panel1.Controls.Add(this.tablaEntrenadores);
            this.splitConsulta.Panel1.Padding = new System.Windows.Forms.Padding(16);
            //
            // splitConsulta.Panel2
            //
            this.splitConsulta.Panel2.BackColor = System.Drawing.Color.White;
            this.splitConsulta.Panel2.Controls.Add(this.lblSocios);
            this.splitConsulta.Panel2.Controls.Add(this.lblSeleccion);
            this.splitConsulta.Panel2.Controls.Add(this.lblResumen);
            this.splitConsulta.Panel2.Controls.Add(this.tablaSocios);
            this.splitConsulta.Panel2.Padding = new System.Windows.Forms.Padding(16);
            this.splitConsulta.Size = new System.Drawing.Size(1068, 622);
            this.splitConsulta.SplitterDistance = 440;
            this.splitConsulta.SplitterWidth = 8;
            this.splitConsulta.TabIndex = 0;
            //
            // lblEntrenadores
            //
            this.lblEntrenadores.AutoSize = true;
            this.lblEntrenadores.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblEntrenadores.Location = new System.Drawing.Point(16, 14);
            this.lblEntrenadores.Name = "lblEntrenadores";
            this.lblEntrenadores.Size = new System.Drawing.Size(100, 20);
            this.lblEntrenadores.TabIndex = 0;
            this.lblEntrenadores.Text = "Entrenadores";
            //
            // lblBuscar
            //
            this.lblBuscar.AutoSize = true;
            this.lblBuscar.Location = new System.Drawing.Point(16, 54);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(49, 17);
            this.lblBuscar.TabIndex = 1;
            this.lblBuscar.Text = "Buscar:";
            //
            // buscar
            //
            this.buscar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buscar.Location = new System.Drawing.Point(68, 50);
            this.buscar.Name = "buscar";
            this.buscar.Size = new System.Drawing.Size(226, 24);
            this.buscar.TabIndex = 0;
            this.buscar.TextChanged += new System.EventHandler(this.buscar_TextChanged);
            //
            // actualizar
            //
            this.actualizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.actualizar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(237)))), ((int)(((byte)(247)))));
            this.actualizar.FlatAppearance.BorderSize = 0;
            this.actualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.actualizar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(68)))), ((int)(((byte)(95)))));
            this.actualizar.Location = new System.Drawing.Point(302, 47);
            this.actualizar.Name = "actualizar";
            this.actualizar.Size = new System.Drawing.Size(119, 32);
            this.actualizar.TabIndex = 1;
            this.actualizar.Text = "Actualizar listado";
            this.actualizar.UseVisualStyleBackColor = false;
            this.actualizar.Click += new System.EventHandler(this.actualizar_Click);
            //
            // tablaEntrenadores
            //
            this.tablaEntrenadores.AllowUserToAddRows = false;
            this.tablaEntrenadores.AllowUserToDeleteRows = false;
            this.tablaEntrenadores.AllowUserToResizeRows = false;
            this.tablaEntrenadores.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tablaEntrenadores.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tablaEntrenadores.BackgroundColor = System.Drawing.Color.White;
            this.tablaEntrenadores.ColumnHeadersHeight = 34;
            this.tablaEntrenadores.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colIdEntrenador,
            this.colEntrenador,
            this.colDniEntrenador,
            this.colCantidadSocios,
            this.colEstadoEntrenador});
            this.tablaEntrenadores.Location = new System.Drawing.Point(16, 92);
            this.tablaEntrenadores.MultiSelect = false;
            this.tablaEntrenadores.Name = "tablaEntrenadores";
            this.tablaEntrenadores.ReadOnly = true;
            this.tablaEntrenadores.RowHeadersVisible = false;
            this.tablaEntrenadores.RowTemplate.Height = 28;
            this.tablaEntrenadores.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tablaEntrenadores.Size = new System.Drawing.Size(405, 511);
            this.tablaEntrenadores.TabIndex = 2;
            this.tablaEntrenadores.SelectionChanged += new System.EventHandler(this.tablaEntrenadores_SelectionChanged);
            //
            // colIdEntrenador
            //
            this.colIdEntrenador.Name = "colIdEntrenador";
            this.colIdEntrenador.ReadOnly = true;
            this.colIdEntrenador.Visible = false;
            //
            // colEntrenador
            //
            this.colEntrenador.FillWeight = 40F;
            this.colEntrenador.HeaderText = "Entrenador";
            this.colEntrenador.Name = "colEntrenador";
            this.colEntrenador.ReadOnly = true;
            //
            // colDniEntrenador
            //
            this.colDniEntrenador.FillWeight = 22F;
            this.colDniEntrenador.HeaderText = "DNI";
            this.colDniEntrenador.Name = "colDniEntrenador";
            this.colDniEntrenador.ReadOnly = true;
            //
            // colCantidadSocios
            //
            this.colCantidadSocios.FillWeight = 16F;
            this.colCantidadSocios.HeaderText = "Socios";
            this.colCantidadSocios.Name = "colCantidadSocios";
            this.colCantidadSocios.ReadOnly = true;
            //
            // colEstadoEntrenador
            //
            this.colEstadoEntrenador.FillWeight = 22F;
            this.colEstadoEntrenador.HeaderText = "Estado";
            this.colEstadoEntrenador.Name = "colEstadoEntrenador";
            this.colEstadoEntrenador.ReadOnly = true;
            //
            // lblSocios
            //
            this.lblSocios.AutoSize = true;
            this.lblSocios.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblSocios.Location = new System.Drawing.Point(16, 14);
            this.lblSocios.Name = "lblSocios";
            this.lblSocios.Size = new System.Drawing.Size(124, 20);
            this.lblSocios.TabIndex = 0;
            this.lblSocios.Text = "Socios asignados";
            //
            // lblSeleccion
            //
            this.lblSeleccion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSeleccion.AutoEllipsis = true;
            this.lblSeleccion.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblSeleccion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.lblSeleccion.Location = new System.Drawing.Point(16, 46);
            this.lblSeleccion.Name = "lblSeleccion";
            this.lblSeleccion.Size = new System.Drawing.Size(586, 22);
            this.lblSeleccion.TabIndex = 1;
            this.lblSeleccion.Text = "Seleccione un entrenador";
            //
            // lblResumen
            //
            this.lblResumen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblResumen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblResumen.Location = new System.Drawing.Point(350, 18);
            this.lblResumen.Name = "lblResumen";
            this.lblResumen.Size = new System.Drawing.Size(252, 20);
            this.lblResumen.TabIndex = 2;
            this.lblResumen.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // tablaSocios
            //
            this.tablaSocios.AllowUserToAddRows = false;
            this.tablaSocios.AllowUserToDeleteRows = false;
            this.tablaSocios.AllowUserToResizeRows = false;
            this.tablaSocios.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tablaSocios.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tablaSocios.BackgroundColor = System.Drawing.Color.White;
            this.tablaSocios.ColumnHeadersHeight = 34;
            this.tablaSocios.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSocio,
            this.colDniSocio,
            this.colPlan,
            this.colVencimiento,
            this.colRutina,
            this.colEstadoMembresia});
            this.tablaSocios.Location = new System.Drawing.Point(16, 76);
            this.tablaSocios.MultiSelect = false;
            this.tablaSocios.Name = "tablaSocios";
            this.tablaSocios.ReadOnly = true;
            this.tablaSocios.RowHeadersVisible = false;
            this.tablaSocios.RowTemplate.Height = 28;
            this.tablaSocios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tablaSocios.Size = new System.Drawing.Size(586, 527);
            this.tablaSocios.TabIndex = 0;
            //
            // colSocio
            //
            this.colSocio.FillWeight = 30F;
            this.colSocio.HeaderText = "Socio";
            this.colSocio.Name = "colSocio";
            this.colSocio.ReadOnly = true;
            //
            // colDniSocio
            //
            this.colDniSocio.FillWeight = 18F;
            this.colDniSocio.HeaderText = "DNI";
            this.colDniSocio.Name = "colDniSocio";
            this.colDniSocio.ReadOnly = true;
            //
            // colPlan
            //
            this.colPlan.FillWeight = 18F;
            this.colPlan.HeaderText = "Plan";
            this.colPlan.Name = "colPlan";
            this.colPlan.ReadOnly = true;
            //
            // colVencimiento
            //
            this.colVencimiento.FillWeight = 18F;
            this.colVencimiento.HeaderText = "Cuota hasta";
            this.colVencimiento.Name = "colVencimiento";
            this.colVencimiento.ReadOnly = true;
            //
            // colRutina
            //
            this.colRutina.FillWeight = 24F;
            this.colRutina.HeaderText = "Rutina";
            this.colRutina.Name = "colRutina";
            this.colRutina.ReadOnly = true;
            //
            // colEstadoMembresia
            //
            this.colEstadoMembresia.FillWeight = 18F;
            this.colEstadoMembresia.HeaderText = "Membresía";
            this.colEstadoMembresia.Name = "colEstadoMembresia";
            this.colEstadoMembresia.ReadOnly = true;
            //
            // lblEstado
            //
            this.lblEstado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.lblEstado.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblEstado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblEstado.Location = new System.Drawing.Point(16, 638);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.lblEstado.Size = new System.Drawing.Size(1068, 26);
            this.lblEstado.TabIndex = 1;
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // ConsultaEntrenadoresFormulario
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1100, 680);
            this.Controls.Add(this.splitConsulta);
            this.Controls.Add(this.lblEstado);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.MinimumSize = new System.Drawing.Size(900, 560);
            this.Name = "ConsultaEntrenadoresFormulario";
            this.Padding = new System.Windows.Forms.Padding(16);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SysGym";
            this.Load += new System.EventHandler(this.ConsultaEntrenadoresFormulario_Load);
            this.splitConsulta.Panel1.ResumeLayout(false);
            this.splitConsulta.Panel1.PerformLayout();
            this.splitConsulta.Panel2.ResumeLayout(false);
            this.splitConsulta.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitConsulta)).EndInit();
            this.splitConsulta.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tablaEntrenadores)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablaSocios)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
