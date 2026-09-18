using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Recepcionista
{
    partial class GestionAsignacionesFormulario
    {
        private IContainer components;
        private Panel panelEncabezado; private Label lblTitulo; private Label lblDescripcion; private Button btnVolver; private Panel barraAcciones; private Label lblEstado; private Panel panelContenido; private Panel panelFormulario; private Panel contenedorFormulario; private Label lblMembresia; private Label lblEntrenador; private DataGridView tabla; private DataGridViewTextBoxColumn colId; private DataGridViewTextBoxColumn colMembresia; private DataGridViewTextBoxColumn colEntrenador; private DataGridViewTextBoxColumn colEstado;
        private ComboBox membresia; private ComboBox entrenador; private Button asignar; private Button cambiar; private Button consultar; private Button darDeBaja;
        protected override void Dispose(bool liberarRecursos) { if (liberarRecursos && components != null) components.Dispose(); base.Dispose(liberarRecursos); }

        private void InitializeComponent()
        {
            components = new Container(); panelEncabezado = new Panel(); lblTitulo = new Label(); lblDescripcion = new Label(); btnVolver = new Button(); barraAcciones = new Panel(); asignar = new Button(); cambiar = new Button(); consultar = new Button(); darDeBaja = new Button(); lblEstado = new Label(); panelContenido = new Panel(); panelFormulario = new Panel(); contenedorFormulario = new Panel(); lblMembresia = new Label(); membresia = new ComboBox(); lblEntrenador = new Label(); entrenador = new ComboBox(); tabla = new DataGridView(); colId = new DataGridViewTextBoxColumn(); colMembresia = new DataGridViewTextBoxColumn(); colEntrenador = new DataGridViewTextBoxColumn(); colEstado = new DataGridViewTextBoxColumn(); panelEncabezado.SuspendLayout(); barraAcciones.SuspendLayout(); panelContenido.SuspendLayout(); panelFormulario.SuspendLayout(); contenedorFormulario.SuspendLayout(); ((ISupportInitialize)(tabla)).BeginInit(); SuspendLayout();
            panelEncabezado.BackColor = Color.FromArgb(5, 150, 105);   panelEncabezado.Controls.Add(lblDescripcion); panelEncabezado.Controls.Add(lblTitulo); panelEncabezado.Controls.Add(btnVolver);   lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold); lblTitulo.ForeColor = Color.White;  lblTitulo.Text = "Asignar entrenador";  lblDescripcion.ForeColor = Color.FromArgb(226, 232, 240);  lblDescripcion.Text = "Vinculacion de entrenadores con membresias";  btnVolver.BackColor = Color.White; btnVolver.FlatStyle = FlatStyle.Flat; btnVolver.FlatAppearance.BorderSize = 0; btnVolver.ForeColor = Color.FromArgb(5, 150, 105);   btnVolver.Text = "Volver"; btnVolver.UseVisualStyleBackColor = false;
            barraAcciones.BackColor = Color.White;   barraAcciones.Padding = new Padding(16, 8, 16, 8);  barraAcciones.Controls.Add(asignar); barraAcciones.Controls.Add(cambiar); barraAcciones.Controls.Add(consultar); barraAcciones.Controls.Add(darDeBaja);
             lblEstado.BackColor = Color.FromArgb(226, 232, 240);  lblEstado.ForeColor = Color.FromArgb(51, 65, 85);  lblEstado.Padding = new Padding(18, 8, 8, 0); lblEstado.Text = "Listo"; panelContenido.BackColor = Color.FromArgb(248, 250, 252);  panelContenido.Padding = new Padding(20); panelContenido.Controls.Add(tabla); panelContenido.Controls.Add(panelFormulario); panelFormulario.BackColor = Color.White; panelFormulario.BorderStyle = BorderStyle.FixedSingle;   panelFormulario.Padding = new Padding(12); panelFormulario.Controls.Add(contenedorFormulario);       contenedorFormulario.Controls.Add(lblMembresia); contenedorFormulario.Controls.Add(membresia); contenedorFormulario.Controls.Add(lblEntrenador); contenedorFormulario.Controls.Add(entrenador);  membresia.DropDownStyle = ComboBoxStyle.DropDownList; membresia.FormattingEnabled = true;  entrenador.DropDownStyle = ComboBoxStyle.DropDownList;
                tabla.BackgroundColor = Color.White; tabla.BorderStyle = BorderStyle.None;       tabla.Columns.AddRange(new DataGridViewColumn[] { colId, colMembresia, colEntrenador, colEstado }); colId.HeaderText = "Id"; colId.Name = "colId"; colId.Visible = false; colMembresia.HeaderText = "Membresia"; colMembresia.Name = "colMembresia"; colEntrenador.HeaderText = "Entrenador"; colEntrenador.Name = "colEntrenador"; colEstado.HeaderText = "Estado"; colEstado.Name = "colEstado";
             panelEncabezado.Name = "panelEncabezado";  panelEncabezado.TabIndex = 0;
            lblTitulo.Name = "lblTitulo";  lblTitulo.TabIndex = 0;
            lblDescripcion.Name = "lblDescripcion";  lblDescripcion.TabIndex = 1;
            btnVolver.Name = "btnVolver"; btnVolver.TabIndex = 2;
             barraAcciones.Name = "barraAcciones";  barraAcciones.TabIndex = 1;
             asignar.BackColor = Color.FromArgb(5, 150, 105); asignar.FlatAppearance.BorderSize = 0; asignar.FlatStyle = FlatStyle.Flat; asignar.ForeColor = Color.White;   asignar.Margin = new Padding(4, 0, 4, 0); asignar.Name = "asignar"; asignar.Padding = new Padding(12, 0, 12, 0);  asignar.TabIndex = 0; asignar.Text = "Asignar"; asignar.UseVisualStyleBackColor = false;
             cambiar.BackColor = Color.FromArgb(226, 232, 240); cambiar.FlatAppearance.BorderSize = 0; cambiar.FlatStyle = FlatStyle.Flat; cambiar.ForeColor = Color.FromArgb(30, 41, 59);   cambiar.Margin = new Padding(4, 0, 4, 0); cambiar.Name = "cambiar"; cambiar.Padding = new Padding(12, 0, 12, 0);  cambiar.TabIndex = 1; cambiar.Text = "Cambiar"; cambiar.UseVisualStyleBackColor = false;
             consultar.BackColor = Color.FromArgb(226, 232, 240); consultar.FlatAppearance.BorderSize = 0; consultar.FlatStyle = FlatStyle.Flat; consultar.ForeColor = Color.FromArgb(30, 41, 59);   consultar.Margin = new Padding(4, 0, 4, 0); consultar.Name = "consultar"; consultar.Padding = new Padding(12, 0, 12, 0);  consultar.TabIndex = 2; consultar.Text = "Consultar"; consultar.UseVisualStyleBackColor = false;
             darDeBaja.BackColor = Color.FromArgb(254, 242, 242); darDeBaja.FlatAppearance.BorderSize = 0; darDeBaja.FlatStyle = FlatStyle.Flat; darDeBaja.ForeColor = Color.FromArgb(185, 28, 28);   darDeBaja.Margin = new Padding(4, 0, 4, 0); darDeBaja.Name = "darDeBaja"; darDeBaja.Padding = new Padding(12, 0, 12, 0);  darDeBaja.TabIndex = 3; darDeBaja.Text = "Dar de baja"; darDeBaja.UseVisualStyleBackColor = false;
             lblEstado.Name = "lblEstado";  lblEstado.TabIndex = 3;
             panelContenido.Name = "panelContenido";  panelContenido.TabIndex = 2;
             panelFormulario.Name = "panelFormulario";  panelFormulario.TabIndex = 0;
             contenedorFormulario.Name = "contenedorFormulario";    contenedorFormulario.TabIndex = 0;
              lblMembresia.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold); lblMembresia.ForeColor = Color.FromArgb(51, 65, 85); lblMembresia.Name = "lblMembresia";  lblMembresia.TabIndex = 0; lblMembresia.Text = "Membresia:";
            membresia.Margin = new Padding(0, 4, 8, 4); membresia.Name = "membresia";  membresia.TabIndex = 1;
              lblEntrenador.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold); lblEntrenador.ForeColor = Color.FromArgb(51, 65, 85); lblEntrenador.Name = "lblEntrenador";  lblEntrenador.TabIndex = 2; lblEntrenador.Text = "Entrenador:";
            entrenador.Margin = new Padding(0, 4, 0, 4); entrenador.Name = "entrenador";  entrenador.TabIndex = 3;
             tabla.Name = "tabla";  tabla.TabIndex = 1;
            colId.FillWeight = 60; colMembresia.FillWeight = 250; colMembresia.MinimumWidth = 90; colEntrenador.FillWeight = 500; colEntrenador.MinimumWidth = 90; colEstado.FillWeight = 250; colEstado.MinimumWidth = 90;
            Controls.Add(panelContenido); Controls.Add(lblEstado); Controls.Add(barraAcciones); Controls.Add(panelEncabezado); AutoScaleMode = AutoScaleMode.Font;  BackColor = Color.FromArgb(241, 245, 249); ClientSize = new Size(1100, 680); Font = new Font("Segoe UI", 9.5F); MinimumSize = new Size(900, 560); this.Name = "GestionAsignacionesFormulario"; StartPosition = FormStartPosition.CenterParent; Text = "SysGym | Asignar entrenador";
            // Encabezado: titulo y descripcion a la izquierda, accion de regreso a la derecha.

            this.panelEncabezado.Padding = new Padding(22, 8, 22, 8);

            this.lblTitulo.Margin = new Padding(0);
            this.lblTitulo.TextAlign = ContentAlignment.BottomLeft;

            this.lblDescripcion.Margin = new Padding(0);
            this.lblDescripcion.TextAlign = ContentAlignment.TopLeft;

            this.btnVolver.Margin = new Padding(16, 0, 0, 0);
            // Barra de acciones: los controles se reacomodan cuando el ancho disminuye.

            this.barraAcciones.Padding = new Padding(16, 8, 16, 8);

            this.asignar.MinimumSize = new Size(120, 34);
            this.asignar.Margin = new Padding(0, 0, 8, 0);
            this.asignar.Padding = new Padding(12, 0, 12, 0);

            this.cambiar.MinimumSize = new Size(120, 34);
            this.cambiar.Margin = new Padding(0, 0, 8, 0);
            this.cambiar.Padding = new Padding(12, 0, 12, 0);

            this.consultar.MinimumSize = new Size(120, 34);
            this.consultar.Margin = new Padding(0, 0, 8, 0);
            this.consultar.Padding = new Padding(12, 0, 12, 0);

            this.darDeBaja.MinimumSize = new Size(120, 34);
            this.darDeBaja.Margin = new Padding(0, 0, 8, 0);
            this.darDeBaja.Padding = new Padding(12, 0, 12, 0);
            // Barra de estado inferior.

            this.lblEstado.Padding = new Padding(18, 0, 12, 0);
            this.lblEstado.TextAlign = ContentAlignment.MiddleLeft;
            // Area de trabajo: la grilla ocupa el alto disponible y el formulario queda debajo.

            this.panelContenido.Padding = new Padding(16);

            this.tabla.Margin = new Padding(0, 0, 0, 16);
            this.tabla.RowTemplate.Height = 30;

            this.panelFormulario.Margin = new Padding(0);
            this.panelFormulario.Padding = new Padding(16, 14, 16, 14);

            this.lblMembresia.Margin = new Padding(0, 0, 8, 8);
            this.lblMembresia.TextAlign = ContentAlignment.MiddleLeft;

            this.membresia.Margin = new Padding(0, 3, 16, 8);

            this.lblEntrenador.Margin = new Padding(0, 0, 8, 8);
            this.lblEntrenador.TextAlign = ContentAlignment.MiddleLeft;

            this.entrenador.Margin = new Padding(0, 3, 16, 8);

            this.AutoScroll = false;
            this.panelEncabezado.AutoSize = false;
            this.panelEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEncabezado.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.panelEncabezado.Location = new System.Drawing.Point(0, 0);
            this.panelEncabezado.Size = new System.Drawing.Size(1100, 84);
            this.panelEncabezado.AutoScroll = false;
            this.lblTitulo.AutoSize = false;
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.None;
            this.lblTitulo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.lblTitulo.Location = new System.Drawing.Point(22, 8);
            this.lblTitulo.Size = new System.Drawing.Size(890, 36);
            this.lblDescripcion.AutoSize = false;
            this.lblDescripcion.Dock = System.Windows.Forms.DockStyle.None;
            this.lblDescripcion.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
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
            this.panelFormulario.AutoSize = false;
            this.panelFormulario.Dock = System.Windows.Forms.DockStyle.None;
            this.panelFormulario.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.panelFormulario.Location = new System.Drawing.Point(16, 388);
            this.panelFormulario.Size = new System.Drawing.Size(1068, 110);
            this.panelFormulario.AutoScroll = false;
            this.contenedorFormulario.AutoSize = false;
            this.contenedorFormulario.Dock = System.Windows.Forms.DockStyle.None;
            this.contenedorFormulario.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.contenedorFormulario.Location = new System.Drawing.Point(16, 14);
            this.contenedorFormulario.Size = new System.Drawing.Size(1034, 80);
            this.contenedorFormulario.AutoScroll = false;
            this.lblMembresia.AutoSize = false;
            this.lblMembresia.Dock = System.Windows.Forms.DockStyle.None;
            this.lblMembresia.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblMembresia.Location = new System.Drawing.Point(0, 0);
            this.lblMembresia.Size = new System.Drawing.Size(102, 30);
            this.lblEntrenador.AutoSize = false;
            this.lblEntrenador.Dock = System.Windows.Forms.DockStyle.None;
            this.lblEntrenador.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblEntrenador.Location = new System.Drawing.Point(0, 38);
            this.lblEntrenador.Size = new System.Drawing.Size(102, 30);
            this.tabla.AutoSize = false;
            this.tabla.Dock = System.Windows.Forms.DockStyle.None;
            this.tabla.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.tabla.Location = new System.Drawing.Point(16, 16);
            this.tabla.Size = new System.Drawing.Size(1068, 356);
            this.tabla.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tabla.ReadOnly = true;
            this.tabla.AllowUserToAddRows = false;
            this.tabla.AllowUserToDeleteRows = false;
            this.tabla.AllowUserToResizeRows = false;
            this.tabla.RowHeadersVisible = false;
            this.tabla.MultiSelect = false;
            this.tabla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tabla.ColumnHeadersHeight = 46;
            this.membresia.AutoSize = false;
            this.membresia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.membresia.FormattingEnabled = true;
            this.membresia.Dock = System.Windows.Forms.DockStyle.None;
            this.membresia.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.membresia.Location = new System.Drawing.Point(110, 4);
            this.membresia.Size = new System.Drawing.Size(908, 26);
            this.entrenador.AutoSize = false;
            this.entrenador.Dock = System.Windows.Forms.DockStyle.None;
            this.entrenador.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.entrenador.Location = new System.Drawing.Point(110, 42);
            this.entrenador.Size = new System.Drawing.Size(908, 26);
            this.asignar.AutoSize = false;
            this.asignar.Dock = System.Windows.Forms.DockStyle.None;
            this.asignar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.asignar.Location = new System.Drawing.Point(16, 8);
            this.asignar.Size = new System.Drawing.Size(120, 34);
            this.cambiar.AutoSize = false;
            this.cambiar.Dock = System.Windows.Forms.DockStyle.None;
            this.cambiar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.cambiar.Location = new System.Drawing.Point(144, 8);
            this.cambiar.Size = new System.Drawing.Size(120, 34);
            this.consultar.AutoSize = false;
            this.consultar.Dock = System.Windows.Forms.DockStyle.None;
            this.consultar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.consultar.Location = new System.Drawing.Point(272, 8);
            this.consultar.Size = new System.Drawing.Size(120, 34);
            this.darDeBaja.AutoSize = false;
            this.darDeBaja.Dock = System.Windows.Forms.DockStyle.None;
            this.darDeBaja.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.darDeBaja.Location = new System.Drawing.Point(400, 8);
            this.darDeBaja.Size = new System.Drawing.Size(120, 34);
            panelEncabezado.ResumeLayout(false); panelEncabezado.PerformLayout(); barraAcciones.ResumeLayout(false); barraAcciones.PerformLayout(); panelContenido.ResumeLayout(false); panelFormulario.ResumeLayout(false); contenedorFormulario.ResumeLayout(false); contenedorFormulario.PerformLayout(); ((ISupportInitialize)(tabla)).EndInit(); ResumeLayout(false);

            this.Load += new System.EventHandler(this.GestionAsignacionesFormulario_Load);
            this.tabla.SelectionChanged += new System.EventHandler(this.tabla_SelectionChanged);
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            this.asignar.Click += new System.EventHandler(this.asignar_Click);
            this.cambiar.Click += new System.EventHandler(this.cambiar_Click);
            this.consultar.Click += new System.EventHandler(this.consultar_Click);
            this.darDeBaja.Click += new System.EventHandler(this.darDeBaja_Click);
                }

    }
}
