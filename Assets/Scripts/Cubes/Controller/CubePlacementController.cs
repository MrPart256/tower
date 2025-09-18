using System;
using System.Collections.Generic;
using System.Linq;
using Cubes.Model;
using Cubes.Presenter;
using Cubes.Strategy;
using Game;
using Hole;
using Notifications;
using Zenject;

namespace Cubes.Controller
{
    public class CubePlacementController
    {
        private readonly GameRepository _gameRepository;
        private readonly HoleController _holeController;
        private readonly IEnumerable<ICubePlacementStrategy> _placementStrategies;
        private readonly CubePlacementPresenter _cubePlacementPresenter;
        private readonly SignalBus _signalBus;
        
        public CubePlacementController(GameRepository gameRepository,
            IEnumerable<ICubePlacementStrategy> cubePlacementStrategies,
            HoleController holeController,
            CubePlacementPresenter cubePlacementPresenter,
            SignalBus signalBus)
        {
            _gameRepository = gameRepository;
            _placementStrategies = cubePlacementStrategies;
            _holeController = holeController;
            _cubePlacementPresenter = cubePlacementPresenter;
            _signalBus = signalBus;
        }

        public void PlaceCube(Cube cube)
        {
            var strategy = _placementStrategies.FirstOrDefault(x => x.CanPerform(cube));

            if (strategy == null)
            {
                _cubePlacementPresenter.HideView(cube.ID);
                _signalBus.Fire<NotificationSignal>(new NotificationSignal(NotificationType.CubeHidden));
                return;
            }
            
            strategy.PlaceCube(cube);
        }
    }
}