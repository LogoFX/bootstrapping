cd contents
rmdir /Q /S lib
mkdir lib
cd lib
mkdir net461
robocopy ../../../../../src/Bin/netframework/Release net461 LogoFX.Client.Bootstrapping.Platform.* /E
robocopy ../../../../../src/Bin/netframework/Release net461 Caliburn.Micro.* /E
robocopy ../../../../../src/Bin/netframework/Release net461 System.Windows.Interactivity.dll /E
mkdir net6.0
robocopy ../../../../../src/Bin/net/Release net6.0 LogoFX.Client.Bootstrapping.Platform.* /E
robocopy ../../../../../src/Bin/net/Release net6.0 Caliburn.Micro.* /E
robocopy ../../../../../src/Bin/net/Release net6.0 System.Windows.Interactivity.dll /E
cd net6.0
rmdir /Q /S ref
cd ..
mkdir netcoreapp3.1
robocopy ../../../../../src/Bin/netcore/Release netcoreapp3.1 LogoFX.Client.Bootstrapping.Platform.* /E
robocopy ../../../../../src/Bin/netcore/Release netcoreapp3.1 Caliburn.Micro.* /E
robocopy ../../../../../src/Bin/netcore/Release netcoreapp3.1 System.Windows.Interactivity.dll /E
cd ../../
nuget pack contents/LogoFX.Client.Bootstrapping.nuspec -OutputDirectory ../../../output