using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace LogoFX.Server.IoC.Registration
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(
            this IServiceCollection serviceCollection,
            Assembly[] assemblies)
        {
            return serviceCollection.AddServicesImpl(assemblies);
        }

        private static IServiceCollection AddServicesImpl(
            this IServiceCollection serviceCollection,
            Assembly[] assemblies)
        {
            serviceCollection.AddServicesAsContracts(assemblies, "Service");
            return serviceCollection;
        }
    }
}
