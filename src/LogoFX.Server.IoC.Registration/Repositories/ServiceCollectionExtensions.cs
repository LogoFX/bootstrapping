using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace LogoFX.Server.IoC.Registration
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(
            this IServiceCollection serviceCollection,
            Assembly[] assemblies)
        {
            return serviceCollection.AddRepositoriesImpl(assemblies);
        }

        private static IServiceCollection AddRepositoriesImpl(
            this IServiceCollection serviceCollection,
            Assembly[] assemblies)
        {
            serviceCollection.AddServicesAsContracts(assemblies, "Repository");
            return serviceCollection;
        }
    }
}
