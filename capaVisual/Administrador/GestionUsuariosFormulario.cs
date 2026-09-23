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

using exxen2._0.capaLogica.Utilidades;

namespace exxen2._0.capaVisual.Administrador
{
    /* Presenta usuarios y atiende sus acciones mediante eventos de Windows Forms. */
    [DesignerCategory("Form")]
    public partial class GestionUsuariosFormulario : Form
    {
        private readonly UsuarioSistemaLogica logica = new UsuarioSistemaLogica();
        private readonly RolLogica roles = new RolLogica();
        /* Avisa al panel contenedor que se guardaron cambios de un usuario (por ejemplo, para refrescar su encabezado). */
        public event EventHandler<UsuarioActualizadoEventArgs> UsuarioActualizado;
        private List<UsuarioSistema> usuariosCargados = new List<UsuarioSistema>();
        private int idSeleccionado;
        private bool cargandoTabla;
        private bool estadoSeleccionado = true;
        private ImagenSeleccionada fotoSeleccionada;
        private string fotoRutaSeleccionada;
        /* Inicializa los componentes existentes y las dependencias de la pantalla sin consultar la base de datos. */
        public GestionUsuariosFormulario()
        {
            InitializeComponent();
            ConfigurarValidaciones();
        }

        private void ConfigurarValidaciones()
        {
            nombre.Validating += campoNombre_Validating;
            apellido.Validating += campoNombre_Validating;
            dni.Validating += dni_Validating;
            fechaNacimiento.Validating += fechaNacimiento_Validating;
            nombreUsuario.Validating += nombreUsuario_Validating;
            nombreUsuario.KeyPress += nombreUsuario_KeyPress;
            clave.Validating += clave_Validating;
            salario.Validating += salario_Validating;
            rol.Validating += rol_Validating;
            nombre.TextChanged += campoValidado_Cambiado;
            apellido.TextChanged += campoValidado_Cambiado;
            dni.TextChanged += campoValidado_Cambiado;
            fechaNacimiento.ValueChanged += campoValidado_Cambiado;
            nombreUsuario.TextChanged += campoValidado_Cambiado;
            clave.TextChanged += campoValidado_Cambiado;
            salario.TextChanged += campoValidado_Cambiado;
            rol.SelectedIndexChanged += campoValidado_Cambiado;
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
                AyudaFormularioVisual.MostrarError(lblEstado, ex);
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
                filtrados = filtrados.Where(u => Contiene(u.Nombre + " " + u.Apellido, criterio) || Contiene(u.DNI, criterio) || Contiene(u.NombreUsuario, criterio));
            }

            cargandoTabla = true;
            tabla.Rows.Clear();
            foreach (var usuario in filtrados)
            {
                tabla.Rows.Add(usuario.IdUsuarioSistema, usuario.Nombre + " " + usuario.Apellido, usuario.DNI, usuario.NombreUsuario, usuario.Rol == null ? "-" : usuario.Rol.Descripcion, usuario.Salario.ToString("C", CultureInfo.CurrentCulture), usuario.Estado ? "Activo" : "Inactivo");
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
        private void tabla_SelectionChanged(object origen, EventArgs e)
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
                fotoSeleccionada = null;
                fotoRutaSeleccionada = usuario.FotoRuta;
                sexo.SelectedIndex = usuario.Sexo == "M" ? 0 : usuario.Sexo == "F" ? 1 : -1;
                AyudaFormularioVisual.MostrarFotoRuta(fotoUsuario, fotoRutaSeleccionada, usuario.Sexo);
                nombre.Text = usuario.Nombre;
                apellido.Text = usuario.Apellido;
                dni.Text = usuario.DNI;
                fechaNacimiento.Value = usuario.FechaNacimiento ?? fechaNacimiento.MaxDate;
                nombreUsuario.Text = usuario.NombreUsuario;
                clave.Clear();
                salario.Text = usuario.Salario.ToString("0.00", CultureInfo.CurrentCulture);
                if (usuario.Rol != null)
                    rol.SelectedValue = usuario.IdRol;
                indicadorErrores.Clear();
                EstablecerModo(false, usuario.Estado);
            }
            catch (Exception ex)
            {
                AyudaFormularioVisual.MostrarError(lblEstado, ex);
            }
        }

