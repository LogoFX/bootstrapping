using System;

namespace LogoFX.Server.Bootstrapping
{
    internal static class ObjectFactory
    {
        internal static TObject CreateObject<TObject, TIocContainer>(TIocContainer iocContainer) where TObject : class
        {
            return Activator.CreateInstance(typeof(TObject), iocContainer) as TObject;
        }
    }
}
