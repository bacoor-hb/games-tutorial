using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    private Transform[] Transforms;
    protected LinkedList<GameObject> nodes = new LinkedList<GameObject>();

    [SerializeField]
    private List<GameObject> listDice;

    [SerializeField]
    private DiceController diceController;
    [SerializeField]
    private MovementController movementController;
    private List<GameObject> nodeList = new List<GameObject>();


    LinkedListNode<GameObject> currentNode;
    int currentIndex = 0;

    void Start()
    {
        GenerateBoard();
        currentNode = nodes.First;
        diceController.OnResult = LogResult;
        movementController.OnStartMoving += LogStart;
        movementController.OnEndMoving += LogEnd;
    }

    private void LogStart()
    {
        Debug.Log("Start moving");
    }

    private void LogEnd()
    {
        currentIndex++;
        currentNode = GetNode(nodeList[currentIndex - 1]);
        if (currentIndex < nodeList.Count)
        {
            movementController.SetTarget(nodeList[currentIndex].transform.position, 0.5f);
            movementController.StartMoving();
        } else
        {
            currentIndex = 0;
            if (currentNode.Value.GetComponent<Property>().data != null)
            {
                Debug.Log(currentNode.Value.GetComponent<Property>().data.description);
            }
        }
    }

    public void LogResult(List<int> diceValues)
    {
        int totalValue = 0;
        for(var i = 0; i < diceValues.Count; i++)
        {
            totalValue += diceValues[i];
        }
        nodeList = GetNodesByStep(currentNode, totalValue * -1);
        Debug.Log(nodeList.Count);
        movementController.SetTarget(nodeList[currentIndex].transform.position, 0.5f);
        movementController.StartMoving();
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            diceController.RollDice(listDice);
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