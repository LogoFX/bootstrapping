using System.Reflection;

namespace LogoFX.Server.IoC.Registration.Specs.Presentation
{
    public static class AssemblyExtensions
    {
        public static void SetWorkingDir(this Assembly assembly)
        {
            var executingDir = Path.GetDirectoryName(assembly.Location);
            Directory.SetCurrentDirectory(executingDir);
        }
    }
}
