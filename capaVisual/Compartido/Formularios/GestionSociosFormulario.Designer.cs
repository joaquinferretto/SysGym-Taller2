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
        private ErrorProvider indicadorErrores;
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
        private FlowLayoutPanel accionesSocio;
        private Button darDeBaja;
        private Button reactivar;
        private Label lblEstadoSocio;
        private Label estadoSocio;
        private ComboBox filtroEstado;
        private TextBox buscador;
        private Panel panelContenido;
        private SplitContainer contenedorContenido;
        private TableLayoutPanel panelListado;
        private Label lblListado;
        private Label lblAyuda;
        private Label lblFiltro;
        private Panel panelDetalle;
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
        private ContextMenuStrip menuSocios;
        private ToolStripMenuItem fichaMenu;
        private ToolStripMenuItem rutinaMenu;
        private ToolStripMenuItem imcMenu;
        private ToolStripMenuItem darDeBajaMenu;
        private ToolStripMenuItem reactivarMenu;

        protected override void Dispose(bool liberarRecursos)
        {
            if (liberarRecursos && components != null) components.Dispose();
            base.Dispose(liberarRecursos);
        }

        private System.Windows.Forms.Button btnFicha;
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.indicadorErrores = new System.Windows.Forms.ErrorProvider(this.components);
            this.fotoSocio = new System.Windows.Forms.PictureBox();
            this.btnSeleccionarFoto = new System.Windows.Forms.Button();
            this.btnQuitarFoto = new System.Windows.Forms.Button();
            this.sexo = new System.Windows.Forms.ComboBox();
            this.lblSexo = new System.Windows.Forms.Label();
            this.lblEstado = new System.Windows.Forms.Label();
            this.panelContenido = new System.Windows.Forms.Panel();
            this.contenedorContenido = new System.Windows.Forms.SplitContainer();
            this.panelListado = new System.Windows.Forms.TableLayoutPanel();
            this.lblListado = new System.Windows.Forms.Label();
            this.lblAyuda = new System.Windows.Forms.Label();
            this.buscador = new System.Windows.Forms.TextBox();
            this.lblFiltro = new System.Windows.Forms.Label();
            this.filtroEstado = new System.Windows.Forms.ComboBox();
            this.tabla = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDni = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNacimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuSocios = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.fichaMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.rutinaMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.imcMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.darDeBajaMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.reactivarMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.panelDetalle = new System.Windows.Forms.Panel();
            this.btnFicha = new System.Windows.Forms.Button();
            this.lblFormulario = new System.Windows.Forms.Label();
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
            this.lblEstadoSocio = new System.Windows.Forms.Label();
            this.estadoSocio = new System.Windows.Forms.Label();
            this.nuevo = new System.Windows.Forms.Button();
            this.guardar = new System.Windows.Forms.Button();
            this.actualizar = new System.Windows.Forms.Button();
            this.calcularImc = new System.Windows.Forms.Button();
            this.darDeBaja = new System.Windows.Forms.Button();
            this.reactivar = new System.Windows.Forms.Button();
            this.verRutina = new System.Windows.Forms.Button();
            this.accionesSocio = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.indicadorErrores)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fotoSocio)).BeginInit();
            this.panelContenido.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.contenedorContenido)).BeginInit();
            this.contenedorContenido.Panel1.SuspendLayout();
            this.contenedorContenido.Panel2.SuspendLayout();
            this.contenedorContenido.SuspendLayout();
            this.panelListado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).BeginInit();
            this.menuSocios.SuspendLayout();
            this.panelDetalle.SuspendLayout();
            this.SuspendLayout();
            //
            // indicadorErrores
            //
            this.indicadorErrores.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.indicadorErrores.ContainerControl = this;
            //
            // fotoSocio
            //
            this.fotoSocio.BackColor = System.Drawing.Color.LightGray;
            this.fotoSocio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fotoSocio.Location = new System.Drawing.Point(320, 72);
            this.fotoSocio.Margin = new System.Windows.Forms.Padding(4);
            this.fotoSocio.Name = "fotoSocio";
            this.fotoSocio.Size = new System.Drawing.Size(120, 120);
            this.fotoSocio.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.fotoSocio.TabIndex = 23;
            this.fotoSocio.TabStop = false;
            this.fotoSocio.Disposed += new System.EventHandler(this.fotoSocio_Disposed);
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
            this.btnSeleccionarFoto.Location = new System.Drawing.Point(300, 205);
            this.btnSeleccionarFoto.Margin = new System.Windows.Forms.Padding(4);
            this.btnSeleccionarFoto.Name = "btnSeleccionarFoto";
            this.btnSeleccionarFoto.Size = new System.Drawing.Size(160, 38);
            this.btnSeleccionarFoto.TabIndex = 24;
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
            this.btnQuitarFoto.Location = new System.Drawing.Point(300, 250);
            this.btnQuitarFoto.Margin = new System.Windows.Forms.Padding(4);
            this.btnQuitarFoto.Name = "btnQuitarFoto";
            this.btnQuitarFoto.Size = new System.Drawing.Size(160, 38);
            this.btnQuitarFoto.TabIndex = 25;
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
            this.sexo.Location = new System.Drawing.Point(112, 310);
            this.sexo.Margin = new System.Windows.Forms.Padding(4);
            this.sexo.Name = "sexo";
            this.sexo.Size = new System.Drawing.Size(165, 29);
            this.sexo.TabIndex = 14;
            this.sexo.SelectedIndexChanged += new System.EventHandler(this.sexo_SelectedIndexChanged);
            //
            // lblSexo
            //
            this.lblSexo.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblSexo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblSexo.Location = new System.Drawing.Point(20, 306);
            this.lblSexo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSexo.Name = "lblSexo";
            this.lblSexo.Size = new System.Drawing.Size(88, 34);
            this.lblSexo.TabIndex = 13;
            this.lblSexo.Text = "Sexo:";
            this.lblSexo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblEstado
            //
            this.lblEstado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.lblEstado.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblEstado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblEstado.Location = new System.Drawing.Point(0, 812);
            this.lblEstado.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Padding = new System.Windows.Forms.Padding(22, 0, 15, 0);
            this.lblEstado.Size = new System.Drawing.Size(1375, 38);
            this.lblEstado.TabIndex = 3;
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // panelContenido
            //
            this.panelContenido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelContenido.Controls.Add(this.contenedorContenido);
            this.panelContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenido.Location = new System.Drawing.Point(0, 0);
            this.panelContenido.Margin = new System.Windows.Forms.Padding(4);
            this.panelContenido.Name = "panelContenido";
            this.panelContenido.Padding = new System.Windows.Forms.Padding(20);
            this.panelContenido.Size = new System.Drawing.Size(1375, 812);
            this.panelContenido.TabIndex = 2;
            //
            // contenedorContenido
            //
            this.contenedorContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contenedorContenido.Location = new System.Drawing.Point(20, 20);
            this.contenedorContenido.Margin = new System.Windows.Forms.Padding(4);
            this.contenedorContenido.Name = "contenedorContenido";
            //
            // contenedorContenido.Panel1
            //
            this.contenedorContenido.Panel1.Controls.Add(this.panelListado);
            this.contenedorContenido.Panel1MinSize = 240;
            //
            // contenedorContenido.Panel2
            //
            this.contenedorContenido.Panel2.Controls.Add(this.panelDetalle);
            this.contenedorContenido.Panel2MinSize = 500;
            this.contenedorContenido.Size = new System.Drawing.Size(1335, 772);
            this.contenedorContenido.SplitterDistance = 815;
            this.contenedorContenido.SplitterWidth = 20;
            this.contenedorContenido.TabIndex = 0;
            //
            // panelListado
            //
            this.panelListado.BackColor = System.Drawing.Color.White;
            this.panelListado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelListado.ColumnCount = 3;
            this.panelListado.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelListado.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panelListado.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 185F));
            this.panelListado.Controls.Add(this.lblListado, 0, 0);
            this.panelListado.Controls.Add(this.lblAyuda, 0, 1);
            this.panelListado.Controls.Add(this.buscador, 0, 2);
            this.panelListado.Controls.Add(this.lblFiltro, 1, 2);
            this.panelListado.Controls.Add(this.filtroEstado, 2, 2);
            this.panelListado.Controls.Add(this.tabla, 0, 3);
            this.panelListado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelListado.Location = new System.Drawing.Point(0, 0);
            this.panelListado.Margin = new System.Windows.Forms.Padding(4);
            this.panelListado.Name = "panelListado";
            this.panelListado.Padding = new System.Windows.Forms.Padding(20);
            this.panelListado.RowCount = 4;
            this.panelListado.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.panelListado.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.panelListado.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 58F));
            this.panelListado.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelListado.Size = new System.Drawing.Size(815, 772);
            this.panelListado.TabIndex = 0;
            //
            // lblListado
            //
            this.lblListado.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelListado.SetColumnSpan(this.lblListado, 3);
            this.lblListado.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblListado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblListado.Location = new System.Drawing.Point(24, 20);
            this.lblListado.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblListado.Name = "lblListado";
            this.lblListado.Size = new System.Drawing.Size(765, 35);
            this.lblListado.TabIndex = 0;
            this.lblListado.Text = "Listado de socios";
            this.lblListado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblAyuda
            //
            this.lblAyuda.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelListado.SetColumnSpan(this.lblAyuda, 3);
            this.lblAyuda.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblAyuda.Location = new System.Drawing.Point(24, 55);
            this.lblAyuda.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAyuda.Name = "lblAyuda";
            this.lblAyuda.Size = new System.Drawing.Size(765, 30);
            this.lblAyuda.TabIndex = 1;
            this.lblAyuda.Text = "Busca por nombre o DNI";
            this.lblAyuda.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // buscador
            //
            this.buscador.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buscador.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.buscador.Location = new System.Drawing.Point(24, 94);
            this.buscador.Margin = new System.Windows.Forms.Padding(4);
            this.buscador.Name = "buscador";
            this.buscador.Size = new System.Drawing.Size(514, 29);
            this.buscador.TabIndex = 0;
            this.buscador.TextChanged += new System.EventHandler(this.buscador_TextChanged);
            //
            // lblFiltro
            //
            this.lblFiltro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFiltro.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblFiltro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblFiltro.Location = new System.Drawing.Point(546, 90);
            this.lblFiltro.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFiltro.Name = "lblFiltro";
            this.lblFiltro.Size = new System.Drawing.Size(58, 30);
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
            this.filtroEstado.Location = new System.Drawing.Point(613, 94);
            this.filtroEstado.Margin = new System.Windows.Forms.Padding(4);
            this.filtroEstado.Name = "filtroEstado";
            this.filtroEstado.Size = new System.Drawing.Size(176, 29);
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
            this.panelListado.SetColumnSpan(this.tabla, 3);
            this.tabla.ContextMenuStrip = this.menuSocios;
            this.tabla.Location = new System.Drawing.Point(24, 152);
            this.tabla.Margin = new System.Windows.Forms.Padding(4);
            this.tabla.MultiSelect = false;
            this.tabla.Name = "tabla";
            this.tabla.ReadOnly = true;
            this.tabla.RowHeadersVisible = false;
            this.tabla.RowHeadersWidth = 51;
            this.tabla.RowTemplate.Height = 30;
            this.tabla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tabla.Size = new System.Drawing.Size(765, 594);
            this.tabla.TabIndex = 3;
            this.tabla.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tabla_CellDoubleClick);
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
            this.colNombre.FillWeight = 150F;
            this.colNombre.HeaderText = "Nombre";
            this.colNombre.MinimumWidth = 160;
            this.colNombre.Name = "colNombre";
            this.colNombre.ReadOnly = true;
            //
            // colDni
            //
            this.colDni.FillWeight = 95F;
            this.colDni.HeaderText = "DNI";
            this.colDni.MinimumWidth = 90;
            this.colDni.Name = "colDni";
            this.colDni.ReadOnly = true;
            //
            // colNacimiento
            //
            this.colNacimiento.FillWeight = 95F;
            this.colNacimiento.HeaderText = "Nacimiento";
            this.colNacimiento.MinimumWidth = 100;
            this.colNacimiento.Name = "colNacimiento";
            this.colNacimiento.ReadOnly = true;
            //
            // colEstado
            //
            this.colEstado.FillWeight = 60F;
            this.colEstado.HeaderText = "Estado";
            this.colEstado.MinimumWidth = 65;
            this.colEstado.Name = "colEstado";
            this.colEstado.ReadOnly = true;
            //
            // menuSocios
            //
            this.menuSocios.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuSocios.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fichaMenu,
            this.rutinaMenu,
            this.imcMenu,
            this.darDeBajaMenu,
            this.reactivarMenu});
            this.menuSocios.Name = "menuSocios";
            this.menuSocios.Size = new System.Drawing.Size(203, 124);
            //
            // fichaMenu
            //
            this.fichaMenu.Name = "fichaMenu";
            this.fichaMenu.Size = new System.Drawing.Size(202, 24);
            this.fichaMenu.Text = "Ver ficha completa";
            this.fichaMenu.Click += new System.EventHandler(this.btnFicha_Click);
            //
            // rutinaMenu
            //
            this.rutinaMenu.Name = "rutinaMenu";
            this.rutinaMenu.Size = new System.Drawing.Size(202, 24);
            this.rutinaMenu.Text = "Ver rutina semanal";
            this.rutinaMenu.Click += new System.EventHandler(this.verRutina_Click);
            //
            // imcMenu
            //
            this.imcMenu.Name = "imcMenu";
            this.imcMenu.Size = new System.Drawing.Size(202, 24);
            this.imcMenu.Text = "Calcular IMC";
            this.imcMenu.Click += new System.EventHandler(this.calcularImc_Click);
            //
            // darDeBajaMenu
            //
            this.darDeBajaMenu.Name = "darDeBajaMenu";
            this.darDeBajaMenu.Size = new System.Drawing.Size(202, 24);
            this.darDeBajaMenu.Text = "Dar de baja";
            this.darDeBajaMenu.Click += new System.EventHandler(this.darDeBaja_Click);
            //
            // reactivarMenu
            //
            this.reactivarMenu.Name = "reactivarMenu";
            this.reactivarMenu.Size = new System.Drawing.Size(202, 24);
            this.reactivarMenu.Text = "Reactivar";
            this.reactivarMenu.Click += new System.EventHandler(this.reactivar_Click);
            //
            // panelDetalle
            //
            this.panelDetalle.Controls.Add(this.btnFicha);
            this.panelDetalle.Controls.Add(this.lblFormulario);
            this.panelDetalle.Controls.Add(this.lblNombre);
            this.panelDetalle.Controls.Add(this.nombre);
            this.panelDetalle.Controls.Add(this.lblApellido);
            this.panelDetalle.Controls.Add(this.apellido);
            this.panelDetalle.Controls.Add(this.lblDni);
            this.panelDetalle.Controls.Add(this.dni);
            this.panelDetalle.Controls.Add(this.lblFechaNacimiento);
            this.panelDetalle.Controls.Add(this.fechaNacimiento);
            this.panelDetalle.Controls.Add(this.lblPeso);
            this.panelDetalle.Controls.Add(this.peso);
            this.panelDetalle.Controls.Add(this.lblAltura);
            this.panelDetalle.Controls.Add(this.altura);
            this.panelDetalle.Controls.Add(this.lblSexo);
            this.panelDetalle.Controls.Add(this.sexo);
            this.panelDetalle.Controls.Add(this.lblEstadoSocio);
            this.panelDetalle.Controls.Add(this.estadoSocio);
            this.panelDetalle.Controls.Add(this.actualizar);
            this.panelDetalle.Controls.Add(this.accionesSocio);
            this.panelDetalle.Controls.Add(this.fotoSocio);
            this.panelDetalle.Controls.Add(this.btnSeleccionarFoto);
            this.panelDetalle.Controls.Add(this.btnQuitarFoto);
            this.panelDetalle.Controls.Add(this.verRutina);
            this.panelDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDetalle.Location = new System.Drawing.Point(0, 0);
            this.panelDetalle.Margin = new System.Windows.Forms.Padding(4);
            this.panelDetalle.Name = "panelDetalle";
            this.panelDetalle.Padding = new System.Windows.Forms.Padding(20);
            this.panelDetalle.Size = new System.Drawing.Size(500, 772);
            this.panelDetalle.TabIndex = 1;
            //
            // accionesSocio
            //
            this.accionesSocio.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.accionesSocio.BackColor = System.Drawing.Color.Transparent;
            this.accionesSocio.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.accionesSocio.Location = new System.Drawing.Point(20, 394);
            this.accionesSocio.Margin = new System.Windows.Forms.Padding(0);
            this.accionesSocio.Name = "accionesSocio";
            this.accionesSocio.Padding = new System.Windows.Forms.Padding(0);
            this.accionesSocio.Size = new System.Drawing.Size(460, 48);
            this.accionesSocio.TabIndex = 31;
            this.accionesSocio.WrapContents = false;
            this.accionesSocio.Controls.Add(this.darDeBaja);
            this.accionesSocio.Controls.Add(this.reactivar);
            this.accionesSocio.Controls.Add(this.guardar);
            this.accionesSocio.Controls.Add(this.calcularImc);
            this.accionesSocio.Controls.Add(this.nuevo);
            //
            // btnFicha
            //
            this.btnFicha.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(75)))), ((int)(((byte)(138)))));
            this.btnFicha.Enabled = false;
            this.btnFicha.FlatAppearance.BorderSize = 0;
            this.btnFicha.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(48)))), ((int)(((byte)(89)))));
            this.btnFicha.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(61)))), ((int)(((byte)(112)))));
            this.btnFicha.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFicha.ForeColor = System.Drawing.Color.White;
            this.btnFicha.Location = new System.Drawing.Point(20, 692);
            this.btnFicha.Margin = new System.Windows.Forms.Padding(4);
            this.btnFicha.Name = "btnFicha";
            this.btnFicha.Size = new System.Drawing.Size(438, 42);
            this.btnFicha.TabIndex = 30;
            this.btnFicha.Text = "Ver ficha completa";
            this.btnFicha.UseVisualStyleBackColor = false;
            this.btnFicha.Click += new System.EventHandler(this.btnFicha_Click);
            //
            // lblFormulario
            //
            this.lblFormulario.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFormulario.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblFormulario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblFormulario.Location = new System.Drawing.Point(20, 20);
            this.lblFormulario.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFormulario.MinimumSize = new System.Drawing.Size(0, 40);
            this.lblFormulario.Name = "lblFormulario";
            this.lblFormulario.Size = new System.Drawing.Size(460, 40);
            this.lblFormulario.TabIndex = 0;
            this.lblFormulario.Text = "Nuevo socio";
            this.lblFormulario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblNombre
            //
            this.lblNombre.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblNombre.Location = new System.Drawing.Point(20, 60);
            this.lblNombre.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(88, 34);
            this.lblNombre.TabIndex = 1;
            this.lblNombre.Text = "Nombre:";
            this.lblNombre.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // nombre
            //
            this.nombre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nombre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nombre.Location = new System.Drawing.Point(112, 62);
            this.nombre.Margin = new System.Windows.Forms.Padding(4);
            this.nombre.MaxLength = 100;
            this.nombre.Name = "nombre";
            this.nombre.Size = new System.Drawing.Size(165, 29);
            this.nombre.TabIndex = 2;
            this.nombre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.nombre_KeyPress);
            //
            // lblApellido
            //
            this.lblApellido.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblApellido.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblApellido.Location = new System.Drawing.Point(20, 100);
            this.lblApellido.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblApellido.Name = "lblApellido";
            this.lblApellido.Size = new System.Drawing.Size(88, 34);
            this.lblApellido.TabIndex = 3;
            this.lblApellido.Text = "Apellido:";
            this.lblApellido.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // apellido
            //
            this.apellido.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.apellido.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.apellido.Location = new System.Drawing.Point(112, 103);
            this.apellido.Margin = new System.Windows.Forms.Padding(4);
            this.apellido.MaxLength = 100;
            this.apellido.Name = "apellido";
            this.apellido.Size = new System.Drawing.Size(165, 29);
            this.apellido.TabIndex = 4;
            this.apellido.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.apellido_KeyPress);
            //
            // lblDni
            //
            this.lblDni.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblDni.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblDni.Location = new System.Drawing.Point(20, 141);
            this.lblDni.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDni.Name = "lblDni";
            this.lblDni.Size = new System.Drawing.Size(88, 34);
            this.lblDni.TabIndex = 5;
            this.lblDni.Text = "DNI:";
            this.lblDni.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // dni
            //
            this.dni.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dni.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dni.Location = new System.Drawing.Point(112, 144);
            this.dni.Margin = new System.Windows.Forms.Padding(4);
            this.dni.MaxLength = 20;
            this.dni.Name = "dni";
            this.dni.Size = new System.Drawing.Size(165, 29);
            this.dni.TabIndex = 6;
            this.dni.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dni_KeyPress);
            //
            // lblFechaNacimiento
            //
            this.lblFechaNacimiento.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblFechaNacimiento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblFechaNacimiento.Location = new System.Drawing.Point(20, 182);
            this.lblFechaNacimiento.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFechaNacimiento.Name = "lblFechaNacimiento";
            this.lblFechaNacimiento.Size = new System.Drawing.Size(88, 34);
            this.lblFechaNacimiento.TabIndex = 7;
            this.lblFechaNacimiento.Text = "Nacimiento:";
            this.lblFechaNacimiento.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // fechaNacimiento
            //
            this.fechaNacimiento.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fechaNacimiento.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.fechaNacimiento.Location = new System.Drawing.Point(112, 185);
            this.fechaNacimiento.Margin = new System.Windows.Forms.Padding(4);
            this.fechaNacimiento.Name = "fechaNacimiento";
            this.fechaNacimiento.ShowCheckBox = true;
            this.fechaNacimiento.Size = new System.Drawing.Size(165, 29);
            this.fechaNacimiento.TabIndex = 8;
            //
            // lblPeso
            //
            this.lblPeso.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblPeso.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblPeso.Location = new System.Drawing.Point(20, 223);
            this.lblPeso.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPeso.Name = "lblPeso";
            this.lblPeso.Size = new System.Drawing.Size(88, 34);
            this.lblPeso.TabIndex = 9;
            this.lblPeso.Text = "Peso (kg):";
            this.lblPeso.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // peso
            //
            this.peso.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.peso.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.peso.Location = new System.Drawing.Point(112, 226);
            this.peso.Margin = new System.Windows.Forms.Padding(4);
            this.peso.Name = "peso";
            this.peso.Size = new System.Drawing.Size(165, 29);
            this.peso.TabIndex = 10;
            this.peso.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.peso_KeyPress);
            //
            // lblAltura
            //
            this.lblAltura.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblAltura.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblAltura.Location = new System.Drawing.Point(20, 264);
            this.lblAltura.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAltura.Name = "lblAltura";
            this.lblAltura.Size = new System.Drawing.Size(88, 34);
            this.lblAltura.TabIndex = 11;
            this.lblAltura.Text = "Altura (m):";
            this.lblAltura.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // altura
            //
            this.altura.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.altura.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.altura.Location = new System.Drawing.Point(112, 267);
            this.altura.Margin = new System.Windows.Forms.Padding(4);
            this.altura.Name = "altura";
            this.altura.Size = new System.Drawing.Size(165, 29);
            this.altura.TabIndex = 12;
            this.altura.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.altura_KeyPress);
            //
            // lblEstadoSocio
            //
            this.lblEstadoSocio.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblEstadoSocio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblEstadoSocio.Location = new System.Drawing.Point(20, 347);
            this.lblEstadoSocio.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEstadoSocio.Name = "lblEstadoSocio";
            this.lblEstadoSocio.Size = new System.Drawing.Size(88, 34);
            this.lblEstadoSocio.TabIndex = 15;
            this.lblEstadoSocio.Text = "Estado:";
            this.lblEstadoSocio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // estadoSocio
            //
            this.estadoSocio.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.estadoSocio.Location = new System.Drawing.Point(112, 347);
            this.estadoSocio.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.estadoSocio.Name = "estadoSocio";
            this.estadoSocio.Size = new System.Drawing.Size(165, 34);
            this.estadoSocio.TabIndex = 16;
            this.estadoSocio.Text = "Activo";
            this.estadoSocio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // nuevo
            //
            this.nuevo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.nuevo.FlatAppearance.BorderSize = 0;
            this.nuevo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(77)))), ((int)(((byte)(88)))));
            this.nuevo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(96)))), ((int)(((byte)(109)))));
            this.nuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nuevo.ForeColor = System.Drawing.Color.White;
            this.nuevo.Location = new System.Drawing.Point(0, 0);
            this.nuevo.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.nuevo.Name = "nuevo";
            this.nuevo.Size = new System.Drawing.Size(108, 40);
            this.nuevo.TabIndex = 17;
            this.nuevo.Text = "+ Nuevo socio";
            this.nuevo.UseVisualStyleBackColor = false;
            this.nuevo.Visible = false;
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
            this.guardar.Location = new System.Drawing.Point(0, 0);
            this.guardar.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.guardar.Name = "guardar";
            this.guardar.Size = new System.Drawing.Size(108, 40);
            this.guardar.TabIndex = 18;
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
            this.actualizar.Location = new System.Drawing.Point(254, 394);
            this.actualizar.Margin = new System.Windows.Forms.Padding(4);
            this.actualizar.Name = "actualizar";
            this.actualizar.Size = new System.Drawing.Size(109, 40);
            this.actualizar.TabIndex = 19;
            this.actualizar.Text = "Actualizar";
            this.actualizar.UseVisualStyleBackColor = false;
            this.actualizar.Visible = false;
            //
            // calcularImc
            //
            this.calcularImc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(75)))), ((int)(((byte)(138)))));
            this.calcularImc.FlatAppearance.BorderSize = 0;
            this.calcularImc.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(48)))), ((int)(((byte)(89)))));
            this.calcularImc.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(61)))), ((int)(((byte)(112)))));
            this.calcularImc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.calcularImc.ForeColor = System.Drawing.Color.White;
            this.calcularImc.Location = new System.Drawing.Point(0, 0);
            this.calcularImc.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.calcularImc.Name = "calcularImc";
            this.calcularImc.Size = new System.Drawing.Size(108, 40);
            this.calcularImc.TabIndex = 20;
            this.calcularImc.Text = "Calcular IMC";
            this.calcularImc.UseVisualStyleBackColor = false;
            this.calcularImc.Visible = false;
            this.calcularImc.Click += new System.EventHandler(this.calcularImc_Click);
            //
            // darDeBaja
            //
            this.darDeBaja.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.darDeBaja.FlatAppearance.BorderSize = 0;
            this.darDeBaja.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.darDeBaja.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.darDeBaja.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.darDeBaja.ForeColor = System.Drawing.Color.White;
            this.darDeBaja.Location = new System.Drawing.Point(0, 0);
            this.darDeBaja.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.darDeBaja.Name = "darDeBaja";
            this.darDeBaja.Size = new System.Drawing.Size(108, 40);
            this.darDeBaja.TabIndex = 21;
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
            this.reactivar.Location = new System.Drawing.Point(0, 0);
            this.reactivar.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.reactivar.Name = "reactivar";
            this.reactivar.Size = new System.Drawing.Size(108, 40);
            this.reactivar.TabIndex = 22;
            this.reactivar.Text = "Reactivar";
            this.reactivar.UseVisualStyleBackColor = false;
            this.reactivar.Visible = false;
            this.reactivar.Click += new System.EventHandler(this.reactivar_Click);
            //
            // verRutina
            //
            this.verRutina.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.verRutina.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(75)))), ((int)(((byte)(138)))));
            this.verRutina.FlatAppearance.BorderSize = 0;
            this.verRutina.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(48)))), ((int)(((byte)(89)))));
            this.verRutina.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(61)))), ((int)(((byte)(112)))));
            this.verRutina.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.verRutina.ForeColor = System.Drawing.Color.White;
            this.verRutina.Location = new System.Drawing.Point(181, 636);
            this.verRutina.Margin = new System.Windows.Forms.Padding(4);
            this.verRutina.Name = "verRutina";
            this.verRutina.Size = new System.Drawing.Size(279, 45);
            this.verRutina.TabIndex = 26;
            this.verRutina.Text = "Ver rutina semanal";
            this.verRutina.UseVisualStyleBackColor = false;
            this.verRutina.Click += new System.EventHandler(this.verRutina_Click);
            //
            // GestionSociosFormulario
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1375, 845);
            this.Controls.Add(this.panelContenido);
            this.Controls.Add(this.lblEstado);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1120, 688);
            this.Name = "GestionSociosFormulario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SysGym";
            this.Load += new System.EventHandler(this.GestionSociosFormulario_Load);
            ((System.ComponentModel.ISupportInitialize)(this.indicadorErrores)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fotoSocio)).EndInit();
            this.panelContenido.ResumeLayout(false);
            this.contenedorContenido.Panel1.ResumeLayout(false);
            this.contenedorContenido.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.contenedorContenido)).EndInit();
            this.contenedorContenido.ResumeLayout(false);
            this.panelListado.ResumeLayout(false);
            this.panelListado.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).EndInit();
            this.menuSocios.ResumeLayout(false);
            this.panelDetalle.ResumeLayout(false);
            this.panelDetalle.PerformLayout();
            this.ResumeLayout(false);

        }

    }
}
