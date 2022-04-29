using Nethereum.Hex.HexTypes;
using Nethereum.JsonRpc.UnityClient;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Util;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;


public class ManagerNetehreum : MonoBehaviour
{
    // Start is called before the first frame update
    [DllImport("__Internal")]
    public static extern void GetBalance(string address,string _currentChainId, string gameObjectName, string callback, string fallback);
    [DllImport("__Internal")]
    public static extern string EnableEthereum(string gameObjectName, string callback, string fallback);
    [DllImport("__Internal")]
    public static extern void GetChainId( string gameObjectName, string callback, string fallback);
    private string _myAddress;
    private string _myBalance;
    private BigInteger _currentChainId;
    [SerializeField]
    private UIManagerNethereum _UIManagerNethereum;

    void Start()
    {
      
        StartCoroutine(loadBalance("eth testnet kovan", "0x81b7E08F65Bdf5648606c89998A9CC8164397647", "https://kovan.infura.io/v3/0e0703a9c74742678b09d56e28e339a7"));
        StartCoroutine(loadBalance("Bsc testnet", "0x6d6247501b822FD4Eaa76FCB64bAEa360279497f", "https://data-seed-prebsc-1-s1.binance.org:8545/"));
        StartCoroutine(loadBalance("Matic testnet", "0x79607c1C96e8BD305c1716E4Dc6e8c8A433AfbAb", "https://matic-mumbai.chainstacklabs.com"));
        StartCoroutine(loadBalance("Tomo testnet", "0xc97e67e3987e3b67a5d72d76422cff3bb260cf85", "https://rpc.testnet.tomochain.com"));
        _UIManagerNethereum.OnEventBtnConnectClicked += (() =>
        {
            EnableEthereum(gameObject.name, nameof(EthereumEnabled), nameof(DisplayError));
        });


        _UIManagerNethereum.OnEventBtnGetBalanceClicked += (() =>
        {
            GetChainId(gameObject.name, nameof(GetChainComplete), nameof(DisplayError));

        });




    }
    public void EthereumEnabled(string addressSelected)
    {
        Debug.Log("addressSelected " + addressSelected);
        _myAddress = addressSelected;
        _UIManagerNethereum.UpdateAddress(addressSelected);

    }
    public void GetChainComplete(string chainId)
    {
        Debug.Log("chainId " + chainId);

        _currentChainId = new HexBigInteger(chainId).Value;
        Debug.Log("_currentChainId " + _currentChainId);
        GetBalance(_myAddress, _currentChainId.ToString(), gameObject.name, nameof(GetBalanceComplete), nameof(DisplayError));
        

    }
    public void GetBalanceComplete(string BalanceSelected)
    {
        Debug.Log("BalanceSelected " + BalanceSelected);
        BigInteger bigInteger = BigInteger.Parse(BalanceSelected);
        Debug.Log("bigInteger " + bigInteger);
        _myBalance = UnitConversion.Convert.FromWei(bigInteger).ToString();
        Debug.Log("_myBalance " + _myBalance);
        _UIManagerNethereum.UpdateBalance(_myBalance);

    }

    public void DisplayError(string errorMessage)
    {
        Debug.Log("error " + errorMessage);

    }


    IEnumerator loadBalance(string nameChain, string address,string provider)
    {
        //var balanceRequest = new EthGetBalanceUnityRequest("https://kovan.infura.io/v3/e4c6b2743e544bdb910ef53155687b0f");
        //yield return balanceRequest.SendRequest("0xC0b4ec83028307053Fbe8d00ba4372384fe4b52B", BlockParameter.CreateLatest());

        //var BalanceAddressTo = UnitConversion.Convert.FromWei(balanceRequest.Result.Value);"https://data-seed-prebsc-1-s1.binance.org:8545/"


        //Debug.Log("Balance of account:" + BalanceAddressTo);
       
        var balanceRequestBsc = new EthGetBalanceUnityRequest(provider);
        yield return balanceRequestBsc.SendRequest(address, BlockParameter.CreateLatest());
        var BalanceAddressBsc = UnitConversion.Convert.FromWei(balanceRequestBsc.Result.Value);
        Debug.Log($"Balance of account {nameChain}:" + address);
        Debug.Log("Balance :" + BalanceAddressBsc);
        
      
    }
    

}
