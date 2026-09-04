# Contexto del proyecto

SysGym es una aplicación de escritorio en C# Windows Forms para gestionar un gimnasio. Utiliza .NET Framework 4.8, Entity Framework 6.4.4 y SQL Server.

La solución `exxen2.0.slnx` contiene un único proyecto clásico. Las carpetas `capaVisual`, `capaLogica` y `capaDatos` representan las capas dentro del mismo ensamblado.

El modelo aprobado incluye Rol, UsuarioSistema, Socio, Plan, Membresia, MembresiaEntrenador, CuotaMembresia, Pago, MetodoPago, MercadoPago, PagoEfectivo, Divisa, Asistencia, Rutina, RutinaEjercicio, RutinaAsignacion y Ejercicio.

La base se crea mediante `capaDatos/Database/SysGymDB.sql`. La integración real con Mercado Pago, reportes y procesos automáticos no forman parte del alcance actual.
