using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Windows.Forms;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaLogica;
using exxen2._0.capaVisual.Compartido.Utilidades;

namespace exxen2._0.capaVisual.Autenticacion
{
    /* Solicita los datos del único primer Administrador antes de permitir iniciar sesión. */
    public partial class RegistrarAdministradorFormulario : Form
    {
        private readonly UsuarioSistemaLogica logica = new UsuarioSistemaLogica();
        private bool registroCompletado;

        public RegistrarAdministradorFormulario()
        {
            InitializeComponent();
            AcceptButton = btnRegistrar;
        }

        private bool ValidarFormulario()
        {
            indicadorErrores.Clear();
            var valido = AyudaFormularioVisual.ValidarNombre(indicadorErrores, nombre, "nombre");
            valido = AyudaFormularioVisual.ValidarNombre(indicadorErrores, apellido, "apellido") & valido;
            valido = AyudaFormularioVisual.ValidarDni(indicadorErrores, dni) & valido;
            valido = AyudaFormularioVisual.ValidarNombreUsuario(indicadorErrores, nombreUsuario) & valido;
            valido = AyudaFormularioVisual.ValidarRequerido(indicadorErrores, clave, "La contraseña es obligatoria.") & valido;
            valido = AyudaFormularioVisual.ValidarConError(indicadorErrores, confirmarClave, delegate
            {
                if (string.IsNullOrWhiteSpace(confirmarClave.Text))
                    throw new InvalidOperationException("Confirmá la contraseña.");
                if (!string.Equals(clave.Text, confirmarClave.Text, StringComparison.Ordinal))
                    throw new InvalidOperationException("Las contraseñas no coinciden.");
            }) & valido;

            if (!valido)
                AyudaFormularioVisual.EnfocarPrimerError(indicadorErrores, nombre, apellido, dni, nombreUsuario, clave, confirmarClave);
            return valido;
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (!ValidarFormulario())
                return;

            btnRegistrar.Enabled = false;
            try
            {
                logica.RegistrarPrimerAdministrador(new UsuarioSistema
                {
                    Nombre = nombre.Text.Trim(),
                    Apellido = apellido.Text.Trim(),
                    DNI = dni.Text.Trim(),
                    NombreUsuario = nombreUsuario.Text.Trim()
                }, clave.Text);
                registroCompletado = true;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                lblEstado.ForeColor = System.Drawing.Color.FromArgb(185, 28, 28);
                lblEstado.Text = EsErrorSql(ex)
                    ? "No se pudo conectar con SQL Server. Verifique que esté instalado y en ejecución."
                    : ex.Message;
                btnRegistrar.Enabled = true;
            }
        }

        private static bool EsErrorSql(Exception excepcion)
        {
            if (excepcion == null)
                return false;
            if (excepcion is SqlException)
                return true;

            var agregado = excepcion as AggregateException;
            if (agregado != null)
            {
                foreach (var interna in agregado.InnerExceptions)
                    if (EsErrorSql(interna))
                        return true;
            }
            return EsErrorSql(excepcion.InnerException);
        }

        private void nombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            AyudaFormularioVisual.ValidarEntradaNombre(e);
        }

        private void dni_KeyPress(object sender, KeyPressEventArgs e)
        {
            AyudaFormularioVisual.ValidarEntradaDni(e);
        }

        private void nombreUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            AyudaFormularioVisual.ValidarEntradaNombreUsuario(e);
        }

        private void nombre_Validating(object sender, CancelEventArgs e)
        {
            AyudaFormularioVisual.ValidarNombre(indicadorErrores, (TextBox)sender, sender == nombre ? "nombre" : "apellido");
        }

        private void dni_Validating(object sender, CancelEventArgs e)
        {
            AyudaFormularioVisual.ValidarDni(indicadorErrores, dni);
        }

        private void nombreUsuario_Validating(object sender, CancelEventArgs e)
        {
            AyudaFormularioVisual.ValidarNombreUsuario(indicadorErrores, nombreUsuario);
        }

        private void campo_TextChanged(object sender, EventArgs e)
        {
            indicadorErrores.SetError((Control)sender, string.Empty);
            if (sender == clave || sender == confirmarClave)
                indicadorErrores.SetError(confirmarClave, string.Empty);
            lblEstado.Text = string.Empty;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!registroCompletado && e.CloseReason == CloseReason.UserClosing)
            {
                var respuesta = MessageBox.Show(this,
                    "¿Desea salir de SysGym? No podrá usar el sistema hasta registrar un administrador.",
                    "SysGym", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (respuesta != DialogResult.Yes)
                {
                    e.Cancel = true;
                    return;
                }
            }
            base.OnFormClosing(e);
        }
    }
}
