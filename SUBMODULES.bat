@echo off
setlocal

REM ================= CONFIGURACIÓN =================
set "DEST=2025\Assets\TelemetrySystem"
set "TMP=%TEMP%\tmp_telemetry"
set "URL=https://github.com/AgusCDT/UAJ_P3_TelemetrySystem.git"
set "SRC=Runtime\telemetry_src"

REM Obtener ruta absoluta
set "DEST_FULL=%CD%\%DEST%"

REM ================= DEPURACIÓN =================
echo.
echo ================= DEPURACIÓN =================
echo Valor de DEST: [%DEST%]
echo Ruta completa: [%DEST_FULL%]
echo =============================================
echo.
pause

REM ================= BORRAR DESTINO =================
if exist "%DEST_FULL%\*" (
    echo Borrando carpeta "%DEST%"...
    rd /s /q "%DEST%"
) else if exist "%DEST_FULL%" (
    echo Archivo con nombre "%DEST%" encontrado. Eliminando...
    del /f /q "%DEST%"
)

if exist "%DEST%" (
    echo ERROR: No se pudo eliminar "%DEST%"
    pause & exit /b 1
)

REM ================= BORRAR TMP SI EXISTE =================
if exist "%TMP%" (
    rd /s /q "%TMP%"
)

REM ================= CLONAR REPO =================
echo Clonando repositorio temporal...
git clone --depth 1 "%URL%" "%TMP%" || (
    echo ERROR al clonar el repositorio
    pause & exit /b 1
)

REM ================= COPIAR ARCHIVOS =================
echo Copiando "%SRC%" a "%DEST%"...
xcopy "%TMP%\%SRC%\*" "%DEST%\" /E /I /Y >nul

REM ================= LIMPIAR TMP =================
echo Borrando carpeta temporal...
rd /s /q "%TMP%"

REM ================= FINAL =================
echo.
echo ====================================
echo ¡Hecho!
echo Archivos copiados a: %DEST%
echo Incluye:
echo - Telemetry.cs
echo - Events\
echo - Persistence\
echo - Serializers\
echo ====================================
pause
endlocal
