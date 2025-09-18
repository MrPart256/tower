using Config;
using Cubes;
using Extensions;
using Game;
using Initialization;
using Input;
using Localization;
using Saves;
using Scenes;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller<GameInstaller>
{
    [SerializeField] private GameSettings _gameSettings;
    [SerializeField] private ConfigRepository _configRepository;
    public override void InstallBindings()
    {
        Container.Bind<GameSettings>()
            .FromScriptableObject(_gameSettings)
            .AsSingle();

        Container.Bind<ConfigRepository>()
            .FromScriptableObject(_configRepository)
            .AsSingle();

        Container.Bind<SceneLoader>().AsSingle();


        Container.Bind<GameConfigRepositoryInitializer>().AsSingle();
        
        Container.BindInterfacesAndSelfTo<GameEntryPoint>().AsSingle();

        Container.BindInterfacesAndSelfTo<GameRepository>()
            .BindSavable()
            .AsSingle();
        
        CubesInitializationInstaller.Install(Container);
        
        InputInstaller.Install(Container);
        
        SaveSystemInstaller.Install(Container);
        
        LocalizationSystemInstaller.Install(Container);
        
        SignalBusInstaller.Install(Container);
    }
}