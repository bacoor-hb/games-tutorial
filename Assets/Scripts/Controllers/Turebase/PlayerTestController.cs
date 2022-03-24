using UnityEngine;
using System.Collections;

public class PlayerTestController : MonoBehaviour
{
    static int count = 0;
    int id;
    // Use this for initialization
    void Start()
    {
        id = count;
        TurnBaseController.Register(id);
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
            ActionTureBase action = new ActionTureBase();
            TurnBaseController.AddAction(action);
        }
    }

    void StartTurn(int playerId)
    {
        Material myMaterial = GetComponent<Renderer>().material;
        if (playerId == id)
        {
            myMaterial.color = Color.red;

        }
        else
        {
            myMaterial.color = Color.grey;
        }
        Debug.Log("StartTurn: id: " + id + ", player: " + playerId);
    }

    void EndTurn(int playerId)
    {
        Debug.Log("EndTurn: id: " + id + ", player: " + playerId);

    }
    void ActionStart(ActionTureBase action)
    {
        Debug.Log("ActionStart: id: " + id);
    }

    void ActionEnd(ActionTureBase action)
    {
        Debug.Log("ActionEnd: id: " + id);
    }

    void ChangePlayer(int playerId)
    {
        Debug.Log("ChangePlayer: id: " + id + ", player: " + playerId);


    }
}
