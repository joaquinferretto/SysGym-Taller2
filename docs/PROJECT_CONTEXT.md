# Contexto del proyecto

## PUNTO DE CONTINUACIÓN — normalización visual integral (19/09/2026)

Esta sección es la referencia vigente para continuar; las secciones inferiores son historia y no sustituyen este estado. Usuario solicita capa visual plana, verificación incremental en Designer REAL de Visual Studio 2026 y runtime por formulario, sin commit. No modificar SQL, EF, entidades, repositorios ni negocio. Sincronización SQL anterior resuelta; EjercicioImagen sigue pendiente fuera de esta tarea. Mantener modificaciones locales preexistentes.

**Alcance de la última tarea: exclusivamente GestionPlanesFormulario.** La unificación visual con Membresías quedó verificada. No avanzar al siguiente formulario en esta tarea. `ConsultaRutinasAdministradorFormulario` continúa en el orden pendiente general.

**4. GestionPlanesFormulario cerrado; unificación visual revisada el 19/09/2026.** Se realizó una unificación visual basada explícitamente en `GestionMembresiasFormulario`: dos recuadros blancos con borde `FixedSingle`, fondo exterior gris claro, padding exterior e interior de 16 px, título y ayuda del listado, búsqueda/filtro alineados y grilla contenida. La ficha conserva Nombre, Descripción y Precio en filas separadas por 38 px; las cinco acciones quedan en dos filas, con botones de 112×38 px y separación de 8 px. Nuevo/Guardar usan el verde de Membresías, Modificar/Reactivar gris y Dar de baja rojo suave. `FixedPanel.Panel2` conserva el ancho de la ficha al redimensionar; el listado absorbe el crecimiento. La escala base por fuente es 7×17, coherente con Segoe UI 9,5 en la comprobación a 96 DPI.

Se conserva un único `SplitContainer`: el borde lo proporciona su propiedad `BorderStyle`, sin agregar controles ni reconstruir la interfaz en runtime. Siguen eliminados los cinco contenedores intermedios `panelEncabezado`, `barraAcciones`, `panelListado`, `contenedorCampos` y `panelAcciones`. Todos los controles siguen directamente en `Panel1`/`Panel2`, con propiedades declarativas editables en Designer. La revisión actual solo modifica propiedades visuales de `GestionPlanesFormulario.Designer.cs`; conserva exactamente el archivo funcional `.cs`, los nombres, las diez suscripciones de eventos, columnas, filtros, selección, validación decimal y operaciones existentes. `lblEstado` conserva sus mensajes funcionales, fondo transparente y texto inicial vacío.

Verificación de esta unificación: Debug y Release compilan con 0 errores; permanecen los dos CS0649 preexistentes de Socios/Ejercicios. El Designer REAL de Visual Studio Community 2026 (instalación 18.10.12201.205) abrió y se inspeccionaron superficie y Properties seleccionando `lblNombre`, `nombre`, `filtroEstado`, `guardar` y `tabla`. Se probó mover `lblNombre` mediante teclado de (16,58) a (17,58) y editar Text desde Properties; ambos cambios de prueba fueron deshechos y se confirmó visualmente la restauración. La prueba runtime abrió el formulario real contra `SysGymDB`, cargó Normal/Premium y verificó selección/modo Editar, búsqueda con/sin coincidencias, Todos/Activos/Inactivos y Nuevo. En áreas cliente 884×521, 1076×599, 1100×680, 1310×731 y 1630×911 no hubo recortes, superposiciones ni scroll de los paneles. Se inspeccionaron capturas comparativas de ambos formularios a 1100×680. Auxiliares y capturas locales ignorados en `bin/NormalizacionPlana/Planes*Check.cs` y `bin/PlanesVisual/`; no incluidos en el proyecto. No se ejecutaron altas, modificaciones, bajas ni reactivaciones persistentes; esas operaciones no se revalidan con escrituras en esta tarea visual. No se modificaron otros formularios, negocio, EF, SQL ni dependencias entre capas.

Antecedente de la simplificación anterior: el Designer abrió tras compilar desde el IDE para resolver errores de referencias de diseño de aquella instancia, sin limpiar cachés. Se habían comprobado selección de Normal, filtro Premium y Nuevo. Las capturas y auxiliares de esa pasada se registraron en `bin/Visual20260919` y `bin/NormalizacionPlana`; no debe asumirse que estén disponibles en todos los equipos. La verificación vigente de la unificación visual es la del párrafo anterior.

**3. InicioPanelAdministrador cerrado.** La simplificación visual, el pronóstico dinámico, el estado de cuenta y las pruebas previas en tres resoluciones se conservaron sin repetir cambios de layout ni de lógica. El pendiente real era la metadata del diseñador: `DesignerCategory` y `SubType` declaraban `Component` aunque la clase hereda de `UserControl`; ambos pasan a `UserControl`. Tras compilar, el Designer REAL de Visual Studio Community 2026 18.10.0 abrió la superficie con Resumen general, pronóstico de siete columnas, estado de cuenta y la bandeja del `ToolTip`. Debug compila sin errores y con los dos warnings CS0649 preexistentes. No cambian la carga de clima, el respaldo local, las cuotas, el doble clic de socio ni las dependencias entre capas.

**2. GestionUsuariosFormulario cerrado.** Contenedores 8 → 1: se conserva únicamente contenedorContenido (SplitContainer listado/detalle; Panel2 permite scroll de toda la ficha). Eliminados panelEncabezado, barraAcciones, panelContenido, panelListado, panelDetalle, contenedorCampos y panelAcciones; retirados título/descripción/Volver locales ocultos y handler de ese Volver. Labels, campos, combos, fecha, foto, grilla y siete botones quedan directamente en Panel1/Panel2. Sin StatusStrip; lblEstado se conserva porque muestra cantidad, validaciones y resultados reales, con texto inicial vacío. Debug 0 errores; dos CS0649 preexistentes. Designer REAL VS2026 abierto y captura inspeccionada. Runtime con login real carga 23 usuarios en 1366×768, 1600×900 y 1920×1080; captura pequeña inspeccionada sin solapamientos. No se ejecutaron altas/bajas ni guardados para no modificar datos. Capturas GestionUsuariosFormulario-*.png y outline en bin/Visual20260919.

**1. PanelAdministrador cerrado.** Causa del rol recortado: lblUsuarioRol de 250 px limitado por panelIdentidad y columna fija de layoutEncabezado; el texto de sesión sí incluye Administrador. Eliminados layoutEncabezado, panelIdentidad y panelPie; header tiene logo, identidad completa, título, subtítulo, Volver y Cambiar de cuenta directos. Conservados panelEncabezado (región global violeta), panelMenu (sidebar), panelOpciones (menú desplegable con scroll) y panelContenido (superficie funcional de navegación). Contenedores 7 → 4. Se conserva Salir: confirma cierre completo, distinto de Cambiar de cuenta. Retirado handler Paint vacío. Volver llama a ControladorNavegacion.VolverAlInicio, que cierra el módulo y reutiliza el evento existente para restaurar inicio. Build Debug: 0 errores, dos CS0649 preexistentes. Designer real abierto e inspeccionado; primer intento falló al resolver System.Windows.Forms.Form y se resolvió compilando desde el propio IDE, sin limpiar cachés ni retocar código. El IDE mostró avisos de referencias NuGet, aunque build y ejecución pasaron. Runtime: rol completo y header sin superposiciones en 1366×768, 1600×900, 1920×1080; Volver desde Usuarios conserva la misma instancia y restaura inicio; Cambiar de cuenta vuelve al login real. Salir conserva su implementación diferenciada revisada en código. Capturas PanelAdministrador-1366/1600/1920.png y PanelAdministrador-designer.png en bin/Visual20260919.

