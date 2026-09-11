# Arquitectura

Última actualización: 9 de septiembre de 2026.

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

- Los 16 formularios heredan directamente de `Form`. `InicioPanelAdministrador` conserva el único `UserControl` existente. `ControladorNavegacion` y `AyudaFormularioVisual` son auxiliares, no clases base visuales.
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

Los 17 componentes se inicializaron y redimensionaron en memoria; se comprobó que mover btnVolver en Planes no mueve los títulos. Esto no sustituye la inspección final en el diseñador real de Visual Studio, que sigue pendiente.

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
