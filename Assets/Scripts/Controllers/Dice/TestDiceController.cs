using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDiceController : MonoBehaviour
{
    [SerializeField]
    private List<Dice> listDice;

    [SerializeField]
    private List<DiceWithValueController> listDiceWithValue;

    [SerializeField]
    List<int> listValue;

    [SerializeField]
    private DiceController diceController;

    private void Start()
    {
        diceController.OnResult = LogResult;
        diceController.OnRoll = LogRolling;
        diceController.SetDice(listDice);
        diceController.SetDiceWithValue(listDiceWithValue);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {            
            diceController.RollDice();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            diceController.RollDice(listValue);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            diceController.SetDice(listDice);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            diceController.SetDiceWithValue(listDiceWithValue);
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
