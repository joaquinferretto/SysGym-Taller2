# Arquitectura

## Unificación de Ejercicios y Socios y Rutinas — 21/09/2026

Última actualización: 21 de septiembre de 2026. Ambos formularios conservan un SplitContainer con listado filtrable a la izquierda y ficha a la derecha, con estilo basado en RutinasEntrenadorFormulario. MisSocios integra los controles directamente en los paneles del splitter; se retiran seis wrappers sin eliminar controles funcionales. Títulos, campos, grillas, botones y ErrorProvider siguen declarados en Designer. Ejercicios conserva la galería FlowLayoutPanel para miniaturas variables y los cambios visuales recuperados. El mínimo del splitter es 862×420 y el del formulario 916×560 para evitar desbordamiento por los márgenes.

No cambian comportamiento, recursos, parciales, dependencias, persistencia ni reglas. Debug/Release Rebuild y ejecución con lecturas pasaron; los dos Designer abrieron en VS2026. Evidencia, límites de la captura del Designer y revisión visual pendiente en PROJECT_CONTEXT.md. Esta sección reemplaza las proporciones y wrappers históricos de estos dos formularios.


## Validaciones de formularios — 21/09/2026

Las reglas reutilizables de nombre personal, DNI, nombre de usuario y edad permanecen en `ValidacionesGimnasio`. `AyudaFormularioVisual` traduce esas reglas a `ErrorProvider` y agrega parseo visual de decimales, enteros, combos y enfoque del primer error. Los formularios configuran eventos y repiten la validación completa antes de invocar lógica; `KeyPress` no se considera una barrera definitiva.

Los diez formularios editables auditados declaran un único `ErrorProvider` no visual dentro de `components`. Los `Designer` conservan construcción declarativa y el componente usa `NeverBlink`, `ContainerControl` y el ciclo estándar `BeginInit/EndInit`. La validación ignora controles invisibles, deshabilitados y `TextBox ReadOnly`, por lo que las fichas informativas no bloquean operaciones. El flujo continúa siendo `capaVisual → capaLogica → capaDatos → EF6 → SQL Server`; no se trasladaron reglas definitivas a Windows Forms.

La matriz completa por formulario y campo está en `docs/VALIDACIONES_FORMULARIOS.md`.

## Convención definitiva de capas visuales — 21/09/2026

La organización adoptada específicamente por SysGym es:

```text
capaVisual
    → Forms / UserControls / Designer / resx
capaLogica
    → lógica de negocio
    → Navegacion
    → Utilidades
capaDatos
    → entidades / Entity Framework / repositorios / unidad de trabajo
SQL Server
    → persistencia
```

`capaVisual` contiene exclusivamente pantallas y controles visuales reales. Los formularios compartidos permanecen en `capaVisual/Compartido/Formularios`. `ControladorNavegacion` e `ISesionPanel` están en `capaLogica/Navegacion`; `AyudaFormularioVisual`, `ImagenSeleccionada` y `MenuDesplegableHelper` están en `capaLogica/Utilidades`.

En este proyecto, `capaLogica` comprende reglas de negocio y comportamiento reutilizable de aplicación que no representa una pantalla ni persistencia. Las subcarpetas Navegacion y Utilidades dependen conscientemente de Windows Forms para coordinar Forms, Panel, mensajes, validaciones visuales y menús. No pertenecen al dominio puro y se mantienen separadas de SocioLogica, MembresiaLogica, PagoLogica y las demás clases de negocio. Esta es una decisión práctica de SysGym para que `capaVisual` quede compuesta únicamente por Form/UserControl y sus archivos asociados; no es una regla arquitectónica universal.

`InicioPanelAdministrador` conserva la estructura declarativa del UserControl. Su TableLayoutPanel de pronóstico usa siete pesos porcentuales iguales y `CellBorderStyle.Single`; los controles de cada día continúan siendo dinámicos porque dependen de los datos meteorológicos. No se agregaron contenedores, servicios ni reglas de clima.

## Imágenes centralizadas y membresías opcionalmente asignadas — 21/09/2026

