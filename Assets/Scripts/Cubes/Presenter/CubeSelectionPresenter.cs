using Cubes.Controller;
using Cubes.PlacementZone;
using Cubes.View;
using Notifications;
using Zenject;

namespace Cubes.Presenter
{
    public class CubeSelectionPresenter : IInitializable
    {
        private readonly CubeMapperProvider _cubeMapperProvider;
        private readonly CubeSelectionViewPool _cubeSelectionViewPool;
        private readonly CubeSelectionScrollView _cubeSelectionScrollView;
        private readonly CubeMovementController _cubeMovementController;
        private readonly SignalBus _signalBus;
        private readonly PlacementRestrictor _placementRestrictor;

        public CubeSelectionPresenter(CubeMapperProvider cubeMapperProvider, 
            CubeSelectionViewPool viewPool,
            CubeSelectionScrollView scrollView,
            CubeMovementController cubeMovementController,
            PlacementRestrictor placementRestrictor,
            SignalBus signalBus)
        {
            _cubeMapperProvider = cubeMapperProvider;
            _cubeSelectionViewPool = viewPool;
            _cubeSelectionScrollView = scrollView;
            _cubeMovementController = cubeMovementController;
            _placementRestrictor = placementRestrictor;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            InitSelectionViews();
            _cubeSelectionScrollView.EnableScroll();
        }

        private void InitSelectionViews()
        {
            foreach (var cubeMapper in _cubeMapperProvider.Collection)
            {
                ShowView(cubeMapper);
            }
        }

        private void ShowView(CubeMapper cubeMapper)
        {
            var view = _cubeSelectionViewPool.Spawn(cubeMapper.ID, cubeMapper.ColorSprite);
            view.gameObject.SetActive(true);
        }

        public void InitiateSelection(int cubeMapperId)
        {
            _cubeSelectionScrollView.BlockScroll();
            if (_placementRestrictor.IsTopReached())
            {
                _signalBus.Fire<NotificationSignal>(new NotificationSignal(NotificationType.ReachedTop));
                return;
            }
            _cubeMovementController.InitiateFirstTimePlacement(cubeMapperId);
        }

        public void EndSelection()
        {
            _cubeSelectionScrollView.EnableScroll();
        }
    }
}