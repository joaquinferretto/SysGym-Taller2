using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Compartido
{
    partial class GestionAsistenciasFormulario
    {
        private IContainer components;
        private Panel panelEncabezado;
        private Label lblTitulo;
        private Label lblDescripcion;
        private Button btnVolver;
        private Panel barraAcciones;
        private Label lblEstadoFiltro;
        private ComboBox socio;
        private DateTimePicker fecha;
        private Button darDeBaja;
        private Button reactivar;
        private ComboBox filtroEstado;
        private Label lblEstado;
        private Panel panelContenido;
        private Panel panelFormulario;
        private Panel contenedorFormulario;
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

        protected override void Dispose(bool liberarRecursos)
        {
            if (liberarRecursos && components != null) components.Dispose();
            base.Dispose(liberarRecursos);
        }

        private void InitializeComponent()
        {
            components = new Container();
            panelEncabezado = new Panel(); lblTitulo = new Label(); lblDescripcion = new Label(); btnVolver = new Button();
            barraAcciones = new Panel(); lblEstadoFiltro = new Label(); lblEstado = new Label(); panelContenido = new Panel(); panelFormulario = new Panel(); contenedorFormulario = new Panel();
            lblSocio = new Label(); socio = new ComboBox(); lblFecha = new Label(); fecha = new DateTimePicker(); registrar = new Button(); actualizar = new Button(); darDeBaja = new Button(); reactivar = new Button(); filtroEstado = new ComboBox();
            tabla = new DataGridView(); colId = new DataGridViewTextBoxColumn(); colFecha = new DataGridViewTextBoxColumn(); colSocio = new DataGridViewTextBoxColumn(); colDescripcion = new DataGridViewTextBoxColumn(); colEstado = new DataGridViewTextBoxColumn();
            panelEncabezado.SuspendLayout(); barraAcciones.SuspendLayout(); panelContenido.SuspendLayout(); panelFormulario.SuspendLayout(); contenedorFormulario.SuspendLayout(); ((ISupportInitialize)(tabla)).BeginInit(); SuspendLayout();

            panelEncabezado.BackColor = Color.FromArgb(79, 70, 229);   panelEncabezado.Controls.Add(lblDescripcion); panelEncabezado.Controls.Add(lblTitulo); panelEncabezado.Controls.Add(btnVolver);
             lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold); lblTitulo.ForeColor = Color.White;  lblTitulo.Text = "Asistencias";
             lblDescripcion.ForeColor = Color.FromArgb(226, 232, 240);  lblDescripcion.Text = "Registro y consulta de ingresos al gimnasio";
             btnVolver.BackColor = Color.White; btnVolver.FlatStyle = FlatStyle.Flat; btnVolver.FlatAppearance.BorderSize = 0; btnVolver.ForeColor = Color.FromArgb(79, 70, 229);   btnVolver.Text = "Volver"; btnVolver.UseVisualStyleBackColor = false;

            barraAcciones.BackColor = Color.White;   barraAcciones.Padding = new Padding(16, 8, 16, 8); 
            barraAcciones.Controls.Add(registrar); barraAcciones.Controls.Add(darDeBaja); barraAcciones.Controls.Add(reactivar); barraAcciones.Controls.Add(actualizar);  lblEstadoFiltro.Margin = new Padding(18, 9, 6, 0); lblEstadoFiltro.Text = "Estado:"; barraAcciones.Controls.Add(lblEstadoFiltro);
            filtroEstado.DropDownStyle = ComboBoxStyle.DropDownList; filtroEstado.Items.AddRange(new object[] { "Todos", "Activos", "Inactivos" }); filtroEstado.SelectedIndex = 0;  filtroEstado.Margin = new Padding(0, 2, 0, 0); barraAcciones.Controls.Add(filtroEstado);
             lblEstado.BackColor = Color.FromArgb(226, 232, 240);  lblEstado.ForeColor = Color.FromArgb(51, 65, 85);  lblEstado.Padding = new Padding(18, 8, 8, 0); lblEstado.Text = "Listo";

            panelContenido.BackColor = Color.FromArgb(248, 250, 252);  panelContenido.Padding = new Padding(20); panelContenido.Controls.Add(tabla); panelContenido.Controls.Add(panelFormulario);
            panelFormulario.BackColor = Color.White; panelFormulario.BorderStyle = BorderStyle.FixedSingle;   panelFormulario.Padding = new Padding(12); panelFormulario.Controls.Add(contenedorFormulario);
                   contenedorFormulario.Controls.Add(lblSocio); contenedorFormulario.Controls.Add(socio); contenedorFormulario.Controls.Add(lblFecha); contenedorFormulario.Controls.Add(fecha);
             socio.DropDownStyle = ComboBoxStyle.DropDownList;  fecha.Format = DateTimePickerFormat.Custom; fecha.CustomFormat = "dd/MM/yyyy HH:mm";
            tabla.AllowUserToAddRows = false; tabla.AllowUserToDeleteRows = false; tabla.AllowUserToResizeRows = false; tabla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; tabla.BackgroundColor = Color.White; tabla.BorderStyle = BorderStyle.None; tabla.ColumnHeadersHeight = 38;  tabla.MultiSelect = false; tabla.ReadOnly = true; tabla.RowHeadersVisible = false; tabla.SelectionMode = DataGridViewSelectionMode.FullRowSelect; tabla.Columns.AddRange(new DataGridViewColumn[] { colId, colFecha, colSocio, colDescripcion, colEstado });
            colId.HeaderText = "Id"; colId.Name = "colId"; colId.Visible = false; colFecha.HeaderText = "Fecha"; colFecha.Name = "colFecha"; colSocio.HeaderText = "Socio"; colSocio.Name = "colSocio"; colDescripcion.HeaderText = "Descripcion"; colDescripcion.Name = "colDescripcion"; colEstado.HeaderText = "Estado"; colEstado.Name = "colEstado";

             panelEncabezado.Name = "panelEncabezado";  panelEncabezado.TabIndex = 0; lblTitulo.Name = "lblTitulo";  lblTitulo.TabIndex = 0; lblDescripcion.Name = "lblDescripcion";  lblDescripcion.TabIndex = 1; btnVolver.Name = "btnVolver"; btnVolver.TabIndex = 2;
             barraAcciones.Name = "barraAcciones";  barraAcciones.TabIndex = 1;
             registrar.BackColor = Color.FromArgb(79, 70, 229); registrar.FlatAppearance.BorderSize = 0; registrar.FlatStyle = FlatStyle.Flat; registrar.ForeColor = Color.White;  registrar.Margin = new Padding(4, 0, 4, 0); registrar.Name = "registrar"; registrar.Padding = new Padding(12, 0, 12, 0); registrar.Text = "Registrar"; registrar.UseVisualStyleBackColor = false;
             darDeBaja.BackColor = Color.FromArgb(254, 242, 242); darDeBaja.FlatAppearance.BorderSize = 0; darDeBaja.FlatStyle = FlatStyle.Flat; darDeBaja.ForeColor = Color.FromArgb(185, 28, 28);  darDeBaja.Margin = new Padding(4, 0, 4, 0); darDeBaja.Name = "darDeBaja"; darDeBaja.Padding = new Padding(12, 0, 12, 0); darDeBaja.Text = "Dar de baja"; darDeBaja.UseVisualStyleBackColor = false;
             reactivar.BackColor = Color.FromArgb(226, 232, 240); reactivar.FlatAppearance.BorderSize = 0; reactivar.FlatStyle = FlatStyle.Flat; reactivar.ForeColor = Color.FromArgb(30, 41, 59);  reactivar.Margin = new Padding(4, 0, 4, 0); reactivar.Name = "reactivar"; reactivar.Padding = new Padding(12, 0, 12, 0); reactivar.Text = "Reactivar"; reactivar.UseVisualStyleBackColor = false;
             actualizar.BackColor = Color.FromArgb(226, 232, 240); actualizar.FlatAppearance.BorderSize = 0; actualizar.FlatStyle = FlatStyle.Flat; actualizar.ForeColor = Color.FromArgb(30, 41, 59);  actualizar.Margin = new Padding(4, 0, 4, 0); actualizar.Name = "actualizar"; actualizar.Padding = new Padding(12, 0, 12, 0); actualizar.Text = "Actualizar"; actualizar.UseVisualStyleBackColor = false;
            lblEstadoFiltro.Name = "lblEstadoFiltro";  lblEstadoFiltro.TabIndex = 4; filtroEstado.Name = "filtroEstado"; filtroEstado.TabIndex = 5;
             lblEstado.Name = "lblEstado";  lblEstado.TabIndex = 3;  panelContenido.Name = "panelContenido";  panelContenido.TabIndex = 2;  panelFormulario.Name = "panelFormulario";  panelFormulario.TabIndex = 0;  contenedorFormulario.Name = "contenedorFormulario";   contenedorFormulario.TabIndex = 0;
              lblSocio.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold); lblSocio.ForeColor = Color.FromArgb(30, 41, 59); lblSocio.Name = "lblSocio"; lblSocio.Text = "Socio:"; socio.Margin = new Padding(0, 4, 8, 4); socio.Name = "socio";   lblFecha.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold); lblFecha.ForeColor = Color.FromArgb(30, 41, 59); lblFecha.Name = "lblFecha"; lblFecha.Text = "Fecha:"; fecha.Margin = new Padding(0, 4, 0, 4); fecha.Name = "fecha";
             tabla.Name = "tabla";  tabla.TabIndex = 1; colId.Width = 60; colFecha.Width = 190; colSocio.Width = 310; colDescripcion.Width = 350; colEstado.Width = 150;
            Controls.Add(panelContenido); Controls.Add(lblEstado); Controls.Add(barraAcciones); Controls.Add(panelEncabezado); AutoScaleMode = AutoScaleMode.Font; this.AutoScroll = true; BackColor = Color.FromArgb(241, 245, 249); ClientSize = new Size(1100, 680); Font = new Font("Segoe UI", 9.5F); MinimumSize = new Size(760, 540); this.Name = "GestionAsistenciasFormulario"; StartPosition = FormStartPosition.CenterParent; Text = "SysGym | Asistencias";

            this.panelEncabezado.AutoSize = false;
            this.panelEncabezado.Dock = System.Windows.Forms.DockStyle.None;
            this.panelEncabezado.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.panelEncabezado.Location = new System.Drawing.Point(0, 0);
            this.panelEncabezado.Size = new System.Drawing.Size(1100, 80);
            this.lblTitulo.AutoSize = false;
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.None;
            this.lblTitulo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblTitulo.Location = new System.Drawing.Point(22, 10);
            this.lblTitulo.Size = new System.Drawing.Size(136, 38);
            this.lblDescripcion.AutoSize = false;
            this.lblDescripcion.Dock = System.Windows.Forms.DockStyle.None;
            this.lblDescripcion.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblDescripcion.Location = new System.Drawing.Point(24, 47);
            this.lblDescripcion.Size = new System.Drawing.Size(253, 22);
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
            this.lblEstadoFiltro.AutoSize = false;
            this.lblEstadoFiltro.Dock = System.Windows.Forms.DockStyle.None;
            this.lblEstadoFiltro.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblEstadoFiltro.Location = new System.Drawing.Point(447, 17);
            this.lblEstadoFiltro.Size = new System.Drawing.Size(46, 22);
            this.socio.AutoSize = false;
            this.socio.Dock = System.Windows.Forms.DockStyle.None;
            this.socio.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.socio.Location = new System.Drawing.Point(56, 4);
            this.socio.Size = new System.Drawing.Size(410, 25);
            this.fecha.AutoSize = false;
            this.fecha.Dock = System.Windows.Forms.DockStyle.None;
            this.fecha.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.fecha.Location = new System.Drawing.Point(522, 4);
            this.fecha.Size = new System.Drawing.Size(512, 24);
            this.darDeBaja.AutoSize = false;
            this.darDeBaja.Dock = System.Windows.Forms.DockStyle.None;
            this.darDeBaja.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.darDeBaja.Location = new System.Drawing.Point(118, 8);
            this.darDeBaja.Size = new System.Drawing.Size(105, 36);
            this.reactivar.AutoSize = false;
            this.reactivar.Dock = System.Windows.Forms.DockStyle.None;
            this.reactivar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.reactivar.Location = new System.Drawing.Point(231, 8);
            this.reactivar.Size = new System.Drawing.Size(91, 36);
            this.filtroEstado.AutoSize = false;
            this.filtroEstado.Dock = System.Windows.Forms.DockStyle.None;
            this.filtroEstado.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.filtroEstado.Location = new System.Drawing.Point(499, 10);
            this.filtroEstado.Size = new System.Drawing.Size(130, 25);
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
            this.panelFormulario.Size = new System.Drawing.Size(1060, 94);
            this.contenedorFormulario.AutoSize = false;
            this.contenedorFormulario.Dock = System.Windows.Forms.DockStyle.None;
            this.contenedorFormulario.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.contenedorFormulario.Location = new System.Drawing.Point(12, 12);
            this.contenedorFormulario.Size = new System.Drawing.Size(1034, 68);
            this.lblSocio.AutoSize = false;
            this.lblSocio.Dock = System.Windows.Forms.DockStyle.None;
            this.lblSocio.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblSocio.Location = new System.Drawing.Point(3, 23);
            this.lblSocio.Size = new System.Drawing.Size(38, 21);
            this.lblFecha.AutoSize = false;
            this.lblFecha.Dock = System.Windows.Forms.DockStyle.None;
            this.lblFecha.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblFecha.Location = new System.Drawing.Point(477, 23);
            this.lblFecha.Size = new System.Drawing.Size(40, 21);
            this.registrar.AutoSize = false;
            this.registrar.Dock = System.Windows.Forms.DockStyle.None;
            this.registrar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.registrar.Location = new System.Drawing.Point(20, 8);
            this.registrar.Size = new System.Drawing.Size(90, 36);
            this.actualizar.AutoSize = false;
            this.actualizar.Dock = System.Windows.Forms.DockStyle.None;
            this.actualizar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.actualizar.Location = new System.Drawing.Point(330, 8);
            this.actualizar.Size = new System.Drawing.Size(95, 36);
            this.tabla.AutoSize = false;
            this.tabla.Dock = System.Windows.Forms.DockStyle.None;
            this.tabla.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.tabla.Location = new System.Drawing.Point(20, 114);
            this.tabla.Size = new System.Drawing.Size(1060, 382);
            panelEncabezado.ResumeLayout(false); panelEncabezado.PerformLayout(); barraAcciones.ResumeLayout(false); barraAcciones.PerformLayout(); panelContenido.ResumeLayout(false); panelFormulario.ResumeLayout(false); contenedorFormulario.ResumeLayout(false); contenedorFormulario.PerformLayout(); ((ISupportInitialize)(tabla)).EndInit(); ResumeLayout(false);

            this.Load += new System.EventHandler(this.GestionAsistenciasFormulario_Load);
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            this.filtroEstado.SelectedIndexChanged += new System.EventHandler(this.filtroEstado_SelectedIndexChanged);
            this.fecha.ValueChanged += new System.EventHandler(this.fecha_ValueChanged);
            this.tabla.SelectionChanged += new System.EventHandler(this.tabla_SelectionChanged);
            this.registrar.Click += new System.EventHandler(this.registrar_Click);
            this.actualizar.Click += new System.EventHandler(this.actualizar_Click);
            this.darDeBaja.Click += new System.EventHandler(this.darDeBaja_Click);
            this.reactivar.Click += new System.EventHandler(this.reactivar_Click);
                }

    }
}
