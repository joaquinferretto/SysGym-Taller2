using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Autenticacion
{
    partial class InicioSesion
    {
        private IContainer components;
        private Label lblTitulo;
        private Label lblSubtitulo;
        private Label lblNombreUsuario;
        private Label lblClave;
        private Label lblClaveVisible;
        private TextBox txtNombreUsuario;
        private TextBox txtClave;
        private Button btnIngresar;
        private Button btnSalir;
        private Panel barraSuperior;

        protected override void Dispose(bool liberarRecursos)
        {
            if (liberarRecursos && components != null) components.Dispose();
            base.Dispose(liberarRecursos);
        }

        private void InitializeComponent()
        {
            components = new Container(); lblTitulo = new Label(); lblSubtitulo = new Label(); lblNombreUsuario = new Label(); lblClave = new Label(); lblClaveVisible = new Label(); txtNombreUsuario = new TextBox(); txtClave = new TextBox(); btnIngresar = new Button(); btnSalir = new Button(); barraSuperior = new Panel(); SuspendLayout();
            BackColor = Color.FromArgb(15, 23, 42); ClientSize = new Size(520, 385); Font = new Font("Segoe UI", 10F); FormBorderStyle = FormBorderStyle.FixedSingle; MaximizeBox = false; this.Name = "InicioSesion"; StartPosition = FormStartPosition.CenterScreen; Text = "SysGym | Inicio de sesion";
             lblTitulo.Font = new Font("Segoe UI", 28F, FontStyle.Bold); lblTitulo.ForeColor = Color.White;  lblTitulo.Name = "lblTitulo";  lblTitulo.TabIndex = 0; lblTitulo.Text = "SYSGYM";
             lblSubtitulo.Font = new Font("Segoe UI", 10F); lblSubtitulo.ForeColor = Color.FromArgb(186, 230, 253);  lblSubtitulo.Name = "lblSubtitulo";  lblSubtitulo.TabIndex = 1; lblSubtitulo.Text = "Gestion simple, segura y organizada";
             lblNombreUsuario.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold); lblNombreUsuario.ForeColor = Color.FromArgb(226, 232, 240);  lblNombreUsuario.Name = "lblNombreUsuario";  lblNombreUsuario.TabIndex = 2; lblNombreUsuario.Text = "Usuario";
            txtNombreUsuario.BackColor = Color.White; txtNombreUsuario.BorderStyle = BorderStyle.FixedSingle; txtNombreUsuario.Font = new Font("Segoe UI", 11F); txtNombreUsuario.ForeColor = Color.FromArgb(15, 23, 42);  txtNombreUsuario.Name = "txtNombreUsuario";  txtNombreUsuario.TabIndex = 0;
             lblClave.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold); lblClave.ForeColor = Color.FromArgb(226, 232, 240);  lblClave.Name = "lblClave";  lblClave.TabIndex = 3; lblClave.Text = "Contrasena";
            txtClave.BackColor = Color.White; txtClave.BorderStyle = BorderStyle.FixedSingle; txtClave.Font = new Font("Segoe UI", 11F); txtClave.ForeColor = Color.FromArgb(15, 23, 42);  txtClave.Name = "txtClave";  txtClave.TabIndex = 1; txtClave.UseSystemPasswordChar = true;
             lblClaveVisible.Font = new Font("Segoe UI", 8.5F); lblClaveVisible.ForeColor = Color.FromArgb(148, 163, 184);  lblClaveVisible.Name = "lblClaveVisible";  lblClaveVisible.TabIndex = 4; lblClaveVisible.Text = string.Empty;
            btnIngresar.BackColor = Color.FromArgb(20, 184, 166); btnIngresar.FlatStyle = FlatStyle.Flat; btnIngresar.FlatAppearance.BorderSize = 0; btnIngresar.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold); btnIngresar.ForeColor = Color.White;  btnIngresar.Name = "btnIngresar";  btnIngresar.TabIndex = 2; btnIngresar.Text = "Ingresar"; btnIngresar.UseVisualStyleBackColor = false; 
            btnSalir.BackColor = Color.FromArgb(51, 65, 85); btnSalir.DialogResult = DialogResult.Cancel; btnSalir.FlatStyle = FlatStyle.Flat; btnSalir.FlatAppearance.BorderSize = 0; btnSalir.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold); btnSalir.ForeColor = Color.FromArgb(226, 232, 240);  btnSalir.Name = "btnSalir";  btnSalir.TabIndex = 3; btnSalir.Text = "Salir"; btnSalir.UseVisualStyleBackColor = false; 
            barraSuperior.BackColor = Color.FromArgb(20, 184, 166);    barraSuperior.Name = "barraSuperior";  barraSuperior.TabIndex = 5;
            Controls.Add(btnSalir); Controls.Add(btnIngresar); Controls.Add(lblClaveVisible); Controls.Add(txtClave); Controls.Add(lblClave); Controls.Add(txtNombreUsuario); Controls.Add(lblNombreUsuario); Controls.Add(lblSubtitulo); Controls.Add(lblTitulo); Controls.Add(barraSuperior); AcceptButton = btnIngresar; CancelButton = btnSalir; 
            this.lblTitulo.AutoSize = false;
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.None;
            this.lblTitulo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblTitulo.Location = new System.Drawing.Point(168, 18);
            this.lblTitulo.Size = new System.Drawing.Size(167, 58);
            this.lblSubtitulo.AutoSize = false;
            this.lblSubtitulo.Dock = System.Windows.Forms.DockStyle.None;
            this.lblSubtitulo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblSubtitulo.Location = new System.Drawing.Point(148, 72);
            this.lblSubtitulo.Size = new System.Drawing.Size(223, 23);
            this.lblNombreUsuario.AutoSize = false;
            this.lblNombreUsuario.Dock = System.Windows.Forms.DockStyle.None;
            this.lblNombreUsuario.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblNombreUsuario.Location = new System.Drawing.Point(100, 121);
            this.lblNombreUsuario.Size = new System.Drawing.Size(53, 23);
            this.lblClave.AutoSize = false;
            this.lblClave.Dock = System.Windows.Forms.DockStyle.None;
            this.lblClave.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblClave.Location = new System.Drawing.Point(100, 213);
            this.lblClave.Size = new System.Drawing.Size(75, 23);
            this.lblClaveVisible.AutoSize = false;
            this.lblClaveVisible.Dock = System.Windows.Forms.DockStyle.None;
            this.lblClaveVisible.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblClaveVisible.Location = new System.Drawing.Point(100, 271);
            this.lblClaveVisible.Size = new System.Drawing.Size(180, 18);
            this.txtNombreUsuario.AutoSize = false;
            this.txtNombreUsuario.Dock = System.Windows.Forms.DockStyle.None;
            this.txtNombreUsuario.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.txtNombreUsuario.Location = new System.Drawing.Point(100, 145);
            this.txtNombreUsuario.Size = new System.Drawing.Size(320, 27);
            this.txtClave.AutoSize = false;
            this.txtClave.Dock = System.Windows.Forms.DockStyle.None;
            this.txtClave.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.txtClave.Location = new System.Drawing.Point(100, 237);
            this.txtClave.Size = new System.Drawing.Size(320, 27);
            this.btnIngresar.AutoSize = false;
            this.btnIngresar.Dock = System.Windows.Forms.DockStyle.None;
            this.btnIngresar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.btnIngresar.Location = new System.Drawing.Point(100, 298);
            this.btnIngresar.Size = new System.Drawing.Size(155, 38);
            this.btnSalir.AutoSize = false;
            this.btnSalir.Dock = System.Windows.Forms.DockStyle.None;
            this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.btnSalir.Location = new System.Drawing.Point(265, 298);
            this.btnSalir.Size = new System.Drawing.Size(155, 38);
            this.barraSuperior.AutoSize = false;
            this.barraSuperior.Dock = System.Windows.Forms.DockStyle.None;
            this.barraSuperior.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.barraSuperior.Location = new System.Drawing.Point(0, 0);
            this.barraSuperior.Size = new System.Drawing.Size(520, 7);
            ResumeLayout(false); PerformLayout();

            this.txtNombreUsuario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNombreUsuario_KeyPress);
            this.txtNombreUsuario.Validating += new System.ComponentModel.CancelEventHandler(this.txtNombreUsuario_Validating);
            this.txtClave.Validating += new System.ComponentModel.CancelEventHandler(this.txtClave_Validating);
            this.txtClave.TextChanged += new System.EventHandler(this.txtClave_TextChanged);
            this.btnIngresar.Click += new System.EventHandler(this.btnIngresar_Click);
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
                }
    }
}
