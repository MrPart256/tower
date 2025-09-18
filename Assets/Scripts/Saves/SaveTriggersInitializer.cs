using System;
using System.Collections.Generic;
using Initialization;
using UniRx;

namespace Saves
{
    public class SaveTriggersInitializer : IInitializer
    {
        private readonly IEnumerable<SaveTriggerListener> _listeners;

        public SaveTriggersInitializer(IEnumerable<SaveTriggerListener> listeners)
        {
            _listeners = listeners;
        }

        public IObservable<Unit> Initialize()
        {
            foreach (var listener in _listeners)
            {
                listener.Initialize();
            }

            return Observable.Return(Unit.Default);
        }
    }
}