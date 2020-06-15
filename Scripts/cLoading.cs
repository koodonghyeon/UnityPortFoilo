using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//로딩씬
public class cLoading : MonoBehaviour
{
    public static string nextScene;
    public Image _LoadingBar;
    public Text _LoadingText;

    private void Start()
    {
        cGameManager.GetInstance.SetBackGruond(BackGroundSound.GameStart);
        _LoadingBar.fillAmount = 0;
    StartCoroutine(LoadScene());
}
    public static void LoadScene(string SceneName)
    {
        nextScene = SceneName;
        SceneManager.LoadScene("LoadingScene");
    }
    IEnumerator LoadScene()

    {
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;
        float timer = 0.0f;
        while (!op.isDone)
        {
            yield return null;
            timer += Time.deltaTime;
            if (op.progress < 0.9f)
            {
                _LoadingBar.fillAmount = Mathf.Lerp(_LoadingBar.fillAmount, op.progress, timer);

                if (_LoadingBar.fillAmount >= op.progress)
                {
                    timer = 0f;
                }
            }

            else
            {
                _LoadingBar.fillAmount = Mathf.Lerp(_LoadingBar.fillAmount, 1f, timer);
                if (_LoadingBar.fillAmount == 1.0f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                    

                }

            }

        }

    }
}
