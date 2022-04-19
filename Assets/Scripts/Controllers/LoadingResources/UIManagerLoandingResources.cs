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
    [SerializeField]
    Button buttonAddressables;
    void Start()
    {
        buttonResourcesFolder.onClick.AddListener(() =>
        {
           Instantiate( LoadingResourcesManager.Instance.LoadcharacterResourcesFolder("Dice") as GameObject);
        }
        );
        buttonAssetBundle.onClick.AddListener(() =>
        {
            LoadingResourcesManager.Instance.LoadAssetBundle();
        }
        );
        buttonClear.onClick.AddListener(() =>
        {
            LoadingResourcesManager.Instance.ClearCacheIndexedDB();
        }
       );
        buttonAddressables.onClick.AddListener(() =>
        {
            LoadingResourcesManager.Instance.LoadAddressables();
        }
    );
    }

    // Update is called once per frame

}
