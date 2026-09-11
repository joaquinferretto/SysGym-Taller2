using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Compartido
{
    partial class GestionEjerciciosFormulario
    {
        private IContainer components;
        private Panel panelEncabezado;
        private Label lblTitulo;
        private Label lblDescripcion;
        private Button btnVolver;
        private Panel barraAcciones;
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
        private Panel contenedorFormulario;
        private Label lblNombre;
        private Label lblDescripcionEjercicio;
        private DataGridView tabla;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colNombre;
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
            panelEncabezado = new Panel();
            lblTitulo = new Label();
            lblDescripcion = new Label();
            btnVolver = new Button();
            barraAcciones = new Panel();
            lblEstadoFiltro = new Label();
            lblEstado = new Label();
            panelContenido = new Panel();
            panelFormulario = new Panel();
            contenedorFormulario = new Panel();
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
            contenedorFormulario.SuspendLayout();
            ((ISupportInitialize)(tabla)).BeginInit();
            SuspendLayout();

            panelEncabezado.BackColor = Color.FromArgb(79, 70, 229);

            panelEncabezado.Controls.Add(lblDescripcion);
            panelEncabezado.Controls.Add(lblTitulo);
            panelEncabezado.Controls.Add(btnVolver);
             lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold); lblTitulo.ForeColor = Color.White;  lblTitulo.Text = "Ejercicios";
             lblDescripcion.ForeColor = Color.FromArgb(226, 232, 240);  lblDescripcion.Text = "Catalogo de ejercicios disponibles para las rutinas";
             btnVolver.BackColor = Color.White; btnVolver.FlatStyle = FlatStyle.Flat; btnVolver.FlatAppearance.BorderSize = 0; btnVolver.ForeColor = Color.FromArgb(79, 70, 229);   btnVolver.Text = "Volver"; btnVolver.UseVisualStyleBackColor = false;

            barraAcciones.BackColor = Color.White;

            barraAcciones.Padding = new Padding(16, 8, 16, 8);

            barraAcciones.Controls.Add(guardar); barraAcciones.Controls.Add(darDeBaja); barraAcciones.Controls.Add(reactivar); barraAcciones.Controls.Add(actualizar);
             lblEstadoFiltro.Margin = new Padding(18, 9, 6, 0); lblEstadoFiltro.Text = "Estado:"; barraAcciones.Controls.Add(lblEstadoFiltro);
            filtroEstado.DropDownStyle = ComboBoxStyle.DropDownList; filtroEstado.Items.AddRange(new object[] { "Todos", "Activos", "Inactivos" }); filtroEstado.SelectedIndex = 0;  filtroEstado.Margin = new Padding(0, 2, 0, 0); barraAcciones.Controls.Add(filtroEstado);
             lblEstado.BackColor = Color.FromArgb(226, 232, 240);  lblEstado.ForeColor = Color.FromArgb(51, 65, 85);  lblEstado.Padding = new Padding(18, 8, 8, 0); lblEstado.Text = "Listo";

            panelContenido.BackColor = Color.FromArgb(248, 250, 252);  panelContenido.Padding = new Padding(20);
            panelContenido.Controls.Add(tabla); panelContenido.Controls.Add(panelFormulario);
            panelFormulario.BackColor = Color.White; panelFormulario.BorderStyle = BorderStyle.FixedSingle;   panelFormulario.Padding = new Padding(12);
            panelFormulario.Controls.Add(contenedorFormulario);

            contenedorFormulario.Controls.Add(lblNombre); contenedorFormulario.Controls.Add(nombre); contenedorFormulario.Controls.Add(lblDescripcionEjercicio); contenedorFormulario.Controls.Add(descripcion);
                tabla.BackgroundColor = Color.White; tabla.BorderStyle = BorderStyle.None;
            tabla.Columns.AddRange(new DataGridViewColumn[] { colId, colNombre, colDescripcion, colEstado });
            colId.HeaderText = "Id"; colId.Name = "colId"; colId.Visible = false; colNombre.HeaderText = "Nombre"; colNombre.Name = "colNombre"; colNombre.FillWeight = 32; colDescripcion.HeaderText = "Descripcion"; colDescripcion.Name = "colDescripcion"; colDescripcion.FillWeight = 53; colEstado.HeaderText = "Estado"; colEstado.Name = "colEstado"; colEstado.FillWeight = 15;

             panelEncabezado.Name = "panelEncabezado";  panelEncabezado.TabIndex = 0; lblTitulo.Name = "lblTitulo";  lblTitulo.TabIndex = 0; lblDescripcion.Name = "lblDescripcion";  lblDescripcion.TabIndex = 1; btnVolver.Name = "btnVolver"; btnVolver.TabIndex = 2;
             barraAcciones.Name = "barraAcciones";  barraAcciones.TabIndex = 1;
             guardar.BackColor = Color.FromArgb(79, 70, 229); guardar.FlatAppearance.BorderSize = 0; guardar.FlatStyle = FlatStyle.Flat; guardar.ForeColor = Color.White;  guardar.Margin = new Padding(4, 0, 4, 0); guardar.Name = "guardar"; guardar.Padding = new Padding(12, 0, 12, 0); guardar.Text = "Guardar"; guardar.UseVisualStyleBackColor = false;
             actualizar.BackColor = Color.FromArgb(226, 232, 240); actualizar.FlatAppearance.BorderSize = 0; actualizar.FlatStyle = FlatStyle.Flat; actualizar.ForeColor = Color.FromArgb(30, 41, 59);  actualizar.Margin = new Padding(4, 0, 4, 0); actualizar.Name = "actualizar"; actualizar.Padding = new Padding(12, 0, 12, 0); actualizar.Text = "Actualizar"; actualizar.UseVisualStyleBackColor = false;
             darDeBaja.BackColor = Color.FromArgb(254, 242, 242); darDeBaja.FlatAppearance.BorderSize = 0; darDeBaja.FlatStyle = FlatStyle.Flat; darDeBaja.ForeColor = Color.FromArgb(185, 28, 28);  darDeBaja.Margin = new Padding(4, 0, 4, 0); darDeBaja.Name = "darDeBaja"; darDeBaja.Padding = new Padding(12, 0, 12, 0); darDeBaja.Text = "Dar de baja"; darDeBaja.UseVisualStyleBackColor = false;
             reactivar.BackColor = Color.FromArgb(226, 232, 240); reactivar.FlatAppearance.BorderSize = 0; reactivar.FlatStyle = FlatStyle.Flat; reactivar.ForeColor = Color.FromArgb(30, 41, 59);  reactivar.Margin = new Padding(4, 0, 4, 0); reactivar.Name = "reactivar"; reactivar.Padding = new Padding(12, 0, 12, 0); reactivar.Text = "Reactivar"; reactivar.UseVisualStyleBackColor = false;
            lblEstadoFiltro.Name = "lblEstadoFiltro";  lblEstadoFiltro.TabIndex = 4; filtroEstado.Name = "filtroEstado"; filtroEstado.TabIndex = 5;
             lblEstado.Name = "lblEstado";  lblEstado.TabIndex = 3;  panelContenido.Name = "panelContenido";  panelContenido.TabIndex = 2;  panelFormulario.Name = "panelFormulario";  panelFormulario.TabIndex = 0;  contenedorFormulario.Name = "contenedorFormulario";   contenedorFormulario.TabIndex = 0;
              lblNombre.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold); lblNombre.ForeColor = Color.FromArgb(30, 41, 59); lblNombre.Name = "lblNombre"; lblNombre.Text = "Nombre:"; nombre.BorderStyle = BorderStyle.FixedSingle; nombre.Margin = new Padding(0, 4, 8, 4); nombre.Name = "nombre";    lblDescripcionEjercicio.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold); lblDescripcionEjercicio.ForeColor = Color.FromArgb(30, 41, 59); lblDescripcionEjercicio.Name = "lblDescripcionEjercicio"; lblDescripcionEjercicio.Text = "Descripcion:"; descripcion.BorderStyle = BorderStyle.FixedSingle; descripcion.Margin = new Padding(0, 4, 0, 4); descripcion.Name = "descripcion";
             tabla.Name = "tabla";  tabla.TabIndex = 1; colId.FillWeight = 60; colNombre.FillWeight = 300; colNombre.MinimumWidth = 90; colDescripcion.FillWeight = 550; colDescripcion.MinimumWidth = 90; colEstado.FillWeight = 150; colEstado.MinimumWidth = 90;
            Controls.Add(panelContenido); Controls.Add(lblEstado); Controls.Add(barraAcciones); Controls.Add(panelEncabezado);
            AutoScaleMode = AutoScaleMode.Font;  BackColor = Color.FromArgb(241, 245, 249); ClientSize = new Size(1100, 680); Font = new Font("Segoe UI", 9.5F); MinimumSize = new Size(900, 560); this.Name = "GestionEjerciciosFormulario"; StartPosition = FormStartPosition.CenterParent; Text = "SysGym | Ejercicios";

            // Encabezado: titulo y descripcion a la izquierda, accion de regreso a la derecha.

            this.panelEncabezado.Padding = new Padding(22, 8, 22, 8);

            this.lblTitulo.Margin = new Padding(0);
            this.lblTitulo.TextAlign = ContentAlignment.BottomLeft;

            this.lblDescripcion.Margin = new Padding(0);
            this.lblDescripcion.TextAlign = ContentAlignment.TopLeft;

            this.btnVolver.Margin = new Padding(16, 0, 0, 0);
            // Barra de acciones: los controles se reacomodan cuando el ancho disminuye.

            this.barraAcciones.Padding = new Padding(16, 8, 16, 8);

            this.guardar.MinimumSize = new Size(120, 34);
            this.guardar.Margin = new Padding(0, 0, 8, 0);
            this.guardar.Padding = new Padding(12, 0, 12, 0);

            this.actualizar.MinimumSize = new Size(120, 34);
            this.actualizar.Margin = new Padding(0, 0, 8, 0);
            this.actualizar.Padding = new Padding(12, 0, 12, 0);

            this.darDeBaja.MinimumSize = new Size(120, 34);
            this.darDeBaja.Margin = new Padding(0, 0, 8, 0);
            this.darDeBaja.Padding = new Padding(12, 0, 12, 0);

            this.reactivar.MinimumSize = new Size(120, 34);
            this.reactivar.Margin = new Padding(0, 0, 8, 0);
            this.reactivar.Padding = new Padding(12, 0, 12, 0);

            this.lblEstadoFiltro.Margin = new Padding(16, 9, 6, 0);

            this.filtroEstado.Margin = new Padding(0, 5, 0, 0);
            // Barra de estado inferior.

            this.lblEstado.Padding = new Padding(18, 0, 12, 0);
            this.lblEstado.TextAlign = ContentAlignment.MiddleLeft;
            // Area de trabajo: la grilla ocupa el alto disponible y el formulario queda debajo.

            this.panelContenido.Padding = new Padding(16);

            this.tabla.Margin = new Padding(0, 0, 0, 16);
            this.tabla.RowTemplate.Height = 30;

            this.panelFormulario.Margin = new Padding(0);
            this.panelFormulario.Padding = new Padding(16, 14, 16, 14);

            this.lblNombre.Margin = new Padding(0, 0, 8, 8);
            this.lblNombre.TextAlign = ContentAlignment.MiddleLeft;

            this.nombre.Margin = new Padding(0, 3, 16, 8);

            this.lblDescripcionEjercicio.Margin = new Padding(0, 0, 8, 8);
            this.lblDescripcionEjercicio.TextAlign = ContentAlignment.MiddleLeft;

            this.descripcion.Margin = new Padding(0, 3, 16, 8);

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
            this.lblEstadoFiltro.AutoSize = false;
            this.lblEstadoFiltro.Dock = System.Windows.Forms.DockStyle.None;
            this.lblEstadoFiltro.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblEstadoFiltro.Location = new System.Drawing.Point(528, 8);
            this.lblEstadoFiltro.Size = new System.Drawing.Size(80, 34);
            this.nombre.AutoSize = false;
            this.nombre.Dock = System.Windows.Forms.DockStyle.None;
            this.nombre.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.nombre.Location = new System.Drawing.Point(110, 4);
            this.nombre.Size = new System.Drawing.Size(395, 26);
            this.descripcion.AutoSize = false;
            this.descripcion.Dock = System.Windows.Forms.DockStyle.None;
            this.descripcion.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.descripcion.Location = new System.Drawing.Point(627, 4);
            this.descripcion.Size = new System.Drawing.Size(395, 26);
            this.filtroEstado.AutoSize = false;
            this.filtroEstado.Dock = System.Windows.Forms.DockStyle.None;
            this.filtroEstado.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.filtroEstado.Location = new System.Drawing.Point(616, 8);
            this.filtroEstado.Size = new System.Drawing.Size(160, 34);
            this.guardar.AutoSize = false;
            this.guardar.Dock = System.Windows.Forms.DockStyle.None;
            this.guardar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.guardar.Location = new System.Drawing.Point(16, 8);
            this.guardar.Size = new System.Drawing.Size(120, 34);
            this.actualizar.AutoSize = false;
            this.actualizar.Dock = System.Windows.Forms.DockStyle.None;
            this.actualizar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.actualizar.Location = new System.Drawing.Point(400, 8);
            this.actualizar.Size = new System.Drawing.Size(120, 34);
            this.darDeBaja.AutoSize = false;
            this.darDeBaja.Dock = System.Windows.Forms.DockStyle.None;
            this.darDeBaja.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.darDeBaja.Location = new System.Drawing.Point(144, 8);
            this.darDeBaja.Size = new System.Drawing.Size(120, 34);
            this.reactivar.AutoSize = false;
            this.reactivar.Dock = System.Windows.Forms.DockStyle.None;
            this.reactivar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.reactivar.Location = new System.Drawing.Point(272, 8);
            this.reactivar.Size = new System.Drawing.Size(120, 34);
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
            this.panelFormulario.Location = new System.Drawing.Point(16, 430);
            this.panelFormulario.Size = new System.Drawing.Size(1068, 68);
            this.panelFormulario.AutoScroll = false;
            this.contenedorFormulario.AutoSize = false;
            this.contenedorFormulario.Dock = System.Windows.Forms.DockStyle.None;
            this.contenedorFormulario.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.contenedorFormulario.Location = new System.Drawing.Point(16, 14);
            this.contenedorFormulario.Size = new System.Drawing.Size(1034, 38);
            this.contenedorFormulario.AutoScroll = false;
            this.lblNombre.AutoSize = false;
            this.lblNombre.Dock = System.Windows.Forms.DockStyle.None;
            this.lblNombre.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblNombre.Location = new System.Drawing.Point(0, 0);
            this.lblNombre.Size = new System.Drawing.Size(102, 30);
            this.lblDescripcionEjercicio.AutoSize = false;
            this.lblDescripcionEjercicio.Dock = System.Windows.Forms.DockStyle.None;
            this.lblDescripcionEjercicio.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblDescripcionEjercicio.Location = new System.Drawing.Point(517, 0);
            this.lblDescripcionEjercicio.Size = new System.Drawing.Size(102, 30);
            this.tabla.AutoSize = false;
            this.tabla.Dock = System.Windows.Forms.DockStyle.None;
            this.tabla.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.tabla.Location = new System.Drawing.Point(16, 16);
            this.tabla.Size = new System.Drawing.Size(1068, 398);
            this.tabla.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tabla.ReadOnly = true;
            this.tabla.AllowUserToAddRows = false;
            this.tabla.AllowUserToDeleteRows = false;
            this.tabla.AllowUserToResizeRows = false;
            this.tabla.RowHeadersVisible = false;
            this.tabla.MultiSelect = false;
            this.tabla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tabla.ColumnHeadersHeight = 46;
            panelEncabezado.ResumeLayout(false); panelEncabezado.PerformLayout(); barraAcciones.ResumeLayout(false); barraAcciones.PerformLayout(); panelContenido.ResumeLayout(false); panelFormulario.ResumeLayout(false); contenedorFormulario.ResumeLayout(false); contenedorFormulario.PerformLayout(); ((ISupportInitialize)(tabla)).EndInit(); ResumeLayout(false);

            this.Load += new System.EventHandler(this.GestionEjerciciosFormulario_Load);
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            this.filtroEstado.SelectedIndexChanged += new System.EventHandler(this.filtroEstado_SelectedIndexChanged);
            this.tabla.SelectionChanged += new System.EventHandler(this.tabla_SelectionChanged);
            this.guardar.Click += new System.EventHandler(this.guardar_Click);
            this.actualizar.Click += new System.EventHandler(this.actualizar_Click);
            this.darDeBaja.Click += new System.EventHandler(this.darDeBaja_Click);
            this.reactivar.Click += new System.EventHandler(this.reactivar_Click);
                }

    }
}
