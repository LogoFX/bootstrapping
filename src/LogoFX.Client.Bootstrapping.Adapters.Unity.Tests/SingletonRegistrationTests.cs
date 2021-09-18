using FluentAssertions;
using Xunit;

namespace LogoFX.Client.Bootstrapping.Adapters.Unity.Tests
{
    public class SingletonRegistrationTests
    {
        [Fact]
        void RegisterSingletonSameType_ReturnsSameObjectsForTwoResolutions()
        {
            var container = new UnityContainerAdapter();
            container.RegisterSingleton<TestDependencyA>();

            var firstResolution = container.Resolve<TestDependencyA>();
            var secondResolution = container.Resolve<TestDependencyA>();
            firstResolution.Should().BeSameAs(secondResolution);
        }

        [Fact]
        void RegisterSingletonDifferentContractAndImplementationWithDependencyCreator_ReturnsSameObjectsForTwoResolutions()
        {
            var container = new UnityContainerAdapter();
            container.RegisterSingleton<ITestDependency, TestDependencyA>(() => new TestDependencyA());

            var firstResolution = container.Resolve<ITestDependency>();
            var secondResolution = container.Resolve<ITestDependency>();
            firstResolution.Should().BeSameAs(secondResolution);
        }

        [Fact]
        void RegisterSingletonWithDependencyCreator_ReturnsSameObjectsForTwoResolutions()
        {
            var container = new UnityContainerAdapter();
            container.RegisterSingleton<ITestDependency>(() => new TestDependencyA());

            var firstResolution = container.Resolve<ITestDependency>();
            var secondResolution = container.Resolve<ITestDependency>();
            firstResolution.Should().BeSameAs(secondResolution);
        }

        [Fact]
        void RegisterTransientExplicitTypesWithDependencyCreator_ReturnsSameObjectsForTwoResolutions()
        {
            var container = new UnityContainerAdapter();
            container.RegisterSingleton(typeof(ITestDependency), typeof(TestDependencyA), () => new TestDependencyA());

            var firstResolution = container.Resolve<ITestDependency>();
            var secondResolution = container.Resolve<ITestDependency>();
            firstResolution.Should().BeSameAs(secondResolution);
        }
    }
}