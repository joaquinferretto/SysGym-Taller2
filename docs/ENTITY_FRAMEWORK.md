# Integración de Entity Framework

El proyecto utiliza Entity Framework 6.4.4 sobre .NET Framework 4.8 con SQL Server. `capaDatos/Contexto/GymContext.cs` contiene los `DbSet`, el mapeo explícito de tablas y las relaciones. `capaDatos/Repositorios/UnidadDeTrabajo.cs` encapsula las operaciones de persistencia para que la capa lógica no cree ni use `GymContext` directamente.

Las FK de membresía son `IdPlan`, `IdSocio` e `IdUsuarioSistema`. `CuotaMembresia.IdRegistroPago` es `int?` y mantiene la relación opcional con `Pago`. `Pago.IdMetodoPago` es obligatorio; `MetodoPago.IdNroPagoMP` e `IdPagoEfectivo` son opcionales. `RutinaEjercicio` es una entidad asociativa explícita entre Rutina y Ejercicio, y `RutinaAsignacion` vincula una plantilla general con una membresía.

Las relaciones usan Fluent API y deshabilitan el borrado en cascada. Los importes, cambio de divisa, peso y altura tienen precisión decimal configurada. El inicializador automático está deshabilitado; la creación inicial se realiza con `capaDatos/Database/SysGymDB.sql`.
