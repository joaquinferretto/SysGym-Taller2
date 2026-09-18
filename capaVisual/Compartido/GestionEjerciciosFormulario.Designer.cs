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
        private Label lblBuscar;
        private TextBox buscador;
        private Label lblEstadoFiltro;
        private ComboBox filtroEstado;
        private Button nuevo;
        private Button actualizar;
        private Label lblEstado;
        private SplitContainer splitContenido;
        private Panel panelListado;
        private Label lblListadoTitulo;
        private DataGridView tabla;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colNombre;
        private DataGridViewTextBoxColumn colDescripcion;
        private DataGridViewTextBoxColumn colEstado;
        private Panel panelDetalle;
        private GroupBox grupoFicha;
        private Label lblDetalleTitulo;
        private TableLayoutPanel tablaFicha;
        private Label lblNombre;
        private TextBox nombre;
        private Label lblDescripcionEjercicio;
        private TextBox descripcion;
        private Label lblEstadoCampo;
        private Label lblEstadoValor;
        private FlowLayoutPanel accionesFicha;
        private GroupBox grupoImagenes;
        private FlowLayoutPanel galeriaImagenes;
        private Panel accionesImagenes;
        private Button agregarImagen;
        private Button quitarImagen;
        private Button guardar;
        private Button cancelar;
        private Button darDeBaja;
        private Button reactivar;

        protected override void Dispose(bool liberarRecursos)
        {
            if (liberarRecursos && components != null) components.Dispose();
            base.Dispose(liberarRecursos);
        }

        private void InitializeComponent()
        {
            this.panelEncabezado = new System.Windows.Forms.Panel();
            this.btnVolver = new System.Windows.Forms.Button();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.barraAcciones = new System.Windows.Forms.Panel();
            this.nuevo = new System.Windows.Forms.Button();
            this.actualizar = new System.Windows.Forms.Button();
            this.filtroEstado = new System.Windows.Forms.ComboBox();
            this.lblEstadoFiltro = new System.Windows.Forms.Label();
            this.buscador = new System.Windows.Forms.TextBox();
            this.lblBuscar = new System.Windows.Forms.Label();
            this.lblEstado = new System.Windows.Forms.Label();
            this.splitContenido = new System.Windows.Forms.SplitContainer();
            this.panelListado = new System.Windows.Forms.Panel();
            this.tabla = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblListadoTitulo = new System.Windows.Forms.Label();
            this.panelDetalle = new System.Windows.Forms.Panel();
            this.grupoFicha = new System.Windows.Forms.GroupBox();
            this.tablaFicha = new System.Windows.Forms.TableLayoutPanel();
            this.grupoImagenes = new System.Windows.Forms.GroupBox();
            this.galeriaImagenes = new System.Windows.Forms.FlowLayoutPanel();
            this.accionesImagenes = new System.Windows.Forms.Panel();
            this.agregarImagen = new System.Windows.Forms.Button();
            this.quitarImagen = new System.Windows.Forms.Button();
            this.lblNombre = new System.Windows.Forms.Label();
            this.nombre = new System.Windows.Forms.TextBox();
            this.lblDescripcionEjercicio = new System.Windows.Forms.Label();
            this.descripcion = new System.Windows.Forms.TextBox();
            this.lblEstadoCampo = new System.Windows.Forms.Label();
            this.lblEstadoValor = new System.Windows.Forms.Label();
            this.accionesFicha = new System.Windows.Forms.FlowLayoutPanel();
            this.guardar = new System.Windows.Forms.Button();
            this.darDeBaja = new System.Windows.Forms.Button();
            this.reactivar = new System.Windows.Forms.Button();
            this.cancelar = new System.Windows.Forms.Button();
            this.lblDetalleTitulo = new System.Windows.Forms.Label();
            this.panelEncabezado.SuspendLayout();
            this.barraAcciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContenido)).BeginInit();
            this.splitContenido.Panel1.SuspendLayout();
            this.splitContenido.Panel2.SuspendLayout();
            this.splitContenido.SuspendLayout();
            this.panelListado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).BeginInit();
            this.panelDetalle.SuspendLayout();
            this.grupoFicha.SuspendLayout();
            this.tablaFicha.SuspendLayout();
            this.grupoImagenes.SuspendLayout();
            this.accionesImagenes.SuspendLayout();
            this.accionesFicha.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEncabezado
            // 
            this.panelEncabezado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.panelEncabezado.Controls.Add(this.btnVolver);
            this.panelEncabezado.Controls.Add(this.lblDescripcion);
            this.panelEncabezado.Controls.Add(this.lblTitulo);
            this.panelEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEncabezado.Location = new System.Drawing.Point(0, 0);
            this.panelEncabezado.Name = "panelEncabezado";
            this.panelEncabezado.Padding = new System.Windows.Forms.Padding(20, 8, 20, 8);
            this.panelEncabezado.Size = new System.Drawing.Size(1100, 63);
            this.panelEncabezado.TabIndex = 3;
            this.panelEncabezado.Visible = false;
            // 
            // btnVolver
            // 
            this.btnVolver.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVolver.BackColor = System.Drawing.Color.White;
            this.btnVolver.FlatAppearance.BorderSize = 0;
            this.btnVolver.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVolver.Location = new System.Drawing.Point(1880, 10);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(100, 36);
            this.btnVolver.TabIndex = 0;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = false;
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.lblDescripcion.Location = new System.Drawing.Point(22, 39);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(700, 22);
            this.lblDescripcion.TabIndex = 1;
            this.lblDescripcion.Text = "Busca, selecciona y edita ejercicios disponibles para las rutinas";
            this.lblDescripcion.Visible = false;
            // 
            // lblTitulo
            // 
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(20, 8);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(700, 40);
            this.lblTitulo.TabIndex = 2;
            this.lblTitulo.Text = "Ejercicios | Catálogo de ejercicios";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // barraAcciones
            // 
            this.barraAcciones.BackColor = System.Drawing.Color.White;
            this.barraAcciones.Controls.Add(this.nuevo);
            this.barraAcciones.Controls.Add(this.actualizar);
            this.barraAcciones.Controls.Add(this.filtroEstado);
            this.barraAcciones.Controls.Add(this.lblEstadoFiltro);
            this.barraAcciones.Controls.Add(this.buscador);
            this.barraAcciones.Controls.Add(this.lblBuscar);
            this.barraAcciones.Dock = System.Windows.Forms.DockStyle.Top;
            this.barraAcciones.Location = new System.Drawing.Point(0, 63);
            this.barraAcciones.Name = "barraAcciones";
            this.barraAcciones.Padding = new System.Windows.Forms.Padding(16, 9, 16, 9);
            this.barraAcciones.Size = new System.Drawing.Size(1100, 54);
            this.barraAcciones.TabIndex = 2;
            // 
            // nuevo
            // 
            this.nuevo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nuevo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.nuevo.FlatAppearance.BorderSize = 0;
            this.nuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nuevo.ForeColor = System.Drawing.Color.White;
            this.nuevo.Location = new System.Drawing.Point(1802, 9);
            this.nuevo.Name = "nuevo";
            this.nuevo.Size = new System.Drawing.Size(178, 34);
            this.nuevo.TabIndex = 3;
            this.nuevo.Text = "+ Nuevo ejercicio";
            this.nuevo.UseVisualStyleBackColor = false;
            // 
            // actualizar
            // 
            this.actualizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.actualizar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.actualizar.FlatAppearance.BorderSize = 0;
            this.actualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.actualizar.Location = new System.Drawing.Point(1644, 9);
            this.actualizar.Name = "actualizar";
            this.actualizar.Size = new System.Drawing.Size(150, 34);
            this.actualizar.TabIndex = 2;
            this.actualizar.Text = "Actualizar listado";
            this.actualizar.UseVisualStyleBackColor = false;
            // 
            // filtroEstado
            // 
            this.filtroEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filtroEstado.Items.AddRange(new object[] {
            "Todos",
            "Activos",
            "Inactivos"});
            this.filtroEstado.Location = new System.Drawing.Point(382, 10);
            this.filtroEstado.Name = "filtroEstado";
            this.filtroEstado.Size = new System.Drawing.Size(120, 25);
            this.filtroEstado.TabIndex = 1;
            // 
            // lblEstadoFiltro
            // 
            this.lblEstadoFiltro.Location = new System.Drawing.Point(330, 10);
            this.lblEstadoFiltro.Name = "lblEstadoFiltro";
            this.lblEstadoFiltro.Size = new System.Drawing.Size(48, 28);
            this.lblEstadoFiltro.TabIndex = 4;
            this.lblEstadoFiltro.Text = "Estado:";
            this.lblEstadoFiltro.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buscador
            // 
            this.buscador.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.buscador.Location = new System.Drawing.Point(72, 10);
            this.buscador.Name = "buscador";
            this.buscador.Size = new System.Drawing.Size(240, 24);
            this.buscador.TabIndex = 0;
            // 
            // lblBuscar
            // 
            this.lblBuscar.Location = new System.Drawing.Point(16, 10);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(52, 28);
            this.lblBuscar.TabIndex = 5;
            this.lblBuscar.Text = "Buscar:";
            this.lblBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEstado
            // 
            this.lblEstado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.lblEstado.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblEstado.Location = new System.Drawing.Point(0, 652);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Padding = new System.Windows.Forms.Padding(16, 0, 8, 0);
            this.lblEstado.Size = new System.Drawing.Size(1100, 28);
            this.lblEstado.TabIndex = 1;
            this.lblEstado.Text = "Listo";
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // splitContenido
            // 
            this.splitContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContenido.Location = new System.Drawing.Point(0, 117);
            this.splitContenido.MinimumSize = new System.Drawing.Size(900, 420);
            this.splitContenido.Name = "splitContenido";
            // 
            // splitContenido.Panel1
            // 
            this.splitContenido.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.splitContenido.Panel1.Controls.Add(this.panelListado);
            this.splitContenido.Panel1.Padding = new System.Windows.Forms.Padding(16);
            this.splitContenido.Panel1MinSize = 420;
            // 
            // splitContenido.Panel2
            // 
            this.splitContenido.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.splitContenido.Panel2.Controls.Add(this.panelDetalle);
            this.splitContenido.Panel2.Padding = new System.Windows.Forms.Padding(0, 16, 16, 16);
            this.splitContenido.Panel2MinSize = 340;
            this.splitContenido.Size = new System.Drawing.Size(1100, 535);
            this.splitContenido.SplitterDistance = 625;
            this.splitContenido.SplitterWidth = 6;
            this.splitContenido.TabIndex = 0;
            // 
            // panelListado
            // 
            this.panelListado.Controls.Add(this.tabla);
            this.panelListado.Controls.Add(this.lblListadoTitulo);
            this.panelListado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelListado.Location = new System.Drawing.Point(16, 16);
            this.panelListado.Name = "panelListado";
            this.panelListado.Size = new System.Drawing.Size(593, 503);
            this.panelListado.TabIndex = 0;
            // 
            // tabla
            // 
            this.tabla.AllowUserToAddRows = false;
            this.tabla.AllowUserToDeleteRows = false;
            this.tabla.AllowUserToResizeRows = false;
            this.tabla.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tabla.BackgroundColor = System.Drawing.Color.White;
            this.tabla.ColumnHeadersHeight = 34;
            this.tabla.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colNombre,
            this.colDescripcion,
            this.colEstado});
            this.tabla.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabla.Location = new System.Drawing.Point(0, 34);
            this.tabla.MultiSelect = false;
            this.tabla.Name = "tabla";
            this.tabla.ReadOnly = true;
            this.tabla.RowHeadersVisible = false;
            this.tabla.RowTemplate.Height = 30;
            this.tabla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tabla.Size = new System.Drawing.Size(593, 469);
            this.tabla.TabIndex = 4;
            // 
            // colId
            // 
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
            // lblListadoTitulo
            // 
            this.lblListadoTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblListadoTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblListadoTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblListadoTitulo.Location = new System.Drawing.Point(0, 0);
            this.lblListadoTitulo.Name = "lblListadoTitulo";
            this.lblListadoTitulo.Size = new System.Drawing.Size(593, 34);
            this.lblListadoTitulo.TabIndex = 5;
            this.lblListadoTitulo.Text = "Ejercicios del catálogo";
            this.lblListadoTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelDetalle
            // 
            this.panelDetalle.Controls.Add(this.grupoFicha);
            this.panelDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDetalle.Location = new System.Drawing.Point(0, 16);
            this.panelDetalle.Name = "panelDetalle";
            this.panelDetalle.Size = new System.Drawing.Size(453, 503);
            this.panelDetalle.TabIndex = 0;
            // 
            // grupoFicha
            // 
            this.grupoFicha.BackColor = System.Drawing.Color.White;
            this.grupoFicha.Controls.Add(this.tablaFicha);
            this.grupoFicha.Controls.Add(this.lblDetalleTitulo);
            this.grupoFicha.Controls.Add(this.accionesFicha);
            this.grupoFicha.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grupoFicha.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.grupoFicha.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.grupoFicha.Location = new System.Drawing.Point(0, 0);
            this.grupoFicha.Name = "grupoFicha";
            this.grupoFicha.Padding = new System.Windows.Forms.Padding(16);
            this.grupoFicha.Size = new System.Drawing.Size(453, 503);
            this.grupoFicha.TabIndex = 0;
            this.grupoFicha.TabStop = false;
            this.grupoFicha.Text = "Ficha / Edición del ejercicio";
            this.grupoFicha.Enter += new System.EventHandler(this.grupoFicha_Enter);
            // 
            // tablaFicha
            // 
            this.tablaFicha.ColumnCount = 2;
            this.tablaFicha.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tablaFicha.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablaFicha.Controls.Add(this.grupoImagenes, 0, 3);
            this.tablaFicha.Controls.Add(this.lblNombre, 0, 0);
            this.tablaFicha.Controls.Add(this.nombre, 1, 0);
            this.tablaFicha.Controls.Add(this.lblDescripcionEjercicio, 0, 1);
            this.tablaFicha.Controls.Add(this.descripcion, 1, 1);
            this.tablaFicha.Controls.Add(this.lblEstadoCampo, 0, 2);
            this.tablaFicha.Controls.Add(this.lblEstadoValor, 1, 2);
            this.tablaFicha.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablaFicha.Location = new System.Drawing.Point(16, 66);
            this.tablaFicha.Name = "tablaFicha";
            this.tablaFicha.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.tablaFicha.RowCount = 4;
            this.tablaFicha.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tablaFicha.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablaFicha.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tablaFicha.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tablaFicha.Size = new System.Drawing.Size(421, 344);
            this.tablaFicha.TabIndex = 0;
            // 
            // grupoImagenes
            // 
            this.tablaFicha.SetColumnSpan(this.grupoImagenes, 2);
            this.grupoImagenes.Controls.Add(this.galeriaImagenes);
            this.grupoImagenes.Controls.Add(this.accionesImagenes);
            this.grupoImagenes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grupoImagenes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.grupoImagenes.Location = new System.Drawing.Point(3, 197);
            this.grupoImagenes.Name = "grupoImagenes";
            this.grupoImagenes.Padding = new System.Windows.Forms.Padding(8);
            this.grupoImagenes.Size = new System.Drawing.Size(415, 144);
            this.grupoImagenes.TabIndex = 0;
            this.grupoImagenes.TabStop = false;
            this.grupoImagenes.Text = "Imágenes del ejercicio";
            // 
            // galeriaImagenes
            // 
            this.galeriaImagenes.AutoScroll = true;
            this.galeriaImagenes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.galeriaImagenes.Location = new System.Drawing.Point(8, 20);
            this.galeriaImagenes.Name = "galeriaImagenes";
            this.galeriaImagenes.Padding = new System.Windows.Forms.Padding(4);
            this.galeriaImagenes.Size = new System.Drawing.Size(399, 76);
            this.galeriaImagenes.TabIndex = 0;
            // 
            // accionesImagenes
            // 
            this.accionesImagenes.Controls.Add(this.agregarImagen);
            this.accionesImagenes.Controls.Add(this.quitarImagen);
            this.accionesImagenes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.accionesImagenes.Location = new System.Drawing.Point(8, 102);
            this.accionesImagenes.Name = "accionesImagenes";
            this.accionesImagenes.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.accionesImagenes.Size = new System.Drawing.Size(399, 34);
            this.accionesImagenes.TabIndex = 1;
            // 
            // agregarImagen
            // 
            this.agregarImagen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.agregarImagen.FlatAppearance.BorderSize = 0;
            this.agregarImagen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.agregarImagen.Location = new System.Drawing.Point(128, 0);
            this.agregarImagen.Name = "agregarImagen";
            this.agregarImagen.Size = new System.Drawing.Size(116, 30);
            this.agregarImagen.TabIndex = 0;
            this.agregarImagen.Text = "Agregar imagen";
            this.agregarImagen.UseVisualStyleBackColor = false;
            this.agregarImagen.Click += new System.EventHandler(this.agregarImagen_Click_1);
            // 
            // quitarImagen
            // 
            this.quitarImagen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.quitarImagen.FlatAppearance.BorderSize = 0;
            this.quitarImagen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.quitarImagen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.quitarImagen.Location = new System.Drawing.Point(0, 0);
            this.quitarImagen.Name = "quitarImagen";
            this.quitarImagen.Size = new System.Drawing.Size(108, 30);
            this.quitarImagen.TabIndex = 1;
            this.quitarImagen.Text = "Quitar imagen";
            this.quitarImagen.UseVisualStyleBackColor = false;
            // 
            // lblNombre
            // 
            this.lblNombre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNombre.Location = new System.Drawing.Point(3, 8);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(104, 34);
            this.lblNombre.TabIndex = 1;
            this.lblNombre.Text = "Nombre";
            this.lblNombre.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nombre
            // 
            this.nombre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nombre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nombre.Location = new System.Drawing.Point(110, 11);
            this.nombre.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.nombre.Name = "nombre";
            this.nombre.Size = new System.Drawing.Size(311, 25);
            this.nombre.TabIndex = 0;
            this.nombre.TextChanged += new System.EventHandler(this.nombre_TextChanged);
            // 
            // lblDescripcionEjercicio
            // 
            this.lblDescripcionEjercicio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDescripcionEjercicio.Location = new System.Drawing.Point(3, 42);
            this.lblDescripcionEjercicio.Name = "lblDescripcionEjercicio";
            this.lblDescripcionEjercicio.Size = new System.Drawing.Size(104, 118);
            this.lblDescripcionEjercicio.TabIndex = 2;
            this.lblDescripcionEjercicio.Text = "Descripción";
            // 
            // descripcion
            // 
            this.descripcion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.descripcion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.descripcion.Location = new System.Drawing.Point(110, 45);
            this.descripcion.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.descripcion.Multiline = true;
            this.descripcion.Name = "descripcion";
            this.descripcion.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.descripcion.Size = new System.Drawing.Size(311, 112);
            this.descripcion.TabIndex = 1;
            // 
            // lblEstadoCampo
            // 
            this.lblEstadoCampo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEstadoCampo.Location = new System.Drawing.Point(3, 160);
            this.lblEstadoCampo.Name = "lblEstadoCampo";
            this.lblEstadoCampo.Size = new System.Drawing.Size(104, 34);
            this.lblEstadoCampo.TabIndex = 3;
            this.lblEstadoCampo.Text = "Estado";
            this.lblEstadoCampo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEstadoValor
            // 
            this.lblEstadoValor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEstadoValor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(101)))), ((int)(((byte)(52)))));
            this.lblEstadoValor.Location = new System.Drawing.Point(113, 160);
            this.lblEstadoValor.Name = "lblEstadoValor";
            this.lblEstadoValor.Size = new System.Drawing.Size(305, 34);
            this.lblEstadoValor.TabIndex = 4;
            this.lblEstadoValor.Text = "Activo";
            this.lblEstadoValor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // accionesFicha
            // 
            this.accionesFicha.Controls.Add(this.guardar);
            this.accionesFicha.Controls.Add(this.darDeBaja);
            this.accionesFicha.Controls.Add(this.reactivar);
            this.accionesFicha.Controls.Add(this.cancelar);
            this.accionesFicha.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.accionesFicha.Location = new System.Drawing.Point(16, 410);
            this.accionesFicha.Name = "accionesFicha";
            this.accionesFicha.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.accionesFicha.Size = new System.Drawing.Size(421, 77);
            this.accionesFicha.TabIndex = 1;
            this.accionesFicha.WrapContents = false;
            this.accionesFicha.Paint += new System.Windows.Forms.PaintEventHandler(this.accionesFicha_Paint);
            // 
            // guardar
            // 
            this.guardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.guardar.FlatAppearance.BorderSize = 0;
            this.guardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.guardar.ForeColor = System.Drawing.Color.White;
            this.guardar.Location = new System.Drawing.Point(3, 11);
            this.guardar.Name = "guardar";
            this.guardar.Size = new System.Drawing.Size(112, 34);
            this.guardar.TabIndex = 7;
            this.guardar.Text = "Guardar";
            this.guardar.UseVisualStyleBackColor = false;
            // 
            // darDeBaja
            // 
            this.darDeBaja.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.darDeBaja.FlatAppearance.BorderSize = 0;
            this.darDeBaja.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.darDeBaja.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.darDeBaja.Location = new System.Drawing.Point(121, 11);
            this.darDeBaja.Name = "darDeBaja";
            this.darDeBaja.Size = new System.Drawing.Size(120, 34);
            this.darDeBaja.TabIndex = 9;
            this.darDeBaja.Text = "Dar de baja";
            this.darDeBaja.UseVisualStyleBackColor = false;
            this.darDeBaja.Visible = false;
            // 
            // reactivar
            // 
            this.reactivar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(252)))), ((int)(((byte)(231)))));
            this.reactivar.FlatAppearance.BorderSize = 0;
            this.reactivar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.reactivar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(101)))), ((int)(((byte)(52)))));
            this.reactivar.Location = new System.Drawing.Point(247, 11);
            this.reactivar.Name = "reactivar";
            this.reactivar.Size = new System.Drawing.Size(100, 34);
            this.reactivar.TabIndex = 10;
            this.reactivar.Text = "Reactivar";
            this.reactivar.UseVisualStyleBackColor = false;
            this.reactivar.Visible = false;
            this.reactivar.Click += new System.EventHandler(this.reactivar_Click_1);
            // 
            // cancelar
            // 
            this.cancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.cancelar.FlatAppearance.BorderSize = 0;
            this.cancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelar.Location = new System.Drawing.Point(353, 11);
            this.cancelar.Name = "cancelar";
            this.cancelar.Size = new System.Drawing.Size(100, 34);
            this.cancelar.TabIndex = 8;
            this.cancelar.Text = "Cancelar";
            this.cancelar.UseVisualStyleBackColor = false;
            // 
            // lblDetalleTitulo
            // 
            this.lblDetalleTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDetalleTitulo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblDetalleTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.lblDetalleTitulo.Location = new System.Drawing.Point(16, 34);
            this.lblDetalleTitulo.Name = "lblDetalleTitulo";
            this.lblDetalleTitulo.Size = new System.Drawing.Size(421, 32);
            this.lblDetalleTitulo.TabIndex = 2;
            this.lblDetalleTitulo.Text = "Nuevo ejercicio";
            // 
            // GestionEjerciciosFormulario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1100, 680);
            this.Controls.Add(this.splitContenido);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.barraAcciones);
            this.Controls.Add(this.panelEncabezado);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.MinimumSize = new System.Drawing.Size(900, 560);
            this.Name = "GestionEjerciciosFormulario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SysGym | Ejercicios";
            this.panelEncabezado.ResumeLayout(false);
            this.barraAcciones.ResumeLayout(false);
            this.barraAcciones.PerformLayout();
            this.splitContenido.Panel1.ResumeLayout(false);
            this.splitContenido.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContenido)).EndInit();
            this.splitContenido.ResumeLayout(false);
            this.panelListado.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).EndInit();
            this.panelDetalle.ResumeLayout(false);
            this.grupoFicha.ResumeLayout(false);
            this.tablaFicha.ResumeLayout(false);
            this.tablaFicha.PerformLayout();
            this.grupoImagenes.ResumeLayout(false);
            this.accionesImagenes.ResumeLayout(false);
            this.accionesFicha.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
