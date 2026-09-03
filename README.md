# SysGym - Taller 2

Aplicación de escritorio para la gestión de un gimnasio, desarrollada con Windows Forms, .NET Framework 4.8, Entity Framework 6 y SQL Server.

## Usuarios del sistema y salario

`UsuarioSistema` representa al personal que utiliza la aplicación: administradores, recepcionistas y entrenadores. Cada usuario debe registrar un **salario mensual**.

- En C# se almacena en `UsuarioSistema.Salario` como `decimal`.
- En SQL Server se almacena como `UsuarioSistema.Salario DECIMAL(18,2) NOT NULL`.
- Al crear o editar un usuario desde **Administración → Usuarios y roles**, el salario debe ser mayor que cero.
- Los usuarios de una base existente reciben inicialmente salario `0` durante la migración para no inventar importes ni perder registros. El administrador debe actualizar esos valores desde la aplicación.

## Actualización de la base de datos

- Base nueva: ejecutar `capaDatos/Database/SysGymDB.sql`.
- Base ya existente: ejecutar `capaDatos/Database/SysGymDB_MigracionExistente.sql` sobre `SysGymDB`. El script conserva los datos y agrega la columna `Salario` cuando todavía no existe.

Después de actualizar el código mediante Git, cada colaborador que ya tenga una base local debe ejecutar el script de migración antes de iniciar la aplicación.
