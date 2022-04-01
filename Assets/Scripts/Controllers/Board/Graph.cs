using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Graph : MonoBehaviour
{
    protected Dictionary<int, GraphNode> nodes = new Dictionary<int,GraphNode>();
    //Hash Table of the same Color (type) of property
    protected Dictionary<int, int> totalType = new Dictionary<int, int>();
    protected Dictionary<string, GraphNode> currentNodes = new Dictionary<string, GraphNode>();

    [SerializeField]
    private GraphEventManager eventManager;

    void Start()
    {
        eventManager.onEnterNode += OnEnterNode;
    }

    /// <summary>
    /// Add all node to the cache
    /// </summary>
    /// <param name="nodeInScene"></param>
    public void GenerateBoard(GraphNode[] nodeInScene)
    {
        for (var i = 0; i < nodeInScene.Length; i++)
        {
            nodes.Add(nodeInScene[i].NodeID, nodeInScene[i]);
            if (nodeInScene[i].property != null)
            {
                int typeId = nodeInScene[i].property.data.typeId;
                if (!totalType.ContainsKey(typeId))
                {
                    totalType.Add(typeId, 1);
                }
                else
                {
                    totalType[typeId] += 1;
                }
            }            
        }
    }

    /// <summary>
    /// Get a node of this graph base on its ID
    /// </summary>
    /// <param name="nodeID"></param>
    /// <returns></returns>
    public GraphNode GetNode(int nodeID)
    {
        return nodes[nodeID];
    }

    /// <summary>
    /// Get a list of all Node in this graph
    /// </summary>
    /// <returns></returns>
    public List<GraphNode> GetNodeList()
    {
        return nodes.Select(x => x.Value).ToList();
    }

    /// <summary>
    /// Get a list of node between currentNode and targetNode, this list include the target node
    /// </summary>
    /// <param name="currentNode"></param>
    /// <param name="targetNode"></param>
    /// <param name="isClockWise"></param>
    /// <returns></returns>
    public List<GraphNode> GetNodesByTargetNode(GraphNode currentNode, GraphNode targetNode, bool isClockWise = true)
    {
        List<GraphNode> listNodes = new List<GraphNode>();
        if (isClockWise)
        {
            GraphNode nextNode = currentNode.Next();
            while (nextNode.NodeID != targetNode.NodeID)
            {
                listNodes.Add(nextNode);
                if (nextNode.Next() != null)
                {
                    nextNode = nextNode.Next();
                }
                //else
                //{
                //    nextNode = nodes.First;
                //}
            }
        }
        else
        {
            GraphNode prevNode = currentNode.Previous();
            while (prevNode != targetNode.Previous())
            {
                listNodes.Add(prevNode);
                if (prevNode.Previous() != null)
                {
                    prevNode = prevNode.Previous();
                }
                //else
                //{
                //    prevNode = nodes.Last;
                //}
            }
        }

        listNodes.Add(targetNode);
        return listNodes;
    }

    /// <summary>
    /// Get a list of node "step" step from the current node
    /// </summary>
    /// <param name="currentNode"></param>
    /// <param name="step">Number of step: [> 0 ~ use Next()] [< 0 ~ use Previous()]</param>
    /// <returns></returns>
    public List<GraphNode> GetNodesByStep(GraphNode currentNode, int step)
    {
        List<GraphNode> listNodes = new List<GraphNode>();
        if (step > 0)
        {
            int count = 0;
            GraphNode nextNode = currentNode;
            while (count < step)
            {
                if (nextNode.Next() != null)
                {
                    nextNode = nextNode.Next();
                }
                listNodes.Add(nextNode);
                count++;
            }
        }
        else if (step < 0)
        {
            int count = 0;
            GraphNode prevNode = currentNode;
            while (count < step * -1)
            {
                if (prevNode.Previous() != null)
                {
                    prevNode = prevNode.Previous();
                }
                listNodes.Add(prevNode);
                count++;
            }
        }
        return listNodes;
    }

    /// <summary>
    /// Get the node that the user is step on, using the user address
    /// </summary>
    /// <param name="address"></param>
    /// <returns></returns>
    public GraphNode GetCurrentNodeByAddress(string address)
    {
        return currentNodes[address];
    }

    /// <summary>
    /// Trigger this Event while enter a node
    /// </summary>
    /// <param name="args"></param>
    private void OnEnterNode(params object[] args)
    {        
        if(args.Length != 2)
        {
            Debug.LogError("[GetOnEnterNode] Invalid Args...");
        }
        try
        {
            //Convert Params
            string address = args[0].ToString();
            GraphNode node = (GraphNode)args[1];

            if (node != null)
            {
                currentNodes[address] = node;
            }
            
            if (currentNodes[address].property.data != null)
            {
                //Debug.Log(currentNodes[address].property.data.description);
                Debug.Log(currentNodes[address].property.data.property_name);
            }
        }
        catch(Exception ex)
        {
            Debug.LogError("[GetOnEnterNodeERROR] " + ex.Message);
        }
    }

    public int GetTotalPropertiesByType(int typeId)
    {
        int total;
        if (totalType.ContainsKey(typeId))
        {
            total = totalType[typeId];
        }
        else
        {
            total = 0;
        }
        return total;
    }
}