using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaLogica;

namespace exxen2._0.capaVisual.Compartido
{
    [DesignerCategory("Form")]
    public partial class GestionEjerciciosForm : Form
    {
        private readonly EjercicioLogica logica = new EjercicioLogica();
        private int idSeleccionado;
        private bool estadoSeleccionado = true;

        public GestionEjerciciosForm()
            : this(Color.FromArgb(79, 70, 229))
        {
        }

        public GestionEjerciciosForm(Color colorPrimario)
        {
            InitializeComponent();
            btnVolver.Click += delegate { Close(); };
            filtroEstado.SelectedIndexChanged += delegate { Cargar(); };
            tabla.SelectionChanged += Seleccionar;
            FormularioVisualHelper.AlCargarEnEjecucion(this, delegate { Cargar(); });
        }

        private void Cargar()
        {
            try
            {
                var ejercicios = logica.ListarParaGestion();
                var estadoElegido = Convert.ToString(filtroEstado.SelectedItem);
                if (estadoElegido == "Activos") ejercicios = ejercicios.Where(e => e.Estado).ToList();
                else if (estadoElegido == "Inactivos") ejercicios = ejercicios.Where(e => !e.Estado).ToList();
                tabla.Rows.Clear();
                foreach (var ejercicio in ejercicios)
                    tabla.Rows.Add(ejercicio.IdEjercicio, ejercicio.Nombre, ejercicio.Descripcion,
                        ejercicio.Estado ? "Activo" : "Inactivo");
                lblEstado.Text = tabla.Rows.Count + " ejercicio(s) encontrado(s)";
            }
            catch (Exception ex) { FormularioVisualHelper.MostrarError(lblEstado, ex); }
        }

        private void Seleccionar(object sender, EventArgs e)
        {
            if (tabla.CurrentRow == null) return;
            idSeleccionado = Convert.ToInt32(tabla.CurrentRow.Cells[0].Value);
            nombre.Text = Convert.ToString(tabla.CurrentRow.Cells[1].Value);
            descripcion.Text = Convert.ToString(tabla.CurrentRow.Cells[2].Value);
            estadoSeleccionado = Convert.ToString(tabla.CurrentRow.Cells[3].Value) == "Activo";
            darDeBaja.Enabled = estadoSeleccionado;
            reactivar.Enabled = !estadoSeleccionado;
        }

        private void Guardar(object sender, EventArgs e)
        {
            try
            {
                var ejercicio = new Ejercicio
                {
                    IdEjercicio = idSeleccionado,
                    Nombre = nombre.Text.Trim(),
                    Descripcion = descripcion.Text.Trim(),
                    Estado = estadoSeleccionado
                };
                if (idSeleccionado == 0) logica.Crear(ejercicio); else logica.Modificar(ejercicio);
                idSeleccionado = 0; nombre.Clear(); descripcion.Clear(); Cargar();
                FormularioVisualHelper.MostrarExito(lblEstado, "Ejercicio guardado correctamente.");
            }
            catch (Exception ex) { FormularioVisualHelper.MostrarError(lblEstado, ex); }
        }

        private void DarDeBaja(object sender, EventArgs e)
        {
            try
            {
                if (idSeleccionado == 0) throw new InvalidOperationException("Selecciona un ejercicio.");
                logica.DarDeBaja(idSeleccionado); Cargar();
                FormularioVisualHelper.MostrarExito(lblEstado, "Ejercicio dado de baja.");
            }
            catch (Exception ex) { FormularioVisualHelper.MostrarError(lblEstado, ex); }
        }

        private void Reactivar(object sender, EventArgs e)
        {
            try
            {
                if (idSeleccionado == 0) throw new InvalidOperationException("Selecciona un ejercicio.");
                logica.Reactivar(idSeleccionado); Cargar();
                FormularioVisualHelper.MostrarExito(lblEstado, "Ejercicio reactivado.");
            }
            catch (Exception ex) { FormularioVisualHelper.MostrarError(lblEstado, ex); }
        }
    }
}
