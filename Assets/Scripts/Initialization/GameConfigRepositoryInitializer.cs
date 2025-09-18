using System;
using Config;
using Cubes;
using UniRx;
namespace Initialization
{
    public class GameConfigRepositoryInitializer : IInitializer
    {
        private readonly ConfigRepository _configRepository;

        private readonly CubesInitializerProxy _cubesInitializer;

        public GameConfigRepositoryInitializer(ConfigRepository configRepository, CubesInitializerProxy cubesInitializer)
        {
            _configRepository = configRepository;
            _cubesInitializer = cubesInitializer;
        }

        public IObservable<Unit> Initialize()
        {
            return InitializeCubes();
        }
        
        private IObservable<Unit> InitializeCubes()
        {
            return _cubesInitializer.Initialize(_configRepository);
        }
    }
}