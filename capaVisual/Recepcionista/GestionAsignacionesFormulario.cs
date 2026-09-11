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
    public partial class GestionAsignacionesFormulario : Form
    {
        private readonly MembresiaEntrenadorLogica logica = new MembresiaEntrenadorLogica();
        private readonly MembresiaLogica membresias = new MembresiaLogica();
        private readonly UsuarioSistemaLogica usuarios = new UsuarioSistemaLogica();
        private int idSeleccionado;
        /* Inicializa los componentes existentes y las dependencias de la pantalla sin consultar la base de datos. */
        public GestionAsignacionesFormulario()
        {
            InitializeComponent();
        }

        /* Carga los usuarios activos con rol de entrenador mostrando una identidad inequívoca. */
        private void CargarEntrenadores()
        {
            entrenador.DataSource = usuarios.ListarPorRol("Entrenador")
                .Select(u => new OpcionEntrenador
                {
                    IdEntrenador = u.IdUsuarioSistema,
                    Texto = u.Apellido + ", " + u.Nombre + " - DNI " + u.DNI
                }).ToList();
            entrenador.DisplayMember = "Texto";
            entrenador.ValueMember = "IdEntrenador";
        }

        /* Carga las membresías para seleccionar su identificador sin exigir escritura manual. */
        private void CargarMembresias()
        {
            membresia.DataSource = membresias.ListarParaGestion()
                .Select(m => new OpcionMembresia
                {
                    IdMembresia = m.IdMembresia,
                    Texto = "#" + m.IdMembresia + " - " + NombreSocio(m) + " - DNI " + DniSocio(m)
                        + " - Plan " + NombrePlan(m) + (m.Estado ? string.Empty : " - Deshabilitada")
                }).ToList();
            membresia.DisplayMember = "Texto";
            membresia.ValueMember = "IdMembresia";
            membresia.SelectedIndex = -1;
        }

        /* Obtiene la membresía seleccionada usando su clave persistida. */
        private int ObtenerIdMembresiaSeleccionada()
        {
            AyudaFormularioVisual.ValidarComboSeleccionado(membresia, "una membresía");
            return Convert.ToInt32(membresia.SelectedValue);
        }

        /* Obtiene el entrenador seleccionado usando su clave persistida. */
        private int ObtenerIdEntrenadorSeleccionado()
        {
            AyudaFormularioVisual.ValidarComboSeleccionado(entrenador, "un entrenador");
            return Convert.ToInt32(entrenador.SelectedValue);
        }

        /* Al hacer clic en asignar, valida la selección y registra la asignación mediante la capa lógica. */
        private void asignar_Click(object origen, EventArgs e)
        {
            try
            {
                var id = ObtenerIdMembresiaSeleccionada();
                var a = logica.AsignarEntrenador(id, ObtenerIdEntrenadorSeleccionado());
                idSeleccionado = a.IdMembresiaEntrenador;
                CargarLista(id);
                AyudaFormularioVisual.MostrarExito(lblEstado, "Entrenador asignado.");
            }
            catch (Exception ex)
            {
                AyudaFormularioVisual.MostrarError(lblEstado, ex);
            }
        }

        /* Al hacer clic en cambiar, reemplaza el entrenador de la membresía mediante la capa lógica. */
        private void cambiar_Click(object origen, EventArgs e)
        {
            try
            {
                var id = ObtenerIdMembresiaSeleccionada();
                var a = logica.CambiarEntrenador(id, ObtenerIdEntrenadorSeleccionado());
                idSeleccionado = a.IdMembresiaEntrenador;
                CargarLista(id);
                AyudaFormularioVisual.MostrarExito(lblEstado, "Entrenador cambiado.");
            }
            catch (Exception ex)
            {
                AyudaFormularioVisual.MostrarError(lblEstado, ex);
            }
        }

        /* Al hacer clic en consultar, consulta el entrenador activo y actualiza el historial de asignaciones. */
        private void consultar_Click(object origen, EventArgs e)
        {
            try
            {
                var id = ObtenerIdMembresiaSeleccionada();
                var activo = logica.ObtenerEntrenadorActivo(id);
                MessageBox.Show(activo == null ? "No hay entrenador activo." : activo.Apellido + ", " + activo.Nombre + " - DNI " + activo.DNI, "Entrenador actual", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarLista(id);
            }
            catch (Exception ex)
            {
                AyudaFormularioVisual.MostrarError(lblEstado, ex);
            }
        }

        /* Consulta las asignaciones de la membresía y muestra el entrenador y su estado histórico. */
        private void CargarLista(int id)
        {
            tabla.Rows.Clear();
            foreach (var a in logica.ListarPorMembresia(id))
                tabla.Rows.Add(a.IdMembresiaEntrenador, a.IdMembresia, a.Entrenador == null ? a.IdEntrenador.ToString() : a.Entrenador.Apellido + ", " + a.Entrenador.Nombre + " - DNI " + a.Entrenador.DNI, a.Estado ? "Activo" : "Historico");
        }

        /* Al hacer clic en darDeBaja, solicita la baja lógica del registro seleccionado y actualiza el listado. */
        private void darDeBaja_Click(object origen, EventArgs e)
        {
            try
            {
                if (idSeleccionado == 0)
                    throw new InvalidOperationException("Selecciona una asignacion.");
                logica.DarDeBajaAsignacion(idSeleccionado);
                CargarLista(ObtenerIdMembresiaSeleccionada());
                AyudaFormularioVisual.MostrarExito(lblEstado, "Asignacion dada de baja.");
            }
            catch (Exception ex)
            {
                AyudaFormularioVisual.MostrarError(lblEstado, ex);
            }
        }

        /* Al cargar la pantalla en ejecución, prepara sus datos iniciales sin realizar consultas desde el diseñador. */
        private void GestionAsignacionesFormulario_Load(object origen, EventArgs e)
        {
            if (AyudaFormularioVisual.EnModoDisenio(this))
                return;
            try
            {
                CargarMembresias();
                CargarEntrenadores();
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

        /* Al cambiar la fila seleccionada, toma su identificador y actualiza los datos o acciones del registro. */
        private void tabla_SelectionChanged(object origen, EventArgs e)
        {
            idSeleccionado = tabla.CurrentRow == null || tabla.CurrentRow.Cells[0].Value == null ? 0 : Convert.ToInt32(tabla.CurrentRow.Cells[0].Value);
        }

        /* Devuelve el nombre completo del socio asociado a una membresía. */
        private static string NombreSocio(Membresia membresiaActual)
        {
            return membresiaActual.Socio == null ? "Socio no disponible" : membresiaActual.Socio.Apellido + ", " + membresiaActual.Socio.Nombre;
        }

        /* Devuelve el DNI del socio asociado a una membresía. */
        private static string DniSocio(Membresia membresiaActual)
        {
            return membresiaActual.Socio == null ? "no disponible" : membresiaActual.Socio.DNI;
        }

        /* Devuelve el nombre del plan asociado a una membresía. */
        private static string NombrePlan(Membresia membresiaActual)
        {
            return membresiaActual.Plan == null ? "no disponible" : membresiaActual.Plan.Nombre;
        }

        /* Proyecta una membresía a un texto visible sin perder su clave persistida. */
        private sealed class OpcionMembresia
        {
            public int IdMembresia { get; set; }
            public string Texto { get; set; }
        }

        /* Proyecta un entrenador a un texto visible sin usar el apellido como identificador. */
        private sealed class OpcionEntrenador
        {
            public int IdEntrenador { get; set; }
            public string Texto { get; set; }
        }
    }
}
