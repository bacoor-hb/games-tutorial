using Nethereum.JsonRpc.UnityClient;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerNetehreum : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(loadBalance());
    }

    // Update is called once per frame
    IEnumerator loadBalance()
    {


        var balanceRequest = new EthGetBalanceUnityRequest("https://kovan.infura.io/v3/e4c6b2743e544bdb910ef53155687b0f");
        yield return balanceRequest.SendRequest("0xC0b4ec83028307053Fbe8d00ba4372384fe4b52B", BlockParameter.CreateLatest());

        var BalanceAddressTo = UnitConversion.Convert.FromWei(balanceRequest.Result.Value);


        Debug.Log("Balance of account:" + BalanceAddressTo);
    }
}
