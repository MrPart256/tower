using System.Collections.Generic;

namespace Saves
{
    public class SavableStorage
    {
        private readonly List<ISavable> _savables = new();

        public void Add(ISavable savable)
        {
            if (!_savables.Contains(savable))
                _savables.Add(savable);
        }

        public IEnumerable<ISavable> GetAll()
            => _savables;
    }
}