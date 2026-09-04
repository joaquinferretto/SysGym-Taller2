using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaLogica;
using exxen2._0.capaVisual.Compartido;

namespace exxen2._0.capaVisual.Administrador
{
    [DesignerCategory("Form")]
    public partial class ConsultaRutinasAdministradorForm : Form
    {
        private readonly RutinaLogica logica = new RutinaLogica();

        public ConsultaRutinasAdministradorForm()
        {
            InitializeComponent();
            btnVolver.Click += delegate { Close(); };
            actualizar.Click += delegate { Cargar(); };
            FormularioVisualHelper.AlCargarEnEjecucion(this, delegate { Cargar(); });
        }

        private void Cargar()
        {
            try
            {
                tabla.Rows.Clear();
                foreach (var rutina in logica.ListarGenerales())
                {
                    tabla.Rows.Add(rutina.IdRutina, rutina.Nombre, rutina.Descripcion ?? "-",
                        rutina.Entrenador == null ? "-" : rutina.Entrenador.Nombre + " " + rutina.Entrenador.Apellido,
                        rutina.Asignaciones == null ? 0 : rutina.Asignaciones.Count(a => a.Estado));
                }
                lblEstado.Text = tabla.Rows.Count + " plantilla(s) activa(s)";
            }
            catch (Exception ex) { FormularioVisualHelper.MostrarError(lblEstado, ex); }
        }
    }
}
