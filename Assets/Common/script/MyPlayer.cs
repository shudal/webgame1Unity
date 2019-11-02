using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using moe.heing.CodeConfig;
using System;
using UnityEngine.UI;

public class MyPlayer : MonoBehaviour
{ 
    public static GameObject myPlayer; 
    public static OtherPlayerManager otherPlayerManager;
    public static TcpClient tcpClient;
    public static int playerid=1;
    public static bool loged = false;
    public static GameObject[] players;
    public static Text noticeText;
    public static Transform noticeTextTF;
    void Awake()
    {
        loged = false;
        this.SetMyPlayerFromId(playerid);
        this.SetOtherPlayerManager();


        GameObject TcpClientGO = GameObject.FindGameObjectWithTag("TcpClientGO");
        tcpClient = TcpClientGO.GetComponent<TcpClient>();

        GameObject noticeTextGO = GameObject.FindGameObjectWithTag("MyMsgLabel");
        noticeText = noticeTextGO.GetComponent<Text>();
        noticeTextTF = noticeTextGO.GetComponent<Transform>();
    }
    void SetOtherPlayerManager()
    {
        GameObject otherPlayerManagerGO = GameObject.FindWithTag("OtherPlayerManagerGO");
        otherPlayerManager = otherPlayerManagerGO.GetComponent<OtherPlayerManager>();

    }
    void SetMyPlayerFromId(int _playerid) {
        players = GameObject.FindGameObjectsWithTag("player"); 
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

    IEnumerator uploadTheGameMap()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            otherPlayerManager.uploadMyGameStatus();
        }
    }

    public static void showNoticeText(string s)
    {
        noticeText.text = s;
        noticeTextTF.localPosition = new Vector3((float)7.7, (float)378, 0);
        noticeTextTF.rotation = Quaternion.Euler(0, 0, 0);
    }
    void Start()
    {
        StartCoroutine(uploadTheGameMap());
    }

    // Update is called once per frame
    void Update()
    {

    }


} 
