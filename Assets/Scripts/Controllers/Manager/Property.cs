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
    protected bool isBought;
    protected int status;

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
    private void OnCollisionEnter(Collision collision)
    {
        var name = collision.gameObject.GetComponent<UserManager>().user.Name;
        Debug.Log(name);
    }
}
