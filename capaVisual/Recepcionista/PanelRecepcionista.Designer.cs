using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Recepcionista
{
    partial class PanelRecepcionista
    {
        private IContainer components = null;
        private Panel panelEncabezado;
        private Label lblMarca;
        private Label lblUsuarioRol;
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
        private Label lblBienvenida;

        protected override void Dispose(bool liberarRecursos)
        {
            if (liberarRecursos && components != null) components.Dispose();
            base.Dispose(liberarRecursos);
        }

        private void InitializeComponent()
        {
            this.panelEncabezado = new System.Windows.Forms.Panel();
            this.lblMarca = new System.Windows.Forms.Label();
            this.lblUsuarioRol = new System.Windows.Forms.Label();
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
            this.btnSalir.FlatAppearance.BorderSize = 0;
            this.panelContenido = new System.Windows.Forms.Panel();
            this.lblBienvenida = new System.Windows.Forms.Label();
            this.panelEncabezado.SuspendLayout();
            this.panelMenu.SuspendLayout();
            this.panelOpciones.SuspendLayout();
            this.panelContenido.SuspendLayout();
            this.SuspendLayout();
            //
            // panelEncabezado
            //
            this.panelEncabezado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.panelEncabezado.Controls.Add(this.lblMarca);
            this.panelEncabezado.Controls.Add(this.lblUsuarioRol);
            this.panelEncabezado.Controls.Add(this.lblModuloActual);
            this.panelEncabezado.Controls.Add(this.lblSubtituloModulo);
            this.panelEncabezado.Controls.Add(this.btnVolver);
            this.panelEncabezado.Controls.Add(this.btnCambiarCuenta);
            this.panelEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEncabezado.Location = new System.Drawing.Point(0, 0);
            this.panelEncabezado.Name = "panelEncabezado";
            this.panelEncabezado.Padding = new System.Windows.Forms.Padding(24, 8, 24, 8);
            this.panelEncabezado.Size = new System.Drawing.Size(1282, 90);
            this.panelEncabezado.TabIndex = 2;
            //
            // lblMarca
            //
            this.lblMarca.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblMarca.ForeColor = System.Drawing.Color.White;
            this.lblMarca.Location = new System.Drawing.Point(24, 8);
            this.lblMarca.Margin = new System.Windows.Forms.Padding(0);
            this.lblMarca.Name = "lblMarca";
            this.lblMarca.Size = new System.Drawing.Size(220, 38);
            this.lblMarca.TabIndex = 0;
            this.lblMarca.Text = "SYSGYM";
            this.lblMarca.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            //
            // lblUsuarioRol
            //
            this.lblUsuarioRol.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUsuarioRol.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(231)))), ((int)(((byte)(255)))));
            this.lblUsuarioRol.Location = new System.Drawing.Point(24, 60);
            this.lblUsuarioRol.Margin = new System.Windows.Forms.Padding(0);
            this.lblUsuarioRol.Name = "lblUsuarioRol";
            this.lblUsuarioRol.Size = new System.Drawing.Size(1234, 26);
            this.lblUsuarioRol.TabIndex = 1;
            this.lblUsuarioRol.Text = "Usuario: Recepcionista de diseno    |    Rol: Recepcionista";
            //
            // lblModuloActual
            //
            this.lblModuloActual.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblModuloActual.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblModuloActual.ForeColor = System.Drawing.Color.White;
            this.lblModuloActual.Location = new System.Drawing.Point(310, 8);
            this.lblModuloActual.Margin = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.lblModuloActual.Name = "lblModuloActual";
            this.lblModuloActual.Size = new System.Drawing.Size(662, 27);
            this.lblModuloActual.TabIndex = 4;
            this.lblModuloActual.Text = "Inicio";
            this.lblModuloActual.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblSubtituloModulo
            //
            this.lblSubtituloModulo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubtituloModulo.ForeColor = System.Drawing.Color.White;
            this.lblSubtituloModulo.Location = new System.Drawing.Point(310, 35);
            this.lblSubtituloModulo.Name = "lblSubtituloModulo";
            this.lblSubtituloModulo.Size = new System.Drawing.Size(662, 24);
            this.lblSubtituloModulo.TabIndex = 5;
            this.lblSubtituloModulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // btnVolver
            //
            this.btnVolver.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVolver.BackColor = System.Drawing.Color.White;
            this.btnVolver.Enabled = false;
            this.btnVolver.FlatAppearance.BorderSize = 0;
            this.btnVolver.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVolver.ForeColor = System.Drawing.Color.FromArgb(72, 66, 217);
            this.btnVolver.Location = new System.Drawing.Point(978, 12);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(100, 36);
            this.btnVolver.TabIndex = 1;
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
            this.btnCambiarCuenta.ForeColor = System.Drawing.Color.FromArgb(72, 66, 217);
            this.btnCambiarCuenta.Location = new System.Drawing.Point(1090, 12);
            this.btnCambiarCuenta.Margin = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.btnCambiarCuenta.Name = "btnCambiarCuenta";
            this.btnCambiarCuenta.Size = new System.Drawing.Size(168, 36);
            this.btnCambiarCuenta.TabIndex = 2;
            this.btnCambiarCuenta.Text = "Cambiar de cuenta";
            this.btnCambiarCuenta.UseVisualStyleBackColor = false;
            this.btnCambiarCuenta.Click += new System.EventHandler(this.btnCambiarCuenta_Click);
            //
            // panelMenu
            //
            this.panelMenu.BackColor = System.Drawing.Color.White;
            this.panelMenu.Controls.Add(this.panelOpciones);
            this.panelMenu.Controls.Add(this.btnSalir);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 90);
            this.panelMenu.MinimumSize = new System.Drawing.Size(264, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Padding = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.panelMenu.Size = new System.Drawing.Size(274, 659);
            this.panelMenu.TabIndex = 1;
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
            this.panelOpciones.Location = new System.Drawing.Point(0, 0);
            this.panelOpciones.Name = "panelOpciones";
            this.panelOpciones.Padding = new System.Windows.Forms.Padding(14, 10, 14, 10);
            this.panelOpciones.Size = new System.Drawing.Size(274, 601);
            this.panelOpciones.TabIndex = 0;
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
            // btnSalir
            //
            this.btnSalir.BackColor = System.Drawing.Color.FromArgb(255, 240, 240);
            this.btnSalir.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSalir.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(202)))), ((int)(((byte)(202)))));
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.ForeColor = System.Drawing.Color.FromArgb(173, 36, 36);
            this.btnSalir.Location = new System.Drawing.Point(0, 601);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnSalir.Size = new System.Drawing.Size(274, 46);
            this.btnSalir.TabIndex = 1;
            this.btnSalir.Text = "Salir";
            this.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            //
            // panelContenido
            //
            this.panelContenido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.panelContenido.Controls.Add(this.lblBienvenida);
            this.panelContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenido.Location = new System.Drawing.Point(274, 90);
            this.panelContenido.Name = "panelContenido";
            this.panelContenido.Size = new System.Drawing.Size(1008, 659);
            this.panelContenido.TabIndex = 2;
            //
            // lblBienvenida
            //
            this.lblBienvenida.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBienvenida.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblBienvenida.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblBienvenida.Location = new System.Drawing.Point(0, 0);
            this.lblBienvenida.Name = "lblBienvenida";
            this.lblBienvenida.Size = new System.Drawing.Size(1008, 659);
            this.lblBienvenida.TabIndex = 0;
            this.lblBienvenida.Text = "Panel de recepcion\r\n\r\nElegi una opcion del menu lateral para comenzar a trabajar." +
    "";
            this.lblBienvenida.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // PanelRecepcionista
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1282, 749);
            this.Controls.Add(this.panelContenido);
            this.Controls.Add(this.panelMenu);
            this.Controls.Add(this.panelEncabezado);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.MinimumSize = new System.Drawing.Size(1100, 700);
            this.Name = "PanelRecepcionista";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SysGym - Recepcionista";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.PanelRecepcionista_Load);
            this.panelEncabezado.ResumeLayout(false);
            this.panelMenu.ResumeLayout(false);
            this.panelOpciones.ResumeLayout(false);
            this.panelContenido.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
