using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Entrenador
{
    partial class MisSociosFormulario
    {
        private IContainer components;
        private Panel panelEncabezado; private Label lblTitulo; private Label lblDescripcion; private Button btnVolver; private Panel barraAcciones; private Label lblEstado; private Panel panelContenido; private DataGridView tabla; private DataGridViewTextBoxColumn colId; private DataGridViewTextBoxColumn colSocio; private DataGridViewTextBoxColumn colRutinas;
        private Button actualizar; private Button verRutina;
        protected override void Dispose(bool liberarRecursos) { if (liberarRecursos && components != null) components.Dispose(); base.Dispose(liberarRecursos); }
        private void InitializeComponent()
        {
            components = new Container(); panelEncabezado = new Panel(); lblTitulo = new Label(); lblDescripcion = new Label(); btnVolver = new Button(); barraAcciones = new Panel(); actualizar = new Button(); verRutina = new Button(); lblEstado = new Label(); panelContenido = new Panel(); tabla = new DataGridView(); colId = new DataGridViewTextBoxColumn(); colSocio = new DataGridViewTextBoxColumn(); colRutinas = new DataGridViewTextBoxColumn(); panelEncabezado.SuspendLayout(); barraAcciones.SuspendLayout(); panelContenido.SuspendLayout(); ((ISupportInitialize)(tabla)).BeginInit(); SuspendLayout();
            panelEncabezado.BackColor = Color.FromArgb(14, 116, 144);   panelEncabezado.Controls.Add(lblDescripcion); panelEncabezado.Controls.Add(lblTitulo); panelEncabezado.Controls.Add(btnVolver);   lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold); lblTitulo.ForeColor = Color.White;  lblTitulo.Text = "Mis socios";  lblDescripcion.ForeColor = Color.FromArgb(226, 232, 240);  lblDescripcion.Text = "Socios con plantillas asignadas a este entrenador";  btnVolver.BackColor = Color.White; btnVolver.FlatStyle = FlatStyle.Flat; btnVolver.FlatAppearance.BorderSize = 0; btnVolver.ForeColor = Color.FromArgb(14, 116, 144);   btnVolver.Text = "Volver"; btnVolver.UseVisualStyleBackColor = false;
            barraAcciones.BackColor = Color.White;   barraAcciones.Padding = new Padding(16, 8, 16, 8);  actualizar.BackColor = Color.FromArgb(14, 116, 144); actualizar.FlatStyle = FlatStyle.Flat; actualizar.FlatAppearance.BorderSize = 0; actualizar.ForeColor = Color.White;  actualizar.Margin = new Padding(4, 0, 4, 0); actualizar.Padding = new Padding(12, 0, 12, 0); actualizar.Text = "Actualizar"; actualizar.UseVisualStyleBackColor = false; barraAcciones.Controls.Add(actualizar); verRutina.BackColor = Color.FromArgb(226, 232, 240); verRutina.FlatStyle = FlatStyle.Flat; verRutina.FlatAppearance.BorderSize = 0; verRutina.ForeColor = Color.FromArgb(30, 41, 59); verRutina.Margin = new Padding(4, 0, 4, 0); verRutina.Padding = new Padding(12, 0, 12, 0); verRutina.Text = "Ver rutina semanal"; verRutina.UseVisualStyleBackColor = false; barraAcciones.Controls.Add(verRutina);
             lblEstado.BackColor = Color.FromArgb(226, 232, 240);  lblEstado.ForeColor = Color.FromArgb(51, 65, 85);  lblEstado.Padding = new Padding(18, 8, 8, 0); lblEstado.Text = "Listo"; panelContenido.BackColor = Color.FromArgb(248, 250, 252);  panelContenido.Padding = new Padding(20); panelContenido.Controls.Add(tabla);     tabla.BackgroundColor = Color.White; tabla.BorderStyle = BorderStyle.None;       tabla.Columns.AddRange(new DataGridViewColumn[] { colId, colSocio, colRutinas }); colId.HeaderText = "Id"; colId.Name = "colId"; colId.Visible = false; colSocio.HeaderText = "Socio"; colSocio.Name = "colSocio"; colRutinas.HeaderText = "Rutinas asignadas"; colRutinas.Name = "colRutinas";
             panelEncabezado.Name = "panelEncabezado";  panelEncabezado.TabIndex = 0;
            lblTitulo.Name = "lblTitulo";  lblTitulo.TabIndex = 0;
            lblDescripcion.Name = "lblDescripcion";  lblDescripcion.TabIndex = 1;
            btnVolver.Name = "btnVolver"; btnVolver.TabIndex = 2;
             barraAcciones.Name = "barraAcciones";  barraAcciones.TabIndex = 1;
             actualizar.Name = "actualizar";  actualizar.TabIndex = 0;
             verRutina.Name = "verRutina";  verRutina.TabIndex = 1;
             lblEstado.Name = "lblEstado";  lblEstado.TabIndex = 3;
             panelContenido.Name = "panelContenido";  panelContenido.TabIndex = 2;
             tabla.Name = "tabla";  tabla.TabIndex = 0;
            colId.FillWeight = 60; colSocio.FillWeight = 700; colSocio.MinimumWidth = 90; colRutinas.FillWeight = 260; colRutinas.MinimumWidth = 90;
            Controls.Add(panelContenido); Controls.Add(lblEstado); Controls.Add(barraAcciones); Controls.Add(panelEncabezado); AutoScaleMode = AutoScaleMode.Font;  BackColor = Color.FromArgb(241, 245, 249); ClientSize = new Size(1100, 680); Font = new Font("Segoe UI", 9.5F); MinimumSize = new Size(900, 560); this.Name = "MisSociosFormulario"; StartPosition = FormStartPosition.CenterParent; Text = "SysGym | Mis socios";

            // Encabezado: titulo y descripcion a la izquierda, accion de regreso a la derecha.

            this.panelEncabezado.Padding = new Padding(22, 8, 22, 8);

            this.lblTitulo.Margin = new Padding(0);
            this.lblTitulo.TextAlign = ContentAlignment.BottomLeft;

            this.lblDescripcion.Margin = new Padding(0);
            this.lblDescripcion.TextAlign = ContentAlignment.TopLeft;

            this.btnVolver.Margin = new Padding(16, 0, 0, 0);
            // Barra de acciones: los controles se reacomodan cuando el ancho disminuye.

            this.barraAcciones.Padding = new Padding(16, 8, 16, 8);

            this.actualizar.MinimumSize = new Size(120, 34);
            this.actualizar.Margin = new Padding(0, 0, 8, 0);
            this.actualizar.Padding = new Padding(12, 0, 12, 0);
            // Barra de estado inferior.

            this.lblEstado.Padding = new Padding(18, 0, 12, 0);
            this.lblEstado.TextAlign = ContentAlignment.MiddleLeft;
            // El contenido ocupa todo el espacio restante de la ventana.

            this.panelContenido.Padding = new Padding(16);

            this.tabla.RowTemplate.Height = 30;

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
            this.tabla.AutoSize = false;
            this.tabla.Dock = System.Windows.Forms.DockStyle.None;
            this.tabla.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.tabla.Location = new System.Drawing.Point(16, 16);
            this.tabla.Size = new System.Drawing.Size(1068, 482);
            this.tabla.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tabla.ReadOnly = true;
            this.tabla.AllowUserToAddRows = false;
            this.tabla.AllowUserToDeleteRows = false;
            this.tabla.AllowUserToResizeRows = false;
            this.tabla.RowHeadersVisible = false;
            this.tabla.MultiSelect = false;
            this.tabla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tabla.ColumnHeadersHeight = 46;
            this.actualizar.AutoSize = false;
            this.actualizar.Dock = System.Windows.Forms.DockStyle.None;
            this.actualizar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.actualizar.Location = new System.Drawing.Point(16, 8);
            this.actualizar.Size = new System.Drawing.Size(120, 34);
            this.verRutina.AutoSize = false;
            this.verRutina.Dock = System.Windows.Forms.DockStyle.None;
            this.verRutina.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.verRutina.MinimumSize = new System.Drawing.Size(120, 34);
            this.verRutina.Location = new System.Drawing.Point(144, 8);
            this.verRutina.Size = new System.Drawing.Size(160, 34);
            panelEncabezado.ResumeLayout(false); panelEncabezado.PerformLayout(); barraAcciones.ResumeLayout(false); barraAcciones.PerformLayout(); panelContenido.ResumeLayout(false); ((ISupportInitialize)(tabla)).EndInit(); ResumeLayout(false);

            this.Load += new System.EventHandler(this.MisSociosFormulario_Load);
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            this.actualizar.Click += new System.EventHandler(this.actualizar_Click);
            this.verRutina.Click += new System.EventHandler(this.verRutina_Click);
                }
    }
}
