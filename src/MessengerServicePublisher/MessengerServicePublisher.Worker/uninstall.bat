echo off

REM Obtener la ruta del script actual
set "scriptPath=%~dp0"

REM Construir la ruta del archivo JSON de configuración basado en el entorno
set "appsettingsFile=%scriptPath%appsettings.json"
set "environment="

REM Leer el valor de "ENVIRONMENT" del archivo appsettings.json
for /f "tokens=2 delims=:," %%a in ('type "%appsettingsFile%" ^| find /i "ENVIRONMENT"') do set "environment=%%~a"
set "environment=%environment:"=%"
set "environment=%environment: =%"

REM Validar que se haya leído el entorno
if "%environment%"=="" (
  echo No se pudo determinar el entorno desde appsettings.json.
  exit /b 1
)

REM Construir la ruta del archivo JSON de configuración
set "jsonFilePath=%scriptPath%appsettings.%environment%.json"

REM Leer el valor de "NameService" del archivo JSON y limpiar espacios en blanco
for /f "tokens=2 delims=:," %%a in ('type "%jsonFilePath%" ^| find /i "NameService"') do set "name=%%~a"
set "name=%name:"=%"
set "name=%name: =%"

REM Eliminar el servicio del gestor pm2
pm2 delete "%name%"

echo Desinstalación completada exitosamente.

REM Esperar 2 segundos
timeout /t 2 /nobreak > NUL

REM Cerrar la ventana de la consola
exit /b 0
