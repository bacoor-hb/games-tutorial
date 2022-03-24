using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurnBaseController : MonoBehaviour
{
    public delegate void Event<T>(T data);
    public static Event<int> OnStartTurn;
    public static Event<int> OnEndTurn;
    public static Event<List<int>> OnAction;
    public static Event<int> OnActionStart;
    public static Event<int> OnActionEnd;
    public static Event<int> OnChangePlayer;

    public static bool startGamge = false;
    public static List<int> playerList;
    private static int turnBase = 0;
    private static int status = 0;


    public static void StartGame()
    {
        if (!startGamge)
        {
            startGamge = true;
            turnBase = 0;
        }
    }

    public static void EndGame()
    {
        startGamge = false;
    }

    public static int Register()
    {
        if (startGamge)
        {
            throw new System.Exception("Game start");
        }
        playerList.Add(playerList.Count);
        return playerList.Count - 1;
    }

    private void Start()
    {
        startGamge = false;
        playerList = new List<int>();
    }

    private void Update()
    {
        if (startGamge)
        {
            switch(status)
            {
                case 1:
                    StartTurn();
                    break;
                case 2:
                    // handle action
                    break;
                case 3:
                    EndTurn();
                    if (true)
                    {
                        ChangePlayer();
                    }
                    break;
            }

        }
    }

    private static void StartTurn()
    {
        if (OnStartTurn != null)
        {
            OnStartTurn(turnBase);
        }
        status++;
    }

    private static void EndTurn()
    {
        if (OnEndTurn != null)
        {
            OnEndTurn(turnBase);
        }
        status = 1;
    }

    private static void ChangePlayer()
    {
        if (OnChangePlayer != null)
        {
            OnChangePlayer(NextPlayer());
        }
    }

    private static int NextPlayer()
    {
        return (turnBase + 1) % playerList.Count;
    }

    private static void ActionStart()
    {
        if (OnActionStart != null)
        {
            OnActionStart(turnBase);
        }
    }

    private static void ActionEnd()
    {
        if (OnActionEnd != null)
        {
            OnActionEnd(turnBase);
        }
    }
}
