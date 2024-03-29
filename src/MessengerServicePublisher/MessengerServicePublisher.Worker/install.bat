@echo off

REM Obtener la ruta del script actual
set "scriptPath=%~dp0"

REM Construir la ruta del archivo JSON de configuraci�n basado en el entorno
set "appsettingsFile=%scriptPath%appsettings.json"
set "environment="

REM Leer el valor de "ENVIRONMENT" del archivo appsettings.json
for /f "tokens=2 delims=:," %%a in ('type "%appsettingsFile%" ^| find /i "ENVIRONMENT"') do set "environment=%%~a"
set "environment=%environment:"=%"
set "environment=%environment: =%"

REM Validar que se haya le�do el entorno
if "%environment%"=="" (
  echo No se pudo determinar el entorno desde appsettings.json.
  exit /b 1
)

REM Construir la ruta del archivo JSON de configuraci�n
set "jsonFilePath=%scriptPath%appsettings.%environment%.json"

REM Leer el valor de "NameService" del archivo JSON y limpiar espacios en blanco
for /f "tokens=2 delims=:," %%a in ('type "%jsonFilePath%" ^| find /i "NameService"') do set "name=%%~a"
set "name=%name:"=%"
set "name=%name: =%"

REM Verificar si el archivo JSON a crear existe y eliminarlo si es el caso
if exist "%scriptPath%%name%.json" (
  del "%scriptPath%%name%.json"
  if exist "%scriptPath%%name%.json" (
    echo No se pudo eliminar el archivo JSON existente o no tiene permisos.
    exit /b 1
  ) else (
    echo Archivo JSON existente eliminado.
  )
)

REM Construir las rutas con doble barra invertida
set "executablePath=%scriptPath%MessengerServicePublisher.Worker.exe"
set "executablePath=%executablePath:\=\\%"
set "cwd=%scriptPath%"
set "cwd=%cwd:\=\\%"
if "%cwd:~-2%"=="\\" set "cwd=%cwd:~0,-2%"

REM Contenido del JSON
(
  echo {
  echo   "apps": [
  echo     {
  echo       "name": "%name%",
  echo       "script": "%executablePath%",
  echo       "cwd": "%cwd%",
  echo       "max_memory_restart": "2G",
  echo       "log_date_format": "MM-DD--YYYY HH:mm Z",
  echo       "max_restarts": 5,
  echo       "autorestart": true
  echo     }
  echo   ]
  echo }
) > "%scriptPath%%name%.json"

REM Verificar si se cre� el archivo JSON exitosamente
if not exist "%scriptPath%%name%.json" (
  echo Error al crear el archivo JSON o no tiene permisos.
  exit /b 1
)

echo JSON creado exitosamente.

REM Esperar 2 segundos
timeout /t 2 /nobreak > NUL

REM Ejecutar el comando pm2 start %name%.json
pm2 start "%scriptPath%%name%.json"
