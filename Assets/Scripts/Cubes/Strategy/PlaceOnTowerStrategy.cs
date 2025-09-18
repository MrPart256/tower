using System.Linq;
using Cubes.Controller;
using Cubes.Model;
using Cubes.Presenter;
using Game;
using Notifications;
using UnityEngine;
using Zenject;

namespace Cubes.Strategy
{
    public class PlaceOnTowerStrategy : ICubePlacementStrategy
    {
        private readonly GameRepository _gameRepository;
        private readonly CubePlacementPresenter _cubePlacementPresenter;
        private readonly CubeMapperProvider _cubeMapperProvider;
        private readonly ICubeTowerPositionCalculator _cubeTowerPositionCalculator;
        private readonly SignalBus _signalBus;
        
        public PlaceOnTowerStrategy(GameRepository gameRepository, 
            CubePlacementPresenter cubePlacementPresenter,
            ICubeTowerPositionCalculator cubeTowerPositionCalculator,
            CubeMapperProvider cubeMapperProvider,
            SignalBus signalBus)
        {
            _gameRepository = gameRepository;
            _cubePlacementPresenter = cubePlacementPresenter;
            _cubePlacementPresenter = cubePlacementPresenter;
            _cubeTowerPositionCalculator = cubeTowerPositionCalculator;
            _cubeMapperProvider = cubeMapperProvider;
            _signalBus = signalBus;
        }

        public void PlaceCube(Cube cube)
        {
            var cubePosition = _cubeTowerPositionCalculator.CalculateCubePosition(cube);
            cube.UpdatePosition(cubePosition);
            _gameRepository.AddCube(cube);
            _signalBus.Fire<NotificationSignal>(new NotificationSignal(NotificationType.CubePlaced));
            _cubePlacementPresenter.PlaceView(cube.ID, cubePosition);
        }

        public bool CanPerform(Cube cube)
        {
            if (_gameRepository.Count.Value == 0)
                return false;
            
            var position = cube.GetPosition();

            var bounds = GetMostRightAndMostLeftCoordinates();

            return bounds.x <= position.x && bounds.y >= position.x &&  position.y >= GetTopCubePosition();
        }
        
        private float GetTopCubePosition()
        {
            var lastCube = _gameRepository.GetCubes().Last();
            var lastCubePosition = lastCube.GetPosition();
            lastCubePosition.y += _cubePlacementPresenter.GetViewSize(lastCube.ID).y / 2;
            return lastCubePosition.y;
        }
        
        private Vector2 GetMostRightAndMostLeftCoordinates()
        {
            var cubes = _gameRepository.GetCubes();

            var orderedCubes = cubes.OrderBy(cube => cube.GetPosition().x);


            var mostLeftCube = orderedCubes.First();
            var mostRightCube = orderedCubes.Last();
            float halfWidth = _cubePlacementPresenter.GetViewSize(mostLeftCube.ID).x / 2;
            float mostLeft = mostLeftCube.GetPosition().x - halfWidth;
            float mostRight = mostRightCube.GetPosition().x + halfWidth;

            return new Vector2(mostLeft, mostRight);
        }
    }
}