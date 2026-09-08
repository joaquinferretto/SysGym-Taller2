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
    /* Presenta ejercicios y atiende sus acciones mediante eventos de Windows Forms. */
    [DesignerCategory("Form")]
    public partial class GestionEjerciciosForm : Form
    {
        private readonly EjercicioLogica logica = new EjercicioLogica();
        private int idSeleccionado;
        private bool estadoSeleccionado = true;
        /* Inicializa los componentes existentes y las dependencias de la pantalla sin consultar la base de datos. */
        public GestionEjerciciosForm() : this(Color.FromArgb(79, 70, 229))
        {
        }

        /* Inicializa los componentes existentes y las dependencias de la pantalla sin consultar la base de datos. */
        public GestionEjerciciosForm(Color colorPrimario)
        {
            InitializeComponent();
        }

        /* Consulta los registros del módulo y actualiza la grilla, informando los errores de carga. */
        private void Cargar()
        {
            try
            {
                var ejercicios = logica.ListarParaGestion();
                var estadoElegido = Convert.ToString(filtroEstado.SelectedItem);
                if (estadoElegido == "Activos")
                    ejercicios = ejercicios.Where(e => e.Estado).ToList();
                else if (estadoElegido == "Inactivos")
                    ejercicios = ejercicios.Where(e => !e.Estado).ToList();
                tabla.Rows.Clear();
                foreach (var ejercicio in ejercicios)
                    tabla.Rows.Add(ejercicio.IdEjercicio, ejercicio.Nombre, ejercicio.Descripcion, ejercicio.Estado ? "Activo" : "Inactivo");
                tabla.ClearSelection();
                idSeleccionado = 0;
                estadoSeleccionado = true;
                nombre.Clear();
                descripcion.Clear();
                lblEstado.Text = tabla.Rows.Count + " ejercicio(s) encontrado(s)";
            }
            catch (Exception ex)
            {
                FormularioVisualHelper.MostrarError(lblEstado, ex);
            }
        }

        /* Al cambiar la fila seleccionada, toma su identificador y actualiza los datos o acciones del registro. */
        private void tabla_SelectionChanged(object sender, EventArgs e)
        {
            if (tabla.CurrentRow == null || !tabla.CurrentRow.Selected)
                return;
            idSeleccionado = Convert.ToInt32(tabla.CurrentRow.Cells[0].Value);
            nombre.Text = Convert.ToString(tabla.CurrentRow.Cells[1].Value);
            descripcion.Text = Convert.ToString(tabla.CurrentRow.Cells[2].Value);
            estadoSeleccionado = Convert.ToString(tabla.CurrentRow.Cells[3].Value) == "Activo";
            darDeBaja.Enabled = estadoSeleccionado;
            reactivar.Enabled = !estadoSeleccionado;
        }

        /* Al hacer clic en guardar, valida los campos y envía el registro a la capa lógica. */
        private void guardar_Click(object sender, EventArgs e)
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
                if (idSeleccionado == 0)
                    logica.Crear(ejercicio);
                else
                    logica.Modificar(ejercicio);
                idSeleccionado = 0;
                nombre.Clear();
                descripcion.Clear();
                Cargar();
                FormularioVisualHelper.MostrarExito(lblEstado, "Ejercicio guardado correctamente.");
            }
            catch (Exception ex)
            {
                FormularioVisualHelper.MostrarError(lblEstado, ex);
            }
        }

        /* Al hacer clic en darDeBaja, solicita la baja lógica del registro seleccionado y actualiza el listado. */
        private void darDeBaja_Click(object sender, EventArgs e)
        {
            try
            {
                if (idSeleccionado == 0)
                    throw new InvalidOperationException("Selecciona un ejercicio.");
                logica.DarDeBaja(idSeleccionado);
                Cargar();
                FormularioVisualHelper.MostrarExito(lblEstado, "Ejercicio dado de baja.");
            }
            catch (Exception ex)
            {
                FormularioVisualHelper.MostrarError(lblEstado, ex);
            }
        }

        /* Al hacer clic en reactivar, solicita la reactivación del registro seleccionado y actualiza el listado. */
        private void reactivar_Click(object sender, EventArgs e)
        {
            try
            {
                if (idSeleccionado == 0)
                    throw new InvalidOperationException("Selecciona un ejercicio.");
                logica.Reactivar(idSeleccionado);
                Cargar();
                FormularioVisualHelper.MostrarExito(lblEstado, "Ejercicio reactivado.");
            }
            catch (Exception ex)
            {
                FormularioVisualHelper.MostrarError(lblEstado, ex);
            }
        }

        /* Al cargar la pantalla en ejecución, prepara sus datos iniciales sin realizar consultas desde el diseñador. */
        private void GestionEjerciciosForm_Load(object sender, EventArgs e)
        {
            if (FormularioVisualHelper.EnModoDisenio(this))
                return;
            try
            {
                Cargar();
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

        /* Al cambiar el filtro de estado, actualiza los registros visibles en la grilla. */
        private void filtroEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cargar();
        }

        /* Al hacer clic en actualizar, vuelve a consultar y mostrar los registros del módulo. */
        private void actualizar_Click(object sender, EventArgs e)
        {
            Cargar();
        }
    }
}
