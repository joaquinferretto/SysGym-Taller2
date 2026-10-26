#define AppName "SysGym"
#define AppVersion "1.0.0"
#define AppExeName "SysGym.exe"

[Setup]
AppId={{E7970B53-12D3-4DCA-9F44-4638A84F2824}
AppName={#AppName}
AppVersion={#AppVersion}
AppPublisher=SysGym
DefaultDirName={autopf}\SysGym
DefaultGroupName=SysGym
DisableProgramGroupPage=yes
UninstallDisplayIcon={app}\{#AppExeName}
SetupIconFile=..\Resources\SysGym.ico
OutputDir=Salida
OutputBaseFilename=SysGym-Setup
Compression=lzma2/ultra64
SolidCompression=yes
WizardStyle=modern
PrivilegesRequired=admin
ArchitecturesAllowed=x64compatible
ArchitecturesInstallIn64BitMode=x64compatible
Uninstallable=yes
CloseApplications=yes
RestartApplications=no

[Languages]
Name: "spanish"; MessagesFile: "compiler:Languages\Spanish.isl"

[Tasks]
Name: "desktopicon"; Description: "Crear un acceso directo en el escritorio"; GroupDescription: "Accesos directos:"

[Dirs]
Name: "{commonappdata}\SysGym"; Permissions: users-modify; Flags: uninsneveruninstall
Name: "{commonappdata}\SysGym\Imagenes"; Permissions: users-modify; Flags: uninsneveruninstall
Name: "{commonappdata}\SysGym\Imagenes\Ejercicios"; Permissions: users-modify; Flags: uninsneveruninstall
Name: "{commonappdata}\SysGym\Imagenes\Socios"; Permissions: users-modify; Flags: uninsneveruninstall
Name: "{commonappdata}\SysGym\Datos"; Permissions: users-modify; Flags: uninsneveruninstall

[Files]
Source: "..\bin\Release\exxen2.0.exe"; DestDir: "{app}"; DestName: "{#AppExeName}"; Flags: ignoreversion
Source: "..\bin\Release\*.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Release\runtimes\*"; DestDir: "{app}\runtimes"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "SysGym.exe.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\Resources\SysGym.ico"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\capaDatos\Database\SysGymDB.sql"; DestDir: "{app}\capaDatos\Database"; Flags: ignoreversion
Source: "..\capaDatos\Database\SysGymBaseInstalacion.sql"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\capaDatos\Imagenes\Ejercicios\*"; DestDir: "{commonappdata}\SysGym\Imagenes\Ejercicios"; Flags: ignoreversion recursesubdirs createallsubdirs uninsneveruninstall
Source: "..\Manual\Manual de usuario SysGym.pdf"; DestDir: "{app}"; Flags: ignoreversion skipifsourcedoesntexist
Source: "Redist\vc_redist.x64.exe"; DestDir: "{tmp}"; Flags: dontcopy
Source: "Redist\SqlLocalDB-2022-x64.msi"; DestDir: "{tmp}"; Flags: dontcopy

[Icons]
Name: "{group}\SysGym"; Filename: "{app}\{#AppExeName}"; IconFilename: "{app}\SysGym.ico"
Name: "{autodesktop}\SysGym"; Filename: "{app}\{#AppExeName}"; IconFilename: "{app}\SysGym.ico"; Tasks: desktopicon

[Run]
Filename: "{app}\{#AppExeName}"; Description: "Iniciar SysGym"; Flags: postinstall nowait skipifsilent runasoriginaluser

[Messages]
ConfirmUninstall=¿Desea desinstalar SysGym? Los datos, las imágenes y las copias de seguridad se conservarán en %ProgramData%\SysGym.

[Code]
function DotNet48Installed: Boolean;
var
  ReleaseVersion: Cardinal;
begin
  Result := (RegQueryDWordValue(HKLM64, 'SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full', 'Release', ReleaseVersion) and (ReleaseVersion >= 528040)) or
    (RegQueryDWordValue(HKLM32, 'SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full', 'Release', ReleaseVersion) and (ReleaseVersion >= 528040));
end;

function VisualCppX64Installed: Boolean;
var
  Installed: Cardinal;
begin
  Result := RegQueryDWordValue(HKLM64, 'SOFTWARE\Microsoft\VisualStudio\14.0\VC\Runtimes\x64', 'Installed', Installed) and (Installed = 1);
end;

function LocalDbInstalled: Boolean;
begin
  Result := RegKeyExists(HKLM64, 'SOFTWARE\Microsoft\Microsoft SQL Server Local DB\Installed Versions\15.0') or
    RegKeyExists(HKLM64, 'SOFTWARE\Microsoft\Microsoft SQL Server Local DB\Installed Versions\16.0') or
    RegKeyExists(HKLM64, 'SOFTWARE\Microsoft\Microsoft SQL Server Local DB\Installed Versions\17.0');
end;

function RunPrerequisite(const FileName, Parameters: String; var NeedsRestart: Boolean; var ExitCode: Integer): Boolean;
begin
  Result := Exec(FileName, Parameters, '', SW_HIDE, ewWaitUntilTerminated, ExitCode);
  if not Result then
    Exit;
  if ExitCode = 3010 then
  begin
    NeedsRestart := True;
    ExitCode := 0;
  end;
  Result := ExitCode = 0;
end;

function PrepareToInstall(var NeedsRestart: Boolean): String;
var
  ExitCode: Integer;
  VcInstaller: String;
  LocalDbInstaller: String;
begin
  Result := '';
  VcInstaller := ExpandConstant('{tmp}\vc_redist.x64.exe');
  LocalDbInstaller := ExpandConstant('{tmp}\SqlLocalDB-2022-x64.msi');

  if not DotNet48Installed then
  begin
    MsgBox('SysGym requiere .NET Framework 4.8 y no se detecto una version compatible.' + #13#10 + #13#10 +
      'Descarguelo desde la pagina oficial de Microsoft:' + #13#10 +
      'https://dotnet.microsoft.com/en-us/download/dotnet-framework/net48', mbError, MB_OK);
    Result := '.NET Framework 4.8 es necesario. Descarguelo desde https://dotnet.microsoft.com/en-us/download/dotnet-framework/net48 y vuelva a iniciar la instalacion.';
    Exit;
  end;

  if not VisualCppX64Installed then
  begin
    ExtractTemporaryFile('vc_redist.x64.exe');
    if not RunPrerequisite(VcInstaller, '/install /quiet /norestart', NeedsRestart, ExitCode) then
    begin
      Result := 'El runtime de Visual C++ x64 no se pudo instalar (código ' + IntToStr(ExitCode) + ').';
      Exit;
    end;
  end;

  if not LocalDbInstalled then
  begin
    ExtractTemporaryFile('SqlLocalDB-2022-x64.msi');
    if not RunPrerequisite(ExpandConstant('{sys}\msiexec.exe'), '/i "' + LocalDbInstaller + '" /qn IACCEPTSQLLOCALDBLICENSETERMS=YES', NeedsRestart, ExitCode) then
    begin
      Result := 'SQL Server Express LocalDB 2022 no se pudo instalar (código ' + IntToStr(ExitCode) + ').';
      Exit;
    end;
  end;
end;

procedure CurUninstallStepChanged(CurUninstallStep: TUninstallStep);
begin
  if CurUninstallStep = usPostUninstall then
    MsgBox('SysGym se desinstaló. La base, las imágenes y las copias de seguridad se conservaron en %ProgramData%\SysGym.', mbInformation, MB_OK);
end;
