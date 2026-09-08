const fs=require('fs');
const inventory=require('./.audit-inventory.json');
const descriptions={},missing=[];
const extraClasses={ValidacionesGym:'Centraliza las comprobaciones de roles activos para los casos de uso del gimnasio.'};
const subjects={Asistencia:'asistencias',CuotaMembresia:'cuotas de membresía',Divisa:'divisas',Ejercicio:'ejercicios',Membresia:'membresías',MembresiaEntrenador:'asignaciones de entrenador',MercadoPago:'datos de Mercado Pago',MetodoPago:'métodos de pago',Pago:'pagos',PagoEfectivo:'detalles de efectivo',Plan:'planes',Rol:'roles',Rutina:'plantillas de rutina',RutinaAsignacion:'asignaciones de rutina',RutinaEjercicio:'ejercicios de una rutina',Socio:'socios',UsuarioSistema:'usuarios del sistema'};
const special={
 'Inicializar':'Carga las opciones y los registros necesarios y prepara el formulario para una nueva operación.',
 'AplicarFiltro':'Filtra los registros cargados por el criterio ingresado y actualiza la grilla y su contador.',
 'Contiene':'Compara el texto de búsqueda sin distinguir mayúsculas y admite valores vacíos.',
 'NombreRol':'Obtiene la descripción del rol o utiliza el nombre predeterminado cuando no está disponible.',
 'NombreSocio':'Compone el nombre que se muestra en pantalla y contempla socios no disponibles.',
 'NombrePlan':'Obtiene el nombre del plan para mostrarlo, contemplando relaciones no disponibles.',
 'DescribirBeneficios':'Presenta los beneficios de entrenador y rutina del plan en una descripción legible.',
 'EstablecerModo':'Habilita las acciones disponibles según la selección y el estado del registro.',
 'LeerUsuario':'Recoge los campos de personal y comprueba que se haya seleccionado un rol.',
 'LeerSocio':'Recoge los datos personales y físicos ingresados, validando su formato.',
 'LeerPlan':'Recoge el precio, los beneficios y la rutina seleccionada para enviar el plan a la lógica.',
 'Periodo':'Presenta el inicio y el fin de la cuota con el formato de fecha usado en la pantalla.',
 'SeleccionarPrimeraPendiente':'Busca la primera cuota sin pago de la membresía elegida y prepara su registro.',
 'MostrarCuota':'Carga el período y los datos de la cuota seleccionada y habilita las acciones correspondientes.',
 'MostrarSinCuota':'Limpia la selección y deshabilita las operaciones cuando no hay una cuota disponible.',
 'EnteroOpcional':'Convierte un campo opcional a entero o informa un formato inválido.',
 'DecimalOpcional':'Convierte un campo opcional a decimal o informa un formato inválido.',
 'CargarLista':'Consulta las asignaciones de la membresía y muestra el entrenador y su estado histórico.',
 'CargarCombos':'Carga los socios y planes activos que pueden seleccionarse al registrar una membresía.',
 'CargarRoles':'Carga los roles activos disponibles para crear o modificar personal.',
 'CargarRutinas':'Carga las plantillas activas que pueden elegirse como rutina base del plan.',
 'CargarEjercicios':'Carga el catálogo de ejercicios activos para incorporarlos a una rutina.',
 'CargarMembresias':'Carga las membresías disponibles y sus datos de presentación para seleccionarlas.',
 'CargarMetodosPago':'Carga los métodos de pago activos que pueden utilizarse para registrar un cobro.',
 'CargarSocios':'Carga los socios activos disponibles para registrar una asistencia.',
 'CargarEntrenadores':'Carga los usuarios activos con rol de entrenador para realizar asignaciones.',
 'ValidarCampos':'Comprueba los campos de acceso y devuelve si están listos para autenticar al usuario.',
 'CrearDashboard':'Selecciona el dashboard existente correspondiente al rol del usuario autenticado.',
 'EnModoDisenio':'Detecta si el control se está editando en Visual Studio para evitar cargas de ejecución.',
 'MostrarError':'Informa el error en la pantalla y en un mensaje para que el usuario pueda corregir la operación.',
 'MostrarExito':'Actualiza el mensaje de estado después de completar una operación.',
 'Entero':'Comprueba que el campo contenga un entero positivo antes de enviarlo a la lógica.',
 'DecimalPositivo':'Interpreta coma o punto decimal y valida el rango permitido del campo.',
 'ValidarEntradaDecimal':'Permite números y un único separador decimal durante la escritura del campo.',
 'CambiarCuenta':'Marca el cambio de sesión y cierra el dashboard para volver al acceso.',
 'Salir':'Solicita confirmación antes de cerrar la sesión y salir de la aplicación.',
 'AbrirFormulario':'Muestra el módulo solicitado dentro del dashboard y libera el módulo anterior.',
 'EstablecerContenidoInicio':'Registra el contenido inicial existente y la acción que lo actualiza al volver.',
 'MostrarContenidoInicio':'Restaura el contenido inicial del dashboard y lo actualiza cuando corresponde.',
 'Actualizar':'Actualiza el estado de cuotas y el pronóstico, evitando cargas simultáneas.',
 'CargarClimaAsync':'Consulta el pronóstico sin bloquear la espera de red y muestra una alternativa si falla.',
 'ObtenerDetalleError':'Recupera la causa del error y traduce el tiempo de espera a un mensaje legible.',
 'MostrarClimaSinConexion':'Presenta los siete días sin datos cuando el servicio de pronóstico no responde.',
 'CrearDiaClima':'Compone la tarjeta de pronóstico con los controles estándar y el aspecto existente.',
 'CargarEstadoCuotas':'Presenta el saldo y la situación de cada membresía y resume cuántas tienen deuda.',
 'get_CambioCuentaSolicitado':'Permite al formulario de acceso saber si debe iniciar otra sesión al cerrar el dashboard.',
 'Registrar':'Valida socio, membresía y cuota pagada para registrar el ingreso, incluyendo todo el día de vencimiento.',
 'RegistrarPago':'Valida el cobro y lo vincula a su cuota, actualizando la deuda en una misma transacción.',
 'CambiarEstadoPago':'Valida la transición y el importe aprobado antes de actualizar pago, cuota y membresía.',
 'ActualizarPago':'Valida los datos modificados y recalcula cuota y deuda sin cambiar su asociación histórica.',
 'AnularPago':'Anula el pago seleccionado y recalcula el saldo mediante el cambio de estado validado.',
 'ReembolsarPago':'Marca el reembolso de un pago aprobado y recalcula su efecto en la cuota.',
 'ValidarMetodoPago':'Exige un método activo con exactamente un detalle de efectivo o Mercado Pago.',
 'ValidarImporteAprobado':'Rechaza importes no positivos o superiores a la cuota antes de contabilizar un pago.',
 'CalcularTotalAprobado':'Obtiene el importe contabilizado de la cuota; los pagos no aprobados aportan cero.',
 'CalcularSaldoPendiente':'Obtiene la cuota y calcula cuánto falta abonar descontando únicamente pagos aprobados.',
 'CrearPrimeraCuota':'Genera la cuota inicial si la membresía todavía no posee cuotas.',
 'GenerarSiguienteCuota':'Crea el siguiente período mensual con el precio actual del plan activo.',
 'ObtenerCuotaActual':'Busca la cuota no anulada que cubre el día actual de la membresía.',
 'CrearEstadoCuenta':'Resume cuotas, deuda y período cubierto para informar la situación de una membresía.',
 'CalcularSaldoSinContexto':'Calcula el saldo con los datos ya cargados sin abrir otra consulta.',
 'ReactivarCuota':'Recupera una cuota anulada y recalcula su estado y la deuda dentro de una transacción.',
 'AnularCuota':'Anula la cuota sin borrar su historia y actualiza la deuda de la membresía.',
 'EstaVencida':'Indica si una cuota pendiente terminó antes del día actual.',
 'CalcularSaldo':'Calcula el importe pendiente de la cuota utilizando su pago aprobado, si existe.',
 'RecalcularEstadoPago':'Actualiza el estado de la cuota y la deuda de su membresía en una transacción.',
 'CrearPrimeraCuotaEnContexto':'Agrega la primera cuota utilizando la misma unidad de trabajo del alta de membresía.',
 'RecalcularEstadoPagoEnContexto':'Determina si la cuota está pagada según el importe aprobado y respeta las anulaciones.',
 'CalcularSaldoEnContexto':'Calcula el saldo de la cuota cargada, considerando cero para cuotas anuladas.',
 'CrearCuotaEnContexto':'Agrega una cuota mensual pendiente conservando el precio histórico del plan.',
 'CalcularPeriodoHasta':'Calcula el último día del período mensual a partir de su fecha de inicio.',
 'ValidarPlanActivo':'Impide generar cuotas con un plan inexistente o dado de baja.',
 'AsignarEntrenador':'Valida los beneficios de la membresía y crea su asignación de entrenador activo.',
 'CambiarEntrenador':'Finaliza las asignaciones activas y registra el nuevo entrenador en una transacción.',
 'ObtenerEntrenadorActivo':'Obtiene el entrenador de la asignación activa de la membresía, si existe.',
 'DarDeBajaAsignacion':'Desactiva la asignación del entrenador conservando el registro histórico.',
 'ReactivarAsignacion':'Valida el plan y el entrenador antes de recuperar una asignación sin duplicar la activa.',
 'ObtenerMembresiaConPlan':'Carga la membresía junto con su plan y rechaza identificadores inexistentes.',
 'ValidarAsignacion':'Comprueba membresía habilitada, beneficio del plan y rol activo del entrenador.',
 'CambiarPlan':'Cambia el plan y finaliza las asignaciones que el flujo existente desactiva, sin borrar historia.',
 'Habilitar':'Habilita la membresía si no posee cuotas vencidas pendientes.',
 'Deshabilitar':'Solicita la baja lógica de la membresía manteniendo su historial.',
 'TieneCuotaVencidaPendiente':'Consulta si la membresía posee cuotas impagas anteriores al día actual.',
 'DebeDeshabilitarMembresia':'Indica si la deuda vencida exige deshabilitar la membresía.',
 'ActualizarEstadoPorDeuda':'Guarda la habilitación de la membresía según su deuda vencida.',
 'TieneCuotaVencidaPendienteEnContexto':'Comprueba la deuda vencida utilizando la unidad de trabajo de la operación.',
 'ActualizarEstadoPorDeudaEnContexto':'Ajusta la habilitación según las cuotas persistidas dentro de la operación actual.',
 'CambiarEstado':'Modifica la habilitación de la membresía y finaliza sus rutinas al deshabilitarla.',
 'ValidarMembresia':'Exige los identificadores de socio, plan y usuario que registra la membresía.',
 'ValidarReferenciasActivas':'Comprueba socio y plan activos y que el registrador tenga un rol permitido.',
 'Asignar':'Valida rutina, entrenador y beneficios de la membresía antes de vincular la plantilla.',
 'Desasignar':'Finaliza la asignación de rutina y conserva su fecha de cierre.',
 'AgregarEjercicio':'Valida la rutina, el ejercicio y sus parámetros antes de incorporarlo a la plantilla.',
 'Quitar':'Da de baja el ejercicio de la rutina sin eliminar su registro.',
 'ValidarEntrenador':'Exige que el creador de la rutina sea un usuario activo con rol de entrenador.',
 'CalcularIMC':'Calcula el índice a partir del peso y la altura registrados, validando los datos requeridos.',
 'ValidarAlturaFraccionaria':'Exige que la altura en metros tenga parte decimal según la regla del proyecto.',
 'Autenticar':'Valida las credenciales y el rol activo; devuelve el usuario autenticado o null.',
 'GenerarPassword':'Genera una sal aleatoria y almacena la contraseña derivada con sus parámetros Argon2id.',
 'VerificarPassword':'Comprueba la contraseña contra el formato Argon2id almacenado y rechaza datos inválidos.',
 'DerivarPassword':'Calcula el hash Argon2id utilizando la sal y los parámetros indicados.',
 'ObtenerRolActivo':'Obtiene el rol requerido y rechaza roles inexistentes o inactivos.',
 'ValidarUnicidad':'Impide duplicar DNI o nombre de usuario, excluyendo el registro que se modifica.',
 'CompararBytes':'Compara el contenido de los hashes sin terminar al encontrar la primera diferencia.',
 'PuedeRegistrarMembresia':'Comprueba si el usuario activo tiene rol de administrador o recepcionista.',
 'EsEntrenadorActivo':'Comprueba que tanto el usuario como su rol de entrenador estén activos.',
 'TieneRolActivo':'Valida el estado del usuario y del rol, comparando su descripción sin distinguir mayúsculas.',
 'get_Ciudad':'Indica la ciudad para la que se solicita el pronóstico configurado.',
 'ObtenerPronosticoSemanalAsync':'Consulta y transforma los siete días de pronóstico, conservando la causa si falla el servicio.',
 'CrearCliente':'Prepara el cliente HTTP con proxy, TLS y tiempo de espera para consultar el clima.',
 'ValidarRespuesta':'Rechaza respuestas del servicio que no contienen los siete días completos.',
 'DescribirClima':'Traduce el código meteorológico a la descripción usada por la aplicación.',
 'ObtenerIcono':'Selecciona el símbolo asociado al código del pronóstico.',
 'OnModelCreating':'Configura tablas, tipos, precisión decimal y relaciones sin borrado en cascada.',
 'Consultar':'Prepara una consulta con seguimiento e incluye las relaciones solicitadas explícitamente.',
 'ConsultarSoloLectura':'Prepara una consulta sin seguimiento con las relaciones solicitadas, evitando cargas implícitas.',
 'Buscar':'Busca una entidad por su clave y la mantiene asociada a la unidad de trabajo para modificarla.',
 'Find':'Mantiene la operación de búsqueda compatible delegando en Buscar.',
 'Primero':'Obtiene el único registro que cumple la condición o null si no existe.',
 'Existe':'Comprueba si hay algún registro que cumpla la condición sin cargar toda la lista.',
 'Agregar':'Registra una entidad nueva para insertarla cuando se confirmen los cambios.',
 'Add':'Mantiene la operación de alta compatible delegando en Agregar.',
 'Confirmar':'Confirma la transacción para conservar todos los cambios de la operación.',
 'GuardarCambios':'Persiste los cambios y conserva la excepción original al informar errores de validación o actualización.',
 'IniciarTransaccion':'Abre una transacción compartida por los repositorios de la unidad de trabajo.',
 'CrearRepositorio':'Construye un repositorio usando el contexto compartido de la unidad de trabajo.',
 'get_ElementType':'Expone el tipo de entidad para componer consultas LINQ sobre el repositorio.',
 'get_Expression':'Expone la expresión de consulta que Entity Framework traducirá a SQL.',
 'get_Provider':'Expone el proveedor de consultas de Entity Framework para ejecutar LINQ.',
 'GetEnumerator':'Permite recorrer los resultados de la consulta del repositorio.',
 'EsValido':'Comprueba que el estado pertenezca al conjunto de valores admitidos por el modelo.',
 'Main':'Inicia Windows Forms con estilos visuales y abre el formulario de acceso.'
};
const classComments={DashboardController:'Coordina la navegación entre los formularios existentes sin aportar controles ni herencia visual.',FormularioVisualHelper:'Agrupa validaciones de entrada y mensajes compartidos por los formularios estándar.',IDashboardSesion:'Expone la intención de cambiar de cuenta al cerrar un dashboard.',PronosticoDia:'Transporta los datos de un día de pronóstico para presentarlos en el dashboard.',RespuestaClima:'Representa la respuesta JSON del servicio de pronóstico.',DatosDiarios:'Representa las series diarias recibidas del servicio meteorológico.',EstadoCuentaMembresia:'Transporta el saldo, el período y la situación calculada de una membresía.',SocioMembresiaItem:'Asocia el identificador del socio con el texto mostrado al elegir una membresía.',MembresiaPagoItem:'Asocia una membresía y su habilitación con el texto mostrado al registrar pagos.',ClienteLogica:'Conserva el tipo existente sin operaciones; la gestión de socios se realiza en SocioLogica.',Conexion:'Conserva el tipo existente sin abrir conexiones; la persistencia utiliza GymUnidadDeTrabajo.',GymContext:'Mapea las entidades del gimnasio a SQL Server mediante Entity Framework 6.',GymUnidadDeTrabajo:'Comparte un contexto y una transacción entre los repositorios de una operación.',IUnidadDeTrabajo:'Define los repositorios y las operaciones de persistencia que utiliza la capa lógica.',IRepositorio:'Define las consultas y altas comunes de las entidades persistidas.',Repositorio:'Implementa las operaciones comunes de las entidades mediante el contexto compartido.',ITransaccion:'Define la confirmación y liberación de una operación atómica.',Transaccion:'Controla la confirmación o reversión de la transacción de Entity Framework.',EstadosCuota:'Centraliza los estados válidos de las cuotas de membresía.',EstadosTransaccionPago:'Centraliza los estados válidos de los pagos registrados.',Program:'Contiene el punto de entrada de la aplicación de escritorio.',ClimaLogica:'Consulta y transforma el pronóstico semanal para la capa visual.'};
for(const row of inventory){
 let comment, n=row.name, subject=subjects[row.owner.replace(/Logica$/,'')]||row.owner;
 if(/ClassDeclaration|InterfaceDeclaration/.test(row.kind)){
  comment=classComments[n]||extraClasses[n];
  if(!comment&&subjects[n])comment='Representa '+subjects[n]+' y sus relaciones persistidas en SQL Server.';
  if(!comment&&n.endsWith('Logica'))comment='Coordina las operaciones y validaciones de negocio de '+subjects[n.replace('Logica','')]+'.';
  if(!comment&&row.file.startsWith('capaVisual'))comment=n==='DashboardInicioAdministrador'?'Presenta el pronóstico y el estado de cuotas en el UserControl existente.':'Presenta '+n.replace(/Form|Dashboard|Gestion|Consulta/g,'').replace(/([a-z])([A-Z])/g,'$1 $2').toLowerCase()+' y atiende sus acciones mediante eventos de Windows Forms.';
 } else if(row.kind==='ConstructorDeclaration'){
  if(row.file.includes('Entidades'))comment='Inicializa los valores y colecciones necesarios para crear '+subjects[n]+'.';
  else if(row.file.startsWith('capaVisual'))comment='Inicializa los componentes existentes y las dependencias de la pantalla sin consultar la base de datos.';
  else comment={GymContext:row.body.startsWith('static')?'Deshabilita la creación automática de la base; su esquema se administra mediante scripts.':'Configura el contexto sin proxies ni carga diferida para exigir relaciones explícitas.',GymUnidadDeTrabajo:'Crea el contexto y los repositorios que comparten la persistencia de la operación.',Repositorio:'Recibe el contexto compartido para consultar y guardar la entidad.',Transaccion:'Recibe la transacción de Entity Framework que se confirmará o revertirá al finalizar.'}[n];
 } else if(n.endsWith('_Click')){
  const c=n.slice(0,-6);
  let action={btnVolver:'cierra el módulo y devuelve el control al dashboard',btnSalir:'inicia el cierre de la pantalla',btnCambiarCuenta:'cierra la sesión para volver al acceso',btnIngresar:'valida las credenciales y abre el dashboard del rol autenticado',nuevo:'limpia la selección y prepara el registro de nuevos datos',nuevaRutina:'limpia la selección para crear una plantilla de rutina',guardar:'valida los campos y envía el registro a la capa lógica',guardarRutina:'crea o actualiza la plantilla mediante RutinaLogica',crear:'registra la membresía y su primera cuota mediante MembresiaLogica',registrar:'valida los campos y registra la operación mediante la capa lógica',darDeBaja:'solicita la baja lógica del registro seleccionado y actualiza el listado',reactivar:'solicita la reactivación del registro seleccionado y actualiza el listado',habilitar:'habilita la membresía seleccionada mediante la capa lógica',deshabilitar:'solicita confirmación y deshabilita la membresía seleccionada',anular:'solicita confirmación y anula el pago seleccionado',reembolsar:'solicita confirmación y reembolsa el pago aprobado',generarCuota:'genera el siguiente período de cuota para la membresía seleccionada',calcularImc:'solicita el cálculo del IMC del socio y muestra el resultado',generar:'consulta los contadores y presenta el reporte básico',agregarEjercicio:'valida los parámetros y agrega el ejercicio a la rutina',asignar:'valida la selección y registra la asignación mediante la capa lógica',cambiar:'reemplaza el entrenador de la membresía mediante la capa lógica',consultar:'consulta el entrenador activo y actualiza el historial de asignaciones',lblPassword:'lleva el foco al campo de contraseña'}[c];
  if(c==='actualizar')action=row.body.includes('Modificar(')?'valida los campos y guarda las modificaciones mediante la capa lógica':'vuelve a consultar y mostrar los registros del módulo';
  if(!action&&c.startsWith('btn'))action='abre el módulo correspondiente dentro del dashboard';
  if(action)comment='Al hacer clic en '+c+', '+action+'.';
 } else if(n.endsWith('_Load'))comment='Al cargar la pantalla en ejecución, prepara sus datos iniciales sin realizar consultas desde el diseñador.';
 else if(n.endsWith('_SelectionChanged'))comment='Al cambiar la fila seleccionada, toma su identificador y actualiza los datos o acciones del registro.';
 else if(n==='txtPassword_TextChanged')comment='Al escribir la contraseña, actualiza la indicación existente y limpia su error de validación.';
 else if(n.endsWith('_TextChanged'))comment='Al escribir un criterio de búsqueda, filtra los registros que se muestran en la grilla.';
 else if(n==='membresia_SelectedIndexChanged')comment='Al elegir otra membresía, prepara su primera cuota pendiente si no se están cargando los controles.';
 else if(n.endsWith('_SelectedIndexChanged'))comment='Al cambiar el filtro de estado, actualiza los registros visibles en la grilla.';
 else if(n==='inicio_ValueChanged')comment='Al cambiar la fecha de inicio de un alta, ajusta el vencimiento del período mensual.';
 else if(n==='fecha_ValueChanged')comment='Al cambiar la fecha de consulta, recarga las asistencias de ese día en ejecución.';
 else if(n==='membresia_Format')comment='Al mostrar una opción de membresía, presenta el nombre del socio y su plan.';
 else if(n.endsWith('_KeyPress'))comment=n==='txtUsername_KeyPress'?'Al escribir el usuario, aplica la restricción de letras del formulario de acceso.':'Al escribir en el campo, permite números y un único separador decimal mediante la validación visual compartida.';
 else if(n.endsWith('_Validating'))comment='Al validar el campo de acceso, informa los datos faltantes o inválidos mediante ErrorProvider.';
 else if(n.endsWith('_FormClosed'))comment=n==='dashboard_FormClosed'?'Al cerrar el dashboard, vuelve al acceso si se solicitó cambiar de cuenta o cierra la aplicación.':'Al cerrar el módulo activo, restaura el contenido inicial del dashboard.';
 else if(n==='Cargar')comment='Consulta los registros del módulo y actualiza la grilla, informando los errores de carga.';
 else if(n==='Dispose')comment=row.owner==='Transaccion'?'Revierte la transacción si no fue confirmada y libera sus recursos.':'Libera el contexto y sus recursos al finalizar la unidad de trabajo.';
 else if(special[n])comment=special[n];
 else if(n==='Crear')comment='Valida y registra '+subject+' mediante la unidad de trabajo, conservando sus reglas de alta.';
 else if(n==='Modificar')comment='Valida y guarda los cambios de '+subject+' sobre el registro existente.';
 else if(n==='DarDeBaja')comment='Desactiva el registro de '+subject+' sin eliminar su historial.';
 else if(n==='Reactivar')comment='Recupera el estado activo del registro de '+subject+' según las validaciones de la operación.';
 else if(n==='ValidarDatos')comment='Comprueba los campos y rangos obligatorios de '+subject+' antes de persistirlos.';
 else if(n.startsWith('Listar')){
  let scope={ListarActivos:'activos',ListarActivas:'activas',ListarHabilitadas:'habilitadas',ListarParaGestion:'activos e inactivos para su gestión',ListarGenerales:'del catálogo reutilizable',ListarPendientes:'pendientes de pago',ListarPagadas:'pagadas',ListarAnuladas:'anuladas',ListarEstadoCuentas:'con su situación de deuda',ListarPorSocio:'del socio indicado',ListarPorFecha:'del día indicado',ListarPorFechaParaGestion:'del día indicado, incluyendo bajas',ListarPorMembresia:'de la membresía indicada',ListarPorEntrenador:'del entrenador indicado',ListarPorEntrenadorParaGestion:'del entrenador indicado, incluyendo bajas',ListarPorRutina:'de la rutina indicada, ordenados para entrenar',ListarPorEstado:'con el estado solicitado',ListarPorRol:'con el rol activo solicitado',ListarPorCuota:'asociados a la cuota indicada',ListarMetodosPagoActivos:'disponibles para registrar un cobro'}[n];
  if(scope)comment='Consulta '+subject+' '+scope+' para devolver los datos a la capa visual.';
 }else if(n.startsWith('ObtenerPor'))comment='Busca el registro de '+subject+' por '+({ObtenerPorId:'identificador',ObtenerPorDni:'DNI',ObtenerPorUsername:'nombre de usuario',ObtenerPorDescripcion:'descripción',ObtenerPorSocio:'socio'}[n]||'el criterio indicado')+' y devuelve los datos disponibles.';
 if(!comment||comment.includes('undefined'))missing.push(row.key);else descriptions[row.key]=comment;
}
if(missing.length)throw Error(missing.join('\n'));
fs.writeFileSync('.audit-comments.json',JSON.stringify(descriptions,null,2));
console.log('Comentarios preparados: '+Object.keys(descriptions).length);
