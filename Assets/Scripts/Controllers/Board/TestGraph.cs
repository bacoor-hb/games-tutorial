using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class TestGraph : MonoBehaviour
{
    [SerializeField]
    private GraphNode imprisonNode;
    [SerializeField]
    private GraphNode startNode;
    [SerializeField]
    private GraphNode[] taxNodes;
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
    private GraphNode targetNodeGameObj1;
    private int currentIndex1 = 0;
    [SerializeField]
    private int step2 = 0;
    [SerializeField]
    private GraphNode targetNodeGameObj2;

    [SerializeField]
    private GraphNode[] nodeList;
    [SerializeField]
    private GraphEventManager graphEvent;
    [SerializeField]
    private ReactManager reactManager;

    private int currentIndex2 = 0;
    private IEnumerator coroutine1;
    private IEnumerator coroutine2;
    private Graph board;

    private void Start()
    {
        board = GetComponent<Graph>();
        board.GenerateBoard(nodeList);
        reactManager.OnRollSuccess += OnRoleSuccess;
    }

    private void Update()
    {
        if (useStep == true)
        {
            useTargetNode = false;
        }
    }

    private void OnRoleSuccess(int value)
    {
        var nodes = board.GetNodeList();
        List<GraphNode> listNodes1 = board.GetNodesByStep(nodes[0], value);
        listNodes1.Insert(0, nodes[0]);
        coroutine1 = Move1(0.5f, listNodes1);
        StartCoroutine(coroutine1);
    }

    public Action GetAction()
    {
        Action action = new Action(ACTION_TYPE.MOVING);
        action.EventStartAction += OnMovingStart;
        action.EventAction += OnMoving;
        action.EventEndAction += OnActionEnd;
        return action;
    }

    private void OnMovingStart()
    {
        Debug.Log("moving ");
    }

    private IEnumerator OnMoving()
    {
        yield return StartCoroutine(GetRequest("https://www.randomnumberapi.com/api/v1.0/random?min=1&max=6&count=1"));
    }

    private void OnActionEnd()
    {
        Debug.Log("end moving");
    }

    public IEnumerator GetRequest(string uri)
    {
        using(UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;
            if (webRequest.isNetworkError)
            {
                Debug.Log(webRequest.error);
            }
            else
            {
                string[] results = webRequest.downloadHandler.text.Split('[', ']');
                Debug.Log(results[1]);
                int step1 = System.Int32.Parse(results[1].ToString());
                var nodes = board.GetNodeList();
                List<GraphNode> listNodes1 = board.GetNodesByStep(nodes[0], step1);
                listNodes1.Insert(0, nodes[0]);
                coroutine1 = Move1(0.5f, listNodes1);
                yield return StartCoroutine(coroutine1);
            }
        }
    }

    public IEnumerator StartMoveByTarget()
    {
        GraphNode targetNode1 = board.GetNode(targetNodeGameObj1.NodeID);
        GraphNode targetNode2 = board.GetNode(targetNodeGameObj2.NodeID);
        var nodes = board.GetNodeList();

        List<GraphNode> listNodes1 = board.GetNodesByTargetNode(nodes[0], targetNode1);
        List<GraphNode> listNodes2 = board.GetNodesByTargetNode(nodes[0], targetNode2);
        listNodes1.Insert(0, nodes[0]);
        listNodes2.Insert(0, nodes[0]);
        coroutine1 = Move1(0.5f, listNodes1);
        yield return StartCoroutine(coroutine1);
        //coroutine2 = Move2(0.5f, listNodes2);
        //StartCoroutine(coroutine2);
        //yield return StartCoroutine(coroutine2);
    }

    private IEnumerator StartMoveByStep()
    {
        var nodes = board.GetNodeList();
        List<GraphNode> listNodes1 = board.GetNodesByStep(nodes[0], step1);
        listNodes1.Insert(0, nodes[0]);
        coroutine1 = Move1(0.5f, listNodes1);
        yield return StartCoroutine(coroutine1);
        List<GraphNode> listNodes2 = board.GetNodesByStep(nodes[0], step2);
        listNodes2.Insert(0, nodes[0]);
        coroutine2 = Move2(0.5f, listNodes2);
        StartCoroutine(coroutine2);
    }

    private IEnumerator Move1(float waitTime, List<GraphNode> nodes)
    {
        while (currentIndex1 < nodes.Count)
        {
            var nodePosition = nodes[currentIndex1].transform.position;
            player1.transform.position = new Vector3(nodePosition.x, 1.5f, nodePosition.z);
            graphEvent.RaiseOnEnterNode("address1", nodes[currentIndex1]);
            currentIndex1++;
            yield return new WaitForSeconds(waitTime);
        }
    }

    private IEnumerator Move2(float waitTime, List<GraphNode> nodes)
    {
        while (currentIndex2 < nodes.Count)
        {
            var nodePosition = nodes[currentIndex2].transform.position;
            player2.transform.position = new Vector3(nodePosition.x, 1.5f, nodePosition.z);
            graphEvent.RaiseOnEnterNode("address2", nodes[currentIndex2]);
            currentIndex2++;
            yield return new WaitForSeconds(waitTime);
        }
    }

    private void CheckEvent(GraphNode node, string address, GameObject player)
    {
        if (node.NodeID == startNode.NodeID)
        {
            graphEvent.RaiseOnEnterStartNode(address);
        }
        else if (node.NodeID == imprisonNode.NodeID)
        {
            var prisonNode = board.GetNode((int)PROPERTY_ID.PRISON);
            var nodePosition = prisonNode.transform.position;
            player.transform.position = new Vector3(nodePosition.x, 1.5f, nodePosition.z);

        }
    }
}
