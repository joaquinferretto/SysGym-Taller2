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
EDMX = os.path.join(ROOT, "capaDatos", "Database", "SysGymDB.edmx")
DOC_SQL = os.path.join(ROOT, "capaDatos", "Database", "DocumentarSysGymDB.sql")
DDL_SQL = os.path.join(ROOT, "capaDatos", "Database", "SysGymDB.sql")
SEED_SQL = os.path.join(ROOT, "capaDatos", "Database", "SysGymDatos.sql")
PDF_OUT = os.path.join(MANUAL, "Manual de usuario SysGym - 2026 monocromo.pdf")
DOCX_OUT = os.path.join(MANUAL, "Manual de usuario SysGym - 2026 monocromo.docx")
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
P("SysGym es una aplicación de escritorio para administrar un gimnasio. Este manual reúne los recorridos de Recepción, Entrenadores y Administración y un apéndice técnico que explica la base SysGymDB, sus tablas, relaciones, índices, scripts y transacciones.")
P("Las capturas de operación son pantallas reales de SysGym. Las capturas identificadas como seed o QA corresponden a datos de demostración y sirven para aprender a leer las salidas. No representan el estado actual de una instalación.")
TBL(
    ["Rol", "Acciones disponibles"],
    [
        ["Recepcionista", "Socios, membresías, cuotas, pagos, asignaciones de entrenadores y reportes operativos."],
        ["Entrenador", "Consultar sus alumnos, ver fichas y rutinas, elegir o modificar rutinas y exportarlas."],
        ["Administrador", "Usuarios y roles, planes, ejercicios e imágenes, configuración, reportes, análisis, auditoría y copias de seguridad."],
    ],
)
NOTE("Antes de empezar", "Los cambios de precio de un plan aplican a cuotas que se generen más adelante. Una baja conserva el historial. Las capturas pueden mostrar nombres o importes de la base de prueba.")

