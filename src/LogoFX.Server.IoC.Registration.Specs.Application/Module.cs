using JetBrains.Annotations;
using LogoFX.Server.IoC.Registration.Specs.Domain;
using Microsoft.Extensions.DependencyInjection;
using Solid.Practices.Modularity;

namespace LogoFX.Server.IoC.Registration.Specs.Application
{
    [UsedImplicitly]
    public sealed class Module : ICompositionModule<IServiceCollection>
    {
        public void RegisterModule(IServiceCollection dependencyRegistrator)
        {
            dependencyRegistrator.AddSingleton<IWeatherService, WeatherService>();
        }
    }
}
