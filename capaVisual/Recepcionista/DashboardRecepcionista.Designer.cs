using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Recepcionista
{
    partial class DashboardRecepcionista
    {
        private IContainer components = null;
        private Panel panelEncabezado;
        private Label lblMarca;
        private Label lblUsuarioRol;
        private Button btnCambiarCuenta;
        private Panel panelMenu;
        private FlowLayoutPanel panelOpciones;
        private Label lblClientes;
        private Button btnSocios;
        private Button btnMembresias;
        private Label lblCaja;
        private Button btnPagos;
        private Label lblEntrenadores;
        private Button btnAsignar;
        private Button btnConsultar;
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
            lblClientes = new Label();
            btnSocios = new Button();
            btnMembresias = new Button();
            lblCaja = new Label();
            btnPagos = new Button();
            lblEntrenadores = new Label();
            btnAsignar = new Button();
            btnConsultar = new Button();
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

            panelEncabezado.BackColor = Color.FromArgb(5, 150, 105);
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
            lblUsuarioRol.ForeColor = Color.FromArgb(209, 250, 229);
            lblUsuarioRol.Location = new Point(27, 48);
            lblUsuarioRol.Name = "lblUsuarioRol";
            lblUsuarioRol.Size = new Size(371, 19);
            lblUsuarioRol.TabIndex = 1;
            lblUsuarioRol.Text = "Usuario: Recepcionista de diseno    |    Rol: Recepcionista";

            btnCambiarCuenta.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCambiarCuenta.BackColor = Color.White;
            btnCambiarCuenta.FlatAppearance.BorderSize = 0;
            btnCambiarCuenta.FlatStyle = FlatStyle.Flat;
            btnCambiarCuenta.ForeColor = Color.FromArgb(5, 150, 105);
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
            panelOpciones.Controls.Add(lblClientes);
            panelOpciones.Controls.Add(btnSocios);
            panelOpciones.Controls.Add(btnMembresias);
            panelOpciones.Controls.Add(lblCaja);
            panelOpciones.Controls.Add(btnPagos);
            panelOpciones.Controls.Add(lblEntrenadores);
            panelOpciones.Controls.Add(btnAsignar);
            panelOpciones.Controls.Add(btnConsultar);
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

            lblClientes.Font = new Font("Segoe UI Semibold", 8F, FontStyle.Bold); lblClientes.ForeColor = Color.FromArgb(100, 116, 139); lblClientes.Location = new Point(14, 26); lblClientes.Margin = new Padding(0, 8, 0, 4); lblClientes.Name = "lblClientes"; lblClientes.Size = new Size(214, 24); lblClientes.TabIndex = 0; lblClientes.Text = "CLIENTES";
            btnSocios.BackColor = Color.FromArgb(248, 250, 252); btnSocios.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240); btnSocios.FlatStyle = FlatStyle.Flat; btnSocios.Font = new Font("Segoe UI", 9.5F); btnSocios.ForeColor = Color.FromArgb(51, 65, 85); btnSocios.Location = new Point(14, 54); btnSocios.Margin = new Padding(0, 0, 0, 5); btnSocios.Name = "btnSocios"; btnSocios.Size = new Size(214, 40); btnSocios.TabIndex = 1; btnSocios.Text = "Socios"; btnSocios.TextAlign = ContentAlignment.MiddleLeft; btnSocios.UseVisualStyleBackColor = false;
            btnMembresias.BackColor = Color.FromArgb(248, 250, 252); btnMembresias.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240); btnMembresias.FlatStyle = FlatStyle.Flat; btnMembresias.Font = new Font("Segoe UI", 9.5F); btnMembresias.ForeColor = Color.FromArgb(51, 65, 85); btnMembresias.Location = new Point(14, 99); btnMembresias.Margin = new Padding(0, 0, 0, 5); btnMembresias.Name = "btnMembresias"; btnMembresias.Size = new Size(214, 40); btnMembresias.TabIndex = 2; btnMembresias.Text = "Membresias"; btnMembresias.TextAlign = ContentAlignment.MiddleLeft; btnMembresias.UseVisualStyleBackColor = false;
            lblCaja.Font = new Font("Segoe UI Semibold", 8F, FontStyle.Bold); lblCaja.ForeColor = Color.FromArgb(100, 116, 139); lblCaja.Location = new Point(14, 152); lblCaja.Margin = new Padding(0, 8, 0, 4); lblCaja.Name = "lblCaja"; lblCaja.Size = new Size(214, 24); lblCaja.TabIndex = 3; lblCaja.Text = "CAJA";
            btnPagos.BackColor = Color.FromArgb(248, 250, 252); btnPagos.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240); btnPagos.FlatStyle = FlatStyle.Flat; btnPagos.Font = new Font("Segoe UI", 9.5F); btnPagos.ForeColor = Color.FromArgb(51, 65, 85); btnPagos.Location = new Point(14, 180); btnPagos.Margin = new Padding(0, 0, 0, 5); btnPagos.Name = "btnPagos"; btnPagos.Size = new Size(214, 40); btnPagos.TabIndex = 4; btnPagos.Text = "Cuotas y pagos"; btnPagos.TextAlign = ContentAlignment.MiddleLeft; btnPagos.UseVisualStyleBackColor = false;
            lblEntrenadores.Font = new Font("Segoe UI Semibold", 8F, FontStyle.Bold); lblEntrenadores.ForeColor = Color.FromArgb(100, 116, 139); lblEntrenadores.Location = new Point(14, 233); lblEntrenadores.Margin = new Padding(0, 8, 0, 4); lblEntrenadores.Name = "lblEntrenadores"; lblEntrenadores.Size = new Size(214, 24); lblEntrenadores.TabIndex = 5; lblEntrenadores.Text = "ENTRENADORES";
            btnAsignar.BackColor = Color.FromArgb(248, 250, 252); btnAsignar.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240); btnAsignar.FlatStyle = FlatStyle.Flat; btnAsignar.Font = new Font("Segoe UI", 9.5F); btnAsignar.ForeColor = Color.FromArgb(51, 65, 85); btnAsignar.Location = new Point(14, 261); btnAsignar.Margin = new Padding(0, 0, 0, 5); btnAsignar.Name = "btnAsignar"; btnAsignar.Size = new Size(214, 40); btnAsignar.TabIndex = 6; btnAsignar.Text = "Asignar entrenador"; btnAsignar.TextAlign = ContentAlignment.MiddleLeft; btnAsignar.UseVisualStyleBackColor = false;
            btnConsultar.BackColor = Color.FromArgb(248, 250, 252); btnConsultar.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240); btnConsultar.FlatStyle = FlatStyle.Flat; btnConsultar.Font = new Font("Segoe UI", 9.5F); btnConsultar.ForeColor = Color.FromArgb(51, 65, 85); btnConsultar.Location = new Point(14, 306); btnConsultar.Margin = new Padding(0, 0, 0, 5); btnConsultar.Name = "btnConsultar"; btnConsultar.Size = new Size(214, 40); btnConsultar.TabIndex = 7; btnConsultar.Text = "Consultar entrenador"; btnConsultar.TextAlign = ContentAlignment.MiddleLeft; btnConsultar.UseVisualStyleBackColor = false;
            lblControl.Font = new Font("Segoe UI Semibold", 8F, FontStyle.Bold); lblControl.ForeColor = Color.FromArgb(100, 116, 139); lblControl.Location = new Point(14, 359); lblControl.Margin = new Padding(0, 8, 0, 4); lblControl.Name = "lblControl"; lblControl.Size = new Size(214, 24); lblControl.TabIndex = 8; lblControl.Text = "CONTROL DE ACCESO";
            btnAsistencias.BackColor = Color.FromArgb(248, 250, 252); btnAsistencias.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240); btnAsistencias.FlatStyle = FlatStyle.Flat; btnAsistencias.Font = new Font("Segoe UI", 9.5F); btnAsistencias.ForeColor = Color.FromArgb(51, 65, 85); btnAsistencias.Location = new Point(14, 387); btnAsistencias.Margin = new Padding(0, 0, 0, 5); btnAsistencias.Name = "btnAsistencias"; btnAsistencias.Size = new Size(214, 40); btnAsistencias.TabIndex = 9; btnAsistencias.Text = "Asistencias"; btnAsistencias.TextAlign = ContentAlignment.MiddleLeft; btnAsistencias.UseVisualStyleBackColor = false;

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
            Name = "DashboardRecepcionista";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SysGym - Recepcionista";
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
