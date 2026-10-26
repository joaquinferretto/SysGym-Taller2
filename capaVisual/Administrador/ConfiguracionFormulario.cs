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
        private readonly CopiaSeguridadLogica copias;
        private int maxVencidasGuardado;
        private int maxAnticipacionGuardado;
        private int diasAvisoGuardado;

        public ConfiguracionFormulario() : this(null) { }
        public ConfiguracionFormulario(UsuarioSistema usuarioActual)
        {
            var idUsuario = usuarioActual == null ? 0 : usuarioActual.IdUsuarioSistema;
            logica = new ConfiguracionSistemaLogica(idUsuario);
            copias = new CopiaSeguridadLogica(idUsuario);
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
                CargarEstadoCopias();
            }
            catch (Exception ex)
            {
                campos.Enabled = false;
                guardar.Enabled = false;
                descartar.Enabled = false;
                btnCrearCopia.Enabled = false;
                btnRestaurarCopia.Enabled = false;
                AyudaFormularioVisual.MostrarError(lblEstado, ex);
            }
        }

        private void CargarEstadoCopias()
        {
            try
            {
                copias.ValidarAcceso();
                btnRestaurarCopia.Enabled = true;
                btnCrearCopia.Enabled = true;
            }
            catch (Exception ex)
            {
                btnCrearCopia.Enabled = false;
                btnRestaurarCopia.Enabled = false;
                AyudaFormularioVisual.MostrarError(lblEstado, ex);
                return;
            }

            try
            {
                var ultima = copias.ObtenerFechaUltimaCopia();
                lblUltimaCopia.Text = ultima.HasValue
                    ? "Última copia: " + ultima.Value.ToString("dd/MM/yyyy HH:mm")
                    : "Última copia: todavía no hay copias registradas";
            }
            catch
            {
                lblUltimaCopia.Text = "Última copia: no disponible";
            }
        }

        private void btnCrearCopia_Click(object sender, EventArgs e)
        {
            try
            {
                using (var dialogo = new SaveFileDialog
                {
                    Title = "Crear copia de seguridad",
                    Filter = "Copia de seguridad SQL Server (*.bak)|*.bak",
                    DefaultExt = "bak",
                    AddExtension = true,
                    OverwritePrompt = true,
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    FileName = "SysGym-" + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".bak"
                })
                {
                    if (dialogo.ShowDialog(this) != DialogResult.OK)
                        return;
                    copias.CrearCopia(dialogo.FileName);
                }
                CargarEstadoCopias();
                MessageBox.Show(this, "La copia de seguridad se guardó correctamente.", "SysGym", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                AyudaFormularioVisual.MostrarError(lblEstado, ex);
            }
        }

        private void btnRestaurarCopia_Click(object sender, EventArgs e)
        {
            using (var dialogo = new OpenFileDialog
            {
                Title = "Seleccionar copia de seguridad",
                Filter = "Copia de seguridad SQL Server (*.bak)|*.bak",
                CheckFileExists = true,
                Multiselect = false
            })
            {
                if (dialogo.ShowDialog(this) != DialogResult.OK)
                    return;
                if (MessageBox.Show(this,
                    "Se reemplazarán TODOS los datos actuales por los de la copia. ¿Continuar?",
                    "Confirmar restauración", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                    return;

                try
                {
                    copias.RestaurarCopia(dialogo.FileName);
                    MessageBox.Show(this, "La copia se restauró correctamente. SysGym se reiniciará.",
                        "SysGym", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.Restart();
                    Close();
                }
                catch (RestauracionCompletadaException ex)
                {
                    MessageBox.Show(this, ex.Message + " SysGym se reiniciará.",
                        "SysGym", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Application.Restart();
                    Close();
                }
                catch (Exception ex)
                {
                    AyudaFormularioVisual.MostrarError(lblEstado, ex);
                }
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
