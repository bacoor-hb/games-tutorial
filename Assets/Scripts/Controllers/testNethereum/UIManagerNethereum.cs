using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UIManagerNethereum : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private Button _btnConnect;
    [SerializeField]
    private Button _btnGetBalance;
    [SerializeField]

    private TextMeshProUGUI _txtAddress;
    [SerializeField]

    private TextMeshProUGUI _txtBalance;
    public delegate void OnEventClicked();
    public event OnEventClicked OnEventBtnConnectClicked;
    public event OnEventClicked OnEventBtnGetBalanceClicked;

    void Start()
    {
        _btnConnect.onClick.AddListener(() =>
        {
            OnEventBtnConnectClicked?.Invoke();

        });
        _btnGetBalance.onClick.AddListener(() =>
        {
            OnEventBtnGetBalanceClicked?.Invoke();
        });


    }
    public void UpdateAddress(string address)
    {
        _txtAddress.text = address;
    }
    public void UpdateBalance(string Balance)
    {
        _txtBalance.text = Balance;
    }


}
