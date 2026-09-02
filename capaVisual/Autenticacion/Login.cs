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
        private Label lblSubtitulo;
        private Label lblPasswordVisible;

        public Login()
        {
            InitializeComponent();
            usuarioSistemaLogica = new UsuarioSistemaLogica();
            errorProvider = new ErrorProvider(this)
            {
                BlinkStyle = ErrorBlinkStyle.NeverBlink
            };

            ConfigurarEstilo();
            txtUsername.KeyPress += txtUsername_KeyPress;
            txtUsername.Validating += txtUsername_Validating;
            txtPassword.Validating += txtPassword_Validating;
            txtPassword.TextChanged += txtPassword_TextChanged;
            Load += Login_Load;
            Resize += Login_Resize;
            AcceptButton = btnIngresar;
            CancelButton = btnSalir;
        }

        private void btnIngresar_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (!ValidarCampos())
                {
                    return;
                }

                var username = txtUsername.Text.Trim();
                var password = txtPassword.Text;

                var usuario = usuarioSistemaLogica.Autenticar(username, password);
                if (usuario == null)
                {
                    errorProvider.SetError(txtPassword, "El usuario o la contraseña no son correctos.");
                    MessageBox.Show("Usuario o contraseña incorrectos.", "Inicio de sesión",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Clear();
                    txtPassword.Focus();
                    return;
                }

                var dashboard = CrearDashboard(usuario);
                errorProvider.Clear();
                Hide();
                dashboard.FormClosed += delegate
                {
                    var dashboardBase = dashboard as DashboardBase;
                    if (dashboardBase != null && dashboardBase.CambioCuentaSolicitado)
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
                };
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

        private void ConfigurarEstilo()
        {
            BackColor = Color.FromArgb(15, 23, 42);
            ClientSize = new Size(520, 385);
            Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            Text = "SysGym | Inicio de sesi\u00f3n";

            lblTitulo.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Text = "SYSGYM";
            lblTitulo.Location = new Point((ClientSize.Width - lblTitulo.Width) / 2, 32);

            lblSubtitulo = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                ForeColor = Color.FromArgb(186, 230, 253),
                Text = "Gesti\u00f3n simple, segura y organizada"
            };
            Controls.Add(lblSubtitulo);

            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblUsername.ForeColor = Color.FromArgb(226, 232, 240);
            lblUsername.Text = "Usuario";
            lblUsername.Location = new Point(100, 133);

            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblPassword.ForeColor = Color.FromArgb(226, 232, 240);
            lblPassword.Text = "Contrase\u00f1a";
            lblPassword.Location = new Point(100, 201);

            ConfigurarEntrada(txtUsername, new Point(100, 157));
            ConfigurarEntrada(txtPassword, new Point(100, 225));
            txtPassword.UseSystemPasswordChar = true;

            ConfigurarBoton(btnIngresar, new Point(100, 286), "Ingresar",
                Color.FromArgb(20, 184, 166), Color.White);
            ConfigurarBoton(btnSalir, new Point(265, 286), "Salir",
                Color.FromArgb(51, 65, 85), Color.FromArgb(226, 232, 240));

            lblPasswordVisible = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 8.5F),
                ForeColor = Color.FromArgb(148, 163, 184),
                Text = string.Empty
            };
            Controls.Add(lblPasswordVisible);

            var barraSuperior = new Panel
            {
                BackColor = Color.FromArgb(20, 184, 166),
                Dock = DockStyle.Top,
                Height = 7
            };
            Controls.Add(barraSuperior);
            barraSuperior.BringToFront();
            AjustarLayout();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            AjustarLayout();
        }

        private void Login_Resize(object sender, EventArgs e)
        {
            AjustarLayout();
        }

        private void AjustarLayout()
        {
            var centro = ClientSize.Width / 2;
            var anchoCampos = txtUsername.Width;
            var izquierda = centro - anchoCampos / 2;
            var altoContenido = 358;
            var arriba = Math.Max(18, (ClientSize.Height - altoContenido) / 2);

            lblTitulo.Left = centro - lblTitulo.Width / 2;
            lblTitulo.Top = arriba;
            lblSubtitulo.Left = centro - lblSubtitulo.Width / 2;
            lblSubtitulo.Top = arriba + 54;

            lblUsername.Left = izquierda;
            lblUsername.Top = arriba + 103;
            txtUsername.Left = izquierda;
            txtUsername.Top = arriba + 127;

            lblPassword.Left = izquierda;
            lblPassword.Top = arriba + 195;
            txtPassword.Left = izquierda;
            txtPassword.Top = arriba + 219;

            btnIngresar.Left = izquierda;
            btnIngresar.Top = arriba + 280;
            btnSalir.Left = izquierda + anchoCampos / 2 + 5;
            btnSalir.Top = arriba + 280;

            lblPasswordVisible.Left = izquierda;
            lblPasswordVisible.Top = arriba + 253;
        }

        private static void ConfigurarEntrada(TextBox entrada, Point ubicacion)
        {
            entrada.Font = new Font("Segoe UI", 11F);
            entrada.Location = ubicacion;
            entrada.Size = new Size(320, 31);
            entrada.BorderStyle = BorderStyle.FixedSingle;
            entrada.BackColor = Color.White;
            entrada.ForeColor = Color.FromArgb(15, 23, 42);
        }

        private static void ConfigurarBoton(Button boton, Point ubicacion, string texto,
            Color fondo, Color textoColor)
        {
            boton.FlatStyle = FlatStyle.Flat;
            boton.FlatAppearance.BorderSize = 0;
            boton.BackColor = fondo;
            boton.ForeColor = textoColor;
            boton.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            boton.Location = ubicacion;
            boton.Size = new Size(155, 38);
            boton.Text = texto;
            boton.Cursor = Cursors.Hand;
            boton.UseVisualStyleBackColor = false;
        }

        private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
                System.Media.SystemSounds.Beep.Play();
            }
        }

        private void txtUsername_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                errorProvider.SetError(txtUsername, "Ingresá el nombre de usuario.");
            }
            else if (txtUsername.Text.Any(c => !char.IsLetter(c)))
            {
                errorProvider.SetError(txtUsername, "El usuario solo puede contener letras.");
            }
            else
            {
                errorProvider.SetError(txtUsername, string.Empty);
            }
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.SetError(txtPassword,
                string.IsNullOrWhiteSpace(txtPassword.Text)
                    ? "Ingresá la contraseña."
                    : string.Empty);
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            lblPasswordVisible.Text = string.IsNullOrEmpty(txtPassword.Text)
                ? string.Empty
                : "Contraseña visible: " + txtPassword.Text;

            if (!string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                errorProvider.SetError(txtPassword, string.Empty);
            }
        }

        private bool ValidarCampos()
        {
            txtUsername_Validating(txtUsername, new CancelEventArgs());
            txtPassword_Validating(txtPassword, new CancelEventArgs());
            return string.IsNullOrEmpty(errorProvider.GetError(txtUsername))
                && string.IsNullOrEmpty(errorProvider.GetError(txtPassword));
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
