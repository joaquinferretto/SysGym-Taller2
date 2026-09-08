# SysGym: resumen, fixes y features futuras

Revisión inicial: 7 de septiembre de 2026. Última actualización: 8 de septiembre de 2026.

Estado actual: FIX-02 y FIX-03 implementados. FIX-01, FIX-04 y FIX-05 continúan pendientes; las funciones futuras no se implementaron. Los apartados de evidencia y propuesta siguientes describen los hallazgos originales; el cierre de esta actualización se registra al final.

## Resumen general

SysGym es una aplicación de escritorio para administrar un gimnasio, construida con C#, Windows Forms, .NET Framework 4.8, Entity Framework 6.4.4 y SQL Server. La solución `exxen2.0.slnx` contiene un proyecto; las capas son carpetas del mismo ensamblado.

El flujo de persistencia pasa por `capaVisual → capaLogica → capaDatos → EF6 → SQL Server`. `UnidadDeTrabajoGimnasio` encapsula el contexto y las transacciones. La interfaz todavía utiliza entidades de `capaDatos`, por lo que existe acoplamiento de tipos entre presentación y persistencia, aunque no se encontró acceso directo al contexto o a conexiones SQL desde los formularios en la búsqueda realizada.

| Área | Implementación presente |
| --- | --- |
| Acceso y personal | Autenticación, roles administrador/recepcionista/entrenador, gestión de usuarios, salario mensual y contraseñas con Argon2id. |
| Socios | Gestión de datos personales y físicos, bajas/reactivaciones y cálculo de IMC. |
| Planes y membresías | Planes con beneficios, altas y cambios de membresía, cuotas mensuales y consulta de deuda. |
| Pagos | Registro manual, estados, anulación, reembolso y cálculo de saldo. El modelo contiene efectivo y Mercado Pago; no hay integración real de cobro con Mercado Pago dentro del alcance documentado. |
| Asistencias | Registro y consulta por socio o fecha, con comprobación de membresía y cuota pagada. |
| Entrenamiento | Ejercicios, plantillas de rutinas reutilizables, ejercicios ordenados, asignaciones a membresías y consulta de socios del entrenador. |
| Administración | Dashboard y reporte básico con contadores de socios, usuarios, membresías, rutinas y ejercicios. |
| Base de datos | Script para instalación nueva y script de migración de bases existentes; relaciones explícitas y bajas lógicas para conservar historia. |

La primera cuota se crea junto con la membresía en una transacción. Conserva el precio del plan al generarse. Los pagos aprobados determinan el saldo; las asignaciones de entrenador y rutina dependen de los beneficios del plan.

## Fixes propuestos

Los siguientes hallazgos surgieron de la lectura inicial del código. La verificación original se distingue de las comprobaciones posteriores en el cierre de esta actualización.

### FIX-01 — Alta: completar el ciclo de pagos

- Evidencia: `capaLogica/PagoLogica.cs`, métodos `RegistrarPago` y `CambiarEstadoPago`; `capaVisual/Recepcionista/GestionPagosFormulario.cs`, métodos `SeleccionarPrimeraPendiente` y `EstablecerModo`.
- Problema: se permite registrar un importe menor que la cuota o un pago pendiente, pero la cuota queda vinculada a ese pago y la pantalla deshabilita edición y registro cuando ya existe. Anular o reembolsar tampoco libera la asociación. No hay un recorrido completo desde esta pantalla para resolver esos saldos o reemplazar un cobro.
- Propuesta: definir el tratamiento de pagos parciales, pendientes y reemplazos; habilitar las operaciones correspondientes en lógica y presentación, conservando historial. Respetar el DER aprobado: cualquier ampliación de relaciones requiere una decisión de modelo previa.
- Aceptación: poder completar un pago pendiente y resolver un saldo parcial o un pago anulado/reembolsado según la regla acordada, sin duplicar cobros ni perder historia.

### FIX-02 — Alta: validar el importe al aprobar un pago existente

- Evidencia: `PagoLogica.RegistrarPago` verifica el máximo únicamente cuando el estado inicial es aprobado; `CambiarEstadoPago` permite pasar a aprobado sin repetir esa validación.
- Problema: un pago pendiente superior al importe de su cuota puede aprobarse mediante la API de lógica. La pantalla actual no expone esa transición, pero el método público admite el caso.
- Propuesta: compartir la validación del importe entre registro, actualización y cambio de estado, antes de guardar.
- Aceptación: registrar un pendiente mayor a la cuota e intentar aprobarlo debe fallar sin cambiar el estado persistido; un importe permitido debe seguir funcionando.

