using System.Linq;
using Cubes.Model;
using Cubes.Presenter;
using Game;
using UnityEngine;

namespace Cubes.PlacementZone
{
    public class PlacementRestrictor
    {
        private readonly PlacementZone _placementZone;
        private readonly CubePlacementPresenter _cubePlacementPresenter;
        private readonly GameRepository _gameRepository;

        public PlacementRestrictor(PlacementZone placementZone, CubePlacementPresenter cubePlacementPresenter, GameRepository gameRepository)
        {
            _placementZone = placementZone;
            _cubePlacementPresenter = cubePlacementPresenter;
            _gameRepository = gameRepository;
        }

        public bool IsInsidePlacementZone(Cube cube)
        {
            var view = _cubePlacementPresenter.GetView(cube.ID);
            return RectTransformUtility.RectangleContainsScreenPoint(_placementZone.GetBounds(), cube.GetPosition());
        }

        public bool IsTopReached()
        {
            if (_gameRepository.Count.Value == 0)
                return false;
            Vector3[] corners = new Vector3[4];
            _placementZone.GetBounds().GetWorldCorners(corners);
            return _cubePlacementPresenter.GetView(_gameRepository.GetCubes().Last().ID).GetPosition().y > corners[1].y;
        }
    }
}