Orden pendiente: ConsultaRutinasAdministradorFormulario; ReportesFormulario; PanelRecepcionista; GestionMembresiasFormulario, GestionPagosFormulario y GestionAsignacionesFormulario; PanelEntrenador; MisSociosFormulario y RutinasEntrenadorFormulario; GestionSociosFormulario, GestionEjerciciosFormulario, RutinaSemanalFormulario e InicioSesion; pasada global. No se han normalizado simultáneamente.

Herramientas locales ignoradas: bin/NormalizacionPlana/DesignerForm.ps1 -FormFile <ruta .cs> abre el Designer real usando Microsoft.VisualStudio.Interop tipado; captura en bin/Visual20260919/<Formulario>-designer.png. bin/NormalizacionPlana/VisualCheck.cs comprueba login/navegación/layout, recibe credenciales por stdin sin almacenarlas en código. Compilar aplicación con MSBuild de VS18, exxen2.0.slnx, /t:Build /p:Configuration=Debug /p:OutputPath=bin\Visual20260919\. Auxiliares fuera del csproj; no confundir un título de pestaña con evidencia visual. No limpiar cachés sin diagnóstico.

Cada formulario cerrado se registrará aquí con eliminados, conservados, controles directos, Designer, runtime, resoluciones y build. Decisiones arquitectónicas en docs/ARCHITECTURE.md.

## Sincronización de esquema para cuotas — 19 de septiembre de 2026

La base local `SysGymDB` fue sincronizada con el modelo EF actual después del error de estado de cuenta. Se agregaron únicamente `Membresia.IdRutina INT NULL`, `Socio.FotoRuta NVARCHAR(260) NULL` y `FK_Membresia_Rutina` hacia `Rutina(IdRutina)` con borrado `NO ACTION`. Las 20 membresías y 20 socios quedaron con esos valores NULL; no hubo pérdida ni actualización de registros. El login real, estado de cuentas, cuotas/pagos, membresías, socios y rutinas pasaron las pruebas de lógica y navegación WinForms. Debug compila sin errores.

La auditoría no destructiva de los 16 `DbSet` encontró una diferencia adicional: falta la tabla completa `EjercicioImagen` en la base local. No se creó automáticamente porque no es una columna nullable aislada. Las columnas históricas `Foto`, `Plan.IdRutina`, `PlanRutina` y `RutinaAsignacion` permanecen intactas. `SysGymDB.sql` ya estaba alineado con las dos columnas y la FK.

## Continuación vigente: controles directos — 19 de septiembre de 2026

Última actualización: 19 de septiembre de 2026. Esta sección prevalece sobre las notas históricas de continuidad siguientes. Se aplicó la nueva instrucción explícita de eliminar contenedores de botones, campos y cajas decorativas. No se hicieron commits. Se preservaron los cambios locales preexistentes de AGENTS.md, GestionAsignacionesFormulario.Designer.cs y exxen2.0.csproj; el trabajo nuevo solo afecta Rutinas y esta documentación/ARCHITECTURE.md.

Rutinas: eliminados Panel `panelEncabezado` y `barraAcciones`; TableLayoutPanel `panelListado`, `layoutDetalle`, `contenedorRutina`, `contenedorFormulario`, `panelEjercicios`; FlowLayoutPanel `accionesRutina`, `accionesFormulario`, `accionesEjercicios`. Contenedores antes/después: Panel 3/1, TableLayoutPanel 6/1, FlowLayoutPanel 3/0; total 12/2. Permanecen la distribución principal 30/70 y el scroll funcional de toda la región derecha. Controles del listado directos en la tabla; 31 controles directos en el Panel de detalle. Sin wrappers de grillas, campos ni acciones. Se preservan controles ocultos del antiguo encabezado, eventos y Form.cs.

StatusStrip: no existía. «Listo» era el texto inicial de lblEstado; se quitó ese texto, conservando el Label porque muestra errores y resultados. Su altura se reduce 20 px. La UI permanece declarativa en InitializeComponent, sin loops, condicionales ni helpers de construcción.

Verificaciones actuales: Debug compila con 0 errores y dos CS0649 preexistentes (components de Socios/Ejercicios). Las 19 suscripciones de eventos coinciden con la copia local anterior a esta tarea. Ejecución auxiliar sin Load de datos en cinco tamaños de cliente (810×551, 990×551, 1076×599, 1310×731 y 1630×911): controles dentro del ancho, sin superposiciones visibles ni scroll horizontal; Guardar/Cancelar visibles y accesibles por scroll. Captura de 990 px inspeccionada. Esta prueba no acredita operaciones de negocio, login ni persistencia. No hay cambios de capas ni de sus dependencias. Diff check del Designer correcto.

Runtime con datos bloqueado y reconfirmado: `RutinaLogica.ListarParaGestion()` falla por `Invalid column name 'FotoRuta'` e `Invalid column name 'IdRutina'`. Metadatos de `.\SQLEXPRESS`, `SysGymDB`: faltan FotoRuta y Membresia.IdRutina. No se modificó SQL ni configuración. Se solicitó identificar la base actualizada; no pasar al siguiente formulario hasta cumplir la verificación funcional exigida. Credenciales no almacenadas.

Designer real verificado en Visual Studio Community 2026 18.9.3: superficie renderizada e inspeccionada, clic directo y selección individual comprobados en lblNombre, nombre, ejercicio, guardarRutina y tablaEjercicios. Properties de lblNombre muestra Anchor, Dock, Location, Size y Margin; las otras capturas acreditan selección mediante sus tiradores, no inspección de Properties para cada tipo. Se corrigió el auxiliar usando las interfaces tipadas de Microsoft.VisualStudio.Interop; no fue necesario limpiar cachés ni modificar código por los fallos de automatización. Visual Studio 2022 no está instalado y sigue sin verificarse. Auxiliares y capturas locales ignorados por Git en bin/NormalizacionPlana; no incluidos en el proyecto. Pendientes: runtime completo con base compatible y luego los demás formularios. No se avanzó al siguiente por la regla expresa de validación secuencial.

## Registro de continuidad entre equipos — 19 de septiembre de 2026

Este registro existe para que otro equipo pueda continuar aunque trabaje en otra PC. El repositorio remoto es `origin` (`https://github.com/joaquinferretto/SysGym-Taller2.git`). La rama de trabajo es `estado-astra`, actualmente alineada con `origin/estado-astra` en el commit `2b84f54` (`se arreglan algunos formularios`). Ese commit es lo último que Astra dejó guardado en Git.

El commit de Astra contiene cambios en 15 archivos: formularios de Reportes, Ejercicios, Socios, MisSocios, Asignaciones y Rutinas, paneles de Administrador/Entrenador, además de `docs/ARCHITECTURE.md` y `docs/PROJECT_CONTEXT.md`. Para recuperar exactamente ese estado en otra PC: clonar el repositorio, ejecutar `git fetch origin`, cambiar a `estado-astra` y ejecutar `git pull --ff-only origin estado-astra`. No usar `main` para continuar esta fase.

Después del commit de Astra quedaron cambios locales sin commit hechos durante la reanudación de Rutinas. Están únicamente en el árbol de trabajo de esta PC y todavía no existen en `origin`: `capaVisual/Entrenador/RutinasEntrenadorFormulario.Designer.cs`, `docs/ARCHITECTURE.md` y `docs/PROJECT_CONTEXT.md`. Antes de cambiar de PC hay que preservar esos cambios mediante una copia/patch o un commit autorizado; como la tarea prohíbe hacer commit, no se subieron. El estado comprobable es `git status --short` y `git diff`.

