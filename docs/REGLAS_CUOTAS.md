# Configuración global y reglas de cuotas

Última actualización: 8 de octubre de 2026.

## Configuración y persistencia

`ConfiguracionSistema` contiene una única fila global, con `IdConfiguracion = 1`. La PK y el CHECK impiden una segunda fila. No contiene una configuración por usuario ni una sesión paralela. EF6 utiliza la entidad, DbSet y repositorio correspondientes; los formularios llaman únicamente a la capa lógica.

| Campo | Inicial | Rango admitido |
| --- | --- | --- |
| MaxCuotasVencidasPermitidas | 2 | 1–120 |
| MaxMesesAnticipacionCuotas | 1 | 0–120 |
| DiasAvisoVencimiento | 7 | 0–365 |

Los máximos mantienen rangos operativos finitos y coinciden en NumericUpDown, validación lógica, atributos EF y CHECK de SQL. Anticipación 0 permite el período actual; aviso 0 incluye solamente vencimientos de hoy.

Una base nueva se crea con `capaDatos/Database/SysGymDB.sql`. Para una base existente, ejecutar `AgregarConfiguracionSistema.sql` sobre SysGymDB. Es aditiva y transaccional: crea la tabla/fila si falta; repetirla conserva los valores previamente guardados. No reconstruye ni modifica cuotas, pagos, responsables históricos o auditorías. EF no actualiza automáticamente el esquema. La fila global no tiene una operación de eliminación en la aplicación.

`ConfiguracionSistemaLogica.Obtener` es la fuente de valores actuales. Las evaluaciones internas leen con AsNoTracking en su unidad de trabajo, sin caché global o asociada a una sesión. Una fila ausente informa que falta aplicar la migración; no sustituye silenciosamente los valores persistidos por defaults.

## Deuda e inactivación

Cuota vencida impaga: `EstadoPago == Pendiente` y `FechaHasta < DateTime.Today`. Pagadas, anuladas y pendientes aún no vencidas quedan excluidas del conteo. El importe pendiente de una cuota no vencida continúa existiendo en el estado de cuenta, pero no significa que haya alcanzado el límite de deuda.

La clasificación compartida por dashboards, EstadoSociosControl y Análisis es:

- 0 vencidas: Al día.
- Más de 0 y menos del límite: Con deuda.
- Cantidad mayor o igual al límite: Límite alcanzado.

`MembresiaLogica` mantiene la inactivación automática al alcanzar el límite y la sincronización del socio según sus membresías activas. Las consultas de Análisis son de solo lectura: clasifican sin persistir bajas. Los listados operativos y operaciones que ya evaluaban deuda siguen sincronizando el estado. Bajar el límite puede producir nuevas bajas en la siguiente evaluación operativa; subirlo no reactiva registros.

Pagar una deuda tampoco reactiva automáticamente. La reactivación es manual y solo se permite cuando la cantidad de vencidas queda por debajo del límite vigente. Una reactivación bloqueada no genera una auditoría de éxito.

## Generación y anticipación

`CuotaMembresiaLogica` vuelve a validar en cada generación: membresía activa, plan activo, cuota inicial existente, vencidas debajo del límite, período no duplicado y horizonte permitido. La disponibilidad que usa el botón Generar cuota tiene las mismas reglas y un motivo legible. Un botón habilitado previamente no evita la nueva validación lógica.

El siguiente período comienza siempre en `UltimaCuota.FechaHasta.Date.AddDays(1)`. Termina en `FechaDesde.AddMonths(1).AddDays(-1)`, conservando el convenio mensual actual, el precio histórico y el ajuste de meses cortos. No se usa hoy como inicio de una cuota posterior. Las cuotas anuladas siguen formando parte de la secuencia histórica; no se recrea automáticamente un período anulado.

El horizonte se calcula desde la fecha de inicio de la primera cuota real, recorriendo períodos mensuales consecutivos con ese mismo convenio hasta encontrar el que contiene hoy. Se permiten ese período y N períodos posteriores, donde N es MaxMesesAnticipacionCuotas. Si la membresía todavía comienza en el futuro, su primer período es la referencia. La cuota candidata debe terminar dentro del horizonte completo. El horizonte depende de hoy y del origen de la membresía; nunca avanza porque el usuario genere otra cuota.

Ejemplo al 08/10/2026, origen 01/09/2026: el período actual es octubre. Con anticipación 1 el horizonte termina 30/11/2026; con 2 termina 31/12/2026. Con origen 23/09/2026, el período actual termina 22/10/2026 y anticipación 1 termina 22/11/2026. Los pasos se calculan secuencialmente para conservar también la secuencia 31 de enero → 28 de febrero → 28 de marzo.

Se reutiliza `UQ_CuotaMembresia_Periodo (IdMembresia, FechaDesde)`, ya existente, para impedir duplicados también ante inserciones concurrentes. La validación lógica informa «Ya existe una cuota para ese período»; un conflicto concurrente también es rechazado por SQL y se revierte la transacción. No se agrega un segundo índice equivalente.

