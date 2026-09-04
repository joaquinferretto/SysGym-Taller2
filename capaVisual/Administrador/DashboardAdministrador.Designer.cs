using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Administrador
{
    partial class DashboardAdministrador
    {
        private IContainer components = null;
        private Panel panelEncabezado; private Label lblMarca; private Label lblUsuarioRol; private Button btnCambiarCuenta; private Panel panelMenu; private FlowLayoutPanel panelOpciones; private Panel panelPie; private Button btnSalir; private Panel panelContenido;
        private Label lblAdministracion; private Label lblOperacion; private Label lblRutinas; private Label lblConsultas; private Button btnUsuarios; private Button btnSocios; private Button btnPlanes; private Button btnMembresias; private Button btnPagos; private Button btnEjercicios; private Button btnRutinas; private Button btnReportes;
        private DashboardInicioAdministrador dashboardInicio;

        protected override void Dispose(bool disposing) { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }
        private void InitializeComponent()
        {
            this.panelEncabezado = new System.Windows.Forms.Panel();
            this.lblMarca = new System.Windows.Forms.Label();
            this.lblUsuarioRol = new System.Windows.Forms.Label();
            this.btnCambiarCuenta = new System.Windows.Forms.Button();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.panelOpciones = new System.Windows.Forms.FlowLayoutPanel();
            this.lblAdministracion = new System.Windows.Forms.Label();
            this.btnUsuarios = new System.Windows.Forms.Button();
            this.btnSocios = new System.Windows.Forms.Button();
            this.lblOperacion = new System.Windows.Forms.Label();
            this.btnPlanes = new System.Windows.Forms.Button();
            this.btnMembresias = new System.Windows.Forms.Button();
            this.btnPagos = new System.Windows.Forms.Button();
            this.lblRutinas = new System.Windows.Forms.Label();
            this.btnEjercicios = new System.Windows.Forms.Button();
            this.btnRutinas = new System.Windows.Forms.Button();
            this.lblConsultas = new System.Windows.Forms.Label();
            this.btnReportes = new System.Windows.Forms.Button();
            this.panelPie = new System.Windows.Forms.Panel();
            this.btnSalir = new System.Windows.Forms.Button();
            this.panelContenido = new System.Windows.Forms.Panel();
            this.dashboardInicio = new exxen2._0.capaVisual.Administrador.DashboardInicioAdministrador();
            this.panelEncabezado.SuspendLayout();
            this.panelMenu.SuspendLayout();
            this.panelOpciones.SuspendLayout();
            this.panelPie.SuspendLayout();
            this.panelContenido.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEncabezado
            // 
            this.panelEncabezado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.panelEncabezado.Controls.Add(this.lblMarca);
            this.panelEncabezado.Controls.Add(this.lblUsuarioRol);
            this.panelEncabezado.Controls.Add(this.btnCambiarCuenta);
            this.panelEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEncabezado.Location = new System.Drawing.Point(0, 0);
            this.panelEncabezado.Name = "panelEncabezado";
            this.panelEncabezado.Size = new System.Drawing.Size(1221, 76);
            this.panelEncabezado.TabIndex = 2;
            // 
            // lblMarca
            // 
            this.lblMarca.AutoSize = true;
            this.lblMarca.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblMarca.ForeColor = System.Drawing.Color.White;
            this.lblMarca.Location = new System.Drawing.Point(24, 8);
            this.lblMarca.Name = "lblMarca";
            this.lblMarca.Size = new System.Drawing.Size(124, 37);
            this.lblMarca.TabIndex = 0;
            this.lblMarca.Text = "SYSGYM";
            // 
            // lblUsuarioRol
            // 
            this.lblUsuarioRol.AutoSize = true;
            this.lblUsuarioRol.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(231)))), ((int)(((byte)(255)))));
            this.lblUsuarioRol.Location = new System.Drawing.Point(27, 48);
            this.lblUsuarioRol.Name = "lblUsuarioRol";
            this.lblUsuarioRol.Size = new System.Drawing.Size(361, 19);
            this.lblUsuarioRol.TabIndex = 1;
            this.lblUsuarioRol.Text = "Usuario: Administrador de diseno    |    Rol: Administrador";
            // 
            // btnCambiarCuenta
            // 
            this.btnCambiarCuenta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCambiarCuenta.BackColor = System.Drawing.Color.White;
            this.btnCambiarCuenta.FlatAppearance.BorderSize = 0;
            this.btnCambiarCuenta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCambiarCuenta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.btnCambiarCuenta.Location = new System.Drawing.Point(1039, 23);
            this.btnCambiarCuenta.Name = "btnCambiarCuenta";
            this.btnCambiarCuenta.Size = new System.Drawing.Size(158, 34);
            this.btnCambiarCuenta.TabIndex = 2;
            this.btnCambiarCuenta.Text = "Cambiar de cuenta";
            this.btnCambiarCuenta.UseVisualStyleBackColor = false;
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.White;
            this.panelMenu.Controls.Add(this.panelOpciones);
            this.panelMenu.Controls.Add(this.panelPie);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 76);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(260, 673);
            this.panelMenu.TabIndex = 1;
            // 
            // panelOpciones
            // 
            this.panelOpciones.AutoScroll = true;
            this.panelOpciones.BackColor = System.Drawing.Color.White;
            this.panelOpciones.Controls.Add(this.lblAdministracion);
            this.panelOpciones.Controls.Add(this.btnUsuarios);
            this.panelOpciones.Controls.Add(this.btnSocios);
            this.panelOpciones.Controls.Add(this.lblOperacion);
            this.panelOpciones.Controls.Add(this.btnPlanes);
            this.panelOpciones.Controls.Add(this.btnMembresias);
            this.panelOpciones.Controls.Add(this.btnPagos);
            this.panelOpciones.Controls.Add(this.lblRutinas);
            this.panelOpciones.Controls.Add(this.btnEjercicios);
            this.panelOpciones.Controls.Add(this.btnRutinas);
            this.panelOpciones.Controls.Add(this.lblConsultas);
            this.panelOpciones.Controls.Add(this.btnReportes);
            this.panelOpciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOpciones.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.panelOpciones.Location = new System.Drawing.Point(0, 0);
            this.panelOpciones.Name = "panelOpciones";
            this.panelOpciones.Padding = new System.Windows.Forms.Padding(14, 18, 14, 18);
            this.panelOpciones.Size = new System.Drawing.Size(260, 603);
            this.panelOpciones.TabIndex = 0;
            this.panelOpciones.WrapContents = false;
            // 
            // lblAdministracion
            // 
            this.lblAdministracion.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold);
            this.lblAdministracion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblAdministracion.Location = new System.Drawing.Point(14, 26);
            this.lblAdministracion.Margin = new System.Windows.Forms.Padding(0, 8, 0, 4);
            this.lblAdministracion.Name = "lblAdministracion";
            this.lblAdministracion.Size = new System.Drawing.Size(214, 24);
            this.lblAdministracion.TabIndex = 0;
            this.lblAdministracion.Text = "ADMINISTRACION";
            // 
            // btnUsuarios
            // 
            this.btnUsuarios.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.btnUsuarios.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnUsuarios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUsuarios.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnUsuarios.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnUsuarios.Location = new System.Drawing.Point(14, 54);
            this.btnUsuarios.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.btnUsuarios.Name = "btnUsuarios";
            this.btnUsuarios.Size = new System.Drawing.Size(214, 40);
            this.btnUsuarios.TabIndex = 1;
            this.btnUsuarios.Text = "Usuarios y roles";
            this.btnUsuarios.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUsuarios.UseVisualStyleBackColor = false;
            // 
            // btnSocios
            // 
            this.btnSocios.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.btnSocios.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnSocios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSocios.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnSocios.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnSocios.Location = new System.Drawing.Point(14, 99);
            this.btnSocios.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.btnSocios.Name = "btnSocios";
            this.btnSocios.Size = new System.Drawing.Size(214, 40);
            this.btnSocios.TabIndex = 2;
            this.btnSocios.Text = "Socios";
            this.btnSocios.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSocios.UseVisualStyleBackColor = false;
            // 
            // lblOperacion
            // 
            this.lblOperacion.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold);
            this.lblOperacion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblOperacion.Location = new System.Drawing.Point(14, 152);
            this.lblOperacion.Margin = new System.Windows.Forms.Padding(0, 8, 0, 4);
            this.lblOperacion.Name = "lblOperacion";
            this.lblOperacion.Size = new System.Drawing.Size(214, 24);
            this.lblOperacion.TabIndex = 3;
            this.lblOperacion.Text = "OPERACION";
            // 
            // btnPlanes
            // 
            this.btnPlanes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.btnPlanes.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnPlanes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlanes.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnPlanes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnPlanes.Location = new System.Drawing.Point(14, 180);
            this.btnPlanes.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.btnPlanes.Name = "btnPlanes";
            this.btnPlanes.Size = new System.Drawing.Size(214, 40);
            this.btnPlanes.TabIndex = 4;
            this.btnPlanes.Text = "Planes";
            this.btnPlanes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPlanes.UseVisualStyleBackColor = false;
            // 
            // btnMembresias
            // 
            this.btnMembresias.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.btnMembresias.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnMembresias.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMembresias.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnMembresias.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnMembresias.Location = new System.Drawing.Point(14, 225);
            this.btnMembresias.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.btnMembresias.Name = "btnMembresias";
            this.btnMembresias.Size = new System.Drawing.Size(214, 40);
            this.btnMembresias.TabIndex = 5;
            this.btnMembresias.Text = "Membresias";
            this.btnMembresias.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMembresias.UseVisualStyleBackColor = false;
            // 
            // btnPagos
            // 
            this.btnPagos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.btnPagos.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnPagos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPagos.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnPagos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnPagos.Location = new System.Drawing.Point(14, 270);
            this.btnPagos.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.btnPagos.Name = "btnPagos";
            this.btnPagos.Size = new System.Drawing.Size(214, 40);
            this.btnPagos.TabIndex = 6;
            this.btnPagos.Text = "Cuotas y pagos";
            this.btnPagos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPagos.UseVisualStyleBackColor = false;
            // 
            // lblRutinas
            // 
            this.lblRutinas.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold);
            this.lblRutinas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblRutinas.Location = new System.Drawing.Point(14, 323);
            this.lblRutinas.Margin = new System.Windows.Forms.Padding(0, 8, 0, 4);
            this.lblRutinas.Name = "lblRutinas";
            this.lblRutinas.Size = new System.Drawing.Size(214, 24);
            this.lblRutinas.TabIndex = 7;
            this.lblRutinas.Text = "RUTINAS";
            // 
            // btnEjercicios
            // 
            this.btnEjercicios.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.btnEjercicios.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnEjercicios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEjercicios.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnEjercicios.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnEjercicios.Location = new System.Drawing.Point(14, 351);
            this.btnEjercicios.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.btnEjercicios.Name = "btnEjercicios";
            this.btnEjercicios.Size = new System.Drawing.Size(214, 40);
            this.btnEjercicios.TabIndex = 8;
            this.btnEjercicios.Text = "Ejercicios";
            this.btnEjercicios.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEjercicios.UseVisualStyleBackColor = false;
            // 
            // btnRutinas
            // 
            this.btnRutinas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.btnRutinas.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnRutinas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRutinas.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnRutinas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnRutinas.Location = new System.Drawing.Point(14, 396);
            this.btnRutinas.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.btnRutinas.Name = "btnRutinas";
            this.btnRutinas.Size = new System.Drawing.Size(214, 40);
            this.btnRutinas.TabIndex = 9;
            this.btnRutinas.Text = "Catalogo de rutinas";
            this.btnRutinas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRutinas.UseVisualStyleBackColor = false;
            // 
            // lblConsultas
            // 
            this.lblConsultas.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold);
            this.lblConsultas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblConsultas.Location = new System.Drawing.Point(14, 449);
            this.lblConsultas.Margin = new System.Windows.Forms.Padding(0, 8, 0, 4);
            this.lblConsultas.Name = "lblConsultas";
            this.lblConsultas.Size = new System.Drawing.Size(214, 24);
            this.lblConsultas.TabIndex = 10;
            this.lblConsultas.Text = "CONSULTAS";
            // 
            // btnReportes
            // 
            this.btnReportes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.btnReportes.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnReportes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReportes.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnReportes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnReportes.Location = new System.Drawing.Point(14, 477);
            this.btnReportes.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.btnReportes.Name = "btnReportes";
            this.btnReportes.Size = new System.Drawing.Size(214, 40);
            this.btnReportes.TabIndex = 11;
            this.btnReportes.Text = "Reportes";
            this.btnReportes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReportes.UseVisualStyleBackColor = false;
            // 
            // panelPie
            // 
            this.panelPie.BackColor = System.Drawing.Color.White;
            this.panelPie.Controls.Add(this.btnSalir);
            this.panelPie.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelPie.Location = new System.Drawing.Point(0, 603);
            this.panelPie.Name = "panelPie";
            this.panelPie.Padding = new System.Windows.Forms.Padding(14, 10, 14, 14);
            this.panelPie.Size = new System.Drawing.Size(260, 70);
            this.panelPie.TabIndex = 1;
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.btnSalir.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSalir.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(202)))), ((int)(((byte)(202)))));
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.btnSalir.Location = new System.Drawing.Point(14, 10);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(232, 46);
            this.btnSalir.TabIndex = 0;
            this.btnSalir.Text = "Salir";
            this.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalir.UseVisualStyleBackColor = false;
            // 
            // panelContenido
            // 
            this.panelContenido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.panelContenido.Controls.Add(this.dashboardInicio);
            this.panelContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenido.Location = new System.Drawing.Point(260, 76);
            this.panelContenido.Name = "panelContenido";
            this.panelContenido.Size = new System.Drawing.Size(961, 673);
            this.panelContenido.TabIndex = 0;
            // 
            // dashboardInicio
            // 
            this.dashboardInicio.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.dashboardInicio.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dashboardInicio.Location = new System.Drawing.Point(0, 0);
            this.dashboardInicio.Name = "dashboardInicio";
            this.dashboardInicio.Size = new System.Drawing.Size(961, 675);
            this.dashboardInicio.TabIndex = 0;
            this.dashboardInicio.Load += new System.EventHandler(this.dashboardInicio_Load);
            // 
            // DashboardAdministrador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1221, 749);
            this.Controls.Add(this.panelContenido);
            this.Controls.Add(this.panelMenu);
            this.Controls.Add(this.panelEncabezado);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "DashboardAdministrador";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SysGym - Administrador";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panelEncabezado.ResumeLayout(false);
            this.panelEncabezado.PerformLayout();
            this.panelMenu.ResumeLayout(false);
            this.panelOpciones.ResumeLayout(false);
            this.panelPie.ResumeLayout(false);
            this.panelContenido.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
