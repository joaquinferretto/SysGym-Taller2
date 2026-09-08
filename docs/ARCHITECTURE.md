# Arquitectura

Última actualización: 8 de septiembre de 2026.

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

### Movimiento libre autorizado — 8 de septiembre de 2026

Por pedido posterior se reemplazaron 36 contenedores `TableLayoutPanel`/`FlowLayoutPanel` por `Panel` estándar en los 17 diseñadores. Se reutilizan los contenedores existentes, sin agregar formularios ni clases visuales. Esta modificación estructural es una excepción autorizada a la restricción anterior de no modificar Designer.

Los 392 controles declarados conservan coordenadas y tamaños explícitos como punto de partida, con `Dock = None`, `AutoSize = false` y anclaje superior izquierdo. Sus posiciones no son recalculadas por tablas ni flujos. Como contrapartida, no se redistribuyen ni estiran automáticamente al redimensionar la ventana; cualquier adaptación posterior debe acordarse sin volver a impedir su edición libre.

Se ajustaron los límites iniciales de grillas, filtros, beneficios, acciones y selección de membresía que quedaban fuera de sus paneles. Las pantallas permiten desplazamiento cuando su contenido fijo supera el área disponible. Esto mantiene accesibles los controles sin introducir un evento que vuelva a imponer sus posiciones.

El movimiento es dentro del contenedor padre. Para seleccionar un contenedor detrás de sus hijos se utiliza el Esquema del documento de Visual Studio. Los elementos internos de `InicioPanelAdministrador` se editan abriendo su propio diseñador, no desde el formulario que lo contiene. Las columnas de grilla siguen siendo columnas estándar, no controles independientes con coordenadas.

El pronóstico mantiene su tarjeta de ejemplo en el diseñador. Las tarjetas de datos creadas en ejecución copian sus posiciones, tamaños y estilos; por lo tanto, la plantilla editable controla su presentación. La navegación sigue acoplando el formulario abierto al área de contenido únicamente en ejecución.

Los nombres propios de contenedores usan `contenedorCampos`, `contenedorDetalle`, `contenedorContenido` y `contenedorFormulario`. Se conservan nombres exigidos por el framework y contratos SQL/JSON existentes; traducir estos últimos exige una migración separada, no reemplazos de texto indiscriminados.

## Repositorios y nombres

`IRepositorio<T>` unifica `Consultar`, `ConsultarSoloLectura`, `Buscar`, `Primero`, `Existe` y `Agregar`. Las modificaciones operan sobre entidades con seguimiento; la unidad de trabajo confirma los cambios. Las bajas de negocio son lógicas. Se eliminaron los alias propios `Find` y `Add`; las llamadas homónimas de EF y colecciones siguen siendo las del framework.

Los nombres propios de clases, métodos, parámetros y archivos se escriben en español. Se mantienen nombres obligatorios de C#/.NET (`Main`, `Dispose`, `InitializeComponent`, `OnModelCreating`, eventos y miembros de interfaces del framework), nombres de paquetes y contratos externos. Las columnas SQL y las claves JSON conservan sus nombres mediante mapeos explícitos, sin cambiar la base ni el servicio.

Cada clase, constructor y método no generado lleva un comentario de bloque `/* */`, conciso y en castellano. Los comentarios de eventos indican qué acción los dispara y su propósito. No se agregan comentarios de esta convención a los `.Designer.cs` ni se usan comentarios XML para sustituirla.
