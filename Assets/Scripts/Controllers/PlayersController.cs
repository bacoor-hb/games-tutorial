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

    private void Start()
    {
        OnStartTurn += StartTurn;
        OnEndTurn += EndTurn;
    }

    void Update()
    {

    }

    private void StartTurn ()
    {
        gameObject.SetActive(true);
    }

    private void EndTurn()
    {
        gameObject.SetActive(false);
    }
}
