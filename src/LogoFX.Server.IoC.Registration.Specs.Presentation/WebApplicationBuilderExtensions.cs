namespace LogoFX.Server.IoC.Registration.Specs.Presentation;

internal static class WebApplicationBuilderExtensions
{
    internal static WebApplicationBuilder AddServices(this WebApplicationBuilder webApplicationBuilder)
    {
        webApplicationBuilder.Services.AddControllers();
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