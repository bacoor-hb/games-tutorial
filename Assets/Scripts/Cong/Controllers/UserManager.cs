using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager : MonoBehaviour
{
    // Start is called before the first frame update 
    public delegate void OnEventCalled<T>(T data);
    public OnEventCalled<long> OnPlusAndMins;
    public User userPlaeyer;
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
        OnPlusAndMins?.Invoke(userPlaeyer.Money);
    } 
}
