using UnityEngine;
using System.Collections;

public class Action : MonoBehaviour
{
    public delegate void Event();
    public Event StartAction;
    public Event EndAction;
    public Event EventAction;

    [SerializeField]
    protected ACTION_TYPE actionType;
    protected int userId;

    public virtual void InitAction(int _userId)
    {
        userId = _userId;
        ClearEvent();
    }

    public ACTION_TYPE GetAction()
    {
        return actionType;
    }

    /// <summary>
    /// Clear all event of this action
    /// </summary>
    public virtual void ClearEvent()
    {
        StartAction = null;
        EndAction = null;
        EventAction = null;
    }

    public virtual void OnStartAction()
    {
        StartAction?.Invoke();
    }

    public virtual void OnAction()
    {
        EventAction?.Invoke();
    }

    public virtual void OnEndAction()
    {
        EndAction?.Invoke();
    }
}
