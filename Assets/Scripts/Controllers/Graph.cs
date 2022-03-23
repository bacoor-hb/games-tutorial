using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    private GameObject[] gameObjects;
    protected LinkedList<GameObject> vectors = new LinkedList<GameObject>();

    private void Start()
    {
        GenerateBoard();
    }

    public void GenerateBoard()
    {
        gameObjects = GetComponentsInChildren<GameObject>();

        for (var i = 0; i < gameObjects.Length; i++)
        {
            vectors.AddLast(gameObjects[i]);
        }
    }

    public LinkedListNode<GameObject> GetNode(GameObject node)
    {
        return vectors.Find(node);
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
                    nextNode = vectors.First;
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
                    prevNode = vectors.Last;
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
                    nextNode = vectors.First;
                }
                listNodes.Add(nextNode.Value);
                count++;
            }
        } else if (step < 0)
        {
            int count = 0;
            LinkedListNode<GameObject> prevNode = currentNode;
            while (count < step)
            {
                if (prevNode.Previous != null)
                {
                    prevNode = prevNode.Previous;
                }else
                {
                    prevNode = vectors.Last;
                }
                listNodes.Add(prevNode.Value);
                count++;
            }
        }
        return listNodes;
    }

}