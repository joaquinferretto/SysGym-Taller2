# exxen2.0

Aplicación de escritorio en C# y Windows Forms, con SQL Server como base de datos prevista y Entity Framework como ORM objetivo.

## Estructura

- `capaVisual/`: formularios e interacción con el usuario.
- `capaLogica/`: lógica de negocio.
- `capaDatos/`: persistencia y acceso a datos.
- `docs/`: documentación local para Codex CLI.

La solución y el proyecto se encuentran directamente en la raíz:

- `exxen2.0.slnx`
- `exxen2.0.csproj`

El proyecto utiliza .NET Framework 4.8. La integración de persistencia prevista es Entity Framework 6, aún pendiente de implementación.

Para conocer el estado actual, la arquitectura y las decisiones pendientes, consultar la documentación de `docs/`.
