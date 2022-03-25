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
    private TextMeshProUGUI MsgTxt;

    [SerializeField]
    private Button BtnEnglish;

    [SerializeField]
    private Button BtnJapanese;

    public void Init()
    {
        SetStartBtnEvent();
        SetLanguageBtnEvent();
        UpdateUI();
    }

    void Update()
    {
    }

    void SetStartBtnEvent()
    {
        startGameBtn.onClick.RemoveAllListeners();
        startGameBtn.onClick.AddListener (OnLogin);
    }

    void SetLanguageBtnEvent()
    {
        BtnJapanese.onClick.RemoveAllListeners();
        BtnJapanese
            .onClick
            .AddListener(() =>
            {
                LanguageManager.Instance.SetLanguage(Language.Japanese);
                UpdateUI();
            });
        BtnEnglish.onClick.RemoveAllListeners();
        BtnEnglish
            .onClick
            .AddListener(() =>
            {
                LanguageManager.Instance.SetLanguage(Language.English);
                UpdateUI();
            });
    }

    void OnLogin()
    {
        Debug.Log("Clicked on start game Button.");
        OnStartGameClicked?.Invoke();
        StartCoroutine(OnStartGameDelay());
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

    void UpdateUI()
    {
        startGameBtn.GetComponentInChildren<TextMeshProUGUI>().text =
            LanguageManager.Instance.GetSentence("start_game");
        MsgTxt.text =
            LanguageManager.Instance.GetSentence("press_start_to_play_game");
    }
}
