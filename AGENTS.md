# Instrucciones del proyecto

## Proyecto

Aplicación de escritorio en C# con Windows Forms. La base de datos prevista es SQL Server y el ORM obligatorio es Entity Framework.

## Arquitectura por capas

El flujo permitido es:

```text
capaVisual
    ↓
capaLogica
    ↓
capaDatos
    ↓
Entity Framework
    ↓
SQL Server
```

### capaVisual

- Contiene formularios Windows Forms, interacción, presentación, captura de datos, validaciones estrictamente visuales y llamadas a `capaLogica`.
- No escribir SQL, utilizar `DbContext` directamente ni acceder directamente a SQL Server.
- No colocar lógica de negocio compleja ni dependencias hacia capas inferiores de persistencia.

### capaLogica

- Contiene lógica y validaciones de negocio, procesamiento y coordinación entre `capaVisual` y `capaDatos`.
- No utilizar controles Windows Forms, formularios ni `MessageBox`.
- No escribir SQL, abrir conexiones SQL directamente ni depender de la capa visual.

### capaDatos

- Contiene persistencia, Entity Framework, `DbContext`, entidades de persistencia, relaciones y operaciones CRUD contra SQL Server.
- Utilizar Entity Framework como ORM.
- Evitar ADO.NET directo y consultas SQL manuales salvo necesidad técnica concreta y justificada.
- No incluir lógica visual, controles Windows Forms ni `MessageBox`.

Evitar dependencias en sentido inverso; por ejemplo, `capaDatos` no debe depender de `capaVisual`.

## Windows Forms

Un formulario puede estar compuesto por `Formulario.cs`, `Formulario.Designer.cs` y `Formulario.resx`.

- No editar manualmente `.Designer.cs` salvo necesidad estricta.
- No separar accidentalmente los archivos parciales.
- Mantener sincronizados los namespaces de `.cs` y `.Designer.cs`.
- Después de cambios estructurales, verificar que los formularios sigan abriendo en el diseñador.

## Entity Framework

Antes de modificar su configuración, revisar el framework y los paquetes actuales y determinar si corresponde EF6 o EF Core. No cambiar la versión de .NET para utilizar otra variante. La capa visual nunca debe usar directamente `DbContext`.

## Reglas para Codex

Antes de implementar: leer los archivos involucrados, entender el flujo, determinar la capa correcta y consultar `/docs` cuando corresponda.

Durante los cambios:

- Mantener convenciones existentes y evitar duplicación innecesaria.
- No eliminar funcionalidad sin solicitud explícita.
- No realizar refactors globales, instalar dependencias innecesarias ni cambiar framework o arquitectura sin autorización.
- Evitar modificar archivos no relacionados.

Después de cambios:

1. Compilar la solución.
2. Revisar errores y warnings relevantes.
3. Verificar los formularios afectados.
4. Verificar las dependencias entre capas.

Para el detalle del estado actual y las decisiones pendientes, consultar:

```text
docs/ARCHITECTURE.md
docs/DATABASE.md
docs/BUSINESS_RULES.md
docs/PROJECT_CONTEXT.md
docs/ENTITY_FRAMEWORK.md
```
