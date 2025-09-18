using System;
using Saves;
using Scenes;
using Zenject;
using UniRx;

namespace Initialization
{
    public class GameEntryPoint : IInitializable, IDisposable
    {
        private readonly GameConfigRepositoryInitializer _gameConfigRepositoryInitializer;
        private readonly SaveSystemInitializer _saveSystemInitializer;
        
        private readonly SceneLoader _sceneLoader;
        private readonly GameSettings _gameSettings;

        private IDisposable _gameInitializationStream;

        public GameEntryPoint(GameConfigRepositoryInitializer gameConfigRepositoryInitializer, SceneLoader sceneLoader, GameSettings gameSettings, SaveSystemInitializer saveSystemInitializer)
        {
            _gameConfigRepositoryInitializer = gameConfigRepositoryInitializer;
            _gameSettings = gameSettings;
            _sceneLoader = sceneLoader;
            _saveSystemInitializer = saveSystemInitializer;
        }

        public void Initialize()
        {
            _gameInitializationStream = _gameConfigRepositoryInitializer
                .Initialize()
                .ContinueWith(_=> _saveSystemInitializer.Initialize())
                .ContinueWith(_sceneLoader.LoadScenes(_gameSettings.Scenes.Scenes))
                .Publish()
                .Connect();
        }

        public void Dispose()
        {
            _gameInitializationStream?.Dispose();
        }
    }
}