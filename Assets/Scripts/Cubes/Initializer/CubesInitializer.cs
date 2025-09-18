using System;
using System.Collections.Generic;
using Initialization;
using UniRx;

namespace Cubes
{
    public class CubesInitializer : IInitializer<IEnumerable<CubeMapper>>
    {
        private readonly CubeMapperProvider _provider;

        public CubesInitializer(CubeMapperProvider provider)
        {
            _provider = provider;
        }

        public IObservable<Unit> Initialize(IEnumerable<CubeMapper> param)
        {
            _provider.AddRange(param);
            return Observable.ReturnUnit();
        }
    }
}