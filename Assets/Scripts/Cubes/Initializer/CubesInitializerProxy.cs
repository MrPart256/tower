using System;
using System.Collections.Generic;
using Config;
using Initialization;
using UniRx;

namespace Cubes
{
    public class CubesInitializerProxy : BaseInitializerProxyConfigRepository<IEnumerable<CubeMapper>>
    {
        public CubesInitializerProxy(IInitializer<IEnumerable<CubeMapper>> initializer) : base(initializer)
        {
        }

        public override IObservable<Unit> Initialize(ConfigRepository repository)
        {
            return _initializer.Initialize(repository.CubesConfig.Cubes);
        }
    }
}