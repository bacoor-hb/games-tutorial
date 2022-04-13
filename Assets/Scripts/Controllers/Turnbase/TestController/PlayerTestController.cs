﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerTestController : IPlayer
{
    [SerializeField]
    private AuctionActionTest AuctionAction;
    [SerializeField]
    private PurchaseActionTest PurchaseAction;
    [SerializeField]
    private Action EndTurnAction;

    /// <summary>
    /// Initialize the Player and all of his Actions
    /// </summary>
    /// <param name="_id"></param>
    public void InitPlayer(int _id)
    {
        id = _id;
        InitPlayerAction();
    }

    /// <summary>
    /// Initialize the player Actions
    /// </summary>
    private void InitPlayerAction()
    {
        PurchaseAction.InitAction(id);
        PurchaseAction.StartAction += OnPurchaseStart;
        PurchaseAction.EndAction += OnPurchaseEnd;

        AuctionAction.InitAction(id);
        AuctionAction.StartAction += OnAuctionStart;
        AuctionAction.EndAction += OnAuctionEnd;
        AuctionAction.EventAction += OnAuction;

        EndTurnAction.InitAction(id);
    }

    #region Turn Management
    public Action GetAction(ACTION_TYPE actionType)
    {
        return actionType switch
        {
            ACTION_TYPE.RELEASE_CARD => null,
            ACTION_TYPE.ROLL_DICE => null,
            ACTION_TYPE.RUN_THE_CELL => null,
            ACTION_TYPE.PURCHASE => PurchaseAction,
            ACTION_TYPE.BUILDING => null,
            ACTION_TYPE.AUCTION => AuctionAction,
            ACTION_TYPE.END_TURN => EndTurnAction,
            _ => null,
        };
    }

    public override void StartTurn()
    {
        Material myMaterial = GetComponent<Renderer>().material;
        myMaterial.color = Color.red;
        Debug.Log("StartTurn: id: " + id);
    }

    public override void EndTurn()
    {
        Material myMaterial = GetComponent<Renderer>().material;
        myMaterial.color = Color.white;
        Debug.Log("EndTurn: id: " + id);
    }
    #endregion      

    #region OnPurchase
    public void OnPurchaseStart()
    {
        Debug.Log("StartAction on PLayer: OnPurchase | id: " + id);        
    }
    public void OnPurchaseEnd()
    {
        Debug.Log("EndAction:OnPurchase on PLayer | id: " + id);
    }

    
    #endregion

    #region OnAunction
    public void OnAuctionStart()
    {
        Debug.Log("StartAction:OnAunction | id: " + id);
    }
    public void OnAuction()
    {
        Debug.Log("Action:OnAunction | id: " + id);
    }
    public void OnAuctionEnd()
    {
        Debug.Log("EndAction:OnAunction | id: " + id);
    }

    
    #endregion
}
