# Índices de SysGym

Un índice es una estructura que ayuda a SQL Server a encontrar registros más rápido sin recorrer toda la tabla. Es parecido al índice de un libro: permite ubicar un dato sin leer todas las páginas.

## Tipos de índices

- La `PRIMARY KEY` identifica cada fila y SQL Server crea un índice asociado.
- `UNIQUE` impide repetir un valor o una combinación de valores y también crea un índice asociado.
- Un índice normal ayuda a buscar o relacionar datos, pero no impide duplicados.
- Un índice único impide valores repetidos.
- Un índice único filtrado aplica la unicidad solamente a las filas que cumplen un filtro.

No se indexan automáticamente todas las Foreign Keys. Una FK protege la relación entre tablas, pero el índice depende de cómo se consulta la información.

## Índices manuales de SysGym

### IX_Membresia_IdSocio

Tabla: `Membresia`  
Columnas: `IdSocio`  
Tipo: índice normal.

Se mantiene porque las membresías se consultan por socio en `MembresiaLogica.ObtenerPorSocio` y también se busca la membresía de un socio al registrar una asistencia.

Consulta que ayuda:

```sql
SELECT *
FROM Membresia
WHERE IdSocio = 10;
```

Sin este índice, SQL Server puede tener que recorrer muchas filas de `Membresia` para encontrar las de un socio concreto.

### IX_Asistencia_IdSocio

Tabla: `Asistencia`  
Columnas: `IdSocio`  
Tipo: índice normal.

Se mantiene porque `AsistenciaLogica.ListarPorSocio` consulta directamente las asistencias de un socio.

Consulta que ayuda:

```sql
SELECT *
FROM Asistencia
WHERE IdSocio = 10;
```

Sin este índice, SQL Server puede necesitar revisar todas las asistencias para devolver las de un socio.

### UX_CuotaMembresia_IdRegistroPago

Tabla: `CuotaMembresia`  
Columna: `IdRegistroPago`  
Tipo: índice único filtrado.

Una cuota pendiente puede no tener pago, por eso la columna admite `NULL`. Varias cuotas pueden tener `NULL`, pero un pago existente no puede asociarse a dos cuotas diferentes.

Filtro:

```sql
IdRegistroPago IS NOT NULL
```

Permite:

```text
Cuota 1 -> NULL
Cuota 2 -> NULL
```

Pero impide:

```text
Cuota 1 -> Pago 5
Cuota 2 -> Pago 5
```

Este índice existe principalmente para hacer cumplir una regla de integridad, no por rendimiento.

## Índices creados por restricciones

SQL Server crea índices asociados a las Primary Keys y a las restricciones Unique. Por eso no se crean índices manuales adicionales para esas columnas.

Ejemplos de SysGym:

- `UsuarioSistema.Username UNIQUE` evita usernames repetidos.
- `UsuarioSistema.DNI UNIQUE` evita DNIs repetidos.
- `Socio.DNI UNIQUE` evita DNIs repetidos entre socios.
- `UQ_CuotaMembresia_Periodo UNIQUE (IdMembresia, FechaDesde)` evita dos cuotas para el mismo inicio de período.

No se agrega, por ejemplo, otro índice `IX_UsuarioSistema_Username`, porque sería redundante.

## Índices evaluados pero no necesarios

### IX_Membresia_IdPlan

Se evaluó porque `IdPlan` es una Foreign Key. Sin embargo, las consultas actuales no filtran directamente las membresías por plan. No se mantiene para evitar agregar un índice que no tiene un uso concreto.

### IX_Membresia_IdUsuarioSistema

Se evaluó porque `IdUsuarioSistema` es una Foreign Key. El código actual no realiza listados directos de membresías por usuario registrador. No se mantiene por la misma razón.

### IX_RutinaEjercicio_IdEjercicio

Se evaluó porque `IdEjercicio` es una Foreign Key. Las consultas actuales listan los ejercicios por `IdRutina`, no por `IdEjercicio`. No se mantiene porque la FK por sí sola no justifica crear un índice.

### IX_CuotaMembresia_IdMembresia

Se evaluó porque la lógica consulta las cuotas por `IdMembresia`. No se mantiene porque el índice creado por `UQ_CuotaMembresia_Periodo (IdMembresia, FechaDesde)` ya tiene `IdMembresia` como primera columna y cubre esa búsqueda.

## Índices finales

Los índices manuales definidos en `capaDatos/Database/SysGymDB.sql` son:

1. `IX_Membresia_IdSocio`
2. `IX_Asistencia_IdSocio`
3. `UX_CuotaMembresia_IdRegistroPago`

Los dos primeros ayudan a consultas habituales. El último garantiza que un pago no se reutilice en dos cuotas y permite múltiples valores `NULL`.
