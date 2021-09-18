using System;
using System.Collections.Generic;
using System.Linq;
using LogoFX.Client.Bootstrapping.Adapters.Contracts;
using Solid.Practices.IoC;

namespace LogoFX.Client.Bootstrapping.Adapters.SimpleContainer
{
    /// <summary>
    /// Represents implementation of IoC container and bootstrapper adapter using <see cref="Practices.IoC.SimpleContainer"/>
    /// </summary>
    public class SimpleContainerAdapter : IIocContainer, IIocContainerAdapter<Practices.IoC.SimpleContainer>,
        IDependencyRegistratorScoped, IBootstrapperAdapter
    {
        private readonly Practices.IoC.SimpleContainer _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleContainerAdapter"/> class.
        /// </summary>
        public SimpleContainerAdapter()
            :this(new Practices.IoC.SimpleContainer())
        {}

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleContainerAdapter"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public SimpleContainerAdapter(Practices.IoC.SimpleContainer container)
        {
            _container = container;
            _container.RegisterSingleton(typeof(Practices.IoC.SimpleContainer), null, typeof(Practices.IoC.SimpleContainer));
        }

        /// <inheritdoc />       
        public void RegisterTransient<TService, TImplementation>() where TImplementation : class, TService
        {
            _container.RegisterPerRequest(typeof(TService), null, typeof(TImplementation));
        }

        /// <inheritdoc />       
        public void RegisterTransient<TService>() where TService : class
        {
            RegisterTransient<TService, TService>();
        }

        /// <inheritdoc />       
        public void RegisterTransient(Type serviceType, Type implementationType)
        {
            _container.RegisterPerRequest(serviceType, null, implementationType);
        }

        /// <inheritdoc />        
        public void RegisterTransient<TService, TImplementation>(Func<TImplementation> dependencyCreator) where TImplementation : class, TService
        {
            _container.RegisterHandler(typeof(TService), null, (container, args) => dependencyCreator);
        }

        /// <inheritdoc />        
        public void RegisterTransient<TService>(Func<TService> dependencyCreator) where TService : class
        {
            _container.RegisterHandler(typeof(TService), null, (container, args) => dependencyCreator);
        }

        /// <inheritdoc />        
        public void RegisterTransient(Type serviceType, Type implementationType, Func<object> dependencyCreator)
        {
            _container.RegisterHandler(serviceType, null, (container, args) => dependencyCreator);
        }

        /// <inheritdoc />       
        public void RegisterSingleton<TService, TImplementation>() where TImplementation : class, TService
        {
            RegisterSingletonImpl(typeof(TService), typeof(TImplementation));
        }

        /// <inheritdoc />       
        public void RegisterSingleton<TService>() where TService : class
        {
            RegisterSingletonImpl(typeof(TService), typeof(TService));
        }

        /// <inheritdoc />        
        public void RegisterSingleton(Type serviceType, Type implementationType)
        {
            RegisterSingletonImpl(serviceType, implementationType);
        }

        private void RegisterSingletonImpl(Type serviceType, Type implementationType)
        {
            _container.RegisterSingleton(serviceType, null, implementationType);
        }

        /// <inheritdoc />       
        public void RegisterSingleton<TService>(Func<TService> dependencyCreator) where TService : class
        {
            _container.RegisterHandler(typeof(TService), null, (container, args) => dependencyCreator);
        }

        /// <inheritdoc />        
        public void RegisterSingleton<TService, TImplementation>(Func<TImplementation> dependencyCreator) where TImplementation : class, TService
        {
            _container.RegisterHandler(typeof(TService), null, (container, args) => dependencyCreator);
        }

        /// <inheritdoc />       
        public void RegisterSingleton(Type serviceType, Type implementationType, Func<object> dependencyCreator)
        {
            _container.RegisterHandler(serviceType, null, (container, args) => dependencyCreator);
        }

        /// <inheritdoc />       
        public void RegisterInstance<TService>(TService instance) where TService : class
        {
            RegisterInstanceImpl(typeof(TService), instance);
        }

        /// <inheritdoc />       
        public void RegisterInstance(Type dependencyType, object instance)
        {
            RegisterInstanceImpl(dependencyType, instance);
        }

        private void RegisterInstanceImpl(Type dependencyType, object instance)
        {
            _container.RegisterInstance(dependencyType, null, instance);
        }

        /// <summary>
        /// Registers the dependency via the handler.
        /// </summary>
        /// <param name="dependencyType">Type of the dependency.</param><param name="handler">The handler.</param>
        public void RegisterHandler(Type dependencyType, Func<object> handler)
        {
            _container.RegisterHandler(dependencyType, null, (container, args) => handler());
        }

        /// <summary>
        /// Registers the dependency via the handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        public void RegisterHandler<TService>(Func<TService> handler) where TService : class
        {
            _container.RegisterHandler(typeof(TService), null, (container, args) => handler());
        }

        /// <inheritdoc />       
        public void RegisterCollection<TService>(IEnumerable<Type> dependencyTypes) where TService : class
        {
            foreach (var type in dependencyTypes)
            {
                _container.RegisterSingleton(typeof (TService), null, type);
            }
        }

        /// <inheritdoc />       
        public void RegisterCollection<TService>(IEnumerable<TService> dependencies) where TService : class
        {
            _container.RegisterInstance(typeof(IEnumerable<TService>),null, dependencies);
        }

        /// <inheritdoc />       
        public void RegisterCollection(Type dependencyType, IEnumerable<Type> dependencyTypes)
        {
            foreach (var type in dependencyTypes)
            {
                _container.RegisterSingleton(dependencyType, null, type);
            }
        }

        /// <inheritdoc />       
        public void RegisterCollection(Type dependencyType, IEnumerable<object> dependencies)
        {
            foreach (var dependency in dependencies)
            {
                _container.RegisterInstance(dependencyType, null, dependency);
            }
        }

        /// <inheritdoc />       
        public void RegisterScoped(Func<object> lifetimeProvider, Type service, Type implementation)
        {
            _container.RegisterPerLifetime(lifetimeProvider, service, null, implementation);
        }

        /// <inheritdoc />       
        public void RegisterScoped<TService, TImplementation>(Func<object> lifetimeProvider)
        {
            _container.RegisterPerLifetime(lifetimeProvider, typeof(TService), null, typeof(TImplementation));
        }

        /// <inheritdoc />        
        public void RegisterScoped<TService>(Func<object> lifetimeProvider)
        {
            _container.RegisterPerLifetime(lifetimeProvider, typeof(TService), null, typeof(TService));
        }

        /// <summary>
        /// Gets the service instance.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns></returns>
        public TService GetInstance<TService>(Type serviceType) where TService : class
        {
            return (TService)_container.GetInstance(serviceType, null);
        }

        /// <summary>
        /// Gets the service instance.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <returns></returns>
        public TService GetInstance<TService>() where TService : class
        {
            return GetInstance<TService>(typeof(TService));
        }

        /// <inheritdoc />        
        public object GetInstance(Type serviceType, string key)
        {
            return _container.GetInstance(serviceType, key);
        }

        /// <inheritdoc />      
        public IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return _container.GetAllInstances(serviceType);
        }

        /// <inheritdoc />       
        public void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

        /// <inheritdoc />       
        public TService Resolve<TService>() where TService : class
        {
            return GetInstance<TService>();
        }

        /// <inheritdoc />       
        public object Resolve(Type serviceType)
        {
            return GetInstance(serviceType, null);
        }

        /// <inheritdoc />
        public IEnumerable<TDependency> ResolveAll<TDependency>() where TDependency : class
        {
            return GetAllInstances(typeof(TDependency)).Cast<TDependency>();
        }

        /// <inheritdoc />
        public IEnumerable<object> ResolveAll(Type dependencyType)
        {
            return GetAllInstances(dependencyType);
        }

        /// <inheritdoc />        
        public void Dispose()
        {
            _container.Dispose();
        }
    }
}
