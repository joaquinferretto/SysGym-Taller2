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
    /* Presenta login y atiende sus acciones mediante eventos de Windows Forms. */
    public partial class Login : Form
    {
        private readonly UsuarioSistemaLogica usuarioSistemaLogica;
        private readonly ErrorProvider errorProvider;
        /* Inicializa los componentes existentes y las dependencias de la pantalla sin consultar la base de datos. */
        public Login()
        {
            InitializeComponent();
            usuarioSistemaLogica = new UsuarioSistemaLogica();
            errorProvider = new ErrorProvider(this)
            {
                BlinkStyle = ErrorBlinkStyle.NeverBlink
            };
            components.Add(errorProvider);
            AcceptButton = btnIngresar;
            CancelButton = btnSalir;
        }

        /* Al hacer clic en btnIngresar, valida las credenciales y abre el dashboard del rol autenticado. */
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarCampos())
                    return;
                var usuario = usuarioSistemaLogica.Autenticar(txtUsername.Text.Trim(), txtPassword.Text);
                if (usuario == null)
                {
                    errorProvider.SetError(txtPassword, "El usuario o la contrasena no son correctos.");
                    MessageBox.Show("Usuario o contrasena incorrectos.", "Inicio de sesion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Clear();
                    txtPassword.Focus();
                    return;
                }

                var dashboard = CrearDashboard(usuario);
                errorProvider.Clear();
                Hide();
                dashboard.FormClosed += new FormClosedEventHandler(dashboard_FormClosed);
                dashboard.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo iniciar sesion.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /* Al hacer clic en btnSalir, inicia el cierre de la pantalla. */
        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        /* Al cerrar el dashboard, vuelve al acceso si se solicitó cambiar de cuenta o cierra la aplicación. */
        private void dashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            var dashboardSesion = sender as IDashboardSesion;
            if (dashboardSesion != null && dashboardSesion.CambioCuentaSolicitado)
            {
                txtPassword.Clear();
                errorProvider.Clear();
                Show();
                BringToFront();
                Activate();
                txtUsername.Focus();
                return;
            }

            Close();
        }

        /* Al escribir el usuario, aplica la restricción de letras del formulario de acceso. */
        private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
                System.Media.SystemSounds.Beep.Play();
            }
        }

        /* Al validar el campo de acceso, informa los datos faltantes o inválidos mediante ErrorProvider. */
        private void txtUsername_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
                errorProvider.SetError(txtUsername, "Ingresa el nombre de usuario.");
            else if (txtUsername.Text.Any(c => !char.IsLetter(c)))
                errorProvider.SetError(txtUsername, "El usuario solo puede contener letras.");
            else
                errorProvider.SetError(txtUsername, string.Empty);
        }

        /* Al validar el campo de acceso, informa los datos faltantes o inválidos mediante ErrorProvider. */
        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.SetError(txtPassword, string.IsNullOrWhiteSpace(txtPassword.Text) ? "Ingresa la contrasena." : string.Empty);
        }

        /* Al escribir la contraseña, actualiza la indicación existente y limpia su error de validación. */
        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            lblPasswordVisible.Text = string.IsNullOrEmpty(txtPassword.Text) ? string.Empty : "Contrasena visible: " + txtPassword.Text;
            if (!string.IsNullOrWhiteSpace(txtPassword.Text))
                errorProvider.SetError(txtPassword, string.Empty);
        }

        /* Comprueba los campos de acceso y devuelve si están listos para autenticar al usuario. */
        private bool ValidarCampos()
        {
            txtUsername_Validating(txtUsername, new CancelEventArgs());
            txtPassword_Validating(txtPassword, new CancelEventArgs());
            return string.IsNullOrEmpty(errorProvider.GetError(txtUsername)) && string.IsNullOrEmpty(errorProvider.GetError(txtPassword));
        }

        /* Selecciona el dashboard existente correspondiente al rol del usuario autenticado. */
        private static Form CrearDashboard(UsuarioSistema usuario)
        {
            switch (usuario.Rol.Descripcion.Trim().ToUpperInvariant())
            {
                case "ADMINISTRADOR":
                    return new DashboardAdministrador(usuario);
                case "RECEPCIONISTA":
                    return new DashboardRecepcionista(usuario);
                case "ENTRENADOR":
                    return new DashboardEntrenador(usuario);
                default:
                    throw new InvalidOperationException("El rol del usuario no tiene un dashboard configurado.");
            }
        }
    }
}
