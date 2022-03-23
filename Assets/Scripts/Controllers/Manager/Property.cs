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

    void Start()
    {
    }   

    void Update()
    {
   
    }
}
