using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuctionActionTest : Action
{
    public override void InitAction(int _userId)
    {
        base.InitAction(_userId);
    }

    public override void ClearEvent()
    {
        base.ClearEvent();
    }

    public override void OnAction()
    {
        base.OnAction();
    }

    public override void OnEndAction()
    {
        base.OnEndAction();
    }

    public override void OnStartAction()
    {
        base.OnStartAction();
    }

    int auctionActionFlag = 0;
    public void OnAuctionAction()
    {
        auctionActionFlag = 0;
        StartCoroutine(OnAunction1());
        StartCoroutine(OnAunction2());
    }

    public IEnumerator OnAunction1()
    {
        Debug.Log("Start:10s: " + userId);
        Debug.Log("Action:OnAunction | id: " + userId);
        yield return new WaitForSeconds(10);
        Debug.Log("10s: " + userId);
        auctionActionFlag++;
    }
    public IEnumerator OnAunction2()
    {
        Debug.Log("Start:15s: " + userId);
        Debug.Log("Action:OnAunction | id: " + userId);
        yield return new WaitForSeconds(15);
        Debug.Log("15s: " + userId);

        while (auctionActionFlag <= 0)
        {
            yield return new WaitForEndOfFrame();
        }
        EndAction?.Invoke();
    }
}
