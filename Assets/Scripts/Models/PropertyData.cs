using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Property Data", menuName = "Scriptable Objects/New Property Data", order = 51)]
public class PropertyData : ScriptableObject
{
    [Header("Model Prefab")]
    [Tooltip("Prefab to be Instantiate on the Scene")]
    public GameObject Model;

    [Header("Properties")]
    [Tooltip("Property ID")]
    public string id;
    [Tooltip("Property Name")]
    public string property_name;
    [Tooltip("Description")]
    public string description;
    [Tooltip("Type ID")]
    public int typeId;
    [Tooltip("Cost")]
    public int cost;
    [Tooltip("Cost rent")]
    public int cost_rent;
    [Tooltip("Cost when rent full color")]
    public int cost_rent_full;
    [Tooltip("Cost when rent with 1 house")]
    public int cost_rent_one_house;
    [Tooltip("Cost when rent with 2 houses")]
    public int cost_rent_two_houses;
    [Tooltip("Cost when rent with 3 houses")]
    public int cost_rent_three_houses;
    [Tooltip("Cost when rent with 4 houses")]
    public int cost_rent_four_houses;
    [Tooltip("Cost when rent with hotel")]
    public int cost_rent_hotel;
    [Tooltip("House cost")]
    public int cost_house;
    [Tooltip("Hotel cost")]
    public int cost_hotel;
    [Tooltip("Bid")]
    public int cost_bid;
}
