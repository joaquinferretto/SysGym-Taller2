using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaLogica;
using exxen2._0.capaVisual.Administrador;
using exxen2._0.capaVisual.Compartido;
using exxen2._0.capaVisual.Entrenador;
using exxen2._0.capaVisual.Recepcionista;

namespace exxen2._0.capaVisual.Autenticacion
{
    public partial class Login : Form
    {
        private readonly UsuarioSistemaLogica usuarioSistemaLogica;
        private readonly ErrorProvider errorProvider;

        public Login()
        {
            InitializeComponent();
            usuarioSistemaLogica = new UsuarioSistemaLogica();
            errorProvider = new ErrorProvider(this) { BlinkStyle = ErrorBlinkStyle.NeverBlink };
            txtUsername.KeyPress += txtUsername_KeyPress;
            txtUsername.Validating += txtUsername_Validating;
            txtPassword.Validating += txtPassword_Validating;
            txtPassword.TextChanged += txtPassword_TextChanged;
            AcceptButton = btnIngresar;
            CancelButton = btnSalir;
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarCampos()) return;
                var usuario = usuarioSistemaLogica.Autenticar(txtUsername.Text.Trim(), txtPassword.Text);
                if (usuario == null)
                {
                    errorProvider.SetError(txtPassword, "El usuario o la contrasena no son correctos.");
                    MessageBox.Show("Usuario o contrasena incorrectos.", "Inicio de sesion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Clear(); txtPassword.Focus(); return;
                }

                var dashboard = CrearDashboard(usuario);
                errorProvider.Clear(); Hide();
                dashboard.FormClosed += delegate
                {
                    var dashboardSesion = dashboard as IDashboardSesion;
                    if (dashboardSesion != null && dashboardSesion.CambioCuentaSolicitado)
                    {
                        txtPassword.Clear(); errorProvider.Clear(); Show(); BringToFront(); Activate(); txtUsername.Focus(); return;
                    }
                    Close();
                };
                dashboard.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo iniciar sesion.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e) { Close(); }
        private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar)) { e.Handled = true; System.Media.SystemSounds.Beep.Play(); }
        }

        private void txtUsername_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text)) errorProvider.SetError(txtUsername, "Ingresa el nombre de usuario.");
            else if (txtUsername.Text.Any(c => !char.IsLetter(c))) errorProvider.SetError(txtUsername, "El usuario solo puede contener letras.");
            else errorProvider.SetError(txtUsername, string.Empty);
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e) { errorProvider.SetError(txtPassword, string.IsNullOrWhiteSpace(txtPassword.Text) ? "Ingresa la contrasena." : string.Empty); }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            lblPasswordVisible.Text = string.IsNullOrEmpty(txtPassword.Text) ? string.Empty : "Contrasena visible: " + txtPassword.Text;
            if (!string.IsNullOrWhiteSpace(txtPassword.Text)) errorProvider.SetError(txtPassword, string.Empty);
        }

        private bool ValidarCampos()
        {
            txtUsername_Validating(txtUsername, new CancelEventArgs()); txtPassword_Validating(txtPassword, new CancelEventArgs());
            return string.IsNullOrEmpty(errorProvider.GetError(txtUsername)) && string.IsNullOrEmpty(errorProvider.GetError(txtPassword));
        }

        private static Form CrearDashboard(UsuarioSistema usuario)
        {
            switch (usuario.Rol.Descripcion.Trim().ToUpperInvariant())
            {
                case "ADMINISTRADOR": return new DashboardAdministrador(usuario);
                case "RECEPCIONISTA": return new DashboardRecepcionista(usuario);
                case "ENTRENADOR": return new DashboardEntrenador(usuario);
                default: throw new InvalidOperationException("El rol del usuario no tiene un dashboard configurado.");
            }
        }
    }
}
