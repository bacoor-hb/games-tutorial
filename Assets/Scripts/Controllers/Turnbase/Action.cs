using UnityEngine;
using System.Collections;

public class Action
{
    public delegate void Event();
    public Event EventStartAction;
    public Event EventAction;
    public Event EventEndAction;

    private ACTION action;

    public Action(ACTION action)
    {
        this.action = action;
    }

    public ACTION GetAction()
    {
        return this.action;
    }

    public void OnStartAction()
    {
        if (EventStartAction != null)
        {
            EventStartAction();
        }
    }

    public void OnAction()
    {
        if (EventAction != null)
        {
            EventAction();
        }
    }

    public void OnEndAction()
    {
        if (EventEndAction != null)
        {
            EventEndAction();
        }
    }
}
