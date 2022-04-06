using UnityEngine;
using System.Collections;

public class Action
{
    public delegate void Event();
    public Event EventStartAction;
    public Event EventAction;
    public Event EventEndAction;

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

    public virtual void OnAction()
    {
        EventAction?.Invoke();
    }

    public virtual void OnEndAction()
    {
        EventEndAction?.Invoke();
    }
}
