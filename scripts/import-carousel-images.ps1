Add-Type -AssemblyName System.Windows.Forms

$ofd = New-Object System.Windows.Forms.OpenFileDialog
$ofd.Multiselect = $true
$ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.webp;*.gif"
$ofd.Title = "Selecione as imagens para o carousel (ordem será preservada)"

if ($ofd.ShowDialog() -ne [System.Windows.Forms.DialogResult]::OK) {
	Write-Host "Nenhuma imagem selecionada." -ForegroundColor Yellow
	return
}

# calcula pasta alvo relativa ao script
$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Definition
$target = Join-Path $scriptDir "..\AtelieDaTransformacao.UI\wwwroot\images\carousel"
$target = Resolve-Path -Path $target -ErrorAction SilentlyContinue | ForEach-Object { $_.Path }
if (-not $target) {
	$target = Join-Path (Split-Path -Parent $scriptDir) "AtelieDaTransformacao.UI\wwwroot\images\carousel"
}

# garante que a pasta exista
New-Item -ItemType Directory -Force -Path $target | Out-Null

# copia e renomeia em ordem: slide1.ext, slide2.ext, ...
$i = 1
foreach ($file in $ofd.FileNames) {
	$ext = [IO.Path]::GetExtension($file)
	$dest = Join-Path $target ("slide{0}{1}" -f $i, $ext)
	Copy-Item -Path $file -Destination $dest -Force
	Write-Host "Copiando '$file' -> '$dest'"
	$i++
}

Write-Host "Importação concluída. Abrindo a pasta: $target" -ForegroundColor Green
Start-Process explorer.exe $target

Write-Host "Recompile a solução se necessário (dotnet build)." -ForegroundColor Cyan
