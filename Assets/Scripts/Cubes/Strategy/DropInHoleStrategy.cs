using Cubes.Model;
using Cubes.Presenter;
using Game;
using Hole;
using Notifications;
using Zenject;

namespace Cubes.Strategy
{
    public class DropInHoleStrategy : ICubePlacementStrategy
    {
        private readonly CubePlacementPresenter _cubePlacementPresenter;
        private readonly GameRepository _gameRepository;
        private readonly HoleController _holeController;
        private readonly SignalBus _signalBus;

        public DropInHoleStrategy(CubePlacementPresenter cubePlacementPresenter
            , HoleController holeController
            , GameRepository gameRepository
            , SignalBus signalBus)
        {
            _cubePlacementPresenter = cubePlacementPresenter;
            _holeController = holeController;
            _gameRepository = gameRepository;
            _signalBus = signalBus;
        }

        public void PlaceCube(Cube cube)
        {
           _cubePlacementPresenter.DropView(cube.ID, _holeController.GetCenter());
           _gameRepository.RemoveCube(cube);
           _signalBus.Fire<NotificationSignal>(new NotificationSignal(NotificationType.CubeDropped));
        }

        public bool CanPerform(Cube cube)
        {
            return _holeController.IsInsideHole(cube.GetPosition());
        }
    }
}