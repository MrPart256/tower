using System;
using UniRx;

namespace Initialization
{
    public interface IInitializer
    {
        public IObservable<Unit> Initialize();
    }
    public interface IInitializer<in T>
    {
        public IObservable<Unit> Initialize(T param);
    }

    public interface IInitializer<in T1, in T2>
    {
        public IObservable<Unit> Initialize(T1 param1, T2 param2);
    }
}