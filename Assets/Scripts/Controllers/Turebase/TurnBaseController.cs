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

    public delegate void Event();
    public Event OnStartGame;
    public Event OnEndGame;

    public bool isStarting = false;
    public  List<IPlayer> playerList = new List<IPlayer>();
    private Queue<IAction> queueActionList;
    private  IAction currentAction;
    private  int turnBase = 0;
    private  CYCLE_TURN status = CYCLE_TURN.START_TURN;

    /// <summary>
    /// call StartGame to start game
    /// </summary>
    public void StartGame()
    {
        if (!isStarting)
        {
            isStarting = true;
            turnBase = 0;
            status = CYCLE_TURN.START_TURN;
            if (OnStartGame != null)
            {
                this.OnStartGame();
            }
        }
    }

    /// <summary>
    /// call EndGame to stop game
    /// </summary>
    public void EndGame()
    {
        isStarting = false;
        if (OnEndGame != null)
        {
            this.OnEndGame();
        }
    }

    /// <summary>
    /// regist game
    /// </summary>
    public void Register(IPlayer player)
    {
        if (isStarting)
        {
            throw new System.Exception("Game start");
        }
        playerList.Add(player);
    }

    /// <summary>
    /// add action when your turn
    /// </summary>
    public void AddAction(IPlayer player, IAction action)
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

    private void Start()
    {
        isStarting = false;
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
    private void OnAction()
    {
        currentAction.OnStartAction();
        currentAction.OnAction();
        currentAction.OnEndAction();


        status = CYCLE_TURN.END_ACTION;
    }

    // check change player when return true
    private bool CheckChangePlayer()
    {
        return true;
    }

    /// <summary>
    ///  Script run before start turn
    /// </summary>
    private void StartTurn()
    {
        //if (OnStartTurn != null)
        //{
        //    OnStartTurn(playerList[turnBase]);
        //}
        playerList[turnBase].StartTurn();

        // init history action in one turn
        queueActionList = new Queue<IAction>();
        // chanage status
        status = CYCLE_TURN.START_ACTION;
    }


    /// <summary>
    ///  Script run after end turn
    /// </summary>
    private void EndTurn()
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

    /// <summary>
    ///  Script run change player
    /// </summary>
    private void ChangePlayer()
    {
        //if (OnChangePlayer != null)
        //{
        //    OnChangePlayer(playerList[NextPlayer()]);
        //}
        turnBase = NextPlayer();
    }

    private int NextPlayer()
    {
        return (turnBase + 1) % playerList.Count;
    }

    /// <summary>
    ///  Script run before excute action
    /// </summary>
    private void ActionStart()
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

    
    /// <summary>
    ///  Script run after excute action
    /// </summary>
    private void ActionEnd()
    {
        //if (OnActionEnd != null)
        //{
        //    OnActionEnd(currentAction);
        //}
        playerList[turnBase].ActionEnd();

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