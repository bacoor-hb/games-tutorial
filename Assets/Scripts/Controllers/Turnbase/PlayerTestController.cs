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

    public void OnPurchaseStart()
    {
        Debug.Log("ActionStart:OnPurchase | id: " + id);
    }
    public void OnPurchaseEnd()
    {
        Debug.Log("ActionEnd:OnPurchase | id: " + id);
    }
    public void OnAunctionStart()
    {
        Debug.Log("ActionStart:OnAunction | id: " + id);
    }
    public void OnAunctionEnd()
    {
        Debug.Log("ActionEnd:OnAunction | id: " + id);
    }

    void IPlayer.ActionStart()
    {
        throw new System.NotImplementedException();
    }

    void IPlayer.ActionEnd()
    {
        throw new System.NotImplementedException();
    }
}