        /* Vuelve a mostrar el usuario después de guardarlo, con sus datos y foto actualizados; si el filtro lo oculta, deja la ficha en blanco. */
        private void SeleccionarUsuario(int idUsuario)
        {
            foreach (DataGridViewRow fila in tabla.Rows)
            {
                if (Convert.ToInt32(fila.Cells[0].Value) != idUsuario)
                    continue;
                tabla.CurrentCell = fila.Cells[1];
                fila.Selected = true;
                tabla_SelectionChanged(tabla, EventArgs.Empty);
                return;
            }

            nuevo_Click(null, EventArgs.Empty);
        }

        /* Al hacer clic en nuevo, limpia la selección y prepara el registro de nuevos datos. */
        private void nuevo_Click(object origen, EventArgs e)
        {
            idSeleccionado = 0;
            estadoSeleccionado = true;
            fotoSeleccionada = null;
            fotoRutaSeleccionada = null;
            sexo.SelectedIndex = -1;
            AyudaFormularioVisual.MostrarFoto(fotoUsuario, null, null);
            nombre.Clear();
            apellido.Clear();
            dni.Clear();
            fechaNacimiento.Value = fechaNacimiento.MaxDate;
            nombreUsuario.Clear();
            clave.Clear();
            salario.Clear();
            if (rol.Items.Count > 0)
                rol.SelectedIndex = 0;
            tabla.ClearSelection();
            indicadorErrores.Clear();
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
                FechaNacimiento = fechaNacimiento.Value.Date,
                NombreUsuario = nombreUsuario.Text.Trim(),
                Salario = AyudaFormularioVisual.DecimalPositivo(salario, "salario"),
                IdRol = Convert.ToInt32(rol.SelectedValue),
                Estado = estadoSeleccionado,
                FotoRuta = fotoRutaSeleccionada,
                Sexo = SexoSeleccionado()
            };
        }

        /* Al hacer clic en guardar, valida los campos y envía el registro a la capa lógica. */
        private void guardar_Click(object origen, EventArgs e)
        {
            try
            {
                if (idSeleccionado != 0)
                    return;
                if (!ValidarFormulario(true))
                    return;
                logica.Crear(LeerUsuario(), clave.Text, fotoSeleccionada == null ? null : fotoSeleccionada.Contenido);
                Cargar();
                nuevo_Click(null, EventArgs.Empty);
                AyudaFormularioVisual.MostrarExito(lblEstado, "Usuario creado correctamente.", true);
                MessageBox.Show("Usuario creado correctamente.", "Usuarios", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                var mensaje = ex is InvalidOperationException || ex is ArgumentException
                    ? ex.Message
                    : "No se pudo crear el usuario.";
                lblEstado.Text = mensaje;
                lblEstado.ForeColor = Color.Red;
                lblEstado.Visible = true;
                MessageBox.Show(mensaje, "Usuarios", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /* Al hacer clic en actualizar, valida los campos y guarda las modificaciones mediante la capa lógica. */
        private void actualizar_Click(object origen, EventArgs e)
        {
            try
            {
                if (idSeleccionado == 0)
                    throw new InvalidOperationException("Selecciona un usuario.");
                if (!ValidarFormulario(false))
                    return;
                var idUsuario = idSeleccionado;
                logica.Modificar(LeerUsuario(), clave.Text, fotoSeleccionada == null ? null : fotoSeleccionada.Contenido);
                Cargar();
                SeleccionarUsuario(idUsuario);
                UsuarioActualizado?.Invoke(this, new UsuarioActualizadoEventArgs(idUsuario));
                AyudaFormularioVisual.MostrarExito(lblEstado, "Usuario actualizado correctamente.");
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
                    throw new InvalidOperationException("Selecciona un usuario.");
                if (MessageBox.Show("Dar de baja al usuario seleccionado?", "Confirmar baja", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;
                logica.DarDeBaja(idSeleccionado);
                Cargar();
                nuevo_Click(null, EventArgs.Empty);
                AyudaFormularioVisual.MostrarExito(lblEstado, "Usuario dado de baja.");
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
                    throw new InvalidOperationException("Selecciona un usuario.");
                logica.Reactivar(idSeleccionado);
                Cargar();
                nuevo_Click(null, EventArgs.Empty);
                AyudaFormularioVisual.MostrarExito(lblEstado, "Usuario reactivado correctamente.");
            }
            catch (Exception ex)
            {
                AyudaFormularioVisual.MostrarError(lblEstado, ex);
            }
        }

        /* Al hacer clic en lblClave, lleva el foco al campo de contraseña. */
        private void lblClave_Click(object origen, EventArgs e)
        {
            clave.Focus();
        }

        /* Al cargar la pantalla en ejecución, prepara sus datos iniciales sin realizar consultas desde el diseñador. */
        private void GestionUsuariosFormulario_Load(object origen, EventArgs e)
        {
            if (AyudaFormularioVisual.EnModoDisenio(this))
                return;
            try
            {
                fechaNacimiento.MaxDate = DateTime.Today.AddYears(-18);
                fechaNacimiento.Value = fechaNacimiento.MaxDate;
                CargarRoles();
                Cargar();
                nuevo_Click(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                AyudaFormularioVisual.MostrarError(lblEstado, ex);
            }
        }

        /* Al escribir un criterio de búsqueda, filtra los registros que se muestran en la grilla. */
        private void buscador_TextChanged(object origen, EventArgs e)
        {
            AplicarFiltro();
        }

        /* Al cambiar el filtro de estado, actualiza los registros visibles en la grilla. */
        private void filtroEstado_SelectedIndexChanged(object origen, EventArgs e)
        {
            AplicarFiltro();
        }

        /* Al escribir en el campo, permite números y un único separador decimal mediante la validación visual compartida. */
        private void salario_KeyPress(object origen, KeyPressEventArgs e)
        {
            AyudaFormularioVisual.ValidarEntradaDecimal(salario, e);
        }

        /* Al escribir el nombre, bloquea caracteres que no pertenecen a un nombre humano. */
        private void nombre_KeyPress(object origen, KeyPressEventArgs e)
        {
            AyudaFormularioVisual.ValidarEntradaNombre(e);
        }

        /* Al escribir el apellido, bloquea números y símbolos no permitidos. */
        private void apellido_KeyPress(object origen, KeyPressEventArgs e)
        {
            AyudaFormularioVisual.ValidarEntradaNombre(e);
        }

        /* Al escribir el DNI, permite únicamente dígitos y teclas de control. */
        private void dni_KeyPress(object origen, KeyPressEventArgs e)
        {
            AyudaFormularioVisual.ValidarEntradaDni(e);
        }

        private void nombreUsuario_KeyPress(object origen, KeyPressEventArgs e)
        {
            AyudaFormularioVisual.ValidarEntradaNombreUsuario(e);
        }

        private void campoNombre_Validating(object origen, CancelEventArgs e)
        {
            AyudaFormularioVisual.ValidarNombre(indicadorErrores, (TextBox)origen, origen == nombre ? "nombre" : "apellido");
        }

        private void dni_Validating(object origen, CancelEventArgs e)
        {
            AyudaFormularioVisual.ValidarDni(indicadorErrores, dni);
        }

        private void nombreUsuario_Validating(object origen, CancelEventArgs e)
        {
            AyudaFormularioVisual.ValidarNombreUsuario(indicadorErrores, nombreUsuario);
        }

        private void fechaNacimiento_Validating(object origen, CancelEventArgs e)
        {
            AyudaFormularioVisual.ValidarConError(indicadorErrores, fechaNacimiento,
                () => ValidacionesGimnasio.ValidarEdadMinima(fechaNacimiento.Value.Date, 18, "El usuario debe tener al menos 18 años."));
        }

        private void clave_Validating(object origen, CancelEventArgs e)
        {
            if (idSeleccionado == 0)
                AyudaFormularioVisual.ValidarRequerido(indicadorErrores, clave, "La contraseña es obligatoria.");
            else
                indicadorErrores.SetError(clave, string.Empty);
        }

        private void salario_Validating(object origen, CancelEventArgs e)
        {
            AyudaFormularioVisual.ValidarDecimal(indicadorErrores, salario, "salario", true, false);
        }

        private void rol_Validating(object origen, CancelEventArgs e)
        {
            AyudaFormularioVisual.ValidarCombo(indicadorErrores, rol, "Seleccioná un rol.");
        }

        /* Al corregir un campo editable, retira su aviso anterior hasta la próxima validación. */
        private void campoValidado_Cambiado(object origen, EventArgs e)
        {
            var control = origen as Control;
            if (control != null)
                indicadorErrores.SetError(control, string.Empty);
        }

        private bool ValidarFormulario(bool alta)
        {
            indicadorErrores.Clear();
            var valido = AyudaFormularioVisual.ValidarNombre(indicadorErrores, nombre, "nombre");
            valido = AyudaFormularioVisual.ValidarNombre(indicadorErrores, apellido, "apellido") & valido;
            valido = AyudaFormularioVisual.ValidarDni(indicadorErrores, dni) & valido;
            valido = AyudaFormularioVisual.ValidarConError(indicadorErrores, fechaNacimiento,
                () => ValidacionesGimnasio.ValidarEdadMinima(fechaNacimiento.Value.Date, 18, "El usuario debe tener al menos 18 años.")) & valido;
            valido = AyudaFormularioVisual.ValidarNombreUsuario(indicadorErrores, nombreUsuario) & valido;
            if (alta)
                valido = AyudaFormularioVisual.ValidarRequerido(indicadorErrores, clave, "La contraseña es obligatoria.") & valido;
            valido = AyudaFormularioVisual.ValidarDecimal(indicadorErrores, salario, "salario", true, false) & valido;
            valido = AyudaFormularioVisual.ValidarCombo(indicadorErrores, rol, "Seleccioná un rol.") & valido;
            if (!valido)
                AyudaFormularioVisual.EnfocarPrimerError(indicadorErrores, nombre, apellido, dni, fechaNacimiento, nombreUsuario, clave, salario, rol);
            return valido;
        }

        /* Devuelve el código del sexo seleccionado sin inventar un valor para registros sin selección. */
        private string SexoSeleccionado()
        {
            return sexo.SelectedIndex == 0 ? "M" : sexo.SelectedIndex == 1 ? "F" : null;
        }

        /* Al elegir una foto, valida el archivo y actualiza la vista antes de aceptar sus bytes. */
        private void btnSeleccionarFoto_Click(object origen, EventArgs e)
        {
            try
            {
                var seleccion = AyudaFormularioVisual.SeleccionarImagen(this, SexoSeleccionado());
                if (seleccion == null)
                    return;
                AyudaFormularioVisual.MostrarFoto(fotoUsuario, seleccion.VistaPrevia, SexoSeleccionado());
                fotoSeleccionada = seleccion;
            }
            catch (Exception ex)
            {
                AyudaFormularioVisual.MostrarError(lblEstado, ex);
            }
        }

        /* Al quitar la foto, conserva el sexo y vuelve al avatar disponible para ese valor. */
        private void btnQuitarFoto_Click(object origen, EventArgs e)
        {
            if (fotoSeleccionada == null && string.IsNullOrWhiteSpace(fotoRutaSeleccionada))
            {
                MessageBox.Show(this, "No existe imagen para quitar", "Quitar foto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            fotoSeleccionada = null;
            fotoRutaSeleccionada = null;
            sexo_SelectedIndexChanged(origen, e);
        }

        /* Al cambiar el sexo sin foto propia, actualiza el avatar de la vista previa. */
        private void sexo_SelectedIndexChanged(object origen, EventArgs e)
        {
            if (fotoSeleccionada != null)
                return;
            try
            {
                AyudaFormularioVisual.MostrarFotoRuta(fotoUsuario, fotoRutaSeleccionada, SexoSeleccionado());
            }
            catch (Exception ex)
            {
                AyudaFormularioVisual.MostrarError(lblEstado, ex);
            }
        }

        /* Libera la copia de la imagen cuando se destruye el control de vista previa. */
        private void fotoUsuario_Disposed(object origen, EventArgs e)
        {
            if (fotoUsuario.Image != null)
            {
                fotoUsuario.Image.Dispose();
                fotoUsuario.Image = null;
            }
        }
    }

    /* Identifica al usuario que se acaba de modificar. */
    public sealed class UsuarioActualizadoEventArgs : EventArgs
    {
        public UsuarioActualizadoEventArgs(int idUsuarioSistema)
        {
            IdUsuarioSistema = idUsuarioSistema;
        }

        public int IdUsuarioSistema { get; private set; }
    }
}
