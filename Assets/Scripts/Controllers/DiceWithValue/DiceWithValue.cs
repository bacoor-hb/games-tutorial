using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceWithValue : MonoBehaviour
{
    public delegate void OnEventCalled();
    public OnEventCalled OnEnd;
    Animator animator;
    private bool thrown = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("diceAnimation", 0);
        animator.SetInteger("diceValue", 0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RollDice(2);
        }
    }

    public void RollDice(int value)
    {
        Debug.Log("aaaaa");
        if (!thrown)
        {
            StartCoroutine(RolldiceAnimation(value));
        }
    }

    IEnumerator RolldiceAnimation(int value)
    {
        animator.SetBool("drop", false);
        thrown = true;
        animator.SetInteger("diceValue", value);
        yield return new WaitForSeconds(3f);
        thrown = false;
        OnEnd?.Invoke();
        animator.SetInteger("diceValue", 0);
    }

    float GetDiceAnimation ()
    {
        float[] arr = new float[3];
        arr[0] = 0;
        arr[1] = 0.5f;
        arr[2] = 1;
        return arr[Random.Range(0, arr.Length - 1)];
    }

    public void DropDice ()
    {
        animator.SetBool("drop", true);
        animator.SetFloat("diceAnimation", GetDiceAnimation());
    }
}
