using System;
using UniRx;
using UnityEngine;

namespace Input
{
    public interface IInputController
    {
        public Vector2 MousePosition { get; }
        public IObservable<Unit> OnDragBegin { get; }
        public IObservable<Unit> OnDrag { get; }
        public IObservable<Unit> OnDragEnd { get; }
    }
}