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
    private UserManager userManager = new UserManager();
    private void Awake()
    {
        User user = new User("12", "cong", 10, null, null, null);
        moneyUserStart.text = "10";
        userManager.setUserManager(user);
        Debug.Log(userManager.walletManager._walletData.Money);
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
        if (userManager.isCheckEnoughMoney(moneyChange))
        {
            moneyUserStart.text = userManager.walletManager._walletData.Money.ToString();
            result.text = "true";
        }
        else
        {
            result.text = "So tien tru qua nhieu";
        }
    }
}
