using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Recepcionista
{
    partial class GestionAsignacionesFormulario
    {
        private IContainer components = null;
        private ErrorProvider indicadorErrores;
        private Label lblBuscar;
        private TextBox buscador;
        private Label lblEstadoFiltro;
        private ComboBox filtroEstado;
        private Label lblEstado;
        private SplitContainer splitContenido;
        private Label lblListadoTitulo;
        private DataGridView tabla;
        private ContextMenuStrip menuAsignaciones;
        private ToolStripMenuItem actualizarMenu;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colSocio;
        private DataGridViewTextBoxColumn colPlan;
        private DataGridViewTextBoxColumn colEntrenador;
        private DataGridViewTextBoxColumn colEstado;
        private Label lblDetalleTitulo;
        private Label lblVinculacion;
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
            this.components = new System.ComponentModel.Container();
            this.indicadorErrores = new System.Windows.Forms.ErrorProvider(this.components);
            this.lblBuscar = new System.Windows.Forms.Label();
            this.buscador = new System.Windows.Forms.TextBox();
            this.lblEstadoFiltro = new System.Windows.Forms.Label();
            this.filtroEstado = new System.Windows.Forms.ComboBox();
            this.lblEstado = new System.Windows.Forms.Label();
            this.splitContenido = new System.Windows.Forms.SplitContainer();
            this.lblListadoTitulo = new System.Windows.Forms.Label();
            this.tabla = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSocio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPlan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEntrenador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuAsignaciones = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.actualizarMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.lblVinculacion = new System.Windows.Forms.Label();
            this.lblDetalleTitulo = new System.Windows.Forms.Label();
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
            this.asignar = new System.Windows.Forms.Button();
            this.cambiar = new System.Windows.Forms.Button();
            this.darDeBaja = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.indicadorErrores)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContenido)).BeginInit();
            this.splitContenido.Panel1.SuspendLayout();
            this.splitContenido.Panel2.SuspendLayout();
            this.splitContenido.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).BeginInit();
            this.menuAsignaciones.SuspendLayout();
            this.SuspendLayout();
            //
            // indicadorErrores
            //
            this.indicadorErrores.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.indicadorErrores.ContainerControl = this;
            //
            // lblBuscar
            //
            this.lblBuscar.Location = new System.Drawing.Point(20, 68);
            this.lblBuscar.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(72, 32);
            this.lblBuscar.TabIndex = 0;
            this.lblBuscar.Text = "Buscar:";
            this.lblBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // buscador
            //
            this.buscador.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buscador.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.buscador.Location = new System.Drawing.Point(98, 68);
            this.buscador.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buscador.Name = "buscador";
            this.buscador.Size = new System.Drawing.Size(372, 29);
            this.buscador.TabIndex = 0;
            this.buscador.TextChanged += new System.EventHandler(this.buscador_TextChanged);
            //
            // lblEstadoFiltro
            //
            this.lblEstadoFiltro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEstadoFiltro.Location = new System.Drawing.Point(483, 68);
            this.lblEstadoFiltro.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEstadoFiltro.Name = "lblEstadoFiltro";
            this.lblEstadoFiltro.Size = new System.Drawing.Size(68, 32);
            this.lblEstadoFiltro.TabIndex = 1;
            this.lblEstadoFiltro.Text = "Estado:";
            this.lblEstadoFiltro.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // filtroEstado
            //
            this.filtroEstado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.filtroEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filtroEstado.Items.AddRange(new object[] {
            "Todos",
            "Asignados",
            "Sin asignar"});
            this.filtroEstado.Location = new System.Drawing.Point(556, 68);
            this.filtroEstado.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.filtroEstado.Name = "filtroEstado";
            this.filtroEstado.Size = new System.Drawing.Size(166, 29);
            this.filtroEstado.TabIndex = 1;
            this.filtroEstado.SelectedIndexChanged += new System.EventHandler(this.filtroEstado_SelectedIndexChanged);
            //
            // lblEstado
            //
            this.lblEstado.BackColor = System.Drawing.Color.Transparent;
            this.lblEstado.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblEstado.Location = new System.Drawing.Point(20, 795);
            this.lblEstado.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Padding = new System.Windows.Forms.Padding(20, 0, 10, 0);
            this.lblEstado.Size = new System.Drawing.Size(1335, 35);
            this.lblEstado.TabIndex = 3;
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // splitContenido
            //
            this.splitContenido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.splitContenido.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContenido.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContenido.Location = new System.Drawing.Point(20, 20);
            this.splitContenido.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitContenido.Name = "splitContenido";
            //
            // splitContenido.Panel1
            //
            this.splitContenido.Panel1.BackColor = System.Drawing.Color.White;
            this.splitContenido.Panel1.Controls.Add(this.lblListadoTitulo);
            this.splitContenido.Panel1.Controls.Add(this.lblBuscar);
            this.splitContenido.Panel1.Controls.Add(this.buscador);
            this.splitContenido.Panel1.Controls.Add(this.lblEstadoFiltro);
            this.splitContenido.Panel1.Controls.Add(this.filtroEstado);
            this.splitContenido.Panel1.Controls.Add(this.tabla);
            this.splitContenido.Panel1.Padding = new System.Windows.Forms.Padding(20, 20, 20, 20);
            this.splitContenido.Panel1MinSize = 420;
            //
            // splitContenido.Panel2
            //
            this.splitContenido.Panel2.BackColor = System.Drawing.Color.White;
            this.splitContenido.Panel2.Controls.Add(this.lblVinculacion);
            this.splitContenido.Panel2.Controls.Add(this.lblDetalleTitulo);
            this.splitContenido.Panel2.Controls.Add(this.lblSocio);
            this.splitContenido.Panel2.Controls.Add(this.txtSocio);
            this.splitContenido.Panel2.Controls.Add(this.lblDni);
            this.splitContenido.Panel2.Controls.Add(this.txtDni);
            this.splitContenido.Panel2.Controls.Add(this.lblPlan);
            this.splitContenido.Panel2.Controls.Add(this.txtPlan);
            this.splitContenido.Panel2.Controls.Add(this.lblVencimiento);
            this.splitContenido.Panel2.Controls.Add(this.txtVencimiento);
            this.splitContenido.Panel2.Controls.Add(this.lblEstadoMembresia);
            this.splitContenido.Panel2.Controls.Add(this.txtEstadoMembresia);
            this.splitContenido.Panel2.Controls.Add(this.lblEntrenadorActual);
            this.splitContenido.Panel2.Controls.Add(this.txtEntrenadorActual);
            this.splitContenido.Panel2.Controls.Add(this.lblNuevoEntrenador);
            this.splitContenido.Panel2.Controls.Add(this.entrenador);
            this.splitContenido.Panel2.Controls.Add(this.asignar);
            this.splitContenido.Panel2.Controls.Add(this.cambiar);
            this.splitContenido.Panel2.Controls.Add(this.darDeBaja);
            this.splitContenido.Panel2.Padding = new System.Windows.Forms.Padding(20, 20, 20, 20);
            this.splitContenido.Panel2MinSize = 480;
            this.splitContenido.Size = new System.Drawing.Size(1335, 775);
            this.splitContenido.SplitterDistance = 732;
            this.splitContenido.SplitterWidth = 15;
            this.splitContenido.TabIndex = 4;
            //
            // lblListadoTitulo
            //
            this.lblListadoTitulo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblListadoTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblListadoTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblListadoTitulo.Location = new System.Drawing.Point(20, 15);
            this.lblListadoTitulo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblListadoTitulo.Name = "lblListadoTitulo";
            this.lblListadoTitulo.Size = new System.Drawing.Size(703, 42);
            this.lblListadoTitulo.TabIndex = 0;
            this.lblListadoTitulo.Text = "Membresías y asignación";
            this.lblListadoTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.tabla.ColumnHeadersHeight = 34;
            this.tabla.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colSocio,
            this.colPlan,
            this.colEntrenador,
            this.colEstado});
            this.tabla.ContextMenuStrip = this.menuAsignaciones;
            this.tabla.Location = new System.Drawing.Point(20, 125);
            this.tabla.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabla.MultiSelect = false;
            this.tabla.Name = "tabla";
            this.tabla.ReadOnly = true;
            this.tabla.RowHeadersVisible = false;
            this.tabla.RowHeadersWidth = 51;
            this.tabla.RowTemplate.Height = 30;
            this.tabla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tabla.Size = new System.Drawing.Size(703, 631);
            this.tabla.TabIndex = 3;
            this.tabla.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.tabla_CellMouseDown);
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
            // menuAsignaciones
            //
            this.menuAsignaciones.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuAsignaciones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.actualizarMenu});
            this.menuAsignaciones.Name = "menuAsignaciones";
            this.menuAsignaciones.Size = new System.Drawing.Size(194, 28);
            //
            // actualizarMenu
            //
            this.actualizarMenu.Name = "actualizarMenu";
            this.actualizarMenu.Size = new System.Drawing.Size(193, 24);
            this.actualizarMenu.Text = "Actualizar listado";
            this.actualizarMenu.Click += new System.EventHandler(this.actualizar_Click);
            //
            // lblVinculacion
            //
            this.lblVinculacion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVinculacion.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblVinculacion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblVinculacion.Location = new System.Drawing.Point(20, 15);
            this.lblVinculacion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVinculacion.Name = "lblVinculacion";
            this.lblVinculacion.Size = new System.Drawing.Size(459, 42);
            this.lblVinculacion.TabIndex = 0;
            this.lblVinculacion.Text = "Vinculación";
            //
            // lblDetalleTitulo
            //
            this.lblDetalleTitulo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDetalleTitulo.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblDetalleTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.lblDetalleTitulo.Location = new System.Drawing.Point(20, 60);
            this.lblDetalleTitulo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDetalleTitulo.Name = "lblDetalleTitulo";
            this.lblDetalleTitulo.Size = new System.Drawing.Size(459, 38);
            this.lblDetalleTitulo.TabIndex = 0;
            this.lblDetalleTitulo.Text = "Seleccioná una membresía";
            //
            // lblSocio
            //
            this.lblSocio.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblSocio.Location = new System.Drawing.Point(20, 118);
            this.lblSocio.Margin = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.lblSocio.Name = "lblSocio";
            this.lblSocio.Size = new System.Drawing.Size(168, 32);
            this.lblSocio.TabIndex = 0;
            this.lblSocio.Text = "Socio";
            this.lblSocio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // txtSocio
            //
            this.txtSocio.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSocio.BackColor = System.Drawing.Color.White;
            this.txtSocio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.txtSocio.Location = new System.Drawing.Point(198, 118);
            this.txtSocio.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSocio.Name = "txtSocio";
            this.txtSocio.ReadOnly = true;
            this.txtSocio.Size = new System.Drawing.Size(280, 29);
            this.txtSocio.TabIndex = 1;
            this.txtSocio.TabStop = false;
            this.txtSocio.Text = "Seleccioná una membresía";
            //
            // lblDni
            //
            this.lblDni.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblDni.Location = new System.Drawing.Point(20, 168);
            this.lblDni.Margin = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.lblDni.Name = "lblDni";
            this.lblDni.Size = new System.Drawing.Size(168, 32);
            this.lblDni.TabIndex = 2;
            this.lblDni.Text = "DNI";
            this.lblDni.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // txtDni
            //
            this.txtDni.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDni.BackColor = System.Drawing.Color.White;
            this.txtDni.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.txtDni.Location = new System.Drawing.Point(198, 168);
            this.txtDni.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtDni.Name = "txtDni";
            this.txtDni.ReadOnly = true;
            this.txtDni.Size = new System.Drawing.Size(280, 29);
            this.txtDni.TabIndex = 3;
            this.txtDni.TabStop = false;
            this.txtDni.Text = "-";
            //
            // lblPlan
            //
            this.lblPlan.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblPlan.Location = new System.Drawing.Point(20, 218);
            this.lblPlan.Margin = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.lblPlan.Name = "lblPlan";
            this.lblPlan.Size = new System.Drawing.Size(168, 32);
            this.lblPlan.TabIndex = 4;
            this.lblPlan.Text = "Plan";
            this.lblPlan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // txtPlan
            //
            this.txtPlan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPlan.BackColor = System.Drawing.Color.White;
            this.txtPlan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.txtPlan.Location = new System.Drawing.Point(198, 218);
            this.txtPlan.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPlan.Name = "txtPlan";
            this.txtPlan.ReadOnly = true;
            this.txtPlan.Size = new System.Drawing.Size(280, 29);
            this.txtPlan.TabIndex = 5;
            this.txtPlan.TabStop = false;
            this.txtPlan.Text = "-";
            //
            // lblVencimiento
            //
            this.lblVencimiento.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblVencimiento.Location = new System.Drawing.Point(20, 268);
            this.lblVencimiento.Margin = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.lblVencimiento.Name = "lblVencimiento";
            this.lblVencimiento.Size = new System.Drawing.Size(168, 32);
            this.lblVencimiento.TabIndex = 6;
            this.lblVencimiento.Text = "Cuota hasta";
            this.lblVencimiento.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // txtVencimiento
            //
            this.txtVencimiento.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVencimiento.BackColor = System.Drawing.Color.White;
            this.txtVencimiento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.txtVencimiento.Location = new System.Drawing.Point(198, 268);
            this.txtVencimiento.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtVencimiento.Name = "txtVencimiento";
            this.txtVencimiento.ReadOnly = true;
            this.txtVencimiento.Size = new System.Drawing.Size(280, 29);
            this.txtVencimiento.TabIndex = 7;
            this.txtVencimiento.TabStop = false;
            this.txtVencimiento.Text = "-";
            //
            // lblEstadoMembresia
            //
            this.lblEstadoMembresia.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblEstadoMembresia.Location = new System.Drawing.Point(20, 318);
            this.lblEstadoMembresia.Margin = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.lblEstadoMembresia.Name = "lblEstadoMembresia";
            this.lblEstadoMembresia.Size = new System.Drawing.Size(168, 32);
            this.lblEstadoMembresia.TabIndex = 8;
            this.lblEstadoMembresia.Text = "Estado membresía";
            this.lblEstadoMembresia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // txtEstadoMembresia
            //
            this.txtEstadoMembresia.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEstadoMembresia.BackColor = System.Drawing.Color.White;
            this.txtEstadoMembresia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.txtEstadoMembresia.Location = new System.Drawing.Point(198, 318);
            this.txtEstadoMembresia.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtEstadoMembresia.Name = "txtEstadoMembresia";
            this.txtEstadoMembresia.ReadOnly = true;
            this.txtEstadoMembresia.Size = new System.Drawing.Size(280, 29);
            this.txtEstadoMembresia.TabIndex = 9;
            this.txtEstadoMembresia.TabStop = false;
            this.txtEstadoMembresia.Text = "-";
            //
            // lblEntrenadorActual
            //
            this.lblEntrenadorActual.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblEntrenadorActual.Location = new System.Drawing.Point(20, 368);
            this.lblEntrenadorActual.Margin = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.lblEntrenadorActual.Name = "lblEntrenadorActual";
            this.lblEntrenadorActual.Size = new System.Drawing.Size(168, 32);
            this.lblEntrenadorActual.TabIndex = 10;
            this.lblEntrenadorActual.Text = "Entrenador actual";
            this.lblEntrenadorActual.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // txtEntrenadorActual
            //
            this.txtEntrenadorActual.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEntrenadorActual.BackColor = System.Drawing.Color.White;
            this.txtEntrenadorActual.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.txtEntrenadorActual.Location = new System.Drawing.Point(198, 368);
            this.txtEntrenadorActual.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtEntrenadorActual.Name = "txtEntrenadorActual";
            this.txtEntrenadorActual.ReadOnly = true;
            this.txtEntrenadorActual.Size = new System.Drawing.Size(280, 29);
            this.txtEntrenadorActual.TabIndex = 11;
            this.txtEntrenadorActual.TabStop = false;
            this.txtEntrenadorActual.Text = "Sin asignar";
            //
            // lblNuevoEntrenador
            //
            this.lblNuevoEntrenador.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblNuevoEntrenador.Location = new System.Drawing.Point(20, 418);
            this.lblNuevoEntrenador.Margin = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.lblNuevoEntrenador.Name = "lblNuevoEntrenador";
            this.lblNuevoEntrenador.Size = new System.Drawing.Size(168, 32);
            this.lblNuevoEntrenador.TabIndex = 12;
            this.lblNuevoEntrenador.Text = "Nuevo entrenador";
            this.lblNuevoEntrenador.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // entrenador
            //
            this.entrenador.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.entrenador.BackColor = System.Drawing.Color.White;
            this.entrenador.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.entrenador.DropDownWidth = 520;
            this.entrenador.Enabled = false;
            this.entrenador.Location = new System.Drawing.Point(198, 418);
            this.entrenador.Margin = new System.Windows.Forms.Padding(0);
            this.entrenador.Name = "entrenador";
            this.entrenador.Size = new System.Drawing.Size(280, 29);
            this.entrenador.TabIndex = 0;
            //
            // asignar
            //
            this.asignar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.asignar.FlatAppearance.BorderSize = 0;
            this.asignar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(75)))), ((int)(((byte)(31)))));
            this.asignar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(101)))), ((int)(((byte)(41)))));
            this.asignar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.asignar.ForeColor = System.Drawing.Color.White;
            this.asignar.Location = new System.Drawing.Point(20, 485);
            this.asignar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.asignar.Name = "asignar";
            this.asignar.Size = new System.Drawing.Size(185, 45);
            this.asignar.TabIndex = 5;
            this.asignar.Text = "Asignar entrenador";
            this.asignar.UseVisualStyleBackColor = false;
            this.asignar.Visible = false;
            this.asignar.Click += new System.EventHandler(this.asignar_Click);
            //
            // cambiar
            //
            this.cambiar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(75)))), ((int)(((byte)(138)))));
            this.cambiar.FlatAppearance.BorderSize = 0;
            this.cambiar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(48)))), ((int)(((byte)(89)))));
            this.cambiar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(61)))), ((int)(((byte)(112)))));
            this.cambiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cambiar.ForeColor = System.Drawing.Color.White;
            this.cambiar.Location = new System.Drawing.Point(215, 485);
            this.cambiar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cambiar.Name = "cambiar";
            this.cambiar.Size = new System.Drawing.Size(215, 45);
            this.cambiar.TabIndex = 6;
            this.cambiar.Text = "Cambiar entrenador";
            this.cambiar.UseVisualStyleBackColor = false;
            this.cambiar.Visible = false;
            this.cambiar.Click += new System.EventHandler(this.cambiar_Click);
            //
            // darDeBaja
            //
            this.darDeBaja.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.darDeBaja.FlatAppearance.BorderSize = 0;
            this.darDeBaja.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.darDeBaja.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.darDeBaja.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.darDeBaja.ForeColor = System.Drawing.Color.White;
            this.darDeBaja.Location = new System.Drawing.Point(440, 485);
            this.darDeBaja.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.darDeBaja.Name = "darDeBaja";
            this.darDeBaja.Size = new System.Drawing.Size(185, 45);
            this.darDeBaja.TabIndex = 7;
            this.darDeBaja.Text = "Dar de baja";
            this.darDeBaja.UseVisualStyleBackColor = false;
            this.darDeBaja.Visible = false;
            this.darDeBaja.Click += new System.EventHandler(this.darDeBaja_Click);
            //
            // GestionAsignacionesFormulario
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1375, 850);
            this.Controls.Add(this.splitContenido);
            this.Controls.Add(this.lblEstado);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(1120, 688);
            this.Name = "GestionAsignacionesFormulario";
            this.Padding = new System.Windows.Forms.Padding(20, 20, 20, 20);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SysGym";
            this.Load += new System.EventHandler(this.GestionAsignacionesFormulario_Load);
            ((System.ComponentModel.ISupportInitialize)(this.indicadorErrores)).EndInit();
            this.splitContenido.Panel1.ResumeLayout(false);
            this.splitContenido.Panel1.PerformLayout();
            this.splitContenido.Panel2.ResumeLayout(false);
            this.splitContenido.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContenido)).EndInit();
            this.splitContenido.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).EndInit();
            this.menuAsignaciones.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
