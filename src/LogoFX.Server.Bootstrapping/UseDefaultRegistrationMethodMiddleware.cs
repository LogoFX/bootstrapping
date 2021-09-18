using Microsoft.Extensions.DependencyInjection;
using Solid.Bootstrapping;
using Solid.IoC.Registration;
using Solid.Practices.Middleware;

namespace LogoFX.Server.Bootstrapping
{
    /// <summary>
    /// Adds support for default registration method for <see cref="IServiceCollection"/>.
    /// The default lifetime is singleton.
    /// </summary>
    /// <typeparam name="TBootstrapper"></typeparam>
    public class UseDefaultRegistrationMethodMiddleware<TBootstrapper> : IMiddleware<TBootstrapper>
        where TBootstrapper : class, IHaveRegistrator<IServiceCollection>
    {
        /// <inheritdoc />
        public TBootstrapper Apply(TBootstrapper @object)
        {
            RegistrationMethodContext.SetDefaultRegistrationMethod<IServiceCollection>((dr, match) =>
                dr.AddScoped(match.ServiceType, match.ImplementationType));
            return @object;
        }
    }
}
