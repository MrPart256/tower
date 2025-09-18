using UnityEngine;
using Cubes;

namespace Config
{
    [CreateAssetMenu]
    public class ConfigRepository : ScriptableObject
    {
        [field: SerializeField] public CubesConfig CubesConfig { get; private set; }
    }
}