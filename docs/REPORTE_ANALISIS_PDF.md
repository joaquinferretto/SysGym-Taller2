# Exportación PDF de Análisis

Última actualización: 8 de octubre de 2026.

## Uso y fuente de datos

Administrador → Análisis → aplicar período → Exportar reporte PDF. SaveFileDialog sugiere `Analisis_SysGym_yyyy-MM-dd_yyyy-MM-dd.pdf`; permite elegir destino, confirma sobrescritura y cancelar no es un error. Solo se habilita después de cargar un análisis válido y con identidad del panel. Cambiar las fechas deshabilita la exportación hasta aplicar el rango o volver al rango cargado. Una carga fallida no conserva un DTO exportable anterior.

`AnalisisFormulario` conserva exactamente el `ResultadoAnalisis` devuelto por `AnalisisLogica.Obtener`, junto con el período aplicado. El servicio recibe ese objeto y los PNG de las series ya cargadas. No ejecuta consultas de análisis, no consulta nuevamente movimientos y no reconstruye cálculos para el PDF. Se reutilizan DatoMensual, DatoCategoria, DatoImporte, EjercicioSinUso e IndicadorComparacion.

Ingresos, pagos y altas corresponden al intervalo elegido. Socios, deuda, planes contratados, entrenadores, rutinas y ejercicios corresponden al estado actual que mostraba la pantalla al aplicar. Cambiar la configuración de deuda requiere volver a aplicar el análisis antes de exportar la clasificación nueva; el exportador no actualiza los datos por separado y no contradice la pantalla.

## Servicio y presentación

`capaLogica/Reportes/ReporteAnalisisServicio.cs` utiliza el paquete PDFsharp-MigraDoc-GDI 6.2.4 ya instalado, sin otra biblioteca, HTML o motor web. Recibe DTO, fechas, PNG, comparación opcional y ruta; resuelve el administrador autenticado y construye el documento. `ReportesPagosServicio` permanece separado y sin cambios.

Contenido:

- Primera página: logo Properties.Resources.SysGymLogo, SysGym, título, período, fecha/hora, nombre/apellido y rol real del generador.
- Resumen ejecutivo: ingresos, pagos, activos/inactivos, al día, deuda total, deuda debajo del límite, límite alcanzado y altas.
- Ingresos por mes: gráfico, tabla breve, total, pagos y variaciones ya calculadas por el módulo.
- Socios: activos/inactivos, estado de deuda y altas por mes. Los históricos sin FechaAlta se excluyen y se informa su cantidad.
- Planes más contratados y dinero cobrado por plan, en secciones separadas; se conserva la atribución al plan actualmente relacionado usada por Análisis.
- Socios por entrenador, incluyendo Sin asignar; distribución de pagos por sus métodos reales.
- Rutinas más utilizadas, socios sin rutina, top 10 ejercicios por rutinas distintas y tabla de ejercicios activos sin uso.
- Comparación A/B de ingresos, pagos y altas, con variación B respecto de A, únicamente cuando esa comparación fue cargada correctamente. Cambiar sus fechas o fallar al compararlas invalida la sección exportable. No se incluye el análisis individual de un socio.

A4 vertical, fondo blanco, Arial, títulos sobrios, tablas con cabecera repetible y pie «Reporte generado por SysGym» con página. Cada gráfico tiene una sección propia para mantenerlo completo; los casos probados producen 12 páginas o 13 con comparación. Tablas breves muestran hasta 10 filas y avisan cuando hay más; los gráficos conservan sus categorías. El top 10 de ejercicios es el ya definido por la consulta, no un ranking nuevo.

Moneda argentina es-AR, dos decimales; porcentajes hasta dos decimales. Sin base anterior se informa que no hay movimiento para calcular variación. Una sección sin datos muestra «Sin datos para el período seleccionado.» sin insertar un gráfico vacío. Un período sin movimientos puede conservar gráficos de estado actual, como en pantalla.

## Gráficos y temporales

`capaVisual/Compartido/ExportadorGraficosAnalisis.cs` utiliza Chart.Serializer para copiar datos y estilo existentes a un Chart transitorio sin Parent. Renderiza con Chart.SaveImage en PNG de 1200 × 675, antialias y fuentes legibles para impresión. No consulta datos, cambia padres o redimensiona el Chart original. La visualización y carga de series de la pantalla no se modificaron.

