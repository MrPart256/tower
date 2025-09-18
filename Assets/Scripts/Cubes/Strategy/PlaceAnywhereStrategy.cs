using Cubes.Model;
using Cubes.PlacementZone;
using Cubes.Presenter;
using Game;
using Input;
using Notifications;
using Zenject;

namespace Cubes.Strategy
{
    public class PlaceAnywhereStrategy : ICubePlacementStrategy
    {
        private readonly GameRepository _gameRepository;
        private readonly IInputController _inputController;
        private readonly CubePlacementPresenter _cubePlacementPresenter;
        private readonly PlacementRestrictor _placementRestrictor;
        private readonly SignalBus _signalBus;

        public PlaceAnywhereStrategy(GameRepository gameRepository
            , IInputController inputController
            , CubePlacementPresenter placementPresenter
            , PlacementRestrictor placementRestrictor
            , SignalBus signalBus)
        {
            _gameRepository = gameRepository;
            _inputController = inputController;
            _cubePlacementPresenter = placementPresenter;
            _placementRestrictor = placementRestrictor;
            _signalBus = signalBus;
        }

        public void PlaceCube(Cube cube)
        {
            var position = _inputController.MousePosition;
            _cubePlacementPresenter.UpdateViewPosition(cube.ID, position);
            _signalBus.Fire<NotificationSignal>(new NotificationSignal(NotificationType.CubePlaced));
            cube.UpdatePosition(position);
            _gameRepository.AddCube(cube);
        }

        public bool CanPerform(Cube cube)
        {
            return _gameRepository.Count.Value == 0 && _placementRestrictor.IsInsidePlacementZone(cube);
        }
    }
}