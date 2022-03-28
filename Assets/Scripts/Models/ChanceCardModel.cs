using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Scriptable Objects/Opportunity Card", order = 52)]
public class ChanceCard : ScriptableObject
{
    [Header("Model Chance Card")]
    [Tooltip("Prefab to be Instantiate on the Scene")]
    public GameObject Model;

    [Header("Properties")]
    [Tooltip("Card Name")]
    public string Name;

    [Tooltip("Card id")]
    public int Id;

    [Tooltip("Card type")]
    [Range(0, 4)]
    public int Type;

    [Tooltip("Card Value1")]
    public int Value1;

    [Tooltip("Card Value2")]
    public string Value2;
}
