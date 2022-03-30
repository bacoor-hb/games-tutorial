using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphEventManager : MonoBehaviour
{
    public delegate void OnEnterNode(string address, GameObject node);
    public static event OnEnterNode onEnterNode;
    public static void RaiseOnEnterNode(string address, GameObject node)
    {
        if (onEnterNode != null)
        {
            onEnterNode(address, node);
        }
    }
}
