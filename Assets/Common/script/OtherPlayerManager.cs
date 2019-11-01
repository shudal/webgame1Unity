using moe.heing.CodeConfig;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherPlayerManager : MonoBehaviour
{

    private GameObject getPlayerObjFromId(int _playerid)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("player");
        foreach (GameObject aplayer in players)
        {
            if (_playerid == aplayer.GetComponent<Player>().playerid)
            {
                return aplayer; 
            }
        }
        return null;
    }
    private void uploadMyGameStatus()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("player");
        string s = "";
        foreach (GameObject aplyer in players)
        {
            int pid = aplyer.GetComponent<Player>().playerid;
            Transform pTransform = aplyer.GetComponent<Transform>();
            float x = pTransform.position.x;
            float y = pTransform.position.y;

            s = s + pid + "," + x + "," + y + "|";
        }
        MyPlayer.tcpClient.sendMsg(CodeConfig.TYPE_UPLOAD_MY_MAP, s);
    }
    private void updateMyMap(String mapStr)
    {
        List<string> playerMapStrList = new List<string>();
        string s1 = "";
        for (int i=0; i<mapStr.Length; i++)
        {
            if (mapStr[i] != '|')
            {
                s1 += mapStr[i];
            } else
            {
                s1 = "";
                playerMapStrList.Add(s1);
            }
        }
        foreach (string s in playerMapStrList)
        {
            int i = 0;
            string idStr = "";
            for (; s[i] != ','; i++)
            {
                idStr += s[i];
            }
            int pid = Convert.ToInt32(idStr);
            string xStr = "";
            for(; s[i] != ','; i++)
            {
                xStr += s[i];
            }
            double x = Convert.ToDouble(xStr);
            string yStr = "";
            for(; i<s.Length; i++)
            {
                yStr += s[i];
            }
            double y = Convert.ToDouble(yStr);
            setPlayerPosition(pid, (float)x, (float)y);
        }


    }
    public void setPlayerPosition(int pid, float x, float y)
    {
        GameObject playerObj = getPlayerObjFromId(pid);
        float z = playerObj.GetComponent<Transform>().position.z;
        playerObj.GetComponent<Transform>().position.Set(x, y, z);
    }
    public void HandleMsg(MyJson myJson)
    { 
        if (myJson.playerid == MyPlayer.playerid)
        {
            return;
        }
        GameObject playerObj = getPlayerObjFromId(myJson.playerid);
        Player player = playerObj.GetComponent<Player>();
        switch(myJson.type)
        {
            case CodeConfig.TYPE_NEW_CLIENT:
                Debug.Log("new client, playerid=" + myJson.playerid);
                uploadMyGameStatus();
                break;
            case CodeConfig.TYPE_MOVE_HOR:
                int _dir = Convert.ToInt32(myJson.msg);
                player.Move(_dir);
                break;
            case CodeConfig.TYPE_JUMP:
                player.Jump();
                break;
            case CodeConfig.TYPE_UPDATE_MAP:
                updateMyMap(myJson.msg);
                break;  
        }
    }
    private void Awake()
    {
        
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
