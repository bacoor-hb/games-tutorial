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
            ActionTureBase action = new ActionTureBase() {
                action = 1,
                param = new Dictionary<string, string>()
                {
                    { "test", "1" }
                }
            };
            TurnBaseController.AddAction(action);
        }
    }

    void StartTurn(PlayerTestController player)
    {
        Material myMaterial = GetComponent<Renderer>().material;
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

    void EndTurn(PlayerTestController player)
    {
        Debug.Log("EndTurn: id: " + id + ", player: " + player.id);

    }
    void ActionStart(ActionTureBase action)
    {
        Debug.Log("ActionStart: id: " + id);
    }

    void ActionEnd(ActionTureBase action)
    {
        Debug.Log("ActionEnd: id: " + id);
    }

    void ChangePlayer(PlayerTestController player)
    {
        Debug.Log("ChangePlayer: id: " + id + ", player: " + player.id);
    }
}
