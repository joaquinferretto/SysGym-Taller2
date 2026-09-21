<#
Paleta de botones de SysGym. Fuente unica de colores y clasificacion por accion real.
Sin parametros: comprueba los Designer, sin escribir. -Aplicar: sincroniza solo estilo.
-Inventario: devuelve las asignaciones para inspeccion o verificaciones.
Los literales resultantes son editables y serializables por el Designer de WinForms.
No se ejecuta al compilar ni al iniciar la aplicacion. No clasifica por Text.
#>
[CmdletBinding()]
param([switch]$Aplicar, [switch]$Inventario)
$ErrorActionPreference = 'Stop'
Add-Type -AssemblyName System.Drawing
$raizPaleta = Split-Path $PSScriptRoot -Parent
$paleta = @{
    Alta = @('#09956F', '#FFFFFF')
    Confirmar = @('#4842D9', '#FFFFFF')
    Quitar = @('#FFF0F0', '#AD2424')
    Secundaria = @('#E7EDF7', '#30445F')
    Reactivar = @('#DCFCE7', '#166534')
    Navegar = @('#FFFFFF', '#4842D9')
}
# Guardar y Actualizar que validan/persisten la ficha son Confirmar, aunque
# el texto sea Modificar o Agregar. Editar que abre el editor es Secundaria.
$acciones = @{
    InicioSesion = @{ btnIngresar='Confirmar'; btnSalir='Quitar' }
    GestionSociosFormulario = @{ nuevo='Alta'; guardar='Confirmar'; actualizar='Confirmar'; calcularImc='Secundaria'; verRutina='Secundaria'; btnSeleccionarFoto='Alta'; btnQuitarFoto='Quitar'; btnVolver='Navegar' }
    RutinaSemanalFormulario = @{ btnVolver='Navegar' }
    GestionEjerciciosFormulario = @{ nuevo='Alta'; actualizar='Secundaria'; guardar='Confirmar'; agregarImagen='Alta'; quitarImagen='Quitar'; darDeBaja='Quitar'; reactivar='Reactivar' }
    GestionUsuariosFormulario = @{ nuevo='Alta'; guardar='Confirmar'; actualizar='Confirmar'; darDeBaja='Quitar'; reactivar='Reactivar'; btnSeleccionarFoto='Alta'; btnQuitarFoto='Quitar' }
    GestionPlanesFormulario = @{ nuevo='Alta'; guardar='Confirmar'; actualizar='Confirmar'; darDeBaja='Quitar'; reactivar='Reactivar' }
    GestionMembresiasFormulario = @{ nuevo='Alta'; crear='Alta'; actualizar='Confirmar'; habilitar='Reactivar'; deshabilitar='Quitar'; generarCuota='Alta'; btnVolver='Navegar' }
    GestionPagosFormulario = @{ nuevo='Alta'; registrar='Confirmar'; anular='Quitar'; reembolsar='Quitar'; btnVolver='Navegar' }
    GestionAsignacionesFormulario = @{ actualizar='Secundaria'; asignar='Confirmar'; cambiar='Confirmar'; darDeBaja='Quitar' }
    ConsultaEntrenadoresFormulario = @{ actualizar='Secundaria' }
    ConsultaRutinasAdministradorFormulario = @{ actualizar='Secundaria'; btnVolver='Navegar' }
    ReportesFormulario = @{ generar='Secundaria'; btnVolver='Navegar' }
    RutinasEntrenadorFormulario = @{ nuevaRutina='Alta'; guardarRutina='Confirmar'; actualizar='Confirmar'; darDeBaja='Quitar'; reactivar='Reactivar'; agregarEjercicio='Alta'; actualizarEjercicio='Secundaria'; quitarEjercicio='Quitar'; guardarEjercicio='Confirmar'; cancelarEjercicio='Secundaria'; btnVolver='Navegar' }
    MisSociosFormulario = @{ actualizar='Secundaria'; asignarRutina='Confirmar'; verRutina='Secundaria'; crearPersonalizada='Alta'; exportarPdf='Secundaria' }
    PanelAdministrador = @{ btnVolver='Navegar'; btnCambiarCuenta='Navegar'; btnSalir='Quitar' }
    PanelRecepcionista = @{ btnVolver='Navegar'; btnCambiarCuenta='Navegar'; btnSalir='Quitar' }
    PanelEntrenador = @{ btnVolver='Navegar'; btnCambiarCuenta='Navegar'; btnSalir='Quitar' }
}
# Accesos de navegacion: conservar el menu existente, no son altas/asignaciones.
$menus = @{
    PanelAdministrador = @('btnUsuarios','btnSocios','btnPlanes','btnMembresias','btnPagos','btnAsignaciones','btnEjercicios','btnRutinas','btnMisSocios','btnReportes')
    PanelRecepcionista = @('btnSocios','btnMembresias','btnPagos','btnAsignar','btnConsultar')
    PanelEntrenador = @('btnSocios','btnRutinas','btnEjercicios')
}
function Convertir-ColorLiteral([string]$hex) {
    $color = [System.Drawing.ColorTranslator]::FromHtml($hex)
    return ('System.Drawing.Color.FromArgb({0}, {1}, {2})' -f $color.R, $color.G, $color.B)
}
function Obtener-Argb([string]$expresion) {
    if($expresion -match 'Color\.FromArgb\(') {
        $numeros = @([regex]::Matches($expresion, '\d+') | ForEach-Object { [int]$_.Value })
        if($numeros.Count -eq 3) { return [System.Drawing.Color]::FromArgb($numeros[0],$numeros[1],$numeros[2]).ToArgb() }
        if($numeros.Count -eq 4) { return [System.Drawing.Color]::FromArgb($numeros[0],$numeros[1],$numeros[2],$numeros[3]).ToArgb() }
    }
    if($expresion -match '^(?:System\.Drawing\.)?Color\.(\w+)$') { return [System.Drawing.Color]::FromName($Matches[1]).ToArgb() }
    return $null
}
function Establecer-Propiedad([string]$texto, [string]$boton, [string]$propiedad, [string]$valor) {
    $patron = '(?<![\w.])(?<prefijo>(?:this\.)?' + [regex]::Escape($boton) + '\.' + [regex]::Escape($propiedad) + '\s*=\s*)(?<valor>[^;]+);'
    $coincidencias = [regex]::Matches($texto,$patron)
    if($coincidencias.Count -gt 1) { throw "Propiedad duplicada: $boton.$propiedad" }
    if($coincidencias.Count -eq 1) {
        $actual = $coincidencias[0].Groups['valor'].Value.Trim()
        if($propiedad -in @('BackColor','ForeColor')) {
            if((Obtener-Argb $actual) -eq (Obtener-Argb $valor)) { return $texto }
        } elseif($actual -eq $valor -or ($propiedad -eq 'FlatStyle' -and $actual -eq 'FlatStyle.Flat')) { return $texto }
        return [regex]::Replace($texto,$patron,[System.Text.RegularExpressions.MatchEvaluator]{ param($m) $m.Groups['prefijo'].Value + $valor + ';' })
    }
    $inicio = '(?<![\w.])(?<referencia>(?:this\.)?' + [regex]::Escape($boton) + ')\s*=\s*new\s+(?:System\.Windows\.Forms\.)?Button\s*\(\s*\);'
    if(-not [regex]::IsMatch($texto,$inicio)) { throw "No se encontro el boton $boton" }
    $salto = if($texto.Contains("`r`n")) { "`r`n" } else { "`n" }
    return [regex]::Replace($texto,$inicio,[System.Text.RegularExpressions.MatchEvaluator]{ param($m) $m.Value + $salto + '            ' + $m.Groups['referencia'].Value + '.' + $propiedad + ' = ' + $valor + ';' })
}
[xml]$proyecto = [IO.File]::ReadAllText((Join-Path $raizPaleta 'exxen2.0.csproj'))
$pendientes = @()
$totalBotones = 0
foreach($item in $proyecto.Project.ItemGroup.Compile) {
    if($item.Include -notlike 'capaVisual*Designer.cs') { continue }
    $ruta = Join-Path $raizPaleta $item.Include
    $formulario = [IO.Path]::GetFileName($ruta).Replace('.Designer.cs','')
    $original = [IO.File]::ReadAllText($ruta)
    $texto = $original
    $botones = @([regex]::Matches($texto,'(?:this\.)?(\w+)\s*=\s*new\s+(?:System\.Windows\.Forms\.)?Button\s*\(') | ForEach-Object { $_.Groups[1].Value })
    foreach($boton in $botones) {
        if($menus.ContainsKey($formulario) -and $boton -in $menus[$formulario]) { continue }
        if(-not $acciones.ContainsKey($formulario) -or -not $acciones[$formulario].ContainsKey($boton)) { throw "Boton activo sin clasificar: $formulario.$boton" }
        $accion = $acciones[$formulario][$boton]
        $colores = $paleta[$accion]
        $totalBotones++
        if($Inventario) { [pscustomobject]@{ Formulario=$formulario; Boton=$boton; Accion=$accion; Fondo=$colores[0]; Texto=$colores[1] }; continue }
        $texto = Establecer-Propiedad $texto $boton 'BackColor' (Convertir-ColorLiteral $colores[0])
        $texto = Establecer-Propiedad $texto $boton 'ForeColor' (Convertir-ColorLiteral $colores[1])
        $texto = Establecer-Propiedad $texto $boton 'UseVisualStyleBackColor' 'false'
        $texto = Establecer-Propiedad $texto $boton 'FlatStyle' 'System.Windows.Forms.FlatStyle.Flat'
        $texto = Establecer-Propiedad $texto $boton 'FlatAppearance.BorderSize' '0'
    }
    if($acciones.ContainsKey($formulario)) { foreach($boton in $acciones[$formulario].Keys) { if($boton -notin $botones) { throw "Asignacion obsoleta: $formulario.$boton" } } }
    if($texto -ne $original) { $pendientes += [pscustomobject]@{ Ruta=$ruta; Texto=$texto } }
}
if($Inventario) { return }
# Validar todo antes de escribir. Mantener codificacion y saltos existentes.
foreach($pendiente in $pendientes) {
    if($Aplicar) {
        $bytes = [IO.File]::ReadAllBytes($pendiente.Ruta)
        $bom = $bytes.Length -ge 3 -and $bytes[0] -eq 239 -and $bytes[1] -eq 187 -and $bytes[2] -eq 191
        [IO.File]::WriteAllText($pendiente.Ruta,$pendiente.Texto,[Text.UTF8Encoding]::new($bom))
    } else { Write-Output ('Diferencia de paleta: ' + $pendiente.Ruta) }
}
if(-not $Aplicar -and $pendientes.Count -gt 0) { throw 'Hay botones fuera de paleta. Revisar y ejecutar -Aplicar para sincronizar.' }
Write-Output ("Paleta: $totalBotones botones; archivos sincronizados: " + $pendientes.Count)