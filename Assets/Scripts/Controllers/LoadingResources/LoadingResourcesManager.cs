using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using System.Runtime.InteropServices;



public class LoadingResourcesManager : Singleton<LoadingResourcesManager>
{
    // Start is called before the first frame update
    [SerializeField]
    private LoadAssetBundle loadAssetBundle;

    [DllImport("__Internal")]
    private static extern void Clear(string nameDB, string pathDB);

    void Start()
    {

    }

    public Object LoadcharacterResourcesFolder(string path)
    {
        Object ojb = Resources.Load<Object>(path);
        return ojb;
    }
    public void LoadAssetBundle()
    {
        loadAssetBundle.Load();


    }
    public void ClearCacheIndexedDB(string nameFile)
    {
        Clear("/idbfs", Application.persistentDataPath + $"/UnityCache/Shared/{nameFile}");
    }

}
