using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBaseController : MonoBehaviour
{
    public delegate void Event<T>(T data);
    public Event<int> OnStartGame;
    public Event<int> OnEndGame;
    public Event<int> OnStartTurn;
    public Event<int> OnEndTurn;
    public Event<ACTION_TYPE> OnStartAction;
    public Event<ACTION_TYPE> OnEndAction;
    public Event<int> OnChangePlayer;

    public delegate void Callback();
    public Callback OnStepStatus;



    public bool isStarting = false;
    public int currentPlayer { get; private set; }
    public List<IPlayer> playerList = new List<IPlayer>();

    private Action currentAction;
    private Queue<Action> queueActionList;

    private CYCLE_TURN status = CYCLE_TURN.START_TURN;
    private bool isWaiting = false;

    #region Unity Event
    private void Start()
    {
        isStarting = false;
        OnStepStatus = StepCycleTurn;
    }

    private void Update()
    {
        // check game start, and status waiting
        if (isStarting && !isWaiting)
        {
            switch (status)
            {
                case CYCLE_TURN.START_TURN:
                    StartTurn();
                    break;
                case CYCLE_TURN.START_ACTION:
                    StartAction();
                    break;
                case CYCLE_TURN.ON_ACTION:
                    OnAction();
                    break;
                case CYCLE_TURN.END_ACTION:
                    EndAction();
                    break;
                case CYCLE_TURN.END_TURN:
                    EndTurn();
                    break;
            }
        }
    }
    #endregion

    #region Action
    /// <summary>
    /// add action when your turn
    /// </summary>
    public void AddAction(IPlayer player, Action action)
    {
        if (isStarting)
        {
            if (queueActionList != null && player == playerList[currentPlayer])
            {
                queueActionList.Enqueue(action);
            }
        }
        else
        {
            Debug.LogError("[AddAction] ERROR: Game is not start.");
        }
    }

    /// <summary>
    /// handle law in game
    /// </summary>
    private void OnAction()
    {
        isWaiting = true;
        currentAction.OnAction(OnStepStatus);
    }

    /// <summary>
    ///  Script run before excute action
    /// </summary>
    private void StartAction()
    {
        if (queueActionList.Count > 0)
        {
            currentAction = queueActionList.Dequeue();

            OnStartAction?.Invoke(currentAction.GetAction());
            playerList[currentPlayer].StartAction();
            currentAction.OnStartAction();

            StepCycleTurn();
        }
    }

    /// <summary>
    ///  Script run after excute action
    /// </summary>
    private void EndAction()
    {
        OnEndAction?.Invoke(currentAction.GetAction());
        playerList[currentPlayer].EndAction();
        currentAction.OnEndAction();

        StepCycleTurn();
    }
    #endregion

    #region Turn Management
    /// <summary>
    ///  Step cycle turn
    /// </summary>
    private void StepCycleTurn()
    {
        isWaiting = false;
        switch (status)
        {
            case CYCLE_TURN.START_TURN:
                status = CYCLE_TURN.START_ACTION;
                break;
            case CYCLE_TURN.START_ACTION:
                status = CYCLE_TURN.ON_ACTION;
                break;
            case CYCLE_TURN.ON_ACTION:
                status = CYCLE_TURN.END_ACTION;
                break;
            case CYCLE_TURN.END_ACTION:
                if (currentAction.GetAction() == ACTION_TYPE.END_TURN)
                {
                    status = CYCLE_TURN.END_TURN;
                }
                else
                {
                    status = CYCLE_TURN.START_ACTION;
                }
                break;
            case CYCLE_TURN.END_TURN:
                status = CYCLE_TURN.START_TURN;
                break;
        }
    }

    /// <summary>
    /// call StartGame to start game
    /// </summary>
    public void StartGame()
    {
        if (!isStarting)
        {
            isStarting = true;
            currentPlayer = 0;
            OnStartGame?.Invoke(currentPlayer);

            status = CYCLE_TURN.START_TURN;
            isWaiting = false;
        }
    }

    /// <summary>
    /// call EndGame to stop game
    /// </summary>
    public void EndGame()
    {
        if (isStarting)
        {
            isStarting = false;

            OnEndGame?.Invoke(currentPlayer);
        }
    }

    /// <summary>
    ///  Script run before start turn
    /// </summary>
    private void StartTurn()
    {
        // init history action in one turn
        queueActionList = new Queue<Action>();

        //Trigger the Start turn function of the current player
        playerList[currentPlayer].StartTurn();
        OnStartTurn?.Invoke(currentPlayer);

        StepCycleTurn();
    }


    /// <summary>
    ///  Script run after end turn
    /// </summary>
    private void EndTurn()
    {
        //Trigger the End turn function of the current player
        playerList[currentPlayer].EndTurn();
        OnEndTurn?.Invoke(currentPlayer);

        // handle change player
        if (CheckChangePlayer())
        {
            ChangePlayer();
        }
        StepCycleTurn();
    }

    /// <summary>
    ///  Modify the currentplayer Id. This function will also trigger the OnChangePlayer event, then reset it.
    /// </summary>
    private void ChangePlayer()
    {
        currentPlayer = GetNextPlayerId();

        //Invoke 1 time OnChangePlayer event and then reset it.
        OnChangePlayer?.Invoke(currentPlayer);
        OnChangePlayer = null;
    }

    /// <summary>
    /// Get the Id of next player in the list
    /// </summary>
    /// <returns></returns>
    private int GetNextPlayerId()
    {
        return (currentPlayer + 1) % playerList.Count;
    }

    /// <summary>
    /// check change player when return true
    /// </summary>
    /// <returns></returns>
    private bool CheckChangePlayer()
    {
        return true;
    }

    /// <summary>
    /// regist game
    /// </summary>
    public void Register(IPlayer player)
    {
        if (isStarting)
        {
            Debug.LogError("[Register]: ERROR: Game is start.");
        }
        else
        {
            playerList.Add(player);
        }
    }
    #endregion
}