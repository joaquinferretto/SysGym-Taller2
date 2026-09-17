# Reglas de negocio

## Identidad y habilitacion de entrenador - 11 de septiembre de 2026

- `UsuarioSistema` y `Socio` son entidades distintas. Pueden compartir nombre o apellido en los datos de prueba, pero se distinguen por su rol, clave e identidad (DNI).
- La asignacion se realiza sobre una `Membresia` seleccionada por `IdMembresia`; el entrenador se selecciona por `IdUsuarioSistema`. La interfaz muestra nombre completo y DNI para evitar confusiones.
- El cambio de plan desde Gestión de membresías conserva la historia de la membresía. Todos los planes incluyen entrenador; la asignación del entrenador se gestiona por membresía en `MembresiaEntrenador`.

## Rutinas reutilizables — 16 de septiembre de 2026

Una rutina puede reutilizarse en varias membresías. Cada membresía tiene cero o una rutina, determinada únicamente por `Membresia.IdRutina`; ningún atributo del plan habilita o restringe esta relación. Una rutina personalizada se crea desde la membresía seleccionada y queda vinculada a ella. Sus ejercicios se gestionan mediante `RutinaEjercicioLogica`, indicando un día entre lunes y viernes.

Última actualización: 16 de septiembre de 2026.

## Validaciones de altas y edición — 17 de septiembre de 2026

- Socios y usuarios validan en capa lógica nombres y apellidos compuestos únicamente con letras, espacios, apóstrofes o guiones; el DNI conserva el formato numérico sin puntos ni letras y mantiene su unicidad.
- La edad se calcula considerando el cumpleaños. Un socio debe tener al menos 13 años y un usuario del sistema al menos 18; las fechas futuras se rechazan.
- Peso, altura, salario, precio e importes se validan como valores numéricos con sus rangos actuales. La altura del socio conserva la regla existente de expresarse en metros con parte decimal.
- Una relación `RutinaEjercicio` nueva o modificada requiere rutina, ejercicio, día, orden, series y repeticiones válidos; peso y descanso no pueden ser negativos. Quitar la relación es una baja lógica y no elimina el ejercicio del catálogo.

## Membresías y cuotas

- Una membresía pertenece a un socio, un plan y al usuario del sistema que la registra.
- Al crear una membresía se genera su primera cuota en la misma transacción.
- Las cuotas son mensuales: `FechaHasta = FechaDesde.AddMonths(1).AddDays(-1)`.
- `CuotaMembresia.Importe` conserva el precio histórico del plan.
- Una cuota sin pago debe tener `IdRegistroPago = NULL`.
- Los registros históricos se conservan y se usan bajas lógicas cuando corresponde.
- Cada socio conserva la misma fila de `Membresia` al volver al gimnasio. Si existe cualquier membresía histórica, no se crea otra: se reactiva la existente mediante su `IdMembresia`.
- `MembresiaLogica` es la fuente única de la regla de deuda: dos o más cuotas con `EstadoPago = Pendiente` y `FechaHasta < hoy` dan de baja lógicamente la membresía y sincronizan el estado del socio. Cero o una cuota vencida no la dan de baja; las cuotas anuladas no cuentan como deuda pendiente.
- La deuda se evalúa al consultar membresías/cuotas, rutinas y asignaciones vigentes, generar cuotas y registrar o actualizar pagos. Regularizar no reactiva automáticamente: Reactivar permite el alta lógica de la misma membresía solo si quedan menos de dos cuotas vencidas pendientes.
- Las bajas y reactivaciones no eliminan cuotas, pagos, asignaciones de entrenador ni rutina. El estado del socio se actualiza desde la lógica de membresías, sin baja manual independiente.

## Pagos

- Solo los pagos aprobados se contabilizan.
- El importe no puede ser positivo por encima del importe de la cuota.
- `Pago` se vincula a las cuotas mediante `CuotaMembresia.IdRegistroPago`, según el DER aprobado.
- Un método de pago usa un detalle Mercado Pago o un detalle efectivo; la lógica rechaza métodos con ambos detalles.

