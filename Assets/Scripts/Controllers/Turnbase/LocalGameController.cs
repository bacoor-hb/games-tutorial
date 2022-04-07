using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalGameController : MonoBehaviour
{
    [SerializeField]
    private List<PlayerTestController> players;

    [SerializeField]
    private TurnBaseController turnBaseController;

    // Start is called before the first frame update
    void Start()
    {
        //Set Player id ~ The turn order
        for (int i = 0; i < players.Count; i++)
        {
            players[i].SetPlayerID(i);
            turnBaseController.Register(players[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            int currentPlayer = turnBaseController.currentPlayer;
            turnBaseController.AddAction(players[currentPlayer], players[currentPlayer].GetAction(ACTION_TYPE.PURCHASE));
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            int currentPlayer = turnBaseController.currentPlayer;
            turnBaseController.AddAction(players[currentPlayer], players[currentPlayer].GetAction(ACTION_TYPE.AUCTION));
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            int currentPlayer = turnBaseController.currentPlayer;
            turnBaseController.AddAction(players[currentPlayer], players[currentPlayer].GetAction(ACTION_TYPE.END_TURN));
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (turnBaseController.isStarting)
            {
                turnBaseController.EndGame();
            }
            else
            {
                turnBaseController.StartGame();
            }
        }
    }
}