Las herramientas y capturas de comprobación también están solo localmente bajo `bin/NormalizacionTools/` y `bin/NormalizacionControlada/`; están ignoradas por Git y no forman parte del proyecto. No son necesarias para compilar ni para continuar: el código fuente y esta documentación son la referencia. Si otro equipo necesita repetir las pruebas del diseñador, debe crear sus propios auxiliares en su PC y usar la versión instalada de Visual Studio; aquí se usó Visual Studio Community 2026, no Visual Studio 2022.

Regla para equipos distintos: no asumir que una base SQL local, una versión de Visual Studio, archivos ignorados ni cambios sin commit existen en otra PC. Verificar siempre rama, commit, `git status`, versión de Visual Studio, cadena de conexión y esquema de SQL Server antes de ejecutar runtime. En esta PC, `SysGymDB` de `DESKTOP-EH3U94C\SQLEXPRESS` no tiene `FotoRuta` ni `Membresia.IdRutina`; por eso el runtime de Rutinas está bloqueado aunque el catálogo de ejercicios sí responde. No modificar SQL ni el esquema para sortearlo.

## Reanudación controlada: Rutinas — 19 de septiembre de 2026

Última actualización: 19 de septiembre de 2026. La normalización completa sigue pendiente. Se retoma desde el estado de Git limpio dejado por el compañero, sin restaurar versiones anteriores ni hacer commits. Se mantiene la condición de no pasar al siguiente formulario antes de verificar Designer, Debug y runtime real.

**Punto de reanudación:** fase 1, `RutinasEntrenadorFormulario`, cambios sin commit. Diseño y comprobaciones visuales de esta versión terminados; fase aún pendiente por runtime con datos. Lo último aplicado es scroll de TODA la región derecha mediante `panelDetalle.AutoScroll=true` y `AutoScrollMinSize=(0,560)`, con `layoutDetalle.Dock=Fill`; no hay contenedores individuales para controles. La grilla de ejercicios conserva un mínimo útil mediante `panelEjercicios.MinimumSize=(0,160)`. Debug y las 12 comprobaciones de layout (tres modos × cuatro resoluciones, incluyendo límites, superposiciones, altura de grilla y ausencia de scroll horizontal del detalle) pasaron. Capturas de 720p inspeccionadas y acceso a Guardar con scroll comprobado. Designer real y Properties de los cinco controles comprobados en la última versión. Siguiente paso: obtener una base compatible para el runtime, o una autorización explícita del usuario para continuar la normalización visual con runtime pendiente. Esa decisión se solicitó; no asumir que un «continúa» anterior a la pregunta autoriza omitir la regla 47. No editar el siguiente formulario hasta resolverla.

