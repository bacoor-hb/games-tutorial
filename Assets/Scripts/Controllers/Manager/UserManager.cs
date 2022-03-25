using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager : MonoBehaviour
{
    [SerializeField]
    private GameObject gameObj;
    private void Start()
    {
        gameObj = GetComponentInParent<Transform>().gameObject;

    }
    private void Update()
    {
        
    }
}
