using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIManagerTestAttack : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Button _attackButton;
    [SerializeField]
    private TextMeshProUGUI _nameUser;

    public delegate void AttackButtonClicked();
    public AttackButtonClicked OnAttackButtonClicked;
    void Start()
    {



        _attackButton.onClick.AddListener(() =>
        {
            OnAttackButtonClicked?.Invoke();
        });
    }

    // Update is called once per frame
    public void UpdateUI(DataUserAttack dataUserAttack)
    {
        Debug.Log("UpdateUI");
        Debug.Log(dataUserAttack.topic.username);
        _nameUser.text = dataUserAttack.topic.username;


    }

}