Para retomar: leer esta sección antes de las notas históricas, revisar `git diff` y continuar esas verificaciones, sin volver a normalizar lo ya terminado. Auxiliares locales ignorados por Git: `bin/NormalizacionTools/Designer.ps1` y `DesignerCheck.cs` (automatización EnvDTE real + UI Automation); `bin/NormalizacionTools/Runtime.cs` y `bin/NormalizacionControlada/RuntimeCheck.exe` (modo `layout` sin datos, ejecución sin argumentos con datos). Las capturas están en esas carpetas. No forman parte de la aplicación ni del `.csproj`. Compilación de aplicación: MSBuild de Visual Studio 18, solución `exxen2.0.slnx`, `/t:Build /p:Configuration=Debug /p:OutputPath=bin\NormalizacionControlada\`. No confundir una prueba auxiliar ni el título de una pestaña con la inspección del Designer real. Usar `EnvDTE.Constants.vsViewKindDesigner`; el GUID terminado en A700 usado inicialmente no era esa vista. Conservar esta sección actualizada antes de interrumpir la tarea.

Formulario: `RutinasEntrenadorFormulario`. Estado inicial: complejo, parcialmente normalizado. Los controles ya estaban directamente en tablas, pero la región principal distribuía listado y detalle con coordenadas y Anchor. `panelContenido` pasa de Panel a TableLayoutPanel con columnas porcentuales 30/70, una fila porcentual y ambos sectores con Dock=Fill y márgenes. No se agrega un divisor ni se reconstruye el layout en Form.cs. La fila de acciones de ejercicios pasa de altura fija a AutoSize: la comprobación detectó un recorte de dos píxeles en sus botones. `lblEstado.BackColor` pasa a Transparent; los labels de campos y títulos ya heredan el fondo de su región.

Contenedores antes/después de esta reanudación: Panel 3/3; TableLayoutPanel 5/6; FlowLayoutPanel 3/3; SplitContainer 0/0. Se sustituye el Panel de distribución principal, conservando su fondo y padding; se agrega un único Panel con función de scroll de toda la región derecha, porque sin esa región la grilla quedaba aplastada al mostrar Guardar/Cancelar en 720p. Los otros dos Panel son el encabezado local oculto y el separador. No se crean contenedores para Label, TextBox, ComboBox, DateTimePicker, NumericUpDown, PictureBox, DataGridView ni Button individuales. Labels, TextBox, ComboBox y ambas DataGridView siguen directamente en las tablas estructurales de sus secciones; los botones están en tablas o FlowLayoutPanel compartidos. No se encontraron loops ni helpers de construcción en InitializeComponent. Las 18 suscripciones de eventos permanecen idénticas; no se encontraron handlers vacíos o duplicados en este formulario. Se conservan columnas, operaciones, validaciones, lógica, navegación y capas.

Verificaciones: Debug compila sin errores; permanecen dos CS0649 preexistentes en `GestionSociosFormulario.components` y `GestionEjerciciosFormulario.components`, pendientes de sus respectivas fases. Layout auxiliar sin carga de datos: Consulta, Nuevo y Edición en 1280×720, 1366×768, 1600×900 y 1920×1080, descontando 290×169 píxeles para regiones externas; controles dentro del área visible o desplazable, sin superposición ni scroll horizontal de detalle, y Guardar alcanzable con scroll. Estos modos se configuraron solo en instancias temporales de prueba y no acreditan operaciones funcionales. Capturas del layout inspeccionadas. Se abrió y se inspeccionó visualmente el diseñador REAL de Rutinas en Visual Studio Community 2026. Se seleccionaron `lblNombre`, `nombre`, `ejercicio`, `guardarRutina` y `tabla` y se comprobó en capturas que Properties muestra las propiedades específicas de Label, TextBox, ComboBox, Button y DataGridView. La selección mediante UI Automation requiere confirmar con Enter: cambiar solo el nombre del selector no actualizaba la grilla y no se aceptó como prueba. No se modificaron ni guardaron propiedades desde la instancia de verificación. Esto no acredita Visual Studio 2022. `git diff --check` sin errores.

Bloqueo de runtime: SQL Server responde y la lectura del catálogo devuelve 45 ejercicios, pero `RutinaLogica.ListarParaGestion()` falla con `Invalid column name 'FotoRuta'` e `Invalid column name 'IdRutina'`. La consulta de metadatos confirma que la base `SysGymDB` de `DESKTOP-EH3U94C\SQLEXPRESS` no tiene esas columnas en Socio/UsuarioSistema/Membresia. El respaldo `localhost,1433` no responde, tampoco fuera del sandbox. La otra base local accesible, `SysGym_Verificacion_20260908`, también tiene esquema anterior: no ofrece una alternativa compatible. No se modificaron SQL, DER, base, conexiones ni reglas para sortear el problema. Se solicitó identificar la base actualizada o autorizar explícitamente la continuación visual con runtime pendiente. Credenciales de acceso no guardadas.

Pendientes: login y navegación real con la base compatible, operaciones del módulo, cierre de la fase 1 y fases 2 a 6; Release y eliminación de warnings según el orden solicitado. Las verificaciones históricas siguientes corresponden a versiones y entornos anteriores y no sustituyen estos pendientes.

### Inventario de los 17 Designer activos — 19 de septiembre de 2026

Conteo estático según los archivos incluidos en `exxen2.0.csproj`; no se incluyen formularios legacy. P/T/F/S significa Panel/TableLayoutPanel/FlowLayoutPanel/SplitContainer. Los candidatos requieren revisar su función antes de eliminarlos; no se modificaron otros formularios durante esta reanudación. La búsqueda inicial no encontró loops ni los helpers prohibidos de layout en estos Designer. Esto no acredita Designer ni runtime de los formularios pendientes.

| Formulario / UserControl | P/T/F/S actuales | Punto para revisar en su fase |
| --- | --- | --- |
| RutinasEntrenadorFormulario | 3/6/3/0 | Fase 1 en curso; ver punto de reanudación. |
| GestionUsuariosFormulario | 4/2/1/1 | Revisar fondo de estado y justificar regiones con padding/scroll ya existentes. |
| GestionPlanesFormulario | 1/3/1/1 | Campos directos ya normalizados; fondo de estado y verificaciones actuales pendientes. |
| GestionAsignacionesFormulario | 1/3/1/1 | Campos directos ya normalizados; fondo de estado/alineación y verificaciones pendientes. |
| GestionSociosFormulario | 4/2/1/1 | CS0649 de components; revisar regiones completas y fondo de estado. |
| GestionMembresiasFormulario | 7/1/0/0 | panelDetalle contiene únicamente otro Panel: contenedorDetalle; revisar estructura, sin alterar negocio. |
| GestionPagosFormulario | 8/1/0/0 | panelDetalle contiene únicamente contenedorDetalle; revisar estructura, sin alterar pagos. |
| GestionEjerciciosFormulario | 3/1/2/1 | CS0649 de components; verificar eventos, galería y fondo de estado. |
| MisSociosFormulario | 4/1/1/1 | Revisar regiones completas y fondo de estado. |
| InicioPanelAdministrador | 6/1/0/0 | panelCabecera contiene solo lblResumen; revisar SubType/DesignerCategory de UserControl. |
| PanelAdministrador | 6/1/0/0 | panelPie contiene solo btnSalir; conservar header/sidebar/navegación y justificar región. |
| PanelRecepcionista | 6/1/0/0 | Igual revisión de panelPie; panelContenido es alojamiento dinámico, no eliminar por tener inicialmente un Label. |
| PanelEntrenador | 6/1/0/0 | Igual revisión de panelPie y alojamiento dinámico. |
| InicioSesion | 1/0/0/0 | Auditoría funcional y Designer pendientes; sin cambio en esta reanudación. |
| ConsultaRutinasAdministradorFormulario | 3/0/0/0 | barraAcciones contiene solo actualizar; panelContenido contiene solo tabla. |
| ReportesFormulario | 8/1/0/0 | barraAcciones contiene solo generar; revisar tarjetas sin rediseñar. |
| RutinaSemanalFormulario | 2/0/0/0 | panelContenido contiene solo tablaSemana; revisar contenedor y fondo de estado. |

## Layout de Gestión de socios — 18 de septiembre de 2026

Última actualización: 18 de septiembre de 2026. Ambas tarjetas ocupan la altura de la misma fila porcentual mediante Dock=Fill y las columnas se distribuyen 50%/50%. El contenedor interior del detalle ocupa el espacio disponible, con campos anclados horizontalmente; foto y acciones quedan dentro de él. Se reducen espacios verticales para mostrar los siete campos y todas las acciones sin scroll interno. Solo cambia el Designer y esta documentación; se preservan modificaciones previas del usuario, eventos, lógica, DataGridView y persistencia.

Verificado: solución Debug compilada en bin/VerificacionLayoutSocios sin errores; dos warnings CS0649 preexistentes de components en GestionSociosFormulario y GestionEjerciciosFormulario. Inicialización y layout en memoria con tamaños de cliente del módulo 810×571, 990×551, 1076×599, 1310×731, 1630×911 y 1215×680: tarjetas de igual altura, todos los controles del detalle dentro de sus respectivos contenedores y sin barra vertical. DesignSurface carga sin errores; suscripciones de eventos idénticas a las anteriores y diff check correcto. No se ejecutó Load ni se consultó SQL Server.

Pendiente: inspección interactiva en Visual Studio Designer y ejecución integrada con datos reales y otras escalas DPI. Las pruebas en memoria no sustituyen esas verificaciones ni acreditan ventanas menores a los tamaños comprobados.


## Tarjetas de Reportes — 18 de septiembre de 2026

Última actualización: 18 de septiembre de 2026. Presentación de ReportesFormulario como dashboard sencillo: tres tarjetas superiores (socios, usuarios, membresías), dos inferiores centradas (rutinas, ejercicios), cinco números con tamaño uniforme y acento violeta. Se sustituye el bloque resumen; el botón Generar reporte conserva evento y funcionalidad, y la fecha se muestra discretamente debajo. Antes de generar los valores muestran «-». Los controles están declarados en Designer. Form.cs solo separa la presentación de los mismos contadores en cinco Labels; mantiene consultas, orden de evaluación, manejo de errores y fecha.

Verificado: solución Debug compilada en bin/VerificacionReportesTarjetas sin errores, con dos CS0649 preexistentes de GestionSociosFormulario y GestionEjerciciosFormulario. Layout en memoria a 900×560, 1100×680, 1280×720 y 1920×1080: cinco tarjetas y etiquetas dentro de sus contenedores y estado debajo sin desbordes. Renderizado del panel de contenido inspeccionado a 1100 px; distribución 3+2, títulos y valores iniciales visibles. DesignSurface carga sin errores. Diff check de los archivos afectados correcto y revisión del handler confirma las mismas cinco consultas y fecha. No se consultó ni modificó SQL Server.

Pendientes: abrir interactivamente ReportesFormulario en Visual Studio Designer y probar generación/carga con datos reales en la aplicación. Las comprobaciones de layout y DesignSurface no acreditan esos pasos.


## Columnas ajustadas al ancho disponible — 18 de septiembre de 2026

Última actualización: 18 de septiembre de 2026. Los tres DataGridView solicitados ya tenían Fill y Dock=Fill; el desbordamiento procedía de MinimumWidth elevados. Se reducen a 20 px solo en sus columnas visibles y se ajustan FillWeight del catálogo (45/35/20) y ejercicios (10/7/32/8/17/8/18). Socios conserva 28/16/20/22/16/16. No se oculta la barra horizontal por configuración: las columnas ahora caben realmente. No hay cambios de Form.cs, lógica, datos, consultas ni dependencias.

Verificado: solución Debug compilada en bin/VerificacionColumnasFill sin errores; permanecen dos CS0649 preexistentes de GestionSociosFormulario y GestionEjerciciosFormulario. Se obtuvieron los tamaños reales de las tablas mediante layout de ambos formularios a anchos de cliente 900, 1100, 1280 y 1920 px (altura 720). Con esos tamaños se comprobaron las grillas en memoria, vacías y con 80 filas de texto largo: todas las columnas caben, sin HScrollBar visible, y con VScrollBar visible cuando hay filas suficientes. Para esa prueba aislada se desconectaron los handlers de selección únicamente de las instancias temporales; no se ejecutaron consultas. Ambos formularios cargan sin errores en DesignSurface. Diff check de los archivos afectados correcto.

Pendientes: inspección interactiva en Visual Studio Designer y en la aplicación con datos reales; las verificaciones en memoria no sustituyen ese recorrido.


## Campos de Socio seleccionado — 18 de septiembre de 2026

Última actualización: 18 de septiembre de 2026. Cambio visual puntual en MisSociosFormulario: seis valores informativos ahora aparecen dentro de TextBox ReadOnly definidos en Designer, conservando la distribución existente. Sin selección, txtSocio muestra «Selecciona un socio» y los demás «-», incluso antes de Load; la selección conserva las asignaciones de los mismos datos existentes. Se mantienen título y mensaje superior, listado, filtros, acciones y rutina semanal.

Verificado: solución Debug compilada sin errores en bin/VerificacionSocioSeleccionado; dos warnings CS0649 preexistentes en GestionSociosFormulario y GestionEjerciciosFormulario. Inicialización y layout en memoria a 1100×680, 1280×680 y 1920×680: seis campos habilitados, ReadOnly, con borde, alineados por fila y dentro de tablaSocio; estado inicial y ejecución de MostrarFichaVacia comprobados. DesignSurface carga el formulario sin errores. El diff de Form.cs solo cambia referencias a los seis controles; no cambia lógica ni dependencias. Diff check de los archivos de esta tarea correcto; el chequeo global encuentra espacios finales preexistentes en GestionEjerciciosFormulario.Designer.cs.

Pendientes: apertura e inspección interactiva en Visual Studio Designer y recorrido de selección/rutina semanal con la aplicación y SQL Server. DesignSurface y layout en memoria no sustituyen esas verificaciones; no se ejecutaron consultas ni escrituras a la base durante esta tarea.


## Campos de Vinculación — 18 de septiembre de 2026

Última actualización: 18 de septiembre de 2026. Mejora visual puntual de GestionAsignacionesFormulario: seis valores pasan de Labels a TextBox ReadOnly declarados en Designer, con bordes estándar, ancho uniforme y TabStop=false. Se conserva tablaFicha y el ComboBox Nuevo entrenador; los métodos existentes cargan los mismos datos en los nuevos campos. Se mantiene el mensaje Seleccioná una membresía y los valores de la ficha vacía.

Verificado: solución Debug compilada en bin/VerificacionAsignaciones, porque la aplicación abierta bloquea la copia a bin/Debug (MSB3021/MSB3027). La compilación inicial informó dos CS0649 preexistentes en Ejercicios/Socios. Inicialización y layout en memoria a 1100×680, 1280×720 y 1920×1080: seis TextBox ReadOnly con borde, alineación y ancho iguales, dentro de su tabla. Con una membresía de prueba en memoria, AplicarFiltro llena la grilla y la selección carga los seis valores esperados; el ComboBox permite seleccionar entrenador. Ficha vacía comprobada. Se revisó que Form.cs solo modifica referencias a los seis controles; no cambian dependencias ni operaciones de asignación.

Pendientes: inspección interactiva en Visual Studio Designer y aplicación, carga contra SQL Server y confirmación de una asignación real. Las comprobaciones en memoria no acreditan esos pasos; no se ejecutaron operaciones de persistencia.

## Simplificación de Ejercicios — 18 de septiembre de 2026

Última actualización: 18 de septiembre de 2026. Diagnóstico: encabezado interno oculto en Designer, alojamiento sin bordes con Dock=Fill y cambios legítimos de estado explican diferencias Designer/runtime; no había reconstrucción visual ni resize personalizado. Además, Nuevo/Actualizar listado tenían coordenadas fuera del ancho de diseño (1802/1644 sobre 1100) y la galería no acompañaba el ancho. La escala actual coincide con AutoScaleDimensions (7×17), por lo que se conserva el escalado por fuente.

Se eliminan panelEncabezado y panelDetalle, Volver (dentro del encabezado siempre oculto), Cancelar (oculto tanto en Nuevo como en selección, sin ningún flujo que lo mostrara) y handlers vacíos. Constructor reducido a InitializeComponent; los dos paneles de rol dejan de pasar un color que solo afectaba al encabezado eliminado. Se mantienen datos, selección, Guardar/Actualizar, baja/reactivación, Nuevo, recarga, filtros y operaciones de imágenes. Los únicos controles dinámicos siguen siendo miniaturas de imágenes variables. No se cambia la lógica de persistencia ni la navegación general.

Verificado: Debug compila sin errores (dos CS0649 preexistentes), 45 ejercicios contra SQL Server, filtros, búsqueda, selección, modos Nuevo/Edición, conexión única de Guardar y recarga; bounds de controles/botones a 900×560, 1100×680, 1280×720 y 1600×900. DesignSurface de WinForms carga sin errores y permite obtener diseñadores/propiedades de ocho controles principales. No equivale a abrir Visual Studio; la captura con DrawToBitmap del formulario invisible no mostró los controles y no se considera validación visual. Pendientes: inspección interactiva en Designer/aplicación, operaciones reales de alta/edición/baja/reactivación e imágenes. No se escribieron datos durante las pruebas.

## Carga del catálogo de ejercicios — 18 de septiembre de 2026

Última actualización: 18 de septiembre de 2026. Causa: GestionEjerciciosFormulario_Load existía pero no estaba suscrito al evento Load en Designer; el constructor no consulta datos, por lo que Cargar nunca se ejecutaba al abrir. También faltaban las conexiones de búsqueda, Estado, selección y botones; Reactivar y Agregar imagen apuntaban a handlers vacíos. Se restauran únicamente las conexiones a los métodos existentes en GestionEjerciciosFormulario.Designer.cs, sin cambios visuales ni de lógica. El flujo sigue siendo Cargar → EjercicioLogica.ListarParaGestion → UnidadDeTrabajoGimnasio → EF6 → SQL Server, y las filas se agregan con Rows.Add. Buscar vacío y Estado sin selección no filtran registros. El catch existente informa errores mediante lblEstado y MessageBox; no era la causa de la grilla vacía.

Verificado: compilación Debug sin errores (persisten dos CS0649 preexistentes), creación/renderizado en memoria, despacho del evento Load con 45 ejercicios reales, búsqueda con/sin coincidencias, filtros Todos/Activos/Inactivos, selección que carga ficha, modos Nuevo/Edición y recarga. Guardar/Actualizar está conectado una sola vez a guardar_Click. La primera prueba restringida falló por autenticación SSPI; la misma prueba autorizada fuera del sandbox pasó. Sin escrituras ni cambios de configuración. Pendientes: confirmar altas/ediciones con guardado real y apertura interactiva en Visual Studio Designer; las pruebas de esta tarea no acreditan esos pasos.

## Ajuste puntual de Gestionar rutinas — 18 de septiembre de 2026

Última actualización: 18 de septiembre de 2026. Se quita splitContenido y se mantienen los dos sectores existentes mediante Anchor en panelContenido. El listado muestra Rutina, Entrenador y Estado; se eliminan colAsignados/colCreacion y sus valores de carga, sin modificar persistencia ni consultas. Los eventos permanecen iguales.

Verificado: solución Debug compilada, creación y renderizado en memoria a 1100×680, 1280×720, 1366×768 y 1920×1080, sin divisor ni superposición; tres columnas visibles, ID/Estado correctamente alineados y reinicio de Nueva rutina. La instancia de verificación desconectó Load/selección para no consultar la base. Persisten dos warnings CS0649 ajenos (Socios y Ejercicios). Pendientes de esta modificación: apertura en Visual Studio Designer y recorrido funcional contra SQL Server de selección, carga de ejercicios y botones de persistencia. No se agregaron clases, controles ni dependencias.


## Normalizacion controlada: Asignaciones - 18 de septiembre de 2026

Estado inicial: complejo. Panel 4 -> 1; TableLayoutPanel 1 -> 3; FlowLayoutPanel 1 -> 1; SplitContainer 1 -> 1; GroupBox 1 -> 1. Se elimina panelDetalle, que solo envolvia grupoFicha. Filtros/listado pasan a tablas; se conserva el GroupBox Vinculacion por su borde, fondo y agrupacion semantica, sin reemplazar paneles por nuevos GroupBox. Labels y valores ya eran directos: la columna de etiquetas pasa a AutoSize, conservando centrado vertical y ComboBox anclado Left/Right en la misma fila. Las acciones permiten ajuste de filas para no recortarse.

No se encontraron loops/helpers en InitializeComponent ni eventos vacios/duplicados a eliminar. Constructor ya simple; Form.cs no cambia. Designer REAL visible y Properties para lblNuevoEntrenador, buscador, entrenador, asignar y tabla, seleccionados mediante Componentes de Properties. Runtime real: login Administrador -> Operacion -> Asignar entrenador, 20 membresias, seleccion de ficha y Cambiar/Dar de baja habilitados. Prueba auxiliar: 15 entrenadores, seleccion de combo, filtros, recarga y alineacion/bounds en cuatro tamaños. No se confirmaron asignaciones, cambios ni bajas. Otros roles y persistencia quedan pendientes.

## Normalizacion controlada: Planes - 18 de septiembre de 2026

Estado inicial: complejo. Panel 6 -> 1; TableLayoutPanel 1 -> 3; FlowLayoutPanel 0 -> 1; SplitContainer 1 -> 1. Se eliminan panelDetalle (envoltorio) y contenedorDetalle (titulo/acciones integrados con su padding en contenedorCampos). panelListado y barraAcciones pasan a tablas declarativas; panelAcciones a FlowLayoutPanel. Solo se conserva el encabezado oculto. Campos, labels y grilla directos, sin helpers/loops encontrados en InitializeComponent. Constructor ya simple y eventos funcionales sin cambios ni duplicados detectados.

Designer real visible en Visual Studio instalado; Properties verificado para lblNombre, nombre, filtroEstado, tabla y guardar (este ultimo seleccionado desde el selector Componentes de Properties). Runtime: Administrador -> Operacion -> Planes; dos planes cargados, seleccion activa Editar plan/Modificar. Prueba auxiliar: seleccion, busqueda, Nuevo y bounds correctos en los cuatro tamaños. Sin confirmar escrituras. Debug sin errores; advertencias de otros formularios pendientes. La ausencia de componentes no visuales se declara explicitamente con components = null, conservando Dispose estandar.

## Normalizacion controlada: Usuarios - 18 de septiembre de 2026

Estado inicial: complejo. Panel 8 -> 4; TableLayoutPanel 2 -> 2; FlowLayoutPanel 0 -> 1; SplitContainer 0 -> 1. Se eliminaron panelFiltro y contenedorDetalle: sus controles pasan directamente a las tablas de listado y campos. panelListado es ahora TableLayoutPanel; panelAcciones es FlowLayoutPanel; contenedorContenido es SplitContainer. Se conserva panelDetalle por AutoScroll, ademas de encabezado oculto, separador y region con padding. Labels, campos, foto y grilla son hijos directos de layouts; no se cambia la logica de imagenes ni validaciones.

No habia loops/helpers de layout que eliminar en InitializeComponent. Eventos funcionales sin duplicados detectados: se conserva incluso lblClave_Click, que da foco al campo y no es vacio. Constructor reducido a InitializeComponent; el limite de edad de fechaNacimiento se aplica en Load antes de consultar datos, conservando la regla existente.

Verificado en Visual Studio instalado: Designer visible, Properties para lblNombre, nombre, filtroEstado, guardar y tabla. Runtime real: login Administrador -> Usuarios y roles, 23 usuarios cargados, seleccion y modo Editar con Actualizar habilitado. Prueba auxiliar: filtro vacio, filtro Activos, Nuevo y acceso a foto/acciones con scroll en los cuatro tamaños. No se guardaron operaciones. Debug sin errores; permanecen tres warnings CS0649 de otros formularios. Pendientes: persistencia y recorrido en otros roles; no se afirma normalizacion completa de capaVisual.

## Normalizacion controlada: Rutinas - 18 de septiembre de 2026

Ultima actualizacion: 18 de septiembre de 2026.

Se simplifico exclusivamente RutinasEntrenadorFormulario como primera fase. Contenedores antes/despues: Panel 7/3, TableLayoutPanel 3/5, FlowLayoutPanel 3/3, SplitContainer 1/1. panelRutina y panelFormulario se eliminaron integrando titulo y acciones en sus tablas; panelListado y panelEjercicios pasaron a tablas con controles directos. Se mantienen el encabezado local oculto, separador y region con padding, sin modificar header global, sidebar, colores ni negocio.

El constructor sin parametros contiene solo InitializeComponent. Se quitaron el handler vacio accionesRutina_Paint y el recalculo continuo splitContenido_Resize; el SplitContainer conserva dimension inicial valida y layout declarativo. La habilitacion del editor se aplica a sus campos para no deshabilitar acciones integradas en la tabla. No hay loops ni helpers propios en InitializeComponent; el if estandar de Dispose permanece.

Verificacion REAL: Visual Studio Community 2026 18.10.1 (version instalada aceptada por el usuario), formulario visible y Properties probado seleccionando lblNombre, nombre, ejercicio, guardarRutina y tabla. En SysGym, acceso autenticado al panel Administrador, menu Rutinas/Gestionar rutinas, seleccion de rutina y ejercicio y modo Editar con Guardar cambios/Cancelar habilitados. Prueba auxiliar sobre formulario real con configuracion de la aplicacion: 26 rutinas, 45 ejercicios de catalogo, seleccion, Edicion, Cancelar y Nuevo; campos dentro del formulario a 1280x720, 1366x768, 1600x900 y 1920x1080. No se confirmaron altas, modificaciones ni bajas. No se guardan credenciales de prueba.

Debug compila sin errores; permanecen CS0649 en Planes, Ejercicios y Socios, pendientes de sus fases. Verificacion especifica en VS2022, operaciones de persistencia y recorrido de los otros roles pendientes. Release se debe repetir al cerrar la normalizacion.

Rectificacion del diagnostico previo: components nulo es valido si no hay componentes no visuales y Dispose lo comprueba. No existe una pila capturada que pruebe que fuera la causa del NRE original; la apertura de una pestaña por si sola tampoco lo demostraba. La comprobacion actual si inspecciono el formulario visual y Properties. No se inventa metodo, linea ni objeto nulo para una excepcion que no se reprodujo en esta validacion.

## Simplificacion incremental de contenedores - 18 de septiembre de 2026

La segunda fase se limito a `GestionSociosFormulario`, `GestionMembresiasFormulario` y `GestionPagosFormulario`. Se elimino el panel intermedio `contenedorContenido` y se represento la relacion listado/detalle mediante un `TableLayoutPanel` principal con columnas y fila declaradas explicitamente en cada `Designer.cs`. Los paneles de listado y detalle conservan sus bordes, padding, scroll, filtros, acciones y medidas visuales; el resto de los contenedores se mantuvo cuando aporta layout o agrupacion.

No se movio diseño programatico a los archivos `.cs`: no se crearon controles en runtime y la logica de formularios y de negocio conserva sus capas. Los tres formularios compilaron en Debug y Release sin errores, se instanciaron y ejecutaron `PerformLayout` en memoria, y sus controles visibles mantuvieron los mismos bounds que la compilacion anterior. `git diff --check` fue correcto. Pendiente: comprobacion manual de "Ver disenador" en Visual Studio y prueba funcional contra la base disponible.

## Fase 1: Designer declarativo - 18 de septiembre de 2026

La primera fase se limito a `GestionUsuariosFormulario`, `GestionPlanesFormulario` y `GestionAsignacionesFormulario`. Cada `InitializeComponent` declara de forma explicita sus controles, propiedades, contenedores, filas y suscripciones de eventos, con el formato esperable de Windows Forms Designer. Se eliminaron los helpers visuales, arrays y loops que reconstruian la interfaz; el comportamiento continua en los archivos `.cs` y la logica de negocio permanece en `capaLogica`.

Se conservaron nombres, jerarquia, layout, columnas, foto, validaciones y funcionalidad. No se modificaron los formularios restantes. Debug y Release compilaron sin errores; la inicializacion en memoria confirmo las filas y controles de las tres tablas principales. Permanece el warning preexistente de `GestionEjerciciosFormulario.components`. Pendiente: abrir manualmente cada formulario con "Ver disenador" en Visual Studio y realizar la prueba funcional con la base disponible; la apertura interactiva no se ejecuto desde esta consola.

## Layout de registro de usuarios — 18 de septiembre de 2026

Última actualización de esta sección: 18 de septiembre de 2026.

`GestionUsuariosFormulario.Designer.cs` adopta la organización visual de `GestionSociosFormulario`: listado flexible a la izquierda, ficha derecha de 396 px, separación y padding de 16 px, etiquetas de 116 px y nueve filas de 38 px con controles de ancho uniforme. Conserva Nombre, Apellido, DNI, Nacimiento, Usuario, Contraseña, Salario, Rol y Sexo. Las cinco acciones mantienen su orden en dos filas, con botones de 108 x 38 px y separación de 8 px para evitar recortes cuando aparece la barra vertical. La foto de usuario y sus botones existentes se ubican debajo de las acciones; no se agregan campos de socios, controles ni dependencias.

Solo se modificaron propiedades visuales en el Designer; el archivo `.cs`, los eventos, las validaciones, los filtros, las columnas y el comportamiento del DataGridView, las notificaciones, las capas inferiores, las entidades y SQL permanecen sin cambios. Se conservaron el namespace, los archivos parciales y los recursos del formulario.

Verificaciones: compilación Debug con MSBuild de Visual Studio en `bin/VerificacionLayoutUsuarios/`, sin errores; permanece el warning preexistente CS0649 de `GestionEjerciciosFormulario.components`. Se verificaron constructor, creación de controles y renderizado en memoria, nueve campos alineados y cinco botones completos a 900 x 560, 1100 x 680 y 1366 x 768 px de área cliente. La ficha conserva su ancho y separación, con desplazamiento vertical cuando hace falta y sin barra horizontal. Para la comprobación final se desconectó el handler Load únicamente en la instancia de verificación, sin modificar el código ni ejecutar operaciones de negocio. Pendiente: abrir manualmente «Ver diseñador» en Visual Studio y revisar el formulario dentro del panel administrador con la base disponible.

## Notificaciones clasicas de altas - 17 de septiembre de 2026

Ultima actualizacion de esta seccion: 17 de septiembre de 2026.

Se revisaron todos los formularios activos. Las altas de usuarios (administrador, entrenador y recepcionista), socios, planes, membresias, cuotas, pagos, ejercicios, imagenes, rutinas y ejercicios de rutina, junto con las asignaciones de entrenador y rutina, informan el resultado usando el Label `lblEstado` existente: `Color.Green` para exito, `Color.Red` para error y `Visible = true`. Se conserva el estilo MessageBox existente, sin controles ni paquetes nuevos.

`AyudaFormularioVisual.MostrarExito` y `MostrarError` reciben un parametro opcional `resaltar`; solo los flujos indicados lo activan. Los eventos compartidos de ejercicios y rutinas distinguen el alta de la edicion; asignar/cambiar entrenador o rutina comparten exactamente el mismo flujo. La creacion de rutina muestra su confirmacion despues de recargar el catalogo para evitar que el contador la reemplace. Usuarios conserva su error generico especifico; los demas usan el mensaje general existente o la causa de negocio disponible.

Verificaciones: Debug y Release compilados con MSBuild de Visual Studio usando `bin/VerificacionNotificaciones/` como salida, porque el ejecutable de Debug estaba bloqueado por un proceso abierto. `dotnet build` no pudo procesar los recursos del proyecto clasico (MSB3822/MSB3823); no se cambio la configuracion del proyecto. Permanece el warning CS0649 de `GestionEjerciciosFormulario.components` en el Designer previamente modificado. Los nueve formularios afectados se inicializaron y ejecutaron layout en memoria; se verificaron texto y color verde mediante el helper. Se revisaron las ramas de error y las dependencias: no hay cambios en logica, datos, entidades, EF6 ni SQL. Pendientes: altas reales y errores contra SQL Server, inspeccion visual de rojo/verde y apertura manual en el disenador de Visual Studio.

## Avisos de alta y foto opcional - 17 de septiembre de 2026

La creacion de usuarios desde `GestionUsuariosFormulario` ahora confirma el alta con un mensaje visual de Windows Forms y, ante errores, muestra la causa de negocio disponible o un mensaje general de alta fallida sin exponer detalles tecnicos. No se modificaron validaciones de campos, capas de logica ni persistencia.

Se verifico que el alta de socios ya admite foto opcional: `GestionSociosFormulario` envia `null` cuando no se selecciona imagen, `SocioLogica` solo copia archivos cuando recibe contenido, `Socio.FotoRuta` no es requerido y el DDL mantiene `Socio.FotoRuta NVARCHAR(260) NULL`. Queda pendiente probar manualmente ambos flujos contra una base disponible y abrir los formularios afectados en el diseñador de Visual Studio.

## Ajuste UX/UI de encabezados y rutinas — 17 de septiembre de 2026

Los paneles de rol presentan el módulo actual dentro del encabezado global, en una sola línea con el formato `Título | Subtítulo`, declarado en `Designer.cs` mediante `lblModuloActual`. Las franjas locales de los módulos activos quedan ocultas para que el contenido comience inmediatamente debajo. El acceso muestra `Contraseña` con ñ.

La ficha de asignaciones alinea etiquetas y controles en filas compartidas. Gestionar rutinas mantiene el patrón master/detail y separa Rutina seleccionada, Ejercicios de la rutina y Detalle del ejercicio; el editor conserva las acciones existentes y dispone de más altura útil.

## Reactivación de membresías y deuda — 16 de septiembre de 2026

La vuelta de un socio reutiliza su fila histórica de `Membresia`; la capa lógica bloquea altas duplicadas para socios con historial y el formulario reserva el alta para socios que todavía no tengan membresía. Dos cuotas `Pendiente` con `FechaHasta` anterior a hoy desactivan lógicamente la membresía y sincronizan el estado del socio. La evaluación central está en `MembresiaLogica` y se invoca desde consultas de membresías, cuotas, rutinas y asignaciones, cambios de cuotas/pagos y generación de cuotas. La reactivación valida el mismo umbral antes de cambiar el estado; no elimina ni reconstruye cuotas, pagos, entrenador o rutina. En la limpieza del 16 de septiembre se alineó el DDL inicial con el modelo EF6 de rutinas, sin modificar el DER.

## UX master/detail en ejercicios, asignaciones y rutinas — 11 de septiembre de 2026

Las tres pantallas priorizadas adoptan una composición de listado y detalle: filtros compactos arriba, grilla útil a la izquierda y ficha/acciones a la derecha. `GestionEjerciciosFormulario` trabaja con el modelo real de tres campos; `GestionAsignacionesFormulario` selecciona la membresía desde la grilla y mantiene nombre/DNI/plan/vencimiento antes de confirmar; `MisSociosFormulario` muestra la rutina semanal vigente inline y conserva el editor existente para crear o editar.

La corrección posterior de layout mantiene esa composición y elimina el colapso visual: los contenedores directos del `SplitContainer` llenan ambos paneles, las fichas no dependen de su `PreferredSize` y las proporciones se preservan al cambiar entre un área equivalente a 1366x768 y otra mayor. La verificación estructural confirmó grillas llenas, selección de fila única y ausencia de colapso al agregar filas.

Se agregaron únicamente resúmenes de lectura en `MembresiaEntrenadorLogica` y `RutinaLogica` para evitar que la capa visual consulte persistencia directamente. En esa corrección visual no se modificaron el DER, SQL Server, Entity Framework 6, navegación general ni las reglas de asignación. La verificación estructural en memoria de los tres constructores y `PerformLayout` fue exitosa; queda pendiente abrir manualmente «Ver diseñador» y probar el flujo completo con una base disponible.

## Identificacion de socios y entrenadores - 11 de septiembre de 2026

Los datos de prueba contienen nombres repetidos entre `UsuarioSistema` con rol Entrenador y `Socio` (por ejemplo, Benjamin Fernandez), con DNI diferentes. Las pantallas activas muestran nombre completo, DNI y la clave de membresia, manteniendo las claves separadas para asignaciones.

Gestion de asignaciones permite elegir una membresia desde un combo y usa su `IdMembresia`; ya no solicita escribir ese numero en un `TextBox`. Gestion de membresias permite cambiar el plan durante la edicion mediante la operacion existente `CambiarPlan`.


## Equivalencia visual de dashboards - 11 de septiembre de 2026

Se reparo la regresion visual de Administrador, Recepcionista y Entrenador. El layout base queda en 1200x760, con header `Dock=Top` de altura fija, menu de 264 px y contenido `Dock=Fill`; se quito el estado maximizado del diseno. `InicioPanelAdministrador` representa desde Designer el titulo, el pronostico y el estado de cuenta. El clima real sigue siendo exclusivamente runtime y agrega tarjetas al `FlowLayoutPanel` sin imponer posiciones.

Debug y Release compilan correctamente sin errores ni warnings reportados. Falta abrir manualmente los tres dashboards con "Ver disenador" y recorrer el flujo de membresia y asignacion contra una base disponible.

## Rutinas personalizadas — 11 de septiembre de 2026

Desde “Mis socios”, se puede asignar una rutina existente o crear una personalizada para la membresía seleccionada. La nueva rutina se vincula mediante `Membresia.IdRutina`; el editor mantiene el catálogo reutilizable y sus ejercicios. La disponibilidad no depende del plan.

Última actualización: 17 de septiembre de 2026.

SysGym es una aplicación de escritorio en C# Windows Forms para gestionar un gimnasio. Utiliza .NET Framework 4.8, Entity Framework 6.4.4 y SQL Server.

La solución `exxen2.0.slnx` contiene un único proyecto clásico. Las carpetas `capaVisual`, `capaLogica` y `capaDatos` representan las capas dentro del mismo ensamblado.

## Menú y respaldo del pronóstico — 11 de septiembre de 2026

Los menús laterales permiten contraer y expandir sus categorías para reducir opciones visibles. El pronóstico se consulta para Corrientes Capital y se conserva localmente con el día actual y los siete siguientes; si no hay internet se presenta el último respaldo disponible.

El modelo aprobado incluye Rol, UsuarioSistema, Socio, Plan, Membresia, MembresiaEntrenador, CuotaMembresia, Pago, MetodoPago, MercadoPago, PagoEfectivo, Divisa, Rutina, RutinaEjercicio y Ejercicio. `Membresia.IdRutina` nullable es la única relación de rutina con membresías.

La base se crea mediante `capaDatos/Database/SysGymDB.sql`. La integración real con Mercado Pago, reportes y procesos automáticos no forman parte del alcance actual.

## Estado de interfaz y fotos — 9 de septiembre de 2026

Los paneles usan contenedores estándar con Dock estructural y Anchor para controles editables, además de FotoRuta/Sexo opcionales en socios y usuarios. Las fotos personalizadas se copian a `Datos/Imagenes` y SQL Server conserva rutas relativas; sin foto se muestran avatares embebidos. La estructura de los formularios permanece en `Designer.cs`.

## Auditoría visual estructural — 11 de septiembre de 2026

Los formularios activos conservan su estructura en `Designer.cs`, con paneles, grillas, filtros, campos y acciones visibles para el diseñador. Se corrigieron desbordes al tamaño mínimo, formularios inferiores horizontales y contenedores de detalle insuficientes. Las variantes antiguas excluidas del proyecto se retiraron tras comprobar que no tenían consumidores y que la navegación usa sus reemplazos activos.

## Ajuste puntual de layout master/detail - 16 de septiembre de 2026

Se corrigió la regresión visual de las pantallas de ejercicios, asignaciones y socios/rutinas sin cambiar su flujo funcional. GestionEjercicios conserva una división aproximada 56/44, GestionAsignaciones 55/45 y MisSocios 33/67. Las grillas y fichas ocupan los paneles completos; la ficha de asignaciones distribuye sus siete filas con porcentaje y el listado de socios evita scroll horizontal mediante mínimos compatibles con su panel.

El layout queda en los archivos `Designer.cs`; los formularios solo cargan datos, filtros, selección y acciones. No se modificaron las clases de lógica, el esquema, SQL Server ni Entity Framework en esta corrección. Las verificaciones estructurales se realizaron en memoria para 1100/1180 px y una ventana mayor; Debug y Release compilan sin errores ni warnings.

## Ajuste UX/UI de módulos - 17 de septiembre de 2026

Los paneles principales de Administrador, Recepcionista y Entrenador se abren maximizados desde Designer. El encabezado global muestra el módulo activo en una columna central porcentual con el formato `Título | Subtítulo`; no se cambia el menú lateral.

Usuarios y roles conserva su formulario vertical, pero organiza los campos en `TableLayoutPanel` y mantiene la fotografía completa con `PictureBoxSizeMode.Zoom`. Gestionar rutinas usa un `SplitContainer` adaptable: listado de rutinas a la izquierda y detalle a la derecha, con acciones de rutina separadas de las acciones de ejercicios. El detalle del ejercicio queda agrupado debajo de su grilla y sus botones se habilitan según la selección.

La verificación estructural de `InitializeComponent`, `PerformLayout` y redimensionamiento se realizó para 1280x720, 1366x768, 1600x900 y 1920x1080. Esta pasada no modificó reglas de negocio, DER, SQL ni Entity Framework; queda pendiente la apertura manual del diseñador de Visual Studio y la prueba interactiva completa.

## Modelo de rutinas y limpieza — 16 de septiembre de 2026

EF6 y `capaDatos/Database/SysGymDB.sql` representan una relación opcional de una rutina por membresía: `Membresia.IdRutina INT NULL`, con una FK hacia `Rutina`. Una rutina puede reutilizarse en varias membresías y sus ejercicios siguen relacionados mediante `RutinaEjercicio`. Se retiraron la entidad intermedia, su lógica, las tablas/semillas paralelas y los formularios legacy sin referencias. El DDL es para bases nuevas; no se distribuye una migración de bases existentes. Los `.resx` dudosos se conservaron para no afectar Visual Studio Designer.

## Imágenes portables — 17 de septiembre de 2026

La foto de Socio y de UsuarioSistema se persiste mediante `FotoRuta`, con rutas relativas a `Datos/Imagenes/Socios` y `Datos/Imagenes/Usuarios`. Los avatares masculino y femenino están embebidos en `Properties.Resources`. El catálogo de Ejercicios usa `EjercicioImagen` para conservar varias imágenes ordenadas por ejercicio; la galería de `GestionEjerciciosFormulario` permite agregarlas y quitarlas. La exportación PDF queda expresamente fuera de esta fase.
