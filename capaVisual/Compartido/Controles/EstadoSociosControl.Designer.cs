using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Compartido.Controles
{
    partial class EstadoSociosControl
    {
        private System.ComponentModel.IContainer components;
        private TableLayoutPanel indicadoresLayout;
        private TableLayoutPanel contenidoLayout;
        private TableLayoutPanel accionesLayout;
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
        private Button btnPago;
        private Button btnFicha;
        private ContextMenuStrip menuSocio;
        private ToolStripMenuItem verCuotasMenu;
        private ToolStripMenuItem verMembresiaMenu;
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

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.contenidoLayout = new System.Windows.Forms.TableLayoutPanel();
            this.lblTitulo = new System.Windows.Forms.Label();
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
            this.menuSocio = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.verCuotasMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.verMembresiaMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.accionesLayout = new System.Windows.Forms.TableLayoutPanel();
            this.btnFicha = new System.Windows.Forms.Button();
            this.btnPago = new System.Windows.Forms.Button();
            this.contenidoLayout.SuspendLayout();
            this.indicadoresLayout.SuspendLayout();
            this.filtrosLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.filas)).BeginInit();
            this.menuSocio.SuspendLayout();
            this.accionesLayout.SuspendLayout();
            this.SuspendLayout();
            //
            // contenidoLayout
            //
            this.contenidoLayout.ColumnCount = 1;
            this.contenidoLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.contenidoLayout.Controls.Add(this.lblTitulo, 0, 0);
            this.contenidoLayout.Controls.Add(this.indicadoresLayout, 0, 1);
            this.contenidoLayout.Controls.Add(this.filtrosLayout, 0, 2);
            this.contenidoLayout.Controls.Add(this.lblAvisos, 0, 3);
            this.contenidoLayout.Controls.Add(this.alertas, 0, 4);
            this.contenidoLayout.Controls.Add(this.filas, 0, 5);
            this.contenidoLayout.Controls.Add(this.accionesLayout, 0, 6);
            this.contenidoLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contenidoLayout.Location = new System.Drawing.Point(0, 0);
            this.contenidoLayout.Name = "contenidoLayout";
            this.contenidoLayout.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.contenidoLayout.RowCount = 7;
            this.contenidoLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.contenidoLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.contenidoLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.contenidoLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.contenidoLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.contenidoLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.contenidoLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.contenidoLayout.Size = new System.Drawing.Size(936, 420);
            this.contenidoLayout.TabIndex = 0;
            //
            // lblTitulo
            //
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblTitulo.Location = new System.Drawing.Point(15, 8);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(906, 34);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Estado de socios";
            //
            // indicadoresLayout
            //
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
            this.indicadoresLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.indicadoresLayout.Location = new System.Drawing.Point(12, 42);
            this.indicadoresLayout.Margin = new System.Windows.Forms.Padding(0);
            this.indicadoresLayout.Name = "indicadoresLayout";
            this.indicadoresLayout.RowCount = 2;
            this.indicadoresLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.indicadoresLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.indicadoresLayout.Size = new System.Drawing.Size(912, 62);
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
            this.valorActivos.Size = new System.Drawing.Size(145, 34);
            this.valorActivos.TabIndex = 0;
            this.valorActivos.Tag = "Activos";
            this.valorActivos.Text = "0";
            this.valorActivos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.valorActivos.Click += new System.EventHandler(this.tarjeta_Click);
            //
            // valorInactivos
            //
            this.valorInactivos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.valorInactivos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.valorInactivos.Font = new System.Drawing.Font("Segoe UI Semibold", 17F, System.Drawing.FontStyle.Bold);
            this.valorInactivos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.valorInactivos.Location = new System.Drawing.Point(154, 0);
            this.valorInactivos.Name = "valorInactivos";
            this.valorInactivos.Size = new System.Drawing.Size(145, 34);
            this.valorInactivos.TabIndex = 1;
            this.valorInactivos.Tag = "Inactivos";
            this.valorInactivos.Text = "0";
            this.valorInactivos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.valorInactivos.Click += new System.EventHandler(this.tarjeta_Click);
            //
            // valorAlDia
            //
            this.valorAlDia.Cursor = System.Windows.Forms.Cursors.Hand;
            this.valorAlDia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.valorAlDia.Font = new System.Drawing.Font("Segoe UI Semibold", 17F, System.Drawing.FontStyle.Bold);
            this.valorAlDia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.valorAlDia.Location = new System.Drawing.Point(305, 0);
            this.valorAlDia.Name = "valorAlDia";
            this.valorAlDia.Size = new System.Drawing.Size(145, 34);
            this.valorAlDia.TabIndex = 2;
            this.valorAlDia.Tag = "Al día";
            this.valorAlDia.Text = "0";
            this.valorAlDia.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.valorAlDia.Click += new System.EventHandler(this.tarjeta_Click);
            //
            // valorConDeuda
            //
            this.valorConDeuda.Cursor = System.Windows.Forms.Cursors.Hand;
            this.valorConDeuda.Dock = System.Windows.Forms.DockStyle.Fill;
            this.valorConDeuda.Font = new System.Drawing.Font("Segoe UI Semibold", 17F, System.Drawing.FontStyle.Bold);
            this.valorConDeuda.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.valorConDeuda.Location = new System.Drawing.Point(456, 0);
            this.valorConDeuda.Name = "valorConDeuda";
            this.valorConDeuda.Size = new System.Drawing.Size(145, 34);
            this.valorConDeuda.TabIndex = 3;
            this.valorConDeuda.Tag = "Con deuda";
            this.valorConDeuda.Text = "0";
            this.valorConDeuda.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.valorConDeuda.Click += new System.EventHandler(this.tarjeta_Click);
            //
            // valorLimiteAlcanzado
            //
            this.valorLimiteAlcanzado.Cursor = System.Windows.Forms.Cursors.Hand;
            this.valorLimiteAlcanzado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.valorLimiteAlcanzado.Font = new System.Drawing.Font("Segoe UI Semibold", 17F, System.Drawing.FontStyle.Bold);
            this.valorLimiteAlcanzado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.valorLimiteAlcanzado.Location = new System.Drawing.Point(607, 0);
            this.valorLimiteAlcanzado.Name = "valorLimiteAlcanzado";
            this.valorLimiteAlcanzado.Size = new System.Drawing.Size(145, 34);
            this.valorLimiteAlcanzado.TabIndex = 4;
            this.valorLimiteAlcanzado.Tag = "Límite alcanzado";
            this.valorLimiteAlcanzado.Text = "0";
            this.valorLimiteAlcanzado.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.valorLimiteAlcanzado.Click += new System.EventHandler(this.tarjeta_Click);
            //
            // valorPendientes
            //
            this.valorPendientes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.valorPendientes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.valorPendientes.Font = new System.Drawing.Font("Segoe UI Semibold", 17F, System.Drawing.FontStyle.Bold);
            this.valorPendientes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.valorPendientes.Location = new System.Drawing.Point(758, 0);
            this.valorPendientes.Name = "valorPendientes";
            this.valorPendientes.Size = new System.Drawing.Size(151, 34);
            this.valorPendientes.TabIndex = 5;
            this.valorPendientes.Tag = "Cuotas pendientes";
            this.valorPendientes.Text = "0";
            this.valorPendientes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.valorPendientes.Click += new System.EventHandler(this.tarjeta_Click);
            //
            // descripcionActivos
            //
            this.descripcionActivos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.descripcionActivos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.descripcionActivos.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.descripcionActivos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.descripcionActivos.Location = new System.Drawing.Point(3, 34);
            this.descripcionActivos.Name = "descripcionActivos";
            this.descripcionActivos.Size = new System.Drawing.Size(145, 28);
            this.descripcionActivos.TabIndex = 6;
            this.descripcionActivos.Tag = "Activos";
            this.descripcionActivos.Text = "Activos";
            this.descripcionActivos.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.descripcionActivos.Click += new System.EventHandler(this.tarjeta_Click);
            //
            // descripcionInactivos
            //
            this.descripcionInactivos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.descripcionInactivos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.descripcionInactivos.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.descripcionInactivos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.descripcionInactivos.Location = new System.Drawing.Point(154, 34);
            this.descripcionInactivos.Name = "descripcionInactivos";
            this.descripcionInactivos.Size = new System.Drawing.Size(145, 28);
            this.descripcionInactivos.TabIndex = 7;
            this.descripcionInactivos.Tag = "Inactivos";
            this.descripcionInactivos.Text = "Inactivos";
            this.descripcionInactivos.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.descripcionInactivos.Click += new System.EventHandler(this.tarjeta_Click);
            //
            // descripcionAlDia
            //
            this.descripcionAlDia.Cursor = System.Windows.Forms.Cursors.Hand;
            this.descripcionAlDia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.descripcionAlDia.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.descripcionAlDia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.descripcionAlDia.Location = new System.Drawing.Point(305, 34);
            this.descripcionAlDia.Name = "descripcionAlDia";
            this.descripcionAlDia.Size = new System.Drawing.Size(145, 28);
            this.descripcionAlDia.TabIndex = 8;
            this.descripcionAlDia.Tag = "Al día";
            this.descripcionAlDia.Text = "Al día";
            this.descripcionAlDia.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.descripcionAlDia.Click += new System.EventHandler(this.tarjeta_Click);
            //
            // descripcionConDeuda
            //
            this.descripcionConDeuda.Cursor = System.Windows.Forms.Cursors.Hand;
            this.descripcionConDeuda.Dock = System.Windows.Forms.DockStyle.Fill;
            this.descripcionConDeuda.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.descripcionConDeuda.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.descripcionConDeuda.Location = new System.Drawing.Point(456, 34);
            this.descripcionConDeuda.Name = "descripcionConDeuda";
            this.descripcionConDeuda.Size = new System.Drawing.Size(145, 28);
            this.descripcionConDeuda.TabIndex = 9;
            this.descripcionConDeuda.Tag = "Con deuda";
            this.descripcionConDeuda.Text = "Con deuda";
            this.descripcionConDeuda.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.descripcionConDeuda.Click += new System.EventHandler(this.tarjeta_Click);
            //
            // descripcionLimiteAlcanzado
            //
            this.descripcionLimiteAlcanzado.Cursor = System.Windows.Forms.Cursors.Hand;
            this.descripcionLimiteAlcanzado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.descripcionLimiteAlcanzado.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.descripcionLimiteAlcanzado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.descripcionLimiteAlcanzado.Location = new System.Drawing.Point(607, 34);
            this.descripcionLimiteAlcanzado.Name = "descripcionLimiteAlcanzado";
            this.descripcionLimiteAlcanzado.Size = new System.Drawing.Size(145, 28);
            this.descripcionLimiteAlcanzado.TabIndex = 10;
            this.descripcionLimiteAlcanzado.Tag = "Límite alcanzado";
            this.descripcionLimiteAlcanzado.Text = "Límite alcanzado";
            this.descripcionLimiteAlcanzado.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.descripcionLimiteAlcanzado.Click += new System.EventHandler(this.tarjeta_Click);
            //
            // descripcionPendientes
            //
            this.descripcionPendientes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.descripcionPendientes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.descripcionPendientes.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.descripcionPendientes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.descripcionPendientes.Location = new System.Drawing.Point(758, 34);
            this.descripcionPendientes.Name = "descripcionPendientes";
            this.descripcionPendientes.Size = new System.Drawing.Size(151, 28);
            this.descripcionPendientes.TabIndex = 11;
            this.descripcionPendientes.Tag = "Cuotas pendientes";
            this.descripcionPendientes.Text = "Cuotas pendientes";
            this.descripcionPendientes.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.descripcionPendientes.Click += new System.EventHandler(this.tarjeta_Click);
            //
            // filtrosLayout
            //
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
            this.filtrosLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filtrosLayout.Location = new System.Drawing.Point(12, 104);
            this.filtrosLayout.Margin = new System.Windows.Forms.Padding(0);
            this.filtrosLayout.Name = "filtrosLayout";
            this.filtrosLayout.RowCount = 1;
            this.filtrosLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.filtrosLayout.Size = new System.Drawing.Size(912, 36);
            this.filtrosLayout.TabIndex = 2;
            //
            // lblBuscar
            //
            this.lblBuscar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBuscar.Location = new System.Drawing.Point(3, 0);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(134, 36);
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
            this.buscador.Size = new System.Drawing.Size(431, 27);
            this.buscador.TabIndex = 1;
            this.buscador.TextChanged += new System.EventHandler(this.buscador_TextChanged);
            //
            // lblEstado
            //
            this.lblEstado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEstado.Location = new System.Drawing.Point(584, 0);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(52, 36);
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
            "Activos",
            "Cuotas pendientes",
            "Cuotas vencidas",
            "Cuotas por vencer",
            "Al día",
            "Con deuda",
            "1 cuota vencida",
            "Límite alcanzado",
            "Inactivos"});
            this.filtroEstado.Location = new System.Drawing.Point(639, 2);
            this.filtroEstado.Margin = new System.Windows.Forms.Padding(0, 2, 8, 2);
            this.filtroEstado.Name = "filtroEstado";
            this.filtroEstado.Size = new System.Drawing.Size(170, 28);
            this.filtroEstado.TabIndex = 3;
            this.filtroEstado.SelectedIndexChanged += new System.EventHandler(this.filtroEstado_SelectedIndexChanged);
            //
            // cantidad
            //
            this.cantidad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cantidad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.cantidad.Location = new System.Drawing.Point(820, 0);
            this.cantidad.Name = "cantidad";
            this.cantidad.Size = new System.Drawing.Size(89, 36);
            this.cantidad.TabIndex = 4;
            this.cantidad.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblAvisos
            //
            this.lblAvisos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAvisos.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblAvisos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblAvisos.Location = new System.Drawing.Point(15, 140);
            this.lblAvisos.Name = "lblAvisos";
            this.lblAvisos.Size = new System.Drawing.Size(906, 22);
            this.lblAvisos.TabIndex = 3;
            this.lblAvisos.Text = "Avisos de cuotas · todas las membresías";
            //
            // alertas
            //
            this.alertas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.alertas.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.alertas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.alertas.Location = new System.Drawing.Point(15, 162);
            this.alertas.Name = "alertas";
            this.alertas.Size = new System.Drawing.Size(906, 44);
            this.alertas.TabIndex = 4;
            this.alertas.Text = "Sin avisos pendientes";
            this.alertas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // filas
            //
            this.filas.AllowUserToAddRows = false;
            this.filas.AllowUserToDeleteRows = false;
            this.filas.AllowUserToResizeRows = false;
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
            this.filas.ContextMenuStrip = this.menuSocio;
            this.filas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filas.Location = new System.Drawing.Point(15, 209);
            this.filas.MultiSelect = false;
            this.filas.Name = "filas";
            this.filas.ReadOnly = true;
            this.filas.RowHeadersVisible = false;
            this.filas.RowHeadersWidth = 51;
            this.filas.RowTemplate.Height = 28;
            this.filas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.filas.Size = new System.Drawing.Size(906, 158);
            this.filas.TabIndex = 5;
            this.filas.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.filas_CellDoubleClick);
            this.filas.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.filas_CellMouseDown);
            this.filas.SelectionChanged += new System.EventHandler(this.filas_SelectionChanged);
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
            this.colEstadoPago.HeaderText = "Situación";
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "C2";
            this.colDeuda.DefaultCellStyle = dataGridViewCellStyle1;
            this.colDeuda.FillWeight = 85F;
            this.colDeuda.HeaderText = "Saldo pendiente";
            this.colDeuda.MinimumWidth = 6;
            this.colDeuda.Name = "colDeuda";
            this.colDeuda.ReadOnly = true;
            //
            // colUltimoPago
            //
            dataGridViewCellStyle2.Format = "dd/MM/yyyy";
            dataGridViewCellStyle2.NullValue = "—";
            this.colUltimoPago.DefaultCellStyle = dataGridViewCellStyle2;
            this.colUltimoPago.FillWeight = 90F;
            this.colUltimoPago.HeaderText = "Último pago";
            this.colUltimoPago.MinimumWidth = 6;
            this.colUltimoPago.Name = "colUltimoPago";
            this.colUltimoPago.ReadOnly = true;
            //
            // colProximoVencimiento
            //
            dataGridViewCellStyle3.Format = "dd/MM/yyyy";
            dataGridViewCellStyle3.NullValue = "—";
            this.colProximoVencimiento.DefaultCellStyle = dataGridViewCellStyle3;
            this.colProximoVencimiento.FillWeight = 160F;
            this.colProximoVencimiento.HeaderText = "Próximo vencimiento";
            this.colProximoVencimiento.MinimumWidth = 155;
            this.colProximoVencimiento.Name = "colProximoVencimiento";
            this.colProximoVencimiento.ReadOnly = true;
            //
            // menuSocio
            //
            this.menuSocio.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuSocio.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.verCuotasMenu,
            this.verMembresiaMenu});
            this.menuSocio.Name = "menuSocio";
            this.menuSocio.Size = new System.Drawing.Size(178, 52);
            //
            // verCuotasMenu
            //
            this.verCuotasMenu.Enabled = false;
            this.verCuotasMenu.Name = "verCuotasMenu";
            this.verCuotasMenu.Size = new System.Drawing.Size(177, 24);
            this.verCuotasMenu.Text = "Ver cuotas";
            this.verCuotasMenu.Click += new System.EventHandler(this.verCuotasMenu_Click);
            //
            // verMembresiaMenu
            //
            this.verMembresiaMenu.Enabled = false;
            this.verMembresiaMenu.Name = "verMembresiaMenu";
            this.verMembresiaMenu.Size = new System.Drawing.Size(177, 24);
            this.verMembresiaMenu.Text = "Ver membresía";
            this.verMembresiaMenu.Click += new System.EventHandler(this.verMembresiaMenu_Click);
            //
            // accionesLayout
            //
            this.accionesLayout.ColumnCount = 2;
            this.accionesLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.accionesLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.accionesLayout.Controls.Add(this.btnFicha, 0, 0);
            this.accionesLayout.Controls.Add(this.btnPago, 1, 0);
            this.accionesLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.accionesLayout.Location = new System.Drawing.Point(15, 373);
            this.accionesLayout.Name = "accionesLayout";
            this.accionesLayout.RowCount = 1;
            this.accionesLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.accionesLayout.Size = new System.Drawing.Size(906, 36);
            this.accionesLayout.TabIndex = 6;
            //
            // btnFicha
            //
            this.btnFicha.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(75)))), ((int)(((byte)(138)))));
            this.btnFicha.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnFicha.Enabled = false;
            this.btnFicha.FlatAppearance.BorderSize = 0;
            this.btnFicha.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(48)))), ((int)(((byte)(89)))));
            this.btnFicha.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(61)))), ((int)(((byte)(112)))));
            this.btnFicha.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFicha.ForeColor = System.Drawing.Color.White;
            this.btnFicha.Location = new System.Drawing.Point(3, 3);
            this.btnFicha.Name = "btnFicha";
            this.btnFicha.Size = new System.Drawing.Size(447, 30);
            this.btnFicha.TabIndex = 9;
            this.btnFicha.Text = "Ver ficha completa";
            this.btnFicha.UseVisualStyleBackColor = false;
            this.btnFicha.Click += new System.EventHandler(this.btnFicha_Click);
            //
            // btnPago
            //
            this.btnPago.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.btnPago.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPago.Enabled = false;
            this.btnPago.FlatAppearance.BorderSize = 0;
            this.btnPago.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(75)))), ((int)(((byte)(31)))));
            this.btnPago.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(101)))), ((int)(((byte)(41)))));
            this.btnPago.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPago.ForeColor = System.Drawing.Color.White;
            this.btnPago.Location = new System.Drawing.Point(456, 3);
            this.btnPago.Name = "btnPago";
            this.btnPago.Size = new System.Drawing.Size(447, 30);
            this.btnPago.TabIndex = 7;
            this.btnPago.Text = "Registrar pago";
            this.btnPago.UseVisualStyleBackColor = false;
            this.btnPago.Click += new System.EventHandler(this.btnPago_Click);
            //
            // EstadoSociosControl
            //
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.Controls.Add(this.contenidoLayout);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(700, 320);
            this.Name = "EstadoSociosControl";
            this.Size = new System.Drawing.Size(936, 420);
            this.contenidoLayout.ResumeLayout(false);
            this.indicadoresLayout.ResumeLayout(false);
            this.filtrosLayout.ResumeLayout(false);
            this.filtrosLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.filas)).EndInit();
            this.menuSocio.ResumeLayout(false);
            this.accionesLayout.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
