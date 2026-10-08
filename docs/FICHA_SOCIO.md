# Ficha 360° del socio

Última actualización: 8 de octubre de 2026.

## Acceso y navegación

FichaSocioFormulario es una consulta compartida por Administrador y Recepcionista. EstadoSociosControl, gestión de socios, pagos y membresías ofrecen Ver ficha completa. Los paneles conectan los eventos a ControladorNavegacion; no hay otro controlador ni sesión. FichaSocioLogica y las exportaciones comprueban usuario/rol activo en persistencia; Entrenador y usuario no autenticado no están autorizados.

Registrar pago y Ver membresía abren los módulos existentes con el socio preseleccionado. Ver ficha completa desde esos módulos vuelve a consultar los datos, incluido un pago recién registrado. El controlador conserva un solo hijo MDI; el botón general Volver sigue llevando a Inicio. No se agregó una pila de navegación. Sin membresía, Registrar pago y Ver membresía quedan deshabilitados. Si una selección preexistente ya no está disponible al entrar a Pagos, no se elige silenciosamente otro socio.

## Datos y lectura

FichaSocioLogica.Obtener devuelve FichaSocioResultado: datos personales, fecha de alta/antigüedad en días, estado, membresía/cobertura, entrenador/asignación, rutina/cantidad de ejercicios activos, resumen y pagos. Los repositorios ConsultarSoloLectura usan AsNoTracking, relaciones explícitas y cantidad fija de consultas, sin N+1 ni escrituras. No hubo cambios de SQL, modelo EF ni reglas de negocio.

La membresía es la seleccionada por EstadoSociosLogica.ObtenerSocioSoloLectura. Estado efectivo del socio, clasificación, deuda, pendientes, vencidas y próximo vencimiento vienen de ese DTO; no se vuelve a calcular la deuda ni se aplican bajas. El umbral viene de ConfiguracionSistema. Cuotas hasta usa la cobertura real de cuotas no anuladas: FechaVencimiento es una columna heredada, no un vencimiento independiente de la membresía.

Entrenador: última asignación marcada activa de esa membresía; informa si el usuario está inactivo. Sin asignación activa: Sin asignar. Rutina: la relacionada, indicando su inactividad cuando corresponda. Ver rutina reutiliza RutinaSemanalFormulario con ID explícito y RutinaEjercicioLogica.ListarPorRutina, de solo lectura; evita la actualización de estados que realiza el flujo habitual de rutina semanal.

Foto: MostrarFotoRuta y avatar existente, únicamente al cargar la ficha. No se copia ni almacena otra imagen; la copia visual se libera al disponer el formulario. FechaAlta desconocida: Sin información; no se sustituye por nacimiento/inicio de membresía.

## Finanzas e historial

- Total y cantidad: todos los pagos actualmente Aprobados asociados a cuotas Pagadas de todas las membresías del socio, deduplicados por IdRegistroPago. No se incluyen anulados, reembolsados ni cuotas pendientes como cobros.
- Cuotas pagadas: cantidad histórica de cuotas con EstadoPago Pagada; puede diferir de la cantidad de pagos si uno cubre varias cuotas.
- Deuda, pendientes, vencidas y próximo vencimiento: membresía actual según EstadoSociosLogica. Último pago: más reciente del historial completo aprobado.
- Grilla inicial: últimos 20 pagos. Ver historial completo muestra todos, ordenados por fecha e ID descendentes. Columnas Fecha, Período, Método, Importe y Registrado por.

El historial y los totales usan ReportesPagosServicio.ConsultarHistorial, la misma proyección que utiliza el PDF. Responsable: Pago.UsuarioRegistro y rol relacionado; NULL muestra Sin información. Nombre/rol del cobrador son los datos actuales; AuditoriaOperacion conserva el nombre/rol del momento de la exportación.

El formulario conserva una lectura hasta reabrirse. Exportar consulta nuevamente los registros persistidos; ante cambios externos simultáneos debe reabrirse la ficha para comparar el mismo estado. No hay transacción de snapshot entre consultas de lectura.

## PDF y auditoría

Se reutilizan GenerarComprobante/GenerarHistorial de ReportesPagosServicio, PDFsharp/MigraDoc GDI 6.2.4 y el logo de recursos. No existe otro servicio de pagos ni temporales del exportador. SaveFileDialog usa nombres sanitizados. Exportar pago seleccionado exige selección aprobada y cuota pagada; el servicio vuelve a validar. Historial sin pagos crea PDF válido de cero pagos y Sin pagos registrados. Ver REPORTES_Y_COMPROBANTES.md.

EXPORTAR_COMPROBANTE_PAGO y EXPORTAR_HISTORIAL_PAGOS se registran con la sesión real después de guardar correctamente. Cancelar no llama al servicio. Render en memoria, auditoría sin commit, escritura y commit posterior: fallo de render/escritura/auditoría no confirma éxito. Disco y SQL no comparten transacción distribuida; un fallo de commit posterior a escribir puede dejar archivo sin evento confirmado.

## Verificaciones

Base aislada SysGymFichaPrueba20261008: comprobantes Efectivo/Mercado Pago y registradores Admin/Recepcionista/NULL; historiales de 0, 1, 23 y 24 pagos; cantidades, totales, orden y responsables pantalla/DTO/PDF. Denegación a Entrenador/sin sesión, lectura sin escrituras, fallos de archivo/auditoría y cancelación de SaveFileDialog. Navegación MDI con cambio real de sesión Admin → Recepcionista, rutina reutilizada, cobro y reapertura con responsable actualizado. Los fixtures no son datos operativos del gimnasio.

Evidencias y scripts fuera del repositorio: directorio temporal sysgym-ficha-verificacion-20261008. La base de pruebas se elimina al terminar. Los datos reales no se modifican.

Verificación final de Ficha 360°: ocho formularios/controles abrieron, permitieron selección, guardado y reapertura en el Designer real VS2026, sin pantalla roja. Los nueve PDFs de prueba se abrieron y renderizaron completos (once páginas) con el lector nativo de Windows; logo/tablas legibles. Cancelación real de ambos diálogos: cero eventos. Selección real con mouse en Socios y responsables completos en la grilla verificados. Rebuild Debug y Release: 0 errores / 0 advertencias; git diff --check correcto. Sin commit.

El evento real Registrar pago se probó en el flujo Recepcionista → Ficha → Pagos → Ficha. Pagos conserva la membresía cobrada después de guardar, evitando que su preparación de un nuevo pago cambie el socio del retorno. El nuevo pago y su registrador Recepcionista quedan visibles. Socio sin membresía y cancelación real de ambos diálogos también se verificaron.
