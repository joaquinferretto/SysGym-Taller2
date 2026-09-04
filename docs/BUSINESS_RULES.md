# Reglas de negocio

## Membresías y cuotas

- Una membresía pertenece a un socio, un plan y al usuario del sistema que la registra.
- Al crear una membresía se genera su primera cuota en la misma transacción.
- Las cuotas son mensuales: `FechaHasta = FechaDesde.AddMonths(1).AddDays(-1)`.
- `CuotaMembresia.Importe` conserva el precio histórico del plan.
- Una cuota sin pago debe tener `IdRegistroPago = NULL`.
- Los registros históricos se conservan y se usan bajas lógicas cuando corresponde.

## Pagos

- Solo los pagos aprobados se contabilizan.
- El importe no puede ser positivo por encima del importe de la cuota.
- `Pago` se vincula a las cuotas mediante `CuotaMembresia.IdRegistroPago`, según el DER aprobado.
- Un método de pago usa un detalle Mercado Pago o un detalle efectivo; la lógica rechaza métodos con ambos detalles.

## Roles y beneficios

- Administradores y recepcionistas activos pueden registrar membresías.
- Las asignaciones de entrenador y las rutinas requieren un usuario activo con rol Entrenador.
- Un cambio a un plan sin entrenador desactiva las asignaciones activas sin borrar la historia.
- Un plan sin rutina personalizada no permite nuevas asignaciones de rutinas.

## Datos físicos del socio

- Cuando se registra la altura, debe expresarse en metros con una parte decimal; por ejemplo, `1,80`. No se admite un valor entero.

## Flujo de rutinas

- El entrenador puede crear ejercicios mediante `EjercicioLogica`.
- El entrenador crea una rutina general mediante `RutinaLogica`; la plantilla no pertenece a un socio y puede reutilizarse.
- `RutinaAsignacionLogica` asigna una plantilla a una membresía activa cuyo plan incluya rutina personalizada. La misma plantilla puede asignarse a muchos socios.
- Los ejercicios se incorporan a la rutina mediante `RutinaEjercicioLogica`, con series, repeticiones, peso, descanso y orden.
