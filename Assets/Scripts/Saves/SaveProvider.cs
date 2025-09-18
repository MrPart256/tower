using System.Linq;

namespace Saves
{
    public class SaveProvider
    {
        private Save _save = new();
        
        public void Set(Save save)
        {
            _save = save;
        }

        public bool Contains(ISavable savable)
            => _save.Mementos.Any(x => x.SaveKey == savable.GetSaveKey());

        public SaveData GetMemento(ISavable savable)
            => _save.Mementos.First(x => x.SaveKey == savable.GetSaveKey());
    }
}