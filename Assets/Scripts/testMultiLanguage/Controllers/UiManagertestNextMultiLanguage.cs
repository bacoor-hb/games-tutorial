using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UiManagertestNextMultiLanguage : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Text text;
    [SerializeField]
    private Button BtnBack;
    void Start()
    {
        text.text = LanguageManager.Instance.GetSentence("main_menu");
                BtnBack.GetComponentInChildren<Text>().text =LanguageManager.Instance.GetSentence("exit");
        BtnBack.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("testMultiLanguage");

        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
