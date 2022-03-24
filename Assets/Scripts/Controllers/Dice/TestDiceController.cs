using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDiceController : MonoBehaviour
{
    // List xí ngầu
    [SerializeField]
    private List<GameObject> listDice;

    // List Value cơ cấu ứng với xí ngầu
    // Chỉ cơ cấu đúng theo với những điều kiện cho trước y = 10 rotate (0, 0, 0)
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
