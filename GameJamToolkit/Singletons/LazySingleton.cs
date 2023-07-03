using System;

namespace IceBlink.GameJamToolkit.Singletons
{
    public abstract class LazySingleton<T> where T : class, new()
    {
        private static readonly Lazy<T> instance = new (() => new T());

        public static T Instance => instance.Value;
    }
}