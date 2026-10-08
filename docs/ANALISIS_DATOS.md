# Análisis de datos — primera etapa

Última actualización: 7 de octubre de 2026.

## Estabilidad de Análisis en el Designer de VS2026 — 7 de octubre de 2026

Esta revisión corresponde al formulario actual de nueve pestañas. Las secciones posteriores conservan las definiciones y verificaciones de la primera etapa; no representan el inventario completo actual de consultas. En esta tarea no se modificaron las consultas, EF, SQL ni la lógica de análisis.

### Diagnóstico y decisión

El error reportado fue `NullReferenceException` en `TabControlDesigner.CheckVerbStatus`, durante una notificación de cambio de bounds. La apertura inicial del formulario en la instancia disponible de VS2026 ya funcionó sin pantalla roja; no se obtuvo una reproducción del stack original. Por lo tanto, la causa exacta de esa excepción y la participación del estado cacheado del diseñador quedan sin demostrar. No se atribuye el stack a una página nula o eliminada: la auditoría no encontró ese defecto.

Se encontró una inicialización fragmentada: el TabControl se incorporaba a `disposicion` cuando todavía estaba vacío, cinco páginas se agregaban antes de configurarlas, cuatro se agregaban en otro bloque, `Multiline` se cambiaba después de agregarlas y no había un `SelectedIndex` explícito. Se normalizó exclusivamente esa inicialización en `AnalisisFormulario.Designer.cs`: configurar las páginas y sus controles, agregar las nueve páginas en un bloque, fijar `SelectedIndex = 0`, incorporar `categorias` a su celda y reanudar su layout y el de su contenedor al final, después de los `EndInit`. Esto evita depender de la selección implícita y de incorporar el TabControl incompleto al layout del contenedor; no demuestra por sí solo la causa del stack informado.

Se conservó el TabControl estándar de WinForms. No se reemplazó el formulario ni su navegación, no se agregó UI dinámica ni un try/catch para ocultar errores del diseñador. El constructor sigue llamando solamente a `InitializeComponent`. `Load` conserva la protección de diseño y carga datos en runtime; no existen manejadores de Layout, Resize, SizeChanged, Shown o SelectedIndexChanged que modifiquen estas páginas.

### Auditoría del TabControl y sus páginas

| Propiedad | Estado verificado |
| --- | --- |
| Name / Parent | `categorias` / `disposicion`, columna 0, fila 3 porcentual |
| Dock / Anchor | Fill / Top, Left (Anchor predeterminado; gobierna Dock) |
| Size / Location base | 1074 × 448 / (3, 215) |
| SelectedIndex / SelectedTab | 0 / `tabResumen`, perteneciente a `categorias` |
| Multiline / Alignment | true / Top |
| SizeMode / Padding | Normal / (6, 3) |
| ItemSize | Automático, sin asignación fija: (0, 0) antes del handle; (65, 22) en la prueba runtime |

| Orden | Field / Name | Texto | Parent |
| --- | --- | --- | --- |
| 0 | `tabResumen` | Resumen | `categorias` |
| 1 | `tabFinanzas` | Finanzas | `categorias` |
| 2 | `tabSocios` | Socios | `categorias` |
| 3 | `tabPlanes` | Planes | `categorias` |
| 4 | `tabEntrenadores` | Entrenadores | `categorias` |
| 5 | `tabRutinas` | Rutinas | `categorias` |
| 6 | `tabEjercicios` | Ejercicios | `categorias` |
| 7 | `tabComparar` | Comparar | `categorias` |
| 8 | `tabSocio` | Socio | `categorias` |

Cada página se declara, instancia, configura y agrega exactamente una vez. No existen Name duplicados, referencias a páginas eliminadas, remociones ni cambios de Parent en runtime. Las páginas conservan Dock None y Anchor Top, Left: TabControl administra sus bounds. Antes de crear el handle tienen tamaño predeterminado positivo de 200 × 100; en el diseñador la página seleccionada ocupa 1066 × 414, ubicación (4, 30). Las nueve aceptaron un DisplayRectangle positivo al seleccionarlas y redimensionar el formulario.

