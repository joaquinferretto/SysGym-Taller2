# Contexto del proyecto

## Identificacion de socios y entrenadores - 11 de septiembre de 2026

Los datos de prueba contienen nombres repetidos entre `UsuarioSistema` con rol Entrenador y `Socio` (por ejemplo, Benjamin Fernandez), con DNI diferentes. Las pantallas activas muestran nombre completo, DNI y la clave de membresia, manteniendo las claves separadas para asignaciones y asistencias.

Gestion de asignaciones permite elegir una membresia desde un combo y usa su `IdMembresia`; ya no solicita escribir ese numero en un `TextBox`. Gestion de membresias permite cambiar el plan durante la edicion mediante la operacion existente `CambiarPlan`.

La asistencia mantiene la regla de cuota pagada vigente para la fecha elegida. Si el socio no tiene esa cuota, la operacion debe informar el motivo y no registrar un ingreso.

## Equivalencia visual de dashboards - 11 de septiembre de 2026

Se reparo la regresion visual de Administrador, Recepcionista y Entrenador. El layout base queda en 1200x760, con header `Dock=Top` de altura fija, menu de 264 px y contenido `Dock=Fill`; se quito el estado maximizado del diseno. `InicioPanelAdministrador` representa desde Designer el titulo, clima, aviso de cuotas, estado de cuenta y tarjeta de ejemplo. El clima real sigue siendo exclusivamente runtime y agrega tarjetas al `FlowLayoutPanel` sin imponer posiciones.

Debug y Release compilan correctamente sin errores ni warnings reportados. Falta abrir manualmente los tres dashboards con "Ver disenador" y recorrer el flujo de membresia, asignacion y asistencia contra una base disponible.

## Rutinas personalizadas — 11 de septiembre de 2026

Desde “Mis socios”, el entrenador puede crear una rutina exclusiva para un socio con membresía activa de un plan que incluya rutina personal. El administrador dispone además de los accesos de recepcionista y entrenador, incluido el editor global de rutinas. La pantalla reutiliza el editor de rutinas y asigna automáticamente la nueva rutina al socio seleccionado.

Última actualización: 11 de septiembre de 2026.

SysGym es una aplicación de escritorio en C# Windows Forms para gestionar un gimnasio. Utiliza .NET Framework 4.8, Entity Framework 6.4.4 y SQL Server.

La solución `exxen2.0.slnx` contiene un único proyecto clásico. Las carpetas `capaVisual`, `capaLogica` y `capaDatos` representan las capas dentro del mismo ensamblado.

## Menú y respaldo del pronóstico — 11 de septiembre de 2026

Los menús laterales permiten contraer y expandir sus categorías para reducir opciones visibles. El pronóstico se consulta para Corrientes, Corrientes, y se conserva localmente con el día actual y los siete siguientes; si no hay internet se presenta el último respaldo disponible.

El modelo aprobado incluye Rol, UsuarioSistema, Socio, Plan, Membresia, MembresiaEntrenador, CuotaMembresia, Pago, MetodoPago, MercadoPago, PagoEfectivo, Divisa, Asistencia, Rutina, RutinaEjercicio, RutinaAsignacion y Ejercicio.

La base se crea mediante `capaDatos/Database/SysGymDB.sql`. La integración real con Mercado Pago, reportes y procesos automáticos no forman parte del alcance actual.

## Estado de interfaz y fotos — 9 de septiembre de 2026

El encargo FIX_LAYOUT_CODEX.md autoriza paneles estándar con Dock estructural y Anchor para controles editables, además de Foto/Sexo opcionales en socios y usuarios. Los binarios se almacenan en SQL Server; no se crean carpetas de imágenes personales. Falta aportar los avatares de recursos y aplicar la migración en la base comercial. La auditoría estructural de los 17 formularios y el UserControl ya fue realizada; la apertura manual en «Ver diseñador» y el recorrido manual de los tres roles siguen pendientes.

## Auditoría visual estructural — 11 de septiembre de 2026

Los formularios activos conservan su estructura en `Designer.cs`, con paneles, grillas, filtros, campos y acciones visibles para el diseñador. Se corrigieron desbordes al tamaño mínimo, formularios inferiores horizontales y contenedores de detalle insuficientes. Los diez archivos legados con sufijo `Form` carecen de `Designer.cs` y no pertenecen al proyecto actual; quedan documentados como advertencia de mantenimiento, no como pantallas activas.

En cada tarea se actualizan los documentos existentes afectados, con fecha, decisiones y verificaciones pendientes, manteniendo consistencia con el código y SQL.

## Catálogo de planes autorizado — 9 de septiembre de 2026

Se incorpora PlanRutina para seleccionar varias rutinas disponibles en cada plan desde la pantalla existente. La rutina base se conserva por compatibilidad. La autorización para asignar una rutina depende de este catálogo, también para Normal. Premium no incluye automáticamente futuras rutinas. Migración comercial e inspección manual del diseñador pendientes; las verificaciones funcionales se realizaron en una base separada.
