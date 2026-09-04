using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Recepcionista
{
    partial class GestionAsignacionesForm
    {
        private IContainer components;
        private Panel panelEncabezado; private Label lblTitulo; private Label lblDescripcion; private Button btnVolver; private FlowLayoutPanel barraAcciones; private Label lblEstado; private Panel panelContenido; private Panel panelFormulario; private TableLayoutPanel layoutFormulario; private Label lblMembresia; private Label lblEntrenador; private DataGridView tabla; private DataGridViewTextBoxColumn colId; private DataGridViewTextBoxColumn colMembresia; private DataGridViewTextBoxColumn colEntrenador; private DataGridViewTextBoxColumn colEstado;
        private TextBox membresia; private ComboBox entrenador; private Button asignar; private Button cambiar; private Button consultar; private Button darDeBaja;
        protected override void Dispose(bool disposing) { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            this.panelEncabezado = new System.Windows.Forms.Panel();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.btnVolver = new System.Windows.Forms.Button();
            this.barraAcciones = new System.Windows.Forms.FlowLayoutPanel();
            this.asignar = new System.Windows.Forms.Button();
            this.cambiar = new System.Windows.Forms.Button();
            this.consultar = new System.Windows.Forms.Button();
            this.darDeBaja = new System.Windows.Forms.Button();
            this.lblEstado = new System.Windows.Forms.Label();
            this.panelContenido = new System.Windows.Forms.Panel();
            this.tabla = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMembresia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEntrenador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelFormulario = new System.Windows.Forms.Panel();
            this.layoutFormulario = new System.Windows.Forms.TableLayoutPanel();
            this.lblMembresia = new System.Windows.Forms.Label();
            this.membresia = new System.Windows.Forms.TextBox();
            this.lblEntrenador = new System.Windows.Forms.Label();
            this.entrenador = new System.Windows.Forms.ComboBox();
            this.panelEncabezado.SuspendLayout();
            this.barraAcciones.SuspendLayout();
            this.panelContenido.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).BeginInit();
            this.panelFormulario.SuspendLayout();
            this.layoutFormulario.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEncabezado
            // 
            this.panelEncabezado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(150)))), ((int)(((byte)(105)))));
            this.panelEncabezado.Controls.Add(this.lblDescripcion);
            this.panelEncabezado.Controls.Add(this.lblTitulo);
            this.panelEncabezado.Controls.Add(this.btnVolver);
            this.panelEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEncabezado.Location = new System.Drawing.Point(0, 0);
            this.panelEncabezado.Name = "panelEncabezado";
            this.panelEncabezado.Size = new System.Drawing.Size(1100, 80);
            this.panelEncabezado.TabIndex = 0;
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.lblDescripcion.Location = new System.Drawing.Point(24, 47);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(274, 17);
            this.lblDescripcion.TabIndex = 1;
            this.lblDescripcion.Text = "Vinculacion de entrenadores con membresias";
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(22, 10);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(237, 32);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Asignar entrenador";
            // 
            // btnVolver
            // 
            this.btnVolver.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVolver.BackColor = System.Drawing.Color.White;
            this.btnVolver.FlatAppearance.BorderSize = 0;
            this.btnVolver.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVolver.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(150)))), ((int)(((byte)(105)))));
            this.btnVolver.Location = new System.Drawing.Point(930, 22);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(92, 34);
            this.btnVolver.TabIndex = 2;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = false;
            // 
            // barraAcciones
            // 
            this.barraAcciones.BackColor = System.Drawing.Color.White;
            this.barraAcciones.Controls.Add(this.asignar);
            this.barraAcciones.Controls.Add(this.cambiar);
            this.barraAcciones.Controls.Add(this.consultar);
            this.barraAcciones.Controls.Add(this.darDeBaja);
            this.barraAcciones.Dock = System.Windows.Forms.DockStyle.Top;
            this.barraAcciones.Location = new System.Drawing.Point(0, 80);
            this.barraAcciones.Name = "barraAcciones";
            this.barraAcciones.Padding = new System.Windows.Forms.Padding(16, 8, 16, 8);
            this.barraAcciones.Size = new System.Drawing.Size(1100, 52);
            this.barraAcciones.TabIndex = 1;
            this.barraAcciones.WrapContents = false;
            // 
            // asignar
            // 
            this.asignar.AutoSize = true;
            this.asignar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(150)))), ((int)(((byte)(105)))));
            this.asignar.FlatAppearance.BorderSize = 0;
            this.asignar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.asignar.ForeColor = System.Drawing.Color.White;
            this.asignar.Location = new System.Drawing.Point(20, 8);
            this.asignar.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.asignar.Name = "asignar";
            this.asignar.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.asignar.Size = new System.Drawing.Size(88, 36);
            this.asignar.TabIndex = 0;
            this.asignar.Text = "Asignar";
            this.asignar.UseVisualStyleBackColor = false;
            // 
            // cambiar
            // 
            this.cambiar.AutoSize = true;
            this.cambiar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.cambiar.FlatAppearance.BorderSize = 0;
            this.cambiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cambiar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.cambiar.Location = new System.Drawing.Point(116, 8);
            this.cambiar.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.cambiar.Name = "cambiar";
            this.cambiar.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.cambiar.Size = new System.Drawing.Size(91, 36);
            this.cambiar.TabIndex = 1;
            this.cambiar.Text = "Cambiar";
            this.cambiar.UseVisualStyleBackColor = false;
            // 
            // consultar
            // 
            this.consultar.AutoSize = true;
            this.consultar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.consultar.FlatAppearance.BorderSize = 0;
            this.consultar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.consultar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.consultar.Location = new System.Drawing.Point(215, 8);
            this.consultar.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.consultar.Name = "consultar";
            this.consultar.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.consultar.Size = new System.Drawing.Size(102, 36);
            this.consultar.TabIndex = 2;
            this.consultar.Text = "Consultar";
            this.consultar.UseVisualStyleBackColor = false;
            // 
            // darDeBaja
            // 
            this.darDeBaja.AutoSize = true;
            this.darDeBaja.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.darDeBaja.FlatAppearance.BorderSize = 0;
            this.darDeBaja.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.darDeBaja.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.darDeBaja.Location = new System.Drawing.Point(325, 8);
            this.darDeBaja.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.darDeBaja.Name = "darDeBaja";
            this.darDeBaja.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.darDeBaja.Size = new System.Drawing.Size(111, 36);
            this.darDeBaja.TabIndex = 3;
            this.darDeBaja.Text = "Dar de baja";
            this.darDeBaja.UseVisualStyleBackColor = false;
            // 
            // lblEstado
            // 
            this.lblEstado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.lblEstado.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblEstado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblEstado.Location = new System.Drawing.Point(0, 648);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Padding = new System.Windows.Forms.Padding(18, 8, 8, 0);
            this.lblEstado.Size = new System.Drawing.Size(1100, 32);
            this.lblEstado.TabIndex = 3;
            this.lblEstado.Text = "Listo";
            // 
            // panelContenido
            // 
            this.panelContenido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelContenido.Controls.Add(this.tabla);
            this.panelContenido.Controls.Add(this.panelFormulario);
            this.panelContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenido.Location = new System.Drawing.Point(0, 132);
            this.panelContenido.Name = "panelContenido";
            this.panelContenido.Padding = new System.Windows.Forms.Padding(20);
            this.panelContenido.Size = new System.Drawing.Size(1100, 516);
            this.panelContenido.TabIndex = 2;
            // 
            // tabla
            // 
            this.tabla.AllowUserToAddRows = false;
            this.tabla.AllowUserToDeleteRows = false;
            this.tabla.AllowUserToResizeRows = false;
            this.tabla.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tabla.BackgroundColor = System.Drawing.Color.White;
            this.tabla.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tabla.ColumnHeadersHeight = 38;
            this.tabla.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colMembresia,
            this.colEntrenador,
            this.colEstado});
            this.tabla.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabla.Location = new System.Drawing.Point(20, 79);
            this.tabla.MultiSelect = false;
            this.tabla.Name = "tabla";
            this.tabla.ReadOnly = true;
            this.tabla.RowHeadersVisible = false;
            this.tabla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tabla.Size = new System.Drawing.Size(1060, 417);
            this.tabla.TabIndex = 1;
            // 
            // colId
            // 
            this.colId.HeaderText = "Id";
            this.colId.Name = "colId";
            this.colId.ReadOnly = true;
            this.colId.Visible = false;
            // 
            // colMembresia
            // 
            this.colMembresia.HeaderText = "Membresia";
            this.colMembresia.Name = "colMembresia";
            this.colMembresia.ReadOnly = true;
            // 
            // colEntrenador
            // 
            this.colEntrenador.HeaderText = "Entrenador";
            this.colEntrenador.Name = "colEntrenador";
            this.colEntrenador.ReadOnly = true;
            // 
            // colEstado
            // 
            this.colEstado.HeaderText = "Estado";
            this.colEstado.Name = "colEstado";
            this.colEstado.ReadOnly = true;
            // 
            // panelFormulario
            // 
            this.panelFormulario.BackColor = System.Drawing.Color.White;
            this.panelFormulario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelFormulario.Controls.Add(this.layoutFormulario);
            this.panelFormulario.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFormulario.Location = new System.Drawing.Point(20, 20);
            this.panelFormulario.Name = "panelFormulario";
            this.panelFormulario.Padding = new System.Windows.Forms.Padding(12);
            this.panelFormulario.Size = new System.Drawing.Size(1060, 59);
            this.panelFormulario.TabIndex = 0;
            // 
            // layoutFormulario
            // 
            this.layoutFormulario.ColumnCount = 4;
            this.layoutFormulario.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.layoutFormulario.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.layoutFormulario.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.layoutFormulario.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.layoutFormulario.Controls.Add(this.entrenador, 3, 0);
            this.layoutFormulario.Controls.Add(this.lblMembresia, 0, 0);
            this.layoutFormulario.Controls.Add(this.membresia, 1, 0);
            this.layoutFormulario.Controls.Add(this.lblEntrenador, 2, 0);
            this.layoutFormulario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutFormulario.Location = new System.Drawing.Point(12, 12);
            this.layoutFormulario.Name = "layoutFormulario";
            this.layoutFormulario.RowCount = 1;
            this.layoutFormulario.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutFormulario.Size = new System.Drawing.Size(1034, 33);
            this.layoutFormulario.TabIndex = 0;
            this.layoutFormulario.Paint += new System.Windows.Forms.PaintEventHandler(this.layoutFormulario_Paint);
            // 
            // lblMembresia
            // 
            this.lblMembresia.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblMembresia.AutoSize = true;
            this.lblMembresia.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblMembresia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblMembresia.Location = new System.Drawing.Point(3, 9);
            this.lblMembresia.Name = "lblMembresia";
            this.lblMembresia.Size = new System.Drawing.Size(69, 15);
            this.lblMembresia.TabIndex = 0;
            this.lblMembresia.Text = "Membresia:";
            // 
            // membresia
            // 
            this.membresia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.membresia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.membresia.Location = new System.Drawing.Point(100, 4);
            this.membresia.Margin = new System.Windows.Forms.Padding(0, 4, 8, 4);
            this.membresia.Name = "membresia";
            this.membresia.Size = new System.Drawing.Size(287, 24);
            this.membresia.TabIndex = 1;
            // 
            // lblEntrenador
            // 
            this.lblEntrenador.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblEntrenador.AutoSize = true;
            this.lblEntrenador.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblEntrenador.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblEntrenador.Location = new System.Drawing.Point(398, 9);
            this.lblEntrenador.Name = "lblEntrenador";
            this.lblEntrenador.Size = new System.Drawing.Size(68, 15);
            this.lblEntrenador.TabIndex = 2;
            this.lblEntrenador.Text = "Entrenador:";
            // 
            // entrenador
            // 
            this.entrenador.Dock = System.Windows.Forms.DockStyle.Fill;
            this.entrenador.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.entrenador.Location = new System.Drawing.Point(485, 4);
            this.entrenador.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.entrenador.Name = "entrenador";
            this.entrenador.Size = new System.Drawing.Size(549, 25);
            this.entrenador.TabIndex = 3;
            // 
            // GestionAsignacionesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1100, 680);
            this.Controls.Add(this.panelContenido);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.barraAcciones);
            this.Controls.Add(this.panelEncabezado);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.MinimumSize = new System.Drawing.Size(760, 540);
            this.Name = "GestionAsignacionesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SysGym | Asignar entrenador";
            this.panelEncabezado.ResumeLayout(false);
            this.panelEncabezado.PerformLayout();
            this.barraAcciones.ResumeLayout(false);
            this.barraAcciones.PerformLayout();
            this.panelContenido.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).EndInit();
            this.panelFormulario.ResumeLayout(false);
            this.layoutFormulario.ResumeLayout(false);
            this.layoutFormulario.PerformLayout();
            this.ResumeLayout(false);

        }

    }
}
