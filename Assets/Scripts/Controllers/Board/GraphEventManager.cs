using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphEventManager : MonoBehaviour
{
    public delegate void onEventEnter(params object[] args);
    public  onEventEnter onEnterNode;
    public void RaiseOnEnterNode(string address, GraphNode node)
    {
        if (onEnterNode != null)
        {
            onEnterNode(address, node);
        }
    }
}
