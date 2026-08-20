Param(
	[string[]] $Urls = @(
		"https://www.magnific.com/br/fotos-gratis/feche-acima-da-foto-de-cortar-madeira-com-fretsaw_7813805.htm#fromView=keyword&page=1&position=0&track=ais_hybrid&query=Marcenaria0",
		"https://www.magnific.com/br/fotos-gratis/um-bonito-carpinteiro-a-trabalhar-com-madeira_5713455.htm#fromView=keyword&page=1&position=10&uuid=931f6c9a-a662-4f90-af4e-46f35d7d1670&track=ais_hybrid&query=Marcenaria",
		"https://www.magnific.com/br/fotos-gratis/ferramentas-e-serragem-de-madeira-na-oficina_10267976.htm#fromView=keyword&page=1&position=25&uuid=931f6c9a-a662-4f90-af4e-46f35d7d1670&track=ais_hybrid&query=Marcenaria"
	),
	[string] $TargetRelative = ".\AtelieDaTransformacao.UI\wwwroot\images\carousel",
	[switch] $Force
)

function Ensure-Folder {
	param([string]$path)
	if (-not (Test-Path $path)) { New-Item -ItemType Directory -Path $path -Force | Out-Null }
	return (Get-Item $path).FullName
}

function Resolve-AbsoluteUrl {
	param(
		[string]$baseUrl,
		[string]$src
	)
	try {
		if ([string]::IsNullOrWhiteSpace($src)) { return $null }
		$src = $src.Trim()
		if ($src.StartsWith("//")) { return "https:$src" }
		if ($src.StartsWith("http://") -or $src.StartsWith("https://")) { return $src }
		# relative
		$b = [Uri] $baseUrl
		return (New-Object System.Uri($b, $src)).AbsoluteUri
	} catch { return $null }
}

function Extract-ImageUrlFromHtml {
	param([string]$html,[string]$baseUrl)

	# 1) og:image meta
	$m = [regex]::Match($html, '<meta[^>]+property\s*=\s*"og:image"[^>]*content\s*=\s*"(?<u>[^"]+)"', 'IgnoreCase')
	if ($m.Success) { return Resolve-AbsoluteUrl -baseUrl $baseUrl -src $m.Groups['u'].Value }

	# 2) meta name="og:image"
	$m = [regex]::Match($html, '<meta[^>]+name\s*=\s*"og:image"[^>]*content\s*=\s*"(?<u>[^"]+)"', 'IgnoreCase')
	if ($m.Success) { return Resolve-AbsoluteUrl -baseUrl $baseUrl -src $m.Groups['u'].Value }

	# 3) link rel="image_src"
	$m = [regex]::Match($html, '<link[^>]+rel\s*=\s*"image_src"[^>]*href\s*=\s*"(?<u>[^"]+)"', 'IgnoreCase')
	if ($m.Success) { return Resolve-AbsoluteUrl -baseUrl $baseUrl -src $m.Groups['u'].Value }

	# 4) first large <img src=...> with extension
	$imgs = [regex]::Matches($html, '<img[^>]+src\s*=\s*"(?<u>[^"]+)"[^>]*>', 'IgnoreCase')
	foreach ($img in $imgs) {
		$u = $img.Groups['u'].Value
		if ($u -match '\.(jpg|jpeg|png|webp|gif)(\?|$)') { return Resolve-AbsoluteUrl -baseUrl $baseUrl -src $u }
	}

	# 5) try data-src or data-lazy
	$imgs = [regex]::Matches($html, '<img[^>]+(data-src|data-lazy|data-original)\s*=\s*"(?<u>[^"]+)"[^>]*>', 'IgnoreCase')
	foreach ($img in $imgs) {
		$u = $img.Groups['u'].Value
		if ($u -match '\.(jpg|jpeg|png|webp|gif)(\?|$)') { return Resolve-AbsoluteUrl -baseUrl $baseUrl -src $u }
	}

	return $null
}

# Start
$targetFull = Ensure-Folder -path (Resolve-Path -Path $TargetRelative -ErrorAction SilentlyContinue | ForEach-Object { $_.Path } )
if (-not $targetFull) { $targetFull = Ensure-Folder -path (Join-Path (Get-Location) $TargetRelative) }

Write-Host "Salvar imagens em: $targetFull" -ForegroundColor Green

# user agent to reduce blocks
$ua = 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/118 Safari/537.36'

$i = 1
foreach ($url in $Urls) {
	Write-Host "\nProcessando URL #$i: $url" -ForegroundColor Cyan
	try {
		$resp = Invoke-WebRequest -Uri $url -UseBasicParsing -Headers @{ 'User-Agent' = $ua } -TimeoutSec 30
		$html = $resp.Content
		$imgUrl = Extract-ImageUrlFromHtml -html $html -baseUrl $url
		if (-not $imgUrl) {
			Write-Warning "Não foi possível localizar imagem na página. Pulando."
			continue
		}
		Write-Host "Imagem encontrada: $imgUrl" -ForegroundColor Yellow

		# determine extension
		$ext = [IO.Path]::GetExtension(([Uri]$imgUrl).AbsolutePath)
		if ([string]::IsNullOrWhiteSpace($ext)) { $ext = '.jpg' }

		$dest = Join-Path $targetFull ("slide{0}{1}" -f $i, $ext)
		if (Test-Path $dest -and -not $Force) {
			Write-Host "Arquivo já existe: $dest (use -Force para sobrescrever)" -ForegroundColor DarkYellow
		} else {
			Write-Host "Baixando para: $dest" -ForegroundColor Green
			Invoke-WebRequest -Uri $imgUrl -OutFile $dest -Headers @{ 'User-Agent' = $ua } -UseBasicParsing -TimeoutSec 60
			Write-Host "Salvo: $dest" -ForegroundColor Green
		}
	} catch {
		Write-Warning ("Falha ao processar {0}: {1}" -f $url, $_.Exception.Message)
	}
	$i++
}

Write-Host "\nConcluído. Abrindo pasta: $targetFull" -ForegroundColor Green
Start-Process explorer.exe $targetFull
