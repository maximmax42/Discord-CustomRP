$verRaw = (Get-Item ..\bin\Release\CustomRP.exe).VersionInfo
$ver = "$($verRaw.FileMajorPart).$($verRaw.FileMinorPart)"
if ($verRaw.FileBuildPart -ne 0 -or $verRaw.FilePrivatePart -ne 0) { $ver += ".$($verRaw.FileBuildPart)" }
if ($verRaw.FilePrivatePart -ne 0) { $ver += ".$($verRaw.FilePrivatePart)" }

xcopy ..\bin\Release CustomRP /e /i /s /y /exclude:exclude.txt
xcopy License.txt CustomRP
xcopy "Privacy Policy.txt" CustomRP
C:\Program` Files` `(x86`)\Inno` Setup` 6\ISCC.exe /DMyAppVersion=$ver Installer.iss

echo "start CustomRP.exe --second-instance" > "CustomRP\Start Second Instance.bat"
Compress-Archive -Path CustomRP,"Windows 7",README.txt -DestinationPath "Artifacts\CustomRP $ver.zip" -CompressionLevel Optimal