# Arquitectura

## Correccion de inicializacion del diseñador en RutinasEntrenadorFormulario - 18 de septiembre de 2026

`RutinasEntrenadorFormulario.Designer.cs` conserva la distribucion master/detail existente: catalogo a la izquierda y, a la derecha, rutina seleccionada, ejercicios de la rutina y detalle del ejercicio. Se elimino el `for` que creaba las cuatro filas de `contenedorFormulario` y se declararon las cuatro `RowStyle` de 25% de forma explicita. Tambien se reemplazaron `ConfigurarTabla`, `ConfigurarBoton` y `AgregarCampo` por propiedades, `Controls.Add` y suscripciones de eventos declarativas. No quedan helpers propios ni control de flujo dentro de `InitializeComponent`; permanecen unicamente arrays estandar para `Columns.AddRange` y `Items.AddRange`.

Durante la auditoria se detecto que esa normalizacion habia omitido `components = new Container();`. El campo se mantenia declarado y `Dispose` dependia de el, pero quedaba en `null` durante la instancia del formulario. Se restauro su inicializacion estandar al comienzo de `InitializeComponent`; no se cambio el layout ni se agregaron defensas genericas. El NRE informado no pudo reproducirse con el host real de Visual Studio despues de la correccion, por lo que no se atribuye a handlers sin una traza que lo confirme.

El handler `Editar` es unico: `actualizarEjercicio_Click`. La seleccion de una fila vuelve a consultar el detalle por `IdRutinaEjercicio`, carga Ejercicio, Dia, Series, Repeticiones, Peso, Descanso y Orden, y deja el editor en consulta. `Agregar` crea mediante `RutinaEjercicioLogica.AgregarEjercicio`; `Guardar cambios` conserva `IdRutinaEjercicio` y llama a `RutinaEjercicioLogica.Modificar`; `Quitar` llama a `Quitar` despues de confirmar y solo baja la relacion. Cancelar descarta el modo nuevo o recarga la fila seleccionada en modo edicion. Las validaciones de rangos permanecen en la capa logica.

Se mantuvo `splitContenido_Resize` porque conserva la proporcion responsive actual y protege los limites de `Panel1MinSize` y `Panel2MinSize`; el valor inicial declarativo de `SplitterDistance` sigue siendo valido. La profundidad maxima de contenedores permanecio en cinco niveles y no se eliminaron contenedores que aportan `Dock`, `Padding`, `BorderStyle`, scroll, agrupacion o layout.

Debug y Release compilaron sin errores. La instancia de Visual Studio abrio realmente `RutinasEntrenadorFormulario.cs [Diseño]` despues del cambio; el constructor y `PerformLayout` tambien finalizaron en Debug y Release. La auditoria confirmo que los inicializadores de campos solo crean coordinadores de logica sin consultas ni acceso a datos; `Load` conserva la salida por `EnModoDisenio`; no se modificaron handlers de seleccion ni resize. Los unicos warnings CS0649 restantes pertenecen a `GestionEjerciciosFormulario` y `GestionSociosFormulario`. Pendiente: ejecutar contra una base de prueba controlada los seis casos funcionales solicitados; esta tarea no altero SQL Server.

## Simplificacion incremental de contenedores WinForms - 18 de septiembre de 2026

Se revisaron unicamente `GestionSociosFormulario`, `GestionMembresiasFormulario` y `GestionPagosFormulario`. En cada uno se elimino el `Panel contenedorContenido`, que solo envolvia `panelListado` y `panelDetalle` sin aportar borde, scroll, padding ni comportamiento propio. `panelContenido` paso a ser un `TableLayoutPanel` declarativo con dos columnas explicitas: listado flexible y detalle fijo de 396 px; ambos paneles son ahora hijos directos. Se conservaron los paneles que aportan borde, padding, scroll, filtro, agrupacion o distribucion de controles.

