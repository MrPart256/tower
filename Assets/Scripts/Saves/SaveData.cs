using UnityEngine;

namespace Saves
{
    [System.Serializable]
    public class SaveData
    {
        public string SaveKey => _saveKey;
        
        [SerializeField] private string _saveKey;

        public SaveData(ISavable savable)
        {
            _saveKey = savable.GetSaveKey();
        }
    }
}