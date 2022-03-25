using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBaseController : MonoBehaviour
{
    public delegate void Event<T>(T data);
    public static Event<IPlayer> OnStartTurn;
    public static Event<IPlayer> OnEndTurn;
    public static Event<int> OnActionStart;
    public static Event<int> OnActionEnd;
    public static Event<IPlayer> OnChangePlayer;


    public static bool startGamge = false;
    public static List<IPlayer> playerList = new List<IPlayer>();
    private static List<int> historyActionList;
    private static Queue<int> queueActionList;
    private static int currentAction;
    private static int turnBase = 0;
    private static int status = TurnBaseConstants.START_TURN;


    public static void StartGame()
    {
        if (!startGamge)
        {
            startGamge = true;
            turnBase = 0;
            status = TurnBaseConstants.START_TURN;
        }
    }

    public static void EndGame()
    {
        startGamge = false;
    }

    public static void Register(IPlayer player)
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

    public static void AddAction(int action)
    {
        if (!startGamge)
        {
            throw new System.Exception("Run StartGame to AddAction");
        }
        if (queueActionList != null)
        {
            queueActionList.Enqueue(action);
        }
    }

    private void Update()
    {
        if (startGamge)
        {
            switch (status)
            {
                case TurnBaseConstants.START_TURN:
                    StartTurn();
                    break;
                case TurnBaseConstants.START_ACTION:
                    ActionStart();
                    break;
                case TurnBaseConstants.ON_ACTION:
                    OnAction();
                    break;
                case TurnBaseConstants.END_ACTION:
                    ActionEnd();
                    break;
                case TurnBaseConstants.END_TURN:
                    EndTurn();
                    break;

            }
        }

        // this code to test to start game
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
    private static void OnAction()
    {
        Debug.Log("Action:" + currentAction.ToString());
        switch (currentAction)
        {
            // action su dung the ra tu
            case TurnBaseConstants.ACTION_RELEASE_CARD:
                playerList[turnBase].OnReleaseCard();
                break;

            // action gieo xuc xac
            case TurnBaseConstants.ACTION_ROLL_DICE:
                playerList[turnBase].OnRollDice();
                break;

            case TurnBaseConstants.ACTION_RUN_THE_CELL:
                playerList[turnBase].OnActionCell();
                break;

            // action mua dat
            case TurnBaseConstants.ACTION_PUNCHARE:
                playerList[turnBase].OnPurchase();
                break;

            // action xay nha
            case TurnBaseConstants.ACTION_BUILDING:
                playerList[turnBase].OnBuilding();
                break;

            // action dau gia
            case TurnBaseConstants.ACTION_AUCTION:
                playerList[turnBase].OnAuctions();
                break;

            // action ket thuc luot
            case TurnBaseConstants.ACTION_END_TURN:
                //
                break;
        }

        status = TurnBaseConstants.END_ACTION;
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
        // init history action in one turn
        historyActionList = new List<int>();
        queueActionList = new Queue<int>();
        // chanage status
        status = TurnBaseConstants.START_ACTION;
    }

    // Script run after end turn
    private static void EndTurn()
    {
        if (OnEndTurn != null)
        {
            OnEndTurn(playerList[turnBase]);
        }

        // handle change player
        if (CheckChangePlayer())
        {
            ChangePlayer();
        }
        status = TurnBaseConstants.START_TURN;
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
    private static void ActionStart()
    {
        if (queueActionList.Count > 0)
        {
            currentAction = queueActionList.Dequeue();
            if (OnActionStart != null)
            {
                OnActionStart(currentAction);
            }
            status = TurnBaseConstants.ON_ACTION;
        }
    }

    // script run after excute action
    private static void ActionEnd()
    {
        if (OnActionEnd != null)
        {
            OnActionEnd(currentAction);
        }
        // add history excuted action
        historyActionList.Add(currentAction);
        // check action end to pass turn
        if (currentAction == TurnBaseConstants.ACTION_END_TURN)
        {
            status = TurnBaseConstants.END_TURN;
        }
        else
        {
            status = TurnBaseConstants.START_ACTION;
        }
    }
}
