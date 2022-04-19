using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAddressables : MonoBehaviour
{
    // Start is called before the first frame update
    public void Load()
    {
        StartCoroutine(LoadAA());
    }
    IEnumerator LoadAA()
    {
        Debug.Log("LoadAddressables");
        yield return null;
    }
}
