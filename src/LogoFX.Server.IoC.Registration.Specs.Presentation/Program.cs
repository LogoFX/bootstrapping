using System.Reflection;

Assembly.GetCallingAssembly().SetWorkingDir();

var builder = WebApplication.CreateBuilder(args);

builder
    .AddServices()
    .ConfigureApi()
    .Build()
    .ConfigureHttpRequestPipeline()
    .Run();    