using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine.SceneManagement;

namespace Scenes
{
    public class SceneLoader
    {
        public IObservable<Unit> LoadScenes(IEnumerable<Scene> scenes)
        {
            var sceneLoadStream = Observable.ReturnUnit();

            foreach (var scene in scenes)
            {
                sceneLoadStream = sceneLoadStream.ContinueWith(LoadScene(scene));
            }

            return sceneLoadStream;
        }
        
        public IObservable<Unit> LoadScene(Scene scene)
        {
            return LoadScene(scene.SceneName, scene.SceneMode);
        }

        public IObservable<Unit> LoadScene(string sceneName, LoadSceneMode sceneMode = LoadSceneMode.Single)
        {
            return Observable.FromCoroutine<Unit>((observer) => LoadSceneCoroutine(sceneName, sceneMode, observer));
        }

        private IEnumerator LoadSceneCoroutine(string sceneName, LoadSceneMode sceneMode, IObserver<Unit> observer)
        {
            if (SceneManager.GetSceneByName(sceneName).isLoaded)
            {
                observer.OnNext(Unit.Default);
                observer.OnCompleted();
                yield break;
            }

            var loadOperation = SceneManager.LoadSceneAsync(sceneName, sceneMode);

            while (!loadOperation.isDone)
                yield return null;

            observer.OnNext(Unit.Default);
            observer.OnCompleted();
        }
    }
}