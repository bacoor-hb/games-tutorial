 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager : MonoBehaviour
{
    public delegate void EventHaveProp<T>(T data);
    public string id;
    public User userData = new User();
    public WalletManager walletManager;


    // Start is called before the first frame update
    private void Awake()
    {

    }
    void Start()
    {
        walletManager.setWalletUer(id, Random.Range(1, 500000).ToString(), 5000); 
        userData.Money = walletManager._walletData.Money; 

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
        return userData.Money >= money;
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
        //int count = 0;
        //if (userData.GetProperties().Count < 2) return false;

        //foreach (Property item in userData.GetProperties())
        //{
        //    if (item.data.typeId == _property.data.typeId)
        //    {
        //        count++;
        //    }
        //}
        ////int countColor = graph.GetTotalPropertiesByType(_property.data.typeId)
        //int countColor = 2;
        //if (countColor == count) return true;
        //return false;
        return true;
    }
    public bool isCheckBuildHotel(Property _property)
    {
        if (_property.level == 4) return true;
        return false;
    }
    public bool isCheckSellPlane(Property property)
    {
        if (property.level == 0) return true;
        return false;
    }
    public int PriceSellForBank(int price)
    {
        return price / 2;
    }
    public void SellPlaneWhenAuction(Property property, long money)
    {
        userData.RemoveProperty(property);
        OnChangeMoney(money);
    }
    public void SellForBank(Property property)
    {
        int price = PriceSellForBank(property.GetPriceSellProperty());
        if (property.level > 0)
        {
            property.level--;
        }
        else
        {
            userData.RemoveProperty(property);
        }
        OnChangeMoney(PriceSellForBank(price));
    }
    public void SellForBank(Property property, int levelWantToSell)
    {
        int price = 0;
        if (property.level >= levelWantToSell )
        {

            for (int i = levelWantToSell; i >= 0; i--)
            {
                price += property.GetPriceSellProperty();
                property.level--;
            }
            if (property.level == 0)
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
        if (isCheckEnoughMoney(property.data.cost) && isCheckBuyPlane(property) && !property.IsCheckPropertyOwned())
        {
            OnChangeMoney(-property.data.cost);
            property.isBought = true;
            property.level = 0;
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
            int price = property.GetPriceBuyProperty();
            if (isCheckEnoughMoney(price))
            {
                OnChangeMoney(-price);
                property.level++;
                Debug.Log("level : " + property.level);

            }
        }
        else
        {
            // thong bao khong mua dc nha
        }


    }
    public void OnBuilding(Property property, int levelWantToBuy)
    {
        if (isCheckBuildHouse(property) && levelWantToBuy > property.level)
        {
            int totalPrice = 0;
            int loop = levelWantToBuy - property.level;
            for (int i = 0; i < loop; i++)
            {
                int price = property.GetPriceBuyProperty();
                if (isCheckEnoughMoney(price))
                {
                    totalPrice += price;
                    property.level++;
                }
            }
            OnChangeMoney(-totalPrice);
        }
        else
        {
            // thong bao khong mua dc nha
        }
    }

}
