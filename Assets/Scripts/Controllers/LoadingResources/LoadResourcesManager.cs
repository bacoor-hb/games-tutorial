using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using System.Runtime.InteropServices;



public class LoadResourcesManager : Singleton<LoadResourcesManager>
{
    // Start is called before the first frame update
    [SerializeField]
    private LoadAssetBundle loadAssetBundle;

    [DllImport("__Internal")]
    private static extern void Clear(string nameDB, string pathDB);

    //dictionary AssetBundle
    public Dictionary<string, AssetBundle> assetBundleDictionary = new Dictionary<string, AssetBundle>();

    public Object LoadcharacterResourcesFolder(string path)
    {
        Object ojb = Resources.Load<Object>(path);
        return ojb;
    }
    public void LoadAssetBundle(string nameFile)
    {
        loadAssetBundle.Load(nameFile);


    }
    public void ClearCacheIndexedDB(string nameFile)
    {
        Clear("/idbfs", Application.persistentDataPath + $"/UnityCache/Shared/{nameFile}");
    }
    public void LoadAssetAsyncFromAssetBundleDictionary(string nameFile, string nameAsset)
    {
        if (assetBundleDictionary.ContainsKey(nameFile))
        {
            Object ojb = assetBundleDictionary[nameFile].LoadAsset(nameAsset);
            Instantiate(ojb);
        }
        else
        {
            Debug.Log("chua tai asset Bundle");
        }


    }

}
