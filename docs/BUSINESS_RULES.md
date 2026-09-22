# Reglas de negocio

## Generación manual de cuotas y catálogo de métodos de pago — 22/09/2026

- `Nuevo` en Gestión de membresías solo prepara el alta: limpia la selección y errores, carga opciones, habilita Socio/Plan y propone las fechas. No crea membresía ni cuota y no guarda cambios.
- `Crear` da de alta una membresía y genera exactamente una primera cuota en la misma transacción. `Actualizar` modifica los datos permitidos de la membresía seleccionada y no genera cuotas.
- `Generar cuota` consulta todas las cuotas de la membresía, incluidas las anuladas para conservar la secuencia cronológica, y toma la mayor `FechaHasta`. La nueva `FechaDesde` es esa fecha más un día y la nueva `FechaHasta` se calcula con `FechaDesde.AddMonths(1).AddDays(-1)`. No usa la fecha actual para elegir el período y crea exactamente una cuota.
- Antes de agregar, la lógica comprueba que no exista ya la misma combinación de membresía, `FechaDesde` y `FechaHasta`; si existe, rechaza con «Ya existe una cuota para ese período.».
- La definición compartida de deuda sigue siendo `EstadoPago = Pendiente` y `FechaHasta < DateTime.Today`. Con cero o una cuota vencida impaga se permite generar; con dos o más se bloquea, se conserva la sincronización vigente de membresía/socio inactivos y no se agrega ninguna cuota. Una membresía inactiva tampoco puede generar aunque tenga menos deuda.
- Pagar deuda no reactiva automáticamente. La reactivación continúa siendo manual y exige menos de dos cuotas vencidas pendientes.
- El selector de método presenta una opción por tipo estructural activo de `MetodoPago`. `IdPagoEfectivo` identifica **Efectivo** e `IdNroPagoMP` identifica **Mercado Pago**; `Observaciones` históricas no son la fuente del texto visible. La opción conserva un `IdMetodoPago` real y la ausencia de selección bloquea el registro con «Seleccioná un método de pago.».

## Auditoría global de validaciones — 21/09/2026

- Nombres y apellidos son obligatorios, admiten letras Unicode, espacios, apóstrofe y guion, y rechazan números u otros símbolos. El máximo compartido vigente es 100 caracteres.
- DNI es obligatorio para socios y usuarios, contiene exclusivamente dígitos y tiene un máximo compartido de 20 caracteres. La unicidad continúa en SocioLogica y UsuarioSistemaLogica.
- NombreUsuario es una identidad distinta del nombre personal: admite letras, números, punto, guion bajo y guion, con máximo 50. `entrenador10` es válido.
- Usuarios mantienen edad mínima de 18 años; socios, 13. Las fechas futuras se rechazan.
- Precio, salario, importe y los valores físicos conservan sus reglas actuales. El precio, salario e importe deben ser mayores que cero; peso y altura son opcionales pero positivos si se informan; la altura conserva parte decimal. En rutinas, peso y descanso admiten cero, mientras series, repeticiones y orden deben ser positivos.
- Foto de socio/usuario, imágenes de ejercicio, sexo, descripciones y buscadores permanecen opcionales. Ninguna validación visual sustituye las comprobaciones de `capaLogica`.

## Política vigente de imágenes y entrenador opcional — 21/09/2026

- Socio y UsuarioSistema admiten como máximo una foto opcional. Ejercicio admite de cero a cuatro filas ordenadas en EjercicioImagen.
- Toda imagen externa usa el mismo procesador: máximo 10 MB y 40 MP, firma y decodificación reales, EXIF, contain sin crop sobre 800×800, JPEG 90 o PNG con transparencia, GUID y ruta relativa. El original y su metadata no se almacenan.
- Una membresía se crea con Socio, Plan y usuario que registra, además de su primera cuota. No requiere entrenador ni crea `MembresiaEntrenador` automáticamente.
- La asignación de entrenador es posterior y opcional mediante Asignar entrenador. Una membresía sin relación activa debe mostrarse como «Sin asignar» y puede generar cuotas normalmente.

## Cardinalidad de fotos confirmada — 20 de septiembre de 2026

- Ejercicio puede tener cero, una o varias imágenes demostrativas, guardadas en EjercicioImagen con ruta y orden; agregar no reemplaza las anteriores. Quitar elimina únicamente la imagen seleccionada. Sin imágenes, Quitar muestra «No existe imagen para quitar»; con imágenes y sin selección se solicita seleccionar una.
- Socio y UsuarioSistema tienen cada uno una única foto opcional mediante FotoRuta; no se cambia su comportamiento.
- La futura función PDF podrá consultar todas las imágenes del ejercicio por IdEjercicio. No se agregó exportación PDF en esta tarea.

## Notificaciones clasicas de altas - 17 de septiembre de 2026

Ultima actualizacion de esta seccion: 17 de septiembre de 2026.

