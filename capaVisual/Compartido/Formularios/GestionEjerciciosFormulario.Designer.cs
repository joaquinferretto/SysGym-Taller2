using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Compartido
{
    partial class GestionEjerciciosFormulario
    {
        private IContainer components;
        private ErrorProvider indicadorErrores;
        private Label lblBuscar;
        private TextBox buscador;
        private Label lblEstadoFiltro;
        private ComboBox filtroEstado;
        private Button nuevo;
        private Button actualizar;
        private Label lblEstado;
        private SplitContainer splitContenido;
        private Label lblListadoTitulo;
        private DataGridView tabla;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colNombre;
        private DataGridViewTextBoxColumn colDescripcion;
        private DataGridViewTextBoxColumn colEstado;
        private Label lblDetalleTitulo;
        private Label lblNombre;
        private TextBox nombre;
        private Label lblDescripcionEjercicio;
        private TextBox descripcion;
        private Label lblEstadoCampo;
        private Label lblEstadoValor;
        private FlowLayoutPanel galeriaImagenes;
        private Button agregarImagen;
        private Button quitarImagen;
        private Label lblImagenes;
        private Button guardar;
        private Button darDeBaja;
        private Button reactivar;
        private ContextMenuStrip menuEjercicios;
        private ToolStripMenuItem actualizarMenu;
        private ToolStripMenuItem darDeBajaMenu;
        private ToolStripMenuItem reactivarMenu;

        protected override void Dispose(bool liberarRecursos)
        {
            if (liberarRecursos && components != null) components.Dispose();
            base.Dispose(liberarRecursos);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.indicadorErrores = new System.Windows.Forms.ErrorProvider(this.components);
            this.nuevo = new System.Windows.Forms.Button();
            this.actualizar = new System.Windows.Forms.Button();
            this.filtroEstado = new System.Windows.Forms.ComboBox();
            this.lblEstadoFiltro = new System.Windows.Forms.Label();
            this.buscador = new System.Windows.Forms.TextBox();
            this.lblBuscar = new System.Windows.Forms.Label();
            this.lblEstado = new System.Windows.Forms.Label();
            this.splitContenido = new System.Windows.Forms.SplitContainer();
            this.lblListadoTitulo = new System.Windows.Forms.Label();
            this.tabla = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuEjercicios = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.actualizarMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.darDeBajaMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.reactivarMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.lblDetalleTitulo = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.nombre = new System.Windows.Forms.TextBox();
            this.lblDescripcionEjercicio = new System.Windows.Forms.Label();
            this.descripcion = new System.Windows.Forms.TextBox();
            this.lblEstadoCampo = new System.Windows.Forms.Label();
            this.lblEstadoValor = new System.Windows.Forms.Label();
            this.guardar = new System.Windows.Forms.Button();
            this.darDeBaja = new System.Windows.Forms.Button();
            this.reactivar = new System.Windows.Forms.Button();
            this.galeriaImagenes = new System.Windows.Forms.FlowLayoutPanel();
            this.agregarImagen = new System.Windows.Forms.Button();
            this.quitarImagen = new System.Windows.Forms.Button();
            this.lblImagenes = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.indicadorErrores)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContenido)).BeginInit();
            this.splitContenido.Panel1.SuspendLayout();
            this.splitContenido.Panel2.SuspendLayout();
            this.splitContenido.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).BeginInit();
            this.menuEjercicios.SuspendLayout();
            this.SuspendLayout();
            //
            // indicadorErrores
            //
            this.indicadorErrores.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.indicadorErrores.ContainerControl = this;
            //
            // nuevo
            //
            this.nuevo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nuevo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.nuevo.FlatAppearance.BorderSize = 0;
            this.nuevo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(77)))), ((int)(((byte)(88)))));
            this.nuevo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(96)))), ((int)(((byte)(109)))));
            this.nuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nuevo.ForeColor = System.Drawing.Color.White;
            this.nuevo.Location = new System.Drawing.Point(0, 568);
            this.nuevo.Name = "nuevo";
            this.nuevo.Size = new System.Drawing.Size(320, 36);
            this.nuevo.TabIndex = 3;
            this.nuevo.Text = "+ Nuevo ejercicio";
            this.nuevo.UseVisualStyleBackColor = false;
            this.nuevo.Click += new System.EventHandler(this.nuevo_Click);
            //
            // actualizar
            //
            this.actualizar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(75)))), ((int)(((byte)(138)))));
            this.actualizar.FlatAppearance.BorderSize = 0;
            this.actualizar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(48)))), ((int)(((byte)(89)))));
            this.actualizar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(61)))), ((int)(((byte)(112)))));
            this.actualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.actualizar.ForeColor = System.Drawing.Color.White;
            this.actualizar.Location = new System.Drawing.Point(3, 104);
            this.actualizar.Name = "actualizar";
            this.actualizar.Size = new System.Drawing.Size(150, 32);
            this.actualizar.TabIndex = 2;
            this.actualizar.Text = "Actualizar listado";
            this.actualizar.UseVisualStyleBackColor = false;
            this.actualizar.Click += new System.EventHandler(this.actualizar_Click);
            //
            // filtroEstado
            //
            this.filtroEstado.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filtroEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filtroEstado.Items.AddRange(new object[] {
            "Todos",
            "Activos",
            "Inactivos"});
            this.filtroEstado.Location = new System.Drawing.Point(67, 68);
            this.filtroEstado.Name = "filtroEstado";
            this.filtroEstado.Size = new System.Drawing.Size(247, 29);
            this.filtroEstado.TabIndex = 1;
            this.filtroEstado.SelectedIndexChanged += new System.EventHandler(this.filtroEstado_SelectedIndexChanged);
            //
            // lblEstadoFiltro
            //
            this.lblEstadoFiltro.Location = new System.Drawing.Point(3, 68);
            this.lblEstadoFiltro.Name = "lblEstadoFiltro";
            this.lblEstadoFiltro.Size = new System.Drawing.Size(60, 26);
            this.lblEstadoFiltro.TabIndex = 4;
            this.lblEstadoFiltro.Text = "Estado:";
            this.lblEstadoFiltro.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // buscador
            //
            this.buscador.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buscador.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.buscador.Location = new System.Drawing.Point(67, 36);
            this.buscador.Name = "buscador";
            this.buscador.Size = new System.Drawing.Size(247, 29);
            this.buscador.TabIndex = 0;
            this.buscador.TextChanged += new System.EventHandler(this.buscador_TextChanged);
            //
            // lblBuscar
            //
            this.lblBuscar.Location = new System.Drawing.Point(3, 36);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(60, 26);
            this.lblBuscar.TabIndex = 5;
            this.lblBuscar.Text = "Buscar:";
            this.lblBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblEstado
            //
            this.lblEstado.BackColor = System.Drawing.Color.Transparent;
            this.lblEstado.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblEstado.Location = new System.Drawing.Point(16, 636);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Padding = new System.Windows.Forms.Padding(16, 0, 8, 0);
            this.lblEstado.Size = new System.Drawing.Size(1068, 28);
            this.lblEstado.TabIndex = 1;
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // splitContenido
            //
            this.splitContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContenido.Location = new System.Drawing.Point(16, 16);
            this.splitContenido.MinimumSize = new System.Drawing.Size(862, 420);
            this.splitContenido.Name = "splitContenido";
            //
            // splitContenido.Panel1
            //
            this.splitContenido.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.splitContenido.Panel1.Controls.Add(this.lblListadoTitulo);
            this.splitContenido.Panel1.Controls.Add(this.lblBuscar);
            this.splitContenido.Panel1.Controls.Add(this.buscador);
            this.splitContenido.Panel1.Controls.Add(this.lblEstadoFiltro);
            this.splitContenido.Panel1.Controls.Add(this.filtroEstado);
            this.splitContenido.Panel1.Controls.Add(this.nuevo);
            this.splitContenido.Panel1.Controls.Add(this.actualizar);
            this.splitContenido.Panel1.Controls.Add(this.tabla);
            this.splitContenido.Panel1.Padding = new System.Windows.Forms.Padding(16);
            this.splitContenido.Panel1MinSize = 300;
            //
            // splitContenido.Panel2
            //
            this.splitContenido.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.splitContenido.Panel2.Controls.Add(this.lblDetalleTitulo);
            this.splitContenido.Panel2.Controls.Add(this.lblNombre);
            this.splitContenido.Panel2.Controls.Add(this.nombre);
            this.splitContenido.Panel2.Controls.Add(this.lblDescripcionEjercicio);
            this.splitContenido.Panel2.Controls.Add(this.descripcion);
            this.splitContenido.Panel2.Controls.Add(this.lblEstadoCampo);
            this.splitContenido.Panel2.Controls.Add(this.lblEstadoValor);
            this.splitContenido.Panel2.Controls.Add(this.guardar);
            this.splitContenido.Panel2.Controls.Add(this.darDeBaja);
            this.splitContenido.Panel2.Controls.Add(this.reactivar);
            this.splitContenido.Panel2.Controls.Add(this.galeriaImagenes);
            this.splitContenido.Panel2.Controls.Add(this.agregarImagen);
            this.splitContenido.Panel2.Controls.Add(this.quitarImagen);
            this.splitContenido.Panel2.Controls.Add(this.lblImagenes);
            this.splitContenido.Panel2.Padding = new System.Windows.Forms.Padding(0, 16, 16, 16);
            this.splitContenido.Panel2MinSize = 550;
            this.splitContenido.Size = new System.Drawing.Size(1068, 620);
            this.splitContenido.SplitterDistance = 320;
            this.splitContenido.SplitterWidth = 12;
            this.splitContenido.TabIndex = 0;
            //
            // lblListadoTitulo
            //
            this.lblListadoTitulo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblListadoTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblListadoTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblListadoTitulo.Location = new System.Drawing.Point(3, 0);
            this.lblListadoTitulo.Name = "lblListadoTitulo";
            this.lblListadoTitulo.Size = new System.Drawing.Size(314, 30);
            this.lblListadoTitulo.TabIndex = 5;
            this.lblListadoTitulo.Text = "Ejercicios del catálogo";
            this.lblListadoTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // tabla
            //
            this.tabla.AllowUserToAddRows = false;
            this.tabla.AllowUserToDeleteRows = false;
            this.tabla.AllowUserToResizeRows = false;
            this.tabla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabla.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tabla.BackgroundColor = System.Drawing.Color.White;
            this.tabla.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tabla.ColumnHeadersHeight = 34;
            this.tabla.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colNombre,
            this.colDescripcion,
            this.colEstado});
            this.tabla.ContextMenuStrip = this.menuEjercicios;
            this.tabla.Location = new System.Drawing.Point(3, 108);
            this.tabla.MultiSelect = false;
            this.tabla.Name = "tabla";
            this.tabla.ReadOnly = true;
            this.tabla.RowHeadersVisible = false;
            this.tabla.RowHeadersWidth = 51;
            this.tabla.RowTemplate.Height = 28;
            this.tabla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tabla.Size = new System.Drawing.Size(314, 448);
            this.tabla.TabIndex = 4;
            this.tabla.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.tabla_CellMouseDown);
            this.tabla.SelectionChanged += new System.EventHandler(this.tabla_SelectionChanged);
            //
            // colId
            //
            this.colId.MinimumWidth = 6;
            this.colId.Name = "colId";
            this.colId.ReadOnly = true;
            this.colId.Visible = false;
            //
            // colNombre
            //
            this.colNombre.FillWeight = 30F;
            this.colNombre.HeaderText = "Nombre";
            this.colNombre.MinimumWidth = 100;
            this.colNombre.Name = "colNombre";
            this.colNombre.ReadOnly = true;
            //
            // colDescripcion
            //
            this.colDescripcion.FillWeight = 52F;
            this.colDescripcion.HeaderText = "Descripción";
            this.colDescripcion.MinimumWidth = 120;
            this.colDescripcion.Name = "colDescripcion";
            this.colDescripcion.ReadOnly = true;
            //
            // colEstado
            //
            this.colEstado.FillWeight = 18F;
            this.colEstado.HeaderText = "Estado";
            this.colEstado.MinimumWidth = 80;
            this.colEstado.Name = "colEstado";
            this.colEstado.ReadOnly = true;
            //
            // menuEjercicios
            //
            this.menuEjercicios.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuEjercicios.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.actualizarMenu,
            this.darDeBajaMenu,
            this.reactivarMenu});
            this.menuEjercicios.Name = "menuEjercicios";
            this.menuEjercicios.Size = new System.Drawing.Size(194, 76);
            //
            // actualizarMenu
            //
            this.actualizarMenu.Name = "actualizarMenu";
            this.actualizarMenu.Size = new System.Drawing.Size(193, 24);
            this.actualizarMenu.Text = "Actualizar listado";
            this.actualizarMenu.Click += new System.EventHandler(this.actualizar_Click);
            //
            // darDeBajaMenu
            //
            this.darDeBajaMenu.Name = "darDeBajaMenu";
            this.darDeBajaMenu.Size = new System.Drawing.Size(193, 24);
            this.darDeBajaMenu.Text = "Dar de baja";
            this.darDeBajaMenu.Click += new System.EventHandler(this.darDeBaja_Click);
            //
            // reactivarMenu
            //
            this.reactivarMenu.Name = "reactivarMenu";
            this.reactivarMenu.Size = new System.Drawing.Size(193, 24);
            this.reactivarMenu.Text = "Reactivar";
            this.reactivarMenu.Click += new System.EventHandler(this.reactivar_Click);
            //
            // lblDetalleTitulo
            //
            this.lblDetalleTitulo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDetalleTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblDetalleTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblDetalleTitulo.Location = new System.Drawing.Point(10, 8);
            this.lblDetalleTitulo.Name = "lblDetalleTitulo";
            this.lblDetalleTitulo.Size = new System.Drawing.Size(630, 28);
            this.lblDetalleTitulo.TabIndex = 2;
            this.lblDetalleTitulo.Text = "Nuevo ejercicio";
            //
            // lblNombre
            //
            this.lblNombre.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblNombre.Location = new System.Drawing.Point(10, 42);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(100, 24);
            this.lblNombre.TabIndex = 1;
            this.lblNombre.Text = "Nombre";
            this.lblNombre.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // nombre
            //
            this.nombre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nombre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nombre.Location = new System.Drawing.Point(116, 42);
            this.nombre.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.nombre.Name = "nombre";
            this.nombre.Size = new System.Drawing.Size(524, 29);
            this.nombre.TabIndex = 0;
            //
            // lblDescripcionEjercicio
            //
            this.lblDescripcionEjercicio.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblDescripcionEjercicio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblDescripcionEjercicio.Location = new System.Drawing.Point(10, 74);
            this.lblDescripcionEjercicio.Name = "lblDescripcionEjercicio";
            this.lblDescripcionEjercicio.Size = new System.Drawing.Size(100, 24);
            this.lblDescripcionEjercicio.TabIndex = 2;
            this.lblDescripcionEjercicio.Text = "Descripción";
            //
            // descripcion
            //
            this.descripcion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descripcion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.descripcion.Location = new System.Drawing.Point(116, 74);
            this.descripcion.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.descripcion.Multiline = true;
            this.descripcion.Name = "descripcion";
            this.descripcion.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.descripcion.Size = new System.Drawing.Size(524, 84);
            this.descripcion.TabIndex = 1;
            //
            // lblEstadoCampo
            //
            this.lblEstadoCampo.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblEstadoCampo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblEstadoCampo.Location = new System.Drawing.Point(10, 170);
            this.lblEstadoCampo.Name = "lblEstadoCampo";
            this.lblEstadoCampo.Size = new System.Drawing.Size(100, 24);
            this.lblEstadoCampo.TabIndex = 3;
            this.lblEstadoCampo.Text = "Estado";
            this.lblEstadoCampo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblEstadoValor
            //
            this.lblEstadoValor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEstadoValor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(101)))), ((int)(((byte)(52)))));
            this.lblEstadoValor.Location = new System.Drawing.Point(116, 170);
            this.lblEstadoValor.Name = "lblEstadoValor";
            this.lblEstadoValor.Size = new System.Drawing.Size(524, 24);
            this.lblEstadoValor.TabIndex = 4;
            this.lblEstadoValor.Text = "Activo";
            this.lblEstadoValor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // guardar
            //
            this.guardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.guardar.FlatAppearance.BorderSize = 0;
            this.guardar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(75)))), ((int)(((byte)(31)))));
            this.guardar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(101)))), ((int)(((byte)(41)))));
            this.guardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.guardar.ForeColor = System.Drawing.Color.White;
            this.guardar.Location = new System.Drawing.Point(10, 204);
            this.guardar.Name = "guardar";
            this.guardar.Size = new System.Drawing.Size(146, 32);
            this.guardar.TabIndex = 7;
            this.guardar.Text = "Guardar";
            this.guardar.UseVisualStyleBackColor = false;
            this.guardar.Click += new System.EventHandler(this.guardar_Click);
            //
            // darDeBaja
            //
            this.darDeBaja.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.darDeBaja.FlatAppearance.BorderSize = 0;
            this.darDeBaja.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.darDeBaja.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.darDeBaja.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.darDeBaja.ForeColor = System.Drawing.Color.White;
            this.darDeBaja.Location = new System.Drawing.Point(162, 204);
            this.darDeBaja.Name = "darDeBaja";
            this.darDeBaja.Size = new System.Drawing.Size(108, 32);
            this.darDeBaja.TabIndex = 9;
            this.darDeBaja.Text = "Dar de baja";
            this.darDeBaja.UseVisualStyleBackColor = false;
            this.darDeBaja.Visible = false;
            this.darDeBaja.Click += new System.EventHandler(this.darDeBaja_Click);
            //
            // reactivar
            //
            this.reactivar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.reactivar.FlatAppearance.BorderSize = 0;
            this.reactivar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(75)))), ((int)(((byte)(31)))));
            this.reactivar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(101)))), ((int)(((byte)(41)))));
            this.reactivar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.reactivar.ForeColor = System.Drawing.Color.White;
            this.reactivar.Location = new System.Drawing.Point(276, 204);
            this.reactivar.Name = "reactivar";
            this.reactivar.Size = new System.Drawing.Size(108, 32);
            this.reactivar.TabIndex = 10;
            this.reactivar.Text = "Reactivar";
            this.reactivar.UseVisualStyleBackColor = false;
            this.reactivar.Visible = false;
            this.reactivar.Click += new System.EventHandler(this.reactivar_Click);
            //
            // galeriaImagenes
            //
            this.galeriaImagenes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.galeriaImagenes.AutoScroll = true;
            this.galeriaImagenes.BackColor = System.Drawing.Color.White;
            this.galeriaImagenes.Location = new System.Drawing.Point(10, 326);
            this.galeriaImagenes.Name = "galeriaImagenes";
            this.galeriaImagenes.Padding = new System.Windows.Forms.Padding(4);
            this.galeriaImagenes.Size = new System.Drawing.Size(630, 278);
            this.galeriaImagenes.TabIndex = 0;
            //
            // agregarImagen
            //
            this.agregarImagen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(75)))), ((int)(((byte)(138)))));
            this.agregarImagen.Enabled = false;
            this.agregarImagen.FlatAppearance.BorderSize = 0;
            this.agregarImagen.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(48)))), ((int)(((byte)(89)))));
            this.agregarImagen.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(61)))), ((int)(((byte)(112)))));
            this.agregarImagen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.agregarImagen.ForeColor = System.Drawing.Color.White;
            this.agregarImagen.Location = new System.Drawing.Point(10, 282);
            this.agregarImagen.Name = "agregarImagen";
            this.agregarImagen.Size = new System.Drawing.Size(146, 32);
            this.agregarImagen.TabIndex = 0;
            this.agregarImagen.Text = "Agregar imagen";
            this.agregarImagen.UseVisualStyleBackColor = false;
            this.agregarImagen.Click += new System.EventHandler(this.agregarImagen_Click);
            //
            // quitarImagen
            //
            this.quitarImagen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.quitarImagen.Enabled = false;
            this.quitarImagen.FlatAppearance.BorderSize = 0;
            this.quitarImagen.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.quitarImagen.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.quitarImagen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.quitarImagen.ForeColor = System.Drawing.Color.White;
            this.quitarImagen.Location = new System.Drawing.Point(162, 282);
            this.quitarImagen.Name = "quitarImagen";
            this.quitarImagen.Size = new System.Drawing.Size(130, 32);
            this.quitarImagen.TabIndex = 1;
            this.quitarImagen.Text = "Quitar imagen";
            this.quitarImagen.UseVisualStyleBackColor = false;
            this.quitarImagen.Click += new System.EventHandler(this.quitarImagen_Click);
            //
            // lblImagenes
            //
            this.lblImagenes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblImagenes.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblImagenes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblImagenes.Location = new System.Drawing.Point(10, 248);
            this.lblImagenes.Name = "lblImagenes";
            this.lblImagenes.Size = new System.Drawing.Size(630, 28);
            this.lblImagenes.TabIndex = 8;
            this.lblImagenes.Text = "Imágenes (0/4)";
            this.lblImagenes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // GestionEjerciciosFormulario
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1100, 680);
            this.Controls.Add(this.splitContenido);
            this.Controls.Add(this.lblEstado);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.MinimumSize = new System.Drawing.Size(916, 560);
            this.Name = "GestionEjerciciosFormulario";
            this.Padding = new System.Windows.Forms.Padding(16);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SysGym";
            this.Load += new System.EventHandler(this.GestionEjerciciosFormulario_Load);
            ((System.ComponentModel.ISupportInitialize)(this.indicadorErrores)).EndInit();
            this.splitContenido.Panel1.ResumeLayout(false);
            this.splitContenido.Panel1.PerformLayout();
            this.splitContenido.Panel2.ResumeLayout(false);
            this.splitContenido.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContenido)).EndInit();
            this.splitContenido.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).EndInit();
            this.menuEjercicios.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