### FIX-03 — Alta: incluir todo el último día de la cuota en asistencias

- Evidencia: `AsistenciaLogica.Registrar` compara `FechaHasta >= fecha`, donde `fecha` puede incluir hora. `CuotaMembresiaLogica.CalcularPeriodoHasta` calcula el final mensual conservando la hora del inicio recibido.
- Problema: si la cuota termina a las 00:00, una asistencia a las 18:00 de ese mismo día queda fuera del intervalo.
- Propuesta: comparar el período por día calendario y conservar por separado la hora real de asistencia; mantener la consulta compatible con EF6.
- Aceptación: una cuota pagada con vencimiento el día 7 permite ingresar el día 7 a las 18:00 y rechaza el día 8 si no existe otra cuota habilitante.

### FIX-04 — Media: mostrar el importe real del pago seleccionado

- Evidencia: `GestionPagosFormulario.MostrarCuota` coloca `seleccionada.Importe` en el campo importe incluso cuando existe `seleccionada.Pago`.
- Problema: un pago parcial se presenta con el importe completo de la cuota, dificultando distinguir lo cobrado de lo adeudado.
- Propuesta: mostrar el importe del pago en su detalle y presentar importe de cuota y saldo como valores separados cuando corresponda.
- Aceptación: para una cuota de 100 y un pago aprobado de 40, el detalle informa pago 40 y saldo 60.

### FIX-05 — Media: documentar el alcance real y reducir acoplamiento pendiente

- Evidencia: `docs/PROJECT_CONTEXT.md` excluye reportes, pero `ReportesFormulario` implementa contadores básicos. Varios formularios importan `capaDatos.Entidades`.
- Propuesta: aclarar que existe un resumen básico y que los reportes avanzados son futuros. Planificar contratos de entrada/salida en lógica para los casos que se modifiquen, sin una refactorización global ni cambio de framework.
- Aceptación: documentación consistente con la interfaz; los casos migrados usan contratos de lógica sin introducir acceso directo a persistencia.

## Features futuras propuestas

Estas propuestas amplían el producto y no son requisitos ya aprobados. En particular, reportes avanzados, integración real de Mercado Pago y automatizaciones están fuera del alcance actual documentado.

| Orden | Feature | Resultado esperado | Capas y dependencias |
| --- | --- | --- | --- |
| 1 | Estado de cuenta detallado | Ver cuotas, pagos y saldo por socio, con filtros por período. | Visual y lógica; aprovechar consultas existentes y completar FIX-01/FIX-04. |
| 2 | Alertas de vencimiento | Identificar membresías próximas a vencer y deudas desde el dashboard. | Lógica calcula fechas; visual muestra resultados. Acordar ventana de aviso. |
| 3 | Reportes y exportación | Consultar cobros aprobados, deuda y asistencias por período; exportar resultados. | Agregaciones en datos/lógica y presentación en visual. Definir formato de exportación. |
| 4 | Historial de cambios | Consultar quién modificó una membresía o cambió el estado de un pago. | Requiere diseño de auditoría y migración aprobada del esquema. |
| 5 | Progreso del socio | Consultar evolución de medidas y entrenamiento a lo largo del tiempo. | Requiere definir registros históricos y su persistencia. |
| 6 | Cobros reales con Mercado Pago | Conciliar el estado real de una transacción y evitar registros duplicados. | Requiere ampliar alcance, diseñar integración y recepción segura de notificaciones; resolver primero el ciclo de pagos. |
| 7 | Generación automática de cuotas | Crear períodos pendientes sin duplicarlos y registrar el resultado del proceso. | Lógica y datos; definir dónde se ejecuta cuando la aplicación está cerrada. |

## Orden de trabajo recomendado

1. Corregir FIX-02 y FIX-03, con pruebas de sus límites y persistencia.
2. Resolver la regla pendiente de FIX-01 e implementar el flujo completo junto con FIX-04.
3. Actualizar documentación y abordar el acoplamiento de FIX-05 por caso de uso.
4. Incorporar estado de cuenta, alertas y reportes; evaluar después las ampliaciones que requieren cambios de esquema o servicios externos.

## Verificación de esta revisión

- `msbuild exxen2.0.slnx /t:Build /p:Configuration=Debug /v:minimal /nologo`: finalizó correctamente, sin errores ni warnings informados.
- Se consultaron las cinco guías de `/docs`, el README, la configuración del proyecto y código de los flujos citados.
- La búsqueda no encontró `DbContext`, `ContextoGimnasio`, conexiones SQL ni repositorios usados directamente desde `capaVisual`, ni dependencias de Windows Forms en lógica/datos. Sí encontró uso de entidades de persistencia en visual.
- No se modificaron formularios ni archivos Designer; no se verificó apertura del diseñador ni interacción de pantallas en esta revisión.
- No se ejecutaron migraciones, cobros ni pruebas contra SQL Server. Compilar no garantiza que esos recorridos funcionen de extremo a extremo.

