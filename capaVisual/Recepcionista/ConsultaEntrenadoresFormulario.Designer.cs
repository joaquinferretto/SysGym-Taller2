namespace exxen2._0.capaVisual.Recepcionista
{
    partial class ConsultaEntrenadoresFormulario
    {
        private System.ComponentModel.IContainer components;
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
            components = new System.ComponentModel.Container();
            splitConsulta = new System.Windows.Forms.SplitContainer();
            lblEntrenadores = new System.Windows.Forms.Label();
            lblBuscar = new System.Windows.Forms.Label();
            buscar = new System.Windows.Forms.TextBox();
            actualizar = new System.Windows.Forms.Button();
            actualizar.ForeColor = System.Drawing.Color.FromArgb(48, 68, 95);
            tablaEntrenadores = new System.Windows.Forms.DataGridView();
            colIdEntrenador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colEntrenador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colDniEntrenador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colCantidadSocios = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colEstadoEntrenador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            lblSocios = new System.Windows.Forms.Label();
            lblSeleccion = new System.Windows.Forms.Label();
            lblResumen = new System.Windows.Forms.Label();
            tablaSocios = new System.Windows.Forms.DataGridView();
            colSocio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colDniSocio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colPlan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colVencimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colRutina = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colEstadoMembresia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            lblEstado = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(splitConsulta)).BeginInit();
            splitConsulta.Panel1.SuspendLayout();
            splitConsulta.Panel2.SuspendLayout();
            splitConsulta.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(tablaEntrenadores)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(tablaSocios)).BeginInit();
            SuspendLayout();

            splitConsulta.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            splitConsulta.Dock = System.Windows.Forms.DockStyle.Fill;
            splitConsulta.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            splitConsulta.Location = new System.Drawing.Point(16, 16);
            splitConsulta.Name = "splitConsulta";
            splitConsulta.Size = new System.Drawing.Size(1068, 622);
            splitConsulta.SplitterDistance = 440;
            splitConsulta.SplitterWidth = 8;
            splitConsulta.TabIndex = 0;
            splitConsulta.Panel1.BackColor = System.Drawing.Color.White;
            splitConsulta.Panel1.Padding = new System.Windows.Forms.Padding(16);
            splitConsulta.Panel2.BackColor = System.Drawing.Color.White;
            splitConsulta.Panel2.Padding = new System.Windows.Forms.Padding(16);

            lblEntrenadores.AutoSize = true;
            lblEntrenadores.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            lblEntrenadores.Location = new System.Drawing.Point(16, 14);
            lblEntrenadores.Name = "lblEntrenadores";
            lblEntrenadores.Size = new System.Drawing.Size(102, 20);
            lblEntrenadores.Text = "Entrenadores";

            lblBuscar.AutoSize = true;
            lblBuscar.Location = new System.Drawing.Point(16, 54);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new System.Drawing.Size(45, 17);
            lblBuscar.Text = "Buscar:";

            buscar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            buscar.Location = new System.Drawing.Point(68, 50);
            buscar.Name = "buscar";
            buscar.Size = new System.Drawing.Size(226, 24);
            buscar.TabIndex = 0;
            buscar.TextChanged += new System.EventHandler(buscar_TextChanged);

            actualizar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            actualizar.BackColor = System.Drawing.Color.FromArgb(231, 237, 247);
            actualizar.FlatAppearance.BorderSize = 0;
            actualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            actualizar.Location = new System.Drawing.Point(302, 47);
            actualizar.Name = "actualizar";
            actualizar.Size = new System.Drawing.Size(119, 32);
            actualizar.TabIndex = 1;
            actualizar.Text = "Actualizar listado";
            actualizar.UseVisualStyleBackColor = false;
            actualizar.Click += new System.EventHandler(actualizar_Click);

            tablaEntrenadores.AllowUserToAddRows = false;
            tablaEntrenadores.AllowUserToDeleteRows = false;
            tablaEntrenadores.AllowUserToResizeRows = false;
            tablaEntrenadores.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            tablaEntrenadores.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            tablaEntrenadores.BackgroundColor = System.Drawing.Color.White;
            tablaEntrenadores.ColumnHeadersHeight = 34;
            tablaEntrenadores.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { colIdEntrenador, colEntrenador, colDniEntrenador, colCantidadSocios, colEstadoEntrenador });
            tablaEntrenadores.Location = new System.Drawing.Point(16, 92);
            tablaEntrenadores.MultiSelect = false;
            tablaEntrenadores.Name = "tablaEntrenadores";
            tablaEntrenadores.ReadOnly = true;
            tablaEntrenadores.RowHeadersVisible = false;
            tablaEntrenadores.RowTemplate.Height = 28;
            tablaEntrenadores.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            tablaEntrenadores.Size = new System.Drawing.Size(405, 511);
            tablaEntrenadores.TabIndex = 2;
            tablaEntrenadores.SelectionChanged += new System.EventHandler(tablaEntrenadores_SelectionChanged);
            colIdEntrenador.Name = "colIdEntrenador";
            colIdEntrenador.Visible = false;
            colEntrenador.HeaderText = "Entrenador";
            colEntrenador.Name = "colEntrenador";
            colEntrenador.FillWeight = 40F;
            colDniEntrenador.HeaderText = "DNI";
            colDniEntrenador.Name = "colDniEntrenador";
            colDniEntrenador.FillWeight = 22F;
            colCantidadSocios.HeaderText = "Socios";
            colCantidadSocios.Name = "colCantidadSocios";
            colCantidadSocios.FillWeight = 16F;
            colEstadoEntrenador.HeaderText = "Estado";
            colEstadoEntrenador.Name = "colEstadoEntrenador";
            colEstadoEntrenador.FillWeight = 22F;

            lblSocios.AutoSize = true;
            lblSocios.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            lblSocios.Location = new System.Drawing.Point(16, 14);
            lblSocios.Name = "lblSocios";
            lblSocios.Size = new System.Drawing.Size(128, 20);
            lblSocios.Text = "Socios asignados";

            lblSeleccion.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lblSeleccion.AutoEllipsis = true;
            lblSeleccion.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            lblSeleccion.ForeColor = System.Drawing.Color.FromArgb(79, 70, 229);
            lblSeleccion.Location = new System.Drawing.Point(16, 46);
            lblSeleccion.Name = "lblSeleccion";
            lblSeleccion.Size = new System.Drawing.Size(586, 22);
            lblSeleccion.Text = "Seleccione un entrenador";

            lblResumen.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            lblResumen.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            lblResumen.Location = new System.Drawing.Point(350, 18);
            lblResumen.Name = "lblResumen";
            lblResumen.Size = new System.Drawing.Size(252, 20);
            lblResumen.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            tablaSocios.AllowUserToAddRows = false;
            tablaSocios.AllowUserToDeleteRows = false;
            tablaSocios.AllowUserToResizeRows = false;
            tablaSocios.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            tablaSocios.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            tablaSocios.BackgroundColor = System.Drawing.Color.White;
            tablaSocios.ColumnHeadersHeight = 34;
            tablaSocios.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { colSocio, colDniSocio, colPlan, colVencimiento, colRutina, colEstadoMembresia });
            tablaSocios.Location = new System.Drawing.Point(16, 76);
            tablaSocios.MultiSelect = false;
            tablaSocios.Name = "tablaSocios";
            tablaSocios.ReadOnly = true;
            tablaSocios.RowHeadersVisible = false;
            tablaSocios.RowTemplate.Height = 28;
            tablaSocios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            tablaSocios.Size = new System.Drawing.Size(586, 527);
            tablaSocios.TabIndex = 0;
            colSocio.HeaderText = "Socio";
            colSocio.Name = "colSocio";
            colSocio.FillWeight = 30F;
            colDniSocio.HeaderText = "DNI";
            colDniSocio.Name = "colDniSocio";
            colDniSocio.FillWeight = 18F;
            colPlan.HeaderText = "Plan";
            colPlan.Name = "colPlan";
            colPlan.FillWeight = 18F;
            colVencimiento.HeaderText = "Vence";
            colVencimiento.Name = "colVencimiento";
            colVencimiento.FillWeight = 18F;
            colRutina.HeaderText = "Rutina";
            colRutina.Name = "colRutina";
            colRutina.FillWeight = 24F;
            colEstadoMembresia.HeaderText = "Membresía";
            colEstadoMembresia.Name = "colEstadoMembresia";
            colEstadoMembresia.FillWeight = 18F;

            lblEstado.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            lblEstado.Dock = System.Windows.Forms.DockStyle.Bottom;
            lblEstado.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            lblEstado.Location = new System.Drawing.Point(16, 638);
            lblEstado.Name = "lblEstado";
            lblEstado.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            lblEstado.Size = new System.Drawing.Size(1068, 26);
            lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            splitConsulta.Panel1.Controls.Add(lblEntrenadores);
            splitConsulta.Panel1.Controls.Add(lblBuscar);
            splitConsulta.Panel1.Controls.Add(buscar);
            splitConsulta.Panel1.Controls.Add(actualizar);
            splitConsulta.Panel1.Controls.Add(tablaEntrenadores);
            splitConsulta.Panel2.Controls.Add(lblSocios);
            splitConsulta.Panel2.Controls.Add(lblSeleccion);
            splitConsulta.Panel2.Controls.Add(lblResumen);
            splitConsulta.Panel2.Controls.Add(tablaSocios);
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            ClientSize = new System.Drawing.Size(1100, 680);
            Controls.Add(splitConsulta);
            Controls.Add(lblEstado);
            Font = new System.Drawing.Font("Segoe UI", 9.5F);
            MinimumSize = new System.Drawing.Size(900, 560);
            Name = "ConsultaEntrenadoresFormulario";
            Padding = new System.Windows.Forms.Padding(16);
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "SysGym";
            Load += new System.EventHandler(ConsultaEntrenadoresFormulario_Load);
            splitConsulta.Panel1.ResumeLayout(false);
            splitConsulta.Panel1.PerformLayout();
            splitConsulta.Panel2.ResumeLayout(false);
            splitConsulta.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(splitConsulta)).EndInit();
            splitConsulta.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(tablaEntrenadores)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(tablaSocios)).EndInit();
            ResumeLayout(false);
        }
    }
}
