using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace LogoFX.Server.IoC.Registration
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInteractors(
            this IServiceCollection serviceCollection,
            Assembly[] assemblies)
        {
            return serviceCollection.AddInteractorsImpl(assemblies);
        }

        private static IServiceCollection AddInteractorsImpl(
            this IServiceCollection serviceCollection,
            Assembly[] assemblies)
        {
            serviceCollection.AddServices(assemblies, "Interactor");
            return serviceCollection;
        }
    }
}
