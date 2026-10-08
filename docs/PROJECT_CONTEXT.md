# Contexto de SysGym

Última actualización: 8 de octubre de 2026.

SysGym es una aplicación Windows Forms para .NET Framework 4.8, EF6 y SQL Server. La navegación del administrador usa `ControladorNavegacion` y formularios hijos MDI dentro del área central.

El módulo **Análisis** está disponible solo en `PanelAdministrador`. Muestra gráficos de ingresos y pagos del período elegido, junto con distribuciones del estado actual de socios, deuda, planes y entrenadores. `ReportesFormulario` conserva su reporte operativo de cinco contadores; `ReportesPagosServicio` y su exportación siguen en gestión de pagos.

En la primera etapa, la entidad `Socio` no registraba fecha de alta y Análisis informaba que esa serie no estaba disponible. En el estado actual existe `FechaAlta` nullable y los históricos sin fecha comprobable conservan NULL; no se usa fecha de nacimiento ni inicio de membresía como sustituto.

Ver [ANALISIS_DATOS.md](ANALISIS_DATOS.md) para definiciones de cada consulta y verificaciones.

## Revisión del Designer de Análisis — 7 de octubre de 2026

El formulario actual conserva nueve pestañas: Resumen, Finanzas, Socios, Planes, Entrenadores, Rutinas, Ejercicios, Comparar y Socio. Los párrafos anteriores describen la primera etapa del módulo; actualmente también existen consultas de altas, ingresos por plan, rutinas, ejercicios, comparación y socio individual.

Se normalizó la inicialización de `categorias` en su Designer: las páginas configuradas se agregan una vez y juntas, se establece `SelectedIndex = 0` antes de incorporar el control a `disposicion` y se reanudan ambos layouts después de completar la inicialización de sus hijos. No se modificaron lógica de análisis, consultas, EF ni SQL.

VS2026 permitió seleccionar las nueve pestañas, guardar, cerrar y reabrir sin pantalla roja ni NullReferenceException. La prueba del panel administrador con su navegación MDI verificó datos, gráficos, filtros, comparación, consulta de socio y regreso a Inicio/reapertura. Debug y Release compilaron con 0 errores y 0 advertencias. El error original no se reprodujo en la apertura inicial: su causa exacta y la participación de caché siguen sin demostrarse. El inventario, las verificaciones y sus límites están documentados en `ANALISIS_DATOS.md`.

## Trazabilidad y Auditoría — 8 de octubre de 2026

La revisión previa confirmó que pagos y usuarios ya disponían de `IdUsuarioRegistro` e `IdUsuarioCreador`, relaciones EF sin cascada, etiquetas de responsable y registrador en comprobante PDF. Se reutilizó esa trazabilidad y la identidad que InicioSesion pasa a los paneles/formularios; no se agregó otra sesión.

Se incorporaron `AuditoriaOperacion`, su entidad/DbSet/repositorio y `AuditoriaLogica`. El historial conserva fecha/hora, usuario, nombre/rol del momento, operación, entidad, ID y detalle legible. Registrar pago, crear usuario/membresía, reactivar membresía y asignar/cambiar/quitar entrenador guardan su auditoría dentro de la transacción de la operación. La lógica exige roles activos; la consulta es exclusiva de Administrador.

El menú Administrador incluye Consultas → Auditoría, con controles estáticos en Designer, filtros opcionales de fecha/usuario/operación/búsqueda y grilla de solo lectura. La consulta usa AsNoTracking, filtra antes de materializar y limita a las 500 coincidencias más recientes. En esta etapa inicial no se agregó configuración; la etapa siguiente, documentada abajo, incorpora esa funcionalidad y su auditoría.

La migración aditiva `AgregarAuditoriaOperaciones.sql` se aplicó dos veces sin duplicar estructuras ni asignar responsables ficticios a históricos. Los 22 pagos y 4 usuarios anteriores conservaron sus NULL. Se probaron las operaciones, el cambio de sesión real Admin → Recepcionista, PDFs, filtros, autorización, historial de usuarios inactivos y rollback si falla la auditoría en una base aislada. Los siete formularios revisados abrieron, guardaron y reabrieron en VS2026. Debug y Release: 0 errores y 0 advertencias; git diff --check correcto. Sin commit.

Ver [AUDITORIA_OPERACIONES.md](AUDITORIA_OPERACIONES.md) para estructura, operaciones, seguridad, filtros, instalación, resultados y límites. En la etapa inicial de auditoría se conservaron las consultas de Análisis y el cálculo de cuotas/deuda.

## Configuración global — 8 de octubre de 2026

La auditoría encontró el umbral fijo 2 en MembresiaLogica, clasificación/contadores de EstadoSociosLogica, filtro de EstadoSociosControl y consultas de deuda de Análisis; el plazo fijo 7 estaba en las alertas compartidas. GenerarSiguienteCuota ya respetaba la cronología y duplicados, pero no tenía horizonte de anticipación. No existía tabla o lógica equivalente a ConfiguracionSistema.

