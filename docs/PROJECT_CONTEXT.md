# Contexto del proyecto

Última actualización: 9 de septiembre de 2026.

SysGym es una aplicación de escritorio en C# Windows Forms para gestionar un gimnasio. Utiliza .NET Framework 4.8, Entity Framework 6.4.4 y SQL Server.

La solución `exxen2.0.slnx` contiene un único proyecto clásico. Las carpetas `capaVisual`, `capaLogica` y `capaDatos` representan las capas dentro del mismo ensamblado.

El modelo aprobado incluye Rol, UsuarioSistema, Socio, Plan, Membresia, MembresiaEntrenador, CuotaMembresia, Pago, MetodoPago, MercadoPago, PagoEfectivo, Divisa, Asistencia, Rutina, RutinaEjercicio, RutinaAsignacion y Ejercicio.

La base se crea mediante `capaDatos/Database/SysGymDB.sql`. La integración real con Mercado Pago, reportes y procesos automáticos no forman parte del alcance actual.

## Estado de interfaz y fotos — 9 de septiembre de 2026

El encargo FIX_LAYOUT_CODEX.md autoriza paneles estándar con Dock estructural y Anchor para controles editables, además de Foto/Sexo opcionales en socios y usuarios. Los binarios se almacenan en SQL Server; no se crean carpetas de imágenes personales. Falta aportar los avatares de recursos y aplicar la migración en la base comercial. La inspección visual final de los 17 diseñadores y el recorrido manual de los tres roles siguen pendientes.

En cada tarea se actualizan los documentos existentes afectados, con fecha, decisiones y verificaciones pendientes, manteniendo consistencia con el código y SQL.

## Catálogo de planes autorizado — 9 de septiembre de 2026

Se incorpora PlanRutina para seleccionar varias rutinas disponibles en cada plan desde la pantalla existente. La rutina base se conserva por compatibilidad. La autorización para asignar una rutina depende de este catálogo, también para Normal. Premium no incluye automáticamente futuras rutinas. Migración comercial e inspección manual del diseñador pendientes; las verificaciones funcionales se realizaron en una base separada.
