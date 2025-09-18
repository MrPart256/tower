using UnityEngine;

namespace Hole
{
    public class Hole : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;

        public Vector2 GetRadius()
        {
            return new Vector2(_rectTransform.rect.width * _rectTransform.lossyScale.x / 2, _rectTransform.rect.height * _rectTransform.lossyScale.y / 2);
        }

        public Vector2 GetCenter()
        {
            return _rectTransform.position;
        }
    }
}