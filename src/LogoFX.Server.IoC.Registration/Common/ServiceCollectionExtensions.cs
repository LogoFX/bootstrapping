using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Solid.IoC.Registration;

// ReSharper disable once CheckNamespace
namespace LogoFX.Server.IoC.Registration
{
    public static partial class ServiceCollectionExtensions
    {
        private static IServiceCollection AddServicesAsContracts
        (this IServiceCollection serviceCollection,
            IEnumerable<Assembly> assemblies,
            string ending)
        {
            return serviceCollection.RegisterImplementationsAsContracts(assemblies,
                assembliesCollection => assembliesCollection.FindTypesByEnding(ending),
                RegistrationMethodContext.GetDefaultRegistrationMethod<IServiceCollection>());
        }
    }
}
