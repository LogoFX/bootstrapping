using Microsoft.Extensions.DependencyInjection;
using Solid.Bootstrapping;
using Solid.Extensibility;
using Solid.Practices.Composition;
using Solid.Practices.Middleware;

namespace LogoFX.Server.Bootstrapping
{
    /// <summary>
    /// Base bootstrapper for server/back-end
    /// </summary>
    public class BootstrapperBase : Solid.Bootstrapping.BootstrapperBase,
         IExtensible<BootstrapperBase>,
         IExtensible<IHaveRegistrator<IServiceCollection>>,         
         IHaveRegistrator<IServiceCollection>         
    {       
        private readonly ExtensibilityAspect<IHaveRegistrator<IServiceCollection>> _registratorExtensibilityAspect;
        private readonly ExtensibilityAspect<BootstrapperBase> _selfExtensibilityAspect;

        /// <summary>
        /// Creates an instance of <see cref="Solid.Bootstrapping.BootstrapperBase"/>
        /// </summary>
        /// <param name="dependencyRegistrator"></param>
        protected BootstrapperBase(IServiceCollection dependencyRegistrator) : this() =>
            Registrator = dependencyRegistrator;

        /// <inheritdoc />
        public IServiceCollection Registrator { get; }

        private BootstrapperBase()
        {            
            _registratorExtensibilityAspect = new ExtensibilityAspect<IHaveRegistrator<IServiceCollection>>(this);
            UseAspect(_registratorExtensibilityAspect);
            _selfExtensibilityAspect = new ExtensibilityAspect<BootstrapperBase>(this);
            UseAspect(_selfExtensibilityAspect);
            AssemblyLoadingManager.Extensions = () => new[] { ".dll" };
        }        

        /// <inheritdoc />
        public IHaveRegistrator<IServiceCollection> Use(IMiddleware<IHaveRegistrator<IServiceCollection>> middleware)
        {
            return _registratorExtensibilityAspect.Use(middleware);
        }

        /// <inheritdoc />
        public BootstrapperBase Use(IMiddleware<BootstrapperBase> middleware)
        {
            return _selfExtensibilityAspect.Use(middleware);
        }
    }    
}
