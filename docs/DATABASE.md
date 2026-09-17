# Base de datos

## Compatibilidad con SQL Server 2008 — 11 de septiembre de 2026

Una membresía puede tener cero o una rutina mediante `Membresia.IdRutina INT NULL`. La FK apunta a `Rutina`, que puede reutilizarse en varias membresías; los ejercicios permanecen en `RutinaEjercicio`.

Los scripts son compatibles con SQL Server 2008. En los bloques `CATCH` se usa `RAISERROR` en lugar de `THROW`, ya que `THROW` fue incorporado en SQL Server 2012. La transacción se revierte antes de volver a informar el error. El esquema conserva `DATETIME2`, `VARBINARY(MAX)`, índices filtrados y las demás características disponibles en SQL Server 2008.

Última actualización: 16 de septiembre de 2026.

El motor es SQL Server y el esquema fuente está en `capaDatos/Database/SysGymDB.sql`. Ese único script crea `SysGymDB`, tablas, claves, índices, restricciones, usuarios iniciales, métodos de pago y el catálogo inicial de ejercicios y rutinas.

Las entidades principales son Rol, UsuarioSistema, Socio, Plan, Membresia, MembresiaEntrenador, CuotaMembresia, Pago, MetodoPago, MercadoPago, PagoEfectivo, Divisa, Rutina, RutinaEjercicio y Ejercicio.

`RutinaEjercicio.DiaSemana` es un `INT NULL` que ubica el ejercicio en la semana: 1 lunes a 5 viernes, con `CK_RutinaEjercicio_DiaSemana` verificando el rango. Nulo significa ejercicio sin dia asignado. `Orden` pasa a ser el orden dentro del dia y no dentro de toda la rutina. Asi una misma plantilla contiene el entrenamiento completo del socio de lunes a viernes. El DDL inicial define la columna y su restriccion.

`Membresia` contiene `FechaInicio`, `FechaVencimiento`, `IdPlan`, `IdSocio`, `IdUsuarioSistema` e `IdRutina` nullable. `CuotaMembresia` contiene `FechaDesde`, `FechaHasta`, `Importe`, `EstadoPago`, `IdMembresia` e `IdRegistroPago` nullable. `Pago` usa `IdRegistroPago` como clave y `IdMetodoPago` como FK.

`MetodoPago.IdNroPagoMP` y `MetodoPago.IdPagoEfectivo` son nullable y apuntan a sus detalles específicos. Los importes usan `DECIMAL(18,2)`.

`Rutina` representa una plantilla reutilizable. La FK opcional `Membresia.IdRutina` implementa la relación Rutina 1:N Membresía: cada membresía apunta a cero o una rutina y una rutina puede ser compartida. `RutinaEjercicio` continúa relacionando Rutina con Ejercicio.

El DDL principal crea una base nueva y es la única fuente de esquema inicial. No se distribuye una migración para bases existentes; no ejecutar el DDL completo sobre una base con datos. La cadena `GymContext` de `App.config` apunta actualmente a `.SQLEXPRESS`; debe cambiarse si la instancia de SQL Server es diferente.

## Foto y sexo — 9 de septiembre de 2026

Socio y UsuarioSistema incorporan `Foto varbinary(max) NULL` y `Sexo char(1) NULL`. La foto se almacena en SQL Server, no en carpetas por persona dentro del proyecto. Sexo admite M, F o NULL mediante validación de negocio. Los registros anteriores conservan ambos campos nulos.

El DDL inicial contiene estas columnas opcionales; no se mantiene un script de migración para esquemas previos.

La validación de estas cuatro columnas se realizó en la base separada SysGym_Verificacion_20260908. Se comprobó alta, lectura y eliminación de fotos por la lógica para ambas entidades, revirtiendo los registros temporales.

## Inserciones manuales de prueba — 16 de septiembre de 2026

Al final de `SysGymDB.sql`, desde `/* Inserciones manuales de prueba. */`, hay INSERT INTO ... VALUES con cada fila explícita, sin ciclos, procedimientos ni funciones propias. Los bloques se identifican con comentarios `/* Inserciones de NombreTabla */`.

- Agregan 20 usuarios, socios, rutinas, detalles de rutina, membresías, métodos de pago, pagos y cuotas; solo dos planes nuevos: Normal (15.000) y Premium (25.000).
- Incluyen 10 referencias ficticias de Mercado Pago, asignaciones de entrenador y una rutina directa en cada una de las 20 membresías de ejemplo. Reutilizan los tres roles, la divisa, el detalle de efectivo y los 24 ejercicios del catálogo inicial; no duplican catálogos para alcanzar veinte filas.
- Usernames variados basados en nombres ficticios: `lucia.garcia` (administradora), `mateo_lopez` (recepcionista), `benja.fernandez` (entrenador), entre otros. La contraseña común sigue siendo `Prueba123!`, almacenada con Argon2id. `matias.navarro` está de baja. Son credenciales públicas de prueba, no de producción.
- Los 40 DNI nuevos son distintos, no consecutivos y están entre 30.000.000 y 50.000.000. Son valores ficticios, no identidades verificadas. Se sincronizaron todas las referencias SQL. Rutinas, teléfonos y pagos tienen nombres y descripciones variados, sin etiquetas numeradas de prueba en los datos visibles.
- Cuotas y membresías del 1 al 30 de septiembre de 2026. Hay diez pagos aprobados, cinco pendientes, tres rechazados y dos anulados. Socios 19 y 20 y sus membresías están de baja.
- Ejecutar una sola vez al crear la base limpia, con los nombres y DNI indicados en el comentario libres y sin planes Normal/Premium previos. Si ya existen otros planes, no se borran ni renombran. No volver a ejecutar el archivo completo sobre una base existente.
- Una transacción evita cargas parciales; errores se propagan con THROW. Las opciones SET requeridas por el índice filtrado quedan explícitas. Se verificó la ejecución en la base separada y se revirtió la transacción; no se conservaron registros ni se cargó la base comercial. La repetición no es idempotente: requiere un destino sin esos datos de prueba.

## Relación de rutinas — 16 de septiembre de 2026

`Membresia.IdRutina` es nullable y tiene una FK a `Rutina`; una misma Rutina puede estar referenciada por muchas membresías. `RutinaEjercicio` conserva su función asociativa con `Ejercicio`.
