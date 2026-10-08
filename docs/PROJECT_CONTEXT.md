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

El menú Administrador incluye Consultas → Auditoría, con controles estáticos en Designer, filtros opcionales de fecha/usuario/operación/búsqueda y grilla de solo lectura. La consulta usa AsNoTracking, filtra antes de materializar y limita a las 500 coincidencias más recientes. No existe configuración global editable para auditar; no se implementó una en esta tarea.

La migración aditiva `AgregarAuditoriaOperaciones.sql` se aplicó dos veces sin duplicar estructuras ni asignar responsables ficticios a históricos. Los 22 pagos y 4 usuarios anteriores conservaron sus NULL. Se probaron las operaciones, el cambio de sesión real Admin → Recepcionista, PDFs, filtros, autorización, historial de usuarios inactivos y rollback si falla la auditoría en una base aislada. Los siete formularios revisados abrieron, guardaron y reabrieron en VS2026. Debug y Release: 0 errores y 0 advertencias; git diff --check correcto. Sin commit.

Ver [AUDITORIA_OPERACIONES.md](AUDITORIA_OPERACIONES.md) para estructura, operaciones, seguridad, filtros, instalación, resultados y límites. Las consultas de Análisis y el cálculo de cuotas/deuda se conservaron.
