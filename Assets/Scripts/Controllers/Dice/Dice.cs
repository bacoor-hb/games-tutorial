using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Dice : MonoBehaviour
{
    public delegate void OnEventCalled<T>(T data);
    public OnEventCalled<int> OnValueChange;

    Rigidbody rb;
    private bool hasLanded = false;
    private bool thrown = false;

    TransformValue initTransform;

    private int diceValue;

    [SerializeField]
    private DiceDataSet dataSet;

    [SerializeField]
    private DiceValue[] diceValues;

    private void Start()
    {
        Init(transform);
    }

    /// <summary>
    /// Initialize the Dice
    /// </summary>
    /// <param name="dicePos"></param>
    public void Init(Transform dicePos = null)
    {
        rb = GetComponent<Rigidbody>();

        if (dicePos != null)
        {
            initTransform = new TransformValue(dicePos);
        }
        else
        {
            initTransform = new TransformValue(transform);
        }
        
        //rb.useGravity = false;
        diceValue = -1;
    }

    private void Update()
    {
        if (rb.IsSleeping() && !hasLanded && thrown)
        {
            thrown = false;
            hasLanded = true;
            //rb.useGravity = false;
            DiceValueCheck();
        }
    }

    public void RollDice(int value = -1)
    {
        if (!thrown)
        {
            transform.position = initTransform.position;
            transform.rotation = initTransform.rotation;
            hasLanded = false;
            thrown = true;
            DiceForce diceForce = GetDiceForceFromValue(value);
            //rb.useGravity = true;
            rb.AddTorque(diceForce.x, diceForce.y, diceForce.z);
        }
    }

    private DiceForce GetDiceForceFromValue(int value)
    {
        return value switch
        {
            1 => dataSet.diceForces1[Random.Range(0, dataSet.diceForces1.Count -1)],
            2 => dataSet.diceForces2[Random.Range(0, dataSet.diceForces2.Count -1)],
            3 => dataSet.diceForces3[Random.Range(0, dataSet.diceForces3.Count -1)],
            4 => dataSet.diceForces4[Random.Range(0, dataSet.diceForces4.Count -1)],
            5 => dataSet.diceForces5[Random.Range(0, dataSet.diceForces5.Count -1)],
            6 => dataSet.diceForces6[Random.Range(0, dataSet.diceForces6.Count -1)],
            _ => new DiceForce(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500)),
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

public class TransformValue
{
    public Vector3 position;
    public Quaternion rotation;

    public TransformValue()
    {
        position = Vector3.zero;
        rotation = Quaternion.identity;
    }

    public TransformValue(Transform transform)
    {
        position = transform.position;
        rotation = transform.rotation;
    }
}
