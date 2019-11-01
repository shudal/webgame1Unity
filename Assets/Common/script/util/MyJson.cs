using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyJson
{
    public int playerid;
    public int type;
    public string msg;
    public MyJson(MyJson _myJson)
    {
        playerid = _myJson.playerid;
        type = _myJson.type;
        msg = _myJson.msg;
    } 
    public MyJson(string j)
    {
        int i = 0;
        for (; i<j.Length && j[i++] != ':';);
        string idStr = "";
        for (; i<j.Length; i++)
        {
            if (j[i] !=',')
            {
                idStr += j[i];
            } else
            {
                break;
            }
        }
        playerid = Convert.ToInt32(idStr);

        for (; i < j.Length && j[i++] != ':';) ;
        string typeStr = "";
        for(; i<j.Length; i++)
        {
            if (j[i] != ',')
            {
                typeStr += j[i]; 
            } else
            {
                break;
            }
        }
        type = Convert.ToInt32(typeStr);

        for (; i < j.Length && j[i++] != ':';) ;
        msg = ""; 

        for (i += 2; i<j.Length; i++)
        { 
            if (j[i] != '\'')
            {
                msg += j[i];
            } else
            {
                break;
            }
        }
        Debug.Log(this.ToString());
    } 

    public override
    string ToString()
    {
        return "Playerid=" + playerid + ", type=" + type + ", msg=" + msg;
    }
}
