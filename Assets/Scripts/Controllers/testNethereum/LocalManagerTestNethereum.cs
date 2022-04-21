using Nethereum.Web3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalManagerTestNethereum : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(loadBalance());

        
    }

    // Update is called once per frame
    IEnumerator loadBalance()
    {
        var publicKey = "0xb2f92112cff116e589900e4622b9d1265284665d";
        var web3 = new Nethereum.Web3.Web3("https://kovan.infura.io/v3/0e0703a9c74742678b09d56e28e339a7");
        //var txCount = await web3.Eth.Transactions.GetTransactionCount.SendRequestAsync(publicKey);
        var balance = web3.Eth.GetBalance.SendRequestAsync(publicKey);
        var price = balance.Result.ToString();
        var etherAmount = Web3.Convert.FromWei(long.Parse(price));

        Debug.Log(price);
        Debug.Log("Get txCount " + etherAmount);
        yield return null;
    }

}