Las tres cargas de imágenes siguen `capaVisual → capaLogica → AlmacenamientoImagenes/ProcesadorImagenes → archivo administrado`, mientras EF6 conserva únicamente la ruta relativa. Los formularios seleccionan bytes, muestran el preview normalizado y comunican errores; el procesamiento técnico único valida firma y decodificación, corrige EXIF, normaliza a 800×800, recodifica y guarda mediante GUID. SocioLogica, UsuarioSistemaLogica y EjercicioImagenLogica coordinan persistencia y limpieza segura. La capa visual no usa contexto, repositorios ni SQL.

`MembresiaLogica.Crear` registra membresía y primera cuota en una transacción sin crear una asignación. La relación opcional se agrega después exclusivamente mediante `MembresiaEntrenadorLogica`; GestionAsignacionesFormulario presenta las membresías sin relación como «Sin asignar». Se mantiene el flujo Visual → Lógica → Unidad de trabajo/repositorios → EF6 → SQL Server.

## Exportación de rutinas y consulta de entrenadores — 20/09/2026

La exportación respeta `MisSociosFormulario → RutinaExportacionLogica → UnidadDeTrabajoGimnasio/repositorios → EF6 → SQL Server`. La consulta materializa una instantánea de solo lectura con socio, membresía, rutina, ejercicios y rutas de imágenes; la capa visual no usa contexto, repositorios, SQL ni celdas del DataGridView como fuente. Luego `ExportadorRutinaPdf` recibe esos datos y genera el archivo, sin referencias a WinForms. Las rutas de `EjercicioImagen` se resuelven mediante `AlmacenamientoImagenes` y no se persisten cambios. PDFsharp-MigraDoc-GDI 6.2.4 aporta documento, paginación e imágenes y es compatible con .NET Framework 4.8.

La consulta de Recepcionista sigue `ConsultaEntrenadoresFormulario → ConsultaEntrenadoresLogica → UnidadDeTrabajoGimnasio/repositorios → EF6 → SQL Server`. Sus dos operaciones usan `ConsultarSoloLectura`: listar usuarios con rol Entrenador y listar asignaciones activas del entrenador seleccionado con socio, plan y rutina. La pantalla no reutiliza GestionAsignacionesFormulario ni ofrece mutaciones. `PanelRecepcionista` conserva ControladorNavegacion y ahora dirige Consultar entrenador al formulario específico.

## Paneles de Entrenador y Recepcionista — 20/09/2026

Última actualización: 20 de septiembre de 2026. Ambos paneles siguen la composición de PanelAdministrador: cuatro Panel estructurales (encabezado, lateral, opciones con scroll y superficie de navegación), con controles estáticos declarados directamente en Designer. Entrenador ya tenía esa estructura; se corrigieron docking y medidas pendientes. Recepcionista elimina layoutEncabezado, panelIdentidad y panelPie, incorpora título/subtítulo y Volver mediante ControladorNavegacion existente y conserva todos sus accesos propios. Form.cs solo coordina comportamiento y navegación; no se introducen controles estáticos dinámicos ni cambios de negocio o persistencia. PanelAdministrador y los helpers compartidos no se modifican.

Designer real VS2026 con edición reversible y guardado en ambos paneles, navegación con datos mediante las capas existentes y compilación Debug/Release verificados. Alcance, evidencias, limitación de altura de la pantalla y revisión visual pendiente del usuario en la primera sección de PROJECT_CONTEXT.md. PDF expresamente pendiente, sin nueva dependencia.

## Pulido de Socios y rutinas — 20/09/2026

MisSociosFormulario mantiene listado izquierdo, ficha y rutina semanal derecha. El único contenedor retirado en esta pasada es accionesRutina (FlowLayoutPanel): mensaje, combo y botones existentes quedan directamente en grupoRutina, mediante Location/Size/Anchor. La tabla estructural de ficha conserva sus cuatro columnas, con etiquetas más anchas y valores equilibrados. No se agregan contenedores ni layout runtime, y Form.cs/eventos/servicios permanecen intactos. El encabezado local sigue oculto y el global sigue a cargo de la navegación existente. Debug/Release y geometría de tres resoluciones comprobados; Designer/runtime visual pendientes por limitación de acceso al escritorio. Detalle vigente en PROJECT_CONTEXT.md.

## Auditoría de comunicación entre capas — 20/09/2026

