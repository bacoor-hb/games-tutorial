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

    /// <summary>
    /// get owned plane
    /// </summary>
    /// 
    public bool isBought = false;
    /// <summary>
    /// Level Owned Properties
    /// </summary>
    public int level = -1;

    [HideInInspector]
    public int propertyId;

    void Start()
    {
        
    }   

    void Update()
    {
        
    }

    void OnMouseDown()
    {
        Debug.Log(data.description);
    }

    /// <summary>
    ///  Check property has owner
    /// </summary>
    /// <returns></returns>
    public bool IsCheckPropertyOwned()
    {
        return isBought;
    }

    /// <summary>
    /// Get price when user buy property
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Get price from Property from level property
    /// </summary>
    /// <returns></returns>
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
}