H1("2. Instalación, primer inicio y acceso")
H2("2.1 Requisitos e instalación")
P("Se necesita Windows 10 u 11 y .NET Framework 4.8. Ejecutá SysGym-Setup.exe, aceptá el aviso de Windows, seguí el asistente y pulsá Instalar. Al terminar, abrí SysGym desde Inicio o el acceso directo. Si se informa que falta .NET Framework 4.8, instalalo desde el enlace oficial que muestra Windows y repetí la instalación.")
H2("2.2 Crear la base y registrar al primer Administrador")
STEPS(
    "Al iniciar por primera vez, respondé Sí a la creación de la base. Esperá a que la instalación termine sin cerrar SysGym.",
    "En Registrar administrador completá nombre, apellido, DNI, sexo, nombre de usuario, contraseña y confirmación.",
    "Pulsá Registrar administrador. Si los datos son válidos, SysGym crea la cuenta inicial y abre el inicio de sesión.",
    "Ingresá con el usuario y contraseña recién creados. Después, creá las cuentas de Recepción y Entrenador desde Usuarios y roles.",
)
FIG("paso-primer-inicio.png", "Primer inicio: confirmación para crear la base.")
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
P("En Windows abrí Aplicaciones instaladas, buscá SysGym y elegí Desinstalar, o usá la opción del menú Inicio. La desinstalación quita el programa y los accesos directos; conserva datos, imágenes y copias de seguridad ubicados en %ProgramData%\\SysGym. Antes de reinstalar o trasladar la PC, guardá una copia externa.")

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
H2("3.7 Cambiar el plan de una membresía")
STEPS(
    "En Membresías, seleccioná la membresía activa que debe cambiar.",
    "Elegí el plan nuevo y pulsá Cambiar plan.",
    "Volvé a revisar la membresía y el precio. Las cuotas ya creadas conservan su importe; las cuotas futuras toman el nuevo precio.",
)
H2("3.8 Baja, reactivación y límite de deuda")
P("Una cuota pendiente se cuenta como vencida cuando terminó su período. La configuración define cuántas cuotas vencidas impagas alcanzan el límite. Con un límite de dos, la segunda cuota vencida puede dejar inactiva la membresía. Cobrar la deuda reduce el saldo impago, pero no reactiva automáticamente la membresía.")
STEPS(
    "Consultá la deuda en Cuotas y pagos o en el reporte operativo de deuda.",
    "Registrá el pago de cada cuota pendiente que se haya cobrado.",
    "Volvé a Membresías y verificá el estado actual.",
    "Si se cumplen las reglas de reactivación, seleccioná la membresía inactiva y pulsá Reactivar. Si la persona también fue dada de baja, reactivá primero al socio.",
)
H2("3.9 Asignar, cambiar o terminar la asignación de un entrenador")
STEPS(
    "Abrí Asignar entrenador y elegí la membresía.",
    "Revisá socio, plan, estado y entrenador actual.",
    "En Nuevo entrenador, seleccioná la cuenta que tenga rol Entrenador activa y guardá.",
    "Para cambiar la asignación, elegí otro entrenador y guardá. Para terminarla, usá Dar de baja en la asignación.",
)
FIG("paso-asignar-entrenador.png", "La asignación vincula una membresía con el entrenador responsable.")
H2("3.10 Consultar y exportar un reporte operativo")
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
H2("5.3 Mantener ejercicios e imágenes")
STEPS(
    "En Ejercicios, pulsá Nuevo y completá nombre y descripción; guardá.",
    "Para editar o dar de baja, seleccioná el ejercicio y usá la acción disponible. La baja conserva sus usos históricos.",
    "Con el ejercicio seleccionado, usá Agregar imagen y elegí un archivo. Si hay varias imágenes, ordenalas y confirmá que aparezcan en la ficha.",
    "Para retirar una imagen, seleccioná su registro y usá la acción disponible. El archivo se almacena fuera de la base; la tabla conserva la ruta relativa.",
)
FIG("paso-ejercicios-imagenes.png", "Catálogo de ejercicios y gestión de imágenes.")
H2("5.4 Configurar vencimientos, anticipación y avisos")
STEPS(
    "Abrí Configuración y el grupo Cuotas y pagos.",
    "Ajustá Cuotas vencidas, Meses de anticipación y Días de aviso dentro de los rangos indicados en pantalla.",
    "Pulsá Guardar para aplicar los nuevos valores.",
    "Pulsá Descartar para recuperar los valores guardados si todavía no confirmaste el cambio.",
    "Revisá el resultado en las próximas consultas de deuda, generación de cuotas y avisos.",
)
P("La configuración inicial de bases nuevas es 2 cuotas vencidas, 2 meses de anticipación y 7 días de aviso. Las bases existentes conservan sus propios valores.")
FIG("paso-configuracion.png", "Parámetros generales de cuotas y pagos.")
H2("5.5 Consultar auditoría")
STEPS(
    "Abrí Auditoría.",
    "Ingresá el rango de fechas y, si corresponde, filtrá por usuario u operación.",
    "Pulsá Aplicar filtros y revisá fecha, usuario, rol, operación, entidad, identificador y detalle.",
    "La auditoría es de consulta; no modifica los registros filtrados.",
)
FIG("paso-auditoria.png", "Historial de operaciones para seguimiento administrativo.")
H2("5.6 Consultar reportes operativos")
P("Administración comparte la consulta y exportación de los reportes operativos descritos en 3.10. La pantalla añade indicadores de socios, usuarios, membresías, rutinas y ejercicios; algunos indicadores se muestran solo al Administrador.")
H2("5.7 Consultar Análisis: período, indicadores y pestañas")
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
H2("5.8 Comparar períodos y exportar el informe")
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
H2("5.9 Crear y restaurar copias de seguridad")
STEPS(
    "Abrí Configuración y Copias de seguridad.",
    "Para crear una copia, pulsá Crear copia de seguridad, elegí destino y guardá el archivo .bak. SysGym propone un nombre con fecha y hora.",
    "Tras finalizar, comprobá la fecha de Última copia y guardá otra copia fuera de la PC.",
    "Para restaurar, pulsá Restaurar copia y seleccioná un .bak compatible de SysGym.",
    "Leé la confirmación: restaurar reemplaza los datos actuales por el contenido del respaldo. Confirmá solo si corresponde y esperá a que SysGym complete la operación y se reinicie.",
)
FIG("paso-copias.png", "Crear y restaurar copias de seguridad.")
NOTE("Restauración", "Antes de reemplazar la base actual, creá una copia de su estado presente. Guardá los archivos de respaldo en un lugar separado del equipo.")
H2("5.10 Cambiar de cuenta y cerrar")
P("Cambiar de cuenta cierra la sesión actual y vuelve al acceso. Volver abre la pantalla principal del rol. Salir cierra la aplicación. Si otra persona usará la PC, cerrá o cambiá la cuenta antes de dejarla.")

