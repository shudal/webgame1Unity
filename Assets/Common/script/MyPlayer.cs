using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using moe.heing.CodeConfig;
 

public class MyPlayer : MonoBehaviour
{ 
    public static GameObject myPlayer; 
    public static OtherPlayerManager otherPlayerManager;
    public static TcpClient tcpClient;
    public static int playerid=1;  
    void Awake()
    {
        this.SetMyPlayerFromId(playerid);
        this.SetOtherPlayerManager();


        GameObject TcpClientGO = GameObject.FindGameObjectWithTag("TcpClientGO");
        tcpClient = TcpClientGO.GetComponent<TcpClient>();
    }
    void SetOtherPlayerManager()
    {
        GameObject otherPlayerManagerGO = GameObject.FindWithTag("OtherPlayerManagerGO");
        otherPlayerManager = otherPlayerManagerGO.GetComponent<OtherPlayerManager>();

    }
    void SetMyPlayerFromId(int _playerid) {
        GameObject[] players = GameObject.FindGameObjectsWithTag("player"); 
        foreach (GameObject aplayer in players)
        {
            if (_playerid == aplayer.GetComponent<Player>().playerid)
            {
                Debug.Log("playerid=" + aplayer.GetComponent<Player>().playerid);
                Debug.Log("playerid in SetMyPlayerFromid=" + _playerid);
                Debug.Log("player name:" + aplayer.GetComponent<Player>().playerName);
                myPlayer = aplayer;
                Debug.Log("initial my player success");
                break;
            }
        }

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
} 
