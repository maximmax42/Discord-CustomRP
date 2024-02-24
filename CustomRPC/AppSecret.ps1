if (-not(Test-Path -Path .appSecret -PathType Leaf)) {
	echo "No .appSecret present, ignoring..."
	return
}

$appSecret = Get-Content .appSecret
$execType = $args[0]

$appCenterSecretCs = Get-Content AppCenterSecret.cs -Encoding UTF8 -Raw

if ($execType -eq "pre") {
	$appCenterSecretCs = $appCenterSecretCs.replace('{app secret}', $appSecret)
}

if ($execType -eq "post") {
	$appCenterSecretCs = $appCenterSecretCs.replace($appSecret, '{app secret}')
}

$appCenterSecretCs | Out-File -FilePath .\AppCenterSecret.cs -Encoding UTF8 -NoNewline