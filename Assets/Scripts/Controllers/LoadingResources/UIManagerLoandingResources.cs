using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManagerLoandingResources : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Button buttonResourcesFolder;
    [SerializeField]
    Button buttonAssetBundle;
    [SerializeField]
    Button buttonClear;
    void Start()
    {
        buttonResourcesFolder.onClick.AddListener(() =>
        {
            Instantiate(LoadingResourcesManager.Instance.LoadcharacterResourcesFolder("Dice") as GameObject);
        }
        );
        buttonAssetBundle.onClick.AddListener(() =>
        {
            LoadingResourcesManager.Instance.LoadAssetBundle();
        }
        );
        buttonClear.onClick.AddListener(() =>
        {
            LoadingResourcesManager.Instance.ClearCacheIndexedDB("ab");
        }
       );
    }

    // Update is called once per frame

}
