using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalManager : Singleton<GlobalManager>
{
    private void Awake()
    {
        LanguageManager languageManager;
        languageManager = FindObjectOfType<LanguageManager>();
    }

    void Start()
    {
        SceneManager.LoadScene("Scenes/Test_Scene/Menu_Scene");
    }

    void Update()
    {
    }
}
