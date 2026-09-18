using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Administrador
{
    partial class GestionUsuariosFormulario
    {
        private System.Windows.Forms.PictureBox fotoUsuario;
        private System.Windows.Forms.Button btnSeleccionarFoto;
        private System.Windows.Forms.Button btnQuitarFoto;
        private System.Windows.Forms.ComboBox sexo;
        private System.Windows.Forms.Label lblSexo;

        private IContainer components = null;
        private Panel panelEncabezado;
        private Label lblTitulo;
        private Label lblDescripcion;
        private Button btnVolver;
        private Panel barraAcciones;
        private Label lblEstado;
        private Panel panelContenido;
        private Panel contenedorContenido;
        private Panel panelListado;
        private Label lblListado;
        private Label lblAyuda;
        private Label lblFiltro;
        private Panel panelFiltro;
        private Panel panelDetalle;
        private Panel contenedorDetalle;
        private Label lblFormulario;
        private Panel contenedorCampos;
        private Panel panelAcciones;
        private Label lblNombre;
        private Label lblApellido;
        private Label lblDni;
        private Label lblNombreUsuario;
        private Label lblClave;
        private Label lblSalario;
        private Label lblRol;
        private TextBox nombre;
        private TextBox apellido;
        private TextBox dni;
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
            if (liberarRecursos && components != null) components.Dispose();
            base.Dispose(liberarRecursos);
        }

        private void InitializeComponent()
        {
            this.fotoUsuario = new System.Windows.Forms.PictureBox();
            this.btnSeleccionarFoto = new System.Windows.Forms.Button();
            this.btnQuitarFoto = new System.Windows.Forms.Button();
            this.sexo = new System.Windows.Forms.ComboBox();
            this.lblSexo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.fotoUsuario)).BeginInit();
            this.panelEncabezado = new System.Windows.Forms.Panel();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.btnVolver = new System.Windows.Forms.Button();
            this.barraAcciones = new System.Windows.Forms.Panel();
            this.lblEstado = new System.Windows.Forms.Label();
            this.panelContenido = new System.Windows.Forms.Panel();
            this.contenedorContenido = new System.Windows.Forms.Panel();
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
            this.contenedorDetalle = new System.Windows.Forms.Panel();
            this.lblFormulario = new System.Windows.Forms.Label();
            this.contenedorCampos = new System.Windows.Forms.Panel();
            this.lblNombre = new System.Windows.Forms.Label();
            this.nombre = new System.Windows.Forms.TextBox();
            this.lblApellido = new System.Windows.Forms.Label();
            this.apellido = new System.Windows.Forms.TextBox();
            this.lblDni = new System.Windows.Forms.Label();
            this.dni = new System.Windows.Forms.TextBox();
            this.lblNombreUsuario = new System.Windows.Forms.Label();
            this.nombreUsuario = new System.Windows.Forms.TextBox();
            this.lblClave = new System.Windows.Forms.Label();
            this.clave = new System.Windows.Forms.TextBox();
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
            this.contenedorContenido.SuspendLayout();
            this.panelListado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).BeginInit();
            this.panelFiltro.SuspendLayout();
            this.panelDetalle.SuspendLayout();
            this.contenedorDetalle.SuspendLayout();
            this.contenedorCampos.SuspendLayout();
            this.panelAcciones.SuspendLayout();
            this.SuspendLayout();
            //
            // panelEncabezado
            //
            this.panelEncabezado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.panelEncabezado.Controls.Add(this.lblDescripcion);
            this.panelEncabezado.Controls.Add(this.lblTitulo);
            this.panelEncabezado.Controls.Add(this.btnVolver);

            this.panelEncabezado.Name = "panelEncabezado";

            this.panelEncabezado.TabIndex = 0;
            //
            // lblDescripcion
            //

            this.lblDescripcion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));

            this.lblDescripcion.Name = "lblDescripcion";

            this.lblDescripcion.TabIndex = 1;
            this.lblDescripcion.Text = "Administracion del personal y sus permisos";
            //
            // lblTitulo
            //

            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;

            this.lblTitulo.Name = "lblTitulo";

            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Usuarios y roles";
            //
            // btnVolver
            //

            this.btnVolver.BackColor = System.Drawing.Color.White;
            this.btnVolver.FlatAppearance.BorderSize = 0;
            this.btnVolver.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVolver.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));

            this.btnVolver.Name = "btnVolver";

            this.btnVolver.TabIndex = 2;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = false;
            //
            // barraAcciones
            //
            this.barraAcciones.BackColor = System.Drawing.Color.White;

            this.barraAcciones.Name = "barraAcciones";
            this.barraAcciones.Padding = new System.Windows.Forms.Padding(16, 8, 16, 8);

            this.barraAcciones.TabIndex = 1;

            //
            // lblEstado
            //
            this.lblEstado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));

            this.lblEstado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));

            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Padding = new System.Windows.Forms.Padding(18, 8, 8, 0);

            this.lblEstado.TabIndex = 3;
            this.lblEstado.Text = "Listo";
            //
            // panelContenido
            //
            this.panelContenido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelContenido.Controls.Add(this.contenedorContenido);

            this.panelContenido.Name = "panelContenido";
            this.panelContenido.Padding = new System.Windows.Forms.Padding(12);

            this.panelContenido.TabIndex = 2;
            //
            // contenedorContenido
            //

            this.contenedorContenido.Controls.Add(this.panelListado);
            this.contenedorContenido.Controls.Add(this.panelDetalle);

            this.contenedorContenido.Name = "contenedorContenido";

            this.contenedorContenido.TabIndex = 0;
            //
            // panelListado
            //
            this.panelListado.BackColor = System.Drawing.Color.White;
            this.panelListado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelListado.Controls.Add(this.tabla);
            this.panelListado.Controls.Add(this.panelFiltro);
            this.panelListado.Controls.Add(this.lblAyuda);
            this.panelListado.Controls.Add(this.lblListado);

            this.panelListado.Name = "panelListado";
            this.panelListado.Padding = new System.Windows.Forms.Padding(16);

            this.panelListado.TabIndex = 0;
            //
            // tabla
            //

            this.tabla.BackgroundColor = System.Drawing.Color.White;
            this.tabla.BorderStyle = System.Windows.Forms.BorderStyle.None;

            this.tabla.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colNombre,
            this.colDni,
            this.colUsuario,
            this.colRol,
            this.colSalario,
            this.colEstado});

            this.tabla.Name = "tabla";

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

            this.panelFiltro.Controls.Add(this.filtroEstado);
            this.panelFiltro.Controls.Add(this.lblFiltro);
            this.panelFiltro.Controls.Add(this.buscador);

            this.panelFiltro.Name = "panelFiltro";

            this.panelFiltro.TabIndex = 2;
            //
            // filtroEstado
            //
            this.filtroEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filtroEstado.Items.AddRange(new object[] {
            "Todos",
            "Activos",
            "Inactivos"});

            this.filtroEstado.Name = "filtroEstado";

            this.filtroEstado.TabIndex = 2;
            //
            // lblFiltro
            //

            this.lblFiltro.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblFiltro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));

            this.lblFiltro.Name = "lblFiltro";

            this.lblFiltro.TabIndex = 1;
            this.lblFiltro.Text = "Estado:";
            //
            // buscador
            //

            this.buscador.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            this.buscador.Name = "buscador";

            this.buscador.TabIndex = 0;
            //
            // lblAyuda
            //

            this.lblAyuda.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));

            this.lblAyuda.Name = "lblAyuda";

            this.lblAyuda.TabIndex = 1;
            this.lblAyuda.Text = "Busca por nombre, DNI o usuario";
            //
            // lblListado
            //

            this.lblListado.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblListado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));

            this.lblListado.Name = "lblListado";

            this.lblListado.TabIndex = 0;
            this.lblListado.Text = "Usuarios";
            //
            // panelDetalle
            //
            this.panelDetalle.BackColor = System.Drawing.Color.White;
            this.panelDetalle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDetalle.Controls.Add(this.contenedorDetalle);

            this.panelDetalle.Name = "panelDetalle";
            this.panelDetalle.Padding = new System.Windows.Forms.Padding(16);

            this.panelDetalle.TabIndex = 1;
            //
            // contenedorDetalle
            //

            this.contenedorDetalle.Controls.Add(this.lblFormulario);
            this.contenedorDetalle.Controls.Add(this.contenedorCampos);
            this.contenedorDetalle.Controls.Add(this.panelAcciones);

            this.contenedorDetalle.Name = "contenedorDetalle";

            this.contenedorDetalle.TabIndex = 0;
            //
            // lblFormulario
            //

            this.lblFormulario.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblFormulario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));

            this.lblFormulario.Name = "lblFormulario";

            this.lblFormulario.TabIndex = 0;
            this.lblFormulario.Text = "Nuevo usuario";
            //
            // contenedorCampos
            //

            this.contenedorCampos.Controls.Add(this.lblNombre);
            this.contenedorCampos.Controls.Add(this.nombre);
            this.contenedorCampos.Controls.Add(this.lblApellido);
            this.contenedorCampos.Controls.Add(this.apellido);
            this.contenedorCampos.Controls.Add(this.lblDni);
            this.contenedorCampos.Controls.Add(this.dni);
            this.contenedorCampos.Controls.Add(this.lblNombreUsuario);
            this.contenedorCampos.Controls.Add(this.nombreUsuario);
            this.contenedorCampos.Controls.Add(this.lblClave);
            this.contenedorCampos.Controls.Add(this.clave);
            this.contenedorCampos.Controls.Add(this.lblSalario);
            this.contenedorCampos.Controls.Add(this.salario);
            this.contenedorCampos.Controls.Add(this.lblRol);
            this.contenedorCampos.Controls.Add(this.rol);

            this.contenedorCampos.Name = "contenedorCampos";

            this.contenedorCampos.TabIndex = 1;
            //
            // lblNombre
            //

            this.lblNombre.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));

            this.lblNombre.Name = "lblNombre";

            this.lblNombre.TabIndex = 0;
            this.lblNombre.Text = "Nombre:";
            //
            // nombre
            //
            this.nombre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            this.nombre.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.nombre.Name = "nombre";

            this.nombre.TabIndex = 1;
            //
            // lblApellido
            //

            this.lblApellido.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblApellido.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));

            this.lblApellido.Name = "lblApellido";

            this.lblApellido.TabIndex = 2;
            this.lblApellido.Text = "Apellido:";
            //
            // apellido
            //
            this.apellido.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            this.apellido.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.apellido.Name = "apellido";

            this.apellido.TabIndex = 3;
            //
            // lblDni
            //

            this.lblDni.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblDni.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));

            this.lblDni.Name = "lblDni";

            this.lblDni.TabIndex = 4;
            this.lblDni.Text = "DNI:";
            //
            // dni
            //
            this.dni.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            this.dni.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.dni.Name = "dni";

            this.dni.TabIndex = 5;
            //
            // lblUsername
            //

            this.lblNombreUsuario.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblNombreUsuario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));

            this.lblNombreUsuario.Name = "lblNombreUsuario";

            this.lblNombreUsuario.TabIndex = 6;
            this.lblNombreUsuario.Text = "Usuario:";
            //
            // username
            //
            this.nombreUsuario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            this.nombreUsuario.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.nombreUsuario.Name = "nombreUsuario";

            this.nombreUsuario.TabIndex = 7;
            //
            // lblPassword
            //

            this.lblClave.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblClave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));

            this.lblClave.Name = "lblClave";

            this.lblClave.TabIndex = 8;
            this.lblClave.Text = "Contrasena:";

            //
            // password
            //
            this.clave.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            this.clave.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.clave.Name = "clave";

            this.clave.TabIndex = 9;
            this.clave.UseSystemPasswordChar = true;
            //
            // lblSalario
            //

            this.lblSalario.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblSalario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));

            this.lblSalario.Name = "lblSalario";

            this.lblSalario.TabIndex = 10;
            this.lblSalario.Text = "Salario:";
            //
            // salario
            //
            this.salario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            this.salario.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.salario.Name = "salario";

            this.salario.TabIndex = 11;
            //
            // lblRol
            //

            this.lblRol.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblRol.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));

            this.lblRol.Name = "lblRol";

            this.lblRol.TabIndex = 12;
            this.lblRol.Text = "Rol:";
            //
            // rol
            //

            this.rol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.rol.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.rol.Name = "rol";

            this.rol.TabIndex = 13;
            //
            // panelAcciones
            //
            this.panelAcciones.Controls.Add(this.nuevo);
            this.panelAcciones.Controls.Add(this.guardar);
            this.panelAcciones.Controls.Add(this.actualizar);
            this.panelAcciones.Controls.Add(this.darDeBaja);
            this.panelAcciones.Controls.Add(this.reactivar);

            this.panelAcciones.Name = "panelAcciones";

            this.panelAcciones.TabIndex = 2;
            //
            // nuevo
            //
            this.nuevo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.nuevo.FlatAppearance.BorderSize = 0;
            this.nuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nuevo.ForeColor = System.Drawing.Color.White;

            this.nuevo.Name = "nuevo";

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

            this.guardar.Name = "guardar";

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

            this.actualizar.Name = "actualizar";

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

            this.darDeBaja.Name = "darDeBaja";

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

            this.reactivar.Name = "reactivar";

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
            this.MinimumSize = new System.Drawing.Size(900, 560);
            this.Name = "GestionUsuariosFormulario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SysGym | Usuarios y roles";

            // Encabezado: titulo y descripcion a la izquierda, accion de regreso a la derecha.

            this.panelEncabezado.Padding = new Padding(22, 8, 22, 8);

            this.lblTitulo.Margin = new Padding(0);
            this.lblTitulo.TextAlign = ContentAlignment.BottomLeft;

            this.lblDescripcion.Margin = new Padding(0);
            this.lblDescripcion.TextAlign = ContentAlignment.TopLeft;

            this.btnVolver.Margin = new Padding(16, 0, 0, 0);
            // Sin acciones propias: la barra queda como separador del encabezado.

            this.barraAcciones.Padding = new Padding(0);
            this.barraAcciones.BackColor = Color.FromArgb(203, 213, 225);
            // Barra de estado inferior.

            this.lblEstado.Padding = new Padding(18, 0, 12, 0);
            this.lblEstado.TextAlign = ContentAlignment.MiddleLeft;
            // Area de trabajo: listado que crece y panel de alta o edicion de ancho fijo.

            this.panelContenido.Padding = new Padding(16);

            this.panelListado.Margin = new Padding(0, 0, 16, 0);
            this.panelListado.Padding = new Padding(16);

            this.lblListado.Margin = new Padding(0);
            this.lblListado.TextAlign = ContentAlignment.MiddleLeft;

            this.lblAyuda.Margin = new Padding(0, 0, 0, 6);
            this.lblAyuda.TextAlign = ContentAlignment.MiddleLeft;

            this.panelFiltro.Margin = new Padding(0, 0, 0, 10);

            this.buscador.Margin = new Padding(0, 8, 12, 8);

            this.lblFiltro.Margin = new Padding(0, 0, 8, 0);
            this.lblFiltro.TextAlign = ContentAlignment.MiddleRight;

            this.filtroEstado.Margin = new Padding(0, 8, 0, 8);

            this.tabla.Margin = new Padding(0);
            this.tabla.RowTemplate.Height = 30;
            // Detalle: se desplaza solo cuando la altura disponible no alcanza.

            this.panelDetalle.Margin = new Padding(0);
            this.panelDetalle.Padding = new Padding(16);

            this.lblFormulario.Margin = new Padding(0, 0, 0, 12);
            this.lblFormulario.TextAlign = ContentAlignment.MiddleLeft;

            this.lblNombre.Margin = new Padding(0, 0, 8, 8);
            this.lblNombre.TextAlign = ContentAlignment.MiddleLeft;

            this.nombre.Margin = new Padding(0, 3, 16, 8);

            this.lblApellido.Margin = new Padding(0, 0, 8, 8);
            this.lblApellido.TextAlign = ContentAlignment.MiddleLeft;

            this.apellido.Margin = new Padding(0, 3, 16, 8);

            this.lblDni.Margin = new Padding(0, 0, 8, 8);
            this.lblDni.TextAlign = ContentAlignment.MiddleLeft;

            this.dni.Margin = new Padding(0, 3, 16, 8);

            this.lblNombreUsuario.Margin = new Padding(0, 0, 8, 8);
            this.lblNombreUsuario.TextAlign = ContentAlignment.MiddleLeft;

            this.nombreUsuario.Margin = new Padding(0, 3, 16, 8);

            this.lblClave.Margin = new Padding(0, 0, 8, 8);
            this.lblClave.TextAlign = ContentAlignment.MiddleLeft;

            this.clave.Margin = new Padding(0, 3, 16, 8);

            this.lblSalario.Margin = new Padding(0, 0, 8, 8);
            this.lblSalario.TextAlign = ContentAlignment.MiddleLeft;

            this.salario.Margin = new Padding(0, 3, 16, 8);

            this.lblRol.Margin = new Padding(0, 0, 8, 8);
            this.lblRol.TextAlign = ContentAlignment.MiddleLeft;

            this.rol.Margin = new Padding(0, 3, 16, 8);
            this.contenedorCampos.Margin = new Padding(0, 0, 0, 16);

            this.panelAcciones.Margin = new Padding(0);

            this.nuevo.Margin = new Padding(0, 0, 8, 8);

            this.guardar.Margin = new Padding(0, 0, 8, 8);

            this.actualizar.Margin = new Padding(0, 0, 8, 8);

            this.darDeBaja.Margin = new Padding(0, 0, 8, 8);

            this.reactivar.Margin = new Padding(0, 0, 8, 8);

            this.AutoScroll = false;
            this.panelEncabezado.AutoSize = false;
            this.panelEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEncabezado.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.panelEncabezado.Location = new System.Drawing.Point(0, 0);
            this.panelEncabezado.Size = new System.Drawing.Size(1100, 84);
            this.panelEncabezado.AutoScroll = false;
            this.lblTitulo.AutoSize = false;
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.None;
            this.lblTitulo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.lblTitulo.Location = new System.Drawing.Point(22, 8);
            this.lblTitulo.Size = new System.Drawing.Size(890, 36);
            this.lblDescripcion.AutoSize = false;
            this.lblDescripcion.Dock = System.Windows.Forms.DockStyle.None;
            this.lblDescripcion.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
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
            this.barraAcciones.Size = new System.Drawing.Size(1100, 1);
            this.barraAcciones.AutoScroll = false;
            this.lblEstado.AutoSize = false;
            this.lblEstado.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblEstado.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblEstado.Location = new System.Drawing.Point(0, 650);
            this.lblEstado.Size = new System.Drawing.Size(1100, 30);
            this.panelContenido.AutoSize = false;
            this.panelContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenido.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.panelContenido.Location = new System.Drawing.Point(0, 85);
            this.panelContenido.Size = new System.Drawing.Size(1100, 565);
            this.panelContenido.AutoScroll = false;
            this.contenedorContenido.AutoSize = false;
            this.contenedorContenido.Dock = System.Windows.Forms.DockStyle.None;
            this.contenedorContenido.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.contenedorContenido.Location = new System.Drawing.Point(16, 16);
            this.contenedorContenido.Size = new System.Drawing.Size(1068, 533);
            this.contenedorContenido.AutoScroll = false;
            this.panelListado.AutoSize = false;
            this.panelListado.Dock = System.Windows.Forms.DockStyle.None;
            this.panelListado.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.panelListado.Location = new System.Drawing.Point(0, 0);
            this.panelListado.Size = new System.Drawing.Size(656, 533);
            this.panelListado.AutoScroll = false;
            this.lblListado.AutoSize = false;
            this.lblListado.Dock = System.Windows.Forms.DockStyle.None;
            this.lblListado.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.lblListado.Location = new System.Drawing.Point(16, 16);
            this.lblListado.Size = new System.Drawing.Size(622, 28);
            this.lblAyuda.AutoSize = false;
            this.lblAyuda.Dock = System.Windows.Forms.DockStyle.None;
            this.lblAyuda.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.lblAyuda.Location = new System.Drawing.Point(16, 44);
            this.lblAyuda.Size = new System.Drawing.Size(622, 24);
            this.lblFiltro.AutoSize = false;
            this.lblFiltro.Dock = System.Windows.Forms.DockStyle.None;
            this.lblFiltro.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.lblFiltro.Location = new System.Drawing.Point(424, 10);
            this.lblFiltro.Size = new System.Drawing.Size(46, 24);
            this.panelFiltro.AutoSize = false;
            this.panelFiltro.Dock = System.Windows.Forms.DockStyle.None;
            this.panelFiltro.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.panelFiltro.Location = new System.Drawing.Point(16, 72);
            this.panelFiltro.Size = new System.Drawing.Size(622, 42);
            this.panelFiltro.AutoScroll = false;
            this.panelDetalle.AutoSize = false;
            this.panelDetalle.Dock = System.Windows.Forms.DockStyle.None;
            this.panelDetalle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this.panelDetalle.Location = new System.Drawing.Point(672, 0);
            this.panelDetalle.Size = new System.Drawing.Size(396, 533);
            this.panelDetalle.AutoScroll = true;
            this.contenedorDetalle.AutoSize = false;
            this.contenedorDetalle.Dock = System.Windows.Forms.DockStyle.None;
            this.contenedorDetalle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.contenedorDetalle.Location = new System.Drawing.Point(16, 16);
            this.contenedorDetalle.Size = new System.Drawing.Size(362, 476);
            this.contenedorDetalle.AutoScroll = false;
            this.lblFormulario.AutoSize = false;
            this.lblFormulario.Dock = System.Windows.Forms.DockStyle.None;
            this.lblFormulario.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblFormulario.Location = new System.Drawing.Point(0, 0);
            this.lblFormulario.Size = new System.Drawing.Size(362, 32);
            this.contenedorCampos.AutoSize = false;
            this.contenedorCampos.Dock = System.Windows.Forms.DockStyle.None;
            this.contenedorCampos.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.contenedorCampos.Location = new System.Drawing.Point(0, 42);
            this.contenedorCampos.Size = new System.Drawing.Size(362, 310);
            this.contenedorCampos.AutoScroll = false;
            this.panelAcciones.AutoSize = false;
            this.panelAcciones.Dock = System.Windows.Forms.DockStyle.None;
            this.panelAcciones.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.panelAcciones.Location = new System.Drawing.Point(0, 368);
            this.panelAcciones.Size = new System.Drawing.Size(362, 100);
            this.panelAcciones.AutoScroll = false;
            this.lblNombre.AutoSize = false;
            this.lblNombre.Dock = System.Windows.Forms.DockStyle.None;
            this.lblNombre.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblNombre.Location = new System.Drawing.Point(0, 0);
            this.lblNombre.Size = new System.Drawing.Size(116, 30);
            this.lblApellido.AutoSize = false;
            this.lblApellido.Dock = System.Windows.Forms.DockStyle.None;
            this.lblApellido.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblApellido.Location = new System.Drawing.Point(0, 38);
            this.lblApellido.Size = new System.Drawing.Size(116, 30);
            this.lblDni.AutoSize = false;
            this.lblDni.Dock = System.Windows.Forms.DockStyle.None;
            this.lblDni.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblDni.Location = new System.Drawing.Point(0, 76);
            this.lblDni.Size = new System.Drawing.Size(116, 30);
            this.lblNombreUsuario.AutoSize = false;
            this.lblNombreUsuario.Dock = System.Windows.Forms.DockStyle.None;
            this.lblNombreUsuario.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblNombreUsuario.Location = new System.Drawing.Point(0, 114);
            this.lblNombreUsuario.Size = new System.Drawing.Size(116, 30);
            this.lblClave.AutoSize = false;
            this.lblClave.Dock = System.Windows.Forms.DockStyle.None;
            this.lblClave.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblClave.Location = new System.Drawing.Point(0, 152);
            this.lblClave.Size = new System.Drawing.Size(116, 30);
            this.lblSalario.AutoSize = false;
            this.lblSalario.Dock = System.Windows.Forms.DockStyle.None;
            this.lblSalario.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblSalario.Location = new System.Drawing.Point(0, 190);
            this.lblSalario.Size = new System.Drawing.Size(116, 30);
            this.lblRol.AutoSize = false;
            this.lblRol.Dock = System.Windows.Forms.DockStyle.None;
            this.lblRol.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblRol.Location = new System.Drawing.Point(0, 228);
            this.lblRol.Size = new System.Drawing.Size(116, 30);
            this.nombre.AutoSize = false;
            this.nombre.Dock = System.Windows.Forms.DockStyle.None;
            this.nombre.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.nombre.Location = new System.Drawing.Point(124, 4);
            this.nombre.Size = new System.Drawing.Size(222, 24);
            this.apellido.AutoSize = false;
            this.apellido.Dock = System.Windows.Forms.DockStyle.None;
            this.apellido.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.apellido.Location = new System.Drawing.Point(124, 42);
            this.apellido.Size = new System.Drawing.Size(222, 24);
            this.dni.AutoSize = false;
            this.dni.Dock = System.Windows.Forms.DockStyle.None;
            this.dni.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.dni.Location = new System.Drawing.Point(124, 80);
            this.dni.Size = new System.Drawing.Size(222, 24);
            this.nombreUsuario.AutoSize = false;
            this.nombreUsuario.Dock = System.Windows.Forms.DockStyle.None;
            this.nombreUsuario.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.nombreUsuario.Location = new System.Drawing.Point(124, 118);
            this.nombreUsuario.Size = new System.Drawing.Size(222, 24);
            this.clave.AutoSize = false;
            this.clave.Dock = System.Windows.Forms.DockStyle.None;
            this.clave.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.clave.Location = new System.Drawing.Point(124, 156);
            this.clave.Size = new System.Drawing.Size(222, 24);
            this.salario.AutoSize = false;
            this.salario.Dock = System.Windows.Forms.DockStyle.None;
            this.salario.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.salario.Location = new System.Drawing.Point(124, 194);
            this.salario.Size = new System.Drawing.Size(222, 24);
            this.rol.AutoSize = false;
            this.rol.Dock = System.Windows.Forms.DockStyle.None;
            this.rol.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.rol.Location = new System.Drawing.Point(124, 234);
            this.rol.Size = new System.Drawing.Size(222, 25);
            this.nuevo.AutoSize = false;
            this.nuevo.Dock = System.Windows.Forms.DockStyle.None;
            this.nuevo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.nuevo.Location = new System.Drawing.Point(0, 0);
            this.nuevo.Size = new System.Drawing.Size(112, 38);
            this.guardar.AutoSize = false;
            this.guardar.Dock = System.Windows.Forms.DockStyle.None;
            this.guardar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.guardar.Location = new System.Drawing.Point(120, 0);
            this.guardar.Size = new System.Drawing.Size(112, 38);
            this.actualizar.AutoSize = false;
            this.actualizar.Dock = System.Windows.Forms.DockStyle.None;
            this.actualizar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.actualizar.Location = new System.Drawing.Point(240, 0);
            this.actualizar.Size = new System.Drawing.Size(112, 38);
            this.darDeBaja.AutoSize = false;
            this.darDeBaja.Dock = System.Windows.Forms.DockStyle.None;
            this.darDeBaja.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.darDeBaja.Location = new System.Drawing.Point(0, 46);
            this.darDeBaja.Size = new System.Drawing.Size(112, 38);
            this.reactivar.AutoSize = false;
            this.reactivar.Dock = System.Windows.Forms.DockStyle.None;
            this.reactivar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.reactivar.Location = new System.Drawing.Point(120, 46);
            this.reactivar.Size = new System.Drawing.Size(112, 38);
            this.filtroEstado.AutoSize = false;
            this.filtroEstado.Dock = System.Windows.Forms.DockStyle.None;
            this.filtroEstado.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.filtroEstado.Location = new System.Drawing.Point(474, 8);
            this.filtroEstado.Size = new System.Drawing.Size(148, 26);
            this.buscador.AutoSize = false;
            this.buscador.Dock = System.Windows.Forms.DockStyle.None;
            this.buscador.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.buscador.Location = new System.Drawing.Point(0, 9);
            this.buscador.Size = new System.Drawing.Size(412, 26);
            this.tabla.AutoSize = false;
            this.tabla.Dock = System.Windows.Forms.DockStyle.None;
            this.tabla.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.tabla.Location = new System.Drawing.Point(16, 124);
            this.tabla.Size = new System.Drawing.Size(622, 391);
            this.tabla.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tabla.ReadOnly = true;
            this.tabla.AllowUserToAddRows = false;
            this.tabla.AllowUserToDeleteRows = false;
            this.tabla.AllowUserToResizeRows = false;
            this.tabla.RowHeadersVisible = false;
            this.tabla.MultiSelect = false;
            this.tabla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tabla.ColumnHeadersHeight = 46;

            this.contenedorCampos.Size = new System.Drawing.Size(362, 310);
            this.panelAcciones.Location = new System.Drawing.Point(0, 368);
            this.contenedorDetalle.Size = new System.Drawing.Size(362, 476);
            this.panelDetalle.Controls.Add(this.fotoUsuario);
            this.panelDetalle.Controls.Add(this.btnSeleccionarFoto);
            this.panelDetalle.Controls.Add(this.btnQuitarFoto);
            this.contenedorCampos.Controls.Add(this.lblSexo);
            this.contenedorCampos.Controls.Add(this.sexo);
            this.fotoUsuario.Name = "fotoUsuario";
            this.fotoUsuario.Location = new System.Drawing.Point(16, 492);
            this.fotoUsuario.Size = new System.Drawing.Size(120, 120);
            this.fotoUsuario.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.fotoUsuario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fotoUsuario.BackColor = System.Drawing.Color.LightGray;
            this.fotoUsuario.TabStop = false;
            this.fotoUsuario.Disposed += new System.EventHandler(this.fotoUsuario_Disposed);
            this.btnSeleccionarFoto.Name = "btnSeleccionarFoto";
            this.btnSeleccionarFoto.Text = "Seleccionar foto";
            this.btnSeleccionarFoto.Location = new System.Drawing.Point(152, 492);
            this.btnSeleccionarFoto.Size = new System.Drawing.Size(180, 36);
            this.btnSeleccionarFoto.Click += new System.EventHandler(this.btnSeleccionarFoto_Click);
            this.btnQuitarFoto.Name = "btnQuitarFoto";
            this.btnQuitarFoto.Text = "Quitar foto";
            this.btnQuitarFoto.Location = new System.Drawing.Point(152, 536);
            this.btnQuitarFoto.Size = new System.Drawing.Size(180, 36);
            this.btnQuitarFoto.Click += new System.EventHandler(this.btnQuitarFoto_Click);
            this.lblSexo.Name = "lblSexo";
            this.lblSexo.Text = "Sexo:";
            this.lblSexo.Location = new System.Drawing.Point(0, 272);
            this.lblSexo.Size = new System.Drawing.Size(116, 28);
            this.sexo.Name = "sexo";
            this.sexo.Location = new System.Drawing.Point(124, 272);
            this.sexo.Size = new System.Drawing.Size(222, 28);
            this.sexo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sexo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.sexo.Items.AddRange(new object[] { "Masculino", "Femenino" });
            this.sexo.SelectedIndex = -1;
            this.sexo.TabIndex = 7;
            this.sexo.SelectedIndexChanged += new System.EventHandler(this.sexo_SelectedIndexChanged);
            ((System.ComponentModel.ISupportInitialize)(this.fotoUsuario)).EndInit();

            this.panelEncabezado.ResumeLayout(false);
            this.panelEncabezado.PerformLayout();
            this.panelContenido.ResumeLayout(false);
            this.contenedorContenido.ResumeLayout(false);
            this.panelListado.ResumeLayout(false);
            this.panelListado.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).EndInit();
            this.panelFiltro.ResumeLayout(false);
            this.panelFiltro.PerformLayout();
            this.panelDetalle.ResumeLayout(false);
            this.contenedorDetalle.ResumeLayout(false);
            this.contenedorDetalle.PerformLayout();
            this.contenedorCampos.ResumeLayout(false);
            this.contenedorCampos.PerformLayout();
            this.panelAcciones.ResumeLayout(false);
            this.ResumeLayout(false);

            this.Load += new System.EventHandler(this.GestionUsuariosFormulario_Load);
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            this.tabla.SelectionChanged += new System.EventHandler(this.tabla_SelectionChanged);
            this.buscador.TextChanged += new System.EventHandler(this.buscador_TextChanged);
            this.filtroEstado.SelectedIndexChanged += new System.EventHandler(this.filtroEstado_SelectedIndexChanged);
            this.lblClave.Click += new System.EventHandler(this.lblClave_Click);
            this.nuevo.Click += new System.EventHandler(this.nuevo_Click);
            this.guardar.Click += new System.EventHandler(this.guardar_Click);
            this.actualizar.Click += new System.EventHandler(this.actualizar_Click);
            this.darDeBaja.Click += new System.EventHandler(this.darDeBaja_Click);
            this.reactivar.Click += new System.EventHandler(this.reactivar_Click);
                    this.salario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.salario_KeyPress);
        }

    }
}
