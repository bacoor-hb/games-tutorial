using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceController : MonoBehaviour
{
    public delegate void OnEventCalled<T>(T data);
    public OnEventCalled<List<int>> OnResult;
    private bool isRoll = false;
    private List<Dice> dices = new List<Dice>();
    private List<int> diceValues = new List<int>();

    public void RollDice(List<GameObject> dices)
    {
        if (!isRoll)
        {
            this.isRoll = true;
            this.dices.Clear();
            this.diceValues.Clear();
            foreach (var item in dices)
            {
                Dice dice = item.gameObject.GetComponent<Dice>();
                dice.OnValueChange = SetDiceValue;
                dice.RollDice();
                this.dices.Add(dice);
            }
        } else
        {
            Debug.Log("Dice is rolling");
        }
    }

    public void RollDiceWithValue(List<GameObject> dices, List<int> values)
    {
        if (!isRoll)
        {
            this.isRoll = true;
            this.dices.Clear();
            this.diceValues.Clear();
            for (int i = 0; i < dices.Count; i++)
            {
                Dice dice = dices[i].gameObject.GetComponent<Dice>();
                dice.OnValueChange = SetDiceValue;
                dice.RollDiceWithValue(values[i]);
                this.dices.Add(dice);
            }
        }
        else
        {
            Debug.Log("Dice is rolling");
        }
    }

    private void SetDiceValue (int value)
    {
        this.diceValues.Add(value);
        if (this.diceValues.Count.Equals(this.dices.Count))
        {
            this.isRoll = false;
            OnResult?.Invoke(this.diceValues);
        }
    }
}  
