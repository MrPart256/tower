using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes
{
    [System.Serializable]
    public class Scene
    {
      [field: SerializeField]  public string SceneName { get; private set; }
      [field: SerializeField]  public LoadSceneMode SceneMode {get; private set;}
    }
}