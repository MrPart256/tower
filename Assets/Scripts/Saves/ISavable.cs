namespace Saves
{
    public interface ISavable
    {
        public string GetSaveKey();

        public SaveData Save();

        public void Load(SaveData saveData);
    }
}