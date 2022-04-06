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
    public Event<int> OnActionStart;
    public Event<int> OnActionEnd;
    public Event<int> OnChangePlayer;

    public bool isStarting = false;
    public List<IPlayer> playerList = new List<IPlayer>();
    private Queue<Action> queueActionList;
    private Action currentAction;
    public int currentPlayer { get; private set; }
    private CYCLE_TURN status = CYCLE_TURN.START_TURN;

    #region Unity Event
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
    #endregion

    #region Action
    /// <summary>
    /// add action when your turn
    /// </summary>
    public void AddAction(IPlayer player, Action action)
    {
        if (!isStarting)
        {
            Debug.LogError("[AddAction] ERROR: Game is not start.");
        }
        if (queueActionList != null && player == playerList[currentPlayer])
        {
            queueActionList.Enqueue(action);
        }
    }

    /// <summary>
    /// handle law in game
    /// </summary>
    private void OnAction()
    {
        currentAction.OnAction();

        status = CYCLE_TURN.END_ACTION;
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
            playerList[currentPlayer].ActionStart();
            currentAction.OnStartAction();

            status = CYCLE_TURN.ON_ACTION;
        }
    }

    /// <summary>
    ///  Script run after excute action
    /// </summary>
    private void ActionEnd()
    {
        playerList[currentPlayer].ActionEnd();
        currentAction.OnEndAction();

        // check action end to pass turn
        if (currentAction.GetAction() == ACTION_TYPE.END_TURN)
        {
            status = CYCLE_TURN.END_TURN;
        }
        else
        {
            status = CYCLE_TURN.START_ACTION;
        }
    }
    #endregion

    #region Turn Management
    /// <summary>
    /// call StartGame to start game
    /// </summary>
    public void StartGame()
    {
        if (!isStarting)
        {
            isStarting = true;
            currentPlayer = 0;
            status = CYCLE_TURN.START_TURN;

            OnStartGame?.Invoke(currentPlayer);
        }
    }

    /// <summary>
    /// call EndGame to stop game
    /// </summary>
    public void EndGame()
    {
        isStarting = false;
        OnEndGame?.Invoke(currentPlayer);        
    }

    /// <summary>
    ///  Script run before start turn
    /// </summary>
    private void StartTurn()
    {
        //Trigger the Start turn function of the current player
        playerList[currentPlayer].StartTurn();

        OnStartTurn?.Invoke(currentPlayer);

        // init history action in one turn
        queueActionList = new Queue<Action>();
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
        playerList[currentPlayer].EndTurn();

        // handle change player
        if (CheckChangePlayer())
        {
            ChangePlayer();
        }
        status = CYCLE_TURN.START_TURN;
    }

    /// <summary>
    ///  Modify the currentplayer Id. This function will also trigger the OnChangePlayer event, then reset it.
    /// </summary>
    private void ChangePlayer()
    {
        //if (OnChangePlayer != null)
        //{
        //    OnChangePlayer(playerList[NextPlayer()]);
        //}
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
            throw new System.Exception("Game start");
        }
        playerList.Add(player);
    }
    #endregion
}