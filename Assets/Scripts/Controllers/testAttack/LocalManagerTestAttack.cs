using System.Collections;
using System.Collections.Generic;
using Socket.Quobject.SocketIoClientDotNet.Client;
using UnityEngine;

public class LocalManagerTestAttack : MonoBehaviour
{

    [SerializeField]
    private SocketIOTestAttack _socketIOTestAttack;

    [SerializeField]
    private UIManagerTestAttack _UIManagerTestAttack;
    // Start is called before the first frame update
    void Start()
    {
        _UIManagerTestAttack.OnAttackButtonClicked += () =>
        {
        
            _socketIOTestAttack.Emit("attack");
        };
    }


}