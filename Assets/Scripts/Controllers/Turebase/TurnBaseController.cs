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
    public static Event<PlayerTestController> OnStartTurn;
    public static Event<PlayerTestController> OnEndTurn;
    public static Event<ActionTureBase> OnActionStart;
    public static Event<ActionTureBase> OnActionEnd;
    public static Event<PlayerTestController> OnChangePlayer;


    public static bool startGamge = false;
    public static List<PlayerTestController> playerList = new List<PlayerTestController>();
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

    public static void Register(PlayerTestController player)
    {
        if (startGamge)
        {
            throw new System.Exception("Game start");
        }
        playerList.Add(player);
    }

    private void Start()
    {
        startGamge = false;
    }

    public static bool AddAction(ActionTureBase action)
    {
        if (currentAction == null)
        {
            currentAction = action;
            return true;
        }
        return false;
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
                    if (currentAction != null)
                    {
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

        if (Input.GetKeyDown("space"))
        {
            if (startGamge)
            {
                EndGame();
            }
            else
            {
                StartGame();
            }
        }
    }

    // handle law in game
    private static void OnAction(ActionTureBase action)
    {
        Debug.Log("Action:" + action.ToString());
    }

    // check change player when return true
    private static bool CheckChangePlayer()
    {
        return true;
    }

    // Script run before start turn
    private static void StartTurn()
    {
        if (OnStartTurn != null)
        {
            OnStartTurn(playerList[turnBase]);
        }
        historyActionList = new List<ActionTureBase>();
    }

    // Script run after end turn
    private static void EndTurn()
    {
        if (OnEndTurn != null)
        {
            OnEndTurn(playerList[turnBase]);
        }
        historyActionList.Add(currentAction);
        currentAction = null;
    }

    // script change player
    private static void ChangePlayer()
    {
        if (OnChangePlayer != null)
        {
            OnChangePlayer(playerList[NextPlayer()]);
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
            OnActionStart(action);
        }
    }

    // script run after excute action
    private static void ActionEnd(ActionTureBase action)
    {
        if (OnActionEnd != null)
        {
            OnActionEnd(action);
        }
    }
}
