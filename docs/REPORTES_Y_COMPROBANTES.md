# Estado de socios y reportes de pago

Última actualización: 8 de octubre de 2026.

## Deuda y dashboard

EstadoSociosLogica comparte Al día / Con deuda / Límite alcanzado con el umbral de ConfiguracionSistema. Solo cuentan cuotas Pendiente con FechaHasta anterior a hoy; pagadas/anuladas no cuentan. Alertas: hoy hasta hoy + DiasAvisoVencimiento. Los dashboards aplican las bajas existentes; pagar no reactiva automáticamente. Ver REGLAS_CUOTAS.md.

EstadoSociosControl ofrece cuotas, pago, membresía y ficha completa. Administrador y Recepcionista usan ControladorNavegacion. La ficha consulta ObtenerSocioSoloLectura sin bajas; distingue finanzas actuales e historial de cobros. Ver FICHA_SOCIO.md.

## Servicio y datos

GestionPagosFormulario y FichaSocioFormulario reutilizan ReportesPagosServicio, PDFsharp/MigraDoc GDI 6.2.4 y Properties.Resources.SysGymLogo. El constructor recibe el ID autenticado del panel; generar y ConsultarHistorial validan Administrador/Recepcionista activos. El constructor sin parámetros conserva compatibilidad, pero no autoriza exportar sin sesión.

ConsultarHistorial y GenerarHistorial comparten consulta/proyección: pagos Aprobados asociados a cuotas Pagadas de todas las membresías, deduplicados por ID, orden fecha/ID descendentes. Includes explícitos de método, socio, plan y UsuarioRegistro.Rol, AsNoTracking y sin N+1. No se agregaron columnas/tablas.

## Comprobante individual

GenerarComprobante valida pago aprobado/cuota pagada. Incluye logo, Comprobante de pago, ID, socio, DNI, plan de la cuota, período, fecha/hora del pago, importe, método, estado, Registrado por: Nombre Apellido - Rol, generación y pie Comprobante generado por SysGym. NULL histórico: Sin información. Se amplió el servicio existente con rol y auditoría.

## Historial completo

A4 horizontal con logo, socio, DNI, plan actual, FechaAlta o Sin información y generación. Tabla: Fecha, Período, Método, Importe, Registrado por, Estado; encabezado repetido al paginar. Resumen: cantidad, total abonado, primer y último pago. No muestra pendientes como pagos. Socio sin pagos: PDF válido de cero pagos y Sin pagos registrados, exportable desde la ficha.

Responsables con la misma representación en ficha y PDFs: datos actuales de nombre/rol del registrador. NULL no se reconstruye ni atribuye a Admin.

## Archivo y auditoría

SaveFileDialog propone nombres sanitizados Comprobante_Pago_ID_Nombre_Apellido.pdf o Historial_Pagos_Nombre_Apellido.pdf. Cancelar no genera archivo/evento. Logo base64 y PDF en memoria; temporales residuales del exportador: 0. No se agregó biblioteca.

EXPORTAR_COMPROBANTE_PAGO referencia Pago/ID; EXPORTAR_HISTORIAL_PAGOS referencia Socio/ID. Éxito: exactamente un evento de la sesión real. Auditoría en transacción antes de escribir, confirmada después: fallo de escritura revierte INSERT; fallo de auditoría impide escribir. No hay transacción distribuida disco/SQL; fallo de commit posterior a escribir puede dejar archivo y devolver error. Exportar no cambia el pago.

## Verificación

Base aislada: comprobantes Admin/Efectivo, Recepcionista e histórico/Mercado Pago; historiales sin pagos, un pago y varios cobradores/NULL. Cantidad/total/orden/responsables comparados con ficha, cambio de sesión y nuevo pago. Cancelación/fallos sin auditorías ficticias. Los 22 pagos históricos reales conservan sus responsables desconocidos.

Verificación final de Ficha 360°: ocho formularios/controles abrieron, permitieron selección, guardado y reapertura en el Designer real VS2026, sin pantalla roja. Los nueve PDFs de prueba se abrieron y renderizaron completos (once páginas) con el lector nativo de Windows; logo/tablas legibles. Cancelación real de ambos diálogos: cero eventos. Selección real con mouse en Socios y responsables completos en la grilla verificados. Rebuild Debug y Release: 0 errores / 0 advertencias; git diff --check correcto. Sin commit.