## Actualización de implementación — 8 de septiembre de 2026

### Correcciones y datos

- FIX-02 implementado: `PagoLogica.ValidarImporteAprobado` se comparte entre registro, actualización y cambio de estado. Un pendiente mayor que su cuota no puede aprobarse.
- FIX-03 implementado: `AsistenciaLogica.Registrar` consulta por límites de día calendario, conservando la hora de asistencia y la traducción a SQL de EF6.
- FIX-01, FIX-04 y FIX-05 pendientes. No se agregaron funciones futuras ni se cambió el DER. Sigue pendiente el desacoplamiento de las entidades usadas como tipos por la capa visual.
- Repositorio genérico con operaciones uniformes en español; consultas de lectura sin seguimiento e inclusiones explícitas. Contexto sin carga diferida ni proxies, fechas `datetime2`, relaciones y precisión conservadas.
- Se mantiene la transacción de membresía con primera cuota y se corrige el orden de guardado de estados antes de recalcular deuda. Los errores de EF conservan su causa original.
- La asignación de rutinas rechaza entrenadores inactivos. La gestión de ejercicios limpia la selección después de recargar para permitir un alta nueva.

### Auditoría visual y eventos por pantalla

Hay 16 clases que heredan directamente de `Form` y un `UserControl`, `InicioPanelAdministrador`, ya existente. Este último es la única composición con un control propio; los demás controles son de Windows Forms. Los auxiliares de navegación no son bases visuales. No hay referencias a Windows Forms en lógica/datos ni acceso al contexto desde visual.

| Pantalla actual | Corrección o normalización de eventos durante la tarea |
| --- | --- |
| InicioSesion | Validaciones y botones con métodos nombrados; cierre del panel de sesión suscrito durante su creación. |
| PanelAdministrador | Navegación mediante Click; preparación inicial y usuario en Load. |
| PanelRecepcionista | Navegación mediante Click; preparación inicial y usuario en Load. |
| PanelEntrenador | Navegación mediante Click; preparación inicial y usuario en Load. |
| InicioPanelAdministrador | Servicios y carga inicial desde Load, con protección de modo diseño. |
| GestionUsuariosFormulario | Faltaban cinco Click: nuevo, guardar, actualizar, darDeBaja y reactivar. Etiqueta de contraseña enfoca el campo; filtros, selección y validación con métodos nombrados. |
| GestionPlanesFormulario | Faltaban los mismos cinco Click de gestión; filtros, selección y entrada decimal con métodos nombrados. |
| GestionAsignacionesFormulario | Faltaba SelectionChanged de tabla para identificar la asignación seleccionada. |
| GestionSociosFormulario | Carga, filtros, selección, acciones y entradas decimales normalizados en Designer. |
| GestionEjerciciosFormulario | Carga, selección y acciones con handlers nombrados; selección vacía tras recargar. |
| GestionAsistenciasFormulario | Carga, filtros, selección y acciones con handlers nombrados. |
| GestionMembresiasFormulario | Carga, selección, cambios de opciones y acciones con handlers nombrados. |
| GestionPagosFormulario | Carga, selección, acciones y entrada decimal con handlers nombrados; sin implementar FIX-01/FIX-04. |
| ConsultaRutinasAdministradorFormulario | Carga y botones con handlers nombrados. |
| RutinasEntrenadorFormulario | Carga, selección y botones con handlers nombrados. |
| MisSociosFormulario | Carga, selección y botones con handlers nombrados. |
| ReportesFormulario | Carga y botones con handlers nombrados. |

No se atribuye un evento faltante a las pantallas que solo requirieron normalización. La revisión estática final no encontró botones sin Click ni suscripciones a handlers inexistentes.

### Español, comentarios y limpieza

