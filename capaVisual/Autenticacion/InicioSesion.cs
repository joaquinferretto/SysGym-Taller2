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
    /* Presenta el inicio de sesión y atiende sus acciones mediante eventos de Windows Forms. */
    public partial class InicioSesion : Form
    {
        private readonly UsuarioSistemaLogica usuarioSistemaLogica;
        private readonly ErrorProvider indicadorErrores;
        /* Inicializa los componentes existentes y las dependencias de la pantalla sin consultar la base de datos. */
        public InicioSesion()
        {
            InitializeComponent();
            usuarioSistemaLogica = new UsuarioSistemaLogica();
            indicadorErrores = new ErrorProvider(this)
            {
                BlinkStyle = ErrorBlinkStyle.NeverBlink
            };
            components.Add(indicadorErrores);
            AcceptButton = btnIngresar;
            CancelButton = btnSalir;
        }

        /* Al hacer clic en btnIngresar, valida las credenciales y abre el panel principal del rol autenticado. */
        private void btnIngresar_Click(object origen, EventArgs e)
        {
            try
            {
                if (!ValidarCampos())
                    return;
                var usuario = usuarioSistemaLogica.Autenticar(txtNombreUsuario.Text.Trim(), txtClave.Text);
                if (usuario == null)
                {
                    indicadorErrores.SetError(txtClave, "El usuario o la contrasena no son correctos.");
                    MessageBox.Show("Usuario o contrasena incorrectos.", "Inicio de sesion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtClave.Clear();
                    txtClave.Focus();
                    return;
                }

                var panelPrincipal = CrearPanelPrincipal(usuario);
                indicadorErrores.Clear();
                Hide();
                panelPrincipal.FormClosed += new FormClosedEventHandler(panelPrincipal_FormClosed);
                panelPrincipal.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo iniciar sesion.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /* Al hacer clic en btnSalir, inicia el cierre de la pantalla. */
        private void btnSalir_Click(object origen, EventArgs e)
        {
            Close();
        }

        /* Al cerrar el panel principal, vuelve al acceso si se solicitó cambiar de cuenta o cierra la aplicación. */
        private void panelPrincipal_FormClosed(object origen, FormClosedEventArgs e)
        {
            var sesionPanel = origen as ISesionPanel;
            if (sesionPanel != null && sesionPanel.CambioCuentaSolicitado)
            {
                txtClave.Clear();
                indicadorErrores.Clear();
                Show();
                BringToFront();
                Activate();
                txtNombreUsuario.Focus();
                return;
            }

            Close();
        }

        /* Al escribir el usuario, aplica la restricción de letras del formulario de acceso. */
        private void txtNombreUsuario_KeyPress(object origen, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
                System.Media.SystemSounds.Beep.Play();
            }
        }

        /* Al validar el campo de acceso, informa los datos faltantes o inválidos mediante ErrorProvider. */
        private void txtNombreUsuario_Validating(object origen, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombreUsuario.Text))
                indicadorErrores.SetError(txtNombreUsuario, "Ingresa el nombre de usuario.");
            else if (txtNombreUsuario.Text.Any(c => !char.IsLetter(c)))
                indicadorErrores.SetError(txtNombreUsuario, "El usuario solo puede contener letras.");
            else
                indicadorErrores.SetError(txtNombreUsuario, string.Empty);
        }

        /* Al validar el campo de acceso, informa los datos faltantes o inválidos mediante ErrorProvider. */
        private void txtClave_Validating(object origen, CancelEventArgs e)
        {
            indicadorErrores.SetError(txtClave, string.IsNullOrWhiteSpace(txtClave.Text) ? "Ingresa la contrasena." : string.Empty);
        }

        /* Al escribir la contraseña, actualiza la indicación existente y limpia su error de validación. */
        private void txtClave_TextChanged(object origen, EventArgs e)
        {
            lblClaveVisible.Text = string.IsNullOrEmpty(txtClave.Text) ? string.Empty : "Contrasena visible: " + txtClave.Text;
            if (!string.IsNullOrWhiteSpace(txtClave.Text))
                indicadorErrores.SetError(txtClave, string.Empty);
        }

        /* Comprueba los campos de acceso y devuelve si están listos para autenticar al usuario. */
        private bool ValidarCampos()
        {
            txtNombreUsuario_Validating(txtNombreUsuario, new CancelEventArgs());
            txtClave_Validating(txtClave, new CancelEventArgs());
            return string.IsNullOrEmpty(indicadorErrores.GetError(txtNombreUsuario)) && string.IsNullOrEmpty(indicadorErrores.GetError(txtClave));
        }

        /* Selecciona el panel principal existente correspondiente al rol del usuario autenticado. */
        private static Form CrearPanelPrincipal(UsuarioSistema usuario)
        {
            switch (usuario.Rol.Descripcion.Trim().ToUpperInvariant())
            {
                case "ADMINISTRADOR":
                    return new PanelAdministrador(usuario);
                case "RECEPCIONISTA":
                    return new PanelRecepcionista(usuario);
                case "ENTRENADOR":
                    return new PanelEntrenador(usuario);
                default:
                    throw new InvalidOperationException("El rol del usuario no tiene un panel principal configurado.");
            }
        }
    }
}
