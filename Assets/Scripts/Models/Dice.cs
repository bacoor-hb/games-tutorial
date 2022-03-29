 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Dice : MonoBehaviour
{
    public delegate void OnEventCalled<T>(T data);
    public OnEventCalled<int> OnValueChange;

    Rigidbody rb;
    Animator animator;
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
        animator = GetComponent<Animator>();

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
        if (value == -1)
        {
            if (!thrown)
            {
                transform.position = initTransform.position;
                transform.rotation = initTransform.rotation;
                hasLanded = false;
                thrown = true;
                rb.AddTorque(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500));
            }
        }
        else
        {
            if (!thrown)
            {
                StartCoroutine(RollDiceWithValue(value));
            }
        }
    }

    public IEnumerator RollDiceWithValue(int value)
    {
        thrown = true;
        animator.SetInteger("diceValue", 0);
        yield return new WaitForSeconds(1);
        animator.SetInteger("diceValue", value);
        yield return new WaitForSeconds(4);
        OnValueChange?.Invoke(value);
        thrown = false;
        yield return null;
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
