namespace exxen2._0.capaVisual.Administrador
{
    partial class ConfiguracionFormulario
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label titulo;
        private System.Windows.Forms.Label subtitulo;
        private System.Windows.Forms.TableLayoutPanel campos;
        private System.Windows.Forms.Label lblVencidas;
        private System.Windows.Forms.Label lblAnticipacion;
        private System.Windows.Forms.Label lblAviso;
        private System.Windows.Forms.Label descripcionVencidas;
        private System.Windows.Forms.Label descripcionAnticipacion;
        private System.Windows.Forms.Label descripcionAviso;
        private System.Windows.Forms.NumericUpDown maxVencidas;
        private System.Windows.Forms.NumericUpDown maxAnticipacion;
        private System.Windows.Forms.NumericUpDown diasAviso;
        private System.Windows.Forms.Label criterio;
        private System.Windows.Forms.Label ayudaGeneral;
        private System.Windows.Forms.Button guardar;
        private System.Windows.Forms.Label lblEstado;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.titulo = new System.Windows.Forms.Label();
            this.subtitulo = new System.Windows.Forms.Label();
            this.campos = new System.Windows.Forms.TableLayoutPanel();
            this.lblVencidas = new System.Windows.Forms.Label();
            this.lblAnticipacion = new System.Windows.Forms.Label();
            this.lblAviso = new System.Windows.Forms.Label();
            this.descripcionVencidas = new System.Windows.Forms.Label();
            this.descripcionAnticipacion = new System.Windows.Forms.Label();
            this.descripcionAviso = new System.Windows.Forms.Label();
            this.maxVencidas = new System.Windows.Forms.NumericUpDown();
            this.maxAnticipacion = new System.Windows.Forms.NumericUpDown();
            this.diasAviso = new System.Windows.Forms.NumericUpDown();
            this.criterio = new System.Windows.Forms.Label();
            this.ayudaGeneral = new System.Windows.Forms.Label();
            this.guardar = new System.Windows.Forms.Button();
            this.lblEstado = new System.Windows.Forms.Label();
            this.campos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxVencidas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxAnticipacion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.diasAviso)).BeginInit();
            this.SuspendLayout();
            // titulo
            this.titulo.AutoSize = true;
            this.titulo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.titulo.Location = new System.Drawing.Point(24, 22);
            this.titulo.Name = "titulo";
            this.titulo.Text = "Configuración del sistema";
            // subtitulo
            this.subtitulo.AutoSize = true;
            this.subtitulo.Location = new System.Drawing.Point(26, 80);
            this.subtitulo.Name = "subtitulo";
            this.subtitulo.Text = "CUOTAS Y PAGOS";
            // campos
            this.campos.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.campos.ColumnCount = 3;
            this.campos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.campos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 53F));
            this.campos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.campos.Controls.Add(this.lblVencidas, 0, 0);
            this.campos.Controls.Add(this.descripcionVencidas, 1, 0);
            this.campos.Controls.Add(this.maxVencidas, 2, 0);
            this.campos.Controls.Add(this.lblAnticipacion, 0, 1);
            this.campos.Controls.Add(this.descripcionAnticipacion, 1, 1);
            this.campos.Controls.Add(this.maxAnticipacion, 2, 1);
            this.campos.Controls.Add(this.lblAviso, 0, 2);
            this.campos.Controls.Add(this.descripcionAviso, 1, 2);
            this.campos.Controls.Add(this.diasAviso, 2, 2);
            this.campos.Location = new System.Drawing.Point(26, 145);
            this.campos.Name = "campos";
            this.campos.RowCount = 3;
            this.campos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.333F));
            this.campos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.333F));
            this.campos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.334F));
            this.campos.Size = new System.Drawing.Size(720, 288);
            // lblVencidas
            this.lblVencidas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVencidas.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblVencidas.Name = "lblVencidas";
            this.lblVencidas.Text = "Cuotas vencidas permitidas";
            this.lblVencidas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // lblAnticipacion
            this.lblAnticipacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAnticipacion.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblAnticipacion.Name = "lblAnticipacion";
            this.lblAnticipacion.Text = "Meses de anticipación para generar cuotas";
            this.lblAnticipacion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // lblAviso
            this.lblAviso.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAviso.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblAviso.Name = "lblAviso";
            this.lblAviso.Text = "Días de aviso de vencimiento";
            this.lblAviso.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // descripcionVencidas
            this.descripcionVencidas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.descripcionVencidas.Name = "descripcionVencidas";
            this.descripcionVencidas.Padding = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.descripcionVencidas.Text = "Cantidad máxima de cuotas vencidas que puede tener un socio antes de que su membresía pase a inactiva.";
            this.descripcionVencidas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // descripcionAnticipacion
            this.descripcionAnticipacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.descripcionAnticipacion.Name = "descripcionAnticipacion";
            this.descripcionAnticipacion.Padding = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.descripcionAnticipacion.Text = "Indica cuántos meses futuros se pueden generar por adelantado. Por ejemplo, con 1 se puede generar el período actual y hasta un mes futuro.";
            this.descripcionAnticipacion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // descripcionAviso
            this.descripcionAviso.Dock = System.Windows.Forms.DockStyle.Fill;
            this.descripcionAviso.Name = "descripcionAviso";
            this.descripcionAviso.Padding = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.descripcionAviso.Text = "Cantidad de días antes del vencimiento en los que una cuota aparece como próxima a vencer en avisos y reportes.";
            this.descripcionAviso.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // maxVencidas
            this.maxVencidas.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.maxVencidas.Maximum = new decimal(new int[] {120, 0, 0, 0});
            this.maxVencidas.Minimum = new decimal(new int[] {1, 0, 0, 0});
            this.maxVencidas.Name = "maxVencidas";
            this.maxVencidas.Size = new System.Drawing.Size(80, 30);
            this.maxVencidas.TabIndex = 0;
            this.maxVencidas.Value = new decimal(new int[] {2, 0, 0, 0});
            // maxAnticipacion
            this.maxAnticipacion.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.maxAnticipacion.Maximum = new decimal(new int[] {120, 0, 0, 0});
            this.maxAnticipacion.Name = "maxAnticipacion";
            this.maxAnticipacion.Size = new System.Drawing.Size(80, 30);
            this.maxAnticipacion.TabIndex = 1;
            this.maxAnticipacion.Value = new decimal(new int[] {1, 0, 0, 0});
            // diasAviso
            this.diasAviso.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.diasAviso.Maximum = new decimal(new int[] {365, 0, 0, 0});
            this.diasAviso.Name = "diasAviso";
            this.diasAviso.Size = new System.Drawing.Size(80, 30);
            this.diasAviso.TabIndex = 2;
            this.diasAviso.Value = new decimal(new int[] {7, 0, 0, 0});
            // criterio
            this.criterio.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.criterio.Location = new System.Drawing.Point(26, 445);
            this.criterio.Name = "criterio";
            this.criterio.Size = new System.Drawing.Size(720, 54);
            this.criterio.Text = "Al alcanzar el límite, la membresía puede quedar inactiva. Pagar la deuda no la reactiva automáticamente; la reactivación es manual.";
            // ayudaGeneral
            this.ayudaGeneral.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.ayudaGeneral.Location = new System.Drawing.Point(26, 105);
            this.ayudaGeneral.Name = "ayudaGeneral";
            this.ayudaGeneral.Size = new System.Drawing.Size(720, 30);
            this.ayudaGeneral.Text = "Estos valores son generales y se aplican al funcionamiento de todo el sistema.";
            this.ayudaGeneral.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // guardar
            this.guardar.Location = new System.Drawing.Point(26, 520);
            this.guardar.Name = "guardar";
            this.guardar.BackColor = System.Drawing.Color.FromArgb(46, 125, 50);
            this.guardar.FlatAppearance.BorderSize = 0;
            this.guardar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(27, 75, 31);
            this.guardar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(37, 101, 41);
            this.guardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.guardar.ForeColor = System.Drawing.Color.White;
            this.guardar.UseVisualStyleBackColor = false;







            this.guardar.Size = new System.Drawing.Size(180, 38);
            this.guardar.TabIndex = 3;
            this.guardar.Text = "Guardar";
            this.guardar.Click += new System.EventHandler(this.guardar_Click);
            // lblEstado
            this.lblEstado.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.lblEstado.Location = new System.Drawing.Point(26, 575);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(720, 55);
            this.lblEstado.Text = "Los cambios se aplican a las próximas operaciones y consultas.";
            // ConfiguracionFormulario
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.ClientSize = new System.Drawing.Size(780, 650);
            this.Controls.Add(this.titulo);
            this.Controls.Add(this.subtitulo);
            this.Controls.Add(this.campos);
            this.Controls.Add(this.ayudaGeneral);
            this.Controls.Add(this.criterio);
            this.Controls.Add(this.guardar);
            this.Controls.Add(this.lblEstado);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.MinimumSize = new System.Drawing.Size(720, 690);
            this.Name = "ConfiguracionFormulario";
            this.Text = "SysGym";
            this.Load += new System.EventHandler(this.ConfiguracionFormulario_Load);
            this.campos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.maxVencidas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxAnticipacion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.diasAviso)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
