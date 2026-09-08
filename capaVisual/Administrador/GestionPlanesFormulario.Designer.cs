using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Administrador
{
    partial class GestionPlanesFormulario
    {
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
        private Panel panelFiltro;
        private Label lblFiltro;
        private Panel panelDetalle;
        private Panel contenedorDetalle;
        private Label lblFormulario;
        private Panel contenedorCampos;
        private Panel panelBeneficios;
        private Panel panelAcciones;
        private Label lblNombre;
        private Label lblDescripcionPlan;
        private Label lblPrecio;
        private Label lblRutina;
        private Label lblBeneficios;
        private TextBox nombre;
        private TextBox descripcion;
        private TextBox precio;
        private ComboBox rutina;
        private CheckBox incluyeEntrenador;
        private CheckBox incluyeRutina;
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
        private DataGridViewTextBoxColumn colPrecio;
        private DataGridViewTextBoxColumn colRutina;
        private DataGridViewTextBoxColumn colBeneficios;
        private DataGridViewTextBoxColumn colEstado;

        protected override void Dispose(bool liberarRecursos)
        {
            if (liberarRecursos && components != null) components.Dispose();
            base.Dispose(liberarRecursos);
        }

        private void InitializeComponent()
        {
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
            this.colPrecio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRutina = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBeneficios = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.lblDescripcionPlan = new System.Windows.Forms.Label();
            this.descripcion = new System.Windows.Forms.TextBox();
            this.lblPrecio = new System.Windows.Forms.Label();
            this.precio = new System.Windows.Forms.TextBox();
            this.lblRutina = new System.Windows.Forms.Label();
            this.rutina = new System.Windows.Forms.ComboBox();
            this.lblBeneficios = new System.Windows.Forms.Label();
            this.panelBeneficios = new System.Windows.Forms.Panel();
            this.incluyeRutina = new System.Windows.Forms.CheckBox();
            this.incluyeEntrenador = new System.Windows.Forms.CheckBox();
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
            this.panelBeneficios.SuspendLayout();
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
            this.lblDescripcion.Text = "Configuracion de los planes Basico y Premium";
            // 
            // lblTitulo
            // 

            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;

            this.lblTitulo.Name = "lblTitulo";

            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Planes";
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
            this.tabla.AllowUserToAddRows = false;
            this.tabla.AllowUserToDeleteRows = false;
            this.tabla.AllowUserToResizeRows = false;

            this.tabla.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tabla.BackgroundColor = System.Drawing.Color.White;
            this.tabla.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tabla.ColumnHeadersHeight = 38;
            this.tabla.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colNombre,
            this.colPrecio,
            this.colRutina,
            this.colBeneficios,
            this.colEstado});

            this.tabla.MultiSelect = false;
            this.tabla.Name = "tabla";
            this.tabla.ReadOnly = true;
            this.tabla.RowHeadersVisible = false;
            this.tabla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

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
            // colPrecio
            // 
            this.colPrecio.HeaderText = "Precio mensual";
            this.colPrecio.Name = "colPrecio";
            this.colPrecio.ReadOnly = true;
            // 
            // colRutina
            // 
            this.colRutina.HeaderText = "Rutina base";
            this.colRutina.Name = "colRutina";
            this.colRutina.ReadOnly = true;
            // 
            // colBeneficios
            // 
            this.colBeneficios.HeaderText = "Beneficios";
            this.colBeneficios.Name = "colBeneficios";
            this.colBeneficios.ReadOnly = true;
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
            this.lblFiltro.Text = "Estado";
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
            this.lblAyuda.Text = "Busca por nombre o descripcion";
            // 
            // lblListado
            // 

            this.lblListado.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblListado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));

            this.lblListado.Name = "lblListado";

            this.lblListado.TabIndex = 0;
            this.lblListado.Text = "Planes";
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
            this.lblFormulario.Text = "Nuevo plan";
            // 
            // contenedorCampos
            // 

            this.contenedorCampos.Controls.Add(this.lblNombre);
            this.contenedorCampos.Controls.Add(this.nombre);
            this.contenedorCampos.Controls.Add(this.lblDescripcionPlan);
            this.contenedorCampos.Controls.Add(this.descripcion);
            this.contenedorCampos.Controls.Add(this.lblPrecio);
            this.contenedorCampos.Controls.Add(this.precio);
            this.contenedorCampos.Controls.Add(this.lblRutina);
            this.contenedorCampos.Controls.Add(this.rutina);
            this.contenedorCampos.Controls.Add(this.lblBeneficios);
            this.contenedorCampos.Controls.Add(this.panelBeneficios);

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
            // lblDescripcionPlan
            // 

            this.lblDescripcionPlan.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblDescripcionPlan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));

            this.lblDescripcionPlan.Name = "lblDescripcionPlan";

            this.lblDescripcionPlan.TabIndex = 2;
            this.lblDescripcionPlan.Text = "Descripcion:";
            // 
            // descripcion
            // 
            this.descripcion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            this.descripcion.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.descripcion.Name = "descripcion";

            this.descripcion.TabIndex = 3;
            // 
            // lblPrecio
            // 

            this.lblPrecio.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblPrecio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));

            this.lblPrecio.Name = "lblPrecio";

            this.lblPrecio.TabIndex = 4;
            this.lblPrecio.Text = "Precio:";
            // 
            // precio
            // 
            this.precio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            this.precio.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.precio.Name = "precio";

            this.precio.TabIndex = 5;
            // 
            // lblRutina
            // 

            this.lblRutina.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblRutina.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));

            this.lblRutina.Name = "lblRutina";

            this.lblRutina.TabIndex = 6;
            this.lblRutina.Text = "Rutina base:";
            // 
            // rutina
            // 

            this.rutina.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.rutina.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.rutina.Name = "rutina";

            this.rutina.TabIndex = 7;
            // 
            // lblBeneficios
            // 

            this.lblBeneficios.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblBeneficios.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));

            this.lblBeneficios.Name = "lblBeneficios";

            this.lblBeneficios.TabIndex = 8;
            this.lblBeneficios.Text = "Beneficios:";
            // 
            // panelBeneficios
            // 
            this.panelBeneficios.Controls.Add(this.incluyeRutina);
            this.panelBeneficios.Controls.Add(this.incluyeEntrenador);

            this.panelBeneficios.Name = "panelBeneficios";

            this.panelBeneficios.TabIndex = 8;
            // 
            // incluyeRutina
            // 

            this.incluyeRutina.Name = "incluyeRutina";

            this.incluyeRutina.TabIndex = 1;
            this.incluyeRutina.Text = "Incluye rutina";
            // 
            // incluyeEntrenador
            // 

            this.incluyeEntrenador.Name = "incluyeEntrenador";

            this.incluyeEntrenador.TabIndex = 0;
            this.incluyeEntrenador.Text = "Incluye entrenador";
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
            // GestionPlanesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font; this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1100, 680);
            this.Controls.Add(this.panelContenido);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.barraAcciones);
            this.Controls.Add(this.panelEncabezado);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.MinimumSize = new System.Drawing.Size(760, 540);
            this.Name = "GestionPlanesFormulario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SysGym | Planes";

            this.panelEncabezado.AutoSize = false;
            this.panelEncabezado.Dock = System.Windows.Forms.DockStyle.None;
            this.panelEncabezado.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.panelEncabezado.Location = new System.Drawing.Point(0, 0);
            this.panelEncabezado.Size = new System.Drawing.Size(1100, 80);
            this.lblTitulo.AutoSize = false;
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.None;
            this.lblTitulo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblTitulo.Location = new System.Drawing.Point(22, 10);
            this.lblTitulo.Size = new System.Drawing.Size(83, 38);
            this.lblDescripcion.AutoSize = false;
            this.lblDescripcion.Dock = System.Windows.Forms.DockStyle.None;
            this.lblDescripcion.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblDescripcion.Location = new System.Drawing.Point(24, 47);
            this.lblDescripcion.Size = new System.Drawing.Size(269, 22);
            this.btnVolver.AutoSize = false;
            this.btnVolver.Dock = System.Windows.Forms.DockStyle.None;
            this.btnVolver.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.btnVolver.Location = new System.Drawing.Point(930, 22);
            this.btnVolver.Size = new System.Drawing.Size(92, 34);
            this.barraAcciones.AutoSize = false;
            this.barraAcciones.Dock = System.Windows.Forms.DockStyle.None;
            this.barraAcciones.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.barraAcciones.Location = new System.Drawing.Point(0, 80);
            this.barraAcciones.Size = new System.Drawing.Size(1100, 52);
            this.lblEstado.AutoSize = false;
            this.lblEstado.Dock = System.Windows.Forms.DockStyle.None;
            this.lblEstado.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblEstado.Location = new System.Drawing.Point(0, 648);
            this.lblEstado.Size = new System.Drawing.Size(1100, 32);
            this.panelContenido.AutoSize = false;
            this.panelContenido.Dock = System.Windows.Forms.DockStyle.None;
            this.panelContenido.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.panelContenido.Location = new System.Drawing.Point(0, 132);
            this.panelContenido.Size = new System.Drawing.Size(1100, 516);
            this.contenedorContenido.AutoSize = false;
            this.contenedorContenido.Dock = System.Windows.Forms.DockStyle.None;
            this.contenedorContenido.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.contenedorContenido.Location = new System.Drawing.Point(12, 12);
            this.contenedorContenido.Size = new System.Drawing.Size(1076, 492);
            this.panelListado.AutoSize = false;
            this.panelListado.Dock = System.Windows.Forms.DockStyle.None;
            this.panelListado.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.panelListado.Location = new System.Drawing.Point(3, 3);
            this.panelListado.Size = new System.Drawing.Size(585, 486);
            this.lblListado.AutoSize = false;
            this.lblListado.Dock = System.Windows.Forms.DockStyle.None;
            this.lblListado.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblListado.Location = new System.Drawing.Point(16, 14);
            this.lblListado.Size = new System.Drawing.Size(49, 25);
            this.lblAyuda.AutoSize = false;
            this.lblAyuda.Dock = System.Windows.Forms.DockStyle.None;
            this.lblAyuda.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblAyuda.Location = new System.Drawing.Point(16, 42);
            this.lblAyuda.Size = new System.Drawing.Size(191, 22);
            this.panelFiltro.AutoSize = false;
            this.panelFiltro.Dock = System.Windows.Forms.DockStyle.None;
            this.panelFiltro.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.panelFiltro.Location = new System.Drawing.Point(16, 68);
            this.panelFiltro.Size = new System.Drawing.Size(551, 54);
            this.lblFiltro.AutoSize = false;
            this.lblFiltro.Dock = System.Windows.Forms.DockStyle.None;
            this.lblFiltro.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblFiltro.Location = new System.Drawing.Point(310, 7);
            this.lblFiltro.Size = new System.Drawing.Size(42, 21);
            this.panelDetalle.AutoSize = false;
            this.panelDetalle.Dock = System.Windows.Forms.DockStyle.None;
            this.panelDetalle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.panelDetalle.Location = new System.Drawing.Point(594, 3);
            this.panelDetalle.Size = new System.Drawing.Size(479, 486);
            this.contenedorDetalle.AutoSize = false;
            this.contenedorDetalle.Dock = System.Windows.Forms.DockStyle.None;
            this.contenedorDetalle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.contenedorDetalle.Location = new System.Drawing.Point(16, 16);
            this.contenedorDetalle.Size = new System.Drawing.Size(445, 452);
            this.lblFormulario.AutoSize = false;
            this.lblFormulario.Dock = System.Windows.Forms.DockStyle.None;
            this.lblFormulario.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblFormulario.Location = new System.Drawing.Point(3, 0);
            this.lblFormulario.Size = new System.Drawing.Size(92, 27);
            this.contenedorCampos.AutoSize = false;
            this.contenedorCampos.Dock = System.Windows.Forms.DockStyle.None;
            this.contenedorCampos.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.contenedorCampos.Location = new System.Drawing.Point(3, 37);
            this.contenedorCampos.Size = new System.Drawing.Size(439, 216);
            this.panelBeneficios.AutoSize = false;
            this.panelBeneficios.Dock = System.Windows.Forms.DockStyle.None;
            this.panelBeneficios.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.panelBeneficios.Location = new System.Drawing.Point(178, 163);
            this.panelBeneficios.Size = new System.Drawing.Size(258, 50);
            this.panelAcciones.AutoSize = false;
            this.panelAcciones.Dock = System.Windows.Forms.DockStyle.None;
            this.panelAcciones.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.panelAcciones.Location = new System.Drawing.Point(3, 337);
            this.panelAcciones.Size = new System.Drawing.Size(439, 116);
            this.lblNombre.AutoSize = false;
            this.lblNombre.Dock = System.Windows.Forms.DockStyle.None;
            this.lblNombre.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblNombre.Location = new System.Drawing.Point(3, 9);
            this.lblNombre.Size = new System.Drawing.Size(54, 21);
            this.lblDescripcionPlan.AutoSize = false;
            this.lblDescripcionPlan.Dock = System.Windows.Forms.DockStyle.None;
            this.lblDescripcionPlan.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblDescripcionPlan.Location = new System.Drawing.Point(3, 49);
            this.lblDescripcionPlan.Size = new System.Drawing.Size(73, 21);
            this.lblPrecio.AutoSize = false;
            this.lblPrecio.Dock = System.Windows.Forms.DockStyle.None;
            this.lblPrecio.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblPrecio.Location = new System.Drawing.Point(3, 89);
            this.lblPrecio.Size = new System.Drawing.Size(42, 21);
            this.lblRutina.AutoSize = false;
            this.lblRutina.Dock = System.Windows.Forms.DockStyle.None;
            this.lblRutina.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblRutina.Location = new System.Drawing.Point(3, 129);
            this.lblRutina.Size = new System.Drawing.Size(73, 21);
            this.lblBeneficios.AutoSize = false;
            this.lblBeneficios.Dock = System.Windows.Forms.DockStyle.None;
            this.lblBeneficios.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblBeneficios.Location = new System.Drawing.Point(3, 169);
            this.lblBeneficios.Size = new System.Drawing.Size(65, 21);
            this.nombre.AutoSize = false;
            this.nombre.Dock = System.Windows.Forms.DockStyle.None;
            this.nombre.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.nombre.Location = new System.Drawing.Point(175, 4);
            this.nombre.Size = new System.Drawing.Size(264, 24);
            this.descripcion.AutoSize = false;
            this.descripcion.Dock = System.Windows.Forms.DockStyle.None;
            this.descripcion.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.descripcion.Location = new System.Drawing.Point(175, 44);
            this.descripcion.Size = new System.Drawing.Size(264, 24);
            this.precio.AutoSize = false;
            this.precio.Dock = System.Windows.Forms.DockStyle.None;
            this.precio.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.precio.Location = new System.Drawing.Point(175, 84);
            this.precio.Size = new System.Drawing.Size(264, 24);
            this.rutina.AutoSize = false;
            this.rutina.Dock = System.Windows.Forms.DockStyle.None;
            this.rutina.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.rutina.Location = new System.Drawing.Point(175, 124);
            this.rutina.Size = new System.Drawing.Size(264, 25);
            this.incluyeEntrenador.AutoSize = false;
            this.incluyeEntrenador.Dock = System.Windows.Forms.DockStyle.None;
            this.incluyeEntrenador.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.incluyeEntrenador.Location = new System.Drawing.Point(0, 4);
            this.incluyeEntrenador.Size = new System.Drawing.Size(132, 23);
            this.incluyeRutina.AutoSize = false;
            this.incluyeRutina.Dock = System.Windows.Forms.DockStyle.None;
            this.incluyeRutina.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.incluyeRutina.Location = new System.Drawing.Point(0, 23);
            this.incluyeRutina.Size = new System.Drawing.Size(101, 23);
            this.nuevo.AutoSize = false;
            this.nuevo.Dock = System.Windows.Forms.DockStyle.None;
            this.nuevo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.nuevo.Location = new System.Drawing.Point(0, 2);
            this.nuevo.Size = new System.Drawing.Size(100, 32);
            this.guardar.AutoSize = false;
            this.guardar.Dock = System.Windows.Forms.DockStyle.None;
            this.guardar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.guardar.Location = new System.Drawing.Point(0, 42);
            this.guardar.Size = new System.Drawing.Size(100, 32);
            this.actualizar.AutoSize = false;
            this.actualizar.Dock = System.Windows.Forms.DockStyle.None;
            this.actualizar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.actualizar.Location = new System.Drawing.Point(104, 42);
            this.actualizar.Size = new System.Drawing.Size(100, 32);
            this.darDeBaja.AutoSize = false;
            this.darDeBaja.Dock = System.Windows.Forms.DockStyle.None;
            this.darDeBaja.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.darDeBaja.Location = new System.Drawing.Point(0, 82);
            this.darDeBaja.Size = new System.Drawing.Size(100, 32);
            this.reactivar.AutoSize = false;
            this.reactivar.Dock = System.Windows.Forms.DockStyle.None;
            this.reactivar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.reactivar.Location = new System.Drawing.Point(104, 82);
            this.reactivar.Size = new System.Drawing.Size(100, 32);
            this.filtroEstado.AutoSize = false;
            this.filtroEstado.Dock = System.Windows.Forms.DockStyle.None;
            this.filtroEstado.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.filtroEstado.Location = new System.Drawing.Point(310, 22);
            this.filtroEstado.Size = new System.Drawing.Size(190, 25);
            this.buscador.AutoSize = false;
            this.buscador.Dock = System.Windows.Forms.DockStyle.None;
            this.buscador.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.buscador.Location = new System.Drawing.Point(0, 22);
            this.buscador.Size = new System.Drawing.Size(298, 24);
            this.tabla.AutoSize = false;
            this.tabla.Dock = System.Windows.Forms.DockStyle.None;
            this.tabla.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.tabla.Location = new System.Drawing.Point(16, 128);
            this.tabla.Size = new System.Drawing.Size(551, 340);
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
            this.panelBeneficios.ResumeLayout(false);
            this.panelBeneficios.PerformLayout();
            this.panelAcciones.ResumeLayout(false);
            this.ResumeLayout(false);

            this.Load += new System.EventHandler(this.GestionPlanesFormulario_Load);
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            this.tabla.SelectionChanged += new System.EventHandler(this.tabla_SelectionChanged);
            this.buscador.TextChanged += new System.EventHandler(this.buscador_TextChanged);
            this.filtroEstado.SelectedIndexChanged += new System.EventHandler(this.filtroEstado_SelectedIndexChanged);
            this.nuevo.Click += new System.EventHandler(this.nuevo_Click);
            this.guardar.Click += new System.EventHandler(this.guardar_Click);
            this.actualizar.Click += new System.EventHandler(this.actualizar_Click);
            this.darDeBaja.Click += new System.EventHandler(this.darDeBaja_Click);
            this.reactivar.Click += new System.EventHandler(this.reactivar_Click);
                    this.precio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.precio_KeyPress);
        }

    }
}
