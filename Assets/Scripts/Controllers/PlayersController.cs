using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayersController : MonoBehaviour
{
    public delegate void Event();
    public Event OnStartTurn;
    public Event OnEndTurn;
    public Event OnAction;
    public Event OnActionStart;
    public Event OnActionEnd;
    public Event OnChangePlayer;


    private int playerId;
    private bool isPlayerTurn = false;

    public void InitPlayer(int _playerId)
    {
        playerId = _playerId;
    }

    public void nextPlayerTurn()
    {
        isPlayerTurn = true;
        StartTurn();
    }

    private void StartTurn()
    {
        Debug.Log("Start Turn" + playerId);
        OnStartTurn?.Invoke(playerId);
    }

    private void EndTurn()
    {
        Debug.Log("End Turn" + playerId);
        OnStartTurn?.Invoke(playerId);
    }

    private void OnChangePlayerEvent()
    {
        Debug.Log("OnChagnePlayer" + playerId);
        //OnChangePlayer?.Invoke(playerId);
    }



    void Update()
    {
        if (isPlayerTurn)
        {

        }
    }
}
