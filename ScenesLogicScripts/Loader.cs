using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
static class Loader 
{
    public  class LoadingMonobehavior : MonoBehaviour {
        private void Start()
        {
            Loader.currentScene = SceneManager.GetActiveScene().ToString();
        }
    }
    public static string currentScene;
    public enum Scene
    {
        FloorOne,
        MainMenu,
        LoadScreen,
        Test,
        FloorTwo,
        FloorThree,
        CurraptedFloorTwo,
        SquereBefore,
        CurraptedFloorOne,
        FinalScene, Current, CutScene
    }
    private static Action onLoaderCallback;
    private static AsyncOperation loadingAcyncOperation;
   public static void LoadScene(Scene scene)
    {
        Debug.Log("started loading");
        onLoaderCallback = () =>
        {
            GameObject loadingGameObject = new GameObject("LoadingGameObject");
            loadingGameObject.AddComponent<LoadingMonobehavior>().StartCoroutine(LoadingScreenAsync(scene));
            currentScene = scene.ToString();
            SceneManager.LoadScene(scene.ToString());

        };
        SceneManager.LoadScene(Scene.LoadScreen.ToString());

    }
    public static void LoadWithString(string sceneName)
    {
        onLoaderCallback = () =>
        {
            GameObject loadingGameObject = new GameObject("LoadingGameObject");
            loadingGameObject.AddComponent<LoadingMonobehavior>().StartCoroutine(LoadingScreenAsyncWintString(sceneName));
            SceneManager.LoadScene(sceneName);

        };
        SceneManager.LoadScene(Scene.LoadScreen.ToString());

    }
    private static IEnumerator LoadingScreenAsync(Scene scene)
    {
        yield return null;
        loadingAcyncOperation = SceneManager.LoadSceneAsync(scene.ToString());
        while (!loadingAcyncOperation.isDone)
        {
            yield return null;
        }
    }
    private static IEnumerator LoadingScreenAsyncWintString(string scene)
    {
        yield return null;
        loadingAcyncOperation = SceneManager.LoadSceneAsync(scene);
        while (!loadingAcyncOperation.isDone)
        {
            yield return null;
        }
    }
    public static float GetLoadingProgerss()
    {
        if(loadingAcyncOperation != null)
        {
            return loadingAcyncOperation.progress;
        }
        else
        {
            return 1f;
        }
    }
    public static void LoaderCallback()
    {
        
        if(onLoaderCallback != null)
        {
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }

}
