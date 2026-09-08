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
        private Button btnCambiarCuenta;
        private Panel panelMenu;
        private Panel panelOpciones;
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

            panelEncabezado.Name = "panelEncabezado";

            panelEncabezado.TabIndex = 0;

            lblMarca.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblMarca.ForeColor = Color.White;

            lblMarca.Name = "lblMarca";

            lblMarca.TabIndex = 0;
            lblMarca.Text = "SYSGYM";

            lblUsuarioRol.ForeColor = Color.FromArgb(207, 250, 254);

            lblUsuarioRol.Name = "lblUsuarioRol";

            lblUsuarioRol.TabIndex = 1;
            lblUsuarioRol.Text = "Usuario: Entrenador de diseno    |    Rol: Entrenador";

            btnCambiarCuenta.BackColor = Color.White;
            btnCambiarCuenta.FlatAppearance.BorderSize = 0;
            btnCambiarCuenta.FlatStyle = FlatStyle.Flat;
            btnCambiarCuenta.ForeColor = Color.FromArgb(14, 116, 144);

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
            panelOpciones.Controls.Add(lblTrabajo);
            panelOpciones.Controls.Add(btnSocios);
            panelOpciones.Controls.Add(btnRutinas);
            panelOpciones.Controls.Add(lblCatalogo);
            panelOpciones.Controls.Add(btnEjercicios);
            panelOpciones.Controls.Add(lblControl);
            panelOpciones.Controls.Add(btnAsistencias);

            panelOpciones.Name = "panelOpciones";
            panelOpciones.Padding = new Padding(14, 18, 14, 18);

            panelOpciones.TabIndex = 0;

            lblTrabajo.Font = new Font("Segoe UI Semibold", 8F, FontStyle.Bold); lblTrabajo.ForeColor = Color.FromArgb(100, 116, 139);  lblTrabajo.Margin = new Padding(0, 8, 0, 4); lblTrabajo.Name = "lblTrabajo";  lblTrabajo.TabIndex = 0; lblTrabajo.Text = "MI TRABAJO";
            btnSocios.BackColor = Color.FromArgb(248, 250, 252); btnSocios.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240); btnSocios.FlatStyle = FlatStyle.Flat; btnSocios.Font = new Font("Segoe UI", 9.5F); btnSocios.ForeColor = Color.FromArgb(51, 65, 85);  btnSocios.Margin = new Padding(0, 0, 0, 5); btnSocios.Name = "btnSocios";  btnSocios.TabIndex = 1; btnSocios.Text = "Mis socios"; btnSocios.TextAlign = ContentAlignment.MiddleLeft; btnSocios.UseVisualStyleBackColor = false;
            btnRutinas.BackColor = Color.FromArgb(248, 250, 252); btnRutinas.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240); btnRutinas.FlatStyle = FlatStyle.Flat; btnRutinas.Font = new Font("Segoe UI", 9.5F); btnRutinas.ForeColor = Color.FromArgb(51, 65, 85);  btnRutinas.Margin = new Padding(0, 0, 0, 5); btnRutinas.Name = "btnRutinas";  btnRutinas.TabIndex = 2; btnRutinas.Text = "Rutinas"; btnRutinas.TextAlign = ContentAlignment.MiddleLeft; btnRutinas.UseVisualStyleBackColor = false;
            lblCatalogo.Font = new Font("Segoe UI Semibold", 8F, FontStyle.Bold); lblCatalogo.ForeColor = Color.FromArgb(100, 116, 139);  lblCatalogo.Margin = new Padding(0, 8, 0, 4); lblCatalogo.Name = "lblCatalogo";  lblCatalogo.TabIndex = 3; lblCatalogo.Text = "CATALOGO";
            btnEjercicios.BackColor = Color.FromArgb(248, 250, 252); btnEjercicios.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240); btnEjercicios.FlatStyle = FlatStyle.Flat; btnEjercicios.Font = new Font("Segoe UI", 9.5F); btnEjercicios.ForeColor = Color.FromArgb(51, 65, 85);  btnEjercicios.Margin = new Padding(0, 0, 0, 5); btnEjercicios.Name = "btnEjercicios";  btnEjercicios.TabIndex = 4; btnEjercicios.Text = "Ejercicios"; btnEjercicios.TextAlign = ContentAlignment.MiddleLeft; btnEjercicios.UseVisualStyleBackColor = false;
            lblControl.Font = new Font("Segoe UI Semibold", 8F, FontStyle.Bold); lblControl.ForeColor = Color.FromArgb(100, 116, 139);  lblControl.Margin = new Padding(0, 8, 0, 4); lblControl.Name = "lblControl";  lblControl.TabIndex = 5; lblControl.Text = "CONTROL DE ACCESO";
            btnAsistencias.BackColor = Color.FromArgb(248, 250, 252); btnAsistencias.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240); btnAsistencias.FlatStyle = FlatStyle.Flat; btnAsistencias.Font = new Font("Segoe UI", 9.5F); btnAsistencias.ForeColor = Color.FromArgb(51, 65, 85);  btnAsistencias.Margin = new Padding(0, 0, 0, 5); btnAsistencias.Name = "btnAsistencias";  btnAsistencias.TabIndex = 6; btnAsistencias.Text = "Asistencias"; btnAsistencias.TextAlign = ContentAlignment.MiddleLeft; btnAsistencias.UseVisualStyleBackColor = false;

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
            this.Name = "PanelEntrenador";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SysGym - Entrenador";
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
            this.lblUsuarioRol.Size = new System.Drawing.Size(315, 23);
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
            this.lblTrabajo.AutoSize = false;
            this.lblTrabajo.Dock = System.Windows.Forms.DockStyle.None;
            this.lblTrabajo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblTrabajo.Location = new System.Drawing.Point(14, 26);
            this.lblTrabajo.Size = new System.Drawing.Size(214, 24);
            this.btnSocios.AutoSize = false;
            this.btnSocios.Dock = System.Windows.Forms.DockStyle.None;
            this.btnSocios.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.btnSocios.Location = new System.Drawing.Point(14, 54);
            this.btnSocios.Size = new System.Drawing.Size(214, 40);
            this.btnRutinas.AutoSize = false;
            this.btnRutinas.Dock = System.Windows.Forms.DockStyle.None;
            this.btnRutinas.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.btnRutinas.Location = new System.Drawing.Point(14, 99);
            this.btnRutinas.Size = new System.Drawing.Size(214, 40);
            this.lblCatalogo.AutoSize = false;
            this.lblCatalogo.Dock = System.Windows.Forms.DockStyle.None;
            this.lblCatalogo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblCatalogo.Location = new System.Drawing.Point(14, 152);
            this.lblCatalogo.Size = new System.Drawing.Size(214, 24);
            this.btnEjercicios.AutoSize = false;
            this.btnEjercicios.Dock = System.Windows.Forms.DockStyle.None;
            this.btnEjercicios.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.btnEjercicios.Location = new System.Drawing.Point(14, 180);
            this.btnEjercicios.Size = new System.Drawing.Size(214, 40);
            this.lblControl.AutoSize = false;
            this.lblControl.Dock = System.Windows.Forms.DockStyle.None;
            this.lblControl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblControl.Location = new System.Drawing.Point(14, 233);
            this.lblControl.Size = new System.Drawing.Size(214, 24);
            this.btnAsistencias.AutoSize = false;
            this.btnAsistencias.Dock = System.Windows.Forms.DockStyle.None;
            this.btnAsistencias.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.btnAsistencias.Location = new System.Drawing.Point(14, 261);
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
            this.btnRutinas.Click += new System.EventHandler(this.btnRutinas_Click);
            this.btnEjercicios.Click += new System.EventHandler(this.btnEjercicios_Click);
            this.btnAsistencias.Click += new System.EventHandler(this.btnAsistencias_Click);
                    this.Load += new System.EventHandler(this.PanelEntrenador_Load);
        }
    }
}
