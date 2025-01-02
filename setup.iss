#define MyAppName "PasswordLab"
#define MyAppVersion "1.2.0"
#define MyAppExeName "PasswordLab.exe"

[Setup]
AppName={#MyAppName}
AppVersion={#MyAppVersion}
DefaultDirName={pf}\{#MyAppName}
DefaultGroupName={#MyAppName}
OutputBaseFilename={#MyAppName}Installer
Compression=lzma
SolidCompression=yes

[Files]
Source: "bin\Release\net8.0\publish\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"