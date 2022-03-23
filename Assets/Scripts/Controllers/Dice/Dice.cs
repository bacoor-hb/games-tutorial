using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public delegate void OnEventCalled<T>(T data);
    public OnEventCalled<int> OnValueChange;
    Rigidbody rb;
    private bool hasLanded = false;
    private bool thrown = false;

    Vector3 initPos;

    private int diceValue;

    private DiceValue[] diceValues;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        initPos = transform.position;
        rb.useGravity = false;
        diceValues = GetComponentsInChildren<DiceValue>();
    }

    private void Update()
    {
        if (rb.IsSleeping() && !hasLanded && thrown)
        {
            thrown = false;
            hasLanded = true;
            rb.useGravity = false;
            DiceValueCheck();
        }
    }

    public void RollDice()
    {
        if (!thrown)
        {
            transform.position = initPos;
            hasLanded = false;
            thrown = true;
            rb.useGravity = true;
            rb.AddTorque(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500));
        }
    }

    void DiceValueCheck()
    {
        diceValue = 0;
        foreach (DiceValue dice in diceValues)
        {
            if (dice.Onground())
            {
                diceValue = dice.Value();
                OnValueChange?.Invoke(diceValue);
            }
        }
    }
}