Mensajes de bloqueo: «El socio alcanzó el límite de cuotas vencidas permitido», «No se pueden generar cuotas más allá del límite configurado» y «Ya existe una cuota para ese período».

## Cuotas futuras existentes

No se elimina, anula ni recorta una cuota por cambiar la anticipación. Si la última cuota excede el horizonte, se bloquea la siguiente generación hasta que la fecha actual o una configuración posterior la permita.

Inventario de SysGymDB al 08/10/2026, configuración inicial 2/1/7:

| Socio | Membresía | Última cuota | Horizonte | Exceso |
| --- | --- | --- | --- | --- |
| Chiara Barbieri | 6 | #38, 01/04/2027–30/04/2027 | 30/11/2026 | 5 períodos mensuales; 151 días |
| leonardo gutierrez | 31 | #41, 23/11/2026–22/12/2026 | 22/11/2026 | 1 período mensual; 30 días |

Los demás registros no excedían su horizonte. La migración conservó las 41 cuotas, 22 pagos y 4 usuarios de la base real; su auditoría permaneció vacía. Los cambios y pagos de prueba se realizaron exclusivamente en una base aislada.

## Alertas, administración y auditoría

Las alertas cuentan cuotas Pendiente cuyo vencimiento está entre hoy y hoy más DiasAvisoVencimiento, inclusive. EstadoSociosControl alimenta ambos dashboards y presenta el plazo configurado. El contador de vencimientos de hoy se conserva como aviso específico. Los filtros y tarjetas de deuda usan la clasificación configurable; no muestran «2+ vencidas» como límite fijo.

Administrador dispone de Administración → Configuración, con tres NumericUpDown y Guardar cambios. Recepcionista y Entrenador no tienen esa opción. La capa lógica verifica usuario, rol y estado activo en SQL al autorizar y guardar; ocultar el menú no constituye la protección. Los valores públicos de lectura están disponibles para aplicar reglas operativas, sin conceder permisos de escritura.

Guardar un cambio real registra una sola `MODIFICAR_CONFIGURACION` con todos los valores anteriores/nuevos, el Administrador autenticado y fecha/hora. Configuración y auditoría comparten unidad de trabajo/transacción: un fallo del historial revierte los valores. Abrir, consultar o guardar sin cambios no audita. La opción aparece automáticamente en los filtros de AuditoriaFormulario.

Los cambios aplican a las siguientes operaciones y consultas. Un listado ya abierto necesita actualizarse o reabrirse para mostrar los valores nuevos. No se realiza una reactivación masiva, reconstrucción histórica ni recalculo de importes.

## Verificación y límites

La base aislada `SysGymConfiguracionPrueba20261008`, creada desde el esquema actualizado, verificó deuda 0/1/2 con límite 2, 2/3 con límite 3, exclusión de pagadas/anuladas, inactivación de socio/membresía, pago sin reactivación y posterior reactivación manual. Análisis coincidió con la clasificación del resumen.

Generaciones repetidas con anticipación 1 se bloquearon; cambiar a 2 permitió exactamente otro período y volver a 0 conservó los futuros. Se probaron cronología, meses cortos, restricción única, alertas 7→10 con una cuota a 8 días y plazo 0. Autorización rechazó Recepcionista, Entrenador y ausencia de sesión, además de negativos y valores fuera de rango. No-op/lecturas no auditaron y un CHECK temporal exclusivamente de prueba forzó un fallo de auditoría que revirtió la configuración.

La aplicación WinForms se ejecutó con inicio de sesión real Admin → Recepcionista. Se verificaron menú, guardar/reabrir configuración, historial visible, dashboards con avisos 7/10 y pago posterior atribuido a Recepcionista. Los fixtures no utilizaron ni cambiaron credenciales reales.

La automatización de estas pruebas es un ejecutable temporal externo a la aplicación; no incorpora frameworks o dependencias al proyecto. El script de creación, fuente de prueba y evidencia se archivan fuera del repositorio. El límite de anticipación aplica a cuotas posteriores; el alta mantiene la primera cuota obligatoria correspondiente al inicio elegido de la membresía. Esta funcionalidad no redefine la fecha permitida de alta de una membresía.

Verificación final: ConfiguracionFormulario, GestionMembresiasFormulario, EstadoSociosControl, AnalisisFormulario y PanelAdministrador abrieron en el Designer real de VS2026; se seleccionó un control, se guardó, cerró y reabrió cada documento, sin pantalla roja ni excepción. Rebuild Debug y Release: 0 errores y 0 advertencias. git diff --check: correcto. Sin commit.

La repetición final sobre la compilación validada terminó con CONFIGURACION_COMPLETAMENTE_VERIFICADA e incluyó el botón Generar cuota deshabilitado y el motivo de anticipación visible. Repetir la migración sobre valores editados 3/2/10 conservó una sola fila y esos valores. Se retiró la base aislada al terminar; la base real mantuvo defaults 2/1/7 y sus registros anteriores. Fuente, ejecutable, configuración aislada, SQL de prueba y logs se archivaron en %TEMP%\sysgym-configuracion-verificacion-20261008.
