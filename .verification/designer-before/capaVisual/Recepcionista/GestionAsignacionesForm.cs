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
    [DesignerCategory("Form")]
    public partial class GestionAsignacionesForm : Form
    {
        private readonly MembresiaEntrenadorLogica logica = new MembresiaEntrenadorLogica();
        private readonly UsuarioSistemaLogica usuarios = new UsuarioSistemaLogica();
        private int idSeleccionado;

        public GestionAsignacionesForm()
        {
            InitializeComponent();
            btnVolver.Click += delegate { Close(); };
            FormularioVisualHelper.AlCargarEnEjecucion(this, delegate { CargarEntrenadores(); });
            asignar.Click += Asignar; cambiar.Click += Cambiar; consultar.Click += Consultar; darDeBaja.Click += DarDeBaja;
        }

        private void CargarEntrenadores() { entrenador.DataSource = usuarios.ListarPorRol("Entrenador"); entrenador.DisplayMember = "Apellido"; entrenador.ValueMember = "IdUsuarioSistema"; }
        private void Asignar(object sender, EventArgs e) { try { var id = FormularioVisualHelper.Entero(membresia, "membresia"); var a = logica.AsignarEntrenador(id, Convert.ToInt32(entrenador.SelectedValue)); idSeleccionado = a.IdMembresiaEntrenador; CargarLista(id); FormularioVisualHelper.MostrarExito(lblEstado, "Entrenador asignado."); } catch (Exception ex) { FormularioVisualHelper.MostrarError(lblEstado, ex); } }
        private void Cambiar(object sender, EventArgs e) { try { var id = FormularioVisualHelper.Entero(membresia, "membresia"); var a = logica.CambiarEntrenador(id, Convert.ToInt32(entrenador.SelectedValue)); idSeleccionado = a.IdMembresiaEntrenador; CargarLista(id); FormularioVisualHelper.MostrarExito(lblEstado, "Entrenador cambiado."); } catch (Exception ex) { FormularioVisualHelper.MostrarError(lblEstado, ex); } }
        private void Consultar(object sender, EventArgs e) { try { var id = FormularioVisualHelper.Entero(membresia, "membresia"); var activo = logica.ObtenerEntrenadorActivo(id); MessageBox.Show(activo == null ? "No hay entrenador activo." : activo.Nombre + " " + activo.Apellido, "Entrenador actual", MessageBoxButtons.OK, MessageBoxIcon.Information); CargarLista(id); } catch (Exception ex) { FormularioVisualHelper.MostrarError(lblEstado, ex); } }
        private void CargarLista(int id) { tabla.Rows.Clear(); foreach (var a in logica.ListarPorMembresia(id)) tabla.Rows.Add(a.IdMembresiaEntrenador, a.IdMembresia, a.Entrenador == null ? a.IdEntrenador.ToString() : a.Entrenador.Nombre + " " + a.Entrenador.Apellido, a.Estado ? "Activo" : "Historico"); }
        private void DarDeBaja(object sender, EventArgs e) { try { if (idSeleccionado == 0) throw new InvalidOperationException("Selecciona una asignacion."); logica.DarDeBajaAsignacion(idSeleccionado); CargarLista(FormularioVisualHelper.Entero(membresia, "membresia")); FormularioVisualHelper.MostrarExito(lblEstado, "Asignacion dada de baja."); } catch (Exception ex) { FormularioVisualHelper.MostrarError(lblEstado, ex); } }
    }
}
