$ErrorActionPreference = 'Stop'
& csc /nologo /target:exe /out:.verification/Checks.exe /r:bin/Debug/exxen2.0.exe /r:bin/Debug/EntityFramework.dll /r:System.Windows.Forms.dll /r:System.Drawing.dll /r:System.Data.dll .verification/Checks.cs
if ($LASTEXITCODE -ne 0) { throw 'Falló la compilación de las pruebas.' }
Get-ChildItem bin/Debug -File | Where-Object { $_.Extension -in '.dll', '.exe' } | Copy-Item -Destination .verification