Las altas y registros informan exito en verde y error en rojo desde la capa visual. Se mantienen las validaciones actuales: ante `InvalidOperationException` o `ArgumentException` se presenta la causa disponible; para los demas errores se usa "No se pudo completar la operacion." (con acento en el codigo), salvo usuarios, que conserva "No se pudo crear el usuario.". No se presentan stack traces ni excepciones internas. No se modificaron reglas de negocio, entidades ni esquema SQL.

Se compilaron Debug y Release y se revisaron las ramas de notificacion. Queda pendiente ejecutar altas y rechazos de negocio contra una base disponible.

## Avisos de alta y foto opcional - 17 de septiembre de 2026

- El alta de usuarios informa el resultado en la capa visual: confirma cuando el usuario se crea correctamente y, si falla, muestra una causa de negocio disponible o un mensaje general de alta fallida.
- La foto del socio es opcional al crear o modificar. Si no se selecciona imagen, `FotoRuta` permanece en `NULL`; si se selecciona una imagen valida, se conserva el guardado administrado en `Datos/Imagenes/Socios`.

## Identidad y habilitacion de entrenador - 11 de septiembre de 2026

- `UsuarioSistema` y `Socio` son entidades distintas. Pueden compartir nombre o apellido en los datos de prueba, pero se distinguen por su rol, clave e identidad (DNI).
- La asignacion se realiza sobre una `Membresia` seleccionada por `IdMembresia`; el entrenador se selecciona por `IdUsuarioSistema`. La interfaz muestra nombre completo y DNI para evitar confusiones.
- El cambio de plan desde Gestión de membresías conserva la historia de la membresía. Todos los planes incluyen entrenador; la asignación del entrenador se gestiona por membresía en `MembresiaEntrenador`.

## Rutinas reutilizables — 16 de septiembre de 2026

Una rutina puede reutilizarse en varias membresías. Cada membresía tiene cero o una rutina, determinada únicamente por `Membresia.IdRutina`; ningún atributo del plan habilita o restringe esta relación. Una rutina personalizada se crea desde la membresía seleccionada y queda vinculada a ella. Sus ejercicios se gestionan mediante `RutinaEjercicioLogica`, indicando un día entre lunes y viernes.

Última actualización: 17 de septiembre de 2026.

## Validaciones de altas y edición — 17 de septiembre de 2026

- Socios y usuarios validan en capa lógica nombres y apellidos compuestos únicamente con letras, espacios, apóstrofes o guiones; el DNI conserva el formato numérico sin puntos ni letras y mantiene su unicidad.
- La edad se calcula considerando el cumpleaños. Un socio debe tener al menos 13 años y un usuario del sistema al menos 18; las fechas futuras se rechazan.
- Peso, altura, salario, precio e importes se validan como valores numéricos con sus rangos actuales. La altura del socio conserva la regla existente de expresarse en metros con parte decimal.
- Una relación `RutinaEjercicio` nueva o modificada requiere rutina, ejercicio, día, orden, series y repeticiones válidos; peso y descanso no pueden ser negativos. Quitar la relación es una baja lógica y no elimina el ejercicio del catálogo.

## Membresías y cuotas

- Una membresía pertenece a un socio, un plan y al usuario del sistema que la registra.
- Al crear una membresía se genera su primera cuota en la misma transacción.
- Las cuotas son mensuales: `FechaHasta = FechaDesde.AddMonths(1).AddDays(-1)`.
- `CuotaMembresia.Importe` conserva el precio histórico del plan.
- Una cuota sin pago debe tener `IdRegistroPago = NULL`.
- Los registros históricos se conservan y se usan bajas lógicas cuando corresponde.
- Cada socio conserva la misma fila de `Membresia` al volver al gimnasio. Si existe cualquier membresía histórica, no se crea otra: se reactiva la existente mediante su `IdMembresia`.
- `MembresiaLogica` es la fuente única de la regla de deuda: dos o más cuotas con `EstadoPago = Pendiente` y `FechaHasta < hoy` dan de baja lógicamente la membresía y sincronizan el estado del socio. Cero o una cuota vencida no la dan de baja; las cuotas anuladas no cuentan como deuda pendiente.
- La deuda se evalúa al consultar membresías/cuotas, rutinas y asignaciones vigentes, generar cuotas y registrar o actualizar pagos. Regularizar no reactiva automáticamente: Reactivar permite el alta lógica de la misma membresía solo si quedan menos de dos cuotas vencidas pendientes.
- Las bajas y reactivaciones no eliminan cuotas, pagos, asignaciones de entrenador ni rutina. El estado del socio se actualiza desde la lógica de membresías, sin baja manual independiente.

## Pagos

- Solo los pagos aprobados se contabilizan.
- El importe no puede ser positivo por encima del importe de la cuota.
- `Pago` se vincula a las cuotas mediante `CuotaMembresia.IdRegistroPago`, según el DER aprobado.
- Un método de pago usa un detalle Mercado Pago o un detalle efectivo; la lógica rechaza métodos con ambos detalles.