H1("6. Ejemplos completos con la seed de demostración")
P("Las siguientes capturas se tomaron de ejecuciones de QA con datos de demostración. Sirven para leer los informes y comparar resultados; no describen la base que esté instalada en otra PC.")
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
FIG("analisis-seed-pdf-p1.png", "Página 1 del reporte PDF real generado por la aplicación con seed QA. Los valores pertenecen a esa captura.")
FIG("analisis-seed-pdf-p3.png", "Página del PDF con el detalle de situación de socios de la misma ejecución.")
FIG("analisis-seed-p17.png", "Página del reporte de QA con los ejercicios más usados.")
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
FIG("analisis-seed-comparar.png", "Comparación A/B en la aplicación; los gráficos y la tabla muestran los mismos totales.")
H2("6.3 Vistas de las pestañas de Análisis")
P("Estas capturas muestran las pestañas pobladas con el conjunto QA. Las cifras dependen del rango seleccionado y del instante en que se generó la captura.")
FIG("analisis-seed-resumen.png", "Seed QA: resumen mensual.")
FIG("analisis-seed-finanzas.png", "Seed QA: finanzas y métodos de pago.")
FIG("analisis-seed-socios.png", "Seed QA: altas, estados y deuda de socios.")
FIG("analisis-seed-planes.png", "Seed QA: uso actual de planes.")
FIG("analisis-seed-entrenadores.png", "Seed QA: socios asignados por entrenador.")
FIG("analisis-seed-rutinas.png", "Seed QA: rutinas utilizadas.")
FIG("analisis-seed-ejercicios.png", "Seed QA: uso de ejercicios.")
FIG("analisis-seed-socio.png", "Seed QA: consulta individual.")
NOTE("Datos de ejemplo", "El seed SysGymDatos.sql es únicamente para desarrollo y QA. No lo ejecutes en una base productiva ni interpretes las capturas como valores actuales.")

H1("7. Preguntas frecuentes")
TBL(
    ["Situación", "Qué hacer"],
    [
        ["SysGym no conecta con SQL Server.", "Cerrá y abrí la app. Comprobá con el responsable del equipo que el servicio de base de datos esté instalado y activo. No borres archivos ni bases para probar."],
        ["Olvidé la contraseña.", "Pedí a un Administrador que la restablezca desde Usuarios y roles. La aplicación no muestra la contraseña anterior."],
        ["No aparece una foto.", "Verificá que el registro tenga ruta de imagen y que el archivo no se haya movido. Pedí a Administración que vuelva a asociarlo."],
        ["No puedo reactivar una membresía.", "Revisá las cuotas vencidas y el límite de deuda, y confirmá que el socio también esté activo."],
        ["El PDF no abre.", "Comprobá que haya un lector PDF predeterminado y guardá el archivo en una carpeta donde tengas permiso."],
        ["El instalador solicita .NET Framework 4.8.", "Instalá el requisito desde el enlace oficial que muestra el aviso y volvé a ejecutar el instalador."],
        ["El análisis muestra datos inesperados.", "Verificá Desde/Hasta y pulsá Aplicar. Recordá que los gráficos de estado reflejan la situación actual, no un historial reconstruido."],
    ],
)

H1("8. Diccionario de la base SysGymDB")
P("El modelo corresponde a SQL Server y Entity Framework 6.4.4 sobre .NET Framework 4.8. El archivo SysGymDB.edmx es una vista de diseño del esquema actual de 17 tablas, 114 columnas y las relaciones generadas desde la base SysGymDB local el 30/10/2026. Abrilo en Visual Studio como archivo EDMX para inspeccionar el diagrama.")
P("Las descripciones del inventario se aplican como propiedad extendida MS_Description para cada tabla, columna e índice mediante DocumentarSysGymDB.sql. El script puede repetirse y no cambia filas ni crea columnas.")
NOTE("Cómo leer el diccionario", "PK identifica la clave primaria; FK identifica una clave foránea; IDENTITY indica que SQL Server genera el número; NULL permitido significa que el dato puede faltar; NOT NULL significa que se requiere. Los nombres de columnas se muestran igual que en SQL Server.")

H2("8.1 Propósito de cada tabla")
TBL(
    ["Tabla", "Qué representa"],
    [
        ["AuditoriaOperacion", "Registro de operación, usuario, fecha, entidad y detalle. IdEntidad es polimórfico y no es FK."],
        ["ConfiguracionSistema", "Única configuración global de deuda, anticipación y avisos."],
        ["CuotaMembresia", "Período e importe histórico facturado a una membresía, con estado y pago vigente."],
        ["Ejercicio", "Catálogo de movimientos disponibles."],
        ["EjercicioImagen", "Ruta relativa y orden de una imagen de ejercicio almacenada fuera de SQL."],
        ["Membresia", "Historial del vínculo socio, plan, usuario que registra y rutina opcional."],
        ["MembresiaEntrenador", "Asignaciones vigentes e históricas de entrenadores a membresías."],
        ["MercadoPago", "Referencias opcionales informadas para pagos; SysGym no inicia cobros mediante API."],
        ["MetodoPago", "Catálogo de método de pago y el detalle efectivo o Mercado Pago."],
        ["Pago", "Intento de pago con importe, fecha, estado, cuota y responsable opcional."],
        ["PagoEfectivo", "Detalle identificador del método de pago en efectivo."],
        ["Plan", "Plan ofrecido y precio vigente para cuotas futuras."],
        ["Rol", "Catálogo de roles de acceso."],
        ["Rutina", "Rutina libre o modelo de entrenamiento asociado a su entrenador creador."],
        ["RutinaEjercicio", "Ejercicio, día, orden y parámetros dentro de una rutina."],
        ["Socio", "Persona registrada, medidas opcionales, foto externa y estado."],
        ["UsuarioSistema", "Cuenta y datos del empleado, rol y usuario creador opcional."],
    ],
    database=True,
)


