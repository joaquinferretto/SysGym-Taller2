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
        private int maxVencidasGuardado;
        private int maxAnticipacionGuardado;
        private int diasAvisoGuardado;

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
                maxVencidasGuardado = (int)maxVencidas.Value;
                maxAnticipacionGuardado = (int)maxAnticipacion.Value;
                diasAvisoGuardado = (int)diasAviso.Value;
                guardar.Enabled = false;
                descartar.Enabled = false;
            }
            catch (Exception ex)
            {
                campos.Enabled = false;
                guardar.Enabled = false;
                descartar.Enabled = false;
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
                if (cambio)
                {
                    maxVencidasGuardado = (int)maxVencidas.Value;
                    maxAnticipacionGuardado = (int)maxAnticipacion.Value;
                    diasAvisoGuardado = (int)diasAviso.Value;
                    guardar.Enabled = false;
                    descartar.Enabled = false;
                }
            }
            catch (Exception ex) { AyudaFormularioVisual.MostrarError(lblEstado, ex); }
        }

        private void descartar_Click(object sender, EventArgs e)
        {
            maxVencidas.Value = maxVencidasGuardado;
            maxAnticipacion.Value = maxAnticipacionGuardado;
            diasAviso.Value = diasAvisoGuardado;
            guardar.Enabled = false;
            descartar.Enabled = false;
            lblEstado.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            lblEstado.Text = "Los cambios se aplican a las próximas operaciones y consultas.";
        }

        private void maxVencidas_ValueChanged(object sender, EventArgs e)
        {
            var valor = (int)maxVencidas.Value;
            descripcionVencidas.Text = $"Con {valor}, el socio queda inactivo al tener {valor} vencidas.";
            guardar.Enabled = valor != maxVencidasGuardado
                || (int)maxAnticipacion.Value != maxAnticipacionGuardado
                || (int)diasAviso.Value != diasAvisoGuardado;
            descartar.Enabled = guardar.Enabled;
        }

        private void maxAnticipacion_ValueChanged(object sender, EventArgs e)
        {
            var valor = (int)maxAnticipacion.Value;
            var periodo = valor == 1 ? "mes futuro" : "meses futuros";
            descripcionAnticipacion.Text = $"Permite generar el período actual y hasta {valor} {periodo}.";
            guardar.Enabled = (int)maxVencidas.Value != maxVencidasGuardado
                || valor != maxAnticipacionGuardado
                || (int)diasAviso.Value != diasAvisoGuardado;
            descartar.Enabled = guardar.Enabled;
        }

        private void diasAviso_ValueChanged(object sender, EventArgs e)
        {
            var valor = (int)diasAviso.Value;
            var unidad = valor == 1 ? "día" : "días";
            descripcionAviso.Text = $"Aviso de vencimiento con {valor} {unidad} de anticipación.";
            guardar.Enabled = (int)maxVencidas.Value != maxVencidasGuardado
                || (int)maxAnticipacion.Value != maxAnticipacionGuardado
                || valor != diasAvisoGuardado;
            descartar.Enabled = guardar.Enabled;
        }
    }
}
