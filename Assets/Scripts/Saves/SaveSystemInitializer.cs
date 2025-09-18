using System;
using Initialization;
using UniRx;

namespace Saves
{
    public class SaveSystemInitializer : IInitializer
    {
        private readonly SaveSystemFacade _saveSystemFacade;
        private readonly SaveTriggersInitializer _saveTriggersInitializer;

        public SaveSystemInitializer(SaveSystemFacade saveSystemFacade
            , SaveTriggersInitializer saveTriggersInitializer)
        {
            _saveSystemFacade = saveSystemFacade;
            _saveTriggersInitializer = saveTriggersInitializer;
        }

        public IObservable<Unit> Initialize()
        {
            return _saveSystemFacade.Load()
                .ContinueWith(_=> _saveTriggersInitializer.Initialize())
                .Select(_ => Unit.Default);
        }
    }
}