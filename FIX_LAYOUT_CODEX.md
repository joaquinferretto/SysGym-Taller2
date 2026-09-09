# Encargo para Codex: volver a controles de posicion libre

Rama de trabajo: `estilos`. No hacer commit ni push. No hacer merge, rebase, reset, checkout ni restore.

## Objetivo

La adaptacion al tamano de la ventana que tiene hoy el proyecto **esta bien y hay que conservarla**: contenido que ocupa toda la ventana al maximizar, sin franja gris, sin textos cortados y sin barras de desplazamiento innecesarias. El unico problema a resolver es el de edicion, descrito abajo. No hay que sacrificar lo primero para lograr lo segundo.

Los formularios deben poder editarse arrastrando controles en el disenador de Visual Studio, **sin que mover un control desplace a los demas**. Hoy no se puede: la maquetacion usa `TableLayoutPanel` y `FlowLayoutPanel`, que posicionan por celda, de modo que cambiar un control reacomoda a sus vecinos.

Al mismo tiempo, el contenido debe seguir ocupando toda la ventana cuando se maximiza. Antes de la maquetacion actual, todos los controles tenian `Dock = None`, `Anchor = Top | Left` y coordenadas fijas calculadas para un cliente de 1200x760: en una ventana maximizada el contenido quedaba confinado al sector superior izquierdo y sobraba una franja gris a la derecha y abajo. **No hay que volver a ese estado.**

La solucion es intermedia y se apoya en dos reglas:

- `Dock` **solo** en los tres o cuatro paneles estructurales de cada formulario. Son contenedores de armazon, nunca se arrastran.
- `Anchor` en todo lo que este dentro de esos paneles. `Anchor` fija distancias a los bordes del panel padre, asi que cada control se estira o se desplaza por su cuenta: mover uno **no** afecta a ningun otro. Es la propiedad que da adaptacion sin correlacion.

Todo lo demas conserva `Location` y `Size` propios y se puede arrastrar libremente.

## Regla general

Para cada control, elegir el `Anchor` segun lo que tenga que hacer al agrandar la ventana:

| Que debe pasar al agrandar | Anchor |
| --- | --- |
| Queda arriba a la izquierda, tamano fijo (etiquetas, titulos) | `Top, Left` |
| Se pega al borde derecho, tamano fijo (boton Volver, combos de la derecha) | `Top, Right` |
| Se ensancha (cajas de texto, combos de formulario, buscador) | `Top, Left, Right` |
| Se ensancha y se estira en alto (grillas) | `Top, Bottom, Left, Right` |
| Sigue al borde inferior, alto fijo (botones bajo una grilla) | `Bottom, Left` o `Bottom, Right` |

`AutoSize` queda en `false` en todos los controles con `Anchor` distinto de `Top, Left`, para que el tamano sea el que se ve en el disenador.

## Paso 1 — Deshacer los contenedores agregados

En los 17 `.Designer.cs` de `capaVisual`, convertir de nuevo a `Panel` estandar todos los contenedores que hoy son `TableLayoutPanel` o `FlowLayoutPanel`, y eliminar los dos contenedores que se agregaron.

Por cada contenedor a revertir hay que:

1. Cambiar la declaracion del campo: `private TableLayoutPanel X;` → `private Panel X;` (idem `FlowLayoutPanel`).
2. Cambiar la instancia: `X = new TableLayoutPanel();` → `X = new Panel();`.
3. Borrar las lineas `X.ColumnCount`, `X.RowCount`, `X.ColumnStyles.Add(...)`, `X.RowStyles.Add(...)`, `X.SetRowSpan(...)`, `X.SetColumnSpan(...)`, `X.AutoSizeMode`, y en los `FlowLayoutPanel` tambien `X.FlowDirection` y `X.WrapContents`.
4. Cambiar los `X.Controls.Add(c, columna, fila);` por `X.Controls.Add(c);`.
5. Darle a cada hijo un `Location` y un `Size` explicitos, tomados de como se ve hoy la pantalla en ejecucion.

