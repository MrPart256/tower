using System;
using System.Collections.Generic;
using System.Linq;
using Cubes.View;
using Game;
using UnityEngine;
using Zenject;

namespace Cubes.Presenter
{
    public class CubePlacementPresenter : IInitializable
    {
        public event Action<int> OnPlacementInitiated;
        
        private Dictionary<int, CubePlacementView> _activeViews = new();

        private GameRepository _gameRepository;
        private CubePlacementViewPool _cubePlacementViewPool;
        private CubeMapperProvider _cubeMapperProvider;

        [Inject]
        private void Construct(
            CubePlacementViewPool cubePlacementViewPool,
            CubeMapperProvider cubeMapperProvider,
            GameRepository gameRepository)
        {
            _cubePlacementViewPool = cubePlacementViewPool;
            _cubeMapperProvider = cubeMapperProvider;
            _gameRepository = gameRepository;
        }

        public void Initialize()
        {
            ShowSavedViews();
        }

        public void ShowView(int cubeId, int cubeMapperId)
        {
            var mapper = _cubeMapperProvider.Get(cubeMapperId);

            var view = _cubePlacementViewPool.Spawn(mapper.ColorSprite);
            view.SetState(CubePlacementView.State.Active);
            _activeViews.Add(cubeId, view);
        }

        public void PlaceView(int cubeId, Vector2 position)
        {
            _activeViews[cubeId].PlayPlaceAnimation(position);
        }

        public void DropView(int cubeId, Vector2 position)
        {
            var view = _activeViews[cubeId];
            view.PlayDropInHoleAnimatio(position);
            _cubePlacementViewPool.Despawn(view);
            _activeViews.Remove(cubeId);
        }

        public CubePlacementView GetView(int cubeId)
            => _activeViews[cubeId];
        

        public void HideView(int cubeId)
        {
            var view = _activeViews[cubeId];
            view.SetState(CubePlacementView.State.Inactive);
            _cubePlacementViewPool.Despawn(view);
            _activeViews.Remove(cubeId);
        }

        public Vector2 GetViewSize(int cubeId)
        => _activeViews[cubeId].GetSize();

        public Vector2 GetViewPosition(int cubeId)
            => _activeViews[cubeId].GetPosition();
        
        public void UpdateViewPosition(int cubeId, Vector2 position)
        {
            _activeViews[cubeId].UpdatePosition(position);
        }

        public void InitiatePlacement(CubePlacementView cubePlacementView)
        {
            OnPlacementInitiated?.Invoke(_activeViews.First(x => x.Value == cubePlacementView).Key);
        }

        private void ShowSavedViews()
        {
            foreach (var savedCube in _gameRepository.GetCubes())
            {
                ShowView(savedCube.ID, savedCube.MapperId);
                UpdateViewPosition(savedCube.ID, savedCube.GetPosition());
            }
        }
    }
}