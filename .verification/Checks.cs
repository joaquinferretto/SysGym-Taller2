using System;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Data.Entity;
using System.Runtime.InteropServices;
using System.Threading;
using Timer = System.Threading.Timer;
using exxen2._0.capaLogica;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaDatos.Repositorios;

/* Ejecuta regresiones contra una base aislada sin utilizar los datos del gimnasio. */
internal static class Checks
{
    private static int passed;
    private static UsuarioSistema admin, reception, trainer;
    private static readonly UsuarioSistemaLogica users = new UsuarioSistemaLogica();
    private static readonly SocioLogica members = new SocioLogica();
    private static readonly MembresiaLogica memberships = new MembresiaLogica();
    private static readonly CuotaMembresiaLogica fees = new CuotaMembresiaLogica();
    private static readonly PagoLogica payments = new PagoLogica();
    private static readonly AsistenciaLogica attendance = new AsistenciaLogica();
    private static Plan plan;
    private static int serial;
    private static readonly System.Collections.Generic.HashSet<string> uiChecked = new System.Collections.Generic.HashSet<string>();
    private const string TestPassword = "PruebaLocal2026!";

    /* Prepara los datos aislados y ejecuta las verificaciones solicitadas. */
    [STAThread]
    private static int Main(string[] args)
    {
        try
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Prepare();
            Logic();
            if (args.Contains("ui")) Screens();
            Console.WriteLine("TOTAL_PASS=" + passed);
            return 0;
        }
        catch (Exception ex) { Console.WriteLine("FAIL: " + ex); return 1; }
    }

    /* Comprueba una condición y registra el resultado de la prueba. */
    private static void Assert(bool condition, string name)
    {
        if (!condition) throw new Exception(name);
        passed++;
        Console.WriteLine("PASS: " + name);
    }

    /* Exige que una operación inválida se rechace con el motivo esperado. */
    private static void Reject(Action action, string fragment, string name)
    {
        try { action(); }
        catch (InvalidOperationException ex) { Assert(ex.Message.Contains(fragment), name); return; }
        throw new Exception("No se rechazó: " + name);
    }

    /* Crea personal de prueba con los tres roles y un plan válido. */
    private static void Prepare()
    {
        var suffix = DateTime.Now.ToString("HHmmss");
        admin = NewUser("AuditAdmin", "81" + suffix + "1", "Administrador");
        reception = NewUser("AuditRecepcion", "81" + suffix + "2", "Recepcionista");
        trainer = NewUser("AuditEntrenador", "81" + suffix + "3", "Entrenador");
        var routine = new RutinaLogica().Crear(new Rutina { Nombre = "Rutina de prueba " + suffix, IdEntrenador = trainer.IdUsuarioSistema });
        plan = new PlanLogica().Crear(new Plan { Nombre = "Plan de prueba", Precio = 100, IdRutina = routine.IdRutina, IncluyeEntrenador = true, IncluyeRutinaPersonal = true });
    }

    /* Crea un usuario aislado o recupera el creado en una ejecución anterior. */
    private static UsuarioSistema NewUser(string username, string dni, string role)
    {
        var existing = users.ObtenerPorUsername(username);
        if (existing != null) return existing;
        return users.Crear(new UsuarioSistema { Nombre = "Prueba", Apellido = role, DNI = dni, Username = username, Salario = 100, IdRol = new RolLogica().ObtenerPorDescripcion(role).IdRol }, TestPassword);
    }

    /* Crea un socio independiente para evitar cruces entre escenarios. */
    private static Socio NewMember()
    {
        return members.Crear(new Socio { Nombre = "Prueba", Apellido = "Aislada", DNI = "9" + DateTime.Now.ToString("MMddHHmmss") + (++serial), Peso = 70, Altura = 1.75m });
    }

    /* Registra una membresía y devuelve su primera cuota. */
    private static CuotaMembresia NewFee(Socio member, DateTime start)
    {
        var membership = memberships.Crear(new Membresia { IdSocio = member.IdSocio, IdPlan = plan.IdPlan, IdUsuarioSistema = reception.IdUsuarioSistema, FechaInicio = start });
        var list = fees.ListarPorMembresia(membership.IdMembresia);
        Assert(list.Count == 1 && list[0].Importe == 100, "Membresía y primera cuota conservan el precio");
        return list[0];
    }

    /* Construye un pago manual para una prueba. */
    private static Pago Payment(decimal amount, string state)
    {
        return new Pago { Importe = amount, Estado = state, Fecha = DateTime.Now, IdMetodoPago = payments.ListarMetodosPagoActivos().First().IdMetodoPago };
    }

    /* Verifica importes, fechas, transacciones, relaciones y operaciones de gestión. */
    private static void Logic()
    {
        foreach (var user in new[] { admin, reception, trainer })
            Assert(users.Autenticar(user.Username, TestPassword)?.IdUsuarioSistema == user.IdUsuarioSistema, "Login lógico " + user.Rol.Descripcion);
        Assert(users.Autenticar(admin.Username, "incorrecta") == null, "Credenciales inválidas rechazadas");
        var member = NewMember();
        var fee = NewFee(member, DateTime.Today);
        var pending = payments.RegistrarPago(Payment(150, EstadosTransaccionPago.Pendiente), fee.IdCuotaMembresia);
        Reject(() => payments.CambiarEstadoPago(pending.IdRegistroPago, EstadosTransaccionPago.Aprobado), "supera", "FIX-02 rechaza aprobación superior a cuota");
        Assert(payments.ObtenerPorId(pending.IdRegistroPago).Estado == EstadosTransaccionPago.Pendiente, "FIX-02 conserva el estado persistido al rechazar");
        pending.Estado = EstadosTransaccionPago.Aprobado;
        Reject(() => payments.ActualizarPago(pending, fee.IdCuotaMembresia), "supera", "FIX-02 valida también la actualización");
        pending.Importe = 100;
        payments.ActualizarPago(pending, fee.IdCuotaMembresia);
        payments.CambiarEstadoPago(pending.IdRegistroPago, EstadosTransaccionPago.Aprobado);
        Assert(fees.ObtenerPorId(fee.IdCuotaMembresia).EstadoPago == EstadosCuota.Pagada, "FIX-02 permite importe exacto y aprobación repetida");
        var another = NewFee(NewMember(), DateTime.Today);
        Reject(() => payments.RegistrarPago(Payment(101, EstadosTransaccionPago.Aprobado), another.IdCuotaMembresia), "supera", "FIX-02 valida también el registro");
        foreach (var hour in new[] { 0, 18, 23 })
        {
            var date = fee.FechaHasta.Date.AddHours(hour).AddMinutes(hour == 23 ? 59 : 0).AddSeconds(hour == 23 ? 59 : 0);
            var entry = attendance.Registrar(new Asistencia { IdSocio = member.IdSocio, Fecha = date });
            Assert(entry.Fecha == date, "FIX-03 admite último día a las " + date.ToString("HH:mm:ss") + " y conserva hora");
        }
        Reject(() => attendance.Registrar(new Asistencia { IdSocio = member.IdSocio, Fecha = fee.FechaHasta.Date.AddDays(1) }), "cuota pagada", "FIX-03 rechaza día posterior sin cuota");
        var oldFee = NewFee(NewMember(), DateTime.Today.AddMonths(-2));
        memberships.ActualizarEstadoPorDeuda(oldFee.IdMembresia);
        Assert(!memberships.ObtenerPorId(oldFee.IdMembresia).Estado, "Deuda vencida inhabilita membresía");
        payments.RegistrarPago(Payment(100, EstadosTransaccionPago.Aprobado), oldFee.IdCuotaMembresia);
        Assert(memberships.ObtenerPorId(oldFee.IdMembresia).Estado, "Pago recalcula deuda con cuota ya guardada");
        payments.AnularPago(fees.ObtenerPorId(oldFee.IdCuotaMembresia).IdRegistroPago.Value);
        Assert(!memberships.ObtenerPorId(oldFee.IdMembresia).Estado, "Anulación recalcula deuda con cuota ya guardada");
        fees.AnularCuota(oldFee.IdCuotaMembresia);
        Assert(memberships.ObtenerPorId(oldFee.IdMembresia).Estado, "Anular cuota actualiza membresía dentro de transacción");
        var rollbackMember = NewMember();
        try { memberships.Crear(new Membresia { IdSocio = rollbackMember.IdSocio, IdPlan = plan.IdPlan, IdUsuarioSistema = admin.IdUsuarioSistema, FechaInicio = new DateTime(9999,12,1), FechaVencimiento = new DateTime(9999,12,31) }); }
        catch (ArgumentOutOfRangeException) { }
        Assert(memberships.ObtenerPorSocio(rollbackMember.IdSocio).Count == 0, "Fallo de primera cuota revierte alta de membresía");
        using (var work = new GymUnidadDeTrabajo())
        {
            var context = (DbContext)typeof(GymUnidadDeTrabajo).GetField("contexto", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(work);
            int queries = 0;
            context.Database.Log = s => { if (s.StartsWith("SELECT")) queries++; };
            var routines = work.Rutinas.ConsultarSoloLectura("Entrenador", "Asignaciones").ToList();
            var initial = queries;
            foreach (var routine in routines) { var name = routine.Entrenador.Nombre; var count = routine.Asignaciones.Count; }
            Assert(!context.Configuration.LazyLoadingEnabled && !context.Configuration.ProxyCreationEnabled, "Carga diferida y proxies deshabilitados");
            Assert(!context.ChangeTracker.Entries().Any(), "AsNoTracking no agrega entidades al seguimiento");
            Assert(initial == queries && initial == 1, "Include carga relaciones en una consulta sin N+1");
            work.Socios.Agregar(new Socio { Nombre = new string('X',101), Apellido = "Prueba", DNI = "ErrorValidacion" });
            try { work.GuardarCambios(); throw new Exception("Se esperaba error EF"); }
            catch (InvalidOperationException ex) { Assert(ex.InnerException != null && ex.Message.Contains("Nombre"), "Error EF conserva causa y campo inválido"); }
        }
        Crud(member);
    }

    /* Comprueba modificaciones, bajas y reactivaciones con datos de prueba. */
    private static void Crud(Socio member)
    {
        member.Nombre = "Editado"; members.Modificar(member); members.DarDeBaja(member.IdSocio);
        Assert(!members.ObtenerPorId(member.IdSocio).Estado, "Baja de socio"); members.Reactivar(member.IdSocio);
        Assert(members.ObtenerPorId(member.IdSocio).Nombre == "Editado", "Edición y reactivación de socio");
        var exerciseLogic = new EjercicioLogica();
        var exercise = exerciseLogic.Crear(new Ejercicio { Nombre = "Prueba " + Guid.NewGuid().ToString("N") });
        exercise.Descripcion = "Editado"; exerciseLogic.Modificar(exercise); exerciseLogic.DarDeBaja(exercise.IdEjercicio); exerciseLogic.Reactivar(exercise.IdEjercicio);
        Assert(exerciseLogic.ObtenerPorId(exercise.IdEjercicio).Estado, "CRUD de ejercicio");
        plan.Descripcion = "Editado"; var plans = new PlanLogica(); plans.Modificar(plan); plans.DarDeBaja(plan.IdPlan); plans.Reactivar(plan.IdPlan);
        Assert(plans.ObtenerPorId(plan.IdPlan).Estado, "CRUD de plan");
        admin.Salario = 120; users.Modificar(admin); users.DarDeBaja(admin.IdUsuarioSistema); users.Reactivar(admin.IdUsuarioSistema);
        Assert(users.ObtenerPorId(admin.IdUsuarioSistema).Salario == 120, "CRUD de usuario");
        var fee = NewFee(NewMember(), DateTime.Today);
        var assigned = new MembresiaEntrenadorLogica(); var assignment = assigned.AsignarEntrenador(fee.IdMembresia,trainer.IdUsuarioSistema);
        assigned.DarDeBajaAsignacion(assignment.IdMembresiaEntrenador); assigned.ReactivarAsignacion(assignment.IdMembresiaEntrenador);
        Assert(assigned.ObtenerEntrenadorActivo(fee.IdMembresia).IdUsuarioSistema == trainer.IdUsuarioSistema, "Asignación, baja y reactivación de entrenador");
        var routines = new RutinaLogica(); var routine = routines.Crear(new Rutina { Nombre = "Rutina CRUD", IdEntrenador = trainer.IdUsuarioSistema });
        routine.Descripcion = "Editada"; routines.Modificar(routine); routines.DarDeBaja(routine.IdRutina); routines.Reactivar(routine.IdRutina);
        var details = new RutinaEjercicioLogica(); var detail = details.AgregarEjercicio(new RutinaEjercicio { IdRutina = routine.IdRutina, IdEjercicio = exercise.IdEjercicio, Series = 3, Repeticiones = 10, Orden = 1 });
        detail.Series = 4; details.Modificar(detail); details.Quitar(detail.IdRutinaEjercicio);
        Assert(details.ListarPorRutina(routine.IdRutina).Count == 0, "CRUD de rutina y sus ejercicios");
        var assignments = new RutinaAsignacionLogica(); var ra = assignments.Asignar(routine.IdRutina,fee.IdMembresia); assignments.Desasignar(ra.IdRutinaAsignacion);
        users.DarDeBaja(trainer.IdUsuarioSistema);
        Reject(() => assignments.Asignar(routine.IdRutina,fee.IdMembresia), "entrenador activo", "Asignación rechaza entrenador inactivo");
        users.Reactivar(trainer.IdUsuarioSistema);
        var entry = attendance.ListarPorSocio(member.IdSocio).First(); attendance.DarDeBaja(entry.IdAsistencia); attendance.Reactivar(entry.IdAsistencia);
        Assert(attendance.ListarPorSocio(member.IdSocio).Any(a => a.IdAsistencia == entry.IdAsistencia), "Baja y reactivación de asistencia");
    }

    /* Obtiene un control existente por nombre de campo para disparar sus eventos reales. */
    private static T Control<T>(object form, string name) where T : class
    {
        return (T)form.GetType().GetField(name, BindingFlags.Instance | BindingFlags.NonPublic).GetValue(form);
    }

    /* Abre el acceso y recorre la navegación real de los tres roles mediante Click. */
    private static void Screens()
    {
        foreach (var user in new[] { admin, reception, trainer })
        {
            using (var login = new exxen2._0.capaVisual.Autenticacion.Login())
            {
                login.Show(); Application.DoEvents();
                Control<TextBox>(login,"txtUsername").Text = user.Username;
                Control<TextBox>(login,"txtPassword").Text = TestPassword;
                Control<Button>(login,"btnIngresar").PerformClick(); Application.DoEvents();
                var dashboard = Application.OpenForms.Cast<Form>().FirstOrDefault(f => f.GetType().Name.StartsWith("Dashboard"));
                Assert(dashboard != null, "Login visual " + user.Rol.Descripcion);
                var panel = Control<Panel>(dashboard,"panelContenido");
                var names = user == admin ? new[] {"btnUsuarios","btnSocios","btnPlanes","btnMembresias","btnPagos","btnEjercicios","btnRutinas","btnReportes"}
                    : user == reception ? new[] {"btnSocios","btnMembresias","btnPagos","btnAsignar","btnConsultar","btnAsistencias"}
                    : new[] {"btnSocios","btnRutinas","btnEjercicios","btnAsistencias"};
                foreach(var name in names)
                {
                    Control<Button>(dashboard,name).PerformClick(); Application.DoEvents();
                    var module = panel.Controls.OfType<Form>().LastOrDefault(f => !f.IsDisposed);
                    Assert(module != null && module.Visible, "Navegación " + user.Rol.Descripcion + " / " + name);
                    ModuleCrud(module);
                    Control<Button>(module,"btnVolver").PerformClick(); Application.DoEvents();
                }
                Control<Button>(dashboard,"btnCambiarCuenta").PerformClick(); Application.DoEvents();
                Assert(login.Visible, "Cambio de cuenta " + user.Rol.Descripcion);
                login.Close();
            }
        }
    }

    [DllImport("kernel32.dll")] private static extern uint GetCurrentThreadId();
    private delegate bool WindowVisitor(IntPtr hwnd, IntPtr state);
    [DllImport("user32.dll")] private static extern bool EnumThreadWindows(uint thread, WindowVisitor visitor, IntPtr state);
    [DllImport("user32.dll", CharSet=CharSet.Unicode)] private static extern int GetClassName(IntPtr hwnd, System.Text.StringBuilder name, int length);
    [DllImport("user32.dll")] private static extern IntPtr SendMessage(IntPtr hwnd, uint message, IntPtr wparam, IntPtr lparam);

    /* Dispara Click y responde únicamente a la confirmación del hilo de prueba. */
    private static void Click(Form form, string name, bool confirm = false)
    {
        var button = Control<Button>(form,name); Assert(button.Enabled, form.Name + " / " + name + " habilitado");
        var thread = GetCurrentThreadId();
        using(var timer = new Timer(_ => {
            if (!confirm) return;
            EnumThreadWindows(thread, (hwnd,state) => {
                var text = new System.Text.StringBuilder(100); GetClassName(hwnd,text,100);
                if(text.ToString() == "#32770") SendMessage(hwnd,0x111,new IntPtr(6),IntPtr.Zero);
                return true;
            }, IntPtr.Zero);
        },null,100,100)) { button.PerformClick(); Application.DoEvents(); }
    }

    /* Escribe un valor en un campo existente del formulario. */
    private static void Text(Form form, string name, string value) { Control<TextBox>(form,name).Text = value; }

    /* Selecciona una fila por clave, disparando SelectionChanged. */
    private static void Select(Form form, int id)
    {
        var grid=Control<DataGridView>(form,"tabla"); grid.ClearSelection();
        var row=grid.Rows.Cast<DataGridViewRow>().First(r=>Convert.ToInt32(r.Cells[0].Value)==id);
        grid.CurrentCell=row.Cells.Cast<DataGridViewCell>().First(c=>c.Visible); row.Selected=true; Application.DoEvents();
    }

    /* Prueba las operaciones disponibles en cada pantalla con datos aislados. */
    private static void ModuleCrud(Form form)
    {
        var type=form.GetType().Name; if(!uiChecked.Add(type))return;
        var suffix=Guid.NewGuid().ToString("N").Substring(0,10);
        if(type=="GestionUsuariosForm")
        {
            Click(form,"nuevo"); Text(form,"nombre","UsuarioUI"); Text(form,"apellido","Prueba"); Text(form,"dni","UI"+suffix);
            Text(form,"username","UI"+suffix); Text(form,"password",TestPassword); Text(form,"salario","100");
            Click(form,"guardar"); var user=users.ObtenerPorUsername("UI"+suffix); Assert(user!=null,"UI alta usuario");
            Select(form,user.IdUsuarioSistema); Text(form,"salario","200"); Click(form,"actualizar");
            Select(form,user.IdUsuarioSistema); Click(form,"darDeBaja",true);
            Control<ComboBox>(form,"filtroEstado").SelectedItem="Inactivos"; Select(form,user.IdUsuarioSistema); Click(form,"reactivar");
            Assert(users.ObtenerPorId(user.IdUsuarioSistema).Salario==200&&users.ObtenerPorId(user.IdUsuarioSistema).Estado,"UI edición/baja/reactivación usuario");
        }
        else if(type=="GestionSociosForm")
        {
            Click(form,"nuevo"); Text(form,"nombre","SocioUI"); Text(form,"apellido","Prueba"); Text(form,"dni","UI"+suffix); Text(form,"peso","70"); Text(form,"altura","1,75");
            Click(form,"guardar"); var member=members.ObtenerPorDni("UI"+suffix); Assert(member!=null,"UI alta socio");
            Select(form,member.IdSocio); Text(form,"nombre","SocioEditadoUI"); Click(form,"actualizar");
            Select(form,member.IdSocio); Click(form,"darDeBaja",true); Control<ComboBox>(form,"filtroEstado").SelectedItem="Inactivos";
            Select(form,member.IdSocio); Click(form,"reactivar"); Assert(members.ObtenerPorId(member.IdSocio).Estado,"UI edición/baja/reactivación socio");
        }
        else if(type=="GestionPlanesForm")
        {
            Click(form,"nuevo"); Text(form,"nombre","PlanUI"+suffix); Text(form,"precio","100"); Click(form,"guardar");
            var plans=new PlanLogica(); var item=plans.ListarActivos().Single(p=>p.Nombre=="PlanUI"+suffix); Assert(item!=null,"UI alta plan");
            Select(form,item.IdPlan); Text(form,"precio","120"); Click(form,"actualizar");
            Select(form,item.IdPlan); Click(form,"darDeBaja",true); Control<ComboBox>(form,"filtroEstado").SelectedItem="Inactivos";
            Select(form,item.IdPlan); Click(form,"reactivar"); Assert(plans.ObtenerPorId(item.IdPlan).Precio==120,"UI edición/baja/reactivación plan");
        }
        else if(type=="GestionMembresiasForm")
        {
            var member=members.ListarActivos().First(s=>!memberships.ObtenerPorSocio(s.IdSocio).Any(m=>m.Estado));
            Click(form,"nuevo"); Control<ComboBox>(form,"socio").SelectedValue=member.IdSocio; Control<ComboBox>(form,"plan").SelectedValue=plan.IdPlan; Click(form,"crear");
            var membership=memberships.ObtenerPorSocio(member.IdSocio).First(); Assert(membership!=null,"UI alta membresía");
            Select(form,membership.IdMembresia); Control<DateTimePicker>(form,"vencimiento").Value=membership.FechaVencimiento.AddDays(1); Click(form,"actualizar");
            Select(form,membership.IdMembresia); Click(form,"deshabilitar",true); Select(form,membership.IdMembresia); Click(form,"habilitar");
            Assert(memberships.ObtenerPorId(membership.IdMembresia).Estado,"UI edición/baja/reactivación membresía");
        }
        else if(type=="GestionPagosForm")
        {
            var fee=fees.ListarParaGestion().First(c=>c.EstadoPago==EstadosCuota.Pendiente&&!c.IdRegistroPago.HasValue&&c.Membresia.Estado);
            Select(form,fee.IdCuotaMembresia); Click(form,"registrar"); Assert(fees.ObtenerPorId(fee.IdCuotaMembresia).EstadoPago==EstadosCuota.Pagada,"UI registro de pago");
            Select(form,fee.IdCuotaMembresia); Click(form,"reembolsar",true); Assert(fees.ObtenerPorId(fee.IdCuotaMembresia).Pago.Estado==EstadosTransaccionPago.Reembolsado,"UI reembolso de pago");
        }
        else if(type=="GestionEjerciciosForm")
        {
            Text(form,"nombre","EjercicioUI"+suffix); Text(form,"descripcion","Prueba"); Click(form,"guardar"); var logic=new EjercicioLogica();
            var item=logic.ListarActivos().Single(e=>e.Nombre=="EjercicioUI"+suffix); Assert(item!=null,"UI alta ejercicio sin modificar selección previa");
            Select(form,item.IdEjercicio); Text(form,"descripcion","EditadoUI"); Click(form,"guardar");
            Select(form,item.IdEjercicio); Click(form,"darDeBaja"); Control<ComboBox>(form,"filtroEstado").SelectedItem="Inactivos"; Select(form,item.IdEjercicio); Click(form,"reactivar");
            Assert(logic.ObtenerPorId(item.IdEjercicio).Estado&&logic.ObtenerPorId(item.IdEjercicio).Descripcion=="EditadoUI","UI edición/baja/reactivación ejercicio");
        }
        else if(type=="GestionAsignacionesForm")
        {
            var membership=memberships.ListarHabilitadas().First(m=>m.Plan.IncluyeEntrenador&&new MembresiaEntrenadorLogica().ObtenerEntrenadorActivo(m.IdMembresia)==null);
            Text(form,"membresia",membership.IdMembresia.ToString()); Control<ComboBox>(form,"entrenador").SelectedValue=trainer.IdUsuarioSistema; Click(form,"asignar");
            var logic=new MembresiaEntrenadorLogica(); var first=logic.ListarPorMembresia(membership.IdMembresia).First(); Assert(first.Estado,"UI asignación entrenador");
            Click(form,"cambiar"); var current=logic.ListarPorMembresia(membership.IdMembresia).First(a=>a.Estado); Select(form,current.IdMembresiaEntrenador); Click(form,"darDeBaja");
            Assert(logic.ObtenerEntrenadorActivo(membership.IdMembresia)==null,"UI cambio y baja sobre asignación seleccionada");
        }
        else if(type=="GestionAsistenciasForm")
        {
            var fee=fees.ListarParaGestion().First(c=>c.EstadoPago==EstadosCuota.Pagada&&c.FechaHasta>=DateTime.Today);
            Control<ComboBox>(form,"socio").SelectedValue=fee.Membresia.IdSocio; Control<DateTimePicker>(form,"fecha").Value=fee.FechaHasta.Date.AddHours(23).AddMinutes(59);
            Click(form,"registrar"); var entry=attendance.ListarPorSocio(fee.Membresia.IdSocio).First(); Assert(entry.Fecha.Hour==23,"UI asistencia último día");
            Select(form,entry.IdAsistencia); Click(form,"darDeBaja"); Control<ComboBox>(form,"filtroEstado").SelectedItem="Inactivos"; Select(form,entry.IdAsistencia); Click(form,"reactivar");
            Assert(attendance.ListarPorSocio(fee.Membresia.IdSocio).Any(a=>a.IdAsistencia==entry.IdAsistencia),"UI baja/reactivación asistencia");
        }
        else if(type=="RutinasEntrenadorForm")
        {
            Click(form,"nuevaRutina"); Text(form,"nombre","RutinaUI"+suffix); Click(form,"guardarRutina"); var logic=new RutinaLogica();
            var routine=logic.ListarPorEntrenador(trainer.IdUsuarioSistema).Single(r=>r.Nombre=="RutinaUI"+suffix); Assert(routine!=null,"UI alta rutina");
            Select(form,routine.IdRutina); Text(form,"descripcion","EditadaUI"); Click(form,"guardarRutina"); Select(form,routine.IdRutina);
            Text(form,"series","3"); Text(form,"repeticiones","10"); Text(form,"descanso","60"); Text(form,"orden","1"); Click(form,"agregarEjercicio");
            var membership=memberships.ListarHabilitadas().First(m=>m.Plan.IncluyeRutinaPersonal); Control<ComboBox>(form,"membresia").SelectedValue=membership.IdMembresia; Click(form,"asignar");
            Select(form,routine.IdRutina); Click(form,"darDeBaja"); Assert(!logic.ObtenerPorId(routine.IdRutina).Estado,"UI edición/ejercicio/asignación/baja rutina");
        }
    }
}
