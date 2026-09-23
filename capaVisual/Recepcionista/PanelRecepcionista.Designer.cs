using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Recepcionista
{
    partial class PanelRecepcionista
    {
        private IContainer components = null;
        private Panel panelEncabezado;
        private PictureBox picLogo;
        private PictureBox picUsuario;
        private Label lblUsuario;
        private Label lblRol;
        private Label lblDniTitulo;
        private Label lblDni;
        private Label lblSexoTitulo;
        private Label lblSexo;
        private Label lblModuloActual;
        private Label lblSubtituloModulo;
        private Button btnVolver;
        private Button btnCambiarCuenta;
        private Panel panelMenu;
        private Panel panelOpciones;
        private Label lblClientes;
        private Button btnSocios;
        private Button btnMembresias;
        private Label lblCaja;
        private Button btnPagos;
        private Label lblEntrenadores;
        private Button btnAsignar;
        private Button btnConsultar;
        private Button btnSalir;
        private Panel panelContenido;
        private Panel panelInicio;
        private Label lblBienvenidaTitulo;
        private Label lblBienvenida;

        protected override void Dispose(bool liberarRecursos)
        {
            if (liberarRecursos && components != null) components.Dispose();
            base.Dispose(liberarRecursos);
        }

        private void InitializeComponent()
        {
            this.panelEncabezado = new System.Windows.Forms.Panel();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.picUsuario = new System.Windows.Forms.PictureBox();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.lblRol = new System.Windows.Forms.Label();
            this.lblDniTitulo = new System.Windows.Forms.Label();
            this.lblDni = new System.Windows.Forms.Label();
            this.lblSexoTitulo = new System.Windows.Forms.Label();
            this.lblSexo = new System.Windows.Forms.Label();
            this.lblModuloActual = new System.Windows.Forms.Label();
            this.lblSubtituloModulo = new System.Windows.Forms.Label();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnCambiarCuenta = new System.Windows.Forms.Button();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.panelOpciones = new System.Windows.Forms.Panel();
            this.lblClientes = new System.Windows.Forms.Label();
            this.btnSocios = new System.Windows.Forms.Button();
            this.btnMembresias = new System.Windows.Forms.Button();
            this.lblCaja = new System.Windows.Forms.Label();
            this.btnPagos = new System.Windows.Forms.Button();
            this.lblEntrenadores = new System.Windows.Forms.Label();
            this.btnAsignar = new System.Windows.Forms.Button();
            this.btnConsultar = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.panelContenido = new System.Windows.Forms.Panel();
            this.panelInicio = new System.Windows.Forms.Panel();
            this.lblBienvenidaTitulo = new System.Windows.Forms.Label();
            this.lblBienvenida = new System.Windows.Forms.Label();
            this.panelEncabezado.SuspendLayout();
            this.panelMenu.SuspendLayout();
            this.panelOpciones.SuspendLayout();
            this.panelContenido.SuspendLayout();
            this.panelInicio.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUsuario)).BeginInit();
            this.SuspendLayout();
            //
            // panelMenu
            //
            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.panelMenu.Controls.Add(this.panelOpciones);
            this.panelMenu.Controls.Add(this.picLogo);
            this.panelMenu.Controls.Add(this.btnSalir);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.MinimumSize = new System.Drawing.Size(264, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.panelMenu.Size = new System.Drawing.Size(274, 749);
            this.panelMenu.TabIndex = 0;
            //
            // panelOpciones
            //
            this.panelOpciones.AutoScroll = true;
            this.panelOpciones.BackColor = System.Drawing.Color.White;
            this.panelOpciones.Controls.Add(this.lblClientes);
            this.panelOpciones.Controls.Add(this.btnSocios);
            this.panelOpciones.Controls.Add(this.btnMembresias);
            this.panelOpciones.Controls.Add(this.lblCaja);
            this.panelOpciones.Controls.Add(this.btnPagos);
            this.panelOpciones.Controls.Add(this.lblEntrenadores);
            this.panelOpciones.Controls.Add(this.btnAsignar);
            this.panelOpciones.Controls.Add(this.btnConsultar);
            this.panelOpciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOpciones.Location = new System.Drawing.Point(0, 96);
            this.panelOpciones.Name = "panelOpciones";
            this.panelOpciones.Padding = new System.Windows.Forms.Padding(14, 10, 14, 10);
            this.panelOpciones.Size = new System.Drawing.Size(273, 607);
            this.panelOpciones.TabIndex = 1;
            //
            // lblClientes
            //
            this.lblClientes.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold);
            this.lblClientes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblClientes.Location = new System.Drawing.Point(14, 18);
            this.lblClientes.Margin = new System.Windows.Forms.Padding(0, 8, 0, 2);
            this.lblClientes.Name = "lblClientes";
            this.lblClientes.Size = new System.Drawing.Size(236, 22);
            this.lblClientes.TabIndex = 0;
            this.lblClientes.Tag = "CLIENTES";
            this.lblClientes.Text = "▼ CLIENTES";
            this.lblClientes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // btnSocios
            //
            this.btnSocios.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.btnSocios.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnSocios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSocios.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnSocios.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnSocios.Location = new System.Drawing.Point(14, 42);
            this.btnSocios.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.btnSocios.Name = "btnSocios";
            this.btnSocios.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnSocios.Size = new System.Drawing.Size(236, 38);
            this.btnSocios.TabIndex = 1;
            this.btnSocios.Text = "Socios";
            this.btnSocios.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSocios.UseVisualStyleBackColor = false;
            this.btnSocios.Click += new System.EventHandler(this.btnSocios_Click);
            //
            // btnMembresias
            //
            this.btnMembresias.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.btnMembresias.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnMembresias.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMembresias.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnMembresias.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnMembresias.Location = new System.Drawing.Point(14, 85);
            this.btnMembresias.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.btnMembresias.Name = "btnMembresias";
            this.btnMembresias.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnMembresias.Size = new System.Drawing.Size(236, 38);
            this.btnMembresias.TabIndex = 2;
            this.btnMembresias.Text = "Membresías";
            this.btnMembresias.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMembresias.UseVisualStyleBackColor = false;
            this.btnMembresias.Click += new System.EventHandler(this.btnMembresias_Click);
            //
            // lblCaja
            //
            this.lblCaja.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold);
            this.lblCaja.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblCaja.Location = new System.Drawing.Point(14, 136);
            this.lblCaja.Margin = new System.Windows.Forms.Padding(0, 8, 0, 2);
            this.lblCaja.Name = "lblCaja";
            this.lblCaja.Size = new System.Drawing.Size(236, 22);
            this.lblCaja.TabIndex = 3;
            this.lblCaja.Tag = "CAJA";
            this.lblCaja.Text = "▶ CAJA";
            this.lblCaja.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // btnPagos
            //
            this.btnPagos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.btnPagos.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnPagos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPagos.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnPagos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnPagos.Location = new System.Drawing.Point(14, 160);
            this.btnPagos.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.btnPagos.Name = "btnPagos";
            this.btnPagos.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnPagos.Size = new System.Drawing.Size(236, 38);
            this.btnPagos.TabIndex = 4;
            this.btnPagos.Text = "Cuotas y pagos";
            this.btnPagos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPagos.UseVisualStyleBackColor = false;
            this.btnPagos.Visible = false;
            this.btnPagos.Click += new System.EventHandler(this.btnPagos_Click);
            //
            // lblEntrenadores
            //
            this.lblEntrenadores.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold);
            this.lblEntrenadores.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblEntrenadores.Location = new System.Drawing.Point(14, 211);
            this.lblEntrenadores.Margin = new System.Windows.Forms.Padding(0, 8, 0, 2);
            this.lblEntrenadores.Name = "lblEntrenadores";
            this.lblEntrenadores.Size = new System.Drawing.Size(236, 22);
            this.lblEntrenadores.TabIndex = 5;
            this.lblEntrenadores.Tag = "ENTRENADORES";
            this.lblEntrenadores.Text = "▶ ENTRENADORES";
            this.lblEntrenadores.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // btnAsignar
            //
            this.btnAsignar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.btnAsignar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnAsignar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAsignar.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnAsignar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnAsignar.Location = new System.Drawing.Point(14, 235);
            this.btnAsignar.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.btnAsignar.Name = "btnAsignar";
            this.btnAsignar.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnAsignar.Size = new System.Drawing.Size(236, 38);
            this.btnAsignar.TabIndex = 6;
            this.btnAsignar.Text = "Asignar entrenador";
            this.btnAsignar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAsignar.UseVisualStyleBackColor = false;
            this.btnAsignar.Visible = false;
            this.btnAsignar.Click += new System.EventHandler(this.btnAsignar_Click);
            //
            // btnConsultar
            //
            this.btnConsultar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.btnConsultar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnConsultar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConsultar.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnConsultar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnConsultar.Location = new System.Drawing.Point(14, 278);
            this.btnConsultar.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnConsultar.Size = new System.Drawing.Size(236, 38);
            this.btnConsultar.TabIndex = 7;
            this.btnConsultar.Text = "Consultar entrenador";
            this.btnConsultar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConsultar.UseVisualStyleBackColor = false;
            this.btnConsultar.Visible = false;
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            //
            // picLogo
            //
            this.picLogo.BackColor = System.Drawing.Color.White;
            this.picLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.picLogo.Image = global::exxen2._0.Properties.Resources.SysGymLogo;
            this.picLogo.Location = new System.Drawing.Point(0, 0);
            this.picLogo.Name = "picLogo";
            this.picLogo.Padding = new System.Windows.Forms.Padding(18, 12, 18, 12);
            this.picLogo.Size = new System.Drawing.Size(273, 96);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 0;
            this.picLogo.TabStop = false;
            //
            // btnSalir
            //
            this.btnSalir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnSalir.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSalir.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(202)))), ((int)(((byte)(202)))));
            this.btnSalir.FlatAppearance.BorderSize = 0;
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.btnSalir.Location = new System.Drawing.Point(0, 703);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnSalir.Size = new System.Drawing.Size(273, 46);
            this.btnSalir.TabIndex = 2;
            this.btnSalir.Text = "Salir";
            this.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            //
            // panelEncabezado
            //
            this.panelEncabezado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.panelEncabezado.Controls.Add(this.picUsuario);
            this.panelEncabezado.Controls.Add(this.lblUsuario);
            this.panelEncabezado.Controls.Add(this.lblRol);
            this.panelEncabezado.Controls.Add(this.lblDniTitulo);
            this.panelEncabezado.Controls.Add(this.lblDni);
            this.panelEncabezado.Controls.Add(this.lblSexoTitulo);
            this.panelEncabezado.Controls.Add(this.lblSexo);
            this.panelEncabezado.Controls.Add(this.lblModuloActual);
            this.panelEncabezado.Controls.Add(this.lblSubtituloModulo);
            this.panelEncabezado.Controls.Add(this.btnVolver);
            this.panelEncabezado.Controls.Add(this.btnCambiarCuenta);
            this.panelEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEncabezado.Location = new System.Drawing.Point(274, 0);
            this.panelEncabezado.Name = "panelEncabezado";
            this.panelEncabezado.Size = new System.Drawing.Size(1008, 96);
            this.panelEncabezado.TabIndex = 1;
            //
            // picUsuario
            //
            this.picUsuario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(231)))), ((int)(((byte)(255)))));
            this.picUsuario.Location = new System.Drawing.Point(16, 16);
            this.picUsuario.Name = "picUsuario";
            this.picUsuario.Size = new System.Drawing.Size(64, 64);
            this.picUsuario.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picUsuario.TabIndex = 0;
            this.picUsuario.TabStop = false;
            //
            // lblUsuario
            //
            this.lblUsuario.AutoEllipsis = true;
            this.lblUsuario.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblUsuario.ForeColor = System.Drawing.Color.White;
            this.lblUsuario.Location = new System.Drawing.Point(92, 18);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(200, 26);
            this.lblUsuario.TabIndex = 1;
            this.lblUsuario.Text = "Recepcionista de diseno";
            this.lblUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblRol
            //
            this.lblRol.AutoEllipsis = true;
            this.lblRol.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblRol.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(231)))), ((int)(((byte)(255)))));
            this.lblRol.Location = new System.Drawing.Point(92, 46);
            this.lblRol.Name = "lblRol";
            this.lblRol.Size = new System.Drawing.Size(200, 22);
            this.lblRol.TabIndex = 2;
            this.lblRol.Text = "Recepcionista";
            this.lblRol.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblDniTitulo
            //
            this.lblDniTitulo.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblDniTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(210)))), ((int)(((byte)(254)))));
            this.lblDniTitulo.Location = new System.Drawing.Point(308, 20);
            this.lblDniTitulo.Name = "lblDniTitulo";
            this.lblDniTitulo.Size = new System.Drawing.Size(110, 18);
            this.lblDniTitulo.TabIndex = 3;
            this.lblDniTitulo.Text = "DNI";
            //
            // lblDni
            //
            this.lblDni.AutoEllipsis = true;
            this.lblDni.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblDni.ForeColor = System.Drawing.Color.White;
            this.lblDni.Location = new System.Drawing.Point(308, 40);
            this.lblDni.Name = "lblDni";
            this.lblDni.Size = new System.Drawing.Size(110, 24);
            this.lblDni.TabIndex = 4;
            this.lblDni.Text = "-";
            //
            // lblSexoTitulo
            //
            this.lblSexoTitulo.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblSexoTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(210)))), ((int)(((byte)(254)))));
            this.lblSexoTitulo.Location = new System.Drawing.Point(428, 20);
            this.lblSexoTitulo.Name = "lblSexoTitulo";
            this.lblSexoTitulo.Size = new System.Drawing.Size(110, 18);
            this.lblSexoTitulo.TabIndex = 5;
            this.lblSexoTitulo.Text = "Sexo";
            //
            // lblSexo
            //
            this.lblSexo.AutoEllipsis = true;
            this.lblSexo.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblSexo.ForeColor = System.Drawing.Color.White;
            this.lblSexo.Location = new System.Drawing.Point(428, 40);
            this.lblSexo.Name = "lblSexo";
            this.lblSexo.Size = new System.Drawing.Size(110, 24);
            this.lblSexo.TabIndex = 6;
            this.lblSexo.Text = "-";
            //
            // lblModuloActual
            //
            this.lblModuloActual.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblModuloActual.AutoEllipsis = true;
            this.lblModuloActual.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.lblModuloActual.ForeColor = System.Drawing.Color.White;
            this.lblModuloActual.Location = new System.Drawing.Point(556, 16);
            this.lblModuloActual.Name = "lblModuloActual";
            this.lblModuloActual.Size = new System.Drawing.Size(252, 32);
            this.lblModuloActual.TabIndex = 7;
            this.lblModuloActual.Text = "Inicio";
            this.lblModuloActual.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblSubtituloModulo
            //
            this.lblSubtituloModulo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubtituloModulo.AutoEllipsis = true;
            this.lblSubtituloModulo.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblSubtituloModulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(231)))), ((int)(((byte)(255)))));
            this.lblSubtituloModulo.Location = new System.Drawing.Point(556, 50);
            this.lblSubtituloModulo.Name = "lblSubtituloModulo";
            this.lblSubtituloModulo.Size = new System.Drawing.Size(252, 24);
            this.lblSubtituloModulo.TabIndex = 8;
            this.lblSubtituloModulo.Text = "Panel del recepcionista";
            this.lblSubtituloModulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // btnVolver
            //
            this.btnVolver.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVolver.BackColor = System.Drawing.Color.White;
            this.btnVolver.Enabled = false;
            this.btnVolver.FlatAppearance.BorderSize = 0;
            this.btnVolver.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVolver.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(66)))), ((int)(((byte)(217)))));
            this.btnVolver.Location = new System.Drawing.Point(824, 10);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(168, 36);
            this.btnVolver.TabIndex = 9;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = false;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            //
            // btnCambiarCuenta
            //
            this.btnCambiarCuenta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCambiarCuenta.BackColor = System.Drawing.Color.White;
            this.btnCambiarCuenta.FlatAppearance.BorderSize = 0;
            this.btnCambiarCuenta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCambiarCuenta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(66)))), ((int)(((byte)(217)))));
            this.btnCambiarCuenta.Location = new System.Drawing.Point(824, 52);
            this.btnCambiarCuenta.Name = "btnCambiarCuenta";
            this.btnCambiarCuenta.Size = new System.Drawing.Size(168, 36);
            this.btnCambiarCuenta.TabIndex = 10;
            this.btnCambiarCuenta.Text = "Cambiar de cuenta";
            this.btnCambiarCuenta.UseVisualStyleBackColor = false;
            this.btnCambiarCuenta.Click += new System.EventHandler(this.btnCambiarCuenta_Click);
            //
            // panelContenido
            //
            this.panelContenido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.panelContenido.Controls.Add(this.panelInicio);
            this.panelContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenido.Location = new System.Drawing.Point(274, 96);
            this.panelContenido.Name = "panelContenido";
            this.panelContenido.Size = new System.Drawing.Size(1008, 653);
            this.panelContenido.TabIndex = 2;
            //
            // panelInicio
            //
            this.panelInicio.Controls.Add(this.lblBienvenidaTitulo);
            this.panelInicio.Controls.Add(this.lblBienvenida);
            this.panelInicio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelInicio.Location = new System.Drawing.Point(0, 0);
            this.panelInicio.Name = "panelInicio";
            this.panelInicio.Size = new System.Drawing.Size(1008, 653);
            this.panelInicio.TabIndex = 0;
            //
            // lblBienvenidaTitulo
            //
            this.lblBienvenidaTitulo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblBienvenidaTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);
            this.lblBienvenidaTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblBienvenidaTitulo.Location = new System.Drawing.Point(204, 268);
            this.lblBienvenidaTitulo.Name = "lblBienvenidaTitulo";
            this.lblBienvenidaTitulo.Size = new System.Drawing.Size(600, 42);
            this.lblBienvenidaTitulo.TabIndex = 0;
            this.lblBienvenidaTitulo.Text = "Panel de recepción";
            this.lblBienvenidaTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblBienvenida
            //
            this.lblBienvenida.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblBienvenida.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblBienvenida.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblBienvenida.Location = new System.Drawing.Point(204, 314);
            this.lblBienvenida.Name = "lblBienvenida";
            this.lblBienvenida.Size = new System.Drawing.Size(600, 28);
            this.lblBienvenida.TabIndex = 1;
            this.lblBienvenida.Text = "Elegí una opción del menú lateral para comenzar a trabajar.";
            this.lblBienvenida.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // PanelRecepcionista
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1282, 749);
            this.Controls.Add(this.panelContenido);
            this.Controls.Add(this.panelEncabezado);
            this.Controls.Add(this.panelMenu);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.MinimumSize = new System.Drawing.Size(1100, 700);
            this.Name = "PanelRecepcionista";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SysGym";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.PanelRecepcionista_Load);
            this.panelEncabezado.ResumeLayout(false);
            this.panelMenu.ResumeLayout(false);
            this.panelOpciones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUsuario)).EndInit();
            this.panelContenido.ResumeLayout(false);
            this.panelInicio.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
