# Contexto del proyecto

## UX master/detail en ejercicios, asignaciones y rutinas — 11 de septiembre de 2026

Las tres pantallas priorizadas adoptan una composición de listado y detalle: filtros compactos arriba, grilla útil a la izquierda y ficha/acciones a la derecha. `GestionEjerciciosFormulario` trabaja con el modelo real de tres campos; `GestionAsignacionesFormulario` selecciona la membresía desde la grilla y mantiene nombre/DNI/plan/vencimiento antes de confirmar; `MisSociosFormulario` muestra la rutina semanal vigente inline y conserva el editor existente para crear o editar.

La corrección posterior de layout mantiene esa composición y elimina el colapso visual: los contenedores directos del `SplitContainer` llenan ambos paneles, las fichas no dependen de su `PreferredSize` y las proporciones se preservan al cambiar entre un área equivalente a 1366x768 y otra mayor. La verificación estructural confirmó grillas llenas, selección de fila única y ausencia de colapso al agregar filas.

Se agregaron únicamente resúmenes de lectura en `MembresiaEntrenadorLogica` y `RutinaAsignacionLogica` para evitar que la capa visual consulte persistencia directamente. No se modificaron el DER, SQL Server, Entity Framework 6, navegación general ni las reglas de asignación. La verificación estructural en memoria de los tres constructores y `PerformLayout` fue exitosa; queda pendiente abrir manualmente «Ver diseñador» y probar el flujo completo con una base disponible.

## Identificacion de socios y entrenadores - 11 de septiembre de 2026

Los datos de prueba contienen nombres repetidos entre `UsuarioSistema` con rol Entrenador y `Socio` (por ejemplo, Benjamin Fernandez), con DNI diferentes. Las pantallas activas muestran nombre completo, DNI y la clave de membresia, manteniendo las claves separadas para asignaciones.

Gestion de asignaciones permite elegir una membresia desde un combo y usa su `IdMembresia`; ya no solicita escribir ese numero en un `TextBox`. Gestion de membresias permite cambiar el plan durante la edicion mediante la operacion existente `CambiarPlan`.


## Equivalencia visual de dashboards - 11 de septiembre de 2026

Se reparo la regresion visual de Administrador, Recepcionista y Entrenador. El layout base queda en 1200x760, con header `Dock=Top` de altura fija, menu de 264 px y contenido `Dock=Fill`; se quito el estado maximizado del diseno. `InicioPanelAdministrador` representa desde Designer el titulo, el pronostico y el estado de cuenta. El clima real sigue siendo exclusivamente runtime y agrega tarjetas al `FlowLayoutPanel` sin imponer posiciones.

Debug y Release compilan correctamente sin errores ni warnings reportados. Falta abrir manualmente los tres dashboards con "Ver disenador" y recorrer el flujo de membresia y asignacion contra una base disponible.

## Rutinas personalizadas — 11 de septiembre de 2026

Desde “Mis socios”, el entrenador puede crear una rutina exclusiva para un socio con membresía activa de un plan que incluya rutina personal. El administrador dispone además de los accesos de recepcionista y entrenador, incluido el editor global de rutinas. La pantalla reutiliza el editor de rutinas y asigna automáticamente la nueva rutina al socio seleccionado.

Última actualización: 16 de septiembre de 2026.

SysGym es una aplicación de escritorio en C# Windows Forms para gestionar un gimnasio. Utiliza .NET Framework 4.8, Entity Framework 6.4.4 y SQL Server.

La solución `exxen2.0.slnx` contiene un único proyecto clásico. Las carpetas `capaVisual`, `capaLogica` y `capaDatos` representan las capas dentro del mismo ensamblado.

## Menú y respaldo del pronóstico — 11 de septiembre de 2026

Los menús laterales permiten contraer y expandir sus categorías para reducir opciones visibles. El pronóstico se consulta para Corrientes Capital y se conserva localmente con el día actual y los siete siguientes; si no hay internet se presenta el último respaldo disponible.

El modelo aprobado incluye Rol, UsuarioSistema, Socio, Plan, Membresia, MembresiaEntrenador, CuotaMembresia, Pago, MetodoPago, MercadoPago, PagoEfectivo, Divisa, Rutina, RutinaEjercicio, RutinaAsignacion y Ejercicio.

La base se crea mediante `capaDatos/Database/SysGymDB.sql`. La integración real con Mercado Pago, reportes y procesos automáticos no forman parte del alcance actual.

## Estado de interfaz y fotos — 9 de septiembre de 2026

El encargo FIX_LAYOUT_CODEX.md autoriza paneles estándar con Dock estructural y Anchor para controles editables, además de Foto/Sexo opcionales en socios y usuarios. Los binarios se almacenan en SQL Server; no se crean carpetas de imágenes personales. Falta aportar los avatares de recursos y aplicar la migración en la base comercial. La auditoría estructural de los 17 formularios y el UserControl ya fue realizada; la apertura manual en «Ver diseñador» y el recorrido manual de los tres roles siguen pendientes.

## Auditoría visual estructural — 11 de septiembre de 2026

Los formularios activos conservan su estructura en `Designer.cs`, con paneles, grillas, filtros, campos y acciones visibles para el diseñador. Se corrigieron desbordes al tamaño mínimo, formularios inferiores horizontales y contenedores de detalle insuficientes. Los diez archivos legados con sufijo `Form` carecen de `Designer.cs` y no pertenecen al proyecto actual; quedan documentados como advertencia de mantenimiento, no como pantallas activas.

En cada tarea se actualizan los documentos existentes afectados, con fecha, decisiones y verificaciones pendientes, manteniendo consistencia con el código y SQL.

## Ajuste puntual de layout master/detail - 16 de septiembre de 2026

Se corrigió la regresión visual de las pantallas de ejercicios, asignaciones y socios/rutinas sin cambiar su flujo funcional. GestionEjercicios conserva una división aproximada 56/44, GestionAsignaciones 55/45 y MisSocios 33/67. Las grillas y fichas ocupan los paneles completos; la ficha de asignaciones distribuye sus siete filas con porcentaje y el listado de socios evita scroll horizontal mediante mínimos compatibles con su panel.

El layout queda en los archivos `Designer.cs`; los formularios solo cargan datos, filtros, selección y acciones. No se modificaron las clases de lógica, el esquema, SQL Server ni Entity Framework en esta corrección. Las verificaciones estructurales se realizaron en memoria para 1100/1180 px y una ventana mayor; Debug y Release compilan sin errores ni warnings.

## Catálogo de planes autorizado — 9 de septiembre de 2026

Se incorpora PlanRutina para seleccionar varias rutinas disponibles en cada plan desde la pantalla existente. La rutina base se conserva por compatibilidad. La autorización para asignar una rutina depende de este catálogo, también para Normal. Premium no incluye automáticamente futuras rutinas. Migración comercial e inspección manual del diseñador pendientes; las verificaciones funcionales se realizaron en una base separada.
