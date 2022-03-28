using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager : MonoBehaviour
{
    public delegate void EventHaveProp<T>(T data);
    public string id;
    public User userData;
    public WalletManager walletManager;
    
    
    // Start is called before the first frame update
    private void Awake()
    {
        walletManager = new WalletManager(); 
        walletManager.setWalletUer(id, userData.Address, userData.Money);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         
    }
    public bool isCheckEnoughtMoney(long money)
    {
        long total = userData.Money += money;
        if (total >= 0)
        {
            this.OnChangeMoney(money);
            return true;
        } 
        return false;
    }
    public void OnChangeMoney(long money)
    {
        walletManager.OnChangeMoney += OnCangeMoneyEnevt;
        walletManager.OnChangeMoneyWeb3 += OnCangeMoneyWeb3Event;

    }
    public void OnCangeMoneyEnevt(long money)
    {
        userData.Money += money;

    }

    public void OnCangeMoneyWeb3Event(long money)
    {

         
    }
    IEnumerable OnChangeMoneyWeb3(long money)
    {
        yield return null;
    } 
    //public bool isCheckAmountPlaneHasBuildHouse(Property _property)
    //{
    //    int count = 0;
    //    if (user.GetProperties().Count < 2) return false;

    //    for (int i = 0; i < user.GetProperties().Count; i++)
    //    {
    //        Property property = user.GetProperties()[i];
    //        if (_property.data.typeId == property.data.typeId)
    //        {
    //            count++;
    //        }
    //    }


    //    // get count colors in board
    //    //int countColor = graph.GetType();
    //    int countColor = 2;
    //    if (countColor == count) return true;
    //    return false;
    //}  
    //public Property GetPropertyUser(Property _property)
    //{
    //    foreach(Property temp in user.GetProperties())
    //    {
    //        if(temp.data.id == _property.data.id)
    //        {
    //            return temp;
    //        }
    //    }
    //    return null;
    //}  
    //public bool checkIsMyProperty(Property property)
    //{
    //    return user.GetProperties().Contains(property);
    //}




}
