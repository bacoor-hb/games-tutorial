using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Quobject.SocketIoClientDotNet.Client;

public class LocalManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        //var socket = IO.Socket("http://192.168.50.165:5000/users/lehoangvuvt/roll");
        //socket.On(Socket.EVENT_CONNECT, () =>
        //{
        //    socket.Emit("hi");

        //});
        Debug.Log(StaticDataApi.dataApi.username);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
