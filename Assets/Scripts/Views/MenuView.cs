using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuView : MonoBehaviour
{
    public delegate void OnButtonCliked();

    public OnButtonCliked OnStartGameClicked;

    [SerializeField]
    private Button startGameBtn;

    [SerializeField]
    private Button BtnChangeLanguage;

    [SerializeField]
    private TextMeshProUGUI MsgTxt;

    private void Awake()
    {
        UpDateText();
    }

    public void UpDateText()
    {
        MsgTxt.text =
            LanguageManager.Instance.GetSentence("Press_start_to_play_game");

        //set TextMeshPro in startGameBtn
        startGameBtn.GetComponentInChildren<TextMeshProUGUI>().text =
            LanguageManager.Instance.GetSentence("Start_Game");
        BtnChangeLanguage.GetComponentInChildren<TextMeshProUGUI>().text =
            LanguageManager.Instance.GetSentence("Change_Language");
    }

    public void Init()
    {
        SetStartBtnEvent();
        SetChangeLanguageBtnEvent();
    }

    void Update()
    {
    }

    void SetStartBtnEvent()
    {
        startGameBtn.onClick.RemoveAllListeners();
        startGameBtn.onClick.AddListener (OnLogin);
    }

    void SetChangeLanguageBtnEvent()
    {
        BtnChangeLanguage.onClick.RemoveAllListeners();
        BtnChangeLanguage.onClick.AddListener (ChangeLanguage);
    }

    void OnLogin()
    {
        Debug.Log("Clicked on start game Button.");
        OnStartGameClicked?.Invoke();
        StartCoroutine(OnStartGameDelay());
    }

    void ChangeLanguage()
    {
        Debug.Log("Clicked on Change Language Button.");

        // LanguageManager.Instance.SetLanguage(Language.Japanese);
        LanguageManager.Instance.ChangeLanguage();

        UpDateText();
    }

    IEnumerator OnStartGameDelay()
    {
        startGameBtn.interactable = false;
        yield return new WaitForSeconds(5.0f);
        startGameBtn.interactable = true;
    }

    public void SetMessage(string _msg)
    {
        MsgTxt.text = _msg;
    }
}
