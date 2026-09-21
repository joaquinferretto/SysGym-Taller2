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

using exxen2._0.capaLogica.Navegacion;
using exxen2._0.capaLogica.Utilidades;

namespace exxen2._0.capaVisual.Autenticacion
{
    /* Presenta el inicio de sesión y atiende sus acciones mediante eventos de Windows Forms. */
    public partial class InicioSesion : Form
    {
        private readonly UsuarioSistemaLogica usuarioSistemaLogica;
        /* Inicializa los componentes existentes y las dependencias de la pantalla sin consultar la base de datos. */
        public InicioSesion()
        {
            InitializeComponent();
            usuarioSistemaLogica = new UsuarioSistemaLogica();
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
                    indicadorErrores.SetError(txtClave, "El usuario o la contraseña no son correctos.");
                    MessageBox.Show("Usuario o contraseña incorrectos.", "Inicio de sesión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                MessageBox.Show("No se pudo iniciar sesión.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        /* Al escribir el usuario, admite letras, números y separadores compatibles. */
        private void txtNombreUsuario_KeyPress(object origen, KeyPressEventArgs e)
        {
            AyudaFormularioVisual.ValidarEntradaNombreUsuario(e);
        }

        /* Al validar el campo de acceso, informa los datos faltantes o inválidos mediante ErrorProvider. */
        private void txtNombreUsuario_Validating(object origen, CancelEventArgs e)
        {
            AyudaFormularioVisual.ValidarNombreUsuario(indicadorErrores, txtNombreUsuario);
        }

        private void txtNombreUsuario_TextChanged(object origen, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNombreUsuario.Text))
                AyudaFormularioVisual.ValidarNombreUsuario(indicadorErrores, txtNombreUsuario);
        }

        /* Al validar el campo de acceso, informa los datos faltantes o inválidos mediante ErrorProvider. */
        private void txtClave_Validating(object origen, CancelEventArgs e)
        {
            indicadorErrores.SetError(txtClave, string.IsNullOrWhiteSpace(txtClave.Text) ? "Ingresa la contraseña." : string.Empty);
        }

        /* Al escribir la contraseña, actualiza la indicación existente y limpia su error de validación. */
        private void txtClave_TextChanged(object origen, EventArgs e)
        {
            lblClaveVisible.Text = string.IsNullOrEmpty(txtClave.Text) ? string.Empty : "Contraseña visible: " + txtClave.Text;
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