- Clases, archivos, métodos y parámetros propios se renombraron en español, manteniendo unidos los archivos parciales y recursos. Se conservan las API de .NET, el alias de conexión, identificadores de recursos y contratos externos necesarios. Los textos visibles no se traducen ni cambian en esta tarea.
- El JSON meteorológico conserva las claves del servicio mediante `DataMember`, usando el serializador estándar de .NET; sus propiedades C# están en español. Las propiedades renombradas de persistencia usan `Column` con los nombres SQL existentes.
- Conteo estático de declaraciones con modificador y comentario de bloque, excluyendo Designer: visual **239**, lógica **148**, datos **37**. Incluye constructores y handlers; no cuenta propiedades automáticas, accesores ni declaraciones de contratos sin modificador.
- Se retiraron `.audit-tools`, `.verification`, las carpetas de capturas y los scripts auxiliares de auditoría/diseñador. Estaban registrados en Git y pueden recuperarse del historial. No se crearon carpetas de pruebas nuevas; solo permanecen las salidas habituales de compilación.

### Verificación y límites

- Reconstrucción de la solución en Debug: **0 errores y 0 advertencias**.
- En esta continuación se verificaron los mapeos de usuario y la deserialización de claves meteorológicas a propiedades en español, sin consultar ni modificar la base real.
- Los 17 Designer conservaron todos sus literales y valores numéricos frente al inicio de esta continuación. No se introdujeron cambios visuales; se modificaron identificadores y referencias de eventos para mantenerlos sincronizados.
- La automatización de Visual Studio recorrió las 17 solicitudes de apertura, pero devolvió títulos vacíos: ese resultado **no certifica** que los 17 diseñadores se hayan renderizado correctamente. Queda pendiente la inspección visual final en el diseñador real tras el renombrado.
- Antes de esta continuación se habían obtenido 35 comprobaciones de lógica/datos y 24 de acceso/navegación en una base separada, incluyendo FIX-02 y FIX-03. No deben confundirse con una repetición completa sobre los nombres finales.
- La prueba ampliada de CRUD visual no se completó: la selección programática de un usuario no habilitó Actualizar. Falta distinguir un problema del mecanismo de prueba de un problema real de interacción. No se declara aprobado el recorrido completo de altas, edición, bajas y reactivaciones de los tres roles.

## Movimiento libre y segunda revisión de español — 8 de septiembre de 2026

- Se autorizó modificar los contenedores para permitir arrastrar y dimensionar los controles. Se convirtieron 36 tablas/flujos a paneles estándar en los 17 Designer, conservando los controles existentes. Se eliminaron restricciones de filas/columnas, acoplamiento y tamaño automático; las posiciones y tamaños quedan declarados en `InitializeComponent`, sin bucles ni utilidades visuales propias.
- El movimiento se comprobó en memoria sobre 392 controles de las 17 pantallas: `Location` editable mediante el descriptor de propiedades de .NET, cambio efectivo de posición y ausencia de retorno a la posición anterior tras `PerformLayout`. Esta comprobación no equivale a una inspección visual de los 17 diseñadores de Visual Studio.
- La distribución pasa a ser manual. No se promete identidad píxel a píxel con el diseño anterior ni adaptación automática a distintos tamaños de ventana. Los colores, fuentes y textos de los controles estáticos no se rediseñaron.
- Se corrigieron recortes en grillas/filtros de gestión, botones de baja/reactivación, beneficios de Planes y selección de membresía en Rutinas. Los mensajes del panel inicial se ubicaron dentro de sus cabeceras. Se habilitó desplazamiento de la pantalla para acceder al contenido fijo cuando no cabe en el área disponible. Las mediciones y el renderizado se realizaron en memoria, sin guardar capturas ni crear carpetas.
- El pronóstico usa la tarjeta de ejemplo del diseñador como plantilla de posiciones y estilos para sus siete días. Los controles temporales se liberan al actualizar los datos; la plantilla se conserva y se libera con los componentes de la pantalla.
- Segunda pasada de español: `layout*` pasó a `contenedor*`; `MembresiaPagoItem`/`SocioMembresiaItem` a `OpcionMembresiaPago`/`OpcionSocioMembresia`; la variable `item` y los mensajes de acceso/usuario pendientes también se tradujeron. Se sincronizaron los nombres internos de controles con sus campos. Los nombres requeridos por .NET y los contratos de columnas SQL/JSON se mantienen.
- Se comprobaron 85 botones con suscripciones Click y los métodos correspondientes, sin faltantes. La solución reconstruye en Debug con 0 errores y 0 advertencias.
- `.designer-check.ps1` está eliminado; no se recrearon scripts, carpetas de auditoría ni capturas. La eliminación sigue siendo recuperable desde Git.
- Las imágenes de socios/usuarios y la decisión de almacenamiento para una o varias computadoras quedan pospuestas por el usuario. En esta pasada no se modificó el esquema SQL ni se implementó carga de fotos.
- Se actualizaron los documentos existentes; continúa vigente la obligación de actualizarlos en cada tarea.