No se agregaron helpers, loops, carga de datos ni logica de negocio a los `Designer.cs`; tampoco se modificaron los archivos `.cs` de comportamiento ni las capas. Los nombres, eventos, foto, validaciones, controles internos y funcionalidad existente permanecen intactos. La comparacion de bounds de los controles visibles contra la compilacion anterior no mostro cambios.

Debug y Release compilaron con MSBuild de Visual Studio con 0 errores. La inicializacion en memoria y `PerformLayout` confirmaron en los tres formularios el `TableLayoutPanel` de dos columnas, una fila y los dos hijos directos; `git diff --check` finalizo correctamente. Permanece el warning preexistente CS0649 de `GestionEjerciciosFormulario.components`. Pendiente: abrir manualmente los tres formularios con "Ver disenador" en Visual Studio y recorrer runtime contra SQL Server; esa inspeccion interactiva no se ejecuto desde esta consola.

## Normalizacion declarativa de tres formularios WinForms - 18 de septiembre de 2026

Se normalizaron unicamente `GestionUsuariosFormulario`, `GestionPlanesFormulario` y `GestionAsignacionesFormulario`. Sus `Designer.cs` ahora contienen controles, propiedades, contenedores, filas explicitas, eventos, `BeginInit`/`EndInit` y `SuspendLayout`/`ResumeLayout`, sin helpers propios ni reconstruccion procedural de la interfaz dentro de `InitializeComponent`.

En Usuarios se eliminaron los arrays, los `Clear`, los dos loops y la reconstruccion de las nueve filas; en Planes se eliminaron `AgregarCampo` y `ConfigurarBoton`; en Asignaciones se eliminaron el loop de siete filas y los helpers de etiquetas y valores. Los archivos `.cs` de comportamiento, las capas, validaciones, eventos, foto, columnas y funcionalidad existente no se modificaron. Los formularios restantes quedan fuera de esta fase.

Debug y Release compilaron con MSBuild de Visual Studio con 0 errores. Se inicializaron los tres formularios en memoria y se verificaron 9/18 controles de la tabla de campos de Usuarios, 3/6 de Planes y 7/14 de la ficha de Asignaciones. `git diff --check` finalizo correctamente. Permanece el warning preexistente CS0649 de `GestionEjerciciosFormulario.components`. Pendientes: abrir manualmente los tres formularios con "Ver disenador" en Visual Studio y probar sus operaciones contra SQL Server; esa inspeccion interactiva no se ejecuto desde esta consola.

## Notificaciones clasicas de altas - 17 de septiembre de 2026

Ultima actualizacion de esta seccion: 17 de septiembre de 2026.

Las notificaciones de alta permanecen en `capaVisual`, mediante los Label existentes y `AyudaFormularioVisual`. El parametro opcional `resaltar` solo asigna `ForeColor` verde/rojo y `Visible = true`; las llamadas anteriores conservan su comportamiento. No se agregan servicios, eventos globales, controles personalizados ni dependencias. Los eventos compartidos conservan las operaciones existentes y solo activan el aviso coloreado donde corresponde. No se editaron los Designer en esta tarea.

Debug y Release compilaron con MSBuild de Visual Studio en una salida alternativa por bloqueo del ejecutable habitual. Se inicializaron los nueve formularios afectados y se verifico el aviso de exito en memoria. Permanece un warning CS0649 en el Designer de ejercicios previamente modificado. Pendientes: abrir los formularios en el disenador y verificar las operaciones contra SQL Server.

## Encabezado global de módulos y edición de rutinas — 17 de septiembre de 2026

Los paneles de rol (`PanelAdministrador`, `PanelRecepcionista` y `PanelEntrenador`) declaran en `Designer.cs` un encabezado de tres zonas: identidad a la izquierda, `lblModuloActual` en una columna central porcentual y «Cambiar de cuenta» a la derecha. `ControladorNavegacion` actualiza únicamente el texto central con el formato `Título | Subtítulo`; los formularios activos conservan su estructura propia en Designer, pero ocultan su franja de título local para que el contenido ocupe todo el espacio disponible.

