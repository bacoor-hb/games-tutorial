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

<<<<<<< HEAD
    [SerializeField]
    private DiceDataSet diceDataSet;

    [SerializeField]
    Vector3 initPos;
    
    [SerializeField]
    Quaternion initRota;
=======
    TransformValue initTransform;
>>>>>>> 0d386a7117f08362cd3573ead178ade74a56c2c8

    private int diceValue;

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
<<<<<<< HEAD
        initPos = transform.position;
        initRota = transform.rotation;
        rb.useGravity = false;
        diceValues = GetComponentsInChildren<DiceValue>();
=======

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
>>>>>>> 0d386a7117f08362cd3573ead178ade74a56c2c8
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

    public void RollDice()
    {
        if (!thrown)
        {
<<<<<<< HEAD
            transform.position = initPos;
            transform.rotation = initRota;
=======
            transform.position = initTransform.position;
>>>>>>> 0d386a7117f08362cd3573ead178ade74a56c2c8
            hasLanded = false;
            thrown = true;
            //rb.useGravity = true;
            rb.AddTorque(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500));
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
