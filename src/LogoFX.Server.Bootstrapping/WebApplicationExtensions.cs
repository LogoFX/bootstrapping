using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace LogoFX.Server.Bootstrapping;

/// <summary>
/// Extension methods for <see cref="WebApplication"/>.
/// </summary>
public static class WebApplicationExtensions
{
    /// <summary>
    /// Applies default setup on the instance of <see cref="WebApplication"/>.
    /// </summary>
    /// <param name="webApplication">The web application.</param>
    /// <param name="setupOptions">Optional setup enhancements.</param>
    /// <returns></returns>
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