Alcance: inspección estática de los archivos Compile incluidos en exxen2.0.csproj: 37 de capaVisual (incluye Designer), 15 de capaLogica y 20 de capaDatos. Se contrastaron dependencias, llamadas desde formularios, interfaces públicas de lógica, repositorios y contexto; no se ejecutaron operaciones de negocio ni SQL. DashboardInicioAdministrador.cs existe fuera del csproj y no se cuenta como código activo.

**Resultado:** no se encontraron accesos directos de capaVisual a conexiones SQL, ContextoGimnasio, DbContext, repositorios, UnidadDeTrabajo o SaveChanges. No se encontraron SQL/DbContext ni referencias a WinForms/MessageBox/capaVisual en capaLogica. No se encontraron dependencias de capaDatos hacia capaLogica o capaVisual. El recorrido de persistencia inspeccionado es capaVisual → capaLogica → UnidadDeTrabajoGimnasio/repositorios (capaDatos) → ContextoGimnasio/EF6 → SQL Server.

| Pantalla / operación | Entrada de lógica | Persistencia |
| --- | --- | --- |
| InicioSesion | UsuarioSistemaLogica.Autenticar | UsuariosSistema, consulta con Rol |
| Usuarios / roles | UsuarioSistemaLogica, RolLogica | UsuariosSistema, Roles |
| Socios | SocioLogica.Crear/Modificar/ObtenerPorId/ListarParaGestion | Socios |
| Planes | PlanLogica | Planes |
| Membresías | MembresiaLogica, PlanLogica, CuotaMembresiaLogica | Membresias, Planes, CuotasMembresia |
| Cuotas y pagos | PagoLogica, CuotaMembresiaLogica, MembresiaLogica | Pagos, métodos, cuotas y membresías |
| Asignar entrenador | MembresiaEntrenadorLogica, UsuarioSistemaLogica | MembresiasEntrenadores, usuarios |
| Ejercicios e imágenes | EjercicioLogica, EjercicioImagenLogica | Ejercicios, EjercicioImagenes |
| Gestionar rutinas | RutinaLogica, RutinaEjercicioLogica, EjercicioLogica | Rutinas, RutinaEjercicios, Ejercicios |
| Socios y rutinas / semana | RutinaLogica, RutinaEjercicioLogica | Membresías, rutinas y detalles |
| Consulta de rutinas | RutinaLogica.ListarActivas | Rutinas |
| Inicio / estado de cuentas | CuotaMembresiaLogica.ListarEstadoCuentas | Cuotas y membresías |
| Reportes | SocioLogica, UsuarioSistemaLogica, EjercicioLogica, RutinaLogica, MembresiaLogica | Listas materializadas y conteos en presentación |

Matices de la arquitectura actual:

- Visual sí referencia `capaDatos.Entidades` para recibir/enviar objetos y enumeraciones. Esto acopla sus tipos al modelo persistente, pero no abre consultas ni salta capaLogica. EF tiene lazy loading y proxies deshabilitados. Las interfaces públicas de lógica revisadas devuelven listas/entidades/resultados, no IQueryable ni unidades de trabajo. Un aislamiento estricto de tipos requeriría DTO/contratos; no se realizó ese refactor en esta auditoría.
- LINQ sobre repositorios en capaLogica expresa filtros/reglas que EF ejecuta a través de capaDatos. LINQ sobre listas materializadas en Visual filtra/presenta información, no consulta SQL directamente.
- ContextoGimnasio.ProbarConexion usa SqlConnection para abrir/cerrar y seleccionar conexión principal/respaldo. Es una excepción técnica ubicada en capaDatos; no ejecuta CRUD por ADO.NET. La persistencia de entidades sigue usando EF6.
- Varias funciones de consulta tienen efectos de negocio: CuotaMembresiaLogica.ListarParaGestion/ListarEstadoCuentas evalúan deuda y llaman GuardarCambios; MembresiaLogica.ObtenerPorSocio/ListarHabilitadas/ListarParaGestion y RutinaEjercicioLogica.ListarSemanaPorSocio también pueden actualizar estados. No viola el recorrido de capas, pero NO deben considerarse lecturas puras durante pruebas. No se alteraron estas reglas.
- Las capas son carpetas/namespaces dentro de un único proyecto, no ensamblados con restricciones de referencia. La separación depende actualmente de las convenciones del código. No se separaron proyectos ni se agregaron dependencias.

