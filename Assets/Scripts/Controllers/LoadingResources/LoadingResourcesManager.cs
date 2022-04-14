using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
