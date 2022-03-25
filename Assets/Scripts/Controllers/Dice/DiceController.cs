using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceController : MonoBehaviour
{
    public delegate void OnEventCalled<T>(T data);
    public OnEventCalled<int> OnRoll;
    public OnEventCalled<List<int>> OnResult;

    private bool isRolling = false;
    private List<Dice> dices = new List<Dice>();
    private List<int> diceValues = new List<int>();

    [SerializeField]
    private List<Transform> diceSpawnPos;
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        diceValues = new List<int>();
        isRolling = false;
    }

    /// <summary>
    /// Initialize the dice Instance
    /// </summary>
    /// <param name="_dices"></param>
    public bool SetDice(List<Dice> _dices)
    {
        if(_dices.Count != diceSpawnPos.Count)
        {
            Debug.LogError("Number of dices is different than the number of spawning position...");
            return false;
        }

        dices.Clear();
        diceValues.Clear();
        for(int i = 0; i < _dices.Count; i++)
        {
            _dices[i].Init(diceSpawnPos[i]);
            _dices[i].OnValueChange = SetDiceValue;
            dices.Add(_dices[i]);
        }

        //Hide all Dice
        SetActiveAllDice(false);
        return true;
    }

    /// <summary>
    /// Roll Action
    /// </summary>
    public void RollDice()
    {
        if (!isRolling)
        {
            //Show All dice
            SetActiveAllDice(true);
            diceValues.Clear();
            isRolling = true;
            for(int i = 0; i < dices.Count; i++)
            {
                OnRoll?.Invoke(i);
                dices[i].RollDice();
            }
        } 
        else
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
        diceValues.Add(value);
        if (diceValues.Count.Equals(dices.Count))
        {
            isRolling = false;
            OnResult?.Invoke(diceValues);
        }
    }

    /// <summary>
    /// Set the dice enable/disable
    /// </summary>
    /// <param name="state">false = Hide all Dice</param>
    public void SetActiveAllDice(bool state)
    {
        if(dices != null && dices.Count > 0)
        {
            for(int i = 0; i < dices.Count; i++)
            {
                dices[i].gameObject.SetActive(state);
            }
        }
    }
}  
