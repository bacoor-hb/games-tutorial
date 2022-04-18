using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class LoadingResourcesManager : Singleton<LoadingResourcesManager>
{
    // Start is called before the first frame update
    [SerializeField]
    private LoadAssetBundle loadAssetBundle;
    void Start()
    {

    }

    public Object LoadcharacterResourcesFolder(string path) {
        Object ojb = Resources.Load<Object>(path);
        return ojb;
    }
    public void LoadAssetBundle()
    {
        loadAssetBundle.Load();
    }
    public void ClearCacheIndexedDB()
    {
        Debug.Log("aaa");
        //check folder has existed
        if (!Directory.Exists(Application.persistentDataPath))
        {
            // Directory.CreateDirectory(Application.persistentDataPath + "/AssetBundles");
            Debug.Log("folder not existed");
        }
        else
        {
            //delete folder
            //Directory.Delete(Application.persistentDataPath + "/UnityCache/Shared/objectbundle", true);
            UnityWebRequest.ClearCookieCache();
            Debug.Log("folder existed");
        }
    }
    

   
}