`RutinasEntrenadorFormulario` mantiene el master/detail y separa las acciones de rutina de las acciones de ejercicios. El mismo panel `Detalle del ejercicio` trabaja en modo visualización, nuevo o edición; `Agregar`, `Guardar cambios` y `Cancelar` se habilitan según el contexto sin crear controles estructurales desde runtime.

## Encabezados compactos y edición contextual — 17 de septiembre de 2026

Los formularios activos usan un encabezado estructural declarado en `Designer.cs`, acoplado arriba con `Dock=Top`, de una sola línea y con el formato `Título | Subtítulo`; el contenido restante se mantiene con `Dock=Fill`. Reportes se incorporó al mismo patrón sin modificar su carga de datos.

GestionAsignaciones mantiene una `TableLayoutPanel` para la ficha y coloca la etiqueta y el combo de nuevo entrenador en la misma fila. Rutinas conserva su `SplitContainer`, sus tres secciones y sus acciones separadas; el editor de `RutinaEjercicio` tiene una fila estructural más amplia para facilitar la edición. La única persistencia adicional de esta pasada corresponde a la foto opcional de `UsuarioSistema`.

## Rediseño master/detail de tres módulos — 11 de septiembre de 2026

Se reorganizaron únicamente `GestionEjerciciosFormulario`, `GestionAsignacionesFormulario` y `MisSociosFormulario` con controles nativos de Windows Forms. Las tres pantallas usan `SplitContainer`: listado filtrable a la izquierda y ficha contextual a la derecha. Los formularios mantienen header, navegación y capas existentes; la carga y las operaciones continúan pasando por `capaLogica`.

## Corrección de layout master/detail — 11 de septiembre de 2026

Se corrigió la regresión visual sin modificar funcionalidad: los paneles `panelListado` y `panelDetalle`, que son los hijos directos de cada `SplitContainer`, ahora usan `Dock=Fill`. También se establecieron mínimos de panel y distancias de splitter coherentes con las proporciones objetivo: Ejercicios 56/44, Asignaciones 55/45 y Socios/Rutinas 33/67. Los `DataGridView` principales y las fichas mantienen `Dock=Fill`, `AutoSize=false` en los `TableLayoutPanel` y columnas `Fill`; el contenido vacío no altera sus límites.

El catálogo de ejercicios conserva exclusivamente Nombre, Descripción y Estado. Asignaciones muestra socio, DNI, plan, vencimiento, entrenador actual y estado, usando un resumen agregado por `MembresiaEntrenadorLogica`. Socios y rutinas muestra el contexto disponible de la membresía y la semana vigente dentro de la misma pantalla mediante `RutinaEjercicioLogica.ListarSemanaPorSocio`. No se agregaron tablas, columnas SQL, campos de dominio ni reglas de negocio.

Las grillas son de solo lectura, selección única y fila completa. La selección reemplaza la consulta redundante y habilita solo acciones válidas: alta/actualización/baja/reactivación de ejercicios, asignar/cambiar/finalizar entrenador y crear/editar/desactivar rutina. Los `TableLayoutPanel`, `GroupBox`, `Panel`, `FlowLayoutPanel` y `DataGridView` principales están declarados en los `Designer.cs`; `Load` solo consulta datos y atiende eventos, mientras el layout queda declarado en Designer.

Debug y Release deben verificarse después de esta reorganización. La apertura manual con «Ver diseñador» en Visual Studio y el recorrido contra una base disponible continúan siendo verificaciones pendientes.

## Identidad en asignaciones - 11 de septiembre de 2026

Las pantallas activas usan `IdMembresia`, `IdUsuarioSistema` e `IdSocio` como claves de las operaciones. Los combos de asignacion muestran nombre completo y DNI, pero conservan esas claves en `ValueMember`; no se identifican personas por apellido. La pantalla de asignaciones obtiene las membresias desde `MembresiaLogica` y no exige escribir un entero manualmente.

