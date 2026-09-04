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
    public partial class ReportesForm : Form
    {
        private readonly SocioLogica socios = new SocioLogica();
        private readonly UsuarioSistemaLogica usuarios = new UsuarioSistemaLogica();
        private readonly EjercicioLogica ejercicios = new EjercicioLogica();
        private readonly RutinaLogica rutinas = new RutinaLogica();
        private readonly MembresiaLogica membresias = new MembresiaLogica();
        public ReportesForm()
        {
            InitializeComponent();
            btnVolver.Click += delegate { Close(); };
            generar.Click += Generar;
            FormularioVisualHelper.AlCargarEnEjecucion(this, delegate { Generar(null, EventArgs.Empty); });
        }

        private void Generar(object sender, EventArgs e)
        {
            try
            {
                resumen.Text = "SOCIOS ACTIVOS\n" + socios.ListarActivos().Count
                    + "\n\nUSUARIOS ACTIVOS\n" + usuarios.ListarActivos().Count
                    + "\n\nMEMBRESIAS HABILITADAS\n" + membresias.ListarHabilitadas().Count
                    + "\n\nRUTINAS ACTIVAS\n" + rutinas.ListarActivas().Count
                    + "\n\nEJERCICIOS DISPONIBLES\n" + ejercicios.ListarActivos().Count;
                lblEstado.Text = "Reporte generado: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            }
            catch (Exception ex) { FormularioVisualHelper.MostrarError(lblEstado, ex); }
        }
    }
}
