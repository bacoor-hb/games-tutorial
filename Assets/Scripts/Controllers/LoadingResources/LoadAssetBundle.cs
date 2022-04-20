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

    [DllImport("__Internal")]
    private static extern void Hello();
    AssetBundle myLoadedAssetBundle;
    string url = "https://raw.githubusercontent.com/HoDienCong12c5/serverBundle/main/ab";
    //string url = "https://raw.githubusercontent.com/Hungduc123/AssetBundle/master/objectbundle";



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
    ///////////////////////////////////////
    public void Load()
    {
        StartCoroutine(LoadAB());
    }
    IEnumerator LoadAB()
    {
        while (!Caching.ready)
            yield return null;
        using (var www = WWW.LoadFromCacheOrDownload(url, 5))
        {
            yield return www;
            if (!string.IsNullOrEmpty(www.error))
            {
                Debug.Log(www.error);
                yield return null;
            }
            myLoadedAssetBundle = www.assetBundle;

            //var asset = myLoadedAssetBundle.mainAsset;
            GameObject obj = (GameObject)myLoadedAssetBundle.LoadAsset("Assault_Rifle_01");
            //GameObject obj = (GameObject)myLoadedAssetBundle.LoadAsset("Assault_Rifle_01");

            //GameObject obj2 = (GameObject)myLoadedAssetBundle.LoadAsset("SA_Prop_DeckChair_01");
            //GameObject obj3 = (GameObject)myLoadedAssetBundle.LoadAsset("SA_Prop_Dumpster_01");
            //GameObject obj4 = (GameObject)myLoadedAssetBundle.LoadAsset("SA_Prop_Fence_Corrugated_01");

            Instantiate(obj);
            //Instantiate(obj2);
            //Instantiate(obj3);
            //Instantiate(obj4);
        }
    }
    ///////////////////////////////////////
    //AssetBundle assetBundle;
    //public void Load()
    //{
    //    //var myLoadedAssetBundle
    //    //   = AssetBundle.LoadFromFile(Path.Combine(Application.persistentDataPath, "AB/ab"));
    //    //if (myLoadedAssetBundle == null)
    //    //{
    //    //    Debug.Log("Failed to load AssetBundle!");
    //    //    return;
    //    //}
    //    //var prefab = myLoadedAssetBundle.LoadAsset<GameObject>("Cube");
    //    //Instantiate(prefab);
    //    //Debug.Log(Application.streamingAssetsPath);
    //    StartCoroutine(DownLoadAsset());
    //}
    //public IEnumerator DownLoadAsset()
    //{
    //    Debug.Log("Application.persistentDataPath" + Application.persistentDataPath);   
    //    string url = "https://raw.githubusercontent.com/HoDienCong12c5/serverBundle/main/ab";
    //    //if (!File.Exists(Path.Combine(Application.persistentDataPath, "AB")))
    //    //{
    //    //    Directory.CreateDirectory(Application.persistentDataPath + "/AB");
    //    //}
    //    if (!File.Exists(Path.Combine(Application.persistentDataPath, "ab")))
    //    {

    //        Uri uri = new Uri(url);

    //        WebClient client = new WebClient();

    //        client.DownloadFileAsync(uri, Application.persistentDataPath + "/ab");

    //        while (client.IsBusy)   // Wait until the file download is complete
    //            yield return null;
    //        Debug.Log("download");
    //    }


    //    assetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.persistentDataPath, "ab"));
    //    var prefab = assetBundle.LoadAsset<GameObject>("Assault_Rifle_01");
    //    Instantiate(prefab);

    //}

}