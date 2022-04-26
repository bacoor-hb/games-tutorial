using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManagerLoadResources : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Button buttonResourcesFolder;
    [SerializeField]
    Button buttonAssetBundle;
    [SerializeField]
    Button buttonClear;
    [SerializeField]
    Button buttonLoad;
    [SerializeField]
    Button buttonCheck;
    [SerializeField]
    Button buttonGetList;
    void Start()
    {
        buttonResourcesFolder.onClick.AddListener(() =>
        {
            Instantiate(LoadResourcesManager.Instance.LoadAssetBundleFromFolder("Dice") as GameObject);

        }
        );
        buttonAssetBundle.onClick.AddListener(() =>
        {
            LoadResourcesManager.Instance.LoadAssetBundleFromUrlByName("ab");
        }
        );
        buttonClear.onClick.AddListener(() =>
        {
            LoadResourcesManager.Instance.ClearCacheIndexedDB("ab");

        });
        buttonLoad.onClick.AddListener(() =>
        {
            LoadResourcesManager.Instance.LoadAssetAsyncFromAssetBundleDictionary("ab", "Assault_Rifle_01");
            //LoadResourcesManager.Instance.CheckAssetBundleHasInCache("ab");

        });
        buttonCheck.onClick.AddListener(() =>
        {
            LoadResourcesManager.Instance.CheckAssetBundleHasInCache("ab");

        } );
        buttonGetList.onClick.AddListener(() =>
         {
             LoadResourcesManager.Instance.GetListKey();

         }
         );

    }

    // Update is called once per frame

}
