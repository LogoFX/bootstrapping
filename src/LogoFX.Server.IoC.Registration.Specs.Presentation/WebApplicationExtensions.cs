namespace LogoFX.Server.IoC.Registration.Specs.Presentation;

internal static class WebApplicationExtensions
{
    internal static WebApplication ConfigureHttpRequestPipeline(this WebApplication webApplication)
    {
        if (webApplication.Environment.IsDevelopment())
        {
            webApplication.UseSwagger();
            webApplication.UseSwaggerUI();
        }

        webApplication.UseHttpsRedirection()
            .UseAuthorization();

        webApplication.MapControllers();
        return webApplication;
    }
}