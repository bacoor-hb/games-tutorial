using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager : Singleton<UserManager>
{
    public delegate void EventHaveProp<T>(T data);
    public EventHaveProp<long> OnPlusAndMins;
    public User user; 
    public int id;
    public Graph graph;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            //TurnBaseController.AddAction(this, TurnBaseConstants.ACTION_END_TURN);
        }
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
        //int countColor = graph.GetType();
        int countColor = 2;
        if (countColor == count) return true;
        return false;
    }  
    public Property GetPropertyUser(Property _property)
    {
        foreach(Property temp in user.GetProperties())
        {
            if(temp.data.id == _property.data.id)
            {
                return temp;
            }
        }
        return null;
    }  
    public bool checkIsMyProperty(Property property)
    {
        return user.GetProperties().Contains(property);
    }

    public void SetUserData(User userData)
    {
        user = userData;
    }
     

}
