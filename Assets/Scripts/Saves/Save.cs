using System.Collections.Generic;

namespace Saves
{
    [System.Serializable]
    public class Save
    {
        public List<SaveData> Mementos;

        public Save(List<SaveData> mementos)
        {
            Mementos = mementos;
        }

        public Save()
        {
            Mementos = new();
        }
    }
}