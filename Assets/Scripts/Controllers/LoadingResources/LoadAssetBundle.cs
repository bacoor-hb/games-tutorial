using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

public class LoadAssetBundle : MonoBehaviour
{


    public void Load(string nameFile)
    {
        StartCoroutine(LoadAssetBundleFromFile(nameFile));
    }

    IEnumerator LoadAssetBundleFromFile(string nameFile)
    {
        while (!Caching.ready)
            yield return null;


        using (var www = WWW.LoadFromCacheOrDownload($"https://raw.githubusercontent.com/HoDienCong12c5/serverBundle/main/{nameFile}", 5))
        {
            yield return www;
            if (!string.IsNullOrEmpty(www.error))
            {
                Debug.Log(www.error);
                yield return null;
            }


            //check if the asset bundle contains the named object
            if (!LoadResourcesManager.Instance.assetBundleDictionary.ContainsKey(nameFile))
            {
                AssetBundle myLoadedAssetBundle;
                myLoadedAssetBundle = www.assetBundle;
                Debug.Log("Loaded AssetBundle: " + myLoadedAssetBundle);
                LoadResourcesManager.Instance.assetBundleDictionary.Add(nameFile, myLoadedAssetBundle);

            }
        }
    }

}