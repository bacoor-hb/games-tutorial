using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


public class TestGraph : MonoBehaviour
{
    [SerializeField]
    private UIManagerBoard _UIManagerBoard;
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

    private int currentIndex2 = 0;
    private IEnumerator coroutine1;
    private IEnumerator coroutine2;
    private Graph board;

    private int currentPos = 0;
    private int diceValue = 0;
    private void Start()
    {
        _UIManagerBoard.onClickEnter += OnClickEnter;
        board = GetComponent<Graph>();

        board.GenerateBoard(nodeList);
        // if (useStep)
        // {
        //     StartCoroutine(StartMoveByStep());
        // }
        // if (useTargetNode)
        // {
        //     StartCoroutine(StartMoveByTarget());
        // }
    }

    private void Update()
    {
        if (useStep == true)
        {
            useTargetNode = false;
        }
    }

    private IEnumerator StartMoveByTarget()
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
        coroutine2 = Move2(0.5f, listNodes2);
        StartCoroutine(coroutine2);
    }
    private IEnumerator StartMoveByTarget(int target)
    {
        GraphNode targetNode1 = board.GetNode(target);
        Debug.Log("targetNode1.NodeID");
        Debug.Log(targetNode1.NodeID);

        var nodes = board.GetNodeList();

        List<GraphNode> listNodes1 = board.GetNodesByTargetNode(nodes[0], targetNode1);

        listNodes1.Insert(0, nodes[0]);

        coroutine1 = Move1(0.5f, listNodes1);
        yield return StartCoroutine(coroutine1);




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
    private IEnumerator StartMoveByStep(int diceValue)
    {
        var nodes = board.GetNodeList();
        List<GraphNode> listNodes1 = board.GetNodesByStep(nodes[currentPos], diceValue);
        listNodes1.Insert(0, nodes[currentPos]);
        coroutine1 = Move1(0.5f, listNodes1);
        yield return StartCoroutine(coroutine1);
        currentPos += diceValue;
        // change scene to testAttack
        SceneManager.LoadScene("Scenes/Test Scene/testAttack");



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
    void OnClickEnter()
    {
        StartCoroutine(getApi("http://192.168.50.165:5000/users/lehoangvuvt/roll"));
    }
    private IEnumerator getApi(string url)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                Debug.Log(webRequest.error);
            }
            else
            {

                string json = webRequest.downloadHandler.text;
                Debug.Log(json);
                //DataApi data = JsonUtility.FromJson<DataApi>(json);
                DataApi data = JsonConvert.DeserializeObject<DataApi>(json);
                StaticDataApi.dataApi = data;
                diceValue = data.diceValue;
                Debug.Log("data.diceValue: " + diceValue);
                StartCoroutine(StartMoveByStep(diceValue));

            }
        }
    }




}
