using System;
using System.Collections.Generic;

[Serializable]
public class DiceDataSet
{
    public List<DiceForce> diceForces1;
    public List<DiceForce> diceForces2;
    public List<DiceForce> diceForces3;
    public List<DiceForce> diceForces4;
    public List<DiceForce> diceForces5;
    public List<DiceForce> diceForces6;
}

[Serializable]
public class DiceForce
{
    public float x;
    public float y;
    public float z;

    public DiceForce(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
}