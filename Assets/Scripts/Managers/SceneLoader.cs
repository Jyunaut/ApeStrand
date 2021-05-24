using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{
    // Do we even need different scenes for this game?
    public static class SceneLoader
    {
        private class LoadingMonoBehaviour : MonoBehaviour {}

        public enum Scene
        {
            Main,
            Title,
            Win,
            Lose,
            Loading
        }

        private static Action onLoaderCallback;

        public static void LoadScene(Scene scene)
        {
            onLoaderCallback = () => 
            {
                GameObject loadingGameObject = new GameObject("Loading Game Object");
                loadingGameObject.AddComponent<LoadingMonoBehaviour>().StartCoroutine(LoadSceneAsync(scene));
            };
            SceneManager.LoadScene(Scene.Loading.ToString());
        }

        public static void LoaderCallback()
        {
            onLoaderCallback?.Invoke();
            onLoaderCallback = null;
        }

        private static IEnumerator LoadSceneAsync(Scene scene)
        {
            yield return null;
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scene.ToString());
            while (!asyncOperation.isDone)
                yield return null;
        }
    }
}