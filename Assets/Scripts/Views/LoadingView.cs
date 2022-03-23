using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class LoadingView : MonoBehaviour
{

    [SerializeField]
    private Slider progressBar;
    [SerializeField]
    private TextMeshProUGUI MsgTxt;

    private float targetProgess = 0;
    public float FillSpeed = 0.0002f;
    void Start()
    {
        StartCoroutine(LoadScene_Async("Game_Scene"));
    }

    void Update()
    {
        if(progressBar.value < targetProgess)
        {
            Debug.Log("Progress :" + progressBar.value);
            progressBar.value += FillSpeed + Time.deltaTime;
        }
    }


    public void IncrementProgess(float newProgess)
    {
        targetProgess = progressBar.value + newProgess;
        //targetProgess = newProgess;
    }

    IEnumerator LoadScene_Async(string sceneName)
    {
        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            Debug.Log("ProgressB :" + asyncOperation.progress);
            IncrementProgess(asyncOperation.progress);
            MsgTxt.text = asyncOperation.progress * 100 + "%";
            if (asyncOperation.progress >= 0.9f)
            {
                MsgTxt.text = "Press the SPACE bar to continue...";
                if (Input.GetKeyDown(KeyCode.Space))
                    asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
