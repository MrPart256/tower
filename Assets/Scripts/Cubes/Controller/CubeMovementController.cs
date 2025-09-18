using System;
using Cubes.Model;
using Cubes.Presenter;
using Game;
using Input;
using UniRx;
using Zenject;

namespace Cubes.Controller
{
    public class CubeMovementController : IInitializable ,IDisposable
    {
        private readonly CubePlacementPresenter _cubePlacementPresenter;
        private readonly IInputController _inputController;
        private readonly CubeFactory _cubeFactory;
        private readonly GameRepository _gameRepository;
        private readonly CubePlacementController _cubePlacementController;
        
        private IDisposable _placementStream;

        public CubeMovementController(
            CubePlacementPresenter cubePlacementPresenter
            , IInputController inputController
            , CubeFactory cubeFactory
            , GameRepository gameRepository
            , CubePlacementController cubePlacementController)
        {
            _cubePlacementPresenter = cubePlacementPresenter;
            _inputController = inputController;
            _cubeFactory = cubeFactory;
            _gameRepository = gameRepository;
            _cubePlacementController = cubePlacementController;
        }

        public void InitiateFirstTimePlacement(int cubeMapperId)
        {
            var cube = _cubeFactory.Create(cubeMapperId);
            
            _cubePlacementPresenter.ShowView(cube.ID, cubeMapperId);
            
            InitiatePlacement(cube);
        }

        public void InitiatePlacement(int cubeId)
        {
            var cube = _gameRepository.GetCube(cubeId);
            
            _gameRepository.RemoveCube(cube);
            
            InitiatePlacement(cube);
        }

        private void InitiatePlacement(Cube cube)
        {
            _cubePlacementPresenter.UpdateViewPosition(cube.ID, _inputController.MousePosition);
            
            _placementStream?.Dispose();

            _placementStream = _inputController.OnDrag
                .Do(_ =>
                {
                    var position = _inputController.MousePosition;
                    cube.UpdatePosition(position);
                    _cubePlacementPresenter.UpdateViewPosition(cube.ID, position);
                })
                .SelectMany(_ => _inputController.OnDragEnd)
                .Take(1)
                .Subscribe(_ => EndPlacement(cube));
        }

        private void EndPlacement(Cube cube)
        {
            _placementStream?.Dispose();
            
            _cubePlacementController.PlaceCube(cube);
        }

        public void Initialize()
        {
            _cubePlacementPresenter.OnPlacementInitiated += InitiatePlacement;
        }
        
        public void Dispose()
        {
            _cubePlacementPresenter.OnPlacementInitiated -= InitiatePlacement;
            _placementStream?.Dispose();
        }
    }
}