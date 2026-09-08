using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Administrador
{
    partial class ReportesFormulario
    {
        private IContainer components;
        private Panel panelEncabezado;
        private Label lblTitulo;
        private Label lblDescripcion;
        private Button btnVolver;
        private Panel barraAcciones;
        private Label lblEstado;
        private Panel panelContenido;
        private Button generar;
        private Label resumen;

        protected override void Dispose(bool liberarRecursos)
        {
            if (liberarRecursos && components != null) components.Dispose();
            base.Dispose(liberarRecursos);
        }

        private void InitializeComponent()
        {
            components = new Container(); panelEncabezado = new Panel(); lblTitulo = new Label(); lblDescripcion = new Label(); btnVolver = new Button(); barraAcciones = new Panel(); generar = new Button(); lblEstado = new Label(); panelContenido = new Panel(); resumen = new Label();
            panelEncabezado.SuspendLayout(); barraAcciones.SuspendLayout(); panelContenido.SuspendLayout(); SuspendLayout();
            panelEncabezado.BackColor = Color.FromArgb(79, 70, 229);   panelEncabezado.Controls.Add(lblDescripcion); panelEncabezado.Controls.Add(lblTitulo); panelEncabezado.Controls.Add(btnVolver);  lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold); lblTitulo.ForeColor = Color.White;  lblTitulo.Text = "Reportes";  lblDescripcion.ForeColor = Color.FromArgb(226, 232, 240);  lblDescripcion.Text = "Indicadores generales del estado del gimnasio";  btnVolver.BackColor = Color.White; btnVolver.FlatStyle = FlatStyle.Flat; btnVolver.FlatAppearance.BorderSize = 0; btnVolver.ForeColor = Color.FromArgb(79, 70, 229);   btnVolver.Text = "Volver"; btnVolver.UseVisualStyleBackColor = false;
            barraAcciones.BackColor = Color.White;   barraAcciones.Padding = new Padding(16, 8, 16, 8);   generar.BackColor = Color.FromArgb(79, 70, 229); generar.FlatStyle = FlatStyle.Flat; generar.FlatAppearance.BorderSize = 0; generar.ForeColor = Color.White;  generar.Margin = new Padding(4, 0, 4, 0); generar.Padding = new Padding(12, 0, 12, 0); generar.Text = "Generar reporte"; generar.UseVisualStyleBackColor = false; barraAcciones.Controls.Add(generar);
             lblEstado.BackColor = Color.FromArgb(226, 232, 240);  lblEstado.ForeColor = Color.FromArgb(51, 65, 85);  lblEstado.Padding = new Padding(18, 8, 8, 0); lblEstado.Text = "Listo";
            panelContenido.BackColor = Color.FromArgb(248, 250, 252);  panelContenido.Padding = new Padding(20); panelContenido.Controls.Add(resumen); resumen.BackColor = Color.White; resumen.BorderStyle = BorderStyle.FixedSingle;  resumen.Font = new Font("Segoe UI", 16F, FontStyle.Bold); resumen.ForeColor = Color.FromArgb(30, 41, 59); resumen.Padding = new Padding(40); resumen.TextAlign = ContentAlignment.MiddleCenter; resumen.Text = "Reporte pendiente";
             panelEncabezado.Name = "panelEncabezado";  panelEncabezado.TabIndex = 0;
            lblTitulo.Name = "lblTitulo";  lblTitulo.TabIndex = 0;
            lblDescripcion.Name = "lblDescripcion";  lblDescripcion.TabIndex = 1;
            btnVolver.Name = "btnVolver"; btnVolver.TabIndex = 2;
             barraAcciones.Name = "barraAcciones";  barraAcciones.TabIndex = 1;
             generar.Name = "generar";  generar.TabIndex = 0;
             lblEstado.Name = "lblEstado";  lblEstado.TabIndex = 3;
             panelContenido.Name = "panelContenido";  panelContenido.TabIndex = 2;
             resumen.Name = "resumen";  resumen.TabIndex = 0;
            Controls.Add(panelContenido); Controls.Add(lblEstado); Controls.Add(barraAcciones); Controls.Add(panelEncabezado); AutoScaleMode = AutoScaleMode.Font; this.AutoScroll = true; BackColor = Color.FromArgb(241, 245, 249); ClientSize = new Size(1100, 680); Font = new Font("Segoe UI", 9.5F); MinimumSize = new Size(760, 540); this.Name = "ReportesFormulario"; StartPosition = FormStartPosition.CenterParent; Text = "SysGym | Reportes";

            this.panelEncabezado.AutoSize = false;
            this.panelEncabezado.Dock = System.Windows.Forms.DockStyle.None;
            this.panelEncabezado.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.panelEncabezado.Location = new System.Drawing.Point(0, 0);
            this.panelEncabezado.Size = new System.Drawing.Size(1100, 80);
            this.lblTitulo.AutoSize = false;
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.None;
            this.lblTitulo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblTitulo.Location = new System.Drawing.Point(22, 10);
            this.lblTitulo.Size = new System.Drawing.Size(112, 38);
            this.lblDescripcion.AutoSize = false;
            this.lblDescripcion.Dock = System.Windows.Forms.DockStyle.None;
            this.lblDescripcion.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblDescripcion.Location = new System.Drawing.Point(24, 47);
            this.lblDescripcion.Size = new System.Drawing.Size(271, 22);
            this.btnVolver.AutoSize = false;
            this.btnVolver.Dock = System.Windows.Forms.DockStyle.None;
            this.btnVolver.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.btnVolver.Location = new System.Drawing.Point(930, 22);
            this.btnVolver.Size = new System.Drawing.Size(92, 34);
            this.barraAcciones.AutoSize = false;
            this.barraAcciones.Dock = System.Windows.Forms.DockStyle.None;
            this.barraAcciones.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.barraAcciones.Location = new System.Drawing.Point(0, 80);
            this.barraAcciones.Size = new System.Drawing.Size(1100, 52);
            this.lblEstado.AutoSize = false;
            this.lblEstado.Dock = System.Windows.Forms.DockStyle.None;
            this.lblEstado.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblEstado.Location = new System.Drawing.Point(0, 648);
            this.lblEstado.Size = new System.Drawing.Size(1100, 32);
            this.panelContenido.AutoSize = false;
            this.panelContenido.Dock = System.Windows.Forms.DockStyle.None;
            this.panelContenido.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.panelContenido.Location = new System.Drawing.Point(0, 132);
            this.panelContenido.Size = new System.Drawing.Size(1100, 516);
            this.generar.AutoSize = false;
            this.generar.Dock = System.Windows.Forms.DockStyle.None;
            this.generar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.generar.Location = new System.Drawing.Point(20, 8);
            this.generar.Size = new System.Drawing.Size(130, 36);
            this.resumen.AutoSize = false;
            this.resumen.Dock = System.Windows.Forms.DockStyle.None;
            this.resumen.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.resumen.Location = new System.Drawing.Point(20, 20);
            this.resumen.Size = new System.Drawing.Size(1060, 476);
            panelEncabezado.ResumeLayout(false); panelEncabezado.PerformLayout(); barraAcciones.ResumeLayout(false); barraAcciones.PerformLayout(); panelContenido.ResumeLayout(false); ResumeLayout(false);

            this.Load += new System.EventHandler(this.ReportesFormulario_Load);
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            this.generar.Click += new System.EventHandler(this.generar_Click);
                }
    }
}
