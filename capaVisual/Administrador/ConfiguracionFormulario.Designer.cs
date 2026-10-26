namespace exxen2._0.capaVisual.Administrador
{
    partial class ConfiguracionFormulario
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label titulo;
        private System.Windows.Forms.Label ayudaGeneral;
        private System.Windows.Forms.GroupBox grupoCuotas;
        private System.Windows.Forms.TableLayoutPanel campos;
        private System.Windows.Forms.Panel panelVencidas;
        private System.Windows.Forms.Panel panelAnticipacion;
        private System.Windows.Forms.Panel panelAviso;
        private System.Windows.Forms.Label lblVencidas;
        private System.Windows.Forms.Label lblAnticipacion;
        private System.Windows.Forms.Label lblAviso;
        private System.Windows.Forms.Label descripcionVencidas;
        private System.Windows.Forms.Label descripcionAnticipacion;
        private System.Windows.Forms.Label descripcionAviso;
        private System.Windows.Forms.Label unidadVencidas;
        private System.Windows.Forms.Label unidadAnticipacion;
        private System.Windows.Forms.Label unidadAviso;
        private System.Windows.Forms.NumericUpDown maxVencidas;
        private System.Windows.Forms.NumericUpDown maxAnticipacion;
        private System.Windows.Forms.NumericUpDown diasAviso;
        private System.Windows.Forms.Label criterio;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.GroupBox grupoCopias;
        private System.Windows.Forms.Label lblUltimaCopia;
        private System.Windows.Forms.Button btnCrearCopia;
        private System.Windows.Forms.Button btnRestaurarCopia;
        private System.Windows.Forms.Button descartar;
        private System.Windows.Forms.Button guardar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.titulo = new System.Windows.Forms.Label();
            this.ayudaGeneral = new System.Windows.Forms.Label();
            this.grupoCuotas = new System.Windows.Forms.GroupBox();
            this.campos = new System.Windows.Forms.TableLayoutPanel();
            this.panelVencidas = new System.Windows.Forms.Panel();
            this.panelAnticipacion = new System.Windows.Forms.Panel();
            this.panelAviso = new System.Windows.Forms.Panel();
            this.lblVencidas = new System.Windows.Forms.Label();
            this.lblAnticipacion = new System.Windows.Forms.Label();
            this.lblAviso = new System.Windows.Forms.Label();
            this.descripcionVencidas = new System.Windows.Forms.Label();
            this.descripcionAnticipacion = new System.Windows.Forms.Label();
            this.descripcionAviso = new System.Windows.Forms.Label();
            this.unidadVencidas = new System.Windows.Forms.Label();
            this.unidadAnticipacion = new System.Windows.Forms.Label();
            this.unidadAviso = new System.Windows.Forms.Label();
            this.maxVencidas = new System.Windows.Forms.NumericUpDown();
            this.maxAnticipacion = new System.Windows.Forms.NumericUpDown();
            this.diasAviso = new System.Windows.Forms.NumericUpDown();
            this.criterio = new System.Windows.Forms.Label();
            this.lblEstado = new System.Windows.Forms.Label();
            this.grupoCopias = new System.Windows.Forms.GroupBox();
            this.lblUltimaCopia = new System.Windows.Forms.Label();
            this.btnCrearCopia = new System.Windows.Forms.Button();
            this.btnRestaurarCopia = new System.Windows.Forms.Button();
            this.descartar = new System.Windows.Forms.Button();
            this.guardar = new System.Windows.Forms.Button();
            this.grupoCuotas.SuspendLayout();
            this.campos.SuspendLayout();
            this.panelVencidas.SuspendLayout();
            this.panelAnticipacion.SuspendLayout();
            this.panelAviso.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxVencidas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxAnticipacion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.diasAviso)).BeginInit();
            this.SuspendLayout();
            // titulo
            this.titulo.AutoSize = true;
            this.titulo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.titulo.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.titulo.Location = new System.Drawing.Point(24, 18);
            this.titulo.Name = "titulo";
            this.titulo.Size = new System.Drawing.Size(292, 32);
            this.titulo.TabIndex = 0;
            this.titulo.Text = "Configuración del sistema";
            // ayudaGeneral
            this.ayudaGeneral.AutoSize = true;
            this.ayudaGeneral.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.ayudaGeneral.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.ayudaGeneral.Location = new System.Drawing.Point(26, 59);
            this.ayudaGeneral.Name = "ayudaGeneral";
            this.ayudaGeneral.Size = new System.Drawing.Size(224, 17);
            this.ayudaGeneral.TabIndex = 1;
            this.ayudaGeneral.Text = "Estos parámetros se aplican a todo el sistema.";
            // grupoCuotas
            this.grupoCuotas.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.grupoCuotas.BackColor = System.Drawing.Color.White;
            this.grupoCuotas.Controls.Add(this.campos);
            this.grupoCuotas.Controls.Add(this.ayudaGeneral);
            this.grupoCuotas.Controls.Add(this.criterio);
            this.grupoCuotas.Controls.Add(this.lblEstado);
            this.grupoCuotas.Controls.Add(this.descartar);
            this.grupoCuotas.Controls.Add(this.guardar);
            this.grupoCuotas.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grupoCuotas.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.grupoCuotas.Location = new System.Drawing.Point(24, 92);
            this.grupoCuotas.Name = "grupoCuotas";
            this.grupoCuotas.Padding = new System.Windows.Forms.Padding(12);
            this.grupoCuotas.Size = new System.Drawing.Size(800, 480);
            this.grupoCuotas.TabIndex = 2;
            this.grupoCuotas.TabStop = false;
            this.grupoCuotas.Text = "Cuotas y pagos";
            // campos
            this.campos.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.campos.BackColor = System.Drawing.Color.White;
            this.campos.ColumnCount = 1;
            this.campos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.campos.Controls.Add(this.panelVencidas, 0, 0);
            this.campos.Controls.Add(this.panelAnticipacion, 0, 1);
            this.campos.Controls.Add(this.panelAviso, 0, 2);
            this.campos.Location = new System.Drawing.Point(20, 86);
            this.campos.Name = "campos";
            this.campos.RowCount = 3;
            this.campos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 86F));
            this.campos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 86F));
            this.campos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 86F));
            this.campos.Size = new System.Drawing.Size(760, 258);
            this.campos.TabIndex = 0;
            // panelVencidas
            this.panelVencidas.Controls.Add(this.lblVencidas);
            this.panelVencidas.Controls.Add(this.descripcionVencidas);
            this.panelVencidas.Controls.Add(this.maxVencidas);
            this.panelVencidas.Controls.Add(this.unidadVencidas);
            this.panelVencidas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelVencidas.Location = new System.Drawing.Point(3, 3);
            this.panelVencidas.Name = "panelVencidas";
            this.panelVencidas.Size = new System.Drawing.Size(344, 80);
            this.panelVencidas.TabIndex = 0;
            // panelAnticipacion
            this.panelAnticipacion.Controls.Add(this.lblAnticipacion);
            this.panelAnticipacion.Controls.Add(this.descripcionAnticipacion);
            this.panelAnticipacion.Controls.Add(this.maxAnticipacion);
            this.panelAnticipacion.Controls.Add(this.unidadAnticipacion);
            this.panelAnticipacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAnticipacion.Location = new System.Drawing.Point(3, 89);
            this.panelAnticipacion.Name = "panelAnticipacion";
            this.panelAnticipacion.Size = new System.Drawing.Size(344, 80);
            this.panelAnticipacion.TabIndex = 1;
            // panelAviso
            this.panelAviso.Controls.Add(this.lblAviso);
            this.panelAviso.Controls.Add(this.descripcionAviso);
            this.panelAviso.Controls.Add(this.diasAviso);
            this.panelAviso.Controls.Add(this.unidadAviso);
            this.panelAviso.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAviso.Location = new System.Drawing.Point(3, 175);
            this.panelAviso.Name = "panelAviso";
            this.panelAviso.Size = new System.Drawing.Size(344, 80);
            this.panelAviso.TabIndex = 2;
            // lblVencidas
            this.lblVencidas.AutoSize = true;
            this.lblVencidas.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblVencidas.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblVencidas.Location = new System.Drawing.Point(0, 5);
            this.lblVencidas.Name = "lblVencidas";
            this.lblVencidas.Size = new System.Drawing.Size(206, 19);
            this.lblVencidas.TabIndex = 0;
            this.lblVencidas.Text = "Cuotas vencidas";
            // lblAnticipacion
            this.lblAnticipacion.AutoSize = true;
            this.lblAnticipacion.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblAnticipacion.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblAnticipacion.Location = new System.Drawing.Point(0, 5);
            this.lblAnticipacion.Name = "lblAnticipacion";
            this.lblAnticipacion.Size = new System.Drawing.Size(307, 19);
            this.lblAnticipacion.TabIndex = 0;
            this.lblAnticipacion.Text = "Meses de anticipación";
            // lblAviso
            this.lblAviso.AutoSize = true;
            this.lblAviso.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblAviso.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.lblAviso.Location = new System.Drawing.Point(0, 5);
            this.lblAviso.Name = "lblAviso";
            this.lblAviso.Size = new System.Drawing.Size(221, 19);
            this.lblAviso.TabIndex = 0;
            this.lblAviso.Text = "D\u00edas de aviso";
            // descripcionVencidas
            this.descripcionVencidas.AutoSize = true;
            this.descripcionVencidas.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.descripcionVencidas.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.descripcionVencidas.Location = new System.Drawing.Point(0, 34);
            this.descripcionVencidas.Name = "descripcionVencidas";
            this.descripcionVencidas.Size = new System.Drawing.Size(286, 15);
            this.descripcionVencidas.TabIndex = 1;
            this.descripcionVencidas.Text = "Con 2, el socio queda inactivo al tener 2 vencidas.";
            // descripcionAnticipacion
            this.descripcionAnticipacion.AutoSize = true;
            this.descripcionAnticipacion.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.descripcionAnticipacion.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.descripcionAnticipacion.Location = new System.Drawing.Point(0, 34);
            this.descripcionAnticipacion.Name = "descripcionAnticipacion";
            this.descripcionAnticipacion.Size = new System.Drawing.Size(326, 15);
            this.descripcionAnticipacion.TabIndex = 1;
            this.descripcionAnticipacion.Text = "Permite generar el período actual y hasta 2 meses futuros.";
            // descripcionAviso
            this.descripcionAviso.AutoSize = true;
            this.descripcionAviso.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.descripcionAviso.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.descripcionAviso.Location = new System.Drawing.Point(0, 34);
            this.descripcionAviso.Name = "descripcionAviso";
            this.descripcionAviso.Size = new System.Drawing.Size(352, 15);
            this.descripcionAviso.TabIndex = 1;
            this.descripcionAviso.Text = "Aviso de vencimiento con 7 días de anticipación.";
            // maxVencidas
            this.maxVencidas.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.maxVencidas.Location = new System.Drawing.Point(317, 2);
            this.maxVencidas.Maximum = new decimal(new int[] { 120, 0, 0, 0 });
            this.maxVencidas.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.maxVencidas.Name = "maxVencidas";
            this.maxVencidas.Size = new System.Drawing.Size(80, 25);
            this.maxVencidas.TabIndex = 1;
            this.maxVencidas.Value = new decimal(new int[] { 2, 0, 0, 0 });
            this.maxVencidas.ValueChanged += new System.EventHandler(this.maxVencidas_ValueChanged);
            // maxAnticipacion
            this.maxAnticipacion.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.maxAnticipacion.Location = new System.Drawing.Point(317, 2);
            this.maxAnticipacion.Maximum = new decimal(new int[] { 120, 0, 0, 0 });
            this.maxAnticipacion.Name = "maxAnticipacion";
            this.maxAnticipacion.Size = new System.Drawing.Size(80, 25);
            this.maxAnticipacion.TabIndex = 2;
            this.maxAnticipacion.Value = new decimal(new int[] { 2, 0, 0, 0 });
            this.maxAnticipacion.ValueChanged += new System.EventHandler(this.maxAnticipacion_ValueChanged);
            // diasAviso
            this.diasAviso.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.diasAviso.Location = new System.Drawing.Point(317, 2);
            this.diasAviso.Maximum = new decimal(new int[] { 365, 0, 0, 0 });
            this.diasAviso.Name = "diasAviso";
            this.diasAviso.Size = new System.Drawing.Size(80, 25);
            this.diasAviso.TabIndex = 3;
            this.diasAviso.Value = new decimal(new int[] { 7, 0, 0, 0 });
            this.diasAviso.ValueChanged += new System.EventHandler(this.diasAviso_ValueChanged);
            // unidadVencidas
            this.unidadVencidas.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.unidadVencidas.AutoSize = true;
            this.unidadVencidas.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.unidadVencidas.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.unidadVencidas.Location = new System.Drawing.Point(403, 5);
            this.unidadVencidas.Name = "unidadVencidas";
            this.unidadVencidas.Size = new System.Drawing.Size(44, 17);
            this.unidadVencidas.TabIndex = 4;
            this.unidadVencidas.Text = "cuotas";
            // unidadAnticipacion
            this.unidadAnticipacion.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.unidadAnticipacion.AutoSize = true;
            this.unidadAnticipacion.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.unidadAnticipacion.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.unidadAnticipacion.Location = new System.Drawing.Point(403, 5);
            this.unidadAnticipacion.Name = "unidadAnticipacion";
            this.unidadAnticipacion.Size = new System.Drawing.Size(47, 17);
            this.unidadAnticipacion.TabIndex = 5;
            this.unidadAnticipacion.Text = "meses";
            // unidadAviso
            this.unidadAviso.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.unidadAviso.AutoSize = true;
            this.unidadAviso.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.unidadAviso.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.unidadAviso.Location = new System.Drawing.Point(403, 5);
            this.unidadAviso.Name = "unidadAviso";
            this.unidadAviso.Size = new System.Drawing.Size(28, 17);
            this.unidadAviso.TabIndex = 6;
            this.unidadAviso.Text = "días";
            // criterio
            this.criterio.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.criterio.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.criterio.Location = new System.Drawing.Point(20, 350);
            this.criterio.Name = "criterio";
            this.criterio.Size = new System.Drawing.Size(760, 28);
            this.criterio.TabIndex = 4;
            this.criterio.Text = "Pagar la deuda no reactiva la membresía; la reactivación es manual.";
            this.criterio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // lblEstado
            this.lblEstado.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblEstado.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lblEstado.Location = new System.Drawing.Point(20, 382);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(760, 36);
            this.lblEstado.TabIndex = 5;
            this.lblEstado.Text = "Los cambios se aplican a las próximas operaciones y consultas.";
            // descartar
            this.descartar.BackColor = System.Drawing.Color.FromArgb(229, 231, 235);
            this.descartar.Enabled = false;
            this.descartar.FlatAppearance.BorderSize = 0;
            this.descartar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(209, 213, 219);
            this.descartar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(243, 244, 246);
            this.descartar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.descartar.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.descartar.Location = new System.Drawing.Point(500, 432);
            this.descartar.Name = "descartar";
            this.descartar.Size = new System.Drawing.Size(120, 34);
            this.descartar.TabIndex = 4;
            this.descartar.Text = "Descartar";
            this.descartar.UseVisualStyleBackColor = false;
            this.descartar.Click += new System.EventHandler(this.descartar_Click);
            // guardar
            this.guardar.BackColor = System.Drawing.Color.FromArgb(46, 125, 50);
            this.guardar.Enabled = false;
            this.guardar.FlatAppearance.BorderSize = 0;
            this.guardar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(27, 75, 31);
            this.guardar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(37, 101, 41);
            this.guardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.guardar.ForeColor = System.Drawing.Color.White;
            this.guardar.Location = new System.Drawing.Point(640, 432);
            this.guardar.Name = "guardar";
            this.guardar.Size = new System.Drawing.Size(120, 34);
            this.guardar.TabIndex = 5;
            this.guardar.Text = "Guardar";
            this.guardar.UseVisualStyleBackColor = false;
            this.guardar.Click += new System.EventHandler(this.guardar_Click);
            // grupoCopias
            this.grupoCopias.BackColor = System.Drawing.Color.White;
            this.grupoCopias.Controls.Add(this.btnCrearCopia);
            this.grupoCopias.Controls.Add(this.btnRestaurarCopia);
            this.grupoCopias.Controls.Add(this.lblUltimaCopia);
            this.grupoCopias.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grupoCopias.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55);
            this.grupoCopias.Location = new System.Drawing.Point(24, 584);
            this.grupoCopias.Name = "grupoCopias";
            this.grupoCopias.Size = new System.Drawing.Size(800, 90);
            this.grupoCopias.TabStop = false;
            this.grupoCopias.Text = "Copias de seguridad";
            // btnCrearCopia
            this.btnCrearCopia.BackColor = System.Drawing.Color.FromArgb(46, 125, 50);
            this.btnCrearCopia.FlatAppearance.BorderSize = 0;
            this.btnCrearCopia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCrearCopia.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCrearCopia.ForeColor = System.Drawing.Color.White;
            this.btnCrearCopia.Location = new System.Drawing.Point(20, 25);
            this.btnCrearCopia.Name = "btnCrearCopia";
            this.btnCrearCopia.Size = new System.Drawing.Size(175, 30);
            this.btnCrearCopia.TabIndex = 6;
            this.btnCrearCopia.Text = "Crear copia de seguridad";
            this.btnCrearCopia.UseVisualStyleBackColor = false;
            this.btnCrearCopia.Click += new System.EventHandler(this.btnCrearCopia_Click);
            // btnRestaurarCopia
            this.btnRestaurarCopia.BackColor = System.Drawing.Color.FromArgb(185, 28, 28);
            this.btnRestaurarCopia.FlatAppearance.BorderSize = 0;
            this.btnRestaurarCopia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestaurarCopia.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnRestaurarCopia.ForeColor = System.Drawing.Color.White;
            this.btnRestaurarCopia.Location = new System.Drawing.Point(205, 25);
            this.btnRestaurarCopia.Name = "btnRestaurarCopia";
            this.btnRestaurarCopia.Size = new System.Drawing.Size(155, 30);
            this.btnRestaurarCopia.TabIndex = 7;
            this.btnRestaurarCopia.Text = "Restaurar copia";
            this.btnRestaurarCopia.UseVisualStyleBackColor = false;
            this.btnRestaurarCopia.Click += new System.EventHandler(this.btnRestaurarCopia_Click);
            // lblUltimaCopia
            this.lblUltimaCopia.AutoSize = false;
            this.lblUltimaCopia.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblUltimaCopia.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.lblUltimaCopia.Location = new System.Drawing.Point(20, 59);
            this.lblUltimaCopia.Name = "lblUltimaCopia";
            this.lblUltimaCopia.Size = new System.Drawing.Size(760, 23);
            this.lblUltimaCopia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // ConfiguracionFormulario
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.ClientSize = new System.Drawing.Size(900, 690);
            this.Controls.Add(this.titulo);
            this.Controls.Add(this.grupoCuotas);
            this.Controls.Add(this.grupoCopias);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.MinimumSize = new System.Drawing.Size(920, 730);
            this.Name = "ConfiguracionFormulario";
            this.Text = "SysGym";
            this.Load += new System.EventHandler(this.ConfiguracionFormulario_Load);
            this.grupoCuotas.ResumeLayout(false);
            this.grupoCopias.ResumeLayout(false);
            this.campos.ResumeLayout(false);
            this.panelVencidas.ResumeLayout(false);
            this.panelVencidas.PerformLayout();
            this.panelAnticipacion.ResumeLayout(false);
            this.panelAnticipacion.PerformLayout();
            this.panelAviso.ResumeLayout(false);
            this.panelAviso.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxVencidas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxAnticipacion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.diasAviso)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
