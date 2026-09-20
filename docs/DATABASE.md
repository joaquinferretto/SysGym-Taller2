# Base de datos

## Estado vigente de imágenes — 20 de septiembre de 2026

Actualización posterior a la ejecución del usuario: se confirmó en SQL Server que dbo.EjercicioImagen existe y contiene 0 filas; tipos/longitud coinciden y FK_EjercicioImagen_Ejercicio usa NO ACTION. Conteos preservados: 45 ejercicios, 20 socios, 23 usuarios, 20 membresías, 20 cuotas, 20 pagos y 26 rutinas. Login/catálogo/selección de ejercicio pasaron sin errores SQL en la prueba de lectura. El bloqueo de creación mencionado debajo quedó resuelto; no se agregaron imágenes de prueba.

Ejercicio admite cero, una o varias imágenes mediante EjercicioImagen: IdEjercicioImagen INT IDENTITY PK, IdEjercicio INT FK sin cascada, RutaRelativa NVARCHAR(260) NOT NULL y Orden INT positivo. RutaRelativa es única; IdEjercicio NO es único. Socio y UsuarioSistema conservan FotoRuta NVARCHAR(260) NULL individual. No agregar FotoRuta a Ejercicio.

SysGymDB.sql ya contiene este modelo. Para bases existentes se distribuye ahora `capaDatos/Database/MigrarEjercicioImagen.sql`, aditivo/transaccional: crea solo la tabla faltante y no altera una tabla existente. No ejecutar el DDL completo en una base con datos. **Aplicación local pendiente:** conexión bloqueada por SSL/SSPI el 20/09/2026, antes de ejecutar comandos. La afirmación histórica inferior de que EjercicioImagen ya se había creado no describe el estado verificado: la última consulta exitosa la encontró ausente. Reconsultar esquema y conteos al recuperar acceso. No se eliminaron tablas ni datos.

## Sincronización de membresías y fotos de socios — 19 de septiembre de 2026

La entidad `Membresia` define `IdRutina` como `int?` y la relación opcional hacia `Rutina.IdRutina`; la entidad `Socio` define `FotoRuta` como `string` nullable con `[StringLength(260)]`. La base local `SysGymDB` carecía de ambas columnas. Se agregaron únicamente `Membresia.IdRutina INT NULL` y `Socio.FotoRuta NVARCHAR(260) NULL`, y se creó `FK_Membresia_Rutina` hacia `Rutina(IdRutina)` con `NO ACTION` en borrado. No se modificaron registros, claves, tablas históricas ni columnas legacy.

Antes y después se verificaron estas cantidades: Socio 20, Membresia 20, UsuarioSistema 23, CuotaMembresia 20, Pago 20 y Rutina 26. Las 20 membresías quedaron con `IdRutina = NULL` y los 20 socios con `FotoRuta = NULL`. La auditoría no destructiva de los 16 `DbSet` encontró como diferencia adicional la tabla completa `EjercicioImagen` ausente en la base local; no se creó porque no es una adición nullable aislada y requiere una decisión separada. `Foto varbinary(max)`, `Plan.IdRutina`, `PlanRutina` y `RutinaAsignacion` se conservaron sin cambios.

`SysGymDB.sql` ya contenía ambas columnas y `FK_Membresia_Rutina`, por lo que no se modificó.

## Sincronización aditiva de UsuarioSistema — 19 de septiembre de 2026

Se comparó `UsuarioSistema.cs`, el mapeo EF6 y `SysGymDB`. La entidad define `FotoRuta` como `string` nullable con `[StringLength(260)]`, por lo que el DDL correcto es `FotoRuta NVARCHAR(260) NULL`. `capaDatos/Database/SysGymDB.sql` ya contenía esa definición para bases nuevas y no necesitó cambios adicionales.

La base local `SysGymDB` tenía ausente únicamente esa propiedad actual y recibió solo `ALTER TABLE dbo.UsuarioSistema ADD FotoRuta NVARCHAR(260) NULL`. No se recreó la tabla, no se eliminaron columnas y no se modificaron usuarios ni claves. Antes y después se verificaron 23 registros, IDs del 1 al 23 y suma de IDs 276; las 23 rutas quedaron NULL. Todas las demás propiedades escalares actuales de la entidad ya estaban presentes y compatibles. La columna histórica extra `Foto varbinary(max)` no pertenece al modelo EF6 y se conserva sin uso.

La autenticación real con `SanMartin` y `cordillera2026` respondió correctamente como usuario Administrador. Debug compiló sin errores; permanecen únicamente los warnings CS0649 preexistentes de otros formularios.

