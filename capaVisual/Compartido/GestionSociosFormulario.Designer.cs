using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Compartido
{
    partial class GestionSociosFormulario
    {
        private System.Windows.Forms.PictureBox fotoSocio;
        private System.Windows.Forms.Button btnSeleccionarFoto;
        private System.Windows.Forms.Button btnQuitarFoto;
        private System.Windows.Forms.ComboBox sexo;
        private System.Windows.Forms.Label lblSexo;

        private IContainer components;
        private Panel panelEncabezado;
        private Label lblTitulo;
        private Label lblDescripcion;
        private Button btnVolver;
        private Panel barraAcciones;
        private Label lblEstado;
        private TextBox nombre;
        private TextBox apellido;
        private TextBox dni;
        private DateTimePicker fechaNacimiento;
        private TextBox peso;
        private TextBox altura;
        private Button nuevo;
        private Button guardar;
        private Button actualizar;
        private Button calcularImc;
        private Button verRutina;
        private ComboBox filtroEstado;
        private TextBox buscador;
        private TableLayoutPanel panelContenido;
        private Panel panelListado;
        private Label lblListado;
        private Label lblAyuda;
        private Panel panelFiltro;
        private Label lblFiltro;
        private Panel panelDetalle;
        private Panel contenedorDetalle;
        private Panel contenedorCampos;
        private Panel panelAcciones;
        private Label lblFormulario;
        private Label lblNombre;
        private Label lblApellido;
        private Label lblDni;
        private Label lblFechaNacimiento;
        private Label lblPeso;
        private Label lblAltura;
        private DataGridView tabla;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colNombre;
        private DataGridViewTextBoxColumn colDni;
        private DataGridViewTextBoxColumn colNacimiento;
        private DataGridViewTextBoxColumn colEstado;

        protected override void Dispose(bool liberarRecursos)
        {
            if (liberarRecursos && components != null) components.Dispose();
            base.Dispose(liberarRecursos);
        }

        private void InitializeComponent()
        {
            this.fotoSocio = new System.Windows.Forms.PictureBox();
            this.btnSeleccionarFoto = new System.Windows.Forms.Button();
            this.btnQuitarFoto = new System.Windows.Forms.Button();
            this.sexo = new System.Windows.Forms.ComboBox();
            this.lblSexo = new System.Windows.Forms.Label();
            this.panelEncabezado = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.btnVolver = new System.Windows.Forms.Button();
            this.barraAcciones = new System.Windows.Forms.Panel();
            this.lblEstado = new System.Windows.Forms.Label();
            this.panelContenido = new System.Windows.Forms.TableLayoutPanel();
            this.panelListado = new System.Windows.Forms.Panel();
            this.lblListado = new System.Windows.Forms.Label();
            this.lblAyuda = new System.Windows.Forms.Label();
            this.panelFiltro = new System.Windows.Forms.Panel();
            this.buscador = new System.Windows.Forms.TextBox();
            this.lblFiltro = new System.Windows.Forms.Label();
            this.filtroEstado = new System.Windows.Forms.ComboBox();
            this.tabla = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDni = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNacimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.lblFechaNacimiento = new System.Windows.Forms.Label();
            this.fechaNacimiento = new System.Windows.Forms.DateTimePicker();
            this.lblPeso = new System.Windows.Forms.Label();
            this.peso = new System.Windows.Forms.TextBox();
            this.lblAltura = new System.Windows.Forms.Label();
            this.altura = new System.Windows.Forms.TextBox();
            this.panelAcciones = new System.Windows.Forms.Panel();
            this.nuevo = new System.Windows.Forms.Button();
            this.guardar = new System.Windows.Forms.Button();
            this.actualizar = new System.Windows.Forms.Button();
            this.calcularImc = new System.Windows.Forms.Button();
            this.verRutina = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.fotoSocio)).BeginInit();
            this.panelEncabezado.SuspendLayout();
            this.panelContenido.SuspendLayout();
            this.panelListado.SuspendLayout();
            this.panelFiltro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).BeginInit();
            this.panelDetalle.SuspendLayout();
            this.contenedorDetalle.SuspendLayout();
            this.contenedorCampos.SuspendLayout();
            this.panelAcciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // fotoSocio
            // 
            this.fotoSocio.BackColor = System.Drawing.Color.LightGray;
            this.fotoSocio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fotoSocio.Location = new System.Drawing.Point(16, 454);
            this.fotoSocio.Name = "fotoSocio";
            this.fotoSocio.Size = new System.Drawing.Size(120, 120);
            this.fotoSocio.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.fotoSocio.TabIndex = 1;
            this.fotoSocio.TabStop = false;
            this.fotoSocio.Disposed += new System.EventHandler(this.fotoSocio_Disposed);
            // 
            // btnSeleccionarFoto
            // 
            this.btnSeleccionarFoto.Location = new System.Drawing.Point(152, 454);
            this.btnSeleccionarFoto.Name = "btnSeleccionarFoto";
            this.btnSeleccionarFoto.Size = new System.Drawing.Size(180, 36);
            this.btnSeleccionarFoto.TabIndex = 2;
            this.btnSeleccionarFoto.Text = "Seleccionar foto";
            this.btnSeleccionarFoto.Click += new System.EventHandler(this.btnSeleccionarFoto_Click);
            // 
            // btnQuitarFoto
            // 
            this.btnQuitarFoto.Location = new System.Drawing.Point(152, 498);
            this.btnQuitarFoto.Name = "btnQuitarFoto";
            this.btnQuitarFoto.Size = new System.Drawing.Size(180, 36);
            this.btnQuitarFoto.TabIndex = 3;
            this.btnQuitarFoto.Text = "Quitar foto";
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
            this.sexo.Location = new System.Drawing.Point(124, 234);
            this.sexo.Name = "sexo";
            this.sexo.Size = new System.Drawing.Size(397, 29);
            this.sexo.TabIndex = 6;
            this.sexo.SelectedIndexChanged += new System.EventHandler(this.sexo_SelectedIndexChanged);
            // 
            // lblSexo
            // 
            this.lblSexo.Location = new System.Drawing.Point(0, 234);
            this.lblSexo.Name = "lblSexo";
            this.lblSexo.Size = new System.Drawing.Size(116, 28);
            this.lblSexo.TabIndex = 12;
            this.lblSexo.Text = "Sexo:";
            // 
            // panelEncabezado
            // 
            this.panelEncabezado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.panelEncabezado.Controls.Add(this.lblTitulo);
            this.panelEncabezado.Controls.Add(this.lblDescripcion);
            this.panelEncabezado.Controls.Add(this.btnVolver);
            this.panelEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEncabezado.Location = new System.Drawing.Point(0, 0);
            this.panelEncabezado.Name = "panelEncabezado";
            this.panelEncabezado.Padding = new System.Windows.Forms.Padding(22, 8, 22, 8);
            this.panelEncabezado.Size = new System.Drawing.Size(1215, 56);
            this.panelEncabezado.TabIndex = 0;
            this.panelEncabezado.Visible = false;
            // 
            // lblTitulo
            // 
            this.lblTitulo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(22, 8);
            this.lblTitulo.Margin = new System.Windows.Forms.Padding(0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(1005, 40);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Socios | Gestión de socios e información personal";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDescripcion.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblDescripcion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.lblDescripcion.Location = new System.Drawing.Point(24, 48);
            this.lblDescripcion.Margin = new System.Windows.Forms.Padding(0);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(1005, 26);
            this.lblDescripcion.TabIndex = 1;
            this.lblDescripcion.Text = "Alta, actualizacion, baja logica y consulta de IMC";
            this.lblDescripcion.Visible = false;
            // 
            // btnVolver
            // 
            this.btnVolver.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVolver.BackColor = System.Drawing.Color.White;
            this.btnVolver.FlatAppearance.BorderSize = 0;
            this.btnVolver.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVolver.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnVolver.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.btnVolver.Location = new System.Drawing.Point(1089, 10);
            this.btnVolver.Margin = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(104, 38);
            this.btnVolver.TabIndex = 2;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = false;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // barraAcciones
            // 
            this.barraAcciones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.barraAcciones.Dock = System.Windows.Forms.DockStyle.Top;
            this.barraAcciones.Location = new System.Drawing.Point(0, 56);
            this.barraAcciones.Name = "barraAcciones";
            this.barraAcciones.Size = new System.Drawing.Size(1215, 1);
            this.barraAcciones.TabIndex = 1;
            // 
            // lblEstado
            // 
            this.lblEstado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.lblEstado.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblEstado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblEstado.Location = new System.Drawing.Point(0, 650);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Padding = new System.Windows.Forms.Padding(18, 0, 12, 0);
            this.lblEstado.Size = new System.Drawing.Size(1215, 30);
            this.lblEstado.TabIndex = 3;
            this.lblEstado.Text = "Listo";
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelContenido
            // 
            this.panelContenido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelContenido.ColumnCount = 2;
            this.panelContenido.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelContenido.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 396F));
            this.panelContenido.Controls.Add(this.panelListado, 0, 0);
            this.panelContenido.Controls.Add(this.panelDetalle, 1, 0);
            this.panelContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenido.Location = new System.Drawing.Point(0, 57);
            this.panelContenido.Name = "panelContenido";
            this.panelContenido.Padding = new System.Windows.Forms.Padding(16);
            this.panelContenido.RowCount = 1;
            this.panelContenido.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelContenido.Size = new System.Drawing.Size(1215, 593);
            this.panelContenido.TabIndex = 2;
            // 
            // panelListado
            // 
            this.panelListado.BackColor = System.Drawing.Color.White;
            this.panelListado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelListado.Controls.Add(this.lblListado);
            this.panelListado.Controls.Add(this.lblAyuda);
            this.panelListado.Controls.Add(this.panelFiltro);
            this.panelListado.Controls.Add(this.tabla);
            this.panelListado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelListado.Location = new System.Drawing.Point(16, 16);
            this.panelListado.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.panelListado.Name = "panelListado";
            this.panelListado.Padding = new System.Windows.Forms.Padding(16);
            this.panelListado.Size = new System.Drawing.Size(771, 561);
            this.panelListado.TabIndex = 0;
            // 
            // lblListado
            // 
            this.lblListado.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblListado.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblListado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblListado.Location = new System.Drawing.Point(16, 16);
            this.lblListado.Margin = new System.Windows.Forms.Padding(0);
            this.lblListado.Name = "lblListado";
            this.lblListado.Size = new System.Drawing.Size(737, 28);
            this.lblListado.TabIndex = 0;
            this.lblListado.Text = "Listado de socios";
            this.lblListado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAyuda
            // 
            this.lblAyuda.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAyuda.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblAyuda.Location = new System.Drawing.Point(16, 44);
            this.lblAyuda.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.lblAyuda.Name = "lblAyuda";
            this.lblAyuda.Size = new System.Drawing.Size(737, 24);
            this.lblAyuda.TabIndex = 1;
            this.lblAyuda.Text = "Busca por nombre o DNI";
            this.lblAyuda.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelFiltro
            // 
            this.panelFiltro.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelFiltro.BackColor = System.Drawing.Color.White;
            this.panelFiltro.Controls.Add(this.buscador);
            this.panelFiltro.Controls.Add(this.lblFiltro);
            this.panelFiltro.Controls.Add(this.filtroEstado);
            this.panelFiltro.Location = new System.Drawing.Point(16, 72);
            this.panelFiltro.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.panelFiltro.Name = "panelFiltro";
            this.panelFiltro.Size = new System.Drawing.Size(737, 42);
            this.panelFiltro.TabIndex = 2;
            // 
            // buscador
            // 
            this.buscador.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buscador.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.buscador.Location = new System.Drawing.Point(0, 9);
            this.buscador.Margin = new System.Windows.Forms.Padding(0, 8, 12, 8);
            this.buscador.Name = "buscador";
            this.buscador.Size = new System.Drawing.Size(454, 26);
            this.buscador.TabIndex = 0;
            this.buscador.TextChanged += new System.EventHandler(this.buscador_TextChanged);
            // 
            // lblFiltro
            // 
            this.lblFiltro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFiltro.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblFiltro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblFiltro.Location = new System.Drawing.Point(487, 11);
            this.lblFiltro.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.lblFiltro.Name = "lblFiltro";
            this.lblFiltro.Size = new System.Drawing.Size(60, 24);
            this.lblFiltro.TabIndex = 1;
            this.lblFiltro.Text = "Estado";
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
            this.filtroEstado.Location = new System.Drawing.Point(555, 8);
            this.filtroEstado.Margin = new System.Windows.Forms.Padding(0, 8, 0, 8);
            this.filtroEstado.Name = "filtroEstado";
            this.filtroEstado.Size = new System.Drawing.Size(148, 29);
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
            this.colNacimiento,
            this.colEstado});
            this.tabla.Location = new System.Drawing.Point(16, 124);
            this.tabla.Margin = new System.Windows.Forms.Padding(0);
            this.tabla.MultiSelect = false;
            this.tabla.Name = "tabla";
            this.tabla.ReadOnly = true;
            this.tabla.RowHeadersVisible = false;
            this.tabla.RowHeadersWidth = 51;
            this.tabla.RowTemplate.Height = 30;
            this.tabla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tabla.Size = new System.Drawing.Size(737, 419);
            this.tabla.TabIndex = 3;
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
            this.colNombre.FillWeight = 150F;
            this.colNombre.HeaderText = "Nombre";
            this.colNombre.MinimumWidth = 180;
            this.colNombre.Name = "colNombre";
            this.colNombre.ReadOnly = true;
            // 
            // colDni
            // 
            this.colDni.FillWeight = 95F;
            this.colDni.HeaderText = "DNI";
            this.colDni.MinimumWidth = 110;
            this.colDni.Name = "colDni";
            this.colDni.ReadOnly = true;
            // 
            // colNacimiento
            // 
            this.colNacimiento.FillWeight = 95F;
            this.colNacimiento.HeaderText = "Nacimiento";
            this.colNacimiento.MinimumWidth = 120;
            this.colNacimiento.Name = "colNacimiento";
            this.colNacimiento.ReadOnly = true;
            // 
            // colEstado
            // 
            this.colEstado.FillWeight = 70F;
            this.colEstado.HeaderText = "Estado";
            this.colEstado.MinimumWidth = 90;
            this.colEstado.Name = "colEstado";
            this.colEstado.ReadOnly = true;
            // 
            // panelDetalle
            // 
            this.panelDetalle.AutoScroll = true;
            this.panelDetalle.BackColor = System.Drawing.Color.White;
            this.panelDetalle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDetalle.Controls.Add(this.contenedorDetalle);
            this.panelDetalle.Controls.Add(this.fotoSocio);
            this.panelDetalle.Controls.Add(this.btnSeleccionarFoto);
            this.panelDetalle.Controls.Add(this.btnQuitarFoto);
            this.panelDetalle.Controls.Add(this.verRutina);
            this.panelDetalle.Location = new System.Drawing.Point(803, 16);
            this.panelDetalle.Margin = new System.Windows.Forms.Padding(0);
            this.panelDetalle.Name = "panelDetalle";
            this.panelDetalle.Padding = new System.Windows.Forms.Padding(16);
            this.panelDetalle.Size = new System.Drawing.Size(396, 561);
            this.panelDetalle.TabIndex = 1;
            // 
            // contenedorDetalle
            // 
            this.contenedorDetalle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.contenedorDetalle.Controls.Add(this.lblFormulario);
            this.contenedorDetalle.Controls.Add(this.contenedorCampos);
            this.contenedorDetalle.Controls.Add(this.panelAcciones);
            this.contenedorDetalle.Location = new System.Drawing.Point(16, 16);
            this.contenedorDetalle.Name = "contenedorDetalle";
            this.contenedorDetalle.Size = new System.Drawing.Size(537, 438);
            this.contenedorDetalle.TabIndex = 0;
            // 
            // lblFormulario
            // 
            this.lblFormulario.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblFormulario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblFormulario.Location = new System.Drawing.Point(0, 0);
            this.lblFormulario.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.lblFormulario.Name = "lblFormulario";
            this.lblFormulario.Size = new System.Drawing.Size(362, 32);
            this.lblFormulario.TabIndex = 0;
            this.lblFormulario.Text = "Nuevo socio";
            this.lblFormulario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // contenedorCampos
            // 
            this.contenedorCampos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.contenedorCampos.Controls.Add(this.lblNombre);
            this.contenedorCampos.Controls.Add(this.nombre);
            this.contenedorCampos.Controls.Add(this.lblApellido);
            this.contenedorCampos.Controls.Add(this.apellido);
            this.contenedorCampos.Controls.Add(this.lblDni);
            this.contenedorCampos.Controls.Add(this.dni);
            this.contenedorCampos.Controls.Add(this.lblFechaNacimiento);
            this.contenedorCampos.Controls.Add(this.fechaNacimiento);
            this.contenedorCampos.Controls.Add(this.lblPeso);
            this.contenedorCampos.Controls.Add(this.peso);
            this.contenedorCampos.Controls.Add(this.lblAltura);
            this.contenedorCampos.Controls.Add(this.altura);
            this.contenedorCampos.Controls.Add(this.lblSexo);
            this.contenedorCampos.Controls.Add(this.sexo);
            this.contenedorCampos.Location = new System.Drawing.Point(0, 42);
            this.contenedorCampos.Margin = new System.Windows.Forms.Padding(0, 0, 0, 16);
            this.contenedorCampos.Name = "contenedorCampos";
            this.contenedorCampos.Size = new System.Drawing.Size(537, 272);
            this.contenedorCampos.TabIndex = 1;
            // 
            // lblNombre
            // 
            this.lblNombre.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblNombre.Location = new System.Drawing.Point(0, 0);
            this.lblNombre.Margin = new System.Windows.Forms.Padding(0, 0, 8, 8);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(116, 30);
            this.lblNombre.TabIndex = 0;
            this.lblNombre.Text = "Nombre:";
            this.lblNombre.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nombre
            // 
            this.nombre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nombre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nombre.Location = new System.Drawing.Point(124, 4);
            this.nombre.Margin = new System.Windows.Forms.Padding(0, 3, 0, 8);
            this.nombre.MaxLength = 100;
            this.nombre.Name = "nombre";
            this.nombre.Size = new System.Drawing.Size(397, 24);
            this.nombre.TabIndex = 1;
            this.nombre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.nombre_KeyPress);
            // 
            // lblApellido
            // 
            this.lblApellido.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblApellido.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblApellido.Location = new System.Drawing.Point(0, 38);
            this.lblApellido.Margin = new System.Windows.Forms.Padding(0, 0, 8, 8);
            this.lblApellido.Name = "lblApellido";
            this.lblApellido.Size = new System.Drawing.Size(116, 30);
            this.lblApellido.TabIndex = 2;
            this.lblApellido.Text = "Apellido:";
            this.lblApellido.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // apellido
            // 
            this.apellido.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.apellido.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.apellido.Location = new System.Drawing.Point(124, 42);
            this.apellido.Margin = new System.Windows.Forms.Padding(0, 3, 0, 8);
            this.apellido.MaxLength = 100;
            this.apellido.Name = "apellido";
            this.apellido.Size = new System.Drawing.Size(397, 24);
            this.apellido.TabIndex = 3;
            this.apellido.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.apellido_KeyPress);
            // 
            // lblDni
            // 
            this.lblDni.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblDni.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblDni.Location = new System.Drawing.Point(0, 76);
            this.lblDni.Margin = new System.Windows.Forms.Padding(0, 0, 8, 8);
            this.lblDni.Name = "lblDni";
            this.lblDni.Size = new System.Drawing.Size(116, 30);
            this.lblDni.TabIndex = 4;
            this.lblDni.Text = "DNI:";
            this.lblDni.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dni
            // 
            this.dni.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dni.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dni.Location = new System.Drawing.Point(124, 80);
            this.dni.Margin = new System.Windows.Forms.Padding(0, 3, 0, 8);
            this.dni.MaxLength = 20;
            this.dni.Name = "dni";
            this.dni.Size = new System.Drawing.Size(397, 24);
            this.dni.TabIndex = 5;
            this.dni.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dni_KeyPress);
            // 
            // lblFechaNacimiento
            // 
            this.lblFechaNacimiento.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblFechaNacimiento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblFechaNacimiento.Location = new System.Drawing.Point(0, 114);
            this.lblFechaNacimiento.Margin = new System.Windows.Forms.Padding(0, 0, 8, 8);
            this.lblFechaNacimiento.Name = "lblFechaNacimiento";
            this.lblFechaNacimiento.Size = new System.Drawing.Size(116, 30);
            this.lblFechaNacimiento.TabIndex = 6;
            this.lblFechaNacimiento.Text = "Nacimiento:";
            this.lblFechaNacimiento.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // fechaNacimiento
            // 
            this.fechaNacimiento.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fechaNacimiento.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.fechaNacimiento.Location = new System.Drawing.Point(124, 118);
            this.fechaNacimiento.Margin = new System.Windows.Forms.Padding(0, 3, 0, 8);
            this.fechaNacimiento.Name = "fechaNacimiento";
            this.fechaNacimiento.ShowCheckBox = true;
            this.fechaNacimiento.Size = new System.Drawing.Size(397, 29);
            this.fechaNacimiento.TabIndex = 7;
            // 
            // lblPeso
            // 
            this.lblPeso.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblPeso.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblPeso.Location = new System.Drawing.Point(0, 152);
            this.lblPeso.Margin = new System.Windows.Forms.Padding(0, 0, 8, 8);
            this.lblPeso.Name = "lblPeso";
            this.lblPeso.Size = new System.Drawing.Size(116, 30);
            this.lblPeso.TabIndex = 8;
            this.lblPeso.Text = "Peso (kg):";
            this.lblPeso.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // peso
            // 
            this.peso.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.peso.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.peso.Location = new System.Drawing.Point(124, 156);
            this.peso.Margin = new System.Windows.Forms.Padding(0, 3, 0, 8);
            this.peso.Name = "peso";
            this.peso.Size = new System.Drawing.Size(397, 24);
            this.peso.TabIndex = 9;
            this.peso.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.peso_KeyPress);
            // 
            // lblAltura
            // 
            this.lblAltura.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblAltura.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblAltura.Location = new System.Drawing.Point(0, 190);
            this.lblAltura.Margin = new System.Windows.Forms.Padding(0, 0, 8, 8);
            this.lblAltura.Name = "lblAltura";
            this.lblAltura.Size = new System.Drawing.Size(116, 30);
            this.lblAltura.TabIndex = 10;
            this.lblAltura.Text = "Altura (m):";
            this.lblAltura.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // altura
            // 
            this.altura.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.altura.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.altura.Location = new System.Drawing.Point(124, 194);
            this.altura.Margin = new System.Windows.Forms.Padding(0, 3, 0, 8);
            this.altura.Name = "altura";
            this.altura.Size = new System.Drawing.Size(397, 24);
            this.altura.TabIndex = 11;
            this.altura.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.altura_KeyPress);
            // 
            // panelAcciones
            // 
            this.panelAcciones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelAcciones.Controls.Add(this.nuevo);
            this.panelAcciones.Controls.Add(this.guardar);
            this.panelAcciones.Controls.Add(this.actualizar);
            this.panelAcciones.Controls.Add(this.calcularImc);
            this.panelAcciones.Location = new System.Drawing.Point(0, 330);
            this.panelAcciones.Margin = new System.Windows.Forms.Padding(0);
            this.panelAcciones.Name = "panelAcciones";
            this.panelAcciones.Size = new System.Drawing.Size(537, 100);
            this.panelAcciones.TabIndex = 2;
            // 
            // nuevo
            // 
            this.nuevo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.nuevo.FlatAppearance.BorderSize = 0;
            this.nuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nuevo.ForeColor = System.Drawing.Color.White;
            this.nuevo.Location = new System.Drawing.Point(0, 0);
            this.nuevo.Margin = new System.Windows.Forms.Padding(0, 0, 8, 8);
            this.nuevo.Name = "nuevo";
            this.nuevo.Size = new System.Drawing.Size(112, 38);
            this.nuevo.TabIndex = 0;
            this.nuevo.Text = "+ Nuevo socio";
            this.nuevo.UseVisualStyleBackColor = false;
            this.nuevo.Click += new System.EventHandler(this.nuevo_Click);
            // 
            // guardar
            // 
            this.guardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.guardar.FlatAppearance.BorderSize = 0;
            this.guardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.guardar.ForeColor = System.Drawing.Color.White;
            this.guardar.Location = new System.Drawing.Point(120, 0);
            this.guardar.Margin = new System.Windows.Forms.Padding(0, 0, 8, 8);
            this.guardar.Name = "guardar";
            this.guardar.Size = new System.Drawing.Size(112, 38);
            this.guardar.TabIndex = 1;
            this.guardar.Text = "Guardar";
            this.guardar.UseVisualStyleBackColor = false;
            this.guardar.Click += new System.EventHandler(this.guardar_Click);
            // 
            // actualizar
            // 
            this.actualizar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.actualizar.FlatAppearance.BorderSize = 0;
            this.actualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.actualizar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.actualizar.Location = new System.Drawing.Point(240, 0);
            this.actualizar.Margin = new System.Windows.Forms.Padding(0, 0, 8, 8);
            this.actualizar.Name = "actualizar";
            this.actualizar.Size = new System.Drawing.Size(112, 38);
            this.actualizar.TabIndex = 2;
            this.actualizar.Text = "Actualizar";
            this.actualizar.UseVisualStyleBackColor = false;
            this.actualizar.Click += new System.EventHandler(this.actualizar_Click);
            // 
            // calcularImc
            // 
            this.calcularImc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.calcularImc.FlatAppearance.BorderSize = 0;
            this.calcularImc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.calcularImc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.calcularImc.Location = new System.Drawing.Point(240, 46);
            this.calcularImc.Margin = new System.Windows.Forms.Padding(0, 0, 8, 8);
            this.calcularImc.Name = "calcularImc";
            this.calcularImc.Size = new System.Drawing.Size(112, 38);
            this.calcularImc.TabIndex = 3;
            this.calcularImc.Text = "Calcular IMC";
            this.calcularImc.UseVisualStyleBackColor = false;
            this.calcularImc.Click += new System.EventHandler(this.calcularImc_Click);
            // 
            // verRutina
            // 
            this.verRutina.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.verRutina.FlatAppearance.BorderSize = 0;
            this.verRutina.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.verRutina.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.verRutina.Location = new System.Drawing.Point(16, 590);
            this.verRutina.Name = "verRutina";
            this.verRutina.Size = new System.Drawing.Size(316, 38);
            this.verRutina.TabIndex = 4;
            this.verRutina.Text = "Ver rutina semanal";
            this.verRutina.UseVisualStyleBackColor = false;
            this.verRutina.Click += new System.EventHandler(this.verRutina_Click);
            // 
            // GestionSociosFormulario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1215, 680);
            this.Controls.Add(this.panelContenido);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.barraAcciones);
            this.Controls.Add(this.panelEncabezado);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.MinimumSize = new System.Drawing.Size(900, 560);
            this.Name = "GestionSociosFormulario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SysGym | Socios";
            this.Load += new System.EventHandler(this.GestionSociosFormulario_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fotoSocio)).EndInit();
            this.panelEncabezado.ResumeLayout(false);
            this.panelContenido.ResumeLayout(false);
            this.panelListado.ResumeLayout(false);
            this.panelFiltro.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).EndInit();
            this.panelDetalle.ResumeLayout(false);
            this.contenedorDetalle.ResumeLayout(false);
            this.contenedorCampos.ResumeLayout(false);
            this.panelAcciones.ResumeLayout(false);
            this.ResumeLayout(false);

        }

    }
}
