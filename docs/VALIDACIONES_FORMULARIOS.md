# Validaciones de formularios

Última actualización: 21 de septiembre de 2026.

Esta matriz documenta la auditoría global de entradas editables. El `ErrorProvider` mejora la experiencia de uso, pero las reglas definitivas permanecen en `capaLogica`. Los buscadores son opcionales, los controles `ReadOnly`, invisibles o deshabilitados no bloquean operaciones y las fotos nunca son obligatorias.

| Formulario | Campo | Obligatorio | Formato | Regla | ErrorProvider | Validación lógica |
| --- | --- | --- | --- | --- | --- | --- |
| InicioSesion | Nombre de usuario | Sí | Letras, números, `.`, `_`, `-` | `ValidarNombreUsuario`; máximo 50 | Sí | `UsuarioSistemaLogica.Autenticar` |
| InicioSesion | Contraseña | Sí | Texto | No puede quedar vacía | Sí | `UsuarioSistemaLogica.Autenticar` |
| GestionUsuariosFormulario | Nombre | Sí | Nombre de persona | `ValidarNombre`; máximo 100 | Sí | `UsuarioSistemaLogica` |
| GestionUsuariosFormulario | Apellido | Sí | Nombre de persona | `ValidarNombre`; máximo 100 | Sí | `UsuarioSistemaLogica` |
| GestionUsuariosFormulario | DNI | Sí | Solo dígitos | `ValidarDni`; máximo 20 y único | Sí | `UsuarioSistemaLogica` |
| GestionUsuariosFormulario | Fecha de nacimiento | Sí | Fecha | No futura; edad mínima 18 | Sí | `UsuarioSistemaLogica` |
| GestionUsuariosFormulario | NombreUsuario | Sí | Letras, números, `.`, `_`, `-` | `ValidarNombreUsuario`; máximo 50 y único | Sí | `UsuarioSistemaLogica` |
| GestionUsuariosFormulario | Contraseña | En alta | Texto | En modificación, vacío conserva la actual | Sí | `UsuarioSistemaLogica` |
| GestionUsuariosFormulario | Salario | Sí | Decimal de cultura actual | Mayor que cero | Sí | `UsuarioSistemaLogica` |
| GestionUsuariosFormulario | Rol | Sí | Selección | Debe existir una opción | Sí | `UsuarioSistemaLogica` |
| GestionUsuariosFormulario | Sexo | No | M, F o sin selección | Solo valores admitidos | No bloqueante | `UsuarioSistemaLogica` |
| GestionUsuariosFormulario | Foto | No | Imagen válida | Una foto como máximo; política central de imágenes | No bloqueante | `UsuarioSistemaLogica` / `ProcesadorImagenes` |
| GestionSociosFormulario | Nombre | Sí | Nombre de persona | `ValidarNombre`; máximo 100 | Sí | `SocioLogica` |
| GestionSociosFormulario | Apellido | Sí | Nombre de persona | `ValidarNombre`; máximo 100 | Sí | `SocioLogica` |
| GestionSociosFormulario | DNI | Sí | Solo dígitos | `ValidarDni`; máximo 20 y único | Sí | `SocioLogica` |
| GestionSociosFormulario | Fecha de nacimiento | Sí | Fecha | No futura; edad mínima 13 | Sí | `SocioLogica` |
| GestionSociosFormulario | Sexo | No | M, F o sin selección | Solo valores admitidos | No bloqueante | `SocioLogica` |
| GestionSociosFormulario | Peso | No | Decimal de cultura actual | Si se informa, mayor que cero | Sí | `SocioLogica` |
| GestionSociosFormulario | Altura | No | Decimal de cultura actual | Si se informa, mayor que cero y con parte decimal | Sí | `SocioLogica` |
| GestionSociosFormulario | Foto | No | Imagen válida | Una foto como máximo; avatar si está vacía | No bloqueante | `SocioLogica` / `ProcesadorImagenes` |
| GestionPlanesFormulario | Nombre | Sí | Texto | No puede quedar vacío | Sí | `PlanLogica` |
| GestionPlanesFormulario | Descripción | No | Texto | Sin regla adicional vigente | No bloqueante | `PlanLogica` |
| GestionPlanesFormulario | Precio | Sí | Decimal de cultura actual | Mayor que cero; cero no está permitido | Sí | `PlanLogica` |
| GestionEjerciciosFormulario | Nombre | Sí | Texto | No puede quedar vacío | Sí | `EjercicioLogica` |
| GestionEjerciciosFormulario | Descripción | No | Texto | Sin regla adicional vigente | No bloqueante | `EjercicioLogica` |
| GestionEjerciciosFormulario | Imágenes | No | Imagen válida | De cero a cuatro; no duplica procesamiento | No bloqueante | `EjercicioImagenLogica` / `ProcesadorImagenes` |
| GestionMembresiasFormulario | Socio | En alta | Selección | Socio disponible sin otra membresía histórica | Sí | `MembresiaLogica` |
| GestionMembresiasFormulario | Plan | Sí | Selección | Plan existente y activo | Sí | `MembresiaLogica` |
| GestionMembresiasFormulario | Inicio | Sí | Fecha | Fecha válida | Sí, por rango | `MembresiaLogica` |
| GestionMembresiasFormulario | Vencimiento | Sí | Fecha | No anterior al inicio | Sí | `MembresiaLogica` |
| GestionPagosFormulario | Membresía/cuota | Sí al registrar | Selección contextual | Debe existir una cuota pendiente registrable | Sí | `PagoLogica` / `CuotaMembresiaLogica` |
| GestionPagosFormulario | Importe | Sí al registrar | Decimal de cultura actual | Mayor que cero; un aprobado no supera la cuota | Sí | `PagoLogica` |
| GestionPagosFormulario | Método | Sí al registrar | Selección | Método activo con un único detalle | Sí | `PagoLogica` |
| GestionPagosFormulario | Estado | Sí al registrar | Selección | Estado de transacción válido | Sí | `PagoLogica` |
| GestionAsignacionesFormulario | Membresía | Sí al asignar/cambiar | Selección de grilla | Debe estar seleccionada y habilitada | Sí | `MembresiaEntrenadorLogica` |
| GestionAsignacionesFormulario | Entrenador | Sí al asignar/cambiar | Selección | Usuario Entrenador activo | Sí | `MembresiaEntrenadorLogica` |
| RutinasEntrenadorFormulario | Nombre de rutina | Sí | Texto | No puede quedar vacío | Sí | `RutinaLogica` |
| RutinasEntrenadorFormulario | Descripción | No | Texto | Sin regla adicional vigente | No bloqueante | `RutinaLogica` |
| RutinasEntrenadorFormulario | Ejercicio | Sí al editar detalle | Selección | Ejercicio activo | Sí | `RutinaEjercicioLogica` |
| RutinasEntrenadorFormulario | Día | Sí al editar detalle | Selección | Lunes a viernes | Sí | `RutinaEjercicioLogica` |
| RutinasEntrenadorFormulario | Series | Sí al editar detalle | Entero | Mayor que cero | Sí | `RutinaEjercicioLogica` |
| RutinasEntrenadorFormulario | Repeticiones | Sí al editar detalle | Entero | Mayor que cero | Sí | `RutinaEjercicioLogica` |
| RutinasEntrenadorFormulario | Peso | No | Decimal de cultura actual | Si se informa, no negativo | Sí | `RutinaEjercicioLogica` |
| RutinasEntrenadorFormulario | Descanso | No | Entero | Si se informa, no negativo; vacío equivale a cero | Sí | `RutinaEjercicioLogica` |
| RutinasEntrenadorFormulario | Orden | Sí al editar detalle | Entero | Mayor que cero | Sí | `RutinaEjercicioLogica` |
| MisSociosFormulario | Socio | Sí al asignar rutina | Selección de grilla | Debe existir una membresía objetivo | Sí | `RutinaLogica` |
| MisSociosFormulario | Rutina disponible | Sí al asignar/cambiar | Selección | Rutina activa | Sí | `RutinaLogica` |

