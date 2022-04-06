using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


//
public class LanguageManager : MonoBehaviour
{
    const string pathLanguage = "https://raw.githubusercontent.com/bacoor-hb/games-tutorial/duc/multiLanguage%2BManager/Assets/Resources/menuSentences.xml";
    public LanguageView languageView;
    //THE language manager
    LanguageReader langReader;

    //Game language
    Language lang = Language.English;

    //path of the file that the game is reading from
    string langFilePath = "Resources/menuSentences.xml";
    public bool isFinishLoadLanguage =false;
    bool isChangeFinish = true;
    //string StoryTellingFilePath = "Resources/storyTelling.xml";
    
   
    IEnumerator LoadFileWeb(string url)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            Debug.Log("1-LoadFileWeb");
            yield return www.SendWebRequest(); 
            yield return new WaitForSeconds(6);
            isFinishLoadLanguage = true;
            if (isFinishLoadLanguage)
            {
                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log("2-LoadFileWeb-err");
                    Debug.Log(www.error);
                }
                else
                {

                    Debug.Log("2-LoadFileWeb");
                    langReader = new LanguageReader(www.downloadHandler.text, lang.Value, true);
                }
            }
            
        }
    }

    IEnumerator LoadFileLocal()
    {
        langReader =
            new LanguageReader(Path.Combine(Application.dataPath, langFilePath),
                lang.Value,
                false);

        yield return langReader;
        yield return new WaitForSeconds(6);
        isFinishLoadLanguage = true;

    }
    protected  void Awake()
    {
        // load from Resource 
        StartCoroutine(LoadFileLocal());

        //load from server
        //StartCoroutine(LoadFileWeb(pathLanguage));

    }

    void Start()
    {
   

    }
    private void Update()
    {
        if (isChangeFinish && isFinishLoadLanguage)
        {
            languageView.Init();
            SetUpEventLanguage();
             isChangeFinish = false;
        }
    }
    void SetUpEventLanguage()
    {
        langReader.OnStartLoadingLanguageFile += OnStartLoadingLanguageFile;
        langReader.OnEndLoadingLanguageFile += OnEndLoadingLanguageFile;
        langReader.OnStartTranslating += OnStartTranslating;
        langReader.OnEndTranslating += OnEndTranslating;
        languageView.OnCloseClicked += OnCloseClicked;
        languageView.OnEnglishClicked += OnEnglishClicked;
        languageView.OnJapaneseClicked += OnJapaneseClicked;
    }

   

    public void SetLanguageFile(string filePath)
    {
        langReader
            .setLanguage(Path.Combine(Application.dataPath, filePath),
            lang.Value);
        langFilePath = filePath;
    }

    public string GetSentence(string sentenceName)
    {
        return langReader.getString(sentenceName);
    }

    //Set game language
    public void SetLanguage(Language _language)
    {
        langReader
            .setLanguage(Path.Combine(Application.dataPath, langFilePath),
            _language.Value);
        lang = _language;
    }

    //Get game language
    public Language GetLanguage()
    {
        return lang;
    }
    /// <summary>
    /// load More File Path with filePath follow format "Resources/nameFile.xml"
    /// </summary>
    /// <param name="filePath"> </param>
    public void loadMoreFilePath(string filePath)
    {
        langReader.loadMoreFilePath(Path.Combine(Application.dataPath, filePath), lang.Value);

    }


    void OnStartLoadingLanguageFile()
    {
        Debug.Log("Loading language file...");
    }

    void OnEndLoadingLanguageFile()
    {
        Debug.Log("Language file loaded!");
    }

    void OnStartTranslating()
    {
        Debug.Log("Translating...");
    }

    void OnEndTranslating()
    {
        Debug.Log("Translation complete!");
    }
    void OnCloseClicked()
    {
        languageView.SetCanvasStatus(false);


        Scene CurrentScene = SceneManager.GetActiveScene();
        //reload CurrentScene
        SceneManager.LoadScene(CurrentScene.name);

    }
    void OnJapaneseClicked()
    {
        Language lang = GlobalManager.Instance.languageManager.GetLanguage();
        if (Language.Japanese.Value != lang.Value)
        {
            GlobalManager.Instance.languageManager.SetLanguage(Language.Japanese);
            languageView.UpdateUI();
        }
    }
    void OnEnglishClicked()
    {
        Language lang = GlobalManager.Instance.languageManager.GetLanguage();
        if (Language.English.Value != lang.Value)
        {
            GlobalManager.Instance.languageManager.SetLanguage(Language.English);
            languageView.UpdateUI();
        }

    }


}
