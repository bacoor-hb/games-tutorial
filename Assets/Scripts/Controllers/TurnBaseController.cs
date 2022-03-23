using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurnBaseController : Singleton<TurnBaseController>
{
    [SerializeField]
#pragma warning disable CS0436 // Type conflicts with imported type
    public List<PlayersController> playerList;
#pragma warning restore CS0436 // Type conflicts with imported type
    private bool startGamge = false;
    private int turnBase = 0;


    public void StartGame()
    {
        if (!startGamge)
        {
            startGamge = true;
            turnBase = 0;
            AddListenPlayer(turnBase);
        }
    }

    public void EndGame()
    {
        startGamge = false;
        RemoveListenPlayer(turnBase);
    }

    private void Start()
    {
        startGamge = false;
        startGamge();
    }

    private void Update()
    {
        if (startGamge)
        {

        }
    }


    private void NextTurn()
    {
        StartCoroutine(OnNextTurn());
    }

    private void StartTurn()
    {
        StartCoroutine(OnStartTurn());
    }

    private void EndTurn()
    {
        StartCoroutine(OnEndTurn());
    }

    private void Action()
    {
        StartCoroutine(OnAction());
    }

    private void ActionStart()
    {
        StartCoroutine(OnActionStart());
    }

    private void ActionEnd()
    {
        StartCoroutine(OnActionEnd());
    }


    float delayTime = 2.0f;
    IEnumerator OnNextTurn()
    {
        if (startGamge)
        {
            yield return new WaitForSeconds(delayTime);

            Debug.Log("OnNextTurn");
            int nextIndex = GetNextIndexPlayer(turnBase);
            RemoveListenPlayer(turnBase);
            AddListenPlayer(nextIndex);
            turnBase = nextIndex;
        }
    }

    IEnumerator OnStartTurn()
    {
        if (startGamge)
        {
            yield return new WaitForSeconds(delayTime);

            Debug.Log("OnStartTurn");

        }
    }

    IEnumerator OnEndTurn()
    {
        if (startGamge)
        {
            yield return new WaitForSeconds(delayTime);

            Debug.Log("OnEndTurn");
        }
    }

    IEnumerator OnAction()
    {
        if (startGamge)
        {
            yield return new WaitForSeconds(delayTime);

            Debug.Log("OnEndTurn");
        }
    }

    IEnumerator OnActionStart()
    {
        if (startGamge)
        {
            yield return new WaitForSeconds(delayTime);

            Debug.Log("OnActionStart");
        }
    }

    IEnumerator OnActionEnd()
    {
        if (startGamge)
        {
            yield return new WaitForSeconds(delayTime);

            Debug.Log("OnActionEnd");

        }
    }

    private void AddListenPlayer(int indexPlayer)
    {
        if (playerList[indexPlayer])
        {
            playerList[indexPlayer].OnStartTurn += StartTurn;
            playerList[indexPlayer].OnEndTurn += EndTurn;
            playerList[indexPlayer].OnAction += Action;
            playerList[indexPlayer].OnActionStart += ActionStart;
            playerList[indexPlayer].OnActionEnd += ActionEnd;
            playerList[indexPlayer].OnChangePlayer += NextTurn;
        }
    }

    private void RemoveListenPlayer(int indexPlayer)
    {
        if (playerList[indexPlayer])
        {
            playerList[indexPlayer].OnStartTurn -= StartTurn;
            playerList[indexPlayer].OnEndTurn -= EndTurn;
            playerList[indexPlayer].OnAction -= Action;
            playerList[indexPlayer].OnActionStart -= ActionStart;
            playerList[indexPlayer].OnActionEnd -= ActionEnd;
            playerList[indexPlayer].OnChangePlayer -= NextTurn;
        }
    }

    private int GetNextIndexPlayer(int indexPlayer)
    {
        int nextIndex = (indexPlayer + 1) % playerList.Count;
        return nextIndex;
    }
}
