using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerBoard : MonoBehaviour
{
    [SerializeField]
    private Button btnRoll;
    // Start is called before the first frame update
    public delegate void Event();
    public Event onClickEnter;
    void Start()
    {
        //add listener
        btnRoll.onClick.AddListener(OnRollClick);



    }
    void OnRollClick()
    {
        onClickEnter?.Invoke();
    }
}
