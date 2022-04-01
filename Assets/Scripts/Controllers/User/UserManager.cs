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
        walletManager.setWalletUer(id, userData.Address, userData.Money); 

    }

    /// <summary>
    /// Check Enough Money User
    /// </summary>
    /// <param name="money"></param>
    /// <returns></returns>
    public bool IsCheckEnoughMoney(long money)
    { 
        return userData.Money - (long)Mathf.Abs(money) >= 0;
    } 

    /// <summary>
    /// Check user can build house
    /// </summary>
    /// <param name="_property"></param>
    /// <returns></returns>
    public bool IsCheckBuildHouse(Property _property)
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
        //return true;
    }

    /// <summary>
    /// Check user can build hotel
    /// </summary>
    /// <param name="_property"></param>
    /// <returns></returns>
    public bool IsCheckBuildHotel(Property _property)
    {
        return _property.level == 4;
    }

    /// <summary>
    /// Chweck Property of user playing
    /// </summary>
    /// <param name="_property"></param>
    /// <returns></returns>
    public bool IsCheckMyProperty(Property _property)
    {
        return userData.GetProperties().Contains(_property);
    }

    /// <summary>
    /// Check user can sell plane
    /// </summary>
    /// <param name="_property"></param>
    /// <returns></returns>
    public bool IsCheckSellPlane(Property _property)
    {
        return _property.level == 0;
    }

    /// <summary>
    ///  Change money user
    /// </summary>
    /// <param name="money"></param>
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

    /// <summary>
    /// Price When Sell Property For Bank
    /// </summary>
    /// <param name="price"></param>
    /// <returns></returns>
    public long PriceSellForBank(long price)
    {
        return price / 2;
    }

    /// <summary>
    /// Sell Prpperty for Bank -  sell one property
    /// </summary>
    /// <param name="_property"></param>
    public void SellForBank(Property _property)
    {
        // get price and change real price 
        long price = PriceSellForBank(_property.GetPriceSellProperty());
        OnChangeMoney(-price);
        _property.level--;
        if (_property.level == -1)
        {
            userData.RemoveProperty(_property);
        }
    }

    /// <summary>
    /// Sell Prpperty for Bank - sell more property
    /// </summary>
    /// <param name="_property"></param>
    /// <param name="levelWantSell"></param>
    public void SellForBank(Property _property, int levelWantSell)
    {
        long price = 0;
        if (_property.level >= levelWantSell - 1)
        {

            for (int i = levelWantSell - 1; i >= 0; i--)
            {
                price += _property.GetPriceSellProperty();
                _property.level--;
            }
            if (_property.level == -1)
            {
                userData.RemoveProperty(_property);
            }
            price = PriceSellForBank(price);
            OnChangeMoney(price);
        }
        else
        {
            Debug.Log("Not enough level");
        }
    }

    /// <summary>
    /// Buy Property new 
    /// </summary>
    /// <param name="property"></param>
    public void OnBuyNewProperty(Property property)
    {
        if (IsCheckEnoughMoney(property.data.cost) && !property.IsCheckPropertyOwned())
        {
            OnChangeMoney(-property.data.cost);
            property.isBought = true;
            property.level = 0;
            userData.AddProperty(property);
        }
        else
        {
            // thong nao khong mua duoc dat
        }
    }

    /// <summary>
    /// Biuld house or hotel - one level
    /// </summary>
    /// <param name="property"></param>
    public void OnBuilding(Property property)
    {
        if (IsCheckBuildHouse(property))
        {
            int price = property.GetPriceBuyProperty();
            if (IsCheckEnoughMoney(price))
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

    /// <summary>
    /// Biuld house or hotel - more level
    /// </summary>
    /// <param name="property"></param>
    /// <param name="levelWantToBuy"></param>
    public void OnBuilding(Property property, int levelWantToBuy)
    {
        if (IsCheckBuildHouse(property) && levelWantToBuy > property.level)
        {
            int totalPrice = 0;
            int loop = levelWantToBuy - property.level;
            for (int i = 0; i < loop; i++)
            {
                int price = property.GetPriceBuyProperty();
                if (IsCheckEnoughMoney(price))
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