El cambio de plan desde la gestión de membresías utiliza `MembresiaLogica.CambiarPlan`. Todos los planes incluyen entrenador; la asignación vigente se registra en `MembresiaEntrenador`, no en una propiedad del plan. La carga inicial de combos continúa en `Load`, nunca en `InitializeComponent` ni en el diseñador.

## Correccion de equivalencia de dashboards - 11 de septiembre de 2026

Se corrigio la regresion visual de los paneles de Administrador, Recepcionista y Entrenador. El layout base queda en 1200x760, con encabezado superior de 90 px, menu lateral fijo de 264 px y contenido con `Dock=Fill`, sin `WindowState=Maximized` en el disenador. El estado inicial de las secciones colapsables tambien queda representado en Designer; solo `Load` configura datos y conecta el comportamiento.

`InicioPanelAdministrador` muestra el pronostico y el estado de cuenta sin bloques de muestra. Sus bloques estaticos usan `Dock`; el listado meteorologico utiliza `FlowLayoutPanel` con flujo horizontal y desplazamiento contenido, y las tarjetas dinamicas no dependen de una tarjeta ficticia. La grilla de cuotas llena su contenedor y sus columnas visibles usan `FillWeight` y `MinimumWidth`.

Las compilaciones Debug y Release fueron exitosas sin errores ni warnings reportados. La apertura manual en Visual Studio 2022 y la prueba funcional contra una base disponible quedan pendientes de verificacion.

## Rutinas personalizadas — 11 de septiembre de 2026

El entrenador puede crear rutinas reutilizables desde “Gestionar rutinas” y asignarlas desde “Mis socios”. El administrador también puede crear, editar, asignar y consultar rutinas. Cada membresía conserva cero o una rutina mediante `Membresia.IdRutina`; crear una rutina personalizada la vincula a la membresía seleccionada sin una entidad de asignación adicional. La capa visual solicita las operaciones a `RutinaLogica` y no accede a `DbContext`.

Última actualización: 17 de septiembre de 2026.

## Estabilización de validaciones y rutinas — 17 de septiembre de 2026

Las reglas reutilizables de nombres, DNI y edad se centralizan en `ValidacionesGimnasio`; la capa visual agrega restricciones inmediatas de teclado y mensajes de advertencia, pero la capa lógica sigue siendo la validación definitiva. El formulario de usuarios incluye la fecha de nacimiento para aplicar el mínimo de 18 años.

`RutinasEntrenadorFormulario` mantiene el catálogo arriba y concentra en un detalle inferior la grilla de ejercicios y el formulario de edición. La estructura permanece en `Designer.cs`; las acciones de reactivación, actualización y baja de ejercicios llaman a `RutinaLogica` y `RutinaEjercicioLogica` sin cambiar el modelo de rutinas.

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

Los contenedores de distribución de los paneles de rol usan `Panel` estándar. Se eliminan `tablaOpciones` de los tres paneles y `contenido` de InicioSesion, conservando sus hijos.

- El armazón usa `Dock`: encabezado superior, menú izquierdo, pie del menú inferior, opciones y contenido con Fill. Los módulos conservan encabezado, barra superior, estado inferior y contenido Fill.
- Los demás controles tienen `Dock = None`, `Location` y `Size` explícitos. Se pueden mover individualmente desde el diseñador; no hay celdas ni flujos que reorganicen sus vecinos.
- `Anchor` adapta cada control: grillas y listado a los cuatro bordes; detalle de ancho 396 a arriba, abajo y derecha; campos y buscadores a arriba, izquierda y derecha; Volver a arriba y derecha. Los formularios bajo las grillas se anclan abajo.
- Los paneles estructurales acoplados no se arrastran: se ajustan mediante Height/Width. Los campos sí se arrastran dentro de su padre. ComboBox, DateTimePicker y TextBox de una línea conservan las restricciones de altura estándar de Windows Forms.
- No se usa AutoScroll en ventanas. Solo lo tienen panelOpciones, panelDetalle y listaClima. Se adopta esta lista del criterio de aceptación ante la indicación contradictoria de habilitarlo también en principal.
- Grillas con `AutoSizeColumnsMode = Fill`, proporciones con `FillWeight` y límites con `MinimumWidth`. No asignar `Column.Width` en este modo. Son de solo lectura, selección de fila completa y columnas declaradas en Designer.
- Se mantienen colores, fuentes y textos existentes. Cambian contenedores, posiciones y anclajes por autorización expresa; no se afirma identidad píxel a píxel. No se crean formularios ni clases visuales propias.
- El dashboard conserva el pronostico y el estado de cuenta como contenido principal. La navegación continúa acoplando el módulo abierto con Fill, sin modificar ControladorNavegacion.