Contenedores a revertir por archivo:

| Archivo | Contenedores a volver a `Panel` |
| --- | --- |
| `Administrador/PanelAdministrador.Designer.cs` | `panelEncabezado` |
| `Administrador/InicioPanelAdministrador.Designer.cs` | `principal`, `panelCabecera`, `cabeceraClima`, `cabeceraCuotas` |
| `Administrador/GestionUsuariosFormulario.Designer.cs` | `panelEncabezado`, `contenedorContenido`, `panelListado`, `panelFiltro`, `contenedorDetalle`, `contenedorCampos`, `panelAcciones` |
| `Administrador/GestionPlanesFormulario.Designer.cs` | los siete anteriores y ademas `panelBeneficios` |
| `Administrador/ConsultaRutinasAdministradorFormulario.Designer.cs` | `panelEncabezado`, `barraAcciones` |
| `Administrador/ReportesFormulario.Designer.cs` | `panelEncabezado`, `barraAcciones` |
| `Autenticacion/InicioSesion.Designer.cs` | `contenido` |
| `Compartido/GestionSociosFormulario.Designer.cs` | `panelEncabezado`, `contenedorContenido`, `panelListado`, `panelFiltro`, `contenedorDetalle`, `contenedorCampos`, `panelAcciones` |
| `Compartido/GestionEjerciciosFormulario.Designer.cs` | `panelEncabezado`, `barraAcciones`, `panelContenido`, `contenedorFormulario` |
| `Compartido/GestionAsistenciasFormulario.Designer.cs` | `panelEncabezado`, `barraAcciones`, `panelContenido`, `contenedorFormulario` |
| `Recepcionista/PanelRecepcionista.Designer.cs` | `panelEncabezado` |
| `Recepcionista/GestionMembresiasFormulario.Designer.cs` | `panelEncabezado`, `contenedorContenido`, `panelListado`, `contenedorDetalle`, `contenedorCampos`, `panelAcciones` |
| `Recepcionista/GestionPagosFormulario.Designer.cs` | `panelEncabezado`, `contenedorContenido`, `panelListado`, `panelFiltro`, `contenedorDetalle`, `contenedorCampos`, `panelAcciones` |
| `Recepcionista/GestionAsignacionesFormulario.Designer.cs` | `panelEncabezado`, `barraAcciones`, `panelContenido`, `contenedorFormulario` |
| `Entrenador/PanelEntrenador.Designer.cs` | `panelEncabezado` |
| `Entrenador/RutinasEntrenadorFormulario.Designer.cs` | `panelEncabezado`, `barraAcciones`, `panelContenido`, `contenedorFormulario` |
| `Entrenador/MisSociosFormulario.Designer.cs` | `panelEncabezado`, `barraAcciones` |

Contenedores agregados que hay que **eliminar por completo** (campo, instancia, `SuspendLayout`/`ResumeLayout` y todas sus lineas), pasando sus hijos al panel que los contiene:

- `tablaOpciones` en `PanelAdministrador`, `PanelRecepcionista` y `PanelEntrenador`: sus hijos vuelven a `panelOpciones.Controls.Add(...)`.
- `contenido` en `InicioSesion`: sus hijos vuelven a `Controls.Add(...)` del formulario, y `barraSuperior` se agrega ultimo para que quede arriba.

En `InicioSesion` hay que respetar el orden de `Controls.Add`: el `Dock` se resuelve del ultimo agregado al primero.

## Paso 2 — Armazon con Dock (unicos controles con Dock)

Estos son los unicos controles que conservan `Dock`. No se arrastran; su tamano se cambia por la propiedad `Height` o `Width`.

En `PanelAdministrador`, `PanelRecepcionista` y `PanelEntrenador`:

- `panelEncabezado` → `Dock = Top`, `Height = 90`
- `panelMenu` → `Dock = Left`, `Width = 264`
- `panelPie` → `Dock = Bottom`, `Height = 76` (dentro de `panelMenu`)
- `panelOpciones` → `Dock = Fill`, `AutoScroll = true` (dentro de `panelMenu`)
- `panelContenido` → `Dock = Fill`