## Compatibilidad con SQL Server 2008 — 11 de septiembre de 2026

Una membresía puede tener cero o una rutina mediante `Membresia.IdRutina INT NULL`. La FK apunta a `Rutina`, que puede reutilizarse en varias membresías; los ejercicios permanecen en `RutinaEjercicio`.

Los scripts son compatibles con SQL Server 2008. En los bloques `CATCH` se usa `RAISERROR` en lugar de `THROW`, ya que `THROW` fue incorporado en SQL Server 2012. La transacción se revierte antes de volver a informar el error. El esquema conserva `DATETIME2`, `VARBINARY(MAX)`, índices filtrados y las demás características disponibles en SQL Server 2008.

Última actualización: 17 de septiembre de 2026.

El motor es SQL Server y el esquema fuente está en `capaDatos/Database/SysGymDB.sql`. Ese único script crea `SysGymDB`, tablas, claves, índices, restricciones, usuarios iniciales, métodos de pago y el catálogo inicial de ejercicios y rutinas.

Las entidades principales son Rol, UsuarioSistema, Socio, Plan, Membresia, MembresiaEntrenador, CuotaMembresia, Pago, MetodoPago, MercadoPago, PagoEfectivo, Divisa, Rutina, RutinaEjercicio y Ejercicio.

`RutinaEjercicio.DiaSemana` es un `INT NULL` que ubica el ejercicio en la semana: 1 lunes a 5 viernes, con `CK_RutinaEjercicio_DiaSemana` verificando el rango. Nulo significa ejercicio sin dia asignado. `Orden` pasa a ser el orden dentro del dia y no dentro de toda la rutina. Asi una misma plantilla contiene el entrenamiento completo del socio de lunes a viernes. El DDL inicial define la columna y su restriccion.

`Membresia` contiene `FechaInicio`, `FechaVencimiento`, `IdPlan`, `IdSocio`, `IdUsuarioSistema` e `IdRutina` nullable. `CuotaMembresia` contiene `FechaDesde`, `FechaHasta`, `Importe`, `EstadoPago`, `IdMembresia` e `IdRegistroPago` nullable. `Pago` usa `IdRegistroPago` como clave y `IdMetodoPago` como FK.

`MetodoPago.IdNroPagoMP` y `MetodoPago.IdPagoEfectivo` son nullable y apuntan a sus detalles específicos. Los importes usan `DECIMAL(18,2)`.

`Rutina` representa una plantilla reutilizable. La FK opcional `Membresia.IdRutina` implementa la relación Rutina 1:N Membresía: cada membresía apunta a cero o una rutina y una rutina puede ser compartida. `RutinaEjercicio` continúa relacionando Rutina con Ejercicio.

El DDL principal crea una base nueva y es la única fuente de esquema inicial. No se distribuye una migración para bases existentes; no ejecutar el DDL completo sobre una base con datos. La cadena `GymContext` de `App.config` permanece como conexión principal a `.\SQLEXPRESS`; `GymContextRespaldo` apunta a `localhost,1433` y solo se usa si la instancia principal no está disponible. Este respaldo no requiere ejecutar `SysGymDB.sql` ni modificar tablas.

## Foto y sexo — 17 de septiembre de 2026

Socio y UsuarioSistema incorporan `FotoRuta NVARCHAR(260) NULL` y `Sexo char(1) NULL`. SQL Server guarda únicamente rutas relativas; los archivos administrados viven bajo `Datos/Imagenes/Socios` y `Datos/Imagenes/Usuarios`. Sexo admite M, F o NULL mediante validación de negocio. Sin foto personalizada se utilizan recursos embebidos.

El DDL inicial contiene estas columnas opcionales; no se mantiene un script de migración para esquemas previos. En una base local anterior puede permanecer la columna binaria histórica de UsuarioSistema sin mapeo EF6; no se elimina automáticamente.

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

## Imágenes portables — 17 de septiembre de 2026

El DDL nuevo define `Socio.FotoRuta NVARCHAR(260) NULL`. La ruta es relativa a `Datos` y no almacena el contenido de la imagen. `EjercicioImagen` contiene `IdEjercicioImagen`, `IdEjercicio`, `RutaRelativa NVARCHAR(260)` y `Orden`, con FK a `Ejercicio`, restricción de orden positivo y ruta única.

La base local existente fue actualizada de forma aditiva: se agregaron `Socio.FotoRuta` y `UsuarioSistema.FotoRuta` y se creó `EjercicioImagen` sin recrear `SysGymDB` ni perder los 20 socios. Las columnas binarias históricas que puedan existir permanecen sin uso para evitar una eliminación destructiva; el modelo EF6 activo no las mapea.
