param([switch]$Resume)
$ErrorActionPreference = 'Stop'
$script = Get-Content -Raw -Encoding UTF8 'capaDatos/Database/SysGymDB.sql'
$script = $script.Replace('SysGymDB', 'SysGym_Verificacion_20260908')
if ($Resume) {
    $script = "USE SysGym_Verificacion_20260908;`r`nGO`r`n" + $script.Substring($script.IndexOf('CREATE UNIQUE INDEX UX_CuotaMembresia_IdRegistroPago'))
}
$script | & sqlcmd -S '.\SQLEXPRESS' -E -I -b
if ($LASTEXITCODE -ne 0) { throw 'No se pudo crear la base de verificación.' }
