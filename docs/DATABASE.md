# Base de datos

## Compatibilidad con SQL Server 2008 — 11 de septiembre de 2026

La creación de rutinas personalizadas no requiere una tabla nueva: utiliza `Rutina` y `RutinaAsignacion`. La aplicación crea ambos registros dentro de una transacción de Entity Framework.

Los scripts son compatibles con SQL Server 2008. En los bloques `CATCH` se usa `RAISERROR` en lugar de `THROW`, ya que `THROW` fue incorporado en SQL Server 2012. La transacción se revierte antes de volver a informar el error. El esquema conserva `DATETIME2`, `VARBINARY(MAX)`, índices filtrados y las demás características disponibles en SQL Server 2008.

Última actualización: 11 de septiembre de 2026.

El motor es SQL Server y el esquema fuente está en `capaDatos/Database/SysGymDB.sql`. Ese único script crea `SysGymDB`, tablas, claves, índices, restricciones, usuarios iniciales, métodos de pago y el catálogo inicial de ejercicios y rutinas.

Las entidades principales son Rol, UsuarioSistema, Socio, Plan, Membresia, MembresiaEntrenador, CuotaMembresia, Pago, MetodoPago, MercadoPago, PagoEfectivo, Divisa, Asistencia, Rutina, RutinaEjercicio, RutinaAsignacion y Ejercicio.

`RutinaEjercicio.DiaSemana` es un `INT NULL` que ubica el ejercicio en la semana: 1 lunes a 5 viernes, con `CK_RutinaEjercicio_DiaSemana` verificando el rango. Nulo significa ejercicio sin dia asignado. `Orden` pasa a ser el orden dentro del dia y no dentro de toda la rutina. Asi una misma plantilla contiene el entrenamiento completo del socio de lunes a viernes. La migracion agrega la columna y reparte los detalles existentes con `((Orden - 1) % 5) + 1`.

`Membresia` contiene `FechaInicio`, `FechaVencimiento`, `IdPlan`, `IdSocio` e `IdUsuarioSistema`. `CuotaMembresia` contiene `FechaDesde`, `FechaHasta`, `Importe`, `EstadoPago`, `IdMembresia` e `IdRegistroPago` nullable. `Pago` usa `IdRegistroPago` como clave y `IdMetodoPago` como FK.

`MetodoPago.IdNroPagoMP` y `MetodoPago.IdPagoEfectivo` son nullable y apuntan a sus detalles específicos. Los importes usan `DECIMAL(18,2)`.

`Rutina` representa una plantilla general creada por un entrenador. `RutinaAsignacion` relaciona la plantilla con una membresía concreta, permitiendo que una misma rutina sea utilizada por muchos socios sin duplicar sus ejercicios.

El DDL principal está pensado para una base nueva. Si ya existe una `SysGymDB` creada con un modelo anterior, usar la migración indicada abajo en lugar de ejecutar nuevamente el DDL completo. La cadena `GymContext` de `App.config` apunta actualmente a `.SQLEXPRESS`; debe cambiarse si la instancia de SQL Server es diferente.

Si `SysGymDB` ya existía con el modelo anterior, ejecutar primero `capaDatos/Database/SysGymDB_MigracionExistente.sql` y luego la sección de catálogo de `SysGymDB.sql`. La migración conserva los datos y vuelve nullable la columna histórica `Rutina.IdSocio`.

## Foto y sexo — 9 de septiembre de 2026

Socio y UsuarioSistema incorporan `Foto varbinary(max) NULL` y `Sexo char(1) NULL`. La foto se almacena en SQL Server, no en carpetas por persona dentro del proyecto. Sexo admite M, F o NULL mediante validación de negocio. Los registros anteriores conservan ambos campos nulos.

El script base incluye las columnas. La migración existente las agrega con comprobaciones COL_LENGTH para no duplicarlas y conserva los registros. Antes de usar esta versión con una base existente, respaldar y aplicar `SysGymDB_MigracionExistente.sql`; revisar también sus cambios históricos si la base todavía no los tiene. No ejecutar el script de creación completo sobre una base con datos.

