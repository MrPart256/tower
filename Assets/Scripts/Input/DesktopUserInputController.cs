using UnityEngine;
using Zenject;

namespace Input
{
    public class DesktopUserInputController : UserInputController, ITickable
    {
        private const float DragThreshold = 0.01f;
        private bool _isDragging = true;
        private Vector2 _previousMousePosition;

        public void Tick()
        {
            if (UnityEngine.Input.GetMouseButton(0))
            {
                if (!_isDragging && (MousePosition - _previousMousePosition).normalized.magnitude > DragThreshold)
                {
                    BeginDrag();
                    _isDragging = true;
                }
                else
                {
                    Drag();
                }
                _previousMousePosition = MousePosition;
            }

            if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                if (_isDragging)
                {
                    EndDrag();
                    _isDragging = false;
                }
            }
        }
    }
}