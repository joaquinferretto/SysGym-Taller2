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
    public partial class GestionAsistenciasForm : Form
    {
        private readonly AsistenciaLogica logica = new AsistenciaLogica();
        private readonly SocioLogica socios = new SocioLogica();
        private int idSeleccionado;
        private bool estadoSeleccionado = true;

        public GestionAsistenciasForm()
            : this(Color.FromArgb(79, 70, 229))
        {
        }

        public GestionAsistenciasForm(Color colorPrimario)
        {
            InitializeComponent();
            btnVolver.Click += delegate { Close(); };
            fecha.Value = DateTime.Now;
            filtroEstado.SelectedIndexChanged += delegate { Cargar(); };
            fecha.ValueChanged += delegate { if (!FormularioVisualHelper.EnModoDisenio(this)) Cargar(); };
            tabla.SelectionChanged += delegate
            {
                if (tabla.CurrentRow == null) return;
                idSeleccionado = Convert.ToInt32(tabla.CurrentRow.Cells[0].Value);
                estadoSeleccionado = Convert.ToString(tabla.CurrentRow.Cells[4].Value) == "Activo";
                darDeBaja.Enabled = estadoSeleccionado;
                reactivar.Enabled = !estadoSeleccionado;
            };
            FormularioVisualHelper.AlCargarEnEjecucion(this, delegate { CargarSocios(); Cargar(); });
        }

        private void CargarSocios()
        {
            socio.DataSource = socios.ListarActivos();
            socio.DisplayMember = "Apellido";
            socio.ValueMember = "IdSocio";
        }

        private void Cargar()
        {
            try
            {
                var asistencias = logica.ListarPorFechaParaGestion(fecha.Value);
                var estadoElegido = Convert.ToString(filtroEstado.SelectedItem);
                if (estadoElegido == "Activos") asistencias = asistencias.Where(a => a.Estado).ToList();
                else if (estadoElegido == "Inactivos") asistencias = asistencias.Where(a => !a.Estado).ToList();
                tabla.Rows.Clear();
                foreach (var asistencia in asistencias)
                {
                    tabla.Rows.Add(asistencia.IdAsistencia, asistencia.Fecha.ToString("dd/MM/yyyy HH:mm"),
                        asistencia.Socio == null ? asistencia.IdSocio.ToString() : asistencia.Socio.Apellido + ", " + asistencia.Socio.Nombre,
                        asistencia.Descripcion, asistencia.Estado ? "Activo" : "Inactivo");
                }
                lblEstado.Text = tabla.Rows.Count + " asistencia(s) para la fecha seleccionada";
            }
            catch (Exception ex) { FormularioVisualHelper.MostrarError(lblEstado, ex); }
        }

        private void Registrar(object sender, EventArgs e)
        {
            try
            {
                logica.Registrar(new Asistencia
                {
                    IdSocio = Convert.ToInt32(socio.SelectedValue),
                    Fecha = fecha.Value,
                    Descripcion = "Ingreso registrado"
                });
                Cargar(); FormularioVisualHelper.MostrarExito(lblEstado, "Asistencia registrada.");
            }
            catch (Exception ex) { FormularioVisualHelper.MostrarError(lblEstado, ex); }
        }

        private void DarDeBaja(object sender, EventArgs e)
        {
            try
            {
                if (idSeleccionado == 0) throw new InvalidOperationException("Selecciona una asistencia.");
                logica.DarDeBaja(idSeleccionado); Cargar(); FormularioVisualHelper.MostrarExito(lblEstado, "Asistencia anulada.");
            }
            catch (Exception ex) { FormularioVisualHelper.MostrarError(lblEstado, ex); }
        }

        private void Reactivar(object sender, EventArgs e)
        {
            try
            {
                if (idSeleccionado == 0) throw new InvalidOperationException("Selecciona una asistencia.");
                logica.Reactivar(idSeleccionado); Cargar(); FormularioVisualHelper.MostrarExito(lblEstado, "Asistencia reactivada.");
            }
            catch (Exception ex) { FormularioVisualHelper.MostrarError(lblEstado, ex); }
        }
    }
}
