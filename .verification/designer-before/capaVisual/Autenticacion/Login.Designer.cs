using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace exxen2._0.capaVisual.Autenticacion
{
    partial class Login
    {
        private IContainer components;
        private Label lblTitulo;
        private Label lblSubtitulo;
        private Label lblUsername;
        private Label lblPassword;
        private Label lblPasswordVisible;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnIngresar;
        private Button btnSalir;
        private Panel barraSuperior;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new Container(); lblTitulo = new Label(); lblSubtitulo = new Label(); lblUsername = new Label(); lblPassword = new Label(); lblPasswordVisible = new Label(); txtUsername = new TextBox(); txtPassword = new TextBox(); btnIngresar = new Button(); btnSalir = new Button(); barraSuperior = new Panel(); SuspendLayout();
            BackColor = Color.FromArgb(15, 23, 42); ClientSize = new Size(520, 385); Font = new Font("Segoe UI", 10F); FormBorderStyle = FormBorderStyle.FixedSingle; MaximizeBox = false; Name = "Login"; StartPosition = FormStartPosition.CenterScreen; Text = "SysGym | Inicio de sesion";
            lblTitulo.AutoSize = true; lblTitulo.Font = new Font("Segoe UI", 28F, FontStyle.Bold); lblTitulo.ForeColor = Color.White; lblTitulo.Location = new Point(168, 18); lblTitulo.Name = "lblTitulo"; lblTitulo.Size = new Size(184, 51); lblTitulo.TabIndex = 0; lblTitulo.Text = "SYSGYM";
            lblSubtitulo.AutoSize = true; lblSubtitulo.Font = new Font("Segoe UI", 10F); lblSubtitulo.ForeColor = Color.FromArgb(186, 230, 253); lblSubtitulo.Location = new Point(148, 72); lblSubtitulo.Name = "lblSubtitulo"; lblSubtitulo.Size = new Size(224, 19); lblSubtitulo.TabIndex = 1; lblSubtitulo.Text = "Gestion simple, segura y organizada";
            lblUsername.AutoSize = true; lblUsername.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold); lblUsername.ForeColor = Color.FromArgb(226, 232, 240); lblUsername.Location = new Point(100, 121); lblUsername.Name = "lblUsername"; lblUsername.Size = new Size(57, 19); lblUsername.TabIndex = 2; lblUsername.Text = "Usuario";
            txtUsername.BackColor = Color.White; txtUsername.BorderStyle = BorderStyle.FixedSingle; txtUsername.Font = new Font("Segoe UI", 11F); txtUsername.ForeColor = Color.FromArgb(15, 23, 42); txtUsername.Location = new Point(100, 145); txtUsername.Name = "txtUsername"; txtUsername.Size = new Size(320, 27); txtUsername.TabIndex = 0;
            lblPassword.AutoSize = true; lblPassword.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold); lblPassword.ForeColor = Color.FromArgb(226, 232, 240); lblPassword.Location = new Point(100, 213); lblPassword.Name = "lblPassword"; lblPassword.Size = new Size(83, 19); lblPassword.TabIndex = 3; lblPassword.Text = "Contrasena";
            txtPassword.BackColor = Color.White; txtPassword.BorderStyle = BorderStyle.FixedSingle; txtPassword.Font = new Font("Segoe UI", 11F); txtPassword.ForeColor = Color.FromArgb(15, 23, 42); txtPassword.Location = new Point(100, 237); txtPassword.Name = "txtPassword"; txtPassword.Size = new Size(320, 27); txtPassword.TabIndex = 1; txtPassword.UseSystemPasswordChar = true;
            lblPasswordVisible.AutoSize = true; lblPasswordVisible.Font = new Font("Segoe UI", 8.5F); lblPasswordVisible.ForeColor = Color.FromArgb(148, 163, 184); lblPasswordVisible.Location = new Point(100, 271); lblPasswordVisible.Name = "lblPasswordVisible"; lblPasswordVisible.Size = new Size(0, 17); lblPasswordVisible.TabIndex = 4; lblPasswordVisible.Text = string.Empty;
            btnIngresar.BackColor = Color.FromArgb(20, 184, 166); btnIngresar.FlatStyle = FlatStyle.Flat; btnIngresar.FlatAppearance.BorderSize = 0; btnIngresar.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold); btnIngresar.ForeColor = Color.White; btnIngresar.Location = new Point(100, 298); btnIngresar.Name = "btnIngresar"; btnIngresar.Size = new Size(155, 38); btnIngresar.TabIndex = 2; btnIngresar.Text = "Ingresar"; btnIngresar.UseVisualStyleBackColor = false; btnIngresar.Click += btnIngresar_Click;
            btnSalir.BackColor = Color.FromArgb(51, 65, 85); btnSalir.DialogResult = DialogResult.Cancel; btnSalir.FlatStyle = FlatStyle.Flat; btnSalir.FlatAppearance.BorderSize = 0; btnSalir.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold); btnSalir.ForeColor = Color.FromArgb(226, 232, 240); btnSalir.Location = new Point(265, 298); btnSalir.Name = "btnSalir"; btnSalir.Size = new Size(155, 38); btnSalir.TabIndex = 3; btnSalir.Text = "Salir"; btnSalir.UseVisualStyleBackColor = false; btnSalir.Click += btnSalir_Click;
            barraSuperior.BackColor = Color.FromArgb(20, 184, 166); barraSuperior.Dock = DockStyle.Top; barraSuperior.Height = 7; barraSuperior.Location = new Point(0, 0); barraSuperior.Name = "barraSuperior"; barraSuperior.Size = new Size(520, 7); barraSuperior.TabIndex = 5;
            Controls.Add(btnSalir); Controls.Add(btnIngresar); Controls.Add(lblPasswordVisible); Controls.Add(txtPassword); Controls.Add(lblPassword); Controls.Add(txtUsername); Controls.Add(lblUsername); Controls.Add(lblSubtitulo); Controls.Add(lblTitulo); Controls.Add(barraSuperior); AcceptButton = btnIngresar; CancelButton = btnSalir; ResumeLayout(false); PerformLayout();
        }
    }
}
