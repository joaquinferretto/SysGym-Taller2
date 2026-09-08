# Integración de Entity Framework

Última actualización: 8 de septiembre de 2026.

El proyecto utiliza Entity Framework 6.4.4 sobre .NET Framework 4.8 con SQL Server. `capaDatos/Contexto/ContextoGimnasio.cs` contiene los `DbSet`, el mapeo explícito de tablas y las relaciones. `capaDatos/Repositorios/UnidadDeTrabajoGimnasio.cs` encapsula las operaciones de persistencia para que la capa lógica no cree ni use `ContextoGimnasio` directamente.

Las FK de membresía son `IdPlan`, `IdSocio` e `IdUsuarioSistema`. `CuotaMembresia.IdRegistroPago` es `int?` y mantiene la relación opcional con `Pago`. `Pago.IdMetodoPago` es obligatorio; `MetodoPago.IdNroPagoMP` e `IdPagoEfectivo` son opcionales. `RutinaEjercicio` es una entidad asociativa explícita entre Rutina y Ejercicio, y `RutinaAsignacion` vincula una plantilla general con una membresía.

Las relaciones usan Fluent API y deshabilitan el borrado en cascada. Los importes, cambio de divisa, peso y altura tienen precisión decimal configurada. El inicializador automático está deshabilitado; la creación inicial se realiza con `capaDatos/Database/SysGymDB.sql`.

## Consultas, transacciones y errores

- Se deshabilitan carga diferida y proxies. `Consultar(relaciones)` incorpora cada relación mediante `Include` explícito; los listados que requieren datos relacionados los solicitan antes de cerrar la unidad de trabajo.
- `ConsultarSoloLectura(relaciones)` aplica `AsNoTracking`, evitando mantener entidades de listados y consultas asociadas al contexto. Las operaciones de modificación mantienen seguimiento.
- Las fechas se mapean a `datetime2`, como en el esquema existente. Se mantienen precisión decimal, claves, relaciones y borrado en cascada deshabilitado; no se requiere migración.
- Membresía y primera cuota se confirman en una única transacción. Las operaciones compuestas de pagos/cuotas también conservan su transacción. Se guardan los estados de cuota antes de consultar la deuda en SQL, dentro de esa misma transacción, para evitar leer el estado anterior.
- `Confirmar` confirma la transacción; su liberación revierte operaciones no confirmadas. Los errores de validación incluyen propiedades y mensajes; los de actualización conservan la excepción original como `InnerException`.

## Compatibilidad de nombres

`ContextoGimnasio` conserva el alias de conexión `GymContext` de `App.config`. `UsuarioSistema.NombreUsuario` y `Clave` se mapean a `Username` y `Password`. En `MercadoPago`, `IdentificadorPago`, `IdentificadorPreferencia`, `ReferenciaExterna` y `DetalleEstado` se mapean a las cuatro columnas originales mediante `[Column]`. Solo cambian nombres C#, no columnas, índices ni datos.
