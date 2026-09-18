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
        private Label lblBienvenida;

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
            lblBienvenida = new Label();
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

            panelContenido.BackColor = Color.FromArgb(226, 232, 240); panelContenido.Controls.Add(lblBienvenida);
            lblBienvenida.Name = "lblBienvenida"; lblBienvenida.TabIndex = 0; lblBienvenida.Font = new Font("Segoe UI", 12F); lblBienvenida.ForeColor = Color.FromArgb(100, 116, 139); lblBienvenida.Text = "Panel de recepcion\r\n\r\nElegi una opcion del menu lateral para comenzar a trabajar.";   panelContenido.Name = "panelContenido";  panelContenido.TabIndex = 2;

            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;

            BackColor = Color.FromArgb(241, 245, 249);
            ClientSize = new Size(1200, 760);
            Controls.Add(panelContenido);
            Controls.Add(panelMenu);
            Controls.Add(panelEncabezado);
            Font = new Font("Segoe UI", 10F);
            MinimumSize = new Size(1100, 700);
            this.Name = "PanelRecepcionista";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SysGym - Recepcionista";
            // Layout base: encabezado fijo, menu lateral estable y contenido restante.

            // Distribucion general: encabezado superior, menu lateral fijo y contenido que ocupa el resto de la ventana.

            panelEncabezado.Padding = new Padding(24, 8, 24, 8);

            lblMarca.Margin = new Padding(0);
            lblMarca.TextAlign = ContentAlignment.BottomLeft;

            lblUsuarioRol.Margin = new Padding(0);
            lblUsuarioRol.TextAlign = ContentAlignment.TopLeft;

            btnCambiarCuenta.Margin = new Padding(16, 0, 0, 0);

            panelMenu.MinimumSize = new Size(264, 0);

            panelPie.Padding = new Padding(14, 12, 14, 16);

            btnSalir.Padding = new Padding(12, 0, 0, 0);

            panelOpciones.Padding = new Padding(14, 10, 14, 10);

            lblClientes.Margin = new Padding(0, 8, 0, 2);
            lblClientes.TextAlign = ContentAlignment.MiddleLeft;

            btnSocios.Margin = new Padding(0, 0, 0, 5);
            btnSocios.Padding = new Padding(12, 0, 0, 0);

            btnMembresias.Margin = new Padding(0, 0, 0, 5);
            btnMembresias.Padding = new Padding(12, 0, 0, 0);

            lblCaja.Margin = new Padding(0, 8, 0, 2);
            lblCaja.TextAlign = ContentAlignment.MiddleLeft;

            btnPagos.Margin = new Padding(0, 0, 0, 5);
            btnPagos.Padding = new Padding(12, 0, 0, 0);

            lblEntrenadores.Margin = new Padding(0, 8, 0, 2);
            lblEntrenadores.TextAlign = ContentAlignment.MiddleLeft;

            btnAsignar.Margin = new Padding(0, 0, 0, 5);
            btnAsignar.Padding = new Padding(12, 0, 0, 0);

            btnConsultar.Margin = new Padding(0, 0, 0, 5);
            btnConsultar.Padding = new Padding(12, 0, 0, 0);

            lblControl.Margin = new Padding(0, 8, 0, 2);
            lblControl.TextAlign = ContentAlignment.MiddleLeft;

            btnAsistencias.Margin = new Padding(0, 0, 0, 5);
            btnAsistencias.Padding = new Padding(12, 0, 0, 0);

            panelContenido.Padding = new Padding(0);

            lblBienvenida.TextAlign = ContentAlignment.MiddleCenter;

            this.AutoScroll = false;
            this.panelEncabezado.AutoSize = false;
            this.panelEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEncabezado.Padding = new System.Windows.Forms.Padding(24, 8, 24, 8);
            this.panelEncabezado.Size = new System.Drawing.Size(1200, 90);
            this.lblMarca.AutoSize = false;
            this.lblMarca.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblMarca.Location = new System.Drawing.Point(24, 8);
            this.lblMarca.Size = new System.Drawing.Size(900, 36);
            this.lblUsuarioRol.AutoSize = false;
            this.lblUsuarioRol.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblUsuarioRol.Location = new System.Drawing.Point(24, 48);
            this.lblUsuarioRol.Size = new System.Drawing.Size(900, 26);
            this.btnCambiarCuenta.AutoSize = false;
            this.btnCambiarCuenta.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnCambiarCuenta.Location = new System.Drawing.Point(1000, 24);
            this.btnCambiarCuenta.Size = new System.Drawing.Size(176, 38);
            this.panelMenu.AutoSize = false;
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.MinimumSize = new System.Drawing.Size(264, 0);
            this.panelMenu.Size = new System.Drawing.Size(264, 670);
            this.panelOpciones.AutoSize = false;
            this.panelOpciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOpciones.AutoScroll = true;
            this.panelOpciones.Padding = new System.Windows.Forms.Padding(14, 10, 14, 10);
            this.lblClientes.AutoSize = false;
            this.lblClientes.Location = new System.Drawing.Point(14, 18);
            this.lblClientes.Size = new System.Drawing.Size(236, 22);
            this.lblClientes.Tag = "CLIENTES";
            this.lblClientes.Text = "▼ CLIENTES";
            this.btnSocios.Size = new System.Drawing.Size(236, 38);
            this.btnSocios.Location = new System.Drawing.Point(14, 42);
            this.btnMembresias.Size = new System.Drawing.Size(236, 38);
            this.btnMembresias.Location = new System.Drawing.Point(14, 85);
            this.lblCaja.AutoSize = false;
            this.lblCaja.Location = new System.Drawing.Point(14, 136);
            this.lblCaja.Size = new System.Drawing.Size(236, 22);
            this.lblCaja.Tag = "CAJA";
            this.lblCaja.Text = "▶ CAJA";
            this.lblEntrenadores.AutoSize = false;
            this.lblEntrenadores.Location = new System.Drawing.Point(14, 166);
            this.lblEntrenadores.Size = new System.Drawing.Size(236, 22);
            this.lblEntrenadores.Tag = "ENTRENADORES";
            this.lblEntrenadores.Text = "▶ ENTRENADORES";
            this.lblControl.AutoSize = false;
            this.lblControl.Location = new System.Drawing.Point(14, 196);
            this.lblControl.Size = new System.Drawing.Size(236, 22);
            this.lblControl.Tag = "CONTROL DE ACCESO";
            this.lblControl.Text = "▶ CONTROL DE ACCESO";
            this.btnPagos.Visible = false;
            this.btnAsignar.Visible = false;
            this.btnConsultar.Visible = false;
            this.btnAsistencias.Visible = false;
            this.panelPie.AutoSize = false;
            this.panelPie.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelPie.Size = new System.Drawing.Size(264, 76);
            this.panelPie.Padding = new System.Windows.Forms.Padding(14, 12, 14, 16);
            this.btnSalir.AutoSize = false;
            this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.btnSalir.Location = new System.Drawing.Point(14, 14);
            this.btnSalir.Size = new System.Drawing.Size(236, 46);
            this.panelContenido.AutoSize = false;
            this.panelContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenido.AutoScroll = false;
            this.panelContenido.Size = new System.Drawing.Size(936, 670);
            this.lblBienvenida.AutoSize = false;
            this.lblBienvenida.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBienvenida.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
