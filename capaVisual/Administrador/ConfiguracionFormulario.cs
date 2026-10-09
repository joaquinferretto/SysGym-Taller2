using System;
using exxen2._0.capaVisual.Compartido.Utilidades;
using System.ComponentModel;
using System.Windows.Forms;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaLogica;

namespace exxen2._0.capaVisual.Administrador
{
    [DesignerCategory("Form")]
    public partial class ConfiguracionFormulario : Form
    {
        private readonly ConfiguracionSistemaLogica logica;
        public ConfiguracionFormulario() : this(null) { }
        public ConfiguracionFormulario(UsuarioSistema usuarioActual)
        {
            logica = new ConfiguracionSistemaLogica(usuarioActual == null ? 0 : usuarioActual.IdUsuarioSistema);
            InitializeComponent();
        }

        private void ConfiguracionFormulario_Load(object sender, EventArgs e)
        {
            if (AyudaFormularioVisual.EnModoDisenio(this)) return;
            try
            {
                logica.ValidarAcceso();
                var configuracion = logica.Obtener();
                maxVencidas.Value = configuracion.MaxCuotasVencidasPermitidas;
                maxAnticipacion.Value = configuracion.MaxMesesAnticipacionCuotas;
                diasAviso.Value = configuracion.DiasAvisoVencimiento;
            }
            catch (Exception ex)
            {
                campos.Enabled = false;
                guardar.Enabled = false;
                AyudaFormularioVisual.MostrarError(lblEstado, ex);
            }
        }

        private void guardar_Click(object sender, EventArgs e)
        {
            try
            {
                var cambio = logica.Guardar((int)maxVencidas.Value, (int)maxAnticipacion.Value, (int)diasAviso.Value);
                lblEstado.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
                lblEstado.Text = cambio
                    ? "Configuración guardada. Se aplicará en próximas operaciones y consultas. Las reactivaciones siguen siendo manuales."
                    : "No hay cambios para guardar.";
            }
            catch (Exception ex) { AyudaFormularioVisual.MostrarError(lblEstado, ex); }
        }
    }
}
