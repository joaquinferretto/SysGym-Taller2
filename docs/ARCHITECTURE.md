# Arquitectura

## Identidad en asignaciones y asistencias - 11 de septiembre de 2026

Las pantallas activas usan `IdMembresia`, `IdUsuarioSistema` e `IdSocio` como claves de las operaciones. Los combos de asignacion y asistencia muestran nombre completo y DNI, pero conservan esas claves en `ValueMember`; no se identifican personas por apellido. La pantalla de asignaciones obtiene las membresias desde `MembresiaLogica` y no exige escribir un entero manualmente.

El cambio de plan desde la gestion de membresias utiliza `MembresiaLogica.CambiarPlan`. La regla de negocio sigue siendo la misma: solo un plan activo con `IncluyeEntrenador` permite asignar o cambiar entrenador. La carga inicial de combos continua en `Load`, nunca en `InitializeComponent` ni en el diseñador.

## Correccion de equivalencia de dashboards - 11 de septiembre de 2026

Se corrigio la regresion visual de los paneles de Administrador, Recepcionista y Entrenador. El layout base queda en 1200x760, con encabezado superior de 90 px, menu lateral fijo de 264 px y contenido con `Dock=Fill`, sin `WindowState=Maximized` en el disenador. El estado inicial de las secciones colapsables tambien queda representado en Designer; solo `Load` configura datos y conecta el comportamiento.

`InicioPanelAdministrador` conserva la tarjeta meteorologica de ejemplo sin consultar servicios en tiempo de diseno. Sus bloques estaticos usan `Dock`; el listado meteorologico utiliza `FlowLayoutPanel` con flujo horizontal y desplazamiento contenido, y las tarjetas dinamicas ya no calculan coordenadas. La grilla de cuotas llena su contenedor y sus columnas visibles usan `FillWeight` y `MinimumWidth`.

Las compilaciones Debug y Release fueron exitosas sin errores ni warnings reportados. La apertura manual en Visual Studio 2022 y la prueba funcional contra una base disponible quedan pendientes de verificacion.

## Rutinas personalizadas — 11 de septiembre de 2026

El entrenador puede iniciar una rutina exclusiva desde “Mis socios”. El administrador también puede acceder a “Gestionar rutinas” y “Socios y rutinas” para crear, editar, asignar y consultar rutinas globalmente. La capa visual solicita a `RutinaLogica.CrearPersonalizada` la creación de la plantilla y su `RutinaAsignacion`; la operación se confirma en una única transacción y no se accede a `DbContext` desde la interfaz.

Última actualización: 11 de septiembre de 2026.

## Navegación desde el estado de cuenta — 11 de septiembre de 2026

La grilla del estado de cuenta conserva `IdMembresia` e `IdSocio` en columnas ocultas. El doble clic emite un evento desde `InicioPanelAdministrador`; `PanelAdministrador` recibe el identificador y abre `GestionSociosFormulario` con edición habilitada y el socio seleccionado. La navegación continúa dentro del flujo visual y no agrega acceso directo a datos desde el control.

## Menú lateral y respaldo meteorológico — 11 de septiembre de 2026

Los encabezados del menú lateral funcionan como secciones expandibles en los paneles de administrador, recepcionista y entrenador. `MenuDesplegableHelper` reacomoda los controles visibles y conserva el desplazamiento cuando el contenido supera el alto disponible. `ClimaLogica` consulta Corrientes capital y guarda ocho días en el almacenamiento local de la aplicación para utilizarlos si el servicio no responde.

El flujo permitido es:

```text
capaVisual → capaLogica → capaDatos → Entity Framework 6 → SQL Server
```

Las tres capas son carpetas dentro de `exxen2.0.csproj`; todavía no son proyectos independientes.

`capaVisual` contiene formularios y solo inicia casos de uso de `capaLogica`. El acceso a `DbContext` queda encapsulado en `capaDatos/Repositorios/UnidadDeTrabajoGimnasio.cs`. `capaLogica` contiene validaciones y casos de uso sin referencias a Windows Forms. `capaDatos` contiene entidades, relaciones, `ContextoGimnasio`, repositorios y el script de base.

La interfaz visual se organiza por rol en `capaVisual/Administrador`, `capaVisual/Recepcionista` y `capaVisual/Entrenador`. La autenticación queda en `capaVisual/Autenticacion`.

No se deben introducir patrones o tecnologías adicionales sin necesidad. El DER aprobado del proyecto tiene prioridad sobre alternativas de diseño.

## Mantenimiento de documentación

