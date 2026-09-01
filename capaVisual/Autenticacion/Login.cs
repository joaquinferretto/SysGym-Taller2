using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaLogica;
using exxen2._0.capaVisual.Dashboards;

namespace exxen2._0.capaVisual.Autenticacion
{
    public partial class Login : Form
    {
        private readonly UsuarioSistemaLogica usuarioSistemaLogica;

        public Login()
        {
            InitializeComponent();
            usuarioSistemaLogica = new UsuarioSistemaLogica();
            AcceptButton = btnIngresar;
            CancelButton = btnSalir;
        }

        private void btnIngresar_Click(object sender, System.EventArgs e)
        {
            try
            {
                var username = txtUsername.Text.Trim();
                var password = txtPassword.Text;

                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("Ingresá usuario y contraseña.", "Inicio de sesión",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var usuario = usuarioSistemaLogica.Autenticar(username, password);
                if (usuario == null)
                {
                    MessageBox.Show("Usuario o contraseña incorrectos.", "Inicio de sesión",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Clear();
                    txtPassword.Focus();
                    return;
                }

                var dashboard = CrearDashboard(usuario);
                Hide();
                dashboard.FormClosed += delegate { Close(); };
                dashboard.Show();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("No se pudo iniciar sesión.\n\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalir_Click(object sender, System.EventArgs e)
        {
            Close();
        }

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
                    throw new System.InvalidOperationException("El rol del usuario no tiene un dashboard configurado.");
            }
        }
    }
}
