using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyType
{
    protected int id;
    protected string color;
    protected string description;

    public int Id
    {
        get
        {
            return id;
        }
        set
        {
            id = value;
        }
    }

    public string Color
    {
        get
        {
            return color;
        }
        set
        {
            color = value;
        }
    }

    public string Description
    {
        get
        {
            return description;
        }
        set
        {
            description = value;
        }
    }

}