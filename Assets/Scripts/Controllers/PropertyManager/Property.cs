using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Property : MonoBehaviour
{
    public delegate void OnEventCalled<T>(T data);
    public OnEventCalled<int> OnDestroyTime;

    [SerializeField]
    public PropertyData data;

    public List<MeshRenderer> renderers;
    public List<Collider> colliders;

    [HideInInspector]
    public int propertyId;
    public bool isBought=false;
    public int level = -1;




    void Start()
    {

    }

    void Update()
    {

    }
    // check property has owner
    public bool IsCheckPropertyOwned()
    {
        return isBought;
    }
    public bool IsLevelCorrect(int level)
    {
        if (level >= 0 && level <= 5)
        {
            return true;
        }
        return false;
    }
    // get price following level of property
    public int GetPriceBuyProperty()
    {
        int price = data.cost_house;
        switch (level)
        {
            case 0:

            case 1:

            case 2:

            case 3:
                break;
            case 4:
                price = data.cost_hotel;
                break;

        }
        return price;
    }

    public int GetPriceSellProperty()
    {
        int price = data.cost_house;
        switch (level)
        {
            case 0:
                price = data.cost;
                break;
            case 1:
                
            case 2:
                
            case 3:
                
            case 4:
                break;
            case 5:
                price = data.cost_hotel;
                break;

        }
        return price;
    }
    void OnMouseDown()
    {
        Debug.Log(data.description);
    }
}
