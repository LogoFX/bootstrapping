using LogoFX.Server.Bootstrapping;
using Solid.Practices.Composition;

namespace LogoFX.Server.IoC.Registration.Specs.Presentation
{
    internal sealed class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper(IServiceCollection dependencyRegistrator) : base(dependencyRegistrator)
        {
        }

        public override CompositionOptions CompositionOptions => new()
        {
            Prefixes = new[]
            {
                "LogoFX.Server.IoC.Registration.Specs"
            }
        };
    }
}
