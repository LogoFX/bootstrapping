using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LogoFX.Client.Bootstrapping.Adapters.Contracts;
using Solid.Practices.IoC;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace LogoFX.Client.Bootstrapping.Adapters.Unity
{
    /// <summary>
    /// Represents implementation of IoC container and bootstrapper adapter using <see cref="UnityContainer"/>
    /// </summary>
    public class UnityContainerAdapter : IIocContainer, IIocContainerAdapter<UnityContainer>, IBootstrapperAdapter
    {
        private readonly IUnityContainer _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnityContainerAdapter"/> class.
        /// </summary>
        public UnityContainerAdapter()
        :this(new UnityContainer())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnityContainerAdapter"/> class.
        /// </summary>
        /// <param name="unityContainer">The instance of <see cref="IUnityContainer"/></param>
        public UnityContainerAdapter(IUnityContainer unityContainer)
        {
            _container = unityContainer;
            _container.RegisterInstance(_container);
            _container.RegisterType(typeof(IEnumerable<>),
                new InjectionFactory((container, type, name) =>
                    container.ResolveAll(type.GetTypeInfo().GenericTypeArguments.Single())));
        }

        /// <inheritdoc />       
        public void RegisterTransient<TService, TImplementation>() where TImplementation : class, TService
        {
            _container.RegisterType<TService, TImplementation>();
        }

        /// <inheritdoc />       
        public void RegisterTransient<TService>() where TService : class
        {
            _container.RegisterType<TService>();            
        }

        /// <inheritdoc />       
        public void RegisterTransient(Type serviceType, Type implementationType)
        {
            _container.RegisterType(serviceType, implementationType);
        }

        /// <inheritdoc />       
        public void RegisterTransient<TService, TImplementation>(Func<TImplementation> dependencyCreator) where TImplementation : class, TService
        {
            _container.RegisterType<TService>(new InjectionFactory(context => dependencyCreator()));
        }

        /// <inheritdoc />       
        public void RegisterTransient<TService>(Func<TService> dependencyCreator) where TService : class
        {
            _container.RegisterType<TService>(new InjectionFactory(context => dependencyCreator()));
        }

        /// <inheritdoc />       
        public void RegisterTransient(Type serviceType, Type implementationType, Func<object> dependencyCreator)
        {
            _container.RegisterType(serviceType,
                new InjectionFactory(context => dependencyCreator()));
        }

        /// <inheritdoc />       
        public void RegisterSingleton<TService>() where TService : class
        {
            _container.RegisterType<TService>(new ContainerControlledLifetimeManager());
        }

        /// <inheritdoc />       
        public void RegisterSingleton<TService, TImplementation>() where TImplementation : class, TService
        {
            _container.RegisterType<TService, TImplementation>(new ContainerControlledLifetimeManager());
        }

        /// <inheritdoc />              
        public void RegisterSingleton(Type serviceType, Type implementationType)
        {
            _container.RegisterType(serviceType, implementationType, new ContainerControlledLifetimeManager());
        }

        /// <inheritdoc />        
        public void RegisterSingleton<TService>(Func<TService> dependencyCreator) where TService : class
        {
            _container.RegisterType<TService>(new ContainerControlledLifetimeManager(),
                new InjectionFactory(context => dependencyCreator()));
        }

        /// <inheritdoc />       
        public void RegisterSingleton<TService, TImplementation>(Func<TImplementation> dependencyCreator) where TImplementation : class, TService
        {
            _container.RegisterType<TService>(new ContainerControlledLifetimeManager(), new InjectionFactory(context => dependencyCreator()));
        }

        /// <inheritdoc />        
        public void RegisterSingleton(Type serviceType, Type implementationType, Func<object> dependencyCreator)
        {
            _container.RegisterType(serviceType, new ContainerControlledLifetimeManager(),
                new InjectionFactory(context => dependencyCreator()));
        }

        /// <inheritdoc />       
        public void RegisterInstance<TService>(TService instance) where TService : class
        {
            _container.RegisterInstance(instance, new ContainerControlledLifetimeManager());
        }

        /// <inheritdoc />       
        public void RegisterInstance(Type dependencyType, object instance)
        {
            _container.RegisterInstance(dependencyType, instance, new ContainerControlledLifetimeManager());
        }        

        /// <inheritdoc />        
        public void RegisterCollection<TService>(IEnumerable<Type> dependencyTypes) where TService : class
        {
            foreach (var type in dependencyTypes)
            {
                _container.RegisterType(typeof (TService), type, type.Name);
            }            
        }

        /// <inheritdoc />       
        public void RegisterCollection<TService>(IEnumerable<TService> dependencies) where TService : class
        {
            _container.RegisterInstance(dependencies);
        }

        /// <inheritdoc />       
        public void RegisterCollection(Type dependencyType, IEnumerable<Type> dependencyTypes)
        {
            foreach (var type in dependencyTypes)
            {
                _container.RegisterType(dependencyType, type, type.Name);
            }            
        }

        /// <inheritdoc />       
        public void RegisterCollection(Type dependencyType, IEnumerable<object> dependencies)
        {
            foreach (var dependency in dependencies)
            {
                _container.RegisterType(dependencyType, dependencyType, dependency.GetType().FullName);
            }                       
        }

        /// <inheritdoc />       
        public TService Resolve<TService>() where TService : class
        {
            return _container.Resolve<TService>();
        }

        /// <inheritdoc />        
        public object Resolve(Type serviceType)
        {
            return _container.Resolve(serviceType);
        }

        /// <inheritdoc />
        public IEnumerable<TDependency> ResolveAll<TDependency>() where TDependency : class
        {
            return _container.ResolveAll<TDependency>();
        }

        /// <inheritdoc />
        public IEnumerable<object> ResolveAll(Type dependencyType)
        {
            return _container.ResolveAll(dependencyType);
        }

        /// <summary>
        /// Resolves an instance of required service by its type.
        /// </summary>
        /// <param name="serviceType">Type of service.</param>
        /// <param name="key">Optional service key.</param>
        /// <returns>Instance of service.</returns>
        public object GetInstance(Type serviceType, string key)
        {
            return _container.Resolve(serviceType);
        }

        /// <summary>
        /// Resolves all instances of required service by its type.
        /// </summary>
        /// <param name="serviceType">Type of service.</param>
        /// <returns>All instances of requested service.</returns>
        public IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return _container.ResolveAll(serviceType);
        }
        
        /// <summary>
        /// Resolves instance's dependencies and injects them into the instance.
        /// </summary>
        /// <param name="instance">Instance to get injected with dependencies.</param>
        public void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

        /// <inheritdoc />              
        public void Dispose()
        {
            ((IDisposable) _container).Dispose();
        }
    }
}
