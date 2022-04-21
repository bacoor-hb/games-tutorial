using System.Collections;
using System.Collections.Generic;
using Socket.Quobject.SocketIoClientDotNet.Client;
using UnityEngine;
using Newtonsoft.Json;


public class SocketIOTestAttack : MonoBehaviour
{
    private QSocket socket;

    public delegate void Event(DataUserAttack dataUserAttack);
    public Event onUpdateUI;
    public DataUserAttack dataUserAttack;
    void Start()
    {
        socket = IO.Socket("http://192.168.50.165:3000");

        socket.On(QSocket.EVENT_CONNECT, () =>
        {
            Debug.Log("Connected");
        });
        socket.On("getData", (dataJson) =>
        {
            Debug.Log("getData");
            Debug.Log(dataJson);


             dataUserAttack = JsonConvert.DeserializeObject<DataUserAttack>(dataJson.ToString());

            UpdateUI(dataUserAttack);
        });
        socket.On("sendData", (data) =>
        {
            Debug.Log("updated");
            Debug.Log(data);
            dataUserAttack = JsonConvert.DeserializeObject<DataUserAttack>(data.ToString());

            //monster.transform.position = monster.transform.position + new Vector3(1 * 5f * Time.deltaTime, 1 * 5f * Time.deltaTime, 0);
            UpdateUI(dataUserAttack);

        });
    }
    private void OnDestroy()
    {
        socket.Disconnect();
    }
    public void Emit(string nameEmit)
    {
        socket.Emit(nameEmit);
        Debug.Log("emit " + nameEmit);

    }
    private void UpdateUI(DataUserAttack dataUserAttack)
    {
        onUpdateUI?.Invoke(dataUserAttack);
    }
}
