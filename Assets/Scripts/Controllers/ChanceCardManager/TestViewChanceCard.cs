using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestViewChanceCard : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textChanceCard;

    [SerializeField]
    private TextMeshProUGUI textLuckyCard;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetChanceCard(int id)
    {
        textChanceCard.text = id.ToString();
    }


    public void SetLuckyCard(int id)
    {
        textLuckyCard.text = id.ToString();
    }
}
