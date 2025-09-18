using UnityEngine;

namespace Scenes
{
    [CreateAssetMenu]
    public class SceneCollection : ScriptableObject
    {
        [field: SerializeField] public Scene[] Scenes { get; private set; }
    }
}