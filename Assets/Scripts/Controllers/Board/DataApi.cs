using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DataPet
{

    public string name { get; set; }
    public int health { get; set; }
    public int attack { get; set; }
    public int defense { get; set; }
    public string image { get; set; }

    public DataPet(string name, int health, int attack, int defense, string image)
    {
        this.name = name;
        this.health = health;
        this.attack = attack;
        this.defense = defense;
        this.image = image;
    }

}
public class DataApi
{
    public string username { get; set; }
    public DataPet[] pets { get; set; }
    public int currentPos { get; set; }
    public int diceValue { get; set; }

    public DataApi(string username, DataPet[] pets, int currentPos, int  diceValue)
    {
        this.username = username;
        this.pets = pets;
        this.currentPos = currentPos;
        this.diceValue = diceValue;
    }
}
public static class StaticDataApi
{
    public static DataApi dataApi { get; set; }
}