## Roles y planes

- Administradores y recepcionistas activos pueden registrar membresías.
- Cada membresía puede crearse sin entrenador. La asignación posterior vincula opcionalmente un usuario activo con rol Entrenador mediante `MembresiaEntrenador`.
- Los planes no tienen indicadores de inclusión de entrenador o rutina ni catálogo de rutinas. La relación con la rutina es independiente del plan.

## Datos físicos del socio

- Cuando se registra la altura, debe expresarse en metros con una parte decimal; por ejemplo, `1,80`. No se admite un valor entero.

## Flujo de rutinas

- El entrenador puede crear ejercicios mediante `EjercicioLogica`.
- El entrenador crea una rutina general mediante `RutinaLogica`; la plantilla no pertenece a un socio y puede reutilizarse.
- `Membresia.IdRutina` es la fuente de verdad de la rutina asignada. La relación permite reutilizar una rutina en varias membresías, con un máximo de una por membresía.
- Asignar una rutina existente, crear una personalizada o cambiarla solo establece o reemplaza `Membresia.IdRutina`; la rutina anterior y sus ejercicios se conservan.
- Los ejercicios se incorporan a la rutina mediante `RutinaEjercicioLogica`, con series, repeticiones, peso, descanso y orden.

## Correcciones implementadas — 8 de septiembre de 2026

- **FIX-02:** el importe de un pago aprobado debe ser mayor que cero y no superar el importe histórico de su cuota. Registro, actualización y cambio de estado comparten `ValidarImporteAprobado`. Aprobar un pendiente excesivo falla antes de modificar su estado. No se cambia la regla pendiente sobre completar pagos parciales.
- El recálculo de deuda posterior a cambios de pagos/cuotas considera los estados recién guardados dentro de la misma transacción.

## Foto y sexo — 9 de septiembre de 2026

- Socios y usuarios pueden guardar una foto opcional; los ejercicios admiten hasta cuatro imágenes. El límite central es 10 MB y 40 MP por archivo, con JPEG, PNG, BMP, GIF, TIFF y WebP detectados por su contenido real.
- Sexo admite únicamente M, F o NULL y se usa para resolver el avatar. No se asigna un sexo por defecto; Nuevo deja el combo sin selección.
- La foto guardada tiene prioridad sobre el avatar por sexo. Sin foto se usa el recurso embebido disponible.
- Quitar foto conserva el sexo elegido y vuelve al avatar. Guardar o actualizar persiste `FotoRuta` y Sexo.
- Los listados de gestión recuperan la ruta cuando la necesitan; el archivo permanece fuera de SQL Server.
- La imagen se recodifica a un archivo nuevo de 800×800, sin metadata y sin deformación; una sustitución fallida conserva la foto anterior.

## Rutina semanal del socio

La rutina de un socio se organiza de lunes a viernes. El socio conserva una unica plantilla asignada a su membresia y esa plantilla contiene la semana completa: cada `RutinaEjercicio` indica su dia en `DiaSemana`, con 1 para lunes y 5 para viernes, y su posicion dentro de ese dia en `Orden`.

`DiaSemana` admite nulo para los ejercicios que todavia no tienen dia asignado; esos ejercicios no aparecen en la consulta semanal y se informan en la barra de estado. `ValidacionesGimnasio.ValidarDiaRutina` rechaza cualquier valor fuera del rango y `ValidacionesGimnasio.NombreDia` traduce el numero al nombre que se muestra.

`RutinaEjercicioLogica.ListarSemanaPorSocio` resuelve la membresia activa del socio y consulta la rutina indicada por `Membresia.IdRutina`. Si el socio no tiene membresia activa o no tiene rutina asignada devuelve una lista vacia, sin lanzar excepcion.

## Imágenes de socios y ejercicios — 17 de septiembre de 2026

- `Socio.FotoRuta` es opcional y contiene únicamente una ruta relativa como `Imagenes\\Socios\\{guid}.jpg`; no se guardan Base64 ni imágenes en `varbinary` para socios.
- Sin foto personalizada, el formulario usa los avatares embebidos `socio_hombre_default` o `socio_mujer_default` según `Sexo`. Si falta una ruta física, se conserva ese fallback y la pantalla no falla.
- Las imágenes se aceptan solo en JPG, JPEG o PNG, se validan y se copian bajo `Datos/Imagenes` con nombres GUID. Quitar una foto deja `FotoRuta` en NULL y elimina el archivo administrado solo si no tiene otra referencia.
- `EjercicioImagen` permite cero o varias imágenes por ejercicio. Cada registro conserva `RutaRelativa` y `Orden`; agregar usa el siguiente orden disponible y quitar elimina la relación y su archivo cuando no está compartido. Las rutinas consultan las imágenes a través de `Ejercicio`, sin duplicarlas en `RutinaEjercicio`.