En `InicioPanelAdministrador`:

- `principal` → `Dock = Fill`, `AutoScroll = true`

En los formularios de modulo (Socios, Usuarios, Planes, Membresias, Pagos, Ejercicios, Asistencias, Asignaciones, Rutinas, Mis socios, Consulta de rutinas, Reportes):

- `panelEncabezado` → `Dock = Top`, `Height = 84`
- `barraAcciones` → `Dock = Top`, `Height = 52`; donde no tiene botones propios (Socios, Usuarios, Planes, Membresias, Pagos) dejarla en `Height = 1` como separador
- `lblEstado` → `Dock = Bottom`, `Height = 30`
- `panelContenido` → `Dock = Fill`

En `InicioSesion`:

- `barraSuperior` → `Dock = Top`, `Height = 7`

Ningun formulario lleva `AutoScroll` a nivel de ventana: dejar `this.AutoScroll = false`. El desplazamiento queda solo en `panelOpciones`, en `panelDetalle` y en `listaClima`.

## Paso 3 — Anchor dentro de cada panel

Todo lo que este dentro de los paneles del paso 2 lleva `Dock = None`, `Location` y `Size` propios, y el `Anchor` que corresponda.

**Encabezado de cualquier formulario o panel** (`panelEncabezado`):

- `lblTitulo` / `lblMarca` → `Top, Left`
- `lblDescripcion` / `lblUsuarioRol` → `Top, Left`
- `btnVolver` / `btnCambiarCuenta` → `Top, Right`

**Menu lateral** (`panelOpciones`): las etiquetas de seccion y los botones quedan en `Top, Left` con el ancho que tengan. Si se quiere que acompanen el ancho del menu, usar `Top, Left, Right`. Como el menu tiene ancho fijo, `Top, Left` alcanza.

**Pantallas con listado y detalle** (Socios, Usuarios, Planes, Membresias, Pagos), dentro de `panelContenido`:

- `contenedorContenido` → `Anchor = Top, Bottom, Left, Right`, ocupando `panelContenido` menos el `Padding`
- `panelListado` → `Anchor = Top, Bottom, Left, Right`
- `panelDetalle` → `Anchor = Top, Bottom, Right` con ancho fijo de 396, `AutoScroll = true`
- dentro de `panelListado`: `lblListado` y `lblAyuda` en `Top, Left`; `panelFiltro` (o `buscador` en Membresias) en `Top, Left, Right`; `tabla` en `Top, Bottom, Left, Right`
- dentro de `panelFiltro`: `buscador` en `Top, Left, Right`; `lblFiltro` y `filtroEstado` en `Top, Right`
- dentro de `panelDetalle`: `contenedorDetalle` en `Top, Left, Right`; `lblFormulario` en `Top, Left`; `contenedorCampos` en `Top, Left, Right`; `panelAcciones` en `Top, Left, Right`
- dentro de `contenedorCampos`: cada etiqueta en `Top, Left`; cada campo en `Top, Left, Right`
- dentro de `panelAcciones`: los botones en `Top, Left`, con `Location` y `Size` propios

**Pantallas con barra de acciones** (Ejercicios, Asistencias, Asignaciones, Rutinas), dentro de `panelContenido`:

- `tabla` → `Anchor = Top, Bottom, Left, Right`
- `panelFormulario` → `Anchor = Bottom, Left, Right`, con el alto que necesiten sus campos
- dentro de `barraAcciones`: los botones, `lblEstadoFiltro` y `filtroEstado` en `Top, Left`, con coordenadas propias
- dentro de `contenedorFormulario`: etiquetas en `Top, Left`, campos en `Top, Left`

**Pantallas de solo consulta** (Mis socios, Consulta de rutinas): `tabla` en `Top, Bottom, Left, Right`. En Reportes, `resumen` en `Top, Bottom, Left, Right`.

**InicioPanelAdministrador**, dentro de `principal`:

- `panelCabecera` → `Top, Left, Right`; `lblResumen` y `lblFecha` dentro, en `Top, Left`
- `tarjetaClima` → `Top, Left, Right`; `cabeceraClima` en `Top, Left, Right`; `listaClima` en `Top, Bottom, Left, Right` con `AutoScroll = true`
- dentro de `cabeceraClima`: `tituloClima` en `Top, Left`, `estadoClima` en `Top, Right`
- `tarjetaSuscripcion` → `Top, Left, Right`; `lblSuscripcion` dentro, en `Top, Left, Right`
- `tarjetaCuotas` → `Top, Bottom, Left, Right`; `cabeceraCuotas` en `Top, Left, Right`; `tablaCuotas` en `Top, Bottom, Left, Right`
- dentro de `cabeceraCuotas`: `lblTituloCuotas` en `Top, Left`, `resumenCuotas` en `Top, Right`
- `tarjetaClimaEjemplo` y sus cinco etiquetas quedan como estan: son la plantilla que `InicioPanelAdministrador.cs` clona en ejecucion. No tocar sus coordenadas ni convertirlas en contenedores.

**InicioSesion**: es un dialogo `FixedSingle` de tamano fijo. Todos sus controles quedan en `Top, Left` con coordenadas propias. Verificar que ninguna etiqueta quede cortada: `lblTitulo` necesita ancho para "SYSGYM" a 28pt, `lblNombreUsuario` para "Usuario" y `lblClave` para "Contrasena".

## Paso 4 — Listados en DataGridView

Todo listado de datos se muestra en un `DataGridView`, con aspecto de tabla: encabezado de columnas arriba y una fila por registro. No usar `ListBox`, `ListView`, etiquetas concatenadas ni texto formateado a mano para listar registros.

El proyecto ya cumple esto en las doce pantallas con listado. Mantenerlo y aplicarlo a cualquier listado nuevo, con esta configuracion:

- `Dock = None` y `Anchor = Top, Bottom, Left, Right`, para que la tabla crezca con la ventana.
- `AutoSizeColumnsMode = Fill`, para que las columnas repartan todo el ancho y no quede espacio muerto a la derecha.
- Proporciones con `FillWeight` y ancho minimo con `MinimumWidth`. **No** asignar `Column.Width` en modo `Fill`: WinForms recalcula `FillWeight` con el ancho de diseno y las proporciones quedan mal (era la causa de que el encabezado "Nacimiento" apareciera cortado como "Nacimier").
- `ReadOnly = true`, `AllowUserToAddRows = false`, `AllowUserToDeleteRows = false`, `AllowUserToResizeRows = false`, `RowHeadersVisible = false`, `MultiSelect = false`, `SelectionMode = FullRowSelect`.
- `ColumnHeadersHeight` suficiente para encabezados de dos lineas (46 donde los titulos son largos, como en `tablaCuotas`).
- Las columnas se declaran en el disenador como `DataGridViewTextBoxColumn`, no se generan solas. La columna de identificador va con `Visible = false`.
- Las filas se cargan desde `capaVisual` con `tabla.Rows.Add(...)`, tomando los datos de `capaLogica`. Nunca consultar `DbContext` desde la capa visual.

## Paso 5 — Imagen en un PictureBox

Para mostrar una imagen en un `PictureBox` hay dos caminos segun de donde venga la imagen. En ambos, el control se configura igual:

- `SizeMode = PictureBoxSizeMode.Zoom`, para que la imagen entre completa sin deformarse. `StretchImage` la deforma y `Normal` la recorta.
- `Anchor` segun el caso, igual que cualquier otro control del paso 3.
- `BorderStyle = FixedSingle` si se quiere marco.

**a) Imagen fija de la aplicacion (logo, icono, imagen decorativa).** Va como recurso del proyecto:

1. Copiar el archivo a una carpeta del repositorio, por ejemplo `capaVisual/Recursos/logo.png`.
2. Agregarlo a `Properties/Resources.resx` desde Visual Studio (Propiedades del proyecto, pestana Recursos, Agregar recurso). Eso genera la propiedad en `Properties/Resources.Designer.cs`.
3. Asignarla en el disenador o en el `.Designer.cs`:

```csharp
this.pictureBoxLogo.Image = global::exxen2._0.Properties.Resources.logo;
this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
```

No incrustar la imagen como base64 dentro del `.resx` del formulario a mano.

**b) Imagen variable por registro (por ejemplo, la foto de un socio).** No se guarda en el disenador: se carga en ejecucion desde el `.cs` del formulario.

Si la imagen esta en disco:

```csharp
/* Carga la imagen del socio sin bloquear el archivo de origen. */
private void MostrarFoto(string ruta)
{
    if (string.IsNullOrWhiteSpace(ruta) || !System.IO.File.Exists(ruta))
    {
        fotoSocio.Image = null;
        return;
    }

    using (var original = System.Drawing.Image.FromFile(ruta))
        fotoSocio.Image = new System.Drawing.Bitmap(original);
}
```

La copia a `Bitmap` es necesaria: `Image.FromFile` deja el archivo bloqueado mientras la imagen viva, y no se podria reemplazar ni borrar.

Si la imagen viene de la base como `byte[]`:

```csharp
/* Reconstruye la imagen almacenada en la entidad para mostrarla en pantalla. */
private void MostrarFoto(byte[] contenido)
{
    if (contenido == null || contenido.Length == 0)
    {
        fotoSocio.Image = null;
        return;
    }

    using (var memoria = new System.IO.MemoryStream(contenido))
        fotoSocio.Image = System.Drawing.Image.FromStream(memoria);
}
```

Para que el usuario elija el archivo, usar `OpenFileDialog` con `Filter = "Imagenes|*.png;*.jpg;*.jpeg;*.bmp"` y quedarse con `openFileDialog.FileName`.

Reglas de capas que hay que respetar si se agrega foto a una entidad:

- La propiedad `byte[]` o la ruta se declara en la entidad de `capaDatos/Entidades`, y se mapea en `ContextoGimnasio`.
- La validacion de tamano y formato va en `capaLogica`, sin usar `System.Drawing` ni controles.
- `capaVisual` solo elige el archivo, lo convierte y lo muestra.
- Antes de asignar una imagen nueva, liberar la anterior con `if (fotoSocio.Image != null) fotoSocio.Image.Dispose();` para no acumular memoria al recorrer la grilla.

## Paso 6 — Foto en el alta de usuario y de socio

Al registrar un usuario del sistema y al registrar un socio se debe poder cargar una imagen. Si no se carga ninguna, se muestra una imagen por defecto segun el sexo.

Hoy ni `UsuarioSistema` ni `Socio` tienen campo de foto ni de sexo, asi que esto implica cambio de esquema. Hacerlo completo en las tres capas, no solo en la pantalla.

### 6.1 Entidades y base de datos

Agregar a `capaDatos/Entidades/UsuarioSistema.cs` y a `capaDatos/Entidades/Socio.cs`:

```csharp
[Column(TypeName = "varbinary(max)")]
public byte[] Foto { get; set; }

[StringLength(1)]
[Column(TypeName = "char")]
public string Sexo { get; set; }
```

Ambas nulables. `Sexo` guarda `"M"` o `"F"`; se usa unicamente para elegir la imagen por defecto.

Reflejar las columnas en `capaDatos/Contexto/ContextoGimnasio.cs` si el mapeo no sale solo de los atributos.

Agregar las columnas al script base `capaDatos/Database/SysGymDB.sql` y las sentencias correspondientes a `capaDatos/Database/SysGymDB_MigracionExistente.sql`:

```sql
ALTER TABLE UsuarioSistema ADD Foto varbinary(max) NULL;
ALTER TABLE UsuarioSistema ADD Sexo char(1) NULL;
ALTER TABLE Socio ADD Foto varbinary(max) NULL;
ALTER TABLE Socio ADD Sexo char(1) NULL;
```

Las filas existentes quedan con `Sexo` nulo. Contemplar ese caso al mostrar la imagen.

### 6.2 Imagenes por defecto

