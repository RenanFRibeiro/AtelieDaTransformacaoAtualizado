@echo off
setlocal

echo ================================================
echo  ATELIE DA TRANSFORMACAO - CONFIGURAR LOCALDB
echo ================================================
echo.

where sqllocaldb >nul 2>&1
if errorlevel 1 (
  echo ERRO: SQL Server LocalDB nao esta instalado.
  echo.
  echo Instale o SQL Server Express LocalDB pelo instalador do SQL Server/Visual Studio.
  echo Depois execute este arquivo novamente.
  pause
  exit /b 1
)

echo Instancias LocalDB atuais:
sqllocaldb info

echo.

sqllocaldb info MSSQLLocalDB >nul 2>&1
if errorlevel 1 (
  echo Criando a instancia MSSQLLocalDB...
  sqllocaldb create MSSQLLocalDB
  if errorlevel 1 (
    echo ERRO ao criar a instancia MSSQLLocalDB.
    pause
    exit /b 1
  )
)

echo Iniciando MSSQLLocalDB...
sqllocaldb start MSSQLLocalDB
if errorlevel 1 (
  echo ERRO ao iniciar MSSQLLocalDB.
  pause
  exit /b 1
)

echo.
echo ================================================
echo  LOCALDB CONFIGURADO COM SUCESSO
echo ================================================
echo.
sqllocaldb info MSSQLLocalDB
echo.
echo Agora abra o Visual Studio e execute AtelieDaTransformacao.UI.
pause
endlocal
