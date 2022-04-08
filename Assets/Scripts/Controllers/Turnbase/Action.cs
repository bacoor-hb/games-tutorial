using UnityEngine;
using System.Collections;

public class Action
{
    public delegate void Event();
    public Event EventStartAction;
    public Event EventEndAction;

    public delegate IEnumerator EventEnumerable<T>(T data);
    public EventEnumerable<TurnBaseController.Callback> EventAction;

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

    public virtual void OnAction(TurnBaseController.Callback OnStepStatus)
    {
        EventAction?.Invoke(OnStepStatus);
    }

    public virtual void OnEndAction()
    {
        EventEndAction?.Invoke();
    }
}
