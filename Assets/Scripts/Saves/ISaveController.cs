using System;
using UniRx;

namespace Saves
{
    public interface ISaveController
    {
        public void Save(Save save);
        public IObservable<Save> Load();
    }
}