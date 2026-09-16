using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaLogica;

namespace exxen2._0.capaVisual.Compartido
{
    /* Presenta el catálogo de ejercicios y su ficha de edición mediante un flujo master/detail. */
    [DesignerCategory("Form")]
    public partial class GestionEjerciciosFormulario : Form
    {
        private readonly EjercicioLogica logica = new EjercicioLogica();
        private int idSeleccionado;
        private bool estadoSeleccionado = true;
        private bool cargandoTabla;

        /* Inicializa los componentes existentes y las dependencias de la pantalla sin consultar la base de datos. */
        public GestionEjerciciosFormulario() : this(Color.FromArgb(79, 70, 229)) { }

        /* Inicializa los componentes existentes y las dependencias de la pantalla sin consultar la base de datos. */
        public GestionEjerciciosFormulario(Color colorPrimario)
        {
            InitializeComponent();
            panelEncabezado.BackColor = colorPrimario;
            btnVolver.ForeColor = colorPrimario;
        }

        /* Consulta, filtra y actualiza el catálogo manteniendo la ficha en un estado coherente. */
        private void Cargar()
        {
            try
            {
                var ejercicios = logica.ListarParaGestion();
                var criterio = buscador.Text.Trim();
                var estadoElegido = Convert.ToString(filtroEstado.SelectedItem);
                if (!string.IsNullOrWhiteSpace(criterio))
                    ejercicios = ejercicios.Where(e => Contiene(e.Nombre, criterio)).ToList();
                if (estadoElegido == "Activos")
                    ejercicios = ejercicios.Where(e => e.Estado).ToList();
                else if (estadoElegido == "Inactivos")
                    ejercicios = ejercicios.Where(e => !e.Estado).ToList();

                cargandoTabla = true;
                tabla.Rows.Clear();
                foreach (var ejercicio in ejercicios)
                    tabla.Rows.Add(ejercicio.IdEjercicio, ejercicio.Nombre, ejercicio.Descripcion ?? string.Empty, ejercicio.Estado ? "Activo" : "Inactivo");
                tabla.ClearSelection();
                cargandoTabla = false;
                PrepararNuevo();
                lblEstado.Text = tabla.Rows.Count + " ejercicio(s) encontrado(s)";
            }
            catch (Exception ex) { AyudaFormularioVisual.MostrarError(lblEstado, ex); }
        }

