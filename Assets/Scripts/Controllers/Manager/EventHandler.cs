using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    void Start()
    {
     
    }
    void Update()
    {
        
    }

    void UpdateCurrentNode(string address, GameObject node)
    {
        EventManager.RaiseOnEnterNode(address, node);
    }
}
