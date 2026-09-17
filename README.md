# SysGym - Taller 2

Aplicación de escritorio para la gestión de un gimnasio, desarrollada con Windows Forms, .NET Framework 4.8, Entity Framework 6 y SQL Server.

## Usuarios del sistema y salario

`UsuarioSistema` representa al personal que utiliza la aplicación: administradores, recepcionistas y entrenadores. Cada usuario debe registrar un **salario mensual**.

- En C# se almacena en `UsuarioSistema.Salario` como `decimal`.
- En SQL Server se almacena como `UsuarioSistema.Salario DECIMAL(18,2) NOT NULL`.
- Al crear o editar un usuario desde **Administración → Usuarios y roles**, el salario debe ser mayor que cero.
- En la base inicial, el salario se carga según los datos semilla y puede modificarse desde la aplicación.

## Actualización de la base de datos

- Para crear una base nueva, ejecutar `capaDatos/Database/SysGymDB.sql`.

El DDL es para una base limpia. No ejecutar el script completo sobre una base existente; el proyecto no incluye migraciones para esquemas anteriores.
