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
    public void sendMsg(int type, string msg, int opType = CodeConfig.TYPE_NORMAL_UPLOAD)
    {
        new Thread(() =>
        {
            int _playerid = MyPlayer.playerid;
            try
            {
                StringBuilder SB = new StringBuilder();
                if (type == CodeConfig.TYPE_UPLOAD_MY_MAP)
                {
                    if (opType == CodeConfig.TYPE_SAVE_UPLOAD)
                    {
                        SB.Append(CodeConfig.OP_SAVE_GAME);
                    }
                } else if (type == CodeConfig.TYPE_NEW_CLIENT)
                {
                    SB.Append(CodeConfig.OP_NEW_LOGIN);
                }
                SB.Append("{'playerid':");
                SB.Append(_playerid);
                SB.Append(", 'type':");
                SB.Append(type);
                SB.Append(",'msg': '");
                SB.Append(msg);
                SB.Append("' }");
                SB.Append("\n"); 
                byte[] buffer = Encoding.UTF8.GetBytes(SB.ToString());
                clientSocket.Send(buffer);
            }
            catch (Exception e)
            {

            }
        }).Start();
        
        
    }
    private void HandleMyJson(MyJson myjson)
    {
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
