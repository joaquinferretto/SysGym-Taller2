using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaLogica;
using exxen2._0.capaVisual.Compartido;

namespace exxen2._0.capaVisual.Administrador
{
    [System.ComponentModel.DesignerCategory("Code")]
    [System.ComponentModel.DesignTimeVisible(false)]
    public abstract class ConsultaRutinasAdministradorFormBase : FormularioModuloBase
    {
        private readonly RutinaLogica logica = new RutinaLogica();

        public ConsultaRutinasAdministradorFormBase()
            : base("Catálogo de rutinas", "Plantillas generales reutilizables y cantidad de socios asignados", Color.FromArgb(79, 70, 229))
        {
            AgregarBoton("Actualizar", delegate { Cargar(); }, true);
            Tabla.Columns.Add("Id", "Id"); Tabla.Columns[0].Visible = false;
            Tabla.Columns.Add("Rutina", "Rutina"); Tabla.Columns.Add("Descripción", "Descripción");
            Tabla.Columns.Add("Entrenador", "Creada por"); Tabla.Columns.Add("Asignados", "Socios asignados");
            AlCargarEnEjecucion(delegate { Cargar(); });
        }

        private void Cargar()
        {
            try
            {
                Tabla.Rows.Clear();
                foreach (var r in logica.ListarGenerales())
                    Tabla.Rows.Add(r.IdRutina, r.Nombre,
                        r.Descripcion ?? "-",
                        r.Entrenador == null ? "-" : r.Entrenador.Nombre + " " + r.Entrenador.Apellido,
                        r.Asignaciones == null ? 0 : r.Asignaciones.Count(a => a.Estado));
                Estado.Text = Tabla.Rows.Count + " plantilla(s) activa(s)";
            }
            catch (Exception ex) { MostrarError(ex); }
        }
    }

    [System.ComponentModel.DesignerCategory("Code")]
    [System.ComponentModel.DesignTimeVisible(false)]
    public abstract class GestionUsuariosFormBase : FormularioModuloBase
    {
        private readonly UsuarioSistemaLogica logica = new UsuarioSistemaLogica();
        private readonly RolLogica roles = new RolLogica();
        private readonly VistaListadoDetalle vista;
        private readonly TextBox nombre;
        private readonly TextBox apellido;
        private readonly TextBox dni;
        private readonly TextBox username;
        private readonly TextBox password;
        private readonly TextBox salario;
        private readonly ComboBox rol;
        private readonly Button guardar;
        private readonly Button actualizar;
        private readonly Button darDeBaja;
        private readonly Button reactivar;
        private readonly ComboBox filtroEstado;
        private List<UsuarioSistema> usuariosCargados = new List<UsuarioSistema>();
        private int idSeleccionado;
        private bool cargandoTabla;
        private bool estadoSeleccionado = true;

        public GestionUsuariosFormBase()
            : base("Usuarios y roles", "Administración del personal y sus permisos", Color.FromArgb(79, 70, 229))
        {
            vista = CrearVistaListadoDetalle("Usuarios", "Buscar por nombre, DNI o usuario",
                "+ Nuevo usuario", NuevoUsuario);
            nombre = vista.AgregarCampo("Nombre", CrearTexto());
            apellido = vista.AgregarCampo("Apellido", CrearTexto());
            dni = vista.AgregarCampo("DNI", CrearTexto());
            username = vista.AgregarCampo("Usuario", CrearTexto());
            password = vista.AgregarCampo("Contraseña (obligatoria al crear)", CrearTexto());
            password.UseSystemPasswordChar = true;
            salario = vista.AgregarCampo("Salario mensual", CrearTexto());
            ConfigurarEntradaDecimal(salario);
            rol = vista.AgregarCampo("Rol", new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList });

            guardar = vista.AgregarAccion("Guardar", GuardarNuevo, true);
            actualizar = vista.AgregarAccion("Actualizar", Actualizar);
            darDeBaja = vista.AgregarAccion("Dar de baja", DarDeBaja, false, true);
            reactivar = vista.AgregarAccion("Reactivar", Reactivar);