En cada tarea se deben actualizar los documentos existentes afectados, indicando fecha de última actualización, decisiones, cambios realizados y verificaciones pendientes. Código, scripts SQL y reglas de negocio deben mantenerse consistentes. Se reutilizan las guías actuales, sin crear documentos nuevos innecesarios.

## Eventos y diseñador

- Los 17 formularios heredan directamente de `Form`. `InicioPanelAdministrador` conserva el único `UserControl` existente. `ControladorNavegacion` y `AyudaFormularioVisual` son auxiliares, no clases base visuales.
- Los controles son estándar de Windows Forms, excepto ese UserControl existente integrado en el panel del administrador. No se agregan pantallas ni controles propios.
- Los handlers siguen `NombreControl_Evento`, por ejemplo `guardar_Click` o `GestionSociosFormulario_Load`. Las suscripciones de controles existentes están en `InitializeComponent`; `FormClosed` de ventanas creadas durante la navegación se suscribe al crearlas.
- Los constructores inicializan componentes y dependencias; las consultas iniciales se ejecutan desde `Load`. Los filtros usan `TextChanged`/`SelectedIndexChanged` y las grillas `SelectionChanged`.
- `InitializeComponent` es un método de instancia declarativo y serializable por el diseñador, no un método `static` de C#. Se conservan `components`, `Dispose` y los archivos parciales asociados.
- Los renombrados mantienen posiciones, tamaños, colores, fuentes, textos y composición de los controles.

### Distribución libre y adaptación al tamaño — 9 de septiembre de 2026

Se aplica el encargo `FIX_LAYOUT_CODEX.md`: los contenedores de distribución pasan a `Panel` estándar. Se eliminan `tablaOpciones` de los tres paneles de rol y `contenido` de InicioSesion, conservando sus hijos.

- El armazón usa `Dock`: encabezado superior, menú izquierdo, pie del menú inferior, opciones y contenido con Fill. Los módulos conservan encabezado, barra superior, estado inferior y contenido Fill.
- Los demás controles tienen `Dock = None`, `Location` y `Size` explícitos. Se pueden mover individualmente desde el diseñador; no hay celdas ni flujos que reorganicen sus vecinos.
- `Anchor` adapta cada control: grillas y listado a los cuatro bordes; detalle de ancho 396 a arriba, abajo y derecha; campos y buscadores a arriba, izquierda y derecha; Volver a arriba y derecha. Los formularios bajo las grillas se anclan abajo.
- Los paneles estructurales acoplados no se arrastran: se ajustan mediante Height/Width. Los campos sí se arrastran dentro de su padre. ComboBox, DateTimePicker y TextBox de una línea conservan las restricciones de altura estándar de Windows Forms.
- No se usa AutoScroll en ventanas. Solo lo tienen panelOpciones, panelDetalle y listaClima. Se adopta esta lista del criterio de aceptación ante la indicación contradictoria de habilitarlo también en principal.
- Grillas con `AutoSizeColumnsMode = Fill`, proporciones con `FillWeight` y límites con `MinimumWidth`. No asignar `Column.Width` en este modo. Son de solo lectura, selección de fila completa y columnas declaradas en Designer.
- Se mantienen colores, fuentes y textos existentes. Cambian contenedores, posiciones y anclajes por autorización expresa; no se afirma identidad píxel a píxel. No se crean formularios ni clases visuales propias.
- La tarjeta de ejemplo meteorológica conserva sus coordenadas y sirve de plantilla en ejecución. La navegación continúa acoplando el módulo abierto con Fill, sin modificar ControladorNavegacion.

Los 18 componentes visuales se inicializaron y redimensionaron en memoria; se comprobó que mover btnVolver en Planes no mueve los títulos. Esto no sustituye la inspección final en el diseñador real de Visual Studio, que sigue pendiente.

## Auditoría de diseñadores — 11 de septiembre de 2026

Se revisaron los 17 formularios y el `UserControl` de `capaVisual` que forman parte del proyecto. La verificación estructural creó cada componente con su constructor predeterminado, ejecutó `PerformLayout` en su tamaño mínimo y revisó controles fuera de los límites, superposiciones, `Dock`, `Anchor`, `AutoScroll`, `AutoScaleMode`, `InitializeComponent`, `Dispose` y balances de `SuspendLayout`/`ResumeLayout` e `ISupportInitialize`. No quedaron errores estructurales en esa pasada.