Las provee el usuario del proyecto. Codex no las genera ni las descarga: si los archivos todavia no estan, dejar el codigo escrito y las referencias listas, y avisarlo.

Ubicacion: `capaVisual/Recursos/avatar-hombre.png`, `capaVisual/Recursos/avatar-mujer.png` y, opcionalmente, `capaVisual/Recursos/avatar-generico.png`.

Agregarlas a `Properties/Resources.resx` desde Visual Studio con los nombres `avatarHombre`, `avatarMujer` y `avatarGenerico`.

Regla de resolucion, en este orden:

1. Si `Foto` tiene bytes, se muestra esa imagen.
2. Si no, y `Sexo` es `"M"`, se muestra `avatarHombre`.
3. Si no, y `Sexo` es `"F"`, se muestra `avatarMujer`.
4. Si no hay sexo cargado, se muestra `avatarGenerico`; si ese recurso no existe, se deja el `PictureBox` vacio con `BackColor` gris claro. No inventar un sexo por defecto.

Metodo compartido en `capaVisual/Compartido/AyudaFormularioVisual.cs`, para no duplicarlo en los dos formularios:

```csharp
/* Resuelve la imagen a mostrar priorizando la foto cargada sobre la imagen por defecto del sexo. */
internal static void MostrarFoto(PictureBox destino, byte[] contenido, string sexo)
{
    if (destino.Image != null)
    {
        destino.Image.Dispose();
        destino.Image = null;
    }

    if (contenido != null && contenido.Length > 0)
    {
        using (var memoria = new System.IO.MemoryStream(contenido))
            destino.Image = System.Drawing.Image.FromStream(memoria);
        return;
    }

    if (sexo == "M")
        destino.Image = Properties.Resources.avatarHombre;
    else if (sexo == "F")
        destino.Image = Properties.Resources.avatarMujer;
    else
        destino.Image = Properties.Resources.avatarGenerico;
}
```

Las imagenes de recurso no se liberan con `Dispose`: son compartidas. Por eso el `Dispose` inicial solo corresponde cuando la imagen anterior vino de un `byte[]`. Resolverlo guardando en un campo del formulario si la imagen actual es propia o de recurso, o clonando el recurso con `new Bitmap(...)` antes de asignarlo.

### 6.3 Controles en los formularios

En `GestionUsuariosFormulario` y en `GestionSociosFormulario`, dentro de `panelDetalle`, agregar:

- `fotoUsuario` / `fotoSocio`: `PictureBox`, `SizeMode = Zoom`, `BorderStyle = FixedSingle`, tamano aproximado 120x120, `Anchor = Top, Left`.
- `btnSeleccionarFoto`: abre un `OpenFileDialog` con `Filter = "Imagenes|*.png;*.jpg;*.jpeg;*.bmp"`.
- `btnQuitarFoto`: pone `Foto` en `null` y vuelve a la imagen por defecto del sexo.
- `sexo`: `ComboBox` con `DropDownStyle = DropDownList` y los items `Masculino` y `Femenino`. Se mapea a `"M"` y `"F"` al leer y escribir la entidad; no guardar el texto visible.

`panelDetalle` ya tiene `AutoScroll = true`, asi que el alto extra no rompe nada.

El combo `sexo` va con los demas campos del formulario y sigue las reglas de `Anchor` del paso 3. Al cambiar su seleccion, si no hay foto cargada, refrescar el `PictureBox` para que se vea la imagen por defecto correspondiente.

### 6.4 Carga y guardado

- Al seleccionar una fila de la grilla, la foto se trae junto con el registro en `ObtenerPorId`, no en el listado. **No** cargar fotos para todas las filas de la grilla: son binarios y hacen lento el listado.
- Al guardar o actualizar, incluir `Foto` y `Sexo` en la entidad que se envia a `capaLogica`.
- En "Nuevo", limpiar la foto y dejar el combo de sexo sin seleccion.

### 6.5 Validaciones en capaLogica

