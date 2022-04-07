using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestChanceCard : MonoBehaviour
{
    [SerializeField]
    private TestViewChanceCard ui;

    [SerializeField]
    private ChanceCardManager chanceCardManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            // bam k de rut the khi van
            ChanceCard card = chanceCardManager.GetRandomChanceCard();
            ui.SetChanceCard(card.Id);

        } else if (Input.GetKeyDown(KeyCode.C))
        {
            // bam c de rut the co hoi
            ChanceCard card = chanceCardManager.GetRandomLuckyCard();
            ui.SetLuckyCard(card.Id);
        }
    }
}
