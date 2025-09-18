using System;
using UniRx;
using UnityEngine;

namespace Input
{
    public class UserInputController : IInputController , IDisposable
    {
        public Vector2 MousePosition => UnityEngine.Input.mousePosition;

        public IObservable<Unit> OnDragBegin => _onDragBegin;
        public IObservable<Unit> OnDrag => _onDrag;
        public IObservable<Unit> OnDragEnd => _onDragEnd;
        
        private readonly Subject<Unit> _onDragBegin = new Subject<Unit>();
        private readonly Subject<Unit> _onDrag = new Subject<Unit>();
        private readonly Subject<Unit> _onDragEnd = new Subject<Unit>();
        
        protected void BeginDrag()
        {
            _onDragBegin.OnNext(Unit.Default);
        }

        protected void Drag()
        {
            _onDrag.OnNext(Unit.Default);
        }
        
        protected void EndDrag()
        {
            _onDragEnd.OnNext(Unit.Default);
        }

        public void Dispose()
        {
            _onDragBegin.Dispose();
            _onDrag.Dispose();
            _onDragEnd.Dispose();
        }
    }
}