using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Recepcionista
{
    partial class GestionAsignacionesFormulario
    {
        private IContainer components;
        private Panel panelEncabezado; private Label lblTitulo; private Label lblDescripcion; private Button btnVolver; private Panel barraAcciones; private Label lblEstado; private Panel panelContenido; private Panel panelFormulario; private Panel contenedorFormulario; private Label lblMembresia; private Label lblEntrenador; private DataGridView tabla; private DataGridViewTextBoxColumn colId; private DataGridViewTextBoxColumn colMembresia; private DataGridViewTextBoxColumn colEntrenador; private DataGridViewTextBoxColumn colEstado;
        private TextBox membresia; private ComboBox entrenador; private Button asignar; private Button cambiar; private Button consultar; private Button darDeBaja;
        protected override void Dispose(bool liberarRecursos) { if (liberarRecursos && components != null) components.Dispose(); base.Dispose(liberarRecursos); }

        private void InitializeComponent()
        {
            components = new Container(); panelEncabezado = new Panel(); lblTitulo = new Label(); lblDescripcion = new Label(); btnVolver = new Button(); barraAcciones = new Panel(); asignar = new Button(); cambiar = new Button(); consultar = new Button(); darDeBaja = new Button(); lblEstado = new Label(); panelContenido = new Panel(); panelFormulario = new Panel(); contenedorFormulario = new Panel(); lblMembresia = new Label(); membresia = new TextBox(); lblEntrenador = new Label(); entrenador = new ComboBox(); tabla = new DataGridView(); colId = new DataGridViewTextBoxColumn(); colMembresia = new DataGridViewTextBoxColumn(); colEntrenador = new DataGridViewTextBoxColumn(); colEstado = new DataGridViewTextBoxColumn(); panelEncabezado.SuspendLayout(); barraAcciones.SuspendLayout(); panelContenido.SuspendLayout(); panelFormulario.SuspendLayout(); contenedorFormulario.SuspendLayout(); ((ISupportInitialize)(tabla)).BeginInit(); SuspendLayout();
            panelEncabezado.BackColor = Color.FromArgb(5, 150, 105);   panelEncabezado.Controls.Add(lblDescripcion); panelEncabezado.Controls.Add(lblTitulo); panelEncabezado.Controls.Add(btnVolver);  lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold); lblTitulo.ForeColor = Color.White;  lblTitulo.Text = "Asignar entrenador";  lblDescripcion.ForeColor = Color.FromArgb(226, 232, 240);  lblDescripcion.Text = "Vinculacion de entrenadores con membresias";  btnVolver.BackColor = Color.White; btnVolver.FlatStyle = FlatStyle.Flat; btnVolver.FlatAppearance.BorderSize = 0; btnVolver.ForeColor = Color.FromArgb(5, 150, 105);   btnVolver.Text = "Volver"; btnVolver.UseVisualStyleBackColor = false;
            barraAcciones.BackColor = Color.White;   barraAcciones.Padding = new Padding(16, 8, 16, 8);  barraAcciones.Controls.Add(asignar); barraAcciones.Controls.Add(cambiar); barraAcciones.Controls.Add(consultar); barraAcciones.Controls.Add(darDeBaja);
             lblEstado.BackColor = Color.FromArgb(226, 232, 240);  lblEstado.ForeColor = Color.FromArgb(51, 65, 85);  lblEstado.Padding = new Padding(18, 8, 8, 0); lblEstado.Text = "Listo"; panelContenido.BackColor = Color.FromArgb(248, 250, 252);  panelContenido.Padding = new Padding(20); panelContenido.Controls.Add(tabla); panelContenido.Controls.Add(panelFormulario); panelFormulario.BackColor = Color.White; panelFormulario.BorderStyle = BorderStyle.FixedSingle;   panelFormulario.Padding = new Padding(12); panelFormulario.Controls.Add(contenedorFormulario);       contenedorFormulario.Controls.Add(lblMembresia); contenedorFormulario.Controls.Add(membresia); contenedorFormulario.Controls.Add(lblEntrenador); contenedorFormulario.Controls.Add(entrenador);  membresia.BorderStyle = BorderStyle.FixedSingle;  entrenador.DropDownStyle = ComboBoxStyle.DropDownList;
            tabla.AllowUserToAddRows = false; tabla.AllowUserToDeleteRows = false; tabla.AllowUserToResizeRows = false; tabla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; tabla.BackgroundColor = Color.White; tabla.BorderStyle = BorderStyle.None; tabla.ColumnHeadersHeight = 38;  tabla.MultiSelect = false; tabla.ReadOnly = true; tabla.RowHeadersVisible = false; tabla.SelectionMode = DataGridViewSelectionMode.FullRowSelect; tabla.Columns.AddRange(new DataGridViewColumn[] { colId, colMembresia, colEntrenador, colEstado }); colId.HeaderText = "Id"; colId.Name = "colId"; colId.Visible = false; colMembresia.HeaderText = "Membresia"; colMembresia.Name = "colMembresia"; colEntrenador.HeaderText = "Entrenador"; colEntrenador.Name = "colEntrenador"; colEstado.HeaderText = "Estado"; colEstado.Name = "colEstado";
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
            colId.Width = 60; colMembresia.Width = 250; colEntrenador.Width = 500; colEstado.Width = 250;
            Controls.Add(panelContenido); Controls.Add(lblEstado); Controls.Add(barraAcciones); Controls.Add(panelEncabezado); AutoScaleMode = AutoScaleMode.Font; this.AutoScroll = true; BackColor = Color.FromArgb(241, 245, 249); ClientSize = new Size(1100, 680); Font = new Font("Segoe UI", 9.5F); MinimumSize = new Size(760, 540); this.Name = "GestionAsignacionesFormulario"; StartPosition = FormStartPosition.CenterParent; Text = "SysGym | Asignar entrenador"; 
            this.panelEncabezado.AutoSize = false;
            this.panelEncabezado.Dock = System.Windows.Forms.DockStyle.None;
            this.panelEncabezado.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.panelEncabezado.Location = new System.Drawing.Point(0, 0);
            this.panelEncabezado.Size = new System.Drawing.Size(1100, 80);
            this.lblTitulo.AutoSize = false;
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.None;
            this.lblTitulo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblTitulo.Location = new System.Drawing.Point(22, 10);
            this.lblTitulo.Size = new System.Drawing.Size(234, 38);
            this.lblDescripcion.AutoSize = false;
            this.lblDescripcion.Dock = System.Windows.Forms.DockStyle.None;
            this.lblDescripcion.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblDescripcion.Location = new System.Drawing.Point(24, 47);
            this.lblDescripcion.Size = new System.Drawing.Size(266, 22);
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
            this.panelFormulario.AutoSize = false;
            this.panelFormulario.Dock = System.Windows.Forms.DockStyle.None;
            this.panelFormulario.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.panelFormulario.Location = new System.Drawing.Point(20, 20);
            this.panelFormulario.Size = new System.Drawing.Size(1060, 96);
            this.contenedorFormulario.AutoSize = false;
            this.contenedorFormulario.Dock = System.Windows.Forms.DockStyle.None;
            this.contenedorFormulario.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.contenedorFormulario.Location = new System.Drawing.Point(12, 12);
            this.contenedorFormulario.Size = new System.Drawing.Size(1034, 70);
            this.lblMembresia.AutoSize = false;
            this.lblMembresia.Dock = System.Windows.Forms.DockStyle.None;
            this.lblMembresia.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblMembresia.Location = new System.Drawing.Point(3, 24);
            this.lblMembresia.Size = new System.Drawing.Size(70, 21);
            this.lblEntrenador.AutoSize = false;
            this.lblEntrenador.Dock = System.Windows.Forms.DockStyle.None;
            this.lblEntrenador.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblEntrenador.Location = new System.Drawing.Point(398, 24);
            this.lblEntrenador.Size = new System.Drawing.Size(70, 21);
            this.tabla.AutoSize = false;
            this.tabla.Dock = System.Windows.Forms.DockStyle.None;
            this.tabla.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.tabla.Location = new System.Drawing.Point(20, 116);
            this.tabla.Size = new System.Drawing.Size(1060, 380);
            this.membresia.AutoSize = false;
            this.membresia.Dock = System.Windows.Forms.DockStyle.None;
            this.membresia.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.membresia.Location = new System.Drawing.Point(100, 4);
            this.membresia.Size = new System.Drawing.Size(287, 24);
            this.entrenador.AutoSize = false;
            this.entrenador.Dock = System.Windows.Forms.DockStyle.None;
            this.entrenador.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.entrenador.Location = new System.Drawing.Point(485, 4);
            this.entrenador.Size = new System.Drawing.Size(549, 25);
            this.asignar.AutoSize = false;
            this.asignar.Dock = System.Windows.Forms.DockStyle.None;
            this.asignar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.asignar.Location = new System.Drawing.Point(20, 8);
            this.asignar.Size = new System.Drawing.Size(88, 36);
            this.cambiar.AutoSize = false;
            this.cambiar.Dock = System.Windows.Forms.DockStyle.None;
            this.cambiar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.cambiar.Location = new System.Drawing.Point(116, 8);
            this.cambiar.Size = new System.Drawing.Size(91, 36);
            this.consultar.AutoSize = false;
            this.consultar.Dock = System.Windows.Forms.DockStyle.None;
            this.consultar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.consultar.Location = new System.Drawing.Point(215, 8);
            this.consultar.Size = new System.Drawing.Size(102, 36);
            this.darDeBaja.AutoSize = false;
            this.darDeBaja.Dock = System.Windows.Forms.DockStyle.None;
            this.darDeBaja.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.darDeBaja.Location = new System.Drawing.Point(325, 8);
            this.darDeBaja.Size = new System.Drawing.Size(111, 36);
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
