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
    /* Presenta reportes y atiende sus acciones mediante eventos de Windows Forms. */
    [DesignerCategory("Form")]
    public partial class ReportesFormulario : Form
    {
        private readonly SocioLogica socios = new SocioLogica();
        private readonly UsuarioSistemaLogica usuarios = new UsuarioSistemaLogica();
        private readonly EjercicioLogica ejercicios = new EjercicioLogica();
        private readonly RutinaLogica rutinas = new RutinaLogica();
        private readonly MembresiaLogica membresias = new MembresiaLogica();
        /* Inicializa los componentes existentes y las dependencias de la pantalla sin consultar la base de datos. */
        public ReportesFormulario()
        {
            InitializeComponent();
        }

        /* Al hacer clic en generar, consulta los contadores y presenta el reporte básico. */
        private void generar_Click(object origen, EventArgs e)
        {
            try
            {
                resumen.Text = "SOCIOS ACTIVOS\n" + socios.ListarActivos().Count + "\n\nUSUARIOS ACTIVOS\n" + usuarios.ListarActivos().Count + "\n\nMEMBRESIAS HABILITADAS\n" + membresias.ListarHabilitadas().Count + "\n\nRUTINAS ACTIVAS\n" + rutinas.ListarActivas().Count + "\n\nEJERCICIOS DISPONIBLES\n" + ejercicios.ListarActivos().Count;
                lblEstado.Text = "Reporte generado: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            }
            catch (Exception ex)
            {
                AyudaFormularioVisual.MostrarError(lblEstado, ex);
            }
        }

        /* Al cargar la pantalla en ejecución, prepara sus datos iniciales sin realizar consultas desde el diseñador. */
        private void ReportesFormulario_Load(object origen, EventArgs e)
        {
            if (AyudaFormularioVisual.EnModoDisenio(this))
                return;
            try
            {
                generar_Click(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                AyudaFormularioVisual.MostrarError(lblEstado, ex);
            }
        }

        /* Al hacer clic en btnVolver, cierra el módulo y devuelve el control al panel principal. */
        private void btnVolver_Click(object origen, EventArgs e)
        {
            Close();
        }
    }
}
