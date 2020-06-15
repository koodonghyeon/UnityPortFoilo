using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//씬전환 매니저
public class cSceneManager : cSingleton<cSceneManager>
{
    public void ChangeScene(int sceneIndex,
        System.Action<AsyncOperation, bool> callback,
        float delay = 0f,
        LoadSceneMode loadsceneMode = LoadSceneMode.Single)
    {
        var scenePath = SceneUtility.GetScenePathByBuildIndex(sceneIndex);

    }


    public void ChangeScene(string SceneName,
        System.Action<AsyncOperation, bool> callback,
        float delay = 0f,
        LoadSceneMode loadSceneMode = LoadSceneMode.Single)
    {

        StartCoroutine(this.DoChangeScene(SceneName,
            callback,
            delay,
            loadSceneMode));
    }

    //씬을 전환한다.
    private IEnumerator DoChangeScene(string sceneName,
        System.Action<AsyncOperation, bool> callBack, 
        float delay,
        LoadSceneMode loadsceneMode)
    {
        yield return new WaitForSeconds(delay);

        var AsyncOperation = SceneManager.
            LoadSceneAsync(sceneName, loadsceneMode);

    }



}
