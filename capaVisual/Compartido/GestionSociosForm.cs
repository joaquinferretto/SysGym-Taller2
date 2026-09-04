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
    public partial class GestionSociosForm : Form
    {
        private readonly SocioLogica logica = new SocioLogica();
        private bool permitirEdicion;
        private List<Socio> sociosCargados = new List<Socio>();
        private int idSeleccionado;
        private bool cargandoTabla;
        private bool estadoSeleccionado = true;

        public GestionSociosForm()
            : this(Color.FromArgb(79, 70, 229))
        {
        }

        public GestionSociosForm(Color colorPrimario, bool permitirEdicion = true)
        {
            this.permitirEdicion = permitirEdicion;
            InitializeComponent();
            btnVolver.Click += delegate { Close(); };
            FormularioVisualHelper.ConfigurarEntradaDecimal(peso);
            FormularioVisualHelper.ConfigurarEntradaDecimal(altura);
            tabla.SelectionChanged += Seleccionar;
            buscador.TextChanged += delegate { AplicarFiltro(); };
            filtroEstado.SelectedIndexChanged += delegate { AplicarFiltro(); };
            FormularioVisualHelper.AlCargarEnEjecucion(this, delegate { Cargar(); NuevoSocio(null, EventArgs.Empty); });

            if (!permitirEdicion)
            {
                nuevo.Visible = false;
                guardar.Visible = false;
                actualizar.Visible = false;
                darDeBaja.Visible = false;
                nombre.ReadOnly = true;
                apellido.ReadOnly = true;
                dni.ReadOnly = true;
                peso.ReadOnly = true;
                altura.ReadOnly = true;
                fechaNacimiento.Enabled = false;
            }
        }

        private void Cargar()
        {
            try
            {
                sociosCargados = logica.ListarParaGestion();
                AplicarFiltro();
            }
            catch (Exception ex) { FormularioVisualHelper.MostrarError(lblEstado, ex); }
        }

        private void AplicarFiltro()
        {
            var criterio = buscador.Text.Trim();
            var estadoElegido = Convert.ToString(filtroEstado.SelectedItem);
            var filtrados = sociosCargados.AsEnumerable();
            if (estadoElegido == "Activos") filtrados = filtrados.Where(s => s.Estado);
            else if (estadoElegido == "Inactivos") filtrados = filtrados.Where(s => !s.Estado);
            if (!string.IsNullOrWhiteSpace(criterio))
            {
                filtrados = filtrados.Where(s =>
                    Contiene(s.Nombre + " " + s.Apellido, criterio) || Contiene(s.DNI, criterio));
            }

            cargandoTabla = true;
            tabla.Rows.Clear();
            foreach (var socio in filtrados)
            {
                tabla.Rows.Add(socio.IdSocio, socio.Apellido + ", " + socio.Nombre, socio.DNI,
                    socio.FechaNacimiento.HasValue ? socio.FechaNacimiento.Value.ToString("dd/MM/yyyy") : "-",
                    socio.Estado ? "Activo" : "Inactivo");
            }
            tabla.ClearSelection();
            cargandoTabla = false;
            lblEstado.Text = tabla.Rows.Count + " socio(s) encontrado(s)";
        }

        private static bool Contiene(string valor, string criterio)
        {
            return !string.IsNullOrEmpty(valor)
                && valor.IndexOf(criterio, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private void Seleccionar(object sender, EventArgs e)
        {
            if (cargandoTabla || tabla.CurrentRow == null || !tabla.CurrentRow.Selected) return;
            try
            {
                idSeleccionado = Convert.ToInt32(tabla.CurrentRow.Cells[0].Value);
                var socio = logica.ObtenerPorId(idSeleccionado);
                if (socio == null) return;
                estadoSeleccionado = socio.Estado;
                nombre.Text = socio.Nombre;
                apellido.Text = socio.Apellido;
                dni.Text = socio.DNI;
                fechaNacimiento.Checked = socio.FechaNacimiento.HasValue;
                if (socio.FechaNacimiento.HasValue) fechaNacimiento.Value = socio.FechaNacimiento.Value;
                peso.Text = socio.Peso.HasValue ? socio.Peso.Value.ToString("0.##") : string.Empty;
                altura.Text = socio.Altura.HasValue ? socio.Altura.Value.ToString("0.00") : string.Empty;
                EstablecerModo(false, socio.Estado);
                lblFormulario.Text = "Editar socio - Estado: " + (socio.Estado ? "Activo" : "Inactivo");
            }
            catch (Exception ex) { FormularioVisualHelper.MostrarError(lblEstado, ex); }
        }

        private void NuevoSocio(object sender, EventArgs e)
        {
            idSeleccionado = 0;
            estadoSeleccionado = true;
            nombre.Clear(); apellido.Clear(); dni.Clear(); peso.Clear(); altura.Clear();
            fechaNacimiento.Value = DateTime.Today.AddYears(-18);
            fechaNacimiento.Checked = false;
            tabla.ClearSelection();
            EstablecerModo(true, true);
            if (permitirEdicion) nombre.Focus();
        }

        private void EstablecerModo(bool nuevoRegistro, bool activo)
        {
            lblFormulario.Text = nuevoRegistro ? "Nuevo socio - Estado inicial: Activo" : "Editar socio";
            guardar.Enabled = permitirEdicion && nuevoRegistro;
            actualizar.Enabled = permitirEdicion && !nuevoRegistro;
            darDeBaja.Enabled = permitirEdicion && !nuevoRegistro && activo;
            reactivar.Enabled = permitirEdicion && !nuevoRegistro && !activo;
        }

        private Socio LeerSocio()
        {
            return new Socio
            {
                IdSocio = idSeleccionado,
                Nombre = nombre.Text.Trim(),
                Apellido = apellido.Text.Trim(),
                DNI = dni.Text.Trim(),
                FechaNacimiento = fechaNacimiento.Checked ? (DateTime?)fechaNacimiento.Value.Date : null,
                Peso = string.IsNullOrWhiteSpace(peso.Text) ? (decimal?)null : FormularioVisualHelper.DecimalPositivo(peso, "peso"),
                Altura = string.IsNullOrWhiteSpace(altura.Text) ? (decimal?)null : FormularioVisualHelper.DecimalPositivo(altura, "altura"),
                Estado = estadoSeleccionado
            };
        }

        private void GuardarNuevo(object sender, EventArgs e)
        {
            try
            {
                if (!permitirEdicion || idSeleccionado != 0) return;
                logica.Crear(LeerSocio());
                Cargar(); NuevoSocio(null, EventArgs.Empty);
                FormularioVisualHelper.MostrarExito(lblEstado, "Socio creado correctamente.");
            }
            catch (Exception ex) { FormularioVisualHelper.MostrarError(lblEstado, ex); }
        }

        private void Actualizar(object sender, EventArgs e)
        {
            try
            {
                if (!permitirEdicion || idSeleccionado == 0) throw new InvalidOperationException("Selecciona un socio.");
                logica.Modificar(LeerSocio());
                Cargar(); NuevoSocio(null, EventArgs.Empty);
                FormularioVisualHelper.MostrarExito(lblEstado, "Socio actualizado correctamente.");
            }
            catch (Exception ex) { FormularioVisualHelper.MostrarError(lblEstado, ex); }
        }

        private void DarDeBaja(object sender, EventArgs e)
        {
            try
            {
                if (!permitirEdicion || idSeleccionado == 0) throw new InvalidOperationException("Selecciona un socio.");
                if (MessageBox.Show("Dar de baja al socio seleccionado?", "Confirmar baja",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
                logica.DarDeBaja(idSeleccionado);
                Cargar(); NuevoSocio(null, EventArgs.Empty);
                FormularioVisualHelper.MostrarExito(lblEstado, "Socio dado de baja.");
            }
            catch (Exception ex) { FormularioVisualHelper.MostrarError(lblEstado, ex); }
        }

        private void Reactivar(object sender, EventArgs e)
        {
            try
            {
                if (!permitirEdicion || idSeleccionado == 0) throw new InvalidOperationException("Selecciona un socio.");
                logica.Reactivar(idSeleccionado);
                Cargar(); NuevoSocio(null, EventArgs.Empty);
                FormularioVisualHelper.MostrarExito(lblEstado, "Socio reactivado correctamente.");
            }
            catch (Exception ex) { FormularioVisualHelper.MostrarError(lblEstado, ex); }
        }

        private void CalcularImc(object sender, EventArgs e)
        {
            try
            {
                var socio = new Socio
                {
                    Peso = FormularioVisualHelper.DecimalPositivo(peso, "peso"),
                    Altura = FormularioVisualHelper.DecimalPositivo(altura, "altura")
                };
                MessageBox.Show("IMC: " + logica.CalcularIMC(socio).ToString("0.00"),
                    "Indice de masa corporal", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { FormularioVisualHelper.MostrarError(lblEstado, ex); }
        }

        private void tabla_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