            filtroEstado = vista.AgregarFiltroListado("Estado", new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList
            });
            filtroEstado.Items.AddRange(new object[] { "Todos", "Activos", "Inactivos" });
            filtroEstado.SelectedIndex = 0;

            Tabla.Columns.Add("Id", "Id"); Tabla.Columns[0].Visible = false;
            Tabla.Columns.Add("Nombre", "Nombre");
            Tabla.Columns.Add("DNI", "DNI");
            Tabla.Columns.Add("Usuario", "Usuario");
            Tabla.Columns.Add("Rol", "Rol");
            Tabla.Columns.Add("Salario", "Salario mensual");
            Tabla.Columns.Add("Estado", "Estado");
            Tabla.Columns[1].FillWeight = 130;
            Tabla.Columns[2].FillWeight = 85;
            Tabla.Columns[3].FillWeight = 95;
            Tabla.Columns[4].FillWeight = 90;
            Tabla.Columns[5].FillWeight = 90;
            Tabla.Columns[6].FillWeight = 65;
            Tabla.SelectionChanged += Seleccionar;
            vista.Buscador.TextChanged += delegate { AplicarFiltro(); };
            filtroEstado.SelectedIndexChanged += delegate { AplicarFiltro(); };
            AlCargarEnEjecucion(delegate { CargarRoles(); Cargar(); NuevoUsuario(null, EventArgs.Empty); });
        }

        private static TextBox CrearTexto()
        {
            return new TextBox { BorderStyle = BorderStyle.FixedSingle };
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
            catch (Exception ex) { MostrarError(ex); }
        }

        private void AplicarFiltro()
        {
            var criterio = vista.Buscador.Text.Trim();
            var estadoElegido = Convert.ToString(filtroEstado.SelectedItem);
            var filtrados = usuariosCargados.AsEnumerable();
            if (estadoElegido == "Activos") filtrados = filtrados.Where(u => u.Estado);
            else if (estadoElegido == "Inactivos") filtrados = filtrados.Where(u => !u.Estado);
            if (!string.IsNullOrWhiteSpace(criterio))
            {
                filtrados = filtrados.Where(u =>
                    Contiene(u.Nombre + " " + u.Apellido, criterio)
                    || Contiene(u.DNI, criterio)
                    || Contiene(u.Username, criterio));
            }

            cargandoTabla = true;
            Tabla.Rows.Clear();
            foreach (var u in filtrados)
                Tabla.Rows.Add(u.IdUsuarioSistema, u.Nombre + " " + u.Apellido, u.DNI,
                    u.Username, u.Rol == null ? "-" : u.Rol.Descripcion,
                    u.Salario.ToString("C", CultureInfo.CurrentCulture), u.Estado ? "Activo" : "Inactivo");
            Tabla.ClearSelection();
            cargandoTabla = false;
            Estado.Text = Tabla.Rows.Count + " usuario(s) encontrado(s)";
        }

        private static bool Contiene(string valor, string criterio)
        {
            return !string.IsNullOrEmpty(valor)
                && valor.IndexOf(criterio, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private void Seleccionar(object sender, EventArgs e)
        {
            if (cargandoTabla || Tabla.CurrentRow == null || !Tabla.CurrentRow.Selected) return;
            try
            {
                idSeleccionado = Convert.ToInt32(Tabla.CurrentRow.Cells[0].Value);
                var usuario = logica.ObtenerPorId(idSeleccionado);
                if (usuario == null) return;
                estadoSeleccionado = usuario.Estado;
                nombre.Text = usuario.Nombre;
                apellido.Text = usuario.Apellido;
                dni.Text = usuario.DNI;
                username.Text = usuario.Username;
                password.Clear();
                salario.Text = usuario.Salario.ToString("0.00", CultureInfo.CurrentCulture);
                if (usuario.Rol != null) rol.SelectedValue = usuario.IdRol;
                EstablecerModo(false, usuario.Estado);
            }
            catch (Exception ex) { MostrarError(ex); }
        }

        private void NuevoUsuario(object sender, EventArgs e)
        {
            idSeleccionado = 0;
            estadoSeleccionado = true;
            nombre.Clear(); apellido.Clear(); dni.Clear(); username.Clear(); password.Clear(); salario.Clear();
            if (rol.Items.Count > 0) rol.SelectedIndex = 0;
            Tabla.ClearSelection();
            EstablecerModo(true, true);
            nombre.Focus();
        }

        private void EstablecerModo(bool nuevo, bool activo)
        {
            vista.TituloDetalle.Text = nuevo ? "Nuevo usuario" : "Editar usuario";
            guardar.Enabled = nuevo;
            actualizar.Enabled = !nuevo;
            darDeBaja.Enabled = !nuevo && activo;
            reactivar.Enabled = !nuevo && !activo;
        }

        private UsuarioSistema LeerUsuario()
        {
            if (rol.SelectedValue == null) throw new InvalidOperationException("Selecciona un rol.");
            return new UsuarioSistema
            {
                IdUsuarioSistema = idSeleccionado,
                Nombre = nombre.Text.Trim(),
                Apellido = apellido.Text.Trim(),
                DNI = dni.Text.Trim(),
                Username = username.Text.Trim(),
                Salario = DecimalPositivo(salario, "salario"),
                IdRol = Convert.ToInt32(rol.SelectedValue),
                Estado = estadoSeleccionado
            };
        }

        private void GuardarNuevo(object sender, EventArgs e)
        {
            try
            {
                if (idSeleccionado != 0) return;
                logica.Crear(LeerUsuario(), password.Text);
                Cargar(); NuevoUsuario(null, EventArgs.Empty);
                MostrarExito("Usuario creado correctamente.");
            }
            catch (Exception ex) { MostrarError(ex); }
        }

        private void Actualizar(object sender, EventArgs e)
        {
            try
            {
                if (idSeleccionado == 0) throw new InvalidOperationException("Selecciona un usuario.");
                logica.Modificar(LeerUsuario(), password.Text);
                Cargar(); NuevoUsuario(null, EventArgs.Empty);
                MostrarExito("Usuario actualizado correctamente.");
            }
            catch (Exception ex) { MostrarError(ex); }
        }

        private void DarDeBaja(object sender, EventArgs e)
        {
            try
            {
                if (idSeleccionado == 0) throw new InvalidOperationException("Selecciona un usuario.");
                if (MessageBox.Show("¿Dar de baja al usuario seleccionado?", "Confirmar baja",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
                logica.DarDeBaja(idSeleccionado);
                Cargar(); NuevoUsuario(null, EventArgs.Empty);
                MostrarExito("Usuario dado de baja.");
            }
            catch (Exception ex) { MostrarError(ex); }
        }

        private void Reactivar(object sender, EventArgs e)
        {
            try
            {
                if (idSeleccionado == 0) throw new InvalidOperationException("Selecciona un usuario.");
                logica.Reactivar(idSeleccionado);
                Cargar(); NuevoUsuario(null, EventArgs.Empty);
                MostrarExito("Usuario reactivado correctamente.");
            }
            catch (Exception ex) { MostrarError(ex); }
        }
    }

    [System.ComponentModel.DesignerCategory("Code")]
    [System.ComponentModel.DesignTimeVisible(false)]
    public abstract class GestionPlanesFormBase : FormularioModuloBase
    {
        private readonly PlanLogica logica = new PlanLogica();
        private readonly RutinaLogica rutinas = new RutinaLogica();
        private readonly VistaListadoDetalle vista;
        private readonly TextBox nombre;
        private readonly TextBox descripcion;
        private readonly TextBox precio;
        private readonly ComboBox rutina;
        private readonly CheckBox incluyeEntrenador;
        private readonly CheckBox incluyeRutina;
        private readonly Button guardar;
        private readonly Button actualizar;
        private readonly Button darDeBaja;
        private readonly Button reactivar;
        private readonly ComboBox filtroEstado;
        private List<Plan> planesCargados = new List<Plan>();
        private int idSeleccionado;
        private bool cargandoTabla;
        private bool estadoSeleccionado = true;

        public GestionPlanesFormBase()
            : base("Planes", "Configuración de los planes Básico y Premium", Color.FromArgb(79, 70, 229))
        {
            vista = CrearVistaListadoDetalle("Planes", "Buscar por nombre o descripción",
                "+ Nuevo plan", NuevoPlan);
            nombre = vista.AgregarCampo("Nombre", CrearTexto());
            descripcion = vista.AgregarCampo("Descripción", CrearTexto());
            precio = vista.AgregarCampo("Precio mensual", CrearTexto());
            ConfigurarEntradaDecimal(precio);
            rutina = vista.AgregarCampo("Rutina base", new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList
            });

            incluyeEntrenador = new CheckBox
            {
                AutoSize = true,
                Margin = new Padding(0, 3, 20, 0),
                Text = "Incluye entrenador"
            };
            incluyeRutina = new CheckBox
            {
                AutoSize = true,
                Margin = new Padding(0, 3, 0, 0),
                Text = "Incluye rutina personalizada"
            };
            var beneficios = new FlowLayoutPanel
            {
                BackColor = Color.White,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false
            };
            beneficios.Controls.Add(incluyeEntrenador);
            beneficios.Controls.Add(incluyeRutina);
            vista.AgregarCampo("Beneficios del plan", beneficios);

            guardar = vista.AgregarAccion("Guardar", GuardarNuevo, true);
            actualizar = vista.AgregarAccion("Actualizar", Actualizar);
            darDeBaja = vista.AgregarAccion("Dar de baja", DarDeBaja, false, true);
            reactivar = vista.AgregarAccion("Reactivar", Reactivar);

            filtroEstado = vista.AgregarFiltroListado("Estado", new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList
            });
            filtroEstado.Items.AddRange(new object[] { "Todos", "Activos", "Inactivos" });
            filtroEstado.SelectedIndex = 0;

            Tabla.Columns.Add("Id", "Id"); Tabla.Columns[0].Visible = false;
            Tabla.Columns.Add("Nombre", "Nombre");
            Tabla.Columns.Add("Precio", "Precio mensual");
            Tabla.Columns.Add("Rutina", "Rutina base");
            Tabla.Columns.Add("Beneficios", "Beneficios");
            Tabla.Columns.Add("Estado", "Estado");
            Tabla.Columns[1].FillWeight = 115;
            Tabla.Columns[2].FillWeight = 80;
            Tabla.Columns[3].FillWeight = 115;
            Tabla.Columns[4].FillWeight = 140;
            Tabla.SelectionChanged += Seleccionar;
            vista.Buscador.TextChanged += delegate { AplicarFiltro(); };
            filtroEstado.SelectedIndexChanged += delegate { AplicarFiltro(); };
            AlCargarEnEjecucion(delegate { Inicializar(); });
        }

        private static TextBox CrearTexto()
        {
            return new TextBox { BorderStyle = BorderStyle.FixedSingle };
        }

        private void Inicializar()
        {
            try
            {
                CargarRutinas();
                Cargar();
                NuevoPlan(null, EventArgs.Empty);
            }
            catch (Exception ex) { MostrarError(ex); }
        }

        private void CargarRutinas()
        {
            rutina.DataSource = rutinas.ListarActivas();
            rutina.DisplayMember = "Nombre";
            rutina.ValueMember = "IdRutina";
        }

        private void Cargar()
        {
            try
            {
                planesCargados = logica.ListarParaGestion();
                AplicarFiltro();
            }
            catch (Exception ex) { MostrarError(ex); }
        }

        private void AplicarFiltro()
        {
            var criterio = vista.Buscador.Text.Trim();
            var estadoElegido = Convert.ToString(filtroEstado.SelectedItem);
            var filtrados = planesCargados.AsEnumerable();
            if (estadoElegido == "Activos") filtrados = filtrados.Where(p => p.Estado);
            else if (estadoElegido == "Inactivos") filtrados = filtrados.Where(p => !p.Estado);
            if (!string.IsNullOrWhiteSpace(criterio))
            {
                filtrados = filtrados.Where(p => Contiene(p.Nombre, criterio)
                    || Contiene(p.Descripcion, criterio));
            }

            cargandoTabla = true;
            Tabla.Rows.Clear();
            foreach (var plan in filtrados)
            {
                Tabla.Rows.Add(plan.IdPlan, plan.Nombre, plan.Precio.ToString("C"),
                    plan.Rutina == null ? "Sin rutina" : plan.Rutina.Nombre,
                    DescribirBeneficios(plan), plan.Estado ? "Activo" : "Inactivo");
            }
            Tabla.ClearSelection();
            cargandoTabla = false;
            Estado.Text = Tabla.Rows.Count + " plan(es) encontrado(s)";
        }

        private static string DescribirBeneficios(Plan plan)
        {
            if (plan.IncluyeEntrenador && plan.IncluyeRutinaPersonal)
                return "Entrenador y rutina personalizada";
            if (plan.IncluyeEntrenador) return "Entrenador";
            if (plan.IncluyeRutinaPersonal) return "Rutina personalizada";
            return "Plan básico";
        }

        private static bool Contiene(string valor, string criterio)
        {
            return !string.IsNullOrEmpty(valor)
                && valor.IndexOf(criterio, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private void Seleccionar(object sender, EventArgs e)
        {
            if (cargandoTabla || Tabla.CurrentRow == null || !Tabla.CurrentRow.Selected) return;
            try
            {
                idSeleccionado = Convert.ToInt32(Tabla.CurrentRow.Cells[0].Value);
                var plan = logica.ObtenerPorId(idSeleccionado);
                if (plan == null) return;
                estadoSeleccionado = plan.Estado;
                nombre.Text = plan.Nombre;
                descripcion.Text = plan.Descripcion;
                precio.Text = plan.Precio.ToString("0.00");
                rutina.SelectedValue = plan.IdRutina;
                incluyeEntrenador.Checked = plan.IncluyeEntrenador;
                incluyeRutina.Checked = plan.IncluyeRutinaPersonal;
                EstablecerModo(false, plan.Estado);
                vista.TituloDetalle.Text = "Editar plan · Estado: Activo";
            }
            catch (Exception ex) { MostrarError(ex); }
        }

        private void NuevoPlan(object sender, EventArgs e)
        {
            idSeleccionado = 0;
            estadoSeleccionado = true;
            nombre.Clear(); descripcion.Clear(); precio.Clear();
            incluyeEntrenador.Checked = false;
            incluyeRutina.Checked = false;
            if (rutina.Items.Count > 0) rutina.SelectedIndex = 0;
            Tabla.ClearSelection();
            EstablecerModo(true, true);
            nombre.Focus();
        }

        private void EstablecerModo(bool nuevo, bool activo)
        {
            vista.TituloDetalle.Text = nuevo ? "Nuevo plan · Estado inicial: Activo" : "Editar plan";
            guardar.Enabled = nuevo;
            actualizar.Enabled = !nuevo;
            darDeBaja.Enabled = !nuevo && activo;
            reactivar.Enabled = !nuevo && !activo;
        }

        private Plan LeerPlan()
        {
            if (rutina.SelectedValue == null)
                throw new InvalidOperationException("Selecciona una rutina base.");

            return new Plan
            {
                IdPlan = idSeleccionado,
                Nombre = nombre.Text.Trim(),
                Descripcion = descripcion.Text.Trim(),
                Precio = DecimalPositivo(precio, "precio"),
                IdRutina = Convert.ToInt32(rutina.SelectedValue),
                IncluyeEntrenador = incluyeEntrenador.Checked,
                IncluyeRutinaPersonal = incluyeRutina.Checked,
                Estado = estadoSeleccionado
            };
        }

        private void GuardarNuevo(object sender, EventArgs e)
        {
            try
            {
                if (idSeleccionado != 0) return;
                logica.Crear(LeerPlan());
                Cargar(); NuevoPlan(null, EventArgs.Empty);
                MostrarExito("Plan creado correctamente.");
            }
            catch (Exception ex) { MostrarError(ex); }
        }

        private void Actualizar(object sender, EventArgs e)
        {
            try
            {
                if (idSeleccionado == 0) throw new InvalidOperationException("Selecciona un plan.");
                logica.Modificar(LeerPlan());
                Cargar(); NuevoPlan(null, EventArgs.Empty);
                MostrarExito("Plan actualizado correctamente.");
            }
            catch (Exception ex) { MostrarError(ex); }
        }

        private void DarDeBaja(object sender, EventArgs e)
        {
            try
            {
                if (idSeleccionado == 0) throw new InvalidOperationException("Selecciona un plan.");
                if (MessageBox.Show("¿Dar de baja al plan seleccionado?", "Confirmar baja",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
                logica.DarDeBaja(idSeleccionado);
                Cargar(); NuevoPlan(null, EventArgs.Empty);
                MostrarExito("Plan dado de baja.");
            }
            catch (Exception ex) { MostrarError(ex); }
        }

        private void Reactivar(object sender, EventArgs e)
        {
            try
            {
                if (idSeleccionado == 0) throw new InvalidOperationException("Selecciona un plan.");
                logica.Reactivar(idSeleccionado);
                Cargar(); NuevoPlan(null, EventArgs.Empty);
                MostrarExito("Plan reactivado correctamente.");
            }
            catch (Exception ex) { MostrarError(ex); }
        }
    }

    [System.ComponentModel.DesignerCategory("Code")]
    [System.ComponentModel.DesignTimeVisible(false)]
    public abstract class ReportesFormBase : FormularioModuloBase
    {
        private readonly Label resumen = new Label();
        private readonly SocioLogica socios = new SocioLogica();
        private readonly UsuarioSistemaLogica usuarios = new UsuarioSistemaLogica();
        private readonly EjercicioLogica ejercicios = new EjercicioLogica();
        private readonly RutinaLogica rutinas = new RutinaLogica();
        private readonly MembresiaLogica membresias = new MembresiaLogica();

        public ReportesFormBase()
            : base("Reportes", "Indicadores generales del estado del gimnasio", Color.FromArgb(79, 70, 229))
        {
            resumen.AutoSize = false; resumen.BackColor = Color.White; resumen.Dock = DockStyle.Fill;
            resumen.Font = new Font("Segoe UI", 16F, FontStyle.Bold); resumen.ForeColor = Color.FromArgb(30, 41, 59);
            resumen.Padding = new Padding(40); resumen.TextAlign = ContentAlignment.MiddleCenter;
            Contenido.Controls.Add(resumen); resumen.BringToFront();
            AgregarBoton("Generar reporte", Generar, true);
            AlCargarEnEjecucion(delegate { Generar(null, EventArgs.Empty); });
        }

        private void Generar(object sender, EventArgs e)
        {
            try
            {
                resumen.Text = "SOCIOS ACTIVOS\n" + socios.ListarActivos().Count
                    + "\n\nUSUARIOS ACTIVOS\n" + usuarios.ListarActivos().Count
                    + "\n\nMEMBRESÍAS HABILITADAS\n" + membresias.ListarHabilitadas().Count
                    + "\n\nRUTINAS ACTIVAS\n" + rutinas.ListarActivas().Count
                    + "\n\nEJERCICIOS DISPONIBLES\n" + ejercicios.ListarActivos().Count;
                Estado.Text = "Reporte generado: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            }
            catch (Exception ex) { MostrarError(ex); }
        }
    }
}
