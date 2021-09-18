using FluentAssertions;
using LogoFX.Client.Bootstrapping.Adapters.SimpleContainer;
using Xunit;

namespace LogoFX.Client.Bootstrapping.Tests
{
    public class SingletonRegistrationTests
    {
        [Fact]
        void RegisterSingletonSameType_ReturnsSameObjectsForTwoResolutions()
        {
            var container = new ExtendedSimpleContainerAdapter();
            container.RegisterSingleton<TestDependencyA>();

            var firstResolution = container.Resolve<TestDependencyA>();
            var secondResolution = container.Resolve<TestDependencyA>();
            firstResolution.Should().BeSameAs(secondResolution);
        }

        [Fact]
        void RegisterSingletonDifferentContractAndImplementationWithDependencyCreator_ReturnsSameObjectsForTwoResolutions()
        {
            var container = new ExtendedSimpleContainerAdapter();
            container.RegisterSingleton<ITestDependency, TestDependencyA>(() => new TestDependencyA());

            var firstResolution = container.Resolve<ITestDependency>();
            var secondResolution = container.Resolve<ITestDependency>();
            firstResolution.Should().BeSameAs(secondResolution);
        }

        [Fact]
        void RegisterSingletonWithDependencyCreator_ReturnsSameObjectsForTwoResolutions()
        {
            var container = new ExtendedSimpleContainerAdapter();
            container.RegisterSingleton<ITestDependency>(() => new TestDependencyA());

            var firstResolution = container.Resolve<ITestDependency>();
            var secondResolution = container.Resolve<ITestDependency>();
            firstResolution.Should().BeSameAs(secondResolution);
        }

        [Fact]
        void RegisterTransientExplicitTypesWithDependencyCreator_ReturnsSameObjectsForTwoResolutions()
        {
            var container = new ExtendedSimpleContainerAdapter();
            container.RegisterSingleton(typeof(ITestDependency), typeof(TestDependencyA), () => new TestDependencyA());

            var firstResolution = container.Resolve<ITestDependency>();
            var secondResolution = container.Resolve<ITestDependency>();
            firstResolution.Should().BeSameAs(secondResolution);
        }
    }
}