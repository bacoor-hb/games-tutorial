using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;



public class LanguageView : MonoBehaviour
{
    [SerializeField]
    private GameObject LanguageCanvas;
    [SerializeField]
    private TextMeshProUGUI LanguageText;
    [SerializeField]

private Button BtnEnglish;
    [SerializeField]

    private Button BtnJapanese;
    [SerializeField]

    private Button BtnClose;

    void Start()
    {
        
    }

    void Update()
    {
    }


    public void Init()
    {
        SetBtnEvent();
        UpdateUI();
    }

    

    public void SetCanvasStatus(bool status)
    {
        if (LanguageCanvas != null)
        {
            LanguageCanvas.SetActive(status);
        }
    }
    public void UpdateUI(){
        Language lang = GlobalManager.Instance.languageManager.GetLanguage();
        if (Language.English.Value == lang.Value)
        {
            //change color of button
            BtnEnglish.GetComponent<Image>().color = Color.green;
        }else{
            BtnEnglish.GetComponent<Image>().color = Color.white;
        }
         if(Language.Japanese.Value == lang.Value){
            BtnJapanese.GetComponent<Image>().color = Color.green;
        }else{
            BtnJapanese.GetComponent<Image>().color = Color.white;
        }
        
        LanguageText.text = GlobalManager.Instance.languageManager.GetSentence(TEXT_UI.LANGUAGE);
        BtnEnglish.GetComponentInChildren<TextMeshProUGUI>().text=GlobalManager.Instance.languageManager.GetSentence(TEXT_UI.ENGLISH);
        BtnJapanese.GetComponentInChildren<TextMeshProUGUI>().text = GlobalManager.Instance.languageManager.GetSentence(TEXT_UI.JAPANESE);
BtnClose.GetComponentInChildren<TextMeshProUGUI>().text = GlobalManager.Instance.languageManager.GetSentence(TEXT_UI.CLOSE);

    }
    public void SetBtnEvent(){
        BtnEnglish.onClick.RemoveAllListeners();
        BtnEnglish.onClick.AddListener(OnEnglish);
        BtnJapanese.onClick.RemoveAllListeners();
        BtnJapanese.onClick.AddListener(OnJapanese);
        BtnClose.onClick.RemoveAllListeners();
        BtnClose.onClick.AddListener(OnClose);
       
    }
    void OnEnglish(){
        Language lang = GlobalManager.Instance.languageManager.GetLanguage();
        if (Language.English.Value != lang.Value)
        {
            GlobalManager.Instance.languageManager.SetLanguage(Language.English);
            UpdateUI();
        }
      
    }
    void OnJapanese(){
        Language lang = GlobalManager.Instance.languageManager.GetLanguage();
        if (Language.Japanese.Value != lang.Value)
        {
            GlobalManager.Instance.languageManager.SetLanguage(Language.Japanese);
            UpdateUI();
        }
          
    }
    void OnClose()
    {
        SetCanvasStatus(false);

       
        Scene CurrentScene = SceneManager.GetActiveScene();
        //reload CurrentScene
        SceneManager.LoadScene(CurrentScene.name);

    }
}
