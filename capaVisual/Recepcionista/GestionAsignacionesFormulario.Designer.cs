using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Recepcionista
{
    partial class GestionAsignacionesFormulario
    {
        private IContainer components = null;
        private Panel panelEncabezado;
        private Label lblTitulo;
        private Label lblDescripcion;
        private Button btnVolver;
        private TableLayoutPanel barraAcciones;
        private Label lblBuscar;
        private TextBox buscador;
        private Label lblEstadoFiltro;
        private ComboBox filtroEstado;
        private Button actualizar;
        private Label lblEstado;
        private SplitContainer splitContenido;
        private TableLayoutPanel panelListado;
        private Label lblListadoTitulo;
        private DataGridView tabla;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colSocio;
        private DataGridViewTextBoxColumn colPlan;
        private DataGridViewTextBoxColumn colEntrenador;
        private DataGridViewTextBoxColumn colEstado;
        private GroupBox grupoFicha;
        private Label lblDetalleTitulo;
        private TableLayoutPanel tablaFicha;
        private Label lblSocio;
        private TextBox txtSocio;
        private Label lblDni;
        private TextBox txtDni;
        private Label lblPlan;
        private TextBox txtPlan;
        private Label lblVencimiento;
        private TextBox txtVencimiento;
        private Label lblEstadoMembresia;
        private TextBox txtEstadoMembresia;
        private Label lblEntrenadorActual;
        private TextBox txtEntrenadorActual;
        private Label lblNuevoEntrenador;
        private ComboBox entrenador;
        private FlowLayoutPanel accionesFicha;
        private Button asignar;
        private Button cambiar;
        private Button darDeBaja;

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
            this.panelEncabezado = new System.Windows.Forms.Panel();
            this.btnVolver = new System.Windows.Forms.Button();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.barraAcciones = new System.Windows.Forms.TableLayoutPanel();
            this.lblBuscar = new System.Windows.Forms.Label();
            this.buscador = new System.Windows.Forms.TextBox();
            this.lblEstadoFiltro = new System.Windows.Forms.Label();
            this.filtroEstado = new System.Windows.Forms.ComboBox();
            this.actualizar = new System.Windows.Forms.Button();
            this.lblEstado = new System.Windows.Forms.Label();
            this.splitContenido = new System.Windows.Forms.SplitContainer();
            this.panelListado = new System.Windows.Forms.TableLayoutPanel();
            this.lblListadoTitulo = new System.Windows.Forms.Label();
            this.tabla = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSocio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPlan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEntrenador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grupoFicha = new System.Windows.Forms.GroupBox();
            this.tablaFicha = new System.Windows.Forms.TableLayoutPanel();
            this.lblSocio = new System.Windows.Forms.Label();
            this.txtSocio = new System.Windows.Forms.TextBox();
            this.lblDni = new System.Windows.Forms.Label();
            this.txtDni = new System.Windows.Forms.TextBox();
            this.lblPlan = new System.Windows.Forms.Label();
            this.txtPlan = new System.Windows.Forms.TextBox();
            this.lblVencimiento = new System.Windows.Forms.Label();
            this.txtVencimiento = new System.Windows.Forms.TextBox();
            this.lblEstadoMembresia = new System.Windows.Forms.Label();
            this.txtEstadoMembresia = new System.Windows.Forms.TextBox();
            this.lblEntrenadorActual = new System.Windows.Forms.Label();
            this.txtEntrenadorActual = new System.Windows.Forms.TextBox();
            this.lblNuevoEntrenador = new System.Windows.Forms.Label();
            this.entrenador = new System.Windows.Forms.ComboBox();
            this.accionesFicha = new System.Windows.Forms.FlowLayoutPanel();
            this.asignar = new System.Windows.Forms.Button();
            this.cambiar = new System.Windows.Forms.Button();
            this.darDeBaja = new System.Windows.Forms.Button();
            this.lblDetalleTitulo = new System.Windows.Forms.Label();
            this.panelEncabezado.SuspendLayout();
            this.barraAcciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContenido)).BeginInit();
            this.splitContenido.Panel1.SuspendLayout();
            this.splitContenido.Panel2.SuspendLayout();
            this.splitContenido.SuspendLayout();
            this.panelListado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).BeginInit();
            this.grupoFicha.SuspendLayout();
            this.tablaFicha.SuspendLayout();
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
            this.panelEncabezado.Size = new System.Drawing.Size(1100, 56);
            this.panelEncabezado.TabIndex = 0;
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
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            //
            // lblDescripcion
            //
            this.lblDescripcion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.lblDescripcion.Location = new System.Drawing.Point(22, 39);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(700, 22);
            this.lblDescripcion.TabIndex = 1;
            this.lblDescripcion.Text = "Seleccioná una membresía para revisar y gestionar su vinculación";
            this.lblDescripcion.Visible = false;
            //
            // lblTitulo
            //
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(20, 8);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(900, 40);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Asignar entrenador | Vinculación de entrenadores y membresías";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // barraAcciones
            //
            this.barraAcciones.BackColor = System.Drawing.Color.White;
            this.barraAcciones.ColumnCount = 6;
            this.barraAcciones.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.barraAcciones.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 262F));
            this.barraAcciones.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.barraAcciones.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 136F));
            this.barraAcciones.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.barraAcciones.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.barraAcciones.Controls.Add(this.lblBuscar, 0, 0);
            this.barraAcciones.Controls.Add(this.buscador, 1, 0);
            this.barraAcciones.Controls.Add(this.lblEstadoFiltro, 2, 0);
            this.barraAcciones.Controls.Add(this.filtroEstado, 3, 0);
            this.barraAcciones.Controls.Add(this.actualizar, 5, 0);
            this.barraAcciones.Dock = System.Windows.Forms.DockStyle.Top;
            this.barraAcciones.Location = new System.Drawing.Point(0, 56);
            this.barraAcciones.Name = "barraAcciones";
            this.barraAcciones.Padding = new System.Windows.Forms.Padding(16, 6, 16, 6);
            this.barraAcciones.RowCount = 1;
            this.barraAcciones.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.barraAcciones.Size = new System.Drawing.Size(1100, 54);
            this.barraAcciones.TabIndex = 1;
            //
            // lblBuscar
            //
            this.lblBuscar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBuscar.Location = new System.Drawing.Point(19, 13);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(52, 28);
            this.lblBuscar.TabIndex = 0;
            this.lblBuscar.Text = "Buscar:";
            this.lblBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // buscador
            //
            this.buscador.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.buscador.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.buscador.Location = new System.Drawing.Point(77, 12);
            this.buscador.Name = "buscador";
            this.buscador.Size = new System.Drawing.Size(256, 29);
            this.buscador.TabIndex = 0;
            this.buscador.TextChanged += new System.EventHandler(this.buscador_TextChanged);
            //
            // lblEstadoFiltro
            //
            this.lblEstadoFiltro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEstadoFiltro.Location = new System.Drawing.Point(339, 13);
            this.lblEstadoFiltro.Name = "lblEstadoFiltro";
            this.lblEstadoFiltro.Size = new System.Drawing.Size(48, 28);
            this.lblEstadoFiltro.TabIndex = 1;
            this.lblEstadoFiltro.Text = "Estado:";
            this.lblEstadoFiltro.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // filtroEstado
            //
            this.filtroEstado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.filtroEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filtroEstado.Items.AddRange(new object[] {
            "Todos",
            "Asignados",
            "Sin asignar"});
            this.filtroEstado.Location = new System.Drawing.Point(393, 15);
            this.filtroEstado.Name = "filtroEstado";
            this.filtroEstado.Size = new System.Drawing.Size(130, 29);
            this.filtroEstado.TabIndex = 1;
            this.filtroEstado.SelectedIndexChanged += new System.EventHandler(this.filtroEstado_SelectedIndexChanged);
            //
            // actualizar
            //
            this.actualizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.actualizar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.actualizar.FlatAppearance.BorderSize = 0;
            this.actualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.actualizar.Location = new System.Drawing.Point(915, 9);
            this.actualizar.Name = "actualizar";
            this.actualizar.Size = new System.Drawing.Size(166, 34);
            this.actualizar.TabIndex = 2;
            this.actualizar.Text = "Actualizar listado";
            this.actualizar.UseVisualStyleBackColor = false;
            this.actualizar.Click += new System.EventHandler(this.actualizar_Click);
            //
            // lblEstado
            //
            this.lblEstado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.lblEstado.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblEstado.Location = new System.Drawing.Point(0, 652);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Padding = new System.Windows.Forms.Padding(16, 0, 8, 0);
            this.lblEstado.Size = new System.Drawing.Size(1100, 28);
            this.lblEstado.TabIndex = 3;
            this.lblEstado.Text = "Listo";
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // splitContenido
            //
            this.splitContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContenido.Location = new System.Drawing.Point(0, 110);
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
            this.splitContenido.Panel2.Controls.Add(this.grupoFicha);
            this.splitContenido.Panel2.Padding = new System.Windows.Forms.Padding(0, 16, 16, 16);
            this.splitContenido.Panel2MinSize = 340;
            this.splitContenido.Size = new System.Drawing.Size(1100, 542);
            this.splitContenido.SplitterDistance = 617;
            this.splitContenido.TabIndex = 4;
            //
            // panelListado
            //
            this.panelListado.ColumnCount = 1;
            this.panelListado.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelListado.Controls.Add(this.lblListadoTitulo, 0, 0);
            this.panelListado.Controls.Add(this.tabla, 0, 1);
            this.panelListado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelListado.Location = new System.Drawing.Point(16, 16);
            this.panelListado.Name = "panelListado";
            this.panelListado.RowCount = 2;
            this.panelListado.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.panelListado.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelListado.Size = new System.Drawing.Size(585, 510);
            this.panelListado.TabIndex = 0;
            //
            // lblListadoTitulo
            //
            this.lblListadoTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblListadoTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblListadoTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblListadoTitulo.Location = new System.Drawing.Point(3, 0);
            this.lblListadoTitulo.Name = "lblListadoTitulo";
            this.lblListadoTitulo.Size = new System.Drawing.Size(579, 34);
            this.lblListadoTitulo.TabIndex = 0;
            this.lblListadoTitulo.Text = "Membresías y asignación";
            this.lblListadoTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.colSocio,
            this.colPlan,
            this.colEntrenador,
            this.colEstado});
            this.tabla.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabla.Location = new System.Drawing.Point(3, 37);
            this.tabla.MultiSelect = false;
            this.tabla.Name = "tabla";
            this.tabla.ReadOnly = true;
            this.tabla.RowHeadersVisible = false;
            this.tabla.RowHeadersWidth = 51;
            this.tabla.RowTemplate.Height = 30;
            this.tabla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tabla.Size = new System.Drawing.Size(579, 470);
            this.tabla.TabIndex = 3;
            this.tabla.SelectionChanged += new System.EventHandler(this.tabla_SelectionChanged);
            //
            // colId
            //
            this.colId.MinimumWidth = 6;
            this.colId.Name = "colId";
            this.colId.ReadOnly = true;
            this.colId.Visible = false;
            //
            // colSocio
            //
            this.colSocio.FillWeight = 31F;
            this.colSocio.HeaderText = "Socio";
            this.colSocio.MinimumWidth = 110;
            this.colSocio.Name = "colSocio";
            this.colSocio.ReadOnly = true;
            //
            // colPlan
            //
            this.colPlan.FillWeight = 20F;
            this.colPlan.HeaderText = "Plan";
            this.colPlan.MinimumWidth = 80;
            this.colPlan.Name = "colPlan";
            this.colPlan.ReadOnly = true;
            //
            // colEntrenador
            //
            this.colEntrenador.FillWeight = 34F;
            this.colEntrenador.HeaderText = "Entrenador actual";
            this.colEntrenador.MinimumWidth = 130;
            this.colEntrenador.Name = "colEntrenador";
            this.colEntrenador.ReadOnly = true;
            //
            // colEstado
            //
            this.colEstado.FillWeight = 15F;
            this.colEstado.HeaderText = "Estado";
            this.colEstado.MinimumWidth = 80;
            this.colEstado.Name = "colEstado";
            this.colEstado.ReadOnly = true;
            //
            // grupoFicha
            //
            this.grupoFicha.BackColor = System.Drawing.Color.White;
            this.grupoFicha.Controls.Add(this.tablaFicha);
            this.grupoFicha.Controls.Add(this.accionesFicha);
            this.grupoFicha.Controls.Add(this.lblDetalleTitulo);
            this.grupoFicha.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grupoFicha.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.grupoFicha.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.grupoFicha.Location = new System.Drawing.Point(0, 16);
            this.grupoFicha.Name = "grupoFicha";
            this.grupoFicha.Padding = new System.Windows.Forms.Padding(16);
            this.grupoFicha.Size = new System.Drawing.Size(463, 510);
            this.grupoFicha.TabIndex = 0;
            this.grupoFicha.TabStop = false;
            this.grupoFicha.Text = "Vinculación";
            //
            // tablaFicha
            //
            this.tablaFicha.ColumnCount = 2;
            this.tablaFicha.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tablaFicha.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablaFicha.Controls.Add(this.lblSocio, 0, 0);
            this.tablaFicha.Controls.Add(this.txtSocio, 1, 0);
            this.tablaFicha.Controls.Add(this.lblDni, 0, 1);
            this.tablaFicha.Controls.Add(this.txtDni, 1, 1);
            this.tablaFicha.Controls.Add(this.lblPlan, 0, 2);
            this.tablaFicha.Controls.Add(this.txtPlan, 1, 2);
            this.tablaFicha.Controls.Add(this.lblVencimiento, 0, 3);
            this.tablaFicha.Controls.Add(this.txtVencimiento, 1, 3);
            this.tablaFicha.Controls.Add(this.lblEstadoMembresia, 0, 4);
            this.tablaFicha.Controls.Add(this.txtEstadoMembresia, 1, 4);
            this.tablaFicha.Controls.Add(this.lblEntrenadorActual, 0, 5);
            this.tablaFicha.Controls.Add(this.txtEntrenadorActual, 1, 5);
            this.tablaFicha.Controls.Add(this.lblNuevoEntrenador, 0, 6);
            this.tablaFicha.Controls.Add(this.entrenador, 1, 6);
            this.tablaFicha.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablaFicha.Location = new System.Drawing.Point(16, 71);
            this.tablaFicha.Name = "tablaFicha";
            this.tablaFicha.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.tablaFicha.RowCount = 7;
            this.tablaFicha.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.2857F));
            this.tablaFicha.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.2857F));
            this.tablaFicha.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.2857F));
            this.tablaFicha.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.2857F));
            this.tablaFicha.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.2857F));
            this.tablaFicha.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.2857F));
            this.tablaFicha.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.2857F));
            this.tablaFicha.Size = new System.Drawing.Size(431, 335);
            this.tablaFicha.TabIndex = 1;
            //
            // lblSocio
            //
            this.lblSocio.AutoSize = true;
            this.lblSocio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSocio.Location = new System.Drawing.Point(0, 8);
            this.lblSocio.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.lblSocio.Name = "lblSocio";
            this.lblSocio.Size = new System.Drawing.Size(151, 46);
            this.lblSocio.TabIndex = 0;
            this.lblSocio.Text = "Socio";
            this.lblSocio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // txtSocio
            //
            this.txtSocio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSocio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.txtSocio.Location = new System.Drawing.Point(166, 18);
            this.txtSocio.Name = "txtSocio";
            this.txtSocio.Size = new System.Drawing.Size(262, 25);
            this.txtSocio.TabIndex = 1;
            this.txtSocio.Text = "Seleccioná una membresía";
            this.txtSocio.ReadOnly = true;
            this.txtSocio.TabStop = false;
            //
            // lblDni
            //
            this.lblDni.AutoSize = true;
            this.lblDni.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDni.Location = new System.Drawing.Point(0, 54);
            this.lblDni.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.lblDni.Name = "lblDni";
            this.lblDni.Size = new System.Drawing.Size(151, 46);
            this.lblDni.TabIndex = 2;
            this.lblDni.Text = "DNI";
            this.lblDni.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // txtDni
            //
            this.txtDni.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDni.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.txtDni.Location = new System.Drawing.Point(166, 64);
            this.txtDni.Name = "txtDni";
            this.txtDni.Size = new System.Drawing.Size(262, 25);
            this.txtDni.TabIndex = 3;
            this.txtDni.Text = "-";
            this.txtDni.ReadOnly = true;
            this.txtDni.TabStop = false;
            //
            // lblPlan
            //
            this.lblPlan.AutoSize = true;
            this.lblPlan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPlan.Location = new System.Drawing.Point(0, 100);
            this.lblPlan.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.lblPlan.Name = "lblPlan";
            this.lblPlan.Size = new System.Drawing.Size(151, 46);
            this.lblPlan.TabIndex = 4;
            this.lblPlan.Text = "Plan";
            this.lblPlan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // txtPlan
            //
            this.txtPlan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPlan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.txtPlan.Location = new System.Drawing.Point(166, 110);
            this.txtPlan.Name = "txtPlan";
            this.txtPlan.Size = new System.Drawing.Size(262, 25);
            this.txtPlan.TabIndex = 5;
            this.txtPlan.Text = "-";
            this.txtPlan.ReadOnly = true;
            this.txtPlan.TabStop = false;
            //
            // lblVencimiento
            //
            this.lblVencimiento.AutoSize = true;
            this.lblVencimiento.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVencimiento.Location = new System.Drawing.Point(0, 146);
            this.lblVencimiento.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.lblVencimiento.Name = "lblVencimiento";
            this.lblVencimiento.Size = new System.Drawing.Size(151, 46);
            this.lblVencimiento.TabIndex = 6;
            this.lblVencimiento.Text = "Vencimiento";
            this.lblVencimiento.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // txtVencimiento
            //
            this.txtVencimiento.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVencimiento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.txtVencimiento.Location = new System.Drawing.Point(166, 156);
            this.txtVencimiento.Name = "txtVencimiento";
            this.txtVencimiento.Size = new System.Drawing.Size(262, 25);
            this.txtVencimiento.TabIndex = 7;
            this.txtVencimiento.Text = "-";
            this.txtVencimiento.ReadOnly = true;
            this.txtVencimiento.TabStop = false;
            //
            // lblEstadoMembresia
            //
            this.lblEstadoMembresia.AutoSize = true;
            this.lblEstadoMembresia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEstadoMembresia.Location = new System.Drawing.Point(0, 192);
            this.lblEstadoMembresia.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.lblEstadoMembresia.Name = "lblEstadoMembresia";
            this.lblEstadoMembresia.Size = new System.Drawing.Size(151, 46);
            this.lblEstadoMembresia.TabIndex = 8;
            this.lblEstadoMembresia.Text = "Estado membresía";
            this.lblEstadoMembresia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // txtEstadoMembresia
            //
            this.txtEstadoMembresia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEstadoMembresia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.txtEstadoMembresia.Location = new System.Drawing.Point(166, 202);
            this.txtEstadoMembresia.Name = "txtEstadoMembresia";
            this.txtEstadoMembresia.Size = new System.Drawing.Size(262, 25);
            this.txtEstadoMembresia.TabIndex = 9;
            this.txtEstadoMembresia.Text = "-";
            this.txtEstadoMembresia.ReadOnly = true;
            this.txtEstadoMembresia.TabStop = false;
            //
            // lblEntrenadorActual
            //
            this.lblEntrenadorActual.AutoSize = true;
            this.lblEntrenadorActual.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEntrenadorActual.Location = new System.Drawing.Point(0, 238);
            this.lblEntrenadorActual.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.lblEntrenadorActual.Name = "lblEntrenadorActual";
            this.lblEntrenadorActual.Size = new System.Drawing.Size(151, 46);
            this.lblEntrenadorActual.TabIndex = 10;
            this.lblEntrenadorActual.Text = "Entrenador actual";
            this.lblEntrenadorActual.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // txtEntrenadorActual
            //
            this.txtEntrenadorActual.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEntrenadorActual.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.txtEntrenadorActual.Location = new System.Drawing.Point(166, 248);
            this.txtEntrenadorActual.Name = "txtEntrenadorActual";
            this.txtEntrenadorActual.Size = new System.Drawing.Size(262, 25);
            this.txtEntrenadorActual.TabIndex = 11;
            this.txtEntrenadorActual.Text = "Sin asignar";
            this.txtEntrenadorActual.ReadOnly = true;
            this.txtEntrenadorActual.TabStop = false;
            //
            // lblNuevoEntrenador
            //
            this.lblNuevoEntrenador.AutoSize = true;
            this.lblNuevoEntrenador.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNuevoEntrenador.Location = new System.Drawing.Point(0, 284);
            this.lblNuevoEntrenador.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.lblNuevoEntrenador.Name = "lblNuevoEntrenador";
            this.lblNuevoEntrenador.Size = new System.Drawing.Size(151, 51);
            this.lblNuevoEntrenador.TabIndex = 12;
            this.lblNuevoEntrenador.Text = "Nuevo entrenador";
            this.lblNuevoEntrenador.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // entrenador
            //
            this.entrenador.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.entrenador.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.entrenador.Enabled = false;
            this.entrenador.Location = new System.Drawing.Point(163, 297);
            this.entrenador.Margin = new System.Windows.Forms.Padding(0);
            this.entrenador.Name = "entrenador";
            this.entrenador.Size = new System.Drawing.Size(268, 31);
            this.entrenador.TabIndex = 0;
            //
            // accionesFicha
            //
            this.accionesFicha.AutoSize = true;
            this.accionesFicha.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.accionesFicha.Controls.Add(this.asignar);
            this.accionesFicha.Controls.Add(this.cambiar);
            this.accionesFicha.Controls.Add(this.darDeBaja);
            this.accionesFicha.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.accionesFicha.Location = new System.Drawing.Point(16, 406);
            this.accionesFicha.Name = "accionesFicha";
            this.accionesFicha.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.accionesFicha.Size = new System.Drawing.Size(431, 88);
            this.accionesFicha.TabIndex = 2;
            //
            // asignar
            //
            this.asignar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.asignar.FlatAppearance.BorderSize = 0;
            this.asignar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.asignar.ForeColor = System.Drawing.Color.White;
            this.asignar.Location = new System.Drawing.Point(3, 11);
            this.asignar.Name = "asignar";
            this.asignar.Size = new System.Drawing.Size(170, 34);
            this.asignar.TabIndex = 5;
            this.asignar.Text = "Asignar entrenador";
            this.asignar.UseVisualStyleBackColor = false;
            this.asignar.Visible = false;
            this.asignar.Click += new System.EventHandler(this.asignar_Click);
            //
            // cambiar
            //
            this.cambiar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.cambiar.FlatAppearance.BorderSize = 0;
            this.cambiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cambiar.ForeColor = System.Drawing.Color.White;
            this.cambiar.Location = new System.Drawing.Point(179, 11);
            this.cambiar.Name = "cambiar";
            this.cambiar.Size = new System.Drawing.Size(170, 34);
            this.cambiar.TabIndex = 6;
            this.cambiar.Text = "Cambiar entrenador";
            this.cambiar.UseVisualStyleBackColor = false;
            this.cambiar.Visible = false;
            this.cambiar.Click += new System.EventHandler(this.cambiar_Click);
            //
            // darDeBaja
            //
            this.darDeBaja.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.darDeBaja.FlatAppearance.BorderSize = 0;
            this.darDeBaja.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.darDeBaja.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.darDeBaja.Location = new System.Drawing.Point(3, 51);
            this.darDeBaja.Name = "darDeBaja";
            this.darDeBaja.Size = new System.Drawing.Size(150, 34);
            this.darDeBaja.TabIndex = 7;
            this.darDeBaja.Text = "Dar de baja";
            this.darDeBaja.UseVisualStyleBackColor = false;
            this.darDeBaja.Visible = false;
            this.darDeBaja.Click += new System.EventHandler(this.darDeBaja_Click);
            //
            // lblDetalleTitulo
            //
            this.lblDetalleTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDetalleTitulo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblDetalleTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.lblDetalleTitulo.Location = new System.Drawing.Point(16, 39);
            this.lblDetalleTitulo.Name = "lblDetalleTitulo";
            this.lblDetalleTitulo.Size = new System.Drawing.Size(431, 32);
            this.lblDetalleTitulo.TabIndex = 0;
            this.lblDetalleTitulo.Text = "Seleccioná una membresía";
            //
            // GestionAsignacionesFormulario
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1100, 680);
            this.Controls.Add(this.splitContenido);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.barraAcciones);
            this.Controls.Add(this.panelEncabezado);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.MinimumSize = new System.Drawing.Size(900, 560);
            this.Name = "GestionAsignacionesFormulario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SysGym | Asignar entrenador";
            this.Load += new System.EventHandler(this.GestionAsignacionesFormulario_Load);
            this.panelEncabezado.ResumeLayout(false);
            this.barraAcciones.ResumeLayout(false);
            this.barraAcciones.PerformLayout();
            this.splitContenido.Panel1.ResumeLayout(false);
            this.splitContenido.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContenido)).EndInit();
            this.splitContenido.ResumeLayout(false);
            this.panelListado.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).EndInit();
            this.grupoFicha.ResumeLayout(false);
            this.grupoFicha.PerformLayout();
            this.tablaFicha.ResumeLayout(false);
            this.tablaFicha.PerformLayout();
            this.accionesFicha.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
