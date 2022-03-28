using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<UserManager> listUserManager = new List<UserManager>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlusAndMins(string _address, long _money)
    {

        if (UserManager.Instance.user.Address == _address)
        {
            if (UserManager.Instance.user.Money + _money >= 0)
            {
                UserManager.Instance.OnPlusAndMins += PlusAndMinsEvent;
            }
            else
            {
                Debug.Log("Error");
            }
        }

    }
    public void OnBuyHouse(PropertyData _propertyData)
    {
        if (_propertyData.cost_house < UserManager.Instance.user.Money)
        {

        }
    }
    private void PlusAndMinsEvent(long _money)
    {
        UserManager.Instance.user.Money += _money;
    }
}
