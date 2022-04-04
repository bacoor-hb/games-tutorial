using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceWithValueController : MonoBehaviour
{
    public delegate void OnEventCalled<T>(T data);
    public OnEventCalled<int> OnValueChange;
    TransformValue initTransform;
    public DiceWithValue dice;
    Rigidbody rb;
    private bool thrown = false;
    private int value;

    private void Start()
    {
        Init();
    }

    public void Init(Transform dicePos = null)
    {
        if (dicePos != null)
        {
            initTransform = new TransformValue(dicePos);
        }
        else
        {
            initTransform = new TransformValue(transform);
        }
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        dice.OnEnd = ReturnResult;
    }

    public void RollDice(int value)
    {
        if (!thrown)
        {
            transform.position = initTransform.position;
            transform.rotation = initTransform.rotation;
            this.rb.useGravity = true;
            this.thrown = true;
            this.value = value;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (this.thrown && collision.gameObject.CompareTag("Ground"))
        {
            this.dice.RollDice(this.value);
        }
    }

    private void ReturnResult ()
    {
        this.thrown = false;
        this.OnValueChange?.Invoke(this.value);
        transform.position = initTransform.position;
        transform.rotation = initTransform.rotation;
        rb.useGravity = false;
    }
} 