        /* Compara texto visible sin distinguir mayúsculas y admite valores vacíos. */
        private static bool Contiene(string valor, string criterio)
        {
            return !string.IsNullOrEmpty(valor) && valor.IndexOf(criterio, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        /* Al seleccionar una fila, carga automáticamente sus datos y acciones contextuales. */
        private void tabla_SelectionChanged(object origen, EventArgs e)
        {
            if (cargandoTabla || tabla.CurrentRow == null || !tabla.CurrentRow.Selected || tabla.CurrentRow.Cells[0].Value == null)
                return;
            idSeleccionado = Convert.ToInt32(tabla.CurrentRow.Cells[0].Value);
            nombre.Text = Convert.ToString(tabla.CurrentRow.Cells[1].Value);
            descripcion.Text = Convert.ToString(tabla.CurrentRow.Cells[2].Value);
            estadoSeleccionado = Convert.ToString(tabla.CurrentRow.Cells[3].Value) == "Activo";
            lblEstadoValor.Text = estadoSeleccionado ? "Activo" : "Inactivo";
            lblEstadoValor.ForeColor = estadoSeleccionado ? Color.FromArgb(22, 101, 52) : Color.FromArgb(185, 28, 28);
            lblDetalleTitulo.Text = "Ficha / Edición del ejercicio";
            guardar.Text = "Actualizar";
            cancelar.Visible = false;
            darDeBaja.Visible = estadoSeleccionado;
            reactivar.Visible = !estadoSeleccionado;
            guardar.Visible = true;
        }

        /* Prepara la ficha para iniciar un alta sin conservar datos del registro anterior. */
        private void PrepararNuevo()
        {
            idSeleccionado = 0;
            estadoSeleccionado = true;
            lblEstadoValor.Text = "Activo";
            lblEstadoValor.ForeColor = Color.FromArgb(22, 101, 52);
            nombre.Clear();
            descripcion.Clear();
            lblDetalleTitulo.Text = "Nuevo ejercicio";
            guardar.Text = "Guardar";
            guardar.Visible = true;
            cancelar.Visible = false;
            darDeBaja.Visible = false;
            reactivar.Visible = false;
        }

        /* Al hacer clic en nuevo, deja la ficha lista para crear un ejercicio. */
        private void nuevo_Click(object origen, EventArgs e)
        {
            tabla.ClearSelection();
            PrepararNuevo();
            nombre.Focus();
        }

        /* Al hacer clic en cancelar, vuelve al estado de alta limpio de la ficha. */
        private void cancelar_Click(object origen, EventArgs e) { PrepararNuevo(); }

        /* Al hacer clic en guardar o actualizar, valida y persiste el ejercicio mediante la capa lógica. */
        private void guardar_Click(object origen, EventArgs e)
        {
            try
            {
                var eraNuevo = idSeleccionado == 0;
                var ejercicio = new Ejercicio { IdEjercicio = idSeleccionado, Nombre = nombre.Text.Trim(), Descripcion = descripcion.Text.Trim(), Estado = estadoSeleccionado };
                if (eraNuevo) logica.Crear(ejercicio); else logica.Modificar(ejercicio);
                Cargar();
                AyudaFormularioVisual.MostrarExito(lblEstado, eraNuevo ? "Ejercicio guardado correctamente." : "Ejercicio actualizado correctamente.");
            }
            catch (Exception ex) { AyudaFormularioVisual.MostrarError(lblEstado, ex); }
        }

        /* Al hacer clic en dar de baja, conserva el historial y actualiza el catálogo. */
        private void darDeBaja_Click(object origen, EventArgs e)
        {
            try
            {
                if (idSeleccionado == 0) throw new InvalidOperationException("Selecciona un ejercicio.");
                logica.DarDeBaja(idSeleccionado); Cargar();
                AyudaFormularioVisual.MostrarExito(lblEstado, "Ejercicio dado de baja.");
            }
            catch (Exception ex) { AyudaFormularioVisual.MostrarError(lblEstado, ex); }
        }

        /* Al hacer clic en reactivar, recupera el estado activo del ejercicio seleccionado. */
        private void reactivar_Click(object origen, EventArgs e)
        {
            try
            {
                if (idSeleccionado == 0) throw new InvalidOperationException("Selecciona un ejercicio.");
                logica.Reactivar(idSeleccionado); Cargar();
                AyudaFormularioVisual.MostrarExito(lblEstado, "Ejercicio reactivado.");
            }
            catch (Exception ex) { AyudaFormularioVisual.MostrarError(lblEstado, ex); }
        }

        /* Al cargar la pantalla en ejecución, prepara los datos iniciales fuera del diseñador. */
        private void GestionEjerciciosFormulario_Load(object origen, EventArgs e)
        {
            if (!AyudaFormularioVisual.EnModoDisenio(this)) Cargar();
        }

        /* Al hacer clic en volver, cierra el módulo y devuelve el control al panel principal. */
        private void btnVolver_Click(object origen, EventArgs e) { Close(); }

        /* Al cambiar el texto de búsqueda, actualiza el listado sin consultar lógica compleja. */
        private void buscador_TextChanged(object origen, EventArgs e) { Cargar(); }

        /* Al cambiar el filtro de estado, actualiza los registros visibles. */
        private void filtroEstado_SelectedIndexChanged(object origen, EventArgs e)
        {
            if (!AyudaFormularioVisual.EnModoDisenio(this)) Cargar();
        }

        /* Al hacer clic en actualizar listado, vuelve a consultar el catálogo. */
        private void actualizar_Click(object origen, EventArgs e) { Cargar(); }
    }
}