Los trece Charts son instancias separadas con Dock Fill: `chartIngresos` en Resumen; `chartMetodos` en Finanzas; `chartAltas`, `chartActivos` y `chartDeuda` en `disposicionSocios`; `chartPlanes` y `chartIngresosPlan` en `disposicionPlanes`; `chartEntrenadores` en Entrenadores; `chartRutinas` en Rutinas; `chartEjercicios` en `tablaEjercicios`; `chartCompararIngresos` y `chartCompararCantidades` en `tablaChartsComparar`; `chartSocio` en `tablaDetalleSocio`. También se verificó que los demás controles permanentes se agregan a un único padre. Los trece Charts y tres DataGridView conservan sus dieciséis pares BeginInit/EndInit. Todos los SuspendLayout conservan su ResumeLayout; no se agregó PerformLayout sobre TabControl o TabPage.

### Verificaciones y límites

- VS2026 real: apertura, selección del TabControl, clic en las nueve pestañas (SelectedIndex 0 a 8), retorno a Resumen, guardar, cerrar y reabrir. Sin `NullReferenceException`, `CheckVerbStatus`, error de altura ni pantalla roja. La solución también se abrió en una instancia nueva de VS2026. No se puede afirmar que el error original desapareció exclusivamente por recargar, porque no se reprodujo antes de los cambios.
- La primera automatización nativa de clics provocó el cierre de Visual Studio; se corrigió para usar coordenadas obtenidas con GetWindowRect y se completó la prueba. Ese incidente de automatización no se considera una reproducción del error original.
- Runtime: una prueba STA con Application.Run ejecutó el PanelAdministrador real y su navegación MDI, abriendo Análisis dos veces y regresando mediante `btnVolver_Click`. Se ejercitaron las nueve páginas, el filtro septiembre de 2026 ($475.000 y 21 pagos), tres tamaños del formulario (1080 × 700, 900 × 600 y 1280 × 800), el dibujo de cada página, la comparación (3 filas) y la selección de un socio (16 filas). Trece gráficos presentes, con ChartAreas y tamaños positivos, sin controles movidos ni excepciones. Comparar y Socio esperan sus respectivas acciones antes de cargar sus series. Esta prueba usa el constructor del panel administrador, sin recorrer el login interactivo.
- La primera versión de la prueba runtime carecía de Application.Run y falló por el contexto de las tareas asíncronas del clima del Inicio; se corrigió el ejecutor de prueba, sin modificar la aplicación. La prueba con el bucle normal de WinForms terminó en `SMOKE_ANALISIS_OK`.
- Rebuild Debug y Release: 0 errores y 0 advertencias. `git diff --check`: correcto. Los archivos `AnalisisFormulario.cs` y `AnalisisFormulario.Consultas.cs` conservaron su contenido original, verificado por SHA-256; no se cambiaron dependencias entre capas.
- No se realizó commit. Pendiente de diagnóstico: si reaparece el stack original en otra sesión, capturar su reproducción y estado de selección; las comprobaciones actuales pasan, pero no establecen su causa histórica.

## Acceso y filtro

`PanelAdministrador` abre `AnalisisFormulario` mediante `ControladorNavegacion`, dentro del área MDI central. El filtro **Desde/Hasta** incluye ambos días y solo afecta movimientos fechados (ingresos y métodos de pago). Las distribuciones de socios, deuda, planes y entrenadores representan el estado **actual**; el formulario lo indica expresamente. El filtro no se guarda.

## Consultas

| Vista | Fuente y criterio | Gráfico |
| --- | --- | --- |
| Ingresos por mes | `Pago.Fecha`, `Importe` y `Estado == Aprobado`; suma por mes y completa meses sin movimientos con cero | Línea |
| Métodos de pago | Pagos aprobados del período; tipo real según `MetodoPago.IdPagoEfectivo` o `IdNroPagoMP`, sin usar `Observaciones` | Torta |
| Altas por mes | No disponible: `Socio` no tiene fecha de alta | Chart con aviso |
| Activos e inactivos | `EstadoSociosLogica.ObtenerResumenSoloLectura()`; incluye total y porcentaje de activos | Torta |
| Estado de deuda | Cuotas vencidas impagas de `EstadoSociosLogica`; `MembresiaLogica.DebeDarseDeBajaPorDeuda` define el límite; categorías exclusivas al día, con deuda bajo el límite y límite alcanzado | Torta |
| Planes contratados | Membresías y socios activos, plan actual de la membresía; socios distintos por plan, orden descendente | Columnas |
| Socios por entrenador | Asignaciones activas con membresía, socio y entrenador activos; socios distintos por entrenador; incluye socios activos sin asignación válida, incluso sin membresía | Barras horizontales |

