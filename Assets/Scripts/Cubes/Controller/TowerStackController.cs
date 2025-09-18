using System;
using System.Linq;
using Cubes.Presenter;
using Game;
using UniRx;
using UnityEngine;
using Zenject;

namespace Cubes.Controller
{
    public class TowerStackController : IInitializable, IDisposable
    {
        private readonly GameRepository _gameRepository;
        private readonly CubePlacementPresenter _cubePlacementPresenter;

        private IDisposable _countChangeTrackStream;

        public TowerStackController(GameRepository gameRepository, CubePlacementPresenter cubePlacementPresenter)
        {
            _gameRepository = gameRepository;
            _cubePlacementPresenter = cubePlacementPresenter;
        }

        public void Initialize()
        {
            _countChangeTrackStream = _gameRepository
                .Count
                .Skip(1)
                .Subscribe(_ => RestackAllCubes());
        }

        private void RestackAllCubes()
        {
            var cubes = _gameRepository.GetCubes().OrderBy(x => x.GetPosition().y).ToArray();

            if(cubes.Length <= 1)//Для одного куба нет смысла делать проверку
                return;
            
            bool hasGap = false;

            for (int i = 1; i < cubes.Length; i++)
            {
                var lower = cubes[i - 1];
                var current = cubes[i];

                
                var expectedPosition = lower.GetPosition().y + _cubePlacementPresenter.GetViewSize(lower.ID).y / 2;

                if (current.GetPosition().y > expectedPosition)
                {
                    hasGap = true;
                    break;
                }
            }
            
            if(!hasGap)
                return;

            for (int i = 1; i < cubes.Length; i++)
            {
                var lower = cubes[i - 1];
                var current = cubes[i];

                var lowerPosition = lower.GetPosition();
                var currentPosition = current.GetPosition();

                var lowerViewSize = _cubePlacementPresenter.GetViewSize(lower.ID);
                var currentViewSize = _cubePlacementPresenter.GetViewSize(current.ID);
                
                currentPosition.x = Mathf.Clamp(currentPosition.x, lowerPosition.x - lowerViewSize.x / 2, lowerPosition.x + lowerViewSize.x / 2);
                currentPosition.y = lowerPosition.y + currentViewSize.y / 2 + currentViewSize.y / 2;
                
                current.UpdatePosition(currentPosition);
                _cubePlacementPresenter.PlaceView(current.ID, currentPosition);
            }
        }

        public void Dispose()
        {
            _countChangeTrackStream?.Dispose();
        }
    }
}