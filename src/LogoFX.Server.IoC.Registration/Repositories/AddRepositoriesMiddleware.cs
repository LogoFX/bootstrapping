using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Solid.Bootstrapping;
using Solid.Practices.Composition.Contracts;
using Solid.Practices.Middleware;

// ReSharper disable once CheckNamespace
namespace LogoFX.Server.IoC.Registration
{
    public class AddRepositoriesMiddleware<TBootstrapper> : 
        IMiddleware<TBootstrapper> where TBootstrapper : class, IHaveRegistrator<IServiceCollection>, IAssemblySourceProvider
    {
        TBootstrapper IMiddleware<TBootstrapper>.Apply(TBootstrapper @object)
        {
            @object.Registrator.AddRepositories(@object.Assemblies.ToArray());
            return @object;
        }
    }
}
