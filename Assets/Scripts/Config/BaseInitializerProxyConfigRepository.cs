using System;
using Initialization;
using UniRx;

namespace Config
{
    public abstract class BaseInitializerProxyConfigRepository<T> : IInitializer<ConfigRepository>
    {
        protected readonly IInitializer<T> _initializer;

        public BaseInitializerProxyConfigRepository(IInitializer<T> initializer)
        {
            _initializer = initializer;
        }


        public abstract IObservable<Unit> Initialize(ConfigRepository repository);
    }
}