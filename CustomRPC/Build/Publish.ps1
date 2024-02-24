#$ver = Read-Host "Enter version"
$verRaw = (Get-Item ..\bin\Release\CustomRP.exe).VersionInfo
$ver = "$($verRaw.FileMajorPart).$($verRaw.FileMinorPart)"
if ($verRaw.FileBuildPart -ne 0) { $ver = "$ver.$($verRaw.FileBuildPart)" }
if ($verRaw.FilePrivatePart -ne 0) { $ver = "$ver.$($verRaw.FilePrivatePart)" }

xcopy ..\bin\Release CustomRP /e /i /s /y /exclude:exclude.txt
C:\Program` Files` `(x86`)\Inno` Setup` 6\ISCC.exe /DMyAppVersion=$ver Installer.iss

echo "start CustomRP.exe --second-instance" > "CustomRP\Start Second Instance.bat"
Compress-Archive -Path CustomRP,"Windows 7",README.txt -DestinationPath "Artifacts\CustomRP $ver.zip" -CompressionLevel Optimal
#C:\PROGRA~1\WinRAR\WinRAR.exe a -afzip -r "CustomRP $ver.zip" CustomRP "Windows 7" README.txt | Out-Null
Remove-Item CustomRP -Recurse