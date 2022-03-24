using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDiceController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> listDice;

    [SerializeField]
    private List<int> valuesDice;

    [SerializeField]
    private DiceController diceController;

    private void Start()
    {
        diceController.OnResult = LodResult;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            diceController.RollDice(listDice);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            diceController.RollDiceWithValue(listDice, valuesDice);
        }
    }

    private void LodResult (List<int> result)
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
}