Las imágenes y el PDF renderizado se mantienen en MemoryStream; MigraDoc recibe imágenes base64. No se crean PNG ni directorios temporales durante la exportación. Residuales esperados: **0**. El único archivo publicado es el PDF elegido. Las copias de Chart, fuentes, streams y documento PDF se liberan al terminar, incluso ante errores.

En las pruebas, un Chart oculto y redimensionado a 250 × 110 produjo bytes PNG iguales a los del Chart de tamaño normal, siempre con resolución 1200 × 675. No se usan capturas de pantalla como contenido del reporte.

## Identidad, seguridad y auditoría

PanelAdministrador pasa el UsuarioSistema autenticado al constructor de Análisis. No se agregó una sesión estática, un combo de operadores ni un nombre fijo. El constructor sin parámetros sirve al Designer y carece de permiso de exportación. La lógica comprueba usuario y rol activos contra SQL tanto al autorizar como al generar; Recepcionista, Entrenador y ausencia de identidad se rechazan.

`GENERAR_REPORTE_ANALISIS` queda disponible en AuditoriaFormulario. Entidad: Analisis; IdEntidad 1 identifica el módulo, no una entidad de socio ni un ID único de archivo. Detalle: «Reporte de análisis generado para el período dd/MM/yyyy - dd/MM/yyyy».

Primero se renderiza completamente el PDF en memoria. Luego se prepara el evento dentro de una transacción, se valida/persiste sin confirmar, se escribe el archivo y se confirma la auditoría. Una falla de renderizado, autorización, auditoría antes de escribir o escritura revierte el evento y no se informa éxito. Cancelar no llama al servicio. Se probaron cancelación real del diálogo, ruta inválida y rechazo del INSERT de auditoría en una base aislada.

El sistema de archivos y SQL Server no comparten una transacción distribuida: una falla de commit después de escribir puede dejar un PDF válido aunque la operación informe error. Tampoco se promete restaurar un destino previo si una escritura falla parcialmente. No se añadieron mecanismos distribuidos para una exportación local sencilla.

## Verificación

Los fixtures se cargaron exclusivamente en `SysGymPdfAnalisisPrueba20261008`. Inicio de sesión real de Admin y navegación al formulario verificaron el flujo del panel. Los cinco casos generados fueron: septiembre con datos, comparación válida, varios meses, período vacío y límite cambiado a 3 con nueva aplicación del análisis.

PDFsharp abrió los archivos y decodificó su texto/ToUnicode para comprobar encabezado, ingresos, pagos, socios activos, clasificación de deuda, plan principal, método principal y cantidades contra el DTO de pantalla. La misma instancia del DTO se mantuvo al exportar. Con datos controlados: $ 100.000,00; 4 pagos; 3 socios activos; 2 con deuda. Cambiar el límite modificó la clasificación, sin reactivar automáticamente membresías.

Se verificaron roles no autorizados, filtros sin aplicar, comparación invalidada al cambiar fechas, ausencia de auditorías en fallos y tamaño/Parent originales de los Chart. Todos los PDFs se abrieron y renderizaron por completo con Windows.Data.Pdf, lector nativo de Windows. La inspección de las páginas comprobó logo, gráficos visibles, texto legible y gráficos completos. Los PNG de esa inspección son evidencia de prueba fuera del repositorio, no temporales del exportador.

Verificación final de esta etapa: el botón Exportar y las nueve pestañas se seleccionaron en VS2026; Análisis y PanelAdministrador abrieron, guardaron y reabrieron sin pantalla roja, NullReferenceException o error de altura. Rebuild Debug y Release: 0 errores y 0 advertencias; git diff --check correcto. La prueba final terminó con SMOKE_PDF_ANALISIS_OK, incluidos Efectivo 3/Mercado Pago 1, indicadores visibles vs texto PDF, PNG independiente del tamaño y cancelación real. Sin commit.

La base aislada se retiró al finalizar. SysGymDB real conservó 41 cuotas, 22 pagos, 4 usuarios, configuración 2/1/7 y cero eventos ficticios. PDFs, fuente/ejecutable de prueba, scripts y logs quedan archivados fuera del repositorio en %TEMP%\sysgym-pdf-analisis-verificacion-20261008. La muestra con datos de prueba es analisis-con-datos.pdf; no representa datos operativos reales.
