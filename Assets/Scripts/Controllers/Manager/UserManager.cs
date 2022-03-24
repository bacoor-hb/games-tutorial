using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager : MonoBehaviour
{
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

    public void SetUserData(User userData)
    {
        user = userData;
    }
}
