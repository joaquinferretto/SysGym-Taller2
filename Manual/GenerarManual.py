from __future__ import print_function

import html
import os
import re
import shutil
import tempfile
import zipfile
import xml.etree.ElementTree as ET

from PIL import Image as PILImage, ImageOps
from reportlab.lib import colors
from reportlab.lib.enums import TA_CENTER, TA_LEFT
from reportlab.lib.pagesizes import A4
from reportlab.lib.styles import ParagraphStyle, getSampleStyleSheet
from reportlab.lib.units import mm
from reportlab.pdfbase import pdfmetrics
from reportlab.pdfbase.ttfonts import TTFont
from reportlab.platypus import (
    BaseDocTemplate, Frame, Image, KeepTogether, PageBreak, PageTemplate,
    Paragraph, Spacer, Table, TableStyle
)
from reportlab.platypus.tableofcontents import TableOfContents
from reportlab.pdfgen.canvas import Canvas


ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
MANUAL = os.path.join(ROOT, "Manual")
IMAGES = os.path.join(MANUAL, "Imagenes")
COVER_LOGO = os.path.join(ROOT, "Resources", "SysGymLogo.png")
PDF_OUT = os.path.join(MANUAL, "Manual de usuario SysGym - 2026 monocromo.pdf")
DOCX_OUT = os.path.join(MANUAL, "Manual de usuario SysGym - 2026 monocromo.docx")
FECHA_EDICION = "04/11/2026"
IMAGE_TEMP_DIR = None
FIGURE_CROPS = {
    "paso-alta-socio.png": (75, 0, 720, 650),
    "paso-asignar-entrenador.png": (86, 0, 850, 640),
    "paso-menu-manual.png": (0, 120, 310, 265),
}

NAVY = "000000"
BLUE = "000000"
TEAL = "000000"
MID = "000000"
INK = "000000"
MUTED = "000000"


BLOCKS = []


def H1(text):
    BLOCKS.append(("h1", text))


def H2(text):
    BLOCKS.append(("h2", text))


def H3(text):
    BLOCKS.append(("h3", text))


def P(text):
    BLOCKS.append(("p", text))


def UL(*items):
    BLOCKS.append(("ul", list(items)))


def STEPS(*items):
    BLOCKS.append(("steps", list(items)))


def FIG(filename, caption, width=None):
    BLOCKS.append(("fig", filename, caption, width))


def TBL(headers, rows, widths=None, database=False):
    BLOCKS.append(("tbl", headers, rows, widths, database))


def CODE(text):
    BLOCKS.append(("code", text.strip("\n")))


def NOTE(title, text):
    BLOCKS.append(("note", title, text))


def prepare_figure(filename):
    source_path = os.path.join(IMAGES, filename)
    if not os.path.exists(source_path):
        raise FileNotFoundError("Falta captura requerida: " + source_path)
    if IMAGE_TEMP_DIR is None:
        return source_path
    with PILImage.open(source_path) as source:
        image = source.convert("RGB")
        crop = FIGURE_CROPS.get(filename)
        if crop:
            image = image.crop(crop)
        image = ImageOps.expand(image, border=12, fill="white")
        output_path = os.path.join(IMAGE_TEMP_DIR, filename)
        image.save(output_path, format="PNG", optimize=True)
    return output_path


def PB():
    BLOCKS.append(("page",))


H1("1. Alcance y uso del manual")
P("SysGym es una aplicación de escritorio para administrar un gimnasio. Este manual explica, paso a paso, las tareas disponibles para Recepción, Entrenadores y Administración.")
P("Las imágenes muestran pantallas y ejemplos de informes. Los nombres, importes y cantidades visibles son ilustrativos; los resultados de cada usuario dependen de los datos registrados y del período consultado.")
TBL(
    ["Rol", "Acciones disponibles"],
    [
        ["Recepcionista", "Socios, membresías, cuotas, pagos, comprobantes e historiales PDF, asignación y consulta de entrenadores y reportes operativos."],
        ["Entrenador", "Consultar sus alumnos, asignar y editar sus rutinas, mantener ejercicios y exportar rutinas."],
        ["Administrador", "Tareas operativas de Recepción, además de usuarios, planes, ejercicios, rutinas, configuración, análisis, auditoría y copias de seguridad."],
    ],
)
NOTE("Antes de empezar", "Los cambios de precio de un plan aplican a cuotas que se generen más adelante. Una baja conserva el historial. Las capturas pueden mostrar datos de demostración.")

H1("2. Instalación, primer inicio y acceso")
H2("2.1 Requisitos e instalación")
P("Se necesita Windows 10 u 11 y .NET Framework 4.8. Ejecutá SysGym-Setup.exe, aceptá el aviso de Windows, seguí el asistente y pulsá Instalar. Al terminar, abrí SysGym desde Inicio o el acceso directo. Si se informa que falta .NET Framework 4.8, instalalo desde el enlace oficial que muestra Windows y repetí la instalación.")
H2("2.2 Primer inicio y registro del Administrador")
STEPS(
    "Si aparece la confirmación de preparación inicial, respondé Sí y esperá a que SysGym termine el proceso sin cerrar la aplicación.",
    "En Registrar administrador completá nombre, apellido, DNI, sexo, nombre de usuario, contraseña y confirmación.",
    "Pulsá Registrar administrador. Si los datos son válidos, SysGym crea la cuenta inicial y abre el inicio de sesión.",
    "Ingresá con el usuario y contraseña recién creados. Después, creá las cuentas de Recepción y Entrenador desde Usuarios y roles.",
)
FIG("paso-primer-inicio.png", "Primer inicio: confirmación para continuar la preparación inicial.")
FIG("paso-registro-admin.png", "Registro obligatorio del primer Administrador.")
H2("2.3 Iniciar sesión, cambiar de cuenta y salir")
STEPS(
    "Escribí el nombre de usuario y la contraseña.",
    "Pulsá Ingresar. Si no entra, comprobá mayúsculas, teclado y credenciales; un Administrador puede restablecer la contraseña.",
    "Usá el menú lateral para abrir las funciones autorizadas para tu rol. El encabezado identifica el usuario y el rol activo.",
    "Cambiar de cuenta vuelve a la pantalla de acceso. Volver regresa al inicio del rol. Salir cierra SysGym.",
    "Manual de usuario abre el PDF con el lector predeterminado de Windows.",
)
FIG("paso-inicio-sesion.png", "Pantalla de acceso de SysGym.")
FIG("paso-menu-manual.png", "El menú lateral permite abrir este manual y navegar por el rol.")
H2("2.4 Desinstalación")
P("En Windows abrí Aplicaciones instaladas, buscá SysGym y elegí Desinstalar, o usá la opción del menú Inicio. La desinstalación quita el programa y los accesos directos, pero conserva los datos y las copias de seguridad. Antes de reinstalar o trasladar la PC, guardá una copia externa y consultá con la persona responsable del sistema.")

