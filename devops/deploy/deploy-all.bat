rem TODO: Use common source for all version instances
SET version=2.2.3
rem TODO: Refactor using loop and automatic discovery
call deploy-single.bat LogoFX.Bootstrapping %version% 
call deploy-single.bat LogoFX.Server.Bootstrapping %version%
call deploy-single.bat LogoFX.Client.Core %version%