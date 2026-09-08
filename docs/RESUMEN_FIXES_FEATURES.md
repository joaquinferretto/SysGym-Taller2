# SysGym: resumen, fixes y features futuras

Revisión: 7 de septiembre de 2026. Este documento propone trabajo pendiente; no representa correcciones ya implementadas.

## Resumen general

SysGym es una aplicación de escritorio para administrar un gimnasio, construida con C#, Windows Forms, .NET Framework 4.8, Entity Framework 6.4.4 y SQL Server. La solución `exxen2.0.slnx` contiene un proyecto; las capas son carpetas del mismo ensamblado.

El flujo de persistencia pasa por `capaVisual → capaLogica → capaDatos → EF6 → SQL Server`. `GymUnidadDeTrabajo` encapsula el contexto y las transacciones. La interfaz todavía utiliza entidades de `capaDatos`, por lo que existe acoplamiento de tipos entre presentación y persistencia, aunque no se encontró acceso directo al contexto o a conexiones SQL desde los formularios en la búsqueda realizada.

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

Los siguientes hallazgos surgen de lectura del código. Sus escenarios deben reproducirse con una base de prueba antes de implementar; esta revisión no ejecutó operaciones sobre SQL Server.

### FIX-01 — Alta: completar el ciclo de pagos

- Evidencia: `capaLogica/PagoLogica.cs`, métodos `RegistrarPago` y `CambiarEstadoPago`; `capaVisual/Recepcionista/GestionPagosForm.cs`, métodos `SeleccionarPrimeraPendiente` y `EstablecerModo`.
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

- Evidencia: `GestionPagosForm.MostrarCuota` coloca `seleccionada.Importe` en el campo importe incluso cuando existe `seleccionada.Pago`.
- Problema: un pago parcial se presenta con el importe completo de la cuota, dificultando distinguir lo cobrado de lo adeudado.
- Propuesta: mostrar el importe del pago en su detalle y presentar importe de cuota y saldo como valores separados cuando corresponda.
- Aceptación: para una cuota de 100 y un pago aprobado de 40, el detalle informa pago 40 y saldo 60.

### FIX-05 — Media: documentar el alcance real y reducir acoplamiento pendiente

- Evidencia: `docs/PROJECT_CONTEXT.md` excluye reportes, pero `ReportesForm` implementa contadores básicos. Varios formularios importan `capaDatos.Entidades`.
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
- La búsqueda no encontró `DbContext`, `GymContext`, conexiones SQL ni repositorios usados directamente desde `capaVisual`, ni dependencias de Windows Forms en lógica/datos. Sí encontró uso de entidades de persistencia en visual.
- No se modificaron formularios ni archivos Designer; no se verificó apertura del diseñador ni interacción de pantallas en esta revisión.
- No se ejecutaron migraciones, cobros ni pruebas contra SQL Server. Compilar no garantiza que esos recorridos funcionen de extremo a extremo.
