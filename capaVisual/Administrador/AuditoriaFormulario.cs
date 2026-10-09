using System;
using exxen2._0.capaVisual.Compartido.Utilidades;
using System.ComponentModel;
using System.Windows.Forms;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaLogica.Auditoria;

namespace exxen2._0.capaVisual.Administrador
{
    [DesignerCategory("Form")]
    public partial class AuditoriaFormulario : Form
    {
        private readonly AuditoriaLogica logica;
        public AuditoriaFormulario() : this(null) { }
        public AuditoriaFormulario(UsuarioSistema usuarioActual)
        {
            logica = new AuditoriaLogica(usuarioActual == null ? 0 : usuarioActual.IdUsuarioSistema);
            InitializeComponent();
        }

        private void AuditoriaFormulario_Load(object sender, EventArgs e)
        {
            if (AyudaFormularioVisual.EnModoDisenio(this)) return;
            try
            {
                logica.ValidarAcceso();
                var usuarios = logica.ListarUsuarios();
                usuarios.Insert(0, new UsuarioFiltroAuditoria { IdUsuario = 0, Nombre = "Todos" });
                usuario.DisplayMember = "Nombre";
                usuario.ValueMember = "IdUsuario";
                usuario.DataSource = usuarios;
                var operaciones = AuditoriaLogica.Operaciones();
                operaciones.Insert(0, new OpcionAuditoria { Codigo = "", Texto = "Todas" });
                operacion.DisplayMember = "Texto";
                operacion.ValueMember = "Codigo";
                operacion.DataSource = operaciones;
                AplicarFiltros();
            }
            catch (Exception ex)
            {
                filtros.Enabled = false;
                tabla.DataSource = null;
                AyudaFormularioVisual.MostrarError(lblEstado, ex);
            }
        }

        private void aplicar_Click(object sender, EventArgs e) { AplicarFiltros(); }

        private void AplicarFiltros()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                var idUsuario = Convert.ToInt32(usuario.SelectedValue);
                var filas = logica.Consultar(desde.Checked ? (DateTime?)desde.Value : null,
                    hasta.Checked ? (DateTime?)hasta.Value : null, idUsuario == 0 ? (int?)null : idUsuario,
                    Convert.ToString(operacion.SelectedValue), buscar.Text);
                tabla.DataSource = filas;
                lblEstado.Text = filas.Count + " registro(s). Se muestran hasta los últimos " + AuditoriaLogica.LimiteRegistros +
                    " que coinciden con los filtros. Marque Desde/Hasta para limitar las fechas.";
                lblEstado.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            }
            catch (Exception ex)
            {
                tabla.DataSource = null;
                AyudaFormularioVisual.MostrarError(lblEstado, ex);
            }
            finally { Cursor = Cursors.Default; }
        }
    }
}
