using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Entrenador
{
    partial class DashboardEntrenador
    {
        private IContainer components = null;
        private Panel panelEncabezado;
        private Label lblMarca;
        private Label lblUsuarioRol;
        private Button btnCambiarCuenta;
        private Panel panelMenu;
        private FlowLayoutPanel panelOpciones;
        private Label lblTrabajo;
        private Button btnSocios;
        private Button btnRutinas;
        private Label lblCatalogo;
        private Button btnEjercicios;
        private Label lblControl;
        private Button btnAsistencias;
        private Panel panelPie;
        private Button btnSalir;
        private Panel panelContenido;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            panelEncabezado = new Panel();
            lblMarca = new Label();
            lblUsuarioRol = new Label();
            btnCambiarCuenta = new Button();
            panelMenu = new Panel();
            panelOpciones = new FlowLayoutPanel();
            lblTrabajo = new Label();
            btnSocios = new Button();
            btnRutinas = new Button();
            lblCatalogo = new Label();
            btnEjercicios = new Button();
            lblControl = new Label();
            btnAsistencias = new Button();
            panelPie = new Panel();
            btnSalir = new Button();
            panelContenido = new Panel();
            panelEncabezado.SuspendLayout();
            panelMenu.SuspendLayout();
            panelOpciones.SuspendLayout();
            panelPie.SuspendLayout();
            SuspendLayout();

            panelEncabezado.BackColor = Color.FromArgb(14, 116, 144);
            panelEncabezado.Controls.Add(lblMarca);
            panelEncabezado.Controls.Add(lblUsuarioRol);
            panelEncabezado.Controls.Add(btnCambiarCuenta);
            panelEncabezado.Dock = DockStyle.Top;
            panelEncabezado.Location = new Point(0, 0);
            panelEncabezado.Name = "panelEncabezado";
            panelEncabezado.Size = new Size(1200, 82);
            panelEncabezado.TabIndex = 0;

            lblMarca.AutoSize = true;
            lblMarca.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblMarca.ForeColor = Color.White;
            lblMarca.Location = new Point(24, 8);
            lblMarca.Name = "lblMarca";
            lblMarca.Size = new Size(124, 37);
            lblMarca.TabIndex = 0;
            lblMarca.Text = "SYSGYM";

            lblUsuarioRol.AutoSize = true;
            lblUsuarioRol.ForeColor = Color.FromArgb(207, 250, 254);
            lblUsuarioRol.Location = new Point(27, 48);
            lblUsuarioRol.Name = "lblUsuarioRol";
            lblUsuarioRol.Size = new Size(329, 19);
            lblUsuarioRol.TabIndex = 1;
            lblUsuarioRol.Text = "Usuario: Entrenador de diseno    |    Rol: Entrenador";

            btnCambiarCuenta.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCambiarCuenta.BackColor = Color.White;
            btnCambiarCuenta.FlatAppearance.BorderSize = 0;
            btnCambiarCuenta.FlatStyle = FlatStyle.Flat;
            btnCambiarCuenta.ForeColor = Color.FromArgb(14, 116, 144);
            btnCambiarCuenta.Location = new Point(1018, 23);
            btnCambiarCuenta.Name = "btnCambiarCuenta";
            btnCambiarCuenta.Size = new Size(158, 34);
            btnCambiarCuenta.TabIndex = 2;
            btnCambiarCuenta.Text = "Cambiar de cuenta";
            btnCambiarCuenta.UseVisualStyleBackColor = false;

            panelMenu.BackColor = Color.White;
            panelMenu.Controls.Add(panelOpciones);
            panelMenu.Controls.Add(panelPie);
            panelMenu.Dock = DockStyle.Left;
            panelMenu.Location = new Point(0, 82);
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new Size(260, 678);
            panelMenu.TabIndex = 1;

            panelOpciones.AutoScroll = true;
            panelOpciones.BackColor = Color.White;
            panelOpciones.Controls.Add(lblTrabajo);
            panelOpciones.Controls.Add(btnSocios);
            panelOpciones.Controls.Add(btnRutinas);
            panelOpciones.Controls.Add(lblCatalogo);
            panelOpciones.Controls.Add(btnEjercicios);
            panelOpciones.Controls.Add(lblControl);
            panelOpciones.Controls.Add(btnAsistencias);
            panelOpciones.Dock = DockStyle.Fill;
            panelOpciones.FlowDirection = FlowDirection.TopDown;
            panelOpciones.Location = new Point(0, 0);
            panelOpciones.Name = "panelOpciones";
            panelOpciones.Padding = new Padding(14, 18, 14, 18);
            panelOpciones.Size = new Size(260, 608);
            panelOpciones.TabIndex = 0;
            panelOpciones.WrapContents = false;

            lblTrabajo.Font = new Font("Segoe UI Semibold", 8F, FontStyle.Bold); lblTrabajo.ForeColor = Color.FromArgb(100, 116, 139); lblTrabajo.Location = new Point(14, 26); lblTrabajo.Margin = new Padding(0, 8, 0, 4); lblTrabajo.Name = "lblTrabajo"; lblTrabajo.Size = new Size(214, 24); lblTrabajo.TabIndex = 0; lblTrabajo.Text = "MI TRABAJO";
            btnSocios.BackColor = Color.FromArgb(248, 250, 252); btnSocios.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240); btnSocios.FlatStyle = FlatStyle.Flat; btnSocios.Font = new Font("Segoe UI", 9.5F); btnSocios.ForeColor = Color.FromArgb(51, 65, 85); btnSocios.Location = new Point(14, 54); btnSocios.Margin = new Padding(0, 0, 0, 5); btnSocios.Name = "btnSocios"; btnSocios.Size = new Size(214, 40); btnSocios.TabIndex = 1; btnSocios.Text = "Mis socios"; btnSocios.TextAlign = ContentAlignment.MiddleLeft; btnSocios.UseVisualStyleBackColor = false;
            btnRutinas.BackColor = Color.FromArgb(248, 250, 252); btnRutinas.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240); btnRutinas.FlatStyle = FlatStyle.Flat; btnRutinas.Font = new Font("Segoe UI", 9.5F); btnRutinas.ForeColor = Color.FromArgb(51, 65, 85); btnRutinas.Location = new Point(14, 99); btnRutinas.Margin = new Padding(0, 0, 0, 5); btnRutinas.Name = "btnRutinas"; btnRutinas.Size = new Size(214, 40); btnRutinas.TabIndex = 2; btnRutinas.Text = "Rutinas"; btnRutinas.TextAlign = ContentAlignment.MiddleLeft; btnRutinas.UseVisualStyleBackColor = false;
            lblCatalogo.Font = new Font("Segoe UI Semibold", 8F, FontStyle.Bold); lblCatalogo.ForeColor = Color.FromArgb(100, 116, 139); lblCatalogo.Location = new Point(14, 152); lblCatalogo.Margin = new Padding(0, 8, 0, 4); lblCatalogo.Name = "lblCatalogo"; lblCatalogo.Size = new Size(214, 24); lblCatalogo.TabIndex = 3; lblCatalogo.Text = "CATALOGO";
            btnEjercicios.BackColor = Color.FromArgb(248, 250, 252); btnEjercicios.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240); btnEjercicios.FlatStyle = FlatStyle.Flat; btnEjercicios.Font = new Font("Segoe UI", 9.5F); btnEjercicios.ForeColor = Color.FromArgb(51, 65, 85); btnEjercicios.Location = new Point(14, 180); btnEjercicios.Margin = new Padding(0, 0, 0, 5); btnEjercicios.Name = "btnEjercicios"; btnEjercicios.Size = new Size(214, 40); btnEjercicios.TabIndex = 4; btnEjercicios.Text = "Ejercicios"; btnEjercicios.TextAlign = ContentAlignment.MiddleLeft; btnEjercicios.UseVisualStyleBackColor = false;
            lblControl.Font = new Font("Segoe UI Semibold", 8F, FontStyle.Bold); lblControl.ForeColor = Color.FromArgb(100, 116, 139); lblControl.Location = new Point(14, 233); lblControl.Margin = new Padding(0, 8, 0, 4); lblControl.Name = "lblControl"; lblControl.Size = new Size(214, 24); lblControl.TabIndex = 5; lblControl.Text = "CONTROL DE ACCESO";
            btnAsistencias.BackColor = Color.FromArgb(248, 250, 252); btnAsistencias.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240); btnAsistencias.FlatStyle = FlatStyle.Flat; btnAsistencias.Font = new Font("Segoe UI", 9.5F); btnAsistencias.ForeColor = Color.FromArgb(51, 65, 85); btnAsistencias.Location = new Point(14, 261); btnAsistencias.Margin = new Padding(0, 0, 0, 5); btnAsistencias.Name = "btnAsistencias"; btnAsistencias.Size = new Size(214, 40); btnAsistencias.TabIndex = 6; btnAsistencias.Text = "Asistencias"; btnAsistencias.TextAlign = ContentAlignment.MiddleLeft; btnAsistencias.UseVisualStyleBackColor = false;

            panelPie.BackColor = Color.White; panelPie.Controls.Add(btnSalir); panelPie.Dock = DockStyle.Bottom; panelPie.Location = new Point(0, 608); panelPie.Name = "panelPie"; panelPie.Padding = new Padding(14, 10, 14, 14); panelPie.Size = new Size(260, 70); panelPie.TabIndex = 1;
            btnSalir.BackColor = Color.FromArgb(254, 242, 242); btnSalir.Dock = DockStyle.Fill; btnSalir.FlatAppearance.BorderColor = Color.FromArgb(254, 202, 202); btnSalir.FlatStyle = FlatStyle.Flat; btnSalir.ForeColor = Color.FromArgb(185, 28, 28); btnSalir.Location = new Point(14, 10); btnSalir.Name = "btnSalir"; btnSalir.Size = new Size(232, 46); btnSalir.TabIndex = 0; btnSalir.Text = "Salir"; btnSalir.TextAlign = ContentAlignment.MiddleLeft; btnSalir.UseVisualStyleBackColor = false;

            panelContenido.BackColor = Color.FromArgb(226, 232, 240); panelContenido.Dock = DockStyle.Fill; panelContenido.Location = new Point(260, 82); panelContenido.Name = "panelContenido"; panelContenido.Size = new Size(940, 678); panelContenido.TabIndex = 2;

            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(241, 245, 249);
            ClientSize = new Size(1200, 760);
            Controls.Add(panelContenido);
            Controls.Add(panelMenu);
            Controls.Add(panelEncabezado);
            Font = new Font("Segoe UI", 10F);
            MinimumSize = new Size(900, 600);
            Name = "DashboardEntrenador";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SysGym - Entrenador";
            WindowState = FormWindowState.Maximized;
            panelEncabezado.ResumeLayout(false);
            panelEncabezado.PerformLayout();
            panelMenu.ResumeLayout(false);
            panelOpciones.ResumeLayout(false);
            panelPie.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
