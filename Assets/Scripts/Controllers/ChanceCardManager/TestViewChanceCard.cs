using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestViewChanceCard : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI cardId;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetViewData(int id)
    {
        cardId.text = id.ToString();
    }
}