Los 18 componentes visuales se inicializaron y redimensionaron en memoria; se comprobó que mover btnVolver en Planes no mueve los títulos. Esto no sustituye la inspección final en el diseñador real de Visual Studio, que sigue pendiente.

## Auditoría de diseñadores — 11 de septiembre de 2026

Se revisaron los 17 formularios y el `UserControl` de `capaVisual` que forman parte del proyecto. La verificación estructural creó cada componente con su constructor predeterminado, ejecutó `PerformLayout` en su tamaño mínimo y revisó controles fuera de los límites, superposiciones, `Dock`, `Anchor`, `AutoScroll`, `AutoScaleMode`, `InitializeComponent`, `Dispose` y balances de `SuspendLayout`/`ResumeLayout` e `ISupportInitialize`. No quedaron errores estructurales en esa pasada.

Se ajustaron encabezados y títulos de listado para respetar el ancho disponible, alturas de contenedores de detalle para evitar recortes de acciones, los formularios inferiores de ejercicios/asignaciones a una distribución vertical, y el editor de rutinas para que sus campos y acciones respondan al ancho mínimo. El menú lateral mantiene sus controles en Designer y solo cambia su visibilidad durante `Load`, fuera del modo de diseño.

La solución utiliza `Panel` estándar con `Dock` y `Anchor`; el listado meteorológico del dashboard administrador utiliza `FlowLayoutPanel` con flujo horizontal y desplazamiento contenido. `RutinasEntrenadorFormulario` usa `TableLayoutPanel` para separar el listado superior del detalle y distribuir la grilla de ejercicios y sus campos. Las variantes antiguas `*Form.cs` se eliminaron tras comprobar que estaban excluidas del proyecto, no tenían consumidores y contaban con reemplazos activos.

La apertura manual mediante «Ver diseñador» en Visual Studio 2022 sigue siendo la verificación final pendiente, porque no puede ejecutarse desde MSBuild o una consola sin iniciar el IDE.

### Fotos de socios y usuarios

Los dos formularios de gestión incorporan `PictureBox` estándar, selección y eliminación de foto y selección opcional de sexo. `AyudaFormularioVisual` y `AlmacenamientoImagenes` comparten selección, validación, nombres GUID, rutas relativas y carga sin mantener bloqueados los archivos. Sin foto personalizada se reutilizan los avatares embebidos `socio_hombre_default` y `socio_mujer_default`.

`Socio.FotoRuta` y `UsuarioSistema.FotoRuta` almacenan únicamente rutas relativas a `Datos/Imagenes/Socios` y `Datos/Imagenes/Usuarios`. El DDL limpio usa esas columnas; la columna binaria histórica `UsuarioSistema.Foto` puede permanecer en una base local existente sin ser mapeada por EF6, evitando una eliminación destructiva.

## Distribución UX/UI de formularios - 17 de septiembre de 2026

Los formularios principales mantienen la separación de responsabilidades: `Designer.cs` declara la estructura visual, Dock, tamaños base y proporciones; `Form.cs` carga datos, atiende eventos y actualiza estados contextuales. Los paneles de rol se declaran maximizados y los módulos continúan integrándose mediante `Dock=Fill`.

