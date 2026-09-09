# Integración de Entity Framework

Última actualización: 9 de septiembre de 2026.

El proyecto utiliza Entity Framework 6.4.4 sobre .NET Framework 4.8 con SQL Server. `capaDatos/Contexto/ContextoGimnasio.cs` contiene los `DbSet`, el mapeo explícito de tablas y las relaciones. `capaDatos/Repositorios/UnidadDeTrabajoGimnasio.cs` encapsula las operaciones de persistencia para que la capa lógica no cree ni use `ContextoGimnasio` directamente.

Las FK de membresía son `IdPlan`, `IdSocio` e `IdUsuarioSistema`. `CuotaMembresia.IdRegistroPago` es `int?` y mantiene la relación opcional con `Pago`. `Pago.IdMetodoPago` es obligatorio; `MetodoPago.IdNroPagoMP` e `IdPagoEfectivo` son opcionales. `RutinaEjercicio` es una entidad asociativa explícita entre Rutina y Ejercicio, y `RutinaAsignacion` vincula una plantilla general con una membresía.

Las relaciones usan Fluent API y deshabilitan el borrado en cascada. Los importes, cambio de divisa, peso y altura tienen precisión decimal configurada. El inicializador automático está deshabilitado; la creación inicial se realiza con `capaDatos/Database/SysGymDB.sql`.

## Consultas, transacciones y errores

- Se deshabilitan carga diferida y proxies. `Consultar(relaciones)` incorpora cada relación mediante `Include` explícito; los listados que requieren datos relacionados los solicitan antes de cerrar la unidad de trabajo.
- `ConsultarSoloLectura(relaciones)` aplica `AsNoTracking`, evitando mantener entidades de listados y consultas asociadas al contexto. Las operaciones de modificación mantienen seguimiento.
- Las fechas se mapean a `datetime2`, como en el esquema existente. Se mantienen precisión decimal, claves, relaciones y borrado en cascada deshabilitado; esas correcciones no requieren migración. La ampliación posterior de Foto/Sexo sí requiere aplicar el script indicado abajo.
- Membresía y primera cuota se confirman en una única transacción. Las operaciones compuestas de pagos/cuotas también conservan su transacción. Se guardan los estados de cuota antes de consultar la deuda en SQL, dentro de esa misma transacción, para evitar leer el estado anterior.
- `Confirmar` confirma la transacción; su liberación revierte operaciones no confirmadas. Los errores de validación incluyen propiedades y mensajes; los de actualización conservan la excepción original como `InnerException`.

## Compatibilidad de nombres

`ContextoGimnasio` conserva el alias de conexión `GymContext` de `App.config`. `UsuarioSistema.NombreUsuario` y `Clave` se mapean a `Username` y `Password`. En `MercadoPago`, `IdentificadorPago`, `IdentificadorPreferencia`, `ReferenciaExterna` y `DetalleEstado` se mapean a las cuatro columnas originales mediante `[Column]`. Solo cambian nombres C#, no columnas, índices ni datos.

## Foto y sexo — 9 de septiembre de 2026

Los atributos de Socio y UsuarioSistema mapean Foto a varbinary(max) nullable y Sexo a char(1) nullable con StringLength(1). No se cambia EF6, las claves, las relaciones ni el borrado en cascada.

Los listados directos de SocioLogica y UsuarioSistemaLogica proyectan en SQL los campos necesarios antes de materializar, excluyendo Foto; los de usuarios también excluyen Clave e incluyen el rol mediante la proyección. Se conserva AsNoTracking. ObtenerPorId recupera la foto del registro seleccionado. Las consultas de otros módulos con entidades relacionadas no se presentan como una auditoría global de transferencia de binarios.

Alta y modificación guardan Foto/Sexo junto con el registro en GuardarCambios; no requieren una segunda operación de archivos ni una transacción adicional. Se mantienen las transacciones existentes de membresías y pagos y la propagación de errores con InnerException.

Aplicar SysGymDB_MigracionExistente.sql antes de usar el modelo contra una base existente. La persistencia se verificó en SysGym_Verificacion_20260908 dentro de una transacción temporal revertida; la base comercial no se migró durante esta tarea.

## Relación PlanRutina — 9 de septiembre de 2026

Plan.RutinasDisponibles usa HasMany/WithMany con tabla PlanRutina y claves IdPlan/IdRutina. La clave compuesta impide duplicar vínculos. Se elimina la convención ManyToManyCascadeDeleteConvention y las FK SQL no tienen cascada. No cambia la versión de EF ni el framework.

No se agrega un contexto ni acceso SQL en lógica/visual. PlanLogica consulta las rutinas elegidas con seguimiento y valida entrenador/rol explícitamente antes de asociarlas; no adjunta como nuevas las entidades del selector. Lecturas de planes incluyen RutinasDisponibles y conservan AsNoTracking. Los filtros de membresía usan Any sobre la relación, traducido a SQL por EF6.

GuardarCambios confirma el catálogo y las asignaciones finalizadas en la transacción automática de EF de una sola llamada. CambiarPlan mantiene su transacción explícita y finaliza solo las rutinas fuera del nuevo catálogo. La migración SQL existente debe aplicarse antes de usar este modelo.
