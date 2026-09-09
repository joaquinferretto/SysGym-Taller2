using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Administrador
{
    partial class PanelAdministrador
    {
        private IContainer components = null;
        private Panel panelEncabezado; private Label lblMarca; private Label lblUsuarioRol; private Button btnCambiarCuenta; private Panel panelMenu; private Panel panelOpciones; private Panel panelPie; private Button btnSalir; private Panel panelContenido;
        private Label lblAdministracion; private Label lblOperacion; private Label lblRutinas; private Label lblConsultas; private Button btnUsuarios; private Button btnSocios; private Button btnPlanes; private Button btnMembresias; private Button btnPagos; private Button btnEjercicios; private Button btnRutinas; private Button btnReportes;

        private InicioPanelAdministrador inicioPanel;

        protected override void Dispose(bool liberarRecursos) { if (liberarRecursos && components != null) components.Dispose(); base.Dispose(liberarRecursos); }
        private void InitializeComponent()
        {
            this.panelEncabezado = new System.Windows.Forms.Panel();
            this.lblMarca = new System.Windows.Forms.Label();
            this.lblUsuarioRol = new System.Windows.Forms.Label();
            this.btnCambiarCuenta = new System.Windows.Forms.Button();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.panelOpciones = new System.Windows.Forms.Panel();

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
            this.inicioPanel = new exxen2._0.capaVisual.Administrador.InicioPanelAdministrador();
            this.panelEncabezado.SuspendLayout();
            this.panelMenu.SuspendLayout();
            this.panelOpciones.SuspendLayout();

            this.panelPie.SuspendLayout();
            this.SuspendLayout();
            //
            // panelEncabezado
            //
            this.panelEncabezado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.panelEncabezado.Controls.Add(this.lblMarca);
            this.panelEncabezado.Controls.Add(this.lblUsuarioRol);
            this.panelEncabezado.Controls.Add(this.btnCambiarCuenta);

            this.panelEncabezado.Name = "panelEncabezado";

            this.panelEncabezado.TabIndex = 2;
            //
            // lblMarca
            //

            this.lblMarca.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblMarca.ForeColor = System.Drawing.Color.White;

            this.lblMarca.Name = "lblMarca";

            this.lblMarca.TabIndex = 0;
            this.lblMarca.Text = "SYSGYM";
            //
            // lblUsuarioRol
            //

            this.lblUsuarioRol.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(231)))), ((int)(((byte)(255)))));

            this.lblUsuarioRol.Name = "lblUsuarioRol";

            this.lblUsuarioRol.TabIndex = 1;
            this.lblUsuarioRol.Text = "Usuario: Administrador de diseno    |    Rol: Administrador";
            //
            // btnCambiarCuenta
            //

            this.btnCambiarCuenta.BackColor = System.Drawing.Color.White;
            this.btnCambiarCuenta.FlatAppearance.BorderSize = 0;
            this.btnCambiarCuenta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCambiarCuenta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));

            this.btnCambiarCuenta.Name = "btnCambiarCuenta";

            this.btnCambiarCuenta.TabIndex = 2;
            this.btnCambiarCuenta.Text = "Cambiar de cuenta";
            this.btnCambiarCuenta.UseVisualStyleBackColor = false;
            //
            // panelMenu
            //
            this.panelMenu.BackColor = System.Drawing.Color.White;
            this.panelMenu.Controls.Add(this.panelOpciones);
            this.panelMenu.Controls.Add(this.panelPie);

            this.panelMenu.Name = "panelMenu";

            this.panelMenu.TabIndex = 1;
            //
            // panelOpciones
            //

            this.panelOpciones.BackColor = System.Drawing.Color.White;
            panelOpciones.Controls.Add(this.lblAdministracion);
            panelOpciones.Controls.Add(this.btnUsuarios);
            panelOpciones.Controls.Add(this.btnSocios);
            panelOpciones.Controls.Add(this.lblOperacion);
            panelOpciones.Controls.Add(this.btnPlanes);
            panelOpciones.Controls.Add(this.btnMembresias);
            panelOpciones.Controls.Add(this.btnPagos);
            panelOpciones.Controls.Add(this.lblRutinas);
            panelOpciones.Controls.Add(this.btnEjercicios);
            panelOpciones.Controls.Add(this.btnRutinas);
            panelOpciones.Controls.Add(this.lblConsultas);
            panelOpciones.Controls.Add(this.btnReportes);

            this.panelOpciones.Name = "panelOpciones";
            this.panelOpciones.Padding = new System.Windows.Forms.Padding(14, 18, 14, 18);

            this.panelOpciones.TabIndex = 0;

            //
            // lblAdministracion
            //

            this.lblAdministracion.Name = "lblAdministracion";

            this.lblAdministracion.TabIndex = 0;
            this.lblAdministracion.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold);
            this.lblAdministracion.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblAdministracion.Margin = new System.Windows.Forms.Padding(0, 8, 0, 4);
            this.lblAdministracion.Text = "ADMINISTRACION";
            //
            // btnUsuarios
            //

            this.btnUsuarios.Name = "btnUsuarios";

            this.btnUsuarios.TabIndex = 1;
            this.btnUsuarios.BackColor = System.Drawing.Color.FromArgb(248, 250, 252); this.btnUsuarios.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnUsuarios.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240); this.btnUsuarios.Font = new System.Drawing.Font("Segoe UI", 9.5F); this.btnUsuarios.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85); this.btnUsuarios.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5); this.btnUsuarios.Text = "Usuarios y roles"; this.btnUsuarios.TextAlign = System.Drawing.ContentAlignment.MiddleLeft; this.btnUsuarios.UseVisualStyleBackColor = false;
            //
            // btnSocios
            //

            this.btnSocios.Name = "btnSocios";

            this.btnSocios.TabIndex = 2;
            this.btnSocios.BackColor = System.Drawing.Color.FromArgb(248, 250, 252); this.btnSocios.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnSocios.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240); this.btnSocios.Font = new System.Drawing.Font("Segoe UI", 9.5F); this.btnSocios.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85); this.btnSocios.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5); this.btnSocios.Text = "Socios"; this.btnSocios.TextAlign = System.Drawing.ContentAlignment.MiddleLeft; this.btnSocios.UseVisualStyleBackColor = false;
            //
            // lblOperacion
            //

            this.lblOperacion.Name = "lblOperacion";

            this.lblOperacion.TabIndex = 3;
            this.lblOperacion.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold); this.lblOperacion.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139); this.lblOperacion.Margin = new System.Windows.Forms.Padding(0, 8, 0, 4); this.lblOperacion.Text = "OPERACION";
            //
            // btnPlanes
            //

            this.btnPlanes.Name = "btnPlanes";

            this.btnPlanes.TabIndex = 4;
            this.btnPlanes.BackColor = System.Drawing.Color.FromArgb(248, 250, 252); this.btnPlanes.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnPlanes.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240); this.btnPlanes.Font = new System.Drawing.Font("Segoe UI", 9.5F); this.btnPlanes.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85); this.btnPlanes.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5); this.btnPlanes.Text = "Planes"; this.btnPlanes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft; this.btnPlanes.UseVisualStyleBackColor = false;
            //
            // btnMembresias
            //

            this.btnMembresias.Name = "btnMembresias";

            this.btnMembresias.TabIndex = 5;
            this.btnMembresias.BackColor = System.Drawing.Color.FromArgb(248, 250, 252); this.btnMembresias.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnMembresias.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240); this.btnMembresias.Font = new System.Drawing.Font("Segoe UI", 9.5F); this.btnMembresias.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85); this.btnMembresias.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5); this.btnMembresias.Text = "Membresias"; this.btnMembresias.TextAlign = System.Drawing.ContentAlignment.MiddleLeft; this.btnMembresias.UseVisualStyleBackColor = false;
            //
            // btnPagos
            //

            this.btnPagos.Name = "btnPagos";

            this.btnPagos.TabIndex = 6;
            this.btnPagos.BackColor = System.Drawing.Color.FromArgb(248, 250, 252); this.btnPagos.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnPagos.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240); this.btnPagos.Font = new System.Drawing.Font("Segoe UI", 9.5F); this.btnPagos.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85); this.btnPagos.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5); this.btnPagos.Text = "Cuotas y pagos"; this.btnPagos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft; this.btnPagos.UseVisualStyleBackColor = false;
            //
            // lblRutinas
            //

            this.lblRutinas.Name = "lblRutinas";

            this.lblRutinas.TabIndex = 7;
            this.lblRutinas.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold); this.lblRutinas.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139); this.lblRutinas.Margin = new System.Windows.Forms.Padding(0, 8, 0, 4); this.lblRutinas.Text = "RUTINAS";
            //
            // btnEjercicios
            //

            this.btnEjercicios.Name = "btnEjercicios";

            this.btnEjercicios.TabIndex = 8;
            this.btnEjercicios.BackColor = System.Drawing.Color.FromArgb(248, 250, 252); this.btnEjercicios.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnEjercicios.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240); this.btnEjercicios.Font = new System.Drawing.Font("Segoe UI", 9.5F); this.btnEjercicios.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85); this.btnEjercicios.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5); this.btnEjercicios.Text = "Ejercicios"; this.btnEjercicios.TextAlign = System.Drawing.ContentAlignment.MiddleLeft; this.btnEjercicios.UseVisualStyleBackColor = false;
            //
            // btnRutinas
            //

            this.btnRutinas.Name = "btnRutinas";

            this.btnRutinas.TabIndex = 9;
            this.btnRutinas.BackColor = System.Drawing.Color.FromArgb(248, 250, 252); this.btnRutinas.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnRutinas.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240); this.btnRutinas.Font = new System.Drawing.Font("Segoe UI", 9.5F); this.btnRutinas.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85); this.btnRutinas.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5); this.btnRutinas.Text = "Catalogo de rutinas"; this.btnRutinas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft; this.btnRutinas.UseVisualStyleBackColor = false;
            //
            // lblConsultas
            //

            this.lblConsultas.Name = "lblConsultas";

            this.lblConsultas.TabIndex = 10;
            this.lblConsultas.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold); this.lblConsultas.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139); this.lblConsultas.Margin = new System.Windows.Forms.Padding(0, 8, 0, 4); this.lblConsultas.Text = "CONSULTAS";
            //
            // btnReportes
            //

            this.btnReportes.Name = "btnReportes";

            this.btnReportes.TabIndex = 11;
            this.btnReportes.BackColor = System.Drawing.Color.FromArgb(248, 250, 252); this.btnReportes.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnReportes.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240); this.btnReportes.Font = new System.Drawing.Font("Segoe UI", 9.5F); this.btnReportes.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85); this.btnReportes.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5); this.btnReportes.Text = "Reportes"; this.btnReportes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft; this.btnReportes.UseVisualStyleBackColor = false;
            //
            // panelPie
            //
            this.panelPie.BackColor = System.Drawing.Color.White;
            this.panelPie.Controls.Add(this.btnSalir);

            this.panelPie.Name = "panelPie";
            this.panelPie.Padding = new System.Windows.Forms.Padding(14, 10, 14, 14);

            this.panelPie.TabIndex = 1;
            //
            // btnSalir
            //
            this.btnSalir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));

            this.btnSalir.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(202)))), ((int)(((byte)(202)))));
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));

            this.btnSalir.Name = "btnSalir";

            this.btnSalir.TabIndex = 0;
            this.btnSalir.Text = "Salir";
            this.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalir.UseVisualStyleBackColor = false;
            //
            // panelContenido
            //
            this.panelContenido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.panelContenido.Controls.Add(this.inicioPanel);

            this.panelContenido.Name = "panelContenido";

            this.panelContenido.TabIndex = 0;
            //
            // dashboardInicio
            //
            this.inicioPanel.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);

            this.inicioPanel.Name = "inicioPanel";

            this.inicioPanel.TabIndex = 0;
            //
            // DashboardAdministrador
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

            this.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.ClientSize = new System.Drawing.Size(1200, 760);
            this.Controls.Add(this.panelContenido);
            this.Controls.Add(this.panelMenu);
            this.Controls.Add(this.panelEncabezado);
            this.Name = "PanelAdministrador";
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.MinimumSize = new System.Drawing.Size(1100, 700);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SysGym - Administrador";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

            // Distribucion general: encabezado superior, menu lateral fijo y contenido que ocupa el resto de la ventana.

            this.panelEncabezado.Padding = new System.Windows.Forms.Padding(24, 8, 24, 8);

            this.lblMarca.Margin = new System.Windows.Forms.Padding(0);
            this.lblMarca.TextAlign = System.Drawing.ContentAlignment.BottomLeft;

            this.lblUsuarioRol.Margin = new System.Windows.Forms.Padding(0);
            this.lblUsuarioRol.TextAlign = System.Drawing.ContentAlignment.TopLeft;

            this.btnCambiarCuenta.Margin = new System.Windows.Forms.Padding(16, 0, 0, 0);

            this.panelMenu.MinimumSize = new System.Drawing.Size(264, 0);

            this.panelPie.Padding = new System.Windows.Forms.Padding(14, 12, 14, 16);

            this.btnSalir.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);

            this.panelOpciones.Padding = new System.Windows.Forms.Padding(14, 10, 14, 10);

            this.lblAdministracion.Margin = new System.Windows.Forms.Padding(0, 8, 0, 2);
            this.lblAdministracion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.lblOperacion.Margin = new System.Windows.Forms.Padding(0, 8, 0, 2);
            this.lblOperacion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.lblRutinas.Margin = new System.Windows.Forms.Padding(0, 8, 0, 2);
            this.lblRutinas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.lblConsultas.Margin = new System.Windows.Forms.Padding(0, 8, 0, 2);
            this.lblConsultas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.btnUsuarios.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.btnUsuarios.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);

            this.btnSocios.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.btnSocios.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);

            this.btnPlanes.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.btnPlanes.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);

            this.btnMembresias.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.btnMembresias.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);

            this.btnPagos.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.btnPagos.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);

            this.btnEjercicios.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.btnEjercicios.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);

            this.btnRutinas.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.btnRutinas.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);

            this.btnReportes.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.btnReportes.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);

            this.panelContenido.Padding = new System.Windows.Forms.Padding(0);

            this.AutoScroll = false;
            this.panelEncabezado.AutoSize = false;
            this.panelEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEncabezado.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.panelEncabezado.Location = new System.Drawing.Point(0, 0);
            this.panelEncabezado.Size = new System.Drawing.Size(1200, 90);
            this.panelEncabezado.AutoScroll = false;
            this.lblMarca.AutoSize = false;
            this.lblMarca.Dock = System.Windows.Forms.DockStyle.None;
            this.lblMarca.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblMarca.Location = new System.Drawing.Point(22, 8);
            this.lblMarca.Size = new System.Drawing.Size(990, 36);
            this.lblUsuarioRol.AutoSize = false;
            this.lblUsuarioRol.Dock = System.Windows.Forms.DockStyle.None;
            this.lblUsuarioRol.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblUsuarioRol.Location = new System.Drawing.Point(24, 48);
            this.lblUsuarioRol.Size = new System.Drawing.Size(990, 26);
            this.btnCambiarCuenta.AutoSize = false;
            this.btnCambiarCuenta.Dock = System.Windows.Forms.DockStyle.None;
            this.btnCambiarCuenta.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnCambiarCuenta.Location = new System.Drawing.Point(1002, 24);
            this.btnCambiarCuenta.Size = new System.Drawing.Size(176, 38);
            this.panelMenu.AutoSize = false;
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 90);
            this.panelMenu.Size = new System.Drawing.Size(264, 670);
            this.panelMenu.AutoScroll = false;
            this.panelOpciones.AutoSize = false;
            this.panelOpciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOpciones.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.panelOpciones.Location = new System.Drawing.Point(0, 0);
            this.panelOpciones.Size = new System.Drawing.Size(264, 594);
            this.panelOpciones.AutoScroll = true;
            this.panelPie.AutoSize = false;
            this.panelPie.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelPie.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.panelPie.Location = new System.Drawing.Point(0, 594);
            this.panelPie.Size = new System.Drawing.Size(264, 76);
            this.panelPie.AutoScroll = false;
            this.btnSalir.AutoSize = false;
            this.btnSalir.Dock = System.Windows.Forms.DockStyle.None;
            this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.btnSalir.Location = new System.Drawing.Point(14, 14);
            this.btnSalir.Size = new System.Drawing.Size(236, 46);
            this.panelContenido.AutoSize = false;
            this.panelContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenido.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.panelContenido.Location = new System.Drawing.Point(264, 90);
            this.panelContenido.Size = new System.Drawing.Size(936, 670);
            this.panelContenido.AutoScroll = false;
            this.lblAdministracion.AutoSize = false;
            this.lblAdministracion.Dock = System.Windows.Forms.DockStyle.None;
            this.lblAdministracion.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblAdministracion.Location = new System.Drawing.Point(14, 18);
            this.lblAdministracion.Size = new System.Drawing.Size(236, 22);
            this.lblOperacion.AutoSize = false;
            this.lblOperacion.Dock = System.Windows.Forms.DockStyle.None;
            this.lblOperacion.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblOperacion.Location = new System.Drawing.Point(14, 136);
            this.lblOperacion.Size = new System.Drawing.Size(236, 22);
            this.lblRutinas.AutoSize = false;
            this.lblRutinas.Dock = System.Windows.Forms.DockStyle.None;
            this.lblRutinas.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblRutinas.Location = new System.Drawing.Point(14, 297);
            this.lblRutinas.Size = new System.Drawing.Size(236, 22);
            this.lblConsultas.AutoSize = false;
            this.lblConsultas.Dock = System.Windows.Forms.DockStyle.None;
            this.lblConsultas.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblConsultas.Location = new System.Drawing.Point(14, 415);
            this.lblConsultas.Size = new System.Drawing.Size(236, 22);
            this.btnUsuarios.AutoSize = false;
            this.btnUsuarios.Dock = System.Windows.Forms.DockStyle.None;
            this.btnUsuarios.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.btnUsuarios.Location = new System.Drawing.Point(14, 42);
            this.btnUsuarios.Size = new System.Drawing.Size(236, 38);
            this.btnSocios.AutoSize = false;
            this.btnSocios.Dock = System.Windows.Forms.DockStyle.None;
            this.btnSocios.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.btnSocios.Location = new System.Drawing.Point(14, 85);
            this.btnSocios.Size = new System.Drawing.Size(236, 38);
            this.btnPlanes.AutoSize = false;
            this.btnPlanes.Dock = System.Windows.Forms.DockStyle.None;
            this.btnPlanes.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.btnPlanes.Location = new System.Drawing.Point(14, 160);
            this.btnPlanes.Size = new System.Drawing.Size(236, 38);
            this.btnMembresias.AutoSize = false;
            this.btnMembresias.Dock = System.Windows.Forms.DockStyle.None;
            this.btnMembresias.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.btnMembresias.Location = new System.Drawing.Point(14, 203);
            this.btnMembresias.Size = new System.Drawing.Size(236, 38);
            this.btnPagos.AutoSize = false;
            this.btnPagos.Dock = System.Windows.Forms.DockStyle.None;
            this.btnPagos.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.btnPagos.Location = new System.Drawing.Point(14, 246);
            this.btnPagos.Size = new System.Drawing.Size(236, 38);
            this.btnEjercicios.AutoSize = false;
            this.btnEjercicios.Dock = System.Windows.Forms.DockStyle.None;
            this.btnEjercicios.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.btnEjercicios.Location = new System.Drawing.Point(14, 321);
            this.btnEjercicios.Size = new System.Drawing.Size(236, 38);
            this.btnRutinas.AutoSize = false;
            this.btnRutinas.Dock = System.Windows.Forms.DockStyle.None;
            this.btnRutinas.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.btnRutinas.Location = new System.Drawing.Point(14, 364);
            this.btnRutinas.Size = new System.Drawing.Size(236, 38);
            this.btnReportes.AutoSize = false;
            this.btnReportes.Dock = System.Windows.Forms.DockStyle.None;
            this.btnReportes.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.btnReportes.Location = new System.Drawing.Point(14, 439);
            this.btnReportes.Size = new System.Drawing.Size(236, 38);
            this.inicioPanel.AutoSize = false;
            this.inicioPanel.Dock = System.Windows.Forms.DockStyle.None;
            this.inicioPanel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.inicioPanel.Location = new System.Drawing.Point(0, 0);
            this.inicioPanel.Size = new System.Drawing.Size(936, 670);
            this.inicioPanel.AutoScroll = false;
            this.panelEncabezado.ResumeLayout(false);
            this.panelEncabezado.PerformLayout();
            this.panelMenu.ResumeLayout(false);

            this.panelOpciones.ResumeLayout(false);
            this.panelPie.ResumeLayout(false);
            this.ResumeLayout(false);

            this.btnCambiarCuenta.Click += new System.EventHandler(this.btnCambiarCuenta_Click);
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            this.btnUsuarios.Click += new System.EventHandler(this.btnUsuarios_Click);
            this.btnSocios.Click += new System.EventHandler(this.btnSocios_Click);
            this.btnPlanes.Click += new System.EventHandler(this.btnPlanes_Click);
            this.btnMembresias.Click += new System.EventHandler(this.btnMembresias_Click);
            this.btnPagos.Click += new System.EventHandler(this.btnPagos_Click);
            this.btnEjercicios.Click += new System.EventHandler(this.btnEjercicios_Click);
            this.btnRutinas.Click += new System.EventHandler(this.btnRutinas_Click);
            this.btnReportes.Click += new System.EventHandler(this.btnReportes_Click);
                    this.Load += new System.EventHandler(this.PanelAdministrador_Load);
        }
    }
}
