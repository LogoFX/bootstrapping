using System.Reflection;
using LogoFX.Server.Bootstrapping;

var currentAssembly = Assembly.GetExecutingAssembly();
currentAssembly.SetWorkingDir();

var builder = WebApplication.CreateBuilder(args);
builder.ExecuteDefaultSetup<Bootstrapper>(currentAssembly);