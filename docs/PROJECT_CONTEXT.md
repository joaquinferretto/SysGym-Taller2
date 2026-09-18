# Contexto del proyecto

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
