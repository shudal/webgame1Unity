using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using moe.heing.CodeConfig; 

public class MoveController : MonoBehaviour
{ 
    

    void Awake() {
        
    }
    public void MoveHor(int _dir) { 
        MyPlayer.myPlayer.GetComponent<Player>().Move(_dir);
        MyPlayer.tcpClient.sendMsg(CodeConfig.TYPE_MOVE_HOR, _dir + "");
    }

    public void Jump()
    {
        MyPlayer.myPlayer.GetComponent<Player>().Jump();
        MyPlayer.tcpClient.sendMsg(CodeConfig.TYPE_JUMP, "");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
