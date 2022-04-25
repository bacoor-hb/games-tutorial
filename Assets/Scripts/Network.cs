using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Colyseus;
using Colyseus.Schema;
using GameDevWare.Serialization;

class Metadata
{
    public string name;
}

class RoomListingData
{
    public double clients;
    public bool locked;
    public bool isPrivate;
    public double maxClients;
    public Metadata metadata;
    public string name;
    public string processId;
    public string roomId;
    public bool unlisted;
}

public class Network : MonoBehaviour
{
    [SerializeField]
    private Button joinRoomButton;
    [SerializeField]
    private Button sendMsgButton;
    [SerializeField]
    private InputField inputField;
    public string roomType = "chatroom";
    private string username;
    private int color;
    protected Client client;
    protected Room<State> room;
    private bool isJoined = false;
    protected Room<IndexedDictionary<string, object>> lobbyRoom;

    // Start is called before the first frame update
    async void Start()
    {
        inputField.onEndEdit.AddListener((fieldValue) =>
        {
            Debug.Log(fieldValue);
        });
        joinRoomButton.GetComponentInChildren<Text>().text = "Join lobby";
        joinRoomButton.onClick.AddListener(() => { JoinLobby(); });
        sendMsgButton.onClick.AddListener(() => { SendMessage(); });
        Connect();
    }



    void Connect()
    {
        string endpoint = "ws://5bf9-115-79-195-10.ngrok.io";
        //string endpoint = "ws://vps735892.ovh.net:2567";
        Debug.Log("Connecting to " + endpoint);
        client = ColyseusManager.Instance.CreateClient(endpoint);
    }
        
    public async void JoinOrLeaveRoom()
    {
        if (!isJoined)
        {
            var options = new Dictionary<string, object>();
            options.Add("authorization", new { token = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhZGRyZXNzIjoiMHg3ZTFiYTAyOWYwZDJmYjM4MzNmOGQ3ZjI5MWVmN2U0NTE2MDM3Mjg5IiwiaWF0IjoxNjUwNTM2NTY5LCJleHAiOjE5NjYxMTI1Njl9.TCWBSo-gK72h1kl2EPEE9gbShPODvE4MgEeNV0rM0Xg" });
            room = await client.JoinOrCreate<State>("my_room", options);
            Debug.Log("Join room");
            RegisterRoomHandlers();
            isJoined = true;
            joinRoomButton.GetComponentInChildren<Text>().text = "Left room";
        } else
        {
            await room.Leave();
            Debug.Log("On leave");
            isJoined = false;
            joinRoomButton.GetComponentInChildren<Text>().text = "Join room";
        }
    }

    public async void JoinLobby()
    {
        string token = "123";
        var options = new Dictionary<string, object>();
        options.Add("authorization", new { token = token });
        lobbyRoom = await client.JoinOrCreate<IndexedDictionary<string, object>>("lobby", options);
        lobbyRoom.OnMessage("rooms", (RoomListingData[] rooms) =>
        {
            Debug.Log(rooms);
        });
        CreateRoom();
    }

    public async void CreateRoom()
    {
        var options = new Dictionary<string, object>();
        options.Add("authorization", new { token = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhZGRyZXNzIjoiMHg3ZTFiYTAyOWYwZDJmYjM4MzNmOGQ3ZjI5MWVmN2U0NTE2MDM3Mjg5IiwiaWF0IjoxNjUwNTM2NTY5LCJleHAiOjE5NjYxMTI1Njl9.TCWBSo-gK72h1kl2EPEE9gbShPODvE4MgEeNV0rM0Xg" });
        room = await client.JoinOrCreate<State>("my_room", options);
        RegisterRoomHandlers();
        Debug.Log("Success");
    }

    public async void SendMessage()
    {
        await room.Send("message", "New Message");
    }

    public void RegisterRoomHandlers()
    {
        room.OnMessage<string>("messages", (data) =>
       {
           Debug.Log("Message received: ");
           Debug.Log(data);
       });
        room.OnError += (code, message) => Debug.LogError("ERROR, code =>" + code + ", message => " + message);
        room.OnJoin += () => Debug.Log("ROOM: ON JOIN");
        room.OnLeave += (code) => Debug.Log("ROOM: ON LEAVE");
    }

    void Update()
    {
        
    }

    async void SendWebSocketMessage()
    {
       
    }

    private async void OnApplicationQuit()
    {
        
    }
}