En `capaLogica/UsuarioSistemaLogica.cs`, `capaLogica/SocioLogica.cs` o `capaLogica/ValidacionesGimnasio.cs`, segun donde encajen las validaciones existentes:

- Tamano maximo de la foto: 2 MB. Si se excede, lanzar `InvalidOperationException` con un mensaje claro; la capa visual ya lo muestra con `AyudaFormularioVisual.MostrarError`.
- `Sexo` solo admite `"M"`, `"F"` o nulo.
- Estas validaciones no usan `System.Drawing` ni controles de Windows Forms: `capaLogica` recibe el `byte[]` ya convertido.

### 6.6 Documentacion

Actualizar `docs/DATABASE.md` con las columnas nuevas y `docs/BUSINESS_RULES.md` con el limite de tamano y los valores admitidos de `Sexo`. Poner la fecha de ultima actualizacion en ambos.

## Paso 7 — Codigo fuera del disenador

- `capaVisual/Recepcionista/PanelRecepcionista.cs` y `capaVisual/Entrenador/PanelEntrenador.cs` tienen una linea `navegacion.EstablecerContenidoInicio(lblBienvenida, null);` en su `Load`. Si se conserva `lblBienvenida`, mantenerla; si se elimina la etiqueta del disenador, eliminar tambien esa linea.
- No tocar `ControladorNavegacion.cs`: sigue acoplando el formulario abierto al area de contenido con `Dock = Fill` solo en ejecucion.
- No cambiar nombres de controles, handlers ni suscripciones de eventos.
- No modificar `exxen2.0.csproj` ni los `.resx`.

## Restricciones

- No resucitar `DashboardAdministrador`, `DashboardInicioAdministrador`, `GestionPlanesForm`, `GestionUsuariosForm`, `GestionSociosForm`, `DashboardRecepcionista`, `GestionAsignacionesForm`, `GestionMembresiasForm` ni `GestionPagosForm`.
- No renombrar los formularios actuales.
- No romper `InitializeComponent` ni separar los archivos parciales.
- No duplicar controles ni dejar eventos apuntando a controles eliminados.
- Conservar la identidad visual: violeta para administrador, verde para recepcionista, celeste para entrenador, y las fuentes y textos actuales.

## Criterios de aceptacion

1. `msbuild exxen2.0.slnx /t:Rebuild /p:Configuration=Debug /nologo` termina con 0 errores.
2. `grep -rn "TableLayoutPanel\|FlowLayoutPanel" capaVisual` no devuelve resultados.
3. En el disenador de Visual Studio, arrastrar `btnVolver` en `GestionPlanesFormulario` mueve **solo** ese boton: ni `lblTitulo` ni `lblDescripcion` cambian de posicion.
4. Con la ventana maximizada, `panelContenido` ocupa todo el espacio a la derecha del menu y por debajo del encabezado. No queda franja gris.
5. En `GestionSociosFormulario` maximizado, la grilla ocupa el alto disponible y el panel de detalle queda pegado al borde derecho.
6. Ninguna etiqueta ni encabezado de columna aparece cortado.
7. No hay barras de desplazamiento fuera de `panelOpciones`, `panelDetalle` y `listaClima`.
8. Los 17 formularios abren sin errores en el disenador de Visual Studio.
9. Todos los listados siguen mostrandose en un `DataGridView` con encabezado de columnas y una fila por registro, y las columnas ocupan todo el ancho disponible.
10. Al dar de alta un usuario y un socio se puede cargar una imagen, y al no cargarla se ve la imagen por defecto segun el sexo elegido.
11. La foto se guarda y se recupera de la base, y el listado sigue cargando igual de rapido porque no trae los binarios.

## Actualizar documentacion

`docs/ARCHITECTURE.md` tiene la seccion "Distribucion por contenedores". Reemplazarla por la convencion nueva: armazon con `Dock` en los paneles estructurales, `Anchor` en el resto, coordenadas libres para todo lo editable, y la nota de que `Width` no se asigna en grillas con `AutoSizeColumnsMode = Fill`. Actualizar la fecha de ultima actualizacion.