Se agregó una sola fila global con defaults 2/1/7, entidad/DbSet/repositorio EF6 y ConfiguracionSistemaLogica como fuente persistida sin caché de sesión. SysGymDB.sql incluye la tabla/fila; AgregarConfiguracionSistema.sql permite actualizar bases existentes sin modificar cuotas. Se aplicó dos veces y la base real conservó 41 cuotas, 22 pagos, 4 usuarios y auditoría vacía.

Administrador dispone de Administración → Configuración con controles permanentes en Designer. NumericUpDown, lógica y SQL comparten rangos: vencidas 1–120; anticipación 0–120 meses; aviso 0–365 días. La lógica verifica rol/usuario activos, y MODIFICAR_CONFIGURACION registra valores anteriores/nuevos solo ante cambios reales dentro de la misma transacción. Recepcionista y Entrenador no pueden editar.

Deuda y Análisis utilizan Al día / Con deuda / Límite alcanzado con el umbral actual. Solo cuentan Pendiente y FechaHasta anterior a hoy. Se conserva inactivación y reactivación exclusivamente manual. Las alertas de ambos dashboards incluyen hoy hasta hoy más los días configurados. La generación permite el período mensual actual de la secuencia real más N futuros; cambiar anticipación no elimina cuotas ni mueve el horizonte por cada inserción.

Las cuotas existentes fuera del horizonte se documentaron, sin modificarlas: Chiara Barbieri, membresía 6, cuota #38 hasta 30/04/2027, excede 5 períodos/151 días; leonardo gutierrez, membresía 31, cuota #41 hasta 22/12/2026, excede 1 período/30 días.

Pruebas en base aislada: límites 2/3, pagadas/anuladas, pago sin reactivación, reactivación manual, anticipación 0/1/2, generaciones repetidas bloqueadas, meses cortos, duplicados, alertas 7/10, no-op/lecturas sin auditoría, roles y rollback conjunto. Inicio de sesión real Admin→Recepcionista verificó menú/formulario, guardar/reabrir, historial, dashboards y pago atribuido a la sesión nueva. No se cambiaron framework, arquitectura, cálculos financieros o credenciales reales.

Ver [REGLAS_CUOTAS.md](REGLAS_CUOTAS.md) y [AUDITORIA_OPERACIONES.md](AUDITORIA_OPERACIONES.md) para reglas, instalación, históricos y verificación.

Verificación final: ConfiguracionFormulario, GestionMembresiasFormulario, EstadoSociosControl, AnalisisFormulario y PanelAdministrador abrieron en el Designer real de VS2026; se seleccionó un control, se guardó, cerró y reabrió cada documento, sin pantalla roja ni excepción. Rebuild Debug y Release: 0 errores y 0 advertencias. git diff --check: correcto. Sin commit.

## Exportación PDF de Análisis — 8 de octubre de 2026

Análisis incorpora Exportar reporte PDF junto a Aplicar. El formulario recibe al administrador autenticado del panel y conserva ResultadoAnalisis y período aplicado. El servicio ReporteAnalisisServicio recibe ese DTO y PNG de las series cargadas: no duplica consultas, modifica reglas de cuotas/deuda ni mezcla responsabilidades con ReportesPagosServicio.

El documento usa PDFsharp/MigraDoc GDI 6.2.4 ya instalado: A4, logo, período, fecha/hora, responsable real, resumen, ingresos, socios/deuda/altas, planes, ingresos por plan, entrenadores, métodos, rutinas y ejercicios. Comparación A/B se incluye solo si está cargada y vigente; se excluye el socio individual. Movimiento del período y estado actual se distinguen expresamente.

Chart.Serializer y SaveImage renderizan copias de tamaño fijo 1200×675; PNG/base64 y PDF se construyen en memoria. Los Chart originales conservan Size/Parent y la exportación no requiere maximizar o mostrar sus pestañas. Temporales del exportador: 0. El PDF se guarda mediante SaveFileDialog y GENERAR_REPORTE_ANALISIS solo se confirma si se escribe el archivo. La lógica exige Admin activo; cancelar y fallar no auditan éxito.

Pruebas aisladas verificaron texto/cantidades en PDF contra el DTO cargado, períodos con datos/vacío/varios meses, comparación opcional y cambio de límite con reaplicación. Los cinco PDFs se abrieron y renderizaron completos mediante el lector nativo de Windows. Detalles, contenido y límites están en REPORTE_ANALISIS_PDF.md.

Verificación final de esta etapa: el botón Exportar y las nueve pestañas se seleccionaron en VS2026; Análisis y PanelAdministrador abrieron, guardaron y reabrieron sin pantalla roja, NullReferenceException o error de altura. Rebuild Debug y Release: 0 errores y 0 advertencias; git diff --check correcto. La prueba final terminó con SMOKE_PDF_ANALISIS_OK, incluidos Efectivo 3/Mercado Pago 1, indicadores visibles vs texto PDF, PNG independiente del tamaño y cancelación real. Sin commit.
