using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGraph : MonoBehaviour
{
    [SerializeField]
    private bool useStep;
    [SerializeField]
    private bool useTargetNode;
    [SerializeField]
    private GameObject player1;
    [SerializeField]
    private GameObject player2;
    [SerializeField]
    private int step1 = 0;
    [SerializeField]
    private GameObject targetNodeGameObj1;
    private int currentIndex1 = 0;
    [SerializeField]
    private int step2 = 0;
    [SerializeField]
    private GameObject targetNodeGameObj2;
    private int currentIndex2 = 0;
    private IEnumerator coroutine1;
    private IEnumerator coroutine2;
    private Graph board;
    public UserManager userManager;
    public List<UserManager> listUserManager = new List<UserManager>();
    private void Start()
    {
        board = GetComponent<Graph>();
        Transform[] transforms = GetComponentsInChildren<Transform>();
        board.GenerateBoard(transforms);
        if (useStep)
        {
            StartCoroutine(StartMoveByStep());
        }
        if (useTargetNode)
        {
            StartCoroutine(StartMoveByTarget());
        }
    }

    void Update()
    {
        if (useStep == true) {
            useTargetNode = false;
        }
    }

    private IEnumerator StartMoveByTarget()
    {
        LinkedListNode<GameObject> targetNode1 = board.GetNode(targetNodeGameObj1);
        LinkedListNode<GameObject> targetNode2 = board.GetNode(targetNodeGameObj2);
        var nodes = board.GetNodes();
        List<GameObject> listNodes1 = board.GetNodesByTargetNode(nodes.First, targetNode1);
        List<GameObject> listNodes2 = board.GetNodesByTargetNode(nodes.First, targetNode2);
        listNodes1.Insert(0, nodes.First.Value);
        listNodes2.Insert(0, nodes.First.Value);
        coroutine1 = Move1(0.5f, listNodes1);
        yield return StartCoroutine(coroutine1);
        coroutine2 = Move2(0.5f, listNodes2);
        StartCoroutine(coroutine2);
    }

    private IEnumerator StartMoveByStep()
    {
        var nodes = board.GetNodes();
        List<GameObject> listNodes1 = board.GetNodesByStep(nodes.First, step1);
        listNodes1.Insert(0, nodes.First.Value); 
        coroutine1 = Move1(0.5f, listNodes1); 
        yield return StartCoroutine(coroutine1);
        List<GameObject> listNodes2 = board.GetNodesByStep(nodes.First, step2);
        listNodes2.Insert(0, nodes.First.Value);
        coroutine2 = Move2(0.5f, listNodes2);
        StartCoroutine(coroutine2);
    }

    private IEnumerator Move1(float waitTime, List<GameObject> nodes)
    {
        while (currentIndex1 < nodes.Count)
        {
            var nodePosition = nodes[currentIndex1].transform.position;
            player1.transform.position = new Vector3(nodePosition.x, 3f, nodePosition.z);
            GraphEventManager.RaiseOnEnterNode("address1", nodes[currentIndex1]);
            currentIndex1++;
            if(currentIndex1 == nodes.Count)
            {
                GameObject temp = nodes[currentIndex1 - 1];
                Property propertyTemp = temp.GetComponent<Property>();
                if (propertyTemp.isBought)
                {
                    Debug.Log("price rent");

                }
                ProcessMoney(listUserManager[0], propertyTemp,4);

            }
            yield return new WaitForSeconds(waitTime);
        }
    }

    private IEnumerator Move2(float waitTime, List<GameObject> nodes)
    {
        while (currentIndex2 < nodes.Count)
        {
            var nodePosition = nodes[currentIndex2].transform.position;
            player2.transform.position = new Vector3(nodePosition.x, 3f, nodePosition.z);
            GraphEventManager.RaiseOnEnterNode("address2", nodes[currentIndex2]);
            currentIndex2++;
            if (currentIndex2 == nodes.Count)
            { 
                GameObject temp = nodes[currentIndex2-1];
                Property propertyTemp = temp.GetComponent<Property>();
                if (propertyTemp.isBought)
                {
                    Debug.Log("price rent");

                }
                ProcessMoney(listUserManager[1], propertyTemp,2);

            }
            yield return new WaitForSeconds(waitTime);
        }
    }
    public void ProcessMoney(UserManager _userManager, Property _Property, int level)
    { 
        
        Debug.Log(_Property.data.name);
        _userManager.OnBuyNewProperty(_Property);
        _userManager.OnBuilding(_Property,3);
        Debug.Log(_userManager.userData.Money);
        Debug.Log(_Property.level);
        Debug.Log("list count:" + _userManager.userData.GetProperties().Count);
        _userManager.SellForBank(_Property,4);
        Debug.Log(_userManager.userData.Money);
        Debug.Log("_Property.level:"+ _Property.level);
        Debug.Log("list count:" + _userManager.userData.GetProperties().Count);
        //Debug.Log(true); 
    }
}