Se ajustaron encabezados y títulos de listado para respetar el ancho disponible, alturas de contenedores de detalle para evitar recortes de acciones, los formularios inferiores de asistencias/ejercicios/asignaciones a una distribución vertical, y el editor de rutinas para que sus campos y acciones respondan al ancho mínimo. El menú lateral mantiene sus controles en Designer y solo cambia su visibilidad durante `Load`, fuera del modo de diseño.

La solución utiliza `Panel` estándar con `Dock` y `Anchor`; el listado meteorológico del dashboard administrador utiliza `FlowLayoutPanel` con flujo horizontal y desplazamiento contenido. No hay `TableLayoutPanel` que requiera configuración de filas o columnas. Los diez archivos legados `*Form.cs` no tienen `Designer.cs` y no están incluidos en `exxen2.0.csproj`; se conservan fuera del flujo activo y requieren una decisión independiente si deben volver a ser pantallas del proyecto.

La apertura manual mediante «Ver diseñador» en Visual Studio 2022 sigue siendo la verificación final pendiente, porque no puede ejecutarse desde MSBuild o una consola sin iniciar el IDE.

### Fotos de socios y usuarios

Los dos formularios de gestión incorporan PictureBox estándar, selección y eliminación de foto y selección opcional de sexo. Sus eventos se suscriben en InitializeComponent. AyudaFormularioVisual comparte la selección y presentación; la lógica valida bytes y sexo y la persistencia guarda los campos opcionales. Se clonan imágenes y recursos antes de mostrarlos y se liberan las copias reemplazadas o descartadas.

Los avatares deben ser aportados por el usuario y registrados desde Visual Studio como avatarHombre, avatarMujer y avatarGenerico. Mientras falten, el control queda vacío con fondo gris claro. No se modificaron recursos ni se descargaron imágenes.

## Repositorios y nombres

`IRepositorio<T>` unifica `Consultar`, `ConsultarSoloLectura`, `Buscar`, `Primero`, `Existe` y `Agregar`. Las modificaciones operan sobre entidades con seguimiento; la unidad de trabajo confirma los cambios. Las bajas de negocio son lógicas. Se eliminaron los alias propios `Find` y `Add`; las llamadas homónimas de EF y colecciones siguen siendo las del framework.

Los nombres propios de clases, métodos, parámetros y archivos se escriben en español. Se mantienen nombres obligatorios de C#/.NET (`Main`, `Dispose`, `InitializeComponent`, `OnModelCreating`, eventos y miembros de interfaces del framework), nombres de paquetes y contratos externos. Las columnas SQL y las claves JSON conservan sus nombres mediante mapeos explícitos, sin cambiar la base ni el servicio.

Cada clase, constructor y método no generado lleva un comentario de bloque `/* */`, conciso y en castellano. Los comentarios de eventos indican qué acción los dispara y su propósito. No se agregan comentarios de esta convención a los `.Designer.cs` ni se usan comentarios XML para sustituirla.

## Catálogo de rutinas por plan — 9 de septiembre de 2026

Cambio de relación y selector visual autorizado por el usuario. Plan.RutinasDisponibles es una colección de Rutina; EF6 la persiste en PlanRutina mediante un mapeo muchos a muchos unidireccional, sin una clase visual ni un formulario nuevos. Se conserva IdRutina como rutina base por compatibilidad.

GestionPlanesFormulario conserva sus campos y agrega un CheckedListBox estándar con casillas, editable en el diseñador. Las acciones se desplazan hacia abajo dentro del detalle con desplazamiento. El combo de rutina base contiene únicamente las rutinas marcadas. ItemCheck usa NewValue para actualizarlo sin tareas diferidas: conserva la base si continúa disponible y limpia la selección si se desmarca. Sin rutinas marcadas el combo queda vacío y deshabilitado. El evento se suscribe en InitializeComponent. La grilla muestra los nombres disponibles.

Ajuste del 9 de septiembre: las casillas «Rutina personalizada» e «Incluye entrenador» ocupan el ancho del panel de beneficios para mostrar sus textos completos. Solo se amplían esos controles, manteniendo sus posiciones y estilos.

La pantalla del entrenador mantiene el catálogo para crear/editar rutinas. Al seleccionar una, su selector de membresía se filtra mediante RutinaAsignacionLogica.ListarMembresiasDisponibles: solo planes que la habilitan, con socio, membresía, plan, rutina y entrenador activos. La lógica vuelve a validar al asignar, independientemente del filtro visual. La capa visual no accede al contexto.

No se alteran otros diseños. La inicialización del formulario se verifica en memoria; queda pendiente la inspección manual en el diseñador real de Visual Studio.
