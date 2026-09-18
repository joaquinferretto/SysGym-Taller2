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
    /* Presenta socios y atiende sus acciones mediante eventos de Windows Forms. */
    [DesignerCategory("Form")]
    public partial class GestionSociosFormulario : Form
    {
        private readonly SocioLogica logica = new SocioLogica();
        private bool permitirEdicion;
        private List<Socio> sociosCargados = new List<Socio>();
        private int idSeleccionado;
        private bool cargandoTabla;
        private bool estadoSeleccionado = true;
        private readonly Color colorPrimario;
        private byte[] fotoSeleccionada;
        private string extensionFotoSeleccionada;
        private string fotoRutaSeleccionada;
        private readonly int idSocioInicial;
        /* Inicializa los componentes existentes y las dependencias de la pantalla sin consultar la base de datos. */
        public GestionSociosFormulario() : this(Color.FromArgb(79, 70, 229))
        {
        }

        /* Inicializa los componentes existentes y las dependencias de la pantalla sin consultar la base de datos. */
        public GestionSociosFormulario(Color colorPrimario, bool permitirEdicion = true)
        {
            this.permitirEdicion = permitirEdicion;
            this.colorPrimario = colorPrimario;
            InitializeComponent();
            fechaNacimiento.MaxDate = DateTime.Today.AddYears(-13);
            fechaNacimiento.Value = fechaNacimiento.MaxDate;
            if (!permitirEdicion)
            {
                nuevo.Visible = false;
                guardar.Visible = false;
                actualizar.Visible = false;
                nombre.ReadOnly = true;
                apellido.ReadOnly = true;
                dni.ReadOnly = true;
                peso.ReadOnly = true;
                altura.ReadOnly = true;
                fechaNacimiento.Enabled = false;
                sexo.Enabled = false;
                btnSeleccionarFoto.Enabled = false;
                btnQuitarFoto.Enabled = false;
            }
        }

        /* Inicializa la gestión de socios enfocada en un registro proveniente de otra pantalla. */
        public GestionSociosFormulario(Color colorPrimario, int idSocioInicial, bool permitirEdicion = true)
            : this(colorPrimario, permitirEdicion)
        {
            if (idSocioInicial <= 0)
                throw new ArgumentException("El socio seleccionado no es válido.", "idSocioInicial");
            this.idSocioInicial = idSocioInicial;
        }

        /* Consulta los registros del módulo y actualiza la grilla, informando los errores de carga. */
        private void Cargar()
        {
            try
            {
                sociosCargados = logica.ListarParaGestion();
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
            var filtrados = sociosCargados.AsEnumerable();
            if (estadoElegido == "Activos")
                filtrados = filtrados.Where(s => s.Estado);
            else if (estadoElegido == "Inactivos")
                filtrados = filtrados.Where(s => !s.Estado);
            if (!string.IsNullOrWhiteSpace(criterio))
            {
                filtrados = filtrados.Where(s => Contiene(s.Nombre + " " + s.Apellido, criterio) || Contiene(s.DNI, criterio));
            }

            cargandoTabla = true;
            tabla.Rows.Clear();
            foreach (var socio in filtrados)
            {
                tabla.Rows.Add(socio.IdSocio, socio.Apellido + ", " + socio.Nombre, socio.DNI, socio.FechaNacimiento.HasValue ? socio.FechaNacimiento.Value.ToString("dd/MM/yyyy") : "-", socio.Estado ? "Activo" : "Inactivo");
            }

            tabla.ClearSelection();
            cargandoTabla = false;
            lblEstado.Text = tabla.Rows.Count + " socio(s) encontrado(s)";
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
                var socio = logica.ObtenerPorId(idSeleccionado);
                if (socio == null)
                    return;
                estadoSeleccionado = socio.Estado;
                fotoSeleccionada = null;
                extensionFotoSeleccionada = null;
                fotoRutaSeleccionada = socio.FotoRuta;
                sexo.SelectedIndex = socio.Sexo == "M" ? 0 : socio.Sexo == "F" ? 1 : -1;
                AyudaFormularioVisual.MostrarFotoRuta(fotoSocio, fotoRutaSeleccionada, socio.Sexo);
                nombre.Text = socio.Nombre;
                apellido.Text = socio.Apellido;
                dni.Text = socio.DNI;
                fechaNacimiento.Checked = socio.FechaNacimiento.HasValue;
                if (socio.FechaNacimiento.HasValue)
                    fechaNacimiento.Value = socio.FechaNacimiento.Value;
                peso.Text = socio.Peso.HasValue ? socio.Peso.Value.ToString("0.##") : string.Empty;
                altura.Text = socio.Altura.HasValue ? socio.Altura.Value.ToString("0.00") : string.Empty;
                EstablecerModo(false, socio.Estado);
                lblFormulario.Text = "Editar socio - Estado: " + (socio.Estado ? "Activo" : "Inactivo");
            }
            catch (Exception ex)
            {
                AyudaFormularioVisual.MostrarError(lblEstado, ex);
            }
        }

        /* Al hacer clic en nuevo, limpia la selección y prepara el registro de nuevos datos. */
        private void nuevo_Click(object origen, EventArgs e)
        {
            idSeleccionado = 0;
            estadoSeleccionado = true;
            fotoSeleccionada = null;
            extensionFotoSeleccionada = null;
            fotoRutaSeleccionada = null;
            sexo.SelectedIndex = -1;
            AyudaFormularioVisual.MostrarFotoRuta(fotoSocio, null, null);
            nombre.Clear();
            apellido.Clear();
            dni.Clear();
            peso.Clear();
            altura.Clear();
            fechaNacimiento.Value = fechaNacimiento.MaxDate;
            fechaNacimiento.Checked = true;
            tabla.ClearSelection();
            EstablecerModo(true, true);
            if (permitirEdicion)
                nombre.Focus();
        }

        /* Habilita las acciones disponibles según la selección y el estado del registro. */
        private void EstablecerModo(bool nuevoRegistro, bool activo)
        {
            lblFormulario.Text = nuevoRegistro ? "Nuevo socio - Estado inicial: Activo" : "Editar socio";
            guardar.Enabled = permitirEdicion && nuevoRegistro;
            actualizar.Enabled = permitirEdicion && !nuevoRegistro;
        }

        /* Selecciona en la grilla el socio solicitado por una pantalla de origen. */
        private void SeleccionarSocioInicial()
        {
            for (var indice = 0; indice < tabla.Rows.Count; indice++)
            {
                if (Convert.ToInt32(tabla.Rows[indice].Cells[0].Value) != idSocioInicial)
                    continue;
                tabla.CurrentCell = tabla.Rows[indice].Cells[1];
                tabla.Rows[indice].Selected = true;
                return;
            }

            lblEstado.Text = "El socio seleccionado no está disponible.";
        }

        /* Recoge los datos personales y físicos ingresados, validando su formato. */
        private Socio LeerSocio()
        {
            return new Socio
            {
                IdSocio = idSeleccionado,
                Nombre = nombre.Text.Trim(),
                Apellido = apellido.Text.Trim(),
                DNI = dni.Text.Trim(),
                FechaNacimiento = fechaNacimiento.Checked ? (DateTime? )fechaNacimiento.Value.Date : null,
                Peso = string.IsNullOrWhiteSpace(peso.Text) ? (decimal? )null : AyudaFormularioVisual.DecimalPositivo(peso, "peso"),
                Altura = string.IsNullOrWhiteSpace(altura.Text) ? (decimal? )null : AyudaFormularioVisual.DecimalPositivo(altura, "altura"),
                FotoRuta = fotoRutaSeleccionada,
                Sexo = SexoSeleccionado()
            };
        }

        /* Al hacer clic en guardar, valida los campos y envía el registro a la capa lógica. */
        private void guardar_Click(object origen, EventArgs e)
        {
            try
            {
                if (!permitirEdicion || idSeleccionado != 0)
                    return;
                logica.Crear(LeerSocio(), fotoSeleccionada, extensionFotoSeleccionada);
                Cargar();
                nuevo_Click(null, EventArgs.Empty);
                AyudaFormularioVisual.MostrarExito(lblEstado, "Socio creado correctamente.", true);
            }
            catch (Exception ex)
            {
                AyudaFormularioVisual.MostrarError(lblEstado, ex, true);
            }
        }

        /* Al hacer clic en actualizar, valida los campos y guarda las modificaciones mediante la capa lógica. */
        private void actualizar_Click(object origen, EventArgs e)
        {
            try
            {
                if (!permitirEdicion || idSeleccionado == 0)
                    throw new InvalidOperationException("Selecciona un socio.");
                logica.Modificar(LeerSocio(), fotoSeleccionada, extensionFotoSeleccionada);
                Cargar();
                nuevo_Click(null, EventArgs.Empty);
                AyudaFormularioVisual.MostrarExito(lblEstado, "Socio actualizado correctamente.");
            }
            catch (Exception ex)
            {
                AyudaFormularioVisual.MostrarError(lblEstado, ex);
            }
        }

        /* Al hacer clic en calcularImc, solicita el cálculo del IMC del socio y muestra el resultado. */
        private void calcularImc_Click(object origen, EventArgs e)
        {
            try
            {
                var socio = new Socio
                {
                    Peso = AyudaFormularioVisual.DecimalPositivo(peso, "peso"),
                    Altura = AyudaFormularioVisual.DecimalPositivo(altura, "altura")
                };
                MessageBox.Show("IMC: " + logica.CalcularIMC(socio).ToString("0.00"), "Indice de masa corporal", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                AyudaFormularioVisual.MostrarError(lblEstado, ex);
            }
        }

        /* Al hacer clic en verRutina, abre la rutina semanal del socio seleccionado en la grilla. */
        private void verRutina_Click(object origen, EventArgs e)
        {
            try
            {
                if (idSeleccionado == 0)
                    throw new InvalidOperationException("Selecciona un socio.");
                using (var semana = new RutinaSemanalFormulario(idSeleccionado, apellido.Text.Trim() + ", " + nombre.Text.Trim(), colorPrimario))
                    semana.ShowDialog(this);
            }
            catch (Exception ex)
            {
                AyudaFormularioVisual.MostrarError(lblEstado, ex);
            }
        }

        /* Al cargar la pantalla en ejecución, prepara sus datos iniciales sin realizar consultas desde el diseñador. */
        private void GestionSociosFormulario_Load(object origen, EventArgs e)
        {
            if (AyudaFormularioVisual.EnModoDisenio(this))
                return;
            try
            {
                Cargar();
                if (idSocioInicial > 0)
                    SeleccionarSocioInicial();
                else
                    nuevo_Click(null, EventArgs.Empty);
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
        private void peso_KeyPress(object origen, KeyPressEventArgs e)
        {
            AyudaFormularioVisual.ValidarEntradaDecimal(peso, e);
        }

        /* Al escribir en el campo, permite números y un único separador decimal mediante la validación visual compartida. */
        private void altura_KeyPress(object origen, KeyPressEventArgs e)
        {
            AyudaFormularioVisual.ValidarEntradaDecimal(altura, e);
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

        /* Devuelve el código del sexo seleccionado sin inventar un valor para registros sin selección. */
        private string SexoSeleccionado()
        {
            return sexo.SelectedIndex == 0 ? "M" : sexo.SelectedIndex == 1 ? "F" : null;
        }

        /* Al elegir una foto, conserva los bytes solo hasta guardar y muestra una vista independiente. */
        private void btnSeleccionarFoto_Click(object origen, EventArgs e)
        {
            try
            {
                var seleccion = AyudaFormularioVisual.SeleccionarImagen(this, SexoSeleccionado());
                if (seleccion == null)
                    return;
                AyudaFormularioVisual.MostrarFoto(fotoSocio, seleccion.Contenido, SexoSeleccionado());
                fotoSeleccionada = seleccion.Contenido;
                extensionFotoSeleccionada = seleccion.Extension;
                fotoRutaSeleccionada = null;
            }
            catch (Exception ex)
            {
                AyudaFormularioVisual.MostrarError(lblEstado, ex);
            }
        }

        /* Al quitar la foto, conserva el sexo y vuelve al avatar disponible para ese valor. */
        private void btnQuitarFoto_Click(object origen, EventArgs e)
        {
            fotoSeleccionada = null;
            extensionFotoSeleccionada = null;
            fotoRutaSeleccionada = null;
            sexo_SelectedIndexChanged(origen, e);
        }

        /* Al cambiar el sexo sin foto propia, actualiza el avatar de la vista previa. */
        private void sexo_SelectedIndexChanged(object origen, EventArgs e)
        {
            if (fotoSeleccionada != null || !string.IsNullOrWhiteSpace(fotoRutaSeleccionada))
                return;
            try
            {
                AyudaFormularioVisual.MostrarFotoRuta(fotoSocio, null, SexoSeleccionado());
            }
            catch (Exception ex)
            {
                AyudaFormularioVisual.MostrarError(lblEstado, ex);
            }
        }

        /* Libera la copia de la imagen cuando se destruye el control de vista previa. */
        private void fotoSocio_Disposed(object origen, EventArgs e)
        {
            if (fotoSocio.Image != null)
            {
                fotoSocio.Image.Dispose();
                fotoSocio.Image = null;
            }
        }

    }
}
