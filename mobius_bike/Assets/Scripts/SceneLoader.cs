using CodeSisters.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    public void LoadCosmos()
    {
        StartCoroutine(UnloadSceneAsync("Gameplay_Menu"));
        StartCoroutine(LoadSceneAsync("Gameplay_Cosmos"));
    }
    
    public void LoadMenu()
    {
        StartCoroutine(UnloadSceneAsync("Gameplay_Cosmos"));
        StartCoroutine(LoadSceneAsync("Gameplay_Menu"));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Scenes/"+sceneName, LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true;
            }
            
            yield return null;
        }

        Scene scene = SceneManager.GetSceneByName(sceneName);
        if (scene.IsValid())
            SceneManager.SetActiveScene(scene);
    }

    private IEnumerator UnloadSceneAsync(string sceneName)
    {
        if (!SceneManager.GetSceneByName(sceneName).IsValid())
            yield break;
        AsyncOperation asyncLoad = SceneManager.UnloadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
