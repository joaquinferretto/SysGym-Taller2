using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Entrenador
{
    partial class PanelEntrenador
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
        private Label lblTrabajo;
        private Button btnSocios;
        private Button btnRutinas;
        private Label lblCatalogo;
        private Button btnEjercicios;
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
            this.lblTrabajo = new System.Windows.Forms.Label();
            this.btnSocios = new System.Windows.Forms.Button();
            this.btnRutinas = new System.Windows.Forms.Button();
            this.lblCatalogo = new System.Windows.Forms.Label();
            this.btnEjercicios = new System.Windows.Forms.Button();
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
            this.lblUsuarioRol.Text = "Usuario: Entrenador de diseno | Rol: Entrenador";
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
            this.panelOpciones.Controls.Add(this.lblTrabajo);
            this.panelOpciones.Controls.Add(this.btnSocios);
            this.panelOpciones.Controls.Add(this.btnRutinas);
            this.panelOpciones.Controls.Add(this.lblCatalogo);
            this.panelOpciones.Controls.Add(this.btnEjercicios);
            this.panelOpciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOpciones.Location = new System.Drawing.Point(0, 0);
            this.panelOpciones.Name = "panelOpciones";
            this.panelOpciones.Padding = new System.Windows.Forms.Padding(14, 10, 14, 10);
            this.panelOpciones.Size = new System.Drawing.Size(274, 601);
            this.panelOpciones.TabIndex = 0;
            //
            // lblTrabajo
            //
            this.lblTrabajo.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold);
            this.lblTrabajo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblTrabajo.Location = new System.Drawing.Point(14, 18);
            this.lblTrabajo.Margin = new System.Windows.Forms.Padding(0, 8, 0, 2);
            this.lblTrabajo.Name = "lblTrabajo";
            this.lblTrabajo.Size = new System.Drawing.Size(236, 22);
            this.lblTrabajo.TabIndex = 0;
            this.lblTrabajo.Tag = "MI TRABAJO";
            this.lblTrabajo.Text = "▼ MI TRABAJO";
            this.lblTrabajo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.btnSocios.Text = "Mis socios";
            this.btnSocios.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSocios.UseVisualStyleBackColor = false;
            this.btnSocios.Click += new System.EventHandler(this.btnSocios_Click);
            //
            // btnRutinas
            //
            this.btnRutinas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.btnRutinas.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnRutinas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRutinas.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnRutinas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnRutinas.Location = new System.Drawing.Point(14, 85);
            this.btnRutinas.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.btnRutinas.Name = "btnRutinas";
            this.btnRutinas.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnRutinas.Size = new System.Drawing.Size(236, 38);
            this.btnRutinas.TabIndex = 2;
            this.btnRutinas.Text = "Rutinas";
            this.btnRutinas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRutinas.UseVisualStyleBackColor = false;
            this.btnRutinas.Click += new System.EventHandler(this.btnRutinas_Click);
            //
            // lblCatalogo
            //
            this.lblCatalogo.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold);
            this.lblCatalogo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblCatalogo.Location = new System.Drawing.Point(14, 136);
            this.lblCatalogo.Margin = new System.Windows.Forms.Padding(0, 8, 0, 2);
            this.lblCatalogo.Name = "lblCatalogo";
            this.lblCatalogo.Size = new System.Drawing.Size(236, 22);
            this.lblCatalogo.TabIndex = 3;
            this.lblCatalogo.Tag = "CATALOGO";
            this.lblCatalogo.Text = "▶ CATALOGO";
            this.lblCatalogo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // btnEjercicios
            //
            this.btnEjercicios.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.btnEjercicios.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnEjercicios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEjercicios.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnEjercicios.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnEjercicios.Location = new System.Drawing.Point(14, 160);
            this.btnEjercicios.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.btnEjercicios.Name = "btnEjercicios";
            this.btnEjercicios.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnEjercicios.Size = new System.Drawing.Size(236, 38);
            this.btnEjercicios.TabIndex = 4;
            this.btnEjercicios.Text = "Ejercicios";
            this.btnEjercicios.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEjercicios.UseVisualStyleBackColor = false;
            this.btnEjercicios.Visible = false;
            this.btnEjercicios.Click += new System.EventHandler(this.btnEjercicios_Click);
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
            this.lblBienvenida.Text = "Panel del entrenador\r\n\r\nElegi una opcion del menu lateral para comenzar a trabaja" +
    "r.";
            this.lblBienvenida.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // PanelEntrenador
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
            this.Name = "PanelEntrenador";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SysGym - Entrenador";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.PanelEntrenador_Load);
            this.panelEncabezado.ResumeLayout(false);
            this.panelMenu.ResumeLayout(false);
            this.panelOpciones.ResumeLayout(false);
            this.panelContenido.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
