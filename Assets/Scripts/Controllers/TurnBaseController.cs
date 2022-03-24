using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class TurnBaseStatus
{
    public const int START_TURN = 1, START_ACTION = 2, ON_ACTION = 3, END_ACTION = 4, END_TURN = 5;
}

public class TurnBaseController : MonoBehaviour
{
    public delegate void Event<T>(T data);
    public static Event<int> OnStartTurn;
    public static Event<int> OnEndTurn;
    public static Event<int> OnActionStart;
    public static Event<int> OnActionEnd;
    public static Event<int> OnChangePlayer;


    public static bool startGamge = false;
    public static List<object> playerList;
    private static List<ActionTureBase> historyActionList;
    private static ActionTureBase currentAction;
    private static int turnBase = 0;
    private static int status = TurnBaseStatus.START_TURN;


    public static void StartGame()
    {
        if (!startGamge)
        {
            startGamge = true;
            turnBase = 0;
            status = TurnBaseStatus.START_TURN;
        }
    }

    public static void EndGame()
    {
        startGamge = false;
    }

    public static int Register(p)
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

    private static void AddAction(ActionTureBase action)
    {
        currentAction = action;
    }

    private void Update()
    {
        if (startGamge)
        {
            switch (status)
            {
                case TurnBaseStatus.START_TURN:
                    StartTurn();
                    status = TurnBaseStatus.START_ACTION;
                    break;
                case TurnBaseStatus.START_ACTION:
                    if(currentAction != null) {
                        ActionStart(currentAction);
                        status = TurnBaseStatus.ON_ACTION;
                    }
                    break;
                case TurnBaseStatus.ON_ACTION:
                    OnAction(currentAction);
                    status = TurnBaseStatus.END_ACTION;
                    break;
                case TurnBaseStatus.END_ACTION:
                    ActionEnd(currentAction);
                    status = TurnBaseStatus.END_TURN;
                    break;
                case TurnBaseStatus.END_TURN:
                    EndTurn();
                    if (CheckChangePlayer())
                    {
                        ChangePlayer();
                    }
                    status = TurnBaseStatus.START_TURN;
                    break;

            }

        }
    }

    // Xu ly trong luat choi
    private static void OnAction(ActionTureBase action)
    {

    }

    // Kiem tra xem luot choi cua player co duoc doi
    private static void CheckChangePlayer()
    {
        return true;
    }

    // Script run before start turn
    private static void StartTurn()
    {
        if (OnStartTurn != null)
        {
            OnStartTurn(turnBase);
        }
        historyActionList = new List<ActionTureBase>();
    }

    // Script run after end turn
    private static void EndTurn()
    {
        if (OnEndTurn != null)
        {
            OnEndTurn(turnBase);
        }
    }

    // script change player
    private static void ChangePlayer()
    {
        if (OnChangePlayer != null)
        {
            OnChangePlayer(NextPlayer());
        }
        turnBase = NextPlayer();
    }

    private static int NextPlayer()
    {
        return (turnBase + 1) % playerList.Count;
    }

    // script run before excute action
    private static void ActionStart(ActionTureBase action)
    {
        if (OnActionStart != null)
        {
            OnActionStart(turnBase);
        }
    }

    // script run after excute action
    private static void ActionEnd(ActionTureBase action)
    {
        if (OnActionEnd != null)
        {
            OnActionEnd(turnBase);
        }
    }
}
