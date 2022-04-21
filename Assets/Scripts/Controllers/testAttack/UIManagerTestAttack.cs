using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerTestAttack : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Button _attackButton;

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
    void Update()
    {

    }
}
