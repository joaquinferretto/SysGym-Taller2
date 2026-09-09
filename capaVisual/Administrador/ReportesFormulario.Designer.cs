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
            panelEncabezado.BackColor = Color.FromArgb(79, 70, 229);   panelEncabezado.Controls.Add(lblDescripcion); panelEncabezado.Controls.Add(lblTitulo); panelEncabezado.Controls.Add(btnVolver);   lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold); lblTitulo.ForeColor = Color.White;  lblTitulo.Text = "Reportes";  lblDescripcion.ForeColor = Color.FromArgb(226, 232, 240);  lblDescripcion.Text = "Indicadores generales del estado del gimnasio";  btnVolver.BackColor = Color.White; btnVolver.FlatStyle = FlatStyle.Flat; btnVolver.FlatAppearance.BorderSize = 0; btnVolver.ForeColor = Color.FromArgb(79, 70, 229);   btnVolver.Text = "Volver"; btnVolver.UseVisualStyleBackColor = false;
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
            Controls.Add(panelContenido); Controls.Add(lblEstado); Controls.Add(barraAcciones); Controls.Add(panelEncabezado); AutoScaleMode = AutoScaleMode.Font;  BackColor = Color.FromArgb(241, 245, 249); ClientSize = new Size(1100, 680); Font = new Font("Segoe UI", 9.5F); MinimumSize = new Size(900, 560); this.Name = "ReportesFormulario"; StartPosition = FormStartPosition.CenterParent; Text = "SysGym | Reportes";

            // Encabezado: titulo y descripcion a la izquierda, accion de regreso a la derecha.

            this.panelEncabezado.Padding = new Padding(22, 8, 22, 8);

            this.lblTitulo.Margin = new Padding(0);
            this.lblTitulo.TextAlign = ContentAlignment.BottomLeft;

            this.lblDescripcion.Margin = new Padding(0);
            this.lblDescripcion.TextAlign = ContentAlignment.TopLeft;

            this.btnVolver.Margin = new Padding(16, 0, 0, 0);
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
            this.panelEncabezado.AutoSize = false;
            this.panelEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEncabezado.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.panelEncabezado.Location = new System.Drawing.Point(0, 0);
            this.panelEncabezado.Size = new System.Drawing.Size(1100, 84);
            this.panelEncabezado.AutoScroll = false;
            this.lblTitulo.AutoSize = false;
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.None;
            this.lblTitulo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblTitulo.Location = new System.Drawing.Point(22, 8);
            this.lblTitulo.Size = new System.Drawing.Size(890, 36);
            this.lblDescripcion.AutoSize = false;
            this.lblDescripcion.Dock = System.Windows.Forms.DockStyle.None;
            this.lblDescripcion.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblDescripcion.Location = new System.Drawing.Point(24, 48);
            this.lblDescripcion.Size = new System.Drawing.Size(890, 26);
            this.btnVolver.AutoSize = false;
            this.btnVolver.Dock = System.Windows.Forms.DockStyle.None;
            this.btnVolver.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnVolver.Location = new System.Drawing.Point(974, 24);
            this.btnVolver.Size = new System.Drawing.Size(104, 38);
            this.barraAcciones.AutoSize = false;
            this.barraAcciones.Dock = System.Windows.Forms.DockStyle.Top;
            this.barraAcciones.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.barraAcciones.Location = new System.Drawing.Point(0, 84);
            this.barraAcciones.Size = new System.Drawing.Size(1100, 52);
            this.barraAcciones.AutoScroll = false;
            this.lblEstado.AutoSize = false;
            this.lblEstado.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblEstado.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblEstado.Location = new System.Drawing.Point(0, 650);
            this.lblEstado.Size = new System.Drawing.Size(1100, 30);
            this.panelContenido.AutoSize = false;
            this.panelContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenido.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.panelContenido.Location = new System.Drawing.Point(0, 136);
            this.panelContenido.Size = new System.Drawing.Size(1100, 514);
            this.panelContenido.AutoScroll = false;
            this.generar.AutoSize = false;
            this.generar.Dock = System.Windows.Forms.DockStyle.None;
            this.generar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.generar.Location = new System.Drawing.Point(16, 8);
            this.generar.Size = new System.Drawing.Size(130, 34);
            this.resumen.AutoSize = false;
            this.resumen.Dock = System.Windows.Forms.DockStyle.None;
            this.resumen.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.resumen.Location = new System.Drawing.Point(16, 16);
            this.resumen.Size = new System.Drawing.Size(1068, 482);
            panelEncabezado.ResumeLayout(false); panelEncabezado.PerformLayout(); barraAcciones.ResumeLayout(false); barraAcciones.PerformLayout(); panelContenido.ResumeLayout(false); ResumeLayout(false);

            this.Load += new System.EventHandler(this.ReportesFormulario_Load);
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            this.generar.Click += new System.EventHandler(this.generar_Click);
                }
    }
}
