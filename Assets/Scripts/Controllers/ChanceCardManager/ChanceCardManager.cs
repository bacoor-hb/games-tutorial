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

    public void performChanceCard(ChanceCard card)
    {
        switch (card.Type)
        {
            case (int)CHANCE_CARD_TYPE.GET_CARD:
                if (card.Value1 > 0)
                {
                    GetRandomChanceCard();
                }
                else
                {
                    GetRandomLuckyCard();
                }
                break;
            case (int)CHANCE_CARD_TYPE.TO_A_POSITION:
                
                break;
            case (int)CHANCE_CARD_TYPE.PAY_RECEIVE_MONEY_AMOUNT:
                break;
            case (int)CHANCE_CARD_TYPE.PAY_REPAIR_HOME:
                break;
            case (int)CHANCE_CARD_TYPE.PRISON:
                break;
        }
    }
}
