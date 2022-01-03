var builder = WebApplication.CreateBuilder(args);

builder
    .AddServices()
    .ConfigureApi()
    .Build()
    .ConfigureHttpRequestPipeline()
    .Run();    