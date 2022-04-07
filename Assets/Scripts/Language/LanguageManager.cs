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
    public bool isFinishLoadLanguage = false;
    string StoryTellingFilePath = "Resources/storyTelling.xml";


    IEnumerator LoadFileWeb(string url)
    {
        isFinishLoadLanguage = false;

        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {

                langReader = new LanguageReader(www.downloadHandler.text, lang.Value, true);
                isFinishLoadLanguage = true;
                LoadedLanguageFile();
            }

        }

    }
    IEnumerator LoadFileLocal()
    {
        isFinishLoadLanguage = false;
        langReader =
            new LanguageReader(Path.Combine(Application.dataPath, langFilePath),
                lang.Value,
                false);

        yield return langReader;
        LoadedLanguageFile();
        isFinishLoadLanguage = true;


    }
    IEnumerator LoadFileLocalMore()
    {
        isFinishLoadLanguage = false;
        LoadMoreFilePathLocal(StoryTellingFilePath);
        yield return null;
        isFinishLoadLanguage = true;


    }

    protected void Awake()
    {
        // load from Resource 
        StartCoroutine(LoadFileLocal());

        //load from server
        //StartCoroutine(LoadFileWeb(pathLanguage));

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
    public void LoadMoreFilePathLocal(string filePath)
    {
        langReader.LoadMoreFilePathLocal(Path.Combine(Application.dataPath, filePath), lang.Value);

    }
 


    void OnStartLoadingLanguageFile()
    {
        Debug.Log("Loading language file...");
    }

    void OnEndLoadingLanguageFile()
    {
        Debug.Log("Loaded language file...");

    }
    void LoadedLanguageFile()
    {
        languageView.Init();
        SetUpEventLanguage();
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
