using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ViewManagerTest : MonoBehaviour
{
    // Start is called before the first frame update
    public Button btnChange;
    public InputField inputField;
    public Text result;
    public Text moneyUserStart;
    public UserManager userManager;
    private void Awake()
    {
        moneyUserStart.text = userManager.userData.Money.ToString();

    }
    void Start()
    {
       
      
        btnChange.onClick.AddListener(OnPressChangeMoney);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPressChangeMoney()
    {
        string money = inputField.text.ToString(); 
        long moneyChange = long.Parse(money);
        if(moneyChange >= 0)
        {
            userManager.OnChangeMoney(moneyChange);
            moneyUserStart.text = userManager.walletManager._walletData.Money.ToString();
            result.text = "true";
        }
        else
        {
            if (userManager.IsCheckEnoughMoney(moneyChange))
            {
                userManager.OnChangeMoney(moneyChange);
                moneyUserStart.text = userManager.walletManager._walletData.Money.ToString();
                result.text = "true";
            }
            else
            {
                result.text = "So tien tru qua nhieu";
            }
        }
       
    }
}
