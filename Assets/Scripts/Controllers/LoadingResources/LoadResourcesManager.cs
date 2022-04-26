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
    
    public String[] arrayKeyIndexedDB;
    delegate bool GetKeyIndexedDBCallbackDelegate(int iteration, string timeString);

    [MonoPInvokeCallback(typeof(GetKeyIndexedDBCallbackDelegate))]
    private static bool GetKeyIndexedDBCallback(int iteration, string arrayKeyString)
    {
        Instance.arrayKeyIndexedDB = arrayKeyString.Split(',');
        return iteration < 1;
    }

    [DllImport("__Internal")]
    private static extern void GetKeyIndexedDB(GetKeyIndexedDBCallbackDelegate getKeyIndexedDBCallback);
 
    [SerializeField]
    private LoadAssetBundle loadAssetBundle;

    [DllImport("__Internal")]
    
    private static extern void Clear(string nameDB, string pathDB);
    [DllImport("__Internal")]
    private static extern string GetAllCacheAssetBundle(string nameDB);

    //dictionary AssetBundle
    public Dictionary<string, AssetBundle> assetBundleDictionary = new Dictionary<string, AssetBundle>();

    /// <summary>
    /// Tai asset bunble tu folder resource
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public GameObject LoadAssetBundleFromFolder(string path)
    {
        GameObject ojb = Resources.Load<GameObject>(path);
        return ojb;
    }
    /// <summary>
    /// Tai asset bundle tu url bang ten file(tag)
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
    /// xoa cache asset bundle tu web
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
    /// </summary>
    /// <param name="nameFile"></param>
    public void CheckAssetBundleHasInCache(string nameFile)
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
                        return;
                    }
                }
                Debug.Log("AssetBundle chua co trong cache");

            }
            else
            {
                Debug.Log("Chua get list key tu IndexedDB hoac IndexedBD dang rong");
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex.Message);
        }
       



    }
    /// <summary>
    ///     Get List Key from IndexedDB
    /// </summary>
    public void GetListKey()
    {
        GetKeyIndexedDB(GetKeyIndexedDBCallback);
    }

}
