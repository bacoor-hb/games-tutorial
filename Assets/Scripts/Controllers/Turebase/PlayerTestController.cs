using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class PlayerTestController : MonoBehaviour, IPlayer
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
            TurnBaseController.AddAction(1);
        }
    }

    void StartTurn(IPlayer iplayer)
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

    void EndTurn(IPlayer iplayer)
    {
        PlayerTestController player = iplayer as PlayerTestController;

        Debug.Log("EndTurn: id: " + id + ", player: " + player.id);

    }
    void ActionStart(int action)
    {
        Debug.Log("ActionStart: id: " + id);
    }

    void ActionEnd(int action)
    {
        Debug.Log("ActionEnd: id: " + id);
    }

    void ChangePlayer(IPlayer iplayer)
    {
        PlayerTestController player = iplayer as PlayerTestController;

        Debug.Log("ChangePlayer: id: " + id + ", player: " + player.id);
    }

    public void OnReleaseCard() { }
    public void OnRollDice() { }
    public void OnActionCell() { }
    public void OnPurchase() { }
    public void OnBuilding() { }
    public void OnAuctions() { }
}
