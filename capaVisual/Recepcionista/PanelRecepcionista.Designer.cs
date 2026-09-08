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
        private Label lblControl;
        private Button btnAsistencias;
        private Panel panelPie;
        private Button btnSalir;
        private Panel panelContenido;

        protected override void Dispose(bool liberarRecursos)
        {
            if (liberarRecursos && components != null) components.Dispose();
            base.Dispose(liberarRecursos);
        }

        private void InitializeComponent()
        {
            panelEncabezado = new Panel();
            lblMarca = new Label();
            lblUsuarioRol = new Label();
            btnCambiarCuenta = new Button();
            panelMenu = new Panel();
            panelOpciones = new Panel();
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

            panelEncabezado.Name = "panelEncabezado";

            panelEncabezado.TabIndex = 0;

            lblMarca.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblMarca.ForeColor = Color.White;

            lblMarca.Name = "lblMarca";

            lblMarca.TabIndex = 0;
            lblMarca.Text = "SYSGYM";

            lblUsuarioRol.ForeColor = Color.FromArgb(209, 250, 229);

            lblUsuarioRol.Name = "lblUsuarioRol";

            lblUsuarioRol.TabIndex = 1;
            lblUsuarioRol.Text = "Usuario: Recepcionista de diseno    |    Rol: Recepcionista";

            btnCambiarCuenta.BackColor = Color.White;
            btnCambiarCuenta.FlatAppearance.BorderSize = 0;
            btnCambiarCuenta.FlatStyle = FlatStyle.Flat;
            btnCambiarCuenta.ForeColor = Color.FromArgb(5, 150, 105);

            btnCambiarCuenta.Name = "btnCambiarCuenta";

            btnCambiarCuenta.TabIndex = 2;
            btnCambiarCuenta.Text = "Cambiar de cuenta";
            btnCambiarCuenta.UseVisualStyleBackColor = false;

            panelMenu.BackColor = Color.White;
            panelMenu.Controls.Add(panelOpciones);
            panelMenu.Controls.Add(panelPie);

            panelMenu.Name = "panelMenu";

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

            panelOpciones.Name = "panelOpciones";
            panelOpciones.Padding = new Padding(14, 18, 14, 18);

            panelOpciones.TabIndex = 0;

            lblClientes.Font = new Font("Segoe UI Semibold", 8F, FontStyle.Bold); lblClientes.ForeColor = Color.FromArgb(100, 116, 139);  lblClientes.Margin = new Padding(0, 8, 0, 4); lblClientes.Name = "lblClientes";  lblClientes.TabIndex = 0; lblClientes.Text = "CLIENTES";
            btnSocios.BackColor = Color.FromArgb(248, 250, 252); btnSocios.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240); btnSocios.FlatStyle = FlatStyle.Flat; btnSocios.Font = new Font("Segoe UI", 9.5F); btnSocios.ForeColor = Color.FromArgb(51, 65, 85);  btnSocios.Margin = new Padding(0, 0, 0, 5); btnSocios.Name = "btnSocios";  btnSocios.TabIndex = 1; btnSocios.Text = "Socios"; btnSocios.TextAlign = ContentAlignment.MiddleLeft; btnSocios.UseVisualStyleBackColor = false;
            btnMembresias.BackColor = Color.FromArgb(248, 250, 252); btnMembresias.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240); btnMembresias.FlatStyle = FlatStyle.Flat; btnMembresias.Font = new Font("Segoe UI", 9.5F); btnMembresias.ForeColor = Color.FromArgb(51, 65, 85);  btnMembresias.Margin = new Padding(0, 0, 0, 5); btnMembresias.Name = "btnMembresias";  btnMembresias.TabIndex = 2; btnMembresias.Text = "Membresias"; btnMembresias.TextAlign = ContentAlignment.MiddleLeft; btnMembresias.UseVisualStyleBackColor = false;
            lblCaja.Font = new Font("Segoe UI Semibold", 8F, FontStyle.Bold); lblCaja.ForeColor = Color.FromArgb(100, 116, 139);  lblCaja.Margin = new Padding(0, 8, 0, 4); lblCaja.Name = "lblCaja";  lblCaja.TabIndex = 3; lblCaja.Text = "CAJA";
            btnPagos.BackColor = Color.FromArgb(248, 250, 252); btnPagos.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240); btnPagos.FlatStyle = FlatStyle.Flat; btnPagos.Font = new Font("Segoe UI", 9.5F); btnPagos.ForeColor = Color.FromArgb(51, 65, 85);  btnPagos.Margin = new Padding(0, 0, 0, 5); btnPagos.Name = "btnPagos";  btnPagos.TabIndex = 4; btnPagos.Text = "Cuotas y pagos"; btnPagos.TextAlign = ContentAlignment.MiddleLeft; btnPagos.UseVisualStyleBackColor = false;
            lblEntrenadores.Font = new Font("Segoe UI Semibold", 8F, FontStyle.Bold); lblEntrenadores.ForeColor = Color.FromArgb(100, 116, 139);  lblEntrenadores.Margin = new Padding(0, 8, 0, 4); lblEntrenadores.Name = "lblEntrenadores";  lblEntrenadores.TabIndex = 5; lblEntrenadores.Text = "ENTRENADORES";
            btnAsignar.BackColor = Color.FromArgb(248, 250, 252); btnAsignar.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240); btnAsignar.FlatStyle = FlatStyle.Flat; btnAsignar.Font = new Font("Segoe UI", 9.5F); btnAsignar.ForeColor = Color.FromArgb(51, 65, 85);  btnAsignar.Margin = new Padding(0, 0, 0, 5); btnAsignar.Name = "btnAsignar";  btnAsignar.TabIndex = 6; btnAsignar.Text = "Asignar entrenador"; btnAsignar.TextAlign = ContentAlignment.MiddleLeft; btnAsignar.UseVisualStyleBackColor = false;
            btnConsultar.BackColor = Color.FromArgb(248, 250, 252); btnConsultar.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240); btnConsultar.FlatStyle = FlatStyle.Flat; btnConsultar.Font = new Font("Segoe UI", 9.5F); btnConsultar.ForeColor = Color.FromArgb(51, 65, 85);  btnConsultar.Margin = new Padding(0, 0, 0, 5); btnConsultar.Name = "btnConsultar";  btnConsultar.TabIndex = 7; btnConsultar.Text = "Consultar entrenador"; btnConsultar.TextAlign = ContentAlignment.MiddleLeft; btnConsultar.UseVisualStyleBackColor = false;
            lblControl.Font = new Font("Segoe UI Semibold", 8F, FontStyle.Bold); lblControl.ForeColor = Color.FromArgb(100, 116, 139);  lblControl.Margin = new Padding(0, 8, 0, 4); lblControl.Name = "lblControl";  lblControl.TabIndex = 8; lblControl.Text = "CONTROL DE ACCESO";
            btnAsistencias.BackColor = Color.FromArgb(248, 250, 252); btnAsistencias.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240); btnAsistencias.FlatStyle = FlatStyle.Flat; btnAsistencias.Font = new Font("Segoe UI", 9.5F); btnAsistencias.ForeColor = Color.FromArgb(51, 65, 85);  btnAsistencias.Margin = new Padding(0, 0, 0, 5); btnAsistencias.Name = "btnAsistencias";  btnAsistencias.TabIndex = 9; btnAsistencias.Text = "Asistencias"; btnAsistencias.TextAlign = ContentAlignment.MiddleLeft; btnAsistencias.UseVisualStyleBackColor = false;

            panelPie.BackColor = Color.White; panelPie.Controls.Add(btnSalir);   panelPie.Name = "panelPie"; panelPie.Padding = new Padding(14, 10, 14, 14);  panelPie.TabIndex = 1;
            btnSalir.BackColor = Color.FromArgb(254, 242, 242);  btnSalir.FlatAppearance.BorderColor = Color.FromArgb(254, 202, 202); btnSalir.FlatStyle = FlatStyle.Flat; btnSalir.ForeColor = Color.FromArgb(185, 28, 28);  btnSalir.Name = "btnSalir";  btnSalir.TabIndex = 0; btnSalir.Text = "Salir"; btnSalir.TextAlign = ContentAlignment.MiddleLeft; btnSalir.UseVisualStyleBackColor = false;

            panelContenido.BackColor = Color.FromArgb(226, 232, 240);   panelContenido.Name = "panelContenido";  panelContenido.TabIndex = 2;

            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font; this.AutoScroll = true;
            BackColor = Color.FromArgb(241, 245, 249);
            ClientSize = new Size(1200, 760);
            Controls.Add(panelContenido);
            Controls.Add(panelMenu);
            Controls.Add(panelEncabezado);
            Font = new Font("Segoe UI", 10F);
            MinimumSize = new Size(900, 600);
            this.Name = "PanelRecepcionista";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SysGym - Recepcionista";
            WindowState = FormWindowState.Maximized;

            this.panelEncabezado.AutoSize = false;
            this.panelEncabezado.Dock = System.Windows.Forms.DockStyle.None;
            this.panelEncabezado.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.panelEncabezado.Location = new System.Drawing.Point(0, 0);
            this.panelEncabezado.Size = new System.Drawing.Size(1200, 82);
            this.lblMarca.AutoSize = false;
            this.lblMarca.Dock = System.Windows.Forms.DockStyle.None;
            this.lblMarca.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblMarca.Location = new System.Drawing.Point(24, 8);
            this.lblMarca.Size = new System.Drawing.Size(119, 42);
            this.lblUsuarioRol.AutoSize = false;
            this.lblUsuarioRol.Dock = System.Windows.Forms.DockStyle.None;
            this.lblUsuarioRol.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblUsuarioRol.Location = new System.Drawing.Point(27, 48);
            this.lblUsuarioRol.Size = new System.Drawing.Size(347, 23);
            this.btnCambiarCuenta.AutoSize = false;
            this.btnCambiarCuenta.Dock = System.Windows.Forms.DockStyle.None;
            this.btnCambiarCuenta.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.btnCambiarCuenta.Location = new System.Drawing.Point(1018, 23);
            this.btnCambiarCuenta.Size = new System.Drawing.Size(158, 34);
            this.panelMenu.AutoSize = false;
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.None;
            this.panelMenu.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 82);
            this.panelMenu.Size = new System.Drawing.Size(260, 678);
            this.panelOpciones.AutoSize = false;
            this.panelOpciones.Dock = System.Windows.Forms.DockStyle.None;
            this.panelOpciones.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.panelOpciones.Location = new System.Drawing.Point(0, 0);
            this.panelOpciones.Size = new System.Drawing.Size(260, 608);
            this.lblClientes.AutoSize = false;
            this.lblClientes.Dock = System.Windows.Forms.DockStyle.None;
            this.lblClientes.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblClientes.Location = new System.Drawing.Point(14, 26);
            this.lblClientes.Size = new System.Drawing.Size(214, 24);
            this.btnSocios.AutoSize = false;
            this.btnSocios.Dock = System.Windows.Forms.DockStyle.None;
            this.btnSocios.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.btnSocios.Location = new System.Drawing.Point(14, 54);
            this.btnSocios.Size = new System.Drawing.Size(214, 40);
            this.btnMembresias.AutoSize = false;
            this.btnMembresias.Dock = System.Windows.Forms.DockStyle.None;
            this.btnMembresias.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.btnMembresias.Location = new System.Drawing.Point(14, 99);
            this.btnMembresias.Size = new System.Drawing.Size(214, 40);
            this.lblCaja.AutoSize = false;
            this.lblCaja.Dock = System.Windows.Forms.DockStyle.None;
            this.lblCaja.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblCaja.Location = new System.Drawing.Point(14, 152);
            this.lblCaja.Size = new System.Drawing.Size(214, 24);
            this.btnPagos.AutoSize = false;
            this.btnPagos.Dock = System.Windows.Forms.DockStyle.None;
            this.btnPagos.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.btnPagos.Location = new System.Drawing.Point(14, 180);
            this.btnPagos.Size = new System.Drawing.Size(214, 40);
            this.lblEntrenadores.AutoSize = false;
            this.lblEntrenadores.Dock = System.Windows.Forms.DockStyle.None;
            this.lblEntrenadores.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblEntrenadores.Location = new System.Drawing.Point(14, 233);
            this.lblEntrenadores.Size = new System.Drawing.Size(214, 24);
            this.btnAsignar.AutoSize = false;
            this.btnAsignar.Dock = System.Windows.Forms.DockStyle.None;
            this.btnAsignar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.btnAsignar.Location = new System.Drawing.Point(14, 261);
            this.btnAsignar.Size = new System.Drawing.Size(214, 40);
            this.btnConsultar.AutoSize = false;
            this.btnConsultar.Dock = System.Windows.Forms.DockStyle.None;
            this.btnConsultar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.btnConsultar.Location = new System.Drawing.Point(14, 306);
            this.btnConsultar.Size = new System.Drawing.Size(214, 40);
            this.lblControl.AutoSize = false;
            this.lblControl.Dock = System.Windows.Forms.DockStyle.None;
            this.lblControl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblControl.Location = new System.Drawing.Point(14, 359);
            this.lblControl.Size = new System.Drawing.Size(214, 24);
            this.btnAsistencias.AutoSize = false;
            this.btnAsistencias.Dock = System.Windows.Forms.DockStyle.None;
            this.btnAsistencias.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.btnAsistencias.Location = new System.Drawing.Point(14, 387);
            this.btnAsistencias.Size = new System.Drawing.Size(214, 40);
            this.panelPie.AutoSize = false;
            this.panelPie.Dock = System.Windows.Forms.DockStyle.None;
            this.panelPie.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.panelPie.Location = new System.Drawing.Point(0, 608);
            this.panelPie.Size = new System.Drawing.Size(260, 70);
            this.btnSalir.AutoSize = false;
            this.btnSalir.Dock = System.Windows.Forms.DockStyle.None;
            this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.btnSalir.Location = new System.Drawing.Point(14, 10);
            this.btnSalir.Size = new System.Drawing.Size(232, 46);
            this.panelContenido.AutoSize = false;
            this.panelContenido.Dock = System.Windows.Forms.DockStyle.None;
            this.panelContenido.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.panelContenido.Location = new System.Drawing.Point(260, 82);
            this.panelContenido.Size = new System.Drawing.Size(940, 678);
            panelEncabezado.ResumeLayout(false);
            panelEncabezado.PerformLayout();
            panelMenu.ResumeLayout(false);
            panelOpciones.ResumeLayout(false);
            panelPie.ResumeLayout(false);
            ResumeLayout(false);

            this.btnCambiarCuenta.Click += new System.EventHandler(this.btnCambiarCuenta_Click);
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            this.btnSocios.Click += new System.EventHandler(this.btnSocios_Click);
            this.btnMembresias.Click += new System.EventHandler(this.btnMembresias_Click);
            this.btnPagos.Click += new System.EventHandler(this.btnPagos_Click);
            this.btnAsignar.Click += new System.EventHandler(this.btnAsignar_Click);
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            this.btnAsistencias.Click += new System.EventHandler(this.btnAsistencias_Click);
                    this.Load += new System.EventHandler(this.PanelRecepcionista_Load);
        }
    }
}
