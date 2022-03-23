using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    public UserManager userManager;

    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlusAndMins(string _address, long _money)
    {
        if (userManager.userPlaeyer.Address == _address)
        {

            if (userManager.userPlaeyer.Money <=0)
            {
                ErrorPlusAndMinsEvent();
            }
            else
            {
                if (userManager.userPlaeyer.Money + _money >= 0)
                {
                    userManager.userPlaeyer.Money += _money;
                    userManager.OnPlusAndMins += PlusAndMinsEvent; 
                }

                else
                {
                    ErrorPlusAndMinsEvent();
                }
            }
        }
        else
        {
            ErrorPlusAndMinsEvent();
        }

    }
    private void ErrorPlusAndMinsEvent()
    {

    }
    private  void PlusAndMinsEvent(long _money)
    {
        StartCoroutine( PlusAndMinsInWeb3(_money));
    }
    IEnumerator PlusAndMinsInWeb3(long _money)
    {
        yield return null;
    }



}
