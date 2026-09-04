using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Administrador
{
    partial class GestionUsuariosForm
    {
        private IContainer components;
        private Panel panelEncabezado;
        private Label lblTitulo;
        private Label lblDescripcion;
        private Button btnVolver;
        private FlowLayoutPanel barraAcciones;
        private Label lblEstado;
        private Panel panelContenido;
        private TableLayoutPanel layoutContenido;
        private Panel panelListado;
        private Label lblListado;
        private Label lblAyuda;
        private Label lblFiltro;
        private Panel panelFiltro;
        private Panel panelDetalle;
        private TableLayoutPanel layoutDetalle;
        private Label lblFormulario;
        private TableLayoutPanel layoutCampos;
        private Panel panelAcciones;
        private Label lblNombre;
        private Label lblApellido;
        private Label lblDni;
        private Label lblUsername;
        private Label lblPassword;
        private Label lblSalario;
        private Label lblRol;
        private TextBox nombre;
        private TextBox apellido;
        private TextBox dni;
        private TextBox username;
        private TextBox password;
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

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelEncabezado = new System.Windows.Forms.Panel();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.btnVolver = new System.Windows.Forms.Button();
            this.barraAcciones = new System.Windows.Forms.FlowLayoutPanel();
            this.lblEstado = new System.Windows.Forms.Label();
            this.panelContenido = new System.Windows.Forms.Panel();
            this.layoutContenido = new System.Windows.Forms.TableLayoutPanel();
            this.panelListado = new System.Windows.Forms.Panel();
            this.tabla = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDni = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUsuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSalario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelFiltro = new System.Windows.Forms.Panel();
            this.filtroEstado = new System.Windows.Forms.ComboBox();
            this.lblFiltro = new System.Windows.Forms.Label();
            this.buscador = new System.Windows.Forms.TextBox();
            this.lblAyuda = new System.Windows.Forms.Label();
            this.lblListado = new System.Windows.Forms.Label();
            this.panelDetalle = new System.Windows.Forms.Panel();
            this.layoutDetalle = new System.Windows.Forms.TableLayoutPanel();
            this.lblFormulario = new System.Windows.Forms.Label();
            this.layoutCampos = new System.Windows.Forms.TableLayoutPanel();
            this.lblNombre = new System.Windows.Forms.Label();
            this.nombre = new System.Windows.Forms.TextBox();
            this.lblApellido = new System.Windows.Forms.Label();
            this.apellido = new System.Windows.Forms.TextBox();
            this.lblDni = new System.Windows.Forms.Label();
            this.dni = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.username = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.password = new System.Windows.Forms.TextBox();
            this.lblSalario = new System.Windows.Forms.Label();
            this.salario = new System.Windows.Forms.TextBox();
            this.lblRol = new System.Windows.Forms.Label();
            this.rol = new System.Windows.Forms.ComboBox();
            this.panelAcciones = new System.Windows.Forms.Panel();
            this.nuevo = new System.Windows.Forms.Button();
            this.guardar = new System.Windows.Forms.Button();
            this.actualizar = new System.Windows.Forms.Button();
            this.darDeBaja = new System.Windows.Forms.Button();
            this.reactivar = new System.Windows.Forms.Button();
            this.panelEncabezado.SuspendLayout();
            this.panelContenido.SuspendLayout();
            this.layoutContenido.SuspendLayout();
            this.panelListado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).BeginInit();
            this.panelFiltro.SuspendLayout();
            this.panelDetalle.SuspendLayout();
            this.layoutDetalle.SuspendLayout();
            this.layoutCampos.SuspendLayout();
            this.panelAcciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEncabezado
            // 
            this.panelEncabezado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
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
            this.lblDescripcion.Size = new System.Drawing.Size(262, 17);
            this.lblDescripcion.TabIndex = 1;
            this.lblDescripcion.Text = "Administracion del personal y sus permisos";
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(22, 10);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(196, 32);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Usuarios y roles";
            // 
            // btnVolver
            // 
            this.btnVolver.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVolver.BackColor = System.Drawing.Color.White;
            this.btnVolver.FlatAppearance.BorderSize = 0;
            this.btnVolver.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVolver.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
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
            this.barraAcciones.Dock = System.Windows.Forms.DockStyle.Top;
            this.barraAcciones.Location = new System.Drawing.Point(0, 80);
            this.barraAcciones.Name = "barraAcciones";
            this.barraAcciones.Padding = new System.Windows.Forms.Padding(16, 8, 16, 8);
            this.barraAcciones.Size = new System.Drawing.Size(1100, 52);
            this.barraAcciones.TabIndex = 1;
            this.barraAcciones.WrapContents = false;
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
            this.panelContenido.Controls.Add(this.layoutContenido);
            this.panelContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenido.Location = new System.Drawing.Point(0, 132);
            this.panelContenido.Name = "panelContenido";
            this.panelContenido.Padding = new System.Windows.Forms.Padding(12);
            this.panelContenido.Size = new System.Drawing.Size(1100, 516);
            this.panelContenido.TabIndex = 2;
            // 
            // layoutContenido
            // 
            this.layoutContenido.ColumnCount = 2;
            this.layoutContenido.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.layoutContenido.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.layoutContenido.Controls.Add(this.panelListado, 0, 0);
            this.layoutContenido.Controls.Add(this.panelDetalle, 1, 0);
            this.layoutContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutContenido.Location = new System.Drawing.Point(12, 12);
            this.layoutContenido.Name = "layoutContenido";
            this.layoutContenido.RowCount = 1;
            this.layoutContenido.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutContenido.Size = new System.Drawing.Size(1076, 492);
            this.layoutContenido.TabIndex = 0;
            // 
            // panelListado
            // 
            this.panelListado.BackColor = System.Drawing.Color.White;
            this.panelListado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelListado.Controls.Add(this.tabla);
            this.panelListado.Controls.Add(this.panelFiltro);
            this.panelListado.Controls.Add(this.lblAyuda);
            this.panelListado.Controls.Add(this.lblListado);
            this.panelListado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelListado.Location = new System.Drawing.Point(3, 3);
            this.panelListado.Name = "panelListado";
            this.panelListado.Padding = new System.Windows.Forms.Padding(16);
            this.panelListado.Size = new System.Drawing.Size(585, 486);
            this.panelListado.TabIndex = 0;
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
            this.tabla.ColumnHeadersHeight = 38;
            this.tabla.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colNombre,
            this.colDni,
            this.colUsuario,
            this.colRol,
            this.colSalario,
            this.colEstado});
            this.tabla.Location = new System.Drawing.Point(16, 128);
            this.tabla.MultiSelect = false;
            this.tabla.Name = "tabla";
            this.tabla.ReadOnly = true;
            this.tabla.RowHeadersVisible = false;
            this.tabla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tabla.Size = new System.Drawing.Size(941, 732);
            this.tabla.TabIndex = 3;
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
            // panelFiltro
            // 
            this.panelFiltro.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelFiltro.Controls.Add(this.filtroEstado);
            this.panelFiltro.Controls.Add(this.lblFiltro);
            this.panelFiltro.Controls.Add(this.buscador);
            this.panelFiltro.Location = new System.Drawing.Point(16, 68);
            this.panelFiltro.Name = "panelFiltro";
            this.panelFiltro.Size = new System.Drawing.Size(945, 54);
            this.panelFiltro.TabIndex = 2;
            // 
            // filtroEstado
            // 
            this.filtroEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filtroEstado.Items.AddRange(new object[] {
            "Todos",
            "Activos",
            "Inactivos"});
            this.filtroEstado.Location = new System.Drawing.Point(310, 22);
            this.filtroEstado.Name = "filtroEstado";
            this.filtroEstado.Size = new System.Drawing.Size(190, 25);
            this.filtroEstado.TabIndex = 2;
            // 
            // lblFiltro
            // 
            this.lblFiltro.AutoSize = true;
            this.lblFiltro.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblFiltro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblFiltro.Location = new System.Drawing.Point(310, 7);
            this.lblFiltro.Name = "lblFiltro";
            this.lblFiltro.Size = new System.Drawing.Size(45, 15);
            this.lblFiltro.TabIndex = 1;
            this.lblFiltro.Text = "Estado:";
            // 
            // buscador
            // 
            this.buscador.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buscador.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.buscador.Location = new System.Drawing.Point(0, 22);
            this.buscador.Name = "buscador";
            this.buscador.Size = new System.Drawing.Size(670, 24);
            this.buscador.TabIndex = 0;
            // 
            // lblAyuda
            // 
            this.lblAyuda.AutoSize = true;
            this.lblAyuda.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblAyuda.Location = new System.Drawing.Point(16, 42);
            this.lblAyuda.Name = "lblAyuda";
            this.lblAyuda.Size = new System.Drawing.Size(204, 17);
            this.lblAyuda.TabIndex = 1;
            this.lblAyuda.Text = "Busca por nombre, DNI o usuario";
            // 
            // lblListado
            // 
            this.lblListado.AutoSize = true;
            this.lblListado.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblListado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblListado.Location = new System.Drawing.Point(16, 14);
            this.lblListado.Name = "lblListado";
            this.lblListado.Size = new System.Drawing.Size(68, 20);
            this.lblListado.TabIndex = 0;
            this.lblListado.Text = "Usuarios";
            // 
            // panelDetalle
            // 
            this.panelDetalle.BackColor = System.Drawing.Color.White;
            this.panelDetalle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDetalle.Controls.Add(this.layoutDetalle);
            this.panelDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDetalle.Location = new System.Drawing.Point(594, 3);
            this.panelDetalle.Name = "panelDetalle";
            this.panelDetalle.Padding = new System.Windows.Forms.Padding(16);
            this.panelDetalle.Size = new System.Drawing.Size(479, 486);
            this.panelDetalle.TabIndex = 1;
            // 
            // layoutDetalle
            // 
            this.layoutDetalle.ColumnCount = 1;
            this.layoutDetalle.Controls.Add(this.lblFormulario, 0, 0);
            this.layoutDetalle.Controls.Add(this.layoutCampos, 0, 1);
            this.layoutDetalle.Controls.Add(this.panelAcciones, 0, 2);
            this.layoutDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutDetalle.Location = new System.Drawing.Point(16, 16);
            this.layoutDetalle.Name = "layoutDetalle";
            this.layoutDetalle.RowCount = 3;
            this.layoutDetalle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.layoutDetalle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutDetalle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 118F));
            this.layoutDetalle.Size = new System.Drawing.Size(445, 452);
            this.layoutDetalle.TabIndex = 0;
            // 
            // lblFormulario
            // 
            this.lblFormulario.AutoSize = true;
            this.lblFormulario.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblFormulario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblFormulario.Location = new System.Drawing.Point(3, 0);
            this.lblFormulario.Name = "lblFormulario";
            this.lblFormulario.Size = new System.Drawing.Size(115, 21);
            this.lblFormulario.TabIndex = 0;
            this.lblFormulario.Text = "Nuevo usuario";
            // 
            // layoutCampos
            // 
            this.layoutCampos.ColumnCount = 2;
            this.layoutCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.layoutCampos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.layoutCampos.Controls.Add(this.lblNombre, 0, 0);
            this.layoutCampos.Controls.Add(this.nombre, 1, 0);
            this.layoutCampos.Controls.Add(this.lblApellido, 0, 1);
            this.layoutCampos.Controls.Add(this.apellido, 1, 1);
            this.layoutCampos.Controls.Add(this.lblDni, 0, 2);
            this.layoutCampos.Controls.Add(this.dni, 1, 2);
            this.layoutCampos.Controls.Add(this.lblUsername, 0, 3);
            this.layoutCampos.Controls.Add(this.username, 1, 3);
            this.layoutCampos.Controls.Add(this.lblPassword, 0, 4);
            this.layoutCampos.Controls.Add(this.password, 1, 4);
            this.layoutCampos.Controls.Add(this.lblSalario, 0, 5);
            this.layoutCampos.Controls.Add(this.salario, 1, 5);
            this.layoutCampos.Controls.Add(this.lblRol, 0, 6);
            this.layoutCampos.Controls.Add(this.rol, 1, 6);
            this.layoutCampos.Dock = System.Windows.Forms.DockStyle.Top;
            this.layoutCampos.Location = new System.Drawing.Point(3, 37);
            this.layoutCampos.Name = "layoutCampos";
            this.layoutCampos.RowCount = 7;
            this.layoutCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.layoutCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.layoutCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.layoutCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.layoutCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.layoutCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.layoutCampos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.layoutCampos.Size = new System.Drawing.Size(439, 266);
            this.layoutCampos.TabIndex = 1;
            // 
            // lblNombre
            // 
            this.lblNombre.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblNombre.Location = new System.Drawing.Point(3, 11);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(54, 15);
            this.lblNombre.TabIndex = 0;
            this.lblNombre.Text = "Nombre:";
            // 
            // nombre
            // 
            this.nombre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nombre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nombre.Location = new System.Drawing.Point(175, 4);
            this.nombre.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.nombre.Name = "nombre";
            this.nombre.Size = new System.Drawing.Size(264, 24);
            this.nombre.TabIndex = 1;
            // 
            // lblApellido
            // 
            this.lblApellido.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblApellido.AutoSize = true;
            this.lblApellido.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblApellido.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblApellido.Location = new System.Drawing.Point(3, 49);
            this.lblApellido.Name = "lblApellido";
            this.lblApellido.Size = new System.Drawing.Size(54, 15);
            this.lblApellido.TabIndex = 2;
            this.lblApellido.Text = "Apellido:";
            // 
            // apellido
            // 
            this.apellido.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.apellido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.apellido.Location = new System.Drawing.Point(175, 42);
            this.apellido.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.apellido.Name = "apellido";
            this.apellido.Size = new System.Drawing.Size(264, 24);
            this.apellido.TabIndex = 3;
            // 
            // lblDni
            // 
            this.lblDni.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDni.AutoSize = true;
            this.lblDni.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblDni.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblDni.Location = new System.Drawing.Point(3, 87);
            this.lblDni.Name = "lblDni";
            this.lblDni.Size = new System.Drawing.Size(32, 15);
            this.lblDni.TabIndex = 4;
            this.lblDni.Text = "DNI:";
            // 
            // dni
            // 
            this.dni.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dni.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dni.Location = new System.Drawing.Point(175, 80);
            this.dni.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.dni.Name = "dni";
            this.dni.Size = new System.Drawing.Size(264, 24);
            this.dni.TabIndex = 5;
            // 
            // lblUsername
            // 
            this.lblUsername.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblUsername.Location = new System.Drawing.Point(3, 125);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(50, 15);
            this.lblUsername.TabIndex = 6;
            this.lblUsername.Text = "Usuario:";
            // 
            // username
            // 
            this.username.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.username.Dock = System.Windows.Forms.DockStyle.Fill;
            this.username.Location = new System.Drawing.Point(175, 118);
            this.username.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(264, 24);
            this.username.TabIndex = 7;
            // 
            // lblPassword
            // 
            this.lblPassword.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblPassword.Location = new System.Drawing.Point(3, 163);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(69, 15);
            this.lblPassword.TabIndex = 8;
            this.lblPassword.Text = "Contrasena:";
            this.lblPassword.Click += new System.EventHandler(this.lblPassword_Click);
            // 
            // password
            // 
            this.password.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.password.Dock = System.Windows.Forms.DockStyle.Fill;
            this.password.Location = new System.Drawing.Point(175, 156);
            this.password.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(264, 24);
            this.password.TabIndex = 9;
            this.password.UseSystemPasswordChar = true;
            // 
            // lblSalario
            // 
            this.lblSalario.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSalario.AutoSize = true;
            this.lblSalario.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblSalario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblSalario.Location = new System.Drawing.Point(3, 201);
            this.lblSalario.Name = "lblSalario";
            this.lblSalario.Size = new System.Drawing.Size(46, 15);
            this.lblSalario.TabIndex = 10;
            this.lblSalario.Text = "Salario:";
            // 
            // salario
            // 
            this.salario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.salario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.salario.Location = new System.Drawing.Point(175, 194);
            this.salario.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.salario.Name = "salario";
            this.salario.Size = new System.Drawing.Size(264, 24);
            this.salario.TabIndex = 11;
            // 
            // lblRol
            // 
            this.lblRol.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblRol.AutoSize = true;
            this.lblRol.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblRol.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblRol.Location = new System.Drawing.Point(3, 239);
            this.lblRol.Name = "lblRol";
            this.lblRol.Size = new System.Drawing.Size(27, 15);
            this.lblRol.TabIndex = 12;
            this.lblRol.Text = "Rol:";
            // 
            // rol
            // 
            this.rol.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.rol.Location = new System.Drawing.Point(175, 232);
            this.rol.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.rol.Name = "rol";
            this.rol.Size = new System.Drawing.Size(264, 25);
            this.rol.TabIndex = 13;
            // 
            // panelAcciones
            // 
            this.panelAcciones.Controls.Add(this.nuevo);
            this.panelAcciones.Controls.Add(this.guardar);
            this.panelAcciones.Controls.Add(this.actualizar);
            this.panelAcciones.Controls.Add(this.darDeBaja);
            this.panelAcciones.Controls.Add(this.reactivar);
            this.panelAcciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAcciones.Location = new System.Drawing.Point(3, 337);
            this.panelAcciones.Name = "panelAcciones";
            this.panelAcciones.Size = new System.Drawing.Size(439, 112);
            this.panelAcciones.TabIndex = 2;
            // 
            // nuevo
            // 
            this.nuevo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.nuevo.FlatAppearance.BorderSize = 0;
            this.nuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nuevo.ForeColor = System.Drawing.Color.White;
            this.nuevo.Location = new System.Drawing.Point(0, 2);
            this.nuevo.Name = "nuevo";
            this.nuevo.Size = new System.Drawing.Size(100, 32);
            this.nuevo.TabIndex = 0;
            this.nuevo.Text = "+ Nuevo";
            this.nuevo.UseVisualStyleBackColor = false;
            // 
            // guardar
            // 
            this.guardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.guardar.FlatAppearance.BorderSize = 0;
            this.guardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.guardar.ForeColor = System.Drawing.Color.White;
            this.guardar.Location = new System.Drawing.Point(0, 42);
            this.guardar.Name = "guardar";
            this.guardar.Size = new System.Drawing.Size(100, 32);
            this.guardar.TabIndex = 1;
            this.guardar.Text = "Guardar";
            this.guardar.UseVisualStyleBackColor = false;
            // 
            // actualizar
            // 
            this.actualizar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.actualizar.FlatAppearance.BorderSize = 0;
            this.actualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.actualizar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.actualizar.Location = new System.Drawing.Point(104, 42);
            this.actualizar.Name = "actualizar";
            this.actualizar.Size = new System.Drawing.Size(100, 32);
            this.actualizar.TabIndex = 2;
            this.actualizar.Text = "Actualizar";
            this.actualizar.UseVisualStyleBackColor = false;
            // 
            // darDeBaja
            // 
            this.darDeBaja.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.darDeBaja.FlatAppearance.BorderSize = 0;
            this.darDeBaja.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.darDeBaja.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.darDeBaja.Location = new System.Drawing.Point(0, 82);
            this.darDeBaja.Name = "darDeBaja";
            this.darDeBaja.Size = new System.Drawing.Size(100, 32);
            this.darDeBaja.TabIndex = 3;
            this.darDeBaja.Text = "Dar de baja";
            this.darDeBaja.UseVisualStyleBackColor = false;
            // 
            // reactivar
            // 
            this.reactivar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.reactivar.FlatAppearance.BorderSize = 0;
            this.reactivar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.reactivar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.reactivar.Location = new System.Drawing.Point(104, 82);
            this.reactivar.Name = "reactivar";
            this.reactivar.Size = new System.Drawing.Size(100, 32);
            this.reactivar.TabIndex = 4;
            this.reactivar.Text = "Reactivar";
            this.reactivar.UseVisualStyleBackColor = false;
            // 
            // GestionUsuariosForm
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
            this.Name = "GestionUsuariosForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SysGym | Usuarios y roles";
            this.panelEncabezado.ResumeLayout(false);
            this.panelEncabezado.PerformLayout();
            this.panelContenido.ResumeLayout(false);
            this.layoutContenido.ResumeLayout(false);
            this.panelListado.ResumeLayout(false);
            this.panelListado.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).EndInit();
            this.panelFiltro.ResumeLayout(false);
            this.panelFiltro.PerformLayout();
            this.panelDetalle.ResumeLayout(false);
            this.layoutDetalle.ResumeLayout(false);
            this.layoutDetalle.PerformLayout();
            this.layoutCampos.ResumeLayout(false);
            this.layoutCampos.PerformLayout();
            this.panelAcciones.ResumeLayout(false);
            this.ResumeLayout(false);

        }

    }
}
