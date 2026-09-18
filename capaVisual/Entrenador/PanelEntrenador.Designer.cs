using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Entrenador
{
    partial class PanelEntrenador
    {
        private IContainer components = null;
        private Panel panelEncabezado;
        private TableLayoutPanel layoutEncabezado;
        private Panel panelIdentidad;
        private Label lblMarca;
        private Label lblUsuarioRol;
        private Label lblModuloActual;
        private Button btnCambiarCuenta;
        private Panel panelMenu;
        private Panel panelOpciones;
        private Label lblTrabajo;
        private Button btnSocios;
        private Button btnRutinas;
        private Label lblCatalogo;
        private Button btnEjercicios;
        private Panel panelPie;
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
            this.layoutEncabezado = new System.Windows.Forms.TableLayoutPanel();
            this.panelIdentidad = new System.Windows.Forms.Panel();
            this.lblUsuarioRol = new System.Windows.Forms.Label();
            this.lblMarca = new System.Windows.Forms.Label();
            this.lblModuloActual = new System.Windows.Forms.Label();
            this.btnCambiarCuenta = new System.Windows.Forms.Button();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.panelOpciones = new System.Windows.Forms.Panel();
            this.lblTrabajo = new System.Windows.Forms.Label();
            this.btnSocios = new System.Windows.Forms.Button();
            this.btnRutinas = new System.Windows.Forms.Button();
            this.lblCatalogo = new System.Windows.Forms.Label();
            this.btnEjercicios = new System.Windows.Forms.Button();
            this.panelPie = new System.Windows.Forms.Panel();
            this.btnSalir = new System.Windows.Forms.Button();
            this.panelContenido = new System.Windows.Forms.Panel();
            this.lblBienvenida = new System.Windows.Forms.Label();
            this.panelEncabezado.SuspendLayout();
            this.layoutEncabezado.SuspendLayout();
            this.panelIdentidad.SuspendLayout();
            this.panelMenu.SuspendLayout();
            this.panelOpciones.SuspendLayout();
            this.panelPie.SuspendLayout();
            this.panelContenido.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEncabezado
            // 
            this.panelEncabezado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(116)))), ((int)(((byte)(144)))));
            this.panelEncabezado.Controls.Add(this.layoutEncabezado);
            this.panelEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEncabezado.Location = new System.Drawing.Point(0, 0);
            this.panelEncabezado.Name = "panelEncabezado";
            this.panelEncabezado.Padding = new System.Windows.Forms.Padding(24, 8, 24, 8);
            this.panelEncabezado.Size = new System.Drawing.Size(1200, 90);
            this.panelEncabezado.TabIndex = 0;
            // 
            // layoutEncabezado
            // 
            this.layoutEncabezado.ColumnCount = 3;
            this.layoutEncabezado.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.layoutEncabezado.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutEncabezado.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 190F));
            this.layoutEncabezado.Controls.Add(this.panelIdentidad, 0, 0);
            this.layoutEncabezado.Controls.Add(this.lblModuloActual, 1, 0);
            this.layoutEncabezado.Controls.Add(this.btnCambiarCuenta, 2, 0);
            this.layoutEncabezado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutEncabezado.Location = new System.Drawing.Point(24, 8);
            this.layoutEncabezado.Margin = new System.Windows.Forms.Padding(0);
            this.layoutEncabezado.Name = "layoutEncabezado";
            this.layoutEncabezado.RowCount = 1;
            this.layoutEncabezado.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutEncabezado.Size = new System.Drawing.Size(1152, 74);
            this.layoutEncabezado.TabIndex = 3;
            // 
            // panelIdentidad
            // 
            this.panelIdentidad.Controls.Add(this.lblUsuarioRol);
            this.panelIdentidad.Controls.Add(this.lblMarca);
            this.panelIdentidad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelIdentidad.Location = new System.Drawing.Point(0, 0);
            this.panelIdentidad.Margin = new System.Windows.Forms.Padding(0);
            this.panelIdentidad.Name = "panelIdentidad";
            this.panelIdentidad.Size = new System.Drawing.Size(250, 74);
            this.panelIdentidad.TabIndex = 0;
            // 
            // lblUsuarioRol
            // 
            this.lblUsuarioRol.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblUsuarioRol.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(250)))), ((int)(((byte)(254)))));
            this.lblUsuarioRol.Location = new System.Drawing.Point(0, 48);
            this.lblUsuarioRol.Margin = new System.Windows.Forms.Padding(0);
            this.lblUsuarioRol.Name = "lblUsuarioRol";
            this.lblUsuarioRol.Size = new System.Drawing.Size(250, 26);
            this.lblUsuarioRol.TabIndex = 1;
            this.lblUsuarioRol.Text = "Usuario: Entrenador de diseno    |    Rol: Entrenador";
            // 
            // lblMarca
            // 
            this.lblMarca.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblMarca.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblMarca.ForeColor = System.Drawing.Color.White;
            this.lblMarca.Location = new System.Drawing.Point(0, 0);
            this.lblMarca.Margin = new System.Windows.Forms.Padding(0);
            this.lblMarca.Name = "lblMarca";
            this.lblMarca.Size = new System.Drawing.Size(250, 36);
            this.lblMarca.TabIndex = 0;
            this.lblMarca.Text = "SYSGYM";
            this.lblMarca.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lblModuloActual
            // 
            this.lblModuloActual.AutoEllipsis = true;
            this.lblModuloActual.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblModuloActual.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblModuloActual.ForeColor = System.Drawing.Color.White;
            this.lblModuloActual.Location = new System.Drawing.Point(262, 0);
            this.lblModuloActual.Margin = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.lblModuloActual.Name = "lblModuloActual";
            this.lblModuloActual.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.lblModuloActual.Size = new System.Drawing.Size(688, 74);
            this.lblModuloActual.TabIndex = 4;
            this.lblModuloActual.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCambiarCuenta
            // 
            this.btnCambiarCuenta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCambiarCuenta.BackColor = System.Drawing.Color.White;
            this.btnCambiarCuenta.FlatAppearance.BorderSize = 0;
            this.btnCambiarCuenta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCambiarCuenta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(116)))), ((int)(((byte)(144)))));
            this.btnCambiarCuenta.Location = new System.Drawing.Point(978, 0);
            this.btnCambiarCuenta.Margin = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.btnCambiarCuenta.Name = "btnCambiarCuenta";
            this.btnCambiarCuenta.Size = new System.Drawing.Size(174, 38);
            this.btnCambiarCuenta.TabIndex = 2;
            this.btnCambiarCuenta.Text = "Cambiar de cuenta";
            this.btnCambiarCuenta.UseVisualStyleBackColor = false;
            this.btnCambiarCuenta.Click += new System.EventHandler(this.btnCambiarCuenta_Click);
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.White;
            this.panelMenu.Controls.Add(this.panelOpciones);
            this.panelMenu.Controls.Add(this.panelPie);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 90);
            this.panelMenu.MinimumSize = new System.Drawing.Size(264, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(264, 670);
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
            this.panelOpciones.Size = new System.Drawing.Size(264, 594);
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
            this.btnEjercicios.Location = new System.Drawing.Point(0, 0);
            this.btnEjercicios.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.btnEjercicios.Name = "btnEjercicios";
            this.btnEjercicios.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnEjercicios.Size = new System.Drawing.Size(75, 23);
            this.btnEjercicios.TabIndex = 4;
            this.btnEjercicios.Text = "Ejercicios";
            this.btnEjercicios.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEjercicios.UseVisualStyleBackColor = false;
            this.btnEjercicios.Visible = false;
            this.btnEjercicios.Click += new System.EventHandler(this.btnEjercicios_Click);
            // 
            // panelPie
            // 
            this.panelPie.BackColor = System.Drawing.Color.White;
            this.panelPie.Controls.Add(this.btnSalir);
            this.panelPie.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelPie.Location = new System.Drawing.Point(0, 594);
            this.panelPie.Name = "panelPie";
            this.panelPie.Padding = new System.Windows.Forms.Padding(14, 12, 14, 16);
            this.panelPie.Size = new System.Drawing.Size(264, 76);
            this.panelPie.TabIndex = 1;
            // 
            // btnSalir
            // 
            this.btnSalir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.btnSalir.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(202)))), ((int)(((byte)(202)))));
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.btnSalir.Location = new System.Drawing.Point(14, 14);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnSalir.Size = new System.Drawing.Size(236, 46);
            this.btnSalir.TabIndex = 0;
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
            this.panelContenido.Location = new System.Drawing.Point(264, 90);
            this.panelContenido.Name = "panelContenido";
            this.panelContenido.Size = new System.Drawing.Size(936, 670);
            this.panelContenido.TabIndex = 2;
            // 
            // lblBienvenida
            // 
            this.lblBienvenida.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBienvenida.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblBienvenida.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblBienvenida.Location = new System.Drawing.Point(0, 0);
            this.lblBienvenida.Name = "lblBienvenida";
            this.lblBienvenida.Size = new System.Drawing.Size(936, 670);
            this.lblBienvenida.TabIndex = 0;
            this.lblBienvenida.Text = "Panel del entrenador\r\n\r\nElegi una opcion del menu lateral para comenzar a trabaja" +
    "r.";
            this.lblBienvenida.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PanelEntrenador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1200, 760);
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
            this.layoutEncabezado.ResumeLayout(false);
            this.panelIdentidad.ResumeLayout(false);
            this.panelMenu.ResumeLayout(false);
            this.panelOpciones.ResumeLayout(false);
            this.panelPie.ResumeLayout(false);
            this.panelContenido.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
