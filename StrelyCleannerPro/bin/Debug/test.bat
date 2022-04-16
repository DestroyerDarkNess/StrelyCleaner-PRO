@echo off
Set ApplicationPath="C:\Users\S4Lsalsoft\Documents\Visual Studio 2015\Projects\StrelyCleannerPro\StrelyCleannerPro\bin\Debug\StrelyCleannerPro.exe"
cmd.exe /C "set __COMPAT_LAYER=RUNASINVOKER && start "" %ApplicationPath%"
pause