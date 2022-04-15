cd contents
rmdir /Q /S lib
mkdir lib
cd lib
mkdir netstandard2.0\
robocopy ../../../../../src/Bin/netstandard/Release netstandard2.0 LogoFX.Server.IoC.Abstractions.* /E
cd ../../
nuget pack contents/LogoFX.Server.IoC.Abstractions.nuspec -OutputDirectory ../../../output