def sql_descriptions():
    text = open(DOC_SQL, "r", encoding="utf-8-sig").read()
    pattern = re.compile(
        r"\(\s*'([^']+)'\s*,\s*(NULL|'([^']*)')\s*,\s*N'((?:''|[^'])*)'\s*\)",
        re.MULTILINE,
    )
    result = {}
    for match in pattern.finditer(text):
        table = match.group(1)
        column = match.group(3)
        description = match.group(4).replace("''", "'")
        result[(table, column)] = description
    return result


def schema_dictionary():
    root = ET.parse(EDMX).getroot()
    ns = {
        "edm": "http://schemas.microsoft.com/ado/2009/11/edm",
        "ssdl": "http://schemas.microsoft.com/ado/2009/11/edm/ssdl",
    }
    csdl = root.find(".//edm:Schema", ns)
    ssdl = root.find(".//ssdl:Schema", ns)
    descriptions = sql_descriptions()
    ss_types = {e.attrib["Name"]: e for e in ssdl.findall("ssdl:EntityType", ns)}
    descriptions_by_table = {
        table: descriptions.get((table, None), "")
        for table in ss_types
    }
    keys = {}
    foreign = set()
    for name, entity in ss_types.items():
        key = entity.find("ssdl:Key/ssdl:PropertyRef", ns)
        keys[name] = key.attrib["Name"] if key is not None else None
    for association in csdl.findall("edm:Association", ns):
        principal = association.find("edm:ReferentialConstraint/edm:Principal", ns)
        dependent = association.find("edm:ReferentialConstraint/edm:Dependent", ns)
        ends = association.findall("edm:End", ns)
        if principal is None or dependent is None or len(ends) != 2:
            continue
        role_types = {end.attrib["Role"]: end.attrib["Type"].split(".")[-1] for end in ends}
        ptable = role_types[principal.attrib["Role"]]
        dtable = role_types[dependent.attrib["Role"]]
        pcol = principal.find("edm:PropertyRef", ns).attrib["Name"]
        dcol = dependent.find("edm:PropertyRef", ns).attrib["Name"]
        foreign.add((dtable, dcol))

    tables = []
    for name in sorted(ss_types):
        entity = ss_types[name]
        fields = []
        for prop in entity.findall("ssdl:Property", ns):
            col = prop.attrib["Name"]
            sql_type = prop.attrib["Type"].lower()
            if sql_type in ("nvarchar", "varchar", "nchar", "char", "varbinary", "binary"):
                length = prop.attrib.get("MaxLength")
                if length:
                    sql_type += "(max)" if length == "Max" else "(" + length + ")"
            elif sql_type in ("decimal", "numeric"):
                precision = prop.attrib.get("Precision", "18")
                scale = prop.attrib.get("Scale", "0")
                sql_type += "(" + precision + "," + scale + ")"
            if prop.attrib.get("StoreGeneratedPattern") == "Identity":
                sql_type += " IDENTITY"
            nullable = prop.attrib.get("Nullable", "true") == "true"
            flags = []
            if keys[name] == col:
                flags.append("PK")
            if (name, col) in foreign:
                flags.append("FK")
            description = descriptions.get((name, col))
            if not description:
                raise ValueError("Falta MS_Description para " + name + "." + col)
            fields.append([col, sql_type, "Sí" if nullable else "No", ", ".join(flags) or "—", description])
        if not descriptions_by_table[name]:
            raise ValueError("Falta MS_Description de tabla para " + name)
        tables.append((name, descriptions_by_table[name], fields))
    if len(tables) != 17 or sum(len(item[2]) for item in tables) != 114:
        raise ValueError("El diccionario cambió: se esperaban 17 tablas y 114 atributos.")
    return tables


TABLES = schema_dictionary()
for table_name, purpose, fields in TABLES:
    H3(table_name)
    P(purpose)
    TBL(["Campo", "Tipo SQL", "NULL", "Clave", "Descripción"], fields, [74, 79, 31, 45, 343], database=True)

H2("8.2 Relaciones y claves foráneas")
P("Una FK conserva la integridad al impedir que una fila apunte a otra que no existe. En la tabla de relaciones, la columna dependiente almacena la clave de la fila referenciada. Los registros históricos de asignaciones y pagos se conservan con bajas o cambios de estado.")


