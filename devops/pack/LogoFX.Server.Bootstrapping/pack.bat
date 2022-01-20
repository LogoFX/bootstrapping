cd contents
rmdir /Q /S lib
mkdir lib
cd lib
mkdir net6.0
robocopy ../../../../../src/Bin/net/Release net6.0 LogoFX.Server.*.dll /E
robocopy ../../../../../src/Bin/net/Release net6.0 LogoFX.Server.*.json /E
robocopy ../../../../../src/Bin/net/Release net6.0 LogoFX.Server.*.xml /E
cd ../../
nuget pack contents/LogoFX.Server.Bootstrapping.nuspec -OutputDirectory ../../../output