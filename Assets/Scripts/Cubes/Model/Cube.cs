using Serialization;
using UnityEngine;

namespace Cubes.Model
{
    [System.Serializable]
    public class Cube
    {
        public int ID => _id;
        public int MapperId => _mapperId;

        [SerializeField] private int _mapperId;
        [SerializeField] private int _id;
        [SerializeField] private SerializableVector2 _position;

        public Cube(int id, int mapperId)
        {
            _id = id;
            _mapperId = mapperId;
        }

        public void UpdatePosition(Vector2 newPosition)
        {
            _position = newPosition;
        }

        public Vector2 GetPosition()
            => _position;
    }
}