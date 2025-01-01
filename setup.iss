[Setup]
AppName=PasswordLab
AppVersion=1.1.0
DefaultDirName={pf}\PasswordLab
DefaultGroupName=PasswordLab
OutputBaseFilename=PasswordLabInstaller
Compression=lzma
SolidCompression=yes

[Files]
Source: "bin\Release\net8.0\publish\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{group}\PasswordLab"; Filename: "{app}\PasswordLab.exe"