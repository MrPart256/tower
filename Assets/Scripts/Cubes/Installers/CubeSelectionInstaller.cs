using Cubes.Presenter;
using Cubes.View;
using UnityEngine;
using Zenject;

namespace Cubes
{
    public class CubeSelectionInstaller : MonoInstaller<CubeSelectionInstaller>
    {
        [SerializeField] private CubeSelectionScrollView _scrollView;
        [SerializeField] private Transform _cubeSelectionViewParent;
        [SerializeField] private CubeSelectionView _cubeSelectionView;
        
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<CubeSelectionPresenter>()
                .AsSingle();
            Container.Bind<CubeSelectionScrollView>().FromInstance(_scrollView)
                .AsSingle();
            Container
                .BindMemoryPool<CubeSelectionView, CubeSelectionViewPool>()
                .FromComponentInNewPrefab(_cubeSelectionView)
                .UnderTransform(_cubeSelectionViewParent);
        }
    }
}