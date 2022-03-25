using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyManager : MonoBehaviour
{
    [SerializeField]
    private List<Property> properties;
    [SerializeField]

    public List<Property> Properties
    {
        get
        {
            return properties;
        }
        set
        {
            properties = value;
        }
    }

    void Start()
    {       
    }

    void Update()
    {
    }



}
