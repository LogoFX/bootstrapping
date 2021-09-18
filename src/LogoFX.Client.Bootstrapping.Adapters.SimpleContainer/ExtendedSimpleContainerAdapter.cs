using System;
using System.Collections.Generic;
using System.Linq;
using LogoFX.Client.Bootstrapping.Adapters.Contracts;
using LogoFX.Practices.IoC;
using Solid.Practices.IoC;

namespace LogoFX.Client.Bootstrapping.Adapters.SimpleContainer
{
    /// <summary>
    /// Represents implementation of ioc container and bootstrapper adapter using <see cref="ExtendedSimpleContainer"/>
    /// </summary>
    public class ExtendedSimpleContainerAdapter : IIocContainer, IIocContainerAdapter<ExtendedSimpleContainer>,
        IDependencyRegistratorScoped, IBootstrapperAdapter
    {
        private readonly ExtendedSimpleContainer _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedSimpleContainerAdapter"/> class.
        /// </summary>
        public ExtendedSimpleContainerAdapter()
            :this(new ExtendedSimpleContainer())
        {}

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedSimpleContainerAdapter"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public ExtendedSimpleContainerAdapter(ExtendedSimpleContainer container)
        {
            _container = container;
            _container.RegisterInstance(typeof(ExtendedSimpleContainer), null, _container);
            _container.RegisterInstance(typeof(Practices.IoC.SimpleContainer), null, _container);
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
        public void RegisterSingleton<TService, TImplementation>() where TImplementation : class, TService
        {
            RegisterSingletonImpl(typeof(TService), typeof(TImplementation));
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

        /// <summary>
        /// Registers service in singleton lifetime style.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="key">The key.</param>
        public void RegisterSingleton<TService, TImplementation>(string key) where TImplementation : class, TService
        {
            _container.RegisterSingleton(typeof(TService), key, typeof(TImplementation));
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
                _container.RegisterSingleton(typeof(TService), null, type);
            }
        }

        /// <inheritdoc />       
        public void RegisterCollection<TService>(IEnumerable<TService> dependencies) where TService : class
        {
            _container.RegisterInstance(typeof(IEnumerable<TService>), null, dependencies);
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

        /// <summary>
        /// Determines whether the specified service has handler.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public bool HasHandler(Type service, string key)
        {
            return _container.HasHandler(service, key);
        }

        /// <summary>
        /// Registers the handler.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="key">The key.</param>
        /// <param name="handler">The handler.</param>
        public void RegisterHandler(Type service, string key, Func<Practices.IoC.SimpleContainer, object> handler)
        {
            _container.RegisterHandler(service,key,(container,args) => handler(container));
        }

        /// <summary>
        /// Registers the service in external object scope lifetime style.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="lifetimeScopeAccess">The lifetime scope access.</param>
        public void RegisterPerLifetime<TService, TImplementation>(Func<object> lifetimeScopeAccess)
        {
            _container.RegisterPerLifetime(lifetimeScopeAccess,typeof(TService), null, typeof(TImplementation));
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns></returns>
        public TService GetInstance<TService>(Type serviceType) where TService : class
        {
            return (TService)_container.GetInstance(serviceType, null);
        }

        /// <summary>
        /// Gets the instance.
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
            return _container.GetInstance(serviceType, null);
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
            ((IDisposable) _container).Dispose();
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

        /// <inheritdoc />        
        public void RegisterTransient<TService, TImplementation>(Func<TImplementation> dependencyCreator) where TImplementation : class, TService
        {
            _container.RegisterHandler(typeof(TService), null, (container, args) => dependencyCreator());
        }

        /// <inheritdoc />      
        public void RegisterTransient<TService>(Func<TService> dependencyCreator) where TService : class
        {
            _container.RegisterHandler(typeof(TService), null, (container, args) => dependencyCreator());
        }

        /// <inheritdoc />       
        public void RegisterTransient(Type serviceType, Type implementationType, Func<object> dependencyCreator)
        {
            _container.RegisterHandler(serviceType, null, (container, args) => dependencyCreator());
        }

        /// <inheritdoc />       
        public void RegisterSingleton<TService>() where TService : class
        {
            _container.RegisterSingleton(typeof(TService), null, typeof(TService));
        }

        /// <inheritdoc />       
        public void RegisterSingleton<TService>(Func<TService> dependencyCreator) where TService : class
        {
            _container.RegisterSingleton(typeof(TService), null, (container, args) => dependencyCreator());
        }

        /// <inheritdoc />       
        public void RegisterSingleton<TService, TImplementation>(Func<TImplementation> dependencyCreator) where TImplementation : class, TService
        {
            _container.RegisterSingleton(typeof(TService), null, (container, args) => dependencyCreator());
        }

        /// <inheritdoc />       
        public void RegisterSingleton(Type serviceType, Type implementationType, Func<object> dependencyCreator)
        {
            _container.RegisterSingleton(serviceType, null, (container, args) => dependencyCreator());
        }
    }
}