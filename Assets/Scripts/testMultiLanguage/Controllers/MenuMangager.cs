using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuMangager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private Button btnStart;

    [SerializeField]
    private Text text1;

    [SerializeField]
    private Text text2;

    [SerializeField]
    private Text text3;

    //private string langSelected;

    void Start()
    {
        UpDateText();
       
    }


     public void UpDateText() {
        text1.text = LanguageManager.Instance.GetSentence("main_menu"); 
        text2.text = LanguageManager.Instance.GetSentence("continue");
        text3.text = LanguageManager.Instance.GetSentence("main_menu");
        btnStart.GetComponentInChildren<Text>().text =
           LanguageManager.Instance.GetSentence("start_new");
    }
    public void PressBtnStart()
    {
        btnStart
                   .onClick
                   .AddListener(() =>
                   {
                       SceneManager.LoadScene("testNextScenesMultiLanguage");

            });
    }
}
