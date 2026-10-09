using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Compartido.Controles
{
    partial class EstadoSociosControl
    {
        private TableLayoutPanel indicadoresLayout;
        private TableLayoutPanel filtrosLayout;
        private Label valorActivos;
        private Label valorInactivos;
        private Label valorAlDia;
        private Label valorConDeuda;
        private Label valorLimiteAlcanzado;
        private Label valorPendientes;
        private Label descripcionActivos;
        private Label descripcionInactivos;
        private Label descripcionAlDia;
        private Label descripcionConDeuda;
        private Label descripcionLimiteAlcanzado;
        private Label descripcionPendientes;
        private Label lblTitulo;
        private Label lblAvisos;
        private Label alertas;
        private Label cantidad;
        private Label lblBuscar;
        private Label lblEstado;
        private ComboBox filtroEstado;
        private TextBox buscador;
        private DataGridView filas;
        private Button btnCuotas;
        private Button btnPago;
        private Button btnMembresia;
        private Button btnFicha;
        private DataGridViewTextBoxColumn colIdSocio;
        private DataGridViewTextBoxColumn colSocio;
        private DataGridViewTextBoxColumn colDni;
        private DataGridViewTextBoxColumn colPlan;
        private DataGridViewTextBoxColumn colMembresia;
        private DataGridViewTextBoxColumn colEstadoPago;
        private DataGridViewTextBoxColumn colVencidas;
        private DataGridViewTextBoxColumn colDeuda;
        private DataGridViewTextBoxColumn colUltimoPago;
        private DataGridViewTextBoxColumn colProximoVencimiento;

        private void InitializeComponent()
        {
            this.indicadoresLayout = new System.Windows.Forms.TableLayoutPanel();
            this.valorActivos = new System.Windows.Forms.Label();
            this.valorInactivos = new System.Windows.Forms.Label();
            this.valorAlDia = new System.Windows.Forms.Label();
            this.valorConDeuda = new System.Windows.Forms.Label();
            this.valorLimiteAlcanzado = new System.Windows.Forms.Label();
            this.valorPendientes = new System.Windows.Forms.Label();
            this.descripcionActivos = new System.Windows.Forms.Label();
            this.descripcionInactivos = new System.Windows.Forms.Label();
            this.descripcionAlDia = new System.Windows.Forms.Label();
            this.descripcionConDeuda = new System.Windows.Forms.Label();
            this.descripcionLimiteAlcanzado = new System.Windows.Forms.Label();
            this.descripcionPendientes = new System.Windows.Forms.Label();
            this.filtrosLayout = new System.Windows.Forms.TableLayoutPanel();
            this.lblBuscar = new System.Windows.Forms.Label();
            this.buscador = new System.Windows.Forms.TextBox();
            this.lblEstado = new System.Windows.Forms.Label();
            this.filtroEstado = new System.Windows.Forms.ComboBox();
            this.cantidad = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblAvisos = new System.Windows.Forms.Label();
            this.alertas = new System.Windows.Forms.Label();
            this.filas = new System.Windows.Forms.DataGridView();
            this.colIdSocio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSocio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDni = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPlan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMembresia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstadoPago = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVencidas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDeuda = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUltimoPago = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProximoVencimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnCuotas = new System.Windows.Forms.Button();
            this.btnPago = new System.Windows.Forms.Button();
            this.btnMembresia = new System.Windows.Forms.Button();
            this.btnFicha = new System.Windows.Forms.Button();
            this.indicadoresLayout.SuspendLayout();
            this.filtrosLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.filas)).BeginInit();
            this.SuspendLayout();
            //
            // indicadoresLayout
            //
            this.indicadoresLayout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.indicadoresLayout.BackColor = System.Drawing.Color.White;
            this.indicadoresLayout.ColumnCount = 6;
            this.indicadoresLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.6667F));
            this.indicadoresLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.6667F));
            this.indicadoresLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.6667F));
            this.indicadoresLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.6667F));
            this.indicadoresLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.6667F));
            this.indicadoresLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.6667F));
            this.indicadoresLayout.Controls.Add(this.valorActivos, 0, 0);
            this.indicadoresLayout.Controls.Add(this.valorInactivos, 1, 0);
            this.indicadoresLayout.Controls.Add(this.valorAlDia, 2, 0);
            this.indicadoresLayout.Controls.Add(this.valorConDeuda, 3, 0);
            this.indicadoresLayout.Controls.Add(this.valorLimiteAlcanzado, 4, 0);
            this.indicadoresLayout.Controls.Add(this.valorPendientes, 5, 0);
            this.indicadoresLayout.Controls.Add(this.descripcionActivos, 0, 1);
            this.indicadoresLayout.Controls.Add(this.descripcionInactivos, 1, 1);
            this.indicadoresLayout.Controls.Add(this.descripcionAlDia, 2, 1);
            this.indicadoresLayout.Controls.Add(this.descripcionConDeuda, 3, 1);
            this.indicadoresLayout.Controls.Add(this.descripcionLimiteAlcanzado, 4, 1);
            this.indicadoresLayout.Controls.Add(this.descripcionPendientes, 5, 1);
            this.indicadoresLayout.Location = new System.Drawing.Point(12, 39);
            this.indicadoresLayout.Margin = new System.Windows.Forms.Padding(0);
            this.indicadoresLayout.Name = "indicadoresLayout";
            this.indicadoresLayout.RowCount = 2;
            this.indicadoresLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.indicadoresLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.indicadoresLayout.Size = new System.Drawing.Size(900, 56);
            this.indicadoresLayout.TabIndex = 1;
            //
            // valorActivos
            //
            this.valorActivos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.valorActivos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.valorActivos.Font = new System.Drawing.Font("Segoe UI Semibold", 17F, System.Drawing.FontStyle.Bold);
            this.valorActivos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.valorActivos.Location = new System.Drawing.Point(3, 0);
            this.valorActivos.Name = "valorActivos";
            this.valorActivos.Size = new System.Drawing.Size(144, 34);
            this.valorActivos.TabIndex = 0;
            this.valorActivos.Tag = "Todos";
            this.valorActivos.Text = "0";
            this.valorActivos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // valorInactivos
            //
            this.valorInactivos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.valorInactivos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.valorInactivos.Font = new System.Drawing.Font("Segoe UI Semibold", 17F, System.Drawing.FontStyle.Bold);
            this.valorInactivos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.valorInactivos.Location = new System.Drawing.Point(153, 0);
            this.valorInactivos.Name = "valorInactivos";
            this.valorInactivos.Size = new System.Drawing.Size(144, 34);
            this.valorInactivos.TabIndex = 1;
            this.valorInactivos.Tag = "Inactivos";
            this.valorInactivos.Text = "0";
            this.valorInactivos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // valorAlDia
            //
            this.valorAlDia.Cursor = System.Windows.Forms.Cursors.Hand;
            this.valorAlDia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.valorAlDia.Font = new System.Drawing.Font("Segoe UI Semibold", 17F, System.Drawing.FontStyle.Bold);
            this.valorAlDia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.valorAlDia.Location = new System.Drawing.Point(303, 0);
            this.valorAlDia.Name = "valorAlDia";
            this.valorAlDia.Size = new System.Drawing.Size(144, 34);
            this.valorAlDia.TabIndex = 2;
            this.valorAlDia.Tag = "Al día";
            this.valorAlDia.Text = "0";
            this.valorAlDia.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // valorConDeuda
            //
            this.valorConDeuda.Cursor = System.Windows.Forms.Cursors.Hand;
            this.valorConDeuda.Dock = System.Windows.Forms.DockStyle.Fill;
            this.valorConDeuda.Font = new System.Drawing.Font("Segoe UI Semibold", 17F, System.Drawing.FontStyle.Bold);
            this.valorConDeuda.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.valorConDeuda.Location = new System.Drawing.Point(453, 0);
            this.valorConDeuda.Name = "valorConDeuda";
            this.valorConDeuda.Size = new System.Drawing.Size(144, 34);
            this.valorConDeuda.TabIndex = 3;
            this.valorConDeuda.Tag = "Con deuda";
            this.valorConDeuda.Text = "0";
            this.valorConDeuda.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // valorLimiteAlcanzado
            //
            this.valorLimiteAlcanzado.Cursor = System.Windows.Forms.Cursors.Hand;
            this.valorLimiteAlcanzado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.valorLimiteAlcanzado.Font = new System.Drawing.Font("Segoe UI Semibold", 17F, System.Drawing.FontStyle.Bold);
            this.valorLimiteAlcanzado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.valorLimiteAlcanzado.Location = new System.Drawing.Point(603, 0);
            this.valorLimiteAlcanzado.Name = "valorLimiteAlcanzado";
            this.valorLimiteAlcanzado.Size = new System.Drawing.Size(144, 34);
            this.valorLimiteAlcanzado.TabIndex = 4;
            this.valorLimiteAlcanzado.Tag = "Límite alcanzado";
            this.valorLimiteAlcanzado.Text = "0";
            this.valorLimiteAlcanzado.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // valorPendientes
            //
            this.valorPendientes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.valorPendientes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.valorPendientes.Font = new System.Drawing.Font("Segoe UI Semibold", 17F, System.Drawing.FontStyle.Bold);
            this.valorPendientes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.valorPendientes.Location = new System.Drawing.Point(753, 0);
            this.valorPendientes.Name = "valorPendientes";
            this.valorPendientes.Size = new System.Drawing.Size(144, 34);
            this.valorPendientes.TabIndex = 5;
            this.valorPendientes.Tag = "Todos";
            this.valorPendientes.Text = "0";
            this.valorPendientes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // descripcionActivos
            //
            this.descripcionActivos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.descripcionActivos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.descripcionActivos.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.descripcionActivos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.descripcionActivos.Location = new System.Drawing.Point(3, 34);
            this.descripcionActivos.Name = "descripcionActivos";
            this.descripcionActivos.Size = new System.Drawing.Size(144, 22);
            this.descripcionActivos.TabIndex = 6;
            this.descripcionActivos.Tag = "Todos";
            this.descripcionActivos.Text = "Activos";
            this.descripcionActivos.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            //
            // descripcionInactivos
            //
            this.descripcionInactivos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.descripcionInactivos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.descripcionInactivos.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.descripcionInactivos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.descripcionInactivos.Location = new System.Drawing.Point(153, 34);
            this.descripcionInactivos.Name = "descripcionInactivos";
            this.descripcionInactivos.Size = new System.Drawing.Size(144, 22);
            this.descripcionInactivos.TabIndex = 7;
            this.descripcionInactivos.Tag = "Inactivos";
            this.descripcionInactivos.Text = "Inactivos";
            this.descripcionInactivos.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            //
            // descripcionAlDia
            //
            this.descripcionAlDia.Cursor = System.Windows.Forms.Cursors.Hand;
            this.descripcionAlDia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.descripcionAlDia.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.descripcionAlDia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.descripcionAlDia.Location = new System.Drawing.Point(303, 34);
            this.descripcionAlDia.Name = "descripcionAlDia";
            this.descripcionAlDia.Size = new System.Drawing.Size(144, 22);
            this.descripcionAlDia.TabIndex = 8;
            this.descripcionAlDia.Tag = "Al día";
            this.descripcionAlDia.Text = "Al día";
            this.descripcionAlDia.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            //
            // descripcionConDeuda
            //
            this.descripcionConDeuda.Cursor = System.Windows.Forms.Cursors.Hand;
            this.descripcionConDeuda.Dock = System.Windows.Forms.DockStyle.Fill;
            this.descripcionConDeuda.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.descripcionConDeuda.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.descripcionConDeuda.Location = new System.Drawing.Point(453, 34);
            this.descripcionConDeuda.Name = "descripcionConDeuda";
            this.descripcionConDeuda.Size = new System.Drawing.Size(144, 22);
            this.descripcionConDeuda.TabIndex = 9;
            this.descripcionConDeuda.Tag = "Con deuda";
            this.descripcionConDeuda.Text = "Con deuda";
            this.descripcionConDeuda.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            //
            // descripcionLimiteAlcanzado
            //
            this.descripcionLimiteAlcanzado.Cursor = System.Windows.Forms.Cursors.Hand;
            this.descripcionLimiteAlcanzado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.descripcionLimiteAlcanzado.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.descripcionLimiteAlcanzado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.descripcionLimiteAlcanzado.Location = new System.Drawing.Point(603, 34);
            this.descripcionLimiteAlcanzado.Name = "descripcionLimiteAlcanzado";
            this.descripcionLimiteAlcanzado.Size = new System.Drawing.Size(144, 22);
            this.descripcionLimiteAlcanzado.TabIndex = 10;
            this.descripcionLimiteAlcanzado.Tag = "Límite alcanzado";
            this.descripcionLimiteAlcanzado.Text = "Límite alcanzado";
            this.descripcionLimiteAlcanzado.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            //
            // descripcionPendientes
            //
            this.descripcionPendientes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.descripcionPendientes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.descripcionPendientes.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.descripcionPendientes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.descripcionPendientes.Location = new System.Drawing.Point(753, 34);
            this.descripcionPendientes.Name = "descripcionPendientes";
            this.descripcionPendientes.Size = new System.Drawing.Size(144, 22);
            this.descripcionPendientes.TabIndex = 11;
            this.descripcionPendientes.Tag = "Todos";
            this.descripcionPendientes.Text = "Cuotas pendientes";
            this.descripcionPendientes.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            //
            // filtrosLayout
            //
            this.filtrosLayout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filtrosLayout.ColumnCount = 5;
            this.filtrosLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.filtrosLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.filtrosLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 58F));
            this.filtrosLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 178F));
            this.filtrosLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 95F));
            this.filtrosLayout.Controls.Add(this.lblBuscar, 0, 0);
            this.filtrosLayout.Controls.Add(this.buscador, 1, 0);
            this.filtrosLayout.Controls.Add(this.lblEstado, 2, 0);
            this.filtrosLayout.Controls.Add(this.filtroEstado, 3, 0);
            this.filtrosLayout.Controls.Add(this.cantidad, 4, 0);
            this.filtrosLayout.Location = new System.Drawing.Point(12, 106);
            this.filtrosLayout.Margin = new System.Windows.Forms.Padding(0);
            this.filtrosLayout.Name = "filtrosLayout";
            this.filtrosLayout.RowCount = 1;
            this.filtrosLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.filtrosLayout.Size = new System.Drawing.Size(900, 28);
            this.filtrosLayout.TabIndex = 2;
            //
            // lblBuscar
            //
            this.lblBuscar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBuscar.Location = new System.Drawing.Point(3, 0);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(134, 28);
            this.lblBuscar.TabIndex = 0;
            this.lblBuscar.Text = "Buscar socio o DNI";
            this.lblBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // buscador
            //
            this.buscador.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buscador.Location = new System.Drawing.Point(140, 3);
            this.buscador.Margin = new System.Windows.Forms.Padding(0, 3, 10, 3);
            this.buscador.Name = "buscador";
            this.buscador.Size = new System.Drawing.Size(419, 27);
            this.buscador.TabIndex = 1;
            //
            // lblEstado
            //
            this.lblEstado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEstado.Location = new System.Drawing.Point(572, 0);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(52, 28);
            this.lblEstado.TabIndex = 2;
            this.lblEstado.Text = "Estado:";
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // filtroEstado
            //
            this.filtroEstado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filtroEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filtroEstado.Items.AddRange(new object[] {
            "Todos",
            "Al día",
            "Con deuda",
            "1 cuota vencida",
            "Límite alcanzado",
            "Inactivos"});
            this.filtroEstado.Location = new System.Drawing.Point(627, 2);
            this.filtroEstado.Margin = new System.Windows.Forms.Padding(0, 2, 8, 2);
            this.filtroEstado.Name = "filtroEstado";
            this.filtroEstado.Size = new System.Drawing.Size(170, 28);
            this.filtroEstado.TabIndex = 3;
            //
            // cantidad
            //
            this.cantidad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cantidad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.cantidad.Location = new System.Drawing.Point(808, 0);
            this.cantidad.Name = "cantidad";
            this.cantidad.Size = new System.Drawing.Size(89, 28);
            this.cantidad.TabIndex = 4;
            this.cantidad.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblTitulo
            //
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblTitulo.Location = new System.Drawing.Point(12, 8);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(175, 30);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Estado de socios";
            //
            // lblAvisos
            //
            this.lblAvisos.AutoSize = true;
            this.lblAvisos.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblAvisos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblAvisos.Location = new System.Drawing.Point(12, 140);
            this.lblAvisos.Name = "lblAvisos";
            this.lblAvisos.Size = new System.Drawing.Size(52, 20);
            this.lblAvisos.TabIndex = 3;
            this.lblAvisos.Text = "Avisos";
            //
            // alertas
            //
            this.alertas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.alertas.AutoEllipsis = true;
            this.alertas.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.alertas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.alertas.Location = new System.Drawing.Point(12, 157);
            this.alertas.Name = "alertas";
            this.alertas.Size = new System.Drawing.Size(900, 23);
            this.alertas.TabIndex = 4;
            this.alertas.Text = "Sin avisos pendientes";
            this.alertas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // filas
            //
            this.filas.AllowUserToAddRows = false;
            this.filas.AllowUserToDeleteRows = false;
            this.filas.AllowUserToResizeRows = false;
            this.filas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.filas.BackgroundColor = System.Drawing.Color.White;
            this.filas.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.filas.ColumnHeadersHeight = 34;
            this.filas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colIdSocio,
            this.colSocio,
            this.colDni,
            this.colPlan,
            this.colMembresia,
            this.colEstadoPago,
            this.colVencidas,
            this.colDeuda,
            this.colUltimoPago,
            this.colProximoVencimiento});
            this.filas.Location = new System.Drawing.Point(12, 184);
            this.filas.MultiSelect = false;
            this.filas.Name = "filas";
            this.filas.ReadOnly = true;
            this.filas.RowHeadersVisible = false;
            this.filas.RowHeadersWidth = 51;
            this.filas.RowTemplate.Height = 28;
            this.filas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.filas.Size = new System.Drawing.Size(900, 169);
            this.filas.TabIndex = 5;
            //
            // colIdSocio
            //
            this.colIdSocio.MinimumWidth = 6;
            this.colIdSocio.Name = "colIdSocio";
            this.colIdSocio.ReadOnly = true;
            this.colIdSocio.Visible = false;
            //
            // colSocio
            //
            this.colSocio.FillWeight = 130F;
            this.colSocio.HeaderText = "Socio";
            this.colSocio.MinimumWidth = 105;
            this.colSocio.Name = "colSocio";
            this.colSocio.ReadOnly = true;
            //
            // colDni
            //
            this.colDni.FillWeight = 75F;
            this.colDni.HeaderText = "DNI";
            this.colDni.MinimumWidth = 6;
            this.colDni.Name = "colDni";
            this.colDni.ReadOnly = true;
            //
            // colPlan
            //
            this.colPlan.FillWeight = 85F;
            this.colPlan.HeaderText = "Plan";
            this.colPlan.MinimumWidth = 6;
            this.colPlan.Name = "colPlan";
            this.colPlan.ReadOnly = true;
            //
            // colMembresia
            //
            this.colMembresia.FillWeight = 90F;
            this.colMembresia.HeaderText = "Membresía";
            this.colMembresia.MinimumWidth = 6;
            this.colMembresia.Name = "colMembresia";
            this.colMembresia.ReadOnly = true;
            //
            // colEstadoPago
            //
            this.colEstadoPago.FillWeight = 110F;
            this.colEstadoPago.HeaderText = "Estado pago";
            this.colEstadoPago.MinimumWidth = 95;
            this.colEstadoPago.Name = "colEstadoPago";
            this.colEstadoPago.ReadOnly = true;
            //
            // colVencidas
            //
            this.colVencidas.FillWeight = 65F;
            this.colVencidas.HeaderText = "Vencidas";
            this.colVencidas.MinimumWidth = 6;
            this.colVencidas.Name = "colVencidas";
            this.colVencidas.ReadOnly = true;
            //
            // colDeuda
            //
            this.colDeuda.FillWeight = 85F;
            this.colDeuda.HeaderText = "Deuda total";
            this.colDeuda.MinimumWidth = 6;
            this.colDeuda.Name = "colDeuda";
            this.colDeuda.ReadOnly = true;
            //
            // colUltimoPago
            //
            this.colUltimoPago.FillWeight = 90F;
            this.colUltimoPago.HeaderText = "Último pago";
            this.colUltimoPago.MinimumWidth = 6;
            this.colUltimoPago.Name = "colUltimoPago";
            this.colUltimoPago.ReadOnly = true;
            //
            // colProximoVencimiento
            //
            this.colProximoVencimiento.FillWeight = 160F;
            this.colProximoVencimiento.HeaderText = "Próximo vencimiento";
            this.colProximoVencimiento.MinimumWidth = 155;
            this.colProximoVencimiento.Name = "colProximoVencimiento";
            this.colProximoVencimiento.ReadOnly = true;
            //
            // btnCuotas
            //
            this.btnCuotas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCuotas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(75)))), ((int)(((byte)(138)))));
            this.btnCuotas.Enabled = false;
            this.btnCuotas.FlatAppearance.BorderSize = 0;
            this.btnCuotas.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(48)))), ((int)(((byte)(89)))));
            this.btnCuotas.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(61)))), ((int)(((byte)(112)))));
            this.btnCuotas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCuotas.ForeColor = System.Drawing.Color.White;
            this.btnCuotas.Location = new System.Drawing.Point(12, 360);
            this.btnCuotas.Name = "btnCuotas";
            this.btnCuotas.Size = new System.Drawing.Size(130, 30);
            this.btnCuotas.TabIndex = 6;
            this.btnCuotas.Text = "Ver cuotas";
            this.btnCuotas.UseVisualStyleBackColor = false;
            //
            // btnPago
            //
            this.btnPago.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPago.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.btnPago.Enabled = false;
            this.btnPago.FlatAppearance.BorderSize = 0;
            this.btnPago.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(75)))), ((int)(((byte)(31)))));
            this.btnPago.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(101)))), ((int)(((byte)(41)))));
            this.btnPago.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPago.ForeColor = System.Drawing.Color.White;
            this.btnPago.Location = new System.Drawing.Point(150, 360);
            this.btnPago.Name = "btnPago";
            this.btnPago.Size = new System.Drawing.Size(140, 30);
            this.btnPago.TabIndex = 7;
            this.btnPago.Text = "Registrar pago";
            this.btnPago.UseVisualStyleBackColor = false;
            //
            // btnMembresia
            //
            this.btnMembresia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnMembresia.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(75)))), ((int)(((byte)(138)))));
            this.btnMembresia.Enabled = false;
            this.btnMembresia.FlatAppearance.BorderSize = 0;
            this.btnMembresia.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(48)))), ((int)(((byte)(89)))));
            this.btnMembresia.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(61)))), ((int)(((byte)(112)))));
            this.btnMembresia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMembresia.ForeColor = System.Drawing.Color.White;
            this.btnMembresia.Location = new System.Drawing.Point(298, 360);
            this.btnMembresia.Name = "btnMembresia";
            this.btnMembresia.Size = new System.Drawing.Size(140, 30);
            this.btnMembresia.TabIndex = 8;
            this.btnMembresia.Text = "Ver membresía";
            this.btnMembresia.UseVisualStyleBackColor = false;
            //
            // btnFicha
            //
            this.btnFicha.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFicha.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(75)))), ((int)(((byte)(138)))));
            this.btnFicha.Enabled = false;
            this.btnFicha.FlatAppearance.BorderSize = 0;
            this.btnFicha.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(48)))), ((int)(((byte)(89)))));
            this.btnFicha.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(61)))), ((int)(((byte)(112)))));
            this.btnFicha.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFicha.ForeColor = System.Drawing.Color.White;
            this.btnFicha.Location = new System.Drawing.Point(446, 360);
            this.btnFicha.Name = "btnFicha";
            this.btnFicha.Size = new System.Drawing.Size(160, 30);
            this.btnFicha.TabIndex = 9;
            this.btnFicha.Text = "Ver ficha completa";
            this.btnFicha.UseVisualStyleBackColor = false;
            this.btnFicha.Click += new System.EventHandler(this.btnFicha_Click);
            //
            // EstadoSociosControl
            //
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.indicadoresLayout);
            this.Controls.Add(this.filtrosLayout);
            this.Controls.Add(this.lblAvisos);
            this.Controls.Add(this.alertas);
            this.Controls.Add(this.filas);
            this.Controls.Add(this.btnCuotas);
            this.Controls.Add(this.btnPago);
            this.Controls.Add(this.btnMembresia);
            this.Controls.Add(this.btnFicha);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(700, 400);
            this.Name = "EstadoSociosControl";
            this.Size = new System.Drawing.Size(936, 420);
            this.indicadoresLayout.ResumeLayout(false);
            this.filtrosLayout.ResumeLayout(false);
            this.filtrosLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.filas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
