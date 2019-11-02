using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
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
        StringBuilder SB = new StringBuilder();
        for (; i<j.Length; i++)
        {
            if (j[i] !=',')
            { 
                SB.Append(j[i]);
            } else
            {
                break;
            }
        }
        playerid = Convert.ToInt32(SB.ToString());

        for (; i < j.Length && j[i++] != ':';) ; 
        SB.Clear();
        for(; i<j.Length; i++)
        {
            if (j[i] != ',')
            { 
                SB.Append(j[i]);
            } else
            {
                break;
            }
        }
        type = Convert.ToInt32(SB.ToString());

        for (; i < j.Length && j[i++] != ':';) ; 
        SB.Clear();

        for (i += 2; i<j.Length; i++)
        { 
            if (j[i] != '\'')
            { 
                SB.Append(j[i]);
            } else
            {
                break;
            }
        }
        this.msg = SB.ToString();
        Debug.Log(this.ToString());
    } 

    public override
    string ToString()
    {
        return "Playerid=" + playerid + ", type=" + type + ", msg=" + msg;
    }
}
