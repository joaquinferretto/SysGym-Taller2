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
        private IContainer components = null;
        private ErrorProvider indicadorErrores;





        private Label lblEstado;

        private SplitContainer contenedorContenido;

        private Label lblListado;
        private Label lblAyuda;
        private Label lblFiltro;

        private Label lblFormulario;


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
        private Label lblCreadoPor;
        private ContextMenuStrip menuUsuarios;
        private ToolStripMenuItem darDeBajaMenu;
        private ToolStripMenuItem reactivarMenu;

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
            this.components = new System.ComponentModel.Container();
            this.indicadorErrores = new System.Windows.Forms.ErrorProvider(this.components);
            this.fotoUsuario = new System.Windows.Forms.PictureBox();
            this.btnSeleccionarFoto = new System.Windows.Forms.Button();
            this.btnQuitarFoto = new System.Windows.Forms.Button();
            this.sexo = new System.Windows.Forms.ComboBox();
            this.lblSexo = new System.Windows.Forms.Label();
            this.lblCreadoPor = new System.Windows.Forms.Label();
            this.lblEstado = new System.Windows.Forms.Label();
            this.contenedorContenido = new System.Windows.Forms.SplitContainer();
            this.lblListado = new System.Windows.Forms.Label();
            this.lblAyuda = new System.Windows.Forms.Label();
            this.buscador = new System.Windows.Forms.TextBox();
            this.lblFiltro = new System.Windows.Forms.Label();
            this.filtroEstado = new System.Windows.Forms.ComboBox();
            this.tabla = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDni = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUsuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSalario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuUsuarios = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.darDeBajaMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.reactivarMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.lblFormulario = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.nombre = new System.Windows.Forms.TextBox();
            this.lblApellido = new System.Windows.Forms.Label();
            this.apellido = new System.Windows.Forms.TextBox();
            this.lblDni = new System.Windows.Forms.Label();
            this.dni = new System.Windows.Forms.TextBox();
            this.lblFechaNacimiento = new System.Windows.Forms.Label();
            this.fechaNacimiento = new System.Windows.Forms.DateTimePicker();
            this.lblNombreUsuario = new System.Windows.Forms.Label();
            this.nombreUsuario = new System.Windows.Forms.TextBox();
            this.lblClave = new System.Windows.Forms.Label();
            this.clave = new System.Windows.Forms.TextBox();
            this.lblSalario = new System.Windows.Forms.Label();
            this.salario = new System.Windows.Forms.TextBox();
            this.lblRol = new System.Windows.Forms.Label();
            this.rol = new System.Windows.Forms.ComboBox();
            this.nuevo = new System.Windows.Forms.Button();
            this.guardar = new System.Windows.Forms.Button();
            this.actualizar = new System.Windows.Forms.Button();
            this.darDeBaja = new System.Windows.Forms.Button();
            this.reactivar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.indicadorErrores)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fotoUsuario)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.contenedorContenido)).BeginInit();
            this.contenedorContenido.Panel1.SuspendLayout();
            this.contenedorContenido.Panel2.SuspendLayout();
            this.contenedorContenido.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).BeginInit();
            this.menuUsuarios.SuspendLayout();
            this.SuspendLayout();
            //
            // indicadorErrores
            //
            this.indicadorErrores.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.indicadorErrores.ContainerControl = this;
            //
            // fotoUsuario
            //
            this.fotoUsuario.BackColor = System.Drawing.Color.LightGray;
            this.fotoUsuario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fotoUsuario.Location = new System.Drawing.Point(10, 554);
            this.fotoUsuario.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.fotoUsuario.Name = "fotoUsuario";
            this.fotoUsuario.Size = new System.Drawing.Size(150, 150);
            this.fotoUsuario.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.fotoUsuario.TabIndex = 17;
            this.fotoUsuario.TabStop = false;
            this.fotoUsuario.Disposed += new System.EventHandler(this.fotoUsuario_Disposed);
            //
            // btnSeleccionarFoto
            //
            this.btnSeleccionarFoto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSeleccionarFoto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(75)))), ((int)(((byte)(138)))));
            this.btnSeleccionarFoto.FlatAppearance.BorderSize = 0;
            this.btnSeleccionarFoto.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(48)))), ((int)(((byte)(89)))));
            this.btnSeleccionarFoto.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(61)))), ((int)(((byte)(112)))));
            this.btnSeleccionarFoto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSeleccionarFoto.ForeColor = System.Drawing.Color.White;
            this.btnSeleccionarFoto.Location = new System.Drawing.Point(178, 568);
            this.btnSeleccionarFoto.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSeleccionarFoto.Name = "btnSeleccionarFoto";
            this.btnSeleccionarFoto.Size = new System.Drawing.Size(158, 45);
            this.btnSeleccionarFoto.TabIndex = 18;
            this.btnSeleccionarFoto.Text = "Seleccionar foto";
            this.btnSeleccionarFoto.UseVisualStyleBackColor = false;
            this.btnSeleccionarFoto.Click += new System.EventHandler(this.btnSeleccionarFoto_Click);
            //
            // btnQuitarFoto
            //
            this.btnQuitarFoto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuitarFoto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.btnQuitarFoto.FlatAppearance.BorderSize = 0;
            this.btnQuitarFoto.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.btnQuitarFoto.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnQuitarFoto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuitarFoto.ForeColor = System.Drawing.Color.White;
            this.btnQuitarFoto.Location = new System.Drawing.Point(178, 632);
            this.btnQuitarFoto.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnQuitarFoto.Name = "btnQuitarFoto";
            this.btnQuitarFoto.Size = new System.Drawing.Size(158, 45);
            this.btnQuitarFoto.TabIndex = 19;
            this.btnQuitarFoto.Text = "Quitar foto";
            this.btnQuitarFoto.UseVisualStyleBackColor = false;
            this.btnQuitarFoto.Click += new System.EventHandler(this.btnQuitarFoto_Click);
            //
            // sexo
            //
            this.sexo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sexo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sexo.Items.AddRange(new object[] {
            "Masculino",
            "Femenino"});
            this.sexo.Location = new System.Drawing.Point(168, 382);
            this.sexo.Margin = new System.Windows.Forms.Padding(0, 4, 20, 10);
            this.sexo.Name = "sexo";
            this.sexo.Size = new System.Drawing.Size(166, 29);
            this.sexo.TabIndex = 7;
            this.sexo.SelectedIndexChanged += new System.EventHandler(this.sexo_SelectedIndexChanged);
            //
            // lblSexo
            //
            this.lblSexo.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblSexo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblSexo.Location = new System.Drawing.Point(15, 382);
            this.lblSexo.Margin = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.lblSexo.Name = "lblSexo";
            this.lblSexo.Size = new System.Drawing.Size(145, 32);
            this.lblSexo.TabIndex = 16;
            this.lblSexo.Text = "Sexo:";
            this.lblSexo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblCreadoPor
            //
            this.lblCreadoPor.Location = new System.Drawing.Point(15, 418);
            this.lblCreadoPor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCreadoPor.Name = "lblCreadoPor";
            this.lblCreadoPor.Size = new System.Drawing.Size(455, 25);
            this.lblCreadoPor.TabIndex = 17;
            this.lblCreadoPor.Text = "Creado por: Sin información";
            this.lblCreadoPor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblEstado
            //
            this.lblEstado.BackColor = System.Drawing.Color.Transparent;
            this.lblEstado.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblEstado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblEstado.Location = new System.Drawing.Point(0, 818);
            this.lblEstado.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Padding = new System.Windows.Forms.Padding(22, 0, 15, 0);
            this.lblEstado.Size = new System.Drawing.Size(1375, 32);
            this.lblEstado.TabIndex = 3;
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // contenedorContenido
            //
            this.contenedorContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contenedorContenido.Location = new System.Drawing.Point(0, 0);
            this.contenedorContenido.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.contenedorContenido.Name = "contenedorContenido";
            //
            // contenedorContenido.Panel1
            //
            this.contenedorContenido.Panel1.Controls.Add(this.lblListado);
            this.contenedorContenido.Panel1.Controls.Add(this.lblAyuda);
            this.contenedorContenido.Panel1.Controls.Add(this.buscador);
            this.contenedorContenido.Panel1.Controls.Add(this.lblFiltro);
            this.contenedorContenido.Panel1.Controls.Add(this.filtroEstado);
            this.contenedorContenido.Panel1.Controls.Add(this.tabla);
            this.contenedorContenido.Panel1MinSize = 240;
            //
            // contenedorContenido.Panel2
            //
            this.contenedorContenido.Panel2.Controls.Add(this.lblFormulario);
            this.contenedorContenido.Panel2.Controls.Add(this.lblNombre);
            this.contenedorContenido.Panel2.Controls.Add(this.nombre);
            this.contenedorContenido.Panel2.Controls.Add(this.lblApellido);
            this.contenedorContenido.Panel2.Controls.Add(this.apellido);
            this.contenedorContenido.Panel2.Controls.Add(this.lblDni);
            this.contenedorContenido.Panel2.Controls.Add(this.dni);
            this.contenedorContenido.Panel2.Controls.Add(this.lblFechaNacimiento);
            this.contenedorContenido.Panel2.Controls.Add(this.fechaNacimiento);
            this.contenedorContenido.Panel2.Controls.Add(this.lblNombreUsuario);
            this.contenedorContenido.Panel2.Controls.Add(this.nombreUsuario);
            this.contenedorContenido.Panel2.Controls.Add(this.lblClave);
            this.contenedorContenido.Panel2.Controls.Add(this.clave);
            this.contenedorContenido.Panel2.Controls.Add(this.lblSalario);
            this.contenedorContenido.Panel2.Controls.Add(this.salario);
            this.contenedorContenido.Panel2.Controls.Add(this.lblRol);
            this.contenedorContenido.Panel2.Controls.Add(this.rol);
            this.contenedorContenido.Panel2.Controls.Add(this.lblSexo);
            this.contenedorContenido.Panel2.Controls.Add(this.sexo);
            this.contenedorContenido.Panel2.Controls.Add(this.lblCreadoPor);
            this.contenedorContenido.Panel2.Controls.Add(this.nuevo);
            this.contenedorContenido.Panel2.Controls.Add(this.guardar);
            this.contenedorContenido.Panel2.Controls.Add(this.actualizar);
            this.contenedorContenido.Panel2.Controls.Add(this.darDeBaja);
            this.contenedorContenido.Panel2.Controls.Add(this.reactivar);
            this.contenedorContenido.Panel2.Controls.Add(this.fotoUsuario);
            this.contenedorContenido.Panel2.Controls.Add(this.btnSeleccionarFoto);
            this.contenedorContenido.Panel2.Controls.Add(this.btnQuitarFoto);
            this.contenedorContenido.Panel2MinSize = 360;
            this.contenedorContenido.Size = new System.Drawing.Size(1375, 818);
            this.contenedorContenido.SplitterDistance = 845;
            this.contenedorContenido.SplitterWidth = 20;
            this.contenedorContenido.TabIndex = 0;
            //
            // lblListado
            //
            this.lblListado.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblListado.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblListado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblListado.Location = new System.Drawing.Point(20, 15);
            this.lblListado.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblListado.Name = "lblListado";
            this.lblListado.Size = new System.Drawing.Size(775, 35);
            this.lblListado.TabIndex = 0;
            this.lblListado.Text = "Usuarios";
            this.lblListado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblAyuda
            //
            this.lblAyuda.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAyuda.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblAyuda.Location = new System.Drawing.Point(20, 52);
            this.lblAyuda.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAyuda.Name = "lblAyuda";
            this.lblAyuda.Size = new System.Drawing.Size(775, 30);
            this.lblAyuda.TabIndex = 1;
            this.lblAyuda.Text = "Busca por nombre, DNI o usuario";
            this.lblAyuda.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // buscador
            //
            this.buscador.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buscador.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.buscador.Location = new System.Drawing.Point(20, 92);
            this.buscador.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buscador.Name = "buscador";
            this.buscador.Size = new System.Drawing.Size(534, 29);
            this.buscador.TabIndex = 0;
            this.buscador.TextChanged += new System.EventHandler(this.buscador_TextChanged);
            //
            // lblFiltro
            //
            this.lblFiltro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFiltro.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblFiltro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblFiltro.Location = new System.Drawing.Point(562, 92);
            this.lblFiltro.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFiltro.Name = "lblFiltro";
            this.lblFiltro.Size = new System.Drawing.Size(65, 32);
            this.lblFiltro.TabIndex = 1;
            this.lblFiltro.Text = "Estado:";
            this.lblFiltro.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // filtroEstado
            //
            this.filtroEstado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.filtroEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filtroEstado.Items.AddRange(new object[] {
            "Todos",
            "Activos",
            "Inactivos"});
            this.filtroEstado.Location = new System.Drawing.Point(638, 92);
            this.filtroEstado.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.filtroEstado.Name = "filtroEstado";
            this.filtroEstado.Size = new System.Drawing.Size(184, 29);
            this.filtroEstado.TabIndex = 2;
            this.filtroEstado.SelectedIndexChanged += new System.EventHandler(this.filtroEstado_SelectedIndexChanged);
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
            this.tabla.ColumnHeadersHeight = 46;
            this.tabla.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colNombre,
            this.colDni,
            this.colUsuario,
            this.colRol,
            this.colSalario,
            this.colEstado});
            this.tabla.ContextMenuStrip = this.menuUsuarios;
            this.tabla.Location = new System.Drawing.Point(20, 142);
            this.tabla.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabla.MultiSelect = false;
            this.tabla.Name = "tabla";
            this.tabla.ReadOnly = true;
            this.tabla.RowHeadersVisible = false;
            this.tabla.RowHeadersWidth = 51;
            this.tabla.RowTemplate.Height = 30;
            this.tabla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tabla.Size = new System.Drawing.Size(802, 655);
            this.tabla.TabIndex = 3;
            this.tabla.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.tabla_CellMouseDown);
            this.tabla.SelectionChanged += new System.EventHandler(this.tabla_SelectionChanged);
            //
            // colId
            //
            this.colId.HeaderText = "Id";
            this.colId.MinimumWidth = 6;
            this.colId.Name = "colId";
            this.colId.ReadOnly = true;
            this.colId.Visible = false;
            //
            // colNombre
            //
            this.colNombre.HeaderText = "Nombre";
            this.colNombre.MinimumWidth = 6;
            this.colNombre.Name = "colNombre";
            this.colNombre.ReadOnly = true;
            //
            // colDni
            //
            this.colDni.HeaderText = "DNI";
            this.colDni.MinimumWidth = 6;
            this.colDni.Name = "colDni";
            this.colDni.ReadOnly = true;
            //
            // colUsuario
            //
            this.colUsuario.HeaderText = "Usuario";
            this.colUsuario.MinimumWidth = 6;
            this.colUsuario.Name = "colUsuario";
            this.colUsuario.ReadOnly = true;
            //
            // colRol
            //
            this.colRol.HeaderText = "Rol";
            this.colRol.MinimumWidth = 6;
            this.colRol.Name = "colRol";
            this.colRol.ReadOnly = true;
            //
            // colSalario
            //
            this.colSalario.HeaderText = "Salario";
            this.colSalario.MinimumWidth = 6;
            this.colSalario.Name = "colSalario";
            this.colSalario.ReadOnly = true;
            //
            // colEstado
            //
            this.colEstado.HeaderText = "Estado";
            this.colEstado.MinimumWidth = 6;
            this.colEstado.Name = "colEstado";
            this.colEstado.ReadOnly = true;
            //
            // menuUsuarios
            //
            this.menuUsuarios.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuUsuarios.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.darDeBajaMenu,
            this.reactivarMenu});
            this.menuUsuarios.Name = "menuUsuarios";
            this.menuUsuarios.Size = new System.Drawing.Size(157, 52);
            //
            // darDeBajaMenu
            //
            this.darDeBajaMenu.Name = "darDeBajaMenu";
            this.darDeBajaMenu.Size = new System.Drawing.Size(156, 24);
            this.darDeBajaMenu.Text = "Dar de baja";
            this.darDeBajaMenu.Click += new System.EventHandler(this.darDeBaja_Click);
            //
            // reactivarMenu
            //
            this.reactivarMenu.Name = "reactivarMenu";
            this.reactivarMenu.Size = new System.Drawing.Size(156, 24);
            this.reactivarMenu.Text = "Reactivar";
            this.reactivarMenu.Click += new System.EventHandler(this.reactivar_Click);
            //
            // lblFormulario
            //
            this.lblFormulario.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFormulario.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblFormulario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblFormulario.Location = new System.Drawing.Point(15, 15);
            this.lblFormulario.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFormulario.MinimumSize = new System.Drawing.Size(0, 40);
            this.lblFormulario.Name = "lblFormulario";
            this.lblFormulario.Size = new System.Drawing.Size(330, 40);
            this.lblFormulario.TabIndex = 0;
            this.lblFormulario.Text = "Nuevo usuario";
            this.lblFormulario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblNombre
            //
            this.lblNombre.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblNombre.Location = new System.Drawing.Point(15, 62);
            this.lblNombre.Margin = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(145, 32);
            this.lblNombre.TabIndex = 0;
            this.lblNombre.Text = "Nombre:";
            this.lblNombre.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // nombre
            //
            this.nombre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nombre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nombre.Location = new System.Drawing.Point(168, 62);
            this.nombre.Margin = new System.Windows.Forms.Padding(0, 4, 20, 10);
            this.nombre.MaxLength = 100;
            this.nombre.Name = "nombre";
            this.nombre.Size = new System.Drawing.Size(167, 29);
            this.nombre.TabIndex = 1;
            this.nombre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.nombre_KeyPress);
            //
            // lblApellido
            //
            this.lblApellido.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblApellido.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblApellido.Location = new System.Drawing.Point(15, 102);
            this.lblApellido.Margin = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.lblApellido.Name = "lblApellido";
            this.lblApellido.Size = new System.Drawing.Size(145, 32);
            this.lblApellido.TabIndex = 2;
            this.lblApellido.Text = "Apellido:";
            this.lblApellido.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // apellido
            //
            this.apellido.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.apellido.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.apellido.Location = new System.Drawing.Point(168, 102);
            this.apellido.Margin = new System.Windows.Forms.Padding(0, 4, 20, 10);
            this.apellido.MaxLength = 100;
            this.apellido.Name = "apellido";
            this.apellido.Size = new System.Drawing.Size(167, 29);
            this.apellido.TabIndex = 3;
            this.apellido.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.apellido_KeyPress);
            //
            // lblDni
            //
            this.lblDni.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblDni.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblDni.Location = new System.Drawing.Point(15, 142);
            this.lblDni.Margin = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.lblDni.Name = "lblDni";
            this.lblDni.Size = new System.Drawing.Size(145, 32);
            this.lblDni.TabIndex = 4;
            this.lblDni.Text = "DNI:";
            this.lblDni.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // dni
            //
            this.dni.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dni.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dni.Location = new System.Drawing.Point(168, 142);
            this.dni.Margin = new System.Windows.Forms.Padding(0, 4, 20, 10);
            this.dni.MaxLength = 20;
            this.dni.Name = "dni";
            this.dni.Size = new System.Drawing.Size(167, 29);
            this.dni.TabIndex = 5;
            this.dni.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dni_KeyPress);
            //
            // lblFechaNacimiento
            //
            this.lblFechaNacimiento.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblFechaNacimiento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblFechaNacimiento.Location = new System.Drawing.Point(15, 182);
            this.lblFechaNacimiento.Margin = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.lblFechaNacimiento.Name = "lblFechaNacimiento";
            this.lblFechaNacimiento.Size = new System.Drawing.Size(145, 32);
            this.lblFechaNacimiento.TabIndex = 6;
            this.lblFechaNacimiento.Text = "Nacimiento:";
            this.lblFechaNacimiento.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // fechaNacimiento
            //
            this.fechaNacimiento.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fechaNacimiento.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.fechaNacimiento.Location = new System.Drawing.Point(168, 182);
            this.fechaNacimiento.Margin = new System.Windows.Forms.Padding(0, 4, 20, 10);
            this.fechaNacimiento.Name = "fechaNacimiento";
            this.fechaNacimiento.Size = new System.Drawing.Size(166, 29);
            this.fechaNacimiento.TabIndex = 7;
            //
            // lblNombreUsuario
            //
            this.lblNombreUsuario.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblNombreUsuario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblNombreUsuario.Location = new System.Drawing.Point(15, 222);
            this.lblNombreUsuario.Margin = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.lblNombreUsuario.Name = "lblNombreUsuario";
            this.lblNombreUsuario.Size = new System.Drawing.Size(145, 32);
            this.lblNombreUsuario.TabIndex = 8;
            this.lblNombreUsuario.Text = "Usuario:";
            this.lblNombreUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // nombreUsuario
            //
            this.nombreUsuario.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nombreUsuario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nombreUsuario.Location = new System.Drawing.Point(168, 222);
            this.nombreUsuario.Margin = new System.Windows.Forms.Padding(0, 4, 20, 10);
            this.nombreUsuario.MaxLength = 50;
            this.nombreUsuario.Name = "nombreUsuario";
            this.nombreUsuario.Size = new System.Drawing.Size(167, 29);
            this.nombreUsuario.TabIndex = 9;
            //
            // lblClave
            //
            this.lblClave.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblClave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblClave.Location = new System.Drawing.Point(15, 262);
            this.lblClave.Margin = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.lblClave.Name = "lblClave";
            this.lblClave.Size = new System.Drawing.Size(145, 32);
            this.lblClave.TabIndex = 10;
            this.lblClave.Text = "Contraseña:";
            this.lblClave.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblClave.Click += new System.EventHandler(this.lblClave_Click);
            //
            // clave
            //
            this.clave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clave.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.clave.Location = new System.Drawing.Point(168, 262);
            this.clave.Margin = new System.Windows.Forms.Padding(0, 4, 20, 10);
            this.clave.MaxLength = 500;
            this.clave.Name = "clave";
            this.clave.Size = new System.Drawing.Size(167, 29);
            this.clave.TabIndex = 11;
            this.clave.UseSystemPasswordChar = true;
            //
            // lblSalario
            //
            this.lblSalario.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblSalario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblSalario.Location = new System.Drawing.Point(15, 302);
            this.lblSalario.Margin = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.lblSalario.Name = "lblSalario";
            this.lblSalario.Size = new System.Drawing.Size(145, 32);
            this.lblSalario.TabIndex = 12;
            this.lblSalario.Text = "Salario:";
            this.lblSalario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // salario
            //
            this.salario.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.salario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.salario.Location = new System.Drawing.Point(168, 302);
            this.salario.Margin = new System.Windows.Forms.Padding(0, 4, 20, 10);
            this.salario.Name = "salario";
            this.salario.Size = new System.Drawing.Size(167, 29);
            this.salario.TabIndex = 13;
            this.salario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.salario_KeyPress);
            //
            // lblRol
            //
            this.lblRol.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblRol.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblRol.Location = new System.Drawing.Point(15, 342);
            this.lblRol.Margin = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.lblRol.Name = "lblRol";
            this.lblRol.Size = new System.Drawing.Size(145, 32);
            this.lblRol.TabIndex = 14;
            this.lblRol.Text = "Rol:";
            this.lblRol.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // rol
            //
            this.rol.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.rol.Location = new System.Drawing.Point(168, 342);
            this.rol.Margin = new System.Windows.Forms.Padding(0, 4, 20, 10);
            this.rol.Name = "rol";
            this.rol.Size = new System.Drawing.Size(166, 29);
            this.rol.TabIndex = 15;
            //
            // nuevo
            //
            this.nuevo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.nuevo.FlatAppearance.BorderSize = 0;
            this.nuevo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(77)))), ((int)(((byte)(88)))));
            this.nuevo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(96)))), ((int)(((byte)(109)))));
            this.nuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nuevo.ForeColor = System.Drawing.Color.White;
            this.nuevo.Location = new System.Drawing.Point(15, 450);
            this.nuevo.Margin = new System.Windows.Forms.Padding(0, 0, 5, 8);
            this.nuevo.Name = "nuevo";
            this.nuevo.Size = new System.Drawing.Size(142, 42);
            this.nuevo.TabIndex = 0;
            this.nuevo.Text = "+ Nuevo";
            this.nuevo.UseVisualStyleBackColor = false;
            this.nuevo.Click += new System.EventHandler(this.nuevo_Click);
            //
            // guardar
            //
            this.guardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.guardar.FlatAppearance.BorderSize = 0;
            this.guardar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(75)))), ((int)(((byte)(31)))));
            this.guardar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(101)))), ((int)(((byte)(41)))));
            this.guardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.guardar.ForeColor = System.Drawing.Color.White;
            this.guardar.Location = new System.Drawing.Point(165, 450);
            this.guardar.Margin = new System.Windows.Forms.Padding(0, 0, 5, 8);
            this.guardar.Name = "guardar";
            this.guardar.Size = new System.Drawing.Size(142, 42);
            this.guardar.TabIndex = 1;
            this.guardar.Text = "Guardar";
            this.guardar.UseVisualStyleBackColor = false;
            this.guardar.Click += new System.EventHandler(this.guardar_Click);
            //
            // actualizar
            //
            this.actualizar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(75)))), ((int)(((byte)(138)))));
            this.actualizar.FlatAppearance.BorderSize = 0;
            this.actualizar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(48)))), ((int)(((byte)(89)))));
            this.actualizar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(61)))), ((int)(((byte)(112)))));
            this.actualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.actualizar.ForeColor = System.Drawing.Color.White;
            this.actualizar.Location = new System.Drawing.Point(315, 450);
            this.actualizar.Margin = new System.Windows.Forms.Padding(0, 0, 5, 8);
            this.actualizar.Name = "actualizar";
            this.actualizar.Size = new System.Drawing.Size(142, 42);
            this.actualizar.TabIndex = 2;
            this.actualizar.Text = "Actualizar";
            this.actualizar.UseVisualStyleBackColor = false;
            //
            // darDeBaja
            //
            this.darDeBaja.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.darDeBaja.FlatAppearance.BorderSize = 0;
            this.darDeBaja.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.darDeBaja.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.darDeBaja.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.darDeBaja.ForeColor = System.Drawing.Color.White;
            this.darDeBaja.Location = new System.Drawing.Point(15, 500);
            this.darDeBaja.Margin = new System.Windows.Forms.Padding(0, 0, 5, 8);
            this.darDeBaja.Name = "darDeBaja";
            this.darDeBaja.Size = new System.Drawing.Size(142, 42);
            this.darDeBaja.TabIndex = 3;
            this.darDeBaja.Text = "Dar de baja";
            this.darDeBaja.UseVisualStyleBackColor = false;
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
            this.reactivar.Location = new System.Drawing.Point(165, 500);
            this.reactivar.Margin = new System.Windows.Forms.Padding(0, 0, 5, 8);
            this.reactivar.Name = "reactivar";
            this.reactivar.Size = new System.Drawing.Size(142, 42);
            this.reactivar.TabIndex = 4;
            this.reactivar.Text = "Reactivar";
            this.reactivar.UseVisualStyleBackColor = false;
            this.reactivar.Click += new System.EventHandler(this.reactivar_Click);
            //
            // GestionUsuariosFormulario
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1375, 850);
            this.Controls.Add(this.contenedorContenido);
            this.Controls.Add(this.lblEstado);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(1120, 688);
            this.Name = "GestionUsuariosFormulario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SysGym";
            this.Load += new System.EventHandler(this.GestionUsuariosFormulario_Load);
            ((System.ComponentModel.ISupportInitialize)(this.indicadorErrores)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fotoUsuario)).EndInit();
            this.contenedorContenido.Panel1.ResumeLayout(false);
            this.contenedorContenido.Panel1.PerformLayout();
            this.contenedorContenido.Panel2.ResumeLayout(false);
            this.contenedorContenido.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.contenedorContenido)).EndInit();
            this.contenedorContenido.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).EndInit();
            this.menuUsuarios.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
