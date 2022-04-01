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
}
