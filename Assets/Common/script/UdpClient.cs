using moe.heing.UrlConfig;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class UdpClient : MonoBehaviour
{
    // Start is called before the first frame update
    public string recvStr;
    public string UDPClientIP; 
    private int playerid = MyPlayer.playerid;
    public string msg;
    Socket socket;
    IPEndPoint ipEnd;

    const int maxn = 1024;
    byte[] sendData = new byte[maxn];
    private void Awake()
    { 
        msg = "{'playerid':" + System.Convert.ToString(playerid) + ",'op':0 }";
        UDPClientIP = UrlConfig.SERVER_IP.Trim();
        InitSocket();
    }
    void InitSocket()
    {
        ipEnd = new IPEndPoint(IPAddress.Parse(UDPClientIP), UrlConfig.SERVER_UDP_PORT);
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        SocketSend(msg); 
    }
    void SocketSend(string msg)
    {
        sendData = new byte[maxn];
        Debug.Log("msg=" + msg);
        sendData = Encoding.UTF8.GetBytes(msg);
        socket.SendTo(sendData, sendData.Length, SocketFlags.None, ipEnd);
    } 
    void SocketQuit()
    { 
        if (socket != null)
            socket.Close();
    }
    void OnApplicationQuit()
    {
        SocketQuit();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
