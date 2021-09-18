﻿using System;
using System.Collections.Generic;
using System.Reflection;
using Solid.Bootstrapping;
using Solid.Common;
using Solid.Core;
using Solid.Extensibility;
using Solid.Practices.Composition;
using Solid.Practices.Composition.Contracts;
using Solid.Practices.IoC;
using Solid.Practices.Middleware;
using Solid.Practices.Modularity;

namespace LogoFX.Client.Bootstrapping.Xamarin.Forms
{
    //TODO: Consider using the base bootstrapper and re-examining the custom discovery aspect usage
    /// <summary>
    /// Base class that enables the following core aspects:
    /// Modularity, 
    /// Assembly inspection, 
    /// Extensibility.
    /// </summary>
    public class BootstrapperBase : 
        IInitializable,
        IExtensible<BootstrapperBase>,
        ICompositionModulesProvider,
        IHaveRegistrator,
        IAssemblySourceProvider, 
        IHaveAspects<BootstrapperBase>
    {
        private readonly PlatformAspect _platformAspect;
        private readonly DiscoveryAspect _discoveryAspect;
        private readonly ModularityAspect _modularityAspect;
        private readonly ExtensibilityAspect<BootstrapperBase> _extensibilityAspect;
        private readonly AspectsWrapper _aspectsWrapper = new AspectsWrapper();

        /// <summary>
        /// Creates an instance of <see cref="BootstrapperBase"/>
        /// </summary>
        /// <param name="dependencyRegistrator">The dependency registrator.</param>
        public BootstrapperBase(IDependencyRegistrator dependencyRegistrator)
        {
            Registrator = dependencyRegistrator;
            _platformAspect = new PlatformAspect();
            _discoveryAspect = new DiscoveryAspect(CompositionOptions);
            _modularityAspect = new ModularityAspect(_discoveryAspect, CompositionOptions);
            _extensibilityAspect = new ExtensibilityAspect<BootstrapperBase>(this);            
        }

        /// <summary>
        /// Gets the prefixes of the modules that will be used by the module registrator
        /// during bootstrapper configuration. Use this to implement efficient discovery.
        /// </summary>
        /// <value>
        /// The prefixes.
        /// </value>
        public virtual CompositionOptions CompositionOptions => new CompositionOptions();

        /// <summary>
        /// Gets the additional types which can extend the list of assemblies
        /// to be inspected for app components. Use this to add dynamic assemblies.
        /// </summary>
        /// <value>The additional types.</value>
        public virtual Type[] AdditionalTypes => new Type[] { };

        /// <summary>
        /// Gets the list of modules that were discovered during bootstrapper configuration.
        /// </summary>
        /// <value>
        /// The list of modules.
        /// </value>
        public IEnumerable<ICompositionModule> Modules => _modularityAspect.Modules;

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public IDependencyRegistrator Registrator { get; }
       
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public IEnumerable<Assembly> Assemblies => _discoveryAspect.Assemblies;       

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public BootstrapperBase Use(
            IMiddleware<BootstrapperBase> middleware)
        {
            _extensibilityAspect.Use(middleware);
            return this;
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public void Initialize()
        {
            _aspectsWrapper.UseCoreAspects(new IAspect[]
                {_platformAspect, _discoveryAspect, _modularityAspect, _extensibilityAspect});
            _aspectsWrapper.Initialize();
        }

        /// <inheritdoc />
        public BootstrapperBase UseAspect(IAspect aspect)
        {
            _aspectsWrapper.UseAspect(aspect);
            return this;
        }
    }
}
