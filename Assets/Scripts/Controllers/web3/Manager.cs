using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Manager : MonoBehaviour
{
    // Start is called before the first frame update
    async Task StartAsync()
    {
       string chain = "ethereum";
string network = "mainnet"; // mainnet ropsten kovan rinkeby goerli
string account = "0xB28Ac17023Bf5fB99E0d38BE0247b6613C92dCE6";

string balance = await EVM.BalanceOf(chain, network, account);
        Debug.Log(balance);
    }
    private void Start()
    {
        _ = StartAsync();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
