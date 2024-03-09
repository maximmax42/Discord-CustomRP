$verRaw = (Get-Item ..\bin\Release\CustomRP.exe).VersionInfo
$ver = "$($verRaw.FileMajorPart).$($verRaw.FileMinorPart)"
if ($verRaw.FileBuildPart -ne 0 -or $verRaw.FilePrivatePart -ne 0) { $ver += ".$($verRaw.FileBuildPart)" }
if ($verRaw.FilePrivatePart -ne 0) { $ver += ".$($verRaw.FilePrivatePart)" }

xcopy ..\bin\Release CustomRP /e /i /s /y /exclude:exclude.txt
xcopy License.txt CustomRP
xcopy "Privacy Policy.txt" CustomRP
git clone https://github.com/jrsoftware/issrc
C:\Program` Files` `(x86`)\Inno` Setup` 6\ISCC.exe /DMyAppVersion=$ver Installer.iss

echo "start CustomRP.exe --second-instance" > "CustomRP\Start Second Instance.bat"
Compress-Archive -Path CustomRP,"Windows 7",README.txt -DestinationPath "Artifacts\CustomRP $ver.zip" -CompressionLevel Optimal

$hashes = "CustomRP $ver.exe`n    $((Get-FileHash "Artifacts\CustomRP $ver.exe" -Algorithm SHA384).Hash)`nCustomRP $ver.zip`n    $((Get-FileHash "Artifacts\CustomRP $ver.zip" -Algorithm SHA384).Hash)"

"=== SHA256 Hashes ==="
$hashes
"====================="

$hashes > "Artifacts\CustomRP Hashes $ver.txt"