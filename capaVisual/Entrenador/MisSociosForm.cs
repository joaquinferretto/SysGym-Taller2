using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaLogica;
using exxen2._0.capaVisual.Compartido;

namespace exxen2._0.capaVisual.Entrenador
{
    [DesignerCategory("Form")]
    public partial class MisSociosForm : Form
    {
        private readonly RutinaAsignacionLogica asignaciones = new RutinaAsignacionLogica();
        private readonly UsuarioSistema usuario;
        public MisSociosForm()
            : this(new UsuarioSistema { Nombre = "Entrenador", Apellido = "de diseno" })
        {
        }

        public MisSociosForm(UsuarioSistema usuario)
        {
            if (usuario == null) throw new ArgumentNullException("usuario");
            this.usuario = usuario; InitializeComponent();
            btnVolver.Click += delegate { Close(); };
            actualizar.Click += delegate { Cargar(); };
            FormularioVisualHelper.AlCargarEnEjecucion(this, delegate { Cargar(); });
        }

        private void Cargar()
        {
            try { tabla.Rows.Clear(); foreach (var grupo in asignaciones.ListarPorEntrenador(usuario.IdUsuarioSistema).GroupBy(a => a.Membresia.IdSocio)) { var primera = grupo.First(); tabla.Rows.Add(grupo.Key, primera.Membresia.Socio.Apellido + ", " + primera.Membresia.Socio.Nombre, grupo.Count()); } lblEstado.Text = tabla.Rows.Count + " socio(s) con rutinas asignadas"; }
            catch (Exception ex) { FormularioVisualHelper.MostrarError(lblEstado, ex); }
        }
    }
}
