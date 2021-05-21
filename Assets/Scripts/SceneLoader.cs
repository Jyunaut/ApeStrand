using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{

    private class LoadingMonoBehaviour : MonoBehaviour {}

    public enum Scene {
        Main,
        Loading
    }

    private static Action onLoaderCallback;
    // private static AsyncOperation loadingAsyncOperation;

    public static void Load(Scene scene) {
        // Set the loader callback action to load the target scene
        onLoaderCallback = () => {
            // GameObject loadingGameObject = new GameObject("Loading Game Object");
            // loadingGameObject.AddComponent<LoadingMonoBehaviour>().StartCoroutine(LoadSceneAsync(scene));
            SceneManager.LoadScene(scene.ToString());
        };

        // Load the loading scene
        SceneManager.LoadScene(Scene.Loading.ToString());
    }

    // private static IEnumerator LoadSceneAsync(Scene scene) {
    //     yield return null;

    //     AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scene.ToString());

    //     while(!asyncOperation.isDone) {
    //         yield return null;
    //     }
    // }

    // public static float GetLoadingProgress() {
    //     if (loadingAsyncOperation != null) {
    //         return loadingAsyncOperation.progress;
    //     } else {
    //         return 1f;
    //     }
    // }

    public static void LoaderCallBack() {
        if (onLoaderCallback != null) {
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }
}