using Solid.Bootstrapping;
using BootstrapperBase = LogoFX.Server.Bootstrapping.BootstrapperBase;

namespace LogoFX.Server.IoC.Registration.Specs.Presentation;

internal static class WebApplicationBuilderExtensions
{
    internal static WebApplicationBuilder AddServices(this WebApplicationBuilder webApplicationBuilder)
    {
        webApplicationBuilder.Services.AddControllers();
        var bootstrapper = new Bootstrapper(webApplicationBuilder.Services);
        bootstrapper.Use(new Bootstrapping.UseDefaultRegistrationMethodMiddleware<IHaveRegistrator<IServiceCollection>>());
        bootstrapper.Use(new RegisterCustomCompositionModulesMiddleware<BootstrapperBase,
                IServiceCollection>());
        bootstrapper.Use(new AddServicesMiddleware<BootstrapperBase>());
        bootstrapper.Initialize();
        return webApplicationBuilder;
    }

    internal static WebApplicationBuilder ConfigureApi(this WebApplicationBuilder webApplicationBuilder)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        webApplicationBuilder.Services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen();
        return webApplicationBuilder;
    }
}