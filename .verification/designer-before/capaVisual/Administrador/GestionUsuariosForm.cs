using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaLogica;
using exxen2._0.capaVisual.Compartido;

namespace exxen2._0.capaVisual.Administrador
{
    [DesignerCategory("Form")]
    public partial class GestionUsuariosForm : Form
    {
        private readonly UsuarioSistemaLogica logica = new UsuarioSistemaLogica();
        private readonly RolLogica roles = new RolLogica();
        private List<UsuarioSistema> usuariosCargados = new List<UsuarioSistema>();
        private int idSeleccionado;
        private bool cargandoTabla;
        private bool estadoSeleccionado = true;

        public GestionUsuariosForm()
        {
            InitializeComponent();
            btnVolver.Click += delegate { Close(); };
            FormularioVisualHelper.ConfigurarEntradaDecimal(salario);
            tabla.SelectionChanged += Seleccionar;
            buscador.TextChanged += delegate { AplicarFiltro(); };
            filtroEstado.SelectedIndexChanged += delegate { AplicarFiltro(); };
            FormularioVisualHelper.AlCargarEnEjecucion(this, delegate { CargarRoles(); Cargar(); NuevoUsuario(null, EventArgs.Empty); });
        }

        private void CargarRoles()
        {
            rol.DataSource = roles.ListarActivos();
            rol.DisplayMember = "Descripcion";
            rol.ValueMember = "IdRol";
        }

        private void Cargar()
        {
            try
            {
                usuariosCargados = logica.ListarParaGestion();
                AplicarFiltro();
            }
            catch (Exception ex) { FormularioVisualHelper.MostrarError(lblEstado, ex); }
        }

        private void AplicarFiltro()
        {
            var criterio = buscador.Text.Trim();
            var estadoElegido = Convert.ToString(filtroEstado.SelectedItem);
            var filtrados = usuariosCargados.AsEnumerable();
            if (estadoElegido == "Activos") filtrados = filtrados.Where(u => u.Estado);
            else if (estadoElegido == "Inactivos") filtrados = filtrados.Where(u => !u.Estado);
            if (!string.IsNullOrWhiteSpace(criterio))
            {
                filtrados = filtrados.Where(u => Contiene(u.Nombre + " " + u.Apellido, criterio)
                    || Contiene(u.DNI, criterio) || Contiene(u.Username, criterio));
            }

            cargandoTabla = true;
            tabla.Rows.Clear();
            foreach (var usuario in filtrados)
            {
                tabla.Rows.Add(usuario.IdUsuarioSistema, usuario.Nombre + " " + usuario.Apellido, usuario.DNI,
                    usuario.Username, usuario.Rol == null ? "-" : usuario.Rol.Descripcion,
                    usuario.Salario.ToString("C", CultureInfo.CurrentCulture), usuario.Estado ? "Activo" : "Inactivo");
            }
            tabla.ClearSelection();
            cargandoTabla = false;
            lblEstado.Text = tabla.Rows.Count + " usuario(s) encontrado(s)";
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
                var usuario = logica.ObtenerPorId(idSeleccionado);
                if (usuario == null) return;
                estadoSeleccionado = usuario.Estado;
                nombre.Text = usuario.Nombre; apellido.Text = usuario.Apellido; dni.Text = usuario.DNI;
                username.Text = usuario.Username; password.Clear();
                salario.Text = usuario.Salario.ToString("0.00", CultureInfo.CurrentCulture);
                if (usuario.Rol != null) rol.SelectedValue = usuario.IdRol;
                EstablecerModo(false, usuario.Estado);
            }
            catch (Exception ex) { FormularioVisualHelper.MostrarError(lblEstado, ex); }
        }

        private void NuevoUsuario(object sender, EventArgs e)
        {
            idSeleccionado = 0; estadoSeleccionado = true;
            nombre.Clear(); apellido.Clear(); dni.Clear(); username.Clear(); password.Clear(); salario.Clear();
            if (rol.Items.Count > 0) rol.SelectedIndex = 0;
            tabla.ClearSelection(); EstablecerModo(true, true); nombre.Focus();
        }

        private void EstablecerModo(bool nuevoRegistro, bool activo)
        {
            lblFormulario.Text = nuevoRegistro ? "Nuevo usuario" : "Editar usuario";
            guardar.Enabled = nuevoRegistro; actualizar.Enabled = !nuevoRegistro;
            darDeBaja.Enabled = !nuevoRegistro && activo; reactivar.Enabled = !nuevoRegistro && !activo;
        }

        private UsuarioSistema LeerUsuario()
        {
            if (rol.SelectedValue == null) throw new InvalidOperationException("Selecciona un rol.");
            return new UsuarioSistema
            {
                IdUsuarioSistema = idSeleccionado, Nombre = nombre.Text.Trim(), Apellido = apellido.Text.Trim(),
                DNI = dni.Text.Trim(), Username = username.Text.Trim(), Salario = FormularioVisualHelper.DecimalPositivo(salario, "salario"),
                IdRol = Convert.ToInt32(rol.SelectedValue), Estado = estadoSeleccionado
            };
        }

        private void GuardarNuevo(object sender, EventArgs e)
        {
            try { if (idSeleccionado != 0) return; logica.Crear(LeerUsuario(), password.Text); Cargar(); NuevoUsuario(null, EventArgs.Empty); FormularioVisualHelper.MostrarExito(lblEstado, "Usuario creado correctamente."); }
            catch (Exception ex) { FormularioVisualHelper.MostrarError(lblEstado, ex); }
        }

        private void Actualizar(object sender, EventArgs e)
        {
            try { if (idSeleccionado == 0) throw new InvalidOperationException("Selecciona un usuario."); logica.Modificar(LeerUsuario(), password.Text); Cargar(); NuevoUsuario(null, EventArgs.Empty); FormularioVisualHelper.MostrarExito(lblEstado, "Usuario actualizado correctamente."); }
            catch (Exception ex) { FormularioVisualHelper.MostrarError(lblEstado, ex); }
        }

        private void DarDeBaja(object sender, EventArgs e)
        {
            try
            {
                if (idSeleccionado == 0) throw new InvalidOperationException("Selecciona un usuario.");
                if (MessageBox.Show("Dar de baja al usuario seleccionado?", "Confirmar baja", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
                logica.DarDeBaja(idSeleccionado); Cargar(); NuevoUsuario(null, EventArgs.Empty); FormularioVisualHelper.MostrarExito(lblEstado, "Usuario dado de baja.");
            }
            catch (Exception ex) { FormularioVisualHelper.MostrarError(lblEstado, ex); }
        }

        private void Reactivar(object sender, EventArgs e)
        {
            try { if (idSeleccionado == 0) throw new InvalidOperationException("Selecciona un usuario."); logica.Reactivar(idSeleccionado); Cargar(); NuevoUsuario(null, EventArgs.Empty); FormularioVisualHelper.MostrarExito(lblEstado, "Usuario reactivado correctamente."); }
            catch (Exception ex) { FormularioVisualHelper.MostrarError(lblEstado, ex); }
        }

        private void lblPassword_Click(object sender, EventArgs e)
        {

        }
    }
}
