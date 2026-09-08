using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Compartido
{
    partial class GestionEjerciciosForm
    {
        private IContainer components;
        private Panel panelEncabezado;
        private Label lblTitulo;
        private Label lblDescripcion;
        private Button btnVolver;
        private FlowLayoutPanel barraAcciones;
        private Label lblEstadoFiltro;
        private TextBox nombre;
        private TextBox descripcion;
        private ComboBox filtroEstado;
        private Button guardar;
        private Button actualizar;
        private Button darDeBaja;
        private Button reactivar;
        private Label lblEstado;
        private Panel panelContenido;
        private Panel panelFormulario;
        private TableLayoutPanel layoutFormulario;
        private Label lblNombre;
        private Label lblDescripcionEjercicio;
        private DataGridView tabla;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colNombre;
        private DataGridViewTextBoxColumn colDescripcion;
        private DataGridViewTextBoxColumn colEstado;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new Container();
            panelEncabezado = new Panel();
            lblTitulo = new Label();
            lblDescripcion = new Label();
            btnVolver = new Button();
            barraAcciones = new FlowLayoutPanel();
            lblEstadoFiltro = new Label();
            lblEstado = new Label();
            panelContenido = new Panel();
            panelFormulario = new Panel();
            layoutFormulario = new TableLayoutPanel();
            lblNombre = new Label();
            nombre = new TextBox();
            lblDescripcionEjercicio = new Label();
            descripcion = new TextBox();
            tabla = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colNombre = new DataGridViewTextBoxColumn();
            colDescripcion = new DataGridViewTextBoxColumn();
            colEstado = new DataGridViewTextBoxColumn();
            filtroEstado = new ComboBox();
            guardar = new Button();
            actualizar = new Button();
            darDeBaja = new Button();
            reactivar = new Button();
            panelEncabezado.SuspendLayout();
            barraAcciones.SuspendLayout();
            panelContenido.SuspendLayout();
            panelFormulario.SuspendLayout();
            layoutFormulario.SuspendLayout();
            ((ISupportInitialize)(tabla)).BeginInit();
            SuspendLayout();

            panelEncabezado.BackColor = Color.FromArgb(79, 70, 229);
            panelEncabezado.Dock = DockStyle.Top;
            panelEncabezado.Height = 80;
            panelEncabezado.Controls.Add(lblDescripcion);
            panelEncabezado.Controls.Add(lblTitulo);
            panelEncabezado.Controls.Add(btnVolver);
            lblTitulo.AutoSize = true; lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold); lblTitulo.ForeColor = Color.White; lblTitulo.Location = new Point(22, 10); lblTitulo.Text = "Ejercicios";
            lblDescripcion.AutoSize = true; lblDescripcion.ForeColor = Color.FromArgb(226, 232, 240); lblDescripcion.Location = new Point(24, 47); lblDescripcion.Text = "Catalogo de ejercicios disponibles para las rutinas";
            btnVolver.Anchor = AnchorStyles.Top | AnchorStyles.Right; btnVolver.BackColor = Color.White; btnVolver.FlatStyle = FlatStyle.Flat; btnVolver.FlatAppearance.BorderSize = 0; btnVolver.ForeColor = Color.FromArgb(79, 70, 229); btnVolver.Location = new Point(930, 22); btnVolver.Size = new Size(92, 34); btnVolver.Text = "Volver"; btnVolver.UseVisualStyleBackColor = false;

            barraAcciones.BackColor = Color.White;
            barraAcciones.Dock = DockStyle.Top;
            barraAcciones.Height = 52;
            barraAcciones.Padding = new Padding(16, 8, 16, 8);
            barraAcciones.WrapContents = false;
            barraAcciones.Controls.Add(guardar); barraAcciones.Controls.Add(darDeBaja); barraAcciones.Controls.Add(reactivar); barraAcciones.Controls.Add(actualizar);
            lblEstadoFiltro.AutoSize = true; lblEstadoFiltro.Margin = new Padding(18, 9, 6, 0); lblEstadoFiltro.Text = "Estado:"; barraAcciones.Controls.Add(lblEstadoFiltro);
            filtroEstado.DropDownStyle = ComboBoxStyle.DropDownList; filtroEstado.Items.AddRange(new object[] { "Todos", "Activos", "Inactivos" }); filtroEstado.SelectedIndex = 0; filtroEstado.Size = new Size(130, 26); filtroEstado.Margin = new Padding(0, 2, 0, 0); barraAcciones.Controls.Add(filtroEstado);
            lblEstado.AutoSize = false; lblEstado.BackColor = Color.FromArgb(226, 232, 240); lblEstado.Dock = DockStyle.Bottom; lblEstado.ForeColor = Color.FromArgb(51, 65, 85); lblEstado.Height = 32; lblEstado.Padding = new Padding(18, 8, 8, 0); lblEstado.Text = "Listo";

            panelContenido.BackColor = Color.FromArgb(248, 250, 252); panelContenido.Dock = DockStyle.Fill; panelContenido.Padding = new Padding(20);
            panelContenido.Controls.Add(tabla); panelContenido.Controls.Add(panelFormulario);
            panelFormulario.BackColor = Color.White; panelFormulario.BorderStyle = BorderStyle.FixedSingle; panelFormulario.Dock = DockStyle.Top; panelFormulario.Height = 94; panelFormulario.Padding = new Padding(12);
            panelFormulario.Controls.Add(layoutFormulario);
            layoutFormulario.ColumnCount = 4; layoutFormulario.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F)); layoutFormulario.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38F)); layoutFormulario.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F)); layoutFormulario.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 62F)); layoutFormulario.Dock = DockStyle.Fill; layoutFormulario.RowCount = 1;
            layoutFormulario.Controls.Add(lblNombre, 0, 0); layoutFormulario.Controls.Add(nombre, 1, 0); layoutFormulario.Controls.Add(lblDescripcionEjercicio, 2, 0); layoutFormulario.Controls.Add(descripcion, 3, 0);
            tabla.AllowUserToAddRows = false; tabla.AllowUserToDeleteRows = false; tabla.AllowUserToResizeRows = false; tabla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; tabla.BackgroundColor = Color.White; tabla.BorderStyle = BorderStyle.None; tabla.ColumnHeadersHeight = 38; tabla.Dock = DockStyle.Fill; tabla.MultiSelect = false; tabla.ReadOnly = true; tabla.RowHeadersVisible = false; tabla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            tabla.Columns.AddRange(new DataGridViewColumn[] { colId, colNombre, colDescripcion, colEstado });
            colId.HeaderText = "Id"; colId.Name = "colId"; colId.Visible = false; colNombre.HeaderText = "Nombre"; colNombre.Name = "colNombre"; colNombre.FillWeight = 32; colDescripcion.HeaderText = "Descripcion"; colDescripcion.Name = "colDescripcion"; colDescripcion.FillWeight = 53; colEstado.HeaderText = "Estado"; colEstado.Name = "colEstado"; colEstado.FillWeight = 15;

            panelEncabezado.Location = new Point(0, 0); panelEncabezado.Name = "panelEncabezado"; panelEncabezado.Size = new Size(1100, 80); panelEncabezado.TabIndex = 0; lblTitulo.Name = "lblTitulo"; lblTitulo.Size = new Size(129, 32); lblTitulo.TabIndex = 0; lblDescripcion.Name = "lblDescripcion"; lblDescripcion.Size = new Size(325, 17); lblDescripcion.TabIndex = 1; btnVolver.Name = "btnVolver"; btnVolver.TabIndex = 2;
            barraAcciones.Location = new Point(0, 80); barraAcciones.Name = "barraAcciones"; barraAcciones.Size = new Size(1100, 52); barraAcciones.TabIndex = 1;
            guardar.AutoSize = true; guardar.BackColor = Color.FromArgb(79, 70, 229); guardar.FlatAppearance.BorderSize = 0; guardar.FlatStyle = FlatStyle.Flat; guardar.ForeColor = Color.White; guardar.Height = 36; guardar.Margin = new Padding(4, 0, 4, 0); guardar.Name = "guardar"; guardar.Padding = new Padding(12, 0, 12, 0); guardar.Text = "Guardar"; guardar.UseVisualStyleBackColor = false;
            actualizar.AutoSize = true; actualizar.BackColor = Color.FromArgb(226, 232, 240); actualizar.FlatAppearance.BorderSize = 0; actualizar.FlatStyle = FlatStyle.Flat; actualizar.ForeColor = Color.FromArgb(30, 41, 59); actualizar.Height = 36; actualizar.Margin = new Padding(4, 0, 4, 0); actualizar.Name = "actualizar"; actualizar.Padding = new Padding(12, 0, 12, 0); actualizar.Text = "Actualizar"; actualizar.UseVisualStyleBackColor = false;
            darDeBaja.AutoSize = true; darDeBaja.BackColor = Color.FromArgb(254, 242, 242); darDeBaja.FlatAppearance.BorderSize = 0; darDeBaja.FlatStyle = FlatStyle.Flat; darDeBaja.ForeColor = Color.FromArgb(185, 28, 28); darDeBaja.Height = 36; darDeBaja.Margin = new Padding(4, 0, 4, 0); darDeBaja.Name = "darDeBaja"; darDeBaja.Padding = new Padding(12, 0, 12, 0); darDeBaja.Text = "Dar de baja"; darDeBaja.UseVisualStyleBackColor = false;
            reactivar.AutoSize = true; reactivar.BackColor = Color.FromArgb(226, 232, 240); reactivar.FlatAppearance.BorderSize = 0; reactivar.FlatStyle = FlatStyle.Flat; reactivar.ForeColor = Color.FromArgb(30, 41, 59); reactivar.Height = 36; reactivar.Margin = new Padding(4, 0, 4, 0); reactivar.Name = "reactivar"; reactivar.Padding = new Padding(12, 0, 12, 0); reactivar.Text = "Reactivar"; reactivar.UseVisualStyleBackColor = false;
            lblEstadoFiltro.Name = "lblEstadoFiltro"; lblEstadoFiltro.Size = new Size(48, 17); lblEstadoFiltro.TabIndex = 4; filtroEstado.Name = "filtroEstado"; filtroEstado.TabIndex = 5;
            lblEstado.Location = new Point(0, 648); lblEstado.Name = "lblEstado"; lblEstado.Size = new Size(1100, 32); lblEstado.TabIndex = 3; panelContenido.Location = new Point(0, 132); panelContenido.Name = "panelContenido"; panelContenido.Size = new Size(1100, 516); panelContenido.TabIndex = 2; panelFormulario.Location = new Point(20, 20); panelFormulario.Name = "panelFormulario"; panelFormulario.Size = new Size(1060, 94); panelFormulario.TabIndex = 0; layoutFormulario.Location = new Point(12, 12); layoutFormulario.Name = "layoutFormulario"; layoutFormulario.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); layoutFormulario.Size = new Size(1034, 68); layoutFormulario.TabIndex = 0;
            lblNombre.Anchor = AnchorStyles.Left; lblNombre.AutoSize = true; lblNombre.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold); lblNombre.ForeColor = Color.FromArgb(30, 41, 59); lblNombre.Name = "lblNombre"; lblNombre.Text = "Nombre:"; nombre.BorderStyle = BorderStyle.FixedSingle; nombre.Margin = new Padding(0, 4, 8, 4); nombre.Name = "nombre"; nombre.Dock = DockStyle.Fill; lblDescripcionEjercicio.Anchor = AnchorStyles.Left; lblDescripcionEjercicio.AutoSize = true; lblDescripcionEjercicio.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold); lblDescripcionEjercicio.ForeColor = Color.FromArgb(30, 41, 59); lblDescripcionEjercicio.Name = "lblDescripcionEjercicio"; lblDescripcionEjercicio.Text = "Descripcion:"; descripcion.BorderStyle = BorderStyle.FixedSingle; descripcion.Margin = new Padding(0, 4, 0, 4); descripcion.Name = "descripcion"; descripcion.Dock = DockStyle.Fill;
            tabla.Location = new Point(20, 114); tabla.Name = "tabla"; tabla.Size = new Size(1060, 382); tabla.TabIndex = 1; colId.Width = 60; colNombre.Width = 300; colDescripcion.Width = 550; colEstado.Width = 150;
            Controls.Add(panelContenido); Controls.Add(lblEstado); Controls.Add(barraAcciones); Controls.Add(panelEncabezado);
            AutoScaleMode = AutoScaleMode.Font; BackColor = Color.FromArgb(241, 245, 249); ClientSize = new Size(1100, 680); Font = new Font("Segoe UI", 9.5F); MinimumSize = new Size(760, 540); Name = "GestionEjerciciosForm"; StartPosition = FormStartPosition.CenterParent; Text = "SysGym | Ejercicios";
            guardar.Click += Guardar; actualizar.Click += delegate { Cargar(); }; darDeBaja.Click += DarDeBaja; reactivar.Click += Reactivar;
            panelEncabezado.ResumeLayout(false); panelEncabezado.PerformLayout(); barraAcciones.ResumeLayout(false); barraAcciones.PerformLayout(); panelContenido.ResumeLayout(false); panelFormulario.ResumeLayout(false); layoutFormulario.ResumeLayout(false); layoutFormulario.PerformLayout(); ((ISupportInitialize)(tabla)).EndInit(); ResumeLayout(false);
        }

    }
}
