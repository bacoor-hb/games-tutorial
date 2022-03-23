using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    private Transform[] list;
    protected LinkedList<Vector3> vectors = new LinkedList<Vector3>();

    private void Start()
    {
        GenerateBoard();
    }

    public void GenerateBoard()
    {
        list = GetComponentsInChildren<Transform>();

        for (var i = 0; i < list.Length; i++)
        {
            vectors.AddLast(list[i].position);
        }
    }

    public LinkedListNode<Vector3> GetNode(Vector3 node)
    {
        return vectors.Find(node);
    }

    public List<Vector3> GetNodesByTargetNode(LinkedListNode<Vector3> currentNode, LinkedListNode<Vector3> targetNode, bool isClockWise)
    {
        List<Vector3> listNodes = new List<Vector3>();
        if (isClockWise)
        {
            LinkedListNode<Vector3> nextNode = currentNode.Next;
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
            LinkedListNode<Vector3> prevNode = currentNode.Previous;
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

    public List<Vector3> GetNodesByStep(LinkedListNode<Vector3> currentNode, int step)
    {
        List<Vector3> listNodes = new List<Vector3>();
        if (step > 0)
        {
            int count = 0;
            LinkedListNode<Vector3> nextNode = currentNode;
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
            LinkedListNode<Vector3> prevNode = currentNode;
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