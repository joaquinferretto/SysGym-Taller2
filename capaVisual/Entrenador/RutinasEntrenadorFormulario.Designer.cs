using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Entrenador
{
    partial class RutinasEntrenadorFormulario
    {
        private IContainer components = null;
        private Panel panelEncabezado;
        private Label lblTitulo;
        private Label lblDescripcion;
        private Button btnVolver;
        private Panel barraAcciones;
        private Label lblEstado;
        private Panel panelContenido;
        private TableLayoutPanel layoutDetalle;
        private TableLayoutPanel panelListado;
        private Label lblListadoTitulo;
        private Label lblDetalleTitulo;
        private TableLayoutPanel contenedorRutina;
        private FlowLayoutPanel accionesRutina;
        private TableLayoutPanel panelEjercicios;
        private Label lblEjerciciosTitulo;
        private FlowLayoutPanel accionesEjercicios;
        private Label lblDetalleEjercicio;
        private TableLayoutPanel contenedorFormulario;
        private FlowLayoutPanel accionesFormulario;
        private Label lblNombre;
        private Label lblDescripcionRutina;
        private Label lblEjercicio;
        private Label lblSeries;
        private Label lblRepeticiones;
        private Label lblPeso;
        private Label lblDescanso;
        private Label lblOrden;
        private Label lblDia;
        private DataGridView tabla;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colRutina;
        private DataGridViewTextBoxColumn colCreador;
        private DataGridViewTextBoxColumn colEstado;
        private ComboBox dia;
        private ComboBox ejercicio;
        private TextBox nombre;
        private TextBox descripcion;
        private TextBox series;
        private TextBox repeticiones;
        private TextBox peso;
        private TextBox descanso;
        private TextBox orden;
        private Button nuevaRutina;
        private Button guardarRutina;
        private Button actualizar;
        private Button agregarEjercicio;
        private Button actualizarEjercicio;
        private Button darDeBaja;
        private Button reactivar;
        private Button quitarEjercicio;
        private Button guardarEjercicio;
        private Button cancelarEjercicio;
        private DataGridView tablaEjercicios;
        private DataGridViewTextBoxColumn colDetalleId;
        private DataGridViewTextBoxColumn colDetalleDia;
        private DataGridViewTextBoxColumn colDetalleOrden;
        private DataGridViewTextBoxColumn colDetalleEjercicio;
        private DataGridViewTextBoxColumn colDetalleSeries;
        private DataGridViewTextBoxColumn colDetalleRepeticiones;
        private DataGridViewTextBoxColumn colDetallePeso;
        private DataGridViewTextBoxColumn colDetalleDescanso;

        protected override void Dispose(bool liberarRecursos)
        {
            if (liberarRecursos && components != null) components.Dispose();
            base.Dispose(liberarRecursos);
        }

        private void InitializeComponent()
        {
            this.panelEncabezado = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.btnVolver = new System.Windows.Forms.Button();
            this.barraAcciones = new System.Windows.Forms.Panel();
            this.lblEstado = new System.Windows.Forms.Label();
            this.panelContenido = new System.Windows.Forms.Panel();
            this.panelListado = new System.Windows.Forms.TableLayoutPanel();
            this.lblListadoTitulo = new System.Windows.Forms.Label();
            this.tabla = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRutina = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCreador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nuevaRutina = new System.Windows.Forms.Button();
            this.layoutDetalle = new System.Windows.Forms.TableLayoutPanel();
            this.contenedorRutina = new System.Windows.Forms.TableLayoutPanel();
            this.lblDetalleTitulo = new System.Windows.Forms.Label();
            this.accionesRutina = new System.Windows.Forms.FlowLayoutPanel();
            this.guardarRutina = new System.Windows.Forms.Button();
            this.actualizar = new System.Windows.Forms.Button();
            this.darDeBaja = new System.Windows.Forms.Button();
            this.reactivar = new System.Windows.Forms.Button();
            this.lblNombre = new System.Windows.Forms.Label();
            this.nombre = new System.Windows.Forms.TextBox();
            this.lblDescripcionRutina = new System.Windows.Forms.Label();
            this.descripcion = new System.Windows.Forms.TextBox();
            this.contenedorFormulario = new System.Windows.Forms.TableLayoutPanel();
            this.lblDetalleEjercicio = new System.Windows.Forms.Label();
            this.accionesFormulario = new System.Windows.Forms.FlowLayoutPanel();
            this.guardarEjercicio = new System.Windows.Forms.Button();
            this.cancelarEjercicio = new System.Windows.Forms.Button();
            this.lblEjercicio = new System.Windows.Forms.Label();
            this.ejercicio = new System.Windows.Forms.ComboBox();
            this.lblDia = new System.Windows.Forms.Label();
            this.dia = new System.Windows.Forms.ComboBox();
            this.lblSeries = new System.Windows.Forms.Label();
            this.series = new System.Windows.Forms.TextBox();
            this.lblRepeticiones = new System.Windows.Forms.Label();
            this.repeticiones = new System.Windows.Forms.TextBox();
            this.lblPeso = new System.Windows.Forms.Label();
            this.peso = new System.Windows.Forms.TextBox();
            this.lblDescanso = new System.Windows.Forms.Label();
            this.descanso = new System.Windows.Forms.TextBox();
            this.lblOrden = new System.Windows.Forms.Label();
            this.orden = new System.Windows.Forms.TextBox();
            this.panelEjercicios = new System.Windows.Forms.TableLayoutPanel();
            this.lblEjerciciosTitulo = new System.Windows.Forms.Label();
            this.accionesEjercicios = new System.Windows.Forms.FlowLayoutPanel();
            this.agregarEjercicio = new System.Windows.Forms.Button();
            this.actualizarEjercicio = new System.Windows.Forms.Button();
            this.quitarEjercicio = new System.Windows.Forms.Button();
            this.tablaEjercicios = new System.Windows.Forms.DataGridView();
            this.colDetalleId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetalleDia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetalleOrden = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetalleEjercicio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetalleSeries = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetalleRepeticiones = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetallePeso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetalleDescanso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEncabezado.SuspendLayout();
            this.panelContenido.SuspendLayout();
            this.panelListado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).BeginInit();
            this.layoutDetalle.SuspendLayout();
            this.contenedorRutina.SuspendLayout();
            this.accionesRutina.SuspendLayout();
            this.contenedorFormulario.SuspendLayout();
            this.accionesFormulario.SuspendLayout();
            this.panelEjercicios.SuspendLayout();
            this.accionesEjercicios.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablaEjercicios)).BeginInit();
            this.SuspendLayout();
            //
            // panelEncabezado
            //
            this.panelEncabezado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(116)))), ((int)(((byte)(144)))));
            this.panelEncabezado.Controls.Add(this.lblTitulo);
            this.panelEncabezado.Controls.Add(this.lblDescripcion);
            this.panelEncabezado.Controls.Add(this.btnVolver);
            this.panelEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEncabezado.Location = new System.Drawing.Point(0, 0);
            this.panelEncabezado.Name = "panelEncabezado";
            this.panelEncabezado.Padding = new System.Windows.Forms.Padding(20, 8, 20, 8);
            this.panelEncabezado.Size = new System.Drawing.Size(1100, 56);
            this.panelEncabezado.TabIndex = 3;
            this.panelEncabezado.Visible = false;
            //
            // lblTitulo
            //
            this.lblTitulo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(20, 8);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(1790, 40);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Gestionar rutinas | Catálogo y composición de rutinas";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblDescripcion
            //
            this.lblDescripcion.Location = new System.Drawing.Point(0, 0);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(100, 23);
            this.lblDescripcion.TabIndex = 1;
            this.lblDescripcion.Visible = false;
            //
            // btnVolver
            //
            this.btnVolver.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVolver.BackColor = System.Drawing.Color.White;
            this.btnVolver.FlatAppearance.BorderSize = 0;
            this.btnVolver.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVolver.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(116)))), ((int)(((byte)(144)))));
            this.btnVolver.Location = new System.Drawing.Point(1874, 10);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(104, 36);
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
            this.barraAcciones.Size = new System.Drawing.Size(1100, 1);
            this.barraAcciones.TabIndex = 2;
            //
            // lblEstado
            //
            this.lblEstado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.lblEstado.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblEstado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblEstado.Location = new System.Drawing.Point(0, 650);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Padding = new System.Windows.Forms.Padding(18, 0, 12, 0);
            this.lblEstado.Size = new System.Drawing.Size(1100, 30);
            this.lblEstado.TabIndex = 1;
            this.lblEstado.Text = "Listo";
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // panelContenido
            //
            this.panelContenido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelContenido.Controls.Add(this.panelListado);
            this.panelContenido.Controls.Add(this.layoutDetalle);
            this.panelContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenido.Location = new System.Drawing.Point(0, 57);
            this.panelContenido.Name = "panelContenido";
            this.panelContenido.Padding = new System.Windows.Forms.Padding(16);
            this.panelContenido.Size = new System.Drawing.Size(1100, 593);
            this.panelContenido.TabIndex = 0;
            //
            // panelListado
            //
            this.panelListado.BackColor = System.Drawing.Color.White;
            this.panelListado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelListado.ColumnCount = 1;
            this.panelListado.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelListado.Controls.Add(this.lblListadoTitulo, 0, 0);
            this.panelListado.Controls.Add(this.tabla, 0, 1);
            this.panelListado.Controls.Add(this.nuevaRutina, 0, 2);
            this.panelListado.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left)));
            this.panelListado.Location = new System.Drawing.Point(24, 24);
            this.panelListado.Name = "panelListado";
            this.panelListado.Padding = new System.Windows.Forms.Padding(10);
            this.panelListado.RowCount = 3;
            this.panelListado.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.panelListado.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelListado.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.panelListado.Size = new System.Drawing.Size(314, 545);
            this.panelListado.TabIndex = 0;
            //
            // lblListadoTitulo
            //
            this.lblListadoTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblListadoTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblListadoTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblListadoTitulo.Location = new System.Drawing.Point(13, 10);
            this.lblListadoTitulo.Name = "lblListadoTitulo";
            this.lblListadoTitulo.Size = new System.Drawing.Size(286, 30);
            this.lblListadoTitulo.TabIndex = 2;
            this.lblListadoTitulo.Text = "Rutinas";
            this.lblListadoTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // tabla
            //
            this.tabla.AllowUserToAddRows = false;
            this.tabla.AllowUserToDeleteRows = false;
            this.tabla.AllowUserToResizeRows = false;
            this.tabla.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tabla.BackgroundColor = System.Drawing.Color.White;
            this.tabla.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tabla.ColumnHeadersHeight = 29;
            this.tabla.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colRutina,
            this.colCreador,
            this.colEstado});
            this.tabla.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabla.Location = new System.Drawing.Point(13, 43);
            this.tabla.MultiSelect = false;
            this.tabla.Name = "tabla";
            this.tabla.ReadOnly = true;
            this.tabla.RowHeadersVisible = false;
            this.tabla.RowHeadersWidth = 51;
            this.tabla.RowTemplate.Height = 28;
            this.tabla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tabla.Size = new System.Drawing.Size(286, 443);
            this.tabla.TabIndex = 1;
            this.tabla.SelectionChanged += new System.EventHandler(this.tabla_SelectionChanged);
            //
            // colId
            //
            this.colId.MinimumWidth = 6;
            this.colId.Name = "colId";
            this.colId.ReadOnly = true;
            this.colId.Visible = false;
            //
            // colRutina
            //
            this.colRutina.FillWeight = 45F;
            this.colRutina.HeaderText = "Rutina";
            this.colRutina.MinimumWidth = 20;
            this.colRutina.Name = "colRutina";
            this.colRutina.ReadOnly = true;
            //
            // colCreador
            //
            this.colCreador.FillWeight = 35F;
            this.colCreador.HeaderText = "Entrenador";
            this.colCreador.MinimumWidth = 20;
            this.colCreador.Name = "colCreador";
            this.colCreador.ReadOnly = true;
            //
            // colEstado
            //
            this.colEstado.FillWeight = 20F;
            this.colEstado.HeaderText = "Estado";
            this.colEstado.MinimumWidth = 20;
            this.colEstado.Name = "colEstado";
            this.colEstado.ReadOnly = true;
            //
            // nuevaRutina
            //
            this.nuevaRutina.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.nuevaRutina.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.nuevaRutina.FlatAppearance.BorderSize = 0;
            this.nuevaRutina.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nuevaRutina.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.nuevaRutina.Location = new System.Drawing.Point(10, 497);
            this.nuevaRutina.Margin = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.nuevaRutina.Name = "nuevaRutina";
            this.nuevaRutina.Size = new System.Drawing.Size(292, 36);
            this.nuevaRutina.TabIndex = 0;
            this.nuevaRutina.Text = "+ Nueva rutina";
            this.nuevaRutina.UseVisualStyleBackColor = false;
            this.nuevaRutina.Click += new System.EventHandler(this.nuevaRutina_Click);
            //
            // layoutDetalle
            //
            this.layoutDetalle.ColumnCount = 1;
            this.layoutDetalle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutDetalle.Controls.Add(this.contenedorRutina, 0, 0);
            this.layoutDetalle.Controls.Add(this.contenedorFormulario, 0, 2);
            this.layoutDetalle.Controls.Add(this.panelEjercicios, 0, 1);
            this.layoutDetalle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.layoutDetalle.Location = new System.Drawing.Point(360, 24);
            this.layoutDetalle.Name = "layoutDetalle";
            this.layoutDetalle.RowCount = 3;
            this.layoutDetalle.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutDetalle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutDetalle.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.layoutDetalle.Size = new System.Drawing.Size(716, 545);
            this.layoutDetalle.TabIndex = 1;
            //
            // contenedorRutina
            //
            this.contenedorRutina.AutoSize = true;
            this.contenedorRutina.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.contenedorRutina.BackColor = System.Drawing.Color.White;
            this.contenedorRutina.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.contenedorRutina.ColumnCount = 2;
            this.contenedorRutina.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.contenedorRutina.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.contenedorRutina.Controls.Add(this.lblDetalleTitulo, 0, 0);
            this.contenedorRutina.Controls.Add(this.accionesRutina, 0, 3);
            this.contenedorRutina.Controls.Add(this.lblNombre, 0, 1);
            this.contenedorRutina.Controls.Add(this.nombre, 1, 1);
            this.contenedorRutina.Controls.Add(this.lblDescripcionRutina, 0, 2);
            this.contenedorRutina.Controls.Add(this.descripcion, 1, 2);
            this.contenedorRutina.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contenedorRutina.Location = new System.Drawing.Point(3, 3);
            this.contenedorRutina.Name = "contenedorRutina";
            this.contenedorRutina.Padding = new System.Windows.Forms.Padding(10);
            this.contenedorRutina.RowCount = 4;
            this.contenedorRutina.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.contenedorRutina.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.contenedorRutina.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.contenedorRutina.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.contenedorRutina.Size = new System.Drawing.Size(710, 153);
            this.contenedorRutina.TabIndex = 0;
            //
            // lblDetalleTitulo
            //
            this.contenedorRutina.SetColumnSpan(this.lblDetalleTitulo, 2);
            this.lblDetalleTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDetalleTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblDetalleTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblDetalleTitulo.Location = new System.Drawing.Point(13, 10);
            this.lblDetalleTitulo.Name = "lblDetalleTitulo";
            this.lblDetalleTitulo.Size = new System.Drawing.Size(682, 25);
            this.lblDetalleTitulo.TabIndex = 2;
            this.lblDetalleTitulo.Text = "Rutina seleccionada";
            this.lblDetalleTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // accionesRutina
            //
            this.accionesRutina.AutoSize = true;
            this.accionesRutina.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.contenedorRutina.SetColumnSpan(this.accionesRutina, 2);
            this.accionesRutina.Controls.Add(this.guardarRutina);
            this.accionesRutina.Controls.Add(this.actualizar);
            this.accionesRutina.Controls.Add(this.darDeBaja);
            this.accionesRutina.Controls.Add(this.reactivar);
            this.accionesRutina.Dock = System.Windows.Forms.DockStyle.Fill;
            this.accionesRutina.Location = new System.Drawing.Point(13, 104);
            this.accionesRutina.Name = "accionesRutina";
            this.accionesRutina.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.accionesRutina.Size = new System.Drawing.Size(682, 34);
            this.accionesRutina.TabIndex = 1;
            this.accionesRutina.WrapContents = false;
            //
            // guardarRutina
            //
            this.guardarRutina.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(116)))), ((int)(((byte)(144)))));
            this.guardarRutina.FlatAppearance.BorderSize = 0;
            this.guardarRutina.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.guardarRutina.ForeColor = System.Drawing.Color.White;
            this.guardarRutina.Location = new System.Drawing.Point(0, 2);
            this.guardarRutina.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.guardarRutina.Name = "guardarRutina";
            this.guardarRutina.Size = new System.Drawing.Size(108, 32);
            this.guardarRutina.TabIndex = 1;
            this.guardarRutina.Text = "Guardar";
            this.guardarRutina.UseVisualStyleBackColor = false;
            this.guardarRutina.Click += new System.EventHandler(this.guardarRutina_Click);
            //
            // actualizar
            //
            this.actualizar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.actualizar.FlatAppearance.BorderSize = 0;
            this.actualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.actualizar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.actualizar.Location = new System.Drawing.Point(114, 2);
            this.actualizar.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.actualizar.Name = "actualizar";
            this.actualizar.Size = new System.Drawing.Size(108, 32);
            this.actualizar.TabIndex = 2;
            this.actualizar.Text = "Actualizar";
            this.actualizar.UseVisualStyleBackColor = false;
            this.actualizar.Click += new System.EventHandler(this.actualizar_Click);
            //
            // darDeBaja
            //
            this.darDeBaja.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.darDeBaja.FlatAppearance.BorderSize = 0;
            this.darDeBaja.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.darDeBaja.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.darDeBaja.Location = new System.Drawing.Point(228, 2);
            this.darDeBaja.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.darDeBaja.Name = "darDeBaja";
            this.darDeBaja.Size = new System.Drawing.Size(108, 32);
            this.darDeBaja.TabIndex = 5;
            this.darDeBaja.Text = "Dar de baja";
            this.darDeBaja.UseVisualStyleBackColor = false;
            this.darDeBaja.Visible = false;
            this.darDeBaja.Click += new System.EventHandler(this.darDeBaja_Click);
            //
            // reactivar
            //
            this.reactivar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(252)))), ((int)(((byte)(231)))));
            this.reactivar.FlatAppearance.BorderSize = 0;
            this.reactivar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.reactivar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(101)))), ((int)(((byte)(52)))));
            this.reactivar.Location = new System.Drawing.Point(342, 2);
            this.reactivar.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.reactivar.Name = "reactivar";
            this.reactivar.Size = new System.Drawing.Size(108, 32);
            this.reactivar.TabIndex = 6;
            this.reactivar.Text = "Reactivar";
            this.reactivar.UseVisualStyleBackColor = false;
            this.reactivar.Visible = false;
            this.reactivar.Click += new System.EventHandler(this.reactivar_Click);
            //
            // lblNombre
            //
            this.lblNombre.AutoSize = true;
            this.lblNombre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNombre.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblNombre.Location = new System.Drawing.Point(10, 37);
            this.lblNombre.Margin = new System.Windows.Forms.Padding(0, 2, 8, 2);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(93, 29);
            this.lblNombre.TabIndex = 0;
            this.lblNombre.Text = "Nombre:";
            this.lblNombre.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // nombre
            //
            this.nombre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nombre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nombre.Location = new System.Drawing.Point(111, 37);
            this.nombre.Margin = new System.Windows.Forms.Padding(0, 2, 8, 2);
            this.nombre.Name = "nombre";
            this.nombre.Size = new System.Drawing.Size(579, 29);
            this.nombre.TabIndex = 1;
            //
            // lblDescripcionRutina
            //
            this.lblDescripcionRutina.AutoSize = true;
            this.lblDescripcionRutina.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDescripcionRutina.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblDescripcionRutina.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblDescripcionRutina.Location = new System.Drawing.Point(10, 70);
            this.lblDescripcionRutina.Margin = new System.Windows.Forms.Padding(0, 2, 8, 2);
            this.lblDescripcionRutina.Name = "lblDescripcionRutina";
            this.lblDescripcionRutina.Size = new System.Drawing.Size(93, 29);
            this.lblDescripcionRutina.TabIndex = 2;
            this.lblDescripcionRutina.Text = "Descripción:";
            this.lblDescripcionRutina.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // descripcion
            //
            this.descripcion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.descripcion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.descripcion.Location = new System.Drawing.Point(111, 70);
            this.descripcion.Margin = new System.Windows.Forms.Padding(0, 2, 8, 2);
            this.descripcion.Name = "descripcion";
            this.descripcion.Size = new System.Drawing.Size(579, 29);
            this.descripcion.TabIndex = 3;
            //
            // contenedorFormulario
            //
            this.contenedorFormulario.AutoSize = true;
            this.contenedorFormulario.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.contenedorFormulario.BackColor = System.Drawing.Color.White;
            this.contenedorFormulario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.contenedorFormulario.ColumnCount = 4;
            this.contenedorFormulario.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.contenedorFormulario.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.contenedorFormulario.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.contenedorFormulario.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.contenedorFormulario.Controls.Add(this.lblDetalleEjercicio, 0, 0);
            this.contenedorFormulario.Controls.Add(this.accionesFormulario, 0, 5);
            this.contenedorFormulario.Controls.Add(this.lblEjercicio, 0, 1);
            this.contenedorFormulario.Controls.Add(this.ejercicio, 1, 1);
            this.contenedorFormulario.Controls.Add(this.lblDia, 2, 1);
            this.contenedorFormulario.Controls.Add(this.dia, 3, 1);
            this.contenedorFormulario.Controls.Add(this.lblSeries, 0, 2);
            this.contenedorFormulario.Controls.Add(this.series, 1, 2);
            this.contenedorFormulario.Controls.Add(this.lblRepeticiones, 2, 2);
            this.contenedorFormulario.Controls.Add(this.repeticiones, 3, 2);
            this.contenedorFormulario.Controls.Add(this.lblPeso, 0, 3);
            this.contenedorFormulario.Controls.Add(this.peso, 1, 3);
            this.contenedorFormulario.Controls.Add(this.lblDescanso, 2, 3);
            this.contenedorFormulario.Controls.Add(this.descanso, 3, 3);
            this.contenedorFormulario.Controls.Add(this.lblOrden, 0, 4);
            this.contenedorFormulario.Controls.Add(this.orden, 1, 4);
            this.contenedorFormulario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contenedorFormulario.Location = new System.Drawing.Point(3, 327);
            this.contenedorFormulario.Name = "contenedorFormulario";
            this.contenedorFormulario.Padding = new System.Windows.Forms.Padding(10);
            this.contenedorFormulario.RowCount = 6;
            this.contenedorFormulario.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.contenedorFormulario.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.contenedorFormulario.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.contenedorFormulario.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.contenedorFormulario.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.contenedorFormulario.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.contenedorFormulario.Size = new System.Drawing.Size(710, 215);
            this.contenedorFormulario.TabIndex = 0;
            //
            // lblDetalleEjercicio
            //
            this.contenedorFormulario.SetColumnSpan(this.lblDetalleEjercicio, 4);
            this.lblDetalleEjercicio.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDetalleEjercicio.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblDetalleEjercicio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblDetalleEjercicio.Location = new System.Drawing.Point(13, 10);
            this.lblDetalleEjercicio.Name = "lblDetalleEjercicio";
            this.lblDetalleEjercicio.Size = new System.Drawing.Size(682, 26);
            this.lblDetalleEjercicio.TabIndex = 2;
            this.lblDetalleEjercicio.Text = "Detalle del ejercicio";
            this.lblDetalleEjercicio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // accionesFormulario
            //
            this.accionesFormulario.AutoSize = true;
            this.accionesFormulario.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.contenedorFormulario.SetColumnSpan(this.accionesFormulario, 4);
            this.accionesFormulario.Controls.Add(this.guardarEjercicio);
            this.accionesFormulario.Controls.Add(this.cancelarEjercicio);
            this.accionesFormulario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.accionesFormulario.Location = new System.Drawing.Point(13, 166);
            this.accionesFormulario.Name = "accionesFormulario";
            this.accionesFormulario.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.accionesFormulario.Size = new System.Drawing.Size(682, 34);
            this.accionesFormulario.TabIndex = 1;
            this.accionesFormulario.WrapContents = false;
            //
            // guardarEjercicio
            //
            this.guardarEjercicio.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(116)))), ((int)(((byte)(144)))));
            this.guardarEjercicio.FlatAppearance.BorderSize = 0;
            this.guardarEjercicio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.guardarEjercicio.ForeColor = System.Drawing.Color.White;
            this.guardarEjercicio.Location = new System.Drawing.Point(0, 2);
            this.guardarEjercicio.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.guardarEjercicio.Name = "guardarEjercicio";
            this.guardarEjercicio.Size = new System.Drawing.Size(120, 32);
            this.guardarEjercicio.TabIndex = 8;
            this.guardarEjercicio.Text = "Agregar";
            this.guardarEjercicio.UseVisualStyleBackColor = false;
            this.guardarEjercicio.Visible = false;
            this.guardarEjercicio.Click += new System.EventHandler(this.guardarEjercicio_Click);
            //
            // cancelarEjercicio
            //
            this.cancelarEjercicio.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.cancelarEjercicio.FlatAppearance.BorderSize = 0;
            this.cancelarEjercicio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelarEjercicio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.cancelarEjercicio.Location = new System.Drawing.Point(126, 2);
            this.cancelarEjercicio.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.cancelarEjercicio.Name = "cancelarEjercicio";
            this.cancelarEjercicio.Size = new System.Drawing.Size(100, 32);
            this.cancelarEjercicio.TabIndex = 9;
            this.cancelarEjercicio.Text = "Cancelar";
            this.cancelarEjercicio.UseVisualStyleBackColor = false;
            this.cancelarEjercicio.Visible = false;
            this.cancelarEjercicio.Click += new System.EventHandler(this.cancelarEjercicio_Click);
            //
            // lblEjercicio
            //
            this.lblEjercicio.AutoSize = true;
            this.lblEjercicio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEjercicio.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblEjercicio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblEjercicio.Location = new System.Drawing.Point(10, 38);
            this.lblEjercicio.Margin = new System.Windows.Forms.Padding(0, 2, 8, 2);
            this.lblEjercicio.Name = "lblEjercicio";
            this.lblEjercicio.Size = new System.Drawing.Size(70, 24);
            this.lblEjercicio.TabIndex = 0;
            this.lblEjercicio.Text = "Ejercicio:";
            this.lblEjercicio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // ejercicio
            //
            this.ejercicio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ejercicio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ejercicio.Location = new System.Drawing.Point(88, 38);
            this.ejercicio.Margin = new System.Windows.Forms.Padding(0, 2, 8, 2);
            this.ejercicio.Name = "ejercicio";
            this.ejercicio.Size = new System.Drawing.Size(193, 29);
            this.ejercicio.TabIndex = 1;
            //
            // lblDia
            //
            this.lblDia.AutoSize = true;
            this.lblDia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDia.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblDia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblDia.Location = new System.Drawing.Point(289, 38);
            this.lblDia.Margin = new System.Windows.Forms.Padding(0, 2, 8, 2);
            this.lblDia.Name = "lblDia";
            this.lblDia.Size = new System.Drawing.Size(99, 24);
            this.lblDia.TabIndex = 2;
            this.lblDia.Text = "Día:";
            this.lblDia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // dia
            //
            this.dia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dia.Items.AddRange(new object[] {
            "Lunes",
            "Martes",
            "Miercoles",
            "Jueves",
            "Viernes"});
            this.dia.Location = new System.Drawing.Point(396, 38);
            this.dia.Margin = new System.Windows.Forms.Padding(0, 2, 8, 2);
            this.dia.Name = "dia";
            this.dia.Size = new System.Drawing.Size(294, 29);
            this.dia.TabIndex = 3;
            //
            // lblSeries
            //
            this.lblSeries.AutoSize = true;
            this.lblSeries.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSeries.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblSeries.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblSeries.Location = new System.Drawing.Point(10, 66);
            this.lblSeries.Margin = new System.Windows.Forms.Padding(0, 2, 8, 2);
            this.lblSeries.Name = "lblSeries";
            this.lblSeries.Size = new System.Drawing.Size(70, 29);
            this.lblSeries.TabIndex = 4;
            this.lblSeries.Text = "Series:";
            this.lblSeries.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // series
            //
            this.series.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.series.Dock = System.Windows.Forms.DockStyle.Fill;
            this.series.Location = new System.Drawing.Point(88, 66);
            this.series.Margin = new System.Windows.Forms.Padding(0, 2, 8, 2);
            this.series.Name = "series";
            this.series.Size = new System.Drawing.Size(193, 29);
            this.series.TabIndex = 3;
            this.series.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.series_KeyPress);
            //
            // lblRepeticiones
            //
            this.lblRepeticiones.AutoSize = true;
            this.lblRepeticiones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRepeticiones.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblRepeticiones.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblRepeticiones.Location = new System.Drawing.Point(289, 66);
            this.lblRepeticiones.Margin = new System.Windows.Forms.Padding(0, 2, 8, 2);
            this.lblRepeticiones.Name = "lblRepeticiones";
            this.lblRepeticiones.Size = new System.Drawing.Size(99, 29);
            this.lblRepeticiones.TabIndex = 5;
            this.lblRepeticiones.Text = "Repeticiones:";
            this.lblRepeticiones.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // repeticiones
            //
            this.repeticiones.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.repeticiones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.repeticiones.Location = new System.Drawing.Point(396, 66);
            this.repeticiones.Margin = new System.Windows.Forms.Padding(0, 2, 8, 2);
            this.repeticiones.Name = "repeticiones";
            this.repeticiones.Size = new System.Drawing.Size(294, 29);
            this.repeticiones.TabIndex = 5;
            this.repeticiones.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.repeticiones_KeyPress);
            //
            // lblPeso
            //
            this.lblPeso.AutoSize = true;
            this.lblPeso.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPeso.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblPeso.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblPeso.Location = new System.Drawing.Point(10, 99);
            this.lblPeso.Margin = new System.Windows.Forms.Padding(0, 2, 8, 2);
            this.lblPeso.Name = "lblPeso";
            this.lblPeso.Size = new System.Drawing.Size(70, 29);
            this.lblPeso.TabIndex = 6;
            this.lblPeso.Text = "Peso:";
            this.lblPeso.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // peso
            //
            this.peso.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.peso.Dock = System.Windows.Forms.DockStyle.Fill;
            this.peso.Location = new System.Drawing.Point(88, 99);
            this.peso.Margin = new System.Windows.Forms.Padding(0, 2, 8, 2);
            this.peso.Name = "peso";
            this.peso.Size = new System.Drawing.Size(193, 29);
            this.peso.TabIndex = 5;
            this.peso.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.peso_KeyPress);
            //
            // lblDescanso
            //
            this.lblDescanso.AutoSize = true;
            this.lblDescanso.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDescanso.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblDescanso.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblDescanso.Location = new System.Drawing.Point(289, 99);
            this.lblDescanso.Margin = new System.Windows.Forms.Padding(0, 2, 8, 2);
            this.lblDescanso.Name = "lblDescanso";
            this.lblDescanso.Size = new System.Drawing.Size(99, 29);
            this.lblDescanso.TabIndex = 7;
            this.lblDescanso.Text = "Descanso:";
            this.lblDescanso.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // descanso
            //
            this.descanso.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.descanso.Dock = System.Windows.Forms.DockStyle.Fill;
            this.descanso.Location = new System.Drawing.Point(396, 99);
            this.descanso.Margin = new System.Windows.Forms.Padding(0, 2, 8, 2);
            this.descanso.Name = "descanso";
            this.descanso.Size = new System.Drawing.Size(294, 29);
            this.descanso.TabIndex = 7;
            this.descanso.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.descanso_KeyPress);
            //
            // lblOrden
            //
            this.lblOrden.AutoSize = true;
            this.lblOrden.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOrden.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblOrden.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblOrden.Location = new System.Drawing.Point(10, 132);
            this.lblOrden.Margin = new System.Windows.Forms.Padding(0, 2, 8, 2);
            this.lblOrden.Name = "lblOrden";
            this.lblOrden.Size = new System.Drawing.Size(70, 29);
            this.lblOrden.TabIndex = 8;
            this.lblOrden.Text = "Orden:";
            this.lblOrden.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // orden
            //
            this.orden.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.orden.Dock = System.Windows.Forms.DockStyle.Fill;
            this.orden.Location = new System.Drawing.Point(88, 132);
            this.orden.Margin = new System.Windows.Forms.Padding(0, 2, 8, 2);
            this.orden.Name = "orden";
            this.orden.Size = new System.Drawing.Size(193, 29);
            this.orden.TabIndex = 7;
            this.orden.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.orden_KeyPress);
            //
            // panelEjercicios
            //
            this.panelEjercicios.BackColor = System.Drawing.Color.White;
            this.panelEjercicios.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelEjercicios.ColumnCount = 1;
            this.panelEjercicios.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelEjercicios.Controls.Add(this.lblEjerciciosTitulo, 0, 0);
            this.panelEjercicios.Controls.Add(this.accionesEjercicios, 0, 1);
            this.panelEjercicios.Controls.Add(this.tablaEjercicios, 0, 2);
            this.panelEjercicios.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEjercicios.Location = new System.Drawing.Point(3, 162);
            this.panelEjercicios.Name = "panelEjercicios";
            this.panelEjercicios.Padding = new System.Windows.Forms.Padding(10);
            this.panelEjercicios.RowCount = 3;
            this.panelEjercicios.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.panelEjercicios.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.panelEjercicios.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelEjercicios.Size = new System.Drawing.Size(710, 159);
            this.panelEjercicios.TabIndex = 1;
            //
            // lblEjerciciosTitulo
            //
            this.lblEjerciciosTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblEjerciciosTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblEjerciciosTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblEjerciciosTitulo.Location = new System.Drawing.Point(13, 10);
            this.lblEjerciciosTitulo.Name = "lblEjerciciosTitulo";
            this.lblEjerciciosTitulo.Size = new System.Drawing.Size(682, 28);
            this.lblEjerciciosTitulo.TabIndex = 4;
            this.lblEjerciciosTitulo.Text = "Ejercicios de la rutina";
            this.lblEjerciciosTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // accionesEjercicios
            //
            this.accionesEjercicios.AutoSize = true;
            this.accionesEjercicios.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.accionesEjercicios.Controls.Add(this.agregarEjercicio);
            this.accionesEjercicios.Controls.Add(this.actualizarEjercicio);
            this.accionesEjercicios.Controls.Add(this.quitarEjercicio);
            this.accionesEjercicios.Dock = System.Windows.Forms.DockStyle.Fill;
            this.accionesEjercicios.Location = new System.Drawing.Point(13, 41);
            this.accionesEjercicios.Name = "accionesEjercicios";
            this.accionesEjercicios.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.accionesEjercicios.Size = new System.Drawing.Size(682, 32);
            this.accionesEjercicios.TabIndex = 3;
            this.accionesEjercicios.WrapContents = false;
            //
            // agregarEjercicio
            //
            this.agregarEjercicio.AutoSize = true;
            this.agregarEjercicio.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(116)))), ((int)(((byte)(144)))));
            this.agregarEjercicio.FlatAppearance.BorderSize = 0;
            this.agregarEjercicio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.agregarEjercicio.ForeColor = System.Drawing.Color.White;
            this.agregarEjercicio.Location = new System.Drawing.Point(0, 2);
            this.agregarEjercicio.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.agregarEjercicio.Name = "agregarEjercicio";
            this.agregarEjercicio.Size = new System.Drawing.Size(152, 32);
            this.agregarEjercicio.TabIndex = 3;
            this.agregarEjercicio.Text = "+ Agregar ejercicio";
            this.agregarEjercicio.UseVisualStyleBackColor = false;
            this.agregarEjercicio.Click += new System.EventHandler(this.agregarEjercicio_Click);
            //
            // actualizarEjercicio
            //
            this.actualizarEjercicio.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.actualizarEjercicio.FlatAppearance.BorderSize = 0;
            this.actualizarEjercicio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.actualizarEjercicio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.actualizarEjercicio.Location = new System.Drawing.Point(158, 2);
            this.actualizarEjercicio.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.actualizarEjercicio.Name = "actualizarEjercicio";
            this.actualizarEjercicio.Size = new System.Drawing.Size(92, 32);
            this.actualizarEjercicio.TabIndex = 4;
            this.actualizarEjercicio.Text = "Editar";
            this.actualizarEjercicio.UseVisualStyleBackColor = false;
            this.actualizarEjercicio.Click += new System.EventHandler(this.actualizarEjercicio_Click);
            //
            // quitarEjercicio
            //
            this.quitarEjercicio.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.quitarEjercicio.FlatAppearance.BorderSize = 0;
            this.quitarEjercicio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.quitarEjercicio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.quitarEjercicio.Location = new System.Drawing.Point(256, 2);
            this.quitarEjercicio.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.quitarEjercicio.Name = "quitarEjercicio";
            this.quitarEjercicio.Size = new System.Drawing.Size(92, 32);
            this.quitarEjercicio.TabIndex = 7;
            this.quitarEjercicio.Text = "Quitar";
            this.quitarEjercicio.UseVisualStyleBackColor = false;
            this.quitarEjercicio.Click += new System.EventHandler(this.quitarEjercicio_Click);
            //
            // tablaEjercicios
            //
            this.tablaEjercicios.AllowUserToAddRows = false;
            this.tablaEjercicios.AllowUserToDeleteRows = false;
            this.tablaEjercicios.AllowUserToResizeRows = false;
            this.tablaEjercicios.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tablaEjercicios.BackgroundColor = System.Drawing.Color.White;
            this.tablaEjercicios.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tablaEjercicios.ColumnHeadersHeight = 29;
            this.tablaEjercicios.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDetalleId,
            this.colDetalleDia,
            this.colDetalleOrden,
            this.colDetalleEjercicio,
            this.colDetalleSeries,
            this.colDetalleRepeticiones,
            this.colDetallePeso,
            this.colDetalleDescanso});
            this.tablaEjercicios.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablaEjercicios.Location = new System.Drawing.Point(13, 79);
            this.tablaEjercicios.MultiSelect = false;
            this.tablaEjercicios.Name = "tablaEjercicios";
            this.tablaEjercicios.ReadOnly = true;
            this.tablaEjercicios.RowHeadersVisible = false;
            this.tablaEjercicios.RowHeadersWidth = 51;
            this.tablaEjercicios.RowTemplate.Height = 28;
            this.tablaEjercicios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tablaEjercicios.Size = new System.Drawing.Size(682, 65);
            this.tablaEjercicios.TabIndex = 2;
            this.tablaEjercicios.SelectionChanged += new System.EventHandler(this.tablaEjercicios_SelectionChanged);
            //
            // colDetalleId
            //
            this.colDetalleId.MinimumWidth = 6;
            this.colDetalleId.Name = "colDetalleId";
            this.colDetalleId.ReadOnly = true;
            this.colDetalleId.Visible = false;
            //
            // colDetalleDia
            //
            this.colDetalleDia.FillWeight = 10F;
            this.colDetalleDia.HeaderText = "Día";
            this.colDetalleDia.MinimumWidth = 20;
            this.colDetalleDia.Name = "colDetalleDia";
            this.colDetalleDia.ReadOnly = true;
            //
            // colDetalleOrden
            //
            this.colDetalleOrden.FillWeight = 7F;
            this.colDetalleOrden.HeaderText = "Orden";
            this.colDetalleOrden.MinimumWidth = 20;
            this.colDetalleOrden.Name = "colDetalleOrden";
            this.colDetalleOrden.ReadOnly = true;
            //
            // colDetalleEjercicio
            //
            this.colDetalleEjercicio.FillWeight = 32F;
            this.colDetalleEjercicio.HeaderText = "Ejercicio";
            this.colDetalleEjercicio.MinimumWidth = 20;
            this.colDetalleEjercicio.Name = "colDetalleEjercicio";
            this.colDetalleEjercicio.ReadOnly = true;
            //
            // colDetalleSeries
            //
            this.colDetalleSeries.FillWeight = 8F;
            this.colDetalleSeries.HeaderText = "Series";
            this.colDetalleSeries.MinimumWidth = 20;
            this.colDetalleSeries.Name = "colDetalleSeries";
            this.colDetalleSeries.ReadOnly = true;
            //
            // colDetalleRepeticiones
            //
            this.colDetalleRepeticiones.FillWeight = 17F;
            this.colDetalleRepeticiones.HeaderText = "Repeticiones";
            this.colDetalleRepeticiones.MinimumWidth = 20;
            this.colDetalleRepeticiones.Name = "colDetalleRepeticiones";
            this.colDetalleRepeticiones.ReadOnly = true;
            //
            // colDetallePeso
            //
            this.colDetallePeso.FillWeight = 8F;
            this.colDetallePeso.HeaderText = "Peso";
            this.colDetallePeso.MinimumWidth = 20;
            this.colDetallePeso.Name = "colDetallePeso";
            this.colDetallePeso.ReadOnly = true;
            //
            // colDetalleDescanso
            //
            this.colDetalleDescanso.FillWeight = 18F;
            this.colDetalleDescanso.HeaderText = "Descanso";
            this.colDetalleDescanso.MinimumWidth = 20;
            this.colDetalleDescanso.Name = "colDetalleDescanso";
            this.colDetalleDescanso.ReadOnly = true;
            //
            // RutinasEntrenadorFormulario
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1100, 680);
            this.Controls.Add(this.panelContenido);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.barraAcciones);
            this.Controls.Add(this.panelEncabezado);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.MinimumSize = new System.Drawing.Size(900, 560);
            this.Name = "RutinasEntrenadorFormulario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SysGym | Gestionar rutinas";
            this.Load += new System.EventHandler(this.RutinasEntrenadorFormulario_Load);
            this.panelEncabezado.ResumeLayout(false);
            this.panelContenido.ResumeLayout(false);
            this.panelListado.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).EndInit();
            this.layoutDetalle.ResumeLayout(false);
            this.layoutDetalle.PerformLayout();
            this.contenedorRutina.ResumeLayout(false);
            this.contenedorRutina.PerformLayout();
            this.accionesRutina.ResumeLayout(false);
            this.contenedorFormulario.ResumeLayout(false);
            this.contenedorFormulario.PerformLayout();
            this.accionesFormulario.ResumeLayout(false);
            this.panelEjercicios.ResumeLayout(false);
            this.panelEjercicios.PerformLayout();
            this.accionesEjercicios.ResumeLayout(false);
            this.accionesEjercicios.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablaEjercicios)).EndInit();
            this.ResumeLayout(false);

        }

    }
}
