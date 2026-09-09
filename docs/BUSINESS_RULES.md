# Reglas de negocio

Última actualización: 9 de septiembre de 2026.

## Membresías y cuotas

- Una membresía pertenece a un socio, un plan y al usuario del sistema que la registra.
- Al crear una membresía se genera su primera cuota en la misma transacción.
- Las cuotas son mensuales: `FechaHasta = FechaDesde.AddMonths(1).AddDays(-1)`.
- `CuotaMembresia.Importe` conserva el precio histórico del plan.
- Una cuota sin pago debe tener `IdRegistroPago = NULL`.
- Los registros históricos se conservan y se usan bajas lógicas cuando corresponde.

## Pagos

- Solo los pagos aprobados se contabilizan.
- El importe no puede ser positivo por encima del importe de la cuota.
- `Pago` se vincula a las cuotas mediante `CuotaMembresia.IdRegistroPago`, según el DER aprobado.
- Un método de pago usa un detalle Mercado Pago o un detalle efectivo; la lógica rechaza métodos con ambos detalles.

## Roles y beneficios

La asignación de rutinas comprueba explícitamente que el entrenador esté activo, además de su rol.

- Administradores y recepcionistas activos pueden registrar membresías.
- Las asignaciones de entrenador y las rutinas requieren un usuario activo con rol Entrenador.
- Un cambio a un plan sin entrenador desactiva las asignaciones activas sin borrar la historia.
- Las rutinas que pueden asignarse se definen en el catálogo del plan, según la ampliación autorizada del 9 de septiembre.

## Datos físicos del socio

- Cuando se registra la altura, debe expresarse en metros con una parte decimal; por ejemplo, `1,80`. No se admite un valor entero.

## Flujo de rutinas

- El entrenador puede crear ejercicios mediante `EjercicioLogica`.
- El entrenador crea una rutina general mediante `RutinaLogica`; la plantilla no pertenece a un socio y puede reutilizarse.
- `RutinaAsignacionLogica` asigna una plantilla a una membresía activa cuyo plan tenga habilitada esa rutina en su catálogo. La misma plantilla puede asignarse a muchos socios.
- Los ejercicios se incorporan a la rutina mediante `RutinaEjercicioLogica`, con series, repeticiones, peso, descanso y orden.

## Correcciones implementadas — 8 de septiembre de 2026

- **FIX-02:** el importe de un pago aprobado debe ser mayor que cero y no superar el importe histórico de su cuota. Registro, actualización y cambio de estado comparten `ValidarImporteAprobado`. Aprobar un pendiente excesivo falla antes de modificar su estado. No se cambia la regla pendiente sobre completar pagos parciales.
- **FIX-03:** la asistencia compara la vigencia de la cuota por día calendario, incluyendo todo su último día. Para el día solicitado se consulta `FechaDesde < siguienteDia` y `FechaHasta >= inicioDia`, con límites calculados fuera de la consulta para mantener compatibilidad con EF6. Se conserva la hora real registrada y las demás validaciones de membresía/cuota pagada.
- El recálculo de deuda posterior a cambios de pagos/cuotas considera los estados recién guardados dentro de la misma transacción.

FIX-01 y FIX-04 continúan pendientes de confirmación. No se implementan los demás fixes ni las funciones futuras de la revisión sin autorización. La ampliación opcional de foto y sexo fue autorizada posteriormente en FIX_LAYOUT_CODEX.md.

## Foto y sexo — 9 de septiembre de 2026

- Socios y usuarios pueden guardar una foto opcional de hasta 2 MB (2.097.152 bytes), en PNG, JPEG o BMP. La lógica comprueba tamaño y firma del formato; la interfaz además decodifica la imagen antes de aceptarla.
- Sexo admite únicamente M, F o NULL y se usa para resolver el avatar. No se asigna un sexo por defecto; Nuevo deja el combo sin selección.
- La foto guardada tiene prioridad sobre el avatar por sexo. Sin foto ni sexo se busca avatarGenerico; si faltan recursos se muestra el fondo gris claro, sin inventar imágenes.
- Quitar foto conserva el sexo elegido y vuelve al avatar disponible. Guardar o actualizar persiste Foto y Sexo.
- Los listados de gestión de socios y usuarios no descargan Foto; la selección recupera el registro completo mediante ObtenerPorId.
- Las validaciones de tamaño, formato y sexo y la persistencia de ambas entidades fueron comprobadas en una base separada, sin conservar registros de prueba.

## Rutinas disponibles por plan — 9 de septiembre de 2026

- El administrador elige explícitamente el catálogo de cada plan. Una rutina puede estar habilitada en Normal, Premium o ambos, sin duplicar la plantilla.
- Todo plan tiene al menos una rutina disponible; su rutina base debe pertenecer a esa selección. Las rutinas elegidas y sus entrenadores deben estar activos al guardar.
- En la interfaz, primero se marcan las rutinas disponibles y luego se elige la base entre ellas. Desmarcar la base limpia esa elección y exige seleccionar otra; Nuevo no marca rutinas automáticamente. La validación de lógica sigue impidiendo guardar una base fuera del catálogo.
- Las asignaciones se autorizan por PlanRutina, no por el nombre del plan ni por IncluyeRutinaPersonal. Esto permite asignar a socios de Normal las rutinas seleccionadas para Normal. El indicador de rutina personalizada se conserva como descripción del beneficio, no como permiso del catálogo.
- Premium puede tener todas las rutinas seleccionadas, pero no incorpora automáticamente las futuras. Las nuevas se habilitan desde Gestión de planes.
- Quitar una rutina del catálogo finaliza sus asignaciones activas a membresías del plan, con FechaFin, sin borrar historial. Los cambios y los vínculos se guardan en una misma operación atómica.
- Cambiar de plan conserva las asignaciones de rutinas que el nuevo plan mantiene disponibles y activas; finaliza las restantes. Las asignaciones de entrenador siguen dependiendo de IncluyeEntrenador.
- La migración inicial incorpora las rutinas base y las asignaciones activas existentes, sin eliminar datos ni habilitar automáticamente todo el catálogo.