## Pantallas auditadas sin entradas obligatorias

`RutinaSemanalFormulario`, `ConsultaRutinasAdministradorFormulario`, `ConsultaEntrenadoresFormulario`, `ReportesFormulario` e `InicioPanelAdministrador` son de consulta o disparan acciones sin una ficha editable obligatoria. Sus buscadores y filtros pueden quedar vacíos. Los paneles de rol solo coordinan navegación y sesión.

## Comportamiento común

- Cada uno de los diez formularios editables auditados declara un único `ErrorProvider` dentro de `components`, con `BlinkStyle = NeverBlink`, `ContainerControl`, `BeginInit` y `EndInit`.
- `Validating` muestra el error al abandonar un campo tocado; cambiar el valor retira el icono anterior.
- Crear, guardar, actualizar, registrar, asignar y cambiar vuelven a validar la ficha completa antes de llamar a `capaLogica`.
- `EnfocarPrimerError` dirige el foco al primer control inválido.
- `KeyPress` solo ayuda durante la escritura. La validación final detecta también contenido pegado inválido.
- Nuevo, cambio de registro, recarga o ficha vacía limpian los errores anteriores.

## Verificaciones del 21/09/2026

- Casos compartidos: `Juan`, `José Luis`, `María-José`, `12345678`, `admin`, `entrenador10`, `usuario123`, `juan_2026` y `78,5` aceptados donde corresponde.
- Rechazos comprobados: `Juan123`, `123A5678`, `ABC`, fecha futura, decimal `ABC`, entero positivo igual a cero y combo requerido sin selección.
- Controles deshabilitados y `TextBox ReadOnly` vacíos no bloquean.
- Los diez formularios editables se instanciaron y expusieron exactamente un `ErrorProvider` no nulo sin consultar SQL Server.
- Auditoría estática: sin handlers vacíos `_Click_1`/`_Click_2`, suscripciones duplicadas ni bucles/helpers de construcción en los `Designer` afectados.
- Debug y Release Rebuild: 0 errores, 0 warnings.
- Visual Studio Community 2026 abrió los diez formularios mediante la vista Designer real (`DesignerOpen=True`, `Saved=True`) y los cerró sin guardar cambios.
- `git diff --check` pasa; los avisos LF/CRLF son informativos.
