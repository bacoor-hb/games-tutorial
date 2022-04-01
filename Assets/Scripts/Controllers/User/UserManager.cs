using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager : MonoBehaviour
{
    public delegate void EventHaveProp<T>(T data);
    public string id;
    public User userData =  new User();
    public WalletManager walletManager;
    
    
    // Start is called before the first frame update
    private void Awake()
    {
        User user = new User("12", "cong", 10, null, null, null);

        setUserManager(user); 
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         
    }

    public void setUserManager(User user)
    {
        userData = user;
      //  Debug.Log(userData.Address);
        walletManager.setWalletUer(id, userData.Address, userData.Money);

    }

    /// <summary>
    /// Check Enough Money User
    /// </summary>
    /// <param name="money"></param>
    /// <returns></returns>
    public bool isCheckEnoughMoney(long money)
    {
        long total = userData.Money + money;
        return total >= 0;
    }
    public void OnChangeMoney(long money)
    {
        walletManager.OnChangeMoney = OnChangeMoneyEnevt;
        walletManager.OnChangeMoneyWeb3 = OnChangeMoneyWeb3Event;
        walletManager.OnChangeMoneyEvent(money);

    }
    public void OnChangeMoneyEnevt(long money)
    {
        
        userData.Money += money;
    }

    public void OnChangeMoneyWeb3Event(long money)
    {

         
    }

    /// <summary>
    /// Change money at Web3 form userMananger
    /// </summary>
    /// <param name="money"></param>
    /// <returns></returns>
    IEnumerable OnChangeMoneyWeb3(long money)
    {
        yield return null;
    }

   
}
