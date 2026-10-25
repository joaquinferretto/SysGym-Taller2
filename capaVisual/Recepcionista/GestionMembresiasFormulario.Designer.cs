using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Recepcionista
{
    partial class GestionMembresiasFormulario
    {
        private IContainer components;
        private ErrorProvider indicadorErrores;
        private Label lblEstado; private TableLayoutPanel panelContenido; private Panel panelListado; private Label lblListado; private Label lblAyuda; private Panel panelDetalle; private Label lblFormulario; private Label lblSocio; private Label lblPlan; private Label lblInicio; private Label lblEstadoMembresia; private Label estadoMembresia; private DataGridView tabla; private DataGridViewTextBoxColumn colId; private DataGridViewTextBoxColumn colSocio; private DataGridViewTextBoxColumn colDni; private DataGridViewTextBoxColumn colPlan; private DataGridViewTextBoxColumn colInicio; private DataGridViewTextBoxColumn colEstado;
        private TextBox buscador; private ComboBox socio; private ComboBox plan; private DateTimePicker inicio; private Button nuevo; private Button nuevaExistente; private Button crear; private Button actualizar; private Button habilitar; private Button deshabilitar; private Button generarCuota;
        private Label lblObjetivo; private ComboBox objetivo; private Label lblNivel; private ComboBox nivel; private Label lblSugerencia;
        private ContextMenuStrip menuMembresias;
        private ToolStripMenuItem fichaMenu;
        private ToolStripMenuItem habilitarMenu;
        private ToolStripMenuItem deshabilitarMenu;

        protected override void Dispose(bool liberarRecursos) { if (liberarRecursos && components != null) components.Dispose(); base.Dispose(liberarRecursos); }

        private System.Windows.Forms.Button btnFicha;
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblEstado = new System.Windows.Forms.Label();
            this.panelContenido = new System.Windows.Forms.TableLayoutPanel();
            this.panelListado = new System.Windows.Forms.Panel();
            this.tabla = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSocio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDni = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPlan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colInicio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuMembresias = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.fichaMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.habilitarMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.deshabilitarMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.buscador = new System.Windows.Forms.TextBox();
            this.lblAyuda = new System.Windows.Forms.Label();
            this.lblListado = new System.Windows.Forms.Label();
            this.panelDetalle = new System.Windows.Forms.Panel();
            this.btnFicha = new System.Windows.Forms.Button();
            this.lblFormulario = new System.Windows.Forms.Label();
            this.lblSocio = new System.Windows.Forms.Label();
            this.socio = new System.Windows.Forms.ComboBox();
            this.lblPlan = new System.Windows.Forms.Label();
            this.plan = new System.Windows.Forms.ComboBox();
            this.lblInicio = new System.Windows.Forms.Label();
            this.inicio = new System.Windows.Forms.DateTimePicker();
            this.lblEstadoMembresia = new System.Windows.Forms.Label();
            this.estadoMembresia = new System.Windows.Forms.Label();
            this.lblObjetivo = new System.Windows.Forms.Label();
            this.objetivo = new System.Windows.Forms.ComboBox();
            this.lblNivel = new System.Windows.Forms.Label();
            this.nivel = new System.Windows.Forms.ComboBox();
            this.lblSugerencia = new System.Windows.Forms.Label();
            this.nuevo = new System.Windows.Forms.Button();
            this.nuevaExistente = new System.Windows.Forms.Button();
            this.crear = new System.Windows.Forms.Button();
            this.actualizar = new System.Windows.Forms.Button();
            this.habilitar = new System.Windows.Forms.Button();
            this.deshabilitar = new System.Windows.Forms.Button();
            this.generarCuota = new System.Windows.Forms.Button();
            this.indicadorErrores = new System.Windows.Forms.ErrorProvider(this.components);
            this.panelContenido.SuspendLayout();
            this.panelListado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).BeginInit();
            this.menuMembresias.SuspendLayout();
            this.panelDetalle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.indicadorErrores)).BeginInit();
            this.SuspendLayout();
            //
            // lblEstado
            //
            this.lblEstado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.lblEstado.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblEstado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblEstado.Location = new System.Drawing.Point(0, 562);
            this.lblEstado.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Padding = new System.Windows.Forms.Padding(22, 0, 15, 0);
            this.lblEstado.Size = new System.Drawing.Size(1250, 38);
            this.lblEstado.TabIndex = 3;
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // panelContenido
            //
            this.panelContenido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelContenido.ColumnCount = 2;
            this.panelContenido.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelContenido.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 525F));
            this.panelContenido.Controls.Add(this.panelListado, 0, 0);
            this.panelContenido.Controls.Add(this.panelDetalle, 1, 0);
            this.panelContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenido.Location = new System.Drawing.Point(0, 0);
            this.panelContenido.Margin = new System.Windows.Forms.Padding(4);
            this.panelContenido.Name = "panelContenido";
            this.panelContenido.Padding = new System.Windows.Forms.Padding(20);
            this.panelContenido.RowCount = 1;
            this.panelContenido.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelContenido.Size = new System.Drawing.Size(1250, 562);
            this.panelContenido.TabIndex = 2;
            //
            // panelListado
            //
            this.panelListado.BackColor = System.Drawing.Color.White;
            this.panelListado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelListado.Controls.Add(this.tabla);
            this.panelListado.Controls.Add(this.buscador);
            this.panelListado.Controls.Add(this.lblAyuda);
            this.panelListado.Controls.Add(this.lblListado);
            this.panelListado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelListado.Location = new System.Drawing.Point(20, 20);
            this.panelListado.Margin = new System.Windows.Forms.Padding(0, 0, 20, 0);
            this.panelListado.Name = "panelListado";
            this.panelListado.Padding = new System.Windows.Forms.Padding(20);
            this.panelListado.Size = new System.Drawing.Size(665, 522);
            this.panelListado.TabIndex = 0;
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
            this.colSocio,
            this.colDni,
            this.colPlan,
            this.colInicio,
            this.colEstado});
            this.tabla.ContextMenuStrip = this.menuMembresias;
            this.tabla.Location = new System.Drawing.Point(20, 132);
            this.tabla.Margin = new System.Windows.Forms.Padding(0);
            this.tabla.MultiSelect = false;
            this.tabla.Name = "tabla";
            this.tabla.ReadOnly = true;
            this.tabla.RowHeadersVisible = false;
            this.tabla.RowHeadersWidth = 51;
            this.tabla.RowTemplate.Height = 30;
            this.tabla.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tabla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tabla.Size = new System.Drawing.Size(623, 370);
            this.tabla.TabIndex = 3;
            this.tabla.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tabla_CellDoubleClick);
            this.tabla.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.tabla_CellMouseDown);
            this.tabla.SelectionChanged += new System.EventHandler(this.tabla_SelectionChanged);
            //
            // colId
            //
            this.colId.FillWeight = 50F;
            this.colId.HeaderText = "Id";
            this.colId.MinimumWidth = 6;
            this.colId.Name = "colId";
            this.colId.ReadOnly = true;
            this.colId.Visible = false;
            //
            // colSocio
            //
            this.colSocio.FillWeight = 135F;
            this.colSocio.HeaderText = "Socio";
            this.colSocio.MinimumWidth = 120;
            this.colSocio.Name = "colSocio";
            this.colSocio.ReadOnly = true;
            //
            // colDni
            //
            this.colDni.FillWeight = 85F;
            this.colDni.HeaderText = "DNI";
            this.colDni.MinimumWidth = 90;
            this.colDni.Name = "colDni";
            this.colDni.ReadOnly = true;
            this.colDni.Visible = false;
            //
            // colPlan
            //
            this.colPlan.FillWeight = 90F;
            this.colPlan.HeaderText = "Plan";
            this.colPlan.MinimumWidth = 100;
            this.colPlan.Name = "colPlan";
            this.colPlan.ReadOnly = true;
            //
            // colInicio
            //
            this.colInicio.FillWeight = 85F;
            this.colInicio.HeaderText = "Inicio";
            this.colInicio.MinimumWidth = 90;
            this.colInicio.Name = "colInicio";
            this.colInicio.ReadOnly = true;
            this.colInicio.Visible = false;
            //
            // colEstado
            //
            this.colEstado.HeaderText = "Estado";
            this.colEstado.MinimumWidth = 85;
            this.colEstado.Name = "colEstado";
            this.colEstado.ReadOnly = true;
            //
            // menuMembresias
            //
            this.menuMembresias.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuMembresias.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fichaMenu,
            this.habilitarMenu,
            this.deshabilitarMenu});
            this.menuMembresias.Name = "menuMembresias";
            this.menuMembresias.Size = new System.Drawing.Size(203, 76);
            //
            // fichaMenu
            //
            this.fichaMenu.Name = "fichaMenu";
            this.fichaMenu.Size = new System.Drawing.Size(202, 24);
            this.fichaMenu.Text = "Ver ficha completa";
            this.fichaMenu.Click += new System.EventHandler(this.btnFicha_Click);
            //
            // habilitarMenu
            //
            this.habilitarMenu.Name = "habilitarMenu";
            this.habilitarMenu.Size = new System.Drawing.Size(202, 24);
            this.habilitarMenu.Text = "Reactivar";
            this.habilitarMenu.Click += new System.EventHandler(this.habilitar_Click);
            //
            // deshabilitarMenu
            //
            this.deshabilitarMenu.Name = "deshabilitarMenu";
            this.deshabilitarMenu.Size = new System.Drawing.Size(202, 24);
            this.deshabilitarMenu.Text = "Dar de baja";
            this.deshabilitarMenu.Click += new System.EventHandler(this.deshabilitar_Click);
            //
            // buscador
            //
            this.buscador.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buscador.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.buscador.Location = new System.Drawing.Point(20, 88);
            this.buscador.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.buscador.Name = "buscador";
            this.buscador.Size = new System.Drawing.Size(623, 29);
            this.buscador.TabIndex = 2;
            this.buscador.TextChanged += new System.EventHandler(this.buscador_TextChanged);
            //
            // lblAyuda
            //
            this.lblAyuda.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAyuda.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblAyuda.Location = new System.Drawing.Point(20, 55);
            this.lblAyuda.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.lblAyuda.Name = "lblAyuda";
            this.lblAyuda.Size = new System.Drawing.Size(749, 30);
            this.lblAyuda.TabIndex = 1;
            this.lblAyuda.Text = "Busca por socio, DNI o plan";
            this.lblAyuda.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblListado
            //
            this.lblListado.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblListado.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblListado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblListado.Location = new System.Drawing.Point(20, 20);
            this.lblListado.Margin = new System.Windows.Forms.Padding(0);
            this.lblListado.Name = "lblListado";
            this.lblListado.Size = new System.Drawing.Size(749, 35);
            this.lblListado.TabIndex = 0;
            this.lblListado.Text = "Membresías";
            this.lblListado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // panelDetalle
            //
            this.panelDetalle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDetalle.BackColor = System.Drawing.Color.White;
            this.panelDetalle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDetalle.Controls.Add(this.btnFicha);
            this.panelDetalle.Controls.Add(this.lblFormulario);
            this.panelDetalle.Controls.Add(this.lblSocio);
            this.panelDetalle.Controls.Add(this.socio);
            this.panelDetalle.Controls.Add(this.lblPlan);
            this.panelDetalle.Controls.Add(this.plan);
            this.panelDetalle.Controls.Add(this.lblInicio);
            this.panelDetalle.Controls.Add(this.inicio);
            this.panelDetalle.Controls.Add(this.lblEstadoMembresia);
            this.panelDetalle.Controls.Add(this.estadoMembresia);
            this.panelDetalle.Controls.Add(this.lblObjetivo);
            this.panelDetalle.Controls.Add(this.objetivo);
            this.panelDetalle.Controls.Add(this.lblNivel);
            this.panelDetalle.Controls.Add(this.nivel);
            this.panelDetalle.Controls.Add(this.lblSugerencia);
            this.panelDetalle.Controls.Add(this.nuevo);
            this.panelDetalle.Controls.Add(this.nuevaExistente);
            this.panelDetalle.Controls.Add(this.crear);
            this.panelDetalle.Controls.Add(this.actualizar);
            this.panelDetalle.Controls.Add(this.habilitar);
            this.panelDetalle.Controls.Add(this.deshabilitar);
            this.panelDetalle.Controls.Add(this.generarCuota);
            this.panelDetalle.Location = new System.Drawing.Point(705, 20);
            this.panelDetalle.Margin = new System.Windows.Forms.Padding(0);
            this.panelDetalle.Name = "panelDetalle";
            this.panelDetalle.Padding = new System.Windows.Forms.Padding(20);
            this.panelDetalle.Size = new System.Drawing.Size(525, 540);
            this.panelDetalle.TabIndex = 1;
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
            this.btnFicha.Location = new System.Drawing.Point(20, 581);
            this.btnFicha.Margin = new System.Windows.Forms.Padding(4);
            this.btnFicha.Name = "btnFicha";
            this.btnFicha.Size = new System.Drawing.Size(485, 42);
            this.btnFicha.TabIndex = 30;
            this.btnFicha.Text = "Ver ficha completa";
            this.btnFicha.UseVisualStyleBackColor = false;
            this.btnFicha.Click += new System.EventHandler(this.btnFicha_Click);
            //
            // lblFormulario
            //
            this.lblFormulario.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblFormulario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblFormulario.Location = new System.Drawing.Point(20, 20);
            this.lblFormulario.Margin = new System.Windows.Forms.Padding(0, 0, 0, 15);
            this.lblFormulario.Name = "lblFormulario";
            this.lblFormulario.Size = new System.Drawing.Size(485, 40);
            this.lblFormulario.TabIndex = 0;
            this.lblFormulario.Text = "Nueva membresía";
            this.lblFormulario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblSocio
            //
            this.lblSocio.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblSocio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblSocio.Location = new System.Drawing.Point(20, 72);
            this.lblSocio.Margin = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.lblSocio.Name = "lblSocio";
            this.lblSocio.Size = new System.Drawing.Size(95, 38);
            this.lblSocio.TabIndex = 1;
            this.lblSocio.Text = "Socio:";
            this.lblSocio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // socio
            //
            this.socio.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.socio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.socio.DropDownWidth = 350;
            this.socio.Location = new System.Drawing.Point(125, 80);
            this.socio.Margin = new System.Windows.Forms.Padding(0, 4, 20, 10);
            this.socio.Name = "socio";
            this.socio.Size = new System.Drawing.Size(380, 29);
            this.socio.TabIndex = 2;
            //
            // lblPlan
            //
            this.lblPlan.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblPlan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblPlan.Location = new System.Drawing.Point(20, 120);
            this.lblPlan.Margin = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.lblPlan.Name = "lblPlan";
            this.lblPlan.Size = new System.Drawing.Size(145, 38);
            this.lblPlan.TabIndex = 3;
            this.lblPlan.Text = "Plan:";
            this.lblPlan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // plan
            //
            this.plan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.plan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.plan.Location = new System.Drawing.Point(175, 128);
            this.plan.Margin = new System.Windows.Forms.Padding(0, 4, 20, 10);
            this.plan.Name = "plan";
            this.plan.Size = new System.Drawing.Size(330, 29);
            this.plan.TabIndex = 4;
            //
            // lblInicio
            //
            this.lblInicio.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblInicio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblInicio.Location = new System.Drawing.Point(20, 168);
            this.lblInicio.Margin = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.lblInicio.Name = "lblInicio";
            this.lblInicio.Size = new System.Drawing.Size(145, 38);
            this.lblInicio.TabIndex = 5;
            this.lblInicio.Text = "Inicio:";
            this.lblInicio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // inicio
            //
            this.inicio.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.inicio.Location = new System.Drawing.Point(175, 172);
            this.inicio.Margin = new System.Windows.Forms.Padding(0, 4, 20, 10);
            this.inicio.Name = "inicio";
            this.inicio.Size = new System.Drawing.Size(330, 29);
            this.inicio.TabIndex = 6;
            this.inicio.ValueChanged += new System.EventHandler(this.inicio_ValueChanged);
            //
            // lblEstadoMembresia
            //
            this.lblEstadoMembresia.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblEstadoMembresia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblEstadoMembresia.Location = new System.Drawing.Point(20, 215);
            this.lblEstadoMembresia.Margin = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.lblEstadoMembresia.Name = "lblEstadoMembresia";
            this.lblEstadoMembresia.Size = new System.Drawing.Size(145, 38);
            this.lblEstadoMembresia.TabIndex = 7;
            this.lblEstadoMembresia.Text = "Estado:";
            this.lblEstadoMembresia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // estadoMembresia
            //
            this.estadoMembresia.Location = new System.Drawing.Point(175, 215);
            this.estadoMembresia.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.estadoMembresia.Name = "estadoMembresia";
            this.estadoMembresia.Size = new System.Drawing.Size(330, 38);
            this.estadoMembresia.TabIndex = 8;
            this.estadoMembresia.Text = "Activa";
            this.estadoMembresia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblObjetivo
            //
            this.lblObjetivo.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblObjetivo.Location = new System.Drawing.Point(20, 259);
            this.lblObjetivo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblObjetivo.Name = "lblObjetivo";
            this.lblObjetivo.Size = new System.Drawing.Size(145, 38);
            this.lblObjetivo.TabIndex = 31;
            this.lblObjetivo.Text = "Objetivo:";
            this.lblObjetivo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // objetivo
            //
            this.objetivo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.objetivo.Items.AddRange(new object[] {
            "Libre",
            "Fuerza",
            "Hipertrofia",
            "Acondicionamiento"});
            this.objetivo.Location = new System.Drawing.Point(175, 264);
            this.objetivo.Margin = new System.Windows.Forms.Padding(4);
            this.objetivo.Name = "objetivo";
            this.objetivo.Size = new System.Drawing.Size(329, 29);
            this.objetivo.TabIndex = 9;
            this.objetivo.SelectedIndexChanged += new System.EventHandler(this.objetivo_SelectedIndexChanged);
            //
            // lblNivel
            //
            this.lblNivel.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblNivel.Location = new System.Drawing.Point(20, 309);
            this.lblNivel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNivel.Name = "lblNivel";
            this.lblNivel.Size = new System.Drawing.Size(145, 38);
            this.lblNivel.TabIndex = 32;
            this.lblNivel.Text = "Nivel:";
            this.lblNivel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // nivel
            //
            this.nivel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.nivel.Items.AddRange(new object[] {
            "Principiante",
            "Intermedio",
            "Avanzado"});
            this.nivel.Location = new System.Drawing.Point(175, 314);
            this.nivel.Margin = new System.Windows.Forms.Padding(4);
            this.nivel.Name = "nivel";
            this.nivel.Size = new System.Drawing.Size(329, 29);
            this.nivel.TabIndex = 10;
            //
            // lblSugerencia
            //
            this.lblSugerencia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.lblSugerencia.Location = new System.Drawing.Point(20, 359);
            this.lblSugerencia.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSugerencia.Name = "lblSugerencia";
            this.lblSugerencia.Size = new System.Drawing.Size(485, 70);
            this.lblSugerencia.TabIndex = 33;
            this.lblSugerencia.Text = "IMC no disponible. Sin sugerencia.";
            //
            // nuevo
            //
            this.nuevo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.nuevo.FlatAppearance.BorderSize = 0;
            this.nuevo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(77)))), ((int)(((byte)(88)))));
            this.nuevo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(96)))), ((int)(((byte)(109)))));
            this.nuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nuevo.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.nuevo.ForeColor = System.Drawing.Color.White;
            this.nuevo.Location = new System.Drawing.Point(350, 438);
            this.nuevo.Margin = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.nuevo.Name = "nuevo";
            this.nuevo.Size = new System.Drawing.Size(155, 40);
            this.nuevo.TabIndex = 9;
            this.nuevo.Text = "+ Nueva";
            this.nuevo.UseVisualStyleBackColor = false;
            this.nuevo.Click += new System.EventHandler(this.nuevo_Click);
            //
            // nuevaExistente
            //
            this.nuevaExistente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.nuevaExistente.FlatAppearance.BorderSize = 0;
            this.nuevaExistente.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(77)))), ((int)(((byte)(88)))));
            this.nuevaExistente.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(96)))), ((int)(((byte)(109)))));
            this.nuevaExistente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nuevaExistente.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.nuevaExistente.ForeColor = System.Drawing.Color.White;
            this.nuevaExistente.Location = new System.Drawing.Point(20, 482);
            this.nuevaExistente.Margin = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.nuevaExistente.Name = "nuevaExistente";
            this.nuevaExistente.Size = new System.Drawing.Size(237, 38);
            this.nuevaExistente.TabIndex = 15;
            this.nuevaExistente.Text = "+ Nueva";
            this.nuevaExistente.UseVisualStyleBackColor = false;
            this.nuevaExistente.Visible = false;
            this.nuevaExistente.Click += new System.EventHandler(this.nuevo_Click);
            //
            // crear
            //
            this.crear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.crear.FlatAppearance.BorderSize = 0;
            this.crear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(75)))), ((int)(((byte)(31)))));
            this.crear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(101)))), ((int)(((byte)(41)))));
            this.crear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.crear.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.crear.ForeColor = System.Drawing.Color.White;
            this.crear.Location = new System.Drawing.Point(185, 438);
            this.crear.Margin = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.crear.Name = "crear";
            this.crear.Size = new System.Drawing.Size(155, 40);
            this.crear.TabIndex = 10;
            this.crear.Text = "Guardar";
            this.crear.UseVisualStyleBackColor = false;
            this.crear.Click += new System.EventHandler(this.guardar_Click);
            //
            // actualizar
            //
            this.actualizar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(75)))), ((int)(((byte)(138)))));
            this.actualizar.FlatAppearance.BorderSize = 0;
            this.actualizar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(48)))), ((int)(((byte)(89)))));
            this.actualizar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(61)))), ((int)(((byte)(112)))));
            this.actualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.actualizar.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.actualizar.ForeColor = System.Drawing.Color.White;
            this.actualizar.Location = new System.Drawing.Point(350, 438);
            this.actualizar.Margin = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.actualizar.Name = "actualizar";
            this.actualizar.Size = new System.Drawing.Size(155, 40);
            this.actualizar.TabIndex = 11;
            this.actualizar.Text = "Asignar rutina";
            this.actualizar.UseVisualStyleBackColor = false;
            this.actualizar.Click += new System.EventHandler(this.asignarRutina_Click);
            //
            // habilitar
            //
            this.habilitar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.habilitar.FlatAppearance.BorderSize = 0;
            this.habilitar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(75)))), ((int)(((byte)(31)))));
            this.habilitar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(101)))), ((int)(((byte)(41)))));
            this.habilitar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.habilitar.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.habilitar.ForeColor = System.Drawing.Color.White;
            this.habilitar.Location = new System.Drawing.Point(268, 482);
            this.habilitar.Margin = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.habilitar.Name = "habilitar";
            this.habilitar.Size = new System.Drawing.Size(237, 38);
            this.habilitar.TabIndex = 12;
            this.habilitar.Text = "Reactivar";
            this.habilitar.UseVisualStyleBackColor = false;
            this.habilitar.Click += new System.EventHandler(this.habilitar_Click);
            //
            // deshabilitar
            //
            this.deshabilitar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.deshabilitar.FlatAppearance.BorderSize = 0;
            this.deshabilitar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.deshabilitar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.deshabilitar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deshabilitar.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.deshabilitar.ForeColor = System.Drawing.Color.White;
            this.deshabilitar.Location = new System.Drawing.Point(268, 482);
            this.deshabilitar.Margin = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.deshabilitar.Name = "deshabilitar";
            this.deshabilitar.Size = new System.Drawing.Size(237, 38);
            this.deshabilitar.TabIndex = 13;
            this.deshabilitar.Text = "Dar de baja";
            this.deshabilitar.UseVisualStyleBackColor = false;
            this.deshabilitar.Click += new System.EventHandler(this.deshabilitar_Click);
            //
            // generarCuota
            //
            this.generarCuota.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.generarCuota.FlatAppearance.BorderSize = 0;
            this.generarCuota.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(75)))), ((int)(((byte)(31)))));
            this.generarCuota.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(101)))), ((int)(((byte)(41)))));
            this.generarCuota.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.generarCuota.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.generarCuota.ForeColor = System.Drawing.Color.White;
            this.generarCuota.Location = new System.Drawing.Point(20, 438);
            this.generarCuota.Margin = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.generarCuota.Name = "generarCuota";
            this.generarCuota.Size = new System.Drawing.Size(155, 40);
            this.generarCuota.TabIndex = 14;
            this.generarCuota.Text = "Generar cuota";
            this.generarCuota.UseVisualStyleBackColor = false;
            this.generarCuota.Click += new System.EventHandler(this.generarCuota_Click);
            //
            // indicadorErrores
            //
            this.indicadorErrores.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.indicadorErrores.ContainerControl = this;
            //
            // GestionMembresiasFormulario
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1250, 600);
            this.Controls.Add(this.panelContenido);
            this.Controls.Add(this.lblEstado);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1146, 563);
            this.Name = "GestionMembresiasFormulario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SysGym";
            this.Load += new System.EventHandler(this.GestionMembresiasFormulario_Load);
            this.panelContenido.ResumeLayout(false);
            this.panelListado.ResumeLayout(false);
            this.panelListado.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).EndInit();
            this.menuMembresias.ResumeLayout(false);
            this.panelDetalle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.indicadorErrores)).EndInit();
            this.ResumeLayout(false);

                }

    }
}
