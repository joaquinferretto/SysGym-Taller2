# Base de datos

El motor es SQL Server y el esquema fuente está en `capaDatos/Database/SysGymDB.sql`. Ese único script crea `SysGymDB`, tablas, claves, índices, restricciones, usuarios iniciales, métodos de pago y el catálogo inicial de ejercicios y rutinas.

Las entidades principales son Rol, UsuarioSistema, Socio, Plan, Membresia, MembresiaEntrenador, CuotaMembresia, Pago, MetodoPago, MercadoPago, PagoEfectivo, Divisa, Asistencia, Rutina, RutinaEjercicio, RutinaAsignacion y Ejercicio.

`Membresia` contiene `FechaInicio`, `FechaVencimiento`, `IdPlan`, `IdSocio` e `IdUsuarioSistema`. `CuotaMembresia` contiene `FechaDesde`, `FechaHasta`, `Importe`, `EstadoPago`, `IdMembresia` e `IdRegistroPago` nullable. `Pago` usa `IdRegistroPago` como clave y `IdMetodoPago` como FK.

`MetodoPago.IdNroPagoMP` y `MetodoPago.IdPagoEfectivo` son nullable y apuntan a sus detalles específicos. Los importes usan `DECIMAL(18,2)`.

`Rutina` representa una plantilla general creada por un entrenador. `RutinaAsignacion` relaciona la plantilla con una membresía concreta, permitiendo que una misma rutina sea utilizada por muchos socios sin duplicar sus ejercicios.

El DDL principal está pensado para una base nueva. Si ya existe una `SysGymDB` creada con un modelo anterior, usar la migración indicada abajo en lugar de ejecutar nuevamente el DDL completo. La cadena `GymContext` de `App.config` apunta actualmente a `.SQLEXPRESS`; debe cambiarse si la instancia de SQL Server es diferente.

Si `SysGymDB` ya existía con el modelo anterior, ejecutar primero `capaDatos/Database/SysGymDB_MigracionExistente.sql` y luego la sección de catálogo de `SysGymDB.sql`. La migración conserva los datos y vuelve nullable la columna histórica `Rutina.IdSocio`.
