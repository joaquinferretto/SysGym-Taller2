using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Entrenador
{
    partial class MisSociosFormulario
    {
        private IContainer components;
        private Panel panelEncabezado; private Label lblTitulo; private Label lblDescripcion; private Button btnVolver; private Panel barraAcciones; private Label lblEstado; private Panel panelContenido; private DataGridView tabla; private DataGridViewTextBoxColumn colId; private DataGridViewTextBoxColumn colSocio; private DataGridViewTextBoxColumn colRutinas;
        private Button actualizar;
        protected override void Dispose(bool liberarRecursos) { if (liberarRecursos && components != null) components.Dispose(); base.Dispose(liberarRecursos); }
        private void InitializeComponent()
        {
            components = new Container(); panelEncabezado = new Panel(); lblTitulo = new Label(); lblDescripcion = new Label(); btnVolver = new Button(); barraAcciones = new Panel(); actualizar = new Button(); lblEstado = new Label(); panelContenido = new Panel(); tabla = new DataGridView(); colId = new DataGridViewTextBoxColumn(); colSocio = new DataGridViewTextBoxColumn(); colRutinas = new DataGridViewTextBoxColumn(); panelEncabezado.SuspendLayout(); barraAcciones.SuspendLayout(); panelContenido.SuspendLayout(); ((ISupportInitialize)(tabla)).BeginInit(); SuspendLayout();
            panelEncabezado.BackColor = Color.FromArgb(14, 116, 144);   panelEncabezado.Controls.Add(lblDescripcion); panelEncabezado.Controls.Add(lblTitulo); panelEncabezado.Controls.Add(btnVolver);  lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold); lblTitulo.ForeColor = Color.White;  lblTitulo.Text = "Mis socios";  lblDescripcion.ForeColor = Color.FromArgb(226, 232, 240);  lblDescripcion.Text = "Socios con plantillas asignadas a este entrenador";  btnVolver.BackColor = Color.White; btnVolver.FlatStyle = FlatStyle.Flat; btnVolver.FlatAppearance.BorderSize = 0; btnVolver.ForeColor = Color.FromArgb(14, 116, 144);   btnVolver.Text = "Volver"; btnVolver.UseVisualStyleBackColor = false;
            barraAcciones.BackColor = Color.White;   barraAcciones.Padding = new Padding(16, 8, 16, 8);  actualizar.BackColor = Color.FromArgb(14, 116, 144); actualizar.FlatStyle = FlatStyle.Flat; actualizar.FlatAppearance.BorderSize = 0; actualizar.ForeColor = Color.White;  actualizar.Margin = new Padding(4, 0, 4, 0); actualizar.Padding = new Padding(12, 0, 12, 0); actualizar.Text = "Actualizar"; actualizar.UseVisualStyleBackColor = false; barraAcciones.Controls.Add(actualizar);
             lblEstado.BackColor = Color.FromArgb(226, 232, 240);  lblEstado.ForeColor = Color.FromArgb(51, 65, 85);  lblEstado.Padding = new Padding(18, 8, 8, 0); lblEstado.Text = "Listo"; panelContenido.BackColor = Color.FromArgb(248, 250, 252);  panelContenido.Padding = new Padding(20); panelContenido.Controls.Add(tabla); tabla.AllowUserToAddRows = false; tabla.AllowUserToDeleteRows = false; tabla.AllowUserToResizeRows = false; tabla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; tabla.BackgroundColor = Color.White; tabla.BorderStyle = BorderStyle.None; tabla.ColumnHeadersHeight = 38;  tabla.MultiSelect = false; tabla.ReadOnly = true; tabla.RowHeadersVisible = false; tabla.SelectionMode = DataGridViewSelectionMode.FullRowSelect; tabla.Columns.AddRange(new DataGridViewColumn[] { colId, colSocio, colRutinas }); colId.HeaderText = "Id"; colId.Name = "colId"; colId.Visible = false; colSocio.HeaderText = "Socio"; colSocio.Name = "colSocio"; colRutinas.HeaderText = "Rutinas asignadas"; colRutinas.Name = "colRutinas";
             panelEncabezado.Name = "panelEncabezado";  panelEncabezado.TabIndex = 0;
            lblTitulo.Name = "lblTitulo";  lblTitulo.TabIndex = 0;
            lblDescripcion.Name = "lblDescripcion";  lblDescripcion.TabIndex = 1;
            btnVolver.Name = "btnVolver"; btnVolver.TabIndex = 2;
             barraAcciones.Name = "barraAcciones";  barraAcciones.TabIndex = 1; 
             actualizar.Name = "actualizar";  actualizar.TabIndex = 0;
             lblEstado.Name = "lblEstado";  lblEstado.TabIndex = 3;
             panelContenido.Name = "panelContenido";  panelContenido.TabIndex = 2;
             tabla.Name = "tabla";  tabla.TabIndex = 0;
            colId.Width = 60; colSocio.Width = 700; colRutinas.Width = 260;
            Controls.Add(panelContenido); Controls.Add(lblEstado); Controls.Add(barraAcciones); Controls.Add(panelEncabezado); AutoScaleMode = AutoScaleMode.Font; this.AutoScroll = true; BackColor = Color.FromArgb(241, 245, 249); ClientSize = new Size(1100, 680); Font = new Font("Segoe UI", 9.5F); MinimumSize = new Size(760, 540); this.Name = "MisSociosFormulario"; StartPosition = FormStartPosition.CenterParent; Text = "SysGym | Mis socios";

            this.panelEncabezado.AutoSize = false;
            this.panelEncabezado.Dock = System.Windows.Forms.DockStyle.None;
            this.panelEncabezado.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.panelEncabezado.Location = new System.Drawing.Point(0, 0);
            this.panelEncabezado.Size = new System.Drawing.Size(1100, 80);
            this.lblTitulo.AutoSize = false;
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.None;
            this.lblTitulo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblTitulo.Location = new System.Drawing.Point(22, 10);
            this.lblTitulo.Size = new System.Drawing.Size(128, 38);
            this.lblDescripcion.AutoSize = false;
            this.lblDescripcion.Dock = System.Windows.Forms.DockStyle.None;
            this.lblDescripcion.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblDescripcion.Location = new System.Drawing.Point(24, 47);
            this.lblDescripcion.Size = new System.Drawing.Size(286, 22);
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
            this.tabla.AutoSize = false;
            this.tabla.Dock = System.Windows.Forms.DockStyle.None;
            this.tabla.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.tabla.Location = new System.Drawing.Point(20, 20);
            this.tabla.Size = new System.Drawing.Size(1060, 476);
            this.actualizar.AutoSize = false;
            this.actualizar.Dock = System.Windows.Forms.DockStyle.None;
            this.actualizar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.actualizar.Location = new System.Drawing.Point(20, 8);
            this.actualizar.Size = new System.Drawing.Size(95, 36);
            panelEncabezado.ResumeLayout(false); panelEncabezado.PerformLayout(); barraAcciones.ResumeLayout(false); barraAcciones.PerformLayout(); panelContenido.ResumeLayout(false); ((ISupportInitialize)(tabla)).EndInit(); ResumeLayout(false);

            this.Load += new System.EventHandler(this.MisSociosFormulario_Load);
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            this.actualizar.Click += new System.EventHandler(this.actualizar_Click);
                }
    }
}
