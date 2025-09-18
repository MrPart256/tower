using System.Linq;
using Cubes.Model;
using Cubes.Presenter;
using Game;
using UnityEngine;

namespace Cubes.Controller
{
    public class CubeTowerTopPositionCalculator : ICubeTowerPositionCalculator
    {
        private readonly GameRepository _gameRepository;
        private readonly CubePlacementPresenter _cubePlacementPresenter;

        public CubeTowerTopPositionCalculator(GameRepository gameRepository, CubePlacementPresenter cubePlacementPresenter)
        {
            _gameRepository = gameRepository;
            _cubePlacementPresenter = cubePlacementPresenter;
        }

        public Vector2 CalculateCubePosition(Cube activeCube)
        {
            var lastCube = _gameRepository.GetCubes().Last();

            var lastCubeSize = _cubePlacementPresenter.GetViewSize(lastCube.ID);
            var lastCubeHalfWidth = lastCubeSize.x / 2;
            var lastCubeHalfHeight = lastCubeSize.y / 2;

            var activeCubeHalfHeight = _cubePlacementPresenter.GetViewSize(activeCube.ID).y / 2;

            var lastCubePosition = lastCube.GetPosition();
            lastCubePosition.x += Random.Range(-lastCubeHalfWidth, lastCubeHalfWidth);
            lastCubePosition.y += lastCubeHalfHeight + activeCubeHalfHeight;

            return lastCubePosition;
        }
    }
}