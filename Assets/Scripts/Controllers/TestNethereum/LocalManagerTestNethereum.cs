using Nethereum.Web3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalManagerTestNethereum : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UpdateBalaceAsync("0x4281eCF07378Ee595C564a59048801330f3084eE");
    }

    // Update is called once per frame
    void Update()
    {

    }
    private async void UpdateBalaceAsync(string account)
    {
        Debug.Log(account);
        Web3 web3 = new Web3("https://kovan.infura.io/v3/0e0703a9c74742678b09d56e28e339a7");
        var balance = await web3.Eth.GetBalance.SendRequestAsync(account);
        var ethBalance = Web3.Convert.FromWei(balance.Value);
        Debug.Log("Balance: " + balance);
        Debug.Log("ethBalance: " + ethBalance);


    }
}
