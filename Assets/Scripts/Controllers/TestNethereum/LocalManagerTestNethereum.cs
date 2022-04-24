using Nethereum.Web3;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class LocalManagerTestNethereum : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;

        UpdateBalaceAsync("0xac0E15a038eedfc68ba3C35c73feD5bE4A07afB5");
    }

    // Update is called once per frame
    void Update()
    {

    }
    private async void UpdateBalaceAsync(string account)
    {

        Debug.Log(account);
        Web3 web3 = new Web3("https://bsc-dataseed.binance.org/");
        var balance = await web3.Eth.GetBalance.SendRequestAsync(account);
        var ethBalance = Web3.Convert.FromWei(balance.Value);
        Debug.Log("Balance: " + balance);
        Debug.Log("ethBalance: " + ethBalance);


    }
}
