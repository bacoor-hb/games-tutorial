using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChanceCardManager : MonoBehaviour
{
    [SerializeField]
    private List<ChanceCard> chanceCards;
    [SerializeField]
    private List<ChanceCard> luckyCards;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public ChanceCard GetRandomChanceCard()
    {
        return chanceCards[Random.Range(0, chanceCards.Count)];
    }

    public ChanceCard GetRandomLuckyCard()
    {
        return luckyCards[Random.Range(0, luckyCards.Count)];
    }
}
