using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace LogoFX.Server.Bootstrapping;

public static class WebApplicationExtensions
{
    public static WebApplication ConfigureHttpRequestPipeline(
        this WebApplication webApplication,
        Action<WebApplication>? setupOptions = default)
    {
        setupOptions?.Invoke(webApplication);
        if (webApplication.Environment.IsDevelopment())
        {
            webApplication.UseSwagger();
            webApplication.UseSwaggerUI();
        }

        webApplication.UseAuthorization();
        webApplication.MapControllers();
        return webApplication;
    }
}
