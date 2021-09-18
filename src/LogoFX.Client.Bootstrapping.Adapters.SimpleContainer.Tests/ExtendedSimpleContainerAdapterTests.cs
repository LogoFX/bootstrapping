using FluentAssertions;
using LogoFX.Client.Bootstrapping.Adapters.SimpleContainer;
using LogoFX.Practices.IoC;
using Xunit;

namespace LogoFX.Client.Bootstrapping.Tests
{    
    public class ExtendedSimpleContainerAdapterTests
    {
        [Fact]
        public void Given_WhenDependencyIsRegisteredViaHandlerAndDependencyIsResolved_ThenResolvedDependencyIsNotNull()
        {
            var container = new ExtendedSimpleContainerAdapter(new ExtendedSimpleContainer());
            container.RegisterHandler<ITestDependency>(() => new TestDependency());
            TestLifetimeScopeProvider.Current = new TestObject();
            var dependency = container.Resolve<ITestDependency>();

            dependency.Should().NotBeNull();            
        }

        [Fact]
        public void Given_WhenDependencyIsRegisteredViaHandlerAndDependencyIsResolvedTwice_ThenResolvedDependenciesAreDifferent()
        {
            var container = new ExtendedSimpleContainerAdapter(new ExtendedSimpleContainer());
            container.RegisterHandler<ITestDependency>(() => new TestDependency());            
            var dependencyOne = container.Resolve<ITestDependency>();
            var dependencyTwo = container.Resolve<ITestDependency>();

            dependencyOne.Should().NotBeSameAs(dependencyTwo);            
        }
    }    
}
