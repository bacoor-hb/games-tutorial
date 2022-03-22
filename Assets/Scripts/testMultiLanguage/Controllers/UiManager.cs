using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UiManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Button buttonEn;

    [SerializeField]
    private Button buttonJp;

    private LanguageManager LM;
    [SerializeField]

    private MenuMangager MM;

    void Start()
    {
        LM = FindObjectOfType<LanguageManager>();
        MM.PressBtnStart();
        buttonEn
            .onClick
            .AddListener(() =>
            {
                LM.SetLanguage(Language.English);
                MM.UpDateText();
            });
        buttonJp
            .onClick
            .AddListener(() =>
            {
                LM.SetLanguage(Language.Japanese);
                MM.UpDateText();

            });
    }
}
