using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Administrador
{
    partial class GestionUsuariosFormulario
    {
        private PictureBox fotoUsuario;
        private Button btnSeleccionarFoto;
        private Button btnQuitarFoto;
        private ComboBox sexo;
        private Label lblSexo;
        private IContainer components;
        private Panel panelEncabezado;
        private Label lblTitulo;
        private Label lblDescripcion;
        private Button btnVolver;
        private Panel barraAcciones;
        private Label lblEstado;
        private Panel panelContenido;
        private TableLayoutPanel contenedorContenido;
        private Panel panelListado;
        private Label lblListado;
        private Label lblAyuda;
        private Label lblFiltro;
        private Panel panelFiltro;
        private Panel panelDetalle;
        private Panel contenedorDetalle;
        private Label lblFormulario;
        private TableLayoutPanel contenedorCampos;
        private Panel panelAcciones;
        private Label lblNombre;
        private Label lblApellido;
        private Label lblDni;
        private Label lblFechaNacimiento;
        private Label lblNombreUsuario;
        private Label lblClave;
        private Label lblSalario;
        private Label lblRol;
        private TextBox nombre;
        private TextBox apellido;
        private TextBox dni;
        private DateTimePicker fechaNacimiento;
        private TextBox nombreUsuario;
        private TextBox clave;
        private TextBox salario;
        private ComboBox rol;
        private Button nuevo;
        private Button guardar;
        private Button actualizar;
        private Button darDeBaja;
        private Button reactivar;
        private ComboBox filtroEstado;
        private TextBox buscador;
        private DataGridView tabla;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colNombre;
        private DataGridViewTextBoxColumn colDni;
        private DataGridViewTextBoxColumn colUsuario;
        private DataGridViewTextBoxColumn colRol;
        private DataGridViewTextBoxColumn colSalario;
        private DataGridViewTextBoxColumn colEstado;

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
            this.fotoUsuario = new PictureBox();
            this.btnSeleccionarFoto = new Button();
            this.btnQuitarFoto = new Button();
            this.sexo = new ComboBox();
            this.lblSexo = new Label();
            this.panelEncabezado = new Panel();
            this.lblTitulo = new Label();
            this.lblDescripcion = new Label();
            this.btnVolver = new Button();
            this.barraAcciones = new Panel();
            this.lblEstado = new Label();
            this.panelContenido = new Panel();
            this.contenedorContenido = new TableLayoutPanel();
            this.panelListado = new Panel();
            this.lblListado = new Label();
            this.lblAyuda = new Label();
            this.lblFiltro = new Label();
            this.panelFiltro = new Panel();
            this.filtroEstado = new ComboBox();
            this.buscador = new TextBox();
            this.tabla = new DataGridView();
            this.colId = new DataGridViewTextBoxColumn();
            this.colNombre = new DataGridViewTextBoxColumn();
            this.colDni = new DataGridViewTextBoxColumn();
            this.colUsuario = new DataGridViewTextBoxColumn();
            this.colRol = new DataGridViewTextBoxColumn();
            this.colSalario = new DataGridViewTextBoxColumn();
            this.colEstado = new DataGridViewTextBoxColumn();
            this.panelDetalle = new Panel();
            this.contenedorDetalle = new Panel();
            this.lblFormulario = new Label();
            this.contenedorCampos = new TableLayoutPanel();
            this.lblNombre = new Label();
            this.nombre = new TextBox();
            this.lblApellido = new Label();
            this.apellido = new TextBox();
            this.lblDni = new Label();
            this.dni = new TextBox();
            this.lblFechaNacimiento = new Label();
            this.fechaNacimiento = new DateTimePicker();
            this.lblNombreUsuario = new Label();
            this.nombreUsuario = new TextBox();
            this.lblClave = new Label();
            this.clave = new TextBox();
            this.lblSalario = new Label();
            this.salario = new TextBox();
            this.lblRol = new Label();
            this.rol = new ComboBox();
            this.panelAcciones = new Panel();
            this.nuevo = new Button();
            this.guardar = new Button();
            this.actualizar = new Button();
            this.darDeBaja = new Button();
            this.reactivar = new Button();
            ((ISupportInitialize)(this.fotoUsuario)).BeginInit();
            ((ISupportInitialize)(this.tabla)).BeginInit();
            this.panelEncabezado.SuspendLayout();
            this.panelContenido.SuspendLayout();
            this.contenedorContenido.SuspendLayout();
            this.panelListado.SuspendLayout();
            this.panelFiltro.SuspendLayout();
            this.panelDetalle.SuspendLayout();
            this.contenedorDetalle.SuspendLayout();
            this.contenedorCampos.SuspendLayout();
            this.panelAcciones.SuspendLayout();
            this.SuspendLayout();
            //
            // panelEncabezado
            //
            this.panelEncabezado.BackColor = Color.FromArgb(79, 70, 229);
            this.panelEncabezado.Controls.Add(this.lblDescripcion);
            this.panelEncabezado.Controls.Add(this.lblTitulo);
            this.panelEncabezado.Controls.Add(this.btnVolver);
            this.panelEncabezado.Dock = DockStyle.Top;
            this.panelEncabezado.Location = new Point(0, 0);
            this.panelEncabezado.Name = "panelEncabezado";
            this.panelEncabezado.Padding = new Padding(22, 8, 22, 8);
            this.panelEncabezado.Size = new Size(1100, 56);
            this.panelEncabezado.TabIndex = 0;
            this.panelEncabezado.Visible = false;
            //
            // lblDescripcion
            //
            this.lblDescripcion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.lblDescripcion.AutoSize = false;
            this.lblDescripcion.ForeColor = Color.FromArgb(226, 232, 240);
            this.lblDescripcion.Location = new Point(24, 48);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new Size(890, 26);
            this.lblDescripcion.TabIndex = 1;
            this.lblDescripcion.Text = "Administracion del personal y sus permisos";
            this.lblDescripcion.Visible = false;
            //
            // lblTitulo
            //
            this.lblTitulo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.lblTitulo.AutoSize = false;
            this.lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            this.lblTitulo.ForeColor = Color.White;
            this.lblTitulo.Location = new Point(22, 8);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new Size(890, 40);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Usuarios y roles | Administración del personal y sus permisos";
            this.lblTitulo.TextAlign = ContentAlignment.MiddleLeft;
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
            this.btnVolver.Size = new Size(104, 38);
            this.btnVolver.TabIndex = 2;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = false;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            //
            // barraAcciones
            //
            this.barraAcciones.BackColor = Color.FromArgb(203, 213, 225);
            this.barraAcciones.Dock = DockStyle.Top;
            this.barraAcciones.Location = new Point(0, 56);
            this.barraAcciones.Name = "barraAcciones";
            this.barraAcciones.Size = new Size(1100, 1);
            this.barraAcciones.TabIndex = 1;
            //
            // lblEstado
            //
            this.lblEstado.BackColor = Color.FromArgb(226, 232, 240);
            this.lblEstado.Dock = DockStyle.Bottom;
            this.lblEstado.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblEstado.Location = new Point(0, 650);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Padding = new Padding(18, 0, 12, 0);
            this.lblEstado.Size = new Size(1100, 30);
            this.lblEstado.TabIndex = 3;
            this.lblEstado.Text = "Listo";
            this.lblEstado.TextAlign = ContentAlignment.MiddleLeft;
            //
            // panelContenido
            //
            this.panelContenido.BackColor = Color.FromArgb(248, 250, 252);
            this.panelContenido.Controls.Add(this.contenedorContenido);
            this.panelContenido.Dock = DockStyle.Fill;
            this.panelContenido.Location = new Point(0, 85);
            this.panelContenido.Name = "panelContenido";
            this.panelContenido.Padding = new Padding(16);
            this.panelContenido.Size = new Size(1100, 565);
            this.panelContenido.TabIndex = 2;
            //
            // contenedorContenido
            //
            this.contenedorContenido.ColumnCount = 2;
            this.contenedorContenido.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.contenedorContenido.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 396F));
            this.contenedorContenido.Controls.Add(this.panelListado, 0, 0);
            this.contenedorContenido.Controls.Add(this.panelDetalle, 1, 0);
            this.contenedorContenido.Dock = DockStyle.Fill;
            this.contenedorContenido.Location = new Point(16, 16);
            this.contenedorContenido.Name = "contenedorContenido";
            this.contenedorContenido.RowCount = 1;
            this.contenedorContenido.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.contenedorContenido.Size = new Size(1068, 533);
            this.contenedorContenido.TabIndex = 0;
            //
            // panelListado
            //
            this.panelListado.BackColor = Color.White;
            this.panelListado.BorderStyle = BorderStyle.FixedSingle;
            this.panelListado.Controls.Add(this.tabla);
            this.panelListado.Controls.Add(this.panelFiltro);
            this.panelListado.Controls.Add(this.lblAyuda);
            this.panelListado.Controls.Add(this.lblListado);
            this.panelListado.Dock = DockStyle.Fill;
            this.panelListado.Location = new Point(0, 0);
            this.panelListado.Margin = new Padding(0, 0, 16, 0);
            this.panelListado.Name = "panelListado";
            this.panelListado.Padding = new Padding(16);
            this.panelListado.Size = new Size(656, 533);
            this.panelListado.TabIndex = 0;
            //
            // lblListado
            //
            this.lblListado.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.lblListado.AutoSize = false;
            this.lblListado.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            this.lblListado.ForeColor = Color.FromArgb(30, 41, 59);
            this.lblListado.Location = new Point(16, 16);
            this.lblListado.Name = "lblListado";
            this.lblListado.Size = new Size(622, 28);
            this.lblListado.TabIndex = 0;
            this.lblListado.Text = "Usuarios";
            this.lblListado.TextAlign = ContentAlignment.MiddleLeft;
            //
            // lblAyuda
            //
            this.lblAyuda.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.lblAyuda.AutoSize = false;
            this.lblAyuda.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblAyuda.Location = new Point(16, 44);
            this.lblAyuda.Name = "lblAyuda";
            this.lblAyuda.Size = new Size(622, 24);
            this.lblAyuda.TabIndex = 1;
            this.lblAyuda.Text = "Busca por nombre, DNI o usuario";
            this.lblAyuda.TextAlign = ContentAlignment.MiddleLeft;
            //
            // panelFiltro
            //
            this.panelFiltro.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.panelFiltro.AutoSize = false;
            this.panelFiltro.Controls.Add(this.filtroEstado);
            this.panelFiltro.Controls.Add(this.lblFiltro);
            this.panelFiltro.Controls.Add(this.buscador);
            this.panelFiltro.Location = new Point(16, 72);
            this.panelFiltro.Name = "panelFiltro";
            this.panelFiltro.Size = new Size(622, 42);
            this.panelFiltro.TabIndex = 2;
            //
            // filtroEstado
            //
            this.filtroEstado.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.filtroEstado.AutoSize = false;
            this.filtroEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            this.filtroEstado.Items.AddRange(new object[] {
            "Todos",
            "Activos",
            "Inactivos"});
            this.filtroEstado.Location = new Point(474, 8);
            this.filtroEstado.Name = "filtroEstado";
            this.filtroEstado.Size = new Size(148, 26);
            this.filtroEstado.TabIndex = 2;
            this.filtroEstado.SelectedIndexChanged += new System.EventHandler(this.filtroEstado_SelectedIndexChanged);
            //
            // lblFiltro
            //
            this.lblFiltro.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.lblFiltro.AutoSize = false;
            this.lblFiltro.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            this.lblFiltro.ForeColor = Color.FromArgb(71, 85, 105);
            this.lblFiltro.Location = new Point(424, 10);
            this.lblFiltro.Name = "lblFiltro";
            this.lblFiltro.Size = new Size(46, 24);
            this.lblFiltro.TabIndex = 1;
            this.lblFiltro.Text = "Estado:";
            this.lblFiltro.TextAlign = ContentAlignment.MiddleRight;
            //
            // buscador
            //
            this.buscador.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.buscador.AutoSize = false;
            this.buscador.BorderStyle = BorderStyle.FixedSingle;
            this.buscador.Location = new Point(0, 9);
            this.buscador.Name = "buscador";
            this.buscador.Size = new Size(412, 26);
            this.buscador.TabIndex = 0;
            this.buscador.TextChanged += new System.EventHandler(this.buscador_TextChanged);
            //
            // tabla
            //
            this.tabla.AllowUserToAddRows = false;
            this.tabla.AllowUserToDeleteRows = false;
            this.tabla.AllowUserToResizeRows = false;
            this.tabla.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.tabla.AutoSize = false;
            this.tabla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.tabla.BackgroundColor = Color.White;
            this.tabla.BorderStyle = BorderStyle.None;
            this.tabla.ColumnHeadersHeight = 46;
            this.tabla.Columns.AddRange(new DataGridViewColumn[] {
            this.colId,
            this.colNombre,
            this.colDni,
            this.colUsuario,
            this.colRol,
            this.colSalario,
            this.colEstado});
            this.tabla.Location = new Point(16, 124);
            this.tabla.MultiSelect = false;
            this.tabla.Name = "tabla";
            this.tabla.ReadOnly = true;
            this.tabla.RowHeadersVisible = false;
            this.tabla.RowTemplate.Height = 30;
            this.tabla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.tabla.Size = new Size(622, 391);
            this.tabla.TabIndex = 3;
            this.tabla.SelectionChanged += new System.EventHandler(this.tabla_SelectionChanged);
            //
            // colId
            //
            this.colId.HeaderText = "Id";
            this.colId.Name = "colId";
            this.colId.ReadOnly = true;
            this.colId.Visible = false;
            //
            // colNombre
            //
            this.colNombre.HeaderText = "Nombre";
            this.colNombre.Name = "colNombre";
            this.colNombre.ReadOnly = true;
            //
            // colDni
            //
            this.colDni.HeaderText = "DNI";
            this.colDni.Name = "colDni";
            this.colDni.ReadOnly = true;
            //
            // colUsuario
            //
            this.colUsuario.HeaderText = "Usuario";
            this.colUsuario.Name = "colUsuario";
            this.colUsuario.ReadOnly = true;
            //
            // colRol
            //
            this.colRol.HeaderText = "Rol";
            this.colRol.Name = "colRol";
            this.colRol.ReadOnly = true;
            //
            // colSalario
            //
            this.colSalario.HeaderText = "Salario";
            this.colSalario.Name = "colSalario";
            this.colSalario.ReadOnly = true;
            //
            // colEstado
            //
            this.colEstado.HeaderText = "Estado";
            this.colEstado.Name = "colEstado";
            this.colEstado.ReadOnly = true;
            //
            // panelDetalle
            //
            this.panelDetalle.AutoScroll = true;
            this.panelDetalle.Controls.Add(this.contenedorDetalle);
            this.panelDetalle.Controls.Add(this.fotoUsuario);
            this.panelDetalle.Controls.Add(this.btnSeleccionarFoto);
            this.panelDetalle.Controls.Add(this.btnQuitarFoto);
            this.panelDetalle.Dock = DockStyle.Fill;
            this.panelDetalle.Location = new Point(672, 0);
            this.panelDetalle.Name = "panelDetalle";
            this.panelDetalle.Padding = new Padding(16);
            this.panelDetalle.Size = new Size(396, 533);
            this.panelDetalle.TabIndex = 1;
            //
            // contenedorDetalle
            //
            this.contenedorDetalle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.contenedorDetalle.AutoSize = false;
            this.contenedorDetalle.Controls.Add(this.lblFormulario);
            this.contenedorDetalle.Controls.Add(this.contenedorCampos);
            this.contenedorDetalle.Controls.Add(this.panelAcciones);
            this.contenedorDetalle.Location = new Point(16, 16);
            this.contenedorDetalle.Name = "contenedorDetalle";
            this.contenedorDetalle.Size = new Size(362, 484);
            this.contenedorDetalle.TabIndex = 0;
            //
            // lblFormulario
            //
            this.lblFormulario.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this.lblFormulario.AutoSize = false;
            this.lblFormulario.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            this.lblFormulario.ForeColor = Color.FromArgb(30, 41, 59);
            this.lblFormulario.Location = new Point(0, 0);
            this.lblFormulario.Name = "lblFormulario";
            this.lblFormulario.Size = new Size(362, 32);
            this.lblFormulario.TabIndex = 0;
            this.lblFormulario.Text = "Nuevo usuario";
            this.lblFormulario.TextAlign = ContentAlignment.MiddleLeft;
            //
            // contenedorCampos
            //
            this.contenedorCampos.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.contenedorCampos.AutoSize = false;
            this.contenedorCampos.ColumnCount = 2;
            this.contenedorCampos.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 124F));
            this.contenedorCampos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.contenedorCampos.Controls.Add(this.lblNombre, 0, 0);
            this.contenedorCampos.Controls.Add(this.nombre, 1, 0);
            this.contenedorCampos.Controls.Add(this.lblApellido, 0, 1);
            this.contenedorCampos.Controls.Add(this.apellido, 1, 1);
            this.contenedorCampos.Controls.Add(this.lblDni, 0, 2);
            this.contenedorCampos.Controls.Add(this.dni, 1, 2);
            this.contenedorCampos.Controls.Add(this.lblFechaNacimiento, 0, 3);
            this.contenedorCampos.Controls.Add(this.fechaNacimiento, 1, 3);
            this.contenedorCampos.Controls.Add(this.lblNombreUsuario, 0, 4);
            this.contenedorCampos.Controls.Add(this.nombreUsuario, 1, 4);
            this.contenedorCampos.Controls.Add(this.lblClave, 0, 5);
            this.contenedorCampos.Controls.Add(this.clave, 1, 5);
            this.contenedorCampos.Controls.Add(this.lblSalario, 0, 6);
            this.contenedorCampos.Controls.Add(this.salario, 1, 6);
            this.contenedorCampos.Controls.Add(this.lblRol, 0, 7);
            this.contenedorCampos.Controls.Add(this.rol, 1, 7);
            this.contenedorCampos.Controls.Add(this.lblSexo, 0, 8);
            this.contenedorCampos.Controls.Add(this.sexo, 1, 8);
            this.contenedorCampos.Location = new Point(0, 42);
            this.contenedorCampos.Name = "contenedorCampos";
            this.contenedorCampos.RowCount = 9;
            this.contenedorCampos.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            this.contenedorCampos.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            this.contenedorCampos.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            this.contenedorCampos.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            this.contenedorCampos.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            this.contenedorCampos.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            this.contenedorCampos.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            this.contenedorCampos.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            this.contenedorCampos.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            this.contenedorCampos.Size = new Size(362, 342);
            this.contenedorCampos.TabIndex = 1;
            //
            // lblNombre
            //
            this.lblNombre.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this.lblNombre.AutoSize = false;
            this.lblNombre.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            this.lblNombre.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblNombre.Location = new Point(0, 0);
            this.lblNombre.Margin = new Padding(0, 0, 8, 8);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new Size(116, 30);
            this.lblNombre.TabIndex = 0;
            this.lblNombre.Text = "Nombre:";
            this.lblNombre.TextAlign = ContentAlignment.MiddleLeft;
            //
            // nombre
            //
            this.nombre.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.nombre.AutoSize = false;
            this.nombre.BorderStyle = BorderStyle.FixedSingle;
            this.nombre.Location = new Point(124, 4);
            this.nombre.Margin = new Padding(0, 3, 16, 8);
            this.nombre.MaxLength = 100;
            this.nombre.Name = "nombre";
            this.nombre.Size = new Size(222, 24);
            this.nombre.TabIndex = 1;
            this.nombre.KeyPress += new KeyPressEventHandler(this.nombre_KeyPress);
            //
            // lblApellido
            //
            this.lblApellido.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this.lblApellido.AutoSize = false;
            this.lblApellido.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            this.lblApellido.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblApellido.Location = new Point(0, 38);
            this.lblApellido.Margin = new Padding(0, 0, 8, 8);
            this.lblApellido.Name = "lblApellido";
            this.lblApellido.Size = new Size(116, 30);
            this.lblApellido.TabIndex = 2;
            this.lblApellido.Text = "Apellido:";
            this.lblApellido.TextAlign = ContentAlignment.MiddleLeft;
            //
            // apellido
            //
            this.apellido.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.apellido.AutoSize = false;
            this.apellido.BorderStyle = BorderStyle.FixedSingle;
            this.apellido.Location = new Point(124, 42);
            this.apellido.Margin = new Padding(0, 3, 16, 8);
            this.apellido.MaxLength = 100;
            this.apellido.Name = "apellido";
            this.apellido.Size = new Size(222, 24);
            this.apellido.TabIndex = 3;
            this.apellido.KeyPress += new KeyPressEventHandler(this.apellido_KeyPress);
            //
            // lblDni
            //
            this.lblDni.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this.lblDni.AutoSize = false;
            this.lblDni.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            this.lblDni.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblDni.Location = new Point(0, 76);
            this.lblDni.Margin = new Padding(0, 0, 8, 8);
            this.lblDni.Name = "lblDni";
            this.lblDni.Size = new Size(116, 30);
            this.lblDni.TabIndex = 4;
            this.lblDni.Text = "DNI:";
            this.lblDni.TextAlign = ContentAlignment.MiddleLeft;
            //
            // dni
            //
            this.dni.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.dni.AutoSize = false;
            this.dni.BorderStyle = BorderStyle.FixedSingle;
            this.dni.Location = new Point(124, 80);
            this.dni.Margin = new Padding(0, 3, 16, 8);
            this.dni.MaxLength = 20;
            this.dni.Name = "dni";
            this.dni.Size = new Size(222, 24);
            this.dni.TabIndex = 5;
            this.dni.KeyPress += new KeyPressEventHandler(this.dni_KeyPress);
            //
            // lblFechaNacimiento
            //
            this.lblFechaNacimiento.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this.lblFechaNacimiento.AutoSize = false;
            this.lblFechaNacimiento.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            this.lblFechaNacimiento.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblFechaNacimiento.Location = new Point(0, 114);
            this.lblFechaNacimiento.Margin = new Padding(0, 0, 8, 8);
            this.lblFechaNacimiento.Name = "lblFechaNacimiento";
            this.lblFechaNacimiento.Size = new Size(116, 30);
            this.lblFechaNacimiento.TabIndex = 6;
            this.lblFechaNacimiento.Text = "Nacimiento:";
            this.lblFechaNacimiento.TextAlign = ContentAlignment.MiddleLeft;
            //
            // fechaNacimiento
            //
            this.fechaNacimiento.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.fechaNacimiento.AutoSize = false;
            this.fechaNacimiento.Format = DateTimePickerFormat.Short;
            this.fechaNacimiento.Location = new Point(124, 118);
            this.fechaNacimiento.Margin = new Padding(0, 3, 16, 8);
            this.fechaNacimiento.Name = "fechaNacimiento";
            this.fechaNacimiento.ShowCheckBox = false;
            this.fechaNacimiento.Size = new Size(222, 24);
            this.fechaNacimiento.TabIndex = 7;
            //
            // lblNombreUsuario
            //
            this.lblNombreUsuario.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this.lblNombreUsuario.AutoSize = false;
            this.lblNombreUsuario.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            this.lblNombreUsuario.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblNombreUsuario.Location = new Point(0, 152);
            this.lblNombreUsuario.Margin = new Padding(0, 0, 8, 8);
            this.lblNombreUsuario.Name = "lblNombreUsuario";
            this.lblNombreUsuario.Size = new Size(116, 30);
            this.lblNombreUsuario.TabIndex = 8;
            this.lblNombreUsuario.Text = "Usuario:";
            this.lblNombreUsuario.TextAlign = ContentAlignment.MiddleLeft;
            //
            // nombreUsuario
            //
            this.nombreUsuario.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.nombreUsuario.AutoSize = false;
            this.nombreUsuario.BorderStyle = BorderStyle.FixedSingle;
            this.nombreUsuario.Location = new Point(124, 156);
            this.nombreUsuario.Margin = new Padding(0, 3, 16, 8);
            this.nombreUsuario.MaxLength = 50;
            this.nombreUsuario.Name = "nombreUsuario";
            this.nombreUsuario.Size = new Size(222, 24);
            this.nombreUsuario.TabIndex = 9;
            //
            // lblClave
            //
            this.lblClave.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this.lblClave.AutoSize = false;
            this.lblClave.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            this.lblClave.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblClave.Location = new Point(0, 190);
            this.lblClave.Margin = new Padding(0, 0, 8, 8);
            this.lblClave.Name = "lblClave";
            this.lblClave.Size = new Size(116, 30);
            this.lblClave.TabIndex = 10;
            this.lblClave.Text = "Contraseña:";
            this.lblClave.TextAlign = ContentAlignment.MiddleLeft;
            this.lblClave.Click += new System.EventHandler(this.lblClave_Click);
            //
            // clave
            //
            this.clave.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.clave.AutoSize = false;
            this.clave.BorderStyle = BorderStyle.FixedSingle;
            this.clave.Location = new Point(124, 194);
            this.clave.Margin = new Padding(0, 3, 16, 8);
            this.clave.MaxLength = 500;
            this.clave.Name = "clave";
            this.clave.Size = new Size(222, 24);
            this.clave.TabIndex = 11;
            this.clave.UseSystemPasswordChar = true;
            //
            // lblSalario
            //
            this.lblSalario.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this.lblSalario.AutoSize = false;
            this.lblSalario.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            this.lblSalario.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblSalario.Location = new Point(0, 228);
            this.lblSalario.Margin = new Padding(0, 0, 8, 8);
            this.lblSalario.Name = "lblSalario";
            this.lblSalario.Size = new Size(116, 30);
            this.lblSalario.TabIndex = 12;
            this.lblSalario.Text = "Salario:";
            this.lblSalario.TextAlign = ContentAlignment.MiddleLeft;
            //
            // salario
            //
            this.salario.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.salario.AutoSize = false;
            this.salario.BorderStyle = BorderStyle.FixedSingle;
            this.salario.Location = new Point(124, 232);
            this.salario.Margin = new Padding(0, 3, 16, 8);
            this.salario.Name = "salario";
            this.salario.Size = new Size(222, 24);
            this.salario.TabIndex = 13;
            this.salario.KeyPress += new KeyPressEventHandler(this.salario_KeyPress);
            //
            // lblRol
            //
            this.lblRol.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this.lblRol.AutoSize = false;
            this.lblRol.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            this.lblRol.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblRol.Location = new Point(0, 266);
            this.lblRol.Margin = new Padding(0, 0, 8, 8);
            this.lblRol.Name = "lblRol";
            this.lblRol.Size = new Size(116, 30);
            this.lblRol.TabIndex = 14;
            this.lblRol.Text = "Rol:";
            this.lblRol.TextAlign = ContentAlignment.MiddleLeft;
            //
            // rol
            //
            this.rol.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.rol.AutoSize = false;
            this.rol.DropDownStyle = ComboBoxStyle.DropDownList;
            this.rol.Location = new Point(124, 270);
            this.rol.Margin = new Padding(0, 3, 16, 8);
            this.rol.Name = "rol";
            this.rol.Size = new Size(222, 25);
            this.rol.TabIndex = 15;
            //
            // lblSexo
            //
            this.lblSexo.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this.lblSexo.AutoSize = false;
            this.lblSexo.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            this.lblSexo.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblSexo.Location = new Point(0, 304);
            this.lblSexo.Margin = new Padding(0, 0, 8, 8);
            this.lblSexo.Name = "lblSexo";
            this.lblSexo.Size = new Size(116, 30);
            this.lblSexo.TabIndex = 16;
            this.lblSexo.Text = "Sexo:";
            this.lblSexo.TextAlign = ContentAlignment.MiddleLeft;
            //
            // sexo
            //
            this.sexo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.sexo.AutoSize = false;
            this.sexo.DropDownStyle = ComboBoxStyle.DropDownList;
            this.sexo.Items.AddRange(new object[] {
            "Masculino",
            "Femenino"});
            this.sexo.Location = new Point(124, 308);
            this.sexo.Margin = new Padding(0, 3, 16, 8);
            this.sexo.Name = "sexo";
            this.sexo.Size = new Size(222, 25);
            this.sexo.TabIndex = 7;
            this.sexo.SelectedIndexChanged += new System.EventHandler(this.sexo_SelectedIndexChanged);
            //
            // panelAcciones
            //
            this.panelAcciones.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.panelAcciones.AutoSize = false;
            this.panelAcciones.Controls.Add(this.nuevo);
            this.panelAcciones.Controls.Add(this.guardar);
            this.panelAcciones.Controls.Add(this.actualizar);
            this.panelAcciones.Controls.Add(this.darDeBaja);
            this.panelAcciones.Controls.Add(this.reactivar);
            this.panelAcciones.Location = new Point(0, 400);
            this.panelAcciones.Name = "panelAcciones";
            this.panelAcciones.Size = new Size(362, 84);
            this.panelAcciones.TabIndex = 2;
            //
            // nuevo
            //
            this.nuevo.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this.nuevo.AutoSize = false;
            this.nuevo.BackColor = Color.FromArgb(79, 70, 229);
            this.nuevo.FlatAppearance.BorderSize = 0;
            this.nuevo.FlatStyle = FlatStyle.Flat;
            this.nuevo.ForeColor = Color.White;
            this.nuevo.Location = new Point(0, 0);
            this.nuevo.Name = "nuevo";
            this.nuevo.Size = new Size(108, 38);
            this.nuevo.TabIndex = 0;
            this.nuevo.Text = "+ Nuevo";
            this.nuevo.UseVisualStyleBackColor = false;
            this.nuevo.Click += new System.EventHandler(this.nuevo_Click);
            //
            // guardar
            //
            this.guardar.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this.guardar.AutoSize = false;
            this.guardar.BackColor = Color.FromArgb(79, 70, 229);
            this.guardar.FlatAppearance.BorderSize = 0;
            this.guardar.FlatStyle = FlatStyle.Flat;
            this.guardar.ForeColor = Color.White;
            this.guardar.Location = new Point(116, 0);
            this.guardar.Name = "guardar";
            this.guardar.Size = new Size(108, 38);
            this.guardar.TabIndex = 1;
            this.guardar.Text = "Guardar";
            this.guardar.UseVisualStyleBackColor = false;
            this.guardar.Click += new System.EventHandler(this.guardar_Click);
            //
            // actualizar
            //
            this.actualizar.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this.actualizar.AutoSize = false;
            this.actualizar.BackColor = Color.FromArgb(226, 232, 240);
            this.actualizar.FlatAppearance.BorderSize = 0;
            this.actualizar.FlatStyle = FlatStyle.Flat;
            this.actualizar.ForeColor = Color.FromArgb(30, 41, 59);
            this.actualizar.Location = new Point(232, 0);
            this.actualizar.Name = "actualizar";
            this.actualizar.Size = new Size(108, 38);
            this.actualizar.TabIndex = 2;
            this.actualizar.Text = "Actualizar";
            this.actualizar.UseVisualStyleBackColor = false;
            this.actualizar.Click += new System.EventHandler(this.actualizar_Click);
            //
            // darDeBaja
            //
            this.darDeBaja.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this.darDeBaja.AutoSize = false;
            this.darDeBaja.BackColor = Color.FromArgb(254, 242, 242);
            this.darDeBaja.FlatAppearance.BorderSize = 0;
            this.darDeBaja.FlatStyle = FlatStyle.Flat;
            this.darDeBaja.ForeColor = Color.FromArgb(185, 28, 28);
            this.darDeBaja.Location = new Point(0, 46);
            this.darDeBaja.Name = "darDeBaja";
            this.darDeBaja.Size = new Size(108, 38);
            this.darDeBaja.TabIndex = 3;
            this.darDeBaja.Text = "Dar de baja";
            this.darDeBaja.UseVisualStyleBackColor = false;
            this.darDeBaja.Click += new System.EventHandler(this.darDeBaja_Click);
            //
            // reactivar
            //
            this.reactivar.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this.reactivar.AutoSize = false;
            this.reactivar.BackColor = Color.FromArgb(226, 232, 240);
            this.reactivar.FlatAppearance.BorderSize = 0;
            this.reactivar.FlatStyle = FlatStyle.Flat;
            this.reactivar.ForeColor = Color.FromArgb(30, 41, 59);
            this.reactivar.Location = new Point(116, 46);
            this.reactivar.Name = "reactivar";
            this.reactivar.Size = new Size(108, 38);
            this.reactivar.TabIndex = 4;
            this.reactivar.Text = "Reactivar";
            this.reactivar.UseVisualStyleBackColor = false;
            this.reactivar.Click += new System.EventHandler(this.reactivar_Click);
            //
            // fotoUsuario
            //
            this.fotoUsuario.BackColor = Color.LightGray;
            this.fotoUsuario.BorderStyle = BorderStyle.FixedSingle;
            this.fotoUsuario.Location = new Point(16, 516);
            this.fotoUsuario.Name = "fotoUsuario";
            this.fotoUsuario.Size = new Size(120, 120);
            this.fotoUsuario.SizeMode = PictureBoxSizeMode.Zoom;
            this.fotoUsuario.TabStop = false;
            this.fotoUsuario.Disposed += new System.EventHandler(this.fotoUsuario_Disposed);
            //
            // btnSeleccionarFoto
            //
            this.btnSeleccionarFoto.Location = new Point(152, 516);
            this.btnSeleccionarFoto.Name = "btnSeleccionarFoto";
            this.btnSeleccionarFoto.Size = new Size(180, 36);
            this.btnSeleccionarFoto.Text = "Seleccionar foto";
            this.btnSeleccionarFoto.UseVisualStyleBackColor = true;
            this.btnSeleccionarFoto.Click += new System.EventHandler(this.btnSeleccionarFoto_Click);
            //
            // btnQuitarFoto
            //
            this.btnQuitarFoto.Location = new Point(152, 560);
            this.btnQuitarFoto.Name = "btnQuitarFoto";
            this.btnQuitarFoto.Size = new Size(180, 36);
            this.btnQuitarFoto.Text = "Quitar foto";
            this.btnQuitarFoto.UseVisualStyleBackColor = true;
            this.btnQuitarFoto.Click += new System.EventHandler(this.btnQuitarFoto_Click);
            //
            // GestionUsuariosFormulario
            //
            this.AutoScaleDimensions = new SizeF(7F, 17F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(241, 245, 249);
            this.ClientSize = new Size(1100, 680);
            this.Controls.Add(this.panelContenido);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.barraAcciones);
            this.Controls.Add(this.panelEncabezado);
            this.Font = new Font("Segoe UI", 9.5F);
            this.MinimumSize = new Size(900, 560);
            this.Name = "GestionUsuariosFormulario";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "SysGym | Usuarios y roles";
            this.Load += new System.EventHandler(this.GestionUsuariosFormulario_Load);
            this.panelEncabezado.ResumeLayout(false);
            this.panelContenido.ResumeLayout(false);
            this.contenedorContenido.ResumeLayout(false);
            this.panelListado.ResumeLayout(false);
            this.panelFiltro.ResumeLayout(false);
            this.panelDetalle.ResumeLayout(false);
            this.contenedorDetalle.ResumeLayout(false);
            this.contenedorCampos.ResumeLayout(false);
            this.panelAcciones.ResumeLayout(false);
            ((ISupportInitialize)(this.tabla)).EndInit();
            ((ISupportInitialize)(this.fotoUsuario)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
