using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Administrador
{
    partial class ReportesFormulario
    {
        private IContainer components;
        private Panel barraAcciones;
        private Label lblEstado;
        private Panel panelContenido;
        private Button generar;
        private TableLayoutPanel tablaIndicadores;
        private Panel pnlSociosActivos;
        private Label lblSociosActivosTitulo;
        private Label lblSociosActivosValor;
        private Panel pnlUsuariosActivos;
        private Label lblUsuariosActivosTitulo;
        private Label lblUsuariosActivosValor;
        private Panel pnlMembresiasHabilitadas;
        private Label lblMembresiasTitulo;
        private Label lblMembresiasValor;
        private Panel pnlRutinasActivas;
        private Label lblRutinasActivasTitulo;
        private Label lblRutinasActivasValor;
        private Panel pnlEjerciciosDisponibles;
        private Label lblEjerciciosTitulo;
        private Label lblEjerciciosValor;

        protected override void Dispose(bool liberarRecursos)
        {
            if (liberarRecursos && components != null) components.Dispose();
            base.Dispose(liberarRecursos);
        }

        private void InitializeComponent()
        {
            components = new Container(); barraAcciones = new Panel(); generar = new Button(); lblEstado = new Label(); panelContenido = new Panel(); tablaIndicadores = new TableLayoutPanel();
            pnlSociosActivos = new Panel();
            lblSociosActivosTitulo = new Label();
            lblSociosActivosValor = new Label();
            pnlUsuariosActivos = new Panel();
            lblUsuariosActivosTitulo = new Label();
            lblUsuariosActivosValor = new Label();
            pnlMembresiasHabilitadas = new Panel();
            lblMembresiasTitulo = new Label();
            lblMembresiasValor = new Label();
            pnlRutinasActivas = new Panel();
            lblRutinasActivasTitulo = new Label();
            lblRutinasActivasValor = new Label();
            pnlEjerciciosDisponibles = new Panel();
            lblEjerciciosTitulo = new Label();
            lblEjerciciosValor = new Label();
            barraAcciones.SuspendLayout(); panelContenido.SuspendLayout(); tablaIndicadores.SuspendLayout(); SuspendLayout();
            barraAcciones.BackColor = Color.White;   barraAcciones.Padding = new Padding(16, 8, 16, 8);   generar.BackColor = System.Drawing.Color.FromArgb(231, 237, 247); generar.FlatStyle = FlatStyle.Flat; generar.FlatAppearance.BorderSize = 0; generar.ForeColor = System.Drawing.Color.FromArgb(48, 68, 95);  generar.Margin = new Padding(4, 0, 4, 0); generar.Padding = new Padding(12, 0, 12, 0); generar.Text = "Generar reporte"; generar.UseVisualStyleBackColor = false; barraAcciones.Controls.Add(generar);
             lblEstado.BackColor = Color.FromArgb(248, 250, 252);  lblEstado.ForeColor = Color.FromArgb(51, 65, 85);  lblEstado.Padding = new Padding(18, 8, 8, 0); lblEstado.Text = string.Empty;
            panelContenido.BackColor = Color.FromArgb(248, 250, 252); panelContenido.Padding = new Padding(16); panelContenido.Controls.Add(lblEstado); panelContenido.Controls.Add(tablaIndicadores);
             barraAcciones.Name = "barraAcciones";  barraAcciones.TabIndex = 1;
             generar.Name = "generar";  generar.TabIndex = 0;
             lblEstado.Name = "lblEstado";  lblEstado.TabIndex = 3;
             panelContenido.Name = "panelContenido";  panelContenido.TabIndex = 2;
            Controls.Add(panelContenido); Controls.Add(barraAcciones); AutoScaleMode = AutoScaleMode.Font;  BackColor = Color.FromArgb(241, 245, 249); ClientSize = new Size(1100, 680); Font = new Font("Segoe UI", 9.5F); MinimumSize = new Size(900, 560); this.Name = "ReportesFormulario"; StartPosition = FormStartPosition.CenterParent; Text = "SysGym";
            // Barra de acciones: los controles se reacomodan cuando el ancho disminuye.

            this.barraAcciones.Padding = new Padding(16, 8, 16, 8);

            this.generar.MinimumSize = new Size(120, 34);
            this.generar.Margin = new Padding(0, 0, 8, 0);
            this.generar.Padding = new Padding(12, 0, 12, 0);
            // Barra de estado inferior.

            this.lblEstado.Padding = new Padding(18, 0, 12, 0);
            this.lblEstado.TextAlign = ContentAlignment.MiddleLeft;
            // El contenido ocupa todo el espacio restante de la ventana.

            this.panelContenido.Padding = new Padding(16);

            this.AutoScroll = false;
            this.barraAcciones.AutoSize = false;
            this.barraAcciones.Dock = System.Windows.Forms.DockStyle.Top;
            this.barraAcciones.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.barraAcciones.Location = new System.Drawing.Point(0, 0);
            this.barraAcciones.Size = new System.Drawing.Size(1100, 52);
            this.barraAcciones.AutoScroll = false;
            this.lblEstado.AutoSize = false;
            this.lblEstado.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblEstado.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblEstado.Location = new System.Drawing.Point(0, 650);
            this.lblEstado.Size = new System.Drawing.Size(1100, 30);
            this.panelContenido.AutoSize = false;
            this.panelContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenido.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.panelContenido.Location = new System.Drawing.Point(0, 52);
            this.panelContenido.Size = new System.Drawing.Size(1100, 628);
            this.panelContenido.AutoScroll = false;
            this.generar.AutoSize = false;
            this.generar.Dock = System.Windows.Forms.DockStyle.None;
            this.generar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.generar.Location = new System.Drawing.Point(16, 8);
            this.generar.Size = new System.Drawing.Size(130, 34);
            tablaIndicadores.Name = "tablaIndicadores";
            tablaIndicadores.Dock = DockStyle.Top;
            tablaIndicadores.Height = 360;
            tablaIndicadores.ColumnCount = 6;
            tablaIndicadores.RowCount = 2;
            tablaIndicadores.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.66667F));
            tablaIndicadores.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.66667F));
            tablaIndicadores.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.66667F));
            tablaIndicadores.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.66667F));
            tablaIndicadores.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.66667F));
            tablaIndicadores.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.66667F));
            tablaIndicadores.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tablaIndicadores.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            pnlSociosActivos.Name = "pnlSociosActivos";
            pnlSociosActivos.Dock = DockStyle.Fill;
            pnlSociosActivos.BackColor = Color.White;
            pnlSociosActivos.BorderStyle = BorderStyle.FixedSingle;
            pnlSociosActivos.Padding = new Padding(16);
            pnlSociosActivos.Margin = new Padding(8);
            pnlSociosActivos.Controls.Add(lblSociosActivosValor);
            pnlSociosActivos.Controls.Add(lblSociosActivosTitulo);
            lblSociosActivosTitulo.Name = "lblSociosActivosTitulo";
            lblSociosActivosTitulo.Dock = DockStyle.Top;
            lblSociosActivosTitulo.Height = 44;
            lblSociosActivosTitulo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSociosActivosTitulo.ForeColor = Color.FromArgb(51, 65, 85);
            lblSociosActivosTitulo.Text = "Socios activos";
            lblSociosActivosTitulo.TextAlign = ContentAlignment.MiddleCenter;
            lblSociosActivosValor.Name = "lblSociosActivosValor";
            lblSociosActivosValor.Dock = DockStyle.Fill;
            lblSociosActivosValor.Font = new Font("Segoe UI", 32F, FontStyle.Bold);
            lblSociosActivosValor.ForeColor = Color.FromArgb(79, 70, 229);
            lblSociosActivosValor.Text = "-";
            lblSociosActivosValor.TextAlign = ContentAlignment.MiddleCenter;
            tablaIndicadores.Controls.Add(pnlSociosActivos, 0, 0);
            tablaIndicadores.SetColumnSpan(pnlSociosActivos, 2);
            pnlUsuariosActivos.Name = "pnlUsuariosActivos";
            pnlUsuariosActivos.Dock = DockStyle.Fill;
            pnlUsuariosActivos.BackColor = Color.White;
            pnlUsuariosActivos.BorderStyle = BorderStyle.FixedSingle;
            pnlUsuariosActivos.Padding = new Padding(16);
            pnlUsuariosActivos.Margin = new Padding(8);
            pnlUsuariosActivos.Controls.Add(lblUsuariosActivosValor);
            pnlUsuariosActivos.Controls.Add(lblUsuariosActivosTitulo);
            lblUsuariosActivosTitulo.Name = "lblUsuariosActivosTitulo";
            lblUsuariosActivosTitulo.Dock = DockStyle.Top;
            lblUsuariosActivosTitulo.Height = 44;
            lblUsuariosActivosTitulo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblUsuariosActivosTitulo.ForeColor = Color.FromArgb(51, 65, 85);
            lblUsuariosActivosTitulo.Text = "Usuarios activos";
            lblUsuariosActivosTitulo.TextAlign = ContentAlignment.MiddleCenter;
            lblUsuariosActivosValor.Name = "lblUsuariosActivosValor";
            lblUsuariosActivosValor.Dock = DockStyle.Fill;
            lblUsuariosActivosValor.Font = new Font("Segoe UI", 32F, FontStyle.Bold);
            lblUsuariosActivosValor.ForeColor = Color.FromArgb(79, 70, 229);
            lblUsuariosActivosValor.Text = "-";
            lblUsuariosActivosValor.TextAlign = ContentAlignment.MiddleCenter;
            tablaIndicadores.Controls.Add(pnlUsuariosActivos, 2, 0);
            tablaIndicadores.SetColumnSpan(pnlUsuariosActivos, 2);
            pnlMembresiasHabilitadas.Name = "pnlMembresiasHabilitadas";
            pnlMembresiasHabilitadas.Dock = DockStyle.Fill;
            pnlMembresiasHabilitadas.BackColor = Color.White;
            pnlMembresiasHabilitadas.BorderStyle = BorderStyle.FixedSingle;
            pnlMembresiasHabilitadas.Padding = new Padding(16);
            pnlMembresiasHabilitadas.Margin = new Padding(8);
            pnlMembresiasHabilitadas.Controls.Add(lblMembresiasValor);
            pnlMembresiasHabilitadas.Controls.Add(lblMembresiasTitulo);
            lblMembresiasTitulo.Name = "lblMembresiasTitulo";
            lblMembresiasTitulo.Dock = DockStyle.Top;
            lblMembresiasTitulo.Height = 44;
            lblMembresiasTitulo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblMembresiasTitulo.ForeColor = Color.FromArgb(51, 65, 85);
            lblMembresiasTitulo.Text = "Membresías habilitadas";
            lblMembresiasTitulo.TextAlign = ContentAlignment.MiddleCenter;
            lblMembresiasValor.Name = "lblMembresiasValor";
            lblMembresiasValor.Dock = DockStyle.Fill;
            lblMembresiasValor.Font = new Font("Segoe UI", 32F, FontStyle.Bold);
            lblMembresiasValor.ForeColor = Color.FromArgb(79, 70, 229);
            lblMembresiasValor.Text = "-";
            lblMembresiasValor.TextAlign = ContentAlignment.MiddleCenter;
            tablaIndicadores.Controls.Add(pnlMembresiasHabilitadas, 4, 0);
            tablaIndicadores.SetColumnSpan(pnlMembresiasHabilitadas, 2);
            pnlRutinasActivas.Name = "pnlRutinasActivas";
            pnlRutinasActivas.Dock = DockStyle.Fill;
            pnlRutinasActivas.BackColor = Color.White;
            pnlRutinasActivas.BorderStyle = BorderStyle.FixedSingle;
            pnlRutinasActivas.Padding = new Padding(16);
            pnlRutinasActivas.Margin = new Padding(8);
            pnlRutinasActivas.Controls.Add(lblRutinasActivasValor);
            pnlRutinasActivas.Controls.Add(lblRutinasActivasTitulo);
            lblRutinasActivasTitulo.Name = "lblRutinasActivasTitulo";
            lblRutinasActivasTitulo.Dock = DockStyle.Top;
            lblRutinasActivasTitulo.Height = 44;
            lblRutinasActivasTitulo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblRutinasActivasTitulo.ForeColor = Color.FromArgb(51, 65, 85);
            lblRutinasActivasTitulo.Text = "Rutinas activas";
            lblRutinasActivasTitulo.TextAlign = ContentAlignment.MiddleCenter;
            lblRutinasActivasValor.Name = "lblRutinasActivasValor";
            lblRutinasActivasValor.Dock = DockStyle.Fill;
            lblRutinasActivasValor.Font = new Font("Segoe UI", 32F, FontStyle.Bold);
            lblRutinasActivasValor.ForeColor = Color.FromArgb(79, 70, 229);
            lblRutinasActivasValor.Text = "-";
            lblRutinasActivasValor.TextAlign = ContentAlignment.MiddleCenter;
            tablaIndicadores.Controls.Add(pnlRutinasActivas, 1, 1);
            tablaIndicadores.SetColumnSpan(pnlRutinasActivas, 2);
            pnlEjerciciosDisponibles.Name = "pnlEjerciciosDisponibles";
            pnlEjerciciosDisponibles.Dock = DockStyle.Fill;
            pnlEjerciciosDisponibles.BackColor = Color.White;
            pnlEjerciciosDisponibles.BorderStyle = BorderStyle.FixedSingle;
            pnlEjerciciosDisponibles.Padding = new Padding(16);
            pnlEjerciciosDisponibles.Margin = new Padding(8);
            pnlEjerciciosDisponibles.Controls.Add(lblEjerciciosValor);
            pnlEjerciciosDisponibles.Controls.Add(lblEjerciciosTitulo);
            lblEjerciciosTitulo.Name = "lblEjerciciosTitulo";
            lblEjerciciosTitulo.Dock = DockStyle.Top;
            lblEjerciciosTitulo.Height = 44;
            lblEjerciciosTitulo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblEjerciciosTitulo.ForeColor = Color.FromArgb(51, 65, 85);
            lblEjerciciosTitulo.Text = "Ejercicios disponibles";
            lblEjerciciosTitulo.TextAlign = ContentAlignment.MiddleCenter;
            lblEjerciciosValor.Name = "lblEjerciciosValor";
            lblEjerciciosValor.Dock = DockStyle.Fill;
            lblEjerciciosValor.Font = new Font("Segoe UI", 32F, FontStyle.Bold);
            lblEjerciciosValor.ForeColor = Color.FromArgb(79, 70, 229);
            lblEjerciciosValor.Text = "-";
            lblEjerciciosValor.TextAlign = ContentAlignment.MiddleCenter;
            tablaIndicadores.Controls.Add(pnlEjerciciosDisponibles, 3, 1);
            tablaIndicadores.SetColumnSpan(pnlEjerciciosDisponibles, 2);

            tablaIndicadores.ResumeLayout(false);
            barraAcciones.ResumeLayout(false); barraAcciones.PerformLayout(); panelContenido.ResumeLayout(false); ResumeLayout(false);

            this.Load += new System.EventHandler(this.ReportesFormulario_Load);
            this.generar.Click += new System.EventHandler(this.generar_Click);
                }
    }
}
