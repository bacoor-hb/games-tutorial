using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalletManager : MonoBehaviour
{
    public delegate void EventHaveProps<T>(T data);
    public EventHaveProps<long> OnChangeMoney;
    public EventHaveProps<long> OnChangeMoneyWeb3;
    /// <summary>
    /// cuntructor Wallet for user
    /// </summary>
    public WalletData _walletData = new WalletData(); 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnChangeMoneyEvent(long money)
    {
        if (OnChangeMoney!=null)
        {
            _walletData.Money += money;
            OnChangeMoney?.Invoke(money);
        }
    }
    /// <summary>
    /// Change money at wweb3
    /// </summary>
    /// <param name="money"></param>
    void OnChangeMoneyWeb3Event(long money)
    {
        if (OnChangeMoney != null)
        {
            OnChangeMoney?.Invoke(money);
        }
    }
    public void setWalletUer(string iduser, string addressWallet, long money)
    {
        _walletData.IDUser = iduser;
        _walletData.AddressWallet = addressWallet;
        _walletData.Money = money;
    }
}
