using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Unity;
using Xunit;

namespace LogoFX.Client.Bootstrapping.Adapters.Unity.Tests
{
    public class CollectionRegistrationTests
    {
        [Fact]
        public void MultipleImplementationAreRegisteredByType_ResolvedCollectionContainsAllImplementations()
        {
            var adapter = new UnityContainerAdapter(new UnityContainer());
            adapter.RegisterCollection<ITestDependency>(new[] {typeof(TestDependencyA), typeof(TestDependencyB)});

            var collection = adapter.Resolve<IEnumerable<ITestDependency>>().ToArray();

            var firstItem = collection.First();
            var secondItem = collection.Last();

            firstItem.Should().BeOfType<TestDependencyA>();
            secondItem.Should().BeOfType<TestDependencyB>();
        }

        [Fact]
        public void MultipleImplementationAreRegisteredByTypeAsParameter_ResolvedCollectionContainsAllImplementations()
        {
            var adapter = new UnityContainerAdapter();
            adapter.RegisterCollection(typeof(ITestDependency), new[] {typeof(TestDependencyA), typeof(TestDependencyB)});

            var collection = adapter.Resolve<IEnumerable<ITestDependency>>().ToArray();

            var firstItem = collection.First();
            var secondItem = collection.Last();

            firstItem.Should().BeOfType<TestDependencyA>();
            secondItem.Should().BeOfType<TestDependencyB>();
        }

        [Fact]
        public void MultipleImplementationAreRegisteredByInstance_ResolvedCollectionContainsAllImplementations()
        {
            var adapter = new UnityContainerAdapter();
            var instanceA = new TestDependencyA();
            var instanceB = new TestDependencyB();
            adapter.RegisterCollection(new ITestDependency[] {instanceA, instanceB});

            var collection = adapter.Resolve<IEnumerable<ITestDependency>>().ToArray();

            var firstItem = collection.First();
            var secondItem = collection.Last();

            firstItem.Should().BeSameAs(instanceA);
            secondItem.Should().BeSameAs(instanceB);
        }
    }
}
