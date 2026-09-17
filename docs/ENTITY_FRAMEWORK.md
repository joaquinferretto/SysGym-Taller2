# Integración de Entity Framework

## Rutinas personalizadas — 16 de septiembre de 2026

La consulta de “Mis socios” usa LINQ sobre los repositorios de EF6 y proyecta los datos que necesita la grilla junto con la rutina de la membresía. El administrador puede solicitar la misma consulta sin filtrar por entrenador. `CrearPersonalizada` crea la plantilla y vincula su `IdRutina` a la membresía seleccionada mediante la unidad de trabajo; los detalles se guardan con `RutinaEjercicioLogica`.

Última actualización: 16 de septiembre de 2026.

El proyecto utiliza Entity Framework 6.4.4 sobre .NET Framework 4.8 con SQL Server. `capaDatos/Contexto/ContextoGimnasio.cs` contiene los `DbSet`, el mapeo explícito de tablas y las relaciones. `capaDatos/Repositorios/UnidadDeTrabajoGimnasio.cs` encapsula las operaciones de persistencia para que la capa lógica no cree ni use `ContextoGimnasio` directamente.

Las FK de membresía son `IdPlan`, `IdSocio`, `IdUsuarioSistema` e `IdRutina`; esta última es nullable. La relación es Rutina 1:N Membresía, por lo que una rutina puede reutilizarse y cada membresía tiene como máximo una. `CuotaMembresia.IdRegistroPago` es `int?` y mantiene la relación opcional con `Pago`. `Pago.IdMetodoPago` es obligatorio; `MetodoPago.IdNroPagoMP` e `IdPagoEfectivo` son opcionales. `RutinaEjercicio` es una entidad asociativa explícita entre Rutina y Ejercicio.

Las relaciones usan Fluent API y deshabilitan el borrado en cascada. Los importes, cambio de divisa, peso y altura tienen precisión decimal configurada. El inicializador automático está deshabilitado; la creación inicial se realiza con `capaDatos/Database/SysGymDB.sql`.

## Consultas, transacciones y errores

- Se deshabilitan carga diferida y proxies. `Consultar(relaciones)` incorpora cada relación mediante `Include` explícito; los listados que requieren datos relacionados los solicitan antes de cerrar la unidad de trabajo.
- `ConsultarSoloLectura(relaciones)` aplica `AsNoTracking`, evitando mantener entidades de listados y consultas asociadas al contexto. Las operaciones de modificación mantienen seguimiento.
- Las fechas se mapean a `datetime2`. Se mantienen precisión decimal, claves, relaciones y borrado en cascada deshabilitado. El inicializador automático permanece deshabilitado.
- Membresía y primera cuota se confirman en una única transacción. Las operaciones compuestas de pagos/cuotas también conservan su transacción. Se guardan los estados de cuota antes de consultar la deuda en SQL, dentro de esa misma transacción, para evitar leer el estado anterior.
- `Confirmar` confirma la transacción; su liberación revierte operaciones no confirmadas. Los errores de validación incluyen propiedades y mensajes; los de actualización conservan la excepción original como `InnerException`.

## Compatibilidad de nombres

`ContextoGimnasio` conserva el alias de conexión `GymContext` de `App.config`. `UsuarioSistema.NombreUsuario` y `Clave` se mapean a `Username` y `Password`. En `MercadoPago`, `IdentificadorPago`, `IdentificadorPreferencia`, `ReferenciaExterna` y `DetalleEstado` se mapean a las cuatro columnas originales mediante `[Column]`. Solo cambian nombres C#, no columnas, índices ni datos.

## Foto y sexo — 9 de septiembre de 2026

Los atributos de Socio y UsuarioSistema mapean Foto a varbinary(max) nullable y Sexo a char(1) nullable con StringLength(1). No se cambia EF6, las claves, las relaciones ni el borrado en cascada.

Los listados directos de SocioLogica y UsuarioSistemaLogica proyectan en SQL los campos necesarios antes de materializar, excluyendo Foto; los de usuarios también excluyen Clave e incluyen el rol mediante la proyección. Se conserva AsNoTracking. ObtenerPorId recupera la foto del registro seleccionado. Las consultas de otros módulos con entidades relacionadas no se presentan como una auditoría global de transferencia de binarios.

Alta y modificación guardan Foto/Sexo junto con el registro en GuardarCambios; no requieren una segunda operación de archivos ni una transacción adicional. Se mantienen las transacciones existentes de membresías y pagos y la propagación de errores con InnerException.

La relación opcional `Membresia.Rutina` se configura con `HasOptional(...).WithMany(...).HasForeignKey(m => m.IdRutina)`, sin borrado en cascada. `Rutina` no tiene FK hacia Socio ni Plan. El DDL inicial declara `Membresia.IdRutina INT NULL` y `FK_Membresia_Rutina`, manteniendo el mismo modelo que EF6. `SysGymDB.sql` es el esquema de creación para una base nueva; no se incluye un script de migración para bases existentes.
