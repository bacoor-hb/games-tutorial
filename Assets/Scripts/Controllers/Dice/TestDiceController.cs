using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDiceController : MonoBehaviour
{
    [SerializeField]
    private List<Dice> listDice;

    [SerializeField]
    private DiceController diceController;

    [SerializeField]
    private List<int> listValue;

    private void Start()
    {
        diceController.OnResult = LogResult;
        diceController.OnRoll = LogRolling;
        diceController.SetDice(listDice);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {            
            diceController.RollDice();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            diceController.RollDice(listValue);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            diceController.SetDice(listDice);
        }
    }

    private void LogResult (List<int> result)
    {
        string stringArray = "[";
        for (int i = 0; i < result.Count; i++)
        {
            if (i == 0)
            {
                stringArray += result[i];
            } else
            {
                stringArray += ", " + result[i];
            }
        }
        stringArray += "]";
        Debug.Log(stringArray);
    } 

    private void LogRolling(int diceID)
    {
        Debug.Log("DIce " + diceID + " is rolling...");
    }
}
