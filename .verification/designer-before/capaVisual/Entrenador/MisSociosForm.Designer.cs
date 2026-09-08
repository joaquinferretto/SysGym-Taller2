using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Entrenador
{
    partial class MisSociosForm
    {
        private IContainer components;
        private Panel panelEncabezado; private Label lblTitulo; private Label lblDescripcion; private Button btnVolver; private FlowLayoutPanel barraAcciones; private Label lblEstado; private Panel panelContenido; private DataGridView tabla; private DataGridViewTextBoxColumn colId; private DataGridViewTextBoxColumn colSocio; private DataGridViewTextBoxColumn colRutinas;
        private Button actualizar;
        protected override void Dispose(bool disposing) { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }
        private void InitializeComponent()
        {
            components = new Container(); panelEncabezado = new Panel(); lblTitulo = new Label(); lblDescripcion = new Label(); btnVolver = new Button(); barraAcciones = new FlowLayoutPanel(); actualizar = new Button(); lblEstado = new Label(); panelContenido = new Panel(); tabla = new DataGridView(); colId = new DataGridViewTextBoxColumn(); colSocio = new DataGridViewTextBoxColumn(); colRutinas = new DataGridViewTextBoxColumn(); panelEncabezado.SuspendLayout(); barraAcciones.SuspendLayout(); panelContenido.SuspendLayout(); ((ISupportInitialize)(tabla)).BeginInit(); SuspendLayout();
            panelEncabezado.BackColor = Color.FromArgb(14, 116, 144); panelEncabezado.Dock = DockStyle.Top; panelEncabezado.Height = 80; panelEncabezado.Controls.Add(lblDescripcion); panelEncabezado.Controls.Add(lblTitulo); panelEncabezado.Controls.Add(btnVolver); lblTitulo.AutoSize = true; lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold); lblTitulo.ForeColor = Color.White; lblTitulo.Location = new Point(22, 10); lblTitulo.Text = "Mis socios"; lblDescripcion.AutoSize = true; lblDescripcion.ForeColor = Color.FromArgb(226, 232, 240); lblDescripcion.Location = new Point(24, 47); lblDescripcion.Text = "Socios con plantillas asignadas a este entrenador"; btnVolver.Anchor = AnchorStyles.Top | AnchorStyles.Right; btnVolver.BackColor = Color.White; btnVolver.FlatStyle = FlatStyle.Flat; btnVolver.FlatAppearance.BorderSize = 0; btnVolver.ForeColor = Color.FromArgb(14, 116, 144); btnVolver.Location = new Point(930, 22); btnVolver.Size = new Size(92, 34); btnVolver.Text = "Volver"; btnVolver.UseVisualStyleBackColor = false;
            barraAcciones.BackColor = Color.White; barraAcciones.Dock = DockStyle.Top; barraAcciones.Height = 52; barraAcciones.Padding = new Padding(16, 8, 16, 8); actualizar.AutoSize = true; actualizar.BackColor = Color.FromArgb(14, 116, 144); actualizar.FlatStyle = FlatStyle.Flat; actualizar.FlatAppearance.BorderSize = 0; actualizar.ForeColor = Color.White; actualizar.Height = 36; actualizar.Margin = new Padding(4, 0, 4, 0); actualizar.Padding = new Padding(12, 0, 12, 0); actualizar.Text = "Actualizar"; actualizar.UseVisualStyleBackColor = false; barraAcciones.Controls.Add(actualizar);
            lblEstado.AutoSize = false; lblEstado.BackColor = Color.FromArgb(226, 232, 240); lblEstado.Dock = DockStyle.Bottom; lblEstado.ForeColor = Color.FromArgb(51, 65, 85); lblEstado.Height = 32; lblEstado.Padding = new Padding(18, 8, 8, 0); lblEstado.Text = "Listo"; panelContenido.BackColor = Color.FromArgb(248, 250, 252); panelContenido.Dock = DockStyle.Fill; panelContenido.Padding = new Padding(20); panelContenido.Controls.Add(tabla); tabla.AllowUserToAddRows = false; tabla.AllowUserToDeleteRows = false; tabla.AllowUserToResizeRows = false; tabla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; tabla.BackgroundColor = Color.White; tabla.BorderStyle = BorderStyle.None; tabla.ColumnHeadersHeight = 38; tabla.Dock = DockStyle.Fill; tabla.MultiSelect = false; tabla.ReadOnly = true; tabla.RowHeadersVisible = false; tabla.SelectionMode = DataGridViewSelectionMode.FullRowSelect; tabla.Columns.AddRange(new DataGridViewColumn[] { colId, colSocio, colRutinas }); colId.HeaderText = "Id"; colId.Name = "colId"; colId.Visible = false; colSocio.HeaderText = "Socio"; colSocio.Name = "colSocio"; colRutinas.HeaderText = "Rutinas asignadas"; colRutinas.Name = "colRutinas";
            panelEncabezado.Location = new Point(0, 0); panelEncabezado.Name = "panelEncabezado"; panelEncabezado.Size = new Size(1100, 80); panelEncabezado.TabIndex = 0;
            lblTitulo.Name = "lblTitulo"; lblTitulo.Size = new Size(132, 32); lblTitulo.TabIndex = 0;
            lblDescripcion.Name = "lblDescripcion"; lblDescripcion.Size = new Size(337, 17); lblDescripcion.TabIndex = 1;
            btnVolver.Name = "btnVolver"; btnVolver.TabIndex = 2;
            barraAcciones.Location = new Point(0, 80); barraAcciones.Name = "barraAcciones"; barraAcciones.Size = new Size(1100, 52); barraAcciones.TabIndex = 1; barraAcciones.WrapContents = false;
            actualizar.Location = new Point(20, 8); actualizar.Name = "actualizar"; actualizar.Size = new Size(94, 36); actualizar.TabIndex = 0;
            lblEstado.Location = new Point(0, 648); lblEstado.Name = "lblEstado"; lblEstado.Size = new Size(1100, 32); lblEstado.TabIndex = 3;
            panelContenido.Location = new Point(0, 132); panelContenido.Name = "panelContenido"; panelContenido.Size = new Size(1100, 516); panelContenido.TabIndex = 2;
            tabla.Location = new Point(20, 20); tabla.Name = "tabla"; tabla.Size = new Size(1060, 476); tabla.TabIndex = 0;
            colId.Width = 60; colSocio.Width = 700; colRutinas.Width = 260;
            Controls.Add(panelContenido); Controls.Add(lblEstado); Controls.Add(barraAcciones); Controls.Add(panelEncabezado); AutoScaleMode = AutoScaleMode.Font; BackColor = Color.FromArgb(241, 245, 249); ClientSize = new Size(1100, 680); Font = new Font("Segoe UI", 9.5F); MinimumSize = new Size(760, 540); Name = "MisSociosForm"; StartPosition = FormStartPosition.CenterParent; Text = "SysGym | Mis socios";
            panelEncabezado.ResumeLayout(false); panelEncabezado.PerformLayout(); barraAcciones.ResumeLayout(false); barraAcciones.PerformLayout(); panelContenido.ResumeLayout(false); ((ISupportInitialize)(tabla)).EndInit(); ResumeLayout(false);
        }
    }
}