H1("3. Recepción: socios, membresías y cobros")
H2("3.1 Alta de socio")
STEPS(
    "Abrí Socios. Pulsá Nuevo para limpiar el detalle y comenzar otro registro.",
    "Completá nombre, apellido y DNI. El DNI identifica al socio y no puede repetirse.",
    "Ingresá, si se conocen, fecha de nacimiento, sexo, peso en kilogramos y altura en metros.",
    "Para una foto, pulsá Seleccionar foto y elegí un archivo accesible desde la PC.",
    "Pulsá Calcular IMC para mostrar el cálculo con el peso y la altura ingresados. Es una referencia general, no una evaluación médica.",
    "Revisá los campos y pulsá Guardar. Buscá el DNI o nombre y seleccioná la fila para confirmar que quedó cargado.",
)
FIG("paso-alta-socio.png", "Alta de socio: datos personales, foto, cálculo de IMC y Guardar.")
H2("3.2 Buscar, modificar, dar de baja y reactivar un socio")
STEPS(
    "En Socios, escribí parte del nombre, apellido o DNI en el buscador y seleccioná la persona en la lista.",
    "Modificá los campos habilitados y guardá. Si la persona tiene membresías o pagos, el sistema conserva esas relaciones históricas.",
    "Para terminar la relación con el gimnasio, elegí Dar de baja y confirmá. La baja lógica cambia el estado y conserva los registros.",
    "Para recuperar el acceso, buscá también entre los inactivos, seleccioná la persona y pulsá Reactivar.",
    "Si hay una membresía inactiva, revisala en Membresías y reactivala allí por separado cuando corresponda. La reactivación puede estar impedida por el límite de cuotas vencidas.",
)
NOTE("Historial", "Dar de baja no borra la fila, las cuotas ni los pagos. Esto permite conservar auditoría, reportes e historial de la persona.")
H2("3.3 Crear una membresía")
STEPS(
    "Abrí Membresías y pulsá Nueva.",
    "Seleccioná el socio, el plan y la fecha de inicio. El precio corresponde al plan vigente.",
    "Elegí objetivo y nivel, o Libre si todavía no se asignará una rutina.",
    "Si se informa peso y altura, la pantalla puede mostrar una sugerencia por IMC. Verificá la decisión con el entrenador; el IMC no determina por sí solo la rutina adecuada.",
    "Pulsá Guardar. La membresía queda asociada con el socio, el plan y el usuario que la registró; se crea la cuota inicial según las reglas vigentes.",
    "Buscá la membresía y revisá estado, plan, fecha y cuota antes de seguir.",
)
FIG("paso-membresia.png", "Nueva membresía: socio, plan, fecha, objetivo y opción de rutina.")
P("La sugerencia mostrada por la aplicación es orientativa: un IMC menor que 18,5 sugiere Hipertrofia; de 18,5 a menos de 25, Hipertrofia o Fuerza; de 25 a menos de 30, Acondicionamiento; desde 30, Acondicionamiento con nivel Principiante. El entrenador debe decidir según la evaluación de la persona.")
H2("3.4 Generar la cuota de otro período")
STEPS(
    "En Membresías, seleccioná una membresía activa.",
    "Pulsá Generar cuota cuando corresponda crear el período siguiente.",
    "SysGym impide duplicar el mismo período y aplica el máximo de anticipación configurado.",
    "En Cuotas y pagos, localizá la cuota recién creada y verificá fechas, importe y estado pendiente.",
)
P("Una cuota conserva el importe que tenía el plan al generarse. Modificar el precio del plan no reescribe cargos anteriores.")
H2("3.5 Registrar un pago aprobado")
STEPS(
    "Abrí Cuotas y pagos, buscá el socio por nombre o DNI y seleccioná la cuota pendiente.",
    "Pulsá Nuevo si el formulario no está limpio. Confirmá la membresía, el período y el importe de la cuota.",
    "Elegí el método disponible, por ejemplo Efectivo. Para una operación cobrada, dejá Aprobado como estado.",
    "Pulsá Registrar pago y confirmá el resultado. El historial muestra el pago aprobado y la cuota pasa a Pagada.",
)
FIG("paso-cobrar.png", "Formulario de pago: cuota e importe seleccionados, método y estado antes de registrar.")
H2("3.6 Anular, reembolsar y volver a cobrar")
STEPS(
    "En Cuotas y pagos, seleccioná el pago del historial que corresponda.",
    "Pulsá Anular para dejar sin efecto el registro o Reembolsar cuando el dinero ya se devolvió. Leé y confirmá el aviso.",
    "El estado anterior queda en el historial; SysGym no elimina el pago. Si era el pago vigente de una cuota, la cuota vuelve a quedar pendiente.",
    "Para cobrarla otra vez, seleccioná la cuota pendiente, elegí el método y registrá un nuevo pago aprobado.",
)
FIG("paso-estados-pago.png", "La lista distingue cuotas pendientes, pagadas, anuladas y pagos reembolsados.")
H2("3.7 Exportar comprobantes e historial de pagos en PDF")
STEPS(
    "Para exportar un comprobante individual, abrí Cuotas y pagos, buscá al socio por nombre o DNI y seleccioná la cuota cuyo pago está aprobado. La cuota debe figurar como Pagada.",
    "Pulsá Exportar comprobante, elegí la carpeta y el nombre del archivo y guardá el PDF. El comprobante incluye los datos del socio, la cuota, el importe, el método, el estado y quién registró el pago.",
    "Para exportar todos los pagos de un socio, abrí su Ficha completa y pulsá Exportar historial PDF. También podés hacer clic derecho sobre una fila del socio en Cuotas y pagos y elegir Exportar historial.",
    "Elegí dónde guardar el archivo. El historial conserva los pagos aprobados, anulados y reembolsados; el total abonado cuenta solo los pagos aprobados.",
)
FIG("paso-exportar-historial.png", "La Ficha completa permite exportar el historial de pagos del socio en PDF.")
NOTE("Disponibilidad", "El comprobante individual se habilita para un pago aprobado. Desde la Ficha completa se puede exportar el historial aunque el socio todavía no tenga pagos registrados.")
H2("3.8 Cambiar el plan de una membresía")
STEPS(
    "En Membresías, seleccioná la membresía activa que debe cambiar.",
    "Elegí el plan nuevo y pulsá Cambiar plan.",
    "Volvé a revisar la membresía y el precio. Las cuotas ya creadas conservan su importe; las cuotas futuras toman el nuevo precio.",
)
H2("3.9 Baja, reactivación y límite de deuda")
P("Una cuota pendiente se cuenta como vencida cuando terminó su período. La configuración define cuántas cuotas vencidas impagas alcanzan el límite. Con un límite de dos, la segunda cuota vencida puede dejar inactiva la membresía. Cobrar la deuda reduce el saldo impago, pero no reactiva automáticamente la membresía.")
STEPS(
    "Consultá la deuda en Cuotas y pagos o en el reporte operativo de deuda.",
    "Registrá el pago de cada cuota pendiente que se haya cobrado.",
    "Volvé a Membresías y verificá el estado actual.",
    "Si se cumplen las reglas de reactivación, seleccioná la membresía inactiva y pulsá Reactivar. Si la persona también fue dada de baja, reactivá primero al socio.",
)
H2("3.10 Asignar, cambiar o terminar la asignación de un entrenador")
STEPS(
    "Abrí Asignar entrenador y elegí la membresía.",
    "Revisá socio, plan, estado y entrenador actual.",
    "En Nuevo entrenador, seleccioná la cuenta que tenga rol Entrenador activa y guardá.",
    "Para cambiar la asignación, elegí otro entrenador y guardá. Para terminarla, usá Dar de baja en la asignación.",
)
FIG("paso-asignar-entrenador.png", "La asignación vincula una membresía con el entrenador responsable.")
H2("3.11 Consultar entrenadores y socios asignados")
STEPS(
    "Abrí Entrenadores y elegí Consultar.",
    "Buscá por nombre o DNI. La lista muestra cada entrenador, la cantidad de socios asignados y su estado.",
    "Seleccioná un entrenador para ver sus socios, DNI, plan, vencimiento de cuota, rutina y estado de la membresía.",
    "Pulsá Actualizar listado si necesitás volver a cargar la información. Para cambiar una asignación, usá Asignar entrenador.",
)
H2("3.12 Consultar y exportar un reporte operativo")
P("Reportes operativos ofrece listados de deuda pendiente, personas en límite de deuda, socios sin entrenador, socios sin rutina, próximos vencimientos y membresías inactivas. La pantalla actualiza el listado cuando cambiás el tipo; también permite filtrar por nombre o DNI.")
STEPS(
    "Elegí el tipo de informe.",
    "Para vencimientos, marcá Desde y Hasta si necesitás acotar fechas. Para otros informes, usá el buscador disponible.",
    "Pulsá Consultar. Revisá el resumen, la cantidad y las filas devueltas.",
    "Para abrir la ficha de una fila compatible, seleccioná el socio y usá Ver ficha.",
    "Pulsá Exportar PDF, elegí carpeta y nombre y guardá el archivo.",
)
FIG("paso-reportes.png", "Consulta de reportes operativos y resultados.")
FIG("reportes-recepcion-1920.png", "Ejemplo capturado en la aplicación de un listado operativo con datos de demostración.")

H1("4. Entrenador: alumnos y rutinas")
H2("4.1 Consultar Mis alumnos y la ficha")
STEPS(
    "Abrí Mis alumnos para listar las personas asignadas al entrenador autenticado.",
    "Buscá por nombre o DNI y aplicá el filtro de rutina si hace falta.",
    "Seleccioná una fila. Revisá plan, cuota, estado, IMC, rutina y ejercicios de la semana.",
    "Usá Ver ficha para abrir la información de trabajo asociada al socio.",
)
FIG("paso-mis-alumnos.png", "El entrenador ve únicamente los alumnos que tiene asignados.")
H2("4.2 Elegir otra rutina o crear una personalizada")
STEPS(
    "Con el alumno seleccionado, revisá la rutina y el objetivo vigente.",
    "Para usar un modelo, elegí objetivo, nivel y rutina; pulsá Cambiar rutina.",
    "Para empezar una rutina propia, pulsá Crear personalizada y completá nombre, descripción y fechas si aplican.",
    "La rutina queda asociada con el entrenador que la creó. La selección de un modelo no reemplaza una evaluación profesional.",
)
P("También podés abrir Rutinas desde el menú del Entrenador para crear y mantener modelos. El catálogo es compartido: podés consultar rutinas de otros entrenadores, pero solo modificar las que creaste. Administración puede mantener el catálogo completo; los pasos están en 5.4.")
H2("4.3 Agregar, editar y ordenar ejercicios")
STEPS(
    "Pulsá Ver / editar en la rutina del alumno.",
    "Elegí el día y el ejercicio disponible. Completá series, repeticiones, peso y segundos de descanso.",
    "Pulsá + Agregar ejercicio para incluir otro movimiento. Revisá el día y el orden en que se presentará.",
    "Seleccioná un ejercicio existente para modificar sus parámetros o quitarlo de la rutina.",
    "Guardá los cambios y volvé a Mis alumnos para revisar la rutina semanal actualizada.",
)
FIG("paso-editar-rutina.png", "Edición de ejercicios y parámetros de una rutina.")
FIG("paso-ejercicios-imagenes.png", "Referencia visual del catálogo de ejercicios.")
P("Si el ejercicio que necesitás no aparece, abrí Ejercicios desde el menú del Entrenador para agregarlo al catálogo. El procedimiento para mantener ejercicios e imágenes está en 5.3.")
H2("4.4 Exportar la rutina del alumno")
STEPS(
    "En Mis alumnos, seleccioná la persona y verificá la rutina que se exportará.",
    "Pulsá Exportar PDF, elegí la ubicación y guardá.",
    "Abrí el archivo generado y comprobá que incluya los ejercicios, los días, los parámetros y las imágenes disponibles.",
)

