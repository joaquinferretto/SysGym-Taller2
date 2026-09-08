$ErrorActionPreference = 'Stop'
& csc /nologo /target:exe /out:.verification/DesignerCheck.exe /r:'C:/Program Files/Microsoft Visual Studio/18/Community/Common7/IDE/PublicAssemblies/envdte.dll' /r:'C:/Program Files/Microsoft Visual Studio/18/Community/Common7/IDE/PublicAssemblies/Microsoft.VisualStudio.Interop.dll' /r:System.Drawing.dll .verification/DesignerCheck.cs
if ($LASTEXITCODE -ne 0) { throw 'Falló la compilación del verificador de diseño.' }
Copy-Item 'C:/Program Files/Microsoft Visual Studio/18/Community/Common7/IDE/PublicAssemblies/envdte.dll' .verification
Copy-Item 'C:/Program Files/Microsoft Visual Studio/18/Community/Common7/IDE/PublicAssemblies/Microsoft.VisualStudio.Interop.dll' .verification
