using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : Singleton<Graph>
{
    protected LinkedList<GameObject> nodes = new LinkedList<GameObject>();
    protected Dictionary<int, int> totalType = new Dictionary<int, int>();
    protected Dictionary<string, GameObject> currentNodes = new Dictionary<string, GameObject>();

    void Start()
    {
        GenerateBoard();
        GraphEventManager.onEnterNode += GetOnEnterNode;
    }

    public void GenerateBoard()
    {
        Transform[] transforms = GetComponentsInChildren<Transform>();

        for (var i = 0; i < transforms.Length; i++)
        {
            if (transforms[i].CompareTag("board_node"))
            {
                nodes.AddLast(transforms[i].gameObject);
                if (transforms[i].gameObject.GetComponent<Property>().data != null)
                {
                    int typeId = transforms[i].gameObject.GetComponent<Property>().data.typeId;
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
    }

    public LinkedListNode<GameObject> GetNode(GameObject node)
    {
        return nodes.Find(node);
    }

    public List<GameObject> GetNodesByTargetNode(LinkedListNode<GameObject> currentNode, LinkedListNode<GameObject> targetNode, bool isClockWise = true)
    {
        List<GameObject> listNodes = new List<GameObject>();
        if (isClockWise)
        {
            LinkedListNode<GameObject> nextNode = currentNode.Next;
            while (nextNode != targetNode.Next)
            {
                listNodes.Add(nextNode.Value);
                if (nextNode.Next != null)
                {
                    nextNode = nextNode.Next;
                }
                else
                {
                    nextNode = nodes.First;
                }
            }
        }
        else
        {
            LinkedListNode<GameObject> prevNode = currentNode.Previous;
            while (prevNode != targetNode.Previous)
            {
                listNodes.Add(prevNode.Value);
                if (prevNode.Previous != null)
                {
                    prevNode = prevNode.Previous;
                }
                else
                {
                    prevNode = nodes.Last;
                }

            }
        }
        return listNodes;
    }

    public List<GameObject> GetNodesByStep(LinkedListNode<GameObject> currentNode, int step)
    {
        List<GameObject> listNodes = new List<GameObject>();
        if (step > 0)
        {
            int count = 0;
            LinkedListNode<GameObject> nextNode = currentNode;
            while (count < step)
            {
                if (nextNode.Next != null)
                {
                    nextNode = nextNode.Next;
                }
                else
                {
                    nextNode = nodes.First;
                }
                listNodes.Add(nextNode.Value);
                count++;
            }
        }
        else if (step < 0)
        {
            int count = 0;
            LinkedListNode<GameObject> prevNode = currentNode;
            while (count < step * -1)
            {
                if (prevNode.Previous != null)
                {
                    prevNode = prevNode.Previous;
                }
                else
                {
                    prevNode = nodes.Last;
                }
                listNodes.Add(prevNode.Value);
                count++;
            }
        }
        return listNodes;
    }

    public GameObject GetCurrentNodeByAddress(string address)
    {
        return currentNodes[address];
    }

    public void GetOnEnterNode(string address, GameObject node)
    {
        if (node.GetComponent<Property>().data != null)
        {
            Debug.Log(node.GetComponent<Property>().data.description);
        }
        else
        {
            Debug.Log(node.GetComponent<Property>().name);
        }
        if (node != null)
        {
            currentNodes[address] = node;
        }
        else
        {
            currentNodes[address] = nodes.First.Value;
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