def schema_relations():
    root = ET.parse(EDMX).getroot()
    ns = {"edm": "http://schemas.microsoft.com/ado/2009/11/edm"}
    schema = root.find(".//edm:Schema", ns)
    relations = []
    for association in schema.findall("edm:Association", ns):
        principal = association.find("edm:ReferentialConstraint/edm:Principal", ns)
        dependent = association.find("edm:ReferentialConstraint/edm:Dependent", ns)
        ends = association.findall("edm:End", ns)
        if principal is None or dependent is None or len(ends) != 2:
            continue
        role_types = {end.attrib["Role"]: end.attrib["Type"].split(".")[-1] for end in ends}
        ptable = role_types[principal.attrib["Role"]]
        dtable = role_types[dependent.attrib["Role"]]
        pcol = principal.find("edm:PropertyRef", ns).attrib["Name"]
        dcol = dependent.find("edm:PropertyRef", ns).attrib["Name"]
        relations.append([dtable + "." + dcol, ptable + "." + pcol, "FK; el lado dependiente guarda la referencia"])
    return sorted(relations)


TBL(["Columna que referencia", "Fila referenciada", "Lectura"], schema_relations(), database=True)
P("Relaciones relevantes: Rol 1:N UsuarioSistema; Socio 1:N Membresia; Plan 1:N Membresia; Membresia 1:N CuotaMembresia; Membresia 1:N MembresiaEntrenador; Rutina 1:N RutinaEjercicio; Ejercicio 1:N RutinaEjercicio e imágenes. UsuarioSistema también tiene una autorrelación de creador. Pago y CuotaMembresia conservan el pago vigente y el historial por cuota.")
P("AuditoriaOperacion.IdEntidad no se une con una FK porque puede identificar entidades de distintos tipos. La columna Entidad indica cómo interpretar ese identificador.")
H2("8.3 JOIN: reunir los datos relacionados")
P("INNER JOIN devuelve únicamente filas que tienen coincidencia en ambos lados. Se usa cuando el resultado requiere una relación existente.")
CODE("""
SELECT s.DNI, s.Apellido + N', ' + s.Nombre AS Socio,
       m.IdMembresia, p.Nombre AS Plan
FROM dbo.Socio AS s
INNER JOIN dbo.Membresia AS m ON m.IdSocio = s.IdSocio
INNER JOIN dbo.Plan AS p ON p.IdPlan = m.IdPlan
WHERE s.Estado = 1 AND m.Estado = 1
ORDER BY s.Apellido, s.Nombre;
""")
P("Cada igualdad compara la FK de Membresia con la PK de Socio o Plan. Si un socio no tiene membresía, INNER JOIN no lo incluye.")
P("LEFT JOIN devuelve todas las filas de la tabla a la izquierda; si no existe pareja, las columnas del lado derecho quedan NULL. Sirve para encontrar socios sin asignación.")
CODE("""
SELECT m.IdMembresia, s.DNI,
       s.Apellido + N', ' + s.Nombre AS Socio,
       e.Nombre AS NombreEntrenador
FROM dbo.Membresia AS m
INNER JOIN dbo.Socio AS s ON s.IdSocio = m.IdSocio
LEFT JOIN dbo.MembresiaEntrenador AS me
       ON me.IdMembresia = m.IdMembresia AND me.Estado = 1
LEFT JOIN dbo.UsuarioSistema AS e
       ON e.IdUsuarioSistema = me.IdEntrenador
WHERE m.Estado = 1
ORDER BY s.Apellido, s.Nombre;
""")
P("La condición me.Estado = 1 está en ON para mantener también las membresías que no tienen un entrenador vigente. Si se filtrara por me.Estado en WHERE, esas filas podrían desaparecer como en una unión interna.")
H2("8.4 Índices: columnas y propósito")
P("Un índice mantiene una estructura ordenada para encontrar filas con menos lecturas. SQL Server crea un índice para cada PK y restricción UNIQUE. Una FK por sí sola no crea un índice automáticamente. Los índices adicionales de SysGym se justifican por consultas usadas por la aplicación.")

INDEX_ROWS = []
for table_name, purpose, fields in TABLES:
    pk = next((field[0] for field in fields if "PK" in field[3]), None)
    if pk:
        INDEX_ROWS.append([table_name, "PK (nombre SQL autogenerado)", pk, "Único, clustered", "Identifica una fila y resuelve búsquedas por clave."])
