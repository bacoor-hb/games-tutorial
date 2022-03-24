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
        {   
            OnPlusAndMins?.Invoke(_money);
        }
    }
    public void OnBuyHouse(Property _property)
    {
        
    }
    public void SetUserData(User userData)
    {
        user = userData;
    }

}
