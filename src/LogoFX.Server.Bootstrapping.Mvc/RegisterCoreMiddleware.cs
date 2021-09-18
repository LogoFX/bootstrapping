using Microsoft.Extensions.DependencyInjection;
using Solid.Bootstrapping;
using Solid.Practices.Composition.Contracts;
using Solid.Practices.Middleware;

namespace LogoFX.Server.Bootstrapping.Mvc
{
    /// <summary>
    /// Defines middleware for registering core AspNetCore dependencies
    /// </summary>
    /// <typeparam name="TBootstrapper"></typeparam>
    public class RegisterCoreMiddleware<TBootstrapper> : IMiddleware<TBootstrapper>
        where TBootstrapper : class, IAssemblySourceProvider, IHaveRegistrator<IServiceCollection>
    {
        /// <summary>
        /// Applies the middleware onto the provided object.
        /// </summary>
        /// <param name="object">The provided object.</param>
        /// <returns></returns>
        public TBootstrapper Apply(TBootstrapper @object)
        {
            @object.Registrator
                .AddMvcCore(options => options.EnableEndpointRouting = false)
                .AddApiExplorer();
            return @object;
        }
    }
}