INDEX_ROWS.extend([
    ["Rol", "UNIQUE (nombre SQL autogenerado)", "Descripcion", "Único, nonclustered", "Evita roles con la misma descripción."],
    ["Socio", "UNIQUE (nombre SQL autogenerado)", "DNI", "Único, nonclustered", "Evita registrar dos personas con el mismo DNI."],
    ["UsuarioSistema", "UNIQUE (nombre SQL autogenerado)", "DNI", "Único, nonclustered", "Evita duplicar la identificación de un empleado."],
    ["UsuarioSistema", "UNIQUE (nombre SQL autogenerado)", "Username", "Único, nonclustered", "Hace inequívoca la cuenta de acceso."],
    ["EjercicioImagen", "UQ_EjercicioImagen_Ruta", "RutaRelativa", "Único, nonclustered", "Evita asociar dos registros a la misma ruta de archivo."],
    ["CuotaMembresia", "UQ_CuotaMembresia_Periodo", "IdMembresia, FechaDesde", "Único, nonclustered", "Evita dos cuotas que comiencen en el mismo período de una membresía."],
    ["CuotaMembresia", "UX_CuotaMembresia_IdRegistroPago", "IdRegistroPago", "Único, nonclustered; filtro IS NOT NULL", "Un pago vigente puede determinar como máximo una cuota."],
    ["AuditoriaOperacion", "IX_AuditoriaOperacion_FechaHora", "FechaHora DESC, IdAuditoria DESC", "Nonclustered", "Acelera la auditoría reciente y ordena empates."],
    ["Membresia", "IX_Membresia_IdSocio", "IdSocio", "Nonclustered", "Acelera la búsqueda del historial por socio."],
    ["Pago", "IX_Pago_IdCuotaMembresia", "IdCuotaMembresia", "Nonclustered; filtro IS NOT NULL", "Acelera el historial de intentos por cuota."],
])
INDEX_ROWS.sort(key=lambda row: (row[0], row[1]))
TBL(["Tabla", "Índice / restricción", "Columnas", "Características", "Propósito"], INDEX_ROWS, [72, 143, 111, 102, 144], database=True)
P("La base actual tiene 27 índices: 17 de PK, 7 únicos y 3 secundarios no únicos. Los nombres que SQL Server asigna automáticamente a PK o UNIQUE pueden variar entre instalaciones; los cuatro índices de consulta y los nombres UQ explícitos son estables.")
H2("8.5 DDL: crear tablas, claves, restricciones e índices")
P("DDL (Data Definition Language) define la estructura. SysGymDB.sql crea la base, tablas, claves primarias y foráneas, restricciones CHECK/UNIQUE, valores por defecto e índices. El instalador ejecuta DocumentarSysGymDB.sql después para guardar descripciones MS_Description.")
CODE("""
CREATE TABLE Socio (
    IdSocio INT IDENTITY(1,1) PRIMARY KEY,
    DNI NVARCHAR(20) NOT NULL UNIQUE,
    Nombre NVARCHAR(100) NOT NULL,
    Apellido NVARCHAR(100) NOT NULL,
    FechaNacimiento DATETIME2 NULL,
    FechaAlta DATETIME2 NULL,
    Peso DECIMAL(6,2) NULL,
    Altura DECIMAL(5,2) NULL,
    FotoRuta NVARCHAR(260) NULL,
    Sexo CHAR(1) NULL,
    Estado BIT NOT NULL DEFAULT 1,
    CONSTRAINT CK_Socio_Peso
        CHECK (Peso IS NULL OR Peso > 0),
    CONSTRAINT CK_Socio_Altura
        CHECK (Altura IS NULL OR Altura > 0)
);
""")
P("El ejemplo corresponde al DDL real de SysGymDB.sql y muestra identidad, unicidad, nulabilidad, tipos decimales y validaciones. El modelo completo está documentado en el diccionario y el EDMX.")
H2("8.6 DML: consultar y modificar filas")
P("DML (Data Manipulation Language) lee y modifica los datos. En la aplicación, Entity Framework traduce las operaciones del repositorio a SQL. SysGymDatos.sql es una carga DML de desarrollo y QA, no una tarea de producción.")
CODE("""
-- Ejemplo de INSERT presente en el seed de desarrollo:
INSERT INTO Ejercicio (Nombre, Descripcion, Estado)
VALUES (N'Press de banca',
        N'Pecho, triceps y deltoides anterior.', 1);

-- Lectura con INNER JOIN sobre claves reales:
SELECT s.DNI, s.Nombre, s.Apellido, p.Nombre AS Plan
FROM dbo.Socio AS s
INNER JOIN dbo.Membresia AS m ON m.IdSocio = s.IdSocio
INNER JOIN dbo.Plan AS p ON p.IdPlan = m.IdPlan;
""")
P("SysGymDatos.sql se protege para no cargar el mismo conjunto dos veces, activa XACT_ABORT, abre una transacción, inserta usuarios de prueba, métodos, ejercicios, rutinas, socios, membresías, cuotas y pagos, y confirma al final. Si falla el lote, SQL Server revierte la transacción. La seed contiene contraseñas almacenadas como hashes; el manual no las reproduce.")
H2("8.7 Transacciones: BEGIN, COMMIT y ROLLBACK")
P("Una transacción agrupa operaciones relacionadas para conservar la regla de todo o nada. BEGIN TRANSACTION inicia el grupo; COMMIT confirma sus cambios; ROLLBACK deshace los cambios no confirmados.")
CODE("""
DECLARE @MensajeError NVARCHAR(4000);
BEGIN TRY
    BEGIN TRANSACTION;
        -- Operaciones relacionadas: por ejemplo, membresía y cuota inicial.
    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    SET @MensajeError = ERROR_MESSAGE();
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    RAISERROR(@MensajeError, 16, 1);
END CATCH;
""")
P("En SysGym, la transacción de negocio se implementa en C# con Entity Framework 6, no en un procedimiento almacenado. UnidadDeTrabajoGimnasio.IniciarTransaccion crea la transacción y el objeto Transaccion confirma con Commit. Si se libera sin Confirmar, Dispose llama Rollback.")
CODE("""
public ITransaccion IniciarTransaccion()
{
    return new Transaccion(contexto.Database.BeginTransaction());
}

public void Confirmar()
{
    transaccion.Commit();
    confirmada = true;
}

public void Dispose()
{
    if (!confirmada)
        transaccion.Rollback();
    transaccion.Dispose();
}
""")
H2("8.8 Scripts SQL incluidos")
TBL(
    ["Script", "Propósito"],
    [
        ["SysGymDB.sql", "Esquema principal de instalación: tablas, relaciones, restricciones, índices y configuración inicial."],
        ["DocumentarSysGymDB.sql", "Aplica o actualiza MS_Description de tablas, 114 columnas e índices; no cambia filas ni estructura."],
        ["SysGymBaseInstalacion.sql", "Inserta catálogos y rutinas modelo que requiere una base recién instalada, sin crear cuentas ni socios."],
        ["SysGymDatos.sql", "Seed de desarrollo y QA con usuarios y datos relacionados; transaccional y protegida contra recarga."],
        ["AgregarAuditoriaOperaciones.sql", "Migración aditiva para el registro de auditoría."],
        ["AgregarConfiguracionSistema.sql", "Migración aditiva de la configuración global y sus restricciones."],
        ["AgregarCuotaEnPago.sql", "Agrega/vincula IdCuotaMembresia, conserva relaciones históricas y crea el índice filtrado de consulta."],
        ["AgregarObjetivoNivelRutina.sql", "Agrega objetivo y nivel de rutina y sus restricciones de combinación."],
        ["AgregarSocioFechaAlta.sql", "Agrega la fecha de alta histórica opcional de los socios."],
        ["MigrarEjercicioImagen.sql", "Migra los datos y estructura asociados al catálogo de imágenes de ejercicios."],
    ],
)
P("Las migraciones resuelven versiones anteriores de forma condicional y aditiva. Para una base nueva se sigue la secuencia de instalación de README.md; no se deben ejecutar migraciones a ciegas sobre una base productiva.")
H2("8.9 Procedimientos almacenados")
P("La base SysGymDB actual no contiene procedimientos almacenados propios: sys.procedures no devolvió filas. La lógica de negocio y las transacciones están en capaLogica y capaDatos mediante EF6.")
P("Algunas migraciones ejecutan sys.sp_executesql, procedimiento integrado de SQL Server que compila una instrucción SQL dinámica dentro del lote. DocumentarSysGymDB.sql usa los procedimientos integrados sys.sp_addextendedproperty y sys.sp_updateextendedproperty para añadir o actualizar descripciones. Ninguno es un procedimiento de negocio creado por SysGym.")
NOTE("COMMIT/ROLLBACK", "COMMIT y ROLLBACK son instrucciones de control transaccional de SQL Server y métodos de DbContextTransaction en EF6; no son procedimientos almacenados de SysGym.")
H2("8.10 Integridad y decisiones del modelo")
UL(
    "Los identificadores IDENTITY son claves técnicas autogeneradas y no se ingresan desde las pantallas.",
    "DNI, username, rol, ruta de imagen y período de cuota tienen restricciones de unicidad donde corresponde.",
    "Los importes históricos se conservan en CuotaMembresia.Importe aunque cambie Plan.Precio.",
    "FotoRuta y RutaRelativa apuntan a archivos en disco; las imágenes no se guardan como binarios en SQL.",
    "Los estados de una cuota y de un intento de pago tienen significados distintos.",
    "Pago.IdCuotaMembresia y CuotaMembresia.IdRegistroPago conservan dos direcciones de la relación: historial del intento y pago vigente de la cuota.",
    "AuditoriaOperacion guarda un nombre de entidad y un IdEntidad polimórfico; no hay una FK hacia cada tabla de negocio.",
    "ConfiguracionSistema restringe su única fila a IdConfiguracion = 1 y acota valores con CHECK.",
)
H2("8.11 Abrir el EDMX")
STEPS(
    "Abrí la solución exxen2.0 en Visual Studio.",
    "En el Explorador de soluciones, expandí capaDatos y Database.",
    "Abrí SysGymDB.edmx. Visual Studio muestra las entidades y las líneas de asociación incluidas en el diagrama.",
    "Seleccioná una entidad y abrí su superficie de diseño o su XML para revisar propiedades y relaciones.",
)
P("SysGymDB.edmx se distribuye como archivo de documentación y no genera una segunda clase de contexto durante la compilación. El contexto ejecutable existente sigue siendo ContextoGimnasio con EF6.")

