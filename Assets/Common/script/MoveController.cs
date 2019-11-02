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
        // MyPlayer.otherPlayerManager.uploadMyGameStatus();
    }

    public void Down()
    {
        MyPlayer.myPlayer.GetComponent<Player>().down();
    }

    public void Jump()
    {
        MyPlayer.myPlayer.GetComponent<Player>().Jump();
        MyPlayer.tcpClient.sendMsg(CodeConfig.TYPE_JUMP, "");
    }
    void KeyControl()
    {
        float KeyVertical = Input.GetAxis("Vertical");
        float KeyHorizontal = Input.GetAxis("Horizontal");
        Debug.Log("keyVertical" + KeyVertical);
        Debug.Log("keyHorizontal" + KeyHorizontal);
        if (KeyVertical == -1)
        {
            Down(); //下 
        }
        else if (KeyVertical == 1)
        {
            Jump();  //上
        }
        if (KeyHorizontal == 1)
        {
            MoveHor(1); //右 
        }
        else if (KeyHorizontal == -1)
        {
            MoveHor(-1);  //左
        } else if (KeyHorizontal == 0)
        {
            MoveHor(0);
        } 
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // KeyControl();
    }
}
