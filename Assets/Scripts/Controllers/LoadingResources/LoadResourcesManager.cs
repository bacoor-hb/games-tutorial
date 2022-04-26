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
    [DllImport("__Internal")]
    private static extern string GetAllCacheAssetBundle(string nameDB);
    [DllImport("__Internal")]
    private static extern string ReceiveString();

    //dictionary AssetBundle
    public Dictionary<string, AssetBundle> assetBundleDictionary = new Dictionary<string, AssetBundle>();

    /// <summary>
    /// Tai asset bunble tu folder resource
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public Object LoadAssetBundleFromFolder(string path)
    {
        Object ojb = Resources.Load<Object>(path);
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
        if (assetBundleDictionary.ContainsKey(nameFile))
        {
            Object ojb = assetBundleDictionary[nameFile].LoadAsset(nameAsset);
            Instantiate(ojb);
            return true;
        }
        else
        {
            Debug.Log("chua tai asset Bundle");
            return false;
        }


    }
    public void CheckAssetBundleHasInCache(string nameFile)
    {
        //string pathAssetBundle = Application.persistentDataPath + $"/UnityCache/Shared/{nameFile}";
        //Debug.Log(GetAllCacheAssetBundle("/idbfs")) ;
       Debug.Log("[C#] ReceiveString: " + ReceiveString());


        //foreach (string x in arrayAssetBundle)
        //{
        //    if (pathAssetBundle.Contains(x))
        //    {
        //        Debug.Log("AssetBundle has in cache");
        //    }
        //    else
        //    {
        //        Debug.Log("AssetBundle not has in cache");
        //    }
        //}



    }

}
