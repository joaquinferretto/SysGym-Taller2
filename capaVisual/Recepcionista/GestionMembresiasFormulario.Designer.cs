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
        private TextBox buscador; private ComboBox socio; private ComboBox plan; private DateTimePicker inicio; private Button nuevo; private Button crear; private Button actualizar; private Button habilitar; private Button deshabilitar; private Button generarCuota;

        protected override void Dispose(bool liberarRecursos) { if (liberarRecursos && components != null) components.Dispose(); base.Dispose(liberarRecursos); }

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
            this.buscador = new System.Windows.Forms.TextBox();
            this.lblAyuda = new System.Windows.Forms.Label();
            this.lblListado = new System.Windows.Forms.Label();
            this.panelDetalle = new System.Windows.Forms.Panel();
            this.lblFormulario = new System.Windows.Forms.Label();
            this.lblSocio = new System.Windows.Forms.Label();
            this.socio = new System.Windows.Forms.ComboBox();
            this.lblPlan = new System.Windows.Forms.Label();
            this.plan = new System.Windows.Forms.ComboBox();
            this.lblInicio = new System.Windows.Forms.Label();
            this.inicio = new System.Windows.Forms.DateTimePicker();
            this.lblEstadoMembresia = new System.Windows.Forms.Label();
            this.estadoMembresia = new System.Windows.Forms.Label();
            this.nuevo = new System.Windows.Forms.Button();
            this.crear = new System.Windows.Forms.Button();
            this.actualizar = new System.Windows.Forms.Button();
            this.habilitar = new System.Windows.Forms.Button();
            this.deshabilitar = new System.Windows.Forms.Button();
            this.generarCuota = new System.Windows.Forms.Button();
            this.indicadorErrores = new System.Windows.Forms.ErrorProvider(this.components);
            this.panelContenido.SuspendLayout();
            this.panelListado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).BeginInit();
            this.panelDetalle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.indicadorErrores)).BeginInit();
            this.SuspendLayout();
            //
            // lblEstado
            //
            this.lblEstado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.lblEstado.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblEstado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblEstado.Location = new System.Drawing.Point(0, 568);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Padding = new System.Windows.Forms.Padding(18, 0, 12, 0);
            this.lblEstado.Size = new System.Drawing.Size(1076, 30);
            this.lblEstado.TabIndex = 3;
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
            this.panelContenido.Location = new System.Drawing.Point(0, 0);
            this.panelContenido.Name = "panelContenido";
            this.panelContenido.Padding = new System.Windows.Forms.Padding(16);
            this.panelContenido.RowCount = 1;
            this.panelContenido.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelContenido.Size = new System.Drawing.Size(1076, 568);
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
            this.panelListado.Location = new System.Drawing.Point(16, 16);
            this.panelListado.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.panelListado.Name = "panelListado";
            this.panelListado.Padding = new System.Windows.Forms.Padding(16);
            this.panelListado.Size = new System.Drawing.Size(632, 536);
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
            this.tabla.Location = new System.Drawing.Point(16, 106);
            this.tabla.Margin = new System.Windows.Forms.Padding(0);
            this.tabla.MultiSelect = false;
            this.tabla.Name = "tabla";
            this.tabla.ReadOnly = true;
            this.tabla.RowHeadersVisible = false;
            this.tabla.RowTemplate.Height = 30;
            this.tabla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tabla.Size = new System.Drawing.Size(598, 412);
            this.tabla.TabIndex = 3;
            this.tabla.SelectionChanged += new System.EventHandler(this.tabla_SelectionChanged);
            //
            // colId
            //
            this.colId.FillWeight = 50F;
            this.colId.HeaderText = "Id";
            this.colId.Name = "colId";
            this.colId.ReadOnly = true;
            this.colId.Visible = false;
            //
            // colSocio
            //
            this.colSocio.FillWeight = 135F;
            this.colSocio.HeaderText = "Socio";
            this.colSocio.MinimumWidth = 90;
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
            //
            // colPlan
            //
            this.colPlan.FillWeight = 90F;
            this.colPlan.HeaderText = "Plan";
            this.colPlan.MinimumWidth = 90;
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
            //
            // colEstado
            //
            this.colEstado.HeaderText = "Estado";
            this.colEstado.MinimumWidth = 90;
            this.colEstado.Name = "colEstado";
            this.colEstado.ReadOnly = true;
            //
            // buscador
            //
            this.buscador.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buscador.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.buscador.Location = new System.Drawing.Point(16, 70);
            this.buscador.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.buscador.Name = "buscador";
            this.buscador.Size = new System.Drawing.Size(598, 26);
            this.buscador.TabIndex = 2;
            this.buscador.TextChanged += new System.EventHandler(this.buscador_TextChanged);
            //
            // lblAyuda
            //
            this.lblAyuda.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAyuda.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblAyuda.Location = new System.Drawing.Point(16, 44);
            this.lblAyuda.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.lblAyuda.Name = "lblAyuda";
            this.lblAyuda.Size = new System.Drawing.Size(598, 24);
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
            this.lblListado.Location = new System.Drawing.Point(16, 16);
            this.lblListado.Margin = new System.Windows.Forms.Padding(0);
            this.lblListado.Name = "lblListado";
            this.lblListado.Size = new System.Drawing.Size(598, 28);
            this.lblListado.TabIndex = 0;
            this.lblListado.Text = "Membresias";
            this.lblListado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // panelDetalle
            //
            this.panelDetalle.AutoScroll = true;
            this.panelDetalle.BackColor = System.Drawing.Color.White;
            this.panelDetalle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDetalle.Controls.Add(this.lblFormulario);
            this.panelDetalle.Controls.Add(this.lblSocio);
            this.panelDetalle.Controls.Add(this.socio);
            this.panelDetalle.Controls.Add(this.lblPlan);
            this.panelDetalle.Controls.Add(this.plan);
            this.panelDetalle.Controls.Add(this.lblInicio);
            this.panelDetalle.Controls.Add(this.inicio);
            this.panelDetalle.Controls.Add(this.lblEstadoMembresia);
            this.panelDetalle.Controls.Add(this.estadoMembresia);
            this.panelDetalle.Controls.Add(this.nuevo);
            this.panelDetalle.Controls.Add(this.crear);
            this.panelDetalle.Controls.Add(this.actualizar);
            this.panelDetalle.Controls.Add(this.habilitar);
            this.panelDetalle.Controls.Add(this.deshabilitar);
            this.panelDetalle.Controls.Add(this.generarCuota);
            this.panelDetalle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDetalle.Location = new System.Drawing.Point(664, 16);
            this.panelDetalle.Margin = new System.Windows.Forms.Padding(0);
            this.panelDetalle.Name = "panelDetalle";
            this.panelDetalle.Padding = new System.Windows.Forms.Padding(16);
            this.panelDetalle.Size = new System.Drawing.Size(396, 332);
            this.panelDetalle.TabIndex = 1;
            //
            // lblFormulario
            //
            this.lblFormulario.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblFormulario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblFormulario.Location = new System.Drawing.Point(16, 16);
            this.lblFormulario.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.lblFormulario.Name = "lblFormulario";
            this.lblFormulario.Size = new System.Drawing.Size(362, 32);
            this.lblFormulario.TabIndex = 0;
            this.lblFormulario.Text = "Nueva membresia";
            this.lblFormulario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblSocio
            //
            this.lblSocio.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblSocio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblSocio.Location = new System.Drawing.Point(16, 58);
            this.lblSocio.Margin = new System.Windows.Forms.Padding(0, 0, 8, 8);
            this.lblSocio.Name = "lblSocio";
            this.lblSocio.Size = new System.Drawing.Size(116, 30);
            this.lblSocio.TabIndex = 1;
            this.lblSocio.Text = "Socio:";
            this.lblSocio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // socio
            //
            this.socio.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.socio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.socio.Location = new System.Drawing.Point(140, 64);
            this.socio.Margin = new System.Windows.Forms.Padding(0, 3, 16, 8);
            this.socio.Name = "socio";
            this.socio.Size = new System.Drawing.Size(222, 25);
            this.socio.TabIndex = 2;
            //
            // lblPlan
            //
            this.lblPlan.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblPlan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblPlan.Location = new System.Drawing.Point(16, 96);
            this.lblPlan.Margin = new System.Windows.Forms.Padding(0, 0, 8, 8);
            this.lblPlan.Name = "lblPlan";
            this.lblPlan.Size = new System.Drawing.Size(116, 30);
            this.lblPlan.TabIndex = 3;
            this.lblPlan.Text = "Plan:";
            this.lblPlan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // plan
            //
            this.plan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.plan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.plan.Location = new System.Drawing.Point(140, 102);
            this.plan.Margin = new System.Windows.Forms.Padding(0, 3, 16, 8);
            this.plan.Name = "plan";
            this.plan.Size = new System.Drawing.Size(222, 25);
            this.plan.TabIndex = 4;
            //
            // lblInicio
            //
            this.lblInicio.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblInicio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblInicio.Location = new System.Drawing.Point(16, 134);
            this.lblInicio.Margin = new System.Windows.Forms.Padding(0, 0, 8, 8);
            this.lblInicio.Name = "lblInicio";
            this.lblInicio.Size = new System.Drawing.Size(116, 30);
            this.lblInicio.TabIndex = 5;
            this.lblInicio.Text = "Inicio:";
            this.lblInicio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // inicio
            //
            this.inicio.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.inicio.Location = new System.Drawing.Point(140, 138);
            this.inicio.Margin = new System.Windows.Forms.Padding(0, 3, 16, 8);
            this.inicio.Name = "inicio";
            this.inicio.Size = new System.Drawing.Size(222, 24);
            this.inicio.TabIndex = 6;
            this.inicio.ValueChanged += new System.EventHandler(this.inicio_ValueChanged);
            //
            // lblEstadoMembresia
            //
            this.lblEstadoMembresia.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblEstadoMembresia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblEstadoMembresia.Location = new System.Drawing.Point(16, 172);
            this.lblEstadoMembresia.Margin = new System.Windows.Forms.Padding(0, 0, 8, 8);
            this.lblEstadoMembresia.Name = "lblEstadoMembresia";
            this.lblEstadoMembresia.Size = new System.Drawing.Size(116, 30);
            this.lblEstadoMembresia.TabIndex = 7;
            this.lblEstadoMembresia.Text = "Estado:";
            this.lblEstadoMembresia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // estadoMembresia
            //
            this.estadoMembresia.Location = new System.Drawing.Point(140, 172);
            this.estadoMembresia.Name = "estadoMembresia";
            this.estadoMembresia.Size = new System.Drawing.Size(222, 30);
            this.estadoMembresia.TabIndex = 8;
            this.estadoMembresia.Text = "Activa";
            this.estadoMembresia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // nuevo
            //
            this.nuevo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(120)))), ((int)(((byte)(87)))));
            this.nuevo.FlatAppearance.BorderSize = 0;
            this.nuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nuevo.ForeColor = System.Drawing.Color.White;
            this.nuevo.Location = new System.Drawing.Point(16, 226);
            this.nuevo.Margin = new System.Windows.Forms.Padding(0, 0, 8, 8);
            this.nuevo.Name = "nuevo";
            this.nuevo.Size = new System.Drawing.Size(112, 38);
            this.nuevo.TabIndex = 9;
            this.nuevo.Text = "+ Nueva";
            this.nuevo.UseVisualStyleBackColor = false;
            this.nuevo.Click += new System.EventHandler(this.nuevo_Click);
            //
            // crear
            //
            this.crear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(120)))), ((int)(((byte)(87)))));
            this.crear.FlatAppearance.BorderSize = 0;
            this.crear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.crear.ForeColor = System.Drawing.Color.White;
            this.crear.Location = new System.Drawing.Point(136, 226);
            this.crear.Margin = new System.Windows.Forms.Padding(0, 0, 8, 8);
            this.crear.Name = "crear";
            this.crear.Size = new System.Drawing.Size(112, 38);
            this.crear.TabIndex = 10;
            this.crear.Text = "Crear";
            this.crear.UseVisualStyleBackColor = false;
            this.crear.Click += new System.EventHandler(this.crear_Click);
            //
            // actualizar
            //
            this.actualizar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(66)))), ((int)(((byte)(217)))));
            this.actualizar.FlatAppearance.BorderSize = 0;
            this.actualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.actualizar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.actualizar.Location = new System.Drawing.Point(256, 226);
            this.actualizar.Margin = new System.Windows.Forms.Padding(0, 0, 8, 8);
            this.actualizar.Name = "actualizar";
            this.actualizar.Size = new System.Drawing.Size(112, 38);
            this.actualizar.TabIndex = 11;
            this.actualizar.Text = "Actualizar";
            this.actualizar.UseVisualStyleBackColor = false;
            this.actualizar.Click += new System.EventHandler(this.actualizar_Click);
            //
            // habilitar
            //
            this.habilitar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(252)))), ((int)(((byte)(231)))));
            this.habilitar.FlatAppearance.BorderSize = 0;
            this.habilitar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.habilitar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(101)))), ((int)(((byte)(52)))));
            this.habilitar.Location = new System.Drawing.Point(16, 272);
            this.habilitar.Margin = new System.Windows.Forms.Padding(0, 0, 8, 8);
            this.habilitar.Name = "habilitar";
            this.habilitar.Size = new System.Drawing.Size(112, 38);
            this.habilitar.TabIndex = 12;
            this.habilitar.Text = "Reactivar";
            this.habilitar.UseVisualStyleBackColor = false;
            this.habilitar.Click += new System.EventHandler(this.habilitar_Click);
            //
            // deshabilitar
            //
            this.deshabilitar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.deshabilitar.FlatAppearance.BorderSize = 0;
            this.deshabilitar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deshabilitar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.deshabilitar.Location = new System.Drawing.Point(136, 272);
            this.deshabilitar.Margin = new System.Windows.Forms.Padding(0, 0, 8, 8);
            this.deshabilitar.Name = "deshabilitar";
            this.deshabilitar.Size = new System.Drawing.Size(112, 38);
            this.deshabilitar.TabIndex = 13;
            this.deshabilitar.Text = "Dar de baja";
            this.deshabilitar.UseVisualStyleBackColor = false;
            this.deshabilitar.Click += new System.EventHandler(this.deshabilitar_Click);
            //
            // generarCuota
            //
            this.generarCuota.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(120)))), ((int)(((byte)(87)))));
            this.generarCuota.FlatAppearance.BorderSize = 0;
            this.generarCuota.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.generarCuota.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.generarCuota.Location = new System.Drawing.Point(256, 272);
            this.generarCuota.Margin = new System.Windows.Forms.Padding(0, 0, 8, 8);
            this.generarCuota.Name = "generarCuota";
            this.generarCuota.Size = new System.Drawing.Size(112, 38);
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
            this.ClientSize = new System.Drawing.Size(1076, 598);
            this.Controls.Add(this.panelContenido);
            this.Controls.Add(this.lblEstado);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.MinimumSize = new System.Drawing.Size(900, 560);
            this.Name = "GestionMembresiasFormulario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SysGym";
            this.Load += new System.EventHandler(this.GestionMembresiasFormulario_Load);
            this.panelContenido.ResumeLayout(false);
            this.panelListado.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).EndInit();
            this.panelDetalle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.indicadorErrores)).EndInit();
            this.ResumeLayout(false);

                }

    }
}
