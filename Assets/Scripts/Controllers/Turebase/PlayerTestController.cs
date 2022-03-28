using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class PlayerTestController : MonoBehaviour
{
    static int count = 0;
    public int id;
    // Use this for initialization
    void Start()
    {
        id = count;
        TurnBaseController.Register(this);
        count++;
        TurnBaseController.OnStartTurn += StartTurn;
        TurnBaseController.OnEndTurn += EndTurn;
        TurnBaseController.OnActionStart += ActionStart;
        TurnBaseController.OnActionEnd += ActionEnd;
        TurnBaseController.OnChangePlayer += ChangePlayer;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            TurnBaseController.AddAction(new ActionEndTurn());
        }

        if (id == 0 && Input.GetKeyDown("space"))
        {
            if (TurnBaseController.isStarting)
            {
                TurnBaseController.EndGame();
            }
            else
            {
                TurnBaseController.StartGame();
            }
        }
    }

    void StartTurn(Object iplayer)
    {
        Material myMaterial = GetComponent<Renderer>().material;
        PlayerTestController player = iplayer as PlayerTestController;
        if (player == this)
        {
            myMaterial.color = Color.red;

        }
        else
        {
            myMaterial.color = Color.grey;
        }
        Debug.Log("StartTurn: id: " + id + ", player: " + player.id);
    }

    void EndTurn(Object iplayer)
    {
        PlayerTestController player = iplayer as PlayerTestController;

        Debug.Log("EndTurn: id: " + id + ", player: " + player.id);

    }
    void ActionStart(IAction action)
    {
        Debug.Log("ActionStart: id: " + id);
    }

    void ActionEnd(IAction action)
    {
        Debug.Log("ActionEnd: id: " + id);
    }

    void ChangePlayer(Object iplayer)
    {
        PlayerTestController player = iplayer as PlayerTestController;

        Debug.Log("ChangePlayer: id: " + id + ", player: " + player.id);
    }
}
