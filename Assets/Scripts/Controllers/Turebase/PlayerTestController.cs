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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            TurnBaseController.AddAction(this, new ActionEndTurn());
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

    public void StartTurn()
    {
        Material myMaterial = GetComponent<Renderer>().material;
        myMaterial.color = Color.red;
        Debug.Log("StartTurn: id: " + id);
    }

    public void EndTurn()
    {
        Material myMaterial = GetComponent<Renderer>().material;
        myMaterial.color = Color.white;
        Debug.Log("EndTurn: id: " + id);

    }
    public void ActionStart()
    {
        Debug.Log("ActionStart: id: " + id);
    }

    public void ActionEnd()
    {
        Debug.Log("ActionEnd: id: " + id);
    }
}
