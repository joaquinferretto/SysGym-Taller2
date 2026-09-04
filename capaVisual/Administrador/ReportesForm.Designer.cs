using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Administrador
{
    partial class ReportesForm
    {
        private IContainer components;
        private Panel panelEncabezado;
        private Label lblTitulo;
        private Label lblDescripcion;
        private Button btnVolver;
        private FlowLayoutPanel barraAcciones;
        private Label lblEstado;
        private Panel panelContenido;
        private Button generar;
        private Label resumen;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new Container(); panelEncabezado = new Panel(); lblTitulo = new Label(); lblDescripcion = new Label(); btnVolver = new Button(); barraAcciones = new FlowLayoutPanel(); generar = new Button(); lblEstado = new Label(); panelContenido = new Panel(); resumen = new Label();
            panelEncabezado.SuspendLayout(); barraAcciones.SuspendLayout(); panelContenido.SuspendLayout(); SuspendLayout();
            panelEncabezado.BackColor = Color.FromArgb(79, 70, 229); panelEncabezado.Dock = DockStyle.Top; panelEncabezado.Height = 80; panelEncabezado.Controls.Add(lblDescripcion); panelEncabezado.Controls.Add(lblTitulo); panelEncabezado.Controls.Add(btnVolver); lblTitulo.AutoSize = true; lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold); lblTitulo.ForeColor = Color.White; lblTitulo.Location = new Point(22, 10); lblTitulo.Text = "Reportes"; lblDescripcion.AutoSize = true; lblDescripcion.ForeColor = Color.FromArgb(226, 232, 240); lblDescripcion.Location = new Point(24, 47); lblDescripcion.Text = "Indicadores generales del estado del gimnasio"; btnVolver.Anchor = AnchorStyles.Top | AnchorStyles.Right; btnVolver.BackColor = Color.White; btnVolver.FlatStyle = FlatStyle.Flat; btnVolver.FlatAppearance.BorderSize = 0; btnVolver.ForeColor = Color.FromArgb(79, 70, 229); btnVolver.Location = new Point(930, 22); btnVolver.Size = new Size(92, 34); btnVolver.Text = "Volver"; btnVolver.UseVisualStyleBackColor = false;
            barraAcciones.BackColor = Color.White; barraAcciones.Dock = DockStyle.Top; barraAcciones.Height = 52; barraAcciones.Padding = new Padding(16, 8, 16, 8); barraAcciones.WrapContents = false; generar.AutoSize = true; generar.BackColor = Color.FromArgb(79, 70, 229); generar.FlatStyle = FlatStyle.Flat; generar.FlatAppearance.BorderSize = 0; generar.ForeColor = Color.White; generar.Height = 36; generar.Margin = new Padding(4, 0, 4, 0); generar.Padding = new Padding(12, 0, 12, 0); generar.Text = "Generar reporte"; generar.UseVisualStyleBackColor = false; barraAcciones.Controls.Add(generar);
            lblEstado.AutoSize = false; lblEstado.BackColor = Color.FromArgb(226, 232, 240); lblEstado.Dock = DockStyle.Bottom; lblEstado.ForeColor = Color.FromArgb(51, 65, 85); lblEstado.Height = 32; lblEstado.Padding = new Padding(18, 8, 8, 0); lblEstado.Text = "Listo";
            panelContenido.BackColor = Color.FromArgb(248, 250, 252); panelContenido.Dock = DockStyle.Fill; panelContenido.Padding = new Padding(20); panelContenido.Controls.Add(resumen); resumen.BackColor = Color.White; resumen.BorderStyle = BorderStyle.FixedSingle; resumen.Dock = DockStyle.Fill; resumen.Font = new Font("Segoe UI", 16F, FontStyle.Bold); resumen.ForeColor = Color.FromArgb(30, 41, 59); resumen.Padding = new Padding(40); resumen.TextAlign = ContentAlignment.MiddleCenter; resumen.Text = "Reporte pendiente";
            panelEncabezado.Location = new Point(0, 0); panelEncabezado.Name = "panelEncabezado"; panelEncabezado.Size = new Size(1100, 80); panelEncabezado.TabIndex = 0;
            lblTitulo.Name = "lblTitulo"; lblTitulo.Size = new Size(112, 32); lblTitulo.TabIndex = 0;
            lblDescripcion.Name = "lblDescripcion"; lblDescripcion.Size = new Size(307, 17); lblDescripcion.TabIndex = 1;
            btnVolver.Name = "btnVolver"; btnVolver.TabIndex = 2;
            barraAcciones.Location = new Point(0, 80); barraAcciones.Name = "barraAcciones"; barraAcciones.Size = new Size(1100, 52); barraAcciones.TabIndex = 1;
            generar.Location = new Point(20, 8); generar.Name = "generar"; generar.Size = new Size(128, 36); generar.TabIndex = 0;
            lblEstado.Location = new Point(0, 648); lblEstado.Name = "lblEstado"; lblEstado.Size = new Size(1100, 32); lblEstado.TabIndex = 3;
            panelContenido.Location = new Point(0, 132); panelContenido.Name = "panelContenido"; panelContenido.Size = new Size(1100, 516); panelContenido.TabIndex = 2;
            resumen.Location = new Point(20, 20); resumen.Name = "resumen"; resumen.Size = new Size(1060, 476); resumen.TabIndex = 0;
            Controls.Add(panelContenido); Controls.Add(lblEstado); Controls.Add(barraAcciones); Controls.Add(panelEncabezado); AutoScaleMode = AutoScaleMode.Font; BackColor = Color.FromArgb(241, 245, 249); ClientSize = new Size(1100, 680); Font = new Font("Segoe UI", 9.5F); MinimumSize = new Size(760, 540); Name = "ReportesForm"; StartPosition = FormStartPosition.CenterParent; Text = "SysGym | Reportes";
            panelEncabezado.ResumeLayout(false); panelEncabezado.PerformLayout(); barraAcciones.ResumeLayout(false); barraAcciones.PerformLayout(); panelContenido.ResumeLayout(false); ResumeLayout(false);
        }
    }
}
