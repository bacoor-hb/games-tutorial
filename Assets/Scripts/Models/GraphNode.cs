using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphNode : MonoBehaviour
{
    public int NodeID
    {
        get
        {
            return (int)property.data.id;
        }
        private set
        {
            NodeID = value;
        }
    }
    public Property property;

    [SerializeField]
    private GraphNode preNode;
    [SerializeField]
    public GraphNode nextNode;

    /// <summary>
    /// Get the next Node of the graph
    /// </summary>
    /// <returns></returns>
    public GraphNode Next()
    {
        return nextNode;
    }

    /// <summary>
    /// Get the previous node of the graph
    /// </summary>
    /// <returns></returns>
    public GraphNode Previous()
    {
        return preNode;
    }
}
