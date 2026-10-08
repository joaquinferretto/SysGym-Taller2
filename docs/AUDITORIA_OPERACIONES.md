# Trazabilidad y auditoría de operaciones de SysGym

Última actualización: 8 de octubre de 2026.

## Propósito y auditoría previa

La trazabilidad directa responde quién registró un pago o creó un usuario específico. El historial de auditoría responde quién hizo qué y cuándo. Es un registro sencillo de operaciones exitosas, independiente de Análisis: no tiene gráficos ni registra consultas, búsquedas, aperturas, filtros, pestañas o clics.

Antes de implementar se verificaron el código y la base local. Ya existían `Pago.IdUsuarioRegistro`, la navegación `UsuarioRegistro`, `UsuarioSistema.IdUsuarioCreador` y la autorrelación `UsuarioCreador`, todas sin cascada. `Membresia.IdUsuarioSistema` ya identificaba al empleado del alta. `MembresiaEntrenador` guardaba entrenador y estado, sin identificar al operador. Pagos y Usuarios ya mostraban sus responsables en etiquetas de solo lectura y el comprobante PDF ya tenía «Registrado por». Se reutilizaron esos campos, relaciones y controles. No existía una tabla de auditoría ni una configuración global editable de cuotas, anticipación o días de aviso.

## Usuario autenticado y autorización

La sesión existente es el objeto `UsuarioSistema` que devuelve `UsuarioSistemaLogica.Autenticar`, pasa de `InicioSesion` al panel de su rol y de allí a sus formularios. `ISesionPanel` indica si el cierre solicita cambiar de cuenta; no es un almacén global de usuario. No se creó una segunda sesión ni un singleton estático.

Los casos de uso reciben el ID del usuario autenticado automáticamente desde ese flujo. `PagoLogica` conserva su constructor con el ID y `UsuarioSistemaLogica.Crear` conserva el parámetro de creador que ya utilizaba el formulario. Membresías y Asignaciones ahora construyen su lógica con el usuario del panel. Cada instancia de `AuditoriaLogica` conserva ese ID, consulta el usuario y el rol actuales en la base y obtiene por sí misma nombre, rol y fecha/hora. No hay un selector de operador para guardar una operación. El combo Usuario de Auditoría sirve únicamente para filtrar el historial.

Se reutilizan las validaciones de rol activo de `ValidacionesGimnasio`, con la comprobación equivalente de Recepcionista. Una operación auditada exige un usuario y rol activos, Administrador o Recepcionista; crear usuarios y consultar Auditoría exigen Administrador. Los constructores sin usuario se mantienen para el Designer y la lectura; no permiten realizar las operaciones auditadas de forma anónima.

Al cambiar de cuenta, el panel y sus módulos se cierran. El nuevo login construye un panel y lógicas nuevos; no se reutiliza la identidad anterior. Se verificó este recorrido real desde InicioSesion.

## Base de datos y EF6

La tabla `AuditoriaOperacion` contiene:

| Campo | Tipo SQL | Propósito |
| --- | --- | --- |
| IdAuditoria | INT IDENTITY, PK | Identificador del evento |
| FechaHora | DATETIME2 NOT NULL | Hora local del equipo al registrar la operación |
| IdUsuario | INT NOT NULL, FK | Usuario autenticado que la realizó |
| UsuarioNombre | NVARCHAR(201) NOT NULL | Nombre y apellido en el momento de la operación |
| Rol | NVARCHAR(50) NOT NULL | Rol en ese momento |
| Operacion | NVARCHAR(50) NOT NULL | Código estable de la operación |
| Entidad | NVARCHAR(50) NOT NULL | Entidad afectada |
| IdEntidad | INT NOT NULL | Identificador de la entidad afectada |
| Detalle | NVARCHAR(1000) NOT NULL | Descripción legible |

`IdUsuario` referencia la PK real `UsuarioSistema.IdUsuarioSistema`. No tiene borrado en cascada en SQL ni en EF6. Desactivar al usuario conserva el historial. Los snapshots de nombre y rol evitan que una edición posterior del personal reescriba lo que el historial informa. `IdEntidad` es una referencia lógica al registro de distintos tipos; no es una FK única a una sola tabla.

El índice `IX_AuditoriaOperacion_FechaHora` ordena FechaHora e IdAuditoria de forma descendente. Se agregó la entidad en `capaDatos`, su DbSet en `ContextoGimnasio` y su repositorio en la unidad de trabajo existente. No se habilitaron inicializadores ni migraciones automáticas de EF y no se cambiaron framework o paquetes.

`SysGymDB.sql` incluye la estructura para una base nueva, sin insertar eventos históricos. Para una base existente se agregó `capaDatos/Database/AgregarAuditoriaOperaciones.sql`: migración aditiva, transaccional y repetible, sin triggers, stored procedures, MERGE ni SQL dinámico. Conserva las columnas de trazabilidad existentes; si faltan, las agrega nullable, sin asignar responsables retrospectivos.

Ejemplo de ejecución en la base de uso:

```powershell
sqlcmd -S '.\SQLEXPRESS' -E -d SysGymDB -I -f 65001 -i capaDatos/Database/AgregarAuditoriaOperaciones.sql -b
```

