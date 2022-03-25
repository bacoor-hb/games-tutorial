﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDiceController : MonoBehaviour
{
    // List xí ngầu
    [SerializeField]
    private List<Dice> listDice;

    // List Value cơ cấu ứng với xí ngầu
    // Chỉ cơ cấu đúng theo với những điều kiện cho trước y = 10 rotate (0, 0, 0)
    [SerializeField]
    private List<int> valuesDice;

    [SerializeField]
    private DiceController diceController;

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
        if (Input.GetKeyDown(KeyCode.Z))
        {
            diceController.SetDice(listDice);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            diceController.RollDiceWithValue(listDice, valuesDice);
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
