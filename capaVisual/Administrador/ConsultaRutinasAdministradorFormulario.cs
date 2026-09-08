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
    /* Presenta rutinas administrador y atiende sus acciones mediante eventos de Windows Forms. */
    [DesignerCategory("Form")]
    public partial class ConsultaRutinasAdministradorFormulario : Form
    {
        private readonly RutinaLogica logica = new RutinaLogica();
        /* Inicializa los componentes existentes y las dependencias de la pantalla sin consultar la base de datos. */
        public ConsultaRutinasAdministradorFormulario()
        {
            InitializeComponent();
        }

        /* Consulta los registros del módulo y actualiza la grilla, informando los errores de carga. */
        private void Cargar()
        {
            try
            {
                tabla.Rows.Clear();
                foreach (var rutina in logica.ListarGenerales())
                {
                    tabla.Rows.Add(rutina.IdRutina, rutina.Nombre, rutina.Descripcion ?? "-", rutina.Entrenador == null ? "-" : rutina.Entrenador.Nombre + " " + rutina.Entrenador.Apellido, rutina.Asignaciones == null ? 0 : rutina.Asignaciones.Count(a => a.Estado));
                }

                lblEstado.Text = tabla.Rows.Count + " plantilla(s) activa(s)";
            }
            catch (Exception ex)
            {
                AyudaFormularioVisual.MostrarError(lblEstado, ex);
            }
        }

        /* Al cargar la pantalla en ejecución, prepara sus datos iniciales sin realizar consultas desde el diseñador. */
        private void ConsultaRutinasAdministradorFormulario_Load(object origen, EventArgs e)
        {
            if (AyudaFormularioVisual.EnModoDisenio(this))
                return;
            try
            {
                Cargar();
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

        /* Al hacer clic en actualizar, vuelve a consultar y mostrar los registros del módulo. */
        private void actualizar_Click(object origen, EventArgs e)
        {
            Cargar();
        }
    }
}
