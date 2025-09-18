using UnityEngine;

namespace Cubes.PlacementZone
{
    public class PlacementZone : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;

        public RectTransform GetBounds()
            => _rectTransform;
    }
}