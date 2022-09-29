using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using System.Runtime.InteropServices;
using AOT;
using System;

public class LoadResourcesManager : Singleton<LoadResourcesManager>
{

    private void Start()
    {
        Debug.Log(" Application.persistentDataPath" + Application.persistentDataPath);
    }
    public String[] arrayKeyIndexedDB;

    [DllImport("__Internal")]
    public static extern string GetKeyIndexedDB(string gameObjectName, string callback, string fallback);

    [SerializeField]
    private LoadAssetBundle loadAssetBundle;

    [DllImport("__Internal")]
    private static extern void Clear(string nameDB, string pathDB);

    //dictionary AssetBundle
    public Dictionary<string, AssetBundle> assetBundleDictionary = new Dictionary<string, AssetBundle>();

    /// <summary>
    /// load asset bunble form folder resource
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public GameObject LoadAssetBundleFromFolder(string path)
    {
        GameObject ojb = Resources.Load<GameObject>(path);
        return ojb;
    }
    /// <summary>
    /// load asset bundle feom url by nameFile(tag)
    /// </summary>
    /// <param name="nameFile"></param>
    public bool LoadAssetBundleFromUrlByName(string nameFile)
    {
        try
        {
            loadAssetBundle.Load(nameFile);
            return true;
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex.Message);
            return false;
        }



    }
    /// <summary>
    /// cleare cache asset bundle from web (indexedDB)
    /// </summary>
    /// <param name="nameFile"></param>
    public bool ClearCacheIndexedDB(string nameFile)
    {
        // try catch function Clear
        try
        {

            Clear("/idbfs", Application.persistentDataPath + $"/UnityCache/Shared/{nameFile}");
            return true;
        }
        catch (System.Exception)
        {
            Debug.Log("Error Clear Cache IndexedDB");
            return false;
        }


    }
    /// <summary>
    /// Load Asset Async From Asset Bundle Cache(IndexedDB)
    /// </summary>
    /// <param name="nameFile"></param>
    /// <param name="nameAsset"></param>
    public bool LoadAssetAsyncFromAssetBundleDictionary(string nameFile, string nameAsset)
    {
        try {
            if (assetBundleDictionary.ContainsKey(nameFile))
            {
                GameObject ojb = assetBundleDictionary[nameFile].LoadAsset(nameAsset) as GameObject;
                Instantiate(ojb);
                return true;
            }
            else
            {
                Debug.Log("chua tai asset Bundle");
                return false;
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex.Message);
            return false;
        }
       


    }
    /// <summary>
    ///Check Asset Bundle Has In Cache
    ///--NOTE: must get keys from indexedDB (func GetListKey()) before use
    /// </summary>
    /// <param name="nameFile"></param>
    public bool CheckAssetBundleHasInCache(string nameFile)
    {
        try
        {
            if (arrayKeyIndexedDB.Length > 0)
            {
                for (int i = 0; i < arrayKeyIndexedDB.Length; i++)
                {
                    if (arrayKeyIndexedDB[i] == Application.persistentDataPath + $"/UnityCache/Shared/{nameFile}")
                    {
                        Debug.Log("AssetBundle da co trong cache");
                        return true;
                    }
                }
                Debug.Log("AssetBundle chua co trong cache");
                return false;

            }
            else
            {
                Debug.Log("Chua get list key tu IndexedDB hoac IndexedBD dang rong");
                return false;
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex.Message);
            return false;

        }
    }

    /// <summary>
    /// get list key from IndexedDB 
    /// </summary>
    public void GetListKey()
    {
        GetKeyIndexedDB(gameObject.name, nameof(GetKeyIndexedDBComplete), nameof(DisplayError));
    }
    /// <summary>
    /// After func GetListKey() was finished, if it was success then would run func GetKeyIndexedDBComplete with return param "keys"-list key 
    /// </summary>
    /// <param name="keys"></param>
    public void  GetKeyIndexedDBComplete( string keys)
    {
        Debug.Log("keys " + keys);

        arrayKeyIndexedDB = keys.Split(',');
        Debug.Log("arrayKeyIndexedDB " + arrayKeyIndexedDB.Length);
    }
    /// <summary>
    /// After func GetListKey() was finished, if it was error then would run func DisplayError 
    /// 
    /// </summary>
    /// <param name="errorMessage"></param>
    public void DisplayError(string errorMessage)
    {
        Debug.Log("errorMessage " + errorMessage);

    }

}
