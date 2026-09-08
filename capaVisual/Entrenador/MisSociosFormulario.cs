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
    /* Presenta mis socios y atiende sus acciones mediante eventos de Windows Forms. */
    [DesignerCategory("Form")]
    public partial class MisSociosFormulario : Form
    {
        private readonly RutinaAsignacionLogica asignaciones = new RutinaAsignacionLogica();
        private readonly UsuarioSistema usuario;
        /* Inicializa los componentes existentes y las dependencias de la pantalla sin consultar la base de datos. */
        public MisSociosFormulario() : this(new UsuarioSistema { Nombre = "Entrenador", Apellido = "de diseno" })
        {
        }

        /* Inicializa los componentes existentes y las dependencias de la pantalla sin consultar la base de datos. */
        public MisSociosFormulario(UsuarioSistema usuario)
        {
            if (usuario == null)
                throw new ArgumentNullException("usuario");
            this.usuario = usuario;
            InitializeComponent();
        }

        /* Consulta los registros del módulo y actualiza la grilla, informando los errores de carga. */
        private void Cargar()
        {
            try
            {
                tabla.Rows.Clear();
                foreach (var grupo in asignaciones.ListarPorEntrenador(usuario.IdUsuarioSistema).GroupBy(a => a.Membresia.IdSocio))
                {
                    var primera = grupo.First();
                    tabla.Rows.Add(grupo.Key, primera.Membresia.Socio.Apellido + ", " + primera.Membresia.Socio.Nombre, grupo.Count());
                }

                lblEstado.Text = tabla.Rows.Count + " socio(s) con rutinas asignadas";
            }
            catch (Exception ex)
            {
                AyudaFormularioVisual.MostrarError(lblEstado, ex);
            }
        }

        /* Al cargar la pantalla en ejecución, prepara sus datos iniciales sin realizar consultas desde el diseñador. */
        private void MisSociosFormulario_Load(object origen, EventArgs e)
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
