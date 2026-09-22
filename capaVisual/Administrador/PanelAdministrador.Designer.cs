using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Administrador
{
    partial class PanelAdministrador
    {
        private IContainer components = null;
        private Panel panelEncabezado; private Label lblMarca; private Label lblUsuarioRol; private Label lblModuloActual; private Button btnCambiarCuenta; private Panel panelMenu; private Panel panelOpciones; private Button btnSalir; private Panel panelContenido;
        private Label lblAdministracion; private Label lblOperacion; private Label lblRutinas; private Label lblConsultas; private Button btnUsuarios; private Button btnSocios; private Button btnPlanes; private Button btnMembresias; private Button btnPagos; private Button btnAsignaciones; private Button btnEjercicios; private Button btnRutinas; private Button btnMisSocios; private Button btnReportes;

        private Label lblSubtituloModulo;
        private Button btnVolver;
        private InicioPanelAdministrador inicioPanel;

        protected override void Dispose(bool liberarRecursos) { if (liberarRecursos && components != null) components.Dispose(); base.Dispose(liberarRecursos); }
        private void InitializeComponent()
        {
            this.lblSubtituloModulo = new System.Windows.Forms.Label();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnVolver.UseVisualStyleBackColor = false;
            this.panelEncabezado = new System.Windows.Forms.Panel();
            this.lblUsuarioRol = new System.Windows.Forms.Label();
            this.lblMarca = new System.Windows.Forms.Label();
            this.lblModuloActual = new System.Windows.Forms.Label();
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
            this.btnAsignaciones = new System.Windows.Forms.Button();
            this.lblRutinas = new System.Windows.Forms.Label();
            this.btnEjercicios = new System.Windows.Forms.Button();
            this.btnRutinas = new System.Windows.Forms.Button();
            this.btnMisSocios = new System.Windows.Forms.Button();
            this.lblConsultas = new System.Windows.Forms.Label();
            this.btnReportes = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnSalir.FlatAppearance.BorderSize = 0;
            this.panelContenido = new System.Windows.Forms.Panel();
            this.inicioPanel = new exxen2._0.capaVisual.Administrador.InicioPanelAdministrador();
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
            // lblUsuarioRol
            //
            this.lblUsuarioRol.Dock = System.Windows.Forms.DockStyle.None;
            this.lblUsuarioRol.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(231)))), ((int)(((byte)(255)))));
            this.lblUsuarioRol.Location = new System.Drawing.Point(24, 60);
            this.lblUsuarioRol.Margin = new System.Windows.Forms.Padding(0);
            this.lblUsuarioRol.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.lblUsuarioRol.AutoSize = false;
            this.lblUsuarioRol.Name = "lblUsuarioRol";
            this.lblUsuarioRol.Size = new System.Drawing.Size(1234, 26);
            this.lblUsuarioRol.TabIndex = 1;
            this.lblUsuarioRol.Text = "Usuario: Administrador de diseno    |    Rol: Administrador";
            //
            // lblMarca
            //
            this.lblMarca.Dock = System.Windows.Forms.DockStyle.None;
            this.lblMarca.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblMarca.ForeColor = System.Drawing.Color.White;
            this.lblMarca.Location = new System.Drawing.Point(24, 8);
            this.lblMarca.Margin = new System.Windows.Forms.Padding(0);
            this.lblMarca.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblMarca.Name = "lblMarca";
            this.lblMarca.Size = new System.Drawing.Size(220, 38);
            this.lblMarca.TabIndex = 0;
            this.lblMarca.Text = "SYSGYM";
            this.lblMarca.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            //
            // lblModuloActual
            //
            this.lblModuloActual.AutoEllipsis = false;
            this.lblModuloActual.Dock = System.Windows.Forms.DockStyle.None;
            this.lblModuloActual.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblModuloActual.ForeColor = System.Drawing.Color.White;
            this.lblModuloActual.Location = new System.Drawing.Point(310, 8);
            this.lblModuloActual.Margin = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.lblModuloActual.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.lblModuloActual.Text = "Resumen general";
            this.lblModuloActual.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblModuloActual.Name = "lblModuloActual";
            this.lblModuloActual.Padding = new System.Windows.Forms.Padding(0);
            this.lblModuloActual.Size = new System.Drawing.Size(662, 27);
            this.lblModuloActual.TabIndex = 4;
            //
            // btnCambiarCuenta
            //
            this.btnCambiarCuenta.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnCambiarCuenta.BackColor = System.Drawing.Color.White;
            this.btnCambiarCuenta.FlatAppearance.BorderSize = 0;
            this.btnCambiarCuenta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCambiarCuenta.ForeColor = System.Drawing.Color.FromArgb(72, 66, 217);
            this.btnCambiarCuenta.Location = new System.Drawing.Point(1090, 12);
            this.btnCambiarCuenta.Margin = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.btnCambiarCuenta.Dock = System.Windows.Forms.DockStyle.None;
            this.btnCambiarCuenta.Name = "btnCambiarCuenta";
            this.btnCambiarCuenta.Size = new System.Drawing.Size(168, 36);
            this.btnCambiarCuenta.TabIndex = 2;
            this.btnCambiarCuenta.Text = "Cambiar de cuenta";
            this.btnCambiarCuenta.UseVisualStyleBackColor = false;
            this.btnCambiarCuenta.Click += new System.EventHandler(this.btnCambiarCuenta_Click);
            this.lblSubtituloModulo.Name = "lblSubtituloModulo";
            this.lblSubtituloModulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSubtituloModulo.Location = new System.Drawing.Point(310, 35);
            this.lblSubtituloModulo.Size = new System.Drawing.Size(662, 24);
            this.lblSubtituloModulo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.lblSubtituloModulo.ForeColor = System.Drawing.Color.White;
            this.lblSubtituloModulo.TabIndex = 5;
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Text = "Volver";
            this.btnVolver.Location = new System.Drawing.Point(978, 12);
            this.btnVolver.Size = new System.Drawing.Size(100, 36);
            this.btnVolver.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnVolver.BackColor = System.Drawing.Color.White;
            this.btnVolver.ForeColor = System.Drawing.Color.FromArgb(72, 66, 217);
            this.btnVolver.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVolver.FlatAppearance.BorderSize = 0;
            this.btnVolver.Enabled = false;
            this.btnVolver.TabIndex = 1;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            //
            // panelMenu
            //
            this.panelMenu.BackColor = System.Drawing.Color.White;
            this.panelMenu.Controls.Add(this.panelOpciones);
            this.panelMenu.Controls.Add(this.btnSalir);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 90);
            this.panelMenu.MinimumSize = new System.Drawing.Size(264, 0);
            this.panelMenu.Padding = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(274, 659);
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
            this.panelOpciones.Controls.Add(this.btnAsignaciones);
            this.panelOpciones.Controls.Add(this.lblRutinas);
            this.panelOpciones.Controls.Add(this.btnEjercicios);
            this.panelOpciones.Controls.Add(this.btnRutinas);
            this.panelOpciones.Controls.Add(this.btnMisSocios);
            this.panelOpciones.Controls.Add(this.lblConsultas);
            this.panelOpciones.Controls.Add(this.btnReportes);
            this.panelOpciones.Location = new System.Drawing.Point(0, 0);
            this.panelOpciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOpciones.Name = "panelOpciones";
            this.panelOpciones.Padding = new System.Windows.Forms.Padding(14, 10, 14, 10);
            this.panelOpciones.Size = new System.Drawing.Size(274, 579);
            this.panelOpciones.TabIndex = 0;
            //
            // lblAdministracion
            //
            this.lblAdministracion.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold);
            this.lblAdministracion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblAdministracion.Location = new System.Drawing.Point(14, 18);
            this.lblAdministracion.Margin = new System.Windows.Forms.Padding(0, 8, 0, 2);
            this.lblAdministracion.Name = "lblAdministracion";
            this.lblAdministracion.Size = new System.Drawing.Size(236, 22);
            this.lblAdministracion.TabIndex = 0;
            this.lblAdministracion.Tag = "ADMINISTRACION";
            this.lblAdministracion.Text = "▼ ADMINISTRACION";
            this.lblAdministracion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // btnUsuarios
            //
            this.btnUsuarios.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.btnUsuarios.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnUsuarios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUsuarios.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnUsuarios.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnUsuarios.Location = new System.Drawing.Point(14, 42);
            this.btnUsuarios.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.btnUsuarios.Name = "btnUsuarios";
            this.btnUsuarios.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnUsuarios.Size = new System.Drawing.Size(236, 38);
            this.btnUsuarios.TabIndex = 1;
            this.btnUsuarios.Text = "Usuarios y roles";
            this.btnUsuarios.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUsuarios.UseVisualStyleBackColor = false;
            this.btnUsuarios.Click += new System.EventHandler(this.btnUsuarios_Click);
            //
            // btnSocios
            //
            this.btnSocios.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.btnSocios.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnSocios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSocios.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnSocios.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnSocios.Location = new System.Drawing.Point(14, 85);
            this.btnSocios.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.btnSocios.Name = "btnSocios";
            this.btnSocios.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnSocios.Size = new System.Drawing.Size(236, 38);
            this.btnSocios.TabIndex = 2;
            this.btnSocios.Text = "Socios";
            this.btnSocios.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSocios.UseVisualStyleBackColor = false;
            this.btnSocios.Click += new System.EventHandler(this.btnSocios_Click);
            //
            // lblOperacion
            //
            this.lblOperacion.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold);
            this.lblOperacion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblOperacion.Location = new System.Drawing.Point(14, 136);
            this.lblOperacion.Margin = new System.Windows.Forms.Padding(0, 8, 0, 2);
            this.lblOperacion.Name = "lblOperacion";
            this.lblOperacion.Size = new System.Drawing.Size(236, 22);
            this.lblOperacion.TabIndex = 3;
            this.lblOperacion.Tag = "OPERACION";
            this.lblOperacion.Text = "▶ OPERACION";
            this.lblOperacion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // btnPlanes
            //
            this.btnPlanes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.btnPlanes.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnPlanes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlanes.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnPlanes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnPlanes.Location = new System.Drawing.Point(14, 160);
            this.btnPlanes.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.btnPlanes.Name = "btnPlanes";
            this.btnPlanes.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnPlanes.Size = new System.Drawing.Size(236, 38);
            this.btnPlanes.TabIndex = 4;
            this.btnPlanes.Text = "Planes";
            this.btnPlanes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPlanes.UseVisualStyleBackColor = false;
            this.btnPlanes.Visible = false;
            this.btnPlanes.Click += new System.EventHandler(this.btnPlanes_Click);
            //
            // btnMembresias
            //
            this.btnMembresias.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.btnMembresias.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnMembresias.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMembresias.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnMembresias.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnMembresias.Location = new System.Drawing.Point(14, 203);
            this.btnMembresias.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.btnMembresias.Name = "btnMembresias";
            this.btnMembresias.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnMembresias.Size = new System.Drawing.Size(236, 38);
            this.btnMembresias.TabIndex = 5;
            this.btnMembresias.Text = "Membresias";
            this.btnMembresias.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMembresias.UseVisualStyleBackColor = false;
            this.btnMembresias.Visible = false;
            this.btnMembresias.Click += new System.EventHandler(this.btnMembresias_Click);
            //
            // btnPagos
            //
            this.btnPagos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.btnPagos.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnPagos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPagos.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnPagos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnPagos.Location = new System.Drawing.Point(14, 246);
            this.btnPagos.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.btnPagos.Name = "btnPagos";
            this.btnPagos.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnPagos.Size = new System.Drawing.Size(236, 38);
            this.btnPagos.TabIndex = 6;
            this.btnPagos.Text = "Cuotas y pagos";
            this.btnPagos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPagos.UseVisualStyleBackColor = false;
            this.btnPagos.Visible = false;
            this.btnPagos.Click += new System.EventHandler(this.btnPagos_Click);
            //
            // btnAsignaciones
            //
            this.btnAsignaciones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.btnAsignaciones.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnAsignaciones.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAsignaciones.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnAsignaciones.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnAsignaciones.Location = new System.Drawing.Point(14, 289);
            this.btnAsignaciones.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.btnAsignaciones.Name = "btnAsignaciones";
            this.btnAsignaciones.Size = new System.Drawing.Size(236, 38);
            this.btnAsignaciones.TabIndex = 7;
            this.btnAsignaciones.Text = "Asignar entrenador";
            this.btnAsignaciones.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAsignaciones.UseVisualStyleBackColor = false;
            this.btnAsignaciones.Visible = false;
            this.btnAsignaciones.Click += new System.EventHandler(this.btnAsignaciones_Click);
            //
            // lblRutinas
            //
            this.lblRutinas.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold);
            this.lblRutinas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblRutinas.Location = new System.Drawing.Point(14, 166);
            this.lblRutinas.Margin = new System.Windows.Forms.Padding(0, 8, 0, 2);
            this.lblRutinas.Name = "lblRutinas";
            this.lblRutinas.Size = new System.Drawing.Size(236, 22);
            this.lblRutinas.TabIndex = 7;
            this.lblRutinas.Tag = "RUTINAS";
            this.lblRutinas.Text = "▶ RUTINAS";
            this.lblRutinas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // btnEjercicios
            //
            this.btnEjercicios.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.btnEjercicios.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnEjercicios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEjercicios.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnEjercicios.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnEjercicios.Location = new System.Drawing.Point(14, 399);
            this.btnEjercicios.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.btnEjercicios.Name = "btnEjercicios";
            this.btnEjercicios.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnEjercicios.Size = new System.Drawing.Size(236, 38);
            this.btnEjercicios.TabIndex = 9;
            this.btnEjercicios.Text = "Ejercicios";
            this.btnEjercicios.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEjercicios.UseVisualStyleBackColor = false;
            this.btnEjercicios.Visible = false;
            this.btnEjercicios.Click += new System.EventHandler(this.btnEjercicios_Click);
            //
            // btnRutinas
            //
            this.btnRutinas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.btnRutinas.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnRutinas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRutinas.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnRutinas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnRutinas.Location = new System.Drawing.Point(14, 442);
            this.btnRutinas.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.btnRutinas.Name = "btnRutinas";
            this.btnRutinas.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnRutinas.Size = new System.Drawing.Size(236, 38);
            this.btnRutinas.TabIndex = 10;
            this.btnRutinas.Text = "Gestionar rutinas";
            this.btnRutinas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRutinas.UseVisualStyleBackColor = false;
            this.btnRutinas.Visible = false;
            this.btnRutinas.Click += new System.EventHandler(this.btnRutinas_Click);
            //
            // btnMisSocios
            //
            this.btnMisSocios.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.btnMisSocios.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnMisSocios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMisSocios.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnMisSocios.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnMisSocios.Location = new System.Drawing.Point(14, 485);
            this.btnMisSocios.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.btnMisSocios.Name = "btnMisSocios";
            this.btnMisSocios.Size = new System.Drawing.Size(236, 38);
            this.btnMisSocios.TabIndex = 11;
            this.btnMisSocios.Text = "Socios y rutinas";
            this.btnMisSocios.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMisSocios.UseVisualStyleBackColor = false;
            this.btnMisSocios.Visible = false;
            this.btnMisSocios.Click += new System.EventHandler(this.btnMisSocios_Click);
            //
            // lblConsultas
            //
            this.lblConsultas.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold);
            this.lblConsultas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblConsultas.Location = new System.Drawing.Point(14, 196);
            this.lblConsultas.Margin = new System.Windows.Forms.Padding(0, 8, 0, 2);
            this.lblConsultas.Name = "lblConsultas";
            this.lblConsultas.Size = new System.Drawing.Size(236, 22);
            this.lblConsultas.TabIndex = 10;
            this.lblConsultas.Tag = "CONSULTAS";
            this.lblConsultas.Text = "▶ CONSULTAS";
            this.lblConsultas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // btnReportes
            //
            this.btnReportes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.btnReportes.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnReportes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReportes.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnReportes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnReportes.Location = new System.Drawing.Point(14, 552);
            this.btnReportes.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.btnReportes.Name = "btnReportes";
            this.btnReportes.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnReportes.Size = new System.Drawing.Size(236, 38);
            this.btnReportes.TabIndex = 13;
            this.btnReportes.Text = "Reportes";
            this.btnReportes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReportes.UseVisualStyleBackColor = false;
            this.btnReportes.Visible = false;
            this.btnReportes.Click += new System.EventHandler(this.btnReportes_Click);
            //
            // btnSalir
            //
            this.btnSalir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalir.BackColor = System.Drawing.Color.FromArgb(255, 240, 240);
            this.btnSalir.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(202)))), ((int)(((byte)(202)))));
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.ForeColor = System.Drawing.Color.FromArgb(173, 36, 36);
            this.btnSalir.Location = new System.Drawing.Point(14, 16);
            this.btnSalir.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnSalir.Size = new System.Drawing.Size(246, 46);
            this.btnSalir.TabIndex = 0;
            this.btnSalir.Text = "Salir";
            this.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            //
            // panelContenido
            //
            this.panelContenido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.panelContenido.Controls.Add(this.inicioPanel);
            this.panelContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenido.Location = new System.Drawing.Point(274, 90);
            this.panelContenido.Name = "panelContenido";
            this.panelContenido.Size = new System.Drawing.Size(1008, 659);
            this.panelContenido.TabIndex = 0;
            //
            // inicioPanel
            //
            this.inicioPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.inicioPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inicioPanel.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.inicioPanel.Location = new System.Drawing.Point(0, 0);
            this.inicioPanel.MinimumSize = new System.Drawing.Size(640, 460);
            this.inicioPanel.Name = "inicioPanel";
            this.inicioPanel.Size = new System.Drawing.Size(1008, 659);
            this.inicioPanel.TabIndex = 0;
            //
            // PanelAdministrador
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
            this.Name = "PanelAdministrador";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SysGym";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.PanelAdministrador_Load);
            this.panelEncabezado.ResumeLayout(false);
            this.panelMenu.ResumeLayout(false);
            this.panelOpciones.ResumeLayout(false);
            this.panelContenido.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
