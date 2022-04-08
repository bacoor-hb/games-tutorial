using UnityEngine;
using System.Collections;

public class Action
{
    public delegate void Event();
    public Event EventStartAction;
    public Event EventEndAction;

    public delegate IEnumerator EventEnumerable();
    public EventEnumerable EventAction;

    private ACTION_TYPE actionType;

    public Action(ACTION_TYPE action)
    {
        this.actionType = action;
    }

    public ACTION_TYPE GetAction()
    {
        return this.actionType;
    }

    public virtual void OnStartAction()
    {
        EventStartAction?.Invoke();
    }

    public virtual IEnumerator OnAction(TurnBaseController.Callback OnStepStatus)
    {
        yield return EventAction?.Invoke();

        OnStepStatus();
    }

    public virtual void OnEndAction()
    {
        EventEndAction?.Invoke();
    }
}