H1("5. Administración: configuración y seguimiento")
H2("5.1 Crear y administrar usuarios y roles")
STEPS(
    "Abrí Usuarios y roles y pulsá Nuevo.",
    "Completá nombre, apellido, DNI, usuario, contraseña, salario, rol y sexo. Teléfono, fecha de nacimiento y foto son opcionales cuando la pantalla los permite.",
    "Pulsá Guardar y comprobá la fila. Revisá el rol antes de entregar el acceso.",
    "Para editar una cuenta, seleccionála, modificá los campos y guardá.",
    "Para baja o reactivación, seleccioná la cuenta y usá la acción disponible. La cuenta inactiva conserva los registros históricos.",
    "Para restablecer una contraseña olvidada, seleccioná la cuenta y usá Restablecer contraseña. El sistema no muestra la contraseña anterior.",
)
FIG("paso-usuarios.png", "Administración de cuentas y roles.")
H2("5.2 Crear y mantener planes")
STEPS(
    "Abrí Planes y pulsá Nuevo.",
    "Completá nombre, descripción y precio positivo.",
    "Pulsá Guardar y comprobá la fila.",
    "Para modificar, dar de baja o reactivar, seleccioná el plan y elegí la acción correspondiente.",
)
FIG("paso-planes.png", "Catálogo de planes y precio vigente.")
H2("5.3 Mantener ejercicios e imágenes (Entrenador y Administración)")
STEPS(
    "En Ejercicios, pulsá Nuevo y completá nombre y descripción; guardá.",
    "Para editar o dar de baja, seleccioná el ejercicio y usá la acción disponible. La baja conserva sus usos históricos.",
    "Con el ejercicio seleccionado, usá Agregar imagen y elegí un archivo. Si hay varias imágenes, ordenalas y confirmá que aparezcan en la ficha.",
    "Para retirar una imagen, seleccioná su registro y usá la acción disponible. El archivo se almacena fuera de la base; la tabla conserva la ruta relativa.",
)
FIG("paso-ejercicios-imagenes.png", "Catálogo de ejercicios y gestión de imágenes.")
H2("5.4 Administrar el catálogo de rutinas (Entrenador y Administración)")
STEPS(
    "Abrí Rutinas desde el menú de Administración.",
    "Pulsá + Nueva rutina, completá el nombre y la descripción y guardá.",
    "Seleccioná una rutina para revisar sus datos y ejercicios. Para agregar un ejercicio, elegí el movimiento, el día y el orden; completá series, repeticiones, peso y descanso y guardá.",
    "Para corregir un ejercicio, seleccioná la fila y pulsá Editar. Para retirarlo, seleccioná la fila y pulsá Quitar.",
    "Usá Dar de baja para dejar de ofrecer una rutina. Para volver a ofrecerla, seleccioná una rutina inactiva y pulsá Reactivar.",
)
FIG("paso-editar-rutina.png", "El editor permite organizar y mantener los ejercicios de cada rutina.")
P("El Entrenador puede consultar rutinas de otros usuarios, pero solo editar las que creó. Administración puede editar y dar de baja cualquier rutina.")
H2("5.5 Asignar y revisar rutinas de socios")
STEPS(
    "Abrí Socios y rutinas. Buscá por nombre o DNI y usá el filtro Con rutina o Sin rutina.",
    "Seleccioná un socio para revisar su plan, entrenador, vencimiento, objetivo, nivel y rutina semanal.",
    "Para asignar o cambiar una rutina modelo, elegí objetivo y nivel, seleccioná una rutina activa y pulsá Asignar rutina o Cambiar rutina.",
    "Para preparar una rutina individual, pulsá Crear personalizada, completá nombre y descripción y guardá. Después agregá sus ejercicios desde el editor.",
    "Pulsá Ver / editar para modificar la rutina del socio. Si tiene una rutina asignada, también podés pulsar Exportar PDF para guardar una copia.",
)
P("La lista de Administración incluye los socios con membresía activa. El Entrenador, desde Mis alumnos, ve únicamente los socios que tiene asignados.")
H2("5.6 Configurar vencimientos, anticipación y avisos")
STEPS(
    "Abrí Configuración y el grupo Cuotas y pagos.",
    "Ajustá Cuotas vencidas, Meses de anticipación y Días de aviso dentro de los rangos indicados en pantalla.",
    "Pulsá Guardar para aplicar los nuevos valores.",
    "Pulsá Descartar para recuperar los valores guardados si todavía no confirmaste el cambio.",
    "Revisá el resultado en las próximas consultas de deuda, generación de cuotas y avisos.",
)
P("La configuración inicial de bases nuevas es 2 cuotas vencidas, 2 meses de anticipación y 7 días de aviso. Las bases existentes conservan sus propios valores.")
FIG("paso-configuracion.png", "Parámetros generales de cuotas y pagos.")
H2("5.7 Consultar auditoría")
STEPS(
    "Abrí Auditoría.",
    "Ingresá el rango de fechas y, si corresponde, filtrá por usuario u operación.",
    "Pulsá Aplicar filtros y revisá fecha, usuario, rol, operación, entidad, identificador y detalle.",
    "La auditoría es de consulta; no modifica los registros filtrados.",
)
FIG("paso-auditoria.png", "Historial de operaciones para seguimiento administrativo.")
H2("5.8 Consultar reportes operativos")
P("Administración comparte la consulta y exportación de los reportes operativos descritos en 3.12. La pantalla añade indicadores de socios, usuarios, membresías, rutinas y ejercicios; algunos indicadores se muestran solo al Administrador.")
H2("5.9 Consultar Análisis: período, indicadores y pestañas")
STEPS(
    "Abrí Análisis. Al ingresar se propone un rango de los últimos doce meses.",
    "Elegí Desde y Hasta y pulsá Aplicar. Ambas fechas se incluyen.",
    "Leé los cuatro indicadores: Ingresos del período, Pagos aprobados, Socios activos y Socios con deuda.",
    "Revisá la pestaña correspondiente. Si cambiás el rango, pulsá Aplicar antes de interpretar o exportar.",
)
TBL(
    ["Pestaña", "Qué muestra y cómo leerla"],
    [
        ["Resumen", "Ingresos por mes y comparación del último mes con el anterior."],
        ["Finanzas", "Ingresos y cantidad de pagos por mes y distribución por método de pago."],
        ["Socios", "Altas por mes, socios activos/inactivos y situación actual de deuda."],
        ["Planes", "Membresías actuales por plan; el volumen no equivale a ingresos."],
        ["Entrenadores", "Socios asignados a cada entrenador y personas sin asignación."],
        ["Rutinas", "Rutinas utilizadas y socios que todavía no tienen rutina."],
        ["Ejercicios", "Ejercicios más usados y ejercicios que no aparecen en rutinas."],
        ["Comparar", "Totales de ingresos, pagos y altas para dos rangos; variación de B respecto de A."],
        ["Socio", "Consulta individual por nombre o DNI. No se incluye en el PDF general."],
    ],
)
P("Ingresos, pagos aprobados y altas se calculan para el rango elegido. Socios, deuda, planes, entrenadores, rutinas y ejercicios reflejan la situación actual consultada. Los pagos anulados, reembolsados o rechazados no cuentan como ingresos. Si el período anterior tuvo cero ingresos, la variación aparece como raya.")
H2("5.10 Comparar períodos y exportar el informe")
STEPS(
    "En Comparar, ingresá las fechas Desde y Hasta para el período A y para el B.",
    "Pulsá Comparar períodos y leé la tabla y los gráficos.",
    "La variación es B con respecto a A; no representa una deuda histórica.",
    "Para exportar, volvé al rango principal, pulsá Aplicar y luego Exportar reporte PDF.",
    "La exportación incluye indicadores, gráficos y tablas de la consulta aplicada; incorpora la comparación si ya se calculó. La consulta individual de Socio queda fuera.",
)
FIG("paso-analisis-rango.png", "Selección del período principal del análisis.")
FIG("paso-analisis-comparar.png", "Comparación en la interfaz con totales de los dos períodos.")
FIG("paso-analisis-socio.png", "Consulta individual de socio dentro de Análisis.")
H2("5.11 Crear y restaurar copias de seguridad")
STEPS(
    "Abrí Configuración y Copias de seguridad.",
    "Para crear una copia, pulsá Crear copia de seguridad, elegí destino y guardá el archivo .bak. SysGym propone un nombre con fecha y hora.",
    "Tras finalizar, comprobá la fecha de Última copia y guardá otra copia fuera de la PC.",
    "Para restaurar, pulsá Restaurar copia y seleccioná un .bak compatible de SysGym.",
    "Leé la confirmación: restaurar reemplaza los datos actuales por el contenido del respaldo. Confirmá solo si corresponde y esperá a que SysGym complete la operación y se reinicie.",
)
FIG("paso-copias.png", "Crear y restaurar copias de seguridad.")
NOTE("Restauración", "Antes de reemplazar la base actual, creá una copia de su estado presente. Guardá los archivos de respaldo en un lugar separado del equipo.")
H2("5.12 Cambiar de cuenta y cerrar")
P("Cambiar de cuenta cierra la sesión actual y vuelve al acceso. Volver abre la pantalla principal del rol. Salir cierra la aplicación. Si otra persona usará la PC, cerrá o cambiá la cuenta antes de dejarla.")

