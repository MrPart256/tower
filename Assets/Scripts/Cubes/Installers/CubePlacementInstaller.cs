using Cubes.Controller;
using Cubes.Presenter;
using Cubes.Strategy;
using Cubes.View;
using UnityEngine;
using Zenject;

namespace Cubes
{
    public class CubePlacementInstaller : MonoInstaller<CubePlacementInstaller>
    {
        [SerializeField] private Transform _placementParent;
        [SerializeField] private CubePlacementView cubePlacementView;

        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<CubeMovementController>()
                .AsSingle();
            Container.BindInterfacesAndSelfTo<CubePlacementPresenter>()
                .AsSingle();
            Container.BindInterfacesTo<CubeTowerTopPositionCalculator>().AsSingle();
            Container
                .BindMemoryPool<CubePlacementView, CubePlacementViewPool>()
                .FromComponentInNewPrefab(cubePlacementView)
                .UnderTransform(_placementParent);
            Container.Bind<CubePlacementController>().AsSingle();

            Container.BindInterfacesAndSelfTo<TowerStackController>().AsSingle();
            
            BindStrategies();
        }

        private void BindStrategies()
        {
            BindPlaceStrategy<PlaceAnywhereStrategy>();
            BindPlaceStrategy<PlaceOnTowerStrategy>();
            BindPlaceStrategy<DropInHoleStrategy>();
        }
        
        private void BindPlaceStrategy<TStrategy>() where TStrategy : ICubePlacementStrategy
        {
            Container.BindInterfacesTo<TStrategy>().AsCached();
        }
    }
}