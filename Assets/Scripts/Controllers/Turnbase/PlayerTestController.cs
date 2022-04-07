using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class PlayerTestController : MonoBehaviour, IPlayer
{
    public int id;

    public void SetPlayerID(int _id)
    {
        id = _id;
    }


    public void StartTurn()
    {
        Material myMaterial = GetComponent<Renderer>().material;
        myMaterial.color = Color.red;
        Debug.Log("StartTurn: id: " + id);
    }

    public void EndTurn()
    {
        Material myMaterial = GetComponent<Renderer>().material;
        myMaterial.color = Color.white;
        Debug.Log("EndTurn: id: " + id);
    }

    public void StartAction()
    {
        Debug.Log("StartAction: id: " + id);
    }

    public void EndAction()
    {
        Debug.Log("EndAction: id: " + id);
    }

    public Action GetAction(ACTION_TYPE actionType)
    {
        Action action = new Action(actionType);
        if (actionType == ACTION_TYPE.PURCHASE)
        {
            action.EventStartAction += OnPurchaseStart;
            action.EventEndAction += OnPurchaseEnd;
            action.EventAction += OnPurchase;
        }
        if (actionType == ACTION_TYPE.AUCTION)
        {
            action.EventStartAction += OnAunctionStart;
            action.EventEndAction += OnAunctionEnd;
            action.EventAction += OnAunction;
        }

        return action;
    }

    #region OnPurchase
    public void OnPurchaseStart()
    {
        Debug.Log("StartAction:OnPurchase | id: " + id);
    }
    public void OnPurchaseEnd()
    {
        Debug.Log("EndAction:OnPurchase | id: " + id);
    }
    public void OnPurchase()
    {
        Debug.Log("Action:OnPurchase | id: " + id);

    }
    #endregion

    #region OnAunction
    public void OnAunctionStart()
    {
        Debug.Log("StartAction:OnAunction | id: " + id);
    }
    public void OnAunctionEnd()
    {
        Debug.Log("EndAction:OnAunction | id: " + id);
    }
    public void OnAunction()
    {
        Debug.Log("Action:OnAunction | id: " + id);
    }
    #endregion
}
