using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using moe.heing.UrlConfig;

public class WsStart : MonoBehaviour
{

    // Start is called before the first frame update
    void Awake() {
        Debug.Log(UrlConfig.SERVER_ADDRESS);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
