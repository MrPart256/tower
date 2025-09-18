using UnityEngine;

namespace Hole
{
    public class HoleController
    {
        private readonly Hole _hole;

        public HoleController(Hole hole)
        {
            _hole = hole;
        }

        public bool IsInsideHole(Vector2 position)
        {
            var radius = _hole.GetRadius();

            var center = _hole.GetCenter();

            return new Vector2((position.x - center.x) / radius.x, (position.y - center.y) / radius.y).magnitude <= 1;
        }

        public Vector2 GetCenter()
            => _hole.GetCenter();
    }
}