$ErrorActionPreference = 'Stop'

function Invoke-VsRetry([scriptblock]$Action) {
    $deadline = (Get-Date).AddSeconds(90)
    while ($true) {
        try { return & $Action }
        catch [Runtime.InteropServices.COMException] {
            if ((Get-Date) -ge $deadline) { throw }
            Start-Sleep -Milliseconds 750
        }
    }
}

$archivos = @(
    'capaVisual\Autenticacion\Login.cs',
    'capaVisual\Administrador\DashboardAdministrador.cs',
    'capaVisual\Recepcionista\DashboardRecepcionista.cs',
    'capaVisual\Entrenador\DashboardEntrenador.cs',
    'capaVisual\Administrador\ConsultaRutinasAdministradorForm.cs',
    'capaVisual\Administrador\GestionUsuariosForm.cs',
    'capaVisual\Administrador\GestionPlanesForm.cs',
    'capaVisual\Administrador\ReportesForm.cs',
    'capaVisual\Recepcionista\GestionMembresiasForm.cs',
    'capaVisual\Recepcionista\GestionPagosForm.cs',
    'capaVisual\Recepcionista\GestionAsignacionesForm.cs',
    'capaVisual\Compartido\GestionSociosForm.cs',
    'capaVisual\Compartido\GestionEjerciciosForm.cs',
    'capaVisual\Compartido\GestionAsistenciasForm.cs',
    'capaVisual\Entrenador\RutinasEntrenadorForm.cs',
    'capaVisual\Entrenador\MisSociosForm.cs',
    'capaVisual\Administrador\DashboardInicioAdministrador.cs'
)

$dte = $null
try {
    Add-Type -AssemblyName System.Drawing
    Add-Type -AssemblyName System.Windows.Forms
    $capturas = Join-Path (Get-Location) ('.designer-shots-' + (Get-Date -Format 'yyyyMMdd-HHmmss'))
    [void](New-Item -ItemType Directory -Path $capturas)
    $dte = New-Object -ComObject VisualStudio.DTE.18.0
    Start-Sleep -Seconds 10
    Invoke-VsRetry { $dte.SuppressUI = $true }
    Invoke-VsRetry { $dte.UserControl = $false }
    Invoke-VsRetry { $dte.MainWindow.Visible = $true }
    Invoke-VsRetry { $dte.MainWindow.WindowState = 1 }
    $solucion = (Resolve-Path 'exxen2.0.slnx').Path
    Invoke-VsRetry { $dte.Solution.Open($solucion) }
    Start-Sleep -Seconds 10
    $designer = '{7651a700-06e5-11d1-8ebd-00a0c90f26ea}'

    $indice = 0
    foreach ($archivo in $archivos) {
        $indice++
        $ruta = (Resolve-Path $archivo).Path
        $item = Invoke-VsRetry { $dte.Solution.FindProjectItem($ruta) }
        if ($null -eq $item) { throw "No se encontro el elemento: $archivo" }
        $ventana = Invoke-VsRetry { $item.Open($designer) }
        Invoke-VsRetry { $ventana.Activate() }
        Invoke-VsRetry { $dte.ExecuteCommand('View.ViewDesigner') }
        Start-Sleep -Seconds 2
        $caption = Invoke-VsRetry { $dte.ActiveWindow.Caption }
        $kind = Invoke-VsRetry { $dte.ActiveWindow.Kind }
        $pantalla = [System.Windows.Forms.Screen]::PrimaryScreen.Bounds
        $bitmap = New-Object System.Drawing.Bitmap $pantalla.Width, $pantalla.Height
        $grafico = [System.Drawing.Graphics]::FromImage($bitmap)
        $grafico.CopyFromScreen($pantalla.Location, [System.Drawing.Point]::Empty, $pantalla.Size)
        $nombreCaptura = ('{0:D2}-{1}.png' -f $indice, ([IO.Path]::GetFileNameWithoutExtension($archivo)))
        $rutaCaptura = Join-Path $capturas $nombreCaptura
        $bitmap.Save($rutaCaptura, [System.Drawing.Imaging.ImageFormat]::Png)
        $grafico.Dispose()
        $bitmap.Dispose()
        $seleccionar = Invoke-VsRetry { $dte.Commands.Item('Edit.SelectAll').IsAvailable }
        if ($seleccionar) { Invoke-VsRetry { $dte.ExecuteCommand('Edit.SelectAll') } }
        $seleccionEditable = Invoke-VsRetry { $dte.Commands.Item('Edit.Cut').IsAvailable }
        $alineacionDisponible = Invoke-VsRetry { $dte.Commands.Item('Format.AlignLefts').IsAvailable }
        Write-Output ("OK|{0}|Caption={1}|Kind={2}|Seleccion={3}|Layout={4}|Captura={5}" -f $archivo, $caption, $kind, $seleccionEditable, $alineacionDisponible, $rutaCaptura)
        Invoke-VsRetry { $ventana.Close(2) }
    }

    Write-Output ("TOTAL_OK={0}" -f $archivos.Count)
    Write-Output ("CAPTURAS={0}" -f $capturas)
}
catch {
    Write-Output 'DESIGNER_CHECK_FAILED'
    Write-Output $_.Exception.ToString()
    if ($_.Exception.InnerException) { Write-Output $_.Exception.InnerException.ToString() }
    exit 1
}
finally {
    if ($null -ne $dte) {
        try { Invoke-VsRetry { $dte.Quit() } } catch {}
        try { [void][Runtime.InteropServices.Marshal]::FinalReleaseComObject($dte) } catch {}
    }
}
