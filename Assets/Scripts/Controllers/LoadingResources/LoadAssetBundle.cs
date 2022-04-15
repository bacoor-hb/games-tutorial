using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAssetBundle : MonoBehaviour
{
    string url = "http://192.168.50.165:3000";

    //public void Load()
    //{
    //    Debug.Log("Application.dataPath" + Application.dataPath);
    //    WWW www = new WWW(url);
    //    // while (!www.isDone)
    //    // {
    //    //     Debug.Log("Loading..." + www.progress);
    //    // }
    //    // Debug.Log("Loaded");
    //    StartCoroutine(webReq(www));

    //    // Update is called once per frame
    //}
    //IEnumerator webReq(WWW www)
    //{
    //    yield return www;

    //    while (www.isDone == false)
    //    {
    //        yield return null;

    //    }
    //    AssetBundle bundle = www.assetBundle;
    //    if (www.error == null)
    //    {
    //        GameObject obj = (GameObject)bundle.LoadAsset("board");
    //        Instantiate(obj);
    //    }
    //    else
    //    {
    //        Debug.Log(www.error);
    //    }
    //}
    public void Load()
    {
        StartCoroutine(LoadAB());
    }
    IEnumerator LoadAB()
    {
        while (!Caching.ready)
            yield return null;
        Debug.LogError(Application.persistentDataPath);

        using (var www = WWW.LoadFromCacheOrDownload(url, 5))
        {
            yield return www;
            if (!string.IsNullOrEmpty(www.error))
            {
                Debug.Log(www.error);
                yield return null;
            }
            var myLoadedAssetBundle = www.assetBundle;

            var asset = myLoadedAssetBundle.mainAsset;
            GameObject obj = (GameObject)myLoadedAssetBundle.LoadAsset("Assault_Rifle_01");
                Instantiate(obj);

        }
    }
}