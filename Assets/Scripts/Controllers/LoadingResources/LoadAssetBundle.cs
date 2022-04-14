using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAssetBundle : MonoBehaviour
{
    // Start is called before the first frame update
    string url = "https://drive.google.com/uc?export=download&id=1f0xeIgP3L3ZVNC9PKVW3qzo59N_5AQHU";
   public void Load()
    {
        WWW www = new WWW(url);
        // while (!www.isDone)
        // {
        //     Debug.Log("Loading..." + www.progress);
        // }
        // Debug.Log("Loaded");
        StartCoroutine(webReq(www));

        // Update is called once per frame
      
        IEnumerator webReq(WWW www)
        {
            yield return www;
            while (www.isDone == false)
            {
                yield return null;

            }
            AssetBundle bundle = www.assetBundle;
            if (www.error == null)
            {
                GameObject obj = (GameObject)bundle.LoadAsset("board");
                Instantiate(obj);
            }
            else
            {
                Debug.Log(www.error);
            }
        }
    }
}