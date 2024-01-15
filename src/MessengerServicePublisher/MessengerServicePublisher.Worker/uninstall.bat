@echo off

REM Obtener la ruta del script actual
set "scriptPath=%~dp0"

REM Construir la ruta del archivo JSON de configuración
set "jsonFilePath=%scriptPath%appsettings.Production.json"

REM Leer el valor de "NameService" del archivo JSON y limpiar espacios en blanco
for /f "tokens=2 delims=:," %%a in ('type "%jsonFilePath%" ^| find /i "NameService"') do set "name=%%~a"
set "name=%name:"=%"
set "name=%name: =%"

REM Detener el servicio con pm2
pm2 stop "%scriptPath%%name%.json"

REM Eliminar el servicio del gestor pm2
pm2 delete "%scriptPath%%name%.json"

echo Desinstalación completada exitosamente.

REM Esperar 2 segundos
timeout /t 2 /nobreak > NUL

REM Cerrar la ventana de la consola
exit /b 0
