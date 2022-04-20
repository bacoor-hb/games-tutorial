using System.Collections;
using System.Collections.Generic;
using Socket.Quobject.SocketIoClientDotNet.Client;
using UnityEngine;

public class SocketIOTestAttack : MonoBehaviour
{
    private QSocket socket;

    void Start()
    {
        socket = IO.Socket("http://192.168.50.165:3000");

        socket.On(QSocket.EVENT_CONNECT, () =>
        {
            Debug.Log("Connected");
        });
        socket.On("getData", (data) =>
        {
            Debug.Log("getData");
            Debug.Log(data);
        });
        socket.On("sendData", (data) =>
        {
            Debug.Log("updated");
            //monster.transform.position = monster.transform.position + new Vector3(1 * 5f * Time.deltaTime, 1 * 5f * Time.deltaTime, 0);
        });
    }
    private void OnDestroy()
    {
        socket.Disconnect();
    }
    public void Emit(string nameEmit)
    {
        socket.Emit(nameEmit);
        Debug.Log("emit "+ nameEmit);
         
    }
}