H1("9. Glosario")
TBL(
    ["Término", "Significado"],
    [
        ["Socio", "Persona registrada en el gimnasio."],
        ["Membresía", "Relación histórica de un socio con un plan."],
        ["Cuota", "Importe que corresponde a un período de una membresía."],
        ["Pago", "Intento registrado de cobro; conserva estados como aprobado, anulado o reembolsado."],
        ["Deuda", "Cuotas vencidas que permanecen pendientes."],
        ["Rutina modelo", "Plan de ejercicios reutilizable con objetivo y nivel."],
        ["Auditoría", "Historial de operaciones que registra usuario, hora, entidad y detalle."],
        ["PK", "Clave primaria: identifica una fila."],
        ["FK", "Clave foránea: referencia una clave de otra tabla."],
        ["DDL", "Instrucciones que definen la estructura de una base."],
        ["DML", "Instrucciones que consultan o cambian las filas."],
        ["Seed", "Datos de ejemplo para desarrollo o QA."],
        ["Índice", "Estructura auxiliar para localizar u ordenar filas, o imponer unicidad."],
        ["Transacción", "Grupo de cambios que se confirma completo o se revierte."],
        ["Copia de seguridad", "Archivo .bak utilizado para recuperar la base."],
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
            canv.drawString(17*mm, A4[1]-10.5*mm, "SYSGYM  /  MANUAL DE USUARIO Y BASE DE DATOS")
            canv.setStrokeColor(colors.HexColor("#" + MID))
            canv.line(17*mm, 13*mm, A4[0]-17*mm, 13*mm)
            canv.setFont("SysGym", 7)
            canv.setFillColor(colors.HexColor("#" + MUTED))
            canv.drawString(17*mm, 8.5*mm, "Versión 30/10/2026 · Datos seed identificados como ejemplos")
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
                      Paragraph("Procesos de trabajo, reportes y documentación técnica de la base de datos", styles["CoverLine"]),
                      Spacer(1, 20*mm), Paragraph("Octubre de 2026", styles["CoverLine"]), PageBreak(),
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
            canv.drawString(17*mm, A4[1]-10.5*mm, "SYSGYM  /  MANUAL DE USUARIO Y BASE DE DATOS")
            canv.line(17*mm, 13*mm, A4[0]-17*mm, 13*mm)
            canv.setFont("SysGym", 7)
            canv.setFillColor(colors.HexColor("#" + MUTED))
            canv.drawString(17*mm, 8.5*mm, "Versión 30/10/2026 · Datos seed identificados como ejemplos")
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
    word_paragraph(body, "Procesos de trabajo, reportes y documentación técnica de la base de datos", color=INK, size=15, after=180, align="center")
    word_paragraph(body, "Octubre de 2026", color=INK, size=15, before=800, after=150, align="center")
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
    word_paragraph(header, "SysGym  /  Manual de usuario y base de datos", color=INK, bold=True, size=13, after=0)
    header_first = ET.Element(wtag("hdr"))
    settings = ET.Element(wtag("settings"))
    ET.SubElement(settings, wtag("updateFields"), {wtag("val"): "true"})
    footer = ET.Element(wtag("ftr"))
    footer_p = ET.SubElement(footer, wtag("p"))
    footer_ppr = ET.SubElement(footer_p, wtag("pPr"))
    ET.SubElement(footer_ppr, wtag("jc"), {wtag("val"): "right"})
    word_run(footer_p, "SysGym · 30/10/2026 · Página ", color=MUTED, size=13)
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
    print("Entidades:", len(TABLES), "Atributos:", sum(len(item[2]) for item in TABLES))
    print("Capturas:", sum(1 for block in BLOCKS if block[0] == "fig"))