No fue necesario corregir código para el recorrido solicitado. El resultado es una auditoría estática del estado actual, no una certificación de todas las reglas de negocio ni de cada ejecución runtime. Se conservan todos los cambios visuales anteriores y no se hace commit.

## Asignar Entrenador: controles directos — 19 de septiembre de 2026

`GestionAsignacionesFormulario` reduce siete contenedores propios a un único `SplitContainer` estructural. Panel1 contiene directamente título, búsqueda, filtro, recarga y grilla; Panel2 contiene directamente título, mensaje, etiquetas, seis campos de lectura, combo y tres acciones. Se eliminan encabezado oculto, tabla de filtros/listado/ficha, GroupBox y FlowLayoutPanel de acciones. El propio SplitContainer aporta división y bordes; sus dos regiones blancas usan Padding/Anchor/Location/Size y ancho estable de ficha, siguiendo Planes/Membresías sin agregar wrappers. `lblEstado` conserva mensajes funcionales con texto inicial vacío. El archivo funcional .cs, consultas, reglas y capas inferiores permanecen intactos. Designer real con edición reversible/guardado, Document Outline y runtime de lectura en tres resoluciones verificados; alcance y evidencias en PROJECT_CONTEXT.md. Próxima etapa sujeta a revisión visual del usuario.

## Normalización de Inicio administrador y Planes — 19 de septiembre de 2026

`InicioPanelAdministrador` mantiene su estructura plana y sus cargas dinámicas. Su metadata de diseño se alinea con la herencia real: `DesignerCategory("UserControl")` y `<SubType>UserControl</SubType>`. Esto permite que Visual Studio use `UserControlDocumentDesigner`; no cambia el constructor, el clima, el estado de cuentas ni la navegación por doble clic.

`GestionPlanesFormulario` conserva un único `SplitContainer` para expresar la relación listado/ficha. La izquierda contiene directamente título, ayuda, buscador, filtro y grilla; la derecha contiene directamente título de modo, Nombre, Descripción, Precio y las cinco acciones. Siguen eliminados los cinco contenedores intermedios y los controles del encabezado local oculto. En la revisión visual del 19/09/2026 se tomó explícitamente `GestionMembresiasFormulario` como referencia: `SplitContainer.BorderStyle=FixedSingle`, ambos paneles blancos, padding de 16 px, tipografías y colores equivalentes y acciones en dos filas. `FixedPanel.Panel2` conserva el ancho del editor mientras crece el listado; los anclajes mantienen campos y grilla dentro de sus regiones. No se añaden contenedores, controles dinámicos ni cálculos de layout en Form.cs. `lblEstado` permanece porque informa conteos, validaciones y resultados. No cambian el archivo funcional del formulario, `PlanLogica`, entidades, Entity Framework, SQL ni las dependencias entre capas. Debug/Release, Designer real con selección/movimiento/edición reversible y runtime de lectura verificados; tamaños y límites de la prueba en PROJECT_CONTEXT.md.

## Header global y retorno al inicio — 19 de septiembre de 2026

PanelAdministrador usa un único Panel para el header violeta y controles directos: logo, identidad completa, título, subtítulo, Volver y Cambiar de cuenta. Eliminados layoutEncabezado y panelIdentidad, que restringían la identidad a 250 px; no se agregan consultas de usuario/rol. Título/subtítulo se separan del texto existente mediante el separador `|`. Volver usa ControladorNavegacion.VolverAlInicio: cierra el módulo actual y deja que su evento FormClosed restaure el inicio existente. En inicio permanece deshabilitado. Cambiar de cuenta y Salir conservan sus funciones distintas. panelPie se elimina y Salir queda directo en la región lateral; panelOpciones conserva scroll de menú. Sin cambios de negocio o persistencia. Registro incremental vigente en PROJECT_CONTEXT.md.

## Regla vigente: estructura plana de Rutinas — 19 de septiembre de 2026

Última actualización: 19 de septiembre de 2026. Esta decisión reemplaza las autorizaciones históricas de FlowLayoutPanel para botones y cajas de secciones que aparecen más abajo. Los botones y campos deben ser hijos directos de una región estructural, sin contenedores específicos de acciones o campos.

