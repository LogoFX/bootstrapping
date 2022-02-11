cd contents
rmdir /Q /S lib
mkdir lib
cd lib
mkdir net6.0
robocopy ../../../../../src/Bin/net/Release net6.0 LogoFX.Server.Bootstrapping.dll /E
robocopy ../../../../../src/Bin/net/Release net6.0 LogoFX.Server.Bootstrapping.json /E
robocopy ../../../../../src/Bin/net/Release net6.0 LogoFX.Server.Bootstrapping.xml /E
robocopy ../../../../../src/Bin/net/Release net6.0 LogoFX.Server.IoC.Registration.dll /E
robocopy ../../../../../src/Bin/net/Release net6.0 LogoFX.Server.IoC.Registration.json /E
robocopy ../../../../../src/Bin/net/Release net6.0 LogoFX.Server.IoC.Registration.xml /E
cd ../../
nuget pack contents/LogoFX.Server.Bootstrapping.nuspec -OutputDirectory ../../../output