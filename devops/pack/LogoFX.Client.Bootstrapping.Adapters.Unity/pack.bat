cd contents
rmdir /Q /S lib
mkdir lib
cd lib
mkdir netstandard2.0\
robocopy ../../../../../src/Bin/netstandard/Release netstandard2.0 LogoFX.Client.Bootstrapping.Adapters.Unity.* /E
cd ../../
nuget pack contents/LogoFX.Client.Bootstrapping.Adapters.Unity.nuspec -OutputDirectory ../../../output