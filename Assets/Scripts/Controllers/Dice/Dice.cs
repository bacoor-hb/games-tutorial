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

    [SerializeField]
    private DiceDataSet diceDataSet;

    [SerializeField]
    private float test1 = 1;

    [SerializeField]
    private float test2 = 1;

    [SerializeField]
    private float test3 = 1;

    [SerializeField]
    Vector3 initPos;
    
    [SerializeField]
    Quaternion initRota;

    private int diceValue;

    private DiceValue[] diceValues;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        initPos = transform.position;
        initRota = transform.rotation;
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
            transform.rotation = initRota;
            hasLanded = false;
            thrown = true;
            rb.useGravity = true;
            rb.AddTorque(test1, test2, test3);
        }
    }

    public void RollDiceWithValue(int value)
    {
        DiceForce diceForce = this.GetForceFromValue(value);
        if (!thrown)
        {
            transform.position = initPos;
            transform.rotation = initRota;
            hasLanded = false;
            thrown = true;
            rb.useGravity = true;
            rb.AddTorque(diceForce.x, diceForce.y, diceForce.z);
        }
    }

    private DiceForce GetForceFromValue(int value)
    {
        return value switch
        {
            1 => diceDataSet.diceForces1[Random.Range(0, diceDataSet.diceForces1.Count)],
            2 => diceDataSet.diceForces2[Random.Range(0, diceDataSet.diceForces2.Count)],
            3 => diceDataSet.diceForces3[Random.Range(0, diceDataSet.diceForces3.Count)],
            4 => diceDataSet.diceForces4[Random.Range(0, diceDataSet.diceForces4.Count)],
            5 => diceDataSet.diceForces5[Random.Range(0, diceDataSet.diceForces5.Count)],
            6 => diceDataSet.diceForces6[Random.Range(0, diceDataSet.diceForces6.Count)],
            _ => diceDataSet.diceForces6[Random.Range(0, diceDataSet.diceForces6.Count)],
        };
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