`RutinasEntrenadorFormulario` conserva únicamente `panelContenido` (TableLayoutPanel principal 30/70) y `panelDetalle` (Panel con scroll de toda la región derecha). Título del listado, grilla y Nueva rutina son hijos directos de la tabla principal; los 31 controles del detalle son hijos directos del Panel. Se utilizan Location, Size y Anchor declarativos, sin reconstrucción visual en Form.cs. Se eliminan diez contenedores: dos Panel decorativos, cinco tablas intermedias y tres FlowLayoutPanel de acciones. Ambas grillas conservan columnas, FillWeight y eventos.

No había StatusStrip en este formulario. El Label `lblEstado` conserva resultados y errores funcionales; se elimina su texto inicial «Listo» y su altura baja de 46 a 26 px. No cambian Form.cs, navegación, lógica, persistencia, SQL, EF6 ni dependencias entre capas. Las verificaciones actuales y sus límites se registran en PROJECT_CONTEXT.md; las pruebas históricas no acreditan esta versión.

## Reanudación de la normalización visual — 19 de septiembre de 2026

Última actualización: 19 de septiembre de 2026. En `RutinasEntrenadorFormulario`, la región principal pasa a TableLayoutPanel 30/70 con Dock=Fill en listado y detalle; sus controles permanecen directamente en las tablas declarativas de sus secciones. La fila de acciones de ejercicios usa AutoSize para evitar recortes y el label de estado no tiene un fondo propio. Un único Panel con AutoScroll contiene toda la región derecha y mantiene accesibles los campos/acciones y una altura útil de grilla en 720p; no envuelve un control individual. No se modifica Form.cs, navegación, header global, sidebar, dependencias, EF6, SQL ni reglas. Panel 3→3, TableLayoutPanel 5→6, FlowLayoutPanel 3→3 y SplitContainer 0→0 respecto del estado recibido.

Debug y layout auxiliar de los tres modos y cuatro resoluciones comprobados, incluyendo ausencia de superposición, altura útil de grilla y acceso por scroll a Guardar. Diseñador real y propiedades individuales de Label/TextBox/ComboBox/Button/DataGridView inspeccionados en Visual Studio 2026; compatibilidad específica con 2022 pendiente. La prueba funcional está bloqueada por un esquema local anterior sin FotoRuta/IdRutina; no se continúa a otros formularios sin cerrar la verificación requerida o recibir una excepción explícita del usuario. Detalle, inventario de los 17 formularios activos y punto de reanudación en PROJECT_CONTEXT.md.

## Layout de Gestión de socios — 18 de septiembre de 2026

Última actualización: 18 de septiembre de 2026. Ajuste exclusivo de GestionSociosFormulario.Designer.cs. panelContenido reutiliza sus dos columnas con 50%/50%; panelListado conserva Dock=Fill y panelDetalle pasa a Dock=Fill. El scroll procedía de panelDetalle con AutoScroll=true, altura fija y contenido que superaba sus límites. contenedorDetalle pasa a Dock=Fill y agrupa también foto, botones de foto y rutina semanal. Se corrigen anchos de contenedorCampos/panelAcciones y campos anclados a izquierda/derecha, y se compactan filas y posiciones desde Designer. AutoScroll=false tras comprobar que los controles caben en los tamaños verificados. Sin cálculos runtime, clases nuevas, cambios de eventos, DataGridView, negocio, datos ni dependencias entre capas. Verificaciones y límites en PROJECT_CONTEXT.md.


## Tarjetas de Reportes — 18 de septiembre de 2026

Última actualización: 18 de septiembre de 2026. ReportesFormulario reemplaza el Label resumen con borde por cinco Panel estándar, cada uno con Label de título y valor. Un único TableLayoutPanel de seis columnas porcentuales y dos filas organiza tres tarjetas arriba y dos centradas abajo, mediante ColumnSpan=2. Se declara todo en Designer; Dock y márgenes resuelven la distribución, sin Resize, controles personalizados ni creación runtime fuera de InitializeComponent. Los valores usan Segoe UI 32 negrita violeta, títulos de 10 y fondo blanco. lblEstado se conserva debajo de las tarjetas; generar y su suscripción Click se mantienen.

El handler consulta los mismos cinco contadores, una vez cada uno y en el mismo orden, antes de asignarlos a los cinco Labels. Esto conserva que un error de consulta no actualice parcialmente los indicadores. La fecha y manejo de errores permanecen iguales. No cambian negocio, persistencia ni dependencias entre capas. Verificaciones en PROJECT_CONTEXT.md.


