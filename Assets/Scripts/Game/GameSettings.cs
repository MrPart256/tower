using Scenes;
using UnityEngine;

[CreateAssetMenu]
public class GameSettings : ScriptableObject
{
    [field: SerializeField] public SceneCollection Scenes { get; private set; }
}