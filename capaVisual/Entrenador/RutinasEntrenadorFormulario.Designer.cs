using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Entrenador
{
    partial class RutinasEntrenadorFormulario
    {
        private IContainer components = null;
        private ErrorProvider indicadorErrores;
        private Label lblTitulo;
        private Label lblDescripcion;
        private Button btnVolver;
        private Label lblEstado;
        private TableLayoutPanel panelContenido;
        private Panel panelDetalle;
        private Label lblListadoTitulo;
        private Label lblDetalleTitulo;
        private Label lblEjerciciosTitulo;
        private Label lblDetalleEjercicio;
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
            this.components = new System.ComponentModel.Container();
            this.indicadorErrores = new System.Windows.Forms.ErrorProvider(this.components);
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.btnVolver = new System.Windows.Forms.Button();
            this.lblEstado = new System.Windows.Forms.Label();
            this.panelContenido = new System.Windows.Forms.TableLayoutPanel();
            this.lblListadoTitulo = new System.Windows.Forms.Label();
            this.tabla = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRutina = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCreador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nuevaRutina = new System.Windows.Forms.Button();
            this.panelDetalle = new System.Windows.Forms.Panel();
            this.lblDetalleTitulo = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.nombre = new System.Windows.Forms.TextBox();
            this.lblDescripcionRutina = new System.Windows.Forms.Label();
            this.descripcion = new System.Windows.Forms.TextBox();
            this.guardarRutina = new System.Windows.Forms.Button();
            this.actualizar = new System.Windows.Forms.Button();
            this.darDeBaja = new System.Windows.Forms.Button();
            this.reactivar = new System.Windows.Forms.Button();
            this.lblEjerciciosTitulo = new System.Windows.Forms.Label();
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
            this.lblDetalleEjercicio = new System.Windows.Forms.Label();
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
            this.guardarEjercicio = new System.Windows.Forms.Button();
            this.cancelarEjercicio = new System.Windows.Forms.Button();
            this.panelContenido.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).BeginInit();
            this.panelDetalle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablaEjercicios)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.indicadorErrores)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(0, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(100, 24);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Gestionar rutinas | Catálogo y composición de rutinas";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTitulo.Visible = false;
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.Location = new System.Drawing.Point(0, 0);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(100, 24);
            this.lblDescripcion.TabIndex = 1;
            this.lblDescripcion.Visible = false;
            // 
            // btnVolver
            // 
            this.btnVolver.BackColor = System.Drawing.Color.White;
            this.btnVolver.FlatAppearance.BorderSize = 0;
            this.btnVolver.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVolver.ForeColor = System.Drawing.Color.FromArgb(72, 66, 217);
            this.btnVolver.Location = new System.Drawing.Point(0, 0);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(100, 24);
            this.btnVolver.TabIndex = 2;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = false;
            this.btnVolver.Visible = false;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // lblEstado
            // 
            this.lblEstado.BackColor = System.Drawing.Color.Transparent;
            this.lblEstado.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblEstado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblEstado.Location = new System.Drawing.Point(0, 651);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Padding = new System.Windows.Forms.Padding(18, 0, 12, 0);
            this.lblEstado.Size = new System.Drawing.Size(1100, 29);
            this.lblEstado.TabIndex = 1;
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelContenido
            // 
            this.panelContenido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelContenido.ColumnCount = 2;
            this.panelContenido.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.panelContenido.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.panelContenido.Controls.Add(this.lblListadoTitulo, 0, 0);
            this.panelContenido.Controls.Add(this.tabla, 0, 1);
            this.panelContenido.Controls.Add(this.nuevaRutina, 0, 2);
            this.panelContenido.Controls.Add(this.panelDetalle, 1, 0);
            this.panelContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenido.Location = new System.Drawing.Point(0, 0);
            this.panelContenido.Name = "panelContenido";
            this.panelContenido.Padding = new System.Windows.Forms.Padding(16);
            this.panelContenido.RowCount = 3;
            this.panelContenido.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.panelContenido.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelContenido.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.panelContenido.Size = new System.Drawing.Size(1100, 651);
            this.panelContenido.TabIndex = 0;
            // 
            // lblListadoTitulo
            // 
            this.lblListadoTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblListadoTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblListadoTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblListadoTitulo.Location = new System.Drawing.Point(19, 16);
            this.lblListadoTitulo.Name = "lblListadoTitulo";
            this.lblListadoTitulo.Size = new System.Drawing.Size(314, 30);
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
            this.tabla.Location = new System.Drawing.Point(19, 51);
            this.tabla.MultiSelect = false;
            this.tabla.Name = "tabla";
            this.tabla.ReadOnly = true;
            this.tabla.RowHeadersVisible = false;
            this.tabla.RowHeadersWidth = 51;
            this.tabla.RowTemplate.Height = 28;
            this.tabla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tabla.Size = new System.Drawing.Size(314, 537);
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
            this.nuevaRutina.BackColor = System.Drawing.Color.FromArgb(9, 149, 111);
            this.nuevaRutina.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.nuevaRutina.FlatAppearance.BorderSize = 0;
            this.nuevaRutina.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nuevaRutina.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.nuevaRutina.Location = new System.Drawing.Point(16, 599);
            this.nuevaRutina.Margin = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.nuevaRutina.Name = "nuevaRutina";
            this.nuevaRutina.Size = new System.Drawing.Size(320, 36);
            this.nuevaRutina.TabIndex = 0;
            this.nuevaRutina.Text = "+ Nueva rutina";
            this.nuevaRutina.UseVisualStyleBackColor = false;
            this.nuevaRutina.Click += new System.EventHandler(this.nuevaRutina_Click);
            // 
            // panelDetalle
            // 
            this.panelDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDetalle.AutoScroll = true;
            this.panelDetalle.AutoScrollMinSize = new System.Drawing.Size(0, 604);
            this.panelDetalle.Controls.Add(this.lblDetalleTitulo);
            this.panelDetalle.Controls.Add(this.lblNombre);
            this.panelDetalle.Controls.Add(this.nombre);
            this.panelDetalle.Controls.Add(this.lblDescripcionRutina);
            this.panelDetalle.Controls.Add(this.descripcion);
            this.panelDetalle.Controls.Add(this.guardarRutina);
            this.panelDetalle.Controls.Add(this.actualizar);
            this.panelDetalle.Controls.Add(this.darDeBaja);
            this.panelDetalle.Controls.Add(this.reactivar);
            this.panelDetalle.Controls.Add(this.lblEjerciciosTitulo);
            this.panelDetalle.Controls.Add(this.agregarEjercicio);
            this.panelDetalle.Controls.Add(this.actualizarEjercicio);
            this.panelDetalle.Controls.Add(this.quitarEjercicio);
            this.panelDetalle.Controls.Add(this.tablaEjercicios);
            this.panelDetalle.Controls.Add(this.lblDetalleEjercicio);
            this.panelDetalle.Controls.Add(this.lblEjercicio);
            this.panelDetalle.Controls.Add(this.ejercicio);
            this.panelDetalle.Controls.Add(this.lblDia);
            this.panelDetalle.Controls.Add(this.dia);
            this.panelDetalle.Controls.Add(this.lblSeries);
            this.panelDetalle.Controls.Add(this.series);
            this.panelDetalle.Controls.Add(this.lblRepeticiones);
            this.panelDetalle.Controls.Add(this.repeticiones);
            this.panelDetalle.Controls.Add(this.lblPeso);
            this.panelDetalle.Controls.Add(this.peso);
            this.panelDetalle.Controls.Add(this.lblDescanso);
            this.panelDetalle.Controls.Add(this.descanso);
            this.panelDetalle.Controls.Add(this.lblOrden);
            this.panelDetalle.Controls.Add(this.orden);
            this.panelDetalle.Controls.Add(this.guardarEjercicio);
            this.panelDetalle.Controls.Add(this.cancelarEjercicio);
            this.panelDetalle.Location = new System.Drawing.Point(344, 24);
            this.panelDetalle.Margin = new System.Windows.Forms.Padding(8);
            this.panelDetalle.Name = "panelDetalle";
            this.panelContenido.SetRowSpan(this.panelDetalle, 3);
            this.panelDetalle.Size = new System.Drawing.Size(732, 603);
            this.panelDetalle.TabIndex = 1;
            this.panelDetalle.SizeChanged += new System.EventHandler(this.panelDetalle_SizeChanged);
            // 
            // lblDetalleTitulo
            // 
            this.lblDetalleTitulo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDetalleTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblDetalleTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblDetalleTitulo.Location = new System.Drawing.Point(10, 0);
            this.lblDetalleTitulo.Name = "lblDetalleTitulo";
            this.lblDetalleTitulo.Size = new System.Drawing.Size(673, 28);
            this.lblDetalleTitulo.TabIndex = 0;
            this.lblDetalleTitulo.Text = "Rutina seleccionada";
            this.lblDetalleTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNombre
            // 
            this.lblNombre.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblNombre.Location = new System.Drawing.Point(10, 34);
            this.lblNombre.Margin = new System.Windows.Forms.Padding(0, 2, 8, 2);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(100, 24);
            this.lblNombre.TabIndex = 1;
            this.lblNombre.Text = "Nombre:";
            this.lblNombre.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nombre
            // 
            this.nombre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nombre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nombre.Location = new System.Drawing.Point(116, 34);
            this.nombre.Margin = new System.Windows.Forms.Padding(0, 2, 8, 2);
            this.nombre.Name = "nombre";
            this.nombre.Size = new System.Drawing.Size(567, 24);
            this.nombre.TabIndex = 2;
            // 
            // lblDescripcionRutina
            // 
            this.lblDescripcionRutina.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblDescripcionRutina.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblDescripcionRutina.Location = new System.Drawing.Point(10, 66);
            this.lblDescripcionRutina.Margin = new System.Windows.Forms.Padding(0, 2, 8, 2);
            this.lblDescripcionRutina.Name = "lblDescripcionRutina";
            this.lblDescripcionRutina.Size = new System.Drawing.Size(100, 24);
            this.lblDescripcionRutina.TabIndex = 3;
            this.lblDescripcionRutina.Text = "Descripción:";
            this.lblDescripcionRutina.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // descripcion
            // 
            this.descripcion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descripcion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.descripcion.Location = new System.Drawing.Point(116, 66);
            this.descripcion.Margin = new System.Windows.Forms.Padding(0, 2, 8, 2);
            this.descripcion.Name = "descripcion";
            this.descripcion.Size = new System.Drawing.Size(567, 24);
            this.descripcion.TabIndex = 4;
            // 
            // guardarRutina
            // 
            this.guardarRutina.BackColor = System.Drawing.Color.FromArgb(72, 66, 217);
            this.guardarRutina.FlatAppearance.BorderSize = 0;
            this.guardarRutina.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.guardarRutina.ForeColor = System.Drawing.Color.White;
            this.guardarRutina.Location = new System.Drawing.Point(10, 102);
            this.guardarRutina.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.guardarRutina.Name = "guardarRutina";
            this.guardarRutina.Size = new System.Drawing.Size(146, 32);
            this.guardarRutina.TabIndex = 5;
            this.guardarRutina.Text = "Guardar";
            this.guardarRutina.UseVisualStyleBackColor = false;
            this.guardarRutina.Click += new System.EventHandler(this.guardarRutina_Click);
            // 
            // actualizar
            // 
            this.actualizar.BackColor = System.Drawing.Color.FromArgb(72, 66, 217);
            this.actualizar.FlatAppearance.BorderSize = 0;
            this.actualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.actualizar.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.actualizar.Location = new System.Drawing.Point(162, 102);
            this.actualizar.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.actualizar.Name = "actualizar";
            this.actualizar.Size = new System.Drawing.Size(108, 32);
            this.actualizar.TabIndex = 6;
            this.actualizar.Text = "Actualizar";
            this.actualizar.UseVisualStyleBackColor = false;
            this.actualizar.Click += new System.EventHandler(this.actualizar_Click);
            // 
            // darDeBaja
            // 
            this.darDeBaja.BackColor = System.Drawing.Color.FromArgb(255, 240, 240);
            this.darDeBaja.FlatAppearance.BorderSize = 0;
            this.darDeBaja.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.darDeBaja.ForeColor = System.Drawing.Color.FromArgb(173, 36, 36);
            this.darDeBaja.Location = new System.Drawing.Point(276, 102);
            this.darDeBaja.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.darDeBaja.Name = "darDeBaja";
            this.darDeBaja.Size = new System.Drawing.Size(108, 32);
            this.darDeBaja.TabIndex = 7;
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
            this.reactivar.Location = new System.Drawing.Point(390, 102);
            this.reactivar.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.reactivar.Name = "reactivar";
            this.reactivar.Size = new System.Drawing.Size(108, 32);
            this.reactivar.TabIndex = 8;
            this.reactivar.Text = "Reactivar";
            this.reactivar.UseVisualStyleBackColor = false;
            this.reactivar.Visible = false;
            this.reactivar.Click += new System.EventHandler(this.reactivar_Click);
            // 
            // lblEjerciciosTitulo
            // 
            this.lblEjerciciosTitulo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEjerciciosTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblEjerciciosTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblEjerciciosTitulo.Location = new System.Drawing.Point(10, 144);
            this.lblEjerciciosTitulo.Name = "lblEjerciciosTitulo";
            this.lblEjerciciosTitulo.Size = new System.Drawing.Size(673, 28);
            this.lblEjerciciosTitulo.TabIndex = 9;
            this.lblEjerciciosTitulo.Text = "Ejercicios de la rutina";
            this.lblEjerciciosTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // agregarEjercicio
            // 
            this.agregarEjercicio.BackColor = System.Drawing.Color.FromArgb(9, 149, 111);
            this.agregarEjercicio.FlatAppearance.BorderSize = 0;
            this.agregarEjercicio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.agregarEjercicio.ForeColor = System.Drawing.Color.White;
            this.agregarEjercicio.Location = new System.Drawing.Point(10, 178);
            this.agregarEjercicio.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.agregarEjercicio.Name = "agregarEjercicio";
            this.agregarEjercicio.Size = new System.Drawing.Size(152, 32);
            this.agregarEjercicio.TabIndex = 10;
            this.agregarEjercicio.Text = "+ Agregar ejercicio";
            this.agregarEjercicio.UseVisualStyleBackColor = false;
            this.agregarEjercicio.Click += new System.EventHandler(this.agregarEjercicio_Click);
            // 
            // actualizarEjercicio
            // 
            this.actualizarEjercicio.BackColor = System.Drawing.Color.FromArgb(231, 237, 247);
            this.actualizarEjercicio.FlatAppearance.BorderSize = 0;
            this.actualizarEjercicio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.actualizarEjercicio.ForeColor = System.Drawing.Color.FromArgb(48, 68, 95);
            this.actualizarEjercicio.Location = new System.Drawing.Point(168, 178);
            this.actualizarEjercicio.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.actualizarEjercicio.Name = "actualizarEjercicio";
            this.actualizarEjercicio.Size = new System.Drawing.Size(92, 32);
            this.actualizarEjercicio.TabIndex = 11;
            this.actualizarEjercicio.Text = "Editar";
            this.actualizarEjercicio.UseVisualStyleBackColor = false;
            this.actualizarEjercicio.Click += new System.EventHandler(this.actualizarEjercicio_Click);
            // 
            // quitarEjercicio
            // 
            this.quitarEjercicio.BackColor = System.Drawing.Color.FromArgb(255, 240, 240);
            this.quitarEjercicio.FlatAppearance.BorderSize = 0;
            this.quitarEjercicio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.quitarEjercicio.ForeColor = System.Drawing.Color.FromArgb(173, 36, 36);
            this.quitarEjercicio.Location = new System.Drawing.Point(266, 178);
            this.quitarEjercicio.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.quitarEjercicio.Name = "quitarEjercicio";
            this.quitarEjercicio.Size = new System.Drawing.Size(92, 32);
            this.quitarEjercicio.TabIndex = 12;
            this.quitarEjercicio.Text = "Quitar";
            this.quitarEjercicio.UseVisualStyleBackColor = false;
            this.quitarEjercicio.Click += new System.EventHandler(this.quitarEjercicio_Click);
            // 
            // tablaEjercicios
            // 
            this.tablaEjercicios.AllowUserToAddRows = false;
            this.tablaEjercicios.AllowUserToDeleteRows = false;
            this.tablaEjercicios.AllowUserToResizeRows = false;
            this.tablaEjercicios.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.tablaEjercicios.Location = new System.Drawing.Point(10, 218);
            this.tablaEjercicios.MultiSelect = false;
            this.tablaEjercicios.Name = "tablaEjercicios";
            this.tablaEjercicios.ReadOnly = true;
            this.tablaEjercicios.RowHeadersVisible = false;
            this.tablaEjercicios.RowHeadersWidth = 51;
            this.tablaEjercicios.RowTemplate.Height = 28;
            this.tablaEjercicios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tablaEjercicios.Size = new System.Drawing.Size(673, 155);
            this.tablaEjercicios.TabIndex = 13;
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
            // lblDetalleEjercicio
            // 
            this.lblDetalleEjercicio.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDetalleEjercicio.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblDetalleEjercicio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblDetalleEjercicio.Location = new System.Drawing.Point(10, 378);
            this.lblDetalleEjercicio.Name = "lblDetalleEjercicio";
            this.lblDetalleEjercicio.Size = new System.Drawing.Size(673, 28);
            this.lblDetalleEjercicio.TabIndex = 14;
            this.lblDetalleEjercicio.Text = "Detalle del ejercicio";
            this.lblDetalleEjercicio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEjercicio
            // 
            this.lblEjercicio.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblEjercicio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblEjercicio.Location = new System.Drawing.Point(10, 414);
            this.lblEjercicio.Margin = new System.Windows.Forms.Padding(0, 2, 8, 2);
            this.lblEjercicio.Name = "lblEjercicio";
            this.lblEjercicio.Size = new System.Drawing.Size(90, 24);
            this.lblEjercicio.TabIndex = 15;
            this.lblEjercicio.Text = "Ejercicio:";
            this.lblEjercicio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ejercicio
            // 
            this.ejercicio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ejercicio.Location = new System.Drawing.Point(106, 414);
            this.ejercicio.Margin = new System.Windows.Forms.Padding(0, 2, 8, 2);
            this.ejercicio.Name = "ejercicio";
            this.ejercicio.Size = new System.Drawing.Size(150, 25);
            this.ejercicio.TabIndex = 16;
            // 
            // lblDia
            // 
            this.lblDia.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblDia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblDia.Location = new System.Drawing.Point(268, 414);
            this.lblDia.Margin = new System.Windows.Forms.Padding(0, 2, 8, 2);
            this.lblDia.Name = "lblDia";
            this.lblDia.Size = new System.Drawing.Size(110, 24);
            this.lblDia.TabIndex = 17;
            this.lblDia.Text = "Día:";
            this.lblDia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dia
            // 
            this.dia.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dia.Items.AddRange(new object[] {
            "Lunes",
            "Martes",
            "Miercoles",
            "Jueves",
            "Viernes"});
            this.dia.Location = new System.Drawing.Point(384, 414);
            this.dia.Margin = new System.Windows.Forms.Padding(0, 2, 8, 2);
            this.dia.Name = "dia";
            this.dia.Size = new System.Drawing.Size(299, 25);
            this.dia.TabIndex = 18;
            // 
            // lblSeries
            // 
            this.lblSeries.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblSeries.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblSeries.Location = new System.Drawing.Point(10, 446);
            this.lblSeries.Margin = new System.Windows.Forms.Padding(0, 2, 8, 2);
            this.lblSeries.Name = "lblSeries";
            this.lblSeries.Size = new System.Drawing.Size(90, 24);
            this.lblSeries.TabIndex = 19;
            this.lblSeries.Text = "Series:";
            this.lblSeries.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // series
            // 
            this.series.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.series.Location = new System.Drawing.Point(106, 446);
            this.series.Margin = new System.Windows.Forms.Padding(0, 2, 8, 2);
            this.series.Name = "series";
            this.series.Size = new System.Drawing.Size(150, 24);
            this.series.TabIndex = 20;
            this.series.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.series_KeyPress);
            // 
            // lblRepeticiones
            // 
            this.lblRepeticiones.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblRepeticiones.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblRepeticiones.Location = new System.Drawing.Point(268, 446);
            this.lblRepeticiones.Margin = new System.Windows.Forms.Padding(0, 2, 8, 2);
            this.lblRepeticiones.Name = "lblRepeticiones";
            this.lblRepeticiones.Size = new System.Drawing.Size(110, 24);
            this.lblRepeticiones.TabIndex = 21;
            this.lblRepeticiones.Text = "Repeticiones:";
            this.lblRepeticiones.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // repeticiones
            // 
            this.repeticiones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.repeticiones.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.repeticiones.Location = new System.Drawing.Point(384, 446);
            this.repeticiones.Margin = new System.Windows.Forms.Padding(0, 2, 8, 2);
            this.repeticiones.Name = "repeticiones";
            this.repeticiones.Size = new System.Drawing.Size(299, 24);
            this.repeticiones.TabIndex = 22;
            this.repeticiones.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.repeticiones_KeyPress);
            // 
            // lblPeso
            // 
            this.lblPeso.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblPeso.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblPeso.Location = new System.Drawing.Point(10, 478);
            this.lblPeso.Margin = new System.Windows.Forms.Padding(0, 2, 8, 2);
            this.lblPeso.Name = "lblPeso";
            this.lblPeso.Size = new System.Drawing.Size(90, 24);
            this.lblPeso.TabIndex = 23;
            this.lblPeso.Text = "Peso:";
            this.lblPeso.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // peso
            // 
            this.peso.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.peso.Location = new System.Drawing.Point(106, 478);
            this.peso.Margin = new System.Windows.Forms.Padding(0, 2, 8, 2);
            this.peso.Name = "peso";
            this.peso.Size = new System.Drawing.Size(150, 24);
            this.peso.TabIndex = 24;
            this.peso.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.peso_KeyPress);
            // 
            // lblDescanso
            // 
            this.lblDescanso.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblDescanso.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblDescanso.Location = new System.Drawing.Point(268, 478);
            this.lblDescanso.Margin = new System.Windows.Forms.Padding(0, 2, 8, 2);
            this.lblDescanso.Name = "lblDescanso";
            this.lblDescanso.Size = new System.Drawing.Size(110, 24);
            this.lblDescanso.TabIndex = 25;
            this.lblDescanso.Text = "Descanso:";
            this.lblDescanso.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // descanso
            // 
            this.descanso.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descanso.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.descanso.Location = new System.Drawing.Point(384, 478);
            this.descanso.Margin = new System.Windows.Forms.Padding(0, 2, 8, 2);
            this.descanso.Name = "descanso";
            this.descanso.Size = new System.Drawing.Size(299, 24);
            this.descanso.TabIndex = 26;
            this.descanso.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.descanso_KeyPress);
            // 
            // lblOrden
            // 
            this.lblOrden.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblOrden.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblOrden.Location = new System.Drawing.Point(10, 510);
            this.lblOrden.Margin = new System.Windows.Forms.Padding(0, 2, 8, 2);
            this.lblOrden.Name = "lblOrden";
            this.lblOrden.Size = new System.Drawing.Size(90, 24);
            this.lblOrden.TabIndex = 27;
            this.lblOrden.Text = "Orden:";
            this.lblOrden.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // orden
            // 
            this.orden.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.orden.Location = new System.Drawing.Point(106, 510);
            this.orden.Margin = new System.Windows.Forms.Padding(0, 2, 8, 2);
            this.orden.Name = "orden";
            this.orden.Size = new System.Drawing.Size(150, 24);
            this.orden.TabIndex = 28;
            this.orden.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.orden_KeyPress);
            // 
            // guardarEjercicio
            // 
            this.guardarEjercicio.BackColor = System.Drawing.Color.FromArgb(72, 66, 217);
            this.guardarEjercicio.FlatAppearance.BorderSize = 0;
            this.guardarEjercicio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.guardarEjercicio.ForeColor = System.Drawing.Color.White;
            this.guardarEjercicio.Location = new System.Drawing.Point(10, 554);
            this.guardarEjercicio.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.guardarEjercicio.Name = "guardarEjercicio";
            this.guardarEjercicio.Size = new System.Drawing.Size(160, 32);
            this.guardarEjercicio.TabIndex = 29;
            this.guardarEjercicio.Text = "Agregar";
            this.guardarEjercicio.UseVisualStyleBackColor = false;
            this.guardarEjercicio.Visible = false;
            this.guardarEjercicio.Click += new System.EventHandler(this.guardarEjercicio_Click);
            // 
            // cancelarEjercicio
            // 
            this.cancelarEjercicio.BackColor = System.Drawing.Color.FromArgb(231, 237, 247);
            this.cancelarEjercicio.FlatAppearance.BorderSize = 0;
            this.cancelarEjercicio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelarEjercicio.ForeColor = System.Drawing.Color.FromArgb(48, 68, 95);
            this.cancelarEjercicio.Location = new System.Drawing.Point(176, 554);
            this.cancelarEjercicio.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.cancelarEjercicio.Name = "cancelarEjercicio";
            this.cancelarEjercicio.Size = new System.Drawing.Size(108, 32);
            this.cancelarEjercicio.TabIndex = 30;
            this.cancelarEjercicio.Text = "Cancelar";
            this.cancelarEjercicio.UseVisualStyleBackColor = false;
            this.cancelarEjercicio.Visible = false;
            this.cancelarEjercicio.Click += new System.EventHandler(this.cancelarEjercicio_Click);
            // 
                        this.indicadorErrores.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.indicadorErrores.ContainerControl = this;
// RutinasEntrenadorFormulario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1100, 680);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblDescripcion);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.panelContenido);
            this.Controls.Add(this.lblEstado);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.MinimumSize = new System.Drawing.Size(900, 560);
            this.Name = "RutinasEntrenadorFormulario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SysGym | Gestionar rutinas";
            this.Load += new System.EventHandler(this.RutinasEntrenadorFormulario_Load);
            this.panelContenido.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).EndInit();
            this.panelDetalle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tablaEjercicios)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.indicadorErrores)).EndInit();
            this.ResumeLayout(false);

        }

    }
}
