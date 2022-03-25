using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    [SerializeField]
    private LoadingView LoadingView;
    void Start()
    {
        if (LoadingView != null)
        {
            StartCoroutine(LoadScene_Async("Game_Scene"));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LoadScene_Async(string sceneName)
    {
        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            Debug.Log("ProgressB :" + asyncOperation.progress);
            LoadingView.IncrementProgess(asyncOperation.progress);
            LoadingView.SetMessage(asyncOperation.progress * 100 + "%");
            if (asyncOperation.progress >= 0.9f)
            {
                LoadingView.SetMessage("Press the space bar to continue...");
                if (Input.GetKeyDown(KeyCode.Space))
                    asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
