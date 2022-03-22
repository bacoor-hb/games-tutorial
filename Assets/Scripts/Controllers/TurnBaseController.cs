using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurnBaseController : Singleton<TurnBaseController>
{
    public delegate void Event<T>(T data);
    public Event<int> OnStartTurn;
    public Event<int> OnEndTurn;
    public Event<int> OnChangePlayer;

    public delegate void EventAction();
    public EventAction OnAction;
    public EventAction OnActionStart;
    public EventAction OnActionEnd;


    private bool isPlayerTurn = false;
    private int playerId;

    public void InitPlayer(int _playerId)
    {
        playerId = _playerId;
    }

    public void StartPlayerTurn()
    {
        isPlayerTurn = true;
    }

    public void EndPlayerTurn()
    {
        isPlayerTurn = false;
    }

    public void OnChangePlayerEvent()
    {
        OnChangePlayer?.Invoke(playerId);
    }

}
