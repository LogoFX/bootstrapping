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

/// <summary>
///     Extension methods for <see cref="WebApplicationBuilder" />.
/// </summary>
public static class WebApplicationBuilderExtensions
{
    /// <summary>
    ///     Applies default setup onto the provided web application builder, and runs the built web application.
    /// </summary>
    /// <typeparam name="TBootstrapper">The type of the bootstrapper.</typeparam>
    /// <param name="webApplicationBuilder">The web application builder.</param>
    /// <param name="assembly">The assembly to be used for the configuration.</param>
    public static void ExecuteDefaultSetup<TBootstrapper>(
        this WebApplicationBuilder webApplicationBuilder,
        Assembly assembly)
        where TBootstrapper : class, IExtensible<IHaveRegistrator<IServiceCollection>>, IExtensible<BootstrapperBase>,
        IInitializable
    {
        webApplicationBuilder
            .UseRuntimeAssemblyLoader()
            .AddServices<TBootstrapper>()
            .UseAssemblyConfiguration(assembly)
            .ConfigureApi()
            .Build()
            .ConfigureHttpRequestPipeline()
            .Run();
    }

    /// <summary>
    /// Adds different services: controllers, DDD services, composition modules. etc.
    /// </summary>
    /// <typeparam name="TBootstrapper">The type of the bootstrapper.</typeparam>
    /// <param name="webApplicationBuilder">The web application builder.</param>
    /// <param name="bootstrapperOptions">Optional bootstrapper invocation enhancements.</param>
    /// <returns></returns>
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
            new UseDefaultRegistrationMethodMiddleware<
                IHaveRegistrator<IServiceCollection>>());
        bootstrapper.Use(new RegisterCustomCompositionModulesMiddleware<BootstrapperBase,
            IServiceCollection>());
        bootstrapper.Use(new AddServicesMiddleware<BootstrapperBase>());
        bootstrapperOptions?.Invoke(bootstrapper);
        bootstrapper.Initialize();
        return webApplicationBuilder;
    }

    /// <summary>
    /// Configures API endpoints to ensure discoverability and OpenAPI compatibility.
    /// </summary>
    /// <param name="webApplicationBuilder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder ConfigureApi(this WebApplicationBuilder webApplicationBuilder)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        webApplicationBuilder.Services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen();
        return webApplicationBuilder;
    }

    /// <summary>
    /// Sets <see cref="RuntimeAssemblyLoader"/> as the default assembly loader.
    /// </summary>
    /// <param name="webApplicationBuilder">The web application builder.</param>
    /// <returns></returns>
    public static WebApplicationBuilder UseRuntimeAssemblyLoader(this WebApplicationBuilder webApplicationBuilder)
    {
        AssemblyLoader.LoadAssembliesFromPaths = RuntimeAssemblyLoader.Get;
        return webApplicationBuilder;
    }

    /// <summary>
    /// Loads configuration from the specified assembly.
    /// </summary>
    /// <param name="webApplicationBuilder">The web application builder.</param>
    /// <param name="assembly">The specified assembly.</param>
    /// <returns></returns>
    public static WebApplicationBuilder UseAssemblyConfiguration(
        this WebApplicationBuilder webApplicationBuilder,
        Assembly assembly)
    {
        webApplicationBuilder.Configuration.AddConfiguration(assembly.BuildConfiguration());
        return webApplicationBuilder;
    }
}