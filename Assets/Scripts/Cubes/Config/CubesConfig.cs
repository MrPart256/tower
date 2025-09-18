using UnityEngine;

namespace Cubes
{
    [CreateAssetMenu]
    public class CubesConfig : ScriptableObject
    {
        [field:SerializeField] public CubeMapper[] Cubes;
    }
}