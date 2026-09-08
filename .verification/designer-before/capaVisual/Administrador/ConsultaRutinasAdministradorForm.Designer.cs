using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Administrador
{
    partial class ConsultaRutinasAdministradorForm
    {
        private IContainer components;
        private Panel panelEncabezado;
        private Label lblTitulo;
        private Label lblDescripcion;
        private Button btnVolver;
        private FlowLayoutPanel barraAcciones;
        private Button actualizar;
        private Label lblEstado;
        private Panel panelContenido;
        private DataGridView tabla;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colRutina;
        private DataGridViewTextBoxColumn colDescripcion;
        private DataGridViewTextBoxColumn colEntrenador;
        private DataGridViewTextBoxColumn colAsignados;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new Container(); panelEncabezado = new Panel(); lblTitulo = new Label(); lblDescripcion = new Label(); btnVolver = new Button(); barraAcciones = new FlowLayoutPanel(); actualizar = new Button(); lblEstado = new Label(); panelContenido = new Panel(); tabla = new DataGridView(); colId = new DataGridViewTextBoxColumn(); colRutina = new DataGridViewTextBoxColumn(); colDescripcion = new DataGridViewTextBoxColumn(); colEntrenador = new DataGridViewTextBoxColumn(); colAsignados = new DataGridViewTextBoxColumn();
            panelEncabezado.SuspendLayout(); barraAcciones.SuspendLayout(); panelContenido.SuspendLayout(); ((ISupportInitialize)(tabla)).BeginInit(); SuspendLayout();
            panelEncabezado.BackColor = Color.FromArgb(79, 70, 229); panelEncabezado.Dock = DockStyle.Top; panelEncabezado.Height = 80; panelEncabezado.Controls.Add(lblDescripcion); panelEncabezado.Controls.Add(lblTitulo); panelEncabezado.Controls.Add(btnVolver);
            panelEncabezado.Location = new Point(0, 0); panelEncabezado.Name = "panelEncabezado"; panelEncabezado.Size = new Size(1100, 80); panelEncabezado.TabIndex = 0;
            lblTitulo.AutoSize = true; lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold); lblTitulo.ForeColor = Color.White; lblTitulo.Location = new Point(22, 10); lblTitulo.Text = "Catalogo de rutinas";
            lblTitulo.Name = "lblTitulo"; lblTitulo.Size = new Size(247, 32); lblTitulo.TabIndex = 0;
            lblDescripcion.AutoSize = true; lblDescripcion.ForeColor = Color.FromArgb(226, 232, 240); lblDescripcion.Location = new Point(24, 47); lblDescripcion.Text = "Plantillas reutilizables y cantidad de socios asignados";
            lblDescripcion.Name = "lblDescripcion"; lblDescripcion.Size = new Size(376, 17); lblDescripcion.TabIndex = 1;
            btnVolver.Anchor = AnchorStyles.Top | AnchorStyles.Right; btnVolver.BackColor = Color.White; btnVolver.FlatStyle = FlatStyle.Flat; btnVolver.FlatAppearance.BorderSize = 0; btnVolver.ForeColor = Color.FromArgb(79, 70, 229); btnVolver.Location = new Point(930, 22); btnVolver.Size = new Size(92, 34); btnVolver.Text = "Volver"; btnVolver.UseVisualStyleBackColor = false;
            btnVolver.Name = "btnVolver"; btnVolver.TabIndex = 2;
            barraAcciones.BackColor = Color.White; barraAcciones.Dock = DockStyle.Top; barraAcciones.Height = 52; barraAcciones.Padding = new Padding(16, 8, 16, 8); barraAcciones.WrapContents = false;
            barraAcciones.Location = new Point(0, 80); barraAcciones.Name = "barraAcciones"; barraAcciones.Size = new Size(1100, 52); barraAcciones.TabIndex = 1;
            actualizar.AutoSize = true; actualizar.BackColor = Color.FromArgb(79, 70, 229); actualizar.FlatStyle = FlatStyle.Flat; actualizar.FlatAppearance.BorderSize = 0; actualizar.ForeColor = Color.White; actualizar.Height = 36; actualizar.Margin = new Padding(4, 0, 4, 0); actualizar.Padding = new Padding(12, 0, 12, 0); actualizar.Text = "Actualizar"; actualizar.UseVisualStyleBackColor = false; barraAcciones.Controls.Add(actualizar);
            actualizar.Location = new Point(20, 8); actualizar.Name = "actualizar"; actualizar.Size = new Size(94, 36); actualizar.TabIndex = 0;
            lblEstado.AutoSize = false; lblEstado.BackColor = Color.FromArgb(226, 232, 240); lblEstado.Dock = DockStyle.Bottom; lblEstado.ForeColor = Color.FromArgb(51, 65, 85); lblEstado.Height = 32; lblEstado.Padding = new Padding(18, 8, 8, 0); lblEstado.Text = "Listo";
            lblEstado.Location = new Point(0, 648); lblEstado.Name = "lblEstado"; lblEstado.Size = new Size(1100, 32); lblEstado.TabIndex = 3;
            panelContenido.BackColor = Color.FromArgb(248, 250, 252); panelContenido.Dock = DockStyle.Fill; panelContenido.Padding = new Padding(20); panelContenido.Controls.Add(tabla);
            panelContenido.Location = new Point(0, 132); panelContenido.Name = "panelContenido"; panelContenido.Size = new Size(1100, 516); panelContenido.TabIndex = 2;
            tabla.AllowUserToAddRows = false; tabla.AllowUserToDeleteRows = false; tabla.AllowUserToResizeRows = false; tabla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; tabla.BackgroundColor = Color.White; tabla.BorderStyle = BorderStyle.None; tabla.ColumnHeadersHeight = 38; tabla.Dock = DockStyle.Fill; tabla.MultiSelect = false; tabla.ReadOnly = true; tabla.RowHeadersVisible = false; tabla.SelectionMode = DataGridViewSelectionMode.FullRowSelect; tabla.Columns.AddRange(new DataGridViewColumn[] { colId, colRutina, colDescripcion, colEntrenador, colAsignados });
            tabla.Location = new Point(20, 20); tabla.Name = "tabla"; tabla.Size = new Size(1060, 476); tabla.TabIndex = 0;
            colId.HeaderText = "Id"; colId.Name = "colId"; colId.Visible = false; colId.Width = 60; colRutina.HeaderText = "Rutina"; colRutina.Name = "colRutina"; colRutina.Width = 190; colDescripcion.HeaderText = "Descripcion"; colDescripcion.Name = "colDescripcion"; colDescripcion.Width = 360; colEntrenador.HeaderText = "Creada por"; colEntrenador.Name = "colEntrenador"; colEntrenador.Width = 230; colAsignados.HeaderText = "Socios asignados"; colAsignados.Name = "colAsignados"; colAsignados.Width = 140;
            Controls.Add(panelContenido); Controls.Add(lblEstado); Controls.Add(barraAcciones); Controls.Add(panelEncabezado); AutoScaleMode = AutoScaleMode.Font; BackColor = Color.FromArgb(241, 245, 249); ClientSize = new Size(1100, 680); Font = new Font("Segoe UI", 9.5F); MinimumSize = new Size(760, 540); Name = "ConsultaRutinasAdministradorForm"; StartPosition = FormStartPosition.CenterParent; Text = "SysGym | Catalogo de rutinas";
            panelEncabezado.ResumeLayout(false); panelEncabezado.PerformLayout(); barraAcciones.ResumeLayout(false); barraAcciones.PerformLayout(); panelContenido.ResumeLayout(false); ((ISupportInitialize)(tabla)).EndInit(); ResumeLayout(false);
        }
    }
}
