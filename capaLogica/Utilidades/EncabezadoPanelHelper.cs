using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using exxen2._0.capaDatos.Entidades;

namespace exxen2._0.capaLogica.Utilidades
{
    /* Comparte entre los tres paneles principales el llenado del header global y el resaltado del menú lateral. */
    internal static class EncabezadoPanelHelper
    {
        private static readonly Color FondoOpcion = Color.FromArgb(248, 250, 252);
        private static readonly Color BordeOpcion = Color.FromArgb(226, 232, 240);
        private static readonly Color TextoOpcion = Color.FromArgb(51, 65, 85);
        private static readonly Color FondoOpcionActiva = Color.FromArgb(238, 242, 255);
        private static readonly Color BordeOpcionActiva = Color.FromArgb(199, 210, 254);
        private static readonly Color TextoOpcionActiva = Color.FromArgb(67, 56, 202);

        /* Muestra foto, nombre, rol, DNI y sexo reales del usuario de la sesión en los controles existentes del header. */
        internal static void MostrarUsuario(UsuarioSistema usuario, string rolPredeterminado, PictureBox foto, Label nombre, Label rol, Label dni, Label sexo)
        {
            AyudaFormularioVisual.MostrarFotoRuta(foto, usuario.FotoRuta, usuario.Sexo);
            nombre.Text = (usuario.Nombre + " " + usuario.Apellido).Trim();
            rol.Text = usuario.Rol == null || string.IsNullOrWhiteSpace(usuario.Rol.Descripcion) ? rolPredeterminado : usuario.Rol.Descripcion;
            dni.Text = FormatearDni(usuario.DNI);
            sexo.Text = usuario.Sexo == "M" ? "Masculino" : usuario.Sexo == "F" ? "Femenino" : "No informado";
        }

        /* Aplica el texto «Título | Subtítulo» que envía ControladorNavegacion; vacío significa el Inicio del rol. */
        internal static void EstablecerModulo(string titulo, string subtituloInicio, Label lblTitulo, Label lblSubtitulo, Button volver, string tituloInicio = "Inicio")
        {
            var enInicio = string.IsNullOrWhiteSpace(titulo);
            var partes = (titulo ?? string.Empty).Split(new[] { '|' }, 2);
            lblTitulo.Text = enInicio ? tituloInicio : partes[0].Trim();
            lblSubtitulo.Text = enInicio ? subtituloInicio : partes.Length > 1 ? partes[1].Trim() : string.Empty;
            volver.Enabled = !enInicio;
        }

        /* Resalta la opción del menú correspondiente al módulo abierto; null deja todas sin resaltar. */
        internal static void MarcarOpcionActiva(Control opciones, Button activa)
        {
            foreach (Control control in opciones.Controls)
            {
                var boton = control as Button;
                if (boton == null)
                    continue;
                var esActiva = ReferenceEquals(boton, activa);
                boton.BackColor = esActiva ? FondoOpcionActiva : FondoOpcion;
                boton.FlatAppearance.BorderColor = esActiva ? BordeOpcionActiva : BordeOpcion;
                boton.ForeColor = esActiva ? TextoOpcionActiva : TextoOpcion;
                boton.Font = new Font(boton.Font, esActiva ? FontStyle.Bold : FontStyle.Regular);
            }
        }

        /* Presenta el DNI numérico con separadores de miles; otros formatos se muestran tal como están guardados. */
        private static string FormatearDni(string dni)
        {
            long numero;
            if (string.IsNullOrWhiteSpace(dni))
                return "-";
            return long.TryParse(dni, NumberStyles.None, CultureInfo.InvariantCulture, out numero)
                ? numero.ToString("#,0", new CultureInfo("es-AR"))
                : dni;
        }
    }
}