## Roles y planes

- Administradores y recepcionistas activos pueden registrar membresías.
- Cada membresía comienza con el usuario reservado Entrenador General; luego puede asignarse un entrenador activo con rol Entrenador.
- Los planes no tienen indicadores de inclusión de entrenador o rutina ni catálogo de rutinas. La relación con la rutina es independiente del plan.

## Datos físicos del socio

- Cuando se registra la altura, debe expresarse en metros con una parte decimal; por ejemplo, `1,80`. No se admite un valor entero.

## Flujo de rutinas

- El entrenador puede crear ejercicios mediante `EjercicioLogica`.
- El entrenador crea una rutina general mediante `RutinaLogica`; la plantilla no pertenece a un socio y puede reutilizarse.
- `Membresia.IdRutina` es la fuente de verdad de la rutina asignada. La relación permite reutilizar una rutina en varias membresías, con un máximo de una por membresía.
- Asignar una rutina existente, crear una personalizada o cambiarla solo establece o reemplaza `Membresia.IdRutina`; la rutina anterior y sus ejercicios se conservan.
- Los ejercicios se incorporan a la rutina mediante `RutinaEjercicioLogica`, con series, repeticiones, peso, descanso y orden.

## Correcciones implementadas — 8 de septiembre de 2026

- **FIX-02:** el importe de un pago aprobado debe ser mayor que cero y no superar el importe histórico de su cuota. Registro, actualización y cambio de estado comparten `ValidarImporteAprobado`. Aprobar un pendiente excesivo falla antes de modificar su estado. No se cambia la regla pendiente sobre completar pagos parciales.
- El recálculo de deuda posterior a cambios de pagos/cuotas considera los estados recién guardados dentro de la misma transacción.

## Foto y sexo — 9 de septiembre de 2026

- Socios y usuarios pueden guardar una foto opcional de hasta 2 MB (2.097.152 bytes), en PNG, JPEG o BMP. La lógica comprueba tamaño y firma del formato; la interfaz además decodifica la imagen antes de aceptarla.
- Sexo admite únicamente M, F o NULL y se usa para resolver el avatar. No se asigna un sexo por defecto; Nuevo deja el combo sin selección.
- La foto guardada tiene prioridad sobre el avatar por sexo. Sin foto ni sexo se busca avatarGenerico; si faltan recursos se muestra el fondo gris claro, sin inventar imágenes.
- Quitar foto conserva el sexo elegido y vuelve al avatar disponible. Guardar o actualizar persiste Foto y Sexo.
- Los listados de gestión de socios y usuarios no descargan Foto; la selección recupera el registro completo mediante ObtenerPorId.
- Las validaciones de tamaño, formato y sexo y la persistencia de ambas entidades fueron comprobadas en una base separada, sin conservar registros de prueba.

## Rutina semanal del socio

La rutina de un socio se organiza de lunes a viernes. El socio conserva una unica plantilla asignada a su membresia y esa plantilla contiene la semana completa: cada `RutinaEjercicio` indica su dia en `DiaSemana`, con 1 para lunes y 5 para viernes, y su posicion dentro de ese dia en `Orden`.

`DiaSemana` admite nulo para los ejercicios que todavia no tienen dia asignado; esos ejercicios no aparecen en la consulta semanal y se informan en la barra de estado. `ValidacionesGimnasio.ValidarDiaRutina` rechaza cualquier valor fuera del rango y `ValidacionesGimnasio.NombreDia` traduce el numero al nombre que se muestra.

`RutinaEjercicioLogica.ListarSemanaPorSocio` resuelve la membresia activa del socio y consulta la rutina indicada por `Membresia.IdRutina`. Si el socio no tiene membresia activa o no tiene rutina asignada devuelve una lista vacia, sin lanzar excepcion.
