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
    /* Presenta usuarios y atiende sus acciones mediante eventos de Windows Forms. */
    [DesignerCategory("Form")]
    public partial class GestionUsuariosForm : Form
    {
        private readonly UsuarioSistemaLogica logica = new UsuarioSistemaLogica();
        private readonly RolLogica roles = new RolLogica();
        private List<UsuarioSistema> usuariosCargados = new List<UsuarioSistema>();
        private int idSeleccionado;
        private bool cargandoTabla;
        private bool estadoSeleccionado = true;
        /* Inicializa los componentes existentes y las dependencias de la pantalla sin consultar la base de datos. */
        public GestionUsuariosForm()
        {
            InitializeComponent();
        }

        /* Carga los roles activos disponibles para crear o modificar personal. */
        private void CargarRoles()
        {
            rol.DataSource = roles.ListarActivos();
            rol.DisplayMember = "Descripcion";
            rol.ValueMember = "IdRol";
        }

        /* Consulta los registros del módulo y actualiza la grilla, informando los errores de carga. */
        private void Cargar()
        {
            try
            {
                usuariosCargados = logica.ListarParaGestion();
                AplicarFiltro();
            }
            catch (Exception ex)
            {
                FormularioVisualHelper.MostrarError(lblEstado, ex);
            }
        }

        /* Filtra los registros cargados por el criterio ingresado y actualiza la grilla y su contador. */
        private void AplicarFiltro()
        {
            var criterio = buscador.Text.Trim();
            var estadoElegido = Convert.ToString(filtroEstado.SelectedItem);
            var filtrados = usuariosCargados.AsEnumerable();
            if (estadoElegido == "Activos")
                filtrados = filtrados.Where(u => u.Estado);
            else if (estadoElegido == "Inactivos")
                filtrados = filtrados.Where(u => !u.Estado);
            if (!string.IsNullOrWhiteSpace(criterio))
            {
                filtrados = filtrados.Where(u => Contiene(u.Nombre + " " + u.Apellido, criterio) || Contiene(u.DNI, criterio) || Contiene(u.Username, criterio));
            }

            cargandoTabla = true;
            tabla.Rows.Clear();
            foreach (var usuario in filtrados)
            {
                tabla.Rows.Add(usuario.IdUsuarioSistema, usuario.Nombre + " " + usuario.Apellido, usuario.DNI, usuario.Username, usuario.Rol == null ? "-" : usuario.Rol.Descripcion, usuario.Salario.ToString("C", CultureInfo.CurrentCulture), usuario.Estado ? "Activo" : "Inactivo");
            }

            tabla.ClearSelection();
            cargandoTabla = false;
            lblEstado.Text = tabla.Rows.Count + " usuario(s) encontrado(s)";
        }

        /* Compara el texto de búsqueda sin distinguir mayúsculas y admite valores vacíos. */
        private static bool Contiene(string valor, string criterio)
        {
            return !string.IsNullOrEmpty(valor) && valor.IndexOf(criterio, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        /* Al cambiar la fila seleccionada, toma su identificador y actualiza los datos o acciones del registro. */
        private void tabla_SelectionChanged(object sender, EventArgs e)
        {
            if (cargandoTabla || tabla.CurrentRow == null || !tabla.CurrentRow.Selected)
                return;
            try
            {
                idSeleccionado = Convert.ToInt32(tabla.CurrentRow.Cells[0].Value);
                var usuario = logica.ObtenerPorId(idSeleccionado);
                if (usuario == null)
                    return;
                estadoSeleccionado = usuario.Estado;
                nombre.Text = usuario.Nombre;
                apellido.Text = usuario.Apellido;
                dni.Text = usuario.DNI;
                username.Text = usuario.Username;
                password.Clear();
                salario.Text = usuario.Salario.ToString("0.00", CultureInfo.CurrentCulture);
                if (usuario.Rol != null)
                    rol.SelectedValue = usuario.IdRol;
                EstablecerModo(false, usuario.Estado);
            }
            catch (Exception ex)
            {
                FormularioVisualHelper.MostrarError(lblEstado, ex);
            }
        }

        /* Al hacer clic en nuevo, limpia la selección y prepara el registro de nuevos datos. */
        private void nuevo_Click(object sender, EventArgs e)
        {
            idSeleccionado = 0;
            estadoSeleccionado = true;
            nombre.Clear();
            apellido.Clear();
            dni.Clear();
            username.Clear();
            password.Clear();
            salario.Clear();
            if (rol.Items.Count > 0)
                rol.SelectedIndex = 0;
            tabla.ClearSelection();
            EstablecerModo(true, true);
            nombre.Focus();
        }

        /* Habilita las acciones disponibles según la selección y el estado del registro. */
        private void EstablecerModo(bool nuevoRegistro, bool activo)
        {
            lblFormulario.Text = nuevoRegistro ? "Nuevo usuario" : "Editar usuario";
            guardar.Enabled = nuevoRegistro;
            actualizar.Enabled = !nuevoRegistro;
            darDeBaja.Enabled = !nuevoRegistro && activo;
            reactivar.Enabled = !nuevoRegistro && !activo;
        }

        /* Recoge los campos de personal y comprueba que se haya seleccionado un rol. */
        private UsuarioSistema LeerUsuario()
        {
            if (rol.SelectedValue == null)
                throw new InvalidOperationException("Selecciona un rol.");
            return new UsuarioSistema
            {
                IdUsuarioSistema = idSeleccionado,
                Nombre = nombre.Text.Trim(),
                Apellido = apellido.Text.Trim(),
                DNI = dni.Text.Trim(),
                Username = username.Text.Trim(),
                Salario = FormularioVisualHelper.DecimalPositivo(salario, "salario"),
                IdRol = Convert.ToInt32(rol.SelectedValue),
                Estado = estadoSeleccionado
            };
        }

        /* Al hacer clic en guardar, valida los campos y envía el registro a la capa lógica. */
        private void guardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (idSeleccionado != 0)
                    return;
                logica.Crear(LeerUsuario(), password.Text);
                Cargar();
                nuevo_Click(null, EventArgs.Empty);
                FormularioVisualHelper.MostrarExito(lblEstado, "Usuario creado correctamente.");
            }
            catch (Exception ex)
            {
                FormularioVisualHelper.MostrarError(lblEstado, ex);
            }
        }

        /* Al hacer clic en actualizar, valida los campos y guarda las modificaciones mediante la capa lógica. */
        private void actualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (idSeleccionado == 0)
                    throw new InvalidOperationException("Selecciona un usuario.");
                logica.Modificar(LeerUsuario(), password.Text);
                Cargar();
                nuevo_Click(null, EventArgs.Empty);
                FormularioVisualHelper.MostrarExito(lblEstado, "Usuario actualizado correctamente.");
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
                    throw new InvalidOperationException("Selecciona un usuario.");
                if (MessageBox.Show("Dar de baja al usuario seleccionado?", "Confirmar baja", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;
                logica.DarDeBaja(idSeleccionado);
                Cargar();
                nuevo_Click(null, EventArgs.Empty);
                FormularioVisualHelper.MostrarExito(lblEstado, "Usuario dado de baja.");
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
                    throw new InvalidOperationException("Selecciona un usuario.");
                logica.Reactivar(idSeleccionado);
                Cargar();
                nuevo_Click(null, EventArgs.Empty);
                FormularioVisualHelper.MostrarExito(lblEstado, "Usuario reactivado correctamente.");
            }
            catch (Exception ex)
            {
                FormularioVisualHelper.MostrarError(lblEstado, ex);
            }
        }

        /* Al hacer clic en lblPassword, lleva el foco al campo de contraseña. */
        private void lblPassword_Click(object sender, EventArgs e)
        {
            password.Focus();
        }

        /* Al cargar la pantalla en ejecución, prepara sus datos iniciales sin realizar consultas desde el diseñador. */
        private void GestionUsuariosForm_Load(object sender, EventArgs e)
        {
            if (FormularioVisualHelper.EnModoDisenio(this))
                return;
            try
            {
                CargarRoles();
                Cargar();
                nuevo_Click(null, EventArgs.Empty);
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

        /* Al escribir un criterio de búsqueda, filtra los registros que se muestran en la grilla. */
        private void buscador_TextChanged(object sender, EventArgs e)
        {
            AplicarFiltro();
        }

        /* Al cambiar el filtro de estado, actualiza los registros visibles en la grilla. */
        private void filtroEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltro();
        }

        /* Al escribir en el campo, permite números y un único separador decimal mediante la validación visual compartida. */
        private void salario_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormularioVisualHelper.ValidarEntradaDecimal(salario, e);
        }
    }
}