## Columnas ajustadas al ancho disponible — 18 de septiembre de 2026

Última actualización: 18 de septiembre de 2026. Ajuste exclusivo de columnas en los Designer de MisSociosFormulario (tabla de socios activos) y RutinasEntrenadorFormulario (tabla y tablaEjercicios). Se conservan AutoSizeColumnsMode=Fill, Dock=Fill y ScrollBars sin cambios. Se reducen los MinimumWidth de las 16 columnas visibles afectadas a 20 px para que sus mínimos no obliguen a desplazar horizontalmente. Los IDs ocultos y tablaRutina de MisSocios permanecen intactos.

Socios conserva FillWeight 28/16/20/22/16/16 (Socio/DNI/Plan/Rutina/Vence/Estado). Catálogo usa 45/35/20 (Rutina/Entrenador/Estado). Ejercicios usa 10/7/32/8/17/8/18 (Día/Orden/Ejercicio/Series/Repeticiones/Peso/Descanso). No se cambia código de comportamiento, consultas, datos, eventos, contenedores ni dependencias entre capas. Verificaciones y pendientes en PROJECT_CONTEXT.md.


## Campos de Socio seleccionado — 18 de septiembre de 2026

Última actualización: 18 de septiembre de 2026. En MisSociosFormulario (pantalla «Socios y rutinas | Consulta de rutinas de socios»), los seis Labels de valores se reemplazan por txtSocio, txtDni, txtPlan, txtEntrenador, txtVencimiento y txtRutina: TextBox estándar con ReadOnly=true, borde Fixed3D, fondo Window y TabStop=false. Se declaran y configuran explícitamente en InitializeComponent del Designer, usando tablaSocio existente y sus tres filas con dos pares etiqueta/valor. Las propiedades de las etiquetas también quedan explícitas para retirar los helpers de esa sección. No se agregan contenedores ni controles runtime.

Form.cs únicamente cambia los nombres de los destinos de Text; conserva selección, datos, consultas y rutina semanal. No cambian dependencias entre capas, servicios, repositorios, SQL ni EF. Verificaciones y pendientes en PROJECT_CONTEXT.md.


## Campos de Vinculación — 18 de septiembre de 2026

Última actualización: 18 de septiembre de 2026. En GestionAsignacionesFormulario, los seis Labels de valores se reemplazan por TextBox estándar ReadOnly: txtSocio, txtDni, txtPlan, txtVencimiento, txtEstadoMembresia y txtEntrenadorActual. Se declaran en Designer dentro de tablaFicha existente, con Anchor Left/Right, ancho uniforme y centrado vertical. No se agregan contenedores ni posicionamiento runtime. Form.cs solo cambia los destinos de las asignaciones de Text; conserva datos, eventos, estado vacío y operaciones. Nuevo entrenador sigue siendo el ComboBox existente. Sin cambios de capas, negocio, consultas, SQL ni EF. Verificaciones y pendientes en PROJECT_CONTEXT.md.

## Simplificación de Ejercicios — 18 de septiembre de 2026

Última actualización: 18 de septiembre de 2026. GestionEjerciciosFormulario elimina panelEncabezado (siempre oculto) y panelDetalle (envoltorio de grupoFicha). Administrador/Entrenador usan el constructor sin parámetros; el título continúa en el encabezado general mediante la navegación existente, sin modificar ControladorNavegacion. Se conservan SplitContainer para la división ajustable, tablaFicha para alinear campos, accionesFicha para botones condicionales y galeriaImagenes para miniaturas variables. Se corrigen posiciones de Nuevo/Actualizar listado y Dock de galería en Designer. Se mantiene AutoScaleMode.Font y su base 7×17; no se introducen cálculos de resize, clases ni dependencias. Verificaciones y límites en PROJECT_CONTEXT.md.

## Ajuste puntual de Gestionar rutinas — 18 de septiembre de 2026

