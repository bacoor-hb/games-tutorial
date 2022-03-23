using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<UserManager> userManager;

    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlusAndMins(int index, string _address, long _money)
    {
        if (userManager[index].userPlaeyer.Address == _address)
        {

            if (userManager[index].userPlaeyer.Money <=0)
            {
                ErrorPlusAndMinsEvent();
            }
            else
            {
                if (userManager[index].userPlaeyer.Money + _money >= 0)
                {
                    userManager[index].userPlaeyer.Money += _money;
                    userManager[index].OnPlusAndMins += PlusAndMinsEvent; 
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
