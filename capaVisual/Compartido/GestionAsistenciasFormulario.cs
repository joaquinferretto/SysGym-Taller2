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
    /* Presenta asistencias y atiende sus acciones mediante eventos de Windows Forms. */
    [DesignerCategory("Form")]
    public partial class GestionAsistenciasFormulario : Form
    {
        private readonly AsistenciaLogica logica = new AsistenciaLogica();
        private readonly SocioLogica socios = new SocioLogica();
        private int idSeleccionado;
        private bool estadoSeleccionado = true;
        /* Inicializa los componentes existentes y las dependencias de la pantalla sin consultar la base de datos. */
        public GestionAsistenciasFormulario() : this(Color.FromArgb(79, 70, 229))
        {
        }

        /* Inicializa los componentes existentes y las dependencias de la pantalla sin consultar la base de datos. */
        public GestionAsistenciasFormulario(Color colorPrimario)
        {
            InitializeComponent();
        }

        /* Carga los socios activos mostrando nombre y DNI para evitar confundir personas. */
        private void CargarSocios()
        {
            socio.DataSource = socios.ListarActivos()
                .Select(s => new OpcionSocio
                {
                    IdSocio = s.IdSocio,
                    Texto = s.Apellido + ", " + s.Nombre + " - DNI " + s.DNI
                }).ToList();
            socio.DisplayMember = "Texto";
            socio.ValueMember = "IdSocio";
        }

        /* Consulta los registros del módulo y actualiza la grilla, informando los errores de carga. */
        private void Cargar()
        {
            try
            {
                var asistencias = logica.ListarPorFechaParaGestion(fecha.Value);
                var estadoElegido = Convert.ToString(filtroEstado.SelectedItem);
                if (estadoElegido == "Activos")
                    asistencias = asistencias.Where(a => a.Estado).ToList();
                else if (estadoElegido == "Inactivos")
                    asistencias = asistencias.Where(a => !a.Estado).ToList();
                tabla.Rows.Clear();
                foreach (var asistencia in asistencias)
                {
                    tabla.Rows.Add(asistencia.IdAsistencia, asistencia.Fecha.ToString("dd/MM/yyyy HH:mm"), asistencia.Socio == null ? asistencia.IdSocio.ToString() : asistencia.Socio.Apellido + ", " + asistencia.Socio.Nombre, asistencia.Descripcion, asistencia.Estado ? "Activo" : "Inactivo");
                }

                lblEstado.Text = tabla.Rows.Count + " asistencia(s) para la fecha seleccionada";
            }
            catch (Exception ex)
            {
                AyudaFormularioVisual.MostrarError(lblEstado, ex);
            }
        }

        /* Al hacer clic en registrar, valida los campos y registra la operación mediante la capa lógica. */
        private void registrar_Click(object origen, EventArgs e)
        {
            try
            {
                AyudaFormularioVisual.ValidarComboSeleccionado(socio, "un socio");
                logica.Registrar(new Asistencia { IdSocio = Convert.ToInt32(socio.SelectedValue), Fecha = fecha.Value, Descripcion = "Ingreso registrado" });
                Cargar();
                AyudaFormularioVisual.MostrarExito(lblEstado, "Asistencia registrada.");
            }
            catch (Exception ex)
            {
                AyudaFormularioVisual.MostrarError(lblEstado, ex);
            }
        }

        /* Al hacer clic en darDeBaja, solicita la baja lógica del registro seleccionado y actualiza el listado. */
        private void darDeBaja_Click(object origen, EventArgs e)
        {
            try
            {
                if (idSeleccionado == 0)
                    throw new InvalidOperationException("Selecciona una asistencia.");
                logica.DarDeBaja(idSeleccionado);
                Cargar();
                AyudaFormularioVisual.MostrarExito(lblEstado, "Asistencia anulada.");
            }
            catch (Exception ex)
            {
                AyudaFormularioVisual.MostrarError(lblEstado, ex);
            }
        }

        /* Al hacer clic en reactivar, solicita la reactivación del registro seleccionado y actualiza el listado. */
        private void reactivar_Click(object origen, EventArgs e)
        {
            try
            {
                if (idSeleccionado == 0)
                    throw new InvalidOperationException("Selecciona una asistencia.");
                logica.Reactivar(idSeleccionado);
                Cargar();
                AyudaFormularioVisual.MostrarExito(lblEstado, "Asistencia reactivada.");
            }
            catch (Exception ex)
            {
                AyudaFormularioVisual.MostrarError(lblEstado, ex);
            }
        }

        /* Al cargar la pantalla en ejecución, prepara sus datos iniciales sin realizar consultas desde el diseñador. */
        private void GestionAsistenciasFormulario_Load(object origen, EventArgs e)
        {
            if (AyudaFormularioVisual.EnModoDisenio(this))
                return;
            try
            {
                fecha.Value = DateTime.Now;
                CargarSocios();
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

        /* Al cambiar el filtro de estado, actualiza los registros visibles en la grilla. */
        private void filtroEstado_SelectedIndexChanged(object origen, EventArgs e)
        {
            Cargar();
        }

        /* Al cambiar la fecha de consulta, recarga las asistencias de ese día en ejecución. */
        private void fecha_ValueChanged(object origen, EventArgs e)
        {
            if (!AyudaFormularioVisual.EnModoDisenio(this))
                Cargar();
        }

        /* Al cambiar la fila seleccionada, toma su identificador y actualiza los datos o acciones del registro. */
        private void tabla_SelectionChanged(object origen, EventArgs e)
        {
            if (tabla.CurrentRow == null)
                return;
            idSeleccionado = Convert.ToInt32(tabla.CurrentRow.Cells[0].Value);
            estadoSeleccionado = Convert.ToString(tabla.CurrentRow.Cells[4].Value) == "Activo";
            darDeBaja.Enabled = estadoSeleccionado;
            reactivar.Enabled = !estadoSeleccionado;
        }

        /* Conserva la clave del socio y separa el texto visible de la identidad persistida. */
        private sealed class OpcionSocio
        {
            public int IdSocio { get; set; }
            public string Texto { get; set; }
        }

        /* Al hacer clic en actualizar, vuelve a consultar y mostrar los registros del módulo. */
        private void actualizar_Click(object origen, EventArgs e)
        {
            Cargar();
        }
    }
}
