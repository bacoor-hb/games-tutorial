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

    public void RollDice(int value)
    {
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
        yield return new WaitForSeconds(5f);
        thrown = false;
        OnEnd?.Invoke();
        animator.SetInteger("diceValue", 0);
    }

    float GetDiceAnimation ()
    {
        List<float> listAnimationValue = new List<float> ();
        listAnimationValue.Add(0);
        listAnimationValue.Add(0.5f);
        listAnimationValue.Add(1);
        return listAnimationValue[Random.Range(0, listAnimationValue.Count - 1)];
    }

    public void DropDice ()
    {
        animator.SetBool("drop", true);
        animator.SetFloat("diceAnimation", GetDiceAnimation());
    }
}
