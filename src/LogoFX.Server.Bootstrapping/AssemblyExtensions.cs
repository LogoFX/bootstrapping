using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace LogoFX.Server.Bootstrapping
{
    public static class AssemblyExtensions
    {
        public static IConfiguration BuildConfiguration(this Assembly assembly)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile($"{assembly.GetFileNameImpl()}.json");
            return builder.Build();
        }

        public static void SetWorkingDir(this Assembly assembly)
        {
            var executingDir = Path.GetDirectoryName(assembly.Location);
            Directory.SetCurrentDirectory(executingDir);
        }

        public static string GetFileName(this Assembly assembly)
        {
            return GetFileNameImpl(assembly);
        }

        private static string GetFileNameImpl(this Assembly assembly)
        {
            return assembly.GetName().Name;
        }
    }
}
