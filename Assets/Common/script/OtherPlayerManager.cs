using moe.heing.CodeConfig;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using System.Text;
using System.Threading;

public class OtherPlayerManager : MonoBehaviour
{
    public int uploadGameMapGap = 1;
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
    public void uploadMyGameStatus(int type = CodeConfig.TYPE_NORMAL_UPLOAD)
    {
        StringBuilder SB = new StringBuilder();
        foreach (GameObject aplyer in MyPlayer.players)
        {
            Transform pTransform = aplyer.GetComponent<Transform>();
            SB.Append(aplyer.GetComponent<Player>().playerid + "," + pTransform.position.x + "," + pTransform.position.y + "," + pTransform.localEulerAngles.z + "|");
        } 
        MyPlayer.tcpClient.sendMsg(CodeConfig.TYPE_UPLOAD_MY_MAP, SB.ToString(), type);
        
    }
    private void updateMyMap(String mapStr, bool setAll = false)
    {
        List<string> playerMapStrList = new List<string>(); 
        StringBuilder SB = new StringBuilder();
        Debug.Log(mapStr);
        for (int i=0; i<mapStr.Length; i++)
        {
            if (mapStr[i] != '|')
            { 
                SB.Append(mapStr[i]);
            } else
            {
                playerMapStrList.Add(SB.ToString());
                Debug.Log(SB.ToString());
                SB.Clear();
            }
        }
        
        foreach (string s in playerMapStrList)
        {
            Debug.Log("s1="  + s);
            int i = 0; 
            for (; s[i] != ','; i++)
            { 
                SB.Append(s[i]);
            }
          
            int pid = Convert.ToInt32(SB.ToString());
            SB.Clear(); 
            for(i++; s[i] != ','; i++)
            { 
                SB.Append(s[i]);
            }
            double x = Convert.ToDouble(SB.ToString());
            SB.Clear();
            for(i++; i<s.Length && s[i] != ','; i++)
            {
                SB.Append(s[i]);
                
            }
            double y = Convert.ToDouble(SB.ToString());
            SB.Clear();
            for (i++; i<s.Length; i++)
            {
                SB.Append(s[i]);
            } 
            double rz = Convert.ToDouble(SB.ToString());
            SB.Clear();
            setPlayerPosition(pid, (float)x, (float)y, (float)rz, setAll);
        }


    }
    public void setPlayerPosition(int pid, float x, float y, float rz, bool setAll = false)
    {
        Debug.Log("pid=" + pid + ",x=" + x + ",y=" + y + ",rz=" + rz);
        if ((!setAll) && pid == MyPlayer.playerid && MyPlayer.loged)
        {
            return;
        }
        GameObject playerObj = getPlayerObjFromId(pid);
        float z = playerObj.GetComponent<Transform>().position.z;
        playerObj.GetComponent<Transform>().position = new Vector3(x, y, z);
        playerObj.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, rz);
    }
    public void HandleMsg(MyJson myJson)
    { 
        if (myJson.playerid == MyPlayer.playerid)
        {
            if (myJson.type == CodeConfig.TYPE_NEW_CLIENT)
            {
                if (!MyPlayer.loged)
                {
                    MyPlayer.loged = true; 
                    MyPlayer.showNoticeText("登陆成功");
                }
            }
            return;

        } else if (myJson.playerid == CodeConfig.SERVER_PLAYER_ID)
        {
            switch(myJson.type)
            { 
                case CodeConfig.TYPE_UPDATE_MAP:
                    updateMyMap(myJson.msg, true);
                    break;
            } 
        } else
        {
            GameObject playerObj = getPlayerObjFromId(myJson.playerid);
            Player player = playerObj.GetComponent<Player>();
            switch (myJson.type)
            {
                case CodeConfig.TYPE_NEW_CLIENT:
                    Debug.Log("new client, playerid=" + myJson.playerid);
                    uploadMyGameStatus();
                    MyPlayer.showNoticeText(getPlayerObjFromId(myJson.playerid).name + "已上线");
                    break;
                case CodeConfig.TYPE_MOVE_HOR:
                    int _dir = Convert.ToInt32(myJson.msg);
                    player.Move(_dir);
                    break;
                case CodeConfig.TYPE_JUMP:
                    player.Jump();
                    break;
                case CodeConfig.TYPE_UPLOAD_MY_MAP:
                    updateMyMap(myJson.msg);
                    break;
                case CodeConfig.TYPE_CLIENT_EXIT:
                    MyPlayer.showNoticeText(getPlayerObjFromId(myJson.playerid).name + "已下线");
                    break;

            }
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
