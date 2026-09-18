using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Recepcionista
{
    partial class GestionAsignacionesFormulario
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
        private Button actualizar;
        private Label lblEstado;
        private SplitContainer splitContenido;
        private Panel panelListado;
        private Label lblListadoTitulo;
        private DataGridView tabla;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colSocio;
        private DataGridViewTextBoxColumn colPlan;
        private DataGridViewTextBoxColumn colEntrenador;
        private DataGridViewTextBoxColumn colEstado;
        private Panel panelDetalle;
        private GroupBox grupoFicha;
        private Label lblDetalleTitulo;
        private TableLayoutPanel tablaFicha;
        private Label lblSocio;
        private Label lblSocioValor;
        private Label lblDni;
        private Label lblDniValor;
        private Label lblPlan;
        private Label lblPlanValor;
        private Label lblVencimiento;
        private Label lblVencimientoValor;
        private Label lblEstadoMembresia;
        private Label lblEstadoMembresiaValor;
        private Label lblEntrenadorActual;
        private Label lblEntrenadorActualValor;
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
            this.components = new Container();
            this.panelEncabezado = new Panel();
            this.lblTitulo = new Label();
            this.lblDescripcion = new Label();
            this.btnVolver = new Button();
            this.barraAcciones = new Panel();
            this.lblBuscar = new Label();
            this.buscador = new TextBox();
            this.lblEstadoFiltro = new Label();
            this.filtroEstado = new ComboBox();
            this.actualizar = new Button();
            this.lblEstado = new Label();
            this.splitContenido = new SplitContainer();
            this.panelListado = new Panel();
            this.lblListadoTitulo = new Label();
            this.tabla = new DataGridView();
            this.colId = new DataGridViewTextBoxColumn();
            this.colSocio = new DataGridViewTextBoxColumn();
            this.colPlan = new DataGridViewTextBoxColumn();
            this.colEntrenador = new DataGridViewTextBoxColumn();
            this.colEstado = new DataGridViewTextBoxColumn();
            this.panelDetalle = new Panel();
            this.grupoFicha = new GroupBox();
            this.lblDetalleTitulo = new Label();
            this.tablaFicha = new TableLayoutPanel();
            this.lblSocio = new Label();
            this.lblSocioValor = new Label();
            this.lblDni = new Label();
            this.lblDniValor = new Label();
            this.lblPlan = new Label();
            this.lblPlanValor = new Label();
            this.lblVencimiento = new Label();
            this.lblVencimientoValor = new Label();
            this.lblEstadoMembresia = new Label();
            this.lblEstadoMembresiaValor = new Label();
            this.lblEntrenadorActual = new Label();
            this.lblEntrenadorActualValor = new Label();
            this.lblNuevoEntrenador = new Label();
            this.entrenador = new ComboBox();
            this.accionesFicha = new FlowLayoutPanel();
            this.asignar = new Button();
            this.cambiar = new Button();
            this.darDeBaja = new Button();
            ((ISupportInitialize)(this.splitContenido)).BeginInit();
            this.splitContenido.Panel1.SuspendLayout();
            this.splitContenido.Panel2.SuspendLayout();
            this.splitContenido.SuspendLayout();
            ((ISupportInitialize)(this.tabla)).BeginInit();
            this.SuspendLayout();
            //
            // panelEncabezado
            //
            this.panelEncabezado.BackColor = Color.FromArgb(79, 70, 229);
            this.panelEncabezado.Controls.Add(this.btnVolver);
            this.panelEncabezado.Controls.Add(this.lblDescripcion);
            this.panelEncabezado.Controls.Add(this.lblTitulo);
            this.panelEncabezado.Dock = DockStyle.Top;
            this.panelEncabezado.Height = 56;
            this.panelEncabezado.Name = "panelEncabezado";
            this.panelEncabezado.TabIndex = 0;
            this.panelEncabezado.Visible = false;
            //
            // lblTitulo
            //
            this.lblTitulo.AutoSize = false;
            this.lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblTitulo.ForeColor = Color.White;
            this.lblTitulo.Location = new Point(20, 8);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new Size(900, 40);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Asignar entrenador | Vinculación de entrenadores y membresías";
            this.lblTitulo.TextAlign = ContentAlignment.MiddleLeft;
            //
            // lblDescripcion
            //
            this.lblDescripcion.AutoSize = false;
            this.lblDescripcion.ForeColor = Color.FromArgb(226, 232, 240);
            this.lblDescripcion.Location = new Point(22, 39);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new Size(700, 22);
            this.lblDescripcion.TabIndex = 1;
            this.lblDescripcion.Text = "Seleccioná una membresía para revisar y gestionar su vinculación";
            this.lblDescripcion.Visible = false;
            //
            // btnVolver
            //
            this.btnVolver.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnVolver.BackColor = Color.White;
            this.btnVolver.FlatAppearance.BorderSize = 0;
            this.btnVolver.FlatStyle = FlatStyle.Flat;
            this.btnVolver.Location = new Point(980, 10);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new Size(100, 36);
            this.btnVolver.TabIndex = 0;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = false;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            //
            // barraAcciones
            //
            this.barraAcciones.BackColor = Color.White;
            this.barraAcciones.Controls.Add(this.actualizar);
            this.barraAcciones.Controls.Add(this.filtroEstado);
            this.barraAcciones.Controls.Add(this.lblEstadoFiltro);
            this.barraAcciones.Controls.Add(this.buscador);
            this.barraAcciones.Controls.Add(this.lblBuscar);
            this.barraAcciones.Dock = DockStyle.Top;
            this.barraAcciones.Height = 54;
            this.barraAcciones.Name = "barraAcciones";
            this.barraAcciones.TabIndex = 1;
            //
            // lblBuscar
            //
            this.lblBuscar.AutoSize = false;
            this.lblBuscar.Location = new Point(16, 10);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new Size(52, 28);
            this.lblBuscar.TabIndex = 0;
            this.lblBuscar.Text = "Buscar:";
            this.lblBuscar.TextAlign = ContentAlignment.MiddleLeft;
            //
            // buscador
            //
            this.buscador.BorderStyle = BorderStyle.FixedSingle;
            this.buscador.Location = new Point(72, 10);
            this.buscador.Name = "buscador";
            this.buscador.Size = new Size(250, 27);
            this.buscador.TabIndex = 0;
            this.buscador.TextChanged += new System.EventHandler(this.buscador_TextChanged);
            //
            // lblEstadoFiltro
            //
            this.lblEstadoFiltro.AutoSize = false;
            this.lblEstadoFiltro.Location = new Point(340, 10);
            this.lblEstadoFiltro.Name = "lblEstadoFiltro";
            this.lblEstadoFiltro.Size = new Size(48, 28);
            this.lblEstadoFiltro.TabIndex = 1;
            this.lblEstadoFiltro.Text = "Estado:";
            this.lblEstadoFiltro.TextAlign = ContentAlignment.MiddleLeft;
            //
            // filtroEstado
            //
            this.filtroEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            this.filtroEstado.Items.AddRange(new object[] {
            "Todos",
            "Asignados",
            "Sin asignar"});
            this.filtroEstado.Location = new Point(392, 10);
            this.filtroEstado.Name = "filtroEstado";
            this.filtroEstado.Size = new Size(130, 28);
            this.filtroEstado.TabIndex = 1;
            this.filtroEstado.SelectedIndex = 0;
            this.filtroEstado.SelectedIndexChanged += new System.EventHandler(this.filtroEstado_SelectedIndexChanged);
            //
            // actualizar
            //
            this.actualizar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.actualizar.BackColor = Color.FromArgb(226, 232, 240);
            this.actualizar.FlatAppearance.BorderSize = 0;
            this.actualizar.FlatStyle = FlatStyle.Flat;
            this.actualizar.Location = new Point(914, 9);
            this.actualizar.Name = "actualizar";
            this.actualizar.Size = new Size(166, 34);
            this.actualizar.TabIndex = 2;
            this.actualizar.Text = "Actualizar listado";
            this.actualizar.UseVisualStyleBackColor = false;
            this.actualizar.Click += new System.EventHandler(this.actualizar_Click);
            //
            // lblEstado
            //
            this.lblEstado.BackColor = Color.FromArgb(226, 232, 240);
            this.lblEstado.Dock = DockStyle.Bottom;
            this.lblEstado.Height = 28;
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Padding = new Padding(16, 0, 8, 0);
            this.lblEstado.TabIndex = 3;
            this.lblEstado.Text = "Listo";
            this.lblEstado.TextAlign = ContentAlignment.MiddleLeft;
            //
            // splitContenido
            //
            this.splitContenido.Dock = DockStyle.Fill;
            this.splitContenido.FixedPanel = FixedPanel.None;
            this.splitContenido.IsSplitterFixed = false;
            this.splitContenido.MinimumSize = new Size(900, 420);
            this.splitContenido.Name = "splitContenido";
            this.splitContenido.Panel1.BackColor = Color.FromArgb(248, 250, 252);
            this.splitContenido.Panel1.Controls.Add(this.panelListado);
            this.splitContenido.Panel1MinSize = 420;
            this.splitContenido.Panel1.Padding = new Padding(16);
            this.splitContenido.Panel2.BackColor = Color.FromArgb(248, 250, 252);
            this.splitContenido.Panel2.Controls.Add(this.panelDetalle);
            this.splitContenido.Panel2MinSize = 340;
            this.splitContenido.Panel2.Padding = new Padding(0, 16, 16, 16);
            this.splitContenido.Size = new Size(1068, 480);
            this.splitContenido.SplitterDistance = 600;
            this.splitContenido.TabIndex = 4;
            //
            // panelListado
            //
            this.panelListado.Controls.Add(this.tabla);
            this.panelListado.Controls.Add(this.lblListadoTitulo);
            this.panelListado.Dock = DockStyle.Fill;
            this.panelListado.Name = "panelListado";
            this.panelListado.TabIndex = 0;
            //
            // lblListadoTitulo
            //
            this.lblListadoTitulo.Dock = DockStyle.Top;
            this.lblListadoTitulo.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            this.lblListadoTitulo.ForeColor = Color.FromArgb(30, 41, 59);
            this.lblListadoTitulo.Height = 34;
            this.lblListadoTitulo.Name = "lblListadoTitulo";
            this.lblListadoTitulo.TabIndex = 0;
            this.lblListadoTitulo.Text = "Membresías y asignación";
            this.lblListadoTitulo.TextAlign = ContentAlignment.MiddleLeft;
            //
            // tabla
            //
            this.tabla.AllowUserToAddRows = false;
            this.tabla.AllowUserToDeleteRows = false;
            this.tabla.AllowUserToResizeRows = false;
            this.tabla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.tabla.BackgroundColor = Color.White;
            this.tabla.BorderStyle = BorderStyle.FixedSingle;
            this.tabla.ColumnHeadersHeight = 34;
            this.tabla.Columns.AddRange(new DataGridViewColumn[] {
            this.colId,
            this.colSocio,
            this.colPlan,
            this.colEntrenador,
            this.colEstado});
            this.tabla.Dock = DockStyle.Fill;
            this.tabla.MultiSelect = false;
            this.tabla.Name = "tabla";
            this.tabla.ReadOnly = true;
            this.tabla.RowHeadersVisible = false;
            this.tabla.RowTemplate.Height = 30;
            this.tabla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.tabla.TabIndex = 3;
            this.tabla.SelectionChanged += new System.EventHandler(this.tabla_SelectionChanged);
            //
            // colId
            //
            this.colId.Name = "colId";
            this.colId.Visible = false;
            //
            // colSocio
            //
            this.colSocio.FillWeight = 31F;
            this.colSocio.HeaderText = "Socio";
            this.colSocio.MinimumWidth = 110;
            this.colSocio.Name = "colSocio";
            //
            // colPlan
            //
            this.colPlan.FillWeight = 20F;
            this.colPlan.HeaderText = "Plan";
            this.colPlan.MinimumWidth = 80;
            this.colPlan.Name = "colPlan";
            //
            // colEntrenador
            //
            this.colEntrenador.FillWeight = 34F;
            this.colEntrenador.HeaderText = "Entrenador actual";
            this.colEntrenador.MinimumWidth = 130;
            this.colEntrenador.Name = "colEntrenador";
            //
            // colEstado
            //
            this.colEstado.FillWeight = 15F;
            this.colEstado.HeaderText = "Estado";
            this.colEstado.MinimumWidth = 80;
            this.colEstado.Name = "colEstado";
            //
            // panelDetalle
            //
            this.panelDetalle.Controls.Add(this.grupoFicha);
            this.panelDetalle.Dock = DockStyle.Fill;
            this.panelDetalle.Name = "panelDetalle";
            this.panelDetalle.TabIndex = 1;
            //
            // grupoFicha
            //
            this.grupoFicha.BackColor = Color.White;
            this.grupoFicha.Controls.Add(this.tablaFicha);
            this.grupoFicha.Controls.Add(this.accionesFicha);
            this.grupoFicha.Controls.Add(this.lblDetalleTitulo);
            this.grupoFicha.Dock = DockStyle.Fill;
            this.grupoFicha.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            this.grupoFicha.ForeColor = Color.FromArgb(30, 41, 59);
            this.grupoFicha.Padding = new Padding(16);
            this.grupoFicha.Name = "grupoFicha";
            this.grupoFicha.TabIndex = 0;
            this.grupoFicha.TabStop = false;
            this.grupoFicha.Text = "Vinculación";
            //
            // lblDetalleTitulo
            //
            this.lblDetalleTitulo.Dock = DockStyle.Top;
            this.lblDetalleTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblDetalleTitulo.ForeColor = Color.FromArgb(79, 70, 229);
            this.lblDetalleTitulo.Height = 32;
            this.lblDetalleTitulo.Name = "lblDetalleTitulo";
            this.lblDetalleTitulo.TabIndex = 0;
            this.lblDetalleTitulo.Text = "Seleccioná una membresía";
            //
            // tablaFicha
            //
            this.tablaFicha.ColumnCount = 2;
            this.tablaFicha.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 142F));
            this.tablaFicha.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.tablaFicha.Controls.Add(this.lblSocio, 0, 0);
            this.tablaFicha.Controls.Add(this.lblSocioValor, 1, 0);
            this.tablaFicha.Controls.Add(this.lblDni, 0, 1);
            this.tablaFicha.Controls.Add(this.lblDniValor, 1, 1);
            this.tablaFicha.Controls.Add(this.lblPlan, 0, 2);
            this.tablaFicha.Controls.Add(this.lblPlanValor, 1, 2);
            this.tablaFicha.Controls.Add(this.lblVencimiento, 0, 3);
            this.tablaFicha.Controls.Add(this.lblVencimientoValor, 1, 3);
            this.tablaFicha.Controls.Add(this.lblEstadoMembresia, 0, 4);
            this.tablaFicha.Controls.Add(this.lblEstadoMembresiaValor, 1, 4);
            this.tablaFicha.Controls.Add(this.lblEntrenadorActual, 0, 5);
            this.tablaFicha.Controls.Add(this.lblEntrenadorActualValor, 1, 5);
            this.tablaFicha.Controls.Add(this.lblNuevoEntrenador, 0, 6);
            this.tablaFicha.Controls.Add(this.entrenador, 1, 6);
            this.tablaFicha.Dock = DockStyle.Fill;
            this.tablaFicha.Name = "tablaFicha";
            this.tablaFicha.Padding = new Padding(0, 8, 0, 0);
            this.tablaFicha.RowCount = 7;
            this.tablaFicha.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857F));
            this.tablaFicha.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857F));
            this.tablaFicha.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857F));
            this.tablaFicha.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857F));
            this.tablaFicha.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857F));
            this.tablaFicha.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857F));
            this.tablaFicha.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857F));
            this.tablaFicha.TabIndex = 1;
            //
            // lblSocio
            //
            this.lblSocio.Dock = DockStyle.Fill;
            this.lblSocio.Margin = new Padding(0, 0, 12, 0);
            this.lblSocio.Name = "lblSocio";
            this.lblSocio.TabIndex = 0;
            this.lblSocio.Text = "Socio";
            this.lblSocio.TextAlign = ContentAlignment.MiddleLeft;
            //
            // lblSocioValor
            //
            this.lblSocioValor.Dock = DockStyle.Fill;
            this.lblSocioValor.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblSocioValor.Name = "lblSocioValor";
            this.lblSocioValor.TabIndex = 1;
            this.lblSocioValor.Text = "Seleccioná una membresía";
            this.lblSocioValor.TextAlign = ContentAlignment.MiddleLeft;
            //
            // lblDni
            //
            this.lblDni.Dock = DockStyle.Fill;
            this.lblDni.Margin = new Padding(0, 0, 12, 0);
            this.lblDni.Name = "lblDni";
            this.lblDni.TabIndex = 2;
            this.lblDni.Text = "DNI";
            this.lblDni.TextAlign = ContentAlignment.MiddleLeft;
            //
            // lblDniValor
            //
            this.lblDniValor.Dock = DockStyle.Fill;
            this.lblDniValor.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblDniValor.Name = "lblDniValor";
            this.lblDniValor.TabIndex = 3;
            this.lblDniValor.Text = "-";
            this.lblDniValor.TextAlign = ContentAlignment.MiddleLeft;
            //
            // lblPlan
            //
            this.lblPlan.Dock = DockStyle.Fill;
            this.lblPlan.Margin = new Padding(0, 0, 12, 0);
            this.lblPlan.Name = "lblPlan";
            this.lblPlan.TabIndex = 4;
            this.lblPlan.Text = "Plan";
            this.lblPlan.TextAlign = ContentAlignment.MiddleLeft;
            //
            // lblPlanValor
            //
            this.lblPlanValor.Dock = DockStyle.Fill;
            this.lblPlanValor.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblPlanValor.Name = "lblPlanValor";
            this.lblPlanValor.TabIndex = 5;
            this.lblPlanValor.Text = "-";
            this.lblPlanValor.TextAlign = ContentAlignment.MiddleLeft;
            //
            // lblVencimiento
            //
            this.lblVencimiento.Dock = DockStyle.Fill;
            this.lblVencimiento.Margin = new Padding(0, 0, 12, 0);
            this.lblVencimiento.Name = "lblVencimiento";
            this.lblVencimiento.TabIndex = 6;
            this.lblVencimiento.Text = "Vencimiento";
            this.lblVencimiento.TextAlign = ContentAlignment.MiddleLeft;
            //
            // lblVencimientoValor
            //
            this.lblVencimientoValor.Dock = DockStyle.Fill;
            this.lblVencimientoValor.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblVencimientoValor.Name = "lblVencimientoValor";
            this.lblVencimientoValor.TabIndex = 7;
            this.lblVencimientoValor.Text = "-";
            this.lblVencimientoValor.TextAlign = ContentAlignment.MiddleLeft;
            //
            // lblEstadoMembresia
            //
            this.lblEstadoMembresia.Dock = DockStyle.Fill;
            this.lblEstadoMembresia.Margin = new Padding(0, 0, 12, 0);
            this.lblEstadoMembresia.Name = "lblEstadoMembresia";
            this.lblEstadoMembresia.TabIndex = 8;
            this.lblEstadoMembresia.Text = "Estado membresía";
            this.lblEstadoMembresia.TextAlign = ContentAlignment.MiddleLeft;
            //
            // lblEstadoMembresiaValor
            //
            this.lblEstadoMembresiaValor.Dock = DockStyle.Fill;
            this.lblEstadoMembresiaValor.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblEstadoMembresiaValor.Name = "lblEstadoMembresiaValor";
            this.lblEstadoMembresiaValor.TabIndex = 9;
            this.lblEstadoMembresiaValor.Text = "-";
            this.lblEstadoMembresiaValor.TextAlign = ContentAlignment.MiddleLeft;
            //
            // lblEntrenadorActual
            //
            this.lblEntrenadorActual.Dock = DockStyle.Fill;
            this.lblEntrenadorActual.Margin = new Padding(0, 0, 12, 0);
            this.lblEntrenadorActual.Name = "lblEntrenadorActual";
            this.lblEntrenadorActual.TabIndex = 10;
            this.lblEntrenadorActual.Text = "Entrenador actual";
            this.lblEntrenadorActual.TextAlign = ContentAlignment.MiddleLeft;
            //
            // lblEntrenadorActualValor
            //
            this.lblEntrenadorActualValor.Dock = DockStyle.Fill;
            this.lblEntrenadorActualValor.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblEntrenadorActualValor.Name = "lblEntrenadorActualValor";
            this.lblEntrenadorActualValor.TabIndex = 11;
            this.lblEntrenadorActualValor.Text = "Sin asignar";
            this.lblEntrenadorActualValor.TextAlign = ContentAlignment.MiddleLeft;
            //
            // lblNuevoEntrenador
            //
            this.lblNuevoEntrenador.Dock = DockStyle.Fill;
            this.lblNuevoEntrenador.Margin = new Padding(0, 0, 12, 0);
            this.lblNuevoEntrenador.Name = "lblNuevoEntrenador";
            this.lblNuevoEntrenador.TabIndex = 12;
            this.lblNuevoEntrenador.Text = "Nuevo entrenador";
            this.lblNuevoEntrenador.TextAlign = ContentAlignment.MiddleLeft;
            //
            // entrenador
            //
            this.entrenador.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.entrenador.AutoSize = false;
            this.entrenador.Dock = DockStyle.None;
            this.entrenador.DropDownStyle = ComboBoxStyle.DropDownList;
            this.entrenador.Enabled = false;
            this.entrenador.Height = 28;
            this.entrenador.Margin = new Padding(0);
            this.entrenador.Name = "entrenador";
            this.entrenador.TabIndex = 0;
            //
            // accionesFicha
            //
            this.accionesFicha.Controls.Add(this.asignar);
            this.accionesFicha.Controls.Add(this.cambiar);
            this.accionesFicha.Controls.Add(this.darDeBaja);
            this.accionesFicha.Dock = DockStyle.Bottom;
            this.accionesFicha.FlowDirection = FlowDirection.LeftToRight;
            this.accionesFicha.Height = 48;
            this.accionesFicha.Name = "accionesFicha";
            this.accionesFicha.Padding = new Padding(0, 8, 0, 0);
            this.accionesFicha.TabIndex = 2;
            this.accionesFicha.WrapContents = false;
            //
            // asignar
            //
            this.asignar.BackColor = Color.FromArgb(79, 70, 229);
            this.asignar.FlatAppearance.BorderSize = 0;
            this.asignar.FlatStyle = FlatStyle.Flat;
            this.asignar.ForeColor = Color.White;
            this.asignar.Name = "asignar";
            this.asignar.Size = new Size(170, 34);
            this.asignar.TabIndex = 5;
            this.asignar.Text = "Asignar entrenador";
            this.asignar.UseVisualStyleBackColor = false;
            this.asignar.Visible = false;
            this.asignar.Click += new System.EventHandler(this.asignar_Click);
            //
            // cambiar
            //
            this.cambiar.BackColor = Color.FromArgb(79, 70, 229);
            this.cambiar.FlatAppearance.BorderSize = 0;
            this.cambiar.FlatStyle = FlatStyle.Flat;
            this.cambiar.ForeColor = Color.White;
            this.cambiar.Name = "cambiar";
            this.cambiar.Size = new Size(170, 34);
            this.cambiar.TabIndex = 6;
            this.cambiar.Text = "Cambiar entrenador";
            this.cambiar.UseVisualStyleBackColor = false;
            this.cambiar.Visible = false;
            this.cambiar.Click += new System.EventHandler(this.cambiar_Click);
            //
            // darDeBaja
            //
            this.darDeBaja.BackColor = Color.FromArgb(254, 242, 242);
            this.darDeBaja.FlatAppearance.BorderSize = 0;
            this.darDeBaja.FlatStyle = FlatStyle.Flat;
            this.darDeBaja.ForeColor = Color.FromArgb(185, 28, 28);
            this.darDeBaja.Name = "darDeBaja";
            this.darDeBaja.Size = new Size(150, 34);
            this.darDeBaja.TabIndex = 7;
            this.darDeBaja.Text = "Dar de baja";
            this.darDeBaja.UseVisualStyleBackColor = false;
            this.darDeBaja.Visible = false;
            this.darDeBaja.Click += new System.EventHandler(this.darDeBaja_Click);
            //
            // GestionAsignacionesFormulario
            //
            this.AutoScaleDimensions = new SizeF(7F, 17F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(241, 245, 249);
            this.ClientSize = new Size(1100, 680);
            this.Controls.Add(this.splitContenido);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.barraAcciones);
            this.Controls.Add(this.panelEncabezado);
            this.Font = new Font("Segoe UI", 9.5F);
            this.MinimumSize = new Size(900, 560);
            this.Name = "GestionAsignacionesFormulario";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "SysGym | Asignar entrenador";
            this.Load += new System.EventHandler(this.GestionAsignacionesFormulario_Load);
            this.splitContenido.Panel2.ResumeLayout(false);
            this.splitContenido.Panel1.ResumeLayout(false);
            ((ISupportInitialize)(this.splitContenido)).EndInit();
            this.splitContenido.ResumeLayout(false);
            ((ISupportInitialize)(this.tabla)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