La migración fue aplicada dos veces en la instancia local. Después de ambas, los 22 pagos anteriores siguieron con registrador NULL y los 4 usuarios anteriores con creador NULL. La auditoría de esa base quedó vacía: los datos de las pruebas funcionales se cargaron exclusivamente en una base aislada.

## Operaciones auditadas

| Código | Caso de uso | Detalle que registra |
| --- | --- | --- |
| REGISTRAR_PAGO | `PagoLogica.RegistrarPago` | Pago #, socio, importe en pesos y tipo real de método de pago |
| CREAR_USUARIO | `UsuarioSistemaLogica.Crear` | Username y rol del usuario creado |
| CREAR_MEMBRESIA | `MembresiaLogica.Crear` | Plan y socio |
| REACTIVAR_MEMBRESIA | `MembresiaLogica.Habilitar` | Socio cuya membresía pasó de inactiva a activa |
| ASIGNAR_ENTRENADOR | `MembresiaEntrenadorLogica.AsignarEntrenador` | Nombre del entrenador y del socio |
| CAMBIAR_ENTRENADOR | `MembresiaEntrenadorLogica.CambiarEntrenador` | Entrenador anterior, nuevo entrenador y socio |
| QUITAR_ENTRENADOR | `MembresiaEntrenadorLogica.DarDeBajaAsignacion` | Entrenador retirado y socio |
| MODIFICAR_CONFIGURACION | `ConfiguracionSistemaLogica.Guardar` | Valores anteriores/nuevos de deuda, anticipación y aviso |

El método preexistente `ReactivarAsignacion`, si se utiliza, registra ASIGNAR_ENTRENADOR cuando recupera una asignación inactiva. Cambiar entrenador sin una asignación anterior se registra como ASIGNAR_ENTRENADOR. Elegir el mismo entrenador, volver a quitar una asignación ya inactiva o habilitar una membresía ya activa no crea un evento ficticio. La reactivación bloqueada por deuda tampoco crea REACTIVAR_MEMBRESIA.

Registrar un pago significa guardar el movimiento válido; no implica que su estado sea Aprobado. La clasificación de ingresos, estados de pago y reglas de cuotas/deuda conservan su comportamiento. No se registran credenciales ni contraseñas en Detalle.

Se incorporó MODIFICAR_CONFIGURACION mediante ConfiguracionSistemaLogica.Guardar. Solo Administrador activo puede guardar valores; se registra una operación por cambio real con el detalle de cada valor anterior/nuevo. Configuración y evento comparten transacción; leer, abrir y guardar sin cambios no audita. Ver [REGLAS_CUOTAS.md](REGLAS_CUOTAS.md) para instalación, rangos, anticipación, alertas y efecto de cambiar el límite.

## Persistencia de operaciones exitosas

`AuditoriaLogica.RegistrarOperacion` es una API interna de la capa lógica. Recibe la unidad de trabajo de la operación y agrega el evento; no abre una conexión separada ni confirma por su cuenta. El caso de uso guarda la operación, obtiene su ID, guarda la auditoría y confirma la misma transacción. Si el INSERT de auditoría falla, se revierte el cambio principal. En el alta de usuarios también se conserva la limpieza existente de la foto nueva si falla el guardado.

Se mantuvo la evaluación automática de deuda que ya ocurría antes de asignar/cambiar un entrenador. Esa evaluación preexistente tiene su propia persistencia; no se modifica ni se registra como una asignación exitosa. La escritura de la asignación y su evento de auditoría sí comparten una transacción. No se agregaron eventos a consultas ni a sincronizaciones automáticas de estado.

## Trazabilidad directa, históricos y PDF

- Pago nuevo: `IdUsuarioRegistro` se asigna desde la identidad autenticada, y queda un evento REGISTRAR_PAGO. Al editar ese pago no se reemplaza su registrador original.
- Usuario nuevo: `IdUsuarioCreador` se asigna desde el Administrador autenticado, y queda un evento CREAR_USUARIO. Modificar al usuario no reemplaza su creador.
- Membresía nueva: se reutiliza `IdUsuarioSistema`, asignado desde el usuario autenticado; no se agrega otra columna equivalente.
- Pagos o usuarios históricos sin responsable: continúan con NULL y se presentan como «Sin información». No se reconstruyeron operaciones anteriores ni se asignó al Admin como responsable ficticio.
- `GestionPagosFormulario` conserva «Registrado por» y `GestionUsuariosFormulario` conserva «Creado por», ambos de solo lectura. Se reutilizó `ReportesPagosServicio`: el comprobante muestra el nombre del registrador o «Sin información». La referencia directa muestra los datos del usuario relacionado; el historial de auditoría conserva el nombre y rol originales.

## AuditoriaFormulario

Disponible desde Consultas → Auditoría en el menú Administrador. No se agregó una entrada a Recepcionista ni Entrenador. La protección se aplica en la lógica, tanto al abrir como en cada consulta y carga de opciones; no depende solo de Visible. Un usuario sin acceso no obtiene registros aunque intente abrir directamente el formulario.

