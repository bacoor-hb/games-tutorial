using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager : MonoBehaviour
{
    public delegate void EventHaveProp<T>(T data);
    public string id;
    public User userData;
    public WalletManager walletManager = new WalletManager(); 
    
    
    // Start is called before the first frame update
    private void Awake()
    { 
        walletManager.setWalletUer(id, userData.Address, userData.Money);
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
        walletManager.setWalletUer(id, user.Address, user.Money);

    }
    public bool isCheckEnoughMoney(long money)
    {

        long total = userData.Money + money;
        Debug.Log("total : " + total);
        if (total >= 0)
        {
            return true;
        }
        return false;
    }
    public void OnChangeMoney(long money)
    {
        walletManager.OnChangeMoney = OnChangeMoneyEvent;
        walletManager.OnChangeMoneyWeb3 = OnChangeMoneyWeb3Event;
        walletManager.OnChangeMoneyEvent(money);

    }
    public void OnChangeMoneyEvent(long money)
    {
        
        userData.Money += money;
    }
    public void OnChangeMoneyWeb3Event(long money)
    {

        StartCoroutine(OnChangeMoneyWeb3(money));
    }
    IEnumerator OnChangeMoneyWeb3(long money)
    {
        yield return null;
    }
    public bool isCheckBuildHouse(Property _property)
    {
        int count = 0;
        if (userData.GetProperties().Count < 2) return false;

        for (int i = 0; i < userData.GetProperties().Count; i++)
        {
            Property property = userData.GetProperties()[i];
            if (_property.data.typeId == property.data.typeId)
            {
                count++;
            }
        }
        //int countColor = graph.GetTotalPropertiesByType(_property.data.typeId)
        int countColor = 2;
        if (countColor == count) return true;
        return false;
    }
    public bool isCheckBuildHotel(Property _property)
    {
       // if (_property.status >= 4) return true;
        return false;
    }
    public bool isCheckSellPlane(Property property)
    {
        //  if (property.level == 0) return true;
        return false;
    }
    public bool IsCheckMyProperty(Property property)
    {
        return userData.GetProperties().Contains(property);
    }
    public Property GetPropertyUser(Property _property)
    {
        foreach (Property temp in userData.GetProperties())
        {
            if (temp.data.id == _property.data.id)
            {
                return temp;
            }
        }
        return null;
    }
    public bool isCheckBuyPlane(Property plane)
    {
        return plane.level == 0;
    }
    public void OnBuyNewProperty(Property property)
    {
        if(isCheckEnoughMoney(property.data.cost) && isCheckBuyPlane(property) && !property.IsCheckPropertyOwned())
        {
            OnChangeMoney(-property.data.cost);
            property.isBought = true;
            userData.GetProperties().Add(property);
        }
        else
        {
            // thong nao khong mua duoc dat
        }
    }
    public void OnBuilding(Property property)
    {
        if (isCheckBuildHouse(property))
        {
            int price=property.GetPriceBuyProperty();
            if (isCheckEnoughMoney(price))
            {
            OnChangeMoney(-price);
            property.level++;     
            }
        }
        else
        {
            // thong bao khong mua dc nha
        }
        

    }
    public void OnBuilding(Property property, int levelWantToBuy)
    {
        if (isCheckBuildHouse(property)&& levelWantToBuy>property.level )
        {
            int loop=levelWantToBuy-property.level;
            for (int i = 0; i < loop; i++)
            {
                int price = property.GetPriceBuyProperty();
                if (isCheckEnoughMoney(price))
                {
                    OnChangeMoney(-price);
                    property.level++;
                }
            }
        }
        else
        {
            // thong bao khong mua dc nha
        }
        } 
}
