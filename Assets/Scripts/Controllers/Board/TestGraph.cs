using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGraph : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private int step = 0;
    private int currentIndex = 0;
    private IEnumerator coroutine;
    private Graph board;

    void Start()
    {
        board = GetComponent<Graph>();
        Transform[] transforms = GetComponentsInChildren<Transform>();
        board.GenerateBoard(transforms);
        if (step > 0)
        {
            List<GameObject> listNodes = board.GetNodesByStep(board.nodes.First, step);
            listNodes.Insert(0, board.nodes.First.Value);
            coroutine = Move(0.75f, listNodes);
            StartCoroutine(coroutine);
        }
    }

    void Update()
    {
        
    }

    private IEnumerator Move(float waitTime, List<GameObject> nodes)
    {
        while (currentIndex < nodes.Count)
        {
            var nodePosition = nodes[currentIndex].transform.position;
            player.transform.position = new Vector3(nodePosition.x, 2.5f, nodePosition.z);
            GraphEventManager.RaiseOnEnterNode("asd", nodes[currentIndex]);
            currentIndex++;
            yield return new WaitForSeconds(waitTime);
        }
    }
}
