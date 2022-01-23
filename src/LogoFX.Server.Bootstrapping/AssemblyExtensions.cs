using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace LogoFX.Server.Bootstrapping
{
    /// <summary>
    /// Various assembly extension methods.
    /// </summary>
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Builds configuration from the specified assembly's configuration file.
        /// </summary>
        /// <param name="assembly">The specified assembly.</param>
        /// <returns></returns>
        public static IConfiguration BuildConfiguration(this Assembly assembly)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile($"{assembly.GetFileNameImpl()}.json");
            return builder.Build();
        }

        /// <summary>
        /// Sets working directory from the specified assembly's location.
        /// </summary>
        /// <param name="assembly">The specified assembly.</param>
        public static void SetWorkingDir(this Assembly assembly)
        {
            var executingDir = Path.GetDirectoryName(assembly.Location);
            Directory.SetCurrentDirectory(executingDir);
        }

        /// <summary>
        /// Gets specified assembly's file name (without the extension).
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
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