Los controles permanentes están declarados en el Designer: DateTimePicker Desde/Hasta con checkbox, combos Usuario/Operación, búsqueda libre, Aplicar filtros y DataGridView. Las fechas están desmarcadas inicialmente y no son obligatorias. Usuario tiene Todos y Operación tiene Todas. Cada filtro es independiente; Desde/Hasta incluyen los días completos y se rechaza Desde posterior a Hasta. La búsqueda consulta Detalle, nombre histórico del usuario y Entidad, según la intercalación de SQL Server.

La grilla muestra Fecha/Hora, Usuario, Rol, Operación legible, Entidad y Detalle, más recientes primero; los empates se ordenan por IdAuditoria descendente. No expone columnas de IDs técnicos ni acciones de edición/eliminación. Utiliza `ConsultarSoloLectura`/AsNoTracking, aplica filtros en SQL antes de materializar y trae como máximo 500 coincidencias. El límite aparece en la pantalla y también se aplica con filtros. Se incluyen usuarios inactivos en las opciones para poder consultar su historia.

## Verificaciones

Pruebas de integración en `SysGymAuditoriaPrueba20261008`, sin insertar datos ficticios en SysGymDB:

- Administrador autenticado crea Recepcionista: creador y evento correctos.
- Creación de dos membresías, reactivación y asignar/cambiar/quitar entrenador: detalles, IDs y usuario correctos.
- Duplicado inválido, pago superior a la cuota, falta de sesión y roles no autorizados: rechazados, sin eventos de éxito.
- Acciones sin cambio efectivo: sin eventos adicionales.
- Fallo forzado del INSERT de auditoría mediante un CHECK temporal, exclusivamente en la base aislada: el usuario nuevo también se revirtió. No se usaron triggers ni stored procedures.
- Login real Admin → registro de pago desde la lógica del formulario → Cambiar cuenta → login Recepcionista → otro pago → regreso a Admin: cada pago mantuvo su registrador correcto.
- PDFs de Admin, Recepcionista e histórico: generados correctamente; el origen de «Registrado por» devolvió los dos nombres reales de prueba y «Sin información» para el histórico.
- Historial con nueve eventos de operaciones exitosas: filtros por fecha, usuario, operación y búsqueda por detalle/nombre/entidad correctos. Cambiar después nombre/rol y desactivar al operador no alteró su historial; desactivar al Admin bloqueó su acceso.
- AuditoriaFormulario ejecutado con filtros opcionales y DataGridView de solo lectura. Con 510 coincidencias controladas, la consulta devolvió 500.
- Designer real de VS2026: AuditoriaFormulario, GestionPagosFormulario, GestionUsuariosFormulario, GestionMembresiasFormulario, GestionAsignacionesFormulario, PanelAdministrador y PanelRecepcionista abrieron, permitieron seleccionar un control, guardar, cerrar y reabrir sin pantalla roja ni excepciones del diseñador.
- Rebuild Debug y Release: 0 errores, 0 advertencias. `git diff --check`: correcto. Sin cambios a consultas de Análisis, sin cambios al cálculo de cuotas/deuda, sin nuevas dependencias entre capas y sin commit.

La primera creación del esquema aislado necesitó SQLCMD con `-I` para el índice filtrado preexistente de cuotas. La primera ejecución de las pruebas no reconoció una ArgumentException esperada del filtro de fechas; se corrigió el ejecutor y la ejecución completa terminó con `SMOKE_AUDITORIA_OK`. Ambos ajustes correspondieron al entorno/ejecutor de pruebas.

Se repitió la integración completa después del Rebuild final con el mismo resultado. La base aislada se retiró al terminar y los ejecutables/PDF de prueba se archivaron fuera del proyecto. La comprobación final de SysGymDB mantuvo cero eventos ficticios, 22 pagos sin registrador y 4 usuarios sin creador.

## Límites de esta etapa

La auditoría cubre los ocho códigos indicados, no todas las escrituras de la aplicación. No incluye modificaciones de usuario, anulación/reembolso/edición de pagos, bajas de membresía, cambios de plan, mantenimiento automático, ni procesos externos a SysGym. No se hicieron cambios para auditar operaciones que no existen.

No se reconstruye historia previa ni se permiten eventos anónimos en esta etapa: IdUsuario es obligatorio. Los horarios dependen del reloj local del equipo que ejecuta SysGym. No hay paginación o exportación del historial ni protección criptográfica contra modificaciones por un administrador de la base. No existen operaciones de edición o borrado de auditoría en la aplicación.

## Verificación de configuración global — 8 de octubre de 2026

La prueba aislada verificó cambio 2→3 atribuido al Admin, varios valores en un único evento, filtro/visualización en AuditoriaFormulario, ausencia de eventos por lecturas/no-op y rechazo de Recepcionista/Entrenador/sin sesión. Un CHECK temporal en la base de prueba forzó un fallo de auditoría: los valores y el historial conservaron su estado anterior. Se cambió la sesión real Admin→Recepcionista y el pago posterior conservó la identidad nueva. La base real solo recibió la migración aditiva y su fila global inicial; no recibió operaciones ni pagos ficticios.
