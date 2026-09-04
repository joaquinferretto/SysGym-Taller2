using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Compartido
{
    partial class GestionAsistenciasForm
    {
        private IContainer components;
        private Panel panelEncabezado;
        private Label lblTitulo;
        private Label lblDescripcion;
        private Button btnVolver;
        private FlowLayoutPanel barraAcciones;
        private Label lblEstadoFiltro;
        private ComboBox socio;
        private DateTimePicker fecha;
        private Button darDeBaja;
        private Button reactivar;
        private ComboBox filtroEstado;
        private Label lblEstado;
        private Panel panelContenido;
        private Panel panelFormulario;
        private TableLayoutPanel layoutFormulario;
        private Label lblSocio;
        private Label lblFecha;
        private Button registrar;
        private Button actualizar;
        private DataGridView tabla;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colFecha;
        private DataGridViewTextBoxColumn colSocio;
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
            panelEncabezado = new Panel(); lblTitulo = new Label(); lblDescripcion = new Label(); btnVolver = new Button();
            barraAcciones = new FlowLayoutPanel(); lblEstadoFiltro = new Label(); lblEstado = new Label(); panelContenido = new Panel(); panelFormulario = new Panel(); layoutFormulario = new TableLayoutPanel();
            lblSocio = new Label(); socio = new ComboBox(); lblFecha = new Label(); fecha = new DateTimePicker(); registrar = new Button(); actualizar = new Button(); darDeBaja = new Button(); reactivar = new Button(); filtroEstado = new ComboBox();
            tabla = new DataGridView(); colId = new DataGridViewTextBoxColumn(); colFecha = new DataGridViewTextBoxColumn(); colSocio = new DataGridViewTextBoxColumn(); colDescripcion = new DataGridViewTextBoxColumn(); colEstado = new DataGridViewTextBoxColumn();
            panelEncabezado.SuspendLayout(); barraAcciones.SuspendLayout(); panelContenido.SuspendLayout(); panelFormulario.SuspendLayout(); layoutFormulario.SuspendLayout(); ((ISupportInitialize)(tabla)).BeginInit(); SuspendLayout();

            panelEncabezado.BackColor = Color.FromArgb(79, 70, 229); panelEncabezado.Dock = DockStyle.Top; panelEncabezado.Height = 80; panelEncabezado.Controls.Add(lblDescripcion); panelEncabezado.Controls.Add(lblTitulo); panelEncabezado.Controls.Add(btnVolver);
            lblTitulo.AutoSize = true; lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold); lblTitulo.ForeColor = Color.White; lblTitulo.Location = new Point(22, 10); lblTitulo.Text = "Asistencias";
            lblDescripcion.AutoSize = true; lblDescripcion.ForeColor = Color.FromArgb(226, 232, 240); lblDescripcion.Location = new Point(24, 47); lblDescripcion.Text = "Registro y consulta de ingresos al gimnasio";
            btnVolver.Anchor = AnchorStyles.Top | AnchorStyles.Right; btnVolver.BackColor = Color.White; btnVolver.FlatStyle = FlatStyle.Flat; btnVolver.FlatAppearance.BorderSize = 0; btnVolver.ForeColor = Color.FromArgb(79, 70, 229); btnVolver.Location = new Point(930, 22); btnVolver.Size = new Size(92, 34); btnVolver.Text = "Volver"; btnVolver.UseVisualStyleBackColor = false;

            barraAcciones.BackColor = Color.White; barraAcciones.Dock = DockStyle.Top; barraAcciones.Height = 52; barraAcciones.Padding = new Padding(16, 8, 16, 8); barraAcciones.WrapContents = false;
            barraAcciones.Controls.Add(registrar); barraAcciones.Controls.Add(darDeBaja); barraAcciones.Controls.Add(reactivar); barraAcciones.Controls.Add(actualizar); lblEstadoFiltro.AutoSize = true; lblEstadoFiltro.Margin = new Padding(18, 9, 6, 0); lblEstadoFiltro.Text = "Estado:"; barraAcciones.Controls.Add(lblEstadoFiltro);
            filtroEstado.DropDownStyle = ComboBoxStyle.DropDownList; filtroEstado.Items.AddRange(new object[] { "Todos", "Activos", "Inactivos" }); filtroEstado.SelectedIndex = 0; filtroEstado.Size = new Size(130, 26); filtroEstado.Margin = new Padding(0, 2, 0, 0); barraAcciones.Controls.Add(filtroEstado);
            lblEstado.AutoSize = false; lblEstado.BackColor = Color.FromArgb(226, 232, 240); lblEstado.Dock = DockStyle.Bottom; lblEstado.ForeColor = Color.FromArgb(51, 65, 85); lblEstado.Height = 32; lblEstado.Padding = new Padding(18, 8, 8, 0); lblEstado.Text = "Listo";

            panelContenido.BackColor = Color.FromArgb(248, 250, 252); panelContenido.Dock = DockStyle.Fill; panelContenido.Padding = new Padding(20); panelContenido.Controls.Add(tabla); panelContenido.Controls.Add(panelFormulario);
            panelFormulario.BackColor = Color.White; panelFormulario.BorderStyle = BorderStyle.FixedSingle; panelFormulario.Dock = DockStyle.Top; panelFormulario.Height = 94; panelFormulario.Padding = new Padding(12); panelFormulario.Controls.Add(layoutFormulario);
            layoutFormulario.ColumnCount = 4; layoutFormulario.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 56F)); layoutFormulario.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F)); layoutFormulario.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 48F)); layoutFormulario.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F)); layoutFormulario.Dock = DockStyle.Fill; layoutFormulario.RowCount = 1; layoutFormulario.Controls.Add(lblSocio, 0, 0); layoutFormulario.Controls.Add(socio, 1, 0); layoutFormulario.Controls.Add(lblFecha, 2, 0); layoutFormulario.Controls.Add(fecha, 3, 0);
            socio.Dock = DockStyle.Fill; socio.DropDownStyle = ComboBoxStyle.DropDownList; fecha.Dock = DockStyle.Fill; fecha.Format = DateTimePickerFormat.Custom; fecha.CustomFormat = "dd/MM/yyyy HH:mm";
            tabla.AllowUserToAddRows = false; tabla.AllowUserToDeleteRows = false; tabla.AllowUserToResizeRows = false; tabla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; tabla.BackgroundColor = Color.White; tabla.BorderStyle = BorderStyle.None; tabla.ColumnHeadersHeight = 38; tabla.Dock = DockStyle.Fill; tabla.MultiSelect = false; tabla.ReadOnly = true; tabla.RowHeadersVisible = false; tabla.SelectionMode = DataGridViewSelectionMode.FullRowSelect; tabla.Columns.AddRange(new DataGridViewColumn[] { colId, colFecha, colSocio, colDescripcion, colEstado });
            colId.HeaderText = "Id"; colId.Name = "colId"; colId.Visible = false; colFecha.HeaderText = "Fecha"; colFecha.Name = "colFecha"; colSocio.HeaderText = "Socio"; colSocio.Name = "colSocio"; colDescripcion.HeaderText = "Descripcion"; colDescripcion.Name = "colDescripcion"; colEstado.HeaderText = "Estado"; colEstado.Name = "colEstado";

            panelEncabezado.Location = new Point(0, 0); panelEncabezado.Name = "panelEncabezado"; panelEncabezado.Size = new Size(1100, 80); panelEncabezado.TabIndex = 0; lblTitulo.Name = "lblTitulo"; lblTitulo.Size = new Size(151, 32); lblTitulo.TabIndex = 0; lblDescripcion.Name = "lblDescripcion"; lblDescripcion.Size = new Size(283, 17); lblDescripcion.TabIndex = 1; btnVolver.Name = "btnVolver"; btnVolver.TabIndex = 2;
            barraAcciones.Location = new Point(0, 80); barraAcciones.Name = "barraAcciones"; barraAcciones.Size = new Size(1100, 52); barraAcciones.TabIndex = 1;
            registrar.AutoSize = true; registrar.BackColor = Color.FromArgb(79, 70, 229); registrar.FlatAppearance.BorderSize = 0; registrar.FlatStyle = FlatStyle.Flat; registrar.ForeColor = Color.White; registrar.Height = 36; registrar.Margin = new Padding(4, 0, 4, 0); registrar.Name = "registrar"; registrar.Padding = new Padding(12, 0, 12, 0); registrar.Text = "Registrar"; registrar.UseVisualStyleBackColor = false;
            darDeBaja.AutoSize = true; darDeBaja.BackColor = Color.FromArgb(254, 242, 242); darDeBaja.FlatAppearance.BorderSize = 0; darDeBaja.FlatStyle = FlatStyle.Flat; darDeBaja.ForeColor = Color.FromArgb(185, 28, 28); darDeBaja.Height = 36; darDeBaja.Margin = new Padding(4, 0, 4, 0); darDeBaja.Name = "darDeBaja"; darDeBaja.Padding = new Padding(12, 0, 12, 0); darDeBaja.Text = "Dar de baja"; darDeBaja.UseVisualStyleBackColor = false;
            reactivar.AutoSize = true; reactivar.BackColor = Color.FromArgb(226, 232, 240); reactivar.FlatAppearance.BorderSize = 0; reactivar.FlatStyle = FlatStyle.Flat; reactivar.ForeColor = Color.FromArgb(30, 41, 59); reactivar.Height = 36; reactivar.Margin = new Padding(4, 0, 4, 0); reactivar.Name = "reactivar"; reactivar.Padding = new Padding(12, 0, 12, 0); reactivar.Text = "Reactivar"; reactivar.UseVisualStyleBackColor = false;
            actualizar.AutoSize = true; actualizar.BackColor = Color.FromArgb(226, 232, 240); actualizar.FlatAppearance.BorderSize = 0; actualizar.FlatStyle = FlatStyle.Flat; actualizar.ForeColor = Color.FromArgb(30, 41, 59); actualizar.Height = 36; actualizar.Margin = new Padding(4, 0, 4, 0); actualizar.Name = "actualizar"; actualizar.Padding = new Padding(12, 0, 12, 0); actualizar.Text = "Actualizar"; actualizar.UseVisualStyleBackColor = false;
            lblEstadoFiltro.Name = "lblEstadoFiltro"; lblEstadoFiltro.Size = new Size(48, 17); lblEstadoFiltro.TabIndex = 4; filtroEstado.Name = "filtroEstado"; filtroEstado.TabIndex = 5;
            lblEstado.Location = new Point(0, 648); lblEstado.Name = "lblEstado"; lblEstado.Size = new Size(1100, 32); lblEstado.TabIndex = 3; panelContenido.Location = new Point(0, 132); panelContenido.Name = "panelContenido"; panelContenido.Size = new Size(1100, 516); panelContenido.TabIndex = 2; panelFormulario.Location = new Point(20, 20); panelFormulario.Name = "panelFormulario"; panelFormulario.Size = new Size(1060, 94); panelFormulario.TabIndex = 0; layoutFormulario.Location = new Point(12, 12); layoutFormulario.Name = "layoutFormulario"; layoutFormulario.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); layoutFormulario.Size = new Size(1034, 68); layoutFormulario.TabIndex = 0;
            lblSocio.Anchor = AnchorStyles.Left; lblSocio.AutoSize = true; lblSocio.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold); lblSocio.ForeColor = Color.FromArgb(30, 41, 59); lblSocio.Name = "lblSocio"; lblSocio.Text = "Socio:"; socio.Margin = new Padding(0, 4, 8, 4); socio.Name = "socio"; lblFecha.Anchor = AnchorStyles.Left; lblFecha.AutoSize = true; lblFecha.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold); lblFecha.ForeColor = Color.FromArgb(30, 41, 59); lblFecha.Name = "lblFecha"; lblFecha.Text = "Fecha:"; fecha.Margin = new Padding(0, 4, 0, 4); fecha.Name = "fecha";
            tabla.Location = new Point(20, 114); tabla.Name = "tabla"; tabla.Size = new Size(1060, 382); tabla.TabIndex = 1; colId.Width = 60; colFecha.Width = 190; colSocio.Width = 310; colDescripcion.Width = 350; colEstado.Width = 150;
            Controls.Add(panelContenido); Controls.Add(lblEstado); Controls.Add(barraAcciones); Controls.Add(panelEncabezado); AutoScaleMode = AutoScaleMode.Font; BackColor = Color.FromArgb(241, 245, 249); ClientSize = new Size(1100, 680); Font = new Font("Segoe UI", 9.5F); MinimumSize = new Size(760, 540); Name = "GestionAsistenciasForm"; StartPosition = FormStartPosition.CenterParent; Text = "SysGym | Asistencias";
            registrar.Click += Registrar; actualizar.Click += delegate { Cargar(); }; darDeBaja.Click += DarDeBaja; reactivar.Click += Reactivar;
            panelEncabezado.ResumeLayout(false); panelEncabezado.PerformLayout(); barraAcciones.ResumeLayout(false); barraAcciones.PerformLayout(); panelContenido.ResumeLayout(false); panelFormulario.ResumeLayout(false); layoutFormulario.ResumeLayout(false); layoutFormulario.PerformLayout(); ((ISupportInitialize)(tabla)).EndInit(); ResumeLayout(false);
        }

    }
}
