using Nethereum.JsonRpc.UnityClient;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Util;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;


public class ManagerNetehreum : Singleton<ManagerNetehreum>
{
    // Start is called before the first frame update
    [DllImport("__Internal")]
    public static extern void GetBalance(string address, string gameObjectName, string callback, string fallback);
    [DllImport("__Internal")]
    public static extern string EnableEthereum(string gameObjectName, string callback, string fallback);
 
    public string _myAddress;
    public string _myBalance;

    [SerializeField]
    private Button _btnConnect;
    [SerializeField]
    private Button _btnGetBalance;
    void Start()
    {
        _btnConnect.onClick.AddListener(() =>
        {
            EnableEthereum(gameObject.name, nameof(EthereumEnabled), nameof(DisplayError));

        });
        _btnGetBalance.onClick.AddListener(() =>
        {
            GetBalance(_myAddress, gameObject.name, nameof(EthereumEnabled), nameof(DisplayError));
        });


    }
    public void EthereumEnabled(string addressSelected)
    {
        Debug.Log("addressSelected "+ addressSelected);
        _myAddress=addressSelected;

    } 
    public void GetBalanceComplete(string BalanceSelected)
    {
        Debug.Log("BalanceSelected " + BalanceSelected);
        _myBalance=BalanceSelected;
    }

    public void DisplayError(string errorMessage)
    {
        Debug.Log("error " + errorMessage);

    }


    // Update is called once per frame
    //IEnumerator loadBalance()
    //{


    //    var balanceRequest = new EthGetBalanceUnityRequest("https://kovan.infura.io/v3/e4c6b2743e544bdb910ef53155687b0f");
    //    yield return balanceRequest.SendRequest("0xC0b4ec83028307053Fbe8d00ba4372384fe4b52B", BlockParameter.CreateLatest());

    //    var BalanceAddressTo = UnitConversion.Convert.FromWei(balanceRequest.Result.Value);


    //    Debug.Log("Balance of account:" + BalanceAddressTo);
    //}

}
