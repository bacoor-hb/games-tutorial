using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
struct DiceForce
{
    public float x;
    public float y;
    public float z;
}

[Serializable]
struct DiceDataSet
{
    public List<DiceForce> diceForces1;
    public List<DiceForce> diceForces2;
    public List<DiceForce> diceForces3;
    public List<DiceForce> diceForces4;
    public List<DiceForce> diceForces5;
    public List<DiceForce> diceForces6;
}
