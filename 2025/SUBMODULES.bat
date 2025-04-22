@echo off
cd /d %~dp0

echo ==========================
echo 1. Eliminando submodulo viejo
echo ==========================

git submodule deinit -f -- 2025/Assets/TelemetrySystem
git rm -f 2025/Assets/TelemetrySystem
rd /s /q .git\modules\2025\Assets\TelemetrySystem
rd /s /q 2025\Assets\TelemetrySystem

echo ==========================
echo 2. Añadiendo submodulo nuevo
echo ==========================

git submodule add https://github.com/AgusCDT/UAJ_P3_TelemetrySystem 2025/Assets/TelemetrySystem

echo ==========================
echo 3. Inicializando y actualizando submodulo
echo ==========================

git submodule update --init --recursive

echo ==========================
echo ¡Submodulo listo!
echo ==========================
pause
