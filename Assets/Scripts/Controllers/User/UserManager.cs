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

        foreach (Property item in userData.GetProperties())
        {
            if (item.data.typeId == _property.data.typeId)
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
       // if (_property.level == 4) return true;
        return false;
    }
    public bool isCheckSellPlane(Property property)
    {
        //  if (property.level == 0) return true;
        return false;
    }
    public int PriceSellForBank(int price){
        return price / 2;
    }
    public void SellPlaneWhenAuction(Property property,long money){
        userData.RemoveProperty(property);
        OnChangeMoney(money);
    }
     public void SellForBank(Property property)
    { 
        int price = PriceSellForBank(property.GetPriceBuyProperty());
        if (property.level > 0)
        {
            property.level--;
        }
        else
        {
            userData.RemoveProperty(property);
        }
        OnChangeMoney(price);
    }
    public void SellForBank(Property property, int levelWantToSell)
    {
        int price = 0;
        if (property.level >= levelWantToSell && levelWantToSell>0)
        {

            for (int i = levelWantToSell; i >= 0; i--)
            {
                price += property.GetPriceBuyProperty();
                property.level--;
            }
            if (property.level == levelWantToSell)
            {
                userData.RemoveProperty(property);
            }
            price = PriceSellForBank(price);
            OnChangeMoney(price);
        }
        else
        {
            Debug.Log("Not enough level");
        }

    }


    public bool IsCheckMyProperty(Property property)
    {
        return userData.GetProperties().Contains(property);
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
            int totalPrice=0;
            int loop=levelWantToBuy-property.level;
            for (int i = 0; i < loop; i++)
            {
                int price = property.GetPriceBuyProperty();
                if (isCheckEnoughMoney(price))
                {
                    totalPrice += price;
                    property.level++;
                }
            }
            OnChangeMoney(-totalPrices);
        }
        else
        {
            // thong bao khong mua dc nha
        }
        } 
    
}
