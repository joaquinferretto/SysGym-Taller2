using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaLogica;
using exxen2._0.capaVisual.Compartido;

namespace exxen2._0.capaVisual.Recepcionista
{
    /* Presenta asignaciones y atiende sus acciones mediante eventos de Windows Forms. */
    [DesignerCategory("Form")]
    public partial class GestionAsignacionesForm : Form
    {
        private readonly MembresiaEntrenadorLogica logica = new MembresiaEntrenadorLogica();
        private readonly UsuarioSistemaLogica usuarios = new UsuarioSistemaLogica();
        private int idSeleccionado;
        /* Inicializa los componentes existentes y las dependencias de la pantalla sin consultar la base de datos. */
        public GestionAsignacionesForm()
        {
            InitializeComponent();
        }

        /* Carga los usuarios activos con rol de entrenador para realizar asignaciones. */
        private void CargarEntrenadores()
        {
            entrenador.DataSource = usuarios.ListarPorRol("Entrenador");
            entrenador.DisplayMember = "Apellido";
            entrenador.ValueMember = "IdUsuarioSistema";
        }

        /* Al hacer clic en asignar, valida la selección y registra la asignación mediante la capa lógica. */
        private void asignar_Click(object sender, EventArgs e)
        {
            try
            {
                var id = FormularioVisualHelper.Entero(membresia, "membresia");
                var a = logica.AsignarEntrenador(id, Convert.ToInt32(entrenador.SelectedValue));
                idSeleccionado = a.IdMembresiaEntrenador;
                CargarLista(id);
                FormularioVisualHelper.MostrarExito(lblEstado, "Entrenador asignado.");
            }
            catch (Exception ex)
            {
                FormularioVisualHelper.MostrarError(lblEstado, ex);
            }
        }

        /* Al hacer clic en cambiar, reemplaza el entrenador de la membresía mediante la capa lógica. */
        private void cambiar_Click(object sender, EventArgs e)
        {
            try
            {
                var id = FormularioVisualHelper.Entero(membresia, "membresia");
                var a = logica.CambiarEntrenador(id, Convert.ToInt32(entrenador.SelectedValue));
                idSeleccionado = a.IdMembresiaEntrenador;
                CargarLista(id);
                FormularioVisualHelper.MostrarExito(lblEstado, "Entrenador cambiado.");
            }
            catch (Exception ex)
            {
                FormularioVisualHelper.MostrarError(lblEstado, ex);
            }
        }

        /* Al hacer clic en consultar, consulta el entrenador activo y actualiza el historial de asignaciones. */
        private void consultar_Click(object sender, EventArgs e)
        {
            try
            {
                var id = FormularioVisualHelper.Entero(membresia, "membresia");
                var activo = logica.ObtenerEntrenadorActivo(id);
                MessageBox.Show(activo == null ? "No hay entrenador activo." : activo.Nombre + " " + activo.Apellido, "Entrenador actual", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarLista(id);
            }
            catch (Exception ex)
            {
                FormularioVisualHelper.MostrarError(lblEstado, ex);
            }
        }

        /* Consulta las asignaciones de la membresía y muestra el entrenador y su estado histórico. */
        private void CargarLista(int id)
        {
            tabla.Rows.Clear();
            foreach (var a in logica.ListarPorMembresia(id))
                tabla.Rows.Add(a.IdMembresiaEntrenador, a.IdMembresia, a.Entrenador == null ? a.IdEntrenador.ToString() : a.Entrenador.Nombre + " " + a.Entrenador.Apellido, a.Estado ? "Activo" : "Historico");
        }

        /* Al hacer clic en darDeBaja, solicita la baja lógica del registro seleccionado y actualiza el listado. */
        private void darDeBaja_Click(object sender, EventArgs e)
        {
            try
            {
                if (idSeleccionado == 0)
                    throw new InvalidOperationException("Selecciona una asignacion.");
                logica.DarDeBajaAsignacion(idSeleccionado);
                CargarLista(FormularioVisualHelper.Entero(membresia, "membresia"));
                FormularioVisualHelper.MostrarExito(lblEstado, "Asignacion dada de baja.");
            }
            catch (Exception ex)
            {
                FormularioVisualHelper.MostrarError(lblEstado, ex);
            }
        }

        /* Al cargar la pantalla en ejecución, prepara sus datos iniciales sin realizar consultas desde el diseñador. */
        private void GestionAsignacionesForm_Load(object sender, EventArgs e)
        {
            if (FormularioVisualHelper.EnModoDisenio(this))
                return;
            try
            {
                CargarEntrenadores();
            }
            catch (Exception ex)
            {
                FormularioVisualHelper.MostrarError(lblEstado, ex);
            }
        }

        /* Al hacer clic en btnVolver, cierra el módulo y devuelve el control al dashboard. */
        private void btnVolver_Click(object sender, EventArgs e)
        {
            Close();
        }

        /* Al cambiar la fila seleccionada, toma su identificador y actualiza los datos o acciones del registro. */
        private void tabla_SelectionChanged(object sender, EventArgs e)
        {
            idSeleccionado = tabla.CurrentRow == null || tabla.CurrentRow.Cells[0].Value == null ? 0 : Convert.ToInt32(tabla.CurrentRow.Cells[0].Value);
        }
    }
}
