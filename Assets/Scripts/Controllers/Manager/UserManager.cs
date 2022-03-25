using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager : Singleton<UserManager>
{
    public delegate void EventHaveProp<T>(T data);
    public EventHaveProp<long> OnPlusAndMins;
    public User user;

    // Start is called before the first frame update
    void Start()
    {
        SetUserData(new User("asd", "asd", 1000, null, null, null));
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnPlusAndMinsEvent(long _money)
    {
        if(OnPlusAndMins!=null)
        {
            OnPlusAndMins?.Invoke(_money);
        }
    }
    public bool isCheckAmountPlaneHasBuildHouse(Property _property)
    {

        int count = 0;
        if (user.GetProperties().Count < 2) return false;

        for (int i = 0; i < user.GetProperties().Count; i++)
        {
            Property property = user.GetProperties()[i];
            if (_property.data.typeId == property.data.typeId)
            {
                count++;
            }
        }
        // get count colors in board
        int countColor = Graph.Instance.GetTotalPropertiesByType(_property.data.typeId);
        if (countColor == count) return true;
        return false;
    }
    public void SetUserData(User userData)
    {
        user = userData;
    }

}
