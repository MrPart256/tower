using System;
using System.Collections.Generic;
using System.Linq;
using Cubes.Model;
using Saves;
using UniRx;

namespace Game
{
    public class GameRepository : ISavable , IDisposable
    {
        private const string SaveKey = "game.repository.save";

        public IReadOnlyReactiveProperty<int> Count => _count;
        
        private List<Cube> _cubes = new();

        private readonly ReactiveProperty<int> _count = new();

        public void AddCube(Cube cube)
        {
            if (!_cubes.Contains(cube))
                _cubes.Add(cube);
            UpdateCount();
        }

        public void RemoveCube(Cube cube)
        {
            if (_cubes.Contains(cube))
                _cubes.Remove(cube);
            UpdateCount();
        }

        private void UpdateCount()
        {
            _count.Value = _cubes.Count;
        }
        
        public Cube GetCube(int cubeId)
            => _cubes.First(x => x.ID == cubeId);

        public IEnumerable<Cube> GetCubes()
            => _cubes;

        public bool HasCube(int cubeId)
        {
            return _cubes.Any(x => x.ID == cubeId);
        }

        public string GetSaveKey()
            => SaveKey;

        public SaveData Save()
            => new RepositorySaveData(this);

        public void Load(SaveData saveData)
        {
            var m = (RepositorySaveData)saveData;

            _cubes = m._cubes;
            
            _count.Value = _cubes.Count;
        }
        
        public void Dispose()
        {
            _count.Dispose();
        }
        
        [System.Serializable]
        public class RepositorySaveData : SaveData
        {
            public List<Cube> _cubes;
            
            public RepositorySaveData(GameRepository gameRepository) : base(gameRepository)
            {
                _cubes = gameRepository._cubes;
            }
        }
    }
}