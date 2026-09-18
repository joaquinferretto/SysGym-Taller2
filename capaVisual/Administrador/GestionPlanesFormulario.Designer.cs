using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Administrador
{
    partial class GestionPlanesFormulario
    {
        private IContainer components;
        private Panel panelEncabezado;
        private Label lblTitulo;
        private Label lblDescripcion;
        private Button btnVolver;
        private Panel barraAcciones;
        private Label lblBuscar;
        private TextBox buscador;
        private Label lblFiltroEstado;
        private ComboBox filtroEstado;
        private Label lblEstado;
        private SplitContainer splitContenido;
        private Panel panelListado;
        private Label lblListado;
        private DataGridView tabla;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colNombre;
        private DataGridViewTextBoxColumn colPrecio;
        private DataGridViewTextBoxColumn colEstado;
        private Panel panelDetalle;
        private Panel contenedorDetalle;
        private Label lblFormulario;
        private TableLayoutPanel contenedorCampos;
        private Label lblNombre;
        private Label lblDescripcionPlan;
        private Label lblPrecio;
        private TextBox nombre;
        private TextBox descripcion;
        private TextBox precio;
        private Panel panelAcciones;
        private Button nuevo;
        private Button guardar;
        private Button actualizar;
        private Button darDeBaja;
        private Button reactivar;

        protected override void Dispose(bool liberarRecursos)
        {
            if (liberarRecursos && components != null)
            {
                components.Dispose();
            }

            base.Dispose(liberarRecursos);
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.panelEncabezado = new Panel();
            this.lblTitulo = new Label();
            this.lblDescripcion = new Label();
            this.btnVolver = new Button();
            this.barraAcciones = new Panel();
            this.lblBuscar = new Label();
            this.buscador = new TextBox();
            this.lblFiltroEstado = new Label();
            this.filtroEstado = new ComboBox();
            this.lblEstado = new Label();
            this.splitContenido = new SplitContainer();
            this.panelListado = new Panel();
            this.lblListado = new Label();
            this.tabla = new DataGridView();
            this.colId = new DataGridViewTextBoxColumn();
            this.colNombre = new DataGridViewTextBoxColumn();
            this.colPrecio = new DataGridViewTextBoxColumn();
            this.colEstado = new DataGridViewTextBoxColumn();
            this.panelDetalle = new Panel();
            this.contenedorDetalle = new Panel();
            this.lblFormulario = new Label();
            this.contenedorCampos = new TableLayoutPanel();
            this.lblNombre = new Label();
            this.lblDescripcionPlan = new Label();
            this.lblPrecio = new Label();
            this.nombre = new TextBox();
            this.descripcion = new TextBox();
            this.precio = new TextBox();
            this.panelAcciones = new Panel();
            this.nuevo = new Button();
            this.guardar = new Button();
            this.actualizar = new Button();
            this.darDeBaja = new Button();
            this.reactivar = new Button();
            ((ISupportInitialize)(this.splitContenido)).BeginInit();
            this.splitContenido.Panel1.SuspendLayout();
            this.splitContenido.Panel2.SuspendLayout();
            this.splitContenido.SuspendLayout();
            ((ISupportInitialize)(this.tabla)).BeginInit();
            this.panelEncabezado.SuspendLayout();
            this.barraAcciones.SuspendLayout();
            this.panelListado.SuspendLayout();
            this.panelDetalle.SuspendLayout();
            this.contenedorDetalle.SuspendLayout();
            this.contenedorCampos.SuspendLayout();
            this.panelAcciones.SuspendLayout();
            this.SuspendLayout();
            //
            // panelEncabezado
            //
            this.panelEncabezado.BackColor = Color.FromArgb(79, 70, 229);
            this.panelEncabezado.Controls.Add(this.lblTitulo);
            this.panelEncabezado.Controls.Add(this.lblDescripcion);
            this.panelEncabezado.Controls.Add(this.btnVolver);
            this.panelEncabezado.Dock = DockStyle.Top;
            this.panelEncabezado.Height = 56;
            this.panelEncabezado.Name = "panelEncabezado";
            this.panelEncabezado.TabIndex = 0;
            this.panelEncabezado.Visible = false;
            //
            // lblTitulo
            //
            this.lblTitulo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.lblTitulo.AutoSize = false;
            this.lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblTitulo.ForeColor = Color.White;
            this.lblTitulo.Location = new Point(22, 8);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new Size(880, 40);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Planes | Gestión de planes y precios";
            this.lblTitulo.TextAlign = ContentAlignment.MiddleLeft;
            //
            // lblDescripcion
            //
            this.lblDescripcion.ForeColor = Color.FromArgb(226, 232, 240);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.TabIndex = 1;
            this.lblDescripcion.Visible = false;
            //
            // btnVolver
            //
            this.btnVolver.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnVolver.BackColor = Color.White;
            this.btnVolver.FlatAppearance.BorderSize = 0;
            this.btnVolver.FlatStyle = FlatStyle.Flat;
            this.btnVolver.ForeColor = Color.FromArgb(79, 70, 229);
            this.btnVolver.Location = new Point(974, 10);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new Size(104, 36);
            this.btnVolver.TabIndex = 2;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = false;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            //
            // barraAcciones
            //
            this.barraAcciones.BackColor = Color.White;
            this.barraAcciones.Controls.Add(this.lblBuscar);
            this.barraAcciones.Controls.Add(this.buscador);
            this.barraAcciones.Controls.Add(this.lblFiltroEstado);
            this.barraAcciones.Controls.Add(this.filtroEstado);
            this.barraAcciones.Dock = DockStyle.Top;
            this.barraAcciones.Height = 54;
            this.barraAcciones.Name = "barraAcciones";
            this.barraAcciones.TabIndex = 1;
            //
            // lblBuscar
            //
            this.lblBuscar.AutoSize = false;
            this.lblBuscar.Location = new Point(16, 12);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new Size(52, 28);
            this.lblBuscar.TabIndex = 0;
            this.lblBuscar.Text = "Buscar:";
            this.lblBuscar.TextAlign = ContentAlignment.MiddleLeft;
            //
            // buscador
            //
            this.buscador.Location = new Point(72, 15);
            this.buscador.Name = "buscador";
            this.buscador.Size = new Size(250, 24);
            this.buscador.TabIndex = 0;
            this.buscador.TextChanged += new System.EventHandler(this.buscador_TextChanged);
            //
            // lblFiltroEstado
            //
            this.lblFiltroEstado.AutoSize = false;
            this.lblFiltroEstado.Location = new Point(350, 12);
            this.lblFiltroEstado.Name = "lblFiltroEstado";
            this.lblFiltroEstado.Size = new Size(46, 28);
            this.lblFiltroEstado.TabIndex = 1;
            this.lblFiltroEstado.Text = "Estado:";
            this.lblFiltroEstado.TextAlign = ContentAlignment.MiddleLeft;
            //
            // filtroEstado
            //
            this.filtroEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            this.filtroEstado.Items.AddRange(new object[] {
            "Todos",
            "Activos",
            "Inactivos"});
            this.filtroEstado.Location = new Point(404, 15);
            this.filtroEstado.Name = "filtroEstado";
            this.filtroEstado.Size = new Size(150, 25);
            this.filtroEstado.TabIndex = 1;
            this.filtroEstado.SelectedIndex = 0;
            this.filtroEstado.SelectedIndexChanged += new System.EventHandler(this.filtroEstado_SelectedIndexChanged);
            //
            // lblEstado
            //
            this.lblEstado.BackColor = Color.FromArgb(226, 232, 240);
            this.lblEstado.Dock = DockStyle.Bottom;
            this.lblEstado.Height = 28;
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Padding = new Padding(16, 0, 8, 0);
            this.lblEstado.TabIndex = 3;
            this.lblEstado.Text = "Listo";
            this.lblEstado.TextAlign = ContentAlignment.MiddleLeft;
            //
            // splitContenido
            //
            this.splitContenido.Dock = DockStyle.Fill;
            this.splitContenido.FixedPanel = FixedPanel.None;
            this.splitContenido.IsSplitterFixed = false;
            this.splitContenido.MinimumSize = new Size(900, 420);
            this.splitContenido.Name = "splitContenido";
            this.splitContenido.Panel1.BackColor = Color.FromArgb(248, 250, 252);
            this.splitContenido.Panel1.Controls.Add(this.panelListado);
            this.splitContenido.Panel1MinSize = 400;
            this.splitContenido.Panel1.Padding = new Padding(16);
            this.splitContenido.Panel2.BackColor = Color.FromArgb(248, 250, 252);
            this.splitContenido.Panel2.Controls.Add(this.panelDetalle);
            this.splitContenido.Panel2MinSize = 400;
            this.splitContenido.Panel2.Padding = new Padding(0, 16, 16, 16);
            this.splitContenido.Size = new Size(1068, 480);
            this.splitContenido.SplitterDistance = 500;
            this.splitContenido.SplitterWidth = 6;
            this.splitContenido.TabIndex = 4;
            //
            // panelListado
            //
            this.panelListado.Controls.Add(this.tabla);
            this.panelListado.Controls.Add(this.lblListado);
            this.panelListado.Dock = DockStyle.Fill;
            this.panelListado.Name = "panelListado";
            this.panelListado.TabIndex = 0;
            //
            // lblListado
            //
            this.lblListado.Dock = DockStyle.Top;
            this.lblListado.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            this.lblListado.ForeColor = Color.FromArgb(30, 41, 59);
            this.lblListado.Height = 34;
            this.lblListado.Name = "lblListado";
            this.lblListado.TabIndex = 0;
            this.lblListado.Text = "Planes";
            this.lblListado.TextAlign = ContentAlignment.MiddleLeft;
            //
            // tabla
            //
            this.tabla.AllowUserToAddRows = false;
            this.tabla.AllowUserToDeleteRows = false;
            this.tabla.AllowUserToResizeRows = false;
            this.tabla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.tabla.BackgroundColor = Color.White;
            this.tabla.BorderStyle = BorderStyle.FixedSingle;
            this.tabla.ColumnHeadersHeight = 34;
            this.tabla.Columns.AddRange(new DataGridViewColumn[] {
            this.colId,
            this.colNombre,
            this.colPrecio,
            this.colEstado});
            this.tabla.Dock = DockStyle.Fill;
            this.tabla.MultiSelect = false;
            this.tabla.Name = "tabla";
            this.tabla.ReadOnly = true;
            this.tabla.RowHeadersVisible = false;
            this.tabla.RowTemplate.Height = 30;
            this.tabla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.tabla.TabIndex = 1;
            this.tabla.SelectionChanged += new System.EventHandler(this.tabla_SelectionChanged);
            //
            // colId
            //
            this.colId.Name = "colId";
            this.colId.Visible = false;
            //
            // colNombre
            //
            this.colNombre.FillWeight = 48F;
            this.colNombre.HeaderText = "Nombre";
            this.colNombre.MinimumWidth = 130;
            this.colNombre.Name = "colNombre";
            //
            // colPrecio
            //
            this.colPrecio.FillWeight = 25F;
            this.colPrecio.HeaderText = "Precio";
            this.colPrecio.MinimumWidth = 90;
            this.colPrecio.Name = "colPrecio";
            //
            // colEstado
            //
            this.colEstado.FillWeight = 27F;
            this.colEstado.HeaderText = "Estado";
            this.colEstado.MinimumWidth = 90;
            this.colEstado.Name = "colEstado";
            //
            // panelDetalle
            //
            this.panelDetalle.Controls.Add(this.contenedorDetalle);
            this.panelDetalle.Dock = DockStyle.Fill;
            this.panelDetalle.Name = "panelDetalle";
            this.panelDetalle.TabIndex = 1;
            //
            // contenedorDetalle
            //
            this.contenedorDetalle.Controls.Add(this.panelAcciones);
            this.contenedorDetalle.Controls.Add(this.contenedorCampos);
            this.contenedorDetalle.Controls.Add(this.lblFormulario);
            this.contenedorDetalle.Dock = DockStyle.Fill;
            this.contenedorDetalle.Padding = new Padding(18);
            this.contenedorDetalle.Name = "contenedorDetalle";
            this.contenedorDetalle.TabIndex = 0;
            //
            // lblFormulario
            //
            this.lblFormulario.Dock = DockStyle.Top;
            this.lblFormulario.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            this.lblFormulario.ForeColor = Color.FromArgb(30, 41, 59);
            this.lblFormulario.Height = 38;
            this.lblFormulario.Name = "lblFormulario";
            this.lblFormulario.TabIndex = 0;
            this.lblFormulario.Text = "Nuevo plan";
            this.lblFormulario.TextAlign = ContentAlignment.MiddleLeft;
            //
            // contenedorCampos
            //
            this.contenedorCampos.ColumnCount = 2;
            this.contenedorCampos.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 116F));
            this.contenedorCampos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.contenedorCampos.Controls.Add(this.lblNombre, 0, 0);
            this.contenedorCampos.Controls.Add(this.nombre, 1, 0);
            this.contenedorCampos.Controls.Add(this.lblDescripcionPlan, 0, 1);
            this.contenedorCampos.Controls.Add(this.descripcion, 1, 1);
            this.contenedorCampos.Controls.Add(this.lblPrecio, 0, 2);
            this.contenedorCampos.Controls.Add(this.precio, 1, 2);
            this.contenedorCampos.Dock = DockStyle.Top;
            this.contenedorCampos.Height = 150;
            this.contenedorCampos.Name = "contenedorCampos";
            this.contenedorCampos.RowCount = 3;
            this.contenedorCampos.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
            this.contenedorCampos.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
            this.contenedorCampos.RowStyles.Add(new RowStyle(SizeType.Percent, 33.34F));
            this.contenedorCampos.TabIndex = 1;
            //
            // lblNombre
            //
            this.lblNombre.Dock = DockStyle.Fill;
            this.lblNombre.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            this.lblNombre.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblNombre.Margin = new Padding(0, 2, 8, 2);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.TabIndex = 0;
            this.lblNombre.Text = "Nombre:";
            this.lblNombre.TextAlign = ContentAlignment.MiddleLeft;
            //
            // nombre
            //
            this.nombre.Dock = DockStyle.Fill;
            this.nombre.Margin = new Padding(0, 3, 12, 3);
            this.nombre.Name = "nombre";
            this.nombre.TabIndex = 1;
            //
            // lblDescripcionPlan
            //
            this.lblDescripcionPlan.Dock = DockStyle.Fill;
            this.lblDescripcionPlan.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            this.lblDescripcionPlan.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblDescripcionPlan.Margin = new Padding(0, 2, 8, 2);
            this.lblDescripcionPlan.Name = "lblDescripcionPlan";
            this.lblDescripcionPlan.TabIndex = 2;
            this.lblDescripcionPlan.Text = "Descripcion:";
            this.lblDescripcionPlan.TextAlign = ContentAlignment.MiddleLeft;
            //
            // descripcion
            //
            this.descripcion.Dock = DockStyle.Fill;
            this.descripcion.Margin = new Padding(0, 3, 12, 3);
            this.descripcion.Name = "descripcion";
            this.descripcion.TabIndex = 3;
            //
            // lblPrecio
            //
            this.lblPrecio.Dock = DockStyle.Fill;
            this.lblPrecio.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            this.lblPrecio.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblPrecio.Margin = new Padding(0, 2, 8, 2);
            this.lblPrecio.Name = "lblPrecio";
            this.lblPrecio.TabIndex = 4;
            this.lblPrecio.Text = "Precio:";
            this.lblPrecio.TextAlign = ContentAlignment.MiddleLeft;
            //
            // precio
            //
            this.precio.Dock = DockStyle.Fill;
            this.precio.Margin = new Padding(0, 3, 12, 3);
            this.precio.Name = "precio";
            this.precio.TabIndex = 5;
            this.precio.KeyPress += new KeyPressEventHandler(this.precio_KeyPress);
            //
            // panelAcciones
            //
            this.panelAcciones.Controls.Add(this.nuevo);
            this.panelAcciones.Controls.Add(this.guardar);
            this.panelAcciones.Controls.Add(this.actualizar);
            this.panelAcciones.Controls.Add(this.darDeBaja);
            this.panelAcciones.Controls.Add(this.reactivar);
            this.panelAcciones.Dock = DockStyle.Top;
            this.panelAcciones.Height = 100;
            this.panelAcciones.Name = "panelAcciones";
            this.panelAcciones.TabIndex = 2;
            //
            // nuevo
            //
            this.nuevo.AutoSize = false;
            this.nuevo.BackColor = Color.FromArgb(79, 70, 229);
            this.nuevo.FlatAppearance.BorderSize = 0;
            this.nuevo.FlatStyle = FlatStyle.Flat;
            this.nuevo.ForeColor = Color.White;
            this.nuevo.Location = new Point(0, 8);
            this.nuevo.Name = "nuevo";
            this.nuevo.Size = new Size(112, 38);
            this.nuevo.TabIndex = 2;
            this.nuevo.Text = "+ Nuevo";
            this.nuevo.UseVisualStyleBackColor = false;
            this.nuevo.Click += new System.EventHandler(this.nuevo_Click);
            //
            // guardar
            //
            this.guardar.AutoSize = false;
            this.guardar.BackColor = Color.FromArgb(79, 70, 229);
            this.guardar.FlatAppearance.BorderSize = 0;
            this.guardar.FlatStyle = FlatStyle.Flat;
            this.guardar.ForeColor = Color.White;
            this.guardar.Location = new Point(120, 8);
            this.guardar.Name = "guardar";
            this.guardar.Size = new Size(112, 38);
            this.guardar.TabIndex = 3;
            this.guardar.Text = "Guardar";
            this.guardar.UseVisualStyleBackColor = false;
            this.guardar.Click += new System.EventHandler(this.guardar_Click);
            //
            // actualizar
            //
            this.actualizar.AutoSize = false;
            this.actualizar.BackColor = Color.FromArgb(226, 232, 240);
            this.actualizar.FlatAppearance.BorderSize = 0;
            this.actualizar.FlatStyle = FlatStyle.Flat;
            this.actualizar.ForeColor = Color.FromArgb(30, 41, 59);
            this.actualizar.Location = new Point(240, 8);
            this.actualizar.Name = "actualizar";
            this.actualizar.Size = new Size(112, 38);
            this.actualizar.TabIndex = 4;
            this.actualizar.Text = "Modificar";
            this.actualizar.UseVisualStyleBackColor = false;
            this.actualizar.Click += new System.EventHandler(this.actualizar_Click);
            //
            // darDeBaja
            //
            this.darDeBaja.AutoSize = false;
            this.darDeBaja.BackColor = Color.FromArgb(254, 242, 242);
            this.darDeBaja.FlatAppearance.BorderSize = 0;
            this.darDeBaja.FlatStyle = FlatStyle.Flat;
            this.darDeBaja.ForeColor = Color.FromArgb(185, 28, 28);
            this.darDeBaja.Location = new Point(0, 54);
            this.darDeBaja.Name = "darDeBaja";
            this.darDeBaja.Size = new Size(112, 38);
            this.darDeBaja.TabIndex = 5;
            this.darDeBaja.Text = "Dar de baja";
            this.darDeBaja.UseVisualStyleBackColor = false;
            this.darDeBaja.Click += new System.EventHandler(this.darDeBaja_Click);
            //
            // reactivar
            //
            this.reactivar.AutoSize = false;
            this.reactivar.BackColor = Color.FromArgb(226, 232, 240);
            this.reactivar.FlatAppearance.BorderSize = 0;
            this.reactivar.FlatStyle = FlatStyle.Flat;
            this.reactivar.ForeColor = Color.FromArgb(30, 41, 59);
            this.reactivar.Location = new Point(120, 54);
            this.reactivar.Name = "reactivar";
            this.reactivar.Size = new Size(112, 38);
            this.reactivar.TabIndex = 6;
            this.reactivar.Text = "Reactivar";
            this.reactivar.UseVisualStyleBackColor = false;
            this.reactivar.Click += new System.EventHandler(this.reactivar_Click);
            //
            // GestionPlanesFormulario
            //
            this.AutoScaleDimensions = new SizeF(7F, 17F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(241, 245, 249);
            this.ClientSize = new Size(1100, 680);
            this.Controls.Add(this.splitContenido);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.barraAcciones);
            this.Controls.Add(this.panelEncabezado);
            this.Font = new Font("Segoe UI", 9.5F);
            this.MinimumSize = new Size(900, 560);
            this.Name = "GestionPlanesFormulario";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "SysGym | Planes";
            this.Load += new System.EventHandler(this.GestionPlanesFormulario_Load);
            this.panelAcciones.ResumeLayout(false);
            this.contenedorCampos.ResumeLayout(false);
            this.contenedorDetalle.ResumeLayout(false);
            this.panelDetalle.ResumeLayout(false);
            this.panelListado.ResumeLayout(false);
            this.splitContenido.Panel2.ResumeLayout(false);
            this.splitContenido.Panel1.ResumeLayout(false);
            ((ISupportInitialize)(this.splitContenido)).EndInit();
            this.splitContenido.ResumeLayout(false);
            this.barraAcciones.ResumeLayout(false);
            this.panelEncabezado.ResumeLayout(false);
            ((ISupportInitialize)(this.tabla)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
