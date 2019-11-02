using moe.heing.CodeConfig;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBarButtonClick : MonoBehaviour
{
    public void Refresh()
    {
        MyPlayer.tcpClient.sendMsg(CodeConfig.TYPE_NEW_CLIENT, "client_connect");
    }
    public void BackEntryScene()
    {
        MyPlayer.otherPlayerManager.uploadMyGameStatus(CodeConfig.TYPE_SAVE_UPLOAD);
        MyPlayer.tcpClient.sendMsg(CodeConfig.TYPE_CLIENT_EXIT, "client_exit");
        SceneManager.LoadSceneAsync("EntryScene");
    }
    private void Awake()
    {
        
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
