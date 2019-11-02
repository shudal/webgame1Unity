using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using moe.heing.CodeConfig;

public class Player : MonoBehaviour
{
    

    public Transform playerTransform;

    public int playerid;
    public int horDirection = 0;
    public int lastHorDir = 0;
    public string playerName;
    public Transform bottomTransformForStand;
    public int yForce = 350;

    [Header("侦测地板的射线起点")]
    private Transform groundCheck;
    private Transform groundCheck2;

    public bool isgroud;
    [Header("地面图册")]
    public LayerMask groundLayer;
    [Header("感应地板的距离")]
    [Range(0, 5)]
    public float distance;

    private const int m_times = 6;
    private Rigidbody2D myRigidbody2D;

    // 水平移动
    public void Move(int _direction) {
        horDirection = _direction;
        if (_direction != 0)
        {
            lastHorDir = _direction; 
        }
    }

    public void down()
    { 
        if (lastHorDir < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        } else {
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }
    }
    public void Jump()
    {
        myRigidbody2D.velocity = Vector2.zero;
        if (!(transform.rotation.eulerAngles.z >= -20 && transform.rotation.eulerAngles.z <= 20))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        myRigidbody2D.AddForce(Vector2.up * yForce);
       
    }

    private void IsGround()
    {
        bool grounded;
        bool grounded2;

        Vector2 start = groundCheck.position;
        Vector2 end = new Vector2(start.x, start.y - distance);

        grounded = Physics2D.Linecast(start, end, groundLayer);

        start = groundCheck2.position;
        end = new Vector2(start.x, start.y - distance);

        grounded2 = Physics2D.Linecast(start, end, groundLayer);

        if (grounded || grounded2)
        {
            isgroud = true;
        }
        else
        {
            isgroud = false;
        }
    }
    IEnumerator standGood()
    {
        while (true)
        {
            if (!(transform.rotation.eulerAngles.z >= -20 && transform.rotation.eulerAngles.z <= 20))
            {
                yield return new WaitForSeconds(0.5f);
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }
    void Awake() {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        groundCheck = transform.Find("detect_ground_1");
        groundCheck2 = transform.Find("detect_ground_2");

        //StartCoroutine(standGood());
    }

    // Update is called once per frame
    void Update()
    {
        IsGround();

        if (horDirection != 0) {
            transform.Translate(horDirection * m_times * Time.deltaTime, 0, 0);  
        }

        myRigidbody2D.centerOfMass = bottomTransformForStand.localPosition;
        
    }
}
