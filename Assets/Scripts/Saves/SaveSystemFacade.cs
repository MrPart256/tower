using System;
using System.Linq;
using UniRx;

namespace Saves
{
    public class SaveSystemFacade
    {
        private readonly ISaveController _saveController;
        private readonly SavableStorage _savableStorage;
        private readonly SaveProvider _saveProvider;

        public SaveSystemFacade(ISaveController saveController, SavableStorage savableStorage, SaveProvider saveProvider)
        {
            _saveController = saveController;
            _savableStorage = savableStorage;
            _saveProvider = saveProvider;
        }

        public void Add(ISavable savable)
        {
            if(_saveProvider.Contains(savable))
                savable.Load(_saveProvider.GetMemento(savable));
            _savableStorage.Add(savable);
        }

        public void Save()
        {
            _saveController.Save(PrepareSave());
        }

        public IObservable<Save> Load()
        {
            return _saveController.Load()
                .Do(save =>
                {
                    _saveProvider.Set(save);
                    foreach (var savable in _savableStorage.GetAll())
                    {
                        if (_saveProvider.Contains(savable))
                            savable.Load(_saveProvider.GetMemento(savable));
                    }
                });
        }
        
        private Save PrepareSave()
        {
            return new Save(_savableStorage.GetAll().Select(savable => savable.Save()).ToList());
        }
    }
}