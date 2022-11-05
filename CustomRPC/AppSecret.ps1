if (-not(Test-Path -Path .appSecret -PathType Leaf)) {
	echo No .appSecret present, ignoring...
	return
}

$appSecret = Get-Content .appSecret
$execType = $args[0]

$programCs = Get-Content Program.cs -Encoding UTF8 -Raw

if ($execType -eq "pre") {
	$programCs = $programCs.replace('{app secret}', $appSecret).replace('// AppCenter.Start', 'AppCenter.Start')
}

if ($execType -eq "post") {
	$programCs = $programCs.replace($appSecret, '{app secret}').replace('AppCenter.Start', '// AppCenter.Start')
}

$programCs | Out-File -FilePath .\Program.cs -Encoding UTF8 -NoNewline