using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChanceCardManager
{
    [SerializeField]
    private List<ChanceCard> chanceCards;
    [SerializeField]
    private List<ChanceCard> luckyCards;

    public ChanceCard GetRandomChanceCard()
    {
        return chanceCards[Random.Range(0, chanceCards.Count)];
    }

    public ChanceCard GetRandomLuckyCard()
    {
        return luckyCards[Random.Range(0, luckyCards.Count)];
    }
}