`RutinasEntrenadorFormulario` usa un `SplitContainer` con una proporción aproximada de 32% para el listado y 68% para el detalle, recalculada sobre el ancho disponible y limitada por `Panel1MinSize`/`Panel2MinSize`. El panel derecho usa filas estructurales para ficha, ejercicios y editor. `GestionUsuariosFormulario` usa una distribución de tabla para sus campos y conserva la foto en un área visible con zoom. No se agregan dependencias ni se modifica el flujo por capas.

## Repositorios y nombres

`IRepositorio<T>` unifica `Consultar`, `ConsultarSoloLectura`, `Buscar`, `Primero`, `Existe` y `Agregar`. Las modificaciones operan sobre entidades con seguimiento; la unidad de trabajo confirma los cambios. Las bajas de negocio son lógicas. Se eliminaron los alias propios `Find` y `Add`; las llamadas homónimas de EF y colecciones siguen siendo las del framework.

Los nombres propios de clases, métodos, parámetros y archivos se escriben en español. Se mantienen nombres obligatorios de C#/.NET (`Main`, `Dispose`, `InitializeComponent`, `OnModelCreating`, eventos y miembros de interfaces del framework), nombres de paquetes y contratos externos. Las columnas SQL y las claves JSON conservan sus nombres mediante mapeos explícitos, sin cambiar la base ni el servicio.

Cada clase, constructor y método no generado lleva un comentario de bloque `/* */`, conciso y en castellano. Los comentarios de eventos indican qué acción los dispara y su propósito. No se agregan comentarios de esta convención a los `.Designer.cs` ni se usan comentarios XML para sustituirla.

## Modelo de rutinas — 16 de septiembre de 2026

`Membresia.IdRutina` es nullable: una membresía puede no tener rutina o tener una. La FK desde `Membresia` permite reutilizar una misma plantilla `Rutina` en varias membresías. `RutinaEjercicio` continúa asociando cada plantilla con sus ejercicios. La selección y el cambio de rutina actualizan únicamente `Membresia.IdRutina`; no eliminan la rutina anterior ni sus ejercicios. Los planes no mantienen catálogos, rutinas base ni indicadores de beneficios.

La relación se configura en EF6 y en el DDL limpio de `capaDatos/Database/SysGymDB.sql`. No hay una entidad intermedia para asignar rutinas.

## Ajuste puntual de layout master/detail - 16 de septiembre de 2026

Se corrigió exclusivamente la distribución de GestionEjerciciosFormulario, GestionAsignacionesFormulario y MisSociosFormulario. Los tres mantienen SplitContainer con ambos paneles en `Dock=Fill`; sus proporciones base son aproximadamente 56/44, 55/45 y 33/67 respectivamente. La grilla principal de cada listado conserva `ReadOnly`, `FullRowSelect`, selección única, sin encabezado de filas y columnas en modo `Fill`.

La ficha de asignaciones usa filas `Percent` para aprovechar la altura completa. El listado de socios reduce sus `MinimumWidth` para que sus seis columnas visibles entren en el panel de un tercio sin scroll horizontal accidental. `SplitterDistance` queda declarado en Designer y no se reasigna desde constructor o `Load`. No se modificaron lógica, DER, SQL ni Entity Framework. Debug y Release fueron compilados correctamente; queda pendiente la apertura manual en Visual Studio y la prueba funcional contra una base disponible.

## Imágenes portables y catálogo de ejercicios — 17 de septiembre de 2026

La foto de `Socio` se almacena como `FotoRuta` nullable, siempre relativa a `Datos`; las imágenes personalizadas se copian a `Datos/Imagenes/Socios` con nombre GUID. Cuando la ruta no existe, `AyudaFormularioVisual` muestra los recursos embebidos `socio_hombre_default` o `socio_mujer_default` sin dejar archivos bloqueados.

`EjercicioImagen` es una entidad 1:N de `Ejercicio`, con `RutaRelativa` y `Orden`. `GestionEjerciciosFormulario` mantiene sus controles estructurales en `Designer.cs` y crea únicamente thumbnails según la cantidad de imágenes. La lógica guarda y quita relaciones y archivos administrados; todavía no existe exportación a PDF.
