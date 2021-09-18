using FluentAssertions;
using Xunit;

namespace LogoFX.Client.Bootstrapping.Adapters.Unity.Tests
{
    public class TransientRegistrationTests
    {
        [Fact]
        void RegisterTransientDifferentContractAndImplementationWithDependencyCreator_ReturnsTwoDifferentObjectsForTwoResolutions()
        {
            var container = new UnityContainerAdapter();
            container.RegisterTransient<ITestDependency, TestDependencyA>(() => new TestDependencyA());

            var firstResolution = container.Resolve<ITestDependency>();
            var secondResolution = container.Resolve<ITestDependency>();
            firstResolution.Should().NotBeSameAs(secondResolution);
        }

        [Fact]
        void RegisterTransientWithDependencyCreator_ReturnsTwoDifferentObjectsForTwoResolutions()
        {
            var container = new UnityContainerAdapter();
            container.RegisterTransient<ITestDependency>(() => new TestDependencyA());

            var firstResolution = container.Resolve<ITestDependency>();
            var secondResolution = container.Resolve<ITestDependency>();
            firstResolution.Should().NotBeSameAs(secondResolution);
        }

        [Fact]
        void RegisterTransientExplicitTypesWithDependencyCreator_ReturnsTwoDifferentObjectsForTwoResolutions()
        {
            var container = new UnityContainerAdapter();
            container.RegisterTransient(typeof(ITestDependency), typeof(TestDependencyA), () => new TestDependencyA());

            var firstResolution = container.Resolve<ITestDependency>();
            var secondResolution = container.Resolve<ITestDependency>();
            firstResolution.Should().NotBeSameAs(secondResolution);
        }
    }
}