Última actualización: 18 de septiembre de 2026. Se elimina el SplitContainer splitContenido: panelListado y layoutDetalle quedan directamente en panelContenido, con las posiciones y tamaños iniciales conservados y Anchor estándar (izquierda de ancho fijo; derecha adaptable). Todo sigue declarado en Designer, sin controles nuevos ni lógica de Resize. Se retiran las columnas Socios y Creación y sus valores de Rows.Add; se conserva el ID oculto. Sin cambios de consultas, entidades, base ni dependencias entre capas. Verificaciones y pendientes en PROJECT_CONTEXT.md.


## Normalizacion de Asignaciones - 18 de septiembre de 2026

Se elimina panelDetalle (wrapper exclusivo de grupoFicha). Listado/filtros son TableLayoutPanel; etiquetas y valores permanecen directos en tablaFicha. Se conserva el GroupBox existente como region semantica con borde/fondo/padding, no como sustituto de paneles individuales. La columna AutoSize y la alineacion vertical compartida resuelven Nuevo entrenador/ComboBox sin geometria runtime. Panel 4/1, TableLayoutPanel 1/3, FlowLayoutPanel 1/1, SplitContainer 1/1, GroupBox 1/1. Form.cs y las reglas de asignacion no cambian. Verificaciones reales y pendientes en PROJECT_CONTEXT.md.

## Normalizacion de Planes - 18 de septiembre de 2026

GestionPlanesFormulario conserva SplitContainer y sustituye wrappers por tablas de listado, filtros y detalle. Titulo y acciones del detalle se integran en contenedorCampos, sin panelDetalle/contenedorDetalle. Los botones comparten FlowLayoutPanel; campos y grilla directos. Panel 6/1, TableLayoutPanel 1/3, FlowLayoutPanel 0/1, SplitContainer 1/1. No cambia Form.cs, negocio ni persistencia. Designer/Properties y navegacion real Administrador/Planes comprobados; pruebas y pendientes en PROJECT_CONTEXT.md.

## Normalizacion de Usuarios - 18 de septiembre de 2026

GestionUsuariosFormulario mantiene estructura declarativa: SplitContainer, tabla de listado con filtros/grilla directos y tabla de detalle con labels/campos/foto directos. Un Panel conserva AutoScroll del detalle; las acciones comparten FlowLayoutPanel. Se eliminan panelFiltro y contenedorDetalle, sin trasladar construccion visual a Form.cs. Contenedores: Panel 8/4, TableLayoutPanel 2/2, FlowLayoutPanel 0/1, SplitContainer 0/1 (antes/despues). La configuracion de fecha dependiente del dia actual pasa de constructor a Load; eventos y reglas permanecen. Designer/Properties reales y navegacion Administrador/Usuarios comprobados; detalles de pruebas y pendientes en PROJECT_CONTEXT.md.

## Normalizacion declarativa incremental - 18 de septiembre de 2026

Ultima actualizacion: 18 de septiembre de 2026.

Primera fase: RutinasEntrenadorFormulario. Estructura principal exclusivamente en Designer.cs, controles directos en tablas y acciones en FlowLayoutPanel. Se integraron los titulos/acciones de panelRutina y panelFormulario y se convirtieron panelListado/panelEjercicios en tablas. Panel 7 -> 3; TableLayoutPanel 3 -> 5; FlowLayoutPanel 3 -> 3; SplitContainer 1 -> 1. Los paneles conservados son encabezado oculto, separador y region con padding. Las alturas se resuelven mediante AutoSize/Percent y no recalculando SplitterDistance en Resize.

Constructor publico sin parametros: solo InitializeComponent. Carga de datos en Load con la proteccion de diseño existente. Se elimina el evento Paint vacio; seleccion y operaciones de negocio permanecen intactas. Se habilitan campos individualmente al integrar botones en la tabla del editor. No hay nuevos accesos entre capas, SQL, cambios de EF, DER ni reglas.

Designer real y Properties comprobados en Visual Studio Community 2026 18.10.1, aceptado por el usuario. Login real, navegacion Administrador -> Gestionar rutinas y seleccion/Editar comprobados. Prueba auxiliar del formulario: carga, modos y cuatro tamaños; no confirma persistencia. Detalle y pendientes en PROJECT_CONTEXT.md.

La atribucion previa del NRE a components nulo queda retirada: un contenedor opcional nulo con Dispose protegido es valido. No se obtuvo una pila que identificara la causa original. La prueba visual actual sustituye la comprobacion insuficiente basada solo en el titulo de la pestaña.

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