El total del período es la suma de pagos aprobados dentro de las fechas. La comparación de ingresos usa el intervalo inmediatamente anterior con la misma cantidad de días. La variación es `(actual - anterior) / anterior × 100`; cuando el anterior vale cero se muestra una raya y se evita dividir por cero. Los estados `Pendiente`, `Rechazado`, `Anulado` y `Reembolsado` no se contabilizan como ingresos. La comparación usa el estado actual de cada pago, no reconstruye estados históricos.

Los gráficos vacíos presentan «Sin datos para el período seleccionado». Las tortas no muestran categorías con valor cero. Los gráficos de tendencia mantienen orden cronológico.

Cuando el filtro abarca al menos dos meses, el gráfico de ingresos también muestra la variación del último mes respecto al anterior. Usa los importes de esos meses dentro del filtro: si las fechas recortan un mes, sus valores son parciales. Si el mes anterior no tuvo cobros, se indica que la variación no se puede calcular.

## Arquitectura

`capaVisual/Administrador/AnalisisFormulario` contiene los controles `Chart` en su Designer y carga series dinámicas en el código del formulario. `capaLogica/Analisis/AnalisisLogica` devuelve `ResultadoAnalisis`, `DatoMensual` y `DatoCategoria`. La lógica utiliza `UnidadDeTrabajoGimnasio` y sus repositorios de solo lectura; estos usan EF6 sobre SQL Server. Para estado de socios se reutiliza `EstadoSociosLogica` y el umbral compartido de `MembresiaLogica`. La nueva entrada de solo lectura conserva la clasificación y evita actualizar estados al consultar Análisis; los consumidores anteriores de `ObtenerResumen()` conservan su comportamiento. Los nombres de métodos reutilizan los clasificadores del catálogo de `PagoLogica`; un método histórico inválido se presenta como «Sin clasificación» para evitar omitir pagos.

Flujo: `capaVisual → capaLogica → capaDatos → EF6 → SQL Server`.

## Próximas etapas posibles

Fecha real de alta del socio (requiere decidir y agregar una columna), ingresos por plan, ejercicios y rutinas más usados, ejercicios nunca utilizados, comparación completa de períodos, análisis individual de socio, ranking de deuda y exportación PDF analítica. Ninguna de estas consultas se implementa en esta etapa.

## Verificación de esta etapa

Rebuild Debug y Release con MSBuild de Visual Studio 2026: **0 errores y 0 advertencias**. `git diff --check`: correcto.

Pruebas de integración contra la base local, sin insertar registros: septiembre de 2026 devuelve 21 pagos aprobados y $475.000; el registro reembolsado queda excluido. Métodos: 13 en efectivo y 8 en Mercado Pago. Socios: 29 activos y 2 inactivos. Deuda: 16 sin cuotas vencidas, 15 bajo el límite y 0 en el límite. Entrenadores: 17 socios asignados y 12 sin asignar. La base no tiene un socio que alcance el límite en esta fecha; esa categoría reutiliza directamente el umbral existente.

Se verificaron un día con 6 pagos (`Desde = Hasta`), un período sin pagos, rechazo de `Desde > Hasta`, la comparación con base anterior distinta de cero, división por cero protegida y meses en cero. Con datos controlados se verificó un mes intermedio en cero y un plan sin membresías. Se cargaron y dibujaron todas las pestañas con datos, y se comprobó el mensaje de torta vacía. Los estados de membresías antes y después de consultar permanecieron iguales.

La serie septiembre–octubre devuelve $475.000 y $0 respectivamente, con variación mensual de −100%; la comparación del intervalo con su período anterior sin ingresos queda sin porcentaje.

El formulario abrió en el Designer de Visual Studio 2026; se verificó guardar, cerrar y reabrir, y la presencia de controles y Charts individuales en Esquema del documento. Los controles permanentes están declarados en `InitializeComponent`; solo las series se cargan dinámicamente.
