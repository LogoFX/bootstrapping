rem TODO: Use common source for all version instances
SET version=2.2.7
rem TODO: Refactor using loop and automatic discovery
call deploy-single.bat LogoFX.Bootstrapping %version% 
call deploy-single.bat LogoFX.Server.IoC.Abstractions %version%
call deploy-single.bat LogoFX.Server.Bootstrapping %version%
call deploy-single.bat LogoFX.Client.Bootstrapping.Adapters.Contracts %version%
call deploy-single.bat LogoFX.Client.Bootstrapping.Adapters.SimpleContainer %version%
call deploy-single.bat LogoFX.Client.Bootstrapping.Adapters.Unity %version%
call deploy-single.bat LogoFX.Client.Bootstrapping.Core %version%
call deploy-single.bat LogoFX.Client.Bootstrapping %version%
call deploy-single.bat LogoFX.Client.Bootstrapping.Testing %version%
call deploy-single.bat LogoFX.Client.Bootstrapping.Xamarin.Forms %version%