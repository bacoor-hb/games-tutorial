using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBaseController : MonoBehaviour
{
    //public delegate void Event<T>(T data);
    //public static Event<Object> OnStartTurn;
    //public static Event<Object> OnEndTurn;
    //public static Event<IAction> OnActionStart;
    //public static Event<IAction> OnActionEnd;
    //public static Event<Object> OnChangePlayer;


    public static bool isStarting = false;
    public static List<IPlayer> playerList = new List<IPlayer>();
    private static List<IAction> historyActionList;
    private static Queue<IAction> queueActionList;
    private static IAction currentAction;
    private static int turnBase = 0;
    private static CYCLE_TURN status = CYCLE_TURN.START_TURN;


    public static void StartGame()
    {
        if (!isStarting)
        {
            isStarting = true;
            turnBase = 0;
            status = CYCLE_TURN.START_TURN;
        }
    }

    public static void EndGame()
    {
        isStarting = false;
    }

    public static void Register(IPlayer player)
    {
        if (isStarting)
        {
            throw new System.Exception("Game start");
        }
        playerList.Add(player);
    }

    private void Start()
    {
        isStarting = false;
    }

    public static void AddAction(IPlayer player, IAction action)
    {
        if (!isStarting)
        {
            throw new System.Exception("Run StartGame to AddAction");
        }
        if (queueActionList != null && player == playerList[turnBase])
        {
            queueActionList.Enqueue(action);
        }
    }

    private void Update()
    {
        if (isStarting)
        {
            switch (status)
            {
                case CYCLE_TURN.START_TURN:
                    StartTurn();
                    break;
                case CYCLE_TURN.START_ACTION:
                    ActionStart();
                    break;
                case CYCLE_TURN.ON_ACTION:
                    OnAction();
                    break;
                case CYCLE_TURN.END_ACTION:
                    ActionEnd();
                    break;
                case CYCLE_TURN.END_TURN:
                    EndTurn();
                    break;

            }
        }
    }

    // handle law in game
    private static void OnAction()
    {
        currentAction.OnStartAction();
        currentAction.OnAction();
        currentAction.OnEndAction();


        status = CYCLE_TURN.END_ACTION;
    }

    // check change player when return true
    private static bool CheckChangePlayer()
    {
        return true;
    }

    // Script run before start turn
    private static void StartTurn()
    {
        //if (OnStartTurn != null)
        //{
        //    OnStartTurn(playerList[turnBase]);
        //}
        playerList[turnBase].StartTurn();

        // init history action in one turn
        historyActionList = new List<IAction>();
        queueActionList = new Queue<IAction>();
        // chanage status
        status = CYCLE_TURN.START_ACTION;
    }

    // Script run after end turn
    private static void EndTurn()
    {
        //if (OnEndTurn != null)
        //{
        //    OnEndTurn(playerList[turnBase]);
        //}
        playerList[turnBase].EndTurn();

        // handle change player
        if (CheckChangePlayer())
        {
            ChangePlayer();
        }
        status = CYCLE_TURN.START_TURN;
    }

    // script change player
    private static void ChangePlayer()
    {
        //if (OnChangePlayer != null)
        //{
        //    OnChangePlayer(playerList[NextPlayer()]);
        //}
        turnBase = NextPlayer();
    }

    private static int NextPlayer()
    {
        return (turnBase + 1) % playerList.Count;
    }

    // script run before excute action
    private static void ActionStart()
    {
        if (queueActionList.Count > 0)
        {
            currentAction = queueActionList.Dequeue();
            //if (OnActionStart != null)
            //{
            //    OnActionStart(currentAction);
            //}
            playerList[turnBase].ActionStart();

            status = CYCLE_TURN.ON_ACTION;
        }
    }

    // script run after excute action
    private static void ActionEnd()
    {
        //if (OnActionEnd != null)
        //{
        //    OnActionEnd(currentAction);
        //}
        playerList[turnBase].ActionEnd();

        // add history excuted action
        historyActionList.Add(currentAction);
        // check action end to pass turn
        if (currentAction.GetAction() == ACTION.END_TURN)
        {
            status = CYCLE_TURN.END_TURN;
        }
        else
        {
            status = CYCLE_TURN.START_ACTION;
        }
    }
}