H1("6. Ejemplos de lectura de Análisis")
P("Estos ejemplos muestran cómo interpretar los informes y gráficos de Análisis. Las cifras son ilustrativas: tus resultados varían según el período elegido y los datos cargados en SysGym.")
H2("6.1 Ejemplo de reporte PDF de Análisis")
TBL(
    ["Indicador del reporte", "Resultado de la captura"],
    [
        ["Período", "01/10/2025 a 30/09/2026"],
        ["Generado", "21/10/2026 20:14"],
        ["Ingresos aprobados", "$13.031.000,00"],
        ["Cantidad de pagos", "768"],
        ["Socios activos / inactivos", "80 / 30"],
        ["Socios al día / con deuda", "73 / 37"],
        ["Deuda bajo límite / en límite", "23 / 14"],
        ["Altas del período", "70"],
    ],
)
FIG("analisis-seed-pdf-p1.png", "Primera página de un reporte PDF de Análisis; los valores son de ejemplo.")
FIG("analisis-seed-pdf-p3.png", "Detalle de la situación de socios en el reporte PDF.")
FIG("analisis-seed-p17.png", "Ejemplo del apartado de ejercicios más usados en el reporte.")
H2("6.2 Ejemplo de comparación A/B")
TBL(
    ["Indicador", "Período A", "Período B", "Variación de B vs. A"],
    [
        ["Rango", "01/10/2025–31/03/2026", "01/04/2026–30/09/2026", "—"],
        ["Ingresos", "$6.780.000,00", "$6.251.000,00", "-7,80 %"],
        ["Pagos", "415", "353", "-14,94 %"],
        ["Altas", "45", "25", "-44,44 %"],
    ],
)
P("Ejemplo de lectura: el total de ingresos de B fue $529.000 menor que A. La división de esa diferencia por el total de A da una disminución de 7,80 %. Este resultado no permite inferir por sí solo la cantidad de socios actuales ni la deuda histórica.")
FIG("analisis-seed-comparar.png", "Comparación de períodos A y B; los gráficos y la tabla muestran los mismos totales.")
H2("6.3 Vistas de las pestañas de Análisis")
P("Las imágenes ilustran las pestañas disponibles. Las cifras dependen del rango seleccionado y de la información registrada en el momento de la consulta.")
FIG("analisis-seed-resumen.png", "Resumen mensual de ingresos y variaciones.")
FIG("analisis-seed-finanzas.png", "Ingresos y distribución por método de pago.")
FIG("analisis-seed-socios.png", "Altas, estados y situación de deuda de socios.")
FIG("analisis-seed-planes.png", "Distribución actual de membresías por plan.")
FIG("analisis-seed-entrenadores.png", "Cantidad de socios asignados a cada entrenador.")
FIG("analisis-seed-rutinas.png", "Rutinas utilizadas por los socios.")
FIG("analisis-seed-ejercicios.png", "Ejercicios más utilizados y ejercicios sin uso.")
FIG("analisis-seed-socio.png", "Consulta individual de la situación de un socio.")

H1("7. Preguntas frecuentes")
TBL(
    ["Situación", "Qué hacer"],
    [
        ["No puedo iniciar SysGym.", "Cerrá y abrí la aplicación. Si el problema continúa, contactá al administrador del equipo o al soporte e informá el mensaje que aparece. No borres datos para probar."],
        ["Olvidé la contraseña.", "Pedí a un Administrador que la restablezca desde Usuarios y roles. La aplicación no muestra la contraseña anterior."],
        ["No aparece una foto.", "Verificá que el registro tenga ruta de imagen y que el archivo no se haya movido. Pedí a Administración que vuelva a asociarlo."],
        ["No puedo reactivar una membresía.", "Revisá las cuotas vencidas y el límite de deuda, y confirmá que el socio también esté activo."],
        ["El PDF no abre.", "Comprobá que haya un lector PDF predeterminado y guardá el archivo en una carpeta donde tengas permiso."],
        ["El instalador solicita .NET Framework 4.8.", "Instalá el requisito desde el enlace oficial que muestra el aviso y volvé a ejecutar el instalador."],
        ["El análisis muestra datos inesperados.", "Verificá Desde/Hasta y pulsá Aplicar. Recordá que los gráficos de estado reflejan la situación actual, no un historial reconstruido."],
    ],
)

H1("8. Glosario")
TBL(
    ["Término", "Significado"],
    [
        ["Socio", "Persona registrada en el gimnasio."],
        ["Membresía", "Servicio contratado por un socio mediante un plan."],
        ["Cuota", "Importe correspondiente a un período de una membresía."],
        ["Pago pendiente", "Pago que todavía no figura como aprobado."],
        ["Pago aprobado", "Pago confirmado y aplicado a una cuota."],
        ["Anular", "Dejar sin efecto un pago registrado, conservando su historial."],
        ["Reembolsar", "Registrar que se devolvió el dinero de un pago aprobado."],
        ["Plan", "Servicio y precio ofrecidos por el gimnasio."],
        ["Rutina modelo", "Rutina reutilizable organizada por objetivo y nivel."],
        ["Rutina personalizada", "Rutina preparada por un entrenador para un alumno."],
        ["IMC", "Indicador calculado con peso y altura; es orientativo y no reemplaza una evaluación profesional."],
        ["Auditoría", "Historial de acciones realizadas en SysGym."],
        ["Reporte operativo", "Listado para consultar situaciones que requieren seguimiento."],
        ["Análisis", "Vista de indicadores e informes para un período seleccionado."],
        ["Copia de seguridad", "Archivo que permite recuperar la información guardada en SysGym."],
    ],
)

def font_paths():
    candidates = [
        ("C:/Windows/Fonts/arial.ttf", "C:/Windows/Fonts/arialbd.ttf", "C:/Windows/Fonts/ariali.ttf"),
        ("C:/Windows/Fonts/calibri.ttf", "C:/Windows/Fonts/calibrib.ttf", "C:/Windows/Fonts/calibrii.ttf"),
    ]
    for regular, bold, italic in candidates:
        if os.path.exists(regular) and os.path.exists(bold):
            return regular, bold, italic if os.path.exists(italic) else regular
    raise RuntimeError("No se encontraron fuentes TrueType Unicode de Windows.")


def register_pdf_fonts():
    regular, bold, italic = font_paths()
    pdfmetrics.registerFont(TTFont("SysGym", regular))
    pdfmetrics.registerFont(TTFont("SysGym-Bold", bold))
    pdfmetrics.registerFont(TTFont("SysGym-Italic", italic))
    serif_regular = "C:/Windows/Fonts/cambria.ttc"
    serif_bold = "C:/Windows/Fonts/cambriab.ttf"
    serif_italic = "C:/Windows/Fonts/cambriai.ttf"
    if all(os.path.exists(path) for path in (serif_regular, serif_bold, serif_italic)):
        pdfmetrics.registerFont(TTFont("SysGym-Serif", serif_regular))
        pdfmetrics.registerFont(TTFont("SysGym-Serif-Bold", serif_bold))
        pdfmetrics.registerFont(TTFont("SysGym-Serif-Italic", serif_italic))
    else:
        pdfmetrics.registerFont(TTFont("SysGym-Serif", regular))
        pdfmetrics.registerFont(TTFont("SysGym-Serif-Bold", bold))
        pdfmetrics.registerFont(TTFont("SysGym-Serif-Italic", italic))


