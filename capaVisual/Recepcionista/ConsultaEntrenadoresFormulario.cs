
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using exxen2._0.capaLogica;
using exxen2._0.capaVisual.Compartido;

using exxen2._0.capaLogica.Utilidades;

namespace exxen2._0.capaVisual.Recepcionista
{
    [System.ComponentModel.DesignerCategory("Form")]
    public partial class ConsultaEntrenadoresFormulario : Form
    {
        private readonly ConsultaEntrenadoresLogica consulta = new ConsultaEntrenadoresLogica();
        private List<EntrenadorConsultaItem> entrenadores = new List<EntrenadorConsultaItem>();
        private bool cargando;

        public ConsultaEntrenadoresFormulario()
        {
            InitializeComponent();
        }

        private void Cargar()
        {
            try
            {
                entrenadores = consulta.ListarEntrenadores();
                AplicarFiltro();
            }
            catch (Exception ex) { AyudaFormularioVisual.MostrarError(lblEstado, ex); }
        }

        private void AplicarFiltro()
        {
            var criterio = buscar.Text.Trim();
            var lista = entrenadores.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(criterio))
                lista = lista.Where(e => Contiene(e.NombreCompleto, criterio) || Contiene(e.DNI, criterio));

            cargando = true;
            tablaEntrenadores.Rows.Clear();
            foreach (var entrenador in lista)
                tablaEntrenadores.Rows.Add(entrenador.IdEntrenador, entrenador.NombreCompleto,
                    entrenador.DNI, entrenador.CantidadSocios, entrenador.Estado ? "Activo" : "Inactivo");
            tablaEntrenadores.ClearSelection();
            tablaSocios.Rows.Clear();
            lblSeleccion.Text = "Seleccione un entrenador";
            lblResumen.Text = "";
            cargando = false;
            lblEstado.Text = tablaEntrenadores.Rows.Count + " entrenador(es) encontrado(s)";
        }

        private static bool Contiene(string valor, string criterio)
        {
            return !string.IsNullOrWhiteSpace(valor) &&
                valor.IndexOf(criterio, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private void tablaEntrenadores_SelectionChanged(object sender, EventArgs e)
        {
            if (cargando || tablaEntrenadores.SelectedRows.Count == 0) return;
            try
            {
                var fila = tablaEntrenadores.SelectedRows[0];
                if (fila.Cells[0].Value == null) return;
                var id = Convert.ToInt32(fila.Cells[0].Value);
                var seleccionado = entrenadores.First(item => item.IdEntrenador == id);
                var socios = consulta.ListarSocios(id);
                tablaSocios.Rows.Clear();
                foreach (var socio in socios)
                    tablaSocios.Rows.Add(socio.NombreCompleto, socio.DNI, socio.Plan,
                        socio.Vencimiento.ToString("dd/MM/yyyy"), socio.Rutina,
                        socio.MembresiaActiva ? "Activa" : "Inactiva");
                lblSeleccion.Text = seleccionado.NombreCompleto;
                lblResumen.Text = socios.Count + " socio(s) con asignación activa";
                lblEstado.Text = "Consulta actualizada";
            }
            catch (Exception ex) { AyudaFormularioVisual.MostrarError(lblEstado, ex); }
        }

        private void buscar_TextChanged(object sender, EventArgs e) { if (!cargando) AplicarFiltro(); }
        private void actualizar_Click(object sender, EventArgs e) { Cargar(); }
        private void ConsultaEntrenadoresFormulario_Load(object sender, EventArgs e)
        {
            if (!AyudaFormularioVisual.EnModoDisenio(this)) Cargar();
        }
    }
}
