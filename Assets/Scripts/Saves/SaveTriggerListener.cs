using System;
using UniRx;
using Zenject;

namespace Saves
{
    public abstract class SaveTriggerListener : IInitializable, IDisposable
    {
        protected CompositeDisposable _trackStream = new();
        
        private readonly SaveSystemFacade _saveSystemFacade;

        protected SaveTriggerListener(SaveSystemFacade saveSystemFacade)
        {
            _saveSystemFacade = saveSystemFacade;
        }

        public abstract void Initialize();

        protected void Save()
        {
            _saveSystemFacade.Save();
        }

        public void Dispose()
        {
            _trackStream.Dispose();
        }
    }
}