using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace LogoFX.Server.Bootstrapping.Mvc
{
    /// <summary>
    /// Contains extensions for registering services into <see cref="IServiceCollection"/>
    /// </summary>
    public static class DependencyRegistratorExtensions
    {        
        /// <summary>
        /// Registers controllers which are defined in the provided assembly into <see cref="IServiceCollection"/>
        /// </summary>
        /// <param name="dependencyRegistrator">The dependency registrator.</param>
        /// <param name="assembly">The assembly</param>
        /// <returns></returns>
        public static IServiceCollection RegisterControllers(this IServiceCollection dependencyRegistrator, 
            Assembly assembly)
        {
            return dependencyRegistrator
                .AddMvcCore()
                .AddApplicationPart(assembly)
                .AddControllersAsServices().Services;
        }
    }
}