using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    private Transform[] Transforms;
    protected LinkedList<GameObject> nodes = new LinkedList<GameObject>();

     void Start()
    {
        GenerateBoard();
    }

    public void GenerateBoard()
    {
        Transforms = GetComponentsInChildren<Transform>();

        for (var i = 0; i < Transforms.Length; i++)
        {
            if (Transforms[i].CompareTag("board_node"))
            {
                nodes.AddLast(Transforms[i].gameObject);
            }
        }
    }

    public LinkedListNode<GameObject> GetNode(GameObject node)
    {
        return nodes.Find(node);
    }

    public List<GameObject> GetNodesByTargetNode(LinkedListNode<GameObject> currentNode, LinkedListNode<GameObject> targetNode, bool isClockWise)
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
                } else
                {
                    nextNode = nodes.First;
                }
                listNodes.Add(nextNode.Value);
                count++;
            }
        } else if (step < 0)
        {
            int count = 0;
            LinkedListNode<GameObject> prevNode = currentNode;
            while (count < step * -1)
            {
                if (prevNode.Previous != null)
                {
                    prevNode = prevNode.Previous;
                }else
                {
                    prevNode = nodes.Last;
                }
                listNodes.Add(prevNode.Value);
                count++;
            }
        }
        return listNodes;
    }

}