La migración de estas cuatro columnas se verificó en la base separada SysGym_Verificacion_20260908. Se comprobó alta, lectura y eliminación de fotos por la lógica para ambas entidades, revirtiendo los registros temporales. No se aplicó esta migración sobre la base comercial SysGymDB.

## Inserciones manuales de prueba — 9 de septiembre de 2026

Al final de `SysGymDB.sql`, desde `/* Inserciones manuales de prueba. */`, hay INSERT INTO ... VALUES con cada fila explícita, sin ciclos, procedimientos ni funciones propias. Los bloques se identifican con comentarios `/* Inserciones de NombreTabla */`.

- Agregan 20 usuarios, socios, rutinas, detalles de rutina, membresías, métodos de pago, pagos, cuotas y asistencias; solo dos planes nuevos: Normal (15.000) y Premium (25.000).
- Incluyen 10 referencias ficticias de Mercado Pago, 10 asignaciones de entrenador y 10 de rutina, estas últimas solo para Premium. Reutilizan los tres roles, la divisa, el detalle de efectivo y los 24 ejercicios del catálogo inicial; no duplican catálogos para alcanzar veinte filas.
- Usernames variados basados en nombres ficticios: `lucia.garcia` (administradora), `mateo_lopez` (recepcionista), `benja.fernandez` (entrenador), entre otros. La contraseña común sigue siendo `Prueba123!`, almacenada con Argon2id. `matias.navarro` está de baja. Son credenciales públicas de prueba, no de producción.
- Los 40 DNI nuevos son distintos, no consecutivos y están entre 30.000.000 y 50.000.000. Son valores ficticios, no identidades verificadas. Se sincronizaron todas las referencias SQL. Rutinas, teléfonos, pagos y asistencias tienen nombres y descripciones variados, sin etiquetas numeradas de prueba en los datos visibles.
- Cuotas y membresías del 1 al 30 de septiembre de 2026. Las 20 asistencias corresponden al 8 y 9 de septiembre, únicamente a los diez socios con cuota pagada. Hay diez pagos aprobados, cinco pendientes, tres rechazados y dos anulados. Socios 19 y 20 y sus membresías están de baja.
- Ejecutar una sola vez, con los nombres y DNI indicados en el comentario libres y sin planes Normal/Premium previos. Si ya existen otros planes, no se borran ni renombran. No volver a ejecutar el archivo completo sobre una base existente: seleccionar solo este bloque después de aplicar la migración y el catálogo inicial.
- Una transacción evita cargas parciales; errores se propagan con THROW. Las opciones SET requeridas por el índice filtrado quedan explícitas. Se verificó la ejecución en la base separada y se revirtió la transacción; no se conservaron registros ni se cargó la base comercial. La repetición no es idempotente: requiere un destino sin esos datos de prueba.

## PlanRutina — implementación autorizada, 9 de septiembre de 2026

El usuario autorizó el catálogo de rutinas por plan y su edición visual. Se agrega PlanRutina con clave primaria compuesta (IdPlan, IdRutina), ambas FK obligatorias y sin borrado en cascada. Una misma rutina puede pertenecer a varios planes. Plan.IdRutina permanece como rutina base por compatibilidad; RutinaAsignacion continúa representando la asignación concreta a una membresía.

Relación actual: Plan (1) → (N) PlanRutina (N) ← (1) Rutina. Esta ampliación reemplaza la limitación del DER original en docs/der.jpeg; esa imagen histórica no fue redibujada.

SysGymDB.sql crea la tabla y contiene 29 INSERT de vínculos explícitos en el bloque manual: tres para Normal y las 26 rutinas iniciales/manuales para Premium. Las rutinas nuevas posteriores se habilitan manualmente. No se convierten los INSERT manuales en ciclos.

SysGymDB_MigracionExistente.sql crea PlanRutina únicamente si falta y carga la unión de rutinas base y asignaciones activas de cada plan, sin duplicados. Volver a ejecutar la migración no repone vínculos retirados posteriormente. Aplicar la migración sobre una copia respaldada antes de usar esta versión; la base comercial no fue migrada en esta tarea.

La migración y el mapeo se verificaron en SysGym_Verificacion_20260908. Las pruebas de altas, edición, filtros y asignaciones temporales se revirtieron.
