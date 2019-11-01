using moe.heing.CodeConfig;
using moe.heing.UrlConfig;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine; 
public class TcpClient : MonoBehaviour {

    IPEndPoint endp;
    Socket clientSocket;
    const int maxn = 1024;
    Queue<MyJson> msgs = new Queue<MyJson>();
    public void sendMsg(int type, string msg)
    {
        int _playerid = MyPlayer.playerid;
        try
        {
            string sendJson = "{'playerid':" + _playerid + ", 'type':" + type +",'msg': '" + msg + "' }";

            sendJson = sendJson + "\n";
            byte[] buffer = Encoding.UTF8.GetBytes(sendJson);
            clientSocket.Send(buffer);
        } catch (Exception e)
        {
            
        }
        
    }
    private void HandleMyJson(MyJson myjson)
    {
        /*
        if (myjson.playerid == 1)
        {
            myjson.playerid = 2;
        } else
        {
            myjson.playerid = 1;
        }
        */
        MyPlayer.otherPlayerManager.HandleMsg(myjson);
    }
    private void ReceiveMsg()
    {
        while (true)
        {
            try
            {
                byte[] buffer = new byte[maxn * maxn];
                int n = clientSocket.Receive(buffer);
                if (n > 0)
                { 
                    string msg = Encoding.UTF8.GetString(buffer, 0, n);
                    Debug.Log("@" + msg);
                    MyJson myjson = new MyJson(msg);
                    msgs.Enqueue(myjson);
                }
            } catch (Exception e)
            {

            }
        }
    }
    private void Awake()
    {
        endp = new IPEndPoint(IPAddress.Parse(UrlConfig.SERVER_IP), UrlConfig.SERVER_TCP_PORT);
        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        clientSocket.Connect(endp);
        Thread receiveThread = new Thread(ReceiveMsg);
        receiveThread.Start();
        sendMsg(CodeConfig.TYPE_NEW_CLIENT, "client_connect");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (msgs.Count > 0)
        { 
            HandleMyJson((msgs.Dequeue()));
        }
    }
}
