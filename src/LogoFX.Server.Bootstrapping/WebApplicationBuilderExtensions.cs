using System;
using System.Reflection;
using LogoFX.Server.IoC.Registration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Solid.Bootstrapping;
using Solid.Core;
using Solid.Extensibility;
using Solid.Practices.Composition;

namespace LogoFX.Server.Bootstrapping;

public static class WebApplicationBuilderExtensions
{
    public static void ExecuteDefaultSetup<TBootstrapper>(
        this WebApplicationBuilder webApplicationBuilder,
        Assembly assembly)
        where TBootstrapper : class, IExtensible<IHaveRegistrator<IServiceCollection>>, IExtensible<BootstrapperBase>,
        IInitializable
    {
        webApplicationBuilder
            .UseDefaultAssemblyLoader()
            .AddServices<TBootstrapper>()
            .UseAssemblyConfiguration(assembly)
            .ConfigureApi()
            .Build()
            .ConfigureHttpRequestPipeline()
            .Run();
    }

    public static WebApplicationBuilder AddServices<TBootstrapper>(
        this WebApplicationBuilder webApplicationBuilder,
        Func<TBootstrapper, TBootstrapper>? bootstrapperOptions = default)
        where TBootstrapper : class, IExtensible<IHaveRegistrator<IServiceCollection>>, IExtensible<BootstrapperBase>,
        IInitializable
    {
        webApplicationBuilder.Services.AddControllers();
        var bootstrapper =
            ObjectFactory.CreateObject<TBootstrapper, IServiceCollection>(webApplicationBuilder.Services);
        bootstrapper.Use(
            new LogoFX.Server.Bootstrapping.UseDefaultRegistrationMethodMiddleware<
                IHaveRegistrator<IServiceCollection>>());
        bootstrapper.Use(new RegisterCustomCompositionModulesMiddleware<BootstrapperBase,
            IServiceCollection>());
        bootstrapper.Use(new AddServicesMiddleware<BootstrapperBase>());
        bootstrapperOptions?.Invoke(bootstrapper);
        bootstrapper.Initialize();
        return webApplicationBuilder;
    }

    public static WebApplicationBuilder ConfigureApi(this WebApplicationBuilder webApplicationBuilder)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        webApplicationBuilder.Services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen();
        return webApplicationBuilder;
    }

    public static WebApplicationBuilder UseDefaultAssemblyLoader(this WebApplicationBuilder serviceCollection)
    {
        AssemblyLoader.LoadAssembliesFromPaths = DefaultAssemblyLoader.Get;
        return serviceCollection;
    }

    public static WebApplicationBuilder UseAssemblyConfiguration(
        this WebApplicationBuilder webApplicationBuilder,
        Assembly assembly)
    {
        webApplicationBuilder.Configuration.AddConfiguration(assembly.BuildConfiguration());
        return webApplicationBuilder;
    }
}