def make_pdf():
    register_pdf_fonts()
    styles = getSampleStyleSheet()
    styles.add(ParagraphStyle(
        name="CoverTitle", fontName="SysGym-Bold", fontSize=26, leading=32,
        textColor=colors.HexColor("#" + INK), alignment=TA_CENTER, spaceAfter=8,
    ))
    styles.add(ParagraphStyle(
        name="CoverSub", fontName="SysGym-Serif-Italic", fontSize=16, leading=21,
        textColor=colors.HexColor("#" + INK), alignment=TA_CENTER, spaceAfter=8,
    ))
    styles.add(ParagraphStyle(
        name="CoverLine", fontName="SysGym", fontSize=9.5, leading=14,
        textColor=colors.HexColor("#" + INK), alignment=TA_CENTER, spaceAfter=5,
    ))
    styles.add(ParagraphStyle(
        name="H1", fontName="SysGym-Bold", fontSize=16, leading=20,
        textColor=colors.HexColor("#" + INK), spaceBefore=13, spaceAfter=8,
        keepWithNext=True,
    ))
    styles.add(ParagraphStyle(
        name="H2", fontName="SysGym-Serif-Bold", fontSize=12.2, leading=15,
        textColor=colors.HexColor("#" + INK), spaceBefore=10, spaceAfter=5,
        keepWithNext=True,
    ))
    styles.add(ParagraphStyle(
        name="H3", fontName="SysGym-Bold", fontSize=9.6, leading=12,
        textColor=colors.HexColor("#" + INK), spaceBefore=7, spaceAfter=4,
        keepWithNext=True,
    ))
    styles.add(ParagraphStyle(
        name="BodyS", fontName="SysGym", fontSize=8.6, leading=12.2,
        textColor=colors.HexColor("#" + INK), spaceAfter=5.5,
    ))
    styles.add(ParagraphStyle(
        name="Small", fontName="SysGym", fontSize=7.3, leading=9.4,
        textColor=colors.HexColor("#" + INK),
    ))
    styles.add(ParagraphStyle(
        name="SmallBold", fontName="SysGym-Bold", fontSize=7.3, leading=9.4,
        textColor=colors.white,
    ))
    styles.add(ParagraphStyle(
        name="SmallHeader", fontName="SysGym-Bold", fontSize=7.3, leading=9.4,
        textColor=colors.black,
    ))
    styles.add(ParagraphStyle(
        name="CodeS", fontName="Courier", fontSize=6.5, leading=8.6,
        textColor=colors.HexColor("#" + INK),
    ))
    styles.add(ParagraphStyle(
        name="CaptionS", fontName="SysGym-Italic", fontSize=7.2, leading=9,
        textColor=colors.HexColor("#" + MUTED), alignment=TA_CENTER,
    ))
    styles.add(ParagraphStyle(name="TOCTitle", fontName="SysGym-Bold", fontSize=20,
                              leading=25, textColor=colors.black, spaceAfter=12))
    styles.add(ParagraphStyle(name="TOC1S", fontName="SysGym-Bold", fontSize=8.8,
                              leading=11, leftIndent=0, spaceBefore=3, textColor=colors.black))
    styles.add(ParagraphStyle(name="TOC2S", fontName="SysGym-Serif-Bold", fontSize=8.1,
                              leading=10, leftIndent=13, textColor=colors.black))
    styles.add(ParagraphStyle(name="TOC3S", fontName="SysGym", fontSize=7.5,
                              leading=9, leftIndent=26, textColor=colors.black))
    styles.add(ParagraphStyle(name="TOCPageS", fontName="SysGym", fontSize=8,
                              leading=10, alignment=2, textColor=colors.black))
    styles.add(ParagraphStyle(name="BulletS", fontName="SysGym", fontSize=8.6, leading=12.2,
                              leftIndent=12, firstLineIndent=-8, textColor=colors.HexColor("#" + INK),
                              spaceAfter=3))
    styles.add(ParagraphStyle(name="StepS", fontName="SysGym", fontSize=8.6, leading=12.2,
                              leftIndent=18, firstLineIndent=-18, textColor=colors.HexColor("#" + INK),
                              spaceAfter=3))

    class ManualDoc(BaseDocTemplate):
        def __init__(self, filename, **kwargs):
            BaseDocTemplate.__init__(self, filename, **kwargs)
            frame = Frame(17*mm, 17*mm, A4[0]-34*mm, A4[1]-33*mm,
                          leftPadding=0, rightPadding=0, topPadding=0, bottomPadding=0, id="normal")
            self.addPageTemplates(PageTemplate(id="main", frames=[frame], onPage=self.draw_page))
            self._heading_count = 0
            self.heading_entries = []

        def draw_page(self, canv, doc):
            if canv.getPageNumber() == 1:
                return
            canv.saveState()
            canv.setStrokeColor(colors.HexColor("#" + MID))
            canv.setLineWidth(0.55)
            canv.line(17*mm, A4[1]-13*mm, A4[0]-17*mm, A4[1]-13*mm)
            canv.setFont("SysGym-Bold", 7)
            canv.setFillColor(colors.HexColor("#" + BLUE))
            canv.drawString(17*mm, A4[1]-10.5*mm, "SYSGYM  /  MANUAL DE USUARIO")
            canv.setStrokeColor(colors.HexColor("#" + MID))
            canv.line(17*mm, 13*mm, A4[0]-17*mm, 13*mm)
            canv.setFont("SysGym", 7)
            canv.setFillColor(colors.HexColor("#" + MUTED))
            canv.drawString(17*mm, 8.5*mm, "Edición " + FECHA_EDICION)
            canv.drawRightString(A4[0]-17*mm, 8.5*mm, "Página " + str(canv.getPageNumber()))
            canv.restoreState()

        def afterFlowable(self, flowable):
            if not isinstance(flowable, Paragraph):
                return
            level_by_style = {"H1": 0, "H2": 1, "H3": 2}
            level = level_by_style.get(flowable.style.name)
            if level is None:
                return
            text = flowable.getPlainText()
            self._heading_count += 1
            key = "sec-%d" % self._heading_count
            self.canv.bookmarkPage(key)
            self.canv.addOutlineEntry(text, key, level=level, closed=(level == 0))
            self.heading_entries.append((level, text, key, self.page))

    def esc(text):
        return html.escape(str(text), quote=False).replace("\n", "<br/>")

    heading_sequence = []
    for block in BLOCKS:
        level = {"h1": 0, "h2": 1, "h3": 2}.get(block[0])
        if level is not None:
            heading_sequence.append((level, str(block[1]), "sec-%d" % (len(heading_sequence) + 1)))

    def fitted_image(path, max_width, max_height):
        flow = Image(path)
        scale = min(max_width / flow.imageWidth, max_height / flow.imageHeight, 1.0)
        flow.drawWidth = flow.imageWidth * scale
        flow.drawHeight = flow.imageHeight * scale
        flow.hAlign = "CENTER"
        return flow

    def make_story(page_map):
        story = []
        if os.path.exists(COVER_LOGO):
            story.append(Spacer(1, 12*mm))
            story.append(fitted_image(COVER_LOGO, 78*mm, 75*mm))
        story.extend([Spacer(1, 14*mm), Paragraph("Manual de usuario", styles["CoverTitle"]),
                      Paragraph("SysGym", styles["CoverSub"]),
                      Paragraph("Guía para Recepción, Entrenadores y Administración", styles["CoverLine"]),
                      Paragraph("Guía paso a paso de los flujos de SysGym", styles["CoverLine"]),
                      Spacer(1, 20*mm), Paragraph("Noviembre de 2026", styles["CoverLine"]), PageBreak(),
                      Paragraph("Contenido", styles["TOCTitle"])])
        toc_rows = []
        for level, title, key in heading_sequence:
            style = styles["TOC%dS" % (level + 1)]
            link_text = '<link href="#%s" color="#000000">%s</link>' % (key, esc(title))
            toc_rows.append([Paragraph(link_text, style),
                             Paragraph(str(page_map.get(key, "·")), styles["TOCPageS"])])
        toc = Table(toc_rows, colWidths=[157*mm, 9*mm], hAlign="LEFT", repeatRows=0)
        toc.setStyle(TableStyle([
            ("VALIGN", (0, 0), (-1, -1), "TOP"),
            ("LEFTPADDING", (0, 0), (-1, -1), 0), ("RIGHTPADDING", (0, 0), (-1, -1), 2),
            ("TOPPADDING", (0, 0), (-1, -1), 1), ("BOTTOMPADDING", (0, 0), (-1, -1), 1),
        ]))
        story.extend([toc, PageBreak()])

        for block in BLOCKS:
            kind = block[0]
            if kind in ("h1", "h2", "h3"):
                story.append(Paragraph(esc(block[1]), styles[kind.upper()]))
            elif kind == "p":
                story.append(Paragraph(esc(block[1]), styles["BodyS"]))
            elif kind in ("ul", "steps"):
                for i, item in enumerate(block[1], 1):
                    prefix = "•" if kind == "ul" else "%d." % i
                    style = styles["BulletS" if kind == "ul" else "StepS"]
                    story.append(Paragraph("%s&nbsp;&nbsp;%s" % (prefix, esc(item)), style))
                story.append(Spacer(1, 2))
            elif kind == "tbl":
                headers, rows, widths = block[1], block[2], block[3]
                database_table = block[4] if len(block) > 4 else False
                raw = [headers] + rows
                data = [[Paragraph(esc(cell), styles["SmallBold"] if row_index == 0 else styles["Small"])
                         for cell in row] for row_index, row in enumerate(raw)]
                available = A4[0] - 34*mm
                if widths:
                    total = float(sum(widths))
                    col_widths = [available * width / total for width in widths]
                    col_widths[-1] += available - sum(col_widths)
                else:
                    col_widths = [available / len(headers)] * len(headers)
                table = Table(data, colWidths=col_widths, repeatRows=1, hAlign="LEFT")
                table_style = [
                    ("BACKGROUND", (0, 0), (-1, 0), colors.black),
                    ("TEXTCOLOR", (0, 0), (-1, 0), colors.white),
                    ("VALIGN", (0, 0), (-1, -1), "TOP"),
                    ("GRID", (0, 0), (-1, -1), 0.35, colors.black),
                    ("LEFTPADDING", (0, 0), (-1, -1), 4), ("RIGHTPADDING", (0, 0), (-1, -1), 4),
                    ("TOPPADDING", (0, 0), (-1, -1), 3), ("BOTTOMPADDING", (0, 0), (-1, -1), 3),
                ]
                table.setStyle(TableStyle(table_style))
                story.extend([table, Spacer(1, 4)])
            elif kind == "code":
                lines = block[1].splitlines() or [""]
                code_data = [[Paragraph(esc(line) or "&nbsp;", styles["CodeS"])] for line in lines]
                code_table = Table(code_data, colWidths=[A4[0] - 34*mm], hAlign="LEFT")
                code_table.setStyle(TableStyle([
                    ("BACKGROUND", (0, 0), (-1, -1), colors.white),
                    ("BOX", (0, 0), (-1, -1), 0.45, colors.black),
                    ("LEFTPADDING", (0, 0), (-1, -1), 6), ("RIGHTPADDING", (0, 0), (-1, -1), 6),
                    ("TOPPADDING", (0, 0), (-1, -1), 2), ("BOTTOMPADDING", (0, 0), (-1, -1), 2),
                ]))
                story.extend([code_table, Spacer(1, 5)])
            elif kind == "note":
                note_text = "<b>%s.</b> %s" % (esc(block[1]), esc(block[2]))
                story.extend([Paragraph(note_text, styles["BodyS"]), Spacer(1, 3)])
            elif kind == "fig":
                path = prepare_figure(block[1])
                story.append(fitted_image(path, A4[0] - 42*mm, A4[1] - 88*mm))
                story.append(Spacer(1, 2))
                story.append(Paragraph(esc(block[2]), styles["CaptionS"]))
                story.append(Spacer(1, 5))
            elif kind == "page":
                story.append(PageBreak())
        return story

    class ManualDoc(BaseDocTemplate):
        def __init__(self, filename, **kwargs):
            BaseDocTemplate.__init__(self, filename, **kwargs)
            frame = Frame(17*mm, 17*mm, A4[0]-34*mm, A4[1]-33*mm,
                          leftPadding=0, rightPadding=0, topPadding=0, bottomPadding=0, id="normal")
            self.addPageTemplates(PageTemplate(id="main", frames=[frame], onPage=self.draw_page))
            self._heading_count = 0
            self.heading_entries = []

        def draw_page(self, canv, doc):
            if canv.getPageNumber() == 1:
                return
            canv.saveState()
            canv.setStrokeColor(colors.HexColor("#" + MID))
            canv.setLineWidth(0.55)
            canv.line(17*mm, A4[1]-13*mm, A4[0]-17*mm, A4[1]-13*mm)
            canv.setFont("SysGym-Bold", 7)
            canv.setFillColor(colors.black)
            canv.drawString(17*mm, A4[1]-10.5*mm, "SYSGYM  /  MANUAL DE USUARIO")
            canv.line(17*mm, 13*mm, A4[0]-17*mm, 13*mm)
            canv.setFont("SysGym", 7)
            canv.setFillColor(colors.HexColor("#" + MUTED))
            canv.drawString(17*mm, 8.5*mm, "Edición " + FECHA_EDICION)
            canv.drawRightString(A4[0]-17*mm, 8.5*mm, "Página " + str(canv.getPageNumber()))
            canv.restoreState()

        def afterFlowable(self, flowable):
            if not isinstance(flowable, Paragraph):
                return
            level = {"H1": 0, "H2": 1, "H3": 2}.get(flowable.style.name)
            if level is None:
                return
            text = flowable.getPlainText()
            self._heading_count += 1
            key = "sec-%d" % self._heading_count
            self.canv.bookmarkPage(key)
            self.canv.addOutlineEntry(text, key, level=level, closed=(level == 0))
            self.heading_entries.append((level, text, key, self.page))

    page_map = {}
    last_map = None
    for _ in range(5):
        doc = ManualDoc(PDF_OUT, pagesize=A4, title="Manual de usuario SysGym 2026",
                        author="SysGym")
        doc.build(make_story(page_map))
        current_map = {key: page for _, _, key, page in doc.heading_entries}
        if current_map == last_map:
            break
        last_map = current_map
        page_map = current_map


W_NS = "http://schemas.openxmlformats.org/wordprocessingml/2006/main"
R_NS = "http://schemas.openxmlformats.org/officeDocument/2006/relationships"
WP_NS = "http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing"
A_NS = "http://schemas.openxmlformats.org/drawingml/2006/main"
PIC_NS = "http://schemas.openxmlformats.org/drawingml/2006/picture"
PKG_REL_NS = "http://schemas.openxmlformats.org/package/2006/relationships"
CT_NS = "http://schemas.openxmlformats.org/package/2006/content-types"
ET.register_namespace("w", W_NS)
ET.register_namespace("r", R_NS)
ET.register_namespace("wp", WP_NS)
ET.register_namespace("a", A_NS)
ET.register_namespace("pic", PIC_NS)


def wtag(local):
    return "{" + W_NS + "}" + local


def word_run(parent, text, *, bold=False, color=None, font=None, size=None, preserve=False, italic=False):
    run = ET.SubElement(parent, wtag("r"))
    props = ET.SubElement(run, wtag("rPr"))
    if bold:
        ET.SubElement(props, wtag("b"))
    if italic:
        ET.SubElement(props, wtag("i"))
    if color:
        ET.SubElement(props, wtag("color"), {wtag("val"): color})
    if font:
        ET.SubElement(props, wtag("rFonts"), {wtag("ascii"): font, wtag("hAnsi"): font})
    if size:
        ET.SubElement(props, wtag("sz"), {wtag("val"): str(size)})
    node = ET.SubElement(run, wtag("t"))
    if preserve or text.startswith(" ") or text.endswith(" ") or "\n" in text:
        node.set("{http://www.w3.org/XML/1998/namespace}space", "preserve")
    node.text = text
    return run


def word_paragraph(parent, text="", *, style=None, color=None, bold=False, size=None, font=None,
                   before=0, after=100, left=0, hanging=0, keep=False, italic=False, align=None):
    paragraph = ET.SubElement(parent, wtag("p"))
    ppr = ET.SubElement(paragraph, wtag("pPr"))
    if style:
        ET.SubElement(ppr, wtag("pStyle"), {wtag("val"): style})
    spacing = {"{"+W_NS+"}before": str(before), "{"+W_NS+"}after": str(after)}
    ET.SubElement(ppr, wtag("spacing"), spacing)
    if left or hanging:
        ind = {wtag("left"): str(left)}
        if hanging:
            ind[wtag("hanging")] = str(hanging)
        ET.SubElement(ppr, wtag("ind"), ind)
    if keep:
        ET.SubElement(ppr, wtag("keepNext"))
    if align:
        ET.SubElement(ppr, wtag("jc"), {wtag("val"): align})
    if text:
        word_run(paragraph, text, bold=bold, color=color, font=font, size=size, italic=italic)
    return paragraph


def word_page_break(parent):
    p = ET.SubElement(parent, wtag("p"))
    run = ET.SubElement(p, wtag("r"))
    ET.SubElement(run, wtag("br"), {wtag("type"): "page"})


def word_table(parent, headers, rows, widths=None, *, code=False, note=False, database=False):
    table = ET.SubElement(parent, wtag("tbl"))
    props = ET.SubElement(table, wtag("tblPr"))
    ET.SubElement(props, wtag("tblW"), {wtag("w"): "9800", wtag("type"): "dxa"})
    ET.SubElement(props, wtag("tblLayout"), {wtag("type"): "fixed"})
    borders = ET.SubElement(props, wtag("tblBorders"))
    for side in ("top", "left", "bottom", "right", "insideH", "insideV"):
        ET.SubElement(borders, wtag(side), {wtag("val"): "single", wtag("sz"): "4", wtag("space"): "0", wtag("color"): MID})
    if widths is None:
        widths = [9800 // len(headers)] * len(headers)
        widths[-1] += 9800 - sum(widths)
    grid = ET.SubElement(table, wtag("tblGrid"))
    for width in widths:
        ET.SubElement(grid, wtag("gridCol"), {wtag("w"): str(width)})
    all_rows = [headers] + rows
    for row_index, row_data in enumerate(all_rows):
        row = ET.SubElement(table, wtag("tr"))
        trpr = ET.SubElement(row, wtag("trPr"))
        ET.SubElement(trpr, wtag("cantSplit"))
        if row_index == 0 and not (code or note):
            ET.SubElement(trpr, wtag("tblHeader"))
        for col_index, value in enumerate(row_data):
            cell = ET.SubElement(row, wtag("tc"))
            tcpr = ET.SubElement(cell, wtag("tcPr"))
            ET.SubElement(tcpr, wtag("tcW"), {wtag("w"): str(widths[col_index]), wtag("type"): "dxa"})
            if row_index == 0 and not (code or note):
                ET.SubElement(tcpr, wtag("shd"), {wtag("fill"): "000000", wtag("val"): "clear"})
            margins = ET.SubElement(tcpr, wtag("tcMar"))
            for side, amount in (("top", "55"), ("bottom", "55"), ("start", "75"), ("end", "75")):
                ET.SubElement(margins, wtag(side), {wtag("w"): amount, wtag("type"): "dxa"})
            p = ET.SubElement(cell, wtag("p"))
            ppr = ET.SubElement(p, wtag("pPr"))
            ET.SubElement(ppr, wtag("spacing"), {wtag("before"): "0", wtag("after"): "20", wtag("line"): "220", wtag("lineRule"): "auto"})
            if code:
                word_run(p, str(value), font="Consolas", size=15, preserve=True, color=INK)
            elif row_index == 0 and not (note):
                word_run(p, str(value), bold=True, color="FFFFFF", size=15)
            elif note and col_index == 0:
                word_run(p, str(value), bold=True, color=NAVY, size=17)
            else:
                word_run(p, str(value), size=14 if len(str(value)) > 95 else 15, color=INK)
    return table


def word_toc(parent, heading_rows):
    table = ET.SubElement(parent, wtag("tbl"))
    props = ET.SubElement(table, wtag("tblPr"))
    ET.SubElement(props, wtag("tblW"), {wtag("w"): "9800", wtag("type"): "dxa"})
    borders = ET.SubElement(props, wtag("tblBorders"))
    for side in ("top", "left", "bottom", "right", "insideH", "insideV"):
        ET.SubElement(borders, wtag(side), {wtag("val"): "nil"})
    grid = ET.SubElement(table, wtag("tblGrid"))
    ET.SubElement(grid, wtag("gridCol"), {wtag("w"): "9000"})
    ET.SubElement(grid, wtag("gridCol"), {wtag("w"): "800"})
    for _, level, title, bookmark in heading_rows:
        row = ET.SubElement(table, wtag("tr"))
        trpr = ET.SubElement(row, wtag("trPr"))
        ET.SubElement(trpr, wtag("cantSplit"))
        for column_index, width in enumerate((9000, 800)):
            cell = ET.SubElement(row, wtag("tc"))
            tcpr = ET.SubElement(cell, wtag("tcPr"))
            ET.SubElement(tcpr, wtag("tcW"), {wtag("w"): str(width), wtag("type"): "dxa"})
            p = ET.SubElement(cell, wtag("p"))
            ppr = ET.SubElement(p, wtag("pPr"))
            ET.SubElement(ppr, wtag("spacing"), {wtag("before"): "0", wtag("after"): "25"})
            if column_index == 0:
                ET.SubElement(ppr, wtag("ind"), {wtag("left"): str(level * 260)})
                hyperlink = ET.SubElement(p, wtag("hyperlink"), {wtag("anchor"): bookmark, wtag("history"): "1"})
                run = ET.SubElement(hyperlink, wtag("r"))
                rpr = ET.SubElement(run, wtag("rPr"))
                toc_font = "Cambria" if level == 1 else "Arial"
                ET.SubElement(rpr, wtag("rFonts"), {wtag("ascii"): toc_font, wtag("hAnsi"): toc_font})
                ET.SubElement(rpr, wtag("sz"), {wtag("val"): str(18 if level == 0 else 16)})
                ET.SubElement(rpr, wtag("color"), {wtag("val"): INK})
                if level < 2:
                    ET.SubElement(rpr, wtag("b"))
                text_node = ET.SubElement(run, wtag("t"))
                text_node.text = title
            else:
                ET.SubElement(ppr, wtag("jc"), {wtag("val"): "right"})
                field = ET.SubElement(p, wtag("fldSimple"), {wtag("instr"): " PAGEREF %s \\h " % bookmark})
                run = ET.SubElement(field, wtag("r"))
                rpr = ET.SubElement(run, wtag("rPr"))
                ET.SubElement(rpr, wtag("rFonts"), {wtag("ascii"): "Arial", wtag("hAnsi"): "Arial"})
                ET.SubElement(rpr, wtag("sz"), {wtag("val"): "16"})
                cached_page = ET.SubElement(run, wtag("t"))
                cached_page.text = "1"
    return table


def word_bookmark(paragraph, bookmark_id, name):
    start = ET.Element(wtag("bookmarkStart"), {wtag("id"): str(bookmark_id), wtag("name"): name})
    end = ET.Element(wtag("bookmarkEnd"), {wtag("id"): str(bookmark_id)})
    paragraph.insert(1, start)
    paragraph.append(end)


def drawing_paragraph(parent, rel_id, filename, width_px, height_px, image_no, caption,
                      max_width=6.55, max_height=4.45, before=80):
    p = ET.SubElement(parent, wtag("p"))
    ppr = ET.SubElement(p, wtag("pPr"))
    ET.SubElement(ppr, wtag("jc"), {wtag("val"): "center"})
    ET.SubElement(ppr, wtag("spacing"), {wtag("before"): str(before), wtag("after"): "35"})
    max_w = max_width
    max_h = max_height
    factor = min(max_w / (width_px / 96.0), max_h / (height_px / 96.0))
    cx = int(width_px / 96.0 * factor * 914400)
    cy = int(height_px / 96.0 * factor * 914400)
    run = ET.SubElement(p, wtag("r"))
    drawing = ET.SubElement(run, wtag("drawing"))
    inline = ET.SubElement(drawing, "{" + WP_NS + "}inline", {"distT": "0", "distB": "0", "distL": "0", "distR": "0"})
    ET.SubElement(inline, "{" + WP_NS + "}extent", {"cx": str(cx), "cy": str(cy)})
    ET.SubElement(inline, "{" + WP_NS + "}effectExtent", {"l": "0", "t": "0", "r": "0", "b": "0"})
    ET.SubElement(inline, "{" + WP_NS + "}docPr", {"id": str(image_no), "name": filename, "descr": caption})
    frame = ET.SubElement(inline, "{" + WP_NS + "}cNvGraphicFramePr")
    ET.SubElement(frame, "{" + A_NS + "}graphicFrameLocks", {"noChangeAspect": "1"})
    graphic = ET.SubElement(inline, "{" + A_NS + "}graphic")
    data = ET.SubElement(graphic, "{" + A_NS + "}graphicData", {"uri": PIC_NS})
    pic = ET.SubElement(data, "{" + PIC_NS + "}pic")
    nv = ET.SubElement(pic, "{" + PIC_NS + "}nvPicPr")
    ET.SubElement(nv, "{" + PIC_NS + "}cNvPr", {"id": str(image_no), "name": filename})
    ET.SubElement(nv, "{" + PIC_NS + "}cNvPicPr")
    fill = ET.SubElement(pic, "{" + PIC_NS + "}blipFill")
    ET.SubElement(fill, "{" + A_NS + "}blip", {"{" + R_NS + "}embed": rel_id})
    stretch = ET.SubElement(fill, "{" + A_NS + "}stretch")
    ET.SubElement(stretch, "{" + A_NS + "}fillRect")
    sp = ET.SubElement(pic, "{" + PIC_NS + "}spPr")
    xfrm = ET.SubElement(sp, "{" + A_NS + "}xfrm")
    ET.SubElement(xfrm, "{" + A_NS + "}off", {"x": "0", "y": "0"})
    ET.SubElement(xfrm, "{" + A_NS + "}ext", {"cx": str(cx), "cy": str(cy)})
    geom = ET.SubElement(sp, "{" + A_NS + "}prstGeom", {"prst": "rect"})
    ET.SubElement(geom, "{" + A_NS + "}avLst")
    if caption:
        cap = word_paragraph(parent, caption, color=MUTED, size=14, italic=True, after=100, align="center")


def make_word():
    root = ET.Element(wtag("document"))
    body = ET.SubElement(root, wtag("body"))
    heading_rows = []
    for block_index, block in enumerate(BLOCKS):
        level = {"h1": 0, "h2": 1, "h3": 2}.get(block[0])
        if level is not None:
            bookmark = "sgSec%03d" % (len(heading_rows) + 1)
            heading_rows.append((block_index, level, block[1], bookmark))
    image_rels = []
    media_files = []
    image_counter = [0]

    def add_word_image(image_path, caption=None, max_width=6.55, max_height=4.45, before=80):
        if not os.path.exists(image_path):
            raise FileNotFoundError("Falta imagen requerida: " + image_path)
        image_counter[0] += 1
        image_no = image_counter[0]
        ext = os.path.splitext(image_path)[1].lower()
        rel_id = "rIdImg%d" % image_no
        media_name = "image%d%s" % (image_no, ext)
        media_files.append((image_path, media_name))
        image_rels.append((rel_id, "media/" + media_name))
        with PILImage.open(image_path) as image:
            width_px, height_px = image.size
        drawing_paragraph(body, rel_id, media_name, width_px, height_px, image_no,
                          caption or "Logotipo SysGym", max_width=max_width,
                          max_height=max_height, before=before)

    add_word_image(COVER_LOGO, max_width=4.35, max_height=2.2, before=1250)
    word_paragraph(body, "Manual de usuario", color=INK, bold=True, size=38, after=120, align="center")
    word_paragraph(body, "SysGym", color=INK, font="Cambria", size=24, italic=True, after=110, align="center")
    word_paragraph(body, "Guía para Recepción, Entrenadores y Administración", color=INK, size=17, after=70, align="center")
    word_paragraph(body, "Guía paso a paso de los flujos de SysGym", color=INK, size=15, after=180, align="center")
    word_paragraph(body, "Noviembre de 2026", color=INK, size=15, before=800, after=150, align="center")
    word_page_break(body)
    word_paragraph(body, "Contenido", color=INK, bold=True, size=30, after=180)
    word_toc(body, heading_rows)
    word_page_break(body)

    heading_bookmarks = {row[0]: (index + 1, row[3]) for index, row in enumerate(heading_rows)}
    for block_index, block in enumerate(BLOCKS):
        kind = block[0]
        if kind == "h1":
            paragraph = word_paragraph(body, block[1], style="Heading1", color=INK, font="Arial", bold=True, size=21, before=150, after=90, keep=True)
            word_bookmark(paragraph, *heading_bookmarks[block_index])
        elif kind == "h2":
            paragraph = word_paragraph(body, block[1], style="Heading2", color=INK, font="Cambria", bold=True, size=17, before=110, after=60, keep=True)
            word_bookmark(paragraph, *heading_bookmarks[block_index])
        elif kind == "h3":
            paragraph = word_paragraph(body, block[1], style="Heading3", color=INK, font="Arial", bold=True, size=15, before=85, after=45, keep=True)
            word_bookmark(paragraph, *heading_bookmarks[block_index])
        elif kind == "p":
            word_paragraph(body, block[1], size=16, color=INK, after=90)
        elif kind in ("ul", "steps"):
            for index, text in enumerate(block[1], 1):
                prefix = "•  " if kind == "ul" else str(index) + ".  "
                word_paragraph(body, prefix + text, size=16, color=INK, after=65, left=250, hanging=180)
        elif kind == "tbl":
            headers, rows = block[1], block[2]
            widths = block[3]
            if widths:
                total = float(sum(widths))
                dxa = [int(9800 * width / total) for width in widths]
                dxa[-1] += 9800 - sum(dxa)
            else:
                dxa = None
            database_table = block[4] if len(block) > 4 else False
            word_table(body, headers, rows, dxa, database=database_table)
            word_paragraph(body, "", after=40)
        elif kind == "code":
            lines = block[1].splitlines() or [" "]
            word_table(body, [lines[0] if lines[0] else " "],
                       [[line if line else " "] for line in lines[1:]], [9800], code=True)
        elif kind == "note":
            paragraph = word_paragraph(body, "", size=16, color=INK, after=100)
            word_run(paragraph, block[1] + ". ", bold=True, color=INK, size=16)
            word_run(paragraph, block[2], color=INK, size=16)
        elif kind == "fig":
            image_path = prepare_figure(block[1])
            add_word_image(image_path, block[2])
        elif kind == "page":
            word_page_break(body)

    sect = ET.SubElement(body, wtag("sectPr"))
    ET.SubElement(sect, wtag("headerReference"), {wtag("type"): "default", "{" + R_NS + "}id": "rIdHeader"})
    ET.SubElement(sect, wtag("headerReference"), {wtag("type"): "first", "{" + R_NS + "}id": "rIdHeaderFirst"})
    ET.SubElement(sect, wtag("footerReference"), {wtag("type"): "default", "{" + R_NS + "}id": "rIdFooter"})
    ET.SubElement(sect, wtag("footerReference"), {wtag("type"): "first", "{" + R_NS + "}id": "rIdFooterFirst"})
    ET.SubElement(sect, wtag("pgSz"), {wtag("w"): "11906", wtag("h"): "16838"})
    ET.SubElement(sect, wtag("pgMar"), {
        wtag("top"): "900", wtag("right"): "950", wtag("bottom"): "900",
        wtag("left"): "950", wtag("header"): "400", wtag("footer"): "450", wtag("gutter"): "0",
    })
    ET.SubElement(sect, wtag("titlePg"))

    styles = ET.Element(wtag("styles"))
    defaults = ET.SubElement(styles, wtag("docDefaults"))
    rpr_default = ET.SubElement(defaults, wtag("rPrDefault"))
    rpr = ET.SubElement(rpr_default, wtag("rPr"))
    ET.SubElement(rpr, wtag("rFonts"), {wtag("ascii"): "Arial", wtag("hAnsi"): "Arial"})
    ET.SubElement(rpr, wtag("sz"), {wtag("val"): "16"})
    for sid, name, size, color, font in [
        ("Normal", "Normal", 16, INK, "Arial"),
        ("Heading1", "heading 1", 21, INK, "Arial"),
        ("Heading2", "heading 2", 17, INK, "Cambria"),
        ("Heading3", "heading 3", 15, INK, "Arial"),
    ]:
        style = ET.SubElement(styles, wtag("style"), {wtag("type"): "paragraph", wtag("styleId"): sid})
        ET.SubElement(style, wtag("name"), {wtag("val"): name})
        if sid != "Normal":
            ET.SubElement(style, wtag("basedOn"), {wtag("val"): "Normal"})
            ET.SubElement(style, wtag("next"), {wtag("val"): "Normal"})
            ET.SubElement(style, wtag("keepNext"))
        sp = ET.SubElement(style, wtag("rPr"))
        ET.SubElement(sp, wtag("rFonts"), {wtag("ascii"): font, wtag("hAnsi"): font})
        ET.SubElement(sp, wtag("b"))
        ET.SubElement(sp, wtag("color"), {wtag("val"): color})
        ET.SubElement(sp, wtag("sz"), {wtag("val"): str(size)})

    header = ET.Element(wtag("hdr"))
    word_paragraph(header, "SysGym  /  Manual de usuario", color=INK, bold=True, size=13, after=0)
    header_first = ET.Element(wtag("hdr"))
    settings = ET.Element(wtag("settings"))
    ET.SubElement(settings, wtag("updateFields"), {wtag("val"): "true"})
    footer = ET.Element(wtag("ftr"))
    footer_p = ET.SubElement(footer, wtag("p"))
    footer_ppr = ET.SubElement(footer_p, wtag("pPr"))
    ET.SubElement(footer_ppr, wtag("jc"), {wtag("val"): "right"})
    word_run(footer_p, "SysGym · Edición " + FECHA_EDICION + " · Página ", color=MUTED, size=13)
    fld = ET.SubElement(footer_p, wtag("r"))
    ET.SubElement(fld, wtag("fldChar"), {wtag("fldCharType"): "begin"})
    inst = ET.SubElement(ET.SubElement(footer_p, wtag("r")), wtag("instrText"))
    inst.set("{http://www.w3.org/XML/1998/namespace}space", "preserve")
    inst.text = " PAGE "
    end = ET.SubElement(ET.SubElement(footer_p, wtag("r")), wtag("fldChar"))
    end.set(wtag("fldCharType"), "end")
    footer_first = ET.Element(wtag("ftr"))

    with tempfile.TemporaryDirectory(prefix="sysgym_manual_") as temp:
        rel_root = ET.Element("{" + PKG_REL_NS + "}Relationships")
        ET.SubElement(rel_root, "{" + PKG_REL_NS + "}Relationship", {
            "Id": "rIdStyles", "Type": "http://schemas.openxmlformats.org/officeDocument/2006/relationships/styles", "Target": "styles.xml"
        })
        ET.SubElement(rel_root, "{" + PKG_REL_NS + "}Relationship", {
            "Id": "rIdSettings", "Type": "http://schemas.openxmlformats.org/officeDocument/2006/relationships/settings", "Target": "settings.xml"
        })
        ET.SubElement(rel_root, "{" + PKG_REL_NS + "}Relationship", {
            "Id": "rIdHeader", "Type": "http://schemas.openxmlformats.org/officeDocument/2006/relationships/header", "Target": "header1.xml"
        })
        ET.SubElement(rel_root, "{" + PKG_REL_NS + "}Relationship", {
            "Id": "rIdHeaderFirst", "Type": "http://schemas.openxmlformats.org/officeDocument/2006/relationships/header", "Target": "header2.xml"
        })
        ET.SubElement(rel_root, "{" + PKG_REL_NS + "}Relationship", {
            "Id": "rIdFooter", "Type": "http://schemas.openxmlformats.org/officeDocument/2006/relationships/footer", "Target": "footer1.xml"
        })
        ET.SubElement(rel_root, "{" + PKG_REL_NS + "}Relationship", {
            "Id": "rIdFooterFirst", "Type": "http://schemas.openxmlformats.org/officeDocument/2006/relationships/footer", "Target": "footer2.xml"
        })
        for rel_id, target in image_rels:
            ET.SubElement(rel_root, "{" + PKG_REL_NS + "}Relationship", {
                "Id": rel_id,
                "Type": "http://schemas.openxmlformats.org/officeDocument/2006/relationships/image",
                "Target": target,
            })
        types = ET.Element("{" + CT_NS + "}Types")
        ET.SubElement(types, "{" + CT_NS + "}Default", {"Extension": "rels", "ContentType": "application/vnd.openxmlformats-package.relationships+xml"})
        ET.SubElement(types, "{" + CT_NS + "}Default", {"Extension": "xml", "ContentType": "application/xml"})
        ET.SubElement(types, "{" + CT_NS + "}Default", {"Extension": "png", "ContentType": "image/png"})
        ET.SubElement(types, "{" + CT_NS + "}Default", {"Extension": "jpg", "ContentType": "image/jpeg"})
        ET.SubElement(types, "{" + CT_NS + "}Default", {"Extension": "jpeg", "ContentType": "image/jpeg"})
        for part, content_type in [
            ("document.xml", "application/vnd.openxmlformats-officedocument.wordprocessingml.document.main+xml"),
            ("styles.xml", "application/vnd.openxmlformats-officedocument.wordprocessingml.styles+xml"),
            ("settings.xml", "application/vnd.openxmlformats-officedocument.wordprocessingml.settings+xml"),
            ("header1.xml", "application/vnd.openxmlformats-officedocument.wordprocessingml.header+xml"),
            ("footer1.xml", "application/vnd.openxmlformats-officedocument.wordprocessingml.footer+xml"),
            ("header2.xml", "application/vnd.openxmlformats-officedocument.wordprocessingml.header+xml"),
            ("footer2.xml", "application/vnd.openxmlformats-officedocument.wordprocessingml.footer+xml"),
        ]:
            ET.SubElement(types, "{" + CT_NS + "}Override", {"PartName": "/word/" + part, "ContentType": content_type})
        package_rels = ET.Element("{" + PKG_REL_NS + "}Relationships")
        ET.SubElement(package_rels, "{" + PKG_REL_NS + "}Relationship", {
            "Id": "rId1", "Type": "http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument", "Target": "word/document.xml"
        })
        with zipfile.ZipFile(DOCX_OUT, "w", zipfile.ZIP_DEFLATED) as archive:
            archive.writestr("[Content_Types].xml", ET.tostring(types, encoding="utf-8", xml_declaration=True))
            archive.writestr("_rels/.rels", ET.tostring(package_rels, encoding="utf-8", xml_declaration=True))
            archive.writestr("word/document.xml", ET.tostring(root, encoding="utf-8", xml_declaration=True))
            archive.writestr("word/styles.xml", ET.tostring(styles, encoding="utf-8", xml_declaration=True))
            archive.writestr("word/settings.xml", ET.tostring(settings, encoding="utf-8", xml_declaration=True))
            archive.writestr("word/header1.xml", ET.tostring(header, encoding="utf-8", xml_declaration=True))
            archive.writestr("word/footer1.xml", ET.tostring(footer, encoding="utf-8", xml_declaration=True))
            archive.writestr("word/header2.xml", ET.tostring(header_first, encoding="utf-8", xml_declaration=True))
            archive.writestr("word/footer2.xml", ET.tostring(footer_first, encoding="utf-8", xml_declaration=True))
            archive.writestr("word/_rels/document.xml.rels", ET.tostring(rel_root, encoding="utf-8", xml_declaration=True))
            for source, name in media_files:
                archive.write(source, "word/media/" + name)


if __name__ == "__main__":
    with tempfile.TemporaryDirectory(prefix="sysgym_manual_figures_") as image_temp:
        IMAGE_TEMP_DIR = image_temp
        make_pdf()
        make_word()
    print("PDF:", PDF_OUT)
    print("DOCX:", DOCX_OUT)
    print("Capturas:", sum(1 for block in BLOCKS if block[0] == "fig"))
