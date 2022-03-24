using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager : Singleton<UserManager>
{
    // Start is called before the first frame update 
    public delegate void OnEventCalled<T>(T data);
    public OnEventCalled<long> OnPlusAndMins;
    public User userPlayer;
    [HideInInspector] 
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    } 
    public void OnPlusAndMinsEvent()
    {
        OnPlusAndMins?.Invoke(userPlayer.Money);
        
    }  
    IEnumerable PlusAndMinsInWeb3(long money)
    {
        
        yield return null